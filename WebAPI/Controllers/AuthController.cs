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
    [Route("api/authorization")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountRegistration request)
        {
            ControllerResponse<bool> response = new ControllerResponse<bool>();
            var service = await _accountService.Register(request);

            if (!service.Status)
            {
                response.Data = false;
                response.Message = service.Message;
                return BadRequest(response);
            }

            response.Data = true;
            response.Message = service.Message;
            return Ok(response);
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountLogIn request)
        {
            ControllerResponse<string> response = new ControllerResponse<string>();
            var service = await _accountService.Login(request);

            if (!service.Status)
            {
                response.Data = null;
                response.Message = service.Message;
                return BadRequest(response);
            }

            response.Data = service.Data;
            response.Message = service.Message;
            return Ok(response);
        }
    }
}
