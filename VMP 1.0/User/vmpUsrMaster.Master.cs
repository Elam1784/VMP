using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VMP.Library;
using System.Configuration;
using System.Web.Configuration;

namespace VMP_1._0.User
{
    public partial class vmpUsrMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Session["Reset"] = true;
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
            SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
            int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    string qur = "Select firstName from Login where userId='" + Session["UserId"].ToString() + "'";
                    string username = dbLibrary.idGetAFieldByQuery(qur);
                    bool isAdmin = bool.Parse(Session["isAdmin"].ToString());
                    if (isAdmin)
                    {
                        lnkAdminPanel.Visible = true;
                    }
                    else
                    {
                        lnkAdminPanel.Visible = false;
                    }
                    lblName.Text = "Hi " + username;
                }
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();

            Response.Redirect("~/Logout.aspx");
        }
    }

}