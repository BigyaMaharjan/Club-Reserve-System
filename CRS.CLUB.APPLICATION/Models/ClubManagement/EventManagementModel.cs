using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRS.CLUB.APPLICATION.Models.ClubManagement
{
    public class EventManagementModel :CommonModel
    {
        public string AgentId { get; set; }
        public string EventId { get; set; }
        [DisplayName("Event Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string EventTitle { get; set; }
        [DisplayName("Event Detail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Description { get; set; }
        [DisplayName("Event Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string EventDate { get; set; }
    }
}