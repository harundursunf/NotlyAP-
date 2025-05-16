using Businness.Abstract;
using Core.Dto.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserDto userDto)
        {
            _userService.Add(userDto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserDto userDto)
        {
            _userService.Update(userDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();

            _userService.Delete(user);
            return Ok();
        }
    }
}
