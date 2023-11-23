using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Collections.Generic;

namespace CRS.CLUB.SHARED.Home
{
    #region Login
    public class LoginRequestCommon : Common
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string SessionId { get; set; }
    }
    public class LoginResponseCommon
    {
        public string Username { get; set; }
        public string AgentId { get; set; }
        public string UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Logo { get; set; }
        public string ClubName { get; set; }
        public string SessionId { get; set; }
        public string RoleId { get; set; }
        public List<MenuCommon> Menus { get; set; }
        public List<string> Functions { get; set; }
    }
    public class MenuCommon
    {
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public string MenuGroup { get; set; }
        public string ParentGroup { get; set; }
        public string CssClass { get; set; }
        public string GroupOrderPosition { get; set; }
        public string MenuOrderPosition { get; set; }
    }
    #endregion
}
