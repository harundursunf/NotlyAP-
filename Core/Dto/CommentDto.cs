using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public string UserFullName { get; set; }    // User'dan alınır

        public int NoteId { get; set; }
        public string NoteTitle { get; set; }       // Note'dan alınır
    }
}
