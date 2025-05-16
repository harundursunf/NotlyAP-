using Businness.Abstract;
using Core.Dto;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public ActionResult<List<CommentDto>> GetAll()
    {
        var comments = _commentService.GetAll();
        return Ok(comments);
    }

    [HttpGet("{id}")]
    public ActionResult<CommentDto> GetById(int id)
    {
        var comment = _commentService.GetById(id);
        if (comment == null) return NotFound();
        return Ok(comment);
    }

    [HttpPost]
    public IActionResult Add(CommentDto commentDto)
    {
        _commentService.Add(commentDto);
        return CreatedAtAction(nameof(GetById), new { id = commentDto.Id }, commentDto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CommentDto commentDto)
    {
        if (id != commentDto.Id)
            return BadRequest("Id mismatch");

        _commentService.Update(commentDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var commentDto = _commentService.GetById(id);
        if (commentDto == null) return NotFound();

        _commentService.Delete(commentDto);
        return NoContent();
    }
}
