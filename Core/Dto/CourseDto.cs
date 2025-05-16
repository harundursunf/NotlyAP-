using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    namespace Core.Dto
    {
        public class CourseDto
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public int UserId { get; set; }
            public string UserFullName { get; set; }    // Course'u ekleyen kullanıcı
        }
    }

}
