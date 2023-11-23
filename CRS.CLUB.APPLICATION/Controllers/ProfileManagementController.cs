using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.UserProfileManagement;
using CRS.CLUB.BUSINESS.ProfileManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ProfileManagement;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    public class ProfileManagementController : BaseController
    {
        private readonly IProfileManagementBusiness _business;

        public ProfileManagementController(IProfileManagementBusiness business) => this._business = business;
        public ActionResult Index()
        {
            var common = new UserProfileCommon()
            {
                AgentId = ApplicationUtilities.GetSessionValue("AgentId")?.ToString()?.DecryptParameter(),
                ActionUser = ApplicationUtilities.GetSessionValue("UserName")?.ToString(),
            };

            var dbResp = _business.GetUserProfile(common);

            if (dbResp == null)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    Message = "Something went wrong",
                    NotificationType = NotificationMessage.ERROR,
                    Title = NotificationMessage.ERROR.ToString(),
                });
                return View();
            }
            var viewModel = dbResp.MapObject<UserProfileModel>();
            string FileLocationPath = "";
            if (ConfigurationManager.AppSettings["Phase"] != null
               && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                FileLocationPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;
            viewModel.ProfilePicture = FileLocationPath + viewModel.ProfilePicture;
            viewModel.CoverPicture = FileLocationPath + viewModel.CoverPicture;
            return View(viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(UserProfileModel userProfile, HttpPostedFileBase profilePicture, HttpPostedFileBase coverPicture)
        {
            if (!ModelState.IsValid)
            {
                AddNotificationMessage(new NotificationModel()
                {
                    Message = "All fields are required",
                    NotificationType = NotificationMessage.ERROR,
                    Title = NotificationMessage.ERROR.ToString(),
                });
            }

            string FileLocationPath = "/Content/userupload/ClubManagement/";
            if (ConfigurationManager.AppSettings["Phase"] != null
                && ConfigurationManager.AppSettings["Phase"].ToString().ToUpper() != "DEVELOPMENT")
                FileLocationPath = ConfigurationManager.AppSettings["ImageVirtualPath"].ToString() + FileLocationPath;

            string profilePicturePath = "";
            string coverPicturePath = "";
            var allowedContentType = new[] { "image/png", "image/jpeg", "image/HEIF", "image/heif" };
            string dateTime = "";
            if (profilePicture != null)
            {
                var contentType = profilePicture.ContentType;
                var ext = Path.GetExtension(profilePicture.FileName);
                if (allowedContentType.Contains(contentType.ToLower()))
                {
                    dateTime = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string fileName = dateTime + ext.ToLower();
                    profilePicturePath = Path.Combine(Server.MapPath(FileLocationPath), fileName);
                    userProfile.ProfilePicture = FileLocationPath + fileName;
                }
                else
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.INFORMATION,
                        Message = "Profile image must be in jpeg, jpg, heif or png format",
                        Title = NotificationMessage.INFORMATION.ToString()
                    });
                    return View(userProfile);
                }
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Profile picture required",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                return View(userProfile);
            }

            if (coverPicture != null)
            {
                var contentType = profilePicture.ContentType;
                var ext = Path.GetExtension(profilePicture.FileName);
                if (allowedContentType.Contains(contentType.ToLower()))
                {
                    dateTime = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    string fileName = dateTime + ext.ToLower();
                    coverPicturePath = Path.Combine(Server.MapPath(FileLocationPath), fileName);
                    userProfile.CoverPicture = FileLocationPath + fileName;
                }
                else
                {
                    AddNotificationMessage(new NotificationModel()
                    {
                        NotificationType = NotificationMessage.INFORMATION,
                        Message = "Cover image must be in jpeg, jpg, heif or png format",
                        Title = NotificationMessage.INFORMATION.ToString()
                    });
                    return View(userProfile);
                }
            }
            else
            {
                AddNotificationMessage(new NotificationModel()
                {
                    NotificationType = NotificationMessage.INFORMATION,
                    Message = "Cover image required",
                    Title = NotificationMessage.INFORMATION.ToString()
                });
                return View(userProfile);
            }
            var dbRequestCommon = userProfile.MapObject<UserProfileCommon>();
            dbRequestCommon.AgentId = ApplicationUtilities.GetSessionValue("AgentId").ToString()?.DecryptParameter();
            var dbResponse = _business.UpdateUserProfile(dbRequestCommon);
            if (dbResponse.Code == ResponseCode.Success)
            {
                if (profilePicture != null) ApplicationUtilities.ResizeImage(profilePicture, profilePicturePath);
                if (coverPicture != null) ApplicationUtilities.ResizeImage(coverPicture, coverPicturePath);
                AddNotificationMessage(new NotificationModel()
                {
                    Message = dbResponse.Message ?? "Success",
                    NotificationType = NotificationMessage.SUCCESS,
                    Title = NotificationMessage.SUCCESS.ToString(),
                });
                return RedirectToAction("Dashboard", "Home");
            }
            AddNotificationMessage(new NotificationModel()
            {
                Message = dbResponse.Message ?? "Failed",
                NotificationType = NotificationMessage.ERROR,
                Title = NotificationMessage.ERROR.ToString(),
            });
            return View(userProfile);
        }
    }
}