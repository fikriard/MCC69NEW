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
    public class CountryController : ControllerBase
    {
        CountryRepository countryRepository;
        
        public CountryController(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = countryRepository.Get(); 
            return Ok(new { status = 200, data = data });
        }

        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var country = countryRepository.GetId(id);
            if (country != null)
            {
                return Ok(new { status = 200, data = country });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post(Countries country)
        {
            var result = countryRepository.Post(country);
                if (result > 0)
                    return Ok(new { result = 200, message = "Succesfuly Created" });
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,Countries countries)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = countryRepository.Put(id, countries);
            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Update" });
            else if (result == -1)
                return NotFound();
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = countryRepository.Delete(id);
            if (result > 0)
                return Ok(new { status = 200, message = "data deleted successfully" });
            else if (result == -1)
                return NotFound();
            return BadRequest();
        }
    }
}
