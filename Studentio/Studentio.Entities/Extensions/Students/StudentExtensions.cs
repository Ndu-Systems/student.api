using Studentio.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Entities.Extensions.Students
{
    public static class StudentExtensions
    {
        public static void Map(this Student dbStudent, Student student) {

            //dbStudent.Id = student.Id;
            dbStudent.Firstname = student.Firstname;
            dbStudent.Surname = student.Surname;
            dbStudent.Email = student.Email;
            dbStudent.Cellphone = student.Cellphone;
            dbStudent.IdNumber = student.IdNumber;
            dbStudent.StudentNumber = student.StudentNumber;
            dbStudent.AddressLine1 = student.AddressLine1;
            dbStudent.AddressLine2 = student.AddressLine2;
            dbStudent.AddressLine3 = student.AddressLine3;
            dbStudent.City = student.City;
            dbStudent.Postcode = student.Postcode;
            dbStudent.CreateUserId = student.CreateUserId;
            dbStudent.CreateDate = student.CreateDate;
            dbStudent.ModifyUserId = student.ModifyUserId;
            dbStudent.ModifyDate = DateTime.Now;
            dbStudent.StatusId = student.StatusId;

        }
    }
}
