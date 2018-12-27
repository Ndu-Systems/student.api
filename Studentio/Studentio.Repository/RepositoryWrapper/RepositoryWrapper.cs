using Studentio.Contracts.IRepositoryWrapper;
using Studentio.Contracts.IStudent;
using Studentio.Entities.Context;
using Studentio.Repository.Students;
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

    }
}
