//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GenInsWebApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Claim_Insurance
    {
        public int Claim_no { get; set; }
        public Nullable<int> Policy_No { get; set; }
        public string Reasons { get; set; }
        public Nullable<System.DateTime> Date_claimed { get; set; }
        public Nullable<System.DateTime> Date_of_Loss { get; set; }
        public string Place_of_Loss { get; set; }
        public string Damage_Description { get; set; }
        public Nullable<bool> Injury_to_Thirdparty { get; set; }
        public string Claim_approval_status { get; set; }
        public Nullable<decimal> Claim_amt { get; set; }
        public string message { get; set; }
        public virtual Subscription_plan Subscription_plan { get; set; }
    }
}
