using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studentio.Contracts.ILoggerService;
using Studentio.Contracts.IRepositoryWrapper;
using Studentio.Entities.Extensions;
using Studentio.Entities.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Studentio.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repoWrapper;

        public StudentsController(ILoggerManager logger, IRepositoryWrapper repoWrapper)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
        }



        // GET: api/students
        [HttpGet]
        public IActionResult Get()
        {        
            try
            {
                var students = _repoWrapper.Student.GetAllStudents();
                if (students != null)
                    _logger.LogInfo($"Students returned from database at : {DateTime.Now}");
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Students/Get error: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
          
        }

        // GET api/students/05bce903-0eac-74f2-c730-d63f4a876064
        [HttpGet("{id}", Name ="StudentById")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var student = _repoWrapper.Student.GetStudentById(id);
                if (student.IsEmptyObject())
                {
                    _logger.LogError($"Student with id : {id}, has not been found in our database");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned student with id : {student.Id}");
                    return Ok(student);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside StudentById error: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        // POST api/students
        [HttpPost]
        public IActionResult RegisterStudent([FromBody]Student student)
        {
            try
            {
                if (student.IsObjectNull())
                {
                    _logger.LogError($"Student model sent from RegisterStudent at : {DateTime.Now}, is a Null object");
                    return BadRequest("Object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Student model sent from RegisterStudent at : {DateTime.Now}, is invalid object");
                    return BadRequest("Object is invalid");
                }
                _repoWrapper.Student.RegisterStudent(student);
                _logger.LogInfo($"Student with id : {student.Id} has been successfully registered on {DateTime.Now}");
                return CreatedAtRoute("StudentById", new { id = student.Id }, student);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside RegisterStudent error: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(Guid id, [FromBody]Student student)
        {
            try
            {
                if (student.IsObjectNull())
                {
                    _logger.LogError($"Student model sent from UpdateStudent at : {DateTime.Now}, is a Null object");
                    return BadRequest("Object is null");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Student model sent from UpdateStudent at : {DateTime.Now}, is invalid object");
                    return BadRequest("Object is invalid");
                }

                var dbStudent = _repoWrapper.Student.GetStudentById(id);
                _repoWrapper.Student.UpdateStudent(dbStudent, student);
                _logger.LogInfo($"Student with id : {student.Id} has been successfully updated on {DateTime.Now}");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateStudent error: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
