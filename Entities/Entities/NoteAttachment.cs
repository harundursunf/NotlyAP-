using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class NoteAttachment
    {
        public int Id { get; set; }
        public string FileUrl { get; set; }               // Dosyanın URL'si veya yolu
        public string FileType { get; set; }              // Dosya türü (image/png, application/pdf vb.)

        public int NoteId { get; set; }
        public Note Note { get; set; }                     // Dosyanın ait olduğu not
    }

}
