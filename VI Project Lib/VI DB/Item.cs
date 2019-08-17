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
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.FirstInspections = new HashSet<FirstInspection>();
            this.ProductionHistories = new HashSet<ProductionHistory>();
            this.LastInspections = new HashSet<LastInspection>();
            this.Lots = new HashSet<Lot>();
            this.ProcessDefects = new HashSet<ProcessDefect>();
            this.TotalMonitorings = new HashSet<TotalMonitoring>();
            this.WorkOrders = new HashSet<WorkOrder>();
        }
    
        public int ItemId { get; set; }
        public string Code { get; set; }
        public int BaecodeId { get; set; }
        public string Name { get; set; }
        public int FirstItemDivisionId { get; set; }
        public int SecondItemDivisionId { get; set; }
        public int WorkPlaceId { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
    
        public virtual Barcode Barcode { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FirstInspection> FirstInspections { get; set; }
        public virtual FirstItemDivision FirstItemDivision { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductionHistory> ProductionHistories { get; set; }
        public virtual SecondItemDivision SecondItemDivision { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LastInspection> LastInspections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lot> Lots { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessDefect> ProcessDefects { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TotalMonitoring> TotalMonitorings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}