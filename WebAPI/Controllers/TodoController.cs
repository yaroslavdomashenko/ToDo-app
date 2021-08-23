﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<TodoTask>> Get(int id)
        {
            var task = await _todoService.Get(User.Identity.Name, id);

            if (task == null) 
                return NotFound();
            return Ok(task);
        }

        /// <summary>
        /// Returns an array of user's tasks
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Returns the user's tasks</response>
        [Authorize]
        [HttpGet("tasks")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<List<TodoTask>>> GetAll()
        {
            return Ok(await _todoService.GetAll(User.Identity.Name));
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
        public async Task<ActionResult<TodoTask>> Create(string text)
        {
            if (text == null) return BadRequest();

            var createdTask = await _todoService.Create(User.Identity.Name, text);
            if (createdTask == null) return BadRequest();

            return Ok(createdTask);
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
        public async Task<ActionResult<ControllerResponse<TodoTask>>> Complete(int id)
        {
            var task = await _todoService.Complete(User.Identity.Name, id);
            if (task == null) 
                return NotFound();

            return Ok(task);
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
        public async Task<ActionResult<TodoTask>> Update(int id, string text)
        {
            var task = await _todoService.Update(User.Identity.Name, id, text);
            if (task == null) 
                return NotFound();

            return Ok(task);
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
        public async Task<ActionResult<TodoTask>> Delete(int id)
        {
            var result = await _todoService.Delete(User.Identity.Name, id);

            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}
