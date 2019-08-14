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
    
    public partial class Facility
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Facility()
        {
            this.FacilitiesHistories = new HashSet<FacilitiesHistory>();
            this.FacilitiesPowers = new HashSet<FacilitiesPower>();
            this.WorkLogs = new HashSet<WorkLog>();
        }
    
        public int FacilitiesId { get; set; }
        public int FacilitiesNo { get; set; }
        public string Name { get; set; }
        public bool InspectionState { get; set; }
        public bool UseState { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double length { get; set; }
        public double volume { get; set; }
        public int ProcessId { get; set; }
        public double Weight { get; set; }
    
        public virtual Process Process { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacilitiesHistory> FacilitiesHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacilitiesPower> FacilitiesPowers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkLog> WorkLogs { get; set; }
    }
}
