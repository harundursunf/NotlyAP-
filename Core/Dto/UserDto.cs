using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    namespace Core.Dto
    {
        public class UserDto
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

            public string? ProfilePictureUrl { get; set; } 
            public string? Bio { get; set; }             
            public string? University { get; set; }      
            public string? Department { get; set; }     
        }
    }

}
