using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }                // Ders adı

        public int UserId { get; set; }
        public User User { get; set; }                   // Dersi ekleyen kullanıcı

        public ICollection<Note> Notes { get; set; }    // Derse ait notlar
    }
}
