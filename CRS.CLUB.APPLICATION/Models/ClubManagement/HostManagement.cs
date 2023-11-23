using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CRS.CLUB.APPLICATION.Models.ClubManagement
{
    public class HostManagement : CommonModel
    {
        public string Sno { get; set; }
        public string Age { get; set; }
        public string AgentID { get; set; }
        public string HostID { get; set; }
        public HttpPostedFileBase HostImage { get; set; }
        public string Email { get; set; }
        [DisplayName("Host Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string HostName { get; set; }
        [DisplayName("Position")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Position { get; set; }
        [DisplayName("Twitter (X)")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Twitter { get; set; }
        [DisplayName("Instagram")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Instagram { get; set; }
        [DisplayName("TikTok")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string TikTok { get; set; }
        [DisplayName("Date of Birth")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string DateOfBirth { get; set; }
        public string Height { get; set; }
        public string Status { get; set; }
        [DisplayName("Liquor")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Liquor { get; set; }
        [DisplayName("Rank")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Rank { get; set; }
        [DisplayName("Constellation")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string Constellation { get; set; }
        [DisplayName("Blood Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string BloodType { get; set; }
        [DisplayName("Previous Occupation")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Required")]
        public string PreviousOccupation { get; set; }
        public string ConstellationGroup { get; set; }
    }
}