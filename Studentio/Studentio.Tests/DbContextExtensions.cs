using Studentio.Entities.Context;
using Studentio.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Tests
{
    public static class DbContextExtensions
    {
        public static void Seed(this RepositoryContext repositoryContext)
        {
            //Add student entities for RepositoryContext instance
            repositoryContext.Students.Add(new Student
            {
                Id = new Guid("f6b9c8f1-ae2c-ac57-b4d5-5de25279b498"),
                Firstname = "Keith",
                Surname = "Wong",
                Email = "Ut.sagittis@cursusnon.org",
                Cellphone = "(27)44 134 3476",
                IdNumber = "1609091874699",
                StudentNumber = "389147-0787",
                AddressLine1 = "643-2282 Nec Street",
                AddressLine2 = "2009 Fusce Ave",
                AddressLine3 = "P.O. Box 152, 8160 Eros Street",
                City = "Gentbrugge",
                Postcode = "98706-470",
                CreateUserId = new Guid("e24cd05f-3904-296f-6581-ee4c1e431833"),
                CreateDate = DateTime.Now.Date,
                ModifyUserId = new Guid("c2009c59-41d9-47ff-1b05-b6b95870f407"),
                ModifyDate = DateTime.Now.Date,
                StatusId = 1
            });
            repositoryContext.Students.Add(new Student
            {
                Id = new Guid("efb6bdfd-3e4a-252f-1f7c-f17c796a8a16"),
                Firstname = "Tester",
                Surname = "WonNewsg",
                Email = "Ut.sagittis@cursusnon.org",
                Cellphone = "(27)44 134 3476",
                IdNumber = "1609091874699",
                StudentNumber = "389147-0787",
                AddressLine1 = "643-2282 Nec Street",
                AddressLine2 = "2009 Fusce Ave",
                AddressLine3 = "P.O. Box 152, 8160 Eros Street",
                City = "Gentbrugge",
                Postcode = "98706-470",
                CreateUserId = new Guid("e24cd05f-3904-296f-6581-ee4c1e431833"),
                CreateDate = DateTime.Now.Date,
                ModifyUserId = new Guid("c2009c59-41d9-47ff-1b05-b6b95870f407"),
                ModifyDate = DateTime.Now.Date,
                StatusId = 1
            });
            repositoryContext.Students.Add(new Student
            {
                Id = new Guid("c2009c59-41d9-47ff-1b05-b6b95870f407"),
                Firstname = "Timmy",
                Surname = "Turnner",
                Email = "Ut.sagittis@cursusnon.org",
                Cellphone = "(27)44 134 3476",
                IdNumber = "1609091874699",
                StudentNumber = "389147-0787",
                AddressLine1 = "643-2282 Nec Street",
                AddressLine2 = "2009 Fusce Ave",
                AddressLine3 = "P.O. Box 152, 8160 Eros Street",
                City = "Gentbrugge",
                Postcode = "98706-470",
                CreateUserId = new Guid("e24cd05f-3904-296f-6581-ee4c1e431833"),
                CreateDate = DateTime.Now.Date,
                ModifyUserId = new Guid("c2009c59-41d9-47ff-1b05-b6b95870f407"),
                ModifyDate = DateTime.Now.Date,
                StatusId = 1
            });

            repositoryContext.SaveChanges();
        }
    }
}
