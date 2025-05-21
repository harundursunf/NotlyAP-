using Microsoft.AspNetCore.Mvc;
using Businness.Abstract;
using Core.Dto; // NoteDto için (namespace'inizi kontrol edin)
using System.Collections.Generic;
using System.Linq; // Any() metodu için
using System.Security.Claims; // ClaimTypes için
using Microsoft.AspNetCore.Authorization; // [Authorize] attribute'ü için bu using eklenmeli

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // Yardımcı metot: Token'dan o anki giriş yapmış kullanıcının ID'sini alır
        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int parsedUserId))
            {
                return parsedUserId;
            }
            return null;
        }

        [HttpGet("{id}")]
        [Authorize] // Endpoint'i koru
        public ActionResult<NoteDto> GetById(int id)
        {
            var currentUserId = GetCurrentUserId();
            var noteDto = _noteService.GetById(id, currentUserId);

            if (noteDto == null)
            {
                return NotFound(new { message = $"Not ID {id} bulunamadı." });
            }
            return Ok(noteDto);
        }

        [HttpGet]
        [Authorize] // Endpoint'i koru
        public ActionResult<List<NoteDto>> GetAll()
        {
            var currentUserId = GetCurrentUserId();
            var notes = _noteService.GetAll(currentUserId);
            return Ok(notes ?? new List<NoteDto>());
        }

        [HttpGet("user/{userId}")]
        [Authorize] // Endpoint'i koru
        public ActionResult<List<NoteDto>> GetNotesByAuthorId(int userId)
        {
            var currentLoggedInUserId = GetCurrentUserId();
            var notes = _noteService.GetNotesByUserId(userId, currentLoggedInUserId);

            if (notes == null)
            {
                return Ok(new List<NoteDto>());
            }
            return Ok(notes);
        }

        [HttpPost]
        [Authorize] // YENİ NOT EKLEME KESİNLİKLE YETKİLENDİRME GEREKTİRİR!
        public IActionResult Add([FromBody] NoteDto noteDto) // İdealde burada NoteCreateDto gibi UserId içermeyen bir DTO kullanılır.
        {
            var currentUserId = GetCurrentUserId();
         
            if (!currentUserId.HasValue)
            {
              
                return Unauthorized(new { message = "Kullanıcı kimliği doğrulanamadı veya bulunamadı." });
            }

           
            noteDto.UserId = currentUserId.Value;

           
            if (!ModelState.IsValid) 
            {
              
                return BadRequest(ModelState);
            }

          
            if (noteDto.CourseId == 0) 
            {
                
                ModelState.AddModelError("CourseId", "Kurs ID alanı zorunludur ve 0'dan farklı olmalıdır.");
                return BadRequest(ModelState); 
            }

            if (string.IsNullOrEmpty(noteDto.Title))
            {
                ModelState.AddModelError("Title", "Başlık alanı zorunludur.");
                return BadRequest(ModelState); 
            }



            _noteService.Add(noteDto); 

            if (noteDto.Id > 0)
            {
                
                return CreatedAtAction(nameof(GetById), new { id = noteDto.Id }, noteDto);
            }

            return Ok(new { message = "Not başarıyla eklendi.", noteId = noteDto.Id }); // Id'yi de ekleyebiliriz.
        }

        [HttpPut("{id}")]
        [Authorize] // Endpoint'i koru
        public IActionResult Update(int id, [FromBody] NoteDto noteDto)
        {
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return Unauthorized(new { message = "Kullanıcı kimliği doğrulanamadı." });
            }

            
            var existingNote = _noteService.GetById(id, currentUserId.Value); 
                                                                              
            if (existingNote == null)
            {
                return NotFound(new { message = $"Güncellenecek Not ID {id} bulunamadı." });
            }

            if (existingNote.UserId != currentUserId.Value /* && !User.IsInRole("Admin") */) // Admin rol kontrolü ekleyebilirsiniz.
            {
                return Forbid("Bu notu güncelleme yetkiniz yok."); // HTTP 403
            }

            if (id != noteDto.Id && noteDto.Id != 0)
            {
                return BadRequest(new { message = "ID uyuşmazlığı." });
            }
            noteDto.Id = id; 
            noteDto.UserId = currentUserId.Value; 

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (noteDto.CourseId == 0)
            {
                ModelState.AddModelError("CourseId", "Kurs ID alanı zorunludur ve 0'dan farklı olmalıdır.");
                return BadRequest(ModelState);
            }
            if (string.IsNullOrEmpty(noteDto.Title))
            {
                ModelState.AddModelError("Title", "Başlık alanı zorunludur.");
                return BadRequest(ModelState);
            }

            _noteService.Update(noteDto);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        [Authorize] 
        public IActionResult Delete(int id)
        {
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return Unauthorized(new { message = "Kullanıcı kimliği doğrulanamadı." });
            }

            
            var noteToDelete = _noteService.GetById(id, currentUserId.Value); 
            if (noteToDelete == null)
            {
                return NotFound(new { message = $"Silinecek Not ID {id} bulunamadı." });
            }

            if (noteToDelete.UserId != currentUserId.Value /* && !User.IsInRole("Admin") */) // Admin rol kontrolü ekleyebilirsiniz.
            {
                return Forbid("Bu notu silme yetkiniz yok."); 
            }

            _noteService.Delete(id);
            return NoContent(); 
        }
    }
}