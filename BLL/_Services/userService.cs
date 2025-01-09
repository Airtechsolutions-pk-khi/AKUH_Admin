using BAL.Repositories;
using AKU_Admin._Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using QRCoder;
using System.IO;
using System.Net.Mail;

namespace AKU_Admin.BLL._Services
{
    public class userService : baseService
    {
        userDB _service;
        public userService()
        {
            _service = new userDB();
        }
        public List<UserBLL> GetAllUser()
        {
            try
            {
                return _service.GetAllUser();
            }
            catch (Exception ex)
            {
                return new List<UserBLL>();
            }
        }
        public UserBLL GetUser(int id)
        {
            try
            {
                return _service.GetUser(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int InsertUser(UserBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "User", _env);
                data.CreatedDate = DateTime.UtcNow.AddMinutes(300);
                var result = _service.InsertUser(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateUser(UserBLL data, IWebHostEnvironment _env)
        {
            try
            {
                data.Image = UploadImage(data.Image, "User", _env);
                data.UpdatedDate = DateTime.UtcNow.AddMinutes(300);
                var result = _service.UpdateUser(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int DeleteUser(UserBLL data)
        {
            try
            {

                var result = _service.DeleteUser(data);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<LoginBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<LoginBLL>();
            }
        }
		public List<PermissionBLL> GetRoles()
		{
			try
			{
				return _service.GetRoles();
			}
			catch (Exception ex)
			{
				return new List<PermissionBLL>();
			}
		}
		public LoginBLL Get(int id)
        {
            try
            {
                return _service.Get(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Insert(LoginBLL data)
        {
            try
            {
                data.Updatedon = _UTCDateTime_SA();
                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Update(LoginBLL data)
        {
            try
            {
                data.Updatedon = _UTCDateTime_SA();
                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(LoginBLL data)
        {
            try
            {
                
                var result = _service.Delete(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int UpdatePermission(PermissionBLL data)
        {
            try
            {
                //for (int i = 0; i < length; i++)
                //{

                //}
                data.CreatedDate = _UTCDateTime_SA();
                var result = _service.UpdatePermission(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public PermissionFormBLL GetPermission(string rn)
        {
            try
            {
                return _service.GetPermission(rn);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<PermissionFormBLL> GetAllPermissions()
        {
            try
            {
                return _service.GetAllPermission();
            }
            catch (Exception ex)
            {
                return new List<PermissionFormBLL>();
            }
        }
        public int Status(UserBLL obj, IWebHostEnvironment _env)
        {
            try
            {
                var data = GetUser(obj.UserID);
                string contentRootPath = _env.ContentRootPath;

                string path = "/ClientApp/dist/assets/Upload/";
                string filePath = contentRootPath + path;

                string Body = "";
                if (obj.StatusID == 102)
                {
                    Body = System.IO.File.ReadAllText(contentRootPath + "\\Template\\password.txt");
                    //UpdateApproved(obj, _env, Body);

                }
                else if (obj.StatusID == 103)
                {
                    Body = System.IO.File.ReadAllText(contentRootPath + "\\Template\\appointmentcancelled.txt");
                    //UpdateCancelled(obj, _env, Body);
                }

                obj.UpdatedDate = _UTCDateTime_SA();
                var result = _service.Status(obj);
                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //public int UpdateApproved(UserBLL obj, IWebHostEnvironment _env, string Body)
        //{
        //    try
        //    {
        //        //var data = Get(obj.AttendeesID);
        //        var email = GetUser(obj.UserID);

        //        // Create a string with all the data you want to include in the QR code
        //        string dataToEncode = $"{data.MessageForAttendee}|{data.Createdon:dd-MM-yyyy}|{data.FullName}|{data.PhoneNo}|{data.Email}|{data.EventName}|{obj.MeetingLink}";

        //        // Generate QR code for the combined data
        //        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //        QRCodeData qrCodeData = qrGenerator.CreateQrCode(dataToEncode, QRCodeGenerator.ECCLevel.Q);

        //        // Use PngByteQRCode class to get the QR code graphic
        //        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
        //        byte[] qrCodeBytes = qrCode.GetGraphic(20);

        //        // Convert the image bytes to a Base64 string
        //        string base64Image = Convert.ToBase64String(qrCodeBytes);

        //        // Create a LinkedResource for the embedded image
        //        LinkedResource qrCodeResource = new LinkedResource(new MemoryStream(qrCodeBytes), "image/png");
        //        qrCodeResource.ContentId = "qrcode";

        //        // Replace the placeholder in the email body with the embedded image
        //        Body = Body.Replace("#QRCode#", $"<img src='cid:{qrCodeResource.ContentId}' style='width: 50%;'/>")
        //               .Replace("#name#", obj.FullName)
        //               .Replace("#email#", obj.Email)
        //               .Replace("#contact#", obj.PhoneNo)
        //               .Replace("#eventname#", obj.EventName)
        //               .Replace("#date#", obj.FromDate.ToString("dd/MM/yyyy") + " - " + obj.ToDate.ToString("dd/MM/yyyy"))
        //               .Replace("#meetinglink#", obj.MeetingLink)
        //               .Replace("#message#", obj.MessageForAttendee);


        //        // Create an AlternateView for the HTML email
        //        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
        //        htmlView.LinkedResources.Add(qrCodeResource);

        //        // Send the email
        //        MailMessage mail = new MailMessage();
        //        mail.To.Add(data.Email);
        //        mail.From = new MailAddress("akuhevents@gmail.com");
        //        mail.Subject = obj.Subject;
        //        mail.Body = Body;
        //        mail.IsBodyHtml = true;
        //        mail.AlternateViews.Add(htmlView);
        //        SmtpClient smtp = new SmtpClient();
        //        smtp.UseDefaultCredentials = false;
        //        smtp.Port = Int32.Parse("587");
        //        smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
        //        smtp.Credentials = new System.Net.NetworkCredential
        //             ("akuhevents@gmail.com", "xkezqmvksalkwwom");
        //        smtp.EnableSsl = true;
        //        smtp.Send(mail);


        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions
        //        Console.WriteLine(ex.Message);
        //        return 0; // Return an appropriate error code or handle the error as needed
        //    }

        //    return 1;
        //}
        public void SendEmail(string _SubjectEmail, string _BodyEmail, string _To)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(_To);
                mail.From = new MailAddress("akuhevents@gmail.com");
                mail.Subject = _SubjectEmail;
                mail.Body = _BodyEmail;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Port = Int32.Parse("587");
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential
                     ("akuhevents@gmail.com", "xkezqmvksalkwwom");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                //smtp.UseDefaultCredentials = true;
            }
            catch (Exception ex)
            {
            }
        }
        //public int UpdateCancelled(EventAttendeesBLL obj, IWebHostEnvironment _env, string Body)
        //{
        //    string msg = "";
        //    //msg = obj.StatusMsg;
        //    string contentRootPath = _env.ContentRootPath;

        //    string path = "/ClientApp/dist/assets/Upload/";
        //    string filePath = contentRootPath + path;

        //    try
        //    {
        //        var data = Get(obj.AttendeesID);
        //        var email = GetUser(obj.UserID);

        //        string ToEmail, SubJect;
        //        ToEmail = email.Email;

        //        Body = Body.Replace("#description#", data.MessageForAttendee == null || data.MessageForAttendee == "" ? "N/A" : data.MessageForAttendee)
        //            .Replace("#date#", data.Createdon.ToString())

        //            .Replace("#name#", email.FullName)
        //            .Replace("#contact#", email.PhoneNo)
        //            .Replace("#username#", obj.Email);
        //        SendEmail(obj.Subject, Body, email.Email);
        //    }
        //    catch { }
        //    return 1;
        //}
    }
}
