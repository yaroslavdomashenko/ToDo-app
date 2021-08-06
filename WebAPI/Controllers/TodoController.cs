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

        [Authorize]
        [HttpGet("task/{id}")]
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

        [Authorize]
        [HttpGet("tasks")]
        public async Task<ActionResult<ControllerResponse<TodoTask>>> GetAll()
        {
            ControllerResponse<List<TodoTask>> response = new ControllerResponse<List<TodoTask>>();
            var service = await _todoService.GetAll(User.Identity.Name);

            if (service == null) {
                response.Message = "Eror";
                return BadRequest(response);
            }

            response.Message = "Success";
            response.Data = service;
            return Ok(response);
        }


        [Authorize]
        [HttpPost("create/")]
        public async Task<ActionResult<ControllerResponse<TodoTask>>> Create(string task)
        {
            ControllerResponse<TodoTask> response = new ControllerResponse<TodoTask>();
            if(task == null)
            {
                response.Message = "Task is empty";
                return response;
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

        [Authorize]
        [HttpPost("complete/{id}")]
        public async Task<ActionResult<ControllerResponse<TodoTask>>> Create(int id)
        {
            ControllerResponse<TodoTask> response = new ControllerResponse<TodoTask>();
            var service = await _todoService.Complete(User.Identity.Name, id);

            if(service == null)
            {
                response.Message = "Task didn't find";
                return NotFound(response);
            }

            response.Data = service;
            response.Message = "Success";
            return Ok(response);
        }

        [Authorize]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ControllerResponse<TodoTask>>> Update(int id, string task)
        {
            ControllerResponse<TodoTask> response = new ControllerResponse<TodoTask>();
            var service = await _todoService.Update(User.Identity.Name, id, task);

            if (service == null)
            {
                response.Message = "Error";
                return BadRequest(response);
            }

            response.Data = service;
            response.Message = "Task has been updated";
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ControllerResponse<TodoTask>>> Delete(int id)
        {
            ControllerResponse<TodoTask> response = new ControllerResponse<TodoTask>();
            var service = await _todoService.Delete(User.Identity.Name, id);

            if(service == null)
            {
                response.Message = "Error";
                return BadRequest(response);
            }

            response.Data = service;
            response.Message = "Success";
            return Ok(response);
        }
    }
}
