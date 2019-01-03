using System;
using System.Collections.Generic;
using System.Linq;
 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 using Studentio.Contracts.ILoggerService;
using Studentio.Contracts.IRepositoryWrapper;
using Studentio.Entities.Extensions;
using Studentio.Entities.HATEOAS;
using Studentio.Entities.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Studentio.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class StudentsController : BaseController
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repoWrapper;
        private readonly IUrlHelper _urlHelper;
        public StudentsController(ILoggerManager logger, IRepositoryWrapper repoWrapper, IUrlHelper urlHelper)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
            _urlHelper = urlHelper;
        }



        // GET: api/students
        [HttpGet(Name = "GetStudents")]
        public IActionResult Get(
            PagingParams pagingParams,
            [FromHeader(Name = "Accept")]string acceptHeader)
        {        
            try
            {
                var students = _repoWrapper.Student.GetAllStudents(pagingParams);
                Response.Headers.Add("X-Pagination", students.GetHeader().ToJson());
                //if (students != null)
                //    _logger.LogInfo($"Students returned from database at : {DateTime.Now}");
                //return Ok(students);
                if (string.Equals(acceptHeader, "application/vnd.fiver.hateoas+json"))
                {
                    _logger.LogInfo($"Students returned from database at : {DateTime.Now}");
                    var outputModel = ToOutputModel_Links(students);
                    return Ok(outputModel);

                }
                else
                {
                    var outputModel = ToOutputModel_Default(students);
                    return Ok(outputModel);
                }

             }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Students/Get error: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
          
        }

        // GET api/students/05bce903-0eac-74f2-c730-d63f4a876064
        [HttpGet("{id}", Name ="GetStudent")]
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
                return CreatedAtRoute("GetStudent", new { id = student.Id }, student);
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


        #region "Links" HATEOAS = Hypermedia as the engine of application state

        //Links for List Looking to make a Repository for re-use
        private List<LinkInfo> GetLinks_List(PagedList<Student> model)
        {
            var links = new List<LinkInfo>();

            links.Add(new LinkInfo
            {
                Href = _urlHelper.Link("GetStudents",
                          new { PageNumber = model.PreviousPageNumber, PageSize = model.PageSize }),
                Rel = "self",
                Method = "GET"
            });

            if (model.HasPreviousPage)
            {
                links.Add(new LinkInfo
                {
                    Href = _urlHelper.Link("GetStudent",
                          new { PageNumber = model.PreviousPageNumber, PageSize = model.PageSize }),
                    Rel = "previous-page",
                    Method = "GET"
                });
            }

            if (model.HasNextPage)
            {
                links.Add(new LinkInfo
                {
                    Href = _urlHelper.Link("GetStudents",
                          new { PageNumber = model.PreviousPageNumber, PageSize = model.PageSize }),
                    Rel = "next-page",
                    Method = "GET"
                });
            }

            links.Add(new LinkInfo
            {
                Href = _urlHelper.Link("RegisterStudent", new { }),
                Rel = "register-a-student",
                Method = "POST"
                
            });

            return links;
        }

        //Links for a single model
        private List<LinkInfo> GetLinks_Model(Student model)
        {
            var links = new List<LinkInfo>();

            links.Add(new LinkInfo
            {
                Href = _urlHelper.Link("GetStudent", new { id = model.Id}),
                Rel = "self",
                Method = "GET"
            });

             links.Add(new LinkInfo
            {
                Href = _urlHelper.Link("UpdateStudent", new { id = model.Id}),
                Rel = "self",
                Method = "GET"
            });

            return links;

        }
        #endregion

        #region Mappings
        private List<Student> ToOutputModel_Default(PagedList<Student> model)
        {
            return model.List.Select(m => ToOutputModel_Default(m)).ToList();

        }       

        private Student ToOutputModel_Default(Student model)
        {
            return new Student
            {
                Id = model.Id,
                Firstname = model.Firstname,
                Surname = model.Surname,
                Email = model.Email,
                Cellphone = model.Cellphone,
                IdNumber = model.IdNumber,
                StudentNumber = model.StudentNumber,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                AddressLine3 = model.AddressLine3,
                City = model.City,
                Postcode = model.Postcode,
                CreateUserId = model.CreateUserId,
                CreateDate = model.CreateDate,
                ModifyUserId = model.ModifyUserId,
                ModifyDate = model.ModifyDate,
                StatusId = model.StatusId
            };
        }

        private LinksWrapperList<Student> ToOutputModel_Links(PagedList<Student> model)
        {
            return new LinksWrapperList<Student>
            {
                Values = model.List.Select(m => ToOutputModel_Links(m)).ToList(),
                Links = GetLinks_List(model)
            };
        }

        private LinksWrapper<Student> ToOutputModel_Links(Student model)
        {
            return new LinksWrapper<Student>
            {
                Value = new Student
                {
                    Id = model.Id,
                    Firstname = model.Firstname,
                    Surname = model.Surname,
                    Email = model.Email,
                    Cellphone = model.Cellphone,
                    IdNumber = model.IdNumber,
                    StudentNumber = model.StudentNumber,
                    AddressLine1 = model.AddressLine1,
                    AddressLine2 = model.AddressLine2,
                    AddressLine3 = model.AddressLine3,
                    City = model.City,
                    Postcode = model.Postcode,
                    CreateUserId = model.CreateUserId,
                    CreateDate = model.CreateDate,
                    ModifyUserId = model.ModifyUserId,
                    ModifyDate = model.ModifyDate,
                    StatusId = model.StatusId
                },
                Links = GetLinks_Model(model)
            };
        }
        #endregion
    }
}
