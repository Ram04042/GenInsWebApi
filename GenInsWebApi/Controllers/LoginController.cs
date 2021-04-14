using GenInsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Security.Cryptography;

namespace GenInsWebApi.Controllers
{
    public class LoginController : ApiController
    {
        string key = "1prt56";

        GeneralInsuranceEntities2 db = new GeneralInsuranceEntities2();
        public object login(LoginApiClass l)
        {
            l.Password = Encryptword(l.Password);
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
        public string Encryptword(string Encryptval)
        {
            byte[] SrctArray;
            byte[] EnctArray = UTF8Encoding.UTF8.GetBytes(Encryptval);
            SrctArray = UTF8Encoding.UTF8.GetBytes(key);
            TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objcrpt = new MD5CryptoServiceProvider();
            SrctArray = objcrpt.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            objcrpt.Clear();
            objt.Key = SrctArray;
            objt.Mode = CipherMode.ECB;
            objt.Padding = PaddingMode.PKCS7;
            ICryptoTransform crptotrns = objt.CreateEncryptor();
            byte[] resArray = crptotrns.TransformFinalBlock(EnctArray, 0, EnctArray.Length);
            objt.Clear();
            return Convert.ToBase64String(resArray, 0, resArray.Length);
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
