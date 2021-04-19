using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenInsWebApi.Models;

namespace GenInsWebApi.Controllers
{
    public class AdminController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();
      
        public object GetClaims()
        {
            var claim = db.Claim_Insurance
                .Select(x => new AdminResponse()
                {
                    Claim_no = x.Claim_no,
                    Policy_No = x.Policy_No,
                    Claim_approval_status = x.Claim_approval_status,
                    Date_claimed = x.Date_claimed,
                    Claim_amt=x.Claim_amt,
                    message = "Successfull"
                }).ToList();
            return Ok(claim);
        }

        public IHttpActionResult Get(int Claim_no)
        {
            var claim_info= db.Claim_Insurance
                .Where(x => x.Claim_no == Claim_no)
                .Select(x => new AdminResponse()
                {
                    Claim_no = x.Claim_no,
                    Policy_No = x.Policy_No,
                    Reasons = x.Reasons,
                    Date_claimed = x.Date_claimed,
                    Claim_approval_status = x.Claim_approval_status,
                    Claim_amt=x.Claim_amt,
                }).FirstOrDefault();
            return Ok(claim_info);
        }
        [HttpPut]
        public IHttpActionResult Update(int Claim_no,Admin insurance)
        {
            var info = db.Claim_Insurance
                .Where(x => x.Claim_no == Claim_no).FirstOrDefault<Claim_Insurance>();
            if(info!=null)
            {
                insurance.message= "Successfull";
                info.Claim_approval_status = insurance.Claim_approval_status;
                info.Claim_amt = insurance.Claim_amt;
                db.SaveChanges();
                return Ok(insurance.message);
            }
            else
            {
                insurance.message = "Invalid";
                return Ok(insurance.message);
            }
        }
    }

    public class AdminResponse
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
        public decimal? Claim_amt { get; set; }

        public string message { get; set; }
    }
}
