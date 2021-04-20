using GenInsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenInsWebApi.Controllers
{
    public class BrandNameController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();

        [HttpPost]
        public object getBrands(vehTypeApiClass va)
        {
            try
            {
                var res = db.Model_od_prem_amt
                .Where(x => x.vehicle_type == va.vehicle_type)
                .Select(x => new brandsResponse()
                {
                    vehicle_type = x.vehicle_type,
                    brand_names = x.Brand_Names.Brand_Name,
                    Brand_Id = x.Brand_Names.Brand_Id

                }).Distinct();
                //throw new Exception();

                return res;
            }
            catch(Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                return response;
            }

        }

    }

    public class vehTypeApiClass
    {
        public string vehicle_type { get; set; }
    }
    public class brandsResponse
    {
        public string vehicle_type { get; set; }

        public string brand_names { get; set; }

        public int Brand_Id { get; set; }

    }

    


}
