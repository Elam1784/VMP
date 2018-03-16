<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/vmpAdmMaster.Master" AutoEventWireup="true" CodeBehind="vmpAdmUsers.aspx.cs" Inherits="VMP_1._0.Admin.vmpAdmUsers" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable = no" />
    <link href="../Content/basic.css" rel="stylesheet" />
    <%--<script src="../Scripts/jquery-1.10.2.min.js"></script>--%>
    <script src="../js/jquery.responsivetable.min.js"></script>
    <%--<script src="../js/bootstrap.min.js"></script>--%>
    <script src="../js/bootstrapvalidator.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            setupResponsiveTables();

            // Register method to fire at the end of an asp:UpdatePanel response
            //$(window).on('load', function () {
            //    //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endUpdatePanelRequest);
            //    $('.responsiveTable1').responsiveTable();
            //});
        });

        function endUpdatePanelRequest() {
            setupResponsiveTables();
        }

        function setupResponsiveTables() {
            //if ($('#SetupResponsiveTableOnReload').is(':checked')) {
            jQuery.noConflict();
            $('.responsiveTable1').responsiveTable();
            //}
        }
    </script>
    <script>
        $(document).ready(function () {
            $("#navbar7 li").removeClass("active");//this will remove the active class from  
            //previously active menu item 
            $('#menu').addClass('active');
        });
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
        function runEffect() {
            $("#<%=divDelMsg.ClientID%>").show();
            setTimeout(function () {
                var selectedEffect = 'blind';
                var options = {};
                $("#<%=divDelMsg.ClientID%>").hide();
            }, 5000);
            return false;
        }
    </script>
    <script type="text/javascript">
        function openModal() {
            jQuery.noConflict();
            $("#myModal").modal("show");
        }
    </script>
    <script type="text/javascript">
        function fnCheck() {
            if (((document.getElementById("<%=txtFirstName.ClientID%>").value).length == 0) && ((document.getElementById("<%=txtUserName.ClientID%>").value).length == 0)) {
                document.getElementById('lblerr').innerHTML = 'First Name and UserName Cannot be empty';
                document.getElementById('lblerr').style.display = 'block';
            }
            else if ((document.getElementById("<%=txtFirstName.ClientID%>").value).length == 0) {
                // alert("The textbox should not be empty");
                document.getElementById('lblerr').innerHTML = 'First Name Cannot be empty';
                document.getElementById('lblerr').style.display = 'block';
            }
            else if ((document.getElementById("<%=txtUserName.ClientID%>").value).length == 0) {
                document.getElementById('lblerr').innerHTML = 'UserName Cannot be empty';
                document.getElementById('lblerr').style.display = 'block';
            } else {

                document.getElementById('lblerr').style.display = 'none';
                document.getElementById("<%=btnSave.ClientID%>").click();
            }
}
    </script>
    <style>
        .iconstyle {
            padding: 4px 8px !important;
        }

        .griddiv {
            height: 20px;
            margin-top: -3px;
        }

        label {
            font-weight: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div style="display: none; text-align: center" id="myMessage1" runat="server" class="alert alert-success col-sm-12">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <asp:Label ID="lblError" runat="server" Text="Saved Successfully"></asp:Label>
        </div>
    </div>
    <div class="container" style="background-color: gainsboro; border-radius: 6px;">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="container">
                        <div id="reg_form" style="width: 88%; margin: 0 auto;" role="form">
                            <div style="text-align: center;">
                                <h2>Add User</h2>
                            </div>
                            <div style="text-align: center;">
                                <label id="lblerr" style="color: red; display: none;"></label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Invalid Phone number #" ControlToValidate="txtPhoneNumber" Style="color: red; margin-left: 15px;" ValidationGroup="vd" ValidationExpression="^\s+(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$"></asp:RegularExpressionValidator>
                            </div>

                            <div class="form-group">
                                <label for="firstName" class="col-sm-3 control-label" style="text-align: right; padding-top: 5px;">First Name</label>
                                <div class="col-sm-9">
                                    <div style="margin-bottom: 20px;" class="input-group">
                                        <span class="input-group-addon iconstyle"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name" class="form-control" Style="width: 88%;"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="!" ControlToValidate="txtFirstName" ValidationGroup="adduser" ForeColor="Red" Style="padding-left: 8px;"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <%--<label id="lblerrFN" style="color: red; margin-top:-15px; display: none;">Enter First Name</label>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="firstName" class="col-sm-3 control-label" style="text-align: right; padding-top: 5px;">Last Name</label>
                                <div class="col-sm-9">
                                    <div style="margin-bottom: 20px;" class="input-group">
                                        <span class="input-group-addon iconstyle"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name" class="form-control" Style="width: 88%;"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="!" ControlToValidate="txtLastName" ValidationGroup="adduser" ForeColor="Red" Style="padding-left: 8px;"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="email" class="col-sm-3 control-label" style="text-align: right; padding-top: 5px;">Email</label>
                                <div class="col-sm-9">
                                    <div style="margin-bottom: 20px;" class="input-group">
                                        <span class="input-group-addon iconstyle"><i class="glyphicon glyphicon-envelope"></i></span>
                                        <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" class="form-control" Style="width: 88%;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="firstName" class="col-sm-3 control-label" style="text-align: right; padding-top: 5px;">UserName</label>
                                <div class="col-sm-9">
                                    <div style="margin-bottom: 20px;" class="input-group">
                                        <span class="input-group-addon iconstyle"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:TextBox ID="txtUserName" runat="server" placeholder="UserName" class="form-control" Style="width: 88%;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="Phone Number" class="col-sm-3 control-label" style="text-align: right; padding-top: 5px;">Phone Number</label>
                                <div class="col-sm-9">
                                    <div style="margin-bottom: 20px;" class="input-group">
                                        <span class="input-group-addon iconstyle"><i class="glyphicon glyphicon-earphone"></i></span>
                                        <asp:TextBox ID="txtPhoneNumber" runat="server" placeholder=" PhoneNumber" class="form-control" Style="width: 88%;" CssClass="form-control bfh-phone" data-format=" (ddd) ddd-dddd" ValidationGroup="vd"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="isAdmin" class="col-sm-3 control-label" style="text-align: right;">Is Admin?</label>
                                <div class="col-sm-6">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="radbtnYes" runat="server" Text="Yes" GroupName="CU" />
                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <label class="radio-inline">
                                                <asp:RadioButton ID="radbtnNo" runat="server" Text="No" GroupName="CU" Checked="true" />

                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-5">
                                </div>
                                <div class="col-sm-6" style="margin-bottom: 20px;">
                                    <button type="button" class="btn btn-primary" onclick="fnCheck()" style="width: 100px;">Save</button>
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="vd" OnClick="btnSave_Click" Style="width: 100px; display: none;" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="btnClear_Click" />
                                </div>
                            </div>


                        </div>
                        <!-- /form -->
                    </div>
                    <!-- ./container -->
                </div>
            </div>
        </div>

        <div class="container">
            <div style="text-align: center;">
                <h2>Users</h2>
            </div>
            <div style="display: none; text-align: center" id="divDelMsg" runat="server" class="alert alert-danger col-sm-12">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <asp:Label ID="Label1" runat="server" Text="User Deleted Successfully"></asp:Label>
            </div>
        </div>

        <div class="container" style="margin-bottom: 20px;">
            <br />
            <br />
            <asp:GridView ID="grdvwUser" runat="server" RowStyle-Wrap="false" CssClass="responsiveTable1" AutoGenerateColumns="False" Style="margin: 0 auto;" OnRowCommand="grdvwUser_RowCommand" BackColor="White">
                <Columns>
                    <asp:BoundField DataField="firstName" HeaderText="First Name" />
                    <asp:BoundField DataField="lastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="emailId" HeaderText="Email" />
                    <asp:BoundField DataField="userName" HeaderText="UserName" />
                    <asp:BoundField DataField="phoneNumber" HeaderText="PhoneNumber" />
                    <asp:BoundField DataField="isAdmin" HeaderText="Admin" />
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <div class="griddiv">
                                <asp:Button ID="btnEdit" runat="server" CausesValidation="false" CommandName="EditUser" CssClass="btn-link"
                                    Text="Edit" CommandArgument='<%# Eval("userId") %>' />
                                / 
                                            <asp:Button ID="btnDelete" runat="server" CausesValidation="false" CommandName="DeleteUser" CssClass="btn-link"
                                                Text="Delete" CommandArgument='<%# Eval("userId") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H3">Are you sure you want to delete this user?</h4>
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
    <script src="../js/bootstrap-formhelpers-phone.js"></script>
</asp:Content>
