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
    
    public partial class ProductionHistory
    {
        public int ProductionHistoryId { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public int ProcessId { get; set; }
        public int ItemId { get; set; }
        public int PassQuantity { get; set; }
        public int ErrorQuantity { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public int FacilitiesId { get; set; }
    
        public virtual Facility Facility { get; set; }
        public virtual Item Item { get; set; }
        public virtual Process Process { get; set; }
    }
}
