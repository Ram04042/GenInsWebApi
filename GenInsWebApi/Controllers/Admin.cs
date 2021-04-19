using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenInsWebApi.Controllers
{
    public class Admin
    {
        public int Claim_no { get; set; }
        public int? Policy_No { get; set; }
        public string Reasons { get; set; }
        public DateTime? Date_claimed { get; set; }
        public DateTime? Date_of_Loss { get; set; }
        public string Place_of_Loss { get; set; }
        public string Damage_Description { get; set; }
        public bool Injury_to_Thirdparty { get; set; }
        public string Claim_approval_status { get; set; }
        public decimal Claim_amt { get; set; }

        public string message { get; set; }
    }
}