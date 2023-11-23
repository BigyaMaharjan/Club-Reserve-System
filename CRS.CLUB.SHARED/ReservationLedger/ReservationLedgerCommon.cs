using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.SHARED.ReservationLedger
{
    public class ReservationLedgerCommon : Common
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string NickName { get; set; }
        public string PlanName { get; set; }
        public string People { get; set; }
        public string NoOfHosts { get; set; }
        public string VisitedDate { get; set; }
        public string VisitedTime { get; set; }
        public string PaymentOption { get; set; }
        public string Commission { get; set; }
        public string AdminPayment { get; set; }
        public string CustomerProfileImage { get; set; }
    }
}
