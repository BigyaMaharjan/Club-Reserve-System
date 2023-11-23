using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.ClubManagement;
using CRS.CLUB.BUSINESS.ClubManagement;
using CRS.CLUB.BUSINESS.CommonManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ClubManagement;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    public class ClubManagementController : Controller
    {
        private readonly IClubManagementBussiness _buss;
        private readonly ICommonManagementBusiness _common;

        public ClubManagementController(IClubManagementBussiness buss, ICommonManagementBusiness comm)
        {
            _buss = buss;
            _common = comm;
        }
        public ActionResult Index()
        {
            var AgentId = Session["AgentId"]?.ToString().DecryptParameter();
            
            List<ClubManagementModel> CMM = new List<ClubManagementModel>();
            List<EventManagementModel> EMM = new List<EventManagementModel>();
            List<HostManagement> HM = new List<HostManagement>();

            var response = _buss.GetClubImages();
            var responseHost = _buss.GetHostList(AgentId);
            var responseEvent = _buss.GetEventList(AgentId);
            if (response != null)
            {
                CMM = response.MapObjects<ClubManagementModel>();

                foreach (var item in CMM)
                {
                    item.AgentID = item.AgentID.EncryptParameter();
                    item.Sno = item.Sno.EncryptParameter();
                }

                ViewBag.LiquoreStrength = ApplicationUtilities.SetDDLValue(LoadDropdownList("liqStrength", "9") as Dictionary<string, string>, "", "--- Select ---");
                ViewBag.BloodType = ApplicationUtilities.SetDDLValue(LoadDropdownList("bloodtype", "11") as Dictionary<string, string>, "", "--- Select ---");
                ViewBag.PreviousOccupation = ApplicationUtilities.SetDDLValue(LoadDropdownList("preOcc", "12") as Dictionary<string, string>, "", "--- Select ---");
                ViewBag.Zodiac = ApplicationUtilities.SetDDLValue(LoadDropdownList("constellation", "13") as Dictionary<string, string>, "", "--- Select ---");
                ViewBag.Rank = ApplicationUtilities.SetDDLValue(LoadDropdownList("rank", "14") as Dictionary<string, string>, "", "--- Select ---");

                ViewData["gridData"] = CMM;
            }
            if (responseHost != null)
            {
                HM = responseHost.MapObjects<HostManagement>();

                foreach (var item in HM)
                {
                    item.AgentID = item.AgentID.EncryptParameter();
                    item.HostID = item.HostID.EncryptParameter();
                }

                ViewData["gridHost"] = HM;
            }
            if (responseEvent != null)
            {
                EMM = responseEvent.MapObjects<EventManagementModel>();

                foreach (var item in EMM)
                {
                    item.AgentId = item.AgentId.EncryptParameter();
                    item.EventId = item.EventId.EncryptParameter();
                }
                ViewData["gridEvent"] = EMM;
                return View();
            }
            else
            {
                return View();
            }
        }

        #region Club Management - Gallery Management
        [HttpGet]
        public ActionResult AddClubImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddClubImage(AddClubImage model)
        {
            var ClubImageCommon = new AddClubImageCommon();
            var response = new CommonDbResponse();

            if (string.IsNullOrEmpty(model.Title))
            {
                this.ShowPopup(100, "Title is required");
                return RedirectToAction("Index");
            }
            HttpPostedFileBase file = Request.Files["imageFile"];
            if (file != null && file.ContentLength > 0)
            {
                string imgPath;
                var allowedContenttype = new[] { ".heif" };
                var ext = Path.GetExtension(file.FileName);
                if (allowedContenttype.Contains(ext.ToLower()))
                {
                    string datet = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string myfilename = "ClubManagementImg_" + datet + ext.ToLower();
                    imgPath = Path.Combine(Server.MapPath("~/Content/userupload/ClubManagement/"), myfilename);

                    ClubImageCommon.ImagePath = "/Content/userupload/ClubManagement/" + myfilename;
                }
                else
                {
                    this.ShowPopup(1, "File Must be .HEIF");
                    return RedirectToAction("Index");
                }
                ClubImageCommon.ActionUser = Session["username"]?.ToString();
                ClubImageCommon.ImageTitle = model.Title;
                ClubImageCommon.Sno = model.Sno.DecryptParameter();
                ClubImageCommon.AgentId = string.IsNullOrEmpty(model.AgentId) ? Session["AgentId"]?.ToString().DecryptParameter() : model.AgentId.DecryptParameter();
                var serviceResp = _buss.InsertClubImage(ClubImageCommon);
                if (serviceResp != null && serviceResp.Code == ResponseCode.Success)
                {
                    file.SaveAs(imgPath);
                    this.ShowPopup(0, serviceResp.Message);
                    return RedirectToAction("Index");

                }
                this.ShowPopup(1, "Something went Wrong !");
                return RedirectToAction("Index");
            }
            else
            {
                this.ShowPopup(1, "Required Parameters are missing");
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteClubManagement(string AgentId, string clubsno)
        {
            var response = new CommonDbResponse();
            var CSNO = !string.IsNullOrEmpty(clubsno) ? clubsno.DecryptParameter() : null;
            var AID = !string.IsNullOrEmpty(AgentId) ? AgentId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(CSNO) || string.IsNullOrEmpty(AID))
                response = new CommonDbResponse { ErrorCode = 1, Message = "Details are missing" };
            var commonRequest = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString()
            };
            var dbResponse = _buss.DeleteImage(CSNO, AID, commonRequest);
            response = dbResponse;
            this.ShowPopup(0, response.Message);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult EditClubManagement(string AgentId, string clubsno)
        {
            var response = new CommonDbResponse();
            AddClubImageCommon ACI = new AddClubImageCommon
            {
                AgentId = AgentId.DecryptParameter(),
                Sno = clubsno.DecryptParameter()
            };
            var CSNO = !string.IsNullOrEmpty(clubsno) ? clubsno.DecryptParameter() : null;
            var AID = !string.IsNullOrEmpty(AgentId) ? AgentId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(CSNO) || string.IsNullOrEmpty(AID))
                response = new CommonDbResponse { ErrorCode = 1, Message = "Details are missing" };

            var dbResponse = _buss.GetClubImage(ACI);
            var model = dbResponse.MapObject<AddClubImage>();
            model.AgentId = model.AgentId.EncryptParameter();
            model.ClubSno = clubsno;
            return Json(new { model }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Club Management - Host Management
        [HttpGet]
        public ActionResult AddClubHost()
        {
            HostManagement HM = new HostManagement()
            {
                AgentID = Session["UserId"]?.ToString()
            };

            ViewBag.LiquoreStrength = ApplicationUtilities.SetDDLValue(LoadDropdownList("liqStrength", "9") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.BloodType = ApplicationUtilities.SetDDLValue(LoadDropdownList("bloodtype", "11") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.PreviousOccupation = ApplicationUtilities.SetDDLValue(LoadDropdownList("preOcc", "12") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.Zodiac = ApplicationUtilities.SetDDLValue(LoadDropdownList("constellation", "13") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.Rank = ApplicationUtilities.SetDDLValue(LoadDropdownList("rank", "14") as Dictionary<string, string>, "", "--- Select ---");

            return View(HM);
        }

        [HttpPost]
        public ActionResult AddClubHost(HostManagement model)
        {
            var response = new CommonDbResponse();
            model.AgentID = model?.AgentID?.DecryptParameter();
            model.HostID = model?.HostID?.DecryptParameter();
            var HostManagementCommon = new HostManagementCommon();
            HostManagementCommon.ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
            HostManagementCommon.ActionIP = ApplicationUtilities.GetIP();
            HostManagementCommon.ActionPlatform = "Admin";
            HttpPostedFileBase file = Request.Files["hostimage"];
            if (file != null && file.ContentLength > 0)
            {
                string imgPath;
                var allowedContenttype = new[] { ".heif" };
                var ext = Path.GetExtension(file.FileName);
                if (allowedContenttype.Contains(ext.ToLower()))
                {
                    string datet = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string myfilename = "ClubHostProfileImg_" + datet + ext.ToLower();
                    imgPath = Path.Combine(Server.MapPath("~/Content/userupload/ClubManagement/ClubHost/"), myfilename);

                    HostManagementCommon.ImagePath = "/Content/userupload/ClubManagement/ClubHost/" + myfilename;
                }
                else
                {
                    this.ShowPopup(1, "File Must be .HEIF");
                    return RedirectToAction("Index");
                }
                HostManagementCommon.ActionUser = Session["username"]?.ToString();
                HostManagementCommon.HostId = model.HostID.DecryptParameter();
                HostManagementCommon.HostName = model.HostName;
                HostManagementCommon.Position = model.Position;
                HostManagementCommon.Rank = model.Rank;
                HostManagementCommon.Height = model.Height;
                HostManagementCommon.Twitter = model.Twitter;
                HostManagementCommon.Instagram = model.Instagram;
                HostManagementCommon.TikTok = model.TikTok;
                HostManagementCommon.DateOfBirth = model.Age;
                HostManagementCommon.Constellation = model.Constellation;
                HostManagementCommon.BloodType = model.BloodType;
                HostManagementCommon.PreviousOccupation = model.PreviousOccupation;
                HostManagementCommon.Liquor = model.Liquor;
                HostManagementCommon.AgentId = string.IsNullOrEmpty(model.AgentID) ? Session["UserId"]?.ToString().DecryptParameter() : model.AgentID.DecryptParameter();
                var serviceResp = _buss.AddClubHost(HostManagementCommon);
                if (serviceResp != null && serviceResp.Code == ResponseCode.Success)
                {
                    file.SaveAs(imgPath);
                    this.ShowPopup(0, serviceResp.Message);
                }
            }
            else
            {
                HostManagementCommon.ActionUser = Session["username"]?.ToString();
                HostManagementCommon.HostId = model.HostID.DecryptParameter();
                HostManagementCommon.HostName = model.HostName;
                HostManagementCommon.Position = model.Position;
                HostManagementCommon.Rank = model.Rank;
                HostManagementCommon.Height = model.Height;
                HostManagementCommon.Twitter = model.Twitter;
                HostManagementCommon.Instagram = model.Instagram;
                HostManagementCommon.TikTok = model.TikTok;
                HostManagementCommon.DateOfBirth = string.IsNullOrEmpty(model.Age) ? model.DateOfBirth : model.Age;
                HostManagementCommon.Constellation = model.Constellation;
                HostManagementCommon.BloodType = model.BloodType;
                HostManagementCommon.PreviousOccupation = model.PreviousOccupation;
                HostManagementCommon.Liquor = model.Liquor;
                HostManagementCommon.AgentId = string.IsNullOrEmpty(model.AgentID) ? Session["AgentID"]?.ToString().DecryptParameter() : model.AgentID.DecryptParameter();
                var serviceResp = _buss.AddClubHost(HostManagementCommon);
                this.ShowPopup(1, "HEIF is missing");
                return RedirectToAction("Index");
            }

            ViewBag.LiquoreStrength = ApplicationUtilities.SetDDLValue(LoadDropdownList("liqStrength", "9") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.BloodType = ApplicationUtilities.SetDDLValue(LoadDropdownList("bloodtype", "11") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.PreviousOccupation = ApplicationUtilities.SetDDLValue(LoadDropdownList("preOcc", "12") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.Zodiac = ApplicationUtilities.SetDDLValue(LoadDropdownList("constellation", "13") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.Rank = ApplicationUtilities.SetDDLValue(LoadDropdownList("rank", "14") as Dictionary<string, string>, "", "--- Select ---");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult EditHostManagement(string AgentId, string HostId)
        {
            var response = new CommonDbResponse();
            HostManagementCommon EHC = new HostManagementCommon
            {
                AgentId = AgentId.DecryptParameter(),
                HostId = HostId.DecryptParameter()
            };
            var CSNO = !string.IsNullOrEmpty(HostId) ? HostId.DecryptParameter() : null;
            var AID = !string.IsNullOrEmpty(AgentId) ? AgentId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(CSNO) || string.IsNullOrEmpty(AID))
                response = new CommonDbResponse { ErrorCode = 1, Message = "Details are missing" };

            var dbResponse = _buss.GetHostByID(EHC);
            var model = dbResponse.MapObject<HostManagement>();
            model.AgentID = model.AgentID.EncryptParameter();
            model.HostID = HostId.EncryptParameter();
            ViewBag.LiquoreStrength = ApplicationUtilities.SetDDLValue(LoadDropdownList("liqStrength", "9") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.BloodType = ApplicationUtilities.SetDDLValue(LoadDropdownList("bloodtype", "11") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.PreviousOccupation = ApplicationUtilities.SetDDLValue(LoadDropdownList("preOcc", "12") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.Zodiac = ApplicationUtilities.SetDDLValue(LoadDropdownList("constellation", "13") as Dictionary<string, string>, "", "--- Select ---");
            ViewBag.Rank = ApplicationUtilities.SetDDLValue(LoadDropdownList("rank", "14") as Dictionary<string, string>, "", "--- Select ---");

            return Json(new { model }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteHostManagement(string AgentId, string HostId)
        {
            var response = new CommonDbResponse();
            var HID = !string.IsNullOrEmpty(HostId) ? HostId.DecryptParameter() : null;
            var AID = !string.IsNullOrEmpty(AgentId) ? AgentId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(HID) || string.IsNullOrEmpty(AID))
                response = new CommonDbResponse { ErrorCode = 1, Message = "Details are missing" };
            var commonRequest = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString()
            };
            var dbResponse = _buss.DeleteHost(HID, AID, commonRequest);
            response = dbResponse;
            this.ShowPopup(0, response.Message);
            return RedirectToAction("Index");
        }
        #endregion

        #region Club Management - Event Management
        [HttpGet]
        public ActionResult AddEvent()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult AddEvent(EventManagementModel model)
        {
            var response = new CommonDbResponse();  
            model.EventId = model?.EventId?.DecryptParameter();
            model.AgentId = model?.AgentId?.DecryptParameter();
            if (string.IsNullOrWhiteSpace(model.EventId))
            {
                this.ShowPopup(ResponseCode.Failed, "Event ID is missing");
            }
            if (!ModelState.IsValid)
            {
                this.ShowPopup(ResponseCode.Failed, "Please input all required fields");
            }
            var common = model.MapObject<EventManagementCommon>();
            common.ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString();
            common.AgentId = string.IsNullOrEmpty(model.AgentId) ? Session["AgentId"]?.ToString().DecryptParameter() : model.AgentId;
            common.ActionIP = ApplicationUtilities.GetIP();
            common.ActionPlatform = "WEB";
            var serviceResp = _buss.AddEvent(common);
            if (serviceResp.Code == ResponseCode.Success)
            {
                this.ShowPopup(ResponseCode.Success, serviceResp.Message, Extra1: "true");
            }
            serviceResp.SetMessageInTempData(this);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult EditEventManagement(string AgentId, string EventId)
        {   
            var response = new CommonDbResponse();            
            EventManagementCommon EMC = new EventManagementCommon
            {
                AgentId = AgentId.DecryptParameter(),
                EventId = EventId.DecryptParameter()
            };
            var tare = "sw041235asf42185Do5y2Zutuxe9l1kA77tt4yh788qqw77tt4yh788qqw";
            var saj = tare.DecryptParameter();
            var CSNO = !string.IsNullOrEmpty(EventId) ? EventId.DecryptParameter() : null;
            var AID = !string.IsNullOrEmpty(AgentId) ? AgentId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(CSNO) || string.IsNullOrEmpty(AID))
                response = new CommonDbResponse { ErrorCode = 1, Message = "Details are missing" };

            var dbResponse = _buss.GetEventById(EMC);
            var model = dbResponse.MapObject<EventManagementModel>();
            model.AgentId = model.AgentId.EncryptParameter();
            model.EventId = EventId;
            return Json(new { model }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEventManagement(string AgentId, string EventId)
        {
            var response = new CommonDbResponse();
            var EID = !string.IsNullOrEmpty(EventId) ? EventId.DecryptParameter() : null;
            var AID = !string.IsNullOrEmpty(AgentId) ? AgentId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(EID) || string.IsNullOrEmpty(AID))
                response = new CommonDbResponse { ErrorCode = 1, Message = "Details are missing" };
            var commonRequest = new Common()
            {
                ActionIP = ApplicationUtilities.GetIP(),
                ActionUser = ApplicationUtilities.GetSessionValue("Username").ToString()
            };
            var dbResponse = _buss.DeleteEvent(EID, AID, commonRequest);
            return RedirectToAction("Index");
        }
        #endregion

        private object LoadDropdownList(string ForMethod, string search1 = "", string search2 = "")
        {
            switch (ForMethod)
            {
                case "rank":
                    return CreateDropdownList(_common.GetDropDown("014", search1, search2));
                case "constellation":
                    return CreateDropdownList(_common.GetDropDown("014", search1, search2));
                case "bloodtype":
                    return CreateDropdownList(_common.GetDropDown("014", search1, search2));
                case "preOcc":
                    return CreateDropdownList(_common.GetDropDown("014", search1, search2));
                case "liqStrength":
                    return CreateDropdownList(_common.GetDropDown("014", search1, search2));
                default:
                    return new Dictionary<string, string>();
            }
        }
        private object CreateDropdownList(Dictionary<string, string> dbResponse)
        {
            var response = new Dictionary<string, string>();
            dbResponse.ForEach(item => { response.Add(item.Key, item.Value); });
            return response;
        }
    }
}