using GenInsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenInsWebApi.Controllers
{
    public class PremiumAmountController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();

        // Gets value of Factors that is required for Calculating Insurance Premium Amount
        [HttpPost]
        public object getpremimum(premapiclass pc)
        {
            try
            {
                // Gets Own Damage Premium Percentage from Model_od_prem_amt table based on Model Name passed to API
                var odpremper = db.Model_od_prem_amt
                                .Where(x => x.Model_Name == pc.Model_Name)
                                .Select(x => new premapires()
                                {
                                    od_prem_per = x.Veh_based_od_prem

                                }).FirstOrDefault();


                // Gets Fixed Third Party Premium Amount based on the range to which the Vehcicle CC passed to API belongs
                var thirdpartyprem = db.Third_Party_Prem
                                    .Where(x => pc.vehicle_cc >= x.Vehicle_CC_Min && pc.vehicle_cc <= x.Vehicle_CC_Max && x.Vehicle_Type == pc.vehicle_type)
                                    .Select(x => new premapires()
                                    {
                                        thirdpartyprem = x.Fixed_TP_Prem

                                    }).FirstOrDefault();


                // Gets Depreciation Percentage from Depreciation_Percentage table based on Age of Vehicle in years
                var depper = db.Depreciation_Percentage
                            .Where(x => x.Age == pc.age)
                            .Select(x => new premapires()
                            {
                                dep_per = x.Depreciation_percentage1

                            }).FirstOrDefault();


                // Returning Factors like OD Premium Percentage, Fixed TP Amount and Depreciation as API Response
                premapires resobj = new premapires();
                resobj.od_prem_per = odpremper.od_prem_per;
                resobj.thirdpartyprem = thirdpartyprem.thirdpartyprem;
                resobj.dep_per = depper.dep_per;

                return resobj;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                return response;
            }
            

        }
    }



    // Class Declaration for the object to pass as response
    public class premapires
    {
        public double? od_prem_per { get; set; }

        public int? thirdpartyprem { get; set; }

        public double? dep_per { get; set; }

    }


    // Class Declaration for the object passed to Api
    public class premapiclass
    {
        public string Model_Name { get; set; }

        public int vehicle_cc { get; set; }

        public string vehicle_type { get; set; }
        public float age { get; set; }

    }
}
