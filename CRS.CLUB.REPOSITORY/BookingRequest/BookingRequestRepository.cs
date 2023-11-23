using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.BookingRequest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.REPOSITORY.BookingRequest
{
    public class BookingRequestRepository : IBookingRequestRepository
    {

        private readonly RepositoryDao _dao;
        public BookingRequestRepository()
        {
            _dao = new RepositoryDao();
        }

        public CommonDbResponse ApproveBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform)
        {
            string sp_name = "EXEC sproc_club_approveandrejectbookingrequest @Flag='abr'";
            sp_name += ",@Id=" + _dao.FilterString(newId);
            sp_name += ",@ActionUser=" + _dao.FilterString(actionUser);
            sp_name += ",@ActionIp=" + _dao.FilterString(actionIp);
            sp_name += ",@ActionPlatform=" + _dao.FilterString(actionPlatform);

            var dbresponse = _dao.ParseCommonDbResponse(sp_name);
            return dbresponse;
        }

        public List<AllBookingRequestListCommon> GetAllBookingRequestList()
        {
            List<AllBookingRequestListCommon> responseinfo = new List<AllBookingRequestListCommon>();
            string sp_name = "EXEC sproc_club_getbookingrequestlist @Flag='gabrl'";
            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow row in dbResponseInfo.Rows)
                {
                    responseinfo.Add(new AllBookingRequestListCommon
                    {
                        Id = row["Id"].ToString(),
                        CustomerName = row["CustomerName"].ToString(),
                        PlanName = row["PlanName"].ToString(),
                        People = row["People"].ToString(),
                        // NoOfHosts = row["NoOfHosts"].ToString(),
                        VisitedDate = row["VisitedDate"].ToString(),
                        VisitedTime = row["VisitedTime"].ToString(),
                        Status = row["Status"].ToString(),
                        Price = row["Price"].ToString(),
                        CreatedDate = row["CreatedDate"].ToString()

                    });
                }
            }
            return responseinfo;
        }

        public List<ApprovedBookingRequestListCommon> GetApprovedBookingList()
        {
            List<ApprovedBookingRequestListCommon> approvedlistResponse = new List<ApprovedBookingRequestListCommon>();
            string sp_name = "EXEC sproc_club_getbookingrequestlist @Flag='gapbrl'";
            var dbApprovedResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbApprovedResponseInfo != null)
            {
                foreach (DataRow row in dbApprovedResponseInfo.Rows)
                {
                    approvedlistResponse.Add(new ApprovedBookingRequestListCommon
                    {
                        Id = row["Id"].ToString(),
                        CustomerName = row["CustomerName"].ToString(),
                        PlanName = row["PlanName"].ToString(),
                        People = row["People"].ToString(),
                        VisitedDate = row["VisitedDate"].ToString(),
                        VisitedTime = row["VisitedTime"].ToString(),
                        Status = row["Status"].ToString(),
                        Price = row["Price"].ToString(),
                        CreatedDate = row["CreatedDate"].ToString()
                    });
                }
            }
            return approvedlistResponse;
        }

        public List<PendingBookingRequestListCommon> GetPendingBookingList()
        {
            List<PendingBookingRequestListCommon> pendingListInfo = new List<PendingBookingRequestListCommon>();
            string sp_name = "EXEC sproc_club_getbookingrequestlist @Flag='gpbrl'";
            var dbPendingResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbPendingResponseInfo != null)
            {
                foreach (DataRow row in dbPendingResponseInfo.Rows)
                {
                    pendingListInfo.Add(new PendingBookingRequestListCommon
                    {
                        Id = row["Id"].ToString(),
                        CustomerName = row["CustomerName"].ToString(),
                        PlanName = row["PlanName"].ToString(),
                        People = row["People"].ToString(),
                        VisitedDate = row["VisitedDate"].ToString(),
                        VisitedTime = row["VisitedTime"].ToString(),
                        Status = row["Status"].ToString(),
                        Price = row["Price"].ToString(),
                        CreatedDate = row["CreatedDate"].ToString()
                    });
                }
            }
            return pendingListInfo;
        }

        public CommonDbResponse RejectBookingRequest(string newId, string actionUser, string actionIp, string actionPlatform)
        {
            string sp_name = "EXEC sproc_club_approveandrejectbookingrequest @Flag='rbr'";
            sp_name += ",@Id=" + _dao.FilterString(newId);
            sp_name += ",@ActionUser=" + _dao.FilterString(actionUser);
            sp_name += ",@ActionIp=" + _dao.FilterString(actionIp);
            sp_name += ",@ActionPlatform=" + _dao.FilterString(actionPlatform);

            var dbresponse = _dao.ParseCommonDbResponse(sp_name);
            return dbresponse;
        }
    }
}
