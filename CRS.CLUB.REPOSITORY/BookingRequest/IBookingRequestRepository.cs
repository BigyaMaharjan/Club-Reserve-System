using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.BookingRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.REPOSITORY.BookingRequest
{
    public interface IBookingRequestRepository
    {
        CommonDbResponse ApproveBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform);
        List<AllBookingRequestListCommon> GetAllBookingRequestList();
        List<ApprovedBookingRequestListCommon> GetApprovedBookingList();
        List<PendingBookingRequestListCommon> GetPendingBookingList();
        CommonDbResponse RejectBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform);
    }
}
