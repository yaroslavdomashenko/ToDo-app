using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.DTO;
using WebAPI.Services.Interfaces;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Authorization controller /authorization
    /// </summary>
    [ApiController]
    [Route("api/authorization")]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Registers some user in database
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /register
        ///     {
        ///        "username": "login",
        ///        "password": "password",
        ///     }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ControllerResponse<bool>>> Register(AccountRegistration request)
        {
            ControllerResponse<bool> response = new ControllerResponse<bool>();
            var result = await _accountService.Register(request);

            if (!result)
            {
                response.Data = false;
                response.Message = "Registration error";
                return BadRequest(response);
            }

            response.Data = true;
            response.Message = "Registered";
            return Ok(response);
        }

        /// <summary>
        /// Returns JWT token
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /register
        ///     {
        ///        "username": "login",
        ///        "password": "password",
        ///     }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ControllerResponse<string>>> Login(AccountLogIn request)
        {
            ControllerResponse<string> response = new ControllerResponse<string>();
            var result = await _accountService.Login(request);

            if (result == null)
            {
                response.Data = null;
                response.Message = "Wrong password";
                return BadRequest(response);
            }

            response.Data = result;
            response.Message = "Loggined";
            return Ok(response);
        }
    }
}
