//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ideal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_Questions
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Question { get; set; }
        public int IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public byte[] ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    }
}
