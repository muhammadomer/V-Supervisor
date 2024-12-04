<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisor.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="vSuperMTClient.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="jsapp/systemapp.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="padding: 0px;">
        <div class="row" style="margin-top: 0%; width: 100%;">
            <div class="row" style="width: 100%;">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="">
                    <span style="font-size: 26px; color: #999999; margin-left: 0%;">User Profiles</span>
                </div>
            </div>
            <div class="row" style="padding-top: 10px; padding-bottom: 20px; width: 100%;">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-1s2">
                    <div style="background-color: white; text-align: left; vertical-align: middle; line-height: 40px;">
                        <i class="icon-home" style="font-size: 20px; color: #999999; padding-left: 1%;"></i>
                        <a href="ACDPorts.aspx">
                            <span style="color: #999999;">Home</span>
                        </a>
                        <img src="Content/images/next_image.png" style="margin-top: -3px;" />
                        <a href="users.aspx">
                            <span style="color: #999999;">User Profiles</span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <div style="width: 100%; border-style: ridge; border-color: #4d5b69; border-width: 1px 1px 1px 1px;">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="text-align: left; vertical-align: middle; line-height: 41px; background-color: #4d5b69;">
                            <span style="font-size: 18px; color: #fffeff;">User Profiles</span>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 0%; width: 100%;">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="padding: 10px;">
                            <div style="text-align: left; margin-bottom: 20px; margin-top: 7px;">
                                <a onclick="UserAddModal();" class="btnFlat">Add New <i class='icon-plus'></i></a>
                            </div>

                            <table class="table table-striped table-bordered table-hover" id="tblUser" >

                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="UserModal" tabindex="-1" role="basic" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content" style="width: 640px;">
                    <div class="row" style="line-height: 46px; background-color: #ffffff; width: 100%; margin-left: 0%; vertical-align: middle; border-bottom: 1px solid #e5e5e5">
                        <div class="col-md-4" style="margin-top: 1%;">
                            <span id="ModalHeader" style="font-size: 18px; color: #404040;">Add User</span>
                        </div>
                    </div>
                    <div id="ErrorDivAdd" class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6; display: none;">
                        <div class="col-md-12" style="padding-left: 0px;">
                            <label class="form-control Label" style="color: red" id="lblErrorAdd"></label>
                        </div>
                        <input id="txbUserId" type="text" class="col-md-12 form-control" style="display: none;" />
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label" style="padding-left: 10px">First Name</label>
                        </div>
                        <div class="col-md-9">
                            <input type="text" id="txbFirstName" class="form-control" placeholder="" maxlength="50" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label" style="padding-left: 10px">Last Name</label>
                        </div>
                        <div class="col-md-9">
                            <input type="text" id="txbLastName" class="form-control" placeholder="" maxlength="50" />
                        </div>
                    </div>

                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label" style="padding-left: 10px">User Name</label>
                        </div>
                        <div class="col-md-9">
                            <input type="text" id="txbUserName" class="form-control" placeholder="" maxlength="50" />
                        </div>
                    </div>

                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label" style="padding-left: 10px">Password</label>
                        </div>
                        <div class="col-md-9">
                            <input id="txbPassword" type="password" class="form-control" placeholder="" maxlength="50" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label" style="padding-left: 10px">User Type</label>
                        </div>
                        <div class="col-md-9" style="padding-top: 5px;">

                            <fieldset>
                                <input type="radio" name="optradio" id="chkUserTypeAdmin" value="Admin" checked="checked">
                                <label style="padding-right: 15px;">Admin</label>
                                <input type="radio" name="optradio" id="chkUserTypeSupervisor" value="Supervisor">
                                <label style="padding-right: 15px;">Supervisor</label>
                                <input type="radio" name="optradio" id="chkUserTypeDashboard" value="Dashboard">
                                <label style="">Dashboard</label>

                            </fieldset>
                            <%--   <input type="checkbox" id="chkforDashboard" class="make-switch" data-on-text="Yes" data-off-text="No" data-on-color="success" data-off-color="danger" onchange="checkForDashboard()" />--%>
                        </div>
                    </div>

                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;display:none">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label" style="padding-left: 10px">User Access</label>
                        </div>
                        <div class="col-md-9">
                            <select id="ddlModule" class="form-control" multiple="multiple">
                                <%foreach (vSuperMTClient.Entities.Modules Obj in ListModuleObj)
                                    {%>
                                <option value="<%=(Obj.moduleID.ToString())%>"><%=(Obj.Name.ToString())%></option>
                                <%}%>
                            </select>
                        </div>
                    </div>
                     <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                        <div class="col-md-6" style="width: 472px;">
                        </div>
                        <div  id="divAddUser" class="col-md-3" style="width: 80px;">

                            <button type="button" onclick="AddUsers();" id="btnAddUser" class="btnFlat">Apply</button>
                        </div>
                          <div id="divUpdateUser" class="col-md-3" style="width: 80px;">

                            <button type="button" onclick="UpdateUsers();" id="btnUpdateUser" class="btnFlat">Apply</button>
                        </div>
                          <div class="col-md-3" style="width: 80px;">
                              <button type="button" class="btnFlat" data-dismiss="modal" >Close</button>
                           
                        </div>
               
                    </div>
               <%--     <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                        <div class="col-md-7" style="padding: 0px;">
                        </div>
                        <div class="col-md-5">
                            <div id="divAddUser" class="col-md-6" style="text-align: right; padding: 0px;">
                                <button type="button" onclick="AddUsers();" id="btnAddUser" class="btnFlat" style="width: 70px;">Apply</button>

                            </div> 
                            <div id="divUpdateUser" class="col-md-6" style="text-align: right; padding-right: 0px;">
                                <button type="button" onclick="UpdateUsers();" id="btnUpdateUser" class="btnFlat" style="width: 70px;">Apply</button>
                            </div>
                            <div class="col-md-6" style="text-align: right; padding-right: 0px;">
                                <button type="button" class="btnFlat" data-dismiss="modal" style="width: 70px;">Close</button>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
    <script>
        function GetUsers() {
            $('#tblUser').empty();
            $.ajax({
                type: "POST",
                url: "Users.aspx/GetAllUsers",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var trHTML = '<thead><tr><th>First Name</th><th>Last Name</th><th>User Name</th><th>User Type</th><th>Edit</th><th>Delete</th></tr></thead><tbody></tbody>';
                    $('#tblUser').append(trHTML);
                    for (var i = 0; i < data.d.length; i++) {
                        var trHTML = '';
                        trHTML += '<tr><td>' + data.d[i].firstName + '</td>';
                        trHTML += '<td>' + data.d[i].lastName + '</td>';
                        trHTML += '<td>' + data.d[i].username + '</td>';
                        //var DashBoardUser = "";
                        //if (data.d[i].userType=="Dashboard") {
                        //    DashBoardUser = "Yes";
                        //}
                        //else {
                        //    DashBoardUser = "No";
                        //}
                        //trHTML += '<td>' + DashBoardUser + '</td>';
                        trHTML += '<td>' + data.d[i].userType + '</td>';
                        trHTML += "<td><a class='edit'  onclick='UserEditInfo(" + data.d[i].UserId + ")';>Edit</a></td>";
                        trHTML += "<td><a onclick='DeleteUser(" + data.d[i].UserId + ")';> Delete</a></td></tr>";
                        $('#tblUser').append(trHTML);
                    }
                  
                }
            });
        }
        function UserEditInfo(UserId) {
            $("#ModalHeader").text("Edit User");
            
            $("#ddlModule option:selected").removeAttr("selected");
            var Param = { "UserId": UserId };
            $.ajax({
                type: "POST",
                url: "Users.aspx/GetUserOnId",
                data: JSON.stringify(Param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var PermissionsArray = new Array();
                    if (data.d.Permissions == null)
                    {

                    }
                    else
                    {
                        for (var i = 0; i < data.d.Permissions.length; i++)
                        {
                            PermissionsArray[i] = data.d.Permissions[i];
                        }
                    }
                    $('#txbUserId').val(UserId);
                    $('#txbFirstName').val(data.d.firstName);
                    $('#txbLastName').val(data.d.lastName);
                    $('#txbUserName').val(data.d.username);
                    $('#txbUserName').attr("disabled", true)
                    $('#txbPassword').val(data.d.password);

                    $("#ddlModule").val(PermissionsArray);
                    $("#ddlModule").multiselect("refresh");
                    $('#divAddUser').hide();
                    
                   
                    if (data.d.userType == "Admin") {
                       
                        $('#chkUserTypeAdmin').prop('checked', true).uniform('refresh');
                        $('#ddlModule').multiselect('enable');
                       
                    }
                    else if (data.d.userType == "Supervisor") {
                      
                        $('#chkUserTypeSupervisor').prop('checked', true).uniform('refresh');
                        $('#ddlModule').multiselect('disable');
                        
                    }
                    else if (data.d.userType == "Dashboard") {
                        $('#chkUserTypeDashboard').prop('checked', true).uniform('refresh');
                        $('#ddlModule').multiselect('disable');
                    }
                    $.uniform.update();
                    
                    $('#divUpdateUser').show();
                    $('#UserModal').modal('show');

                }
            });
        }
        function AddUsers() {

            var UserId = 0;
            var username = $('#txbUserName').val();
            var firstName = $('#txbFirstName').val();
            var lastName = $('#txbLastName').val();
            var password = $('#txbPassword').val();

            var UserPermissions = new Array();
            var permissions = $('#ddlModule option');
            // give all permissions 
            //var permissions = $('#ddlModule option:selected');
            $(permissions).each(function () {
               
                UserPermissions.push($(this).val());
            });

            if (username == "") {

                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide UserName');
                return;
            }
            if (firstName == "") {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide FirstName');
                return;
            }
            if (lastName == "") {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide LastName');
                return;
            }
            if (password == "") {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide Password');
                return;
            }
            var userType = $("input[type='radio']:checked").val();
            //var isDashboard = $('#chkforDashboard').is(":checked");




            var Param = { "UserId": UserId, "username": username, "firstName": firstName, "lastName": lastName, "password": password, "userType": userType, "Permissions": UserPermissions };

            $.ajax({
                type: "POST",
                url: "Users.aspx/InsertIntoUsers",
                data: "{UsersEntityObj:" + JSON.stringify(Param) + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d === true) {

                        $().toastmessage("showSuccessToast", "User added sucessfully.");
                        $('#UserModal').modal('hide');
                        GetUsers();
                    }
                    else {
                        $('#UserModal').modal('hide');
                        $().toastmessage("showErrorToast", "Failed to add user.");
                    }
                }
            });
        }
        function UpdateUsers() {
            var UserId = $('#txbUserId').val();
            var username = $('#txbUserName').val();
            var firstName = $('#txbFirstName').val();
            var lastName = $('#txbLastName').val();
            var password = $('#txbPassword').val();
            var UserPermissions = new Array();
            var permissions = $('#ddlModule option:selected');
            $(permissions).each(function (index, permissions) {
                UserPermissions.push($(this).val());
            });
            if (username == "") {

                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide Username');
                return;
            }
            if (firstName == "") {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide Firstname');
                return;
            }
            if (lastName == "") {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide Lastname');
                return;
            }
            if (password == "") {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide Password');
                return;
            }
            var userType = $("input[type='radio']:checked").val();
            //var isDashboard = $('#chkforDashboard').is(":checked");

            //if (isDashboard == true) {
            //    isDashboard = "1";

            //}
            //else if (isDashboard == false) {
            //    isDashboard = "0";

            //}

            var Param = { "UserId": UserId, "username": username, "firstName": firstName, "lastName": lastName, "password": password, "userType": userType, "Permissions": UserPermissions };

            $.ajax({
                type: "POST",
                url: "Users.aspx/UpdateIntoUsers",
                data: "{UsersEntityObj:" + JSON.stringify(Param) + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d === true) {

                        $().toastmessage("showSuccessToast", "User updated sucessfully.");

                        $('#UserModal').modal('hide');
                        GetUsers();
                    }
                    else {
                        $('#UserModal').modal('hide');
                        $().toastmessage("showErrorToast", "Failed to update user.");
                    }
                }
            });
        }
        function DeleteUser(UserId) {

            var Param = { "UserId": UserId }
            var ConfirmDelete = confirm("Are you sure you want to delete?");
            if (ConfirmDelete == false) {
                return;
            }
            $.ajax({
                type: "POST",
                url: "Users.aspx/DeleteFromUsers",
                data: JSON.stringify(Param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d === true) {
                        GetUsers();
                        $().toastmessage("showSuccessToast", "User deleted successfully.");
                    }
                    else {
                        $().toastmessage("showErrorToast", "Failed to delete user.");
                    }
                }
            });
        }
        function UserAddModal() {

            $("#ModalHeader").text("Add User");
            $('#txbFirstName').val('');
            $('#txbLastName').val('');
            $('#txbUserName').val('');
            $('#txbPassword').val('');
            $('#divUpdateUser').hide();
            $('#txbUserName').attr("disabled", false)
            $('#txbUserName').removeAttr("readonly");
            $('#divAddUser').show();
            $('#UserModal').modal('show');
            $('#ErrorDivAdd').hide();
            $('#lblErrorAdd').text('');
            $("#ddlModule option:selected").removeAttr("selected");
            Permissionlist();



            //$('#chkforDashboard').bootstrapSwitch('state', false);

            $('#ddlModule').multiselect('enable');
        }
        function Permissionlist() {

            $('#ddlModule').multiselect({
                includeSelectAllOption: true,
                selectAllValue: 'multiselect-all',
                enableCaseInsensitiveFiltering: true,
                enableFiltering: true,
                maxHeight: '300',
                buttonWidth: '300',
                onSelectAll: function () {
                    $.uniform.update();
                },

            });
            $("#ddlModule").multiselect('updateButtonText');
            //$("#ddlModule option:selected").removeAttr("selected");
            $('#ddlModule').multiselect('refresh');


        }
        $(document).ready(function () {
            GetUsers();
            Permissionlist();
            $("#linkUsers").addClass("active open");
            $(document).ready(function () {
                $('input[type=radio][name=optradio]').change(function () {
                    if (this.value == 'Admin') {

                        $('#ddlModule').multiselect('enable');
                    }
                    else if (this.value == 'Supervisor' || this.value == 'Dashboard') {
                        $('#ddlModule').multiselect('disable');
                    }
                });
            });



        });

    </script>
</asp:Content>
