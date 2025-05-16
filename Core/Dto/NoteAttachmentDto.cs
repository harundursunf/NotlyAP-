using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    namespace Core.Dto
    {
        public class NoteAttachmentDto
        {
            public int Id { get; set; }
            public string FileUrl { get; set; }
            public string FileType { get; set; }

            public int NoteId { get; set; }
            public string NoteTitle { get; set; }
        }
    }

}
