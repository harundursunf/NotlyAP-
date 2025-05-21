using Businness.Abstract;
using Core.Dto.Core.Dto;
using DataAccess.Abstract;
using Entities.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Businness.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IConfiguration _configuration;

        public UserManager(IUserDal userDal, IConfiguration configuration)
        {
            _userDal = userDal;
            _configuration = configuration;
        }

        public UserDto GetById(int id)
        {
            var user = _userDal.GetById(id);
            return user?.Adapt<UserDto>();
        }

        public List<UserDto> GetAll()
        {
            var users = _userDal.GetAll();
       
            return users?.Select(u => u.Adapt<UserDto>()).ToList();
        }

        public void Add(UserDto userDto)
        {
            var user = userDto.Adapt<User>();
            _userDal.Add(user);
        }

        public void Update(UserDto userDto)
        {
            var userToUpdate = _userDal.GetById(userDto.Id);
            if (userToUpdate != null)
            {
                userToUpdate.FullName = userDto.FullName;
                userToUpdate.Email = userDto.Email;
                userToUpdate.University = userDto.University;
                userToUpdate.Department = userDto.Department;
                userToUpdate.Bio = userDto.Bio;
      
                _userDal.Update(userToUpdate);
            }
        }

        public async Task<bool> UpdateProfileDetailsAsync(int userId, UserProfileUpdateDto profileDto)
        {
            var userEntity = _userDal.GetById(userId);
            if (userEntity == null)
            {
                return false;
            }

            userEntity.Bio = profileDto.Bio ?? userEntity.Bio;
            userEntity.University = profileDto.University ?? userEntity.University;
            userEntity.Department = profileDto.Department ?? userEntity.Department;

            _userDal.Update(userEntity);
            return true;
        }

        public async Task<string> UploadProfilePictureAsync(int userId, IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            var userEntity = _userDal.GetById(userId);
            if (userEntity == null) return null;

            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(fileExtension)) return null;

            var uploadPathConfig = _configuration["FileUploadSettings:ProfilePicturesPath"];
            if (string.IsNullOrEmpty(uploadPathConfig)) return null;

            var physicalUploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uploadPathConfig);

            if (!Directory.Exists(physicalUploadFolder))
            {
                Directory.CreateDirectory(physicalUploadFolder);
            }

            var newFileName = $"{Guid.NewGuid()}{fileExtension}";
            var physicalFilePath = Path.Combine(physicalUploadFolder, newFileName);

            using (var stream = new FileStream(physicalFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativeUrl = Path.Combine("/", uploadPathConfig, newFileName).Replace("\\", "/");

    

            userEntity.ProfilePictureUrl = relativeUrl; 
            _userDal.Update(userEntity);

            return relativeUrl; 
        }

        public void Delete(int id)
        {
            var user = _userDal.GetById(id);
            if (user != null)
            {
                _userDal.Delete(user);
            }
        }
    }
}