<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/vmpAdmMaster.Master" AutoEventWireup="true" CodeBehind="vmpAdmOtherCert.aspx.cs" Inherits="VMP_1._0.Admin.vmpAdmOtherCert" EnableEventValidation="false" %>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: none; text-align: center" id="myMessage1" runat="server" class="alert alert-success col-sm-12">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <asp:Label ID="lblError" runat="server" Text="Saved Successfully"></asp:Label>
    </div>
    <script type="text/javascript">
        function showModal() {
            $("#myModal").modal('show');
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
    <script type="text/javascript">
        function fnCheck() {
            if ((document.getElementById("<%=txtOtherCertNew.ClientID%>").value).length == 0) {
                document.getElementById('lblerr').style.display = 'block';
            }
            else {
                document.getElementById('lblerr').style.display = 'none';
                document.getElementById("<%=btnSaveNewOC.ClientID%>").click();
            }
        }
    </script>   
    <div class="container">
        <%--<div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-4">
                <ul class="nav nav-pills">
                    <li role="presentation" class="active" runat="server" id="liOC">
                        <asp:LinkButton ID="lnkbtnOC" runat="server" Text="Manage Other Certification" CssClass="btn-link" OnClick="lnkbtnOC_Click"></asp:LinkButton></li>
                </ul>
            </div>
            <div class="col-md-3"></div>
        </div>--%>
    </div>
    <br />
    <br />
    <asp:MultiView ID="MultiViewOC" runat="server">
        <asp:View ID="viewOC" runat="server" OnLoad="viewOC_Load">
            <div class="container"   style="background-color: gainsboro; border-radius: 6px;">
                <div class="row">
                    <div class="col-md-4"></div>
                    <div class="col-md-4">
                        <div class="input-group" style="margin: 0 auto;">
                            <h4>Manage Other Certification</h4>
                        </div>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <div class="row">
                    <div class="col-md-4"></div>
                    <div class="col-md-4">
                        <asp:GridView ID="grdOtherCert" runat="server" CssClass="table1" AutoGenerateColumns="false" OnRowDataBound="grdOtherCert_RowDataBound" OnSelectedIndexChanged="grdOtherCert_SelectedIndexChanged" BackColor="White" DataKeyNames="OtherDBETypeID" HeaderStyle-Font-Bold="true" HeaderStyle-BackColor="Gray" HeaderStyle-ForeColor="White" Style="width: 100%;">
                            <Columns>
                                <asp:BoundField DataField="OtherDBEType" HeaderText="Other Certification" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="OtherDBETypeID" HeaderText="Other Certification" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblErrorC" runat="server" Text="No Other Certification Found" Visible="false" Style="color: red;"></asp:Label>
                    </div>
                    <div class="col-md-4"></div>
                </div>
                <br />
                <div class="row" style="margin-bottom:20px;">
                    <div class="col-md-4"></div>
                    <div class="col-md-4" style="text-align: center;">
                        <asp:Button ID="btnUpdateOC" runat="server" Text="Update" Visible="false" CssClass="btn btn-primary" OnClick="btnUpdateOC_Click" Style="margin-left: 3px;" />
                        <a href="#" onclick="$('#myModalNew').modal({'backdrop': 'static'});" class="btn btn-primary" style="margin-left: 3px;">New</a>
                    </div>
                    <div class="col-md-4"></div>
                </div>
            </div>
            <div class="modal fade" id="myModal" role="dialog" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Update Other Certification</h4>
                        </div>
                        <div class="modal-body">
                            <label>Other Certification Type</label>
                            <div class="input-group" style="width: 100%; margin: 0 auto; padding-bottom: 10px; text-align: center; margin-left: -30px;">
                                <asp:TextBox ID="txtOtherCert" runat="server" class="form-control" TextMode="MultiLine" Style="width: 90%; border-top-right-radius: 4px; border-bottom-right-radius: 4px; height: 90px;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSaveOC" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="cp" OnClick="btnSaveOC_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="myModalNew" role="dialog" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">New Other Certification Type</h4>
                        </div>
                        <div class="modal-body">
                            <label>Other Certification Description</label>
                            <div class="input-group" style="width: 100%; margin: 0 auto; padding-bottom: 10px; text-align: center; margin-left: -30px;">
                                <asp:TextBox ID="txtOtherCertNew" runat="server" class="form-control" TextMode="MultiLine" Style="width: 90%; border-top-right-radius: 4px; border-bottom-right-radius: 4px; height: 90px;"></asp:TextBox>
                                <label id="lblerr" style="color: red; display: none;">Textbox cannot be empty</label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" onclick="fnCheck()">Save</button>
                            <asp:Button ID="btnSaveNewOC" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnSaveNewOC_Click" Style="display: none;" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
