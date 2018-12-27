using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Studentio.Contracts.IRepositoryWrapper;
using Studentio.Entities.Models;

namespace Studentio.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //StudentRepo
        //CourseRepo
        private IRepositoryWrapper _repoWrapper;

        public ValuesController(IRepositoryWrapper repositoryWrapper)
        {
            _repoWrapper = repositoryWrapper;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _repoWrapper.Student.GetAllStudents();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
