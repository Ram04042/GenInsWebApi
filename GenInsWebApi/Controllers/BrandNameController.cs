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
        GeneralInsuranceEntities2 db = new GeneralInsuranceEntities2();

        [HttpPost]
        public object getBrands(vehTypeApiClass va)
        {
            var res = db.Model_od_prem_amt
                .Where(x => x.vehicle_type == va.vehicle_type)
                .Select(x => new brandsResponse()
                {
                    vehicle_type = x.vehicle_type,
                    brand_names = x.Brand_Names.Brand_Name,
                    Brand_Id = x.Brand_Names.Brand_Id

                }).Distinct();


            return Ok(res);

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
