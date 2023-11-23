using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRS.CLUB.APPLICATION.Models.ClubManagement
{
    public class ClubManagementModel : CommonModel
    {
        public string Title { get; set; }
        public string AgentID { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public string Sno { get; set; }
        public string LoginId { get; set; }
        public string ImagePath { get; set; }
        public string CreatedDate { get; set; }
    }

    public class AddClubImage
    {
        [DisplayName("Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [DisplayName("Gallery/Banner")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "HEIF image is required.")]
        public HttpPostedFileBase ImageFile { get; set; }
        public string AgentId { get; set; }
        public string ClubSno { get; set; }
        public string Sno { get; set; }
        public string ImagePath { get; set; }
    }
}