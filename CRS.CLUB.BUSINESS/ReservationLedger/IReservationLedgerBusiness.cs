using CRS.CLUB.SHARED.ReservationLedger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.BUSINESS.ReservationLedger
{
    public interface IReservationLedgerBusiness
    {
        List<ReservationLedgerCommon> GetReservationLedgerListDetail();
    }
}
