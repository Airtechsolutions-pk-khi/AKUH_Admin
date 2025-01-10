using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AKU_Admin._Models
{
    public class eventsViewModel
    {
    }
    public class EventBLL
    {
        public int EventID { get; set; }

        public int? EventCategoryID { get; set; }

        public int? OrganizerID { get; set; }

        public int? AttendeesID { get; set; }

        public string Name { get; set; }

        public string EventTime { get; set; }
        public string EventEndTime { get; set; }
        public string Type { get; set; }
        [AllowHtml]
        public string Description { get; set; }

        public string Location { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public decimal? TicketNormal { get; set; }

        public decimal? TicketPremium { get; set; }

        public decimal? TicketPlatinum { get; set; }

        public DateTime? EventDate { get; set; }

        public string? EventCity { get; set; }

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
        public int? DisplayOrder { get; set; }
        public bool IsFeatured { get; set; }

        public string EventCategories { get; set; }
        public string Speakers { get; set; }
        public string Organizers { get; set; }
        public string EventLink { get; set; }

        public List<EventImagesBLL> EventImages = new List<EventImagesBLL>();
        public List<string> ImagesSource { get; set; }
    }
    public class EventImagesBLL
    {
        public int EventImageID { get; set; }

        public string Image { get; set; }

        public int? EventID { get; set; }

        public DateTime? Createdon { get; set; }

    }
    public class EventBLL1
    {
        public int EventID { get; set; }
        public int? EventCategoryID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Location { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LocationLink { get; set; }
        public int StatusID { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public int DisplayOrder { get; set; }
        public string Speakers { get; set; }
        public string Organizers { get; set; }
        public string Image { get; set; }
        List<EventImagesBLL> EventImages = new List<EventImagesBLL>();
        public List<string> ImagesSource { get; set; }
        public DateTime? Createdon { get; set; }
        public DateTime? Updatedon { get; set; }
        public string EventTime { get; set; }
        public string EventEndTime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsFeatured { get; set; }
        public string EventCategories { get; set; }
        public string EventLink { get; set; }
        public string EventDate { get; set; }
        public string EventCity { get; set; }
        public int RemainingTicket { get; set; }
    }
}
