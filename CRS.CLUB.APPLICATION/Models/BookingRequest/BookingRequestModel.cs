using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.CLUB.APPLICATION.Models.BookingRequest
{
    public class PendingBookingRequestList
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string PlanName { get; set; }
        public string People { get; set; }
        public string NoOfHosts { get; set; }
        public string VisitedDate { get; set; }
        public string VisitedTime { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string CreatedDate { get; set; }
        public string Action { get; set; }
    }
    public class ApprovedBookingRequestList
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string PlanName { get; set; }
        public string People { get; set; }
        public string NoOfHosts { get; set; }
        public string VisitedDate { get; set; }
        public string VisitedTime { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string CreatedDate { get; set; }
        public string Action { get; set; }
    }
    public class AllBookingRequestList
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string PlanName { get; set; }
        public string People { get; set; }
        public string NoOfHosts { get; set; }
        public string VisitedDate { get; set; }
        public string VisitedTime { get; set; }
        public string Status { get; set; }
        public string Price { get; set; }
        public string CreatedDate { get; set; }
        public string Action { get; set; }
    }
}