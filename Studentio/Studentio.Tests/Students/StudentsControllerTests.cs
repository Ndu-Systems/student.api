using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Studentio.Api.Controllers;
using Studentio.Contracts.ILoggerService;
using Studentio.Contracts.IRepositoryWrapper;
using Studentio.Contracts.IStudent;
using Studentio.Entities.Context;
using Studentio.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Studentio.Tests.Students
{
    public class StudentsControllerTests
    {
        [Fact]
        public void GetAllStudents_WhenCalled_MustReturnAllStudents()
        {

            //Arrange
            var helper = new StudentHelper();
            var studentList = helper.GetAllStudents();

            var mockStudentSet = new Mock<DbSet<Student>>();
            mockStudentSet.As<IQueryable<Student>>().Setup(p => p.Provider).Returns(studentList.Provider);
            mockStudentSet.As<IQueryable<Student>>().Setup(p => p.Expression).Returns(studentList.Expression);
            mockStudentSet.As<IQueryable<Student>>().Setup(p => p.ElementType).Returns(studentList.ElementType);
            var mockContext = new Mock<RepositoryContext>();
            mockStudentSet.As<IQueryable<Student>>().Setup(p => p.GetEnumerator()).Returns(studentList.GetEnumerator());
            mockContext.Setup(m => m.Students).Returns(mockStudentSet.Object);
            var mockLoggerService = new Mock<ILoggerManager>();
            mockLoggerService.Setup(l => l.LogInfo("This is a test"));

            var mockRepository = new Mock<IStudentRepository>();
            mockRepository.Setup(r => r.GetAllStudents()).Returns(studentList);

            var mockRepoWrapper = new Mock<IRepositoryWrapper>();
            mockRepoWrapper.Setup(wr => wr.Student).Returns(mockRepository.Object);

            //Act
            var studentsController = new StudentsController(mockLoggerService.Object, mockRepoWrapper.Object);
            var result = studentsController.Get() as OkObjectResult;

            //Assert
            Assert.NotNull(result.Value);

        }
    }
}
