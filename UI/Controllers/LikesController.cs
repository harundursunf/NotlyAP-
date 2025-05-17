// WebApi/Controllers/LikesController.cs
using Businness.Abstract;
using Core.Dto.Core.Dto; // Ensure this is the correct namespace for LikeDto
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

// [Authorize] // Uncomment if like operations require authorization
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
        var likes = _likeService.GetAll();
        return Ok(likes);
    }

    [HttpGet("{id}", Name = "GetLikeById")] // Added Name for CreatedAtAction
    public ActionResult<LikeDto> GetById(int id)
    {
        var like = _likeService.GetById(id);
        if (like == null)
        {
            return NotFound($"Like with ID {id} not found.");
        }
        return Ok(like);
    }

    [HttpPost]
    public IActionResult Add([FromBody] LikeDto likeDto) 
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        LikeDto createdLikeDto = _likeService.Add(likeDto);

        if (createdLikeDto == null || createdLikeDto.Id == 0)
        {
           
            return StatusCode(500, "Beğeni oluşturulamadı veya oluşturulan beğeniye ait ID alınamadı.");
        }

        
        return CreatedAtAction(nameof(GetById), new { id = createdLikeDto.Id }, createdLikeDto);
    }

    [HttpPut("{id}")] 
    public IActionResult Update(int id, [FromBody] LikeDto likeDto)
    {
        if (id != likeDto.Id)
        {
            return BadRequest("ID mismatch: ID in URL does not match ID in request body.");
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingLike = _likeService.GetById(id);
        if (existingLike == null)
        {
            return NotFound($"Like with ID {id} not found for update.");
        }

        _likeService.Update(likeDto); 
        return NoContent(); 
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var likeDto = _likeService.GetById(id); 
        if (likeDto == null)
        {
            return NotFound($"Like with ID {id} not found for deletion.");
        }

        
        _likeService.Delete(likeDto);
        return NoContent(); 
    }

    [HttpGet("user/{userId}")]
    public ActionResult<List<LikeDto>> GetLikesByUserId(int userId)
    {
        var likes = _likeService.GetLikesByUserId(userId);
        if (likes == null || !likes.Any())
        {
            return NotFound($"No likes found for User ID {userId}.");
        }
        return Ok(likes);
    }
}
