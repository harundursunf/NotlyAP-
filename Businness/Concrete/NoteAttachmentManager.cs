using Businness.Abstract;
using Core.Dto;
using Core.Dto.Core.Dto;
using DataAccess.Abstract;
using Entities.Entities;
using Mapster;

namespace Businness.Concrete
{
    public class NoteAttachmentManager : INoteAttachmentService
    {
        private readonly INoteAttachmentDal _noteAttachmentDal;

        public NoteAttachmentManager(INoteAttachmentDal noteAttachmentDal)
        {
            _noteAttachmentDal = noteAttachmentDal;
        }

        public void Add(NoteAttachmentDto dto)
        {
            var entity = dto.Adapt<NoteAttachment>();
            _noteAttachmentDal.Add(entity);
        }

        public void Update(NoteAttachmentDto dto)
        {
            var existing = _noteAttachmentDal.GetById(dto.Id);
            if (existing != null)
            {
                dto.Adapt(existing);
                _noteAttachmentDal.Update(existing);
            }
        }

        public void Delete(NoteAttachmentDto dto)
        {
            var entity = _noteAttachmentDal.GetById(dto.Id);
            if (entity != null)
            {
                _noteAttachmentDal.Delete(entity);
            }
        }

        public NoteAttachmentDto GetById(int id)
        {
            var entity = _noteAttachmentDal.GetById(id);
            return entity?.Adapt<NoteAttachmentDto>();
        }

        public List<NoteAttachmentDto> GetAll()
        {
            return _noteAttachmentDal.GetAll().Adapt<List<NoteAttachmentDto>>();
        }
    }
}
