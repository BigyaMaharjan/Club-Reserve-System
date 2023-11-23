using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ProfileManagement;

namespace CRS.CLUB.REPOSITORY.ProfileManagement
{
    public class ProfileManagementRepository : IProfileManagementRepository
    {
        private readonly RepositoryDao _dao;

        public ProfileManagementRepository(RepositoryDao dao) => this._dao = dao;

        public UserProfileCommon GetUserProfile(UserProfileCommon userProfileCommon)
        {
            var userProfile = new UserProfileCommon();
            string sql = "sproc_club_profile_management";
            sql += " @flag= 's'";
            sql += ", @AgentId=" + _dao.FilterString(userProfileCommon.AgentId);
            sql += ", @UserId=" + _dao.FilterString(userProfileCommon.ActionUserId);
            sql += ", @ActionUser=" + _dao.FilterString(userProfileCommon.ActionUser);
            var dr = _dao.ExecuteDataRow(sql);
            if (dr != null)
            {
                userProfile.ClubNameEng = dr["ClubNameEng"]?.ToString();
                userProfile.ClubNameEng = dr["ClubNameEng"]?.ToString();
                userProfile.GroupName = dr["GroupName"]?.ToString();
                userProfile.Role = dr["Role"]?.ToString();
                userProfile.UserName = dr["username"]?.ToString();
                userProfile.EmailAddress = dr["EmailAddress"]?.ToString();
                userProfile.Bio = dr["Bio"].ToString();
                userProfile.ProfilePicture = dr["ProfilePhoto"].ToString();
                userProfile.CoverPicture = dr["CoverPhoto"].ToString();
            }
            return userProfile;
        }

        public CommonDbResponse UpdateUserProfile(UserProfileCommon userProfileCommon)
        {
            string sql = "sproc_club_profile_management";
            sql += " @flag= 'u'";
            sql += ", @FullName=" + _dao.FilterString(userProfileCommon.FullName);
            sql += ", @UserId=" + _dao.FilterString(userProfileCommon.ActionUserId);
            sql += ", @AgentId=" + _dao.FilterString(userProfileCommon.AgentId);
            sql += ", @ClubNameEng=" + _dao.FilterString(userProfileCommon.ClubNameEng);
            sql += ", @ClubNameJap=" + _dao.FilterString(userProfileCommon.ClubNameJap);
            sql += ", @GroupName=" + _dao.FilterString(userProfileCommon.GroupName);
            sql += ", @Bio=" + _dao.FilterString(userProfileCommon.Bio);
            sql += ", @CoverPicture=" + _dao.FilterString(userProfileCommon.CoverPicture);
            sql += ", @ProfilePicture=" + _dao.FilterString(userProfileCommon.ProfilePicture);
            sql += ", @ActionUser=" + _dao.FilterString(userProfileCommon.ActionUser);
            return _dao.ParseCommonDbResponse(sql);
        }
    }
}