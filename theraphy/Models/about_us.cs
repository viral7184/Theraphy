//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace theraphy.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class about_us
    {
        public int ID { get; set; }
        public string ABOUT_US1 { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
        public Nullable<int> CREATED_BY { get; set; }
        public Nullable<int> UPDATED_BY { get; set; }
        public Nullable<bool> IS_DELETED { get; set; }
        public Nullable<bool> IS_ACTIVE { get; set; }
    
        public virtual login login { get; set; }
        public virtual login login1 { get; set; }
    }
}