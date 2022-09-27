using API.Models;
using CLIENT.Base;
using CLIENT.Repositories.Data;
using CLIENT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLIENT.Controllers
{
    public class CountryController : BaseController<Countries, CountryRepository>
    {
        public CountryController(CountryRepository repository) : base(repository)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
