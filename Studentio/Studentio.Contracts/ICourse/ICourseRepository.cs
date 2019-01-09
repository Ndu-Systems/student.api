using Studentio.Contracts.IRepositoryBase;
using Studentio.Entities.HATEOAS;
using Studentio.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Contracts.ICourse
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
        PagedList<Course> GetAllCourses(PagingParams pagingParams);

        Course GetCourseById(Guid id);

        bool RegisterCourse(Course model);

        bool UpdateCourse(Course dbCourse, Course course);

    }
}
