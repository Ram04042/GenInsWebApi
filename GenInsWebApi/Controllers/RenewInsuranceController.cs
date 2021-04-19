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

        public IHttpActionResult insert(RenewInsuranceApi apiObj)
        {
            ObjectParameter message = new ObjectParameter("message", typeof(string));

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
    }

}

