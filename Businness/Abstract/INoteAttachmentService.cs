using Core.Dto.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businness.Abstract
{
    public interface INoteAttachmentService
    {
        void Add(NoteAttachmentDto noteAttachmentDto);
        void Update(NoteAttachmentDto noteAttachmentDto);
        void Delete(NoteAttachmentDto noteAttachmentDto);
        NoteAttachmentDto GetById(int id);
        List<NoteAttachmentDto> GetAll();
    }
}
