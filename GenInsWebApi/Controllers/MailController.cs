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
        string key = "1prt56";
        General_InsuranceEntities db = new General_InsuranceEntities();
        
        User_Registration user = new User_Registration();

        forgot_password forget_pwd = new forgot_password();

        //[HttpPost]
        public object mail(Mail objMail)
        {            

            bool EmailAlreadyExists = db.User_Registration.Any(x => x.Email_ID == objMail.Email_ID);

            if (ModelState.IsValid && EmailAlreadyExists == true)
            {
                string from = "raidinsurance@gmail.com"; //example:- sourabh9303@gmail.com  
                using (MailMessage mail = new MailMessage(from, objMail.Email_ID))
                {
                    var url = "http://localhost:4200/ForgetPwd/?token=" + HttpUtility.UrlEncode(Encryptword(objMail.Email_ID));
                    forget_pwd.Email_id = objMail.Email_ID;
                    forget_pwd.encryted_link = url;
                    db.forgot_password.Add(forget_pwd);
                    db.SaveChanges();
                    mail.Subject = "Reset Password Link";
                    mail.Body = "Hello!\nWe received a request to reset the password for your account.\nIf you made this request, click the link given below. If you didn't make this request, you can ignore this email.\n" + url;
                    //if (fileUploader != null)
                    //{
                    //    string fileName = Path.GetFileName(fileUploader.FileName);
                    //    mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                    //}
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from, "raidinsurane1234");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    var res = "Successfull";
                    return Ok(res);
                    //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, objMail);
                    //return response;
                }
            }
            else
            {
                var res = new MailResponse();
                res.message = "Invalid";
                return Ok(res);
                //HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Mail Not Sent");
                //return response;
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
