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
    
    public partial class sp_getPensionPlanPendingRequest_Result
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long MobileNumber { get; set; }
        public int PurchaseId { get; set; }
        public string InsuranceType { get; set; }
        public string SubType { get; set; }
        public Nullable<System.DateTime> DateOfPurchase { get; set; }
        public long MonthlyIncome { get; set; }
        public string Occupation { get; set; }
        public System.DateTime PensionStartYear { get; set; }
        public int Age { get; set; }
        public Nullable<long> PensionAmount { get; set; }
    }
}