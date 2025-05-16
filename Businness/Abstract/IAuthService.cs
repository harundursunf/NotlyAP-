using Core.Dto;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businness.Abstract
{
    public interface IAuthService
    {
        void Register(RegisterDto registerDto);
        Token Login(LoginDto loginDto);
    }
}
