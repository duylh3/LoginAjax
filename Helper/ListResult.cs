using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ideal.Helper
{
    public class ListResult<T> : ListResultBase where T : class
    {
        public ListResult()
        {
            Results = new List<T>();
        }
        public IList<T> Results { get; set; }
    }
}