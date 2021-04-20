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
    public class AdminLoginController : ApiController
    {
        string key = "1prt56";

        General_InsuranceEntities db = new General_InsuranceEntities();

        public object Adminlogin(AdminloginResponse adminlogin)
        {
            try
            {
                adminlogin.Password = Encryptword(adminlogin.Password);
                var res = db.Admins
                            .Where(x => (x.Admin_id == adminlogin.Admin_id && x.Password == adminlogin.Password))
                            .Select(x => new AdminloginResponse()
                            {
                                Admin_id = x.Admin_id,
                                message = "Successfull"
                            })
                            .FirstOrDefault();

                if (res != null)
                {
                    return res;
                }

                res = new AdminloginResponse();
                res.message = "Invalid";
                return res;
            }
            catch(Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                return response;
            }
            
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
    public class AdminloginResponse
    {

        public string Admin_id { get; set; }

        public string Password { get; set; }

        public string message { get; set; }

    }
}
