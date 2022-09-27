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
    public class LocationController : ControllerBase
    {
        LocationRepository locationRepository;
        public LocationController(LocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = locationRepository.Get();
            return Ok(new { status = 200, data = data });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var locations = locationRepository.GetId(id);
            if (locations != null)
            {
                return Ok(new { status = 200, data = locations });
            }
            else
            {
                return NotFound(); // 404 not found if the region is not found
            }
        }

        [HttpPost]
        public IActionResult Post(Locations location)
        {
            var result = locationRepository.Post(location);
            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Created" });// returning the detail of item that just created
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Locations locations)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = locationRepository.Put(id, locations);
            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Update" });
            else if (result == -1)
                return NotFound();
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id, Locations locations)
        {
            if (id == 0 || string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = locationRepository.Delete(id);
            if (result > 0)
                return Ok(new { status = 200, message = "data deleted successfully" });
            else if (result == -1)
                return NotFound();
            return BadRequest();
        }
    }
}
