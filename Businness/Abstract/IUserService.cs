using Core.Dto.Core.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Businness.Abstract
{
    public interface IUserService
    {
        List<UserDto> GetAll();
        UserDto GetById(int id);
        void Add(UserDto userDto);
        void Update(UserDto userDto);
        void Delete(int id);
        Task<bool> UpdateProfileDetailsAsync(int userId, UserProfileUpdateDto profileDto);
        Task<string> UploadProfilePictureAsync(int userId, IFormFile file); // Bu metot göreceli URL döndürecek
    }
}