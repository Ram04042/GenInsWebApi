using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenInsWebApi.Models
{
    public class RenewInsuranceApi
    {
        public int User_Id { get; set; }

        public int policy_no { get; set; }

        public string veh_type { get; set; }

        public int brand_name { get; set; }

        public string model_name { get; set; }

        public string license_no { get; set; }

        public DateTime? purchase_date { get; set; }

        public string registeration_number { get; set; }

        public int engine_number { get; set; }

        public int vehicle_cc { get; set; }

        public int chassis_number { get; set; }

        public decimal market_price { get; set; }

        public string plan_type { get; set; }

        public int plan_duration { get; set; }

        public decimal idv { get; set; }

        public decimal total_tp { get; set; }

        public decimal total_od { get; set; }

        public decimal total_payable { get; set; }

        public string card_holder_name { get; set; }

        public decimal card_no { get; set; }

        public int card_exp_month { get; set; }

        public int card_exp_year { get; set; }

        public int card_cvc { get; set; }

        public string insurance_type { get; set; }
    }
}