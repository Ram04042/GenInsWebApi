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
        //key used for encryption
        string key = "1prt56";

        General_InsuranceEntities db = new General_InsuranceEntities();
        public object login(LoginApiClass l)
        {
            try
            {
                //key used for encryption
                l.Password = Encryptword(l.Password);

                //check for the correct email id and password for user in db
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

                if (res != null)//res will not return null if valid
                {
                    return res;
                }

                res = new loginResponse();
                res.message = "Invalid";
                return res;
            }
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

            //set the secret key for tripleDES algorithm
            SrctArray = objcrpt.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            objcrpt.Clear();
            objt.Key = SrctArray;

            //mode of operation- electronic code block
            objt.Mode = CipherMode.ECB;

            //padding mode(if any extra byte added)
            objt.Padding = PaddingMode.PKCS7;
            ICryptoTransform crptotrns = objt.CreateEncryptor();

            //transform the specifies region of bytes array to resultArray
            byte[] resArray = crptotrns.TransformFinalBlock(EnctArray, 0, EnctArray.Length);

            //release sources released by tripleDES encryptor
            objt.Clear();

            //return the encrypted data into unreadable string format
            return Convert.ToBase64String(resArray, 0, resArray.Length);

        }

    }


    //class declaration for the object passed to api
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
