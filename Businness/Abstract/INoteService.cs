using Core.Dto.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businness.Abstract
{
    public interface INoteService
    {
        void Add(NoteDto noteDto);
        void Update (NoteDto noteDto);
        void Delete (NoteDto noteDto);
        NoteDto GetById (int id);
        List <NoteDto> GetAll ();

        List<NoteDto> GetNotesByUserId(int userId);
    }
}
