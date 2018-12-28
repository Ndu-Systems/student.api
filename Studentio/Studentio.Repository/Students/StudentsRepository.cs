using Studentio.Contracts.IStudent;
using Studentio.Entities.Context;
using Studentio.Entities.Models;
using Studentio.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Studentio.Repository.Students
{
    public class StudentsRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentsRepository(RepositoryContext context) : base(context) { }
        public IEnumerable<Student> GetAllStudents() 
        {
            return FindAll().Where(p => p.StatusId == 1);
        }

        public Student GetStudentById(Guid id)
        {
            return FindByCondition(s => s.Id.Equals(id))
                    .DefaultIfEmpty(new Student())
                    .FirstOrDefault();
        }

        public bool RegisterStudent(Student model)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStudent(Student dbStudent, Student student)
        {
            throw new NotImplementedException();
        }
    }
}
