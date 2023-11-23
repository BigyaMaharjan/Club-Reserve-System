using CRS.CLUB.SHARED.Home;
using CRS.CLUB.SHARED;

namespace CRS.CLUB.REPOSITORY.Home
{
    public interface IHomeRepository
    {
        CommonDbResponse Login(LoginRequestCommon Request);
    }
}
