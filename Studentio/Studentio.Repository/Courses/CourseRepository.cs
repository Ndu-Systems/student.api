using Studentio.Contracts.ICourse;
using Studentio.Entities.Context;
using Studentio.Entities.HATEOAS;
using Studentio.Entities.Models;
using Studentio.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Studentio.Repository.Courses
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(RepositoryContext context): base(context) { }
        public PagedList<Course> GetAllCourses(PagingParams pagingParams)
        {
            var courses = FindAll().Where(c => c.StatusId == 1).AsQueryable();
            return new PagedList<Course>(
                courses, pagingParams.PageNumber, pagingParams.PageSize
                );
        }

        public Course GetCourseById(Guid id)
        {
            return FindByCondition(c => c.Id.Equals(id))
                    .DefaultIfEmpty(new Course())
                    .FirstOrDefault();
        }

        public bool RegisterCourse(Course model)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCourse(Course dbCourse, Course course)
        {
            throw new NotImplementedException();
        }
    }
}
