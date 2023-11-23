using CRS.CLUB.APPLICATION.Library;
using CRS.CLUB.APPLICATION.Models.ReservationValidationManagement;
using CRS.CLUB.BUSINESS.ReservationValidationManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ReservationValidationManagement;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION.Controllers
{
    [OverrideActionFilters]
    public class ReservationValidationManagementController : Controller
    {
        private readonly IReservationValidationManagementBusiness _reservationValidationManagementBuss;
        public ReservationValidationManagementController(IReservationValidationManagementBusiness reservationValidationManagementBuss)
        {
            _reservationValidationManagementBuss = reservationValidationManagementBuss;
        }

        [HttpGet]
        public ActionResult CustomerValidation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CustomerValidationConfirmation(string OTPCode)
        {
            var Model = new ReservationDetailViaOTPModel();
            if (!string.IsNullOrEmpty(OTPCode))
            {
                var actionUser = "ClubOwner";//ApplicationUtilities.GetSessionValue("LoginId").ToString().DecryptParameter();
                var dbResponse = _reservationValidationManagementBuss.GetReservationDetailViaOTP(OTPCode, actionUser);
                if (dbResponse != null)
                {
                    if (dbResponse.Code == ResponseCode.Success)
                    {
                        Model = dbResponse.MapObject<ReservationDetailViaOTPModel>();
                        Model.OTPCode = OTPCode;
                        if (string.IsNullOrEmpty(Model.ReservationId))
                        {
                            this.ShowPopup(1, "Invalid details");
                            return RedirectToAction("CustomerValidation", "ReservationValidationManagement");
                        }
                        var HostDBResponse = _reservationValidationManagementBuss.GetReservationHostDetail(Model.ReservationId);
                        if (HostDBResponse.Count > 0)
                        {
                            Model.ReservationHostListModel = HostDBResponse.MapObjects<ReservationHostDetail>();
                            Model.ReservationHostListModel.ForEach(x => x.HostId = x.HostId.EncryptParameter());
                        }
                        Model.ReservationId = Model.ReservationId.EncryptParameter();
                        return View(Model);
                    }
                    this.ShowPopup(dbResponse.Code, dbResponse.Message ?? "Invalid details");
                    return RedirectToAction("CustomerValidation", "ReservationValidationManagement");
                }
            }
            this.ShowPopup(1, "Invalid details");
            return RedirectToAction("CustomerValidation", "ReservationValidationManagement");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CustomerValidationConfirmation(ManageReservationOTPStatusModel Model)
        {
            var ReservationId = !string.IsNullOrEmpty(Model.ReservationId) ? Model.ReservationId.DecryptParameter() : null;
            if (string.IsNullOrEmpty(ReservationId) || string.IsNullOrEmpty(Model.OTPCode))
            {
                this.ShowPopup(1, "Invalid details");
                return RedirectToAction("CustomerValidation", "ReservationValidationManagement");
            }
            var dbRequestModel = new ManageReservationOTPStatusCommon()
            {
                OTPCode = Model.OTPCode,
                ReservationId = ReservationId,
                ActionUser = "ClubOwner",//ApplicationUtilities.GetSessionValue("LoginId").ToString().DecryptParameter(),
                ActionIP = ApplicationUtilities.GetIP()
            };
            var dbResponse = _reservationValidationManagementBuss.ManageReservationOTPStatus(dbRequestModel);
            if (dbResponse != null && dbResponse.Code == ResponseCode.Success)
            {
                this.ShowPopup(0, dbResponse.Message ?? "Success");
                return RedirectToAction("CustomerValidationConfirmation", "ReservationValidationManagement", new { OTPCode = Model.OTPCode });
            }
            this.ShowPopup(1, dbResponse.Message ?? "Failed");
            return RedirectToAction("CustomerValidation", "ReservationValidationManagement");
        }
    }
}