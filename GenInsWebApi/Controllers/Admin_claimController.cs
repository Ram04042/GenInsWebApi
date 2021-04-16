using GenInsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenInsWebApi.Controllers
{
    public class Admin_claimController : ApiController
    {
        GeneralInsuranceEntities3 db = new GeneralInsuranceEntities3();
        public IHttpActionResult Getadmin(adminclaim Adminclaim)

        {
            var details = db.Claim_Insurance.ToList();
            return Ok(details);

        }
        public IHttpActionResult Get(int claimno)
        {
            return Ok(db.Claim_Insurance.Where(x => x.Claim_no == claimno).FirstOrDefault());
        }

    }
}
