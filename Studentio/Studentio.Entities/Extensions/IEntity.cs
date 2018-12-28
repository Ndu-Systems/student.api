using System;
using System.Collections.Generic;
using System.Text;

namespace Studentio.Entities.Extensions
{
    public interface IEntity
    {
         Guid Id { get; set; }
         Guid CreateUserId { get; set; }
         DateTime CreateDate { get; set; }
         Guid ModifyUserId { get; set; }
         DateTime ModifyDate { get; set; }
         int StatusId { get; set; }
    }
}
