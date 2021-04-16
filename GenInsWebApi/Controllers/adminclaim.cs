using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenInsWebApi.Controllers
{
    public class adminclaim
    {
        public  int Claim_no { get; set; }
        public int Policy_no { get; set; }
        public  DateTime Claim_date { get; set; }
        public string Claim_approval_status { get; set; }
    }
}