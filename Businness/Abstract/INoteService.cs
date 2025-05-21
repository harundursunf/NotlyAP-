using Core.Dto;
using System.Collections.Generic;

namespace Businness.Abstract
{
    public interface INoteService
    {
        void Add(NoteDto noteDto);
        void Update(NoteDto noteDto);
        void Delete(int noteId);
        NoteDto GetById(int noteId, int? currentLoggedInUserId);
        List<NoteDto> GetAll(int? currentLoggedInUserId);
        List<NoteDto> GetNotesByUserId(int userId, int? currentLoggedInUserId);
    }
}