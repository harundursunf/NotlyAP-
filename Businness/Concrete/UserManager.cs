using Businness.Abstract;
using Core.Dto.Core.Dto; // UserDto'nun bulunduğu namespace
using DataAccess.Abstract; // IUserDal'ın bulunduğu namespace
using Entities.Entities; // User entity'nin bulunduğu namespace
using Mapster; // Mapster için
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Eğer async metotlar kullanıyorsanız

namespace Businness.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        // Mevcut CRUD metotları (DTO ile çalışıyorlar)
        public void Add(UserDto userDto)
        {
            // Şifre hashleme burada veya ayrı bir Register/Auth servisinde yapılmalı
            var user = userDto.Adapt<User>();
            _userDal.Add(user);
        }

        public void Update(UserDto userDto)
        {
            // Güncelleme mantığı: Sadece DTO'da gelen alanları güncellemek için entity'yi çekip eşleme yapmalısınız
            var user = _userDal.GetById(userDto.Id); // Entity'yi çek
            if (user != null)
            {
                 // DTO'dan gelen güncel bilgileri (Department, Bio, University, ProfilePictureUrl) entity'ye eşle
                userDto.Adapt(user); // Mapster ile eşleme
                _userDal.Update(user); // Entity'yi güncelle
            }
        }

        public void Delete(UserDto userDto)
        {
            // Delete metodu genellikle sadece ID almalı veya entity çekip silmeli
            var user = _userDal.GetById(userDto.Id); // Entity'yi çek
             if (user != null)
             {
                 _userDal.Delete(user); // Entity silme
             }
        }

        public UserDto GetById(int id)
        {
            // Entity'yi çek ve DTO'ya dönüştür
            var user = _userDal.GetById(id); // Entity'yi çek
            return user?.Adapt<UserDto>(); // DTO'ya dönüştür ve döndür
        }

        public List<UserDto> GetAll()
        {
            // Tüm Entity'leri çek ve DTO listesine dönüştür
            return _userDal.GetAll().Adapt<List<UserDto>>();
        }


        // Yeni eklenecek metot implementasyonu
        public void UpdateProfilePictureUrl(int userId, string profilePictureUrl)
        {
            // Kullanıcıyı DAL üzerinden Entity olarak çek
            var user = _userDal.GetById(userId); // IUserDal'da GetById metodu Entity döndürmeli
            if (user != null)
            {
                // Profil fotoğrafı URL'sini güncelle
                user.ProfilePictureUrl = profilePictureUrl;
                // Kullanıcıyı veritabanında güncelle
                _userDal.Update(user); // IUserDal'da Update metodu Entity almalı
            }
            // Kullanıcı bulunamazsa hata fırlatılabilir veya loglanabilir
        }

        // Eğer IUserDal async metotlar kullanıyorsa, UserManager'da async olmalı:
        // public async Task UpdateProfilePictureUrlAsync(int userId, string profilePictureUrl)
        // {
        //     var user = await _userDal.GetByIdAsync(userId);
        //     if (user != null)
        //     {
        //         user.ProfilePictureUrl = profilePictureUrl;
        //         await _userDal.UpdateAsync(user);
        //     }
        // }

        // Eğer Service katmanında Entity döndüren bir GetById metodu eklediyseniz:
        // public Entities.Entities.User GetEntityById(int id)
        // {
        //      return _userDal.GetById(id); // DAL'dan entity'yi döndür
        // }
    }
}
