//using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VMP.Library;
using VMP_1._0.Library;
using System.Net.Mail;
using System.Web.Configuration;
using System.Net.Mime;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using DocumentFormat.OpenXml;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Interop.Word;
using Spire.Doc.Documents;

namespace VMP_1._0.User
{
    public partial class vmpUsrStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                string appTypes = ruleLibrary.getApplicationType(Request.QueryString["VId"]);
                string qur = dbLibrary.idBuildQuery("proc_getStatusData", Request.QueryString["VId"]);
                DataSet ds = dbLibrary.idGetCustomResult(qur);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblVendorHeading.Text = ds.Tables[0].Rows[0]["vendorName"].ToString();
                    lblStatus.Text = ds.Tables[0].Rows[0]["status"].ToString();
                    lblDesignation.Text = ds.Tables[0].Rows[0]["finalClassification"].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    lblCategory.Text = ds.Tables[1].Rows[0]["supplierClassification"].ToString();
                    lblCategory.Attributes.Add("style", "color:black;");
                }
                else
                {
                    lblCategory.Text = "Missing Primary NAICS Code";
                    lblCategory.Attributes.Add("style", "color:red;");
                }
                if (lblStatus.Text != "Approved")
                {
                    if (appTypes != string.Empty)
                    {
                        if (appTypes.Contains(","))
                        {
                            lblAppType.Text = "Vendor might be eligible under other application type(s) - " + appTypes.Substring(appTypes.IndexOf(',') + 1);
                            divOtherAppType.Attributes.Add("style", "display:block;text-align:center");
                            divAppType.Attributes.Add("style", "display:block");
                            lblVendorAppType.Text = appTypes.Substring(0, appTypes.IndexOf(','));
                        }
                        else
                        {
                            divAppType.Attributes.Add("style", "display:block");
                            lblVendorAppType.Text = appTypes;
                        }
                    }
                    else
                    {
                        lblStatus.Text = "Denied";
                        lblAppType.Text = "Vendor is not eligible for any of the application types";
                        divOtherAppType.Attributes.Add("style", "display:block;text-align:center");
                        divAppType.Attributes.Add("style", "display:none");
                        // radbtnDeny.Checked = true;
                    }
                }
                if (lblStatus.Text == "Approved")
                {
                    lblStatus.Attributes.Add("style", "color:green");
                    // radbtnApprove.Checked = true;
                }
                else
                {
                    lblStatus.Attributes.Add("style", "color:red");
                }
                if (lblStatus.Text == "Approved")
                {
                    radbtnApprove.Checked = true;
                }
                else if (lblStatus.Text == "Denied")
                {
                    radbtnDeny.Checked = true;
                }
                else if (lblStatus.Text == "Pending")
                {
                    radbtnPending.Checked = true;
                }
                else if (lblStatus.Text == "Removed")
                {
                    radbtnRemove.Checked = true;
                }
            }
        }

        protected void btnRecommend_Click(object sender, EventArgs e)
        {
            txtOverrideReason.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (ddlLetters.SelectedIndex != 0)
            {
                divError.Attributes.Add("style", "display:none");
            }
            else
            {
                divError.Attributes.Add("style", "display:block");
                lblError.Text = "Select Any Letter to Download";
                return;
            }
            string qur = dbLibrary.idBuildQuery("[proc_getDataForLetter]", Request.QueryString["VId"].ToString(), Session["UserId"].ToString(), ddlLetters.SelectedValue);
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string vendorname = ds.Tables[0].Rows[0]["vendorName"].ToString().Replace('.', ' ').ToString().Replace(',', ' ').ToString();
                vendorname = Regex.Replace(vendorname, @"[^0-9a-zA-Z\s]+", "");
                if (vendorname.Length > 20)
                {
                    vendorname = vendorname.Substring(0, 20);
                }
                vendorname = vendorname.Trim();
                if (!(Directory.Exists(WebConfigurationManager.AppSettings["FilePath"])))
                {
                    Directory.CreateDirectory(WebConfigurationManager.AppSettings["FilePath"]);
                }
                if (!(Directory.Exists(WebConfigurationManager.AppSettings["FilePath"] + vendorname + "-" + Request.QueryString["VId"].ToString())))
                {
                    Directory.CreateDirectory(WebConfigurationManager.AppSettings["FilePath"] + vendorname + "-" + Request.QueryString["VId"].ToString());
                }
                string destFile = WebConfigurationManager.AppSettings["FilePath"] + vendorname + "-" + Request.QueryString["VId"].ToString() + "/" + vendorname + "_" + ddlLetters.SelectedValue + ".docx";
                string sourceFile = Server.MapPath(WebConfigurationManager.AppSettings["TemplatePath"] + ddlLetters.SelectedValue + ".docx");
                DirectoryInfo directory = new DirectoryInfo(WebConfigurationManager.AppSettings["FilePath"] + "/" + vendorname + "-" + Request.QueryString["VId"].ToString());
                foreach (FileInfo file in directory.GetFiles())
                {
                    if (file.Extension.ToLower().Equals(".docx"))
                    {
                        file.Delete();
                    }
                }
                File.Copy(sourceFile, destFile, true);
                object fileName = destFile;

                using (WordprocessingDocument doc =
           WordprocessingDocument.Open((string)fileName, true))
                {
                    DocumentFormat.OpenXml.Wordprocessing.Document mainDoc = doc.MainDocumentPart.Document;
                    try
                    {
                        ReplaceParagraphParts(false, mainDoc, "<Date>", DateTime.Now.ToString("MMMM dd, yyyy"));
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["title"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<Mr/Ms>", ds.Tables[0].Rows[0]["title"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ownerFirstName"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<OwnerFirstName>", ds.Tables[0].Rows[0]["ownerFirstName"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ownerLastName"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<OwnerLastName>", ds.Tables[0].Rows[0]["ownerLastName"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["vendorName"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<CompanyName>", ds.Tables[0].Rows[0]["vendorName"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CompanyAddress"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<Address>", ds.Tables[0].Rows[0]["CompanyAddress"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["city"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<City>", ds.Tables[0].Rows[0]["city"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["state"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<State>", ds.Tables[0].Rows[0]["state"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["zipCode"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<ZipCode>", ds.Tables[0].Rows[0]["zipCode"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["webAddress"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<CompanyWebsite>", ds.Tables[0].Rows[0]["webAddress"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["swiftNumber"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<VendorID#>", ds.Tables[0].Rows[0]["swiftNumber"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["fax"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<FaxNumber>", ds.Tables[0].Rows[0]["fax"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CompanyAddress"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<StreetAddress>", ds.Tables[0].Rows[0]["CompanyAddress"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["telephone"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<PhoneNumber>", ds.Tables[0].Rows[0]["telephone"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["productDesc"].ToString()))
                        {
                            string description = ds.Tables[0].Rows[0]["productDesc"].ToString();
                            if (description.Length > int.Parse(WebConfigurationManager.AppSettings["BusinessDescLength"]))
                            {
                                description = description.Substring(0, int.Parse(WebConfigurationManager.AppSettings["BusinessDescLength"])) + ".....Visit website for more information.";
                            }
                            ReplaceParagraphParts(false, mainDoc, "<BusinessDescription>", description);
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["naicCode"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<PNAICS>", ds.Tables[0].Rows[0]["naicCode"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["region"].ToString()))
                        {
                            ReplaceParagraphParts(false, mainDoc, "<CountyName>", ds.Tables[0].Rows[0]["region"].ToString());
                        }

                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["finalClassification"].ToString()))
                        {
                            switch (ds.Tables[0].Rows[0]["finalClassification"].ToString().Substring(0, 1))
                            {
                                case "A":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Asian/Pacific");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "A");
                                    break;
                                case "B":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Black");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "B");
                                    break;
                                case "H":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Hispanic");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "H");
                                    break;
                                case "I":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Indigenous American");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "I");
                                    break;
                                case "D":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Physical Disability");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "D");
                                    break;
                                case "W":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Woman");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "W");
                                    break;
                                case "R":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Rehabilitation Facilities");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "R");
                                    break;
                                case "V":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Veteran");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "V");
                                    break;
                                case "S":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Service Disabled Veteran");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "S");
                                    break;
                                case "L":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Labor Surplus County");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "L");
                                    break;
                                case "M":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "70% Median Income County");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "M");
                                    break;
                                case "T":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Targeted Neighborhood");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "T");
                                    break;
                                case "E":
                                    ReplaceParagraphParts(false, mainDoc, "<Designation>", "Enterprise Zone");
                                    ReplaceParagraphParts(false, mainDoc, "<DesignationLetter>", "E");
                                    break;
                            }
                        }
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["certDate"].ToString()))
                        {
                            DateTime date = DateTime.Parse(ds.Tables[0].Rows[0]["certDate"].ToString());
                            ReplaceParagraphParts(false, mainDoc, "<CertificationDate>", date.ToString("MMMM dd, yyyy"));
                            //Recert Date
                            ReplaceParagraphParts(false, mainDoc, "<RecertificationDate>", date.ToString("MMMM dd, yyyy"));
                        }
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["emailId"].ToString()))
                            {
                                ReplaceParagraphParts(false, mainDoc, "<VendorsEmailAddress>", ds.Tables[0].Rows[0]["emailId"].ToString());
                            }
                            if (ddlLetters.SelectedValue == "REMOVAL-Failure to Recert")
                            {
                                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["requestedDate"].ToString()))
                                {
                                    ReplaceParagraphParts(false, mainDoc, "<RequestForRecertificationDate>", DateTime.Parse(ds.Tables[0].Rows[0]["requestedDate"].ToString()).ToString("MMMM dd, yyyy"));
                                }
                            }
                            if (ddlLetters.SelectedValue == "SD FED REMOVAL" || ddlLetters.SelectedValue == "VO FED REMOVAL")
                            {
                                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["nextCertDate"].ToString()))
                                {
                                    ReplaceParagraphParts(false, mainDoc, "<CertificationExpirationDate>", DateTime.Parse(ds.Tables[0].Rows[0]["nextCertDate"].ToString()).ToString("MMMM dd, yyyy"));
                                }
                            }
                        }
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["SpecialistName"].ToString()))
                            {
                                ReplaceParagraphParts(false, mainDoc, "<VendorSpecialistName>", ds.Tables[1].Rows[0]["SpecialistName"].ToString());
                            }
                            if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["phoneNumber"].ToString()))
                            {
                                ReplaceParagraphParts(false, mainDoc, "<VendorSpecialistPhoneNumber>", ds.Tables[1].Rows[0]["phoneNumber"].ToString());
                            }


                        }
                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            string sNAICSCodes = string.Empty;

                            foreach (DataRow r in ds.Tables[3].Rows)
                            {
                                sNAICSCodes = sNAICSCodes + r["naicCode"].ToString() + ", ";
                            }
                            ReplaceParagraphParts(false, mainDoc, "<SNAICS>", sNAICSCodes.Trim().TrimEnd(','));
                        }
                        else
                        {
                            ReplaceParagraphParts(false, mainDoc, "<SNAICS>", string.Empty);
                        }
                        if (ddlLetters.SelectedValue == "RECERT REQ")
                        {
                            if (ds.Tables[5].Rows.Count > 0)
                            {
                                string owners = string.Empty;
                                foreach (DataRow dr in ds.Tables[5].Rows)
                                {
                                    owners = owners + "                                                                                                                                        " + dr["ownershipPercentage"].ToString() + " % " + dr["ownerFirstName"].ToString() + " " + dr["ownerLastName"].ToString().Trim() + Environment.NewLine;

                                }
                                ReplaceParagraphParts(false, mainDoc, "<OwnersList>", owners);
                            }
                        }
                        if (ddlLetters.SelectedValue == "OUT70" || ddlLetters.SelectedValue == "OUTLSA")
                        {
                            string date120 = DateTime.Today.AddDays(120).ToString("MMMM dd, yyyy");
                            string date119 = DateTime.Today.AddDays(119).ToString("MMMM dd, yyyy");
                            ReplaceParagraphParts(false, mainDoc, "<120DaysFromDateOfThisLetter>", date120);
                            ReplaceParagraphParts(false, mainDoc, "<119DaysFromDateOfThisLetter>", date119);
                        }


                        //Footer
                        foreach (FooterPart footerPart in doc.MainDocumentPart.FooterParts)
                        {
                            if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["phoneNumber"].ToString()))
                            {
                                ReplaceParagraphParts(true, footerPart.Footer, "<VendorSpecialistPhone>", ds.Tables[1].Rows[0]["phoneNumber"].ToString());
                            }
                        }
                        foreach (FooterPart footerPart in doc.MainDocumentPart.FooterParts)
                        {
                            if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["emailId"].ToString()))
                            {
                                ReplaceParagraphParts(true, footerPart.Footer, "<VendorSpecialistEmail>", ds.Tables[1].Rows[0]["emailId"].ToString());
                            }
                        }
                        replaceSEQCHARACTER(mainDoc);
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        mainDoc.Save();
                        doc.Close();
                        Response.Clear();
                        Response.ContentType = "Application/octet-stream";
                        Response.AddHeader("Content-Disposition", string.Format("attachment; filename = \"{0}\"", System.IO.Path.GetFileName(vendorname + "_" + ddlLetters.SelectedValue + ".docx")));
                        Response.TransmitFile(destFile);
                        Response.End();
                    }
                }
            }
        }

        private void replaceSEQCHARACTER(OpenXmlElement element)
        {
            Repeat:
            if (element.InnerText.Contains("SEQ CHAPTER"))
            {
                foreach (var paragraph in element.Descendants<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
                {
                    if (paragraph.InnerText.Contains("SEQ CHAPTER"))
                    {
                        Run newRun = new Run();
                        newRun.AppendChild(new Text(paragraph.InnerText.Remove(0, 20)));
                        paragraph.RemoveAllChildren<Run>();
                        //add the newly created run
                        paragraph.AppendChild(newRun);
                    }

                }
                goto Repeat;
            }
        }

        private static void ReplaceParagraphParts(bool isFooter, OpenXmlElement element, string oldText, string replacementText)
        {
            foreach (var paragraph in element.Descendants<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
            {
                Regex regexText = new Regex(oldText, RegexOptions.Compiled);
                Match match = regexText.Match(paragraph.InnerText);
                if (match.Success)
                {
                    //create a new run and set its value to the correct text
                    //this must be done before the child runs are removed otherwise
                    //paragraph.InnerText will be empty
                    Run newRun = new Run();
                    RunProperties runProperties = newRun.AppendChild(new RunProperties());
                    RunFonts runFont = new RunFonts();           // Create font
                    runFont.Ascii = WebConfigurationManager.AppSettings["LetterFontStyle"].ToString();                     // Specify font family
                    DocumentFormat.OpenXml.Wordprocessing.FontSize size = new DocumentFormat.OpenXml.Wordprocessing.FontSize();
                    if (isFooter)
                    {
                        size.Val = new StringValue(WebConfigurationManager.AppSettings["LetterFooterFont"].ToString());  // 48 half-point font size
                    }
                    else
                    {
                        size.Val = new StringValue(WebConfigurationManager.AppSettings["LetterBodyFont"].ToString());  // 48 half-point font size
                    }
                    runProperties.Append(runFont);
                    runProperties.Append(size);
                    newRun.AppendChild(new Text(paragraph.InnerText.Replace(match.Value, replacementText)));
                    //remove any child runs
                    paragraph.RemoveAllChildren<Run>();
                    //add the newly created run
                    paragraph.AppendChild(newRun);
                }
            }
        }
        protected void btnSavePreview_Click(object sender, EventArgs e)
        {
            if (ddlLetters.SelectedIndex != 0)
            {
                divError.Attributes.Add("style", "display:none");
            }
            else
            {
                divError.Attributes.Add("style", "display:block");
                lblError.Text = "Select Appropriate Letter";
                return;
            }
            if (fileUploader.HasFile)
            {
                string Extension = Path.GetExtension(fileUploader.PostedFile.FileName);
                if (!(Extension == ".docx" || Extension == ".doc"))
                {
                    divError.Attributes.Add("style", "display:block");
                    lblError.Text = "Invalid FileType!!Please Upload the Proper File";
                    return;
                }

                string qur = dbLibrary.idBuildQuery("[proc_getDataForLetter]", Request.QueryString["VId"].ToString(), Session["UserId"].ToString(), ddlLetters.SelectedValue);
                DataSet ds = dbLibrary.idGetCustomResult(qur);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string vendorname = ds.Tables[0].Rows[0]["vendorName"].ToString().Replace('.', ' ').ToString().Replace(',', ' ').ToString();
                    vendorname = Regex.Replace(vendorname, @"[^0-9a-zA-Z\s]+", "");
                    if (vendorname.Length > 20)
                    {
                        vendorname = vendorname.Substring(0, 20);
                    }
                    vendorname = vendorname.Trim();
                    if (!(Directory.Exists(WebConfigurationManager.AppSettings["FilePath"] + vendorname + "-" + Request.QueryString["VId"].ToString())))
                    {
                        Directory.CreateDirectory(WebConfigurationManager.AppSettings["FilePath"] + vendorname + "-" + Request.QueryString["VId"].ToString());
                    }
                    string destFile = WebConfigurationManager.AppSettings["FilePath"] + vendorname + "-" + Request.QueryString["VId"].ToString() + "\\" + ddlLetters.SelectedValue + ".docx";
                    divError.Attributes.Add("style", "display:none");
                    string extension = fileUploader.FileName.Split('.')[1];
                    if (extension == "docx")
                    {
                        DirectoryInfo directory = new DirectoryInfo(WebConfigurationManager.AppSettings["FilePath"] + "/" + vendorname + "-" + Request.QueryString["VId"].ToString());
                        foreach (FileInfo file in directory.GetFiles())
                        {
                            if (file.Extension.ToLower().Equals(".docx"))
                            {
                                file.Delete();
                            }
                        }
                        fileUploader.SaveAs(destFile);
                        object sourcepath = destFile;
                        try
                        {
                            object fileFormat = WdSaveFormat.wdFormatPDF;
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                string fileName = ds.Tables[2].Rows[0]["filePath"].ToString().Split('_')[1].ToString();
                                int count = int.Parse(fileName.Split('.')[0].ToString());
                                count++;
                                sourcepath = sourcepath.ToString().Replace(".docx", "_" + count + ".pdf");
                            }
                            else
                            {
                                sourcepath = sourcepath.ToString().Replace(".docx", "_1.pdf");
                            }
                            Spire.Doc.Document doc = new Spire.Doc.Document();
                            doc.LoadFromFile(destFile);
                            doc.SaveToFile(sourcepath.ToString(), Spire.Doc.FileFormat.PDF);
                           
                            string qry = dbLibrary.idBuildQuery("[proc_saveLetterInfo]", Request.QueryString["VId"].ToString(), Session["UserId"].ToString(), ddlLetters.SelectedValue, sourcepath.ToString());
                            btnSendLetter.CommandName = dbLibrary.idGetAFieldByQuery(qry);
                            btnSendLetter.CommandArgument = sourcepath.ToString();
                            int n = sourcepath.ToString().Split('\\').Length;
                            iframepdf.Src = sourcepath.ToString().Split('\\')[n - 3] + "/" + sourcepath.ToString().Split('\\')[n - 2] + "/" + sourcepath.ToString().Split('\\')[n - 1];
                        }
                        catch
                        {
                            throw;
                        }
                        finally
                        {
                            File.Delete(destFile);
                        }
                        txtSubject.Text = ds.Tables[0].Rows[0]["vendorName"].ToString() + " - " + lblStatus.Text;
                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            txtBody.Text = ds.Tables[4].Rows[0]["eMailBody"].ToString();

                        }
                        lblLetterMsg.Text = "Letter Generated Successfully!!!";
                        lblLetterMsg.Attributes.Add("style", "color:green");
                        iframepdf.Visible = true;
                        btnSavePreview.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "LetterPreview();", true);
                    }
                    else
                    {
                        divError.Attributes.Add("style", "display:block");
                        lblError.Text = "Invalid File!!Please Upload Only docx file";
                        return;
                    }
                }
            }
            else
            {
                divError.Attributes.Add("style", "display:block");
                lblError.Text = "Please Upload the File to Preview";
                return;
            }
        }
        protected void lnkRules_Click(object sender, EventArgs e)
        {
            string qur = "select description, status from   VendorRulesResult where vendorId=" + Request.QueryString["VId"];
            DataSet ds = dbLibrary.idGetCustomResult(qur);
            grdRulesView.DataSource = ds;
            grdRulesView.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalRulesView();", true);
        }

        protected void btnSendLetter_Click(object sender, EventArgs e)
        {
            try
            {
                string qur = dbLibrary.idBuildQuery("[proc_getDataToEmail]", Session["UserId"].ToString(), Request.QueryString["VId"].ToString());
                DataSet ds = dbLibrary.idGetCustomResult(qur);
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["emailId"].ToString()))
                {
                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(WebConfigurationManager.AppSettings["SmtpClient"]);
                    mail.From = new MailAddress(WebConfigurationManager.AppSettings["FromEmailAddress"]);
                    mail.To.Add(ds.Tables[0].Rows[0]["emailId"].ToString());
                    mail.To.Add(WebConfigurationManager.AppSettings["BccEmailAddress"]);
                    mail.Subject = txtSubject.Text;
                    mail.IsBodyHtml = true;
                    string path = Server.MapPath(WebConfigurationManager.AppSettings["SignatureLogo"]);
                    LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
                    Img.ContentId = "MyImage";
                    string str = @"<table style='font-family:Calibri;color:#003865'><tr><td> Dear " + ds.Tables[0].Rows[0]["vendorName"].ToString() + "</td></tr><tr><td>" + txtBody.Text + "</td></tr><tr></tr><tr></tr><tr></tr><tr><td><img src=cid:MyImage  id='img' alt='' width='100px' height='100px'/></td></tr><tr><td><b>" + ds.Tables[1].Rows[0]["SpecialistName"].ToString() + " | Vendor Specialist</b></td></tr><tr><td>" + WebConfigurationManager.AppSettings["AddressLine1"] + "</td></tr><tr><td>" + WebConfigurationManager.AppSettings["AddressLine2"] + "</td></tr><tr><td>" + ds.Tables[1].Rows[0]["phoneNumber"].ToString() + "</td></tr><tr><td>" + ds.Tables[1].Rows[0]["emailId"].ToString() + "</td></tr></table>";
                    AlternateView AV = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
                    AV.LinkedResources.Add(Img);
                    mail.AlternateViews.Add(AV);
                    Attachment attachment = new Attachment(btnSendLetter.CommandArgument);
                    mail.Attachments.Add(attachment);
                    //--Another Attachment Start--
                    string sourcePath = Server.MapPath(WebConfigurationManager.AppSettings["TemplatePath"]);
                    switch (ddlLetters.SelectedValue)
                    {
                        case "CERT ED(Products&Services)":
                            sourcePath = sourcePath + "Cert Attachment2.pdf";
                            break;
                        case "CERT ED(Prof&Tech)":
                            sourcePath = sourcePath + "Cert Attachment2.pdf";
                            break;
                        case "RECERT REQ":
                            sourcePath = sourcePath + "RECERT REQ Attachment2.pdf";
                            break;
                        case "SD FED CERT(Products&Services)":
                            sourcePath = sourcePath + "Cert Attachment2.pdf";
                            break;
                        case "SD FED CERT(Prof&Tech)":
                            sourcePath = sourcePath + "Cert Attachment2.pdf";
                            break;
                        case "SD MN CERT(Products&Services)":
                            sourcePath = sourcePath + "Cert Attachment2.pdf";
                            break;
                        case "SD MN CERT(Prof&Tech)":
                            sourcePath = sourcePath + "Cert Attachment2.pdf";
                            break;
                        case "VO FED CERT(Products&Services)":
                            sourcePath = sourcePath + "Cert Attachment2.pdf";
                            break;
                        case "VO FED CERT(Prof&Tech)":
                            sourcePath = sourcePath + "Cert Attachment2.pdf";
                            break;
                        case "VO MN CERT(Products&Services)":
                            sourcePath = sourcePath + "Cert Attachment2.pdf";
                            break;
                        case "VO MN CERT Template(Prof&Tech)":
                            sourcePath = sourcePath + "Cert Attachment2.pdf";
                            break;
                        case "CERT TG(Products&Services)":
                            sourcePath = sourcePath + "CERT TG Attachment2.pdf";
                            break;
                        case "CERT TG(Prof&Tech)":
                            sourcePath = sourcePath + "CERT TG Attachment2.pdf";
                            break;
                    }
                    if (sourcePath != Server.MapPath(WebConfigurationManager.AppSettings["TemplatePath"]))
                    {
                        Attachment att1 = new Attachment(sourcePath);
                        mail.Attachments.Add(att1);
                    }
                    //--Another Attachment End--

                    SmtpServer.Port = int.Parse(WebConfigurationManager.AppSettings["EmailPort"]);
                    SmtpServer.EnableSsl = Boolean.Parse(WebConfigurationManager.AppSettings["EnableSSL"]);
                    SmtpServer.Send(mail);
                    divError.Attributes.Add("style", "display:block");
                    lblError.Style.Value = "color: green";
                    lblError.Text = "Letter Sent Successfully!!!";
                    string qry = dbLibrary.idBuildQuery("[proc_saveE-MailInfo]", Request.QueryString["VId"].ToString(), Session["UserId"].ToString(), btnSendLetter.CommandName, ddlLetters.SelectedValue, txtSubject.Text, txtBody.Text, Session["UserId"].ToString());
                    string eMailStatus = dbLibrary.idGetAFieldByQuery(qry);
                    if (eMailStatus == string.Empty)
                    {
                        lblError.Style.Value = "color: red";
                        lblError.Text = "Letter Sent Successfully!!! But error while saving the eMail info in Database.";
                    }
                    txtBody.Text = "";
                    txtSubject.Text = "";
                }
                else
                {
                    divError.Attributes.Add("style", "display:block");
                    lblError.Text = "No e-Mail ID present for this Vendor, Please update e-Mail ID and Send Letter.";
                }
            }
            catch (Exception ex)
            {
                divError.Attributes.Add("style", "display:block");
                lblError.Text = "Error While Sending the Letter, Please Try Again !!!";
                if (ex.Message.Contains("NoCatch") || ex.Message.Contains("maxUrlLength"))
                    return;
                string userid;
                try
                {
                    userid = Session["UserId"] != null ? Session["UserId"].ToString() : "";
                }
                catch
                {
                    userid = string.Empty;
                }
                string errMsg;
                if (ex.InnerException != null)
                {
                    errMsg = ex.InnerException.Message.ToString().Length >= 250 ? ex.InnerException.Message.ToString().Substring(0, 249).Replace('\'', '\"').TrimEnd('.') : ex.InnerException.Message.ToString().Replace('\'', '\"').TrimEnd('.');
                }
                else
                {
                    errMsg = ex.Message.ToString().Length >= 250 ? ex.Message.ToString().Substring(0, 249).Replace('\'', '\"').TrimEnd('.') : ex.Message.ToString().Replace('\'', '\"').TrimEnd('.');
                }
                // Response.Write(errMsg);
                string qur = dbLibrary.idBuildQuery("sp_ErrorLog", errMsg, "", userid);
                dbLibrary.idExecute(qur);
                Server.ClearError();
            }
        }

        protected void btnOverRide_Click(object sender, EventArgs e)
        {
            string status = radbtnApprove.Checked == true ? "Approved" : radbtnDeny.Checked == true ? "Denied" : radbtnPending.Checked == true ? "Pending" : "Removed";
            string qur = dbLibrary.idBuildQuery("[proc_overrideCertStatus]", Request.QueryString["VId"], status, txtOverrideReason.Text, status == "Approved" ? DateTime.Now.AddYears(1).ToString() : "", Session["UserId"].ToString());
            dbLibrary.idExecute(qur);
            Response.Redirect("vmpUsrStatus.aspx?VId=" + Request.QueryString["VId"]);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("vmpUsrVendor4.aspx?VId=" + Request.QueryString["VId"] + "&EditMode=True");
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