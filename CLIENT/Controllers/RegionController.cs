﻿using API.Models;
using CLIENT.Base;
using CLIENT.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CLIENT.Controllers
{
    public class RegionController : BaseController<Region, RegionRepository>
    {
        public RegionController(RegionRepository repository) : base(repository)
        {

        }
       /* public async Task<IActionResult> Index()
        {
            var region = await Get();
            return View(region.AsEnumerable());
        }*/

        //CREATE
        public IActionResult Index()
        {
            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Region region)
        {
            var result = Post(region);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        //EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var result = await Get(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Region region)
        {
            var result = Put(id, region);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        //DELETE
        public IActionResult Delete(int id)
        {
            var result = Get(id);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Region region)
        {
            var result = DeleteEntity(region.Id);
            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }*/


    }
}
