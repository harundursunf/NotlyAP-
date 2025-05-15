using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Like
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }                    // Beğeniyi yapan kullanıcı

        public int NoteId { get; set; }
        public Note Note { get; set; }                    // Beğenilen not
    }

}
