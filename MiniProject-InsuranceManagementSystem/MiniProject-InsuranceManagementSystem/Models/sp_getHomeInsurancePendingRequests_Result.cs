//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MiniProject_InsuranceManagementSystem.Models
{
    using System;
    
    public partial class sp_getHomeInsurancePendingRequests_Result
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long MobileNumber { get; set; }
        public int PurchaseId { get; set; }
        public string InsuranceType { get; set; }
        public string SubType { get; set; }
        public Nullable<System.DateTime> DateOfPurchase { get; set; }
        public long Valuation { get; set; }
        public string City { get; set; }
        public int PlanDuration { get; set; }
        public string HouseNumber { get; set; }
        public Nullable<int> FloorArea { get; set; }
    }
}
