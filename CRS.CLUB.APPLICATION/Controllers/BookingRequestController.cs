using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.BookingRequest;
using CRS.CLUB.BUSINESS.BookingRequest;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    [OverrideActionFilters]
    public class BookingRequestController : Controller
    {
        private readonly IBookingRequestBusiness _business;
        public BookingRequestController(IBookingRequestBusiness business)
        {
            _business = business;
        }
        public ActionResult BookingRequestList()
        {
            List<AllBookingRequestList> allBookingRequestList = new List<AllBookingRequestList>();
            var dbResponse = _business.GetAllBookingRequestList();
            allBookingRequestList = dbResponse.MapObjects<AllBookingRequestList>();
            ViewBag.AllBookingRequestList = allBookingRequestList;
            List<PendingBookingRequestList> pendinglistInfo = new List<PendingBookingRequestList>();
            var dbPendingResponse = _business.GetPendingBookingList();
            pendinglistInfo = dbPendingResponse.MapObjects<PendingBookingRequestList>();
            ViewBag.PendingBookingList = pendinglistInfo;
            List<ApprovedBookingRequestList> approvedBookingRequestLists = new List<ApprovedBookingRequestList>();
            var dbapprovedResponse = _business.GetApprovedBookingList();
            approvedBookingRequestLists = dbapprovedResponse.MapObjects<ApprovedBookingRequestList>();
            ViewBag.ApprovedBookingList = approvedBookingRequestLists;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ApproveBookingRequest(string id = "")
        {
            if (string.IsNullOrEmpty(id)) return Json(new { Code = 1, Message = "Invalid Data." });
            string newId = id;
            string actionUser = Session["username"]?.ToString();
            string actionIp = ApplicationUtilities.GetIP();
            string actionPlatform = "Club";

            var dbResponse = _business.ApproveBookingRequest(newId, actionUser, actionIp, actionPlatform);
            dbResponse.Extra1 = "true";
            return Json(dbResponse.SetMessageInTempData(this));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RejectBookingRequest(string id = "")
        {
            if (string.IsNullOrEmpty(id)) return Json(new { Code = 1, Message = "Invalid Data." });
            string newId = id;
            string actionUser = Session["username"]?.ToString();
            string actionIp = ApplicationUtilities.GetIP();
            string actionPlatform = "Club";

            var dbResponse = _business.RejectBookingRequest(newId, actionUser, actionIp, actionPlatform);
            dbResponse.Extra1 = "true";
            return Json(dbResponse.SetMessageInTempData(this));
        }
    }
}