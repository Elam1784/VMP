<%@ Page Title="" Language="C#" MasterPageFile="~/User/vmpUsrMaster.Master" AutoEventWireup="true" CodeBehind="manageVendor.aspx.cs" Inherits="VMP_1._0.User.manageVendor" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-ui.js"></script>
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <script>
        function pageLoad(sender, args) {
            jQuery.noConflict();
            $('.Otherdatepicker').datepicker();
        }
    </script>
    <style>
        label {
            font-weight: normal;
        }

        .grid td, .grid th {
            text-align: center !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container" style="background-color: gainsboro; border-radius: 6px;">
        <div class="row" style="text-align: center">
            <h3>Manage Vendor</h3>
        </div>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnOverRide" />
            </Triggers>
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <div class="row" style="border-style: ridge;">
                                <div class="col-md-2" style="border-right: ridge;">
                                    <h5 style="margin-top: 14px !important;"><b>Search By</b></h5>
                                </div>
                                <div class="col-md-9">
                                    <div class="col-md-4">
                                        <asp:RadioButton ID="radSearchVN" runat="server" Text="Vendor Name" CssClass="radio" GroupName="s" Checked="true" OnCheckedChanged="radSearchVN_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:RadioButton ID="radSearchSpecialist" runat="server" Text="Specialist" CssClass="radio" GroupName="s" OnCheckedChanged="radSearchSpecialist_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:RadioButton ID="radSearchLName" runat="server" Text="Primary Owner Lastname" CssClass="radio" GroupName="s" OnCheckedChanged="radSearchLName_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1"></div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-4"></div>
                    <div class="col-md-3" id="divSearchV" runat="server">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Vendor Name" Style="margin-bottom: 5px" ValidationGroup="g"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="Field Cannot be Empty" ControlToValidate="txtSearch" ValidationGroup="g"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3" id="divSearchS" runat="server" style="display: none;">
                        <asp:DropDownList ID="ddlSpecialist" runat="server" CssClass="form-control" Style="margin-bottom: 5px"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" ValidationGroup="g" />
                    </div>
                    <div class="col-md-3"></div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-8">
                        <asp:GridView ID="grdSearchResult" runat="server" DataKeyNames="vendorId" CssClass="grid" AllowSorting="true" AllowPaging="true" PageSize="10" OnRowDataBound="grdSearchResult_RowDataBound" OnSelectedIndexChanged="grdSearchResult_SelectedIndexChanged" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="Gray" HeaderStyle-BorderColor="White" HeaderStyle-ForeColor="White" Style="width: 100%; text-align: center; margin-bottom: 10px;" BackColor="White" EmptyDataRowStyle-BorderColor="White" OnPageIndexChanging="grdSearchResult_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="vendorName" HeaderText="Vendor" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="specialistName" HeaderText="Specialist" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="primaryOwner" HeaderText="Primary Owner" HeaderStyle-HorizontalAlign="Center" />
                            </Columns>
                            <EmptyDataTemplate>
                                <label style="color: red;">No Data Found</label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <br />
                <div class="row" id="divButtons" runat="server" style="display: none;">
                    <div class="col-md-4"></div>
                    <div class="col-md-2" style="text-align: center">
                        <asp:HyperLink ID="btnEdit" runat="server" Text="View/Edit" CssClass="btn btn-primary" Target="_blank" Style="margin-bottom: 5px;"></asp:HyperLink>
                    </div>
                    <div class="col-md-2" style="text-align: center">
                        <asp:Button ID="btnRecommendation" runat="server" Text="Recommendation" CssClass="btn btn-primary" Style="margin-bottom: 5px;" OnClick="btnRecommendation_Click" />
                    </div>
                    <div class="col-md-4"></div>
                </div>

                <%--Modal Recommendataion Begin--%>
                <div class="modal fade" id="myModal" role="dialog" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Recommendation</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <asp:RadioButton ID="radbtnApprove" runat="server" Text="Approve" CssClass="radio radio-inline" GroupName="b" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:RadioButton ID="radbtnDeny" runat="server" Text="Deny" CssClass="radio radio-inline" GroupName="b" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:RadioButton ID="radbtnPending" runat="server" Text="Pending" CssClass="radio radio-inline" GroupName="b" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:RadioButton ID="radbtnRemove" runat="server" Text="Remove" CssClass="radio radio-inline" GroupName="b" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <label style="margin-top: 25%;">Override Reason</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtOverrideReason" runat="server" CssClass="form-control" TextMode="MultiLine" Style="width: 100%; border-top-right-radius: 4px; border-bottom-right-radius: 4px; height: 90px;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3"></div>
                                        <div class="col-md-8">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Reason Cannont Be Blank" ValidationGroup="r" ControlToValidate="txtOverrideReason" Style="color: red;"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnOverRide" runat="server" Text="Override" CssClass="btn btn-primary" ValidationGroup="r" OnClick="btnOverRide_Click" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
                <%--Modal Recommendataion End--%>
                <br />
                <div style="display: none; text-align: center" id="myMessage1" runat="server" class="alert alert-success col-sm-12">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <asp:Label ID="lblError" runat="server" Text="Letter Deleted Successfully"></asp:Label>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="btnYes" EventName="Click" />--%>
                        <asp:PostBackTrigger ControlID="btnYes" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="row" id="divAccordions" runat="server" style="display: none;">
                            <div class="col-md-12">
                                <div class="col-md-1"></div>
                                <div class="col-md-10">
                                    <div class="panel-group" id="accordionStatus" role="tablist" aria-multiselectable="true">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#accordionStatus" href="#collapseStatus">
                                                    <h4 class="panel-title">Status</h4>
                                                </a>
                                            </div>
                                            <div id="collapseStatus" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4"></div>
                                                                <div class="col-md-4">
                                                                    <h5 id="vendorNameStatus" runat="server" style="text-align: center; text-decoration: underline; font-weight: bold; font-style: italic" class="col-md-offset-1"></h5>
                                                                </div>
                                                                <div class="col-md-4"></div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6" style="border-right-style: ridge">
                                                                    <div class="col-md-12" style="text-align: center">
                                                                        <label><b>Category</b></label>
                                                                    </div>
                                                                    <br />
                                                                    <div class="col-md-12">
                                                                        <div class="row" style="margin-bottom: 5px; border-bottom-style: inset;">
                                                                            <div class="col-md-4">
                                                                            </div>
                                                                            <div class="col-md-8">
                                                                                <div class="col-md-12">
                                                                                    <asp:CheckBox ID="chkCitizen" runat="server" Text="Citizen/Permanent Resident" CssClass="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" style="margin-bottom: 5px;">
                                                                            <div class="col-md-4" style="margin-top: 10px;">
                                                                                <label>Targeted Group</label>
                                                                            </div>
                                                                            <div class="col-md-8">
                                                                                <div class="col-md-12">
                                                                                    <asp:CheckBoxList ID="chkbxTargetGroup" runat="server" CssClass="checkbox">
                                                                                        <asp:ListItem>Asian/Pacific</asp:ListItem>
                                                                                        <asp:ListItem>Black</asp:ListItem>
                                                                                        <asp:ListItem>Hispanic</asp:ListItem>
                                                                                        <asp:ListItem>Indigenous American</asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" style="margin-bottom: 5px; display: none;" runat="server" id="divIndiginess">
                                                                            <div class="col-md-4">
                                                                            </div>
                                                                            <div class="col-md-8">
                                                                                <div class="col-md-12">
                                                                                    <asp:CheckBoxList ID="chkIndiginess" runat="server" CssClass="checkbox">
                                                                                        <asp:ListItem>Aluets/Eskimos</asp:ListItem>
                                                                                        <asp:ListItem>Native Hawaiians</asp:ListItem>
                                                                                        <asp:ListItem>American Indian</asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" style="margin-bottom: 5px; display: none;" id="divTribal" runat="server">
                                                                            <div class="col-md-4">
                                                                                <label>Tribal ID</label>
                                                                            </div>
                                                                            <div class="col-md-7">
                                                                                <asp:Label ID="lblTribalID" runat="server" Style="margin-left: 14px;"></asp:Label>
                                                                                <%--<asp:TextBox ID="txtTribalID" runat="server" CssClass="form-control" MaxLength="16" ></asp:TextBox>--%>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" style="margin-bottom: 5px; border-bottom-style: inset;">
                                                                            <div class="col-md-4">
                                                                            </div>
                                                                            <div class="col-md-8">
                                                                                <div class="col-md-12">
                                                                                    <asp:CheckBoxList ID="chkTG1" runat="server" AutoPostBack="true" CssClass="checkbox">
                                                                                        <asp:ListItem>Physical Disability</asp:ListItem>
                                                                                        <asp:ListItem>Woman</asp:ListItem>
                                                                                    </asp:CheckBoxList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" style="margin-bottom: 5px; border-bottom-style: inset;">
                                                                            <div class="col-md-4" style="margin-top: 10px;">
                                                                                <label>Veterans</label>
                                                                            </div>
                                                                            <div class="col-md-8">
                                                                                <div class="col-md-12">
                                                                                    <asp:RadioButtonList ID="radVeteran" runat="server" CssClass="radio">
                                                                                        <asp:ListItem>Veteran</asp:ListItem>
                                                                                        <asp:ListItem>Service Disabled Veteran</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" style="margin-bottom: 5px; border-bottom-style: inset;">
                                                                            <div class="col-md-4" style="margin-top: 10px;">
                                                                                <label>Non-Profit</label>
                                                                            </div>
                                                                            <div class="col-md-8">
                                                                                <div class="col-md-12">
                                                                                    <asp:CheckBox ID="chkRehabilitation" runat="server" Text="Rehabilitation Facilities" CssClass="checkbox" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="col-md-12" style="text-align: center">
                                                                        <label><b>Certification Dates</b></label>
                                                                    </div>
                                                                    <br />
                                                                    <div class="row">
                                                                        <div class="row" style="margin-bottom: 5px">
                                                                            <div class="col-md-3"></div>
                                                                            <div class="col-md-9">
                                                                                <div class="col-md-3">
                                                                                    <label style="margin-top: 6px;">Orig:</label>
                                                                                </div>
                                                                                <div class="col-md-9" style="margin-left: -10px;">

                                                                                    <%--<asp:Label ID="lblOrig" runat="server" Text="Not Found"></asp:Label>--%>
                                                                                    <div class="col-md-7">
                                                                                        <asp:TextBox ID="txtOrig" runat="server" CssClass="form-control Otherdatepicker"></asp:TextBox>
                                                                                    </div>
                                                                                    <div class="col-md-5">
                                                                                        <%--<asp:TextBox ID="txtOrig" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                                        <asp:LinkButton ID="lnkUpdateOrig" runat="server" Text="Update" Style="display: block; margin-top: 5px;" OnClick="lnkUpdateOrig_Click"></asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row" style="margin-bottom: 5px">
                                                                            <div class="col-md-3"></div>
                                                                            <div class="col-md-9">
                                                                                <div class="col-md-3">
                                                                                    <label style="margin-top: 6px;">Last:</label>
                                                                                </div>
                                                                                <div class="col-md-9" style="margin-left: -10px;">
                                                                                    <div class="col-md-7">
                                                                                        <%--<asp:Label ID="lblLast" runat="server" Text="Not Found"></asp:Label>--%>
                                                                                        <asp:TextBox ID="txtLast" runat="server" CssClass="form-control Otherdatepicker"></asp:TextBox>
                                                                                    </div>
                                                                                    <%--<asp:TextBox ID="txtLast" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                                    <div class="col-md-5">
                                                                                        <asp:LinkButton ID="lnkUpdateLast" runat="server" Text="Update" Style="display: block; margin-top: 5px;" OnClick="lnkUpdateLast_Click"></asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-3"></div>
                                                                            <div class="col-md-9" id="divNext" runat="server">
                                                                                <div class="col-md-3">
                                                                                    <label style="margin-top: 6px;">Next:</label>
                                                                                </div>
                                                                                <div class="col-md-9" style="margin-left: -10px;">

                                                                                    <%--<asp:Label ID="lblNext" runat="server" Text="Not Found" Style="color: red;"></asp:Label>--%>
                                                                                    <div class="col-md-7">
                                                                                        <asp:TextBox ID="txtNext" runat="server" CssClass="form-control Otherdatepicker"></asp:TextBox>
                                                                                        <%--<asp:TextBox ID="txtNext" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                                    </div>
                                                                                    <div class="col-md-5">
                                                                                        <asp:LinkButton ID="lnkUpdateNext" runat="server" Text="Update" Style="display: block; margin-top: 5px;" OnClick="lnkUpdateNext_Click"></asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-8" id="divRemoved" runat="server" style="display: none;">
                                                                                <div class="col-md-3">
                                                                                    <label>Removed Date:</label>
                                                                                </div>
                                                                                <div class="col-md-9">
                                                                                    <b>
                                                                                        <asp:Label ID="lblRemoved" runat="server" Style="color: red;"></asp:Label></b>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <br />
                                                                    <br />
                                                                    <div class="col-md-12" style="text-align: center">
                                                                        <label><b>Status</b></label>
                                                                    </div>
                                                                    <div class="col-md-12 col-md-offset-4">
                                                                        <asp:RadioButtonList ID="radbutStatus" runat="server" CssClass="radio">
                                                                            <asp:ListItem>Approved</asp:ListItem>
                                                                            <asp:ListItem>Denied</asp:ListItem>
                                                                            <asp:ListItem>Pending</asp:ListItem>
                                                                            <asp:ListItem>Removed</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                    <br />
                                                                    <div class="col-md-12" style="text-align: center">
                                                                        <label><b>Designation</b></label>
                                                                    </div>
                                                                    <div class="col-md-12" style="text-align: center">
                                                                        <div class="col-md-2"></div>
                                                                        <div class="col-md-8">
                                                                            <%--<asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                                            <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-md-2"></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-6" style="border-right-style: ridge;">
                                                                    <div class="col-md-12" style="text-align: center">
                                                                        <label><b>Economically Disadvantaged Region</b></label>
                                                                    </div>
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-2"></div>
                                                                        <div class="col-md-8">
                                                                            <asp:CheckBoxList ID="chkEDR" CssClass="checkbox" runat="server"></asp:CheckBoxList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <label>Reason</label>
                                                                        </div>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtInfoPending" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="4"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <asp:LinkButton ID="lnkRules" runat="server" OnClick="lnkRules_Click">Rules View <span class="glyphicon glyphicon-new-window" aria-hidden="true"></span></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="panel panel-default">
                                            <%-- <asp:UpdatePanel ID="updpnlLog" runat="server">
                                <ContentTemplate>--%>
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#accordionStatus" href="#<%=collapseLog.ClientID%>">
                                                    <h4 class="panel-title">Log</h4>
                                                </a>
                                            </div>
                                            <div id="collapseLog" runat="server" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4"></div>
                                                                <div class="col-md-4">
                                                                    <h5 id="vendorNameLog" runat="server" style="text-align: center; text-decoration: underline; font-weight: bold; font-style: italic" class="col-md-offset-1"></h5>
                                                                </div>
                                                                <div class="col-md-4"></div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-2"></div>
                                                                <div class="col-md-8">

                                                                    <asp:GridView ID="grdVLog" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdVLog_PageIndexChanging" CssClass="grid" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" Style="width: 100%; text-align: center; margin-bottom: 10px;" EmptyDataRowStyle-BorderColor="White" PagerStyle-BorderColor="White">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="Specialist" HeaderText="Specialist" HeaderStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="activityDesc" HeaderText="Activity" HeaderStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="actDateTime" HeaderText="Date Time" HeaderStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            <label style="color: red;">No Data Found</label>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>

                                                                </div>
                                                                <div class="col-md-2"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                                        </div>
                                        <div class="panel panel-default">
                                            <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>--%>
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#accordionStatus" href="#<%=collapseLetters.ClientID%>">
                                                    <h4 class="panel-title">Letters</h4>
                                                </a>
                                            </div>
                                            <div id="collapseLetters" runat="server" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4"></div>
                                                                <div class="col-md-4">
                                                                    <h5 id="vendorNameLetter" runat="server" style="text-align: center; text-decoration: underline; font-weight: bold; font-style: italic" class="col-md-offset-1"></h5>
                                                                </div>
                                                                <div class="col-md-4"></div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-md-12" style="text-align: center">
                                                                <asp:Button ID="btnAdditionalLetter" runat="server" Text="Send Additional Letter" CssClass="btn btn-primary" OnClick="btnAdditionalLetter_Click" />
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-2"></div>
                                                                <div class="col-md-8">
                                                                    <asp:GridView ID="grdLetters" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdLetters_PageIndexChanging" OnRowDataBound="grdLetters_RowDataBound" OnRowCommand="grdLetters_RowCommand" CssClass="grid" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" Style="width: 100%; text-align: center; margin-bottom: 10px;" EmptyDataRowStyle-BorderColor="White" PagerStyle-BorderColor="White">
                                                                        <Columns>
                                                                            <%--<asp:BoundField DataField="Specialist" HeaderText="Letter" HeaderStyle-HorizontalAlign="Center" />--%>
                                                                            <asp:TemplateField HeaderText="Letter">
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink ID="lnkLetter" runat="server" Target="_blank" Text='<%# Eval("templateType") %>'></asp:HyperLink>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="createdDateTime" HeaderText="Created Date" HeaderStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="createdBy" HeaderText="Created By" HeaderStyle-HorizontalAlign="Center" />
                                                                            <asp:TemplateField ShowHeader="False">
                                                                                <ItemTemplate>
                                                                                    <div class="griddiv">
                                                                                        <asp:Button ID="btnDelete" runat="server" CausesValidation="false" CommandName="DeleteLetter" CssClass="btn-link" Text="Delete" CommandArgument='<%# Eval("letterInfoId") %>' />
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            <label style="color: red;">No Data Found</label>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </div>
                                                                <div class="col-md-2"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>
                                        </div>
                                        <div class="panel panel-default">
                                            <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>--%>
                                            <div class="panel-heading">
                                                <a data-toggle="collapse" data-parent="#accordionStatus" href="#<%=collapseEmails.ClientID%>">
                                                    <h4 class="panel-title">E-Mails</h4>
                                                </a>
                                            </div>
                                            <div id="collapseEmails" runat="server" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4"></div>
                                                                <div class="col-md-4">
                                                                    <h5 id="vendorNameEmail" runat="server" style="text-align: center; text-decoration: underline; font-weight: bold; font-style: italic" class="col-md-offset-1"></h5>
                                                                </div>
                                                                <div class="col-md-4"></div>
                                                            </div>
                                                        </div>
                                                        <br />
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-2"></div>
                                                                <div class="col-md-8">
                                                                    <asp:GridView ID="grdEmail" runat="server" AllowPaging="true" PageSize="10" OnRowDataBound="grdEmail_RowDataBound" OnPageIndexChanging="grdEmail_PageIndexChanging" CssClass="grid" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" Style="width: 100%; text-align: center; margin-bottom: 10px;" EmptyDataRowStyle-BorderColor="White" PagerStyle-BorderColor="White">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="sentBy" HeaderText="Sent By" HeaderStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="emailedDate" HeaderText="Sent On" HeaderStyle-HorizontalAlign="Center" />
                                                                            <asp:TemplateField HeaderText="Letter">
                                                                                <ItemTemplate>
                                                                                    <asp:HyperLink ID="lnkLetter" runat="server" Target="_blank" Text='<%# Eval("templateType") %>'></asp:HyperLink>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="E-Mail">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkEmail" runat="server" Text="View in Detail" CommandArgument='<%# Eval("eMailLogID") %>' OnClick="lnkEmail_Click"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            <label style="color: red;">No Data Found</label>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </div>
                                                                <div class="col-md-2"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--  <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <div class="modal fade" id="myModalConfirm" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" id="H3">Are you sure you want to delete this letter?</h4>
                            </div>
                            <div class="modal-footer" style="text-align: center; margin-top: 2px; border-top: none !important">
                                <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-primary" OnClick="btnYes_Click" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                            </div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
                <%--   </ContentTemplate>
                </asp:UpdatePanel>--%>

                <div class="modal fade" id="ModalRulesView" role="dialog" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Rules View</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdRulesView" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" Style="width: 100%; margin-bottom: 10px;" EmptyDataRowStyle-BorderColor="White">
                                            <Columns>
                                                <asp:BoundField DataField="description" HeaderText="Rule Description" HeaderStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="status" HeaderText="Activity" HeaderStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <label style="color: red;">No Data Found</label>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="ModalEMailView" role="dialog" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">E-Mail</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-2">
                                        <label>Subject</label>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblSubject" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-1"></div>
                                </div>
                                <div class="row" style="margin-top: 20px;">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-2">
                                        <label>Body</label>
                                    </div>
                                    <div class="col-md-8">
                                        <asp:Label ID="lblBody" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-1"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
    </div>
    <script>
        $(document).ready(function () {
            $("#navbar7 li").removeClass("active");//this will remove the active class from  
            $('#menu').addClass('active');
        });
    </script>
    <script type="text/javascript">
        function showModal() {
            jQuery.noConflict();
            $("#myModal").modal('show');
        }
    </script>
    <script type="text/javascript">
        function showModalRulesView() {
            jQuery.noConflict();
            $("#ModalRulesView").modal('show');
        }
    </script>
    <script type="text/javascript">
        function showModalEmailView() {
            jQuery.noConflict();
            $("#ModalEMailView").modal('show');
        }
    </script>
    <script type="text/javascript">
        function openModal() {
            jQuery.noConflict();
            $("#myModalConfirm").modal("show");
        }
    </script>
     <script type="text/javascript">
        function runEffect1() {
            $("#<%=myMessage1.ClientID%>").show();
            setTimeout(function () {
                var selectedEffect = 'blind';
                var options = {};
                $("#<%=myMessage1.ClientID%>").hide();
            }, 5000);
            return false;
        }
    </script>
</asp:Content>
