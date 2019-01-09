using Studentio.Contracts.ICourse;
using Studentio.Contracts.IStudent;
using Studentio.Contracts.IUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Contracts.IRepositoryWrapper
{
    public interface IRepositoryWrapper
    {
        IStudentRepository Student { get;  }

        IUserRepository User { get;  }

        ICourseRepository Course { get; }
    }
}
