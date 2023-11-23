using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.ClubManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRS.CLUB.REPOSITORY.ClubManagement
{
    public interface IClubManagementRepository
    {
        CommonDbResponse DeleteImage(string CSNO, string AID, Common commonRequest);
        List<ClubManagementCommon> GetClubImages();
        CommonDbResponse InsertClubImage(AddClubImageCommon clubImageCommon);
        AddClubImageCommon GetClubImage(AddClubImageCommon ACC);
        List<HostManagementCommon> GetHostList(String AgentID);
        CommonDbResponse DeleteHost(string HID, string AID, Common commonRequest);
        HostManagementCommon GetHostByID(HostManagementCommon EHC);
        CommonDbResponse AddClubHost(HostManagementCommon common);
        List<EventManagementCommon> GetEventList(string agentId);
        CommonDbResponse DeleteEvent(string EID, string aID, Common commonRequest);
        CommonDbResponse AddEvent(EventManagementCommon common);
        EventManagementCommon GetEventById(EventManagementCommon EMC);
    }
}
