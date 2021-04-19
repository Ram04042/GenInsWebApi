using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenInsWebApi.Models;

namespace GenInsWebApi.Controllers
{
    public class ClaimInsuranceController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();

        public IHttpActionResult Claim(Claim_Insurance claim)
        {
            bool PlanExists = db.Subscription_plan.Any(x => x.Policy_No == claim.Policy_No && x.Status_of_sub=="active");
            if(PlanExists == true)
            {
                claim.Claim_approval_status = "Pending";
                db.Claim_Insurance.Add(claim);
                db.SaveChanges();
                claim.message = "Successfull";
                return Ok(claim);
            }
            else
            {
                claim.message = "Invalid";
                return Ok(claim);
            }
        }
        
    }
}
