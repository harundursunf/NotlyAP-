using Businness.Abstract;
using Core.Dto.Core.Dto; // UserDto ve UserProfileUpdateDto için namespace'iniz
using Microsoft.AspNetCore.Authorization; // [Authorize] için
using Microsoft.AspNetCore.Http;    // IFormFile için
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; // IConfiguration için
using System.Collections.Generic;
using System.Linq; // Select için
using System.Security.Claims;     // ClaimTypes için
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        private string GetAbsoluteUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return null;
            if (relativeUrl.StartsWith("http://") || relativeUrl.StartsWith("https://"))
                return relativeUrl;

            var publicBaseUrl = _configuration["ApiSettings:PublicBaseUrl"];
            if (string.IsNullOrEmpty(publicBaseUrl))
            {
                
                return $"{Request.Scheme}://{Request.Host}{relativeUrl}";
            }
            return $"{publicBaseUrl.TrimEnd('/')}{relativeUrl}";
        }

        private int? GetCurrentUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim != null && int.TryParse(userIdClaim, out int userId))
            {
                return userId;
            }
            return null;
        }

        [HttpGet("{id}")]
        [Authorize] 
        public ActionResult<UserDto> GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound(new { message = $"Kullanıcı ID {id} bulunamadı." });

            user.ProfilePictureUrl = GetAbsoluteUrl(user.ProfilePictureUrl);
            return Ok(user);
        }

        [HttpPut("{id}/profile-update")]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile(int id, [FromBody] UserProfileUpdateDto profileDto)
        {
            var currentUserIdInToken = GetCurrentUserIdFromToken();
            if (!currentUserIdInToken.HasValue || id != currentUserIdInToken.Value)
            {
                
                return Forbid("Bu profil bilgilerini güncelleme yetkiniz yok.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _userService.UpdateProfileDetailsAsync(id, profileDto);

            if (!success)
            {
                return NotFound(new { message = $"Kullanıcı ID {id} bulunamadı veya güncelleme başarısız oldu." });
            }
            return NoContent();
        }

        [HttpPost("{id}/upload-profile-picture")]
        [Authorize]
        public async Task<IActionResult> UploadProfilePicture(int id, IFormFile file)
        {
            var currentUserIdInToken = GetCurrentUserIdFromToken();
            if (!currentUserIdInToken.HasValue || id != currentUserIdInToken.Value)
            {
              
                return Forbid("Bu profil için fotoğraf yükleme yetkiniz yok.");
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "Lütfen yüklenecek bir dosya seçin." });
            }
           

            var relativeImageUrl = await _userService.UploadProfilePictureAsync(id, file);

            if (string.IsNullOrEmpty(relativeImageUrl))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Profil fotoğrafı yüklenirken bir sorun oluştu veya geçersiz dosya." });
            }

            var absoluteImageUrl = GetAbsoluteUrl(relativeImageUrl);
            return Ok(new { profilePictureUrl = absoluteImageUrl });
        }

      
       
    }
}
