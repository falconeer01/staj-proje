//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace proje
{
    using System;
    using System.Collections.Generic;
    
    public partial class General_Manager
    {
        public int GM_Record_Number { get; set; }
        public string GM_Name { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<bool> isUpdated { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
