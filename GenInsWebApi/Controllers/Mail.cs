using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenInsWebApi.Controllers
{
    public class Mail
    {
        public string Email_ID { get; set; }

        public string encrypted_link { get; set; }

        public string message { get; set; }
    }
}