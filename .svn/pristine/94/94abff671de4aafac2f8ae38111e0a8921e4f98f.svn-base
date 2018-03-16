using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VMP.Library;
using System.Data;
using System.Drawing;

namespace VMP_1._0.Admin
{
    public partial class vmpAdmOtherCert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiViewOC.SetActiveView(viewOC);
            }
        }
        protected void btnUpdateOC_Click(object sender, EventArgs e)
        {
            txtOtherCert.Text = btnUpdateOC.CommandName;
            btnSaveOC.CommandArgument = btnUpdateOC.CommandArgument;
            btnUpdateOC.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }

        protected void btnSaveOC_Click(object sender, EventArgs e)
        {
            if (grdOtherCert.SelectedValue.ToString() == btnSaveOC.CommandArgument)
            {
                dbLibrary.idUpdateTable("OtherDBECert",
                    "OtherDBETypeID='" + btnSaveOC.CommandArgument + "'",
                    "OtherDBEType", txtOtherCert.Text);
                viewOC_Load(sender, e);
                btnUpdateOC.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction1", "runEffect1()", true);
            }
        }

        protected void btnSaveNewOC_Click(object sender, EventArgs e)
        {
            dbLibrary.idInsertInto("OtherDBECert",
                "OtherDBEType", txtOtherCertNew.Text);
            viewOC_Load(sender, e);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction1", "runEffect1()", true);
        }

        protected void viewOC_Load(object sender, EventArgs e)
        {
            string qur = "select * from OtherDBECert";
            if (dbLibrary.idHasRows(qur))
            {
                DataSet ds = dbLibrary.idGetCustomResult(qur);
                grdOtherCert.DataSource = ds;
                grdOtherCert.DataBind();
                lblErrorC.Visible = false;
            }
            else
            {
                lblErrorC.Visible = true;
            }
        }

        protected void grdOtherCert_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdOtherCert, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void grdOtherCert_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdOtherCert.Rows)
            {
                if (row.RowIndex == grdOtherCert.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#6192d3");
                    row.ForeColor = Color.White;
                    row.ToolTip = string.Empty;
                    string text = grdOtherCert.SelectedRow.Cells[0].Text;
                    string value = grdOtherCert.SelectedDataKey.Value.ToString();
                    btnUpdateOC.Visible = true;
                    btnUpdateOC.CommandName = text;
                    btnUpdateOC.CommandArgument = value;
                }
            }
        }
    }
}