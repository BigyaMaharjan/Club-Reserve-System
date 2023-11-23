using CRS.CLUB.APPLICATION.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Google.Apis.Requests.RequestError;

namespace CRS.CLUB.APPLICATION.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(string Id = "")
        {
            if (string.IsNullOrEmpty(Id))
                return RedirectToAction("Dashboard", "Home");
            ErrorModel model = new ErrorModel();
            model.ErrorId = Id;
            return View(model);
        }

        public ActionResult error_403()
        {
            return View();
        }
        public ActionResult error_404()
        {
            return View();
        }
    }
}