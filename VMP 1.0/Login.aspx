<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="VMP_1._0.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VMP</title>
    <link runat="server" rel="shortcut icon" href="images/faviconLogo.ico" type="image/x-icon" />
    <link runat="server" rel="icon" href="images/faviconLogo.ico" type="image/ico" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge">
    <script type="text/javascript">
        history.pushState(null, null, document.URL);
        window.addEventListener('popstate', function () {
            history.pushState(null, null, document.URL);
        });
    </script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <style class="cp-pen-styles">
        .navbar-brand {
            padding: 0px;
            margin: 0px 20px 0px 10px;
        }

        .brand-centered {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-pack: center;
            -ms-flex-pack: center;
            justify-content: center;
            position: absolute;
            width: 100%;
            left: 0;
            top: 0;
        }

            .brand-centered .navbar-brand {
                display: -webkit-box;
                display: -ms-flexbox;
                display: flex;
                -webkit-box-align: center;
                -ms-flex-align: center;
                align-items: center;
            }

        .navbar-alignit .navbar-header {
            -webkit-transform-style: preserve-3d;
            transform-style: preserve-3d;
            height: 50px;
        }

        .navbar-alignit .navbar-brand {
            top: 50%;
            display: block;
            position: relative;
            height: auto;
            -webkit-transform: translate(0,-50%);
            transform: translate(0,-50%);
            margin-right: 15px;
            margin-left: 15px;
        }
    </style>
    <style>
        .loader {
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            width: 120px;
            height: 120px;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        @-webkit-keyframes spin {
            0% {
                -webkit-transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(360deg);
            }
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <nav class="navbar navbar-default">
                <div class="container-fluid">
                    <div class="navbar-header">
                        <img class="navbar-brand" src="images/Logo.ico" alt="Vendor Management" />
                    </div>
                    <div id="navbar7" class="navbar-collapse collapse">
                    </div>
                </div>
            </nav>
        </div>
        <div style="margin-top: 15%;">
            <div style="text-align: center;">
                <h3>Authentication in Progress....</h3>
            </div>
            <div class="loader" style="margin: 0 auto;"></div>
            <div style="text-align: center;">
                <h4>Logging in as <%=User.Identity.Name.Split('\\')[1].ToString()%></h4>
            </div>
        </div>
    </form>
</body>
</html>
