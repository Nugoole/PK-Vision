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
    
    public partial class FacilitiesPower
    {
        public int FacilitiesPowerId { get; set; }
        public int ProcessId { get; set; }
        public int FacilitiesId { get; set; }
        public int ProductionHistoryId { get; set; }
    
        public virtual Facility Facility { get; set; }
        public virtual Process Process { get; set; }
        public virtual ProductionHistory ProductionHistory { get; set; }
    }
}
