using Studentio.Contracts.IStudent;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Contracts.IRepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        IStudentRepository Student { get;  }
    }
}
