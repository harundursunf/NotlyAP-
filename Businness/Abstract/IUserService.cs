using Core.Dto.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businness.Abstract
{
    public interface IUserService
    {
        void Add(UserDto userDto);
        void Update(UserDto userDto);
        void Delete(UserDto userDto);
        UserDto GetById(int id);
        List<UserDto> GetAll();


    }
}
