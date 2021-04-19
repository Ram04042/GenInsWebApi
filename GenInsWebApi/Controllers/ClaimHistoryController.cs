using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenInsWebApi.Models;

namespace GenInsWebApi.Controllers
{
    public class ClaimHistoryController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();

        [HttpPost]
        public object claimHistory(int User_Id)
        {
            //var Policy_No = db.Subscription_plan
            //    .Where(x => x.User_Id == User_Id)
            //    .Select(x => new claimInfo()
            //    {
            //        Policy_No = x.Policy_No
            //    });

            var res = db.Claim_Insurance
                .Where(x => x.Subscription_plan.User_Id == User_Id && x.Subscription_plan.Policy_No == x.Policy_No)
                .Select(x => new claimInfo()
                {
                    Claim_no = x.Claim_no,
                    Policy_No = x.Policy_No,
                    Date_claimed = x.Date_claimed,
                    Claim_approval_status = x.Claim_approval_status,
                    Claim_amt = x.Claim_amt,
                }).ToList();
            return Ok(res);
        }
    }
    public class claimInfo
    {
        public int Claim_no { get; set; }
        public int? Policy_No { get; set; }
        public DateTime? Date_claimed { get; set; }
        public string Claim_approval_status { get; set; }
        public decimal? Claim_amt { get; set; }
    }
}
