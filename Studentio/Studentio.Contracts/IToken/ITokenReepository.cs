using Studentio.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Contracts.IToken
{
    public interface ITokenReepository
    {
        TokenResponse RequestToken(string email, string password);
    }
}
