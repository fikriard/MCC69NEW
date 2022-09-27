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
    public class JobHistoryController : ControllerBase
    {
        JobHistoryRepository jobHistoryRepository;

        public JobHistoryController(JobHistoryRepository jobHistoryRepository)
        {
            this.jobHistoryRepository = jobHistoryRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var data = jobHistoryRepository.Get();
            return Ok(new { status = 200, data = data });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var region = jobHistoryRepository.GetId(id);
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
        public IActionResult Post(JobHistory jobHistory)
        {
            var result = jobHistoryRepository.Post(jobHistory);
            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Created" });// returning the detail of item that just created
            return BadRequest();
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, JobHistory jobHistory)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = jobHistoryRepository.Put(id, jobHistory);

            // updating the data

            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Update" });
            else if (result == -1)
                return NotFound();
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id, JobHistory jobHistory)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = jobHistoryRepository.Delete(id);
            if (result > 0)
                return Ok(new { status = 200, message = "data deleted successfully" });
            return BadRequest();
        }
    }
}
