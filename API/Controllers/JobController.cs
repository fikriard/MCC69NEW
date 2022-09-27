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
    public class JobController : ControllerBase
    {
        JobsRepository jobsRepository;

        public JobController(JobsRepository jobsRepository)
        {
            this.jobsRepository = jobsRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = jobsRepository.Get();
            return Ok(new { status = 200, data = data });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var jobs = jobsRepository.GetId(id);
            if (jobs != null)
            {
                return Ok(new { status = 200, data = jobs });
            }
            else
            {
                return NotFound(); 
            }
        }

        [HttpPost]
        public IActionResult Post(Jobs jobs)
        {
            var result = jobsRepository.Post(jobs);
            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Created" });
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Jobs jobs)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = jobsRepository.Put(id, jobs);

            // updating the data

            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Update" });
            else if (result == -1)
                return NotFound();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id, Jobs jobs)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = jobsRepository.Delete(id);
            if (result > 0)
                return Ok(new { status = 200, message = "data deleted successfully" });
            return BadRequest();
        }
    }
}
