using Studentio.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Studentio.Entities.Models
{
    [Table("user")]
    public class Student : IEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string cell { get; set; }
        public string address { get; set; }
        public string password { get; set; }
        public string createdate { get; set; }
        public string role { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string userstatus { get; set; }
        public string idnumber { get; set; }
        public string user_nmuber { get; set; }
        public string token { get; set; }
    }

}
