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
    using System.Collections.Generic;
    
    public partial class LifeInsurance
    {
        public Nullable<int> CustomerId { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Occupation { get; set; }
        public string Plan_Duration { get; set; }
        public string HealthCondition { get; set; }
        public System.DateTime PensionStartYear { get; set; }
        public long InsuranceAmount { get; set; }
        public int id { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
