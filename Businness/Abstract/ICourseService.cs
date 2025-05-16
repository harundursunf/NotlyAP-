using Core.Dto.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businness.Abstract
{
    public interface ICourseService
    {
        void Add(CourseDto courseDto);
        void Update(CourseDto courseDto);
        void Delete(CourseDto courseDto);
        CourseDto GetById(int id);
        List<CourseDto> GetAll();
        List<CourseDto> GetCoursesByUserId(int userId);
    }
}
