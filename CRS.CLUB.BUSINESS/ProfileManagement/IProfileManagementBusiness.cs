using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ProfileManagement;

namespace CRS.CLUB.BUSINESS.ProfileManagement
{
    public interface IProfileManagementBusiness
    {
        UserProfileCommon GetUserProfile(UserProfileCommon userProfileCommon);
        CommonDbResponse UpdateUserProfile(UserProfileCommon userProfileCommon);
    }
}