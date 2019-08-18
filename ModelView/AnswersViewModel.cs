using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ideal.ModelView
{
    public class AnswersViewModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int IsDeleted { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
    }
}