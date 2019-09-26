using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TodoApp.API.Models;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[assembly: ApiConventionType(typeof(DefaultApiConventions))] //I did not go in a deep research on this, but custome conventions can be created
namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        // USING ENTITY IN ALMOST ALL CASES
        // A GOOD IMPROVEMENT IT WOULD BE THE USE OF VIEWMODELS AND AUTOMAPPER
        // ^^^^^^^^^^^^
        private ITODOService _toDoService { get; set; }

        public ToDoController(ITODOService toDoService)
        {
            _toDoService = toDoService;
        }
        // GET: api/ToDo
        [HttpGet]
        //[ProducesDefaultResponseType]
        //[ProducesResponseType(StatusCodes.Status200OK)] //>>>>>>> not necessary here, due DefaultApiConventions <<<<<<<<<
        public async Task<ActionResult<ICollection<TODO>>> GetToDo(string filterBy = "", string value = "") //case sensitive
        {
            try
            {
                Type myType = typeof(TODO);
                PropertyInfo propertyInfo = myType.GetProperty(filterBy.ToString());
                string propertyName = propertyInfo != null ? propertyInfo.Name : "";

                if ((!string.IsNullOrEmpty(filterBy) && propertyInfo is null)
                    || (!string.IsNullOrEmpty(filterBy) && string.IsNullOrEmpty(value))
                    || (string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(value)))
                {
                    return BadRequest("Incorrect filter values");
                }

                return await _toDoService.GetAllAsync(propertyName, value); // line 33 of TODOService to check this function
            }
            catch (FormatException e)
            {
                return BadRequest(e.Message.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: api/ToDo/5
        //[HttpGet("{id}", Name = "Get")] 
        [HttpGet("{id}")]
        public async Task<ActionResult<TODO>> GetToDo(int id)
        {
            var itemToDo = await _toDoService.GetByIdAsync(id);

            if (itemToDo == null)
            {
                return NotFound();
            }

            return itemToDo;
        }

        // POST: api/ToDo
        [HttpPost]
        public async Task<ActionResult<TODO>> PostToDo(TODO itemToDo)
        {
            await _toDoService.CreateAsync(itemToDo);

            return CreatedAtAction("GetToDo", new { id = itemToDo.Id }, itemToDo);
        }

        ///TEEEESSTTTT
        [HttpPost("PostFile")]
        public async Task<ActionResult<TODO>> PostFile([FromForm] TodoModel itemToDo)
        {
            var todo = new TODO();
            todo.Id = itemToDo.Id;
            todo.Description = itemToDo.Description;
            todo.StatusId = itemToDo.StatusId;

            //DO SOMETHING WITH THE FILE
            //var filePath = Path.GetTempFileName();
            //using (var stream = new FileStream(filePath, FileMode.Create))
            //{
            //    await itemToDo.File.CopyToAsync(stream);
            //}

            await _toDoService.CreateAsync(todo);

            return CreatedAtAction("GetToDo", new { id = itemToDo.Id }, itemToDo);
        }

        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo(int id, TODO itemToDo)
        {
            if (id != itemToDo.Id)
            {
                return BadRequest();
            }

            try
            {
                await _toDoService.UpdateAsync(itemToDo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _toDoService.ExistAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //// PUT: api/ToDoUpdate/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUpdateToDoStatus(int id, int statusId) //una mejora seria utilizar DTOs o ViewModels y mapearlos con automapper en lugar de utilizar la Entidad
        //{
        //    if (!await _toDoService.ExistAsync(id))
        //    {
        //        return NotFound();
        //    }
        //    await _toDoService.UpdateStatusAsync(id, statusId);

        //    return NoContent();
        //}

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TODO>> DeleteToDo(int id)
        {
            var itemToDo = await _toDoService.GetByIdAsync(id);

            if (itemToDo == null)
            {
                return NotFound();
            }
            await _toDoService.DeleteAsync(itemToDo.Id);

            return itemToDo;
        }
    }
}
