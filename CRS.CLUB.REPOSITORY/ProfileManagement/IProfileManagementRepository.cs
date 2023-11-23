using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ProfileManagement;

namespace CRS.CLUB.REPOSITORY.ProfileManagement
{
    public interface IProfileManagementRepository
    {
        UserProfileCommon GetUserProfile(UserProfileCommon userProfileCommon);
        CommonDbResponse UpdateUserProfile(UserProfileCommon userProfileCommon);
    }
}