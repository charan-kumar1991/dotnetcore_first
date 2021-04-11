using DotnetCoreFirst.Interfaces;
using DotnetCoreFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository repository;
        public TodoController(ITodoRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            IEnumerable<TodoItem> items = await repository.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            TodoItem item = await repository.GetAsync(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                     ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                );
            }

            if (await repository.ExistsAsync(item.Title))
            {
                return Conflict(new
                {
                    message = $"Todo item with same title ${item.Title} already present!"
                });
            }

            TodoItem newItem = new TodoItem(Guid.NewGuid(), item.Title, item.Completed);
            await repository.AddAsync(newItem);

            // HATEOAS
            return Created($"http://localhost:5000/api/Todo/{newItem.Id}", newItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute]Guid id, [FromBody] TodoItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                     ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
                );
            }

            await repository.UpdateAsync(id, item);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
