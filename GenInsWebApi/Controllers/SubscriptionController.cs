using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenInsWebApi.Models;

namespace GenInsWebApi.Controllers
{
    public class SubscriptionController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();
        [HttpPost]
        public object subscriptionPlan_details(int User_Id)
        {
            var res = db.Subscription_plan
                .Where(x => x.User_Id == User_Id && x.User_Id == x.Vehicle_Info.User_Id)
                .Select(x => new Subscriptionresponse()
                {
                    Plan_type = x.Policy_Plans.Plan_type,
                    Vehicle_Type = x.Vehicle_Info.Vehicle_Type,
                    Manufacturer_Name = x.Vehicle_Info.Manufacturer_Name,
                    Model_Name = x.Vehicle_Info.Model_Name,
                    Sub_date = x.Sub_date,
                    End_date = x.End_date,
                    Policy_No = x.Policy_No,
                    Reg_No = x.Reg_No,
                    Engine_No = x.Vehicle_Info.Engine_No,
                    Chasis_No = x.Vehicle_Info.Chasis_No,
                    Status_of_sub = x.Status_of_sub,
                    message = "Successfull"
                }).ToList();


            return Ok(res);

        }
    }
    public class UserInfo
    {
        public int User_Id { get; set; }
    }
    public class Subscriptionresponse
    {
        public string Plan_type { get; set; }
        public string Vehicle_Type { get; set; }
        public string Manufacturer_Name { get; set; }
        public string Model_Name { get; set; }
        public string Reg_No { get; set; }
        public int? Engine_No { get; set; }
        public int? Chasis_No { get; set; }
        public DateTime? Sub_date { get; set; }
        public DateTime? End_date { get; set; }
        public int Policy_No { get; set; }
        public string Status_of_sub { get; set; }
        public string message { get; set; }
    }
}
