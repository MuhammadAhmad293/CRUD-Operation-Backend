﻿using CRUDoperations.Dto.DTOs;
using CRUDoperations.IServices.IService;
using Microsoft.AspNetCore.Mvc;

namespace CRUDoperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService { get; }
        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            return Ok(await UserService.Add(userDto));
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserDto userDto)
        {
            return Ok(await UserService.Update(userDto));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await UserService.Delete(id));
        }
        [HttpGet("order/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await UserService.GetById(id));
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await UserService.GetAll());
        }
    }
}
