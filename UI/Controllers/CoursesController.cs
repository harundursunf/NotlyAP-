using Businness.Abstract;
using Core.Dto.Core.Dto; // CourseDto'nun bulunduğu namespace
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks; // Eğer async metotlar kullanıyorsanız

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
            // Tüm dersleri getiren endpoint
            return Ok(_courseService.GetAll()); // Ok() içine alınması daha standart
        }

        [HttpGet("{id}")]
        public ActionResult<CourseDto> GetById(int id)
        {
            // Belirli bir dersi ID'ye göre getiren endpoint
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();
            return Ok(course); // Ok() içine alınması daha standart
        }

        // ************************************
        // Yeni Endpoint: Kullanıcı ID'sine Göre Dersleri Getir
        // Bu endpoint, belirli bir kullanıcının kayıtlı olduğu dersleri döndürür.
        // Rota Örneği: GET /api/Courses/user/14
        // ************************************
        [HttpGet("user/{userId}")] // Yeni rota tanımlandı: api/Courses/user/{userId}
        public ActionResult<List<CourseDto>> GetCoursesByUserId(int userId)
        {
            // Service katmanındaki ilgili metodu çağırarak kullanıcıya ait dersleri al
            var userCourses = _courseService.GetCoursesByUserId(userId);

            // Service katmanı kullanıcı bulunamazsa hata fırlatıyorsa buraya ulaşamayız.
            // Eğer Service boş liste döndürüyorsa (kullanıcının dersi yoksa), yine de Ok(boş liste) döndürmek genellikle daha iyi bir API tasarımıdır.
            return Ok(userCourses); // Ders listesini (boş olsa bile) 200 OK yanıtıyla döndür
        }
        // ************************************
        // Endpoint Sonu
        // ************************************


        [HttpPost]
        public IActionResult Add([FromBody] CourseDto courseDto) // [FromBody] attribute'u eklenmesi önerilir
        {
            // Yeni ders ekleme endpoint'i
            _courseService.Add(courseDto);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody] CourseDto courseDto) // [FromBody] attribute'u eklenmesi önerilir
        {
            // Ders güncelleme endpoint'i
            _courseService.Update(courseDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) // ID almak daha yaygın
        {
            // Ders silme endpoint'i
            // Silme işlemi için Service'in ID alan metodunu çağırın veya önce çekip silin.
            // Eğer Service'in Delete metodu hala DTO alıyorsa, önce çekip DTO'ya dönüştürmeniz gerekir.
            // Daha iyi bir yaklaşım, Service'in Delete(int id) metodu olmasıdır.
            var courseToDelete = _courseService.GetById(id); // Önce dersi çek (DTO olarak)
            if (courseToDelete == null) return NotFound();

            _courseService.Delete(courseToDelete); // Service'in Delete metodu DTO alıyorsa

            return Ok();
        }
    }
}
