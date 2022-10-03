using API.Models;
using CLIENT.Base;
using CLIENT.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLIENT.Controllers
{
    [Authorize]
    public class EmployeesController : BaseController<Employees , EmployeesRepository>
    {
        public EmployeesController(EmployeesRepository employeesRepository) :base(employeesRepository)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
