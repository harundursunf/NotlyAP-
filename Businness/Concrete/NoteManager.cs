using Businness.Abstract;
using Core.Dto;
using DataAccess.Abstract;
using Entities.Entities;
using Mapster;
using Microsoft.Extensions.Configuration; // IConfiguration için
using System.Collections.Generic;
using System.Linq;

namespace Businness.Concrete
{
    public class NoteManager : INoteService
    {
        private readonly INoteDal _noteDal;
        private readonly ILikeDal _likeDal;
        private readonly IConfiguration _configuration; // appsettings.json okumak için

        public NoteManager(INoteDal noteDal, ILikeDal likeDal, IConfiguration configuration)
        {
            _noteDal = noteDal;
            _likeDal = likeDal;
            _configuration = configuration;
        }

        private string GetAbsoluteUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl)) return null;
            if (relativeUrl.StartsWith("http://") || relativeUrl.StartsWith("https")) return relativeUrl;

            var publicBaseUrl = _configuration["ApiSettings:PublicBaseUrl"];
            if (string.IsNullOrEmpty(publicBaseUrl))
            {
               
                return relativeUrl; 
            }
            return $"{publicBaseUrl.TrimEnd('/')}{relativeUrl}";
        }

        private void PopulateLikeAndImageDetails(NoteDto noteDto, int? currentLoggedInUserId)
        {
            if (noteDto == null) return;

            
            noteDto.UserProfilePictureUrl = GetAbsoluteUrl(noteDto.UserProfilePictureUrl);
            noteDto.ImageUrl = GetAbsoluteUrl(noteDto.ImageUrl);

           
            noteDto.LikesCount = _likeDal.CountLikesForNote(noteDto.Id);
            if (currentLoggedInUserId.HasValue && currentLoggedInUserId.Value > 0)
            {
                var userLike = _likeDal.GetByUserIdAndNoteId(currentLoggedInUserId.Value, noteDto.Id);
                noteDto.IsLikedByCurrentUser = userLike != null;
                noteDto.CurrentUserLikeId = userLike?.Id;
            }
            else
            {
                noteDto.IsLikedByCurrentUser = false;
                noteDto.CurrentUserLikeId = null;
            }
        }

        public NoteDto GetById(int noteId, int? currentLoggedInUserId)
        {
            var noteEntity = _noteDal.GetById(noteId); 
            if (noteEntity == null) return null;

            var noteDto = noteEntity.Adapt<NoteDto>(); 
            PopulateLikeAndImageDetails(noteDto, currentLoggedInUserId); 

            return noteDto;
        }

        public List<NoteDto> GetAll(int? currentLoggedInUserId)
        {
            var noteEntities = _noteDal.GetAll(); 
            if (noteEntities == null || !noteEntities.Any()) return new List<NoteDto>();

            var noteDtos = noteEntities.Adapt<List<NoteDto>>();
            foreach (var noteDto in noteDtos)
            {
                PopulateLikeAndImageDetails(noteDto, currentLoggedInUserId);
            }
            return noteDtos;
        }

        public List<NoteDto> GetNotesByUserId(int userId, int? currentLoggedInUserId)
        {
            var noteEntities = _noteDal.GetNotesByUserId(userId); // DAL'ın User, Course, Attachments içerdiğinden emin olun
            if (noteEntities == null || !noteEntities.Any()) return new List<NoteDto>();

            var noteDtos = noteEntities.Adapt<List<NoteDto>>();
            foreach (var noteDto in noteDtos)
            {
                PopulateLikeAndImageDetails(noteDto, currentLoggedInUserId);
            }
            return noteDtos;
        }

        public void Add(NoteDto noteDto)
        {
            var noteEntity = noteDto.Adapt<Note>();
          
            _noteDal.Add(noteEntity);
            
        }

        public void Update(NoteDto noteDto)
        {
            var noteEntity = _noteDal.GetById(noteDto.Id);
            if (noteEntity != null)
            {
                
                noteEntity.Title = noteDto.Title;
                noteEntity.Content = noteDto.Content;
                noteEntity.CourseId = noteDto.CourseId;
                _noteDal.Update(noteEntity);
            }
        }

        public void Delete(int noteId)
        {
            var noteEntity = _noteDal.GetById(noteId);
            if (noteEntity != null)
            {
               
                _noteDal.Delete(noteEntity);
            }
        }
    }
}
