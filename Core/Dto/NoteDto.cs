using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    namespace Core.Dto
    {
        public class NoteDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }

            public int UserId { get; set; }
            public string UserFullName { get; set; }    // User'dan alınır

            public int CourseId { get; set; }
            public string CourseName { get; set; }      // Course'dan alınır
        }
    }

}
