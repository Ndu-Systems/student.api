using Studentio.Contracts.IStudent;
using Studentio.Entities.Context;
using Studentio.Entities.Extensions.Students;
using Studentio.Entities.HATEOAS;
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

        public PagedList<Student> GetAllStudents(PagingParams pagingParams)
        {
            var students = FindAll().Where(s => s.StatusId == 1).AsQueryable();
            return new PagedList<Student>(
                    students, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public Student GetStudentById(Guid id)
        {
            return FindByCondition(s => s.Id.Equals(id))
                    .DefaultIfEmpty(new Student())
                    .FirstOrDefault();
        }

        public bool RegisterStudent(Student model)
        {
            bool isDone = false;

            model.CreateDate = DateTime.Now;
            model.ModifyDate = DateTime.Now;
            model.Id = Guid.NewGuid();
            model.CreateUserId = Guid.NewGuid();
            model.ModifyUserId = model.CreateUserId;
            model.StatusId = 1;
            try
            {
                Create(model);
                Save();
                isDone = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return isDone;
        }

        public bool UpdateStudent(Student dbStudent, Student student)
        {
            bool isDone = false;
            try
            {
                dbStudent.Map(student);
                Update(dbStudent);
                Save();
                isDone = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return isDone;
        }
    }
}
