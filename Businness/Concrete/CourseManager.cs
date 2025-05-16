using Businness.Abstract;
using Core.Dto;
using Core.Dto.Core.Dto;
using DataAccess.Abstract;
using Entities.Entities;
using Mapster;

namespace Businness.Concrete
{
    public class CourseManager : ICourseService
    {
        private readonly ICourseDal _courseDal;

        public CourseManager(ICourseDal courseDal)
        {
            _courseDal = courseDal;
        }

        public void Add(CourseDto courseDto)
        {
            var course = courseDto.Adapt<Course>();
            _courseDal.Add(course);
        }

        public void Update(CourseDto courseDto)
        {
            var course = _courseDal.GetById(courseDto.Id);
            if (course != null)
            {
                courseDto.Adapt(course);
                _courseDal.Update(course);
            }
        }

        public void Delete(CourseDto courseDto)
        {
            var course = _courseDal.GetById(courseDto.Id);
            if (course != null)
            {
                _courseDal.Delete(course);
            }
        }

        public CourseDto GetById(int id)
        {
            var course = _courseDal.GetById(id);
            return course?.Adapt<CourseDto>();
        }

        public List<CourseDto> GetAll()
        {
            return _courseDal.GetAll()
                             .Adapt<List<CourseDto>>();
        }

        public List<CourseDto> GetCoursesByUserId(int userId)
        {
            var courses = _courseDal.GetCoursesByUserId(userId);
            return courses.Adapt<List<CourseDto>>();
        }

    }
}
