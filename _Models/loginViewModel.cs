using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKU_Admin._Models
{
    public class loginViewModel
    {
    }
    public class RspLogin : Rsp
    {
        public LoginBLL login { get; set; }

    }
    public class LoginBLL
    {
        public int AdminID { get; set; }

        public string Name { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }
    }
    //public class BannerBLL
    //{
    //    public int BannerID { get; set; }

    //    public string Type { get; set; }

    //    public string Screen { get; set; }

    //    public string Title { get; set; }

    //    public string Description { get; set; }

    //    public string Image { get; set; }

    //    public int? DisplayOrder { get; set; }

    //    public int? StatusID { get; set; }

    //    public DateTime? Createdon { get; set; }

    //    public DateTime? Updatedon { get; set; }

    //    public int? UpdatedBy { get; set; }

    //}
    
    public class EventAttendeesBLL
    {
        public int AttendeesID { get; set; }

        public int? EventID { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
    public class EventCategoryBLL
    {
        public int EventCategoryID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
    public class EventImageJuncBLL
    {
        public int EventImageID { get; set; }

        public int? EventID { get; set; }

        public string Image { get; set; }

        public DateTime? Createdon { get; set; }

    }
    public class GalleryBLL
    {
        public int GalleryID { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
    //public class NotificationBLL
    //{
    //    public int NotificationID { get; set; }

    //    public int? UserID { get; set; }

    //    public string NotificationType { get; set; }

    //    public string Title { get; set; }

    //    public string Description { get; set; }

    //    public DateTime? NotificationDate { get; set; }

    //    public bool? IsRead { get; set; }

    //    public int? StatusID { get; set; }

    //}
    public class OrganizerBLL
    {
        public int OrganizerID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int? StatusID { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
    //public class PushTokenBLL
    //{
    //    public int TokenID { get; set; }

    //    public string Token { get; set; }

    //    public int? UserID { get; set; }

    //    public int? StatusID { get; set; }

    //    public int? Device { get; set; }

    //}
    public class SpeakerBLL
    {
        public int SpeakerID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public string About { get; set; }
        public string Image { get; set; }
        public int? StatusID { get; set; }
        public DateTime? Createdon { get; set; }
        public DateTime? Updatedon { get; set; }
        public int? UpdatedBy { get; set; }
    }
    public class UserBLL
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Password { get; set; }
        public int? StatusID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
    public class SettingBLL
    {
        public int SettingID { get; set;}
        public string SplashScreen { get; set; }
        public string About { get; set; }
        public string PrivacyPolicy { get; set; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public int StatusID { get; set; }
        public DateTime? Createdon { get; set; }
        public DateTime? Updatedon { get; set; }
    }
}
