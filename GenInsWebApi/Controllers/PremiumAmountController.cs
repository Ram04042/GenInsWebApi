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
        GeneralInsuranceEntities2 db = new GeneralInsuranceEntities2();

        [HttpPost]
        public object getpremimum(premapiclass pc)
        {
            var odpremper = db.Model_od_prem_amt
                .Where(x => x.Model_Name == pc.Model_Name)
                .Select(x => new premapires()
                {
                    od_prem_per = x.Veh_based_od_prem
                }).FirstOrDefault();

            var thirdpartyprem = db.Third_Party_Prem
                .Where(x => pc.vehicle_cc >= x.Vehicle_CC_Min && pc.vehicle_cc <= x.Vehicle_CC_Max && x.Vehicle_Type == pc.vehicle_type)
                .Select(x => new premapires()
                {
                    thirdpartyprem = x.Fixed_TP_Prem
                }).FirstOrDefault();

            var depper = db.Depreciation_Percentage
                .Where(x => x.Age == pc.age)
                .Select(x => new premapires()
                {
                    dep_per = x.Depreciation_percentage1
                }).FirstOrDefault();

            premapires resobj = new premapires();
            resobj.od_prem_per = odpremper.od_prem_per;
            resobj.thirdpartyprem = thirdpartyprem.thirdpartyprem;
            resobj.dep_per = depper.dep_per;
            
            return Ok(resobj);

        }
    }



    public class premapires
    {
        public double? od_prem_per { get; set; }

        public int? thirdpartyprem { get; set; }

        public float? dep_per { get; set; }

    }

    public class premapiclass
    {
        public string Model_Name { get; set; }

        public int vehicle_cc { get; set; }

        public string vehicle_type { get; set; }
        public float age { get; set; }

    }
}
