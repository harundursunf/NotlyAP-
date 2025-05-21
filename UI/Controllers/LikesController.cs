using Businness.Abstract;
using Core.Dto; 
using Core.Dto.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

    [HttpGet("{id}", Name = "GetLikeById")]
    public ActionResult<LikeDto> GetById(int id)
    {
        var like = _likeService.GetById(id); 
        if (like == null)
        {
            return NotFound($"Like with ID {id} not found.");
        }
        return Ok(like);
    }

    public class LikeActionRequestDto
    {
        public int UserId { get; set; } 
        public int NoteId { get; set; }
      
        public string UserFullName { get; set; } 
        public string NoteTitle { get; set; }    
    }

    [HttpPost] 
    public IActionResult Add([FromBody] LikeActionRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        
        if (requestDto.UserId == 0 || requestDto.NoteId == 0)
        {
            return BadRequest("UserId ve NoteId alanları gereklidir ve geçerli olmalıdır.");
        }

        var likeDtoForService = new LikeDto
        {
            UserId = requestDto.UserId, 
            NoteId = requestDto.NoteId,
          
        };

        LikeDto resultLikeDto = _likeService.Add(likeDtoForService); 
        return Ok(resultLikeDto);
    }


    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] LikeDto likeDto) 
    {
       
        if (likeDto.Id == 0) likeDto.Id = id;
        else if (id != likeDto.Id)
        {
            return BadRequest("ID uyuşmazlığı.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _likeService.Update(likeDto);
        return NoContent();
    }

   
    [HttpDelete("{likeId}")] 
    public IActionResult DeleteByLikeId(int likeId)
    {
     
        var like = _likeService.GetById(likeId); 
        if (like == null)
        {
            return NotFound($"Like with ID {likeId} not found for deletion.");
        }
        _likeService.DeleteByLikeId(likeId);
        return NoContent();
    }


    [HttpDelete("user/{userId}/note/{noteId}")]
    public IActionResult UnlikeNote(int userId, int noteId)
    {
        
        if (userId == 0 || noteId == 0)
        {
            return BadRequest("Geçerli UserId ve NoteId gereklidir.");
        }

        bool DTO = _likeService.DeleteByUserIdAndNoteId(userId, noteId);

        if (DTO)
        {
            return NoContent();
        }
        else
        {
            return NotFound("Bu nota ait beğeni bulunamadı veya zaten beğenilmemişti.");
        }
    }

   
    [HttpGet("user/{userId}")]
    public ActionResult<List<LikedNoteInfoDto>> GetLikesByUserId(int userId)
    {
       
        var likedNotesInfo = _likeService.GetLikesByUserId(userId);
        return Ok(likedNotesInfo);
    }
}