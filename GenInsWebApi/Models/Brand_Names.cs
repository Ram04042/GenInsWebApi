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
    
    public partial class Brand_Names
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Brand_Names()
        {
            this.Model_od_prem_amt = new HashSet<Model_od_prem_amt>();
        }
    
        public int Brand_Id { get; set; }
        public string Brand_Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Model_od_prem_amt> Model_od_prem_amt { get; set; }
    }
}
