using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VMP.Library;
using VMP_1._0.Library;
using System.IO;
using System.Web.Configuration;
using System.Text.RegularExpressions;

namespace VMP_1._0.User
{
    public partial class vmpUsrVendor3 : System.Web.UI.Page
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
                    else if (!dbLibrary.isValidVendor(Request.QueryString["VId"]))
                    {
                        Response.Redirect("vmpUsrVendor1.aspx");
                    }
                    loadStepsProgress();
                    repeaterDatabind();
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

        // Set Step 1, 2, 3 & 4 CSS Classes based on completion 
        private void loadStepsProgress()
        {
            string qur = dbLibrary.idBuildQuery("proc_getNewVendorProgress", Request.QueryString["VId"].ToString());
            DataSet ds = dbLibrary.idGetCustomResult(qur);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if ((bool)ds.Tables[0].Rows[0]["step4"] == true)
                {
                    divStep1.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                    divStep2.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                    divStep4.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                    divStep3.Attributes.Add("class", "col-xs-3 bs-wizard-step active activecomplete");
                    hdnFourthPageComplete.Value = "True";
                }
                else
                {
                    divStep4.Attributes.Add("class", "col-xs-3 bs-wizard-step disabled");
                    if ((bool)ds.Tables[0].Rows[0]["step3"] == true)
                    {
                        divStep1.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                        divStep2.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                        divStep3.Attributes.Add("class", "col-xs-3 bs-wizard-step active activecomplete");
                    }
                    else
                    {
                        divStep1.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                        divStep2.Attributes.Add("class", "col-xs-3 bs-wizard-step complete");
                        divStep3.Attributes.Add("class", "col-xs-3 bs-wizard-step active");
                    }
                }

            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblVendorHeading.Text = ds.Tables[1].Rows[0]["vendorName"].ToString();
            }
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

