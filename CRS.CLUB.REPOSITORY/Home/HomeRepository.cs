using CRS.CLUB.SHARED;
using CRS.CLUB.SHARED.Home;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CRS.CLUB.REPOSITORY.Home
{
    public class HomeRepository : IHomeRepository
    {
        RepositoryDao _dao;
        public HomeRepository()
        {
            _dao = new RepositoryDao();
        }
        #region Login
        public CommonDbResponse Login(LoginRequestCommon Request)
        {
            string SQL = "EXEC sproc_club_login_management @flag='Login'";
            SQL += ",@LoginId=" + _dao.FilterString(Request.LoginId);
            SQL += ",@Password=" + _dao.FilterString(Request.Password);
            SQL += ",@ActionIP=" + _dao.FilterString(Request.ActionIP);
            SQL += ",@ActionPlatform=" + _dao.FilterString(Request.ActionPlatform);
            var dbResponse = _dao.ExecuteDataRow(SQL);
            if (dbResponse != null)
            {
                string code = _dao.ParseColumnValue(dbResponse, "Code").ToString();
                string message = _dao.ParseColumnValue(dbResponse, "Message").ToString();
                if (string.IsNullOrEmpty(code) || code.Trim() != "0")
                {
                    return new CommonDbResponse()
                    {
                        Code = ResponseCode.Failed,
                        Message = string.IsNullOrEmpty(message) ? "Failed" : message
                    };
                }
                else
                {
                    var loginResponse = new LoginResponseCommon()
                    {
                        Username = _dao.ParseColumnValue(dbResponse, "Username").ToString(),
                        ClubName = _dao.ParseColumnValue(dbResponse, "ClubName").ToString(),
                        AgentId = _dao.ParseColumnValue(dbResponse, "AgentId").ToString(),
                        UserId = _dao.ParseColumnValue(dbResponse, "UserId").ToString(),
                        EmailAddress = _dao.ParseColumnValue(dbResponse, "EmailAddress").ToString(),
                        Logo = _dao.ParseColumnValue(dbResponse, "Logo").ToString(),
                        SessionId = _dao.ParseColumnValue(dbResponse, "SessionId").ToString(),
                        RoleId = _dao.ParseColumnValue(dbResponse, "RoleId").ToString()
                    };

                    string menuSQL = "EXEC sproc_get_menus @Flag='gcm'";
                    menuSQL += ",@Username=" + _dao.FilterString(Request.LoginId);
                    var menuDBResponse = _dao.ExecuteDataTable(menuSQL);
                    if (menuDBResponse != null)
                        loginResponse.Menus = _dao.DataTableToListObject<MenuCommon>(menuDBResponse).ToList();
                    else loginResponse.Menus = null;

                    string functionSQL = "EXEC sproc_get_function @Flag='gcf'";
                    functionSQL += ",@RoleId=" + _dao.FilterString(loginResponse.RoleId);
                    var functionDBResponse = _dao.ExecuteDataTable(functionSQL);
                    loginResponse.Functions = new List<string>();
                    if (functionDBResponse != null)
                    {
                        foreach (DataRow item in functionDBResponse.Rows)
                        {
                            loginResponse.Functions.Add(item["FunctionURL"].ToString());
                        }
                    }

                    return new CommonDbResponse()
                    {
                        Code = ResponseCode.Success,
                        Message = "Success",
                        Data = loginResponse
                    };
                }
            }
            return new CommonDbResponse()
            {
                Code = ResponseCode.Failed,
                Message = "Login failed"
            };
        }
        #endregion
    }
}
