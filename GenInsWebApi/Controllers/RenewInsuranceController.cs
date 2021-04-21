using GenInsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenInsWebApi.Controllers
{
    public class RenewInsuranceController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();

        // Executes renew_policy Procedure to Renew Insurance
        public IHttpActionResult insert(RenewInsuranceApi apiObj)
        {
            try
            {
                // Creating Object Parameter of type string to get output from the Procedure
                ObjectParameter message = new ObjectParameter("message", typeof(string));

                // Passing Variables required as input for Procedure from the object passed to API
                // Procedure Returns Message as Output with the error message handled and returned in Procedure
                // If no error returns Message Successfull
                var res = db.renew_policy(

                    apiObj.User_Id,
                    apiObj.policy_no,
                    apiObj.market_price,
                    apiObj.plan_type,
                    apiObj.plan_duration,
                    apiObj.idv,
                    apiObj.total_tp,
                    apiObj.total_od,
                    apiObj.total_payable,
                    apiObj.card_holder_name,
                    apiObj.card_no,
                    apiObj.card_exp_month,
                    apiObj.card_exp_year,
                    message);

                return Ok(message);
            }
            catch(Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                return Ok(response);
            }
            
        }
    }

}

