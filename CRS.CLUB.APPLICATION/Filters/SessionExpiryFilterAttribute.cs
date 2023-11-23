using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace CRS.CLUB.APPLICATION.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SessionExpiryFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // If the browser session or authentication session has expired...
            if (ctx.Session["UserName"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                        { "Controller", "Home" },
                        { "Action", "LogOff" }
                        });
            }
            else
            {
                var controllerName = string.Empty;
                var actionName = string.Empty;
                var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
                var dataTokens = HttpContext.Current.Request.RequestContext.RouteData.DataTokens;

                if (routeValues != null)
                {
                    if (routeValues.ContainsKey("action"))
                    {
                        actionName = routeValues["action"].ToString();
                    }
                    if (routeValues.ContainsKey("controller"))
                    {
                        controllerName = routeValues["controller"].ToString();
                    }

                    var Role = ctx.Session["UserType"] != null ? ctx.Session["UserType"].ToString() : "";

                    #region check menu rights
                    var functions = ctx.Session["UserFunctions"] as List<string>;
                    if ((controllerName.ToUpper() == "HOME" && (actionName.ToUpper() == "INDEX" || actionName.ToUpper() == "LOGOFF" || actionName.ToUpper() == "VERIFYCODE"
                        || actionName.ToUpper() == "DASHBOARD")) || (controllerName.ToUpper() == "ERROR"))
                    {

                    }
                    else
                    {
                        var func = functions.ConvertAll(x => x.ToUpper());
                        var actionUrl = "/" + (controllerName + "/" + actionName).ToUpper();
                        if (func.Contains(actionUrl) == false && func.Equals(actionUrl) == false)
                        {
                            filterContext.Result = new RedirectToRouteResult(
                                new RouteValueDictionary
                                {
                                        {"Controller", "Error"},
                                        {"Action", "error_403"}
                                });
                        }
                    }
                    #endregion check menu rights

                }
                ////ApplicationUtilities.LogRequest();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}