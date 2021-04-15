using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenInsWebApi.Models
{
    public class Reset_Pwd
    {
        public string token { get; set; }

        public string password { get; set; }

        public string message { get; set; }
    }
}