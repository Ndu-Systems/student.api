using Studentio.Contracts.ICourse;
using Studentio.Contracts.IRepositoryWrapper;
using Studentio.Contracts.IStudent;
using Studentio.Contracts.IToken;
using Studentio.Contracts.IUser;
using Studentio.Entities.Context;
using Studentio.Repository.Courses;
using Studentio.Repository.Students;
using Studentio.Repository.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Repository.RepositoryWrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        //Declarations
        private RepositoryContext _repoContext { get; set; }

        private IStudentRepository _student;
        private IUserRepository _user;
        private ICourseRepository _course;
       
        public RepositoryWrapper(RepositoryContext repoContext)
        {
            _repoContext = repoContext;
        }

        //Patient Repository
        public IStudentRepository Student
        {
            get
            {
                if (_student == null)
                {
                    _student = new StudentsRepository(_repoContext);
                }
                return _student;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }

        public ICourseRepository Course
        {
            get
            {
                if(_course == null)
                {
                    _course = new CourseRepository(_repoContext);
                }
                return _course;
            }
        }

       

    }
}
