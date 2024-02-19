using BAL.Repositories;
using AKU_Admin._Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.IO;
using System.Web.Helpers;
using System.Drawing;
using QRCoder;
using System.Net.Mime;

namespace AKU_Admin.BLL._Services
{
    public class eventAttendeesService : baseService
    {
        eventAttendeesDB _service;
        public eventAttendeesService()
        {
            _service = new eventAttendeesDB();
        }

        public List<EventAttendeesBLL> GetAll()
        {
            try
            {
                return _service.GetAll();
            }
            catch (Exception ex)
            {
                return new List<EventAttendeesBLL>();
            }
        }
        public List<EventBLL> GetAllDropdown()
        {
            try
            {
                return _service.GetAllDropdown();
            }
            catch (Exception ex)
            {
                return new List<EventBLL>();
            }
        }

        public EventAttendeesBLL Get(int id)
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
        public List<string> GetItemImages(int id)
        {
            try
            {
                return _service.GetItemImages(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public EventAttendeesBLL Getcustomer(int? id)
        {
            try
            {
                return _service.Getcustomer(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int UpdateApproved(EventAttendeesBLL obj, IWebHostEnvironment _env, string Body)
        {
            try
            {
                var data = Get(obj.AttendeesID);
                //var email = Getcustomer(obj.UserID);

                // Create a string with all the data you want to include in the QR code
                string dataToEncode = $"{data.MessageForAttendee}|{data.Createdon:dd-MM-yyyy}|{data.FullName}|{data.PhoneNo}|{data.Email}|{obj.Password}|{obj.MeetingLink}";

                // Generate QR code for the combined data
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(dataToEncode, QRCodeGenerator.ECCLevel.Q);

                // Use PngByteQRCode class to get the QR code graphic
                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                byte[] qrCodeBytes = qrCode.GetGraphic(20);

                // Convert the image bytes to a Base64 string
                string base64Image = Convert.ToBase64String(qrCodeBytes);

                // Create a LinkedResource for the embedded image
                LinkedResource qrCodeResource = new LinkedResource(new MemoryStream(qrCodeBytes), "image/png");
                qrCodeResource.ContentId = "qrcode";

                // Replace the placeholder in the email body with the embedded image
                Body = Body.Replace("#QRCode#", $"<img src='cid:{qrCodeResource.ContentId}' style='width: 50%;'/>")
                       .Replace("#name#", obj.FullName)
                       .Replace("#email#", obj.Email)
                       .Replace("#password#", obj.Password)
                       .Replace("#contact#", obj.PhoneNo)                      
                       .Replace("#date#", obj.FromDate.ToString("dd/MM/yyyy") + " - " + obj.ToDate.ToString("dd/MM/yyyy"))
                       .Replace("#meetinglink#", obj.MeetingLink)
                       .Replace("#message#", obj.MessageForAttendee);


                // Create an AlternateView for the HTML email
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
                htmlView.LinkedResources.Add(qrCodeResource);
                 
                // Send the email
                MailMessage mail = new MailMessage();
                mail.To.Add(data.Email);
                mail.From = new MailAddress("akuhevents@gmail.com");
                mail.Subject = obj.Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                mail.AlternateViews.Add(htmlView);
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Port = Int32.Parse("587");
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential
                     ("akuhevents@gmail.com", "ueuzxvrsgtaxdbev");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                 
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine(ex.Message);
                return 0; // Return an appropriate error code or handle the error as needed
            }

            return 1;
        }        
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
                     ("akuhevents@gmail.com", "ueuzxvrsgtaxdbev");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                //smtp.UseDefaultCredentials = true;
            }
            catch (Exception ex)
            {
            }
        }
        public int UpdateCancelled(EventAttendeesBLL obj, IWebHostEnvironment _env, string Body)
        {
            string msg = "";
            //msg = obj.StatusMsg;
            string contentRootPath = _env.ContentRootPath;

            string path = "/ClientApp/dist/assets/Upload/";
            string filePath = contentRootPath + path;

            try
            {
                var data = Get(obj.AttendeesID);
                var email = Getcustomer(obj.UserID);

                string ToEmail, SubJect;
                ToEmail = email.Email;

                Body = Body.Replace("#description#", data.MessageForAttendee == null || data.MessageForAttendee == "" ? "N/A" : data.MessageForAttendee)
                    .Replace("#date#", data.Createdon.ToString())

                    .Replace("#name#", email.FullName)
                    .Replace("#contact#", email.PhoneNo)
                    .Replace("#username#", obj.Email);
                SendEmail(obj.Subject, Body, email.Email);
            }
            catch { }
            return 1;
        }
        public int Status(EventAttendeesBLL obj, IWebHostEnvironment _env)
        {
            try
            {
                //Random random = new Random();
                //string Password = random.Next(10000000, 99999999).ToString();
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                int passwordLength = 8;  

                string password = new string(Enumerable.Repeat(chars, passwordLength)
                                                  .Select(s => s[random.Next(s.Length)]).ToArray());

                obj.Password = password;
                var data = Getcustomer(obj.AttendeesID);
                string contentRootPath = _env.ContentRootPath;

                string path = "/ClientApp/dist/assets/Upload/";
                string filePath = contentRootPath + path;

                string Body = "";
                if (obj.StatusID == 102)
                {
                    Body = System.IO.File.ReadAllText(contentRootPath + "\\Template\\approved.txt");
                    
                    UpdateApproved(obj, _env, Body);
                    //try
                    //{
                    //    var ds = _service.GetToken(obj.UserID);
                    //    var getTokens = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<PushTokenBLL>>();
                    //    foreach (var item in getTokens)
                    //    {
                    //        var token = new PushNotificationBLL();
                    //        token.Title = "Mamji Hospital" + " | Appointment Update";
                    //        token.Message = "Your appointment has been confirmed";
                    //        token.DeviceID = item.Token;
                    //        _service.PushNotificationAndroid(token);
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //}
                }
               

                else if (obj.StatusID == 103)
                {
                    Body = System.IO.File.ReadAllText(contentRootPath + "\\Template\\appointmentcancelled.txt");
                    UpdateCancelled(obj, _env, Body);
                    //try
                    //{
                    //    var ds = _service.GetToken(obj.CustomerID);
                    //    var getTokens = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0])).ToObject<List<PushTokenBLL>>();
                    //    foreach (var item in getTokens)
                    //    {
                    //        var token = new PushNotificationBLL();
                    //        token.Title = "Mamji Hospital" + " | Appointment Update";
                    //        token.Message = "Your appointment has been cancelled";
                    //        token.DeviceID = item.Token;
                    //        _service.PushNotificationAndroid(token);
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //}
                }
                obj.Updatedon = _UTCDateTime_SA();
                var result = _service.Status(obj);


                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public ItemSettingsBLL GetItemSettings(int brandID)
        {
            return _service.GetItemSettings(brandID);
        }
        public int Insert(EventAttendeesBLL data, IWebHostEnvironment _env)
        {
            List<EventImagesBLL> imBLL = new List<EventImagesBLL>();
            try
            {
                //data.Image = UploadImage(data.Image, "Event", _env);
                data.Createdon = DateTime.UtcNow.AddMinutes(300);
                //for (int i = 0; i < data.ImagesSource.Count; i++)
                //{
                //    var img = data.ImagesSource[i].ToString();
                //    if (i == 0)
                //    {
                //        data.Image = UploadImage(img, "Event", _env);
                //    }

                //    imBLL.Add(new EventImagesBLL
                //    {
                //        Image = UploadImage(img, "Event", _env),
                //        Createdon = data.Createdon
                //    });

                //}
                //data.EventImages = imBLL;

                var result = _service.Insert(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int UpdateItemSettings(ItemSettingsBLL data)
        {
            try
            {

                var result = _service.UpdateItemSettings(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int Update(EventAttendeesBLL data, IWebHostEnvironment _env)
        {
            
            try
            {
                data.Updatedon = DateTime.UtcNow.AddMinutes(300);
                

                var result = _service.Update(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(EventAttendeesBLL data)
        {
            try
            {
                data.Updatedon = DateTime.Now.AddMinutes(300);
                var result = _service.Delete(data);

                return result;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<EventDetailsBLL> GetEventsDetailRpt(string EventID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.GetEventsDetailRpt(EventID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<EventDetailsBLL>();
            }
        }
        public List<EventDetailsBLL> ConfirmListReport(string EventID, DateTime FromDate, DateTime ToDate)
        {
            try
            {
                return _service.ConfirmListReport(EventID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                return new List<EventDetailsBLL>();
            }
        }
        
    }
}
