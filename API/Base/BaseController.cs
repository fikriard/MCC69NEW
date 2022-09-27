using API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Repository,Entity,Primary> : ControllerBase
        where Entity : class
        where Repository : IGeneralRepository<Entity, Primary>
    {
        Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = repository.Get();
            return Ok(new { status = 200, data = data });
        }

        [HttpGet("{id}")]
        public IActionResult Get(Primary id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var data = repository.GetId(id);
            if (data != null)
            {
                return Ok(new { status = 200, data = data });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post(Entity entity)
        {
            var result = repository.Post(entity);
            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Created" });
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Primary id, Entity entity)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = repository.Put(id, entity);
            if (result > 0)
                return Ok(new { result = 200, message = "Succesfuly Update" });
            
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Primary id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return BadRequest();
            }
            var result = repository.Delete(id);
            if (result > 0)
                return Ok(new { status = 200, message = "data deleted successfully" });
            return BadRequest();
        }
    }
}
