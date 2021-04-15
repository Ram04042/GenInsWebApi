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
        GeneralInsuranceEntities3 db = new GeneralInsuranceEntities3();

        User_Registration user = new User_Registration();

        string key = "1prt56";

        public IHttpActionResult Forget(Reset_Pwd reset_Pwd)
        {
            //reset_Pwd.token = reset_Pwd.Email_id;
            reset_Pwd.token = Decryptword(reset_Pwd.token);
            reset_Pwd.password = Encryptword(reset_Pwd.password);
            var res = db.User_Registration
                    .Where(x => (x.Email_ID == reset_Pwd.token))
                    .FirstOrDefault<User_Registration>();
            if (res != null)
            {
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

        public string Decryptword(string DecryptText)
        {
            byte[] SrctArray;
            byte[] DrctArray = Convert.FromBase64String(DecryptText);
            SrctArray = UTF8Encoding.UTF8.GetBytes(key);
            TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objmdcript = new MD5CryptoServiceProvider();
            SrctArray = objmdcript.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            objmdcript.Clear();
            objt.Key = SrctArray;
            objt.Mode = CipherMode.ECB;
            objt.Padding = PaddingMode.PKCS7;
            ICryptoTransform crptotrns = objt.CreateDecryptor();
            byte[] resArray = crptotrns.TransformFinalBlock(DrctArray, 0, DrctArray.Length);
            objt.Clear();
            return UTF8Encoding.UTF8.GetString(resArray);
        }
    }
}
