using API.Context;
using API.Models;
using API.Repositories.Data;
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
    public class DepartmentController : ControllerBase
    {
        DepartmentRepository departmentRepository;
        public DepartmentController(DepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = departmentRepository.Get();
            return Ok(new { status = 200, data = data });
        }

        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var department = departmentRepository.GetId(id);
            if (department != null)
            {
                return Ok(new { status = 200, data = department });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post(Departments department)
        {
            var result = departmentRepository.Post(department);
            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Created" });// returning the detail of item that just created
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Departments department)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = departmentRepository.Put(id, department);

            // updating the data
            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Update" });
            else if (result == -1)
                return NotFound();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id, Departments department)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = departmentRepository.Delete(id);
            if (result > 0)
                return Ok(new { status = 200, message = "data deleted successfully" });
            return BadRequest();
        }
    }
}
