<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/vmpAdmMaster.Master" AutoEventWireup="true" CodeBehind="vmpAdmNaic.aspx.cs" Inherits="VMP_1._0.Admin.vmpAdmNaic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $("#navbar7 li").removeClass("active");//this will remove the active class from  
            $('#menu').addClass('active');//previously active menu item 
        });
    </script>
    <style>
        .table1 tr, td, th {
            text-align: left !important;
        }
        label{
            font-weight:normal;
        }
    </style>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="display: none; text-align: center" id="myMessage1" runat="server" class="alert alert-success col-sm-12">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <asp:Label ID="lblError" runat="server" Text="Saved Successfully"></asp:Label>
    </div>
    <asp:MultiView ID="MultiViewNAIC" runat="server">
        <asp:View ID="viewMNC" runat="server">
            <div class="container"   style="background-color: gainsboro; border-radius: 6px;">
                <div class="row">
                    <div class="col-md-4"></div>
                    <div class="col-md-4">
                        <div class="input-group" style="margin: 0 auto;">
                            <h4>Manage NAIC Codes</h4>
                        </div>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row" style="margin-top: 20px">
                            <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-md-2">
                                    <label style="margin-top: 5px;">Select NAIC Code</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlPNAIC" runat="server" CssClass="form-control" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="ddlPNAIC_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                        </div>
                        <div class="row" id="divNaic" runat="server">
                             <div class="row" style="margin-top:10px">
                                <div class="col-md-3"></div>
                                <div class="col-md-2">
                                    <label style="margin-top: 5px;">Small Business Size Limit</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtSbsl" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                            <div class="row" style="margin-top:10px">
                                <div class="col-md-3"></div>
                                <div class="col-md-2">
                                    <label style="margin-top: 5px;">Full Description</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtFullDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                            <div class="row" style="margin-top:10px">
                                <div class="col-md-3"></div>
                                <div class="col-md-2">
                                    <%--<label style="margin-top: 5px;">Description</label>--%>
                                </div>
                                <div class="col-md-4">
                                    <%--<asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                    <asp:CheckBox ID="chkNIU" runat="server" CssClass="checkbox" Text="NOT IN USE AT THIS TIME" />
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                             <div class="row" style="margin-top:10px">
                                <div class="col-md-3"></div>
                                <div class="col-md-2">
                                    <label style="margin-top: 5px;">Supplier Classification</label>
                                </div>
                                <div class="col-md-4">
                                    <asp:DropDownList ID="ddlSClassification" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Goods">Goods</asp:ListItem>
                                        <asp:ListItem Value="Construction">Construction</asp:ListItem>
                                        <asp:ListItem Value="Services">Services</asp:ListItem>
                                        <asp:ListItem Value="Professional/Technical">Professional/Technical</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                            <div class="row" style="margin-top:10px;">
                                <div class="col-md-4"></div>
                                <div class="col-md-4" style="text-align:center">
                                <asp:Label ID="lblErrorU" runat="server" Visible="false" Style="color: red;" Text="All fields are Mandatory."></asp:Label>
                                </div>
                                <div class="col-md-4"></div>
                            </div>
                            <div class="row" style="margin-top:10px; margin-bottom:20px;">
                                <div class="col-md-4"></div>
                                <div class="col-md-4" style="text-align:center">
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdate_Click" Visible="false" />
                                </div>
                                <div class="col-md-4"></div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