        protected void lnkStep2_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["EditMode"] == null)
            {
                Response.Redirect("vmpUsrVendor2.aspx?VId=" + Request.QueryString["VId"]);
            }
            else
            {
                Response.Redirect("vmpUsrVendor2.aspx?VId=" + Request.QueryString["VId"] + "&EditMode=True");
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            save(sender);
        }

        //Save all values while adding a new owner
        private void save(object sender)
        {
            DropDownList ddlTitle = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("ddlTitle") as DropDownList;
            TextBox txtTitle = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtTitle") as TextBox;
            TextBox txtFName = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtFName") as TextBox;
            TextBox txtMName = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtMName") as TextBox;
            TextBox txtLName = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtLName") as TextBox;
            TextBox txtAddress = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtAddress") as TextBox;
            TextBox txtCity = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtCity") as TextBox;
            DropDownList ddlState = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("ddlState") as DropDownList;
            TextBox txtZip = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtZip") as TextBox;
            TextBox txtPhone = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtPhone") as TextBox;
            TextBox txtCellPhone = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtCellPhone") as TextBox;
            TextBox txtOtherPhone = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtOtherPhone") as TextBox;
            RadioButton radbutSSYes = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("radbutSSYes") as RadioButton;
            RadioButton radbutSAYes = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("radbutSAYes") as RadioButton;
            TextBox txtPOwnership = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtPOwnership") as TextBox;
            TextBox txtSharesOwned = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtSharesOwned") as TextBox;

            CheckBox chkCitizenFoot = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkCitizenFoot") as CheckBox;
            CheckBoxList chkbxTargetGroupFoot = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkbxTargetGroupFoot") as CheckBoxList;
            RadioButtonList radVeteranFoot = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("radVeteranFoot") as RadioButtonList;
            CheckBoxList chkIndiginessFoot = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkIndiginessFoot") as CheckBoxList;
            TextBox txtTribalIDFoot = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtTribalIDFoot") as TextBox;
            CheckBoxList chkTG1Foot = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkTG1Foot") as CheckBoxList;
            TextBox txtCSalary = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtCSalary") as TextBox;
            TextBox txtEducation = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtEducation") as TextBox;
            CheckBox chkCEO = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkCEO") as CheckBox;
            CheckBox chkChairman = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkChairman") as CheckBox;
            CheckBox chkPresident = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkPresident") as CheckBox;
            CheckBox chkVisePresident = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkVisePresident") as CheckBox;
            CheckBox chkKeyPersonnel = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkKeyPersonnel") as CheckBox;
            CheckBox chkPartner = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkPartner") as CheckBox;
            CheckBox chkOther = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkOther") as CheckBox;
            TextBox txtOtherPosition = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtOtherPosition") as TextBox;
            RadioButtonList radbutYE = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("radbutYE") as RadioButtonList;
            CheckBoxList chkbxRE = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkbxRE") as CheckBoxList;
            CheckBoxList chkbxCR = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkbxCR") as CheckBoxList;


            //Personal Networth
            TextBox txtYearFoot = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtYearFoot") as TextBox;
            TextBox txtTliabilitiesFoot = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtTliabilitiesFoot") as TextBox;
            TextBox txtCAssetFoot = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtCAssetFoot") as TextBox;
            TextBox txtPNetWorthFoot = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("txtPNetWorthFoot") as TextBox;

            if (txtFName.Text != "" || txtLName.Text != "")
            {
                string qur = dbLibrary.idBuildQuery("proc_VendorDetails3", "Insert", "", ddlTitle.SelectedValue, txtFName.Text, txtMName.Text, txtLName.Text, Request.QueryString["VId"].ToString(), txtAddress.Text, txtCity.Text, ddlState.SelectedValue, txtZip.Text,
                txtPhone.Text, txtCellPhone.Text, txtOtherPhone.Text, radbutSSYes.Checked == true ? "1" : "0", radbutSAYes.Checked == true ? "1" : "0", txtPOwnership.Text == "" ? "0" : txtPOwnership.Text, txtSharesOwned.Text == "" ? "0" : txtSharesOwned.Text,
                chkCitizenFoot.Checked == true ? "1" : "0", chkbxTargetGroupFoot.Items[0].Selected.ToString(), chkbxTargetGroupFoot.Items[1].Selected.ToString(), chkbxTargetGroupFoot.Items[2].Selected.ToString(), chkbxTargetGroupFoot.Items[3].Selected.ToString(), chkIndiginessFoot.Items[0].Selected.ToString(), chkIndiginessFoot.Items[1].Selected.ToString(), chkIndiginessFoot.Items[2].Selected.ToString(), txtTribalIDFoot.Text.Trim(),
                chkTG1Foot.Items[0].Selected.ToString(), chkTG1Foot.Items[1].Selected.ToString(), radVeteranFoot.Items[0].Selected.ToString(), radVeteranFoot.Items[1].Selected.ToString(), txtCSalary.Text.Trim().Replace(",", ""), txtEducation.Text.Trim(), chkCEO.Checked.ToString(), chkChairman.Checked.ToString(), chkPresident.Checked.ToString(), chkVisePresident.Checked.ToString(), chkKeyPersonnel.Checked.ToString(), chkPartner.Checked.ToString(), chkOther.Checked == true ? txtOtherPosition.Text.Trim() : "",
                radbutYE.SelectedValue, chkbxRE.Items[0].Selected.ToString(), chkbxRE.Items[1].Selected.ToString(), chkbxRE.Items[2].Selected.ToString(), chkbxRE.Items[3].Selected.ToString(), chkbxRE.Items[4].Selected.ToString(), chkbxRE.Items[5].Selected.ToString(),
                chkbxCR.Items[0].Selected.ToString(), chkbxCR.Items[1].Selected.ToString(), chkbxCR.Items[2].Selected.ToString(), chkbxCR.Items[3].Selected.ToString(), chkbxCR.Items[4].Selected.ToString(), chkbxCR.Items[5].Selected.ToString(),
                "True", txtYearFoot.Text, txtCAssetFoot.Text == "" ? "0" : txtCAssetFoot.Text.Replace(",", ""), txtTliabilitiesFoot.Text == "" ? "0" : txtTliabilitiesFoot.Text.Replace(",", ""), txtPNetWorthFoot.Text == "" ? "0" : txtPNetWorthFoot.Text.Replace(",", ""), "", Session["UserId"].ToString());

                string value = dbLibrary.idGetAFieldByQuery(qur);
                // Commented the below - Rule need to be executed only after only on save of 3a page
                //if (hdnFourthPageComplete.Value == "True")
                //{
                //    ruleLibrary.executeRule(Request.QueryString["VId"], Session["UserId"].ToString());
                //}
                if (value == "0")
                {
                    string message = "alert('Percentage Ownership Cannot Be More Than 100%')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                else
                {
                    repeaterDatabind();
                }
            }
        }

        //Binding all the owners information to the repeater control to display it
        private void repeaterDatabind()
        {
            string qur = "select * from Owners where vendorID=" + Request.QueryString["VId"] + " order by isActive desc";
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            rptSecOwners.DataSource = ds;
            rptSecOwners.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["EditMode"] == null)
            {
                Response.Redirect("vmpUsrVendor2.aspx?VId=" + Request.QueryString["VId"]);
            }
            else
            {
                Response.Redirect("vmpUsrVendor2.aspx?VId=" + Request.QueryString["VId"] + "&EditMode=True");
            }
        }

        protected void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkOther = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("chkOther") as CheckBox;
            HtmlGenericControl divOtherPosition = rptSecOwners.Controls[rptSecOwners.Controls.Count - 1].Controls[0].FindControl("divOtherPosition") as HtmlGenericControl;
            if (chkOther.Checked)
            {
                divOtherPosition.Attributes.Add("style", "display:block");
            }
            else
            {
                divOtherPosition.Attributes.Add("style", "display:none");
            }
        }

        protected void rptSecOwners_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                DropDownList ddlTitle = (DropDownList)e.Item.FindControl("ddlTitle");
                ddlTitle.SelectedValue = drv.Row["title"].ToString();
                RadioButton radbutSSYes = (RadioButton)e.Item.FindControl("radbutSSYes");
                RadioButton radbutSSNo = (RadioButton)e.Item.FindControl("radbutSSNo");
                if ((bool)drv.Row["sharesSigned"])
                {
                    radbutSSYes.Checked = true;
                    radbutSSNo.Checked = false;
                }
                RadioButton radbutSAYes = (RadioButton)e.Item.FindControl("radbutSAYes");
                RadioButton radbutSANo = (RadioButton)e.Item.FindControl("radbutSANo");
                if ((bool)drv.Row["applicationSigned"])
                {
                    radbutSAYes.Checked = true;
                    radbutSANo.Checked = false;
                }
                CheckBox chkCitizen = (CheckBox)e.Item.FindControl("chkCitizen");
                CheckBoxList chkbxTargetGroup = (CheckBoxList)e.Item.FindControl("chkbxTargetGroup");
                RadioButtonList radVeteran = (RadioButtonList)e.Item.FindControl("radVeteran");
                CheckBoxList chkIndiginess = (CheckBoxList)e.Item.FindControl("chkIndiginess");
                CheckBoxList chkTG1 = (CheckBoxList)e.Item.FindControl("chkTG1");
                HtmlGenericControl divIndiginess = (HtmlGenericControl)e.Item.FindControl("divIndiginess");

                chkCitizen.Checked = (bool)drv.Row["citizen"];
                chkCitizen_CheckedChanged(chkCitizen, EventArgs.Empty);
                chkbxTargetGroup.Items[0].Selected = (bool)drv.Row["asian"];
                chkbxTargetGroup.Items[1].Selected = (bool)drv.Row["black"];
                chkbxTargetGroup.Items[2].Selected = (bool)drv.Row["hispanic"];
                chkbxTargetGroup.Items[3].Selected = (bool)drv.Row["indiginessAmerican"];

                if (chkbxTargetGroup.Items[3].Selected)
                {
                    divIndiginess.Attributes.Add("style", "display:block");
                }
                else
                {
                    divIndiginess.Attributes.Add("style", "display:none");
                }
                chkIndiginess.Items[0].Selected = (bool)drv.Row["alaskan"];
                chkIndiginess.Items[1].Selected = (bool)drv.Row["hawaiinNative"];
                chkIndiginess.Items[2].Selected = (bool)drv.Row["americanIndian"];


                chkTG1.Items[0].Selected = (bool)drv.Row["physicallyDisabled"];
                chkTG1.Items[1].Selected = (bool)drv.Row["woman"];

                radVeteran.Items[0].Selected = (bool)drv.Row["veteran"];
                radVeteran.Items[1].Selected = (bool)drv.Row["serviceDisabled"];
                if (!radVeteran.Items[0].Selected && !radVeteran.Items[1].Selected)
                {
                    radVeteran.Items[2].Selected = true;
                }


                if (chkbxTargetGroup.Items[0].Selected)
                {
                    chkbxTargetGroup_SelectedIndexChanged(chkbxTargetGroup, EventArgs.Empty);
                    // chkIndiginess_SelectedIndexChanged(chkIndiginess, EventArgs.Empty);
                }


                if (chkIndiginess.Items[2].Selected)
                {
                    HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("divTribal");
                    div.Attributes.Add("style", "display:block");
                    TextBox txtTribalID = (TextBox)e.Item.FindControl("txtTribalID");
                    txtTribalID.Text = drv.Row["tribalID"].ToString();
                }

                DropDownList ddlState = (DropDownList)e.Item.FindControl("ddlState");
                ddlState.SelectedValue = drv.Row["state"].ToString();

                CheckBox chkbxActive = (CheckBox)e.Item.FindControl("chkbxActive");
                HtmlGenericControl divActive = (HtmlGenericControl)e.Item.FindControl("divActive");
                if ((bool)drv.Row["isActive"])
                {

                    chkbxActive.Checked = true;
                    divActive.Attributes.Add("style", "border-style:outset");
                }
                else
                {
                    chkbxActive.Checked = false;
                    divActive.Attributes.Add("style", "border-style:inset");
                }

                CheckBox chkbxCEO = (CheckBox)e.Item.FindControl("chkbxCEO");
                CheckBox chkbxChairman = (CheckBox)e.Item.FindControl("chkbxChairman");
                CheckBox chkbxPresident = (CheckBox)e.Item.FindControl("chkbxPresident");
                CheckBox chkbxVisePresident = (CheckBox)e.Item.FindControl("chkbxVisePresident");
                CheckBox chkbxKeyPersonnel = (CheckBox)e.Item.FindControl("chkbxKeyPersonnel");
                CheckBox chkbxPartner = (CheckBox)e.Item.FindControl("chkbxPartner");
                CheckBox chkbxOther = (CheckBox)e.Item.FindControl("chkbxOther");

                chkbxCEO.Checked = (bool)drv.Row["isCEO"];
                chkbxChairman.Checked = (bool)drv.Row["isChairman"];
                chkbxPresident.Checked = (bool)drv.Row["isPresident"];
                chkbxVisePresident.Checked = (bool)drv.Row["isVicePresident"];
                chkbxKeyPersonnel.Checked = (bool)drv.Row["isKeyPersonnel"];
                chkbxPartner.Checked = (bool)drv.Row["isPartner"];
                if (drv.Row["otherPosition"].ToString().Length > 0)
                {
                    chkbxOther.Checked = true;
                }
                if (chkbxOther.Checked)
                {
                    chkbxOther_CheckedChanged(chkbxOther, EventArgs.Empty);
                }

                RadioButtonList radbutYE = (RadioButtonList)e.Item.FindControl("radbutYE");
                radbutYE.SelectedValue = drv.Row["yearsExp"].ToString();

                CheckBoxList chkbxRE = (CheckBoxList)e.Item.FindControl("chkbxRE");
                chkbxRE.Items[0].Selected = (bool)drv.Row["management"];
                chkbxRE.Items[1].Selected = (bool)drv.Row["marketing"];
                chkbxRE.Items[2].Selected = (bool)drv.Row["operations"];
                chkbxRE.Items[3].Selected = (bool)drv.Row["finance"];
                chkbxRE.Items[4].Selected = (bool)drv.Row["industrySkills"];
                chkbxRE.Items[5].Selected = (bool)drv.Row["industryExposure"];
                CheckBoxList chkbxCR = (CheckBoxList)e.Item.FindControl("chkbxCR");
                chkbxCR.Items[0].Selected = (bool)drv.Row["busPlanning"];
                chkbxCR.Items[1].Selected = (bool)drv.Row["salesMarket"];
                chkbxCR.Items[2].Selected = (bool)drv.Row["financial"];
                chkbxCR.Items[3].Selected = (bool)drv.Row["personnel"];
                chkbxCR.Items[4].Selected = (bool)drv.Row["projManagement"];
                chkbxCR.Items[5].Selected = (bool)drv.Row["anotherJob"];
                TextBox txtCSalary = (TextBox)e.Item.FindControl("txtCSalary");
                txtCSalary.Text = string.Format("{0:n0}", drv.Row["currentSalary"]);

                // Personal Networth
                TextBox txtYear = (TextBox)e.Item.FindControl("txtYear");
                TextBox txtTliabilities = (TextBox)e.Item.FindControl("txtTliabilities");
                TextBox txtCAsset = (TextBox)e.Item.FindControl("txtCAsset");
                TextBox txtPNetWorth = (TextBox)e.Item.FindControl("txtPNetWorth");
                HiddenField hdnFlag = (HiddenField)e.Item.FindControl("hdnFlag");
                string qur = "Select * from PersonalNetworth where OwnerId=" + drv.Row["ownerID"] + " and year=" + DateTime.Now.Year;
                DataSet ds = dbLibrary.idGetCustomResult(qur);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtYear.Text = ds.Tables[0].Rows[0]["year"].ToString();
                    txtTliabilities.Text = string.Format("{0:n0}", ds.Tables[0].Rows[0]["totalLiabilities"]);
                    txtCAsset.Text = string.Format("{0:n0}", ds.Tables[0].Rows[0]["currentAsset"]);
                    txtPNetWorth.Text = string.Format("{0:n0}", ds.Tables[0].Rows[0]["personalNetWorth"]);
                    if ((decimal.Parse(txtPNetWorth.Text)) > 1320000)
                    {
                        txtPNetWorth.Attributes.Add("style", "color:red;");
                    }
                    else
                    {
                        txtPNetWorth.Attributes.Add("style", "color:black;");
                    }
                    hdnFlag.Value = "1";
                }
                else
                {
                    hdnFlag.Value = "0";
                    txtYear.Text = DateTime.Now.Year.ToString();
                }

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                TextBox txtYearFoot = (TextBox)e.Item.FindControl("txtYearFoot");
                txtYearFoot.Text = DateTime.Now.Year.ToString();
            }
        }

        protected void chkbxOther_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            HtmlGenericControl divOtherPosition = ((Control)sender).Parent.FindControl("divOtherPositionIT") as HtmlGenericControl;
            if (checkBox.Checked)
            {
                divOtherPosition.Attributes.Add("style", "display:block");
            }
            else
            {
                divOtherPosition.Attributes.Add("style", "display:none");
            }
        }

        protected void rptSecOwners_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        //This button click will save all the updated details of the existing owners
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DropDownList ddlTitle = ((Control)sender).Parent.FindControl("ddlTitle") as DropDownList;
            TextBox txtTitle = ((Control)sender).Parent.FindControl("txtTitle") as TextBox;
            TextBox txtFName = ((Control)sender).Parent.FindControl("txtFName") as TextBox;
            TextBox txtMName = ((Control)sender).Parent.FindControl("txtMName") as TextBox;
            TextBox txtLName = ((Control)sender).Parent.FindControl("txtLName") as TextBox;
            TextBox txtAddress = ((Control)sender).Parent.FindControl("txtAddress") as TextBox;
            TextBox txtCity = ((Control)sender).Parent.FindControl("txtCity") as TextBox;
            DropDownList ddlState = ((Control)sender).Parent.FindControl("ddlState") as DropDownList;
            TextBox txtZip = ((Control)sender).Parent.FindControl("txtZip") as TextBox;
            TextBox txtPhone = ((Control)sender).Parent.FindControl("txtPhone") as TextBox;
            TextBox txtCellPhone = ((Control)sender).Parent.FindControl("txtCellPhone") as TextBox;
            TextBox txtOtherPhone = ((Control)sender).Parent.FindControl("txtOtherPhone") as TextBox;
            RadioButton radbutSSYes = ((Control)sender).Parent.FindControl("radbutSSYes") as RadioButton;
            RadioButton radbutSAYes = ((Control)sender).Parent.FindControl("radbutSAYes") as RadioButton;
            TextBox txtPOwnership = ((Control)sender).Parent.FindControl("txtPOwnership") as TextBox;
            TextBox txtSharesOwned = ((Control)sender).Parent.FindControl("txtSharesOwned") as TextBox;

            CheckBox chkCitizen = ((Control)sender).Parent.FindControl("chkCitizen") as CheckBox;
            CheckBoxList chkbxTargetGroup = ((Control)sender).Parent.FindControl("chkbxTargetGroup") as CheckBoxList;
            RadioButtonList radVeteran = ((Control)sender).Parent.FindControl("radVeteran") as RadioButtonList;
            CheckBoxList chkIndiginess = ((Control)sender).Parent.FindControl("chkIndiginess") as CheckBoxList;
            TextBox txtTribalID = ((Control)sender).Parent.FindControl("txtTribalID") as TextBox;
            CheckBoxList chkTG1 = ((Control)sender).Parent.FindControl("chkTG1") as CheckBoxList;

            TextBox txtCSalary = ((Control)sender).Parent.FindControl("txtCSalary") as TextBox;
            TextBox txtEducation = ((Control)sender).Parent.FindControl("txtEducation") as TextBox;
            CheckBox chkbxCEO = ((Control)sender).Parent.FindControl("chkbxCEO") as CheckBox;
            CheckBox chkbxChairman = ((Control)sender).Parent.FindControl("chkbxChairman") as CheckBox;
            CheckBox chkbxPresident = ((Control)sender).Parent.FindControl("chkbxPresident") as CheckBox;
            CheckBox chkbxVisePresident = ((Control)sender).Parent.FindControl("chkbxVisePresident") as CheckBox;
            CheckBox chkbxKeyPersonnel = ((Control)sender).Parent.FindControl("chkbxKeyPersonnel") as CheckBox;
            CheckBox chkbxPartner = ((Control)sender).Parent.FindControl("chkbxPartner") as CheckBox;
            CheckBox chkbxOther = ((Control)sender).Parent.FindControl("chkbxOther") as CheckBox;
            TextBox txtOtherPositionIT = ((Control)sender).Parent.FindControl("txtOtherPositionIT") as TextBox;
            RadioButtonList radbutYE = ((Control)sender).Parent.FindControl("radbutYE") as RadioButtonList;
            CheckBoxList chkbxRE = ((Control)sender).Parent.FindControl("chkbxRE") as CheckBoxList;
            CheckBoxList chkbxCR = ((Control)sender).Parent.FindControl("chkbxCR") as CheckBoxList;
            CheckBox chkbxActive = ((Control)sender).Parent.FindControl("chkbxActive") as CheckBox;

            //Personal Networth
            TextBox txtYear = ((Control)sender).Parent.FindControl("txtYear") as TextBox;
            TextBox txtTliabilities = ((Control)sender).Parent.FindControl("txtTliabilities") as TextBox;
            TextBox txtCAsset = ((Control)sender).Parent.FindControl("txtCAsset") as TextBox;
            TextBox txtPNetWorth = ((Control)sender).Parent.FindControl("txtPNetWorth") as TextBox;
            HiddenField hdnFlag = ((Control)sender).Parent.FindControl("hdnFlag") as HiddenField;

            Button btnUpdate = (Button)sender;

            if (txtFName.Text != "" || txtLName.Text != "")
            {
                string qur = dbLibrary.idBuildQuery("proc_VendorDetails3", "Update", btnUpdate.CommandArgument.ToString(), ddlTitle.SelectedValue, txtFName.Text, txtMName.Text, txtLName.Text, Request.QueryString["VId"].ToString(), txtAddress.Text, txtCity.Text, ddlState.SelectedValue, txtZip.Text,
            txtPhone.Text, txtCellPhone.Text, txtOtherPhone.Text, radbutSSYes.Checked == true ? "1" : "0", radbutSAYes.Checked == true ? "1" : "0", txtPOwnership.Text == "" ? "0" : txtPOwnership.Text, txtSharesOwned.Text == "" ? "0" : txtSharesOwned.Text,
            chkCitizen.Checked == true ? "1" : "0", chkbxTargetGroup.Items[0].Selected.ToString(), chkbxTargetGroup.Items[1].Selected.ToString(), chkbxTargetGroup.Items[2].Selected.ToString(), chkbxTargetGroup.Items[3].Selected.ToString(), chkIndiginess.Items[0].Selected.ToString(), chkIndiginess.Items[1].Selected.ToString(), chkIndiginess.Items[2].Selected.ToString(), txtTribalID.Text.Trim(),
            chkTG1.Items[0].Selected.ToString(), chkTG1.Items[1].Selected.ToString(), radVeteran.Items[0].Selected.ToString(), radVeteran.Items[1].Selected.ToString(),
            txtCSalary.Text.Trim().Replace(",", ""), txtEducation.Text.Trim(), chkbxCEO.Checked.ToString(), chkbxChairman.Checked.ToString(), chkbxPresident.Checked.ToString(), chkbxVisePresident.Checked.ToString(), chkbxKeyPersonnel.Checked.ToString(), chkbxPartner.Checked.ToString(), chkbxOther.Checked == true ? txtOtherPositionIT.Text.Trim() : "",
            radbutYE.SelectedValue, chkbxRE.Items[0].Selected.ToString(), chkbxRE.Items[1].Selected.ToString(), chkbxRE.Items[2].Selected.ToString(), chkbxRE.Items[3].Selected.ToString(), chkbxRE.Items[4].Selected.ToString(), chkbxRE.Items[5].Selected.ToString(),
            chkbxCR.Items[0].Selected.ToString(), chkbxCR.Items[1].Selected.ToString(), chkbxCR.Items[2].Selected.ToString(), chkbxCR.Items[3].Selected.ToString(), chkbxCR.Items[4].Selected.ToString(), chkbxCR.Items[5].Selected.ToString(),
            chkbxActive.Checked == true ? "1" : "0", txtYear.Text, txtCAsset.Text == "" ? "0" : txtCAsset.Text.Replace(",", ""), txtTliabilities.Text == "" ? "0" : txtTliabilities.Text.Replace(",", ""), txtPNetWorth.Text == "" ? "0" : txtPNetWorth.Text.Replace(",", ""), hdnFlag.Value, Session["UserId"].ToString());

                string value = dbLibrary.idGetAFieldByQuery(qur);
                if (value == "0")
                {
                    string message = "alert('Total Percentage Ownership Cannot Be More Than 100%')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                else
                {
                    repeaterDatabind();
                    Label1.Text = "Owner Details Updated Successfully";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction1", "runEffect1()", true);
                }
                // Commented the below - Rule need to be executed only after only on save of 3a page
                //if (hdnFourthPageComplete.Value == "True")
                //{
                //    ruleLibrary.executeRule(Request.QueryString["VId"], Session["UserId"].ToString());
                //}
            }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            save(sender);
            if (Request.QueryString["EditMode"] == null)
            {
                Response.Redirect("vmpUsrVendor3a.aspx?VId=" + Request.QueryString["VId"]);
            }
            else
            {
                Response.Redirect("vmpUsrVendor3a.aspx?VId=" + Request.QueryString["VId"] + "&EditMode=True");
            }
        }

        protected void chkbxTargetGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList chkbxTargetGroup = ((Control)sender).Parent.FindControl("chkbxTargetGroup") as CheckBoxList;
            CheckBoxList chkIndiginess = ((Control)sender).Parent.FindControl("chkIndiginess") as CheckBoxList;
            HtmlGenericControl divIndiginess = ((Control)sender).Parent.FindControl("divIndiginess") as HtmlGenericControl;
            HtmlGenericControl divTribal = ((Control)sender).Parent.FindControl("divTribal") as HtmlGenericControl;
            TextBox txtTribal = ((Control)sender).Parent.FindControl("txtTribalID") as TextBox;
            if (chkbxTargetGroup.Items[3].Selected)
            {
                divIndiginess.Attributes.Add("style", "display:block");
            }
            else
            {
                chkIndiginess.SelectedIndex = -1;
                chkIndiginess_SelectedIndexChanged(chkIndiginess, EventArgs.Empty);
                divIndiginess.Attributes.Add("style", "display:none");
            }
            if (chkIndiginess.Items[2].Selected)
            {
                divTribal.Attributes.Add("style", "display:block");
            }
            else
            {
                divTribal.Attributes.Add("style", "display:none");
                txtTribal.Text = "";

            }
        }

        protected void chkIndiginess_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBoxList chkIndiginess = ((Control)sender).Parent.FindControl("chkIndiginess") as CheckBoxList;
            TextBox txtTribalID = ((Control)sender).Parent.FindControl("txtTribalID") as TextBox;
            HtmlGenericControl div = ((Control)sender).Parent.FindControl("divTribal") as HtmlGenericControl;
            if (chkIndiginess.Items[2].Selected)
            {
                div.Attributes.Add("style", "display:block");
            }
            else
            {
                div.Attributes.Add("style", "display:none");
                txtTribalID.Text = "";
            }
        }

        protected void chkCitizen_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkCitizen = ((Control)sender).Parent.FindControl("chkCitizen") as CheckBox;
            CheckBoxList chkbxTargetGroup = ((Control)sender).Parent.FindControl("chkbxTargetGroup") as CheckBoxList;
            CheckBoxList chkTG1 = ((Control)sender).Parent.FindControl("chkTG1") as CheckBoxList;
            RadioButtonList radVeteran = ((Control)sender).Parent.FindControl("radVeteran") as RadioButtonList;
            CheckBoxList chkIndiginess = ((Control)sender).Parent.FindControl("chkIndiginess") as CheckBoxList;
            HtmlGenericControl div = ((Control)sender).Parent.FindControl("divIndiginess") as HtmlGenericControl;
            HtmlGenericControl divTribal = ((Control)sender).Parent.FindControl("divTribal") as HtmlGenericControl;
            TextBox txtTribalID = ((Control)sender).Parent.FindControl("txtTribalID") as TextBox;
            if (chkCitizen.Checked == true)
            {
                chkbxTargetGroup.Items[0].Enabled = true;
                chkbxTargetGroup.Items[1].Enabled = true;
                chkbxTargetGroup.Items[2].Enabled = true;
                chkbxTargetGroup.Items[3].Enabled = true;
                chkIndiginess.Items[0].Enabled = true;
                chkIndiginess.Items[1].Enabled = true;
                chkIndiginess.Items[2].Enabled = true;
                chkTG1.Items[0].Enabled = true;
                chkTG1.Items[1].Enabled = true;
                radVeteran.Items[0].Enabled = true;
                radVeteran.Items[1].Enabled = true;
                radVeteran.Items[2].Enabled = true;
                if (chkIndiginess.Items[2].Selected)
                {
                    divTribal.Attributes.Add("style", "display:block");
                }
                else
                {
                    divTribal.Attributes.Add("style", "display:none");
                    txtTribalID.Text = "";

                }
            }
            else
            {
                chkbxTargetGroup.SelectedIndex = -1;
                chkIndiginess.SelectedIndex = -1;
                chkTG1.SelectedIndex = -1;
                radVeteran.SelectedIndex = -1;
                chkbxTargetGroup.Items[0].Enabled = false;
                chkbxTargetGroup.Items[1].Enabled = false;
                chkbxTargetGroup.Items[2].Enabled = false;
                chkbxTargetGroup.Items[3].Enabled = false;
                chkIndiginess.Items[0].Enabled = false;
                chkIndiginess.Items[1].Enabled = false;
                chkIndiginess.Items[2].Enabled = false;
                chkTG1.Items[0].Enabled = false;
                chkTG1.Items[1].Enabled = false;
                radVeteran.Items[0].Enabled = false;
                radVeteran.Items[1].Enabled = false;
                radVeteran.Items[2].Enabled = false;
                if (chkIndiginess.Items[2].Selected)
                {
                    divTribal.Attributes.Add("style", "display:block");
                }
                else
                {
                    divTribal.Attributes.Add("style", "display:none");
                    txtTribalID.Text = "";
                }
            }
        }

        protected void chkIndiginessFoot_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CheckBoxList chkIndiginessFoot = ((Control)sender).Parent.FindControl("chkIndiginessFoot") as CheckBoxList;
            TextBox txtTribalIDFoot = ((Control)sender).Parent.FindControl("txtTribalIDFoot") as TextBox;
            HtmlGenericControl div = ((Control)sender).Parent.FindControl("divTribalFoot") as HtmlGenericControl;
            if (chkIndiginessFoot.Items[2].Selected)
            {
                div.Attributes.Add("style", "display:block");
            }
            else
            {
                div.Attributes.Add("style", "display:none");
                txtTribalIDFoot.Text = "";
            }
        }

        protected void chkbxTargetGroupFoot_SelectedIndexChanged1(object sender, EventArgs e)
        {
            CheckBoxList chkbxTargetGroupFoot = ((Control)sender).Parent.FindControl("chkbxTargetGroupFoot") as CheckBoxList;
            CheckBoxList chkIndiginessFoot = ((Control)sender).Parent.FindControl("chkIndiginessFoot") as CheckBoxList;
            HtmlGenericControl divIndiginessFoot = ((Control)sender).Parent.FindControl("divIndiginessFoot") as HtmlGenericControl;
            if (chkbxTargetGroupFoot.Items[3].Selected)
            {

                divIndiginessFoot.Attributes.Add("style", "display:block");
            }
            else
            {
                chkIndiginessFoot.SelectedIndex = -1;
                chkIndiginessFoot_SelectedIndexChanged1(chkIndiginessFoot, EventArgs.Empty);
                divIndiginessFoot.Attributes.Add("style", "display:none");
            }
        }

        protected void chkCitizenFoot_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkCitizenFoot = ((Control)sender).Parent.FindControl("chkCitizenFoot") as CheckBox;
            CheckBoxList chkbxTargetGroupFoot = ((Control)sender).Parent.FindControl("chkbxTargetGroupFoot") as CheckBoxList;
            CheckBoxList chkTG1Foot = ((Control)sender).Parent.FindControl("chkTG1Foot") as CheckBoxList;
            RadioButtonList radVeteranFoot = ((Control)sender).Parent.FindControl("radVeteranFoot") as RadioButtonList;
            CheckBoxList chkIndiginessFoot = ((Control)sender).Parent.FindControl("chkIndiginessFoot") as CheckBoxList;
            if (chkCitizenFoot.Checked == true)
            {
                chkbxTargetGroupFoot.Items[0].Enabled = true;
                chkbxTargetGroupFoot.Items[1].Enabled = true;
                chkbxTargetGroupFoot.Items[2].Enabled = true;
                chkbxTargetGroupFoot.Items[3].Enabled = true;
                chkIndiginessFoot.Items[0].Enabled = true;
                chkIndiginessFoot.Items[1].Enabled = true;
                chkIndiginessFoot.Items[2].Enabled = true;
                chkTG1Foot.Items[0].Enabled = true;
                chkTG1Foot.Items[1].Enabled = true;
                radVeteranFoot.Items[0].Enabled = true;
                radVeteranFoot.Items[1].Enabled = true;
                radVeteranFoot.Items[2].Enabled = true;
            }
            else
            {
                chkbxTargetGroupFoot.SelectedIndex = -1;
                chkIndiginessFoot.SelectedIndex = -1;
                chkTG1Foot.SelectedIndex = -1;
                radVeteranFoot.SelectedIndex = -1;
                chkbxTargetGroupFoot.Items[0].Enabled = false;
                chkbxTargetGroupFoot.Items[1].Enabled = false;
                chkbxTargetGroupFoot.Items[2].Enabled = false;
                chkbxTargetGroupFoot.Items[3].Enabled = false;
                chkIndiginessFoot.Items[0].Enabled = false;
                chkIndiginessFoot.Items[1].Enabled = false;
                chkIndiginessFoot.Items[2].Enabled = false;
                chkTG1Foot.Items[0].Enabled = false;
                chkTG1Foot.Items[1].Enabled = false;
                radVeteranFoot.Items[0].Enabled = false;
                radVeteranFoot.Items[1].Enabled = false;
                radVeteranFoot.Items[2].Enabled = false;
            }
        }

        protected void txtTliabilitiesFoot_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCAssetFoot = ((Control)sender).Parent.FindControl("txtCAssetFoot") as TextBox;
            TextBox txtTliabilitiesFoot = ((Control)sender).Parent.FindControl("txtTliabilitiesFoot") as TextBox;
            TextBox txtPNetWorthFoot = ((Control)sender).Parent.FindControl("txtPNetWorthFoot") as TextBox;
            txtCAssetFoot.Text = txtCAssetFoot.Text.Trim() == "" ? "0" : txtCAssetFoot.Text;
            txtTliabilitiesFoot.Text = txtTliabilitiesFoot.Text.Trim() == "" ? "0" : txtTliabilitiesFoot.Text;
            if (txtCAssetFoot.Text.Length > 0 && txtTliabilitiesFoot.Text.Length > 0)
            {
                txtPNetWorthFoot.Text = (int.Parse(txtCAssetFoot.Text.Replace(",", "")) - int.Parse(txtTliabilitiesFoot.Text.Replace(",", ""))).ToString();
                txtPNetWorthFoot.Text = string.Format("{0:n0}", decimal.Parse(txtPNetWorthFoot.Text));
                if ((decimal.Parse(txtPNetWorthFoot.Text)) > 1320000)
                {
                    txtPNetWorthFoot.Attributes.Add("style", "color:red;");
                }
                else
                {
                    txtPNetWorthFoot.Attributes.Add("style", "color:black;");
                }
            }
        }

        protected void txtCAssetFoot_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCAssetFoot = ((Control)sender).Parent.FindControl("txtCAssetFoot") as TextBox;
            TextBox txtTliabilitiesFoot = ((Control)sender).Parent.FindControl("txtTliabilitiesFoot") as TextBox;
            TextBox txtPNetWorthFoot = ((Control)sender).Parent.FindControl("txtPNetWorthFoot") as TextBox;
            txtCAssetFoot.Text = txtCAssetFoot.Text.Trim() == "" ? "0" : txtCAssetFoot.Text;
            txtTliabilitiesFoot.Text = txtTliabilitiesFoot.Text.Trim() == "" ? "0" : txtTliabilitiesFoot.Text;
            if (txtCAssetFoot.Text.Length > 0 && txtTliabilitiesFoot.Text.Length > 0)
            {
                txtPNetWorthFoot.Text = (int.Parse(txtCAssetFoot.Text.Replace(",", "")) - int.Parse(txtTliabilitiesFoot.Text.Replace(",", ""))).ToString();
                txtPNetWorthFoot.Text = string.Format("{0:n0}", decimal.Parse(txtPNetWorthFoot.Text));
                if ((decimal.Parse(txtPNetWorthFoot.Text)) > 1320000)
                {
                    txtPNetWorthFoot.Attributes.Add("style", "color:red;");
                }
                else
                {
                    txtPNetWorthFoot.Attributes.Add("style", "color:black;");
                }
            }
        }

        protected void txtTliabilities_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCAsset = ((Control)sender).Parent.FindControl("txtCAsset") as TextBox;
            TextBox txtTliabilities = ((Control)sender).Parent.FindControl("txtTliabilities") as TextBox;
            TextBox txtPNetWorth = ((Control)sender).Parent.FindControl("txtPNetWorth") as TextBox;
            txtCAsset.Text = txtCAsset.Text.Trim() == "" ? "0" : txtCAsset.Text;
            txtTliabilities.Text = txtTliabilities.Text.Trim() == "" ? "0" : txtTliabilities.Text;
            if (txtCAsset.Text.Length > 0 && txtTliabilities.Text.Length > 0)
            {
                txtPNetWorth.Text = (int.Parse(txtCAsset.Text.Replace(",", "")) - int.Parse(txtTliabilities.Text.Replace(",", ""))).ToString();
                txtPNetWorth.Text = string.Format("{0:n0}", decimal.Parse(txtPNetWorth.Text));
                if ((decimal.Parse(txtPNetWorth.Text)) > 1320000)
                {
                    txtPNetWorth.Attributes.Add("style", "color:red;");
                }
                else
                {
                    txtPNetWorth.Attributes.Add("style", "color:black;");
                }
            }
        }

        protected void txtCAsset_TextChanged(object sender, EventArgs e)
        {
            TextBox txtCAsset = ((Control)sender).Parent.FindControl("txtCAsset") as TextBox;
            TextBox txtTliabilities = ((Control)sender).Parent.FindControl("txtTliabilities") as TextBox;
            TextBox txtPNetWorth = ((Control)sender).Parent.FindControl("txtPNetWorth") as TextBox;
            txtCAsset.Text = txtCAsset.Text.Trim() == "" ? "0" : txtCAsset.Text;
            txtTliabilities.Text = txtTliabilities.Text.Trim() == "" ? "0" : txtTliabilities.Text;
            if (txtCAsset.Text.Length > 0 && txtTliabilities.Text.Length > 0)
            {
                txtPNetWorth.Text = (int.Parse(txtCAsset.Text.Replace(",", "")) - int.Parse(txtTliabilities.Text.Replace(",", ""))).ToString();
                txtPNetWorth.Text = string.Format("{0:n0}", decimal.Parse(txtPNetWorth.Text));
                if ((decimal.Parse(txtPNetWorth.Text)) > 1320000)
                {
                    txtPNetWorth.Attributes.Add("style", "color:red;");
                }
                else
                {
                    txtPNetWorth.Attributes.Add("style", "color:black;");
                }
            }
        }

        protected void lnkHistory_Click(object sender, EventArgs e)
        {
            LinkButton lnkHistory = (LinkButton)sender;
            string id = lnkHistory.CommandArgument;
            GridView grdPNHistory = ((Control)sender).Parent.FindControl("grdPNHistory") as GridView;
            string qur = "Select year, Format(currentAsset, '##,##0') as currentAsset, Format(totalLiabilities, '##,##0') as totalLiabilities, Format(personalNetWorth, '##,##0') as personalNetWorth from PersonalNetworth where ownerId=" + id + " and year<>" + DateTime.Now.Year;
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            grdPNHistory.DataSource = ds;
            grdPNHistory.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal(" + id + ");", true);
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
            Label1.Text = "Document Uploaded Successfully";
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