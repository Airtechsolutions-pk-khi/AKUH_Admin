﻿using DAL.Models;
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
    public class EventBLL
    {
        public int EventID { get; set; }

        public int? EventCategoryID { get; set; }

        public int? OrganizerID { get; set; }

        public int? AttendeesID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public decimal? TicketNormal { get; set; }

        public decimal? TicketPremium { get; set; }

        public decimal? TicketPlatinum { get; set; }

        public DateTime? EventDate { get; set; }

        public DateTime? EventCity { get; set; }

        public string LocationLink { get; set; }

        public int? StatusID { get; set; }

        public DateTime? DoorTime { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public int? RemainingTicket { get; set; }

        public int? EventAttendeesID { get; set; }

        public string Facebook { get; set; }

        public string Instagram { get; set; }

        public string Twitter { get; set; }

        public string Image { get; set; }

        public DateTime? Createdon { get; set; }

        public DateTime? Updatedon { get; set; }

        public int? UpdatedBy { get; set; }

    }
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
}