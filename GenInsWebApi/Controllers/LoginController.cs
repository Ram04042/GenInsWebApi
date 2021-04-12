using GenInsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GenInsWebApi.Controllers
{
    public class LoginController : ApiController
    {
        GeneralInsuranceEntities1 db = new GeneralInsuranceEntities1();
        public object login(LoginApiClass l)
        {
            var res = db.User_Registration
                        .Where(x => (x.Email_ID == l.Email_ID && x.Password == l.Password))
                        .Select(x => new loginResponse()
                        {
                            User_Id = x.User_Id,
                            Email_ID = x.Email_ID,
                            Name = x.Name,
                            Phone_No = x.Phone_No,
                            DOB = x.DOB,
                            Address = x.Address,
                            message = "Successfull"
                        })
                        .FirstOrDefault();

            if (res != null)
            {
                return Ok(res);
            }

            res = new loginResponse();
            res.message = "Invalid";
            return Ok(res);
        }

    }

    public class loginResponse
    {
        public int User_Id { get; set; }

        public string Email_ID { get; set; }

        public string Name { get; set; }

        public string Phone_No { get; set; }

        public Nullable<System.DateTime> DOB { get; set; }

        public string Address { get; set; }



        public string message { get; set; }


    }

}
