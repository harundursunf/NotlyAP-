using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }                 // Yorum içeriği
        public DateTime CreatedAt { get; set; }          // Yorum tarihi

        public int UserId { get; set; }
        public User User { get; set; }                    // Yorumu yapan kullanıcı

        public int NoteId { get; set; }
        public Note Note { get; set; }                    // Yorum yapılan not
    }

}
