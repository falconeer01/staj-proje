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
    
    public partial class Chef
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chef()
        {
            this.Employee = new HashSet<Employee>();
        }
    
        public int C_Record_Number { get; set; }
        public Nullable<int> M_Record_Number { get; set; }
        public string Chef_Name { get; set; }
        public Nullable<bool> isUpdated { get; set; }
        public Nullable<bool> isActive { get; set; }
    
        public virtual Manager Manager { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
