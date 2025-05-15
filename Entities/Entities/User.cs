using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }            // Kullanıcı adı
        public string Email { get; set; }               // Email (giriş için)
        public string PasswordHash { get; set; }        // Şifre hash olarak tutulacak

        // Profil bilgileri
        public string ProfilePictureUrl { get; set; }   // Profil fotoğrafı URL'si
        public string Bio { get; set; }                  // Kullanıcı biyografisi
        public string University { get; set; }           // Üniversite ismi

        // İlişkiler
        public ICollection<Course> Courses { get; set; }    // Kullanıcının eklediği dersler
        public ICollection<Note> Notes { get; set; }        // Kullanıcının paylaştığı notlar
        public ICollection<Comment> Comments { get; set; }  // Kullanıcının yaptığı yorumlar
        public ICollection<Like> Likes { get; set; }        // Kullanıcının yaptığı beğeniler
    }
}
    