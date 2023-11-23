namespace CRS.CLUB.APPLICATION.Models.UserProfileManagement
{
    public class UserProfileModel
    {
        #region BASIC INFO
        public string ClubNameEng { get; set; }
        public string ClubNameJap { get; set; }
        public string GroupName { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public string CoverPicture { get; set; }
        #endregion

        #region SOCIAL LINKS
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Tiktok { get; set; }
        public string Website { get; set; }
        public string GoogleMaps { get; set; }
        #endregion

        #region USER INFO
        public string FullName { get; set; }
        public string Password { get; set; }
        #endregion

        #region CLUB INFO
        public string WorkingHoursFrom { get; set; }
        public string WorkingHoursTo { get; set; }
        public string Holiday { get; set; }
        public string Zip { get; set; }
        public string Prefecture { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingAndRoomNo { get; set; }
        #endregion
    }
}