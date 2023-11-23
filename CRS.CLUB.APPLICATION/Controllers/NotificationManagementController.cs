using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.NotificationManagement;
using CRS.CLUB.BUSINESS.NotificationManagement;
using CRS.CLUB.SHARED.NotificationManagement;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    [OverrideActionFilters]
    public class NotificationManagementController : Controller
    {
        private readonly INotificationManagementBusiness _buss;
        public NotificationManagementController(INotificationManagementBusiness buss)
        {
            _buss = buss;
        }
        [HttpGet]
        public ActionResult ViewAllNotifications(string NotificationId = "")
        {
            var requestCommon = new ManageNotificationCommon()
            {
                AdminId = ApplicationUtilities.GetSessionValue("UserId").ToString().DecryptParameter(),
                NotificationId = !string.IsNullOrEmpty(NotificationId) ? NotificationId.DecryptParameter() : null,
            };
            var dbResponse = _buss.GetAllNotification(requestCommon);
            List<NotificationDetailModel> response = dbResponse.MapObjects<NotificationDetailModel>();
            IDictionary<string, string> param = new Dictionary<string, string>();
            param.Add("NotificationSubject", "Subject");
            param.Add("NotificationBody", "Notification Body");
            param.Add("CreatedDate", "Created On");
            param.Add("UpdatedDate", "Updated On");
            ProjectGrid.column = param;
            var grid = ProjectGrid.MakeGrid(dbResponse, "Notification Management", "", 0, false, "", "", "", "", "", "", "datatable-total", "false");
            ViewData["Grid"] = grid;
            return View();
        }
    }
}