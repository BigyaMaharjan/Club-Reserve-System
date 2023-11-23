using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRS.CLUB.APPLICATION.Models.Home
{
    public class LoginRequestModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "MobileNumberRequired")]
        [Display(Name = "Username")]
        public string LoginId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "PasswordRequired")]
        [Display(Name = "Password")]
        [MaxLength(16, ErrorMessage = "Maximum 16 characters allowed")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters required")]
        public string Password { get; set; }
        public string SessionId { get; set; }
    }
    public class LoginResponseModel
    {
        public string Username { get; set; }
        public string AgentId { get; set; }
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Logo { get; set; }
        public string ClubName { get; set; }
        public string SessionId { get; set; }
        public List<MenuModel> Menus { get; set; }
        public List<string> Functions { get; set; }
    }
    public class MenuModel
    {
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string MenuGroup { get; set; }
        public string ParentGroup { get; set; }
        public string CssClass { get; set; }
        public string GroupOrderPosition { get; set; }
        public string MenuOrderPosition { get; set; }
    }
}