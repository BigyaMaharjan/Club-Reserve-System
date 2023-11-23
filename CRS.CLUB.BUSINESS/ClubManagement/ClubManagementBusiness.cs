using CRS.CLUB.REPOSITORY.ClubManagement;
using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ClubManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.BUSINESS.ClubManagement
{
    public class ClubManagementBusiness : IClubManagementBussiness
    {
        private readonly IClubManagementRepository _clubMgmtRepo;
        public ClubManagementBusiness(ClubManagementRepository repo)
        {
            _clubMgmtRepo = repo;
        }

        public CommonDbResponse DeleteImage(string CSNO, string AID, Common commonRequest)
        {
            return _clubMgmtRepo.DeleteImage(CSNO,AID,commonRequest);
        }

        public List<ClubManagementCommon> GetClubImages()
        {
            return _clubMgmtRepo.GetClubImages();
        }

        public CommonDbResponse InsertClubImage(AddClubImageCommon clubImageCommon)
        {
            return _clubMgmtRepo.InsertClubImage(clubImageCommon);
        }

        public AddClubImageCommon GetClubImage(AddClubImageCommon ACC)
        {
            return _clubMgmtRepo.GetClubImage(ACC);
        }

        public List<HostManagementCommon> GetHostList(string AgentID)
        {
            return _clubMgmtRepo.GetHostList(AgentID);
        }

        public CommonDbResponse DeleteHost(string HID, string AID, Common commonRequest)
        {
            return _clubMgmtRepo.DeleteHost(HID,AID,commonRequest);
        }

        public HostManagementCommon GetHostByID(HostManagementCommon EHC)
        {
            return _clubMgmtRepo.GetHostByID(EHC);
        }

        public CommonDbResponse AddClubHost(HostManagementCommon common)
        {
            return _clubMgmtRepo.AddClubHost(common);
        }

        public List<EventManagementCommon> GetEventList(string agentId)
        {
            return _clubMgmtRepo.GetEventList(agentId);
        }

        public CommonDbResponse DeleteEvent(string EID, string aID, Common commonRequest)
        {
            return _clubMgmtRepo.DeleteEvent(EID, aID, commonRequest);
        }

        public CommonDbResponse AddEvent(EventManagementCommon common)
        {
            return _clubMgmtRepo.AddEvent(common);
        }

        public EventManagementCommon GetEventById(EventManagementCommon eMC)
        {
            return _clubMgmtRepo.GetEventById(eMC);
        }
    }
}
