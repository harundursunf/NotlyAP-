using Businness.Abstract;
using Core.Dto.Core.Dto; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        public ActionResult<List<CourseDto>> GetAll()
        {
           
            return Ok(_courseService.GetAll()); 
        }
        [HttpGet("{id}")]
        public ActionResult<CourseDto> GetById(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();
            return Ok(course); 
        }
        [HttpGet("user/{userId}")] 
        public ActionResult<List<CourseDto>> GetCoursesByUserId(int userId)
        {
            var userCourses = _courseService.GetCoursesByUserId(userId);
            return Ok(userCourses); 
        }
        [HttpPost]
        public IActionResult Add([FromBody] CourseDto courseDto) 
        {
            _courseService.Add(courseDto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] CourseDto courseDto) 
        {
            _courseService.Update(courseDto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var courseToDelete = _courseService.GetById(id); // Önce dersi çek (DTO olarak)
            if (courseToDelete == null) return NotFound();
            _courseService.Delete(courseToDelete); // Service'in Delete metodu DTO alıyorsa

            return Ok();
        }
    }
}
