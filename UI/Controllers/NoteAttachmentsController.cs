using Businness.Abstract;
using Core.Dto;
using Core.Dto.Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteAttachmentsController : ControllerBase
    {
        private readonly INoteAttachmentService _noteAttachmentService;

        public NoteAttachmentsController(INoteAttachmentService noteAttachmentService)
        {
            _noteAttachmentService = noteAttachmentService;
        }

        [HttpGet]
        public ActionResult<List<NoteAttachmentDto>> GetAll()
        {
            return _noteAttachmentService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<NoteAttachmentDto> GetById(int id)
        {
            var result = _noteAttachmentService.GetById(id);
            if (result == null) return NotFound();
            return result;
        }

        [HttpPost]
        public IActionResult Add(NoteAttachmentDto dto)
        {
            _noteAttachmentService.Add(dto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(NoteAttachmentDto dto)
        {
            _noteAttachmentService.Update(dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dto = _noteAttachmentService.GetById(id);
            if (dto == null) return NotFound();
            _noteAttachmentService.Delete(dto);
            return Ok();
        }
    }
}
