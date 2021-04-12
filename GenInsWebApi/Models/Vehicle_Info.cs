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
    
    public partial class Vehicle_Info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle_Info()
        {
            this.Subscription_plan = new HashSet<Subscription_plan>();
        }
    
        public string Reg_No { get; set; }
        public string Vehicle_Type { get; set; }
        public string Manufacturer_Name { get; set; }
        public string Driving_license { get; set; }
        public Nullable<System.DateTime> Veh_purchase_date { get; set; }
        public Nullable<int> User_Id { get; set; }
        public string Model_Name { get; set; }
        public Nullable<int> Chasis_No { get; set; }
        public Nullable<int> Vehicle_CC { get; set; }
        public Nullable<decimal> Market_price { get; set; }
    
        public virtual Model_od_prem_amt Model_od_prem_amt { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subscription_plan> Subscription_plan { get; set; }
        public virtual User_Registration User_Registration { get; set; }
    }
}
