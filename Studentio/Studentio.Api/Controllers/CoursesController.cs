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
    public class CoursesController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repoWrapper;
        private readonly IUrlHelper _urlHelper;

        public CoursesController(IRepositoryWrapper repoWrapper, IUrlHelper urlHelper, ILoggerManager logger) {

            _repoWrapper = repoWrapper;
            _urlHelper = urlHelper;
            _logger = logger;

        }

        [Route("/")]
        [HttpGet(Name = "GetCourses")]

        public IActionResult Get(PagingParams pagingParams, [FromHeader(Name ="Accept")]string acceptHeader)
        {
            try
            {
                var courses = _repoWrapper.Course.GetAllCourses(pagingParams);
                
                if(string.Equals(acceptHeader, "application/vnd.fiver.hateoas+json"))
                {
                    _logger.LogInfo($"Courses returned from database at : {DateTime.Now}");
                    return Ok(ToOutputModel_Links(courses));
                }
                else
                    return Ok(ToOutputModel_Default(courses));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Courses/Get error: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        [Route("/")]
        [HttpGet("{id}", Name = "GetCourse")]
        public IActionResult Get(Guid id , [FromHeader(Name = "Accept")]string acceptHeader)
        {
            try
            {
                var course = _repoWrapper.Course.GetCourseById(id);
                if (course.IsEmptyObject())
                {
                    _logger.LogError($"Course with id : {id}, Has not been found in our database");
                    return NotFound();
                }
                else
                    return Ok(ToOutputModel_Links(course));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Courses/GetCourse err : {ex.Message}");
                return StatusCode(500, "Internal server error");
                
            }
        }

        #region "Links" HATEOAS = Hypermedia as the engine of application state


        //Links for a Collection
        private List<LinkInfo> GetLinks_List(PagedList<Course> model)
        {
            var links = new List<LinkInfo>();

            links.Add(new LinkInfo
            {
                Href = _urlHelper.Link("GetCourses", new { PageNumber = model.PreviousPageNumber, model.PageSize }),
                Rel ="self",
                Method = "GET"
            });

            if (model.HasPreviousPage)
            {
                links.Add(new LinkInfo
                {
                    Href = _urlHelper.Link("GetCourses", new { PageNumber = model.PreviousPageNumber, model.PageSize }),
                    Rel = "has-previous-page",
                    Method = "GET"
                });
            }

            if (model.HasNextPage)
            {
                links.Add(new LinkInfo
                {
                    Href = _urlHelper.Link("GetCourses", new { PageNumber = model.PreviousPageNumber, model.PageSize }),
                    Rel = "has-next-page",
                    Method = "GET"
                });
            }

          
           links.Add(new LinkInfo
           {
               Href = _urlHelper.Link("RegisterCourse", new { PageNumber = model.PreviousPageNumber, model.PageSize }),
               Rel = "register-a-course",
               Method = "POST"
           });
       

            return links;
            
        }

        //Links for a single model
        private List<LinkInfo> GetLinks_Model(Course model)
        {
            var links = new List<LinkInfo>();

            links.Add(new LinkInfo
            {
                Href = _urlHelper.Link("GetCourse", new { id = model.Id }),
                Rel = "self",
                Method = "GET"
            });

            return links;
        }


        #endregion

        #region Mappings

        private List<Course> ToOutputModel_Default(PagedList<Course> model)
        {
            return model.List.Select(m => ToOutputModel_Default(m)).ToList();
        }

        private Course ToOutputModel_Default(Course model)
        {
            return new Course
            {
                Id = model.Id,
                CourseCode = model.CourseCode,
                Description = model.Description,
                Credits = model.Credits,
                CreateUserId = model.CreateUserId,
                CreateDate = model.CreateDate,
                ModifyUserId = model.ModifyUserId,
                ModifyDate = model.ModifyDate,
                StatusId = model.StatusId
            };                
        }

        private LinksWrapperList<Course> ToOutputModel_Links(PagedList<Course> model)
        {
            return new LinksWrapperList<Course>
            {
                Values = model.List.Select(m => ToOutputModel_Links(m)).ToList(),
                Links = GetLinks_List(model)
            };
        }

        private LinksWrapper<Course> ToOutputModel_Links(Course model)
        {
            return new LinksWrapper<Course>
            {
                Value = new Course
                {

                    Id = model.Id,
                    CourseCode = model.CourseCode,
                    Description = model.Description,
                    Credits = model.Credits,
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
