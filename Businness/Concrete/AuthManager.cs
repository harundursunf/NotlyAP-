using Businness.Abstract;
using Core.Dto;
using Core.Utilities.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businness.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserDal _userDal;
        private readonly ITokenHandler _tokenHandler;

        public AuthManager(IUserDal userDal, ITokenHandler tokenHandler)
        {
            _userDal = userDal;
            _tokenHandler = tokenHandler;
        }

        public void Register(RegisterDto registerDto)
        {
            var existingUser = _userDal.GetByFilter(u => u.Email == registerDto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("Bu e-posta zaten kayıtlı.");

            HashingHelper.CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                FullName = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Bio = "",               
                ProfilePictureUrl = "",
                University = ""
            };

            _userDal.Add(user);
        }

        public Token Login(LoginDto loginDto)
        {
            var user = _userDal.GetByFilter(u => u.Email == loginDto.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Geçersiz e-posta veya şifre.");

            bool isValid = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (!isValid)
                throw new UnauthorizedAccessException("Geçersiz e-posta veya şifre.");

            return _tokenHandler.CreateAccessToken(user);
        }
    }
}
