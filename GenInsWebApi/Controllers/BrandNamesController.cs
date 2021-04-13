using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenInsWebApi.Models;

namespace GenInsWebApi.Controllers
{
    public class BrandNamesController : ApiController
    {
        GeneralInsuranceEntities1 db = new GeneralInsuranceEntities1();
        // GET: api/BrandNames
        public IHttpActionResult GetBrandNames()
        {
            return Ok();
        }

        // GET: api/BrandNames/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BrandNames
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/BrandNames/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BrandNames/5
        public void Delete(int id)
        {
        }
    }
}
