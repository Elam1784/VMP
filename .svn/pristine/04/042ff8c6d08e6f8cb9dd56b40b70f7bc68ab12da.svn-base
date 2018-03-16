<%@ Page Title="" Language="C#" MasterPageFile="~/User/vmpUsrMaster.Master" AutoEventWireup="true" CodeBehind="vmpUsrHome.aspx.cs" Inherits="VMP_1._0.User.vmpUsrHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .chart {
            width: 100%;
            min-height: 270px;
            margin: -5px;
        }

        .chartLast {
            width: 100%;
            min-height: 200px;
            margin: -5px;
        }

        .borderleft {
            border-left: 1px;
            border-left-color: gainsboro;
            border-left-style: outset;
        }

        .grid td, .grid th {
            text-align: center !important;
            font-weight: normal !important;
        }

        label {
            font-weight: normal !important;
        }

        .customgrid {
            border-color: antiquewhite;
            border-style: solid;
            border-width: 1px;
            background-color: #ECF4FE;
            color: gray;
        }

        .customgridvalue {
            height: 27px;
        }

        .modal-lg {
            /* new custom width */
            width: 950px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="hdnPT" runat="server" Value="0" />
    <asp:HiddenField ID="hdnGoods" runat="server" Value="0" />
    <asp:HiddenField ID="hdnConstruction" runat="server" Value="0" />
    <asp:HiddenField ID="hdnServices" runat="server" Value="0" />
    <div style="display: none; text-align: center" id="myMessage" runat="server" class="alert alert-success col-sm-12">
        <asp:Label ID="lblError" runat="server" Text="Vendor Details Saved Successfully"></asp:Label>
    </div>
    <div class="container">
        <div class="row">
            <h2><b>Dashboard</b></h2>
        </div>
        <div class="row" style="border-radius: 6px; border: 1px solid; border-color: gainsboro; margin-bottom: 30px">
            <div class="row" style="margin-top: 20px">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <%--<div class="panel panel-info">
                        <div class="panel-heading"><asp:Label ID="lblPortalAppCount" runat="server">9 New Vendors From Portal</asp:Label></div>
                        <div class="panel-body">
                            Panel content
                        </div>
                    </div>--%>
                    <%--  <div style="display: block; text-align: center" id="myMessage1" runat="server" class="alert alert-info col-sm-12">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <asp:LinkButton ID="lnkPortalAppCount" runat="server" Text="9 New Applications From Portal" ToolTip="Click here to view the portal applications"></asp:LinkButton>
                    </div>--%>
                    <div class="panel-group alert" id="accordion" role="tablist" aria-multiselectable="true" runat="server">
                        <div class="panel panel-info">
                            <div class="panel-heading" role="tab" id="headingOne">
                                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                    <h6 class="panel-title"><asp:Label ID="lblPortalAppCount" runat="server"></asp:Label> New Application(s) From Portal</h6>
                                </a>
                            </div>
                            <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                             <asp:GridView ID="grdPortalApps" runat="server" CssClass="grid" HeaderStyle-BackColor="#6794ce" PagerStyle-BackColor="#ECF4FE" PagerStyle-ForeColor="Gray" AlternatingRowStyle-BackColor="White" BorderColor="AntiqueWhite" RowStyle-BackColor="#ECF4FE" HeaderStyle-ForeColor="White" RowStyle-ForeColor="Gray" AllowSorting="true" AllowPaging="true" PageSize="8" AutoGenerateColumns="false" ShowHeader="true" Style="width: 100%; text-align: center; margin-bottom: 10px;" BackColor="White" EmptyDataRowStyle-BorderColor="White" OnPageIndexChanging="grdPortalApps_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="link" runat="server" Text='<%# Eval("vendorName") %>' NavigateUrl='<%# Eval("vendorId","vmpUsrVendor1?VId={0}&EditMode=True") %>' Target="_blank" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="addedDateTime" HeaderText="Received Date" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DaysSinceArrived" HeaderText="Days Since Application Arrived" HeaderStyle-HorizontalAlign="Center" />
                                    </Columns>
                                        </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-md-1"></div>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-6">
                    <div id="chart_div1" class="chart">
                        <div class="col-md-12">
                            <h4 style="text-align: center; color: #6794CE; margin-top: 15px;"><b>Vendors In Progress</b></h4>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <asp:GridView ID="grdProgress" runat="server" CssClass="grid" HeaderStyle-BackColor="#6794ce" PagerStyle-BackColor="#ECF4FE" PagerStyle-ForeColor="Gray" AlternatingRowStyle-BackColor="White" BorderColor="AntiqueWhite" RowStyle-BackColor="#ECF4FE" HeaderStyle-ForeColor="White" RowStyle-ForeColor="Gray" AllowSorting="true" AllowPaging="true" PageSize="8" AutoGenerateColumns="false" ShowHeader="true" Style="width: 100%; text-align: center; margin-bottom: 10px;" BackColor="White" EmptyDataRowStyle-BorderColor="White" OnPageIndexChanging="grdProgress_PageIndexChanging" OnRowDataBound="grdProgress_RowDataBound">
                                    <Columns>
                                        <%--<asp:HyperLinkField DataTextField="vendorName" HeaderText="Vendor Name" NavigateUrl='<%# Eval("vendorId","vmpUsrVendor1?VId={0}&EditMode=True") %>' HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />--%>
                                        <asp:TemplateField HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="link" runat="server" Text='<%# Eval("vendorName") %>' NavigateUrl='<%# Eval("vendorId","vmpUsrVendor1?VId={0}&EditMode=True") %>' Target="_blank" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="vendorName" HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />--%>
                                        <asp:BoundField DataField="addedDateTime" HeaderText="Added Date" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DaysSinceArrived" HeaderText="Days Since Application Pending" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="View Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkStatus" runat="server" Text="View" CommandArgument='<%# Eval("vendorId") %>' OnClick="lnkStatus_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <label style="color: red;">No Data Found</label>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 borderleft">
                    <div id="chart_div2" class="chart">
                        <div class="col-md-12">
                            <h4 style="text-align: center; color: #6794CE; margin-top: 15px;"><b>Vendors Due to be Re-Certified</b></h4>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-1"></div>
                            <div class="col-md-10">
                                <asp:GridView ID="grdReCert" runat="server" CssClass="grid" HeaderStyle-BackColor="#6794ce" PagerStyle-BackColor="#ECF4FE" PagerStyle-ForeColor="Gray" AlternatingRowStyle-BackColor="White" BorderColor="AntiqueWhite" RowStyle-BackColor="#ECF4FE" HeaderStyle-ForeColor="White" RowStyle-ForeColor="Gray" AllowSorting="true" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" ShowHeader="true" Style="width: 100%; text-align: center; margin-bottom: 10px;" BackColor="White" EmptyDataRowStyle-BorderColor="White" OnPageIndexChanging="grdReCert_PageIndexChanging" OnRowDataBound="grdReCert_RowDataBound">
                                    <Columns>
                                        <%--<asp:BoundField DataField="vendorName" HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />--%>
                                        <asp:TemplateField HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="link" runat="server" Text='<%# Eval("vendorName") %>' NavigateUrl='<%# Eval("vendorId","vmpUsrVendor1?VId={0}&EditMode=True") %>' Target="_blank" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nextCertDate" HeaderText="Due Date" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DaysForRecert" HeaderText="Days Due To Be Certified" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="View Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkStatus" runat="server" Text="View" CommandArgument='<%# Eval("vendorId") %>' OnClick="lnkStatus_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <label style="color: red;">No Data Found</label>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-6">
                    <div id="chart_div3" class="chart">
                        <div class="col-md-12">
                            <h4 style="text-align: center; color: #6794CE;"><b>Vendors By Designation</b></h4>
                        </div>
                        <div class="col-md-12" style="margin-top: 20px">
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-3"></div>
                                <div class="col-md-5 customgrid">
                                    <label>Minority</label>
                                    <asp:LinkButton ID="lnkMinorityDetails" runat="server" Text="(+)" CssClass="btn-link" OnClick="lnkMinorityDetails_Click"></asp:LinkButton>
                                </div>
                                <div class="col-md-2 customgrid customgridvalue">
                                    <asp:Label ID="lblMinority" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-3"></div>
                                <div class="col-md-5 customgrid">
                                    <label>Women</label>
                                </div>
                                <div class="col-md-2 customgrid customgridvalue">
                                    <asp:Label ID="lblWomen" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-3"></div>
                                <div class="col-md-5 customgrid">
                                    <label>Disabled</label>
                                </div>
                                <div class="col-md-2 customgrid customgridvalue">
                                    <asp:Label ID="lblDisabled" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-3"></div>
                                <div class="col-md-5 customgrid">
                                    <label>Economically Disadvantaged</label>
                                    <asp:LinkButton ID="lnkEDRDetails" runat="server" Text="(+)" CssClass="btn-link" OnClick="lnkEDRDetails_Click"></asp:LinkButton>

                                </div>
                                <div class="col-md-2 customgrid customgridvalue">
                                    <asp:Label ID="lblEDR" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-3"></div>
                                <div class="col-md-5 customgrid">
                                    <label>Veteran</label>
                                    <asp:LinkButton ID="lnkbtnVeteranDetails" runat="server" Text="(+)" CssClass="btn-link" OnClick="lnkbtnVeteranDetails_Click"></asp:LinkButton>
                                </div>
                                <div class="col-md-2 customgrid customgridvalue">
                                    <asp:Label ID="lblVeteran" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-md-3"></div>
                                <div class="col-md-5 customgrid">
                                    <label style="color: black">Total</label>
                                </div>
                                <div class="col-md-2 customgrid customgridvalue">
                                    <asp:Label ID="lblTotal" runat="server" Style="color: black;" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 borderleft">
                    <h4 style="text-align: center; color: #6794CE;"><b>Vendor Categories</b></h4>
                    <div id="chart_div4" class="chart">
                    </div>
                </div>
            </div>
            <hr />
            <div class="row" style="margin-top: 20px;">
                <div class="col-md-6">
                    <div id="chart_div5" class="chartLast">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-1"></div>
                                <div class="col-md-10">
                                    <h4 style="text-align: center; color: #6794CE;"><b>Average # of Days for Application Review</b></h4>
                                    <div class="row">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-6 customgrid">
                                            <label>Avg of All Specialists</label>
                                        </div>
                                        <div class="col-md-2 customgrid customgridvalue" style="text-align: center;">
                                            <asp:Label ID="lblAvgAll" runat="server" Text="--" />
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                    <div class="row" id="divAvgYours" runat="server" style="margin-top: 10px;">
                                        <div class="col-md-2"></div>
                                        <div class="col-md-6 customgrid">
                                            <%--<asp:Label ID="lblSpecialistName" runat="server" Text="Your's"></asp:Label>--%>
                                            <label>Yours</label>

                                        </div>
                                        <div class="col-md-2 customgrid customgridvalue" style="text-align: center">
                                            <asp:Label ID="lblAvgSpecialist" runat="server" Text="--" />
                                        </div>
                                        <div class="col-md-2"></div>
                                    </div>
                                </div>
                                <div class="col-md-1"></div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <h4 style="text-align: center; color: #6794CE;"><b>Applications By Month</b></h4>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3"></div>
                                <div class="col-md-6">
                                    <asp:GridView ID="grdMonthStat" runat="server" CssClass="grid" HeaderStyle-BackColor="#6794ce" PagerStyle-BackColor="#ECF4FE" PagerStyle-ForeColor="Gray" AlternatingRowStyle-BackColor="White" BorderColor="AntiqueWhite" RowStyle-BackColor="#ECF4FE" HeaderStyle-ForeColor="White" RowStyle-ForeColor="Gray" AllowSorting="true" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" ShowHeader="true" Style="width: 100%; text-align: center; margin-bottom: 10px;" BackColor="White" EmptyDataRowStyle-BorderColor="White" OnRowDataBound="grdMonthStat_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Month" HeaderText="Month" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                                            <asp:BoundField HeaderText="Received" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Completed" HeaderStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <label style="color: red;">No Data Found</label>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6 borderleft">
                    <div id="chart_div6" class="chartLast">
                        <div class="col-md-12">
                            <h4 style="text-align: center; color: #6794CE; margin-top: 15px;"><b>Vendors Pending Response for Recertification</b></h4>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <asp:GridView ID="grdPendingResponse" runat="server" CssClass="grid" HeaderStyle-BackColor="#6794ce" PagerStyle-BackColor="#ECF4FE" PagerStyle-ForeColor="Gray" AlternatingRowStyle-BackColor="White" BorderColor="AntiqueWhite" RowStyle-BackColor="#ECF4FE" HeaderStyle-ForeColor="White" RowStyle-ForeColor="Gray" AllowSorting="true" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" ShowHeader="true" Style="width: 100%; text-align: center; margin-bottom: 10px;" BackColor="White" EmptyDataRowStyle-BorderColor="White" OnPageIndexChanging="grdPendingResponse_PageIndexChanging" OnRowDataBound="grdPendingResponse_RowDataBound">
                                    <Columns>
                                        <%--<asp:BoundField DataField="vendorName" HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />--%>
                                        <asp:TemplateField HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="link" runat="server" Text='<%# Eval("vendorName") %>' NavigateUrl='<%# Eval("vendorId","vmpUsrVendor1?VId={0}&EditMode=True") %>' Target="_blank" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="requestedDate" HeaderText="Recert Requested Date" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DaysSinceEmailed" HeaderText="Days Since Letter Sent" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="View Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkStatus" runat="server" Text="View" CommandArgument='<%# Eval("vendorId") %>' OnClick="lnkStatus_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
    </div>
    <div class="modal fade" id="ModalViewDetails" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <asp:Label ID="lblModalHeading" runat="server" Text="Modal Heading" Style="text-align: center; color: #6794CE;"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" style="margin-top: 20px">
                            <div class="row" style="margin-top: 10px; display: none;" id="divBlack" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblblack" runat="server" Text="Black"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblBlackCount" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px; display: none;" id="divHispanic" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblHispanic" runat="server" Text="Hispanic"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblHispanicCount" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px; display: none;" id="divAsian" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblAsian" runat="server" Text="Asian"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblAsianCount" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px; display: none;" id="divIndiginessAmerican" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblIndiginessAmerican" runat="server" Text="Indigenous American"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblIndiginessAmericanCount" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px; display: none;" id="divVeteran" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblVeteranModal" runat="server" Text="Veteran"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblVeteranCount" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px; display: none;" id="divServiceDisabled" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblServiceDisabled" runat="server" Text="Service Disabled"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblServiceDisabledCount" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px; display: none;" id="divRehab" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblRehab" runat="server" Text="Rehabilitation Facilities"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblRehabCount" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px; display: none;" id="divLaborSurplus" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblLaborSurplus" runat="server" Text="Labor Surplus County"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblLaborSurplusCount" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px; display: none;" id="divMedianIncome" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblMedianIncome" runat="server" Text="70% Median Income County"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblMedianIncomeCount" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px; display: none;" id="divTargetedNeighbor" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblTargetedNeighbor" runat="server" Text="Targeted Neighborhood"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblTargetedNeighborCount" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                            <div class="row" style="margin-top: 10px; display: none;" id="divEnterpriseZone" runat="server">
                                <div class="col-md-3"></div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblEnterpriseZone" runat="server" Text="Enterprise Zone"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblEnterpriseZoneCount" runat="server" Text="--"></asp:Label>
                                </div>
                                <div class="col-md-2"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ModalViewStatus" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <asp:Label ID="Label1" runat="server" Text="Status" Style="text-align: center; color: #6794CE;"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-1"></div>
                                    <div class="col-md-10">
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
                                                                            <asp:ListItem>Indiginess American</asp:ListItem>
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
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtOrig" runat="server" CssClass="form-control Otherdatepicker"></asp:TextBox>
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
                                                                        <div class="col-md-12">
                                                                            <%--<asp:Label ID="lblLast" runat="server" Text="Not Found"></asp:Label>--%>
                                                                            <asp:TextBox ID="txtLast" runat="server" CssClass="form-control Otherdatepicker"></asp:TextBox>
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
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox ID="txtNext" runat="server" CssClass="form-control Otherdatepicker"></asp:TextBox>
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
                                                            <div class="col-md-10">
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-1"></div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            if ($(<%=hdnGoods.ClientID %>).val() == '0' && $(<%=hdnServices.ClientID %>).val() == '0' && $(<%=hdnPT.ClientID %>).val() == '0' && $(<%=hdnConstruction.ClientID %>).val() == '0') {
                $('#chart_div4').empty();
                $('<br><p style="color:red;text-align: center;">No Data Found</p>').appendTo('#chart_div4');
            }
            else {
                var data = google.visualization.arrayToDataTable([
                  ['Classification', 'Count'],
                  ['Goods', parseInt($(<%=hdnGoods.ClientID %>).val())],
          ['Services', parseInt($(<%=hdnServices.ClientID %>).val())],
          ['PT', parseInt($(<%=hdnPT.ClientID %>).val())],
          ['Construction', parseInt($(<%=hdnConstruction.ClientID %>).val())]
                ]);

                var options = {
                    //title: 'Vendor Categories',
                    is3D: true,
                    height: 270,
                    //width: 540
                };

                var chart = new google.visualization.PieChart(document.getElementById('chart_div4'));

                //Clickable and Link it
                //function selectHandler() {
                //    var selectedItem = chart.getSelection()[0];
                //    if (selectedItem) {
                //        var topping = data.getValue(selectedItem.row, 0);
                //        alert('The user selected ' + topping);
                //    }
                //  }
                // google.visualization.events.addListener(chart, 'select', selectHandler);

                chart.draw(data, options);
            }
        }
        $(window).resize(function () {
            drawChart();
        });
    </script>
    <script type="text/javascript">
        function showmessage() {
            $("#<%=myMessage.ClientID%>").show();
            setTimeout(function () {
                var selectedEffect = 'blind';
                var options = {};
                $("#<%=myMessage.ClientID%>").hide();
            }, 5000);
            return false;
        }
    </script>
    <script type="text/javascript">
        function showModal() {
            //   jQuery.noConflict();
            $("#ModalViewDetails").modal('show');
        }
    </script>
    <script type="text/javascript">
        function showModalforStatus() {
            //   jQuery.noConflict();
            $("#ModalViewStatus").modal('show');
        }
    </script>
</asp:Content>
