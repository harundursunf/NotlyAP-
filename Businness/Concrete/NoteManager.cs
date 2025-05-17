using Businness.Abstract;
using Core.Dto;
using Core.Dto.Core.Dto;
using DataAccess.Abstract;
using Entities.Entities;
using Mapster; // Mapster kullanıyorsan
using System;
using System.Collections.Generic;
using System.Linq;

namespace Businness.Concrete
{
    public class NoteManager : INoteService
    {
        private readonly INoteDal _noteDal;

        public NoteManager(INoteDal noteDal)
        {
            _noteDal = noteDal;
        }

        public void Add(NoteDto noteDto)
        {
           
            var noteEntity = noteDto.Adapt<Note>();
            _noteDal.Add(noteEntity);
        }

        public void Delete(NoteDto noteDto)
        {
            var noteEntity = noteDto.Adapt<Note>();
            _noteDal.Delete(noteEntity);
        }

        public List<NoteDto> GetAll()
        {
            var notes = _noteDal.GetAll();
            return notes.Adapt<List<NoteDto>>();
        }

        public NoteDto GetById(int id)
        {
            var note = _noteDal.GetById(id);
            return note.Adapt<NoteDto>();
        }

        public List<NoteDto> GetNotesByUserId(int userId)
        {
            var notes = _noteDal.GetNotesByUserId(userId);
            return notes.Adapt<List<NoteDto>>();
        }

        public void Update(NoteDto noteDto)
        {
            var noteEntity = noteDto.Adapt<Note>();
            _noteDal.Update(noteEntity);
        }
    }
}
