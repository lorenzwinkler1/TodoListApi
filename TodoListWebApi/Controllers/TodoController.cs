using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoClassLibEF;
using TodoClassLib;

namespace TodoListWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        readonly ITodoRepository repository;
        public TodoController(ITodoRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Todo
        [HttpGet]
        public ActionResult<Todo[]> Get()
        {
            return this.repository.ReadAll().ToArray();
        }

        // GET: api/Todo/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Todo> Get(int id)
        {
            var todo = this.repository.Read(id);
            if (todo == null)
                return NotFound();
            return todo;
        }

        // POST: api/Todo
        [HttpPost]
        public ActionResult<Todo> Post([FromBody] Todo value)
        {
            if (value.Id != 0)
                return BadRequest();
            else if (this.repository.Create(value))
            {
                return value;
            }
            return BadRequest();
        }

        // POST: api/Todo
        [HttpPost("array")]
        public ActionResult<IEnumerable<Todo>> Post([FromBody] Todo[] values)
        {
            for (int i = 0; i < values.Count(); i++)
            {
                var value = values[i];
                if (value.Id != 0)
                    return BadRequest();
                else if (!this.repository.Create(value, i == values.Count() - 1))
                {
                    return BadRequest();
                }
            }
            return values;
        }

        // PUT: api/Todo/5
        [HttpPut]
        public ActionResult<Todo> Put([FromBody] Todo value)
        {
            if (this.repository.Read(value.Id) == null)
                return NotFound();
            if (this.repository.Update(value))
                return value;
            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (this.repository.Read(id) == null)
                return NotFound();
            this.repository.Delete(id);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("array")]
        public ActionResult Delete([FromBody]int[] ids)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                var id = ids[i];
                if (this.repository.Read(id) == null)
                    return NotFound();
                this.repository.Delete(id, i == ids.Length - 1);
            }
            return Ok();
        }
    }
}
