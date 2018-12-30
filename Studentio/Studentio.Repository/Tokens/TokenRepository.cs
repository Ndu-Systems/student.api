using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Studentio.Contracts.IRepositoryWrapper;
using Studentio.Contracts.IToken;
using Studentio.Entities.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Studentio.Repository.Tokens
{
    public class TokenRepository : ITokenReepository
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryWrapper _repoWrapper;

        public TokenRepository(IConfiguration configuration, IRepositoryWrapper repoWrapper)
        {
            _configuration = configuration;
            _repoWrapper = repoWrapper;
        }
        public TokenResponse RequestToken(string email, string password)
        {
            var user = _repoWrapper.User.GetUserByEmailAndPassword(email, password);
            //var users = _repoWrapper.User.GetAllUsers();
            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),

                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Hash, password)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                   issuer: "https://www.student-io.net",
                   audience: "https://www.student-io.net",
                   claims: claims,
                   expires: DateTime.Now.AddHours(12),
                   signingCredentials: credentials
                   );

                var response = new TokenResponse();
                response.Token = new JwtSecurityTokenHandler().WriteToken(token);
                return response;
            }

            return null;
        }
    }
}
