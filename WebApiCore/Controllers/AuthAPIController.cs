using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCore.Models;
using WebApiCore.Services;

namespace WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        AuthService service;

        public AuthAPIController(AuthService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegitserUser registerUser)
        {
            if (ModelState.IsValid)
            {
                var isCreated = await service.CreateUserAsync(registerUser);
                if(isCreated == false)
                {
                    return Conflict($"User is already preseNt {registerUser.Email}");
                }
                var response = new ResponseData()
                {
                    responseMessage = $"{registerUser.Email} User is created successfully"
                };
                return Ok(response);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                var token = await service.AuthUserAsync(user);
                if(token == null)
                {
                    return Unauthorized("The Autentication Failed");
                }
                var response = new ResponseData()
                {
                    responseMessage = token
                };
                return Ok(response);
            }
            return BadRequest(ModelState);
        }
    }
}
