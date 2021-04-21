using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using GenInsWebApi.Models;

namespace GenInsWebApi.Controllers
{
    public class MailController : ApiController
    {
        //key used for encryption

        string key = "1prt56";

        General_InsuranceEntities db = new General_InsuranceEntities();
        
        User_Registration user = new User_Registration();

        forgot_password forget_pwd = new forgot_password();

        public object mail(Mail objMail)
        {
            try
            {
                //check for email existence in database

                bool EmailAlreadyExists = db.User_Registration.Any(x => x.Email_ID == objMail.Email_ID);

                if (ModelState.IsValid && EmailAlreadyExists == true)
                {
                    //From : email ID

                    string from = "raidinsurance@gmail.com";  

                    using (MailMessage mail = new MailMessage(from, objMail.Email_ID))
                    {
                        //encode the  encrypted email id

                        var url = "http://localhost:4200/ForgetPwd/?token=" + HttpUtility.UrlEncode(Encryptword(objMail.Email_ID));

                        //adding elements to database

                        forget_pwd.Email_id = objMail.Email_ID;
                        forget_pwd.encryted_link = url;
                        db.forgot_password.Add(forget_pwd);
                        db.SaveChanges();

                        //set content for email

                        mail.Subject = "Reset Password Link";
                        mail.Body = "Hello!\nWe received a request to reset the password for your account.\nIf you made this request, click the link given below. If you didn't make this request, you can ignore this email.\n" + url;
                        mail.IsBodyHtml = false;

                        //send email by simple mail transfer protocol

                        SmtpClient smtp = new SmtpClient();

                        //set the ip address of the host

                        smtp.Host = "smtp.gmail.com";

                        //specify the uses of SSL by SMTP

                        smtp.EnableSsl = true;

                        //credentials for authentication

                        NetworkCredential networkCredential = new NetworkCredential(from, "raidinsurane1234");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCredential;

                        //set the port for transactions and send email

                        smtp.Port = 587;
                        smtp.Send(mail);

                        var res = "Successfull";
                        return res;
                    }
                }
                else
                {
                    var res = new MailResponse();
                    res.message = "Invalid";
                    return res;
                }
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
        public class MailResponse
        {
            public string Email_ID { get; set; }
            public string message { get; set; }
        }

        public class forgot_password1
        {
            public string email_id { get; set; }
            public string encrypted_link { get; set; }
        }
    }
}
