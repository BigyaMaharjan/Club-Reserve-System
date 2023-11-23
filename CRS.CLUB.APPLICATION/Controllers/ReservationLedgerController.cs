using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.ReservationLedger;
using CRS.CLUB.BUSINESS.ReservationLedger;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    [OverrideActionFilters]
    public class ReservationLedgerController : Controller
    {
        private readonly IReservationLedgerBusiness _business;
        public ReservationLedgerController(IReservationLedgerBusiness business)
        {
            _business = business;
        }
        public ActionResult ReservationLedger()

        {
            List<ReservationLedgerModel> responseInfo = new List<ReservationLedgerModel>();
            var dbResponse = _business.GetReservationLedgerListDetail();
            responseInfo = dbResponse.MapObjects<ReservationLedgerModel>();
            ViewBag.ReservationLedgerListDetail = responseInfo;
            return View();
        }
    }
}