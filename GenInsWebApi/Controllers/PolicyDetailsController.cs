using GenInsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenInsWebApi.Controllers
{
    public class PolicyDetailsController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();

        [HttpPost]
        public object getPolicyDetails(policyDetailsApi policyobj)
        {
            try
            {
                var res = db.Subscription_plan
                        .Where(x => x.Policy_No == policyobj.policy_no && x.User_Id == policyobj.User_Id)
                        .Select(x => new policyDetailsResponse()
                        {
                            Name = x.User_Registration.Name,
                            Address = x.User_Registration.Address,
                            Phone_no = x.User_Registration.Phone_No,
                            Email = x.User_Registration.Email_ID,

                            Vehicle_Type = x.Vehicle_Info.Vehicle_Type,
                            Manufacturer_Name = x.Vehicle_Info.Manufacturer_Name,
                            Model_Name = x.Vehicle_Info.Model_Name,
                            Reg_No = x.Reg_No,
                            license_no = x.Vehicle_Info.Driving_license,
                            Engine_No = x.Vehicle_Info.Engine_No,
                            Chasis_No = x.Vehicle_Info.Chasis_No,
                            veh_purchase_date = x.Vehicle_Info.Veh_purchase_date,


                            plan_type = x.Policy_Plans.Plan_type,
                            plan_duration = x.Policy_Plans.Duration,
                            Sub_date = x.Sub_date,
                            End_date = x.End_date,
                            Policy_No = x.Policy_No,
                            Status_of_sub = x.Status_of_sub,

                            market_price = x.Vehicle_Info.Market_price,
                            idv = x.IDV,
                            total_tp = x.Total_tp_prem_amt,
                            total_od = x.Total_od_prem_amt,
                            total_payable = x.Total_Payable,




                        })
                        .FirstOrDefault();


                if (res == null)
                {
                    var errrorRes = new policyDetailsResponse();
                    errrorRes.message = "Access Denied";
                    return errrorRes;
                }
                else
                {
                    return res;
                }
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                return response;
            }

        }
    }

    public class policyDetailsResponse
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone_no { get; set; }
        public string Email { get; set; }
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
        public string license_no { get; set; }

        public DateTime? veh_purchase_date { get; set; }

        public decimal? market_price { get; set; }

        public string plan_type { get; set; }

        public int? plan_duration { get; set; }

        public decimal? idv { get; set; }

        public decimal? total_tp { get; set; }

        public decimal? total_od { get; set; }

        public decimal? total_payable { get; set; }

        public string message { get; set; }

        public int payment_id { get; set; }




        public string card_holder_name { get; set; }

        public decimal card_no { get; set; }

        public int card_exp_month { get; set; }

        public int card_exp_year { get; set; }

        public int card_cvc { get; set; }

    }

    public class policyDetailsApi
    {
        public int User_Id { get; set; }
        public int policy_no { get; set; }
    }





}
