using GenInsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenInsWebApi.Controllers
{
    public class ModelNameController : ApiController
    {
        GeneralInsuranceEntities2 db = new GeneralInsuranceEntities2();
        [HttpPost]
        public object getModels(brandIdVehtypeApiClass ba)
        {
            var res = db.Model_od_prem_amt
                .Where(x => x.Brand_Id == ba.Brand_Id && x.vehicle_type==ba.vehicle_type)
                .Select(x => new modelsResponse()
                {
                    Brand_Id = x.Brand_Id,
                    Model_Name = x.Model_Name

                });


            return Ok(res);

        }


    }

    public class brandIdVehtypeApiClass
    {
        public int Brand_Id { get; set; }

        public string vehicle_type { get; set; }

    }
    public class modelsResponse
    {
        public int? Brand_Id { get; set; }

        public string Model_Name { get; set; }
    }
}
