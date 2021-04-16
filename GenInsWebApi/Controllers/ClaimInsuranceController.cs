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
        GeneralInsuranceEntities3 db = new GeneralInsuranceEntities3();

        public IHttpActionResult Claim(Claim_Insurance claim)
        {
            claim.Claim_approval_status = "Pending";
            db.Claim_Insurance.Add(claim);
            db.SaveChanges();
            return Ok();
        }
    }
}
