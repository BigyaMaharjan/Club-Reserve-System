using CRS.CLUB.REPOSITORY.ReservationLedger;
using CRS.CLUB.SHARED.ReservationLedger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.BUSINESS.ReservationLedger
{
    public class ReservationLedgerBusiness : IReservationLedgerBusiness
    {
        private readonly IReservationLedgerRepository _repo;
        public ReservationLedgerBusiness(ReservationLedgerRepository repo)
        {
            _repo = repo;
        }
        public List<ReservationLedgerCommon> GetReservationLedgerListDetail()
        {
            return _repo.GetReservationLedgerListDetail();
        }
    }
}
