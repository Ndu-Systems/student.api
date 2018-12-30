using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studentio.Contracts.ILoggerService;
using Studentio.Contracts.IRepositoryWrapper;
using Studentio.Contracts.IToken;
using Studentio.Entities.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Studentio.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private ITokenReepository _repoWrapper;
        private ILoggerManager _logger;

        public UsersController(ITokenReepository repoWrapper, ILoggerManager logger)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Token")]
        public IActionResult RequestToken([FromBody] TokenRequest request)
        {

            try
            {
                var token = _repoWrapper.RequestToken(request.Username, request.Password);
                if (token != null) { 
                    _logger.LogInfo($"Authentication successful for {request.Username} @ : {DateTime.Now}");
                    return Ok(token);
                }
                else
                {
                    _logger.LogError($"User {request.Username} has not entered the correct credentials");
                    return BadRequest("Invalid request");

                }

            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
    }
}
