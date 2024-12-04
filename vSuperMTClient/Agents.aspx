<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisor.Master" AutoEventWireup="true" CodeBehind="Agents.aspx.cs" Inherits="vSuperMTClient.Agents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" id="lblSocketServerConnectMessage2" style="font-size: 18px; padding-top: 16%; text-align: center; display: none; color: red">
    </div>
    <div class="row" id="AgentsDiv" style="display: none;">
        <div class="col-md-3">
            <div class="row" style="padding: 0px 0px 10px 0px;">
                <div class="col-md-12" style="padding: 5px 10px 12px 10px; background-color: white; text-align: left">
                    <span style="padding-top: 10px; display: block; font-weight: 600">Agent Display</span>
                </div>
            </div>
            <div id="divLeftMenu" style="background-color: white; padding-right: 5px;">
                <div class="row" style="background-color: white; padding: 5px 5px 5px 10px;">
                    <div class="row" style="">
                        <div class="col-md-12" style="padding: 0px;">
                            <span style="font-weight: 600">Sort by</span>
                        </div>
                    </div>
                </div>
                <div class="row" style="background-color: white; padding: 0px 5px 5px 10px;">
                    <div class="col-md-12" style="padding: 0px;">
                        <select id="lstAgentSort" class="form-control">

                            <option value="Name">Name</option>
                            <option value="Extension">Extension</option>
                        </select>
                    </div>
                </div>
                <%--  <div class="row" style="background-color: white; padding: 5px 5px 5px 10px;">
                    <div class="col-md-12" style="padding: 0px;">
                        <span style="font-weight: 600">Display Agents</span>
                    </div>
                </div>
                <div class="row" style="background-color: white; padding: 0px 5px 5px 10px;">
                    <div class="col-md-12" style="padding: 0px;">
                        <select id="lstAgentDisplay" class="form-control">
                            <option value="Euro">All</option>
                            <option value="Dollar">Male</option>
                            <option value="Pound">Female</option>
                        </select>

                    </div>
                </div>--%>
                <div class="row" style="background-color: white; padding: 5px 5px 5px 10px;">
                    <div class="col-md-12" style="padding: 0px;">
                        <span style="font-weight: 600">Display Queues</span>
                    </div>
                </div>
                <div class="row" style="background-color: white; padding: 0px 5px 5px 10px;">
                    <div class="col-md-12" style="padding: 0px;">
                        <div id="QueuesList" class="icheck-list" style="border: 1px solid #e5e5e5; padding: 5px 10px 5px 10px;">
                        </div>
                    </div>
                </div>
                <div class="row" style="background-color: white; padding: 5px 5px 5px 10px;">
                    <div class="col-md-12" style="padding: 0px;">
                        <span style="font-weight: 600">Agent State</span>
                    </div>
                </div>
                <div class="row" style="background-color: white; padding: 0px 5px 5px 10px;">
                    <div class="col-md-12" style="padding: 0px;">
                        <div id="AgentStateFiltersList" class="icheck-list" style="border: 1px solid #e5e5e5; padding: 5px 10px 5px 10px;">
                            <%--  <label style="border-bottom: 1px solid #e5e5e5; padding-bottom: 5px;">
                                <input id="cbAgentStateLoggedIn" type="checkbox" class="icheck" value="1" name="FiltersList" checked="checked">
                                Logged In
                            </label>
                            <label style="border-bottom: 1px solid #e5e5e5; padding-bottom: 5px;">
                                <input id="cbAgentStateLoggedOut" type="checkbox" class="icheck" value="2" name="FiltersList" checked="checked" >
                                Logged out
                            </label>
                            <label style="border-bottom: 1px solid #e5e5e5; padding-bottom: 5px;">
                                <input id="cbAgentStateAvailable" type="checkbox" class="icheck" value="3" name="FiltersList" checked="checked ">
                                Available
                            </label>
                            <label style="border-bottom: 1px solid #e5e5e5; padding-bottom: 5px;">
                                <input id="cbAgentStateInWrapUp" type="checkbox" class="icheck" value="4" name="FiltersList" checked="checked">
                                In Wrap-up
                            </label>
                            <label style="border-bottom: 1px solid #e5e5e5; padding-bottom: 5px;">
                                <input id="cbAgentStateBusy" type="checkbox" class="icheck" value="5" name="FiltersList" checked="checked">
                                Busy
                            </label>--%>
                        </div>
                    </div>
                </div>
                <div class="row" style="background-color: white; padding: 5px 5px 5px 10px;">
                    <div class="col-md-4" style="padding-left: 10px; padding-top: 5px;">
                        <span style="font-weight: 600">Number of Agents</span>
                    </div>
                    <div class="col-md-8" style="padding-left: 10px;">
                        <select id="ddlNoOfTiles" class="form-control">
                                            <option value="3">3</option>
                                            <option value="4">4</option>
                                            <option value="6">6</option>
                                            <option value="12">12</option>                                         
                                        </select>
                    </div>
                </div>
                 <div class="row" style="background-color: white; padding: 5px 5px 5px 10px;">
                    <div class="col-md-4" style="padding-left: 10px; padding-top: 5px;">
                        <span style="font-weight: 600">Display Avatar</span>
                    </div>
                    <div class="col-md-8" style="padding-left: 10px;">
                        <input type="checkbox" id="cb_Avatar" class="make-switch" checked="checked" data-on-text="ON" data-off-text="OFF"  data-on-color="success" data-off-color="danger" />
                    </div>
                </div>
                 <div class="row" style="background-color: white; padding: 5px 5px 5px 10px;">
                    <div class="col-md-4" style="padding-left: 10px; padding-top: 5px;">
                        
                    </div>
                    <div class="col-md-8 " style="padding-left: 10px;text-align:right">
                        <button type="button" id="agentSave"  class="btnFlat" title="Apply" onclick="saveAgentDisplay();" >Save</button>
                    </div>
                </div>

            </div>
        </div>
        <div class="col-md-9" style="padding-left: 0px;">

            <div id="divCenterMenu" style="background-color: white;">
                <div class="row" style="padding: 0px 0px 10px 0px;">
                    <div class="col-md-12" style="padding: 5px 10px 5px 0px;">
                        <div class="row" id="AllAgents">
                        </div>

                    </div>
                </div>

            </div>
        </div>
        <div id="ModalEmergencyClosure" class="modal fade" role="basic" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog" style="padding: 10px">
                                <div class="modal-content" style="width: 440px; margin-left: 15%; margin-top: 18%; padding: 5px;">
                                    <div class="row" style="line-height: 40px; background-color: #ffffff; width: 100%; vertical-align: middle; border-bottom: 1px solid #e5e5e5">
                                        <div class="col-md-12">
                                            <span style="font-size: 18px; color: #404040;">Emergency Closure</span>
                                        </div>
                                    </div>
                                    <div class="row" style="line-height: 30px; background-color: #ffffff; width: 100%; text-align: center; vertical-align: middle; border-bottom: 1px solid #e5e5e5">
                                        <div class="col-md-12">
                                            <span style="font-size: 16px; color: #404040;">Are you sure you want to put all queues out of service?</span>
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 5px">

                                        <div class="col-md-2" style="float: right">
                                            <button type="button" class="btnFlat" data-dismiss="modal">No</button>

                                        </div>
                                        <div class="col-md-2" style="float: right">
                                            <button type="button" onclick="EmergencyClosure();" id="btnEmergencyClosure" class="btnFlat">Yes</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
        <div id="AgentDiscModal" data-backdrop="static" class="modal fade" role="dialog" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog" id="" style="width: 750px; height: 100%;">
                <div class="modal-content">
                    <div class="row" style="line-height: 25px; background-color: #ffffff; width: 100%; margin-left: 0%; vertical-align: middle; border-bottom: 1px solid #e5e5e5;">
                        <div class="col-md-11" style="margin-top: 5px;">
                            <span style="font-size: 16px; color: #404040;">Agent Status</span>
                        </div>
                        <div class="col-md-1" style="margin-top: 0%; text-align: right" onclick="CloseAgentDiscModal();">
                            <span style="font-size: 20px; color: black;">
                                <i class="fa fa-times"></i>
                            </span>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                        <div class="col-md-3" id="AgentDescModalLeftPanel">
                        </div>

                        <div class="col-md-9" id="AgentDescModalDetails">
                            <div class="row" style="padding-top: 0px;">
                                <span style="font-weight: 600;">Agent Skills</span>
                            </div>
                            <div class="row" style="padding-top: 7px;">
                                <div id="AgentSkillTable" style="border: 1px solid #e5e5e5">
                                    <table style="width: 100%" id="tblAgentSkills">

                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="row" style="padding-top: 7px;">
                                <div class="col-md-3" style="padding-left: 0px; padding-top: 7px">
                                    Physical Extension:
                                </div>
                                 <div class="col-md-3" style="padding-left: 0px">
                                    <input id="txtLogAgentInPE" type="text" style="height: 35px;width: 100%;" maxlength="5" />
                                </div>
                                <div class="col-md-3" style="padding-left: 0px">
                                    <button type="button" onclick="LogAgentIn();" id="btnLogAgentIn" class="btnFlat" style="width: 100%;">Log Agent In</button>
                                </div>
                                <div class="col-md-3" style="padding-right:0px;padding-left: 0px">
                                    <button type="button" onclick="SetAgentAvailable();" id="btnSetAgentAvailable" class="btnFlat" style="width: 100%;">Set Available</button>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>


    </div>
    <div id="ServerConnectionModal" data-backdrop="static" class="modal fade" role="dialog" tabindex="-1" aria-hidden="true" style="top: 60px;">
        <div class="modal-dialog" style="width: 500px; height: 100%;">
            <%--<div class="modal-content">
                <div class="row" style="height: 300px; width: 500px; position: relative; background-color: #666666">
                       <div class="col-md-11">
                           
                        </div>
                        <div class="col-md-1" style="margin-top: 0%; text-align: right" onclick="CloseServerConnectionModal();">
                            <span style="font-size: 20px; color: black;">
                                <i class="fa fa-times"></i>
                            </span>
                        </div>
                </div>
                <div style="position: absolute; top: 50px; left: 50px; right: 50px; bottom: 50px; background-color: black">
                    <div class="row" style="line-height: 30px; background-color: #f91627; width: 100%; margin-left: 0%; vertical-align: middle; border-bottom: 2px solid #e5e5e5;">
                        <div class="col-md-12" style="margin-top: 5px;">
                            <span style="font-size: 20px; color: white;">Please Wait ...</span>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                        <div class="col-md-12" id="">
                            <span id="lblSocketServerConnectMessage" style="font-size: 14px; color: white;"></span>
                        </div>
                    </div>
                </div>

            </div>--%>
            <div class="modal-content">

                <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                    <div class="col-md-11" id="" style="text-align: center;">
                        <span id="lblSocketServerConnectMessage" style="font-size: 16px; color: red;"></span>
                    </div>
                    <div class="col-md-1" style="margin-top: 0%; text-align: right" onclick="CloseServerConnectionModal();">
                        <span style="font-size: 20px; color: black;">
                            <i class="fa fa-times"></i>
                        </span>
                    </div>
                </div>


            </div>

        </div>
    </div>
    <script>

        var websocket;
        var socketInterval = '0';
        var UFID = "";
        var UserName = "";
        var Password = "";
        var wsUri = "";
        var ServerIp = "";
        var AgentExtension = "";
        var AgentChange = false;
        var FirstAgentMessage = true;
        var lockMessage = false;
        var AgentsArray = [];
        var AgentResponseObj;

        var AllQueues = [];
       // var CheckedQueues = [];
        var CheckedQueues1 = [];
        var CheckedQueues2 = [];

        var UncheckedQueues = [];
        var AgentStatesFiltersList = ["LoggedOut", "Unavailable", "OffHook", "Ringing", "Connected", "WrapUp", "Held", "Available", "Occupied", "Routing"];
        var CheckedAgentStates = [];
       
        var CheckedAgentStates2 = [];

        var UncheckedAgentStates = [];
        var AgentStateTimer = [];

        var NumberOfAgents = "6";

        $(document).ready(function () {
            $("#HeaderLinkAgents").removeClass("DeactiveHeaderLink").addClass("ActiveHeaderLink");
            //$("#imgSocketServerConnectState").show();
            //$('#ServerConnectionModal').modal('show');

            $('.icheck').iCheck({
                checkboxClass: 'icheckbox_square-red',
                radioClass: 'iradio_square-red',
                increaseArea: '20%' // optional
            });


            var ScreenHeight = (screen.height) - 250;
            $('#divLeftMenu').slimScroll({
                height: ScreenHeight
            });
            $('#divCenterMenu').slimScroll({
                height: ScreenHeight + 55
            });
            $('#AgentSkillTable').slimScroll({
                height: 177
            });
            $('#lstAgentSort').change(function () {

                LoadAgentsCompleteInfo();
            });

            $("#ddlNoOfTiles").val(6);

            //$('#cbAgentStateLoggedIn').on("ifChecked", function () {

            //    alert("hiiii");
            //    LoadAgentsPartialInfo();
            //});
            //$('#cbAgentStateLoggedOut').on("ifChanged", function () {

            //    alert("hiiii");
            //    LoadAgentsPartialInfo();
            //});
            //$('#cbAgentStateAvailable').on("ifChanged", function () {

            //    alert("hiiii");
            //    LoadAgentsPartialInfo();
            //});
            //$('#cbAgentStateInWrapUp').on("ifChanged", function () {

            //    alert("hiiii");
            //    LoadAgentsPartialInfo();
            //});
            //$('#cbAgentStateBusy').on("ifChanged", function () {

            //    alert("hiiii");
            //    LoadAgentsPartialInfo();
            //});

            LoadAgentDisplay();
            //LoadWebSocket();
        });
        function changeAgentStateFormat(AgentState) {
            if (AgentState == "Unavailable") {
                return "Unavailable"
            } else if (AgentState == "LoggedOut") {
                return "Logged Out"
            } else if (AgentState == "WrapUp") {
                return "Wrap-up"
            } else if (AgentState == "OffHook") {
                return "Off Hook"
            }
            return AgentState;
        }
        function LoadAgentStateFilterList() {
            console.log("LoadAgentStateFilterList");
            $("#AgentStateFiltersList").empty();
            CheckedAgentStates = [];
            for (var i = 0; i < AgentStatesFiltersList.length; i++) {
                var AgentState = AgentStatesFiltersList[i];
                var $AgentStateDiv = $("#AgentStateFiltersList");

                $label = $("<label/>");
                $label.css({ "border-bottom": "1px solid #e5e5e5", "padding-bottom": "5px" }).appendTo($AgentStateDiv);
                var $Cb = $('<input />', { "id": "cb_" + AgentState + "", "type": 'checkbox', "class": "icheck", "checked": "checked", "value": "" + AgentState + "" }).appendTo($label);
                ////if ($.inArray(AgentState, UncheckedAgentStates) > -1) {
                ////    $Cb.prop("checked", false);
                ////}
                ////else {
                ////    $Cb.prop("checked", true);
                ////    CheckedAgentStates.push(AgentState);
                ////}
                if ($.inArray(AgentState, CheckedAgentStates2) == -1) {
                    $Cb.prop("checked", false);
                }
                else {
                   $Cb.prop("checked", true);
                   CheckedAgentStates.push(AgentState);
                }
                
                $Cb.on('ifChecked', function () {

                    UncheckedAgentStates.splice($.inArray($(this).val(), UncheckedAgentStates), 1);
                    CheckedAgentStates.push($(this).val());
                    LoadAgentsPartialInfo();

                });
                $Cb.on('ifUnchecked', function () {

                    UncheckedAgentStates.push($(this).val());
                    CheckedAgentStates.splice($.inArray($(this).val(), CheckedAgentStates), 1);
                    LoadAgentsPartialInfo();

                });


                $label.append("" + changeAgentStateFormat(AgentState) + "");
                //$label.append(AgentState);
            }

        }
        function LoadQueuesList() {

            //AllQueues.push('-1:No Skill:0');

            $("#QueuesList").empty();
            var $QueuesListDiv = $("#QueuesList");
            CheckedQueues = [];
            for (var i = 0; i < AllQueues.length; i++) {

                var objQueue = AllQueues[i];
                var QueueId = objQueue.split(':')[0];
                var QueueName = objQueue.split(':')[1];
                var QueueInService = objQueue.split(':')[2];

                var $div = $("<div id='t" + QueueId + "'/>");
                $label = $("<label/>");
                var $Cb = $('<input />', { "id": "cb_" + QueueName + "", "type": 'checkbox', "data-id": "" + QueueId + "", "class": "icheck qued-item-" + QueueId, "checked": "checked", "value": "" + QueueName + "" }).appendTo($label);
                ($label).appendTo($div);

                //if ($.inArray(QueueName, UncheckedQueues) > -1) {
                //    $Cb.prop("checked", false);
                //}
                //else {
                //    $Cb.prop("checked", true);
                //    CheckedQueues.push(QueueName);

                //}
                ////alert(CheckedQueues.length);

                
                if ($.inArray(QueueId, CheckedQueues2) == -1) {
                    $Cb.prop("checked", false);
                }
               else{
                                      $Cb.prop("checked", true);
                CheckedQueues.push(QueueName);
               }
                $Cb.on('ifChecked', function () {

                    UncheckedQueues.splice($.inArray($(this).val(), UncheckedQueues), 1);
                    CheckedQueues.push($(this).val());
                   
                    LoadAgentsPartialInfo();
                    // LoadAgentsCompleteInfo();
                    // alert(CheckedQueues.length);
                });
                $Cb.on('ifUnchecked', function () {

                    UncheckedQueues.push($(this).val());
                    CheckedQueues.splice($.inArray($(this).val(), CheckedQueues), 1);
                    LoadAgentsPartialInfo();
                    //CheckedQueues = jQuery.grep(CheckedQueues, function (value) {
                    //    return value != $(this).val();
                    //});
                    //CheckedQueues = CheckedQueues.filter(function (elem) {
                    //return elem != $(this).val();

                    //});

                    //alert(CheckedQueues.length);
                    //  LoadAgentsCompleteInfo();
                });


                $label.append("" + QueueName + "");

                if (QueueId > 0) {
                    var btnClass = 'btnInService';
                    var btnValue = 'In Service';
                    if (QueueInService == 0) {
                        btnClass = 'btnOutService';
                        btnValue = 'Out of Service';
                    }

                    var $btnService = $("<input id=btnService_" + QueueName + " type='button' data-id='" + QueueId + "' data-service='" + QueueInService + "' onclick='QueueServiceInOut(this)' value='" + btnValue + "' class='" + btnClass + "' />");
                    $btnService.css({ "float": "right", "padding-right": "5px" }).appendTo($div);
                }

                $div.css({ "border-bottom": "1px solid #e5e5e5", "padding-bottom": "3px", "padding-top": "6px" }).appendTo($QueuesListDiv);

            }
            
            if (AllQueues.length > 0) {

                $label = $("<label/>");
                $label.css({ "padding-top": "8px", "height": "35px" }).appendTo($QueuesListDiv);

                var $btnService = $("<input id=btnEmergencyClosure type='button' onclick='ShowEmergencyClosurePopup()' value='Emergency Closure' class='btnEmergencyClosure' />");
                $btnService.css({ "float": "right", "padding-right": "5px" }).appendTo($label);
            }

            $('.icheck').iCheck({
                checkboxClass: 'icheckbox_square-red',
                radioClass: 'iradio_square-red',
                increaseArea: '20%' // optionalrea
            });
            
        }

        function saveAgentDisplay() {
            var avatar = "On"

            


       //     $('#cb_Avatar').prop('checked', false).trigger('change');

            if ($('#cb_Avatar').is(":checked")) {
                avatar = "On";

            }
            else {
                avatar = "Off";
            }

            var QueueList = [];
            $('#QueuesList input').each(function (index) {
                if ($(this).is(':checked')) {
                    let localId = $(this).attr('data-id');
                    //alert(localId);
                    QueueList.push(localId);
                }
            });


            var AgentState = [];
            $('#AgentStateFiltersList input').each(function (index) {
                if ($(this).is(':checked')) {
                    AgentState.push($(this).val());
                }
            });
            //console.log(testAgents);
            SortBy1 = $('#lstAgentSort').val();

            var Param = { "UserId": 1, "SortBy": SortBy1, "QueueList": QueueList, "AgentState": CheckedAgentStates, "NumberOfAgents": NumberOfAgents, "DisplayAvatar": avatar };

            

            $.ajax({
                type: "Post",
                url: "Agents.aspx/saveAgentDisplay",
                data: "{agentDisplayObj:" + JSON.stringify(Param) + "}",

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d === "1") {
                        
                        $().toastmessage("showSuccessToast", "Display Agent saved sucessfully.");

                    }
                    else {
                        $().toastmessage("showErrorToast", data.d);
                    }

                }

            });
        }

        function LoadAgentDisplay() {

            $.ajax({
                type: "POST",
                url: "Agents.aspx/GetAgentDisplay",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    
                    if (data != null) {
                        // $('#lstAgentSort').val(data.d.SortBy)
                        // $("#ddlNoOfTiles").val(data.d.NumberOfAgents)
                        CheckedAgentStates2 = data.d.AgentState;
                        CheckedQueues2 = data.d.QueueList;

                        //for (var i = 0; i < CheckedQueues2.length; i++) {
                        //    $(".qued-item-" + $.trim(CheckedQueues2[i])).prop('checked', true);
                        //    $(".qued-item-" + $.trim(CheckedQueues2[i])).parent('div').addClass('checked');
                                                        
                        //}
                        dbdata = "yes";
                        console.log(CheckedAgentStates2);
                        
                        //$('#AgentStateFiltersList input').each(function (index) {
                        //    let value = `${index + 1}`;
                        //    console.log(value + " : Index value");
                        //    if ($.inArray(value, CheckedAgentStates2) != -1) {
                        //      //  
                        //        $(this).prop('checked', true);
                        //        $(this).parent('div').addClass('checked');
                        //    }
                        //});

                        if (data.d.SortBy != null) {
                            $('#lstAgentSort').val(data.d.SortBy);

                        }
                        if (data.d.DisplayAvatar != null) {
                            if (data.d.DisplayAvatar == "On") {
                                $('#cb_Avatar').prop('checked', true).trigger('change');


                            }
                            else {
                                $('#cb_Avatar').prop('checked', false).trigger('change');


                            }

                        }
                        if (data.d.NumberOfAgents != null) {
                            $('#ddlNoOfTiles').val(data.d.NumberOfAgents);

                        }
                        LoadWebSocket();
                        
                    }
                }
            });
        }



        //function LoadAgentsCompleteInfo() {

        //    console.log("Loading the Complete Info of agents.....");

        //    var $AllAgents = $("#AllAgents");
        //    $AllAgents.empty();
        //    var SortBy = $('#lstAgentSort').val();

        //    // Sort array 
        //    AgentsArray.sort(function (a, b) {
        //        var nameA = a.Name.toUpperCase(); // ignore upper and lowercase
        //        var nameB = b.Name.toUpperCase(); // ignore upper and lowercase

        //        if (SortBy == "Extension") {
        //            nameA = a.AgentExtension.toUpperCase(); // ignore upper and lowercase
        //            nameB = b.AgentExtension.toUpperCase(); // ignore upper and lowercase
        //        }

        //        if (nameA < nameB) {
        //            return -1;
        //        }
        //        if (nameA > nameB) {
        //            return 1;
        //        }


        //        return 0;
        //    });

        //    var maxrows = 1;
        //    for (var i = 0; i < AgentsArray.length; i++) {
        //        if (AgentsArray[i].AssociatedGroups.length > 3 && AgentsArray[i].AssociatedGroups.length <= 6) {
        //            if (maxrows < 2)
        //                maxrows = 2;
        //        }
        //        if (AgentsArray[i].AssociatedGroups.length > 6 && AgentsArray[i].AssociatedGroups.length <= 9) {
        //            if (maxrows < 3)
        //                maxrows = 3;
        //        }
        //        if (AgentsArray[i].AssociatedGroups.length > 9 && AgentsArray[i].AssociatedGroups.length <= 12) {
        //            if (maxrows < 4)
        //                maxrows = 4;
        //        }
        //        if (AgentsArray[i].AssociatedGroups.length > 12 && AgentsArray[i].AssociatedGroups.length <= 15) {
        //            if (maxrows < 5)
        //                maxrows = 5;
        //        }
        //        if (AgentsArray[i].AssociatedGroups.length > 15 && AgentsArray[i].AssociatedGroups.length <= 18) {
        //            if (maxrows < 6)
        //                maxrows = 6;
        //        }
        //        if (AgentsArray[i].AssociatedGroups.length > 18 && AgentsArray[i].AssociatedGroups.length <= 21) {
        //            if (maxrows < 7)
        //                maxrows = 7;
        //        }

        //    }
        //    maxrows = maxrows * 25;

        //    for (var i = 0; i < AgentsArray.length; i++) {
        //        var Name = AgentsArray[i].Name;
        //        var Gender = AgentsArray[i].Gender;
        //        var Extension = AgentsArray[i].AgentExtension;
        //        var ImagePath = AgentsArray[i].ImagePath;
        //        var IsLoggedin = AgentsArray[i].IsLoggedin;  //false
        //        var RaiseHand = AgentsArray[i].RaiseHand; //false
        //        var AgentState = AgentsArray[i].AgentState;// 0 1
        //        var IsAvailable = AgentsArray[i].IsAvailable; //false
        //        var UnAvailibilityReason = AgentsArray[i].UnAvailibilityReason;
        //        var AssociatedGroups = AgentsArray[i].AssociatedGroups;
        //        var PhoneExtension = AgentsArray[i].PhoneExtension;

        //        if (PhoneExtension.trim() == "")
        //            PhoneExtension = Extension;
                
        //        var $Col = $("<div/>", { id: "AgentIndexed_" + Extension, "class": "col-md-1" });
        //        $Col.css({ "width": "16.5%", "cursor": "default", "position": "relative", "margin-bottom": "15px" });
        //        $Col.appendTo($AllAgents);

        //        //$Col.on("dblclick", AgentDiscModal(i));
        //        //$Col.dblclick(AgentDiscModal(i));
        //        //$Col.dblclick(function () {                   
        //        //    AgentDiscModal(i-1);
        //        //});

        //        //could pass the extension only but passing here the whole Agent object to the AgentDiscModal handler
        //        //$Col.dblclick({ AgentObj: AgentsArray[i] }, AgentDiscModal);

        //        var $OuterDiv = $("<div/>", { id: "AgentOuterDiv_" + Extension });
        //        $OuterDiv.css({ "border": "1px solid #e5e5e5", "border-radius": "10px" });
        //        $OuterDiv.appendTo($Col);


        //        var $AgentColorDiv = $("<div/>", { id: "AgentColorDiv_" + Extension });
        //        //$AgentColorDiv.css({ "height": "15px", "background-color": "" + Color + "", "border-top-left-radius": "10px", "border-top-right-radius": "10px" });
        //        $AgentColorDiv.css({ "height": "30px", "border-top-left-radius": "10px", "border-top-right-radius": "10px", "padding-top" : "4px", "text-align" : "center" });
        //        $AgentColorDiv.appendTo($OuterDiv);

        //        var $AgentTimerLabel = $("<span/>", { id: "AgentTimerLabel_" + Extension });
        //        $AgentColorDiv.css({ "font-size": "16px", "font-weight": "bold" });
        //        $AgentTimerLabel.text("00:00");
        //        $AgentTimerLabel.appendTo($AgentColorDiv);

        //        var $AgentImageDiv = $("<div/>", { id: "AgentImageDiv_" + Extension });
        //        $AgentImageDiv.css({ "text-align": "center", "cursor": "pointer" });
        //        $AgentImageDiv.appendTo($OuterDiv);
        //        $AgentImageDiv.on('click', { Extension: Extension }, AgentDiscModal);

        //        $AgentImage = $("<img/>", { id: "AgentImage_" + Extension });
        //        //$AgentImage.css({ "height": "80px", "width": "102px" });
        //        $AgentImage.css({ "width": "auto", "margin-top": "5px", "height": "80px" });
        //        //$AgentImage.attr({ "src": "" + ImagePath.replace("[IP]", ServerIp) + "" });
        //        $AgentImage.attr({ "src": "" + ImagePath.replace("[IP]", ServerIp) + "?t=" + new Date().getTime() });
        //        $AgentImage.appendTo($AgentImageDiv);

        //        var $RaiseHandDiv = $("<div/>", { id: "AgentRaiseHandDiv_" + Extension });
        //        $RaiseHandDiv.css({ "cursor": "pointer", "position": "absolute", "top": "20px", "right": "20px", "display": "none" });
        //        $RaiseHandDiv.appendTo($OuterDiv);
        //        //$RaiseHandDiv.on("click",RaiseHandAck(Extension));
        //        $RaiseHandDiv.on('click', { Extension: Extension }, RaiseHandAck);
        //        //$RaiseHandDiv.click(function () {

        //        //    RaiseHandAck(Extension);
        //        //});

        //        $RaiseHandImage = $("<img/>", { id: "AgentRaiseHandImage_" + Extension });
        //        $RaiseHandImage.css({ "height": "30px" });
        //        $RaiseHandImage.attr({ "src": "Content/images/RaiseHand.png" });
        //        $RaiseHandImage.appendTo($RaiseHandDiv);

        //        var $AgentDescDiv = $("<div/>", { id: "AgentDescDiv_" + Extension });
        //        $AgentDescDiv.appendTo($OuterDiv);

        //        var $Extension = $("<div/>", { "class": "row" });
        //        $Extension.css({ "text-align": "center" });
        //        $Extension.appendTo($AgentDescDiv);
        //        var $ExtensionSpan = $("<span/>", { id: "AgentExtensionSpan_" + Extension });

        //        $ExtensionSpan.css({ "font-weight": "600" });
        //        $ExtensionSpan.text(PhoneExtension);
        //        $ExtensionSpan.appendTo($Extension);

        //        var $Name = $("<div/>", { "class": "row" });
        //        $Name.css({ "text-align": "center" });
        //        $Name.appendTo($AgentDescDiv);
        //        var $NameSpan = $("<span/>", { id: "AgentNameSpan_" + Extension });
        //        $NameSpan.css({ "font-weight": "bold" });
        //        $NameSpan.text(Name.length > 15 ? Name.substring(0, 15) + "..." : Name);
        //        $NameSpan.attr({ "title": Name });
        //        $NameSpan.appendTo($Name);

        //        var $Status = $("<div/>", { "class": "row" });
        //        $Status.css({ "text-align": "center" });
        //        $Status.appendTo($AgentDescDiv);

        //        var $StatusSpan = $("<span/>", { id: "AgentStatusSpan_" + Extension });
        //        $StatusSpan.css({ "font-weight": "600", "font-size": "10px", "color": "green" });
        //        $StatusSpan.appendTo($Status);


        //        var $QueueSep = $("<div/>", { "class": "row" });
        //        $QueueSep.css({ "text-align": "center" });
        //        $QueueSep.appendTo($AgentDescDiv);

        //        var $Queue = $("<div id='AgentQueueRow_" + Extension + "'/>").css({ "padding-top": "7px", "height": "" + maxrows+"px", "margin": "7px 15px", "border-top": "1px solid grey" });
        //        $Queue.appendTo($QueueSep);

                

        //        SetAgentState(AgentsArray[i]);
        //    }
        //}
        function LoadAgentsCompleteInfo() {

            console.log("Loading the Complete Info of agents.....");

            var $AllAgents = $("#AllAgents");
            $AllAgents.empty();
            var SortBy = $('#lstAgentSort').val();

            // Sort array 
            AgentsArray.sort(function (a, b) {
                var nameA = a.Name.toUpperCase(); // ignore upper and lowercase
                var nameB = b.Name.toUpperCase(); // ignore upper and lowercase

                if (SortBy == "Extension") {
                    nameA = a.AgentExtension.toUpperCase(); // ignore upper and lowercase
                    nameB = b.AgentExtension.toUpperCase(); // ignore upper and lowercase
                }

                if (nameA < nameB) {
                    return -1;
                }
                if (nameA > nameB) {
                    return 1;
                }


                return 0;
            });

            var maxrows = 1;
            for (var i = 0; i < AgentsArray.length; i++) {
                if (AgentsArray[i].AssociatedGroups.length > 3 && AgentsArray[i].AssociatedGroups.length <= 6) {
                    if (maxrows < 2)
                        maxrows = 2;
                }
                if (AgentsArray[i].AssociatedGroups.length > 6 && AgentsArray[i].AssociatedGroups.length <= 9) {
                    if (maxrows < 3)
                        maxrows = 3;
                }
                if (AgentsArray[i].AssociatedGroups.length > 9 && AgentsArray[i].AssociatedGroups.length <= 12) {
                    if (maxrows < 4)
                        maxrows = 4;
                }
                if (AgentsArray[i].AssociatedGroups.length > 12 && AgentsArray[i].AssociatedGroups.length <= 15) {
                    if (maxrows < 5)
                        maxrows = 5;
                }
                if (AgentsArray[i].AssociatedGroups.length > 15 && AgentsArray[i].AssociatedGroups.length <= 18) {
                    if (maxrows < 6)
                        maxrows = 6;
                }
                if (AgentsArray[i].AssociatedGroups.length > 18 && AgentsArray[i].AssociatedGroups.length <= 21) {
                    if (maxrows < 7)
                        maxrows = 7;
                }

            }

            maxrows = maxrows * 25;

            for (var i = 0; i < AgentsArray.length; i++) {
                var Name = AgentsArray[i].Name;
                var Gender = AgentsArray[i].Gender;
                var Extension = AgentsArray[i].AgentExtension;
                var ImagePath = AgentsArray[i].ImagePath;
                var IsLoggedin = AgentsArray[i].IsLoggedin;  //false
                var RaiseHand = AgentsArray[i].RaiseHand; //false
                var AgentState = AgentsArray[i].AgentState;// 0 1
                var IsAvailable = AgentsArray[i].IsAvailable; //false
                var UnAvailibilityReason = AgentsArray[i].UnAvailibilityReason;
                var AssociatedGroups = AgentsArray[i].AssociatedGroups;
                var PhoneExtension = AgentsArray[i].PhoneExtension;

                if (PhoneExtension.trim() == "")
                    PhoneExtension = Extension;

                var nooftiles = $("#ddlNoOfTiles").val();
                NumberOfAgents = $("#ddlNoOfTiles").val();


                var $Col;
                if (nooftiles == 3) {
                    $Col = $("<div/>", { id: "AgentIndexed_" + Extension, "class": "col-md-4" });
                }
                else if (nooftiles == 4) {
                    $Col = $("<div/>", { id: "AgentIndexed_" + Extension, "class": "col-md-3" });
                }
                else if (nooftiles == 6) {
                    $Col = $("<div/>", { id: "AgentIndexed_" + Extension, "class": "col-md-2" });
                }
                else if (nooftiles == 12) {
                    $Col = $("<div/>", { id: "AgentIndexed_" + Extension, "class": "col-md-1" });
                }
                else {
                    $Col = $("<div/>", { id: "AgentIndexed_" + Extension, "class": "col-md-2" });
                }

                $Col.css({ "cursor": "default", "position": "relative", "margin-bottom": "0px", "padding": "3px" });
                $Col.appendTo($AllAgents);

                var $OuterDiv1 = $("<div/>", { id: "AgentOuterDiv1_" + Extension });
                $OuterDiv1.css({ "border": "2px solid #e5e5e5", "border-radius": "10px", "margin": "0px", "padding": "0px" });
                $OuterDiv1.appendTo($Col);

                var $OuterDiv = $("<div/>", { id: "AgentOuterDiv_" + Extension });
                $OuterDiv.css({ "border": "0px solid #e5e5e5", "border-radius": "10px" });
                $OuterDiv.appendTo($OuterDiv1);


                var $AgentColorDiv = $("<div/>", { id: "AgentColorDiv_" + Extension });
                //$AgentColorDiv.css({ "height": "15px", "background-color": "" + Color + "", "border-top-left-radius": "10px", "border-top-right-radius": "10px" });
                $AgentColorDiv.css({ "height": "30px", "border-top-left-radius": "10px", "border-top-right-radius": "10px", "padding-top": "4px", "text-align": "center" });
                $AgentColorDiv.appendTo($OuterDiv);

                var $AgentTimerLabel = $("<span/>", { id: "AgentTimerLabel_" + Extension });
                $AgentColorDiv.css({ "font-size": "16px", "font-weight": "bold" });
                $AgentTimerLabel.text("00:00");
                $AgentTimerLabel.appendTo($AgentColorDiv);

                var $AgentLogoutLabel = $("<span/>", { id: "AgentLogoutLabel_" + Extension });
                $AgentColorDiv.css({ "font-size": "16px", "font-weight": "bold" });
                $AgentLogoutLabel.text("00:00");
                $AgentLogoutLabel.appendTo($AgentColorDiv);

                var $AgentImageDiv = $("<div/>", { id: "AgentImageDiv_" + Extension });
                $AgentImageDiv.css({ "text-align": "center", "cursor": "pointer" });
                $AgentImageDiv.appendTo($OuterDiv);
                $AgentImageDiv.on('click', { Extension: Extension }, AgentDiscModal);

                //$('#cb_Avatar').is(":checked")
                if (!$('#cb_Avatar').is(":checked")) {
                    $AgentImageDiv.hide();
                }
                else {
                    $AgentImageDiv.show();
                }


                $AgentImage = $("<img/>", { id: "AgentImage_" + Extension });
                //$AgentImage.css({ "height": "80px", "width": "102px" });
                $AgentImage.css({ "width": "auto", "margin-top": "5px", "height": "80px" });
                //$AgentImage.attr({ "src": "" + ImagePath.replace("[IP]", ServerIp) + "" });
                $AgentImage.attr({ "src": "" + ImagePath.replace("[IP]", ServerIp) + "?t=" + new Date().getTime() });
                $AgentImage.appendTo($AgentImageDiv);

                var $RaiseHandDiv = $("<div/>", { id: "AgentRaiseHandDiv_" + Extension });
                $RaiseHandDiv.css({ "cursor": "pointer", "position": "absolute", "top": "20px", "right": "20px", "display": "none" });
                $RaiseHandDiv.appendTo($OuterDiv);
                //$RaiseHandDiv.on("click",RaiseHandAck(Extension));
                $RaiseHandDiv.on('click', { Extension: Extension }, RaiseHandAck);
                //$RaiseHandDiv.click(function () {

                //    RaiseHandAck(Extension);
                //});

                $RaiseHandImage = $("<img/>", { id: "AgentRaiseHandImage_" + Extension });
                $RaiseHandImage.css({ "height": "30px" });
                $RaiseHandImage.attr({ "src": "Content/images/RaiseHand.png" });
                $RaiseHandImage.appendTo($RaiseHandDiv);

                var $AgentDescDiv = $("<div/>", { id: "AgentDescDiv_" + Extension });
                $AgentDescDiv.appendTo($OuterDiv);

                var $Extension = $("<div/>", { "class": "row" });
                $Extension.css({ "text-align": "center" });
                $Extension.appendTo($AgentDescDiv);
                var $ExtensionSpan = $("<span/>", { id: "AgentExtensionSpan_" + Extension });

                $ExtensionSpan.css({ "font-weight": "600" });
                $ExtensionSpan.text(PhoneExtension);
                $ExtensionSpan.appendTo($Extension);

                var $Name = $("<div/>", { "class": "row" });
                $Name.css({ "text-align": "center" });
                $Name.appendTo($AgentDescDiv);
                var $NameSpan = $("<span/>", { id: "AgentNameSpan_" + Extension });
                $NameSpan.css({ "font-weight": "bold", "display": "block", "white-space": "nowrap", "overflow": "hidden", "text-overflow": "ellipsis" });
                //if (nooftiles <= 6)
                $NameSpan.text(Name);
                   // $NameSpan.text(Name.length > 15 ? Name.substring(0, 15) + "..." : Name);
                //else
                //    $NameSpan.text(Name.length > 10 ? Name.substring(0, 10) + "..." : Name);

                $NameSpan.attr({ "title": Name });
                $NameSpan.appendTo($Name);

                var $Status = $("<div/>", { "class": "row" });
                $Status.css({ "text-align": "center", "margin-bottom": "3px" });
                $Status.appendTo($AgentDescDiv);

                var $StatusSpan = $("<span/>", { id: "AgentStatusSpan_" + Extension });
                $StatusSpan.css({ "font-weight": "600", "font-size": "10px", "color": "green" });
                $StatusSpan.appendTo($Status);


                var $QueueSep = $("<div/>", { "class": "row" });
                $QueueSep.css({ "text-align": "center", "padding": "5px" });
                $QueueSep.appendTo($AgentDescDiv);

                // var $Queue = $("<div id='AgentQueueRow_" + Extension + "'/>").css({ "padding-top": "7px", "height": maxrows+"px", "margin": "7px 15px", "border-top": "1px solid grey" });
                var $Queue = $("<div id='AgentQueueRow_" + Extension + "'/>").css({ "padding-top": "5px", "height": maxrows + "px", "margin": "0px 0px", "border-top": "1px solid grey" });
                $Queue.appendTo($QueueSep);



                SetAgentState(AgentsArray[i]);
            }
        }
        function LoadAgentsPartialInfo() {
            //alert(JSON.stringify(AgentsArray));

            //if (AgentExtension != "") e.g a popup is opened.
            if (AgentExtension != "")// if pop up is opened update the state on popup as well 
            {
                var AgentObj = AgentsArray.filter(function (obj) {
                    return obj.AgentExtension == AgentExtension;
                })[0];
                SetAgentStateOnPopup(AgentObj);
            }
            for (var i = 0; i < AgentsArray.length; i++) {

                //alert(AgentsArray[i].IsLoggedin);
                SetAgentState(AgentsArray[i]);
            }
        }

        var objPopupTimer = null;
        function AgentDiscModal(e) {
            var Extension = e.data.Extension;

            if (objPopupTimer != null)
                clearInterval(objPopupTimer);

            $("#AgentTimerLabelOnPopup").text($("#AgentTimerLabel_" + Extension).text());
            objPopupTimer = setInterval(function () {                
                $("#AgentTimerLabelOnPopup").text($("#AgentTimerLabel_" + Extension).text());
            }, 900);

            //alert(index);
            //var AgentObj = AgentsArray[index];

            
            //var AgentObjTemp = param.data.AgentObj;
            //var ext = AgentObjTemp.Extension;


            var AgentObj = AgentsArray.filter(function (obj) {
                return obj.AgentExtension == Extension;
            })[0];


            //alert(AgentObj.Extension);

            //Set Extension in Global variable
            AgentExtension = AgentObj.AgentExtension;
            var Name = AgentObj.Name;
            var Gender = AgentObj.Gender;
            var Extension = AgentObj.AgentExtension;
            var ImagePath = AgentObj.ImagePath;
            var IsLoggedin = AgentObj.IsLoggedin;  //false
            var RaiseHand = AgentObj.RaiseHand; //false
            var AgentState = AgentObj.AgentState;// 0 1
            var IsAvailable = AgentObj.IsAvailable; //false
            var UnAvailibilityReason = AgentObj.UnAvailibilityReason;
            var AssociatedGroups = AgentObj.AssociatedGroups;


            var $AgentDescModalLeftPanel = $("#AgentDescModalLeftPanel");
            //var Color = $("#AgentColorDiv_" + Extension).css("background-color");
            //var LogInStatus = $("#AgentStatusSpan_" + Extension).text();



            $AgentDescModalLeftPanel.html("");
            var $Row = $("<div/>", { "class": "row" });
            $Row.appendTo($AgentDescModalLeftPanel);
            var $OuterDiv = $("<div/>", { id: "AgentOuterDivOnPopup_" + Extension + "" });
            //$OuterDiv.css({ "border": "1px solid " + Color, "border-radius": "10px" });
            $OuterDiv.appendTo($Row);


            var $AgentColorDiv = $("<div/>", { id: "AgentColorDivOnPopup_" + Extension + "" });
            //$AgentColorDiv.css({ "height": "30px", "background-color": "" + Color + "", "border-top-left-radius": "10px", "border-top-right-radius": "10px" });
            $AgentColorDiv.css({ "height": "30px", "border-top-left-radius": "10px", "border-top-right-radius": "10px", "padding-top": "4px", "text-align": "center" });
            $AgentColorDiv.appendTo($OuterDiv);

            var $AgentTimerLabel = $("<span/>", { id: "AgentTimerLabelOnPopup" });
            $AgentColorDiv.css({ "font-size": "16px", "font-weight": "bold" });
            $AgentTimerLabel.text("");
            $AgentTimerLabel.appendTo($AgentColorDiv);

            var $AgentImageDiv = $("<div/>", { id: "AgentImageDivOnPopup_" + Extension + "" });
            $AgentImageDiv.css({ "text-align": "center" });
            $AgentImageDiv.appendTo($OuterDiv);


            $AgentImage = $("<img/>", { id: "AgentImageOnPopup_" + Extension + "" });
            //$AgentImage.css({ "height": "130px", "width": "130px" });
            $AgentImage.css({ "width": "auto", "margin-top": "5px", "height": "80px" });
            $AgentImage.attr({ "src": "" + ImagePath.replace("[IP]", ServerIp) + "?t=" + new Date().getTime() });
            $AgentImage.appendTo($AgentImageDiv);

            var $RaiseHandDiv = $("<div/>", { id: "AgentRaiseHandDivOnPopup_" + Extension });
            $RaiseHandDiv.css({ "cursor": "pointer", "position": "absolute", "top": "20px", "right": "20px", "display": "none" });
            $RaiseHandDiv.appendTo($OuterDiv);
            $RaiseHandDiv.on('click', { Extension: Extension }, RaiseHandAck);
            //$RaiseHandDiv.click(function () {
            //    //AgentExtension = Extension;
            //    RaiseHandAck(Extension);
            //});

            $RaiseHandImage = $("<img/>", { id: "AgentRaiseHandImageOnPopup_" + Extension });
            $RaiseHandImage.css({ "height": "30px" });
            $RaiseHandImage.attr({ "src": "Content/images/RaiseHand.png" });
            $RaiseHandImage.appendTo($RaiseHandDiv);

            var $AgentDescDiv = $("<div/>", { id: "AgentDescDivOnPopup_" + Extension + "" });

            $AgentDescDiv.appendTo($OuterDiv);

            var $Extension = $("<div/>", { "class": "row" });
            $Extension.css({ "text-align": "center" });
            $Extension.appendTo($AgentDescDiv);
            var $ExtensionSpan = $("<span/>", { id: "AgentExtensionSpanOnPopup_" + Extension });
            $ExtensionSpan.css({ "font-weight": "600" });
            $ExtensionSpan.text(Extension);
            $ExtensionSpan.appendTo($Extension);


            var $Name = $("<div/>", { "class": "row" });
            $Name.css({ "text-align": "center" });
            $Name.appendTo($AgentDescDiv);
            var $NameSpan = $("<span/>", { id: "AgentNameSpanOnPopup_" + Extension });
            $NameSpan.css({ "font-weight": "bold" });
            $NameSpan.text(Name.length > 15 ? Name.substring(0, 15) + "..." : Name);
            $NameSpan.appendTo($Name);

            var $Status = $("<div/>", { "class": "row" });
            $Status.css({ "text-align": "center" });
            $Status.appendTo($AgentDescDiv);

            var $StatusSpan = $("<span/>", { id: "AgentStatusSpanOnPopup_" + Extension + "" });
            $StatusSpan.css({ "font-weight": "600", "font-size": "10px", "color": "green" });
            $StatusSpan.appendTo($Status);


            var $QueueSep = $("<div/>", { "class": "row" });
            $QueueSep.css({ "text-align": "center" });
            $QueueSep.appendTo($AgentDescDiv);

            var $Queue = $("<div id='AgentQueueRowOnPopup_" + Extension + "'/>").css({ "padding-top": "7px", "height": "60px", "margin": "7px 15px", "border-top": "1px solid grey" });
            $Queue.appendTo($QueueSep);


            //if (!IsLoggedin || AgentState == "10")
            //{
            //    $NameSpan.css({ "color": "#a1a1a1" }); //#44515d
            //    $ExtensionSpan.css({ "color": "#a1a1a1" }); //#44515d
            //    $OuterDiv.css({ "opacity": "0.3" });   //1
            //    $StatusSpan.css({ "color": "#a1a1a1" });  //"color": "green"
            //}

            var $tblAgentSkills = $("#tblAgentSkills");
            $tblAgentSkills.empty();

            $thead = $("<thead/>");
            $thead.appendTo($tblAgentSkills);
            var $tr = $("<tr/>");
            $tr.css({ "border-bottom": "1px solid #e5e5e5" });
            $tr.appendTo($thead);


            var $th = $("<th/>");
            $th.css({ "text-align": "left", "padding": "10px", "width": "80px" });
            $th.text("Queue");
            $th.appendTo($tr);
            var $th = $("<th/>");
            $th.css({ "text-align": "left", "padding": "10px", "width": "40%" });
            $th.appendTo($tr);
            var $th = $("<th/>");
            $th.css({ "text-align": "center", "padding": "10px" });
            $th.text("Level");
            $th.appendTo($tr);
            var $th = $("<th/>");
            $th.css({ "text-align": "center", "padding": "10px" });
            $th.text("Available");
            $th.appendTo($tr);

            var $tr = $("<tr/>");
            var $td = $("<td/>");
            var $imgAvaiable;
            for (var j = 0; j < AssociatedGroups.length; j++) {
                $tr = $("<tr/>");

                var QueueId = AssociatedGroups[j].QueueId;
                var QueueColor = AssociatedGroups[j].Color;
                var QueueAbbr = AssociatedGroups[j].QueueAbr;
                var QueueName = AssociatedGroups[j].Name;
                var QueueLevel = AssociatedGroups[j].SkillLevel;
                var QueueAvailable = AssociatedGroups[j].IsAvailable;
                var QueueAvailableText = "true";

                var $QueueSpan = $("<span/>");
                if (QueueAvailable) {
                    QueueColor = '#44515d';
                } else {
                    QueueColor = "#ddd";
                }
                $QueueSpan.css({ "font-size": "14px", "text-transform": "uppercase" });
                $QueueSpan.text(QueueAbbr);
                //$QueueSpan.appendTo($colDiv);



                if (QueueAvailable) {
                    $imgAvaiable = $("<img src='Content/images/tick.png' style='cursor: pointer;' data-id='" + QueueId + "' height='14' width='14' />");
                    //$imgAvaiable = $("<img src='Content/images/tick.png' style='cursor: pointer;' data-id='" + QueueId + "' height='14' width='14' onclick='ChangeIcon(this," + AssosiatedGroup + ")' />");
                    $QueueSpan.prepend($("<i class='fa fa-circle' style='color:" + AssociatedGroups[j].Color + ";padding-right:25px;padding-top:2px;'></i>"));
                } else {
                    QueueAvailableText = "false";
                    $imgAvaiable = $("<img src='Content/images/cross.png' style='cursor: pointer;' data-id='" + QueueId + " height='14' width='14' />");
                    $QueueSpan.prepend($("<i class='fa fa-circle' style='color:" + QueueColor + ";padding-right:25px;padding-top:2px;'></i>"));
                }

                $imgAvaiable.on("click", { AssosiatedGroup: AssociatedGroups[j] }, ChangeIcon);


                $td = $("<td/>");
                $td.css({ "text-align": "left", "padding": "5px 10px 5px 10px", "color": QueueColor });
                $td.append($QueueSpan);
                $td.appendTo($tr);

                $td = $("<td/>");
                $td.css({ "text-align": "left", "padding": "5px 10px 5px 10px", "color": QueueColor });
                $td.append(QueueName);
                $td.appendTo($tr);

                $td = $("<td/>");
                $td.css({ "text-align": "center", "padding": "5px 10px 5px 10px", "color": QueueColor });
                $td.append(QueueLevel);
                $td.appendTo($tr);

                $td = $("<td/>");
                $td.css({ "text-align": "center", "padding": "0px", "vertical-align": "top" });
                $td.append($imgAvaiable);
                $td.appendTo($tr);

                $tr.appendTo($tblAgentSkills);
            }

            SetAgentStateOnPopup(AgentObj);
            $('#AgentDiscModal').modal('show');
        }

        function ChangeIcon( event ) {
            var AssociatedGroup = event.data.AssosiatedGroup;
            if ($(this).attr("src").indexOf("cross.png") > -1)
            {
                AssociatedGroup.IsAvailable = true;
                $(this).attr("src", "Content/images/tick.png");
            }
            else
            {
                AssociatedGroup.IsAvailable = false;
                $(this).attr("src", "Content/images/cross.png");
            }

            var row = $(this).parent().parent();
            row.find('td').each(function () {                
                var QueueColor = "#ddd";
                $(this).find('i').css("color", QueueColor);
                if(AssociatedGroup.IsAvailable) {
                    QueueColor = '#44515d';
                    $(this).find('i').css("color", AssociatedGroup.Color);
                }
                $(this).css("color",QueueColor);
            });

            var Param = { "AgentExtension": AgentExtension, "Groups": AssociatedGroup };
            //console.dir("SET_AGENT_QUEUE_AVAILABILITY");
            //console.dir(Param);
            SendMessageToWebSocket("SET_AGENT_QUEUE_AVAILABILITY", Param);
        }

        function CloseServerConnectionModal() {
            //$('#ServerConnectionModal').modal('hide');
        }

        function CloseAgentDiscModal() {
            if (objPopupTimer != null)
                clearInterval(objPopupTimer);
            AgentExtension = "";
            $('#AgentDiscModal').modal('hide');
        }

        function SetAgentState(AgentObj) {
            //alert(JSON.stringify(AgentObj));
            var AgentStateArrray = [];
            var AgentStateFilter = [];
            var Name = AgentObj.Name;
            var Gender = AgentObj.Gender;
            var Extension = AgentObj.AgentExtension;
            var ImagePath = AgentObj.ImagePath;
            var IsLoggedin = AgentObj.IsLoggedin;  //false
            var RaiseHand = AgentObj.RaiseHand; //false
            var AgentState = AgentObj.AgentState;// 0 1
            var IsAvailable = AgentObj.IsAvailable; //false
            var UnAvailibilityReason = AgentObj.UnAvailibilityReason;
            var AssociatedGroups = AgentObj.AssociatedGroups;
            var LastStateTime = AgentObj.LastStateTime;
            var CallType = AgentObj.CallType;
            var LastGroupName = AgentObj.LastGroupName;
            var _LastGroupName = "";
            var IsAgentAddedFirstTime = false;
          

            var PhoneExtension = AgentObj.PhoneExtension;

            if (PhoneExtension.trim() == "")
                PhoneExtension = Extension;


            if (AgentStateTimer[Extension] == undefined || AgentStateTimer[Extension] == null) {
                AgentStateTimer[Extension] = { "status": "", "timer": "" };
                IsAgentAddedFirstTime = true;
            }
            
            var objAgentStateTime = AgentStateTimer[Extension];

            // if (!$('#cb_Avatar').prop('checked')) {
            if (!$('#cb_Avatar').is(":checked")) {
                $("#AgentImageDiv_" + Extension).hide();
            }
            else {
                $("#AgentImageDiv_" + Extension).show();
            }

            var nooftiles = $("#ddlNoOfTiles").val();
            NumberOfAgents = $("#ddlNoOfTiles").val();


            //#region Agent Queues
            $("#AgentQueueRow_" + Extension).empty();
            var $Queue = $("#AgentQueueRow_" + Extension);
            var $rowDiv;
            var $colDiv;
            var textAlign = 'left';
            for (var j = 0; j < AssociatedGroups.length; j++) {

                if (j % 3 == 0) {
                    $rowDiv = $("<div/>", { "class": "row" });
                    $rowDiv.css({ "text-align": "center" });
                    $rowDiv.appendTo($Queue);
                    textAlign = 'left';
                }
                else if (j % 3 == 1) {
                    textAlign = 'center';
                }
                else {
                    textAlign = 'right';
                }

                $colDiv = $("<div/>", { "class": "col-md-4" });
                $colDiv.css({ "text-align": textAlign, "padding": "0px" });
                $colDiv.appendTo($rowDiv);

                var AvaialableColor = AssociatedGroups[j].Color;
                var $QueueSpan = $("<span/>");

                if (AssociatedGroups[j].IsAvailable) {
                    $QueueSpan.css({ "font-size": "14px", "text-transform": "uppercase" });
                } else {
                    AvaialableColor = "#ddd";
                    $QueueSpan.css({ "font-size": "14px", "text-transform": "uppercase", "color": AvaialableColor });
                }

                $QueueSpan.text(AssociatedGroups[j].QueueAbr);
                $QueueSpan.appendTo($colDiv);
                if (nooftiles <= 6)//Zeeshan
                    $QueueSpan.prepend($("<i class='fa fa-circle' style='color:" + AvaialableColor + ";padding-right:5px;padding-top:2px;'></i>"));
            }
            //#end region Agent Queues
            
            

            if (UnAvailibilityReason == null || UnAvailibilityReason.trim() == "") {
                UnAvailibilityReason = "Unavailable"
            }

            var _CallType = "External";
            if (CallType == "I") {
                _CallType = "Internal";
            }
            
            $("#AgentTimerLabel_" + Extension).show();
            $("#AgentLogoutLabel_" + Extension).hide();
            var Color = "";
            var Status = "";

            if (!IsLoggedin) {
                Color = "#a1a1a1"; //grey
                Status = "Logged out";
                AgentStateArrray.push("LoggedOut");
                $("#AgentTimerLabel_" + Extension).hide();
                $("#AgentLogoutLabel_" + Extension).show();
                var currentDate = new Date(Date.now() - (LastStateTime * 1000));

                const options = { month: 'short', day: 'numeric', hour: "numeric", minute: "numeric", hour12:false};   
                $("#AgentLogoutLabel_" + Extension).text(currentDate.toLocaleString('en-us', options));
            }
            else if (!IsAvailable) {
                Color = "#faac16"; //orange
                Status = "Unavailable";
                AgentStateArrray.push("Unavailable");
            }
            else if (AgentState == "1" || AgentState == "7" || AgentState == "4") {//1 available 7 Idle 
                Color = "#23b14d"; //green
                Status = "Available";
                AgentStateArrray.push("Available");
            }
            else if (AgentState == "5") {//Ringing_IN_ACD 
                Color = "#f91627"; //red
                Status = "Ringing (ACD)";
                _LastGroupName = LastGroupName;
                AgentStateArrray.push("Ringing");
            }
            else if (AgentState == "9") {//Ringing_IN_ACD 
                Color = "#f91627"; //red
                Status = "Routing";
                _LastGroupName = LastGroupName;
                AgentStateArrray.push("Routing");
            }
            else if (AgentState == "6") {//ACD_Busy 
                Color = "#f91627"; //red
                Status = "Connected (ACD)";
                _LastGroupName = LastGroupName;
                AgentStateArrray.push("Connected");
            }
            else if (AgentState == "17") {//Ringing_IN_ACD 
                Color = "#f91627"; //red
                Status = "Ringing Callback";
                _LastGroupName = LastGroupName;
                AgentStateArrray.push("Ringing");
            }
            else if (AgentState == "18") {//ACD_Busy 
                Color = "#f91627"; //red
                Status = "Connected Callback";
                _LastGroupName = LastGroupName;
                AgentStateArrray.push("Connected");
            }
            else if (AgentState == "2") {//Clerical_Work 
                Color = "#00aded"; //blue
                Status = "Wrap-up";
                AgentStateArrray.push("WrapUp");
            }
            else if (AgentState == "14") {// 13 Busy_OutboundCall  14 Non_Acd_Ringing 
                Color = "#f91627"; //red
                Status = "Ringing (" + _CallType + ")";
                AgentStateArrray.push("Ringing");
            }
            else if (AgentState == "13") {// 13 Busy_OutboundCall  14 Non_Acd_Ringing 
                Color = "#f91627"; //red
                Status = "Connected Out";
                AgentStateArrray.push("Connected");
            }
            else if (AgentState == "8") {//Non_Acd_Busy 
                Color = "#f91627"; //red
                Status = "Connected (" + _CallType + ")";
                AgentStateArrray.push("Connected");
            }
            else if (AgentState == "28") {//Busy_OutgoingDialing 
                Color = "#f91627"; //red
                Status = "Off Hook";
                AgentStateArrray.push("OffHook");
            }
            else if (AgentState == "11") {//Busy_OutgoingDialing 
                Color = "#f91627"; //red
                Status = "Ringing Out";
                AgentStateArrray.push("Ringing");
            }
            else if (AgentState == "15") {//15 Acd_OnHold
                Color = "#f91627"; //red
                Status = "Held (ACD)";
                _LastGroupName = LastGroupName;
                AgentStateArrray.push("Held");
            }
            else if (AgentState == "19") {//15 Acd_OnHold
                Color = "#f91627"; //red
                Status = "Held Callback";
                _LastGroupName = LastGroupName;
                AgentStateArrray.push("Held");
            }
            else if (AgentState == "16") {//16 Non_Acd_OnHold 
                Color = "#f91627"; //red
                Status = "Held (" + _CallType + ")";
                AgentStateArrray.push("Held");
            }
            else {
                Color = "#faac16"; //orange
                Status = "Occupied"; //unknown
                AgentStateArrray.push("Occupied");
            }

            //if (objAgentStateTime.status == "" || objAgentStateTime.status != Status) {

            //    if ((AgentState == "6" || AgentState == "15" || AgentState == "18" || AgentState == "19") && (objAgentStateTime.status == "Held (ACD)" || objAgentStateTime.status == "Connected (ACD)" || objAgentStateTime.status == "Held Callback" || objAgentStateTime.status == "Connected Callback")) {
            //        //To combine Held and Connected record, skip it
            //    }
            //    else
                {

                    if (objAgentStateTime.status != "")
                        clearInterval(objAgentStateTime.timer);

                    objAgentStateTime.status = Status;

                    if ($('#AgentTimerLabel_' + Extension).is(":visible")) {
                        var index = 1;

                       // if (IsAgentAddedFirstTime)
                            index = LastStateTime;

                        var secFormat = secondsTimeSpanToHMS(index);
                        $("#AgentTimerLabel_" + Extension).text(secFormat);

                        objAgentStateTime.timer = setInterval(function () {
                            secFormat = secondsTimeSpanToHMS(index);
                            $("#AgentTimerLabel_" + Extension).text(secFormat);
                            index++;
                        }, 1000);
                    }

                    AgentStateTimer[Extension] = objAgentStateTime;
                }
           // }

            var LoadThisAgent = false;
            for (var i = 0; i < AssociatedGroups.length; i++) {
                if ($.inArray(AssociatedGroups[i].Name, CheckedQueues) > -1) {
                    for (var j = 0; j < AgentStateArrray.length; j++) {
                        if ($.inArray(AgentStateArrray[j], CheckedAgentStates) > -1) {
                            LoadThisAgent = true;
                            break;
                        }
                    }
                }
            }

            if (AssociatedGroups.length == 0) {
                if ($.inArray('No Skill', CheckedQueues) > -1) {
                    LoadThisAgent = true;
                }
            }


            if (LoadThisAgent) {
                $("#AgentIndexed_" + Extension + "").show();
            }
            else {
                $("#AgentIndexed_" + Extension + "").hide();
            }

            $("#AgentOuterDiv_" + Extension).css({ "border": "1px solid " + Color, "border-radius": "10px" });
            $("#AgentColorDiv_" + Extension).css({ "height": "30px", "background-color": "" + Color + "", "border-top-left-radius": "10px", "border-top-right-radius": "10px" });

            if (Color == "#f91627") {
                $("#AgentTimerLabel_" + Extension).addClass("whiteColor");
            }
            else {
                $("#AgentTimerLabel_" + Extension).removeClass("whiteColor");
            }

            $("#AgentStatusSpan_" + Extension).removeAttr("title");
            $("#AgentStatusSpan_" + Extension).text(Status);

            $("#AgentExtensionSpan_" + Extension).removeAttr("title");
            $("#AgentExtensionSpan_" + Extension).text(PhoneExtension);

            if (!IsAvailable) {
                $("#AgentStatusSpan_" + Extension).attr("title", UnAvailibilityReason);
            }
            else if (_LastGroupName != "") {
                $("#AgentStatusSpan_" + Extension).attr("title", _LastGroupName);
            }

            if (RaiseHand)
                $("#AgentRaiseHandDiv_" + Extension).css({ "display": "block" });
            else
                $("#AgentRaiseHandDiv_" + Extension).css({ "display": "none" });

            if (!IsLoggedin || AgentState == "10") {

                $("#AgentNameSpan_" + Extension).css({ "color": "#a1a1a1" });
                $("#AgentExtensionSpan_" + Extension).css({ "color": "#a1a1a1" });
                $("#AgentStatusSpan_" + Extension).css({ "color": "#a1a1a1" });
                $("#AgentOuterDiv_" + Extension).css({ "opacity": "0.3" });

            }
            else {
                $("#AgentNameSpan_" + Extension).css({ "color": "#44515d" });
                $("#AgentExtensionSpan_" + Extension).css({ "color": "#44515d" });
                $("#AgentStatusSpan_" + Extension).css({ "color": "green" });
                $("#AgentOuterDiv_" + Extension).css({ "opacity": "1" });

            }
        }

