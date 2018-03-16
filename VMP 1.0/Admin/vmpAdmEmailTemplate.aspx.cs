using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VMP.Library;

namespace VMP_1._0.Admin
{
    public partial class vmpAdmEmailTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadgrdeMail();
            }
        }

        private void loadgrdeMail()
        {
            string qur = "Select * from eMailTemplate";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if(ds.Tables[0].Rows.Count>0)
            {
                divView.Attributes.Add("style", "display:block");
                divView.Attributes.Add("style", "margin-top:30px");
                grdeMail.DataSource = ds;
                grdeMail.DataBind();
            }
            else
            {
                divView.Attributes.Add("style", "display:none");
            }
        }

        protected void grdeMail_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdeMail.Rows)
            {
                if (row.RowIndex == grdeMail.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#6192d3");
                    row.ForeColor = Color.White;
                    row.ToolTip = string.Empty;
                    ddlLetters.SelectedValue = grdeMail.SelectedRow.Cells[0].Text;
                    txtBody.Text = grdeMail.SelectedRow.Cells[1].Text;
                    btnSave.Text = "Update";
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ForeColor = Color.Black;
                    row.ToolTip = "Click to select this row.";
                }
            }
        }

        protected void grdeMail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdeMail, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblErrorSave.Visible = false;
            if(ddlLetters.SelectedValue=="-1")
            {
                lblErrorSave.Text = "Select Letter";
                lblErrorSave.Visible = true;
                return;
            }
            if(txtBody.Text=="")
            {
                lblErrorSave.Text = "Enter email Body";
                lblErrorSave.Visible = true;
                return;
            }

            string qur = dbLibrary.idBuildQuery("[proc_saveE-MailTemplate]", ddlLetters.SelectedValue, txtBody.Text);
            dbLibrary.idExecute(qur);
            btnNew_Click(sender, EventArgs.Empty);
            ScriptManager.RegisterStartupScript(this,this.GetType(), "CallMyFunction1", "runEffect1()", true);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            loadgrdeMail();
            txtBody.Text = "";
            ddlLetters.SelectedValue = "-1";
            btnSave.Text = "Save";
        }

        protected void ddlLetters_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBody.Text = "";
            loadgrdeMail();
        }
    }
}