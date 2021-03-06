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
        General_InsuranceEntities db = new General_InsuranceEntities();

        // Gets Model Names of Vehicles from Model_od_prem_amt table based on Brand Id
        [HttpPost]
        public object getModels(brandIdVehtypeApiClass ba)
        {
            try
            {
                var res = db.Model_od_prem_amt
                .Where(x => x.Brand_Id == ba.Brand_Id && x.vehicle_type == ba.vehicle_type)
                .Select(x => new modelsResponse()
                {
                    Brand_Id = x.Brand_Id,
                    Model_Name = x.Model_Name

                });
                return res;
            }
            catch(Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                return response;
            }
            

        }


    }

    // Class Declaration for the object passed to Api
    public class brandIdVehtypeApiClass
    {
        public int Brand_Id { get; set; }

        public string vehicle_type { get; set; }

    }

    // Class Declaration for the object to pass as response
    public class modelsResponse
    {
        public int? Brand_Id { get; set; }

        public string Model_Name { get; set; }
    }
}
