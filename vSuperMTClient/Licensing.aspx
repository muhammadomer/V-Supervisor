<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisor.Master" AutoEventWireup="true" CodeBehind="Licensing.aspx.cs" Inherits="vSuperMTClient.Licensing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-6" id="divGenerateLicenseCode">
            <div style="border-style: ridge; border-color: #6b696a; border-width: 1px 1px 1px 1px; height: auto;">
                <div class="row" style="width: 100%; background-color: #6b696a; text-align: left; vertical-align: middle; line-height: 40px;">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="">
                        <span style="font-size: 18px; color: #fffeff;">License Code</span>
                    </div>
                </div>
                <div class="row" style="margin-top: 20px; margin-bottom: 22px; text-align: right;">
                    <div class=" col-xs-12 col-sm-12 col-md-12">
                        <asp:DropDownList ID="cboLicense" runat="server" Width="100%" Height="36px" Style="border: 1px solid #e5e5e5;">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="margin-top: 20px; margin-bottom: 22px; text-align: right; display: none" id="trQuantity">
                    <div class=" col-xs-12 col-sm-12 col-md-7">
                        <asp:DropDownList ID="cboQuantity" runat="server" Width="100%" Height="36px" Style="border: 1px solid #e5e5e5;">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-5"></div>
                </div>



                <div class="row" style="margin-top: 20px; margin-bottom: 22px; text-align: right;">
                    <div class="col-md-7">
                        <button type="button" class="btnFlat" data-dismiss="modal" onclick="AddLicense()" style="width: 100%;">Add License Code</button>
                    </div>
                    <div class="col-md-5"></div>
                </div>
                <div class="row" style="margin-top: 20px; margin-bottom: 22px;" id="divGridLicense">
                    <div class=" col-xs-12 col-sm-12 col-md-12">
                        <table id='tblLicenses' class='table table-striped table-hover table-bordered'>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row" style="margin-top: 20px; margin-bottom: 22px; text-align: right; display: none;" id="divGenerateLicense">
                    <div class=" col-xs-12 col-sm-12 col-md-7">
                        <asp:Button ID="btnCreateLicenseKey" OnClick="btnCreateLicenseKey_Click" ClientIDMode="Static" runat="server" Text="Generate License Code" Width="100%" CssClass="btnFlat" OnClientClick="DownloadFileComplete();" />
                    </div>
                    <div class="col-md-5"></div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" id="divApplyLicenseCode">
            <div style="border-style: ridge; border-color: #6b696a; border-width: 1px 1px 1px 1px; height: 120px;">
                <div class="row" style="width: 100%; background-color: #6b696a; text-align: left; vertical-align: middle; line-height: 40px;">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="">
                        <span style="font-size: 18px; color: #fffeff;">License Key</span>
                    </div>
                </div>
                <div class="row" style="margin-top: 25px;">
                    <div class="col-xs-12 col-sm-12 col-md-6" style="cursor: pointer;">
                        <act:AsyncFileUpload ID="AsyncFileUpload1" runat="server" OnClientUploadError="uploadError"
                            OnClientUploadStarted="StartUpload" OnClientUploadComplete="UploadComplete" CompleteBackColor="Lime"
                            UploaderStyle="Modern" BackColor="Red" ThrobberID="Throbber" OnUploadedComplete="AsyncFileUpload1_UploadedComplete"
                            UploadingBackColor="#66CCFF" CssClass="FileUploadClass" Style="position: absolute; left: -10px; z-index: 2; opacity: 0; filter: alpha(opacity=0); cursor: pointer" />
                        <asp:Button ID="BtnFsAttch" runat="server" Text="Apply License Key" Style="position: absolute; top: -5px; left: 10px; z-index: 1; cursor: pointer" CssClass="btnFlat" Width="100%" />
                        <asp:Label ID="lblStatus" runat="server" class="form-control Label"></asp:Label>
                    </div>
                    <div class=" col-xs-12 col-sm-12 col-md-3" style="visibility: hidden;">
                        <asp:Button ID="btnShow" OnClick="btnShow_Click" runat="server" class="form-control Button" Text="Show"></asp:Button>
                    </div>
                    <div class=" col-xs-12 col-sm-12 col-md-3"></div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div id="divAvaliableLicenses" class="col-xs-12 col-sm-12 col-md-6 col-lg-6" style="margin-top: 10px;">

            <div style="border-style: ridge; border-color: #6b696a; border-width: 1px 1px 1px 1px;">
                <div class="row" style="width: 100%; background-color: #6b696a; text-align: left; vertical-align: middle; line-height: 40px;">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="">
                        <span style="font-size: 18px; color: #fffeff;">Available Licenses</span>
                    </div>
                </div>
                <div class="row" style="margin-top: 0%; width: 100%;">
                    <div id="grdContainer" class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="padding: 10px;">
                        <table id='tblAvailableLicenses' class='table table-striped table-hover table-bordered'>
                            <tbody>
                            </tbody>
                        </table>
                    </div>

                </div>

            </div>
        </div>
    </div>


    <script>
        function notifyMe(msgType, msg) {
            $().toastmessage(msgType, msg);
        }
        function DownloadFileComplete()
        {
            setTimeout(function () {
                $("#divGridLicense").hide();
                $("#divGenerateLicense").hide();
                $("#trQuantity").hide();
                document.getElementById('<%=cboLicense.ClientID %>').selectedIndex = 0;
                document.getElementById('<%=cboQuantity.ClientID %>').selectedIndex = 0;
            }, 1000);
        }
        function btnFileUpload_Click()
        {
            document.getElementById('<%=AsyncFileUpload1.ClientID %>').click();
        }
        function uploadError(sender, args) {
            document.getElementById('<%=lblStatus.ClientID %>').innerText = args.get_fileName(),
	        "<span style='color:red;'>" + args.get_errorMessage() + "</span>";
        }
        function StartUpload(sender, args) {
            var filename = args.get_fileName();
            var stringCheck = ".lic";
            var foundIt = (filename.lastIndexOf(stringCheck) === filename.length - stringCheck.length) > 0;
            if (foundIt) {
                document.getElementById('<%=lblStatus.ClientID %>').innerText = 'Uploading Started.';
            }  
        }
        function UploadComplete(sender, args) {
            var filename = args.get_fileName();
            var contentType = args.get_contentType();
            var text = "Size of " + filename + " is " + args.get_length() + " bytes";
            if (contentType.length > 0) {
                text += " and content type is '" + contentType + "'.";
            }
            document.getElementById('<%=lblStatus.ClientID %>').innerText = text;
            
            __doPostBack('<%= btnShow.UniqueID %>', '');
        }
        function ShowLicensingForCloudUser() {
            if (<%=ShowLicensing%> ==1) {
                $("#divGenerateLicenseCode").show();
                $("#divApplyLicenseCode").show();
                
            }
            else{
                $("#divGenerateLicenseCode").hide();
                $("#divApplyLicenseCode").hide();
            }
        }
        function OnFieldNameChange() {
            var dropdownIndex = document.getElementById('<%=cboLicense.ClientID %>').selectedIndex;
            var dropdownValue = document.getElementById('<%=cboLicense.ClientID %>')[dropdownIndex].text;
            if (dropdownValue.indexOf("Client") != -1 ) {
                $("#trQuantity").show();
            }
            else {
                $("#trQuantity").hide();
                
            }
        }
        function GetAvailableLicenses() {
            $('#tblAvailableLicenses').empty();
            $.ajax(
            {
                type: "POST",
                url: "Licensing.aspx/GetAvailableLicenses",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var trHTML = '<thead><tr><th>License</th><th>Description</th><th>Status</th></tr></thead><tbody></tbody>';
                    $('#tblAvailableLicenses').append(trHTML);
                    $('#divAvaliableLicenses').show();
                    if (data.d.length > 0) {
                        for (var i = 0; i < data.d.length; i++) {
                            var trHTML = '';
                            trHTML += '<tr><td>' + data.d[i].License + '</td>';
                            trHTML += '<td>' + data.d[i].Description + '</td>';
                            trHTML += '<td>' + data.d[i].Status + '</td></tr>';
                            $('#tblAvailableLicenses').append(trHTML);


                        }
                    }
                }
            });
        }
        function AddLicense() {

            <%--var dropdownIndex = document.getElementById('<%=cboLicense.ClientID %>').selectedIndex;
            var dropdownValue = document.getElementById('<%=cboLicense.ClientID %>')[dropdownIndex].text;--%>
            if ($("#<%=cboLicense.ClientID%>").get(0).selectedIndex == 0) {
                notifyMe('showErrorToast', 'Please select license.');
                return;
            }
            
           <%-- else if (dropdownValue.indexOf("Client") != -1  && $("#<%=cboQuantity.ClientID%>").get(0).selectedIndex == 0) {
                notifyMe('showErrorToast', 'Please select quantity.');
                return;
            }--%>

            var Lic = $("#<%=cboLicense.ClientID%>").val();
            var Quantity = $("#<%=cboQuantity.ClientID%>").val();
            var LicText = $('#<%=cboLicense.ClientID %> option:selected').text()
            var Param = { "Lic": Lic, "Quantity": Quantity, "LicText": LicText };
            $('#tblLicenses').empty();
            $.ajax(
            {
                type: "POST",
                url: "Licensing.aspx/AddLicenses",
                data: JSON.stringify(Param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d.length > 0) {
                        var trHTML = '<thead><tr><th>Module</th><th>Quantity</th><th>TimePeriod</th></tr></thead><tbody></tbody>';
                        $('#tblLicenses').append(trHTML);
                        for (var i = 0; i < data.d.length; i++) {
                            var trHTML = '';
                            trHTML += '<tr><td>' + data.d[i].Module + '</td>';
                            trHTML += '<td>' + data.d[i].Quantity + '</td>';
                            trHTML += '<td>' + data.d[i].TimePeriod + '</td></tr>';
                            $('#tblLicenses').append(trHTML);
                            $('#divGenerateLicense').show();
                            $('#divGridLicense').show();

                        }
                    }
                }
            });
        }

        $(document).ready(function () {
            $("#HeaderLinkLicensing").removeClass("DeactiveHeaderLink").addClass("ActiveHeaderLink");
            GetAvailableLicenses();
            $("#linkLicensing").addClass("active open");


            ShowLicensingForCloudUser();
        });
    </script>

</asp:Content>
