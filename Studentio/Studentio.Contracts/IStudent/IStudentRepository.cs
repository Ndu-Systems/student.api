using Studentio.Contracts.IRepositoryBase;
using Studentio.Entities.HATEOAS;
using Studentio.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Contracts.IStudent
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        PagedList<Student> GetAllStudents(PagingParams pagingParams);

        Student GetStudentById(Guid id);

        bool RegisterStudent(Student model);

        bool UpdateStudent(Student dbStudent, Student student);
    }
}
