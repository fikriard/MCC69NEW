using MCC69_AppNew.Models;
using MCC69_AppNew.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MCC69_AppNew.Controllers
{
    public class CountriesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            Json<Countries> countrieslist = new Json<Countries>();
            using (var httpClient = new HttpClient())
            {
                //tempat Token
                /*string token = HttpContext.Session.GetString("token");
                if (token == null)
                {
                    return View("UnAuthorize");
                }*/
               /* httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);*/


                using (var response = await httpClient.GetAsync("https://localhost:44374/api/Country"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    countrieslist = JsonConvert.DeserializeObject<Json<Countries>>(apiResponse);
                }
            }

            return View(countrieslist.data);
        }

        
    }
}
