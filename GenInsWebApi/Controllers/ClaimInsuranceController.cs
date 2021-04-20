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
        
        public IHttpActionResult Claim(ClaimInsurance_Response claim)
        {
            bool UserAuthentication = db.Subscription_plan.Any(x => x.User_Id == claim.User_Id && x.Policy_No == claim.Policy_No);
            bool PolicyActive = db.Subscription_plan.Any(x => x.Policy_No == claim.Policy_No && x.Status_of_sub=="active");
            bool ClaimExists = db.Claim_Insurance.Any(x => x.Policy_No == claim.Policy_No && (x.Claim_approval_status == "Pending" || x.Claim_approval_status == "Under Verification"));
            
            if(UserAuthentication == true && PolicyActive == true && ClaimExists != true)
            {
                Claim_Insurance claim_insurance = new Claim_Insurance();

                claim_insurance.Claim_no = claim.Claim_no;
                claim_insurance.Policy_No = claim.Policy_No;
                claim_insurance.Reasons = claim.Reasons;
                claim_insurance.Date_claimed = claim.Date_claimed;
                claim_insurance.Date_of_Loss = claim.Date_of_Loss;
                claim_insurance.Place_of_Loss = claim.Place_of_Loss;
                claim_insurance.Damage_Description = claim.Damage_Description;
                claim_insurance.Injury_to_Thirdparty = claim.Injury_to_Thirdparty;
                claim_insurance.Claim_approval_status = claim.Claim_approval_status;
                claim_insurance.Claim_amt = claim.Claim_amt;

                claim.Claim_approval_status = "Pending";
                db.Claim_Insurance.Add(claim_insurance);
                db.SaveChanges();
                claim.message = "Successfull";
                return Ok(claim);
            }
            else
            {
                if(UserAuthentication != true)
                {
                    claim.message = "You are not having this policy subscription";
                    return Ok(claim);
                }
                else if (PolicyActive != true)
                {
                    claim.message = "Policy is not active";
                    return Ok(claim);
                }
                else
                {
                    claim.message = "Claim for this policy number is already existing";
                    return Ok(claim);
                }
            }
        }
        
    }
    public class ClaimInsurance_Response
    {
        public int User_Id { get; set; }
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

        
    }
}
