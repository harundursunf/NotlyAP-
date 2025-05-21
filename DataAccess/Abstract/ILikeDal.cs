// DataAccess/Abstract/ILikeDal.cs
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ILikeDal : IGenericDal<Like>
    {
        List<Like> GetLikesByUserId(int userId);
        Like GetByUserIdAndNoteId(int userId, int noteId); // <<< YENİ METOT TANIMI
        int CountLikesForNote(int noteId); // << YENİ: Bir nota ait toplam beğeni sayısını verir
    }
}