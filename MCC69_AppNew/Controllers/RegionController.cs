using MCC69_AppNew.Models;
using MCC69_AppNew.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MCC69_AppNew.Controllers
{
    public class RegionController : Controller
    {
        public async Task<IActionResult> Index()
        {
            Json<Region> regionlist = new Json<Region>();
            using(var httpClient = new HttpClient())
            {
                using(var response = await httpClient.GetAsync("https://localhost:44374/api/Region"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    regionlist = JsonConvert.DeserializeObject<Json<Region>>(apiResponse);
                }
            }

            return View(regionlist);
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(Region region)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44374/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Region>("student", region);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(region);
        }

        public ActionResult Edit(int id)
        {
            Region region = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44374/api");
                //HTTP GET
                var responseTask = client.GetAsync("Region/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Region>();
                    readTask.Wait();

                    region = readTask.Result;
                }
            }

            return View(region);
        }

        [HttpPost]
        public ActionResult Edit(Region region)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44374/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Region>("Region", region);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(region);
        }

        public IActionResult Delete(int id)
        {
            Region regions = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44374/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Region/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    // Get the response
                    var ResultJsonString = result.Content.ReadAsStringAsync();
                    ResultJsonString.Wait();
                    JObject rss = JObject.Parse(ResultJsonString.Result);
                    JObject data = (JObject)rss["data"];
                    regions = JsonConvert.DeserializeObject<Region>(JsonConvert.SerializeObject(data));
                }
                else //web api sent error response 
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(regions);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Region regions)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44374/api/");
                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Regions/" + regions.Id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        


        }

    }
}
