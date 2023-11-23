using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.Home;
using CRS.CLUB.BUSINESS.Home;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.Home;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    [OverrideActionFilters]
    public class HomeController : BaseController
    {
        IHomeBusiness _BUSS;
        public HomeController(IHomeBusiness BUSS)
        {
            _BUSS = BUSS;
        }
        #region Login Management
        [OverrideActionFilters]
        public ActionResult Index()
        {
            var Username = ApplicationUtilities.GetSessionValue("Username").ToString();
            if (string.IsNullOrEmpty(Username))
            {
                this.ClearSessionData();
                return View(new LoginRequestModel());
            }
            else
                return RedirectToAction("Dashboard", "Home");
        }

        [OverrideActionFilters]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(LoginRequestModel Model)
        {
            if (ModelState.IsValid)
            {
                var loginResponse = Login(Model);
                return RedirectToAction(loginResponse.Item1, loginResponse.Item2);
            }
            var errorMessages = ModelState.Where(x => x.Value.Errors.Count > 0)
                                    .SelectMany(x => x.Value.Errors.Select(e => $"{x.Key}: {e.ErrorMessage}"))
                                    .ToList();

            var notificationModels = errorMessages.Select(errorMessage => new NotificationModel
            {
                NotificationType = NotificationMessage.ERROR,
                Message = errorMessage,
                Title = NotificationMessage.ERROR.ToString(),
            }).ToArray();
            AddNotificationMessage(notificationModels);
            return View(Model);
        }

        public Tuple<string, string> Login(LoginRequestModel Model)
        {
            LoginRequestCommon commonRequest = Model.MapObject<LoginRequestCommon>();
            commonRequest.SessionId = Session.SessionID;
            var dbResponse = _BUSS.Login(commonRequest);
            try
            {
                if (dbResponse.Code == 0)
                {
                    var response = dbResponse.Data.MapObject<LoginResponseModel>();
                    Session["SessionGuid"] = commonRequest.SessionId;
                    Session["Username"] = response.Username;
                    Session["UserId"] = response.UserId.EncryptParameter();
                    Session["AgentId"] = response.AgentId.EncryptParameter();
                    Session["ClubName"] = response.ClubName;
                    Session["EmailAddress"] = response.EmailAddress;
                    Session["LogoImage"] = response.Logo;
                    Session["Menus"] = response.Menus;
                    Session["Functions"] = response.Functions;
                    return new Tuple<string, string>("Dashboard", "Home");
                }
                this.AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = dbResponse.Message,
                    Title = NotificationMessage.ERROR.ToString(),
                });
                return new Tuple<string, string>("Index", "Home");
            }
            catch (Exception)
            {
                this.AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.ERROR,
                    Message = "Something went wrong",
                    Title = NotificationMessage.ERROR.ToString(),
                });
                return new Tuple<string, string>("Index", "Home");
            }
        }

        public ActionResult LogOff()
        {
            Session["Username"] = null;
            return RedirectToAction("Index", "Home");
        }
        #endregion

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}