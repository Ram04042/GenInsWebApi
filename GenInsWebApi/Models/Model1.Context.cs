﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class General_InsuranceEntities : DbContext
    {
        public General_InsuranceEntities()
            : base("name=General_InsuranceEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Brand_Names> Brand_Names { get; set; }
        public virtual DbSet<Claim_Insurance> Claim_Insurance { get; set; }
        public virtual DbSet<Depreciation_Percentage> Depreciation_Percentage { get; set; }
        public virtual DbSet<forgot_password> forgot_password { get; set; }
        public virtual DbSet<Model_od_prem_amt> Model_od_prem_amt { get; set; }
        public virtual DbSet<No_Claim_Bonus> No_Claim_Bonus { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Policy_Plans> Policy_Plans { get; set; }
        public virtual DbSet<Subscription_plan> Subscription_plan { get; set; }
        public virtual DbSet<Third_Party_Prem> Third_Party_Prem { get; set; }
        public virtual DbSet<User_Registration> User_Registration { get; set; }
        public virtual DbSet<Vehicle_Info> Vehicle_Info { get; set; }
    }
}
