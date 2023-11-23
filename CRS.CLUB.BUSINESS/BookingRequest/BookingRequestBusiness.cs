using CRS.CLUB.REPOSITORY.BookingRequest;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.BookingRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.BUSINESS.BookingRequest
{
    public class BookingRequestBusiness : IBookingRequestBusiness
    {
        private readonly IBookingRequestRepository _repo;
        public BookingRequestBusiness(BookingRequestRepository repo)
        {
            _repo = repo;
        }

        public CommonDbResponse ApproveBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform)
        {
            return _repo.ApproveBookingRequest(newId, actionUser, actionIp, actionPlatform);
        }

        public List<AllBookingRequestListCommon> GetAllBookingRequestList()
        {
            return _repo.GetAllBookingRequestList();
        }

        public List<ApprovedBookingRequestListCommon> GetApprovedBookingList()
        {
            return _repo.GetApprovedBookingList();
        }

        public List<PendingBookingRequestListCommon> GetPendingBookingList()
        {
            return _repo.GetPendingBookingList();
        }

        public CommonDbResponse RejectBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform)
        {
            return _repo.RejectBookingRequest(newId, actionUser, actionIp, actionPlatform);
        }
    }
}
