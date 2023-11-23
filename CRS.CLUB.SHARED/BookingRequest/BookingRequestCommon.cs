using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.SHARED.BookingRequest
{
    public class PendingBookingRequestListCommon : Common
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
    }
    public class ApprovedBookingRequestListCommon : Common
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
    }
    public class AllBookingRequestListCommon : Common
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
    }
}
