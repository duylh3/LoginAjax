using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ideal.ModelView
{
    public class HistoryViewModel
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> EnterDate { get; set; }
        public string WorkGroup { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
    }
}