<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisor.Master" AutoEventWireup="true" CodeBehind="Costing.aspx.cs" Inherits="vSuperMTClient.Costing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="padding: 0px;">
        <div class="row" style="margin-top: 0%; width: 100%;">
            <div class="row" style="width: 100%;">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="">
                    <span style="font-size: 26px; color: #999999; margin-left: 0%;">Costing</span>
                </div>
            </div>
            <div class="row" style="padding-top: 10px; padding-bottom: 20px; width: 100%;">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-1s2">
                    <div style="background-color: white; text-align: left; vertical-align: middle; line-height: 40px;">
                        <i class="icon-home" style="font-size: 20px; color: #999999; padding-left: 1%;"></i>
                        <a href="Home.aspx">
                            <span style="color: #999999;">Home</span>
                        </a>
                        <img src="../Content/images/next_image.png" style="margin-top: -3px;" />
                        <a href="Costing.aspx">
                            <span style="color: #999999;">Costing</span>
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
                            <span style="font-size: 18px; color: #fffeff;">Costing</span>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 0%; width: 100%;">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="padding: 10px;">
                            <div style="text-align: left; margin-bottom: 20px; margin-top: 7px;">
                                <a onclick="CostingAddModal();" class="btnFlat">Add New <i class='icon-plus'></i></a>
                            </div>

                            <table class="table table-striped table-bordered table-hover" id="tblCosting">
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="UpdateCostingModal" tabindex="-1" role="basic" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content" style="width: 640px;">
                    <div class="row" style="line-height: 46px; background-color: #ffffff; width: 100%; margin-left: 0%; vertical-align: middle; border-bottom: 1px solid #e5e5e5">
                        <div class="col-md-4" style="margin-top: 1%;">
                            <span style="font-size: 18px; color: #404040;">Costing</span>
                        </div>
                    </div>
                    <div id="ErrorDivEdit" class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6; display: none;">
                        <div class="col-md-12" style="padding-left: 0px;">
                            <label class="form-control Label" style="color: red" id="lblErrorEdit"></label>
                            <input id="txbEditCostingId" type="text" class="form-control" style="display: none;" />

                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label " style="padding-left: 10px">Cost Type<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-md-9">
                            <input id="txbEditCostType" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label " style="padding-left: 10px">Cost Per Sec<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-md-9">
                            <input id="txbEditCostPerSec" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label " style="padding-left: 10px">Connect Cost<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-md-9">
                            <input id="txbEditConnectCost" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label " style="padding-left: 10px">Call Setup Cost<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-md-9">
                            <input id="txbEditCallSetupCost" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6;">

                        <div class="col-md-3">
                            <label class=" control-label">DialNumber</label>
                        </div>
                        <div class="col-md-9">
                            <div class="input-group">
                                <input id="txbEditCostingDialNumber" type="text" class="form-control" style="border-bottom: 0px !important;" />
                                <span class="input-group-btn" style="vertical-align: bottom">
                                    <button type="button" id="btnEditCostingAddDialNumber" class="btn green" onclick="btnEditCostingAddDialNumber_Click();" style="padding: 7px 14px;"><i class="icon-plus-sign"></i></button>

                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-9">
                            <div class="input-group">
                                <select id="lstEditCostingDialNumbers" class="form-control" multiple="multiple" style="width: 100%;">
                                </select>
                                <span class="input-group-btn" style="vertical-align: bottom">
                                    <button type="button" id="btnEditCostingRemoveDialNumber" class="btn green" onclick="btnEditCostingRemoveDialNumber_Click();" style="padding: 7px 14px;"><i class="icon-trash"></i></button>
                                </span>
                            </div>
                        </div>

                    </div>


                    <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                        <div class="col-md-6" style="width: 472px;">
                        </div>
                        <div class="col-md-3" style="width: 80px;">
                            <button type="button" onclick="UpdateCosting();" id="btnEditCosting" class="btnFlat">Apply</button>
                        </div>
                        <div class="col-md-3" style="width: 80px;">
                            <button type="button" class="btnFlat" data-dismiss="modal">Close</button>
                        </div>
                      
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="AddCostingModal" tabindex="-1" role="basic" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content" style="width: 640px;">
                    <div class="row" style="line-height: 46px; background-color: #ffffff; width: 100%; margin-left: 0%; vertical-align: middle; border-bottom: 1px solid #e5e5e5">
                        <div class="col-md-4" style="margin-top: 1%;">
                            <span style="font-size: 18px; color: #404040;">Costing</span>
                        </div>
                    </div>
                    <div id="ErrorDivAdd" class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6; display: none;">
                        <div class="col-md-12" style="padding-left: 0px;">
                            <label class="form-control Label" style="color: red" id="lblErrorAdd"></label>
                            <input id="txbAddCostingId" type="text" class="form-control" style="display: none;" />

                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label " style="padding-left: 10px">Cost Type<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-md-9">
                            <input id="txbAddCostType" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label " style="padding-left: 10px">Cost Per Sec<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-md-9">
                            <input id="txbAddCostPerSec" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label " style="padding-left: 10px">Connect Cost<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-md-9">
                            <input id="txbAddConnectCost" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label " style="padding-left: 10px">Call Setup Cost<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-md-9">
                            <input id="txbAddCallSetupCost" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6;">

                        <div class="col-md-3">
                            <label class=" control-label">DialNumber</label>
                        </div>
                        <div class="col-md-9">
                            <div class="input-group">
                                <input id="txbAddCostingDialNumber" type="text" class="form-control" style="border-bottom: 0px !important;" />
                                <span class="input-group-btn" style="vertical-align: bottom">
                                    <button type="button" id="btnAddCostingAddDialNumber" class="btn green" onclick="btnAddCostingAddDialNumber_Click();" style="padding: 7px 14px;"><i class="icon-plus-sign"></i></button>

                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-9">
                            <div class="input-group">
                                <select id="lstAddCostingDialNumbers" class="form-control" multiple="multiple" style="width: 100%;">
                                </select>
                                <span class="input-group-btn" style="vertical-align: bottom">
                                    <button type="button" id="btnAddCostingRemoveDialNumber" class="btn green" onclick="btnAddCostingRemoveDialNumber_Click();" style="padding: 7px 14px;"><i class="icon-trash"></i></button>
                                </span>
                            </div>
                        </div>

                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                        <div class="col-md-6" style="width: 472px;">
                        </div>
                    
                             <div class="col-md-3" style="width: 80px;">
                                <button type="button" onclick="InsertCosting();" id="btnAddCosting" class="btnFlat" style="">Apply</button>
                            </div>
                               <div class="col-md-3" style="width: 80px;">
                                <button type="button" class="btnFlat" data-dismiss="modal" style="">Close</button>
                            </div>
                      
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function isValidDialNumber(DialNumber) {
            return true;
        }
        function CostingAddModal() {
            $('#AddCostingModal').modal('show');
        }
        function btnAddCostingAddDialNumber_Click() {
            $('#ErrorDivAdd').hide();
            var DialNumber = $('#txbAddCostingDialNumber').val();
            if (isValidDialNumber(DialNumber)) {
                $('#lstAddCostingDialNumbers').append($('<option>', { value: DialNumber, text: DialNumber }));
                $('#txbAddCostingDialNumber').val("");
            }
            else {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide valid Dial Number');
                return;
            }
        }
        function btnAddCostingRemoveDialNumber_Click() {
            $('#lstAddCostingDialNumbers option:selected').remove();
        }
        function btnEditCostingAddDialNumber_Click() {
            $('#ErrorDivEdit').hide();
            var DialNumber = $('#txbEditCostingDialNumber').val();
            if (isValidDialNumber(DialNumber)) {
                $('#lstEditCostingDialNumbers').append($('<option>', { value: DialNumber, text: DialNumber }));
                $('#txbEditCostingDialNumber').val("");
            }
            else {
                $('#ErrorDivEdit').show();
                $('#lblErrorEdit').text('Please provide valid Dial Number');
                return;
            }
        }
        function InsertCosting() {
            var CostingExists = 0;
            var Id = 0;
            var CostType = $('#txbAddCostType').val();
            if (CostType == "") {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide Costing Type');
                return;

            }

            var Param = { "CostingId": Id, "CostType": CostType };

            $.ajax({
                type: "POST",
                url: "Costing.aspx/CheckIfCostingNameAlreadyExists",
                data: JSON.stringify(Param),
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d === true) {
                        CostingExists = 1;

                    }
                    else {
                        CostingExists = 0;

                    }
                }
            });
            if (CostingExists == 1) {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Costing with this name already exists.');
                return;
            }
            else {
                $('#ErrorDivAdd').hide();
            }
            var DialNumber = ""
            var CostPerSec = $('#txbAddCostPerSec').val();
            var ConnectCost = $('#txbAddConnectCost').val();
            var CallSetupCost = $('#txbAddCallSetupCost').val();
            $('#lstAddCostingDialNumbers option').each(function () {
                DialNumber += "" + $(this).val() + ";"
            });

            Param = { "Id": Id, "CostType": CostType, "CostPerSec": CostPerSec, "ConnectCost": ConnectCost, "CallSetupCost": CallSetupCost, "DialNumber": DialNumber };

            $.ajax({
                type: "POST",
                url: "Costing.aspx/InsertIntoCosting",
                data: "{CostingEntityObj:" + JSON.stringify(Param) + "}",

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    if (data.d === "1") {
                        $().toastmessage('showSuccessToast', 'Costing added successfully.');
                        GetCosting();
                    }
                    else {
                        $().toastmessage('showErrorToast', 'Error adding  Costing.');
                    }
                }
            });
        }
        function btnEditCostingRemoveDialNumber_Click() {
            $('#lstEditCostingDialNumbers option:selected').remove();
        }
        function GetCosting() {
            $('#tblCosting').empty();
            $.ajax({
                type: "POST",
                url: "Costing.aspx/GetAllCostings",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var trHTML = '<thead><tr><th>Cost Type</th><th>Cost Per Sec</th><th>Connect Cost</th><th>Call Setup Cost</th><th>Edit</th><th>Delete</th></tr></thead><tbody></tbody>';
                    $('#tblCosting').append(trHTML);
                    for (var i = 0; i < data.d.length; i++) {
                        var trHTML = '';
                        trHTML += '<tr><td>' + data.d[i].CostType + '</td>';
                        trHTML += '<td>' + data.d[i].CostPerSec + '</td>';
                        trHTML += '<td>' + data.d[i].ConnectCost + '</td>';
                        trHTML += '<td>' + data.d[i].CallSetupCost + '</td>';

                        trHTML += '<td><a class="edit"  onclick="CostingEdit(\'' + data.d[i].Id + '\',\'' + data.d[i].CostType + '\',\'' + data.d[i].CostPerSec + '\',\'' + data.d[i].ConnectCost + '\',\'' + data.d[i].CallSetupCost + '\',\'' + data.d[i].DialNumber + '\')";>Edit</a></td>';
                        trHTML += '<td><a class="edit" style="padding: 0px;" onclick="DeleteFromCosting(\'' + data.d[i].Id + '\')";>Delete</a></td></tr>';
                        $('#tblCosting').append(trHTML);
                    }
                }
            });
        }
        function CostingEdit(CostingId, CostType, CostPerSec, ConnectCost, CallSetupCost, DialNumbers) {

            $('#lstEditCostingDialNumbers').empty();
            $('#txbEditCostingId').val(CostingId);
            $('#txbEditCostType').val(CostType);
            $('#txbEditCostPerSec').val(CostPerSec);
            $('#txbEditConnectCost').val(ConnectCost);
            $('#txbEditCallSetupCost').val(CallSetupCost);

            var CostingDialNumbers = DialNumbers.split(";");
            for (var i = 0; i < CostingDialNumbers.length; i++) {
                if (CostingDialNumbers[i] != "")
                    $('#lstEditCostingDialNumbers').append($('<option>', { value: CostingDialNumbers[i], text: CostingDialNumbers[i] }));
            }
            $('#UpdateCostingModal').modal('show');


        }
        function UpdateCosting() {
            var CostingExists = 0;
            var Id = $('#txbEditCostingId').val();
            var CostType = $('#txbEditCostType').val();
            if (CostType == "") {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide Costing Type');
                return;

            }

            var Param = { "CostingId": Id, "CostType": CostType };

            $.ajax({
                type: "POST",
                url: "Costing.aspx/CheckIfCostingNameAlreadyExists",
                data: JSON.stringify(Param),
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d === true) {
                        CostingExists = 1;

                    }
                    else {
                        CostingExists = 0;

                    }
                }
            });
            if (CostingExists == 1) {
                $('#ErrorDivEdit').show();
                $('#lblErrorEdit').text('Costing with this name already exists.');
                return;
            }
            else {
                $('#ErrorDivAdd').hide();
            }
            var DialNumber = ""
            var CostPerSec = $('#txbEditCostPerSec').val();
            var ConnectCost = $('#txbEditConnectCost').val();
            var CallSetupCost = $('#txbEditCallSetupCost').val();
            $('#lstEditCostingDialNumbers option').each(function () {
                DialNumber += "" + $(this).val() + ";"
            });

            Param = { "Id": Id, "CostType": CostType, "CostPerSec": CostPerSec, "ConnectCost": ConnectCost, "CallSetupCost": CallSetupCost, "DialNumber": DialNumber };

            $.ajax({
                type: "POST",
                url: "Costing.aspx/UpdateIntoCosting",
                data: "{CostingEntityObj:" + JSON.stringify(Param) + "}",

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d === true) {
                        $().toastmessage('showSuccessToast', 'Costing updated successfully.');
                        GetCosting();
                    }
                    else {
                        $().toastmessage('showErrorToast', 'Error updating  Costing.');
                    }
                }
            });
        }
        function DeleteFromCosting(CostingId) {

            var Param = { "CostingId": CostingId }
            var ConfirmDelete = confirm("Are you sure you want to delete?");
            if (ConfirmDelete == false) {
                return;
            }
            $.ajax({
                type: "POST",
                url: "Costing.aspx/DeleteFromCostings",
                data: JSON.stringify(Param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d === "1") {
                        GetCosting();
                        $().toastmessage("showSuccessToast", "Costing deleted successfully.");
                    }
                    else {
                        $().toastmessage("showErrorToast", "Failed to delete Costing.");
                    }
                }
            });
        }
        $(document).ready(function () {
            GetCosting();
            $("#linkCosting").addClass("active open");
        });
    </script>
</asp:Content>
