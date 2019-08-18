using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ideal.ModelView
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Question { get; set; }
        public string Category { get; set; }
        public string CategoryName { get; set; }
        public string EmpId { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    }
}