using Businness.Abstract;
using Core.Dto;
using Core.Dto.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet]
        public ActionResult<List<NoteDto>> GetAll()
        {
            var notes = _noteService.GetAll();
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public ActionResult<NoteDto> GetById(int id)
        {
            var note = _noteService.GetById(id);
            if (note == null) return NotFound();
            return Ok(note);
        }

        [HttpPost]
        public IActionResult Add(NoteDto noteDto)
        {
            _noteService.Add(noteDto);
            return CreatedAtAction(nameof(GetById), new { id = noteDto.Id }, noteDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, NoteDto noteDto)
        {
            if (id != noteDto.Id)
                return BadRequest("Id mismatch");

            _noteService.Update(noteDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var noteDto = _noteService.GetById(id);
            if (noteDto == null) return NotFound();

            _noteService.Delete(noteDto);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<NoteDto>> GetNotesByUserId(int userId)
        {
            var notes = _noteService.GetNotesByUserId(userId);
            if (notes == null || notes.Count == 0)
                return NotFound();

            return Ok(notes);
        }
    }
}
