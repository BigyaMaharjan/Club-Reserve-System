using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.SHARED.ClubManagement
{
    public class EventManagementCommon : Common
    {
        public string AgentId { get; set; }
        public string EventId { get; set; }
        public string EventTitle { get; set; }
        public string Description { get; set; }
        public string EventDate { get; set; }
    }
}
