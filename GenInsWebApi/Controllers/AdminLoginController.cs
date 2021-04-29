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
        //key used for encryption

        string key = "1prt56";

        General_InsuranceEntities db = new General_InsuranceEntities();

        public object Adminlogin(AdminloginResponse adminlogin)
        {
            adminlogin.Password = Encryptword(adminlogin.Password);

            // check for the username and password match

            

            
            try
            {
                //encrypt the password 
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

            //catches an exception

            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                return response;
            }

        }
        public string Encryptword(string Encryptval)
        {
            byte[] SrctArray;

            //get the byte code of the string and key

            byte[] EnctArray = UTF8Encoding.UTF8.GetBytes(Encryptval);
            SrctArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objcrpt = new MD5CryptoServiceProvider();
            SrctArray = objcrpt.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            objcrpt.Clear();
            //set the secret key for the tripleDES algorithm

            objt.Key = SrctArray;

            //mode of operation - ECB(Electronic code Book)

            objt.Mode = CipherMode.ECB;

            //padding mode(if any extra byte added)

            objt.Padding = PaddingMode.PKCS7;

            ICryptoTransform crptotrns = objt.CreateEncryptor();

            //transform the specified region of bytes array to resultArray

            byte[] resArray = crptotrns.TransformFinalBlock(EnctArray, 0, EnctArray.Length);

            //Release resources held by TripleDes Encryptor

            objt.Clear();

            //Return the encrypted data into unreadable string format

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
