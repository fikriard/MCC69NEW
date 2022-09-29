using API.Context;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        EmployeesRepository employeesRepository;
        public EmployeesController(EmployeesRepository employeesRepository)
        {
            this.employeesRepository = employeesRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = employeesRepository.Get();
            return Ok(new { status = 200, data = data });
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var region = employeesRepository.GetId(id);
            if (region != null)
            {
                return Ok(new { status = 200, data = region });
            }
            else
            {
                return NotFound(); // 404 not found if the region is not found
            }
        }

        [HttpPost]
        public IActionResult Post(Employees employees)
        {
            var result = employeesRepository.Post(employees);
            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Created" });
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Employees employees)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = employeesRepository.Put(id, employees);

            // updating the data
            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Update" });
            else if (result == -1)
                return NotFound();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id, Employees employees)
        {
            if(id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = employeesRepository.Delete(id);
            if (result > 0)
                return Ok(new { status = 200, message = "data deleted successfully" });
            return BadRequest();
        }
    }
}
