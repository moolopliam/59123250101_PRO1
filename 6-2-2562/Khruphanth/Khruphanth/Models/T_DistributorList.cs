//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Khruphanth.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_DistributorList
    {
        public int DistributorList { get; set; }
        public string DL_DistributorID { get; set; }
        public string DL_KhruphanthID { get; set; }
    
        public virtual T_Distributor T_Distributor { get; set; }
        public virtual T_Khruphanth T_Khruphanth { get; set; }
    }
}
