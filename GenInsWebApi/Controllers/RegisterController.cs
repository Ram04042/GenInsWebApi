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
    public class RegisterController : ApiController
    {

        string key = "1prt56";

        GeneralInsuranceEntities2 db = new GeneralInsuranceEntities2();

        public HttpResponseMessage Register(User_Registration user)
        {
            bool EmailAlreadyExists = db.User_Registration.Any(x => x.Email_ID == user.Email_ID);
            bool PhoneNoAlreadyExists = db.User_Registration.Any(x => x.Phone_No == user.Phone_No);

            if (ModelState.IsValid && EmailAlreadyExists != true)
            {
                if (PhoneNoAlreadyExists != true)
                {
                    user.Email_ID = user.Email_ID;
                    user.Password = Encryptword(user.Password);
                    db.User_Registration.Add(user);
                    db.SaveChanges();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Phone Number Already Exists");
                    return response;
                }
            }
            else
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email Id Already Exists");
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
}
