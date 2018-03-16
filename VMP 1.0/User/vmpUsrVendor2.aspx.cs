using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VMP.Library;
using System.IO;
using System.Web.Configuration;
using System.Text.RegularExpressions;

namespace VMP_1._0.User
{
    public partial class vmpUsrVendor2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["VId"] != null)
                {
                    if (Request.QueryString["VId"].ToString().Contains("'"))
                    {
                        Response.Redirect("vmpUsrVendor1.aspx");
                    }
                    //condition to check to prevent users entering invalid vendor ID on query string (on the URL)
                    else if (!dbLibrary.isValidVendor(Request.QueryString["VId"]))
                    {
                        Response.Redirect("vmpUsrVendor1.aspx");
                    }
                    loadStepsProgress();
                    loadActionDates();
                }
                if (hdnFourthPageComplete.Value == "True")
                {
                    lnkGoToStatus.Attributes.Add("style", "display:block");
                }
                else
                {
                    lnkGoToStatus.Attributes.Add("style", "display:none");
                }
                if (Request.QueryString["EditMode"] != null)
                {
                    heading.InnerText = "Edit Vendor";
                }
            }
        }
        /*
         *Method to get the log actions  
          */
        private void loadActionDates()
        {
            string qur = "Select logActionId, actionDateTime from LogActions where vendorId=" + Request.QueryString["VId"].ToString() + " order by actionDateTime DESC";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count != 0)
            {
                grdActionDates.DataSource = ds;
                grdActionDates.DataBind();
                grdActionDates.SelectedIndex = 0;
                grdActionDates_SelectedIndexChanged(grdActionDates, EventArgs.Empty);
            }
        }

        /*
         *Method to get the vendor progress status to show the progress bar  
          */
        private void loadStepsProgress()
        {
            string qur = dbLibrary.idBuildQuery("proc_getNewVendorProgress", Request.QueryString["VId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if ((bool)ds.Tables[0].Rows[0]["step4"] == true)
                {
                    divStep4.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                    divStep3.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                    divStep1.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                    divStep2.Attributes.Add("class", "col-xs-3 bs-wizard-step active activecomplete");
                    hdnFourthPageComplete.Value = "True";
                }
                else
                {
                    divStep4.Attributes.Add("class", "col-xs-3 bs-wizard-step disabled");
                    if ((bool)ds.Tables[0].Rows[0]["step3"] == true)
                    {
                        divStep1.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                        divStep3.Attributes.Add("class", "col-xs-3 bs-wizard-step complete lastcomplete");
                        divStep2.Attributes.Add("class", "col-xs-3 bs-wizard-step active activecomplete");
                    }
                    else
                    {
                        divStep3.Attributes.Add("class", "col-xs-3 bs-wizard-step disabled");
                        if ((bool)ds.Tables[0].Rows[0]["step2"] == true)
                        {
                            divStep2.Attributes.Add("class", "col-xs-3 bs-wizard-step active lastcomplete");
                            divStep1.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                        }
                        else
                        {
                            divStep2.Attributes.Add("class", "col-xs-3 bs-wizard-step active");
                            divStep1.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                        }
                    }
                }
            }
            if(ds.Tables[1].Rows.Count>0)
            {
                lblVendorHeading.Text = ds.Tables[1].Rows[0]["vendorName"].ToString();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["EditMode"] == null)
            {
                Response.Redirect("vmpUsrVendor1.aspx?VId=" + Request.QueryString["VId"]);
            }
            else
            {
                Response.Redirect("vmpUsrVendor1.aspx?VId=" + Request.QueryString["VId"] + "&EditMode=True");
            }
            // Pass Vendor Id as a Query String
        }

        protected void lnkStep1_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["EditMode"] == null)
            {
                Response.Redirect("vmpUsrVendor1.aspx?VId=" + Request.QueryString["VId"]);
            }
            else
            {
                Response.Redirect("vmpUsrVendor1.aspx?VId=" + Request.QueryString["VId"] + "&EditMode=True");
            }
        }

        protected void lnkStep3_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["EditMode"] == null)
            {
                Response.Redirect("vmpUsrVendor3.aspx?VId=" + Request.QueryString["VId"]);
            }
            else
            {
                Response.Redirect("vmpUsrVendor3.aspx?VId=" + Request.QueryString["VId"] + "&EditMode=True");
            }
        }

        protected void lnkStep4_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["EditMode"] == null)
            {
                Response.Redirect("vmpUsrVendor4.aspx?VId=" + Request.QueryString["VId"]);
            }
            else
            {
                Response.Redirect("vmpUsrVendor4.aspx?VId=" + Request.QueryString["VId"] + "&EditMode=True");
            }
        }
        protected void grdActionDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdActionDates.Rows)
            {
                if (row.RowIndex == grdActionDates.SelectedIndex)
                {
                    row.BackColor = ColorTranslator.FromHtml("#6192d3");
                    row.ForeColor = Color.White;
                    row.ToolTip = string.Empty;
                    clearAllSelections();
                    string qur = "Select * from LogActions where logActionId=" + grdActionDates.SelectedDataKey.Value;
                    DataSet ds = dbLibrary.idGetCustomResult(qur);
                    hdnFieldLogActionId.Value = ds.Tables[0].Rows[0]["logActionId"].ToString();
                    if ((bool)ds.Tables[0].Rows[0]["phonedResponse"] == true)
                    {
                        chkbxVendorActions.Items[0].Selected = true;
                    }

                    if ((bool)ds.Tables[0].Rows[0]["writtenResponse"] == true)
                    {
                        chkbxVendorActions.Items[1].Selected = true;
                    }

                    if ((bool)ds.Tables[0].Rows[0]["appealDenial"] == true)
                    {
                        chkbxVendorActions.Items[2].Selected = true;
                    }

                    if ((bool)ds.Tables[0].Rows[0]["failedToAttend"] == true)
                    {
                        chkbxVendorActions.Items[3].Selected = true;
                    }

                    if ((bool)ds.Tables[0].Rows[0]["reApplicationAttempt"] == true)
                    {
                        chkbxVendorActions.Items[4].Selected = true;
                    }

                    if (ds.Tables[0].Rows[0]["otherVResponse"] != null && ds.Tables[0].Rows[0]["otherVResponse"].ToString() != "")
                    {
                        chkbxVendorActions.Items[5].Selected = true;
                        chkbxVendorActions_SelectedIndexChanged(chkbxVendorActions, EventArgs.Empty);
                        txtVANotes.Text = ds.Tables[0].Rows[0]["otherVResponse"].ToString();
                    }

                    if ((bool)ds.Tables[0].Rows[0]["telephoneInterview"] == true)
                    {
                        chkbxOspActions.Items[0].Selected = true;
                    }

                    if ((bool)ds.Tables[0].Rows[0]["siteVisit"] == true)
                    {
                        chkbxOspActions.Items[1].Selected = true;
                    }

                    if ((bool)ds.Tables[0].Rows[0]["complaintInvestigation"] == true)
                    {
                        chkbxOspActions.Items[2].Selected = true;
                    }

                    if ((bool)ds.Tables[0].Rows[0]["hearingRescheduled"] == true)
                    {
                        chkbxOspActions.Items[3].Selected = true;
                    }

                    if ((bool)ds.Tables[0].Rows[0]["appealUpheld"] == true)
                    {
                        radbutAppeal.Items[0].Selected = true;
                    }

                    if ((bool)ds.Tables[0].Rows[0]["appealOverturned"] == true)
                    {
                        radbutAppeal.Items[1].Selected = true;
                    }

                    if (ds.Tables[0].Rows[0]["status"] != null)
                    {
                        radbutStatus.SelectedValue = ds.Tables[0].Rows[0]["status"].ToString();
                    }

                    if (ds.Tables[0].Rows[0]["otherOSPActions"] != null && ds.Tables[0].Rows[0]["otherOSPActions"].ToString() != "")
                    {
                        chkbxOspActions.Items[4].Selected = true;
                        chkbxOspActions_SelectedIndexChanged(chkbxOspActions, EventArgs.Empty);
                        txtOSPNotes.Text = ds.Tables[0].Rows[0]["otherOSPActions"].ToString();
                    }
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ForeColor = Color.Black;
                    row.ToolTip = "Click to select this row.";
                }
            }
        }

        protected void chkbxVendorActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkbxVendorActions.Items[chkbxVendorActions.Items.Count - 1].Selected == true)
            {
                divVANotes.Attributes.Add("style", "display:block");
            }
            else
            {
                divVANotes.Attributes.Add("style", "display:none");
            }
        }

        protected void chkbxOspActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkbxOspActions.Items[chkbxOspActions.Items.Count - 1].Selected == true)
            {
                divOSPNotes.Attributes.Add("style", "display:block");
            }
            else
            {
                divOSPNotes.Attributes.Add("style", "display:none");
            }
        }

        protected void btnSaveProceed_Click(object sender, EventArgs e)
        {
            Save();
            if (Request.QueryString["EditMode"] == null)
            {
                Response.Redirect("vmpUsrVendor3.aspx?VId=" + Request.QueryString["VId"]);
            }
            else
            {
                Response.Redirect("vmpUsrVendor3.aspx?VId=" + Request.QueryString["VId"] + "&EditMode=True");
            }
        }

        //This method is called to save all the values entered in this page.
        private string Save()
        {
            string status = "False";
            if (hdnFieldLogActionId.Value == string.Empty)
            {
                Boolean vAction = chkbxVendorActions.Items.Cast<ListItem>().Any(i => i.Selected);
                Boolean ospAction = chkbxOspActions.Items.Cast<ListItem>().Any(i => i.Selected);
                Boolean ospAppeal = radbutAppeal.Items.Cast<ListItem>().Any(i => i.Selected);
                Boolean ospStatus = radbutStatus.Items.Cast<ListItem>().Any(i => i.Selected);
                if (vAction || ospAction || ospAppeal || ospStatus)
                {
                    string qur = dbLibrary.idBuildQuery("proc_VendorDetails2", Request.QueryString["VId"].ToString(), chkbxVendorActions.Items[0].Selected == true ? "1" : "0", chkbxVendorActions.Items[1].Selected == true ? "1" : "0", chkbxVendorActions.Items[2].Selected == true ? "1" : "0", chkbxVendorActions.Items[3].Selected == true ? "1" : "0", chkbxVendorActions.Items[4].Selected == true ? "1" : "0", txtVANotes.Text.Trim(), chkbxOspActions.Items[0].Selected == true ? "1" : "0", chkbxOspActions.Items[1].Selected == true ? "1" : "0", chkbxOspActions.Items[2].Selected == true ? "1" : "0", chkbxOspActions.Items[3].Selected == true ? "1" : "0", txtOSPNotes.Text.Trim(), radbutAppeal.Items[0].Selected == true ? "1" : "0", radbutAppeal.Items[1].Selected == true ? "1" : "0", radbutStatus.SelectedValue, Session["UserId"].ToString());
                    dbLibrary.idExecute(qur);
                    loadActionDates();
                    status = "Success";
                }
                else
                {
                    status = "Empty";
                }
            }
            return status;

        }

        protected void grdActionDates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdActionDates, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (chkbxVendorActions.SelectedIndex != -1)
            {
                chkbxVendorActions.SelectedIndex = -1;
            }
            if (chkbxOspActions.SelectedIndex != -1)
            {
                chkbxOspActions.SelectedIndex = -1;
            }
            if (radbutAppeal.SelectedIndex != -1)
            {
                radbutAppeal.SelectedIndex = -1;
            }
            if (radbutStatus.SelectedIndex != -1)
            {
                radbutStatus.SelectedIndex = -1;
            }
            grdActionDates.SelectedIndex = -1;
            grdActionDates_SelectedIndexChanged(grdActionDates, EventArgs.Empty);
            chkbxOspActions_SelectedIndexChanged(sender, e);
            chkbxVendorActions_SelectedIndexChanged(sender, e);
            hdnFieldLogActionId.Value = string.Empty;
            txtOSPNotes.Text = string.Empty;
            txtVANotes.Text = string.Empty;
        }

        private void clearAllSelections()
        {
            if (chkbxVendorActions.SelectedIndex != -1)
            {
                chkbxVendorActions.SelectedIndex = -1;
            }
            if (chkbxOspActions.SelectedIndex != -1)
            {
                chkbxOspActions.SelectedIndex = -1;
            }
            if (radbutAppeal.SelectedIndex != -1)
            {
                radbutAppeal.SelectedIndex = -1;
            }
            if (radbutStatus.SelectedIndex != -1)
            {
                radbutStatus.SelectedIndex = -1;
            }
            chkbxOspActions_SelectedIndexChanged(chkbxOspActions, EventArgs.Empty);
            chkbxVendorActions_SelectedIndexChanged(chkbxVendorActions, EventArgs.Empty);
            txtOSPNotes.Text = string.Empty;
            txtVANotes.Text = string.Empty;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string status = Save();
            if (status == "False")
            {
                string message = "alert('Please Click New to Add New Log Action and then Save')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
           else if (status == "Empty")
            {
                string message = "alert('All Log Actions Cannot Be Empty')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }

        protected void lnkGoToStatus_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["VId"] != null)
            {
                Response.Redirect("vmpUsrStatus.aspx?VId=" + Request.QueryString["VId"]);
            }
        }

        protected void lnkAttachments_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["VId"] != null)
            {
                string qur = "Select * from AttachmentInfo where vendorId=" + Request.QueryString["VId"];
                DataSet ds = dbLibrary.idGetCustomResult(qur);
                ViewState["gridAttachments"] = ds;
                grdAttachments.DataSource = ds;
                grdAttachments.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalAttachments();", true);
        }

        protected void grdAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                HyperLink lnkAttachment = e.Row.FindControl("lnkAttachment") as HyperLink;
                int n = drv["attachmentPath"].ToString().Split('\\').Length;
                string url = drv["attachmentPath"].ToString().Split('\\')[n - 4] + "/" + drv["attachmentPath"].ToString().Split('\\')[n - 3] + "/" + drv["attachmentPath"].ToString().Split('\\')[n - 2] + "/" + drv["attachmentPath"].ToString().Split('\\')[n - 1];
                //string url = drv["filePath"].ToString();
                lnkAttachment.Text = Path.GetFileNameWithoutExtension(drv["attachmentPath"].ToString());
                lnkAttachment.NavigateUrl = url;
            }
        }

        protected void btnUploadAttachment_Click(object sender, EventArgs e)
        {
            string qur = "select vendorName from vendorDetails where vendorId=" + Request.QueryString["VId"].ToString();
            string vendorname = dbLibrary.idGetAFieldByQuery(qur);
            string vendorid = Request.QueryString["VId"].ToString();
            vendorname = vendorname.Replace('.', ' ').ToString().Replace(',', ' ').ToString();
            vendorname = Regex.Replace(vendorname, @"[^0-9a-zA-Z\s]+", "");
            if (vendorname.Length > 20)
            {
                vendorname = vendorname.Substring(0, 20);
            }
            vendorname = vendorname.Trim();
            if (!(Directory.Exists(WebConfigurationManager.AppSettings["VendorAttachmentPath"])))
            {
                Directory.CreateDirectory(WebConfigurationManager.AppSettings["VendorAttachmentPath"]);
            }
            if (!(Directory.Exists(WebConfigurationManager.AppSettings["VendorAttachmentPath"] + vendorname + "-" + vendorid)))
            {
                Directory.CreateDirectory(WebConfigurationManager.AppSettings["VendorAttachmentPath"] + vendorname + "-" + vendorid);
            }
            string vendorAttachmentPath = WebConfigurationManager.AppSettings["VendorAttachmentPath"] + vendorname + "-" + vendorid + "\\" + "Attachments";
            try
            {
                if (!Directory.Exists(vendorAttachmentPath))
                {
                    Directory.CreateDirectory(vendorAttachmentPath);
                }
                if (FileUploadAttachment.HasFile)
                {
                    FileUploadAttachment.SaveAs(vendorAttachmentPath + "\\" + FileUploadAttachment.FileName);
                    dbLibrary.idInsertInto("AttachmentInfo",
                       "vendorId", vendorid,
                       "attachmentPath", vendorAttachmentPath + "\\" + FileUploadAttachment.FileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction1", "runEffect1()", true);
        }

        protected void grdAttachments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAttachments.PageIndex = e.NewPageIndex;
            grdAttachments.DataSource = (DataSet)ViewState["gridAttachments"];
            grdAttachments.DataBind();
        }
    }
}