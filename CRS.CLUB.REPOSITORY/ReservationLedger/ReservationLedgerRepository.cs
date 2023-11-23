using CRS.CLUB.SHARED.ReservationLedger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.REPOSITORY.ReservationLedger
{
    public class ReservationLedgerRepository : IReservationLedgerRepository
    {
        private readonly RepositoryDao _dao;
        public ReservationLedgerRepository()
        {
            _dao = new RepositoryDao();
        }
        public List<ReservationLedgerCommon> GetReservationLedgerListDetail()
        {
            List<ReservationLedgerCommon> responseInfo = new List<ReservationLedgerCommon>();
            string sp_name = "EXEC sproc_club_reservationledgerdetaillist @Flag='rlld'";
            var dbResponseInfo = _dao.ExecuteDataTable(sp_name);
            if (dbResponseInfo != null)
            {
                foreach (DataRow dataRow in dbResponseInfo.Rows)
                {
                    responseInfo.Add(new ReservationLedgerCommon
                    {
                        Id = dataRow["Id"].ToString(),
                        CustomerName = dataRow["CustomerName"].ToString(),
                        NickName = dataRow["NickName"].ToString(),
                        PlanName = dataRow["PlanName"].ToString(),
                       // NoOfHosts = dataRow["NoOfHosts"].ToString(),
                        People = dataRow["People"].ToString(),
                        VisitedDate = dataRow["VisitedDate"].ToString(),
                        VisitedTime = dataRow["VisitedTime"].ToString(),
                        PaymentOption = dataRow["PaymentOption"].ToString(),
                        AdminPayment = dataRow["AdminPayment"].ToString(),
                        CustomerProfileImage = dataRow["CustomerProfileImage"].ToString(),
                        Commission = dataRow["Commission"].ToString(),
                    });
                }
            }
            return responseInfo;
        }
    }
}
