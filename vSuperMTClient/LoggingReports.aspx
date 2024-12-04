<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisor.Master" AutoEventWireup="true" CodeBehind="LoggingReports.aspx.cs" Inherits="vSuperMTClient.LoggingReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="padding: 0px;">
        <div class="modal fade" id="AddScheduleModal" tabindex="-1" role="basic" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content" style="width: 640px;">
                    <div class="row" style="line-height: 46px; background-color: #ffffff; width: 100%; margin-left: 0%; vertical-align: middle; border-bottom: 1px solid #e5e5e5">
                        <div class="col-md-4" style="margin-top: 1%;">
                            <span style="font-size: 18px; color: #404040;">Report Schedule</span>
                        </div>
                    </div>
                    <div id="ErrorDivAdd" class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6; display: none;">
                        <div class="col-md-12" style="padding-left: 0px;">
                            <label class="form-control Label" style="color: red" id="lblErrorAdd"></label>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label " style="padding-left: 10px">Name<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-md-9">
                            <input id="txbScheduleName" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <label class="form-control Label" style="padding-left: 10px">Schedule</label>
                        </div>
                        <div class="col-md-9">
                            <select id="lstScheduleInterval" class="form-control">
                                <option value="1">Daily</option>
                                <option value="2">Weekly</option>
                                <option value="3">Monthly</option>

                            </select>
                        </div>
                    </div>
                    <div id="SchedulValueRow" class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6; display: none;">
                        <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3" style="padding-left: 0px;">
                            <span class="form-control Label">On</span>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-9 col-lg-9">
                            <select id="ddScheduleValue" class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3" style="padding-left: 0px;">
                            <span class="form-control Label">At</span>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-9 col-lg-9">
                            <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8" style="padding-left: 0px; width: 42%;">

                                <select id="ddScheduleTimeHours" class="form-control"></select>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8" style="padding-left: 0px; width: 42%;">
                                <select id="ddScheduleTimeMinutes" class="form-control"></select>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8" style="padding-left: 0px; padding-top: 15px; padding-right: 0px; width: 16%; text-align: right;">
                                <span>[HH:MM]</span>
                            </div>

                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6;">

                        <div class="col-md-3">
                            <label class=" control-label">Email</label>
                        </div>
                        <div class="col-md-9">
                            <div class="input-group">
                                <input id="txbScheduleEmail" type="text" class="form-control" style="border-bottom: 0px !important;" />
                                <span class="input-group-btn" style="vertical-align: bottom">
                                    <button type="button" id="btnScheduleAddEmail" class="btn green" onclick="btnScheduleAddEmail_Click();" style="padding: 7px 14px;"><i class="icon-plus-sign"></i></button>

                                </span>
                            </div>
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-9">
                            <div class="input-group">
                                <select id="lstScheduleEmails" class="form-control" multiple="multiple" style="width: 100%;">
                                </select>
                                <span class="input-group-btn" style="vertical-align: bottom">
                                    <button type="button" id="btnScheduleRemoveEmail" class="btn green" onclick="btnScheduleRemoveEmail_Click();" style="padding: 7px 14px;"><i class="icon-trash"></i></button>
                                </span>
                            </div>
                        </div>

                    </div>
                   <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                          <div class="col-md-6" style="width: 472px;">
                        </div>
                        <div class="col-md-3" style="width: 80px;">
                            <button type="button" onclick="AddSchedule();" id="btnSchedule" class="btnFlat" style="width: 70px;">Apply</button>
                        </div>
                        <div class="col-md-3" style="width: 80px;">

                            <button type="button" class="btnFlat" data-dismiss="modal" style="width: 70px;">Close</button>
                        </div>
                     
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="ReportSettingsModal" tabindex="-1" role="basic" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content" style="width: 640px;">
                    <div class="row" style="line-height: 46px; background-color: #ffffff; width: 100%; margin-left: 0%; vertical-align: middle; border-bottom: 1px solid #e5e5e5">
                        <div class="col-md-4" style="margin-top: 1%;">
                            <span style="font-size: 18px; color: #404040;">Report Settings</span>
                        </div>
                    </div>
                    <div id="ErrorDivReportSettings" class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6; display: none;">
                        <div class="col-md-12" style="padding-left: 0px;">
                            <label class="form-control Label" style="color: red" id="lblErrorReportSettings"></label>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-4" style="padding-left: 0px;">
                            <label class="form-control Label " style="padding-left: 10px">Working Hours<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-md-8">
                            <input id="txbAgentWorkingHours" type="text" class="form-control" />
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                        <div class="col-md-4" style="padding-left: 0px;">
                            <label class="form-control Label " style="padding-left: 10px">Hangup Threshold<span style="color: red;">*</span></label>
                        </div>
                        <div class="col-md-8">
                            <input id="txbAgentHangUpThreshold" type="text" class="form-control" />
                        </div>
                    </div>

                    <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                        <div class="col-md-7" style="padding: 0px;">
                        </div>
                        <div class="col-md-5">
                            <div class="col-md-6" style="text-align: right; padding: 0px;">
                                <button type="button" onclick="UpdateReportSettings();" id="btnUpdateReportSettings" class="btnFlat" style="width: 70px;">Apply</button>
                            </div>
                            <div class="col-md-6" style="text-align: right; padding-right: 0px;">
                                <button type="button" class="btnFlat" data-dismiss="modal" style="width: 70px;">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="margin-top: 0%; width: 100%;">
            <div class="row" style="width: 100%;">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="">
                    <span style="font-size: 26px; color: #999999; margin-left: 0%;">Logging Reports</span>
                </div>
            </div>
            <div class="row" style="padding-top: 10px; padding-bottom: 20px; width: 100%;">
                <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div style="background-color: white; text-align: left; vertical-align: middle; line-height: 40px;">
                        <i class="icon-home" style="font-size: 20px; color: #999999; padding-left: 1%;"></i>
                        <a href="Home.aspx">
                            <span style="color: #999999;">Home</span>
                        </a>
                        <img src="../Content/images/next_image.png" style="margin-top: -3px;" />
                        <a href="LoggingReports.aspx">
                            <span style="color: #999999;">Logging Reports</span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                <div style="width: 100%; border-style: ridge; border-color: #4d5b69; border-width: 1px 1px 1px 1px;">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="line-height: 41px; background-color: #4d5b69;">
                            <span style="font-size: 18px; color: #fffeff;">Logging Reports</span>
                        </div>
                        <div class="col-xs-1 col-sm-1 col-md-1 col-lg-1" onclick="ReportSettings();" style="line-height: 43px; background-color: #4d5b69; cursor: pointer;display:none;">
                            <i id="btnReportSettings" class="icon-wrench" style="font-size: 20px; color: white; padding-left: 0%;"></i>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px; padding-bottom: 10px;">

                        <div class="col-md-3" style="padding-right: 0px;">
                            <span class="help-block">Logging Report</span>
                        </div>
                        <div class="col-md-9">
                            <select id="lstReports" class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="row" id="Sections" style="display: none;">

                        <div class="row SubSection" id="SDateRange" style="width: 100%; padding-top: 0px; padding-bottom: 0px; display: none;">
                            <div class="row" style="padding-top: 10px; padding-bottom: 10px;">

                                <div class="col-md-3" style="padding-right: 0px;">
                                    <span class="help-block">Date Range</span>
                                </div>
                                <div class="col-md-9">
                                    <select id="lstDateRangeOption" class="form-control">
                                        <option value="1">Today</option>
                                        <option value="2">Yesterday</option>
                                        <option value="3">This Week</option>
                                        <option value="4">Last Week</option>
                                        <option value="5">This Month</option>
                                        <option value="6">Last Month</option>
                                        <option value="7">Custom</option>
                                    </select>
                                </div>

                            </div>
                            <div class="row" id="CustomDatePickerRow" style="padding-top: 0px; padding-bottom: 0px; display: none;">
                                <div class="row" style="padding-top: 10px; padding-bottom: 0px;">
                                    <div class="col-md-3"></div>
                                    <div class="col-md-2" style="padding-right: 0px;">
                                        <span class="help-block">Start Date</span>
                                    </div>
                                    <div class="col-md-7">

                                        <div class="form-group">
                                            <div class="input-group date" id="dp1">
                                                <input id="txbDateFrom" class="form-control" size="16" type="text" value="" placeholder="From" />
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="padding-top: 10px; padding-bottom: 0px;">
                                    <div class="col-md-3"></div>
                                    <div class="col-md-2">
                                        <span class="help-block">End Date</span>
                                    </div>
                                    <div class="col-md-7">

                                        <div class="form-group">
                                            <div class="input-group date" id="dp2">
                                                <input id="txbDateTo" class="form-control" size="16" type="text" value="" placeholder="To" />
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="CustomTimePickerRow" style="padding-top: 0px; padding-bottom: 0px;">
                                <div class="row" style="padding-top: 10px; padding-bottom: 0px;">
                                    <div class="col-md-3"></div>
                                    <div class="col-md-2" style="padding-right: 0px;">
                                        <span class="help-block">Start Time</span>
                                    </div>
                                    <div class="col-md-7">

                                        <div class="form-group">
                                            <div class="input-group date" id="dp3">
                                                <input id="txbTimeFrom" class="form-control" size="16" type="text" value="" placeholder="From" />
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="padding-top: 10px; padding-bottom: 0px;">
                                    <div class="col-md-3"></div>
                                    <div class="col-md-2">
                                        <span class="help-block">End Time</span>
                                    </div>
                                    <div class="col-md-7">

                                        <div class="form-group">
                                            <div class="input-group date" id="dp4">
                                                <input id="txbTimeTo" class="form-control" size="16" type="text" value="" placeholder="To" />
                                                <span class="input-group-addon">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                     <%--   <div class="row SubSection" id="SGroups" style="width: 100%; padding-top: 10px; padding-bottom: 10px; display: none;">


                            <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                                <div class="col-md-3">
                                    <span class="help-block">Boards</span>
                                </div>
                                <div class="col-md-9">
                                    <select id="lstGroups" class="form-control" multiple="multiple">
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="row SubSection" id="SAgents" style="width: 100%; padding-top: 10px; padding-bottom: 10px; display: none;">


                            <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                                <div class="col-md-3">
                                    <span class="help-block">Agents</span>
                                </div>
                                <div class="col-md-9">
                                    <select id="lstAgents" class="form-control" multiple="multiple">
                                    </select>
                                </div>
                            </div>
                        </div>--%>
                        <div class="row SubSection" id="SExtensions" style="width: 100%; padding-top: 10px; padding-bottom: 10px; display: none;">


                            <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                                <div class="col-md-3">
                                    <span class="help-block">Extensions</span>
                                </div>
                                <div class="col-md-9">
                                    <select id="lstExtensions" class="form-control" multiple="multiple">
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="=row" id="SCreateReport" style="padding-bottom: 45px;">
                            <div class="col-md-6" style="width: 287px;"></div>
                            <div class="col-md-3" style="width: 110px;">
                                <input type="button" class="btnFlat" onclick="ScheduleReportModal();" title="Schedule" value="Schedule" />
                            </div>
                            <div class="col-md-3" style="width: 78px;">
                                <input type="button" class="btnFlat" onclick="ViewReport();" title="PDF" value="PDF" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <script>

        $(document).ready(function () {
            var Param = { "ReportType": "Logging" }
            //Fill Reports Groups Agents DropDowns
            $.ajax({
                type: "POST",
                url: "ReportsCommonMethods.aspx/GetReports",
                data: JSON.stringify(Param),
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d.length > 0) {
                        $("#lstReports").find('option').remove().end();
                        $("#lstReports").append("<option value=0>Select Report</option>");
                        for (var i = 0; i < data.d.length; i++) {
                            var RID = data.d[i].RID;
                            var ReportName = data.d[i].ReportName;
                            $("#lstReports").append("<option value=" + RID + ">" + ReportName + "</option>");

                        }
                    }
                }
            });
            //$.ajax({
            //    type: "POST",
            //    url: "LoggingReports.aspx/GetBoards",
            //    data: {},
            //    async: false,
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (data) {
            //        if (data.d.length > 0) {
            //            $("#lstGroups").find('option').remove().end();
            //            for (var i = 0; i < data.d.length; i++) {
            //                var huntGroupId = data.d[i].huntGroupId;
            //                var Title = data.d[i].Title;
            //                $("#lstGroups").append("<option value=" + huntGroupId + ">" + Title + "</option>");
            //            }
            //        }
            //    }
            //});
            //$.ajax({
            //    type: "POST",
            //    url: "LoggingReports.aspx/GetAgents",
            //    data: {},
            //    async: false,
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (data) {
            //        if (data.d.length > 0) {
            //            $("#lstAgents").find('option').remove().end();
            //            for (var i = 0; i < data.d.length; i++) {
            //                var AgentName = data.d[i].AgentName;
            //                $("#lstAgents").append("<option value='" + AgentName + "'>" + AgentName + "</option>");
            //            }
            //        }
            //    }
            //});

            $.ajax({
                type: "POST",
                url: "ReportsCommonMethods.aspx/GetExtensions",
                data: {},
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d.length > 0) {
                        $("#lstExtensions").find('option').remove().end();
                        for (var i = 0; i < data.d.length; i++) {
                            var Id = data.d[i].Id;
                            var Extension = data.d[i].Extension;
                            $("#lstExtensions").append("<option value='" + Extension + "'>" + Extension + "</option>");
                        }
                    }
                }
            });

            //Date Filter Criteria change , show hide  custom date pickers
            $('#lstDateRangeOption').on('change', function () {
                var DateRangeOption = this.value;
                if (parseInt(DateRangeOption) == 5) {
                    $("#CustomTimePickerRow").hide();
                    $("#CustomDatePickerRow").show();
                }
                else {
                    $("#CustomTimePickerRow").show();
                    $("#CustomDatePickerRow").hide();
                }
            });
            // Fill Hours Dropdown from 0-23
            for (var i = 0; i <= 23; i++) {
                var j = i;
                if (i < 10) {
                    j = "0" + i;
                }
                $("#ddScheduleTimeHours").append("<option value=" + i + ">" + j.toString() + "</option>");
            }
            // Fill Minutes Dropdown from 0-59
            for (var i = 0; i <= 59; i++) {
                var j = i;
                if (i < 10) {
                    j = "0" + i;
                }
                $("#ddScheduleTimeMinutes").append("<option value=" + i + ">" + j.toString() + "</option>");
            }
            // Groups Dropdown make multiselect
            //$('#lstGroups').multiselect({
            //    includeSelectAllOption: true,
            //    selectAllValue: 'multiselect-all',
            //    enableCaseInsensitiveFiltering: true,
            //    enableFiltering: true,
            //    maxHeight: '300',
            //    onSelectAll: function () {

            //    },
            //    onSelectAll: function () {
            //        $.uniform.update();
            //    },
            //    onChange: function (element, checked) {


            //    },
            //    onDropdownHide: function (element, checked) {

            //    }
            //});
            //$("#lstGroups").multiselect('selectAll', false);
            //$("#lstGroups").multiselect('updateButtonText');
            //// Agents Dropdown make multiselect
            //$('#lstAgents').multiselect({
            //    includeSelectAllOption: true,
            //    selectAllValue: 'multiselect-all',
            //    enableCaseInsensitiveFiltering: true,
            //    enableFiltering: true,
            //    maxHeight: '300',

            //    onSelectAll: function () {

            //    },
            //    onSelectAll: function () {
            //        $.uniform.update();
            //    },
            //    onChange: function (element, checked) {


            //    },
            //    onDropdownHide: function (element, checked) {

            //    }
            //});
            //$("#lstAgents").multiselect('selectAll', false);
            //$("#lstAgents").multiselect('updateButtonText');

            $('#lstExtensions').multiselect({
                includeSelectAllOption: true,
                selectAllValue: 'multiselect-all',
                enableCaseInsensitiveFiltering: true,
                enableFiltering: true,
                maxHeight: '300',
                onSelectAll: function () {

                },
                onSelectAll: function () {
                    $.uniform.update();
                },
                onChange: function (element, checked) {


                },
                onDropdownHide: function (element, checked) {

                }
            });
            $("#lstExtensions").multiselect('selectAll', false);
            $("#lstExtensions").multiselect('updateButtonText');
            //register event on schedule interval type change 
            $("#lstScheduleInterval").change(function () {

                if (this.value == 1) {
                    $("#SchedulValueRow").hide();
                }
                else if (this.value == 2) {
                    $("#ddScheduleValue").find('option').remove().end();
                    $("#ddScheduleValue").append('<option value=Monday>Monday</option>');
                    $("#ddScheduleValue").append('<option value=Tuesday>Tuesday</option>');
                    $("#ddScheduleValue").append('<option value=Wednesday>Wednesday</option>');
                    $("#ddScheduleValue").append('<option value=Thursday>Thursday</option>');
                    $("#ddScheduleValue").append('<option value=Friday>Friday</option>');
                    $("#ddScheduleValue").append('<option value=Saturday>Saturday</option>');
                    $("#ddScheduleValue").append('<option value=Sunday>Sunday</option>');
                    $("#SchedulValueRow").show();
                }
                else if (this.value == 3) {
                    $("#ddScheduleValue").find('option').remove().end();

                    for (var i = 1; i <= 31; i++) {
                        $("#ddScheduleValue").append("<option value=" + i + ">" + i + "</option>");
                    }

                    $("#SchedulValueRow").show();
                }
            }
           );

            //register event on report type chnage , show hide sections
            $('#lstReports').on('change', function () {

                $("#Sections").hide();
                $(".SubSection").hide();
                var ReportId = this.value;
                if (parseInt(ReportId) > 0) {


                    var Param = { "ReportId": ReportId }
                    $.ajax({
                        type: "POST",
                        url: "ReportsCommonMethods.aspx/GetSectionsOnReportId",
                        data: JSON.stringify(Param),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {

                            if (data.d.length > 0) {
                                $("#Sections").show();
                                for (var i = 0; i < data.d.length; i++) {
                                    var SID = data.d[i].SID;
                                    var SectionName = data.d[i].SectionName;
                                    $("#" + SectionName + "").show();

                                }
                            }

                        }
                    });
                }
                else {
                    $("#Sections").hide();
                }
            });


            // Date pickers default values set
            $("#dp1").datetimepicker({
                format: 'DD/MM/YYYY HH:mm:ss'
            }).datetimepicker("setTime", '00:00:00');
            $("#dp2").datetimepicker({
                format: 'DD/MM/YYYY HH:mm:ss'
            }).datetimepicker("setTime", '23:59:59');
            $("#dp3").datetimepicker({
                format: 'HH:mm:ss'
            }).datetimepicker("setTime", '00:00:00');
            $("#dp4").datetimepicker({
                format: 'HH:mm:ss'
            }).datetimepicker("setTime", '23:59:59');
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }
            var today = dd + '/' + mm + '/' + yyyy;


            $("#txbDateFrom").val(today + ' 00:00:00');
            $("#txbDateTo").val(today + ' 23:59:59');
            $("#txbTimeFrom").val('00:00:00');
            $("#txbTimeTo").val('23:59:59');
            $('#dp1').datetimepicker().on('dp.change', function (ev) {
            });
            $('#dp2').datetimepicker().on('dp.change', function (ev) {

            });
            $('#dp3').datetimepicker().on('dp.change', function (ev) {
            });
            $('#dp4').datetimepicker().on('dp.change', function (ev) {

            });

            //Left Menu 
            $("#Reports").addClass("active open");

        });
        $(document).ajaxStart(function () {
            // $('body').addClass('wait');
        }).ajaxComplete(function () {
            // alert("hello");
            $('body').removeClass('wait');
        });
        function ReportSettings() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "../Settings.aspx/GetSettings",
                type: "POST",
                asynch: true,
                success: function (data) {
                    $("#txbAgentWorkingHours").val(data.d.WorkingHours);
                    $("#txbAgentHangUpThreshold").val(data.d.HangUpThreshold);
                }
            });
            $('#ReportSettingsModal').modal('show');



        }

        function UpdateReportSettings() {

            var WorkingHours = $("#txbAgentWorkingHours").val();
            var HangUpThreshold = $("#txbAgentHangUpThreshold").val();
            var Param = { "WorkingHours": WorkingHours, "HangUpThreshold": HangUpThreshold };
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "../settings.aspx/UpdateAgentReportSpecificSettings",
                data: "{SettingsUpdatedInfo:" + JSON.stringify(Param) + "}",
                type: "POST",
                success: function (data) {

                    $().toastmessage("showSuccessToast", "Settings updated successfully.</span>");
                }
            });

        }

        function ScheduleReportModal() {

            $('#ErrorDivAdd').hide();
            $('#txbScheduleEmail').val("");
            $('#txbScheduleName').val("");
            $('#lstScheduleInterval').val(1);
            $("#ddScheduleTimeHours").val(0);
            $("#ddScheduleTimeMinutes").val(0);
            $('#lstScheduleEmails option').remove();
            $("#SchedulValueRow").hide();


            $('#AddScheduleModal').modal('show');
        }
        function btnScheduleAddEmail_Click() {
            $('#ErrorDivAdd').hide();
            var email = $('#txbScheduleEmail').val();
            if (isEmail(email)) {
                $('#lstScheduleEmails').append($('<option>', { value: email, text: email }));
                $('#txbScheduleEmail').val("");
            }
            else {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide valid Email Address');
                return;
            }
        }
        function isEmail(email) {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return regex.test(email);
        }
        function btnScheduleRemoveEmail_Click() {
            $('#lstScheduleEmails option:selected').remove();
        }
        function AddSchedule() {

            var ScheduleExists = 0;
            var Name = $('#txbScheduleName').val();
            if (Name == "") {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide Schedule Name');
                return;
            }
            var Param = { "ScheduleId": 0, "Name": Name };

            $.ajax({
                type: "POST",
                url: "Schedules.aspx/CheckIfScheduleNameAlreadyExists",
                data: JSON.stringify(Param),
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d === true) {
                        ScheduleExists = 1;

                    }
                    else {
                        ScheduleExists = 0;
                    }
                }
            });
            if (ScheduleExists == 1) {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Schedule with this name already exists.');
                return;
            }
            else {
                $('#ErrorDivAdd').hide();
            }
            var ReportId = parseInt($('#lstReports').val());
            var DateFilterCriteria = $('#lstDateRangeOption').val();
            var DateFrom = $("#txbDateFrom").val();
            var DateTo = $("#txbDateTo").val();
            var TimeFrom = $("#txbTimeFrom").val();
            var TimeTo = $("#txbTimeTo").val();
            var Boards = "";
            var Agents = "";
            var Extensions = "";
            var BoardsSelected = 0;
            var Emails = ""
            var ExtensionSelected = 0;
            var lstExtensionSelected = $('#lstExtensions option:selected');
            $(lstExtensionSelected).each(function () {
                if (ExtensionSelected == 0) {
                    Extensions += "" + $(this).val() + "";
                    ExtensionSelected = 1;
                }
                else if (ExtensionSelected > 0) {
                    Extensions += ',';
                    Extensions += "" + $(this).val() + "";
                }
            });
            //var lstBoardSelected = $('#lstGroups option:selected');
            //$(lstBoardSelected).each(function () {
            //    if (BoardsSelected == 0) {
            //        Boards += $(this).val();
            //        BoardsSelected = 1;
            //    }
            //    else if (BoardsSelected > 0) {
            //        Boards += ',';
            //        Boards += $(this).val();
            //    }
            //});
            //var AgentSelected = 0;
            //var lstAgentSelected = $('#lstAgents option:selected');
            //$(lstAgentSelected).each(function () {
            //    if (AgentSelected == 0) {
            //        Agents += "" + $(this).val() + "";
            //        AgentSelected = 1;
            //    }
            //    else if (AgentSelected > 0) {
            //        Agents += ',';
            //        Agents += "" + $(this).val() + "";
            //    }
            //});
            //if (BoardsSelected == 0) {
            //    $('#ErrorDivAdd').show();
            //    $('#lblErrorAdd').text('Please select atleast one Board');
            //    return;
            //}
            //if ((ReportId == 5 || ReportId == 6 || ReportId == 7 || ReportId == 8) && AgentSelected == 0) {
            //    $('#ErrorDivAdd').show();
            //    $('#lblErrorAdd').text('Please select atleast one Agent');
            //    return;
            //}
            var ScheduleInterval = parseInt($('#lstScheduleInterval').val());

            var ScheduleValue = $('#ddScheduleValue option:selected').text();
            var ScheduleTimeHours = $('#ddScheduleTimeHours option:selected').text();
            var ScheduleTimeMinutes = $('#ddScheduleTimeMinutes option:selected').text();



            $('#lstScheduleEmails option').each(function () {

                Emails += "" + $(this).val() + ";"
            });

            var Param = { "Name": Name, "ReportId": ReportId, "DateFilterCriteria": DateFilterCriteria, "Extensions": Extensions, "Boards": Boards, "Agents": Agents, "ScheduleInterval": ScheduleInterval, "ScheduleValue": ScheduleValue, "ScheduleTimeHours": ScheduleTimeHours, "ScheduleTimeMinutes": ScheduleTimeMinutes, "Emails": Emails, "TimeFrom": TimeFrom, "TimeTo": TimeTo };

            $.ajax({
                type: "POST",
                url: "Schedules.aspx/AddReportSchedule",
                data: "{ReportScheduleEntityObj:" + JSON.stringify(Param) + ",DateFrom:" + JSON.stringify(DateFrom) + ",DateTo:" + JSON.stringify(DateTo) + "}",

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d.length > 0) {
                        $().toastmessage('showSuccessToast', 'Report Schedule saved successfully.');
                        ScheduleReportModal();
                        //$('#ErrorDivAdd').hide();
                        //$('#txbScheduleName').val("");
                        //$('#txbScheduleEmail').val("");
                        //$('#lstScheduleInterval option:selected').text();
                        //$('#ddScheduleTimeHours option:selected').text();
                        //$('#ddScheduleTimeMinutes option:selected').text();

                    }
                    else {
                        $().toastmessage('showErrorToast', 'Error saving Report Schedule.');
                    }
                }
            });

        }
        function ViewReport() {
            $('body').addClass('wait');
            setTimeout(function () {
                ViewReport1();
            }, 500);
        }
        function ViewReport1() {

            var ReportId = parseInt($('#lstReports').val());
            var DateRangeOption = $('#lstDateRangeOption').val();
            var dateFrom = $("#txbDateFrom").val();
            var dateTo = $("#txbDateTo").val();
            var timeFrom = $("#txbTimeFrom").val();
            var timeTo = $("#txbTimeTo").val();
            var Extensions = "";
            var Groups = "";
            var Agents = "";
            var ExtensionSelected = 0;
            var lstExtensionSelected = $('#lstExtensions option:selected');
            $(lstExtensionSelected).each(function () {
                if (ExtensionSelected == 0) {
                    Extensions += "" + $(this).val() + "";
                    ExtensionSelected = 1;
                }
                else if (ExtensionSelected > 0) {
                    Extensions += ',';
                    Extensions += "" + $(this).val() + "";
                }
            });
            //var Groups = "";
            //var Agents = "";
            //var GroupSelected = 0;
            //var lstGroupSelected = $('#lstGroups option:selected');
            //$(lstGroupSelected).each(function () {
            //    if (GroupSelected == 0) {
            //        Groups += "" + $(this).val() + "";
            //        GroupSelected = 1;
            //    }
            //    else if (GroupSelected > 0) {
            //        Groups += ',';
            //        Groups += "" + $(this).val() + "";
            //    }
            //});
            //var AgentSelected = 0;
            //var lstAgentSelected = $('#lstAgents option:selected');
            //$(lstAgentSelected).each(function () {
            //    if (AgentSelected == 0) {
            //        Agents += "" + $(this).val() + "";
            //        AgentSelected = 1;
            //    }
            //    else if (AgentSelected > 0) {
            //        Agents += ',';
            //        Agents += "" + $(this).val() + "";
            //    }
            //});
            var DurationOption = $('#lstDuration').val();
            var DurationVal = $('#txbDuration').val();
            var Param = { "ReportId": ReportId, "DateRangeOption": DateRangeOption, "dateFrom": dateFrom, "dateTo": dateTo, "timeFrom": timeFrom, "timeTo": timeTo, "IsSchedule": false, "Extensions": Extensions, "Groups": Groups, "Agents": Agents };

            $.ajax({
                type: "POST",
                url: "ReportsCommonMethods.aspx/GetReport",
                data: JSON.stringify(Param),
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('#tblReport').empty();
                    if (data.d.length > 0) {

                        window.open('Reports/' + data.d, '_blank');
                    }
                    else {

                    }
                }
            });

        }

       
       
    </script>
</asp:Content>
