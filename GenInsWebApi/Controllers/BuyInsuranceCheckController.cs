using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenInsWebApi.Models;

namespace GenInsWebApi.Controllers
{
    public class BuyInsuranceCheckController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();

        public IHttpActionResult Subscheck(regnoclass robj)
        {
            try
            {
                //checking active subscription plans

                bool subsPlanExists = db.Subscription_plan.Any(x => x.Reg_No == robj.registration_no && x.Status_of_sub == "Active");

                var responseobj = new response();

                if (subsPlanExists != true)
                {
                    responseobj.message = "Valid";
                }
                else
                {
                    responseobj.message = "Invalid";
                }

                return Ok(responseobj);
            }

            //catches an exception

            catch(Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                return Ok(response);
            }
            
        }
    }

    public class regnoclass
    {
        public string registration_no { get; set; }


    }

    public class response
    {
        public string message { get; set; }
    }
}
