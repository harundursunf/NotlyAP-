using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    namespace Core.Dto
    {
        public class LikeDto
        {
            public int Id { get; set; }

            public int UserId { get; set; }
            public string UserFullName { get; set; }

            public int NoteId { get; set; }
            public string NoteTitle { get; set; }
        }
    }

}
