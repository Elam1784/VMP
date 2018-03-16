using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using VMP.Library;

namespace VMP
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();

            // Handle HTTP errors
            // if (exc.GetType() == typeof(HttpException))
            //  {
            // The Complete Error Handling Example generates
            // some errors using URLs with "NoCatch" in them;
            // ignore these here to simulate what would happen
            // if a global.asax handler were not implemented.
            if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                return;
            string userid;
            try { 
            userid = Session["UserId"] != null ? Session["UserId"].ToString() : "";
            }
            catch
            {
                userid = string.Empty;     
            }
            string errMsg;
            if(exc.InnerException!=null)
            { 
            errMsg = exc.InnerException.Message.ToString().Length >= 250 ? exc.InnerException.Message.ToString().Substring(0, 249).Replace('\'', '\"').TrimEnd('.') : exc.InnerException.Message.ToString().Replace('\'', '\"').TrimEnd('.');
            }
            else
            {
                errMsg = exc.Message.ToString().Length >= 250 ? exc.Message.ToString().Substring(0, 249).Replace('\'', '\"').TrimEnd('.') : exc.Message.ToString().Replace('\'', '\"').TrimEnd('.');
            }


            // Response.Write(errMsg);
            string qur = dbLibrary.idBuildQuery("sp_ErrorLog", errMsg, "", userid);
            dbLibrary.idExecute(qur);
            Server.ClearError();
            //Redirect HTTP errors to HttpError page
            Response.Redirect("~/Error.html");
        }

        //protected void Application_BeginRequest(Object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(false))
        //    {
        //        Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"]
        //    + HttpContext.Current.Request.RawUrl);
        //    }
        //}

        //  For other kinds of errors give the user some information
        //     but stay on the default page
        //    Response.Write("<h2>Global Page Error</h2>\n");
        //Response.Write(
        //        "<p>" + exc.Message + "</p>\n");
        //    Response.Write("Return to the <a href='Default.aspx'>" +
        //        "Default Page</a>\n");

        //    // Log the exception and notify system operators
        //    ExceptionUtility.LogException(exc, "DefaultPage");
        //    ExceptionUtility.NotifySystemOps(exc);

        // Clear the error from the server
        //}
    }
}