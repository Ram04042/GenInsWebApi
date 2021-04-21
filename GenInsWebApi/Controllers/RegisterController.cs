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
        //key used for encryption
        string key = "1prt56";

        General_InsuranceEntities db = new General_InsuranceEntities();

        public HttpResponseMessage Register(User_Registration user)
        {
            //checks if email id already exits or not
            bool EmailAlreadyExists = db.User_Registration.Any(x => x.Email_ID == user.Email_ID);

            if (ModelState.IsValid && EmailAlreadyExists != true)
            {
                user.Email_ID = user.Email_ID;
                user.Password = Encryptword(user.Password);//key used for encryption
                db.User_Registration.Add(user);
                db.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                return response;
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
}
