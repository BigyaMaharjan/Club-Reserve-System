using CRS.CLUB.APPLICATION.Library;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    public class LanguageManagementController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChangeLanguage(string lang)
        {
            new ManageLanguage().SetLanguage(lang);
            return RedirectToAction("Index", "LanguageManagement");
        }
    }
}