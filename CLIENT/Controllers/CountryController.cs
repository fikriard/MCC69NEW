using API.Models;
using CLIENT.Base;
using CLIENT.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLIENT.Controllers
{
    public class CountryController : BaseController<Countries, CountryRepository>
    {
        public CountryController(CountryRepository countryRepository) : base(countryRepository)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
