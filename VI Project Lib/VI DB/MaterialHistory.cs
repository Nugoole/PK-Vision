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
    
    public partial class MaterialHistory
    {
        public int MaterialHistoryId { get; set; }
        public int LotId { get; set; }
        public int BarcodeId { get; set; }
        public string MaterialState { get; set; }
        public System.DateTime EnterDate { get; set; }
        public int EnterQuantity { get; set; }
        public int WorkPlaceId { get; set; }
    
        public virtual Barcode Barcode { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual WorkPlace WorkPlace { get; set; }
    }
}
