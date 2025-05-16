using Businness.Abstract;
using Core.Dto;
using Core.Dto.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpGet]
        public ActionResult<List<LikeDto>> GetAll()
        {
            return _likeService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<LikeDto> GetById(int id)
        {
            var like = _likeService.GetById(id);
            if (like == null) return NotFound();
            return like;
        }

        [HttpPost]
        public IActionResult Add(LikeDto likeDto)
        {
            _likeService.Add(likeDto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(LikeDto likeDto)
        {
            _likeService.Update(likeDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var likeDto = _likeService.GetById(id);
            if (likeDto == null) return NotFound();

            _likeService.Delete(likeDto);
            return Ok();
        }
        [HttpGet("user/{userId}")]
        public ActionResult<List<LikeDto>> GetLikesByUserId(int userId)
        {
            var likes = _likeService.GetLikesByUserId(userId);
            if (likes == null || likes.Count == 0)
                return NotFound();

            return Ok(likes);
        }
    }
}
