using GenInsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenInsWebApi.Controllers
{
    public class RenewCheckController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();

        [HttpPost]
        public object Check(renewApiClass ra)
        {
            var res = db.Subscription_plan
                        .Where(x => x.Policy_No == ra.policyNumber &&
                                        x.User_Registration.Email_ID == ra.email &&
                                            x.User_Registration.Phone_No == ra.mobile &&
                                                x.User_Id == ra.User_Id)
                        .Select(x => new renewCheckResponse()
                        {
                            model_name = x.Vehicle_Info.Model_Name,
                            vehicleCC = x.Vehicle_Info.Vehicle_CC,
                            veh_type = x.Vehicle_Info.Vehicle_Type,
                            purchase_date = x.Vehicle_Info.Veh_purchase_date,
                            market_price = x.Vehicle_Info.Market_price,
                            subscription_status = x.Status_of_sub,
                        }).FirstOrDefault();

            var responseObj = new renewCheckResponse();
            if(res==null)
            {
                responseObj.message = "Invalid Policy Number";
            }
            else
            {
                responseObj = res;
                responseObj.subscription_status = res.subscription_status;
                responseObj.message = "Valid";
            }

            return responseObj;

        }
    }

    public class renewApiClass
    {
        public int policyNumber { get; set; }
        public string email { get; set; }

        public string mobile { get; set; }

        public int User_Id { get; set; }
    }

    public class renewCheckResponse
    {
        public string message { get; set; }
        public string subscription_status { get; set; }

        public string model_name { get; set; }
        public int? vehicleCC { get; set; }

        public decimal? market_price { get; set; }
        public string veh_type { get; set; }
        public DateTime? purchase_date { get; set; }
    }

}
