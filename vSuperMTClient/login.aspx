<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="vSuperMTClient.login" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>vSupervisor-Login</title>

    <link href="styles/login.css" rel="stylesheet" />
    <link href="styles/font-awesome.min.css" rel="stylesheet" />
    <link href="styles/bootstrap.min.css" rel="stylesheet" />
    <link href="styles/components.css" rel="stylesheet" />
    <link href="styles/layout.css" rel="stylesheet" />
    <link href="styles/custom.css" rel="stylesheet" />
     <link rel="shortcut icon" href="favicon.ico" type="image/x-icon" />
    <script src="scripts/jquery-1.11.1-jquery.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>
    <script src="scripts/metronic.js"></script>
    <script src="scripts/layout.js"></script>
    <script src="scripts/jquery.cokie.min.js"></script>


    <style>
    { margin: 0; padding: 0; }
    html { 
            background: url(Content/images/BG.png) no-repeat center center fixed; 
            background-size: cover;
    }
    </style>
</head>
<body class="login">

    <div id="mainouter" style="margin-top: 150px;">
        <div class="content">
            <form id="Form1" class="login-form" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row" style="background-color:#f5f5f5;border-radius:20px !important;">
                            <div class="col-md-12" style="padding-left: 0px; padding-right: 0px">
                                <div class="row" style="text-align: center; vertical-align: middle; padding-top: 20px">
                                    <img src="Content/images/Logo.png" id="imgLogo" runat="server" height="60"/>
                                     <%--<img src="Content/images/vBoard.png" style="width: 80%" />--%>
                                </div>
                                <div class="row" style="padding-top: 15px">
                                    <div class="alert alert-danger display-hide">
                                        <button class="close" data-close="alert"></button>
                                        <span>Enter username and password. </span>
                                    </div>
                                </div>
                                <div class="row" style="">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-8">
                                        <div class="form-group" style="background-color: #edeef2;">
                                            <div class="input-icon">
                                                <i class="icon-user"></i>
                                                <input id="txbUserName" class="form-control form-control-solid placeholder-no-fix" type="text" autocomplete="off" placeholder="Username" name="username" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                </div>
                                <div class="row" style="" >
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-8">
                                        <div class="form-group" style="">
                                            <div class="input-icon">
                                                <i class="icon-lock"></i>
                                                <input id="txbPassword" class="form-control form-control-solid placeholder-no-fix" type="password" autocomplete="off" placeholder="Password" name="password" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                </div>
                                 <div class="row" style="" id="rowUFID" runat="server">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-8">
                                        <div class="form-group">

                                            <div class="input-icon">
                                                <i class="icon-cloud"></i>
                                                <input id="txbUFID" class="form-control form-control-solid placeholder-no-fix" type="text" placeholder="User ID" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                </div>
                                <div id="ErrorMessageRow" class="row" style="text-align: center; vertical-align: middle; line-height: 10px; height: 15px;display:none;" runat="server">
                                    <div class="col-md-12">
                                        <label id="lblErrorMessage" runat="server" style="color: red"></label>
                                    </div>
                                </div>
                                <div class="row" style=" text-align: center; vertical-align: middle;">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-8 login-button-group">
                                        <input type="button" id="btnLogin" onclick="Login()" class="btnFlat" value="Sign In"  style="width:212px;" runat="server"/>
                                        <%-- <asp:Button ID="btn" runat="server" OnClick="btn_Click" CssClass="btnFlat" Text="Sign In" Width="212px" />--%>
                                      <%--  <asp:ImageButton ImageUrl="Content/images/login_btn.png" ID="btn" runat="server" OnClick="lnkbtnLogin_Click" CssClass="login-button" />--%>
                                    </div>
                                    <div class="col-md-2">
                                    </div>
                                </div>
                                <div class="row" style="text-align: center; vertical-align: middle; border-radius: 0px 0px 10px 10px !important">
                                    <div class="col-md-12">
                                        <label id="lblCopyRights" runat="server" style="color: black; padding-top: 10px; padding-bottom: 20px; font-size: 11px"></label>
                                        <label id="lblVersion" runat="server" style="color: black; margin-left: 0%; font-size: 11px"></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>
        </div>
    </div>
    <script src="scripts/jquery.min.js"></script>
    <script src="scripts/bootstrap.min.js"></script>

    <script>
        function Login() {
            var UserName = $('#<%=txbUserName.ClientID%>').val(); 
            var Password = $('#<%=txbPassword.ClientID%>').val();
            var Client = $('#<%=txbUFID.ClientID%>').val();
            if (!Client) {
                Client = "";
            }
            var DatatoFunction = { UserName: UserName, Password: Password, Client: Client }
            $.ajax({
                type: "POST",
                url: "login.aspx/LogIn",
                data: JSON.stringify(DatatoFunction),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
                    if (data.d.startsWith('Supervisor')) {
                        window.location.href = "Dashboard.aspx";
                    }
                    else if (data.d.startsWith('Invalid')) {
                       $('#<%=ErrorMessageRow.ClientID%>').show();
                        $('#<%=lblErrorMessage.ClientID%>').text("Invalid Username or Password.");
                    }
                    else if (data.d.startsWith('LicenseExpired')) {
                        $('#<%=ErrorMessageRow.ClientID%>').show();
                        $('#<%=lblErrorMessage.ClientID%>').text("License Expired.");
                    }
                   
                }
            });
        }
    </script>
    <script>
        jQuery(document).ready(function () {

            Metronic.init();
            $('#Form1').bind('keypress', function(e) {
                if(e.keyCode==13){
                    Login();
                }
            });
            
           
        });
    </script>
</body>
</html>
