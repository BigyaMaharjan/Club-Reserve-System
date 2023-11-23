using CRS.CLUB.REPOSITORY.Home;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.Home;

namespace CRS.CLUB.BUSINESS.Home
{
    public class HomeBusiness : IHomeBusiness
    {
        IHomeRepository _REPO;
        public HomeBusiness(HomeRepository REPO)
        {
            _REPO = REPO;
        }

        public CommonDbResponse Login(LoginRequestCommon Request)
        {
            return _REPO.Login(Request);
        }
    }
}
