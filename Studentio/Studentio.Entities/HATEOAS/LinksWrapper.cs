using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Studentio.Entities.HATEOAS
{
    public class LinksWrapper<T>
    {
        public T Value { get; set; }
        public List<LinkInfo> Links { get; set; }
    }

    public class LinksWrapperList<T>
    {
        public List<LinksWrapper<T>> Values { get; set; }
        public List<LinkInfo> Links { get; set; }
    }
}
