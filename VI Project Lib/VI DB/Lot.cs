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
    
    public partial class Lot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lot()
        {
            this.DefectiveProducts = new HashSet<DefectiveProduct>();
            this.LotBarCodes = new HashSet<LotBarCode>();
            this.MaterialHistories = new HashSet<MaterialHistory>();
            this.MaterialsManages = new HashSet<MaterialsManage>();
        }
    
        public int LotId { get; set; }
        public string LotCode { get; set; }
        public int WorkPlaceId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DefectiveProduct> DefectiveProducts { get; set; }
        public virtual WorkPlace WorkPlace { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LotBarCode> LotBarCodes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialHistory> MaterialHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialsManage> MaterialsManages { get; set; }
    }
}
