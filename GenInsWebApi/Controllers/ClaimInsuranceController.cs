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
            db.Claim_Insurance.Add(claim);
            db.SaveChanges();
            return Ok();
        }
        public IHttpActionResult GetClaims()
        {
            var claims = db.Claim_Insurance.ToList();
            return Ok(claims);
        }
    }
}
