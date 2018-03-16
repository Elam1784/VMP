using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VMP.Library;

namespace VMP_1._0.Admin
{
    public partial class vmpAdmUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
                LoadUserData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                string qur = dbLibrary.idBuildQuery("proc_AddUser", txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtUserName.Text,txtPhoneNumber.Text.Trim(), radbtnYes.Checked == true ? "True" : radbtnNo.Checked == true ? "False" : "False", "Insert", "");
                dbLibrary.idExecute(qur);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction1", "runEffect1()", true);
            }
            if (btnSave.Text == "Update")
            {
                if (Session["uid"] != null)
                {
                    string qur = dbLibrary.idBuildQuery("proc_AddUser", txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtUserName.Text, txtPhoneNumber.Text.Trim(), radbtnYes.Checked == true ? "True" : radbtnNo.Checked == true ? "False" : "False", "Update", Session["uid"].ToString());
                    dbLibrary.idExecute(qur);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction1", "runEffect1()", true);
                }
            }
            LoadUserData();
            Clear();

        }


        private void LoadUserData()
        {
            //LinkButton b1 = new LinkButton();
            //ButtonField b = new ButtonField();
            //b.Text = "Edit";
            //this.grdvwUser.Columns.Add(b);
            string qur = "Select * from Login where isDeleted='0'";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                //b.CommandName = ds.Tables[0].Rows[0][0].ToString();
                grdvwUser.DataSource = ds.Tables[0];
                grdvwUser.DataBind();
            }
        }

        protected void grdvwUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int Userid = 0;
            if (e.CommandName == "EditUser")
            {
                Userid = Convert.ToInt32(e.CommandArgument);
                Session["uid"] = Userid;
                string qur = "Select * from Login where userId='" + Userid + "'";
                DataSet ds = dbLibrary.idGetCustomResult(qur);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    txtFirstName.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtLastName.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtUserName.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtPhoneNumber.Text = ds.Tables[0].Rows[0][5].ToString();
                    if (ds.Tables[0].Rows[0][6].ToString() == "True")
                    {
                        radbtnYes.Checked = true;
                        radbtnNo.Checked = false;
                    }
                    else if (ds.Tables[0].Rows[0][6].ToString() == "False")
                    {
                        radbtnNo.Checked = true;
                        radbtnYes.Checked = false;
                    }
                    btnSave.Text = "Update";
                }
            }
            if (e.CommandName == "DeleteUser")
            {
                Userid = Convert.ToInt32(e.CommandArgument);
                Session["Delete"] = Userid;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            // string qur = "Delete from Login where userId='" + Session["Delete"].ToString() + "'";
            //  dbLibrary.idExecute(qur);
            dbLibrary.idUpdateTable("Login",
                "userId='"+ Session["Delete"].ToString() + "'",
                "isDeleted", "1");
            Session.Remove("Delete");
            LoadUserData();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "runEffect()", true);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtFirstName.Text = txtLastName.Text = txtEmail.Text = txtUserName.Text = txtPhoneNumber.Text = "";
            radbtnNo.Checked = radbtnYes.Checked = false;
            btnSave.Text = "Save";
            Session.Remove("Delete");
        }
    }
}