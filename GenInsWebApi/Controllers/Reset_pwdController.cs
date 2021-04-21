using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Cryptography;
using System.Text;
using GenInsWebApi.Models;


namespace GenInsWebApi.Models
{
    public class Reset_pwdController : ApiController
    {
        General_InsuranceEntities db = new General_InsuranceEntities();

        User_Registration user = new User_Registration();

        //key used for encryption

        string key = "1prt56";

        public IHttpActionResult Forget(Reset_Pwd reset_Pwd)
        {
            try
            {
                //Decrypt the token to get user email id and encrypt the password 

                reset_Pwd.token = Decryptword(reset_Pwd.token);
                reset_Pwd.password = Encryptword(reset_Pwd.password);

                //check for email id match with database

                var res = db.User_Registration
                        .Where(x => (x.Email_ID == reset_Pwd.token))
                        .FirstOrDefault<User_Registration>();

                if (res != null)
                {

                    // store changed password in database

                    reset_Pwd.message = "Successfull";
                    res.Password = reset_Pwd.password;
                    db.SaveChanges();
                    return Ok(reset_Pwd.message);
                }
                else
                {
                    reset_Pwd.message = "Invalid";
                    return Ok(reset_Pwd.message);
                }
            }

            //catches an exception

            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad Request");
                return Ok(response);
            }
            
        }
        public string Encryptword(string Encryptval)
        {
            byte[] SrctArray;

            //get the byte code of the string and key

            byte[] EnctArray = UTF8Encoding.UTF8.GetBytes(Encryptval);
            SrctArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();

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

        public string Decryptword(string DecryptText)
        {
            byte[] SrctArray;

            //get the byte code of the string and key

            byte[] DrctArray = Convert.FromBase64String(DecryptText);
            SrctArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();

            //set the secret key for the tripleDES algorithm

            objt.Key = SrctArray;

            //mode of operation - ECB(Electronic code Book)

            objt.Mode = CipherMode.ECB;

            //padding mode(if any extra byte added)

            objt.Padding = PaddingMode.PKCS7;

            ICryptoTransform crptotrns = objt.CreateDecryptor();

            //transform the specified region of bytes array to resultArray

            byte[] resArray = crptotrns.TransformFinalBlock(DrctArray, 0, DrctArray.Length);

            //Release resources held by TripleDes Encryptor

            objt.Clear();

            //return the Clear decrypted TEXT

            return UTF8Encoding.UTF8.GetString(resArray);
        }
    }
}
