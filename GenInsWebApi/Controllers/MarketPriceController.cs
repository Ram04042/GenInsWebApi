using GenInsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenInsWebApi.Controllers
{
    public class MarketPriceController : ApiController
    {
       
        General_InsuranceEntities db = new General_InsuranceEntities();


        // Gets Market Price of Vehicle form Model_od_prem_amt Table based on Model Name
        [HttpGet]
        public IHttpActionResult getMarketPrice(string Model_Name)
        {
            try
            {
                var res = db.Model_od_prem_amt
                .Where(x => x.Model_Name == Model_Name)
                .Select(x => new marketPriceResponse()
                {
                    Market_Price = x.market_price
                }).FirstOrDefault();
                return Ok(res);
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                return Ok(response);
            }

            
        }
    }


    // Class Declaration for the object to pass as response
    public class marketPriceResponse
    { 
        public decimal? Market_Price { get; set; }

    }
}
