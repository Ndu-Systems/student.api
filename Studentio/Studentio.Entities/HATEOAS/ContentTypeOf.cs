using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studentio.Entities.HATEOAS
{
    public class ContentTypeOf : Attribute, IActionConstraint
    {
        public ContentTypeOf(string exceptedContentType)
        {
            _expectedContentType = exceptedContentType;
        }
        private readonly string _expectedContentType;
        public int Order => 0;

        public bool Accept(ActionConstraintContext context)
        {
            var request = context.RouteContext.HttpContext.Request;

            if (!request.Headers.ContainsKey("Content-Type"))
                return false;

            return string.Equals(request.Headers["Content-Type"],
                        _expectedContentType, StringComparison.OrdinalIgnoreCase);
        }
    }
}