function secondsTimeSpanToHMS(s) {
		
		    
            var h = Math.floor(s / 3600); //Get whole hours
			
			if(h >= 0){
				s -= h * 3600;
				var m = Math.floor(s / 60); //Get remaining minutes
				s -= m * 60;
				return (h < 10 ? '0' + h : h) + ":" + (m < 10 ? '0' + m : m) + ":" + (s < 10 ? '0' + s : s); //zero padding on minutes and seconds
			}
			else{
				var m = Math.floor(s / 60); //Get remaining minutes
				s -= m * 60;
				return  (m < 10 ? '0' + m : m) + ":" + (s < 10 ? '0' + s : s); //zero padding on minutes and seconds
			}
        }

        function SetAgentStateOnPopup(AgentObj) {

            console.dir(AgentObj);
            var Name = AgentObj.Name;
            var Gender = AgentObj.Gender;
            var Extension = AgentObj.AgentExtension;
            var PhoneExtension = AgentObj.PhoneExtension;
            var ImagePath = AgentObj.ImagePath;
            var IsLoggedin = AgentObj.IsLoggedin;  //false
            var RaiseHand = AgentObj.RaiseHand; //false
            var AgentState = AgentObj.AgentState;// 0 1
            var IsAvailable = AgentObj.IsAvailable; //false
            var UnAvailibilityReason = AgentObj.UnAvailibilityReason;
            var AssociatedGroups = AgentObj.AssociatedGroups;
            var CallType = AgentObj.CallType;

            $('#AgentTimerLabelOnPopup').hide();
            //if ($('#AgentTimerLabel_' + Extension).css('display') == 'none') {}
            if ($('#AgentTimerLabel_' + Extension).is(":visible")) {
                $('#AgentTimerLabelOnPopup').show();
            }


            $("#AgentQueueRowOnPopup_" + Extension).empty();
            var $Queue = $("#AgentQueueRowOnPopup_" + Extension);
            var $rowDiv;
            var $colDiv;
            var textAlign = 'left';
            for (var j = 0; j < AssociatedGroups.length; j++) {

                if (j % 3 == 0) {
                    $rowDiv = $("<div/>", { "class": "row" });
                    $rowDiv.css({ "text-align": "center" });
                    $rowDiv.appendTo($Queue);
                    textAlign = 'left';
                }
                else if (j % 3 == 1) {
                    textAlign = 'center';
                }
                else {
                    textAlign = 'right';
                }

                $colDiv = $("<div/>", { "class": "col-md-4" });
                $colDiv.css({ "text-align": textAlign, "padding": "0px" });
                $colDiv.appendTo($rowDiv);


                var AvaialableColor = AssociatedGroups[j].Color;
                var $QueueSpan = $("<span/>");

                if (AssociatedGroups[j].IsAvailable) {
                    $QueueSpan.css({ "font-size": "14px", "text-transform": "uppercase" });
                } else {
                    AvaialableColor = "#ddd";
                    $QueueSpan.css({ "font-size": "14px", "text-transform": "uppercase", "color": AvaialableColor });
                }

                $QueueSpan.text(AssociatedGroups[j].QueueAbr);
                $QueueSpan.appendTo($colDiv);

                $QueueSpan.prepend($("<i class='fa fa-circle' style='color:" + AvaialableColor + ";padding-right:5px;padding-top:2px;'></i>"));
            }




            if (UnAvailibilityReason == null || UnAvailibilityReason.trim() == "") {
                UnAvailibilityReason = "Unavailable"
            }

            var _CallType = "External";
            if (CallType == "I") {
                _CallType = "Internal";
            }

            var Color = "";
            var Status = "";


            if (!IsLoggedin) {
                Color = "#a1a1a1"; //grey
                Status = "Logged out";
            }
            else if (!IsAvailable) {
                Color = "#faac16"; //orange
                Status = "Unavailable";
            }
            else if (AgentState == "1" || AgentState == "7") {//1 available 7 Idle 
                Color = "#23b14d"; //green
                Status = "Available";
            }
            else if (AgentState == "5") {//Ringing_IN_ACD 
                Color = "#f91627"; //red
                Status = "Ringing (ACD)";
            }
            else if (AgentState == "17") {//Ringing_IN_ACD 
                Color = "#f91627"; //red
                Status = "Ringing Callback";
            }
            else if (AgentState == "6") {//ACD_Busy 
                Color = "#f91627"; //red
                Status = "Connected (ACD)";
            }
            else if (AgentState == "18") {//ACD_Busy 
                Color = "#f91627"; //red
                Status = "Connected Callback";
            }
            else if (AgentState == "2") {//Clerical_Work 
                Color = "#00aded"; //blue
                Status = "Wrap-up";
            }
            else if (AgentState == "14") {// 13 Busy_OutboundCall  14 Non_Acd_Ringing 
                Color = "#f91627"; //red
                Status = "Ringing (" + _CallType + ")";
            }
            else if (AgentState == "13") {// 13 Busy_OutboundCall  14 Non_Acd_Ringing 
                Color = "#f91627"; //red
                Status = "Connected Out";
            }
            else if (AgentState == "8") {//Non_Acd_Busy 
                Color = "#f91627"; //red
                Status = "Connected (" + _CallType + ")";
            }
            else if (AgentState == "28") {//Busy_OutgoingDialing 
                Color = "#f91627"; //red
                Status = "Off Hook";
            }
            else if (AgentState == "11") {//Busy_OutgoingDialing 
                Color = "#f91627"; //red
                Status = "Ringing Out";
            }
            else if (AgentState == "15") {//15 Acd_OnHold
                Color = "#f91627"; //red
                Status = "Held (ACD)";
            }
            else if (AgentState == "19") {//15 Acd_OnHold
                Color = "#f91627"; //red
                Status = "Held Callback";
            }
            else if (AgentState == "16") {//16 Non_Acd_OnHold 
                Color = "#f91627"; //red
                Status = "Held (" + _CallType + ")";
            }
            else {
                Color = "#faac16"; //orange
                Status = "Occupied"; //unknown
            }


            $("#AgentOuterDivOnPopup_" + Extension).css({ "border": "1px solid " + Color, "border-radius": "10px" });
            $("#AgentColorDivOnPopup_" + Extension).css({ "height": "30px", "background-color": "" + Color + "", "border-top-left-radius": "10px", "border-top-right-radius": "10px" });

            if (!IsAvailable) {
                $("#AgentStatusSpanOnPopup_" + Extension).attr("title", UnAvailibilityReason).text(Status);
            }
            else {
                $("#AgentStatusSpanOnPopup_" + Extension).removeAttr("title").text(Status);
            }

            if (Color == "#f91627") {
                $("#AgentTimerLabelOnPopup").addClass("whiteColor");
            }
            else {
                $("#AgentTimerLabelOnPopup").removeClass("whiteColor");
            }

            if (RaiseHand)
                $("#AgentRaiseHandDivOnPopup_" + Extension).css({ "display": "block" });
            else
                $("#AgentRaiseHandDivOnPopup_" + Extension).css({ "display": "none" });


            //if (!IsLoggedin || AgentState == "10") {

            //    $("#AgentNameSpanOnPopup_" + Extension).css({ "color": "#a1a1a1" });
            //    $("#AgentExtensionSpanOnPopup_" + Extension).css({ "color": "#a1a1a1" });
            //    $("#AgentStatusSpanOnPopup_" + Extension).css({ "color": "#a1a1a1" });
            //    $("#AgentOuterDivOnPopup_" + Extension).css({ "opacity": "0.3" });

            //}
            if (!IsLoggedin || AgentState == "10") {

                $("#AgentNameSpanOnPopup_" + Extension).css({ "color": "#a1a1a1" });
                $("#AgentExtensionSpanOnPopup_" + Extension).css({ "color": "#a1a1a1" });
                $("#AgentStatusSpanOnPopup_" + Extension).css({ "color": "#a1a1a1" });
                $("#AgentOuterDivOnPopup_" + Extension).css({ "opacity": "0.3" });

            }
            else {
                $("#AgentNameSpanOnPopup_" + Extension).css({ "color": "#44515d" });
                $("#AgentExtensionSpanOnPopup_" + Extension).css({ "color": "#44515d" });
                $("#AgentStatusSpanOnPopup_" + Extension).css({ "color": "green" });
                $("#AgentOuterDivOnPopup_" + Extension).css({ "opacity": "1" });

            }

            if (!IsLoggedin) {
                $("#btnLogAgentIn").text("Log Agent In");
                $("#btnSetAgentAvailable").text("Set Available");
                $("#btnLogAgentIn").css("background-color", "#f91627");
                $("#btnSetAgentAvailable").css("background-color", "#f91627");
                $("#btnSetAgentAvailable").prop("disabled", true);

                if (PhoneExtension.trim() == "")
                    PhoneExtension = Extension;

                $("#txtLogAgentInPE").val(PhoneExtension);
                $("#txtLogAgentInPE").prop("disabled", false);
            }
            else {
                $("#btnLogAgentIn").text("Log Agent Out");
                $("#btnLogAgentIn").css("background-color", "green");
                $("#btnSetAgentAvailable").prop("disabled", false);
                if (!IsAvailable) {
                    $("#btnSetAgentAvailable").text("Set Available");
                    $("#btnSetAgentAvailable").css("background-color", "#f91627");
                }
                else {
                    $("#btnSetAgentAvailable").text("Set Unavailable");
                    $("#btnSetAgentAvailable").css("background-color", "green");
                }
                $("#txtLogAgentInPE").val(PhoneExtension);
                $("#txtLogAgentInPE").prop("disabled", true);
            }
        }

        function LoadWebSocket() {
            
            $.ajax({
                type: "POST",
                url: "Agents.aspx/GetSocketAuthentications",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                  
                    UFID = data.d.UFID;
                    UserName = data.d.UserName;
                    Password = data.d.Password;
                    ServerIp = data.d.ServerIp;
                    console.log("Socket start here");
                    console.log(data);
                  // wsUri = data.d.WSURI;
                    wsUri= "wss://192.168.0.53:6100";
                    if (websocket == null) {
                        ConfigWebSocket();
                    }
                }
            });
        }
        function ConfigWebSocket() {
            //alert("ConfigWebSocket");

            websocket = new WebSocket(wsUri);

          

            websocket.onopen = function (evt) { onOpen(evt) };
            websocket.onclose = function (evt) { onClose(evt) };
            websocket.onmessage = function (evt) { onMessage(evt) };
            websocket.onerror = function (evt) { onError(evt) };
            FirstAgentMessage = true;
            socketInterval += setInterval(ServerHeartBeat, 10000);

        }

        function onOpen(evt) {
            //alert();
            console.log("Socket opened.");
            //$('#imgSocketServerConnectState').css({ "color": "yellow" });
            //$('#lblSocketServerConnectMessage').text("Server connection opened. Trying to Authenticate User.");
            //$('#ServerConnectionModal').modal('show');

            $('#lblSocketServerConnectMessage2').css({ "color": "green" });
            $('#lblSocketServerConnectMessage2').text("Server connection opened. Trying to Authenticate User.");
            $("#lblSocketServerConnectMessage2").show();
            $("#AgentsDiv").hide();
            SendAuthenticationMessageToServer();

        }
        function onClose(evt) {
            console.log("Socket closed.");
            //$('#imgSocketServerConnectState').css({ "color": "#f91627" });
            //$('#lblSocketServerConnectMessage').text("Server connection closed");
            //$('#ServerConnectionModal').modal('show');

            $('#lblSocketServerConnectMessage2').css({ "color": "#f91627" });
            $('#lblSocketServerConnectMessage2').text("Server connection closed.");
            $("#lblSocketServerConnectMessage2").show();
            $("#AgentsDiv").hide();
        }
        function onError(evt) {
            console.log("Error from socket :");
            //$('#imgSocketServerConnectState').css({ "color": "#f91627" });
            //$('#lblSocketServerConnectMessage').text("An Error occured while connecting to the Server");
            //$('#ServerConnectionModal').modal('show');

            $('#lblSocketServerConnectMessage2').css({ "color": "#f91627" });
            $('#lblSocketServerConnectMessage2').text("An Error occured while connecting to the Server.");
            $("#lblSocketServerConnectMessage2").show();
            $("#AgentsDiv").hide();
        }
        function onMessage(msg) {

            var objResponse = jQuery.parseJSON(msg.data);
            var objResponseValue = objResponse.Value;

            if (objResponse.Type == "AGENT_STATE") {
                //console.log("objResponse: " + objResponse.Type);
                //console.dir(objResponseValue);

                AgentResponseObj = objResponseValue;

                SetAgentState(AgentResponseObj);
                SetAgentStateOnPopup(AgentResponseObj);

            }
            else if (lockMessage == false) {
                lockMessage = true;
                setTimeout(function () { ParseMessage(msg); }, 50);
            }


        }
        function ParseMessage(msg) {
            var objResponse = jQuery.parseJSON(msg.data);
            var objResponseValue = objResponse.Value;

            //console.log("ParseMessage - objResponse: " + objResponse.Type);
            //console.dir(objResponseValue);

            if (objResponse.Type == "LOGIN_RESPONSE") {

                if (objResponseValue.IsSuccess) {

                    //$('#imgSocketServerConnectState').css({ "color": "green" });
                    //$('#lblSocketServerConnectMessage').text("Connected to Server.");
                    $('#lblSocketServerConnectMessage2').text("Connected to Server.");
                    //$('#ServerConnectionModal').modal('hide');
                    $("#AgentsDiv").show();
                    $("#lblSocketServerConnectMessage2").hide();
                    //alert(objResponseValue.Groups.split(","));
                    

                   

                    AllQueues = objResponseValue.Groups.split(",");

                  //  var QueueIds = AllQueues.split(':')[0];
                   // QueueIds = CheckedQueues2;

                    //$('#QueuesList input').each(function (index) {
                    //    let value = `${index + 1}`;
                    //    console.log(value + " : Index value");
                    //    if ($.inArray(value, CheckedQueues2) != -1) {
                    //        //  
                          
                    //    }
                    //});



                   
                    LoadAgentStateFilterList();
                    LoadQueuesList();
                    

                                    }
                else {

                    //$('#imgSocketServerConnectState').css({ "color": "Yellow" });
                    //$('#lblSocketServerConnectMessage').text("User authentication failed.");
                    $('#lblSocketServerConnectMessage2').css({ "color": "red" });
                    $('#lblSocketServerConnectMessage2').text("User authentication failed.");
                    //$('#ServerConnectionModal').modal('show');
                    $("#AgentsDiv").hide();
                }

            }
            else if (objResponse.Type == "AGENTSTATUS_LIST") {
                if (FirstAgentMessage == true || AgentChange == true) {
                    console.log("First Agents list received");
                    FirstAgentMessage = false;
                    AgentChange = false
                    AgentsArray = objResponseValue;
                    LoadAgentsCompleteInfo();
                }
                else {
                    console.log("Partial Agents list:::");
                    AgentsArray = objResponseValue;
                    LoadAgentsPartialInfo();
                }

            }
            else if (objResponse.Type == "QUEUE_CHANGE") {
                AllQueues = objResponseValue.Groups.split(",");
                //$.each(UncheckedQueues, function (index, value) {
                //    CheckedQueues
                //});

                LoadQueuesList();
            }
            else if (objResponse.Type == "AGENT_CHANGE") {
                AgentChange = true;

            }

            lockMessage = false;

        }


        function RaiseHandAck(e) {

            var Extension = e.data.Extension;
            var Param = { "AgentExtension": Extension, "IsLogin": false, "IsAvailable": false, "RaiseHand": false, "UnAvailibilityReason": "", "AutoAnswer": false, "PhoneExtension": "" };
            SendMessageToWebSocket("SET_RAISEHAND", Param);
        }

        function SetAgentAvailable() {
            var Status = false;
            if ($("#btnSetAgentAvailable").text() == "Set Available")
                Status = true;

            var Param = { "AgentExtension": AgentExtension, "IsLogin": false, "IsAvailable": Status, "RaiseHand": false, "UnAvailibilityReason": "", "AutoAnswer": false, "PhoneExtension": "" };
            SendMessageToWebSocket("SET_AVAILABILITY", Param);
        }

        function LogAgentIn() {
            var Status = false;
            var physicalExt = $("#txtLogAgentInPE").val();
            if ($("#btnLogAgentIn").text() == "Log Agent In") {
                if ($.trim(physicalExt) == "") {
                    $().toastmessage('showErrorToast', 'Please provide physical extension.');
                    return;
                }
                Status = true;
            }
            else {
                $("#txtLogAgentInPE").val('');
            }
            $("#txtLogAgentInPE").prop("disabled", Status);

            var Param = { "AgentExtension": AgentExtension, "IsLogin": Status, "IsAvailable": false, "RaiseHand": false, "UnAvailibilityReason": "", "AutoAnswer": false, "PhoneExtension": physicalExt };
            SendMessageToWebSocket("SET_LOGIN", Param);

        }

        function QueueServiceInOut(objQueueInfo) {
            var QueueId = $(objQueueInfo).data("id");
            var QueueService = $(objQueueInfo).data("service");

            var MsgType = 'QUEUE_INSERVICE';
            if (QueueService == 1)
                MsgType = 'QUEUE_OUTOFSERVICE';

            var Param = { "AccountId": -1, "EntityId": QueueId };
            SendMessageToWebSocket(MsgType, Param);

        }

        function ShowEmergencyClosurePopup() {
            $('#ModalEmergencyClosure').modal('show');
        }

        function EmergencyClosure() {
            $('#ModalEmergencyClosure').modal('hide');
            var Param = { "AccountId": -1, "EntityId": -1 };
            SendMessageToWebSocket("EMERGENCY_CLOSURE", Param);

        }

        function SendAuthenticationMessageToServer() {
            var Param = { "UFID": UFID, "UserName": UserName, "Password": Password, "IsSuperVisor": true };
            if (websocket.readyState === websocket.OPEN) {

                SendMessageToWebSocket("LOGIN", Param);
            }
            else {

                //setTimeout(SendMessageToWebSocket("LOGIN", Param), 10000);
                setTimeout(function () {
                    SendMessageToWebSocket("LOGIN", Param);
                }, 10000)

            }
        }
        function SendMessageToWebSocket(Type, Param) {
            //alert();
            if (websocket.readyState === websocket.OPEN) {
                var msg = { "Type": "" + Type + "", "Value": Param }
                msg = JSON.stringify(msg);
                websocket.send(msg);

            }
        }
        function ServerHeartBeat() {
            if (websocket.readyState === websocket.CLOSED) {
                clearInterval(socketInterval);
                ConfigWebSocket();
            }
            else if (websocket.readyState === websocket.OPEN) {

            }
        }

        $("#cb_Avatar").on("change.bootstrapSwitch", function (e) {

          //  OnAvatorClick(e.tar.checked);
            LoadAgentsCompleteInfo();
        });

        $("#ddlNoOfTiles").on('change', function (e) {
           // LoadAgentsCompleteInfo();
            NumberOfAgents = $("#ddlNoOfTiles").val();
            LoadAgentsCompleteInfo();

        });

    </script>
</asp:Content>
