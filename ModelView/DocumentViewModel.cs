using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ideal.ModelView
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string DocumentName { get; set; }
        public string Username { get; set; }
        public string CategoryName { get; set; }
        public string Url { get; set; }
        public int IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
}