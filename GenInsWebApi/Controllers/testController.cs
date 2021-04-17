using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenInsWebApi.Models;

namespace GenInsWebApi.Controllers
{
    public class testController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();

        public IHttpActionResult check()
        {
            //db.add_user_sub.ActionContext;
            return Ok();
        }



    }
}
