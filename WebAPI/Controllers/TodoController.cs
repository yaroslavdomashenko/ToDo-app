using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.Entities;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Returns users task by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Returns the users task</response>
        /// <response code="404">If the task not found</response>       
        [Authorize]
        [HttpGet("task/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ControllerResponse<TodoTask>>> Get(int id)
        {
            ControllerResponse<TodoTask> response = new ControllerResponse<TodoTask>();

            var service = await _todoService.Get(User.Identity.Name, id);
            if(service == null){
                response.Message = "Task didn't find";
                return NotFound(response);
            }

            response.Data = service;
            response.Message = "Success";
            return Ok(response);
        }

        /// <summary>
        /// Returns an array of user's tasks
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns the user's tasks</response>
        [Authorize]
        [HttpGet("tasks")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ControllerResponse<TodoTask>>> GetAll()
        {
            ControllerResponse<List<TodoTask>> response = new ControllerResponse<List<TodoTask>>();
            var service = await _todoService.GetAll(User.Identity.Name);

            response.Message = "Success";
            response.Data = service;
            return Ok(response);
        }

        /// <summary>
        /// Returns a new task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        /// <response code="201">Returns the new task</response>
        /// <response code="400">If user not found</response>
        [Authorize]
        [HttpPost("create/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ControllerResponse<TodoTask>>> Create(string task)
        {
            ControllerResponse<TodoTask> response = new ControllerResponse<TodoTask>();
            if(task == null)
            {
                response.Message = "Task is empty";
                return BadRequest(response);
            }

            var service = await _todoService.Create(User.Identity.Name, task);
            if(service == null)
            {
                response.Message = "Error";
                return BadRequest(response);
            }

            response.Message = "Task has been created";
            response.Data = service;
            return Ok(response);
        }

        /// <summary>
        /// Returns completed task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Returns the completed task</response>
        /// <response code="404">If task is not found</response>
        [Authorize]
        [HttpPost("complete/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ControllerResponse<TodoTask>>> Create(int id)
        {
            ControllerResponse<TodoTask> response = new ControllerResponse<TodoTask>();
            var service = await _todoService.Complete(User.Identity.Name, id);

            if(service == null)
            {
                response.Message = "Task is not found";
                return NotFound(response);
            }

            response.Data = service;
            response.Message = "Success";
            return Ok(response);
        }

        /// <summary>
        /// Returns updated task
        /// </summary>
        /// <param name="id"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        /// <response code="201">Returns the updated task</response>
        /// <response code="404">If task is not found</response>
        [Authorize]
        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ControllerResponse<TodoTask>>> Update(int id, string task)
        {
            ControllerResponse<TodoTask> response = new ControllerResponse<TodoTask>();
            var service = await _todoService.Update(User.Identity.Name, id, task);

            if (service == null)
            {
                response.Message = "Task is not found";
                return NotFound(response);
            }

            response.Data = service;
            response.Message = "Task has been updated";
            return Ok(response);
        }

        /// <summary>
        /// Returns deleted task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="201">Returns the updated task</response>
        /// <response code="404">If task is not found</response>
        [Authorize]
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ControllerResponse<TodoTask>>> Delete(int id)
        {
            ControllerResponse<TodoTask> response = new ControllerResponse<TodoTask>();
            var service = await _todoService.Delete(User.Identity.Name, id);

            if(service == null)
            {
                response.Message = "Task is not found";
                return NotFound(response);
            }

            response.Data = service;
            response.Message = "Success";
            return Ok(response);
        }
    }
}
