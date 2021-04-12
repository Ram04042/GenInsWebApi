using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenInsWebApi.Models
{
    public class LoginApiClass
    {
        public string Email_ID { get; set; }
        public string Password { get; set; }

        public string message { get; set; }
    }
}