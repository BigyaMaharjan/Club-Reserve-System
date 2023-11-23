using CRS.CLUB.SHARED.Home;
using CRS.CLUB.SHARED;

namespace CRS.CLUB.BUSINESS.Home
{
    public interface IHomeBusiness
    {
        CommonDbResponse Login(LoginRequestCommon Request);
    }
}
