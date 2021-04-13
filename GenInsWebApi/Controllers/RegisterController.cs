using GenInsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenInsWebApi.Controllers
{
    public class RegisterController : ApiController
    {
        GeneralInsuranceEntities2 db = new GeneralInsuranceEntities2();

        public IHttpActionResult Register(User_Registration user)
        {
            db.User_Registration.Add(user);
            db.SaveChanges();
            return Ok();
        }
    }
}
