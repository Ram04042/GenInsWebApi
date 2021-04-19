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

    public class BuyInsuranceController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();

        public IHttpActionResult insert(BuyInsuranceApi apiObj)
        {
            ObjectParameter message = new ObjectParameter("message", typeof(string));

            var res = db.add_user_sub(
                apiObj.registeration_number,
                apiObj.veh_type,
                apiObj.brand_name,
                apiObj.license_no,
                apiObj.purchase_date,
                apiObj.User_Id,
                apiObj.model_name,
                apiObj.chassis_number,
                apiObj.vehicle_cc,
                apiObj.market_price,
                apiObj.engine_number,
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
