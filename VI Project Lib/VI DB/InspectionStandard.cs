//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VI_DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class InspectionStandard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InspectionStandard()
        {
            this.Processes = new HashSet<Process>();
        }
    
        public int InspectionStandardId { get; set; }
        public int InspectionStandardTypeId { get; set; }
        public string Note { get; set; }
    
        public virtual InspectionStandardType InspectionStandardType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Process> Processes { get; set; }
    }
}