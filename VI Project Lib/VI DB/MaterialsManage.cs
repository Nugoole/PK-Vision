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
    
    public partial class MaterialsManage
    {
        public int MaterialsManageId { get; set; }
        public int LotId { get; set; }
        public int ProcessId { get; set; }
        public int MaterialStateId { get; set; }
        public int PassQuantity { get; set; }
        public int ErrorQuantity { get; set; }
        public int TotalQuantity { get; set; }
    
        public virtual Lot Lot { get; set; }
        public virtual MaterialState MaterialState { get; set; }
        public virtual Process Process { get; set; }
    }
}
