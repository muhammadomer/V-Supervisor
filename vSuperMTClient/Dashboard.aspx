<%@ Page Title="" Language="C#" MasterPageFile="~/Supervisor.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="vSuperMTClient.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .on,.off{
            color:#000;
        }
        .extension-name{
            white-space:nowrap;
        }
        .table-options{
            display:flex;
        }
        .table-pager{
            padding-left:0px;
        }
        .dataTables_length{
            float:left;
        }
        .w-0{
            width:0% !important;
        }
    </style>
    <div class="row">
        <div id="LeftCol" class="col-md-3" style="padding-right: 0px;">
            <div class="row" style="padding: 0px 0px 10px 0px;">
                <div class="col-md-12" style="text-align: left; background-color: white; height: 44px;">
                    <span style="padding-top: 10px; display: block; font-weight: 600">Reports</span>
                </div>
            </div>
            <div id="divLeftMenu" style="background-color: white; padding-right: 5px;">
                 <div id="CustomSection">
                    <div class="row" style="background-color: white; padding: 10px 5px 5px 10px;">
                        <div class="row" style="border-bottom: 1px solid #dedede">
                            <div class="col-md-10" style="padding: 0px;">
                                <span id=""><%=System.Configuration.ConfigurationManager.AppSettings["SectionTitle"] %></span>

                            </div>
                            <div class="col-md-2" style="text-align: right; padding: 0px;">
                                <i id="btnShowHideCustomReports" class=" fa fa-angle-up" style="color: black; font-size: 20px; padding-top: 2px; cursor: pointer;"></i>
                            </div>
                        </div>

                    </div>
                    <div id="divCustomReports">
                    </div>
                </div>
               
                <div id="ACDSection">
                    <div class="row" style="background-color: white; padding: 10px 5px 5px 10px;">
                        <div class="row" style="border-bottom: 1px solid #dedede">
                            <div class="col-md-10" style="padding: 0px;">
                                <span id="typeName">ACD Reports</span>

                            </div>
                            <div class="col-md-2" style="text-align: right; padding: 0px;">
                                <i id="btnShowHideAcdReports" class=" fa fa-angle-up" style="color: black; font-size: 20px; padding-top: 2px; cursor: pointer;"></i>
                            </div>
                        </div>

                    </div>
                    <div id="divACDReports">
                    </div>
                </div>
                <div id="LoggingSection">
                    <div class="row" style="background-color: white; padding: 5px 5px 5px 10px;">
                        <div class="row" style="border-bottom: 1px solid #dedede">
                            <div class="col-md-10" style="padding: 0px;">
                                <span>Call Logging Reports</span>

                            </div>
                            <div class="col-md-2" style="text-align: right; padding: 0px;">
                                <i id="btnShowHideLoggingReports" class=" fa fa-angle-up" style="color: black; font-size: 20px; padding-top: 2px; cursor: pointer"></i>
                            </div>
                        </div>
                    </div>
                    <div id="divLoggingReports">
                    </div>
                </div>
                <div id="RecorderSection">
                    <div class="row" style="background-color: white; padding: 5px 5px 5px 10px;">
                        <div class="row" style="border-bottom: 1px solid #dedede">
                            <div class="col-md-10" style="padding: 0px;">
                                <span>Recorder Reports</span>

                            </div>
                            <div class="col-md-2" style="text-align: right; padding: 0px;">
                                <i id="btnShowHideRecorderReports" class=" fa fa-angle-up" style="color: black; font-size: 20px; padding-top: 2px; cursor: pointer"></i>
                            </div>
                        </div>
                    </div>
                    <div id="divRecorderReports">
                    </div>
                </div>
            </div>
        </div>
        <div id="CenterCol" class="col-md-6" style="">
            <div id="divCenterCol1" class="row" style="padding: 0px 0px 10px 0px;">
                <div class="col-md-1" style="height: 44px; background-color: white; text-align: left; padding-top: 4px;" onclick="ShowHideLeftCol();">
                    <i id="btnShowHideLeftCol" class="on fa fa-angle-left" style=""></i>
                </div>
                <%--<div class="col-md-8" style="height: 44px; background-color: white; text-align: right; padding-top: 4px;padding-right:0">
                    <select id="ddlGraphDuration" class="bs-select form-control input-small" data-style="red">
                        <option value="1">Today</option>
                        <option value="2">Yesterday</option>
                        <option value="3">Last 7 Days</option>
                        <option value="4">Last 14 Days</option>
                        <option value="5">Last 30 Days</option>
                        <option value="6">This month</option>
                    </select>
                </div>
                <div class="col-md-3" style="height: 44px; background-color: white; text-align: right; padding-top: 4px;padding-left:0">
                    <select id="ddlCallsOption" class="bs-select form-control input-small" data-style="red">
                        <option value="2">External Calls</option>
                        <option value="1">Internal Calls</option>
                        <option value="0">Both</option>
                    </select>
                </div>--%>

                <div class="col-md-11" style="height: 44px; background-color: white; text-align: right; padding-top: 4px;padding-right:4px">
                    <select id="ddlGraphDuration" class="bs-select form-control input-small" data-style="red">
                        <option value="1">Today</option>
                        <option value="2">Yesterday</option>
                        <option value="3">Last 7 Days</option>
                        <option value="4">Last 14 Days</option>
                        <option value="5">Last 30 Days</option>
                        <option value="6">This month</option>
                    </select>
                
                    <select id="ddlCallsOption" class="bs-select form-control input-small" data-style="red">
                        <option value="2">External Calls</option>
                        <option value="1">Internal Calls</option>
                        <option value="0">Both</option>
                    </select>
                </div>
                
                
                <%--<div class="col-md-1" style="height: 44px; background-color: white; text-align: right;padding-top:4px;">
                           <i id="btnShowHideRightCol" class="on fa fa-arrow-right" style=""></i>
                     </div>--%>
            </div>
            <div id="divCenterMenu" style="background: #fff;">
                <div class="row" style="background: #fff;border-bottom: 1px solid #dedede;padding:9px 0px 4px 0;">
                    <div class="col-md-8"></div>
                    <div class="col-md-4" style="text-align: right;">
                        <span style="margin-top: 8px;/* display: block; */">Call Summary By Day</span>
                        <i id="btnShowHideCallSummary" class="fa fa-angle-up" style="color: black;font-size: 20px;cursor: pointer;margin-left: 2px;margin-right: 4px;vertical-align: sub;"></i>                    </div>
                </div>
                <div id="divCenterMenu1n2">
                    <div id="divCenterMenu1" class="row" style="padding: 0px 0px 10px 0px;">
                    <div class="col-md-12" style="padding: 5px 10px 5px 0px; background-color: white; height: 200px;">
                        <div class="row">
                            <div class="col-md-8" id="LegendCallSummaryByDayChart">
                            </div>
                        </div>
                        <div class="row" id="divCallSummaryByDayChart" style="height: 150px;"></div>
                    </div>
                </div>
                <div id="divCenterMenu2" class="row" style="padding: 0px 0px 10px 0px;">
                     <div class="col-md-4" style="padding: 0px 10px 0px 0px;">
                        <div class="row" style="background-color: white; height: 200px;">
                            <div class="row" style="text-align: center; padding-top: 5px;">
                                <span style="display: block; font-weight: 600;">Outbound Calls By Call Type</span>
                            </div>
                            <div id="divOutBoundCallsByRegionChart" class="col-md-12" style="padding: 0px; height: 150px;">
                            </div>
                            <div id="LegendOutBoundCallsByRegionChart" class="col-md-12" style="padding: 0px;">
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8" style="padding: 0px 0px 0px 0px">
                        <div class="row" style="background-color: white; height: 200px;">
                            <div class="row" style="text-align: center; padding-top: 5px;">
                                <span style="display: block; font-weight: 600;">Calls Serviced By Day</span>
                            </div>
                            <div id="divCallsServicedByDayChart" class="col-md-12" style="padding: 0px; height: 150px;">
                            </div>
                            <div id="LegendCallsServicedByDayChart" class="col-md-12" style="padding: 0px;">
                            </div>
                        </div>
                    </div>
                   <%-- <div class="col-md-4" style="padding: 0px 0px 0px 0px;">
                        <div class="row" style="background-color: white; height: 200px">
                            <div class="row" style="text-align: center; padding-top: 5px;">
                                <span style="display: block; font-weight: 600;">Cost Summary</span>
                            </div>
                            <div id="divCostSummaryChart" class="col-md-12" style="padding: 0px; height: 150px;">
                            </div>
                            <div id="LegendCostSummaryChart" class="col-md-12" style="padding: 0px; text-align: left; font-size: 5px;">
                            </div>
                        </div>
                    </div>--%>
                </div>

                </div>
                <div class="row" style="background: #fff;border-bottom: 1px solid #dedede;padding:9px 0px 4px 0;">
                    <div class="col-md-8" ></div>
                    <div class="col-md-4" style="text-align: right;">
                        <span style="margin-top: 8px;/* display: block; */">Call Summary By Extension</span>
                        <i id="btnShowHideCallSummaryExtension" class="fa fa-angle-up" style="color: black;font-size: 20px;cursor: pointer;margin-left: 2px;margin-right: 4px;vertical-align: sub;"></i>                    </div>
                </div>
                <div id="divCenterMenu3" class="row" style="padding: 0px 0px 0px 0px;">
                    <div class="col-md-12" style="padding: 0px 0px 0px 0px; background-color: white;height:100%">
                        
                        <div class="row" style="text-align: center; padding-top: 0px;">
                            <div class="col-md-12" style="background-color:white">
                                <table id='tblCallSummaryByExtension' class='table table-striped table-hover table-bordered'>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="RightCol" class="col-md-3" style="padding-left: 0px;">
            <div class="row" style="padding: 0px 0px 10px 0px;">
                <div class="col-md-12" style="text-align: right; background-color: white; height: 44px;">
                    <span style="padding-top: 10px; display: block; font-weight: 600">Todays Call Traffic- By Hour</span>

                </div>

            </div>
            <div id="divRightMenu">



                <div class="row" style="padding: 0px 0px 10px 0px;">
                    <div class="col-md-12" style="text-align: center; background-color: white; height: 200px;">
                        <div class="row" style="padding-top: 5px; padding-bottom: 0px;">
                            <div class="col-md-6" style=""></div>
                            <div class="col-md-6" style="background-color: #f91627">

                                <div class="row">
                                    <div class="col-md-12">
                                        <span style="display: block; float: right; font-weight: 700; color: white">Today
                                        </span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align: center; padding: 0px;">
                                        <span style="font-weight: 500; color: white; font-size: 11px;">Inbound Calls By Hour
                                        </span>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px;">
                            <div class="col-md-11" style=""></div>
                            <div class="col-md-1" style="">
                                <i id="btnInboundCallsByHourChart" class=" fa fa-angle-double-down" style="color: #f91627; font-size: 25px; padding-top: 0px; cursor: pointer"></i>
                            </div>
                        </div>
                        <div id="divInboundCallsByHourChart" class="row" style="height: 100px;">
                        </div>
                        <div class="row" style="">
                            <div class="col-md-12" style="text-align: right; padding-right: 0px;">
                                <span style="font-weight: 500; font-size: 11px;">Highest
                                </span>
                                <span id="spanInboundCallsByHourChartMaxVal" style="font-weight: 700; color: #f91627; font-size: 11px;"></span>
                                <span style="font-weight: 500; font-size: 11px;">Compared To Lowest
                                </span>
                                <span id="spanInboundCallsByHourChartMinVal" style="font-weight: 700; color: #f91627; font-size: 11px;"></span>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="row" style="padding: 0px 0px 10px 0px;">
                    <div class="col-md-12" style="text-align: center; background-color: white; height: 200px;">
                        <div class="row" style="padding-top: 5px; padding-bottom: 0px;">
                            <div class="col-md-6" style=""></div>
                            <div class="col-md-6" style="background-color: #5b595a">

                                <div class="row">
                                    <div class="col-md-12">
                                        <span style="display: block; float: right; font-weight: 700; color: white">Today
                                        </span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align: center; padding: 0px;">
                                        <span style="font-weight: 500; color: white; font-size: 10px;">Outbound Calls By Hour
                                        </span>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px;">
                            <div class="col-md-11" style=""></div>
                            <div class="col-md-1" style="">
                                <i id="btnOutboundCallsByHourChart" class=" fa fa-angle-double-down" style="color: #5b595a; font-size: 25px; padding-top: 0px; cursor: pointer"></i>
                            </div>
                        </div>
                        <div id="divOutboundCallsByHourChart" class="row" style="height: 100px;">
                        </div>
                        <div class="row" style="">
                            <div class="col-md-12" style="text-align: right; padding-right: 0px;">
                                <span style="font-weight: 500; font-size: 11px;">Highest
                                </span>
                                <span id="spanOutboundCallsByHourChartMaxVal" style="font-weight: 700; color: #f91627; font-size: 11px;"></span>
                                <span style="font-weight: 500; font-size: 11px;">Compared To Lowest
                                </span>
                                <span id="spanOutboundCallsByHourChartMinVal" style="font-weight: 700; color: #f91627; font-size: 11px;"></span>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="row" style="padding: 0px 0px 10px 0px;">
                    <div class="col-md-12" style="text-align: center; background-color: white; height: 200px;">
                        <div class="row" style="padding-top: 5px; padding-bottom: 0px;">
                            <div class="col-md-6" style=""></div>
                            <div class="col-md-6" style="background-color: #f91627">

                                <div class="row">
                                    <div class="col-md-12">
                                        <span style="display: block; float: right; font-weight: 700; color: white">Today
                                        </span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align: center; padding: 0px;">
                                        <span style="font-weight: 500; color: white; font-size: 11px;">Unanswered By Hour
                                        </span>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px;">
                            <div class="col-md-11" style=""></div>
                            <div class="col-md-1" style="">
                                <i id="btnCallsLostByHourChart" class=" fa fa-angle-double-down" style="color: #f91627; font-size: 25px; padding-top: 0px; cursor: pointer"></i>
                            </div>
                        </div>
                        <div id="divCallsLostByHourChart" class="row" style="height: 100px">
                        </div>
                        <div class="row" style="">
                            <div class="col-md-12" style="text-align: right; padding-right: 0px;">
                                <span style="font-weight: 500; font-size: 11px;">Highest
                                </span>
                                <span id="spanCallsLostByHourChartMaxVal" style="font-weight: 700; color: #f91627; font-size: 11px;"></span>
                                <span style="font-weight: 500; font-size: 11px;">Compared To Lowest
                                </span>
                                <span id="spanCallsLostByHourChartMinVal" style="font-weight: 700; color: #f91627; font-size: 11px;"></span>
                            </div>
                        </div>

                    </div>

                </div>
                <div class="row" style="padding: 0px 0px 10px 0px;">
                    <div class="col-md-12" style="text-align: center; background-color: white; height: 200px;">
                        <div class="row" style="padding-top: 5px; padding-bottom: 0px;">
                            <div class="col-md-6" style=""></div>
                            <div class="col-md-6" style="background-color: #5b595a">

                                <div class="row">
                                    <div class="col-md-12">
                                        <span style="display: block; float: right; font-weight: 700; color: white">Today
                                        </span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="text-align: center; padding: 0px;">
                                        <span style="font-weight: 500; color: white; font-size: 11px;">Longest Ring By Hour
                                        </span>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="row" style="padding-bottom: 5px;">
                            <div class="col-md-11" style=""></div>
                            <div class="col-md-1" style="">
                                <i id="btnLongestRingTimeByHourChart" class=" fa fa-angle-double-down" style="color: #5b595a; font-size: 25px; padding-top: 0px; cursor: pointer"></i>
                            </div>
                        </div>
                        <div id="divLongestRingTimeByHourChart" style="height: 100px;">
                        </div>
                        <div class="row" style="">
                            <div class="col-md-12" style="text-align: right; padding-right: 0px;">
                                <span style="font-weight: 500; font-size: 11px;">Highest
                                </span>
                                <span id="spanLongestRingTimeByHourChartMaxVal" style="font-weight: 700; color: #f91627; font-size: 11px;"></span>
                                <span style="font-weight: 500; font-size: 11px;">Compared To Lowest
                                </span>
                                <span id="spanLongestRingTimeByHourChartMinVal" style="font-weight: 700; color: #f91627; font-size: 11px;"></span>
                            </div>
                        </div>

                    </div>

                </div>

            </div>
        </div>
    </div>
    <div class="modal fade" id="ReportFilter" tabindex="-1" role="basic" aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 2;">
        <div class="modal-dialog" style="width: 540px;">
            <div class="modal-content">
                <div class="row" style="line-height: 35px; background-color: #ffffff; width: 100%; margin-left: 0%; vertical-align: middle; border-bottom: 1px solid #e5e5e5">
                    <div class="col-md-3" style="margin-top: 0.5%; padding-right: 0px;">
                        <span style="font-size: 18px; color: #404040;">Report Filters -</span>
                        <input id="txbScheduleFilterId" type="text" class="form-control" style="display: none;" />
                    </div>
                    <div class="col-md-8" style="margin-top: 0.7%; padding-left: 0px;">
                        <span id="ReportNameOnFilter" style="font-size: 14px; color: #737373;"></span>

                    </div>
                </div>
                <div class="row" id="Sections" style="display: none;">

                    <div class="row SubSection" id="SFilter" style="width: 100%; padding-top: 0px; padding-bottom: 0px; display: none;">
                        <div class="row" style="padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6;">
                            <div class="col-md-2" style="padding-right: 0px;">
                                <span class="help-block">Date Range</span>
                            </div>
                            <div class="col-md-10">
                                <select id="lstDateRangeOption" class="form-control">
                                    <option value="1" selected="selected">Today</option>
                                    <option value="2">Yesterday</option>
                                    <option value="3">This Week</option>
                                    <option value="4">Last Week</option>
                                    <option value="5">This Month</option>
                                    <option value="6">Last Month</option>
                                    <option value="7">Custom</option>
                                </select>
                            </div>

                        </div>
                        
                    </div>
                    <div class="row SubSection" id="SDateRange" style="width: 100%; display: none;">
                        <div class="row" id="CustomDatePickerRow" style="padding-top: 0px; padding-bottom: 0px; display: none; border-bottom: 1px solid #d6d6d6;">
                            <div class="row" style="padding-top: 10px; padding-bottom: 0px;">
                                <div class="col-md-2"></div>
                                <div class="col-md-2" style="padding-right: 0px;">
                                    <span class="help-block">Start Date</span>
                                </div>
                                <div class="col-md-8">

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
                                <div class="col-md-2"></div>
                                <div class="col-md-2">
                                    <span class="help-block">End Date</span>
                                </div>
                                <div class="col-md-8">

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
                    </div>
                    <div class="row SubSection" id="STimeRange" style="width: 100%; display: none;">
                        <div class="row" id="CustomTimePickerRow" style="padding-top: 0px; padding-bottom: 0px; border-bottom: 1px solid #d6d6d6;">
                            <div class="row" style="padding-top: 10px; padding-bottom: 0px;">
                                <div class="col-md-2"></div>
                                <div class="col-md-2" style="padding-right: 0px;">
                                    <span class="help-block">Start Time</span>
                                </div>
                                <div class="col-md-8">

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
                                <div class="col-md-2"></div>
                                <div class="col-md-2">
                                    <span class="help-block">End Time</span>
                                </div>
                                <div class="col-md-8">

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
                    <div class="row" id="SWeekDays" style="width: 100%; padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6;">

                        <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="col-md-2">
                                <span class="help-block">Week Days</span>
                            </div>
                            <div class="col-md-10">
                                <select id="lstWeekDays" class="form-control" multiple="multiple">
                                    <option value="0" title="Monday">Monday</option>
                                    <option value="1" title="Tuesday">Tuesday</option>
                                    <option value="2" title="Wednesday">Wednesday</option>
                                    <option value="3" title="Thrusday">Thrusday</option>
                                    <option value="4" title="Friday">Friday</option>
                                    <option value="5" title="Saturday">Saturday</option>
                                    <option value="6" title="Sunday">Sunday</option>
                                </select>
                            </div>
                        </div>

                    </div>
                    <div class="row SubSection" id="SExtensions" style="width: 100%; padding-top: 10px; padding-bottom: 10px; display: none; border-bottom: 1px solid #d6d6d6;">

                        <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="col-md-2">
                                <span class="help-block">Extensions</span>
                            </div>
                            <div class="col-md-10">
                                <select id="lstExtensions" class="form-control" multiple="multiple">
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row SubSection" id="SGroups" style="width: 100%; padding-top: 10px; padding-bottom: 10px; display: none; border-bottom: 1px solid #d6d6d6;">

                        <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="col-md-2">
                                <span class="help-block">Queues</span>
                            </div>
                            <div class="col-md-10">
                                <select id="lstGroups" class="form-control" multiple="multiple">
                                </select>
                            </div>
                        </div>

                    </div>
                    <div class="row SubSection" id="SAgents" style="width: 100%; padding-top: 10px; padding-bottom: 10px; display: none; border-bottom: 1px solid #d6d6d6;">


                        <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="col-md-2">
                                <span class="help-block">Agents</span>
                            </div>
                            <div class="col-md-10">
                                <select id="lstAgents" class="form-control" multiple="multiple">
                                </select>
                            </div>
                        </div>

                    </div>
                      <div class="row SubSection" id="Status" style="width: 100%; padding-top: 10px; padding-bottom: 10px; display: none;">


                           

                            <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                                <div class="col-md-2">
                                    <span class="help-block">Status</span>
                                </div>
                                <div class="col-md-10">
                                    <select id="lstStatus" class="form-control" multiple="multiple" >
                                        <option value="1">Calling</option>
                                        <option value="3">Ringing</option>
                                        <option value="4">Answered</option>
                                         <option value="5">Not Answered</option>
                                        <option value="6">Failed</option>
                                        <option value="7">Incorrect Number</option>
                                        <option value="8">Agent Not Answered</option>
                                    </select>
                                </div>
                            </div>


                        </div>
                    <div class="row SubSection" id="SUsers" style="width: 100%; padding-top: 10px; padding-bottom: 10px; display: none; border-bottom: 1px solid #d6d6d6;">

                        <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="col-md-2">
                                <span class="help-block">Users</span>
                            </div>
                            <div class="col-md-10">
                                <select id="lstUsers" class="form-control" multiple="multiple">
                                </select>
                            </div>
                        </div>

                    </div>
                     <div class="row SubSection" id="SDDI" style="width: 100%; padding-top: 10px; padding-bottom: 10px; display: none; border-bottom: 1px solid #d6d6d6;">

                        <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="col-md-2">
                                <span class="help-block">DDIs</span>
                            </div>
                            <div class="col-md-10">
                                <select id="lstDDIs" class="form-control" multiple="multiple">
                                </select>
                            </div>
                        </div>

                    </div>
                    <div class="row SubSection" id="STimeInterval" style="width: 100%; padding-top: 10px; padding-bottom: 10px; display: none; border-bottom: 1px solid #d6d6d6;">
                        <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="col-md-2">
                                <span class="help-block">Interval</span>
                            </div>
                            <div class="col-md-10">
                                <select id="lstTimeInterval" class="form-control">
                                    <option value="15" title="15 minutes">15</option>
                                    <option value="30" title="30 minutes">30</option>
                                    <option value="45" title="45 minutes">45</option>
                                    <option value="60" title="60 minutes">60</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row SubSection" id="SExternalRouting" style="width: 100%; padding-top: 10px; padding-bottom: 10px; display: none; border-bottom: 1px solid #d6d6d6;">

                        <div class="=row" style="padding-top: 0px; padding-bottom: 0px;">
                            <div class="col-md-2">
                                <span class="help-block">Ext.Routing</span>
                            </div>
                            <div class="col-md-10">
                                <select id="lstExternalRouting" class="form-control" multiple="multiple">
                                </select>
                            </div>
                        </div>


                    </div>
                    <div class="row SubSection" id="SCalls" style="width: 100%; padding-top: 0px; padding-bottom: 0px; display: none;">
                        <div class="row" style="padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6;">
                            <div class="col-md-2" style="padding-right: 0px;">
                                <span class="help-block">Call Type</span>
                            </div>
                            <div class="col-md-10">
                                <select id="lstCallsOption" class="form-control">
                                    <option value="2" selected="selected">External Calls</option>
                                    <option value="1">Internal Calls</option>
                                    <option value="0">Both</option>
                                    
                                </select>
                            </div>

                        </div>
                        
                        
                    </div>
                    <div class="row" id="SCreateReport" style="padding-top: 10px; padding-bottom: 10px; display: none;">

                        <div class="col-md-12" style="text-align: right;">
                            <input type="button" class="btnFlat" onclick="ShowAddEditScheduleModal('Add');" title="Schedule" value="Schedule" />
                            <input type="button" class="btnFlat" onclick="ViewReport();" title="PDF" value="PDF" />
                            <input type="button" class="btnFlat" onclick="CSV();" title="Excel" value="Excel" />
                            <input type="button" class="btnFlat" onclick="Excel();" title="CSV" value="CSV" />

                            <button type="button" class="btnFlat" onclick="ClearReportFilters()" style="width: 70px;">Close</button>
                        </div>
                    </div>
                    <div class="row" id="SUpdateReport" style="padding-top: 10px; padding-bottom: 10px; display: none;">

                        <div class="col-md-12" style="text-align: right;">
                            <input type="button" class="btnFlat" onclick="UpdateReportFilters();" title="Update" value="Update" />
                            <button type="button" class="btnFlat" onclick="ClearReportFilters()" style="width: 70px;">Close</button>
                        </div>
                    </div>
                    
                </div>

            </div>
        </div>
    </div>
    <div class="modal fade" id="AddEditScheduleModal" tabindex="-1" role="basic" aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 3 !important;">
        <div class="modal-dialog" style="width: 540px;">
            <div class="modal-content">
                <div class="row" style="line-height: 35px; background-color: #ffffff; width: 100%; margin-left: 0%; vertical-align: middle; border-bottom: 1px solid #e5e5e5;">
                    <div class="col-md-2" style="margin-top: 0.5%; padding-right: 0px; width: 18%">
                        <span style="font-size: 18px; color: #404040;">Schedule - </span>
                    </div>
                    <div class="col-md-10" style="margin-top: 0.7%; padding-left: 2px; width: 82%">
                        <span id="ReportNameOnScheduleAddEdit" style="font-size: 14px; color: #737373;"></span>

                    </div>
                </div>
                <div id="ErrorDivAdd" class="row" style="padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6; display: none;">
                    <div class="col-md-12">
                        <label class="" style="color: red" id="lblErrorAdd"></label>
                        <input id="txbScheduleId" type="text" class="form-control" style="display: none;" />
                    </div>
                </div>
                <div class="row" style="padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6;">
                    <div class="col-md-2">
                        <label class="">Name<span style="color: red;">*</span></label>
                    </div>
                    <div class="col-md-10">
                        <input id="txbScheduleName" type="text" class="form-control" />
                    </div>
                </div>
                <div class="row" style="padding-top: 7px; padding-bottom: 5px; border-bottom: 1px solid #d6d6d6;">
                            <div class="col-md-3">
                                <label class="">Report Type</label>
                            </div>
                            <div class="col-md-9">
                                <fieldset>
                                <input style="margin-left:-8px" type="radio" name="optradio" id="chkReportTypePdf" value="pdf" checked="checked">
                                <label style="padding-right: 10px;">Pdf</label>
                                <input style="margin-left:-8px" type="radio" name="optradio" id="chkReportTypeExcel" value="csv">
                                <label style="padding-right: 10px;">Excel</label>
                                    <input style="margin-left:-8px" type="radio" name="optradio" id="chkReportTypeCSV" value="excel">
                                <label style="padding-right: 10px;">CSV</label>

                            </fieldset>
                            </div>

                        </div>
                <div class="row" style="padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6;">
                    <div class="col-md-2">
                        <label class="" style="">Schedule</label>
                    </div>
                    <div class="col-md-10">
                        <select id="lstScheduleInterval" class="form-control">
                            <option value="1">Daily</option>
                            <option value="2">Weekly</option>
                            <option value="3">Monthly</option>
                        </select>
                    </div>
                </div>
                <div id="SchedulValueRow" class="row" style="padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6; display: none;">
                    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2" style="">
                        <span class="">On</span>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-10 col-lg-10">
                        <select id="ddScheduleValue" class="form-control">
                        </select>
                    </div>
                </div>
                <div class="row" style="padding-top: 10px; padding-bottom: 10px; border-bottom: 1px solid #d6d6d6;">
                    <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2" style="">
                        <span class="">At</span>
                    </div>
                    <div class="col-xs-12 col-sm-12 col-md-10 col-lg-10">
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

                    <div class="col-md-2">
                        <label class="">Email</label>
                    </div>
                    <div class="col-md-10">
                        <div class="input-group">
                            <input id="txbScheduleEmail" type="text" class="form-control" style="border-bottom: 0px !important;" />
                            <span class="input-group-btn" style="vertical-align: bottom">
                                <button type="button" id="btnScheduleAddEmail" class="btn green" onclick="btnScheduleAddEmail_Click();" style="padding: 7px 14px;"><i class="fa fa-plus"></i></button>

                            </span>
                        </div>
                    </div>
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-10">
                        <div class="input-group">
                            <select id="lstScheduleEmails" class="form-control" multiple="multiple" style="width: 100%;">
                            </select>
                            <span class="input-group-btn" style="vertical-align: bottom">
                                <button type="button" id="btnScheduleRemoveEmail" class="btn green" onclick="btnScheduleRemoveEmail_Click();" style="padding: 7px 14px;"><i class="fa fa-trash"></i></button>
                            </span>
                        </div>
                    </div>

                </div>
                <div class="row" style="padding-top: 10px; padding-bottom: 10px;">

                    <div id="divAddSchedule" class="col-md-12" style="text-align: right; display: none;">
                        <button type="button" onclick="AddSchedule();" id="btnAddSchedule" class="btnFlat">Apply</button>
                        <button type="button" class="btnFlat" data-dismiss="modal" style="">Close</button>
                    </div>
                    <div id="divEditSchedule" class="col-md-12" style="text-align: right; display: none;">
                        <button type="button" onclick="UpdateSchedule();" id="btnEditSchedule" class="btnFlat">Apply</button>
                        <button type="button" class="btnFlat" data-dismiss="modal" style="">Close</button>
                    </div>


                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ViewScheduleModal" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false" style="right: 75px; z-index: 1 !important;">
        <div class="modal-dialog" style="width: 900px;">
            <div class="modal-content">
                <div class="row" style="line-height: 35px; background-color: #ffffff; width: 100%; margin-left: 0%; vertical-align: middle; border-bottom: 1px solid #e5e5e5;">
                    <div class="col-md-4" style="margin-top: 1%;">
                        <span style="font-size: 18px; color: #404040;">Schedules</span>
                    </div>
                </div>
                <div class="row" style="margin-top: 0%; width: 100%;">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" style="padding: 10px 10px 0px 10px;">
                        <table class="table table-striped table-bordered table-hover" id="tblSchedules" style="margin-bottom: 0px !important">
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                    <div class="col-md-12" style="text-align: right">
                        <button type="button" class="btnFlat" data-dismiss="modal" style="width: 70px;">Close</button>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div id="ModalDeleteFromSchedules" class="modal fade" role="basic" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" style="padding:10px">
                <div class="modal-content" style="width: 440px;margin-left:15%;margin-top: 18%;padding: 5px;">
                    <div class="row" style="line-height: 40px; background-color: #ffffff; width: 100%; vertical-align: middle; border-bottom: 1px solid #e5e5e5">
                        <div class="col-md-12">
                            <span style="font-size: 18px; color: #404040;">Delete Schedule</span>
                        </div>
                    </div>
                    <div class="row" style="line-height: 30px; background-color: #ffffff; width: 100%;text-align:center; vertical-align: middle; border-bottom: 1px solid #e5e5e5">
                        <div class="col-md-12">
                            <span style="font-size: 16px; color: #404040;">Are you sure you want to Delete?</span>
                        </div>
                    </div>
                    <div class="row" style="margin-top:5px">
                        <div class="col-md-2" style="width: 80px;float:right">
                            <input type="hidden" id="hidDeleteScheduleId" />
                            <button type="button" onclick="DeleteFromSchedules();" id="btnDeleteSchedule" class="btnFlat">Delete</button>
                        </div>
                        <div class="col-md-2" style="width: 80px;float:right">
                            <button type="button" class="btnFlat" data-dismiss="modal">Close</button>

                        </div>
                    </div>
                </div>
            </div>
        </div>

    <script>

        function PageDesign()
        {
            //var ScreenHeight = (screen.height) - 240; //528

            var WindowHeight = $(window).height() - 134;  //422

            //console.log( " " + WindowHeight);

            $('#divLeftMenu').slimScroll({
                height: WindowHeight-2
            });

            $('#divCenterMenu').slimScroll({
                height: WindowHeight
            });
            
            var height = WindowHeight - $('#divCenterMenu1').height() - $('#divCenterMenu2').height() - $('#divCenterCol1').height();
            $('#divCenterMenu3').height(height+20);

            $('#divRightMenu').slimScroll({
                height: WindowHeight
            });

        }

        $(window).resize(function () {

            if ($.fn.DataTable.isDataTable('#tblCallSummaryByExtension') ) {

                var height = window.innerHeight;
                var pageLength = 20;
                if (height < 800){
                    pageLength = 5;
                }else if (height < 980){
                    pageLength = 10;
                }else if (height < 1080){
                    pageLength = 13;
                }else if (height < 1180){
                    pageLength = 20;
                }
                //console.log("pageLength height : "+ height +" "+pageLength);
                $('#tblCallSummaryByExtension').DataTable().page.len(pageLength).draw();
                
            }

            PageDesign();
        });

        $(document).ready(function () {
            
            PageDesign();
            $("#btnShowHideCallSummary").click(function () {
                
                if ($("#btnShowHideCallSummary").hasClass("fa-angle-up")) {
                    $('#divCenterMenu1n2').slideUp();
                    $('#btnShowHideCallSummary').removeClass("fa-angle-up").addClass("fa-angle-down");

                   
                }
                else {
                    $('#divCenterMenu1n2').slideDown();
                    $('#btnShowHideCallSummary').removeClass("fa-angle-down").addClass("fa-angle-up");

                    
                }
            });
            $("#btnShowHideCallSummaryExtension").click(function () {
                if ($("#btnShowHideCallSummaryExtension").hasClass("fa-angle-up")) {
                    $('#divCenterMenu3').slideUp();
                    $('#btnShowHideCallSummaryExtension').removeClass("fa-angle-up").addClass("fa-angle-down");
                }
                else {
                    $('#divCenterMenu3').slideDown();
                    $('#btnShowHideCallSummaryExtension').removeClass("fa-angle-down").addClass("fa-angle-up");
                    
                }
            });
            $("#btnShowHideCustomReports").click(function () {
                if ($("#btnShowHideCustomReports").hasClass("fa-angle-up")) {
                    $('#divCustomReports').css({ "display": "none" });
                    $('#btnShowHideCustomReports').removeClass("fa-angle-up").addClass("fa-angle-down");
                }
                else {
                    $('#divCustomReports').css({ "display": "block" });
                    $('#btnShowHideCustomReports').removeClass("fa-angle-down").addClass("fa-angle-up");
                }
            });

            $("#btnShowHideAcdReports").click(function () {
                if ($("#btnShowHideAcdReports").hasClass("fa-angle-up")) {
                    $('#divACDReports').css({ "display": "none" });
                    $('#btnShowHideAcdReports').removeClass("fa-angle-up").addClass("fa-angle-down");
                }
                else {
                    $('#divACDReports').css({ "display": "block" });
                    $('#btnShowHideAcdReports').removeClass("fa-angle-down").addClass("fa-angle-up");
                }
            });
            $("#btnShowHideLoggingReports").click(function () {
                if ($("#btnShowHideLoggingReports").hasClass("fa-angle-up")) {
                    $('#divLoggingReports').css({ "display": "none" });
                    $('#btnShowHideLoggingReports').removeClass("fa-angle-up").addClass("fa-angle-down");
                }
                else {
                    $('#divLoggingReports').css({ "display": "block" });
                    $('#btnShowHideLoggingReports').removeClass("fa-angle-down").addClass("fa-angle-up");
                }
            });
            $("#btnShowHideRecorderReports").click(function () {
                if ($("#btnShowHideRecorderReports").hasClass("fa-angle-up")) {
                    $('#divRecorderReports').css({ "display": "none" });
                    $('#btnShowHideRecorderReports').removeClass("fa-angle-up").addClass("fa-angle-down");
                }
                else {
                    $('#divRecorderReports').css({ "display": "block" });
                    $('#btnShowHideRecorderReports').removeClass("fa-angle-down").addClass("fa-angle-up");
                }
            });

            //setTimeout(function () {
            
            //    if ($.fn.DataTable.isDataTable('#tblCallSummaryByExtension') ) {
            //        if ($(this).height() >= 800){
            //            $('#tblCallSummaryByExtension').DataTable().page.len(10).draw();
            //        } else {
            //            $('#tblCallSummaryByExtension').DataTable().page.len(5).draw();
            //        }
            //    }
            
            //}, 1000);

        });
    </script>
    <script>
        var LoggingReportsLicense=0;
        var ACDReportsLicense=0;
        var GlobalVarDBType = "vBoard";
        var GlobalVarReportId = 0;
        var RightMenuChart1Type = 0;
        var RightMenuChart2Type = 0;
        var RightMenuChart3Type = 0;
        var RightMenuChart4Type = 0;
        var Timer1;
        var Timer2;
        var IsOXO = "1";
        $(document).ready(function () {
            
            var IsOXO = <%=IsOXO%>;

            if(IsOXO == "0")
            {
                $('#typeName').text('CCD Reports');
            }
            else
            {
                $('#typeName').text('ACD Reports');
            }
                        
            <%
                 var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            %>

            var ExtensionsArray = <%= serializer.Serialize(ExtensionsEntityList) %>;
            if(ExtensionsArray != null)
            {
                for (var i = 0; i < ExtensionsArray.length; i++) {
                    $("#lstExtensions").append("<option value='" + ExtensionsArray[i].Extension + "'>" + ExtensionsArray[i].Extension+" ("+ExtensionsArray[i].FirstName+" "+ExtensionsArray[i].LastName+")"+"</option>");
                }
            }

            var BoardsArray = <%= serializer.Serialize(BoardsEntityList) %>;
            if(BoardsArray != null)
            {
                for (var i = 0; i < BoardsArray.length; i++) {
                    $("#lstGroups").append("<option value=" + BoardsArray[i].huntGroupId + ">" + BoardsArray[i].Title + "</option>");
                }
            }

            if (BoardsArray != null) {
                for (var i = 0; i < BoardsArray.length; i++) {
                    for (var j = 0; j < BoardsArray[i].HuntGroup_Extension.length; j++) {
                        $("#lstDDIs").append("<option value=" + BoardsArray[i].HuntGroup_Extension[j].DDI + ">" + BoardsArray[i].HuntGroup_Extension[j].Name + "</option>");
                    }
                }
            }

            var AgentsArray = <%= serializer.Serialize(AgentStatsEntityList) %>;
            if(AgentsArray != null)
            {
                for (var i = 0; i < AgentsArray.length; i++) {
                    $("#lstAgents").append("<option value='" + AgentsArray[i].AgentName + "'>" + AgentsArray[i].AgentName + "</option>");
                }
            }

            var UsersArray = <%= serializer.Serialize(UsersEntityList) %>;
            if(UsersArray != null)
            {
                for (var i = 0; i < UsersArray.length; i++) {
                    $("#lstUsers").append("<option value='" + UsersArray[i].UserId + "'>" + UsersArray[i].UserName + "</option>");
                }
            }

            var ExternalRoutingArray = <%= serializer.Serialize(ExternalRoutingEntityList) %>;
            if(ExternalRoutingArray != null)
            {
                for (var i = 0; i < ExternalRoutingArray.length; i++) {
                    $("#lstExternalRouting").append("<option value=" + ExternalRoutingArray[i].Number + ">" + ExternalRoutingArray[i].Name + "</option>");
                }
            }


            $("#HeaderLinkDashboard").removeClass("DeactiveHeaderLink").addClass("ActiveHeaderLink");
            CheckLicenses();
            DrawLeftCustomPanel();
            DrawLeftACDPanel();
            DrawLeftLoggingPanel();
            DrawLeftRecorderPanel();

            //No Ajax Requests methods
            GetExtension();
            GetBoards();
            GetDDIs();
            GetAgents();
            GetStatus();
            GetRecorderUsers();
            GetExtRouting();
            GetWeekDays();

            FillHourMinuteDropdowns();
            DatePickersDefaultSet();
            RegEventDateRangeChange();
            RegEventScheduleIntervalChange();
            RegEventddlGraphDurationChange();
            RegEventddlCallsOptionChange();
            RegEventRightMenuButton1Click();
            RegEventRightMenuButton2Click();
            RegEventRightMenuButton3Click();
            RegEventRightMenuButton4Click();
            DrawCenterMenuGraphs();
            DrawRightMenuGraphs();
        });

        function CheckLicenses()
        {
            if (<%=LoggingReportsLicense%> ==1) {
                LoggingReportsLicense=1;
                //$("#LoggingSection").show();
            }
            else{
                LoggingReportsLicense=0;
                //$("#LoggingSection").hide();
            }
            if (<%=ACDReportsLicense%> ==1) {
                //$("#ACDSection").show();
                ACDReportsLicense=1;
            }
            else{
                ACDReportsLicense=0;
                //$("#ACDSection").hide();
            }

        }
        function ShowHideLeftCol() {
            if ($("#btnShowHideLeftCol").hasClass("on")) {

                $("#LeftCol").css("display", "none");
                $("#btnShowHideLeftCol").removeClass("on").addClass("off");
                $("#btnShowHideLeftCol").removeClass("fa-angle-left").addClass("fa-angle-right");

                $("#CenterCol").removeClass("col-md-6").addClass("col-md-9");

            }
            else if ($("#btnShowHideLeftCol").hasClass("off")) {

                $("#LeftCol").css("display", "block");
                $("#btnShowHideLeftCol").removeClass("off").addClass("on");
                $("#btnShowHideLeftCol").removeClass("fa-angle-right").addClass("fa-angle-left");
                $("#CenterCol").removeClass("col-md-9").addClass("col-md-6");
            }
        }
        function GetSectionsOnReportId(ReportId, ReportName, DBType, AddEditMode) {
            GlobalVarDBType = DBType;
            $('#ReportFilter').modal('show');
            $('#ReportNameOnFilter').text(ReportName);
            $("#Sections").hide();
            $(".SubSection").hide();

            var Param = { "ReportId": ReportId, "DBType": DBType }
            $.ajax({
                type: "POST",
                url: "ReportsCommonMethods.aspx/GetSectionsOnReportId",
                data: JSON.stringify(Param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                   // debugger;
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
            if (AddEditMode == "Add") {
                $("#SCreateReport").show();
                $("#SUpdateReport").hide();
            }
            else if (AddEditMode == "Edit") {
                $("#SCreateReport").hide();
                $("#SUpdateReport").show();
            }

        }
        function ViewReport() {
            //$('body').addClass('wait');
            setTimeout(function () {
                ViewReport1();
            }, 500);
        }
        function ViewReport1() {

            //var ReportId = parseInt($('#lstReports').val());
            //debugger;
            var ReportId = GlobalVarReportId;
            var DateRangeOption = $('#lstDateRangeOption').val();
            var CallsOption = $('#lstCallsOption').val();
            var dateFrom = $("#txbDateFrom").val();
            var dateTo = $("#txbDateTo").val();
            var timeFrom = $("#txbTimeFrom").val();
            var timeTo = $("#txbTimeTo").val();
            var Extensions = "";
            var Groups = "";
            var ExternalRouting = "";
            var Agents = "";
            var Status = "";
            var Users = "";
            var WeekDays = "";
            var TimeInterval = parseInt($('#lstTimeInterval').val());
            var Interval = "";
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
            var GroupSelected = 0;
            if (ReportId == 45 || ReportId == 46) {

                var lstGroupSelected = $('#lstDDIs option:selected');
                $(lstGroupSelected).each(function () {
                    if (GroupSelected == 0) {
                        Groups += "" + $(this).val() + "";
                        GroupSelected = 1;
                    }
                    else if (GroupSelected > 0) {
                        Groups += ',';
                        Groups += "" + $(this).val() + "";
                    }
                });
            }
            else {
                var lstGroupSelected = $('#lstGroups option:selected');
                $(lstGroupSelected).each(function () {
                    if (GroupSelected == 0) {
                        Groups += "" + $(this).val() + "";
                        GroupSelected = 1;
                    }
                    else if (GroupSelected > 0) {
                        Groups += ',';
                        Groups += "" + $(this).val() + "";
                    }
                });
            }
            var ExternalRoutingSelected = 0;
            var lstExternalRoutingSelected = $('#lstExternalRouting option:selected');
            $(lstExternalRoutingSelected).each(function () {
                if (ExternalRoutingSelected == 0) {
                    ExternalRouting += "" + $(this).val() + "";
                    ExternalRoutingSelected = 1;
                }
                else if (ExternalRoutingSelected > 0) {
                    
                    ExternalRouting += ',';
                    ExternalRouting += "" + $(this).val() + "";
                }
            });
            var AgentSelected = 0;
            var lstAgentSelected = $('#lstAgents option:selected');
            $(lstAgentSelected).each(function () {
                if (AgentSelected == 0) {
                    Agents += "" + $(this).val() + "";
                    AgentSelected = 1;
                }
                else if (AgentSelected > 0) {
                    Agents += ',';
                    Agents += "" + $(this).val() + "";
                }
            });
            var StatusSelected = 0;
            var lstStatusSelected = $('#lstStatus option:selected');
            $(lstStatusSelected).each(function () {
                if (StatusSelected == 0) {
                    Status += "" + $(this).val() + "";
                    StatusSelected = 1;
                }
                else if (StatusSelected > 0) {
                    Status += ',';
                    Status += "" + $(this).val() + "";
                }
            });
            var UserSelected = 0;
            var lstUserSelected = $('#lstUsers option:selected');
            $(lstUserSelected).each(function () {
                if (UserSelected == 0) {
                    Users += "" + $(this).val() + "";
                    UserSelected = 1;
                }
                else if (UserSelected > 0) {
                    Users += ',';
                    Users += "" + $(this).val() + "";
                }
            });
            var WeekDaysSelected = 0;
            var lstWeekDaysSelected = $('#lstWeekDays option:selected');
            $(lstWeekDaysSelected).each(function () {
                if (WeekDaysSelected == 0) {
                    WeekDays += "" + $(this).val() + "";
                    WeekDaysSelected = 1;
                }
                else if (WeekDaysSelected > 0) {
                    WeekDays += ',';
                    WeekDays += "" + $(this).val() + "";
                }
            });
            var DurationOption = $('#lstDuration').val();
            var DurationVal = $('#txbDuration').val();

            var Param = { "ReportType": "PDF", "ReportId": ReportId, "DateRangeOption": DateRangeOption, "CallsOption": CallsOption, "dateFrom": dateFrom, "dateTo": dateTo, "timeFrom": timeFrom, "timeTo": timeTo, "IsSchedule": false, "Extensions": Extensions, "Groups": Groups, "ExternalRouting": ExternalRouting, "Agents": Agents, "Users": Users, "WeekDays": WeekDays, "TimeInterval": TimeInterval, "DBType": GlobalVarDBType, "Status": Status };

            $.ajax({
                type: "POST",
                url: "ReportsCommonMethods.aspx/GetReport",
                data: JSON.stringify(Param),

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    //$('#ReportFilter').modal('hide');
                    $.blockUI({
                        //message: 'Helloooooooooooooooooooooooooooooooooooo',
                        message: "<svg class='lds-spinner' width='200px'  height='200px'  xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' viewBox='0 0 100 100' preserveAspectRatio='xMidYMid' style='background: none;'><g transform='rotate(0 50 50)'>" +
   "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.9166666666666666s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(30 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.8333333333333334s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(60 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.75s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(90 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.6666666666666666s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(120 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.5833333333333334s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(150 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.5s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(180 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.4166666666666667s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(210 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.3333333333333333s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(240 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.25s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(270 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.16666666666666666s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(300 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.08333333333333333s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(330 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='0s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g></svg>",
                        css: {

                            padding: 0,
                            margin: 0,
                            width: '15%',
                            top: '40%',
                            left: '45%',
                            textAlign: 'center',
                            cursor: 'wait',
                            "z-index": "10001",
                            "border": "0px",
                            "background-color": "transparent"
                        }
                    });

                },
                success: function (data) {

                    //$('#ReportFilter').modal('show');
                    $.unblockUI();
                    $('#tblReport').empty();
                    if (data.d.length > 0) {
                        //  GlobalVarReportId = 0;
                        var Ac_Id = <%=HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]%>
                        window.open('Reports/'+Ac_Id+'/' + data.d, '_blank');

                    }
                    else {
                        GlobalVarReportId = 0;
                    }
                }
                ,
                error: function (request, status, error) {
                    //$('#ReportFilter').modal('show');
                    $.unblockUI();
                }
            });

        }
        function CSV() {
            //$('body').addClass('wait');
            setTimeout(function () {
                CSV1();
            }, 500);
        }
        function CSV1() {
            //debugger;
            //var ReportId = parseInt($('#lstReports').val());
            //debugger;
            var ReportId = GlobalVarReportId;
            var DateRangeOption = $('#lstDateRangeOption').val();
            var CallsOption = $('#lstCallsOption').val();
            var dateFrom = $("#txbDateFrom").val();
            var dateTo = $("#txbDateTo").val();
            var timeFrom = $("#txbTimeFrom").val();
            var timeTo = $("#txbTimeTo").val();
            var Extensions = "";
            var Groups = "";
            var ExternalRouting = "";
            var Agents = "";
            var Status = "";
            var Users = "";
            var WeekDays = "";
            var TimeInterval = parseInt($('#lstTimeInterval').val());
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
            var GroupSelected = 0;
            if (ReportId == 45 || ReportId == 46) {

                var lstGroupSelected = $('#lstDDIs option:selected');
                $(lstGroupSelected).each(function () {
                    if (GroupSelected == 0) {
                        Groups += "" + $(this).val() + "";
                        GroupSelected = 1;
                    }
                    else if (GroupSelected > 0) {
                        Groups += ',';
                        Groups += "" + $(this).val() + "";
                    }
                });
            }
            else {
                var lstGroupSelected = $('#lstGroups option:selected');
                $(lstGroupSelected).each(function () {
                    if (GroupSelected == 0) {
                        Groups += "" + $(this).val() + "";
                        GroupSelected = 1;
                    }
                    else if (GroupSelected > 0) {
                        Groups += ',';
                        Groups += "" + $(this).val() + "";
                    }
                });
            }
            var ExternalRoutingSelected = 0;
            var lstExternalRoutingSelected = $('#lstExternalRouting option:selected');
            $(lstExternalRoutingSelected).each(function () {
                if (ExternalRoutingSelected == 0) {
                    ExternalRouting += "" + $(this).val() + "";
                    ExternalRoutingSelected = 1;
                }
                else if (ExternalRoutingSelected > 0) {
                    
                    ExternalRouting += ',';
                    ExternalRouting += "" + $(this).val() + "";
                }
            });
            var AgentSelected = 0;
            var lstAgentSelected = $('#lstAgents option:selected');
            $(lstAgentSelected).each(function () {
                if (AgentSelected == 0) {
                    Agents += "" + $(this).val() + "";
                    AgentSelected = 1;
                }
                else if (AgentSelected > 0) {
                    Agents += ',';
                    Agents += "" + $(this).val() + "";
                }
            });
            var StatusSelected = 0;
            var lstStatusSelected = $('#lstStatus option:selected');
            $(lstStatusSelected).each(function () {
                if (StatusSelected == 0) {
                    Status += "" + $(this).val() + "";
                    StatusSelected = 1;
                }
                else if (StatusSelected > 0) {
                    Status += ',';
                    Status += "" + $(this).val() + "";
                }
            });
            var UserSelected = 0;
            var lstUserSelected = $('#lstUsers option:selected');
            $(lstUserSelected).each(function () {
                if (UserSelected == 0) {
                    Users += "" + $(this).val() + "";
                    UserSelected = 1;
                }
                else if (UserSelected > 0) {
                    Users += ',';
                    Users += "" + $(this).val() + "";
                }
            });
            var WeekDaysSelected = 0;
            var lstWeekDaysSelected = $('#lstWeekDays option:selected');
            $(lstWeekDaysSelected).each(function () {
                if (WeekDaysSelected == 0) {
                    WeekDays += "" + $(this).val() + "";
                    WeekDaysSelected = 1;
                }
                else if (WeekDaysSelected > 0) {
                    WeekDays += ',';
                    WeekDays += "" + $(this).val() + "";
                }
            });
            var DurationOption = $('#lstDuration').val();
            var DurationVal = $('#txbDuration').val();
            var Param = { "ReportType": "CSV", "ReportId": ReportId, "DateRangeOption": DateRangeOption, "CallsOption": CallsOption, "dateFrom": dateFrom, "dateTo": dateTo, "timeFrom": timeFrom, "timeTo": timeTo, "IsSchedule": false, "Extensions": Extensions, "Groups": Groups, "ExternalRouting": ExternalRouting, "Agents": Agents, "Users": Users, "WeekDays": WeekDays, "TimeInterval": TimeInterval, "DBType": GlobalVarDBType, "Status": Status };

            $.ajax({
                type: "POST",
                url: "ReportsCommonMethods.aspx/GetReport",
                data: JSON.stringify(Param),
                //async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {
                    
                    //$('#ReportFilter').modal('hide');
                    $.blockUI({
                        //message: 'Helloooooooooooooooooooooooooooooooooooo',
                        message: "<svg class='lds-spinner' width='200px'  height='200px'  xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' viewBox='0 0 100 100' preserveAspectRatio='xMidYMid' style='background: none;'><g transform='rotate(0 50 50)'>" +
   "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.9166666666666666s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(30 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.8333333333333334s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(60 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.75s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(90 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.6666666666666666s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(120 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.5833333333333334s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(150 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.5s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(180 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.4166666666666667s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(210 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.3333333333333333s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(240 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.25s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(270 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.16666666666666666s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(300 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.08333333333333333s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g><g transform='rotate(330 50 50)'>" +
  "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
    "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='0s' repeatCount='indefinite'></animate>" +
  "</rect>" +
"</g></svg>",
                        css: {

                            padding: 0,
                            margin: 0,
                            width: '15%',
                            top: '40%',
                            left: '45%',
                            textAlign: 'center',
                            cursor: 'wait',
                            "z-index": "10001",
                            "border": "0px",
                            "background-color": "transparent"
                        }
                    });

                },
                success: function (data) {
                    $.unblockUI();
                    //$('#ReportFilter').modal('show');
                    $('#tblReport').empty();
                    if (data.d.length > 0) {
                        //debugger;
                        //  GlobalVarReportId = 0;
                        var Ac_Id = <%=HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]%>
                        window.open('Reports/'+Ac_Id+'/' + data.d, '_blank');

                    }
                    else {
                        GlobalVarReportId = 0;
                    }
                }
                ,
                error: function (request, status, error) {
                    $.unblockUI();
                    console.log(error);
                    console.log(status);
                    console.log(request);
                    //$('#ReportFilter').modal('show');
                }
            });

        }
        function Excel() {
            //$('body').addClass('wait');
            setTimeout(function () {
                Excel1();
            }, 500);
        }
        function Excel1() {
            //debugger;
            //var ReportId = parseInt($('#lstReports').val());
            //debugger;
            var ReportId = GlobalVarReportId;
            var DateRangeOption = $('#lstDateRangeOption').val();
            var CallsOption = $('#lstCallsOption').val();
            var dateFrom = $("#txbDateFrom").val();
            var dateTo = $("#txbDateTo").val();
            var timeFrom = $("#txbTimeFrom").val();
            var timeTo = $("#txbTimeTo").val();
            var Extensions = "";
            var Groups = "";
            var ExternalRouting = "";
            var Agents = "";
            var Status = "";
            var Users = "";
            var WeekDays = "";
            var TimeInterval = parseInt($('#lstTimeInterval').val());
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
            var GroupSelected = 0;
            if (ReportId == 45 || ReportId == 46) {

                var lstGroupSelected = $('#lstDDIs option:selected');
                $(lstGroupSelected).each(function () {
                    if (GroupSelected == 0) {
                        Groups += "" + $(this).val() + "";
                        GroupSelected = 1;
                    }
                    else if (GroupSelected > 0) {
                        Groups += ',';
                        Groups += "" + $(this).val() + "";
                    }
                });
            }
            else {
                var lstGroupSelected = $('#lstGroups option:selected');
                $(lstGroupSelected).each(function () {
                    if (GroupSelected == 0) {
                        Groups += "" + $(this).val() + "";
                        GroupSelected = 1;
                    }
                    else if (GroupSelected > 0) {
                        Groups += ',';
                        Groups += "" + $(this).val() + "";
                    }
                });
            }
            var ExternalRoutingSelected = 0;
            var lstExternalRoutingSelected = $('#lstExternalRouting option:selected');
            $(lstExternalRoutingSelected).each(function () {
                if (ExternalRoutingSelected == 0) {
                    ExternalRouting += "" + $(this).val() + "";
                    ExternalRoutingSelected = 1;
                }
                else if (ExternalRoutingSelected > 0) {

                    ExternalRouting += ',';
                    ExternalRouting += "" + $(this).val() + "";
                }
            });
            var AgentSelected = 0;
            var lstAgentSelected = $('#lstAgents option:selected');
            $(lstAgentSelected).each(function () {
                if (AgentSelected == 0) {
                    Agents += "" + $(this).val() + "";
                    AgentSelected = 1;
                }
                else if (AgentSelected > 0) {
                    Agents += ',';
                    Agents += "" + $(this).val() + "";
                }
            });
            var StatusSelected = 0;
            var lstStatusSelected = $('#lstStatus option:selected');
            $(lstStatusSelected).each(function () {
                if (StatusSelected == 0) {
                    Status += "" + $(this).val() + "";
                    StatusSelected = 1;
                }
                else if (StatusSelected > 0) {
                    Status += ',';
                    Status += "" + $(this).val() + "";
                }
            });
            var UserSelected = 0;
            var lstUserSelected = $('#lstUsers option:selected');
            $(lstUserSelected).each(function () {
                if (UserSelected == 0) {
                    Users += "" + $(this).val() + "";
                    UserSelected = 1;
                }
                else if (UserSelected > 0) {
                    Users += ',';
                    Users += "" + $(this).val() + "";
                }
            });
            var WeekDaysSelected = 0;
            var lstWeekDaysSelected = $('#lstWeekDays option:selected');
            $(lstWeekDaysSelected).each(function () {
                if (WeekDaysSelected == 0) {
                    WeekDays += "" + $(this).val() + "";
                    WeekDaysSelected = 1;
                }
                else if (WeekDaysSelected > 0) {
                    WeekDays += ',';
                    WeekDays += "" + $(this).val() + "";
                }
            });
            var DurationOption = $('#lstDuration').val();
            var DurationVal = $('#txbDuration').val();
            var Param = { "ReportType": "Excel", "ReportId": ReportId, "DateRangeOption": DateRangeOption, "CallsOption": CallsOption, "dateFrom": dateFrom, "dateTo": dateTo, "timeFrom": timeFrom, "timeTo": timeTo, "IsSchedule": false, "Extensions": Extensions, "Groups": Groups, "ExternalRouting": ExternalRouting, "Agents": Agents, "Users": Users, "WeekDays": WeekDays, "TimeInterval": TimeInterval, "DBType": GlobalVarDBType, "Status": Status };

            $.ajax({
                type: "POST",
                url: "ReportsCommonMethods.aspx/GetReport",
                data: JSON.stringify(Param),
                //async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function () {

                    //$('#ReportFilter').modal('hide');
                    $.blockUI({
                        //message: 'Helloooooooooooooooooooooooooooooooooooo',
                        message: "<svg class='lds-spinner' width='200px'  height='200px'  xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' viewBox='0 0 100 100' preserveAspectRatio='xMidYMid' style='background: none;'><g transform='rotate(0 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.9166666666666666s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g><g transform='rotate(30 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.8333333333333334s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g><g transform='rotate(60 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.75s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g><g transform='rotate(90 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.6666666666666666s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g><g transform='rotate(120 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.5833333333333334s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g><g transform='rotate(150 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.5s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g><g transform='rotate(180 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.4166666666666667s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g><g transform='rotate(210 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.3333333333333333s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g><g transform='rotate(240 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.25s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g><g transform='rotate(270 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.16666666666666666s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g><g transform='rotate(300 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='-0.08333333333333333s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g><g transform='rotate(330 50 50)'>" +
                            "<rect x='47' y='15' rx='23.5' ry='7.5' width='6' height='20' fill='#f91627'>" +
                            "<animate attributeName='opacity' values='1;0' times='0;1' dur='1s' begin='0s' repeatCount='indefinite'></animate>" +
                            "</rect>" +
                            "</g></svg>",
                        css: {

                            padding: 0,
                            margin: 0,
                            width: '15%',
                            top: '40%',
                            left: '45%',
                            textAlign: 'center',
                            cursor: 'wait',
                            "z-index": "10001",
                            "border": "0px",
                            "background-color": "transparent"
                        }
                    });

                },
                success: function (data) {
                    $.unblockUI();
                    //$('#ReportFilter').modal('show');
                    $('#tblReport').empty();
                    if (data.d.length > 0) {
                        //debugger;
                        //  GlobalVarReportId = 0;
                        var Ac_Id = <%=HttpContext.Current.Session["vSupervisorDB"].ToString().Split('_')[1]%>
                            window.open('Reports/' + Ac_Id + '/' + data.d, '_blank');

                    }
                    else {
                        GlobalVarReportId = 0;
                    }
                }
                ,
                error: function (request, status, error) {
                    $.unblockUI();
                    console.log(error);
                    console.log(status);
                    console.log(request);
                    //$('#ReportFilter').modal('show');
                }
            });

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
            if (Name.trim() == "") {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide Schedule Name');
                return;
            }
            var Param = { "ScheduleId": 0, "Name": Name, "DBType": GlobalVarDBType };

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

            var ReportId = GlobalVarReportId;
            //var ReportId = parseInt($('#lstReports').val());
            var DateFilterCriteria = $('#lstDateRangeOption').val();
            var CallsOption = $('#lstCallsOption').val();
            var DateFrom = $("#txbDateFrom").val();
            var DateTo = $("#txbDateTo").val();
            var TimeFrom = $("#txbTimeFrom").val();
            var TimeTo = $("#txbTimeTo").val();
            var Boards = "";
            var Agents = "";
            var ExternalRouting = "";
            var Extensions = "";
            var Users = "";
            var WeekDays = "";
            var Emails = ""
            var TimeInterval = parseInt($('#lstTimeInterval').val());
            var ReportType = $("input[type='radio']:checked").val();
            
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
            var BoardsSelected = 0;
            if (ReportId == 45 || ReportId == 46) {
                var lstBoardSelected = $('#lstDDIs option:selected');
                $(lstBoardSelected).each(function () {
                    if (BoardsSelected == 0) {
                        Boards += $(this).val();
                        BoardsSelected = 1;
                    }
                    else if (BoardsSelected > 0) {
                        Boards += ',';
                        Boards += $(this).val();
                    }
                });
            }
            else {
                var lstBoardSelected = $('#lstGroups option:selected');
                $(lstBoardSelected).each(function () {
                    if (BoardsSelected == 0) {
                        Boards += $(this).val();
                        BoardsSelected = 1;
                    }
                    else if (BoardsSelected > 0) {
                        Boards += ',';
                        Boards += $(this).val();
                    }
                });
            }
            var AgentSelected = 0;
            var lstAgentSelected = $('#lstAgents option:selected');
            $(lstAgentSelected).each(function () {
                if (AgentSelected == 0) {
                    Agents += "" + $(this).val() + "";
                    AgentSelected = 1;
                }
                else if (AgentSelected > 0) {
                    Agents += ',';
                    Agents += "" + $(this).val() + "";
                }
            });
            var UserSelected = 0;
            var lstUserSelected = $('#lstUsers option:selected');
            $(lstUserSelected).each(function () {
                if (UserSelected == 0) {
                    Users += "" + $(this).val() + "";
                    UserSelected = 1;
                }
                else if (UserSelected > 0) {
                    Users += ',';
                    Users += "" + $(this).val() + "";
                }
            });
            var WeekDaysSelected = 0;
            var lstWeekDaysSelected = $('#lstWeekDays option:selected');
            $(lstWeekDaysSelected).each(function () {
                if (WeekDaysSelected == 0) {
                    WeekDays += "" + $(this).val() + "";
                    WeekDaysSelected = 1;
                }
                else if (WeekDaysSelected > 0) {
                    WeekDays += ',';
                    WeekDays += "" + $(this).val() + "";
                }
            });
            var ExternalRoutingSelected = 0;
            var lstExternalRoutingSelected = $('#lstExternalRouting option:selected');
            $(lstExternalRoutingSelected).each(function () {
                if (ExternalRoutingSelected == 0) {
                    ExternalRouting += "" + $(this).val() + "";
                    ExternalRoutingSelected = 1;
                }
                else if (ExternalRoutingSelected > 0) {
                    
                    ExternalRouting += ',';
                    ExternalRouting += "" + $(this).val() + "";
                }
            });

            var ScheduleInterval = parseInt($('#lstScheduleInterval').val());

            var ScheduleValue = $('#ddScheduleValue option:selected').text();
            var ScheduleTimeHours = $('#ddScheduleTimeHours option:selected').text();
            var ScheduleTimeMinutes = $('#ddScheduleTimeMinutes option:selected').text();



            $('#lstScheduleEmails option').each(function () {

                Emails += "" + $(this).val() + ";"
            });
           // debugger;
            var Param = { "Name": Name, "ReportId": ReportId, "DateFilterCriteria": DateFilterCriteria, "CallsOption": CallsOption, "WeekDays": WeekDays, "Extensions": Extensions, "Boards": Boards, "Agents": Agents, "Users": Users, "ExternalRouting":ExternalRouting, "ReportType": ReportType, "TimeInterval": TimeInterval, "ScheduleInterval": ScheduleInterval, "ScheduleValue": ScheduleValue, "ScheduleTimeHours": ScheduleTimeHours, "ScheduleTimeMinutes": ScheduleTimeMinutes, "Emails": Emails, "TimeFrom": TimeFrom, "TimeTo": TimeTo };

            $.ajax({
                type: "POST",
                url: "Schedules.aspx/AddReportSchedule",
                data: "{ReportScheduleEntityObj:" + JSON.stringify(Param) + ",DateFrom:" + JSON.stringify(DateFrom) + ",DateTo:" + JSON.stringify(DateTo) + ", DBType: '" + GlobalVarDBType + "' }",

               

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d.length > 0) {
                        $().toastmessage('showSuccessToast', 'Report Schedule saved successfully.');
                        ShowAddEditScheduleModal("Add");
                    }
                    else {
                        $().toastmessage('showErrorToast', 'Error saving Report Schedule.');
                    }
                }
            });

            DrawLeftACDPanel();
            DrawLeftLoggingPanel();
            DrawLeftRecorderPanel();
        }
        function GetScheduleOnReportId(ReportId, DBType) {

           // alert(ReportId);
           // alert(DBType);
            GlobalVarDBType = DBType;
           // debugger;
            $('#tblSchedules').empty();
            //ReportId = ReportId.substr(ReportId.length - 1)
            var Param = { "ReportId": ReportId, "DBType": DBType }
            $.ajax({
                type: "POST",
                url: "Schedules.aspx/GetScheduleOnReportId",
                data: JSON.stringify(Param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    var trHTML = '<thead><tr><th>Name</th><th>Report Name</th><th>Schedule Interval</th><th>Execution Time</th><th>Filters</th><th>Schedule</th><th>Delete</th></tr></thead><tbody></tbody>';
                    $('#tblSchedules').append(trHTML);
                    var boardsList = '';
                    for (var i = 0; i < data.d.length; i++) {

                        var trHTML = '';
                        trHTML += '<tr><td>' + data.d[i].Name + '</td>';
                        trHTML += '<td>' + data.d[i].ReportName + '</td>';
                        if (data.d[i].ScheduleInterval == "1") {
                            trHTML += '<td>Daily</td>';
                        }
                        else if (data.d[i].ScheduleInterval == "2") {
                            trHTML += '<td>Weekly</td>';
                        }
                        else if (data.d[i].ScheduleInterval == "3") {
                            trHTML += '<td>Monthly</td>';
                        }
                        if (data.d[i].ExecutionTime != null) {
                            trHTML += '<td>' + moment(new Date(parseInt(data.d[i].ExecutionTime.replace(/[^0-9 +]/g, '')))).format("DD-MM-YYYY HH:mm:ss") + '</td>';
                        }
                        else {
                            trHTML += '<td></td>';
                        }
                        
                       
                        trHTML += '<td><a class="edit"  onclick="EditReportFiltersOnSchedule(\'' + data.d[i].Id + '\',\'' + data.d[i].ReportId + '\',\'' + data.d[i].ReportName + '\',\'' + data.d[i].DateFilterCriteria+ '\',\'' + data.d[i].CallsOption + '\',\'' + data.d[i].DateFrom + '\',\'' + data.d[i].DateTo + '\',\'' + data.d[i].TimeFrom + '\',\'' + data.d[i].TimeTo + '\',\'' + data.d[i].Agents + '\',\'' + data.d[i].Boards + '\',\'' + data.d[i].Extensions + '\',\'' + data.d[i].Users + '\',\'' + data.d[i].WeekDays + '\',\'' + data.d[i].TimeInterval + '\',\''+ data.d[i].ExternalRouting + '\',\'' + DBType + '\')";>Edit</a></td>';
                        trHTML += '<td><a class="edit"  onclick="ScheduleEdit(\'' + data.d[i].Id + '\',\'' + data.d[i].Name + '\',\'' + data.d[i].ReportName + '\',\'' + data.d[i].ScheduleInterval + '\',\'' + data.d[i].ScheduleValue + '\',\'' + data.d[i].ScheduleTimeHours + '\',\'' + data.d[i].ScheduleTimeMinutes + '\',\'' + data.d[i].Emails + '\',\''+ data.d[i].ReportType + '\',\'' + DBType + '\')";>Edit</a></td>';
                        trHTML += '<td><a class="edit" style="padding: 0px;" onclick="showModalDeleteFromSchedules(\'' + data.d[i].Id + '\')";>Delete</a></td></tr>';
                        $('#tblSchedules').append(trHTML);
                    }
                }
            });

        }
        function ScheduleEdit(ScheduleId, ScheduleName, ReportName, ScheduleInterval, ScheduleValue, ScheduleTimeHours, ScheduleTimeMinutes, Emails, ReportType, DBType) {

            $('#lstScheduleEmails').empty();
            $('#txbScheduleId').val(ScheduleId);
            $('#txbScheduleName').val(ScheduleName);
            $('#lstScheduleInterval').val(ScheduleInterval);
            debugger;
            if(ReportType != null && ReportType != "")
            {
                $("input[name=optradio][value=" + ReportType + "]").attr('checked', 'checked');
            }
            if (ScheduleInterval == 1) {
                $("#SchedulValueRow").hide();
            }
            else if (ScheduleInterval == 2) {
                $("#ddScheduleValue").find('option').remove().end();
                $("#ddScheduleValue").append('<option value=Monday>Monday</option>');
                $("#ddScheduleValue").append('<option value=Tuesday>Tuesday</option>');
                $("#ddScheduleValue").append('<option value=Wednesday>Wednesday</option>');
                $("#ddScheduleValue").append('<option value=Thursday>Thursday</option>');
                $("#ddScheduleValue").append('<option value=Friday>Friday</option>');
                $("#ddScheduleValue").append('<option value=Saturday>Saturday</option>');
                $("#ddScheduleValue").append('<option value=Sunday>Sunday</option>');
                $("#SchedulValueRow").show();
                $("#ddScheduleValue").val(ScheduleValue);
            }
            else if (ScheduleInterval == 3) {
                $("#ddScheduleValue").find('option').remove().end();

                for (var i = 1; i <= 31; i++) {
                    $("#ddScheduleValue").append("<option value=" + i + ">" + i + "</option>");
                }
                $("#SchedulValueRow").show();
                $("#ddScheduleValue").val(ScheduleValue);
            }
            $("#ddScheduleTimeHours").val(ScheduleTimeHours);
            $("#ddScheduleTimeMinutes").val(ScheduleTimeMinutes);

            var ScheduleEmails = Emails.split(";");
            for (var i = 0; i < ScheduleEmails.length; i++) {
                if (ScheduleEmails[i] != "")
                    $('#lstScheduleEmails').append($('<option>', { value: ScheduleEmails[i], text: ScheduleEmails[i] }));
            }
            ShowAddEditScheduleModal("Edit", ReportName);
        }
        function EditReportFiltersOnSchedule(ScheduleId, ReportId, ReportName, DateFilterCriteria, CallsOption, DateFrom, DateTo, TimeFrom, TimeTo, Agents, Boards, Extensions, Users, WeekDays, TimeInterval, ExternalRouting, DBType) {

            $('#txbScheduleFilterId').val(ScheduleId);
            GetSectionsOnReportId(ReportId, ReportName, DBType, "Edit");
            $("#ddlModule option:selected").removeAttr("selected");
            $("#lstDateRangeOption").val(DateFilterCriteria);
            $("#lstCallsOption").val(CallsOption);
            var AgentsArray = Agents.split(',');
            var BoardsArray = Boards.split(',');
            var ExtensionsArray = Extensions.split(',');
            var UsersArray = Users.split(',');
            var ExternalRoutingArray = ExternalRouting.split(',');
            var WeekDaysArray = WeekDays.split(',');
            $("#lstAgents").val(AgentsArray);
            $("#lstExtensions").val(ExtensionsArray);
            $("#lstExtensions").multiselect("refresh");

            if (ReportId == 45 || ReportId == 46) {
                $("#lstDDIs").val(BoardsArray);
            }
            else {
                $("#lstGroups").val(BoardsArray);
            }
            $("#lstUsers").val(UsersArray);
            $("#lstWeekDays").val(WeekDaysArray);
            $('#lstTimeInterval').val(TimeInterval);
            $("#lstExternalRouting").val(ExternalRoutingArray);
            $("#lstAgents").multiselect("refresh");            
            $("#lstGroups").multiselect("refresh");
            $("#lstDDIs").multiselect("refresh");
            $("#lstStatus").multiselect("refresh");
            $("#lstUsers").multiselect("refresh");
            $("#lstWeekDays").multiselect("refresh");
            $("#lstExternalRouting").multiselect("refresh");

            if (parseInt(DateFilterCriteria) == 7) {
                $("#CustomTimePickerRow").hide();
                $("#CustomDatePickerRow").show();
                $("#txbDateFrom").val(moment(DateFrom).format("DD/MM/YYYY HH:mm:ss"));
                $("#txbDateTo").val(moment(DateTo).format("DD/MM/YYYY HH:mm:ss"));
            }
            else {
              
                $("#txbTimeFrom").val(TimeFrom);
                $("#txbTimeTo").val(TimeTo);
                
            }
        }
        function UpdateReportFilters() {
            //alert(GlobalVarReportId);
            var Id = $('#txbScheduleFilterId').val();
            var DateFilterCriteria = $('#lstDateRangeOption').val();
            var CallsOption = $('#lstCallsOption').val();
            var DateFrom = $("#txbDateFrom").val();
            var DateTo = $("#txbDateTo").val();
            var TimeFrom = $("#txbTimeFrom").val();
            var TimeTo = $("#txbTimeTo").val();
            var Boards = "";
            var Agents = "";
            var Status = "";
            var ExternalRouting = "";
            var Extensions = "";
            var Users = "";
            var WeekDays = "";
            var Emails = "";
            var TimeInterval = parseInt($('#lstTimeInterval').val());
            var ReportType = $("input[type='radio']:checked").val();

            var ExtensionSelected = 0;
            var lstExtensionSelected = $('#lstExtensions option:selected');
            var ReportId = GlobalVarReportId;

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
            var BoardsSelected = 0;
            if (ReportId == 45 || ReportId == 46) {
                var lstBoardSelected = $('#lstDDIs option:selected');
                $(lstBoardSelected).each(function () {
                    if (BoardsSelected == 0) {
                        Boards += $(this).val();
                        BoardsSelected = 1;
                    }
                    else if (BoardsSelected > 0) {
                        Boards += ',';
                        Boards += $(this).val();
                    }
                });
            }
            else {
                var lstBoardSelected = $('#lstGroups option:selected');
                $(lstBoardSelected).each(function () {
                    if (BoardsSelected == 0) {
                        Boards += $(this).val();
                        BoardsSelected = 1;
                    }
                    else if (BoardsSelected > 0) {
                        Boards += ',';
                        Boards += $(this).val();
                    }
                });
            }
            var AgentSelected = 0;
            var lstAgentSelected = $('#lstAgents option:selected');
            $(lstAgentSelected).each(function () {
                if (AgentSelected == 0) {
                    Agents += "" + $(this).val() + "";
                    AgentSelected = 1;
                }
                else if (AgentSelected > 0) {
                    Agents += ',';
                    Agents += "" + $(this).val() + "";
                }
            });
            var StatusSelected = 0;
            var lstStatusSelected = $('#lstStatus option:selected');
            $(lstStatusSelected).each(function () {
                if (StatusSelected == 0) {
                    Status += "" + $(this).val() + "";
                    StatusSelected = 1;
                }
                else if (StatusSelected > 0) {
                    Status += ',';
                    Status += "" + $(this).val() + "";
                }
            });
            var ExternalRoutingSelected = 0;
            var lstExternalRoutingSelected = $('#lstExternalRouting option:selected');
            $(lstExternalRoutingSelected).each(function () {
                if (ExternalRoutingSelected == 0) {
                    ExternalRouting += "" + $(this).val() + "";
                    ExternalRoutingSelected = 1;
                }
                else if (ExternalRoutingSelected > 0) {
                    
                    ExternalRouting += ',';
                    ExternalRouting += "" + $(this).val() + "";
                }
            });
            var UserSelected = 0;
            var lstUserSelected = $('#lstUsers option:selected');
            $(lstUserSelected).each(function () {
                if (UserSelected == 0) {
                    Users += "" + $(this).val() + "";
                    UserSelected = 1;
                }
                else if (UserSelected > 0) {
                    Users += ',';
                    Users += "" + $(this).val() + "";
                }
            });
            var WeekDaysSelected = 0;
            var lstWeekDaysSelected = $('#lstWeekDays option:selected');
            $(lstWeekDaysSelected).each(function () {
                if (WeekDaysSelected == 0) {
                    WeekDays += "" + $(this).val() + "";
                    WeekDaysSelected = 1;
                }
                else if (WeekDaysSelected > 0) {
                    WeekDays += ',';
                    WeekDays += "" + $(this).val() + "";
                }
            });

            var Param = { "Id": Id, "DateFilterCriteria": DateFilterCriteria, "CallsOption": CallsOption, "WeekDays": WeekDays, "Extensions": Extensions, "Boards": Boards, "Agents": Agents, "Users": Users, "ExternalRouting":ExternalRouting,"ReportType":ReportType, "TimeInterval": TimeInterval, "TimeFrom": TimeFrom, "TimeTo": TimeTo };

            $.ajax({
                type: "POST",
                url: "Schedules.aspx/UpdateScheduleFilters",
                data: "{ReportScheduleEntityObj:" + JSON.stringify(Param) + ",DateFrom:" + JSON.stringify(DateFrom) + ",DateTo:" + JSON.stringify(DateTo) + ", DBType: '" + GlobalVarDBType + "' }",

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    if (data.d == true) {

                        $().toastmessage('showSuccessToast', 'Report Schedule saved successfully.');
                        GetScheduleOnReportId(GlobalVarReportId, GlobalVarDBType);
                        //ShowAddEditScheduleModal("Add");
                    }
                    else {
                        $().toastmessage('showErrorToast', 'Error saving Report Schedule.');
                    }
                }
            });
        }
        function UpdateSchedule() {
            var ScheduleExists = 0;
            var Id = $('#txbScheduleId').val();

            var Name = $('#txbScheduleName').val();
            if (Name.trim() == "") {
                $('#ErrorDivAdd').show();
                $('#lblErrorAdd').text('Please provide Schedule Name');
                return;
            }
            var Param = { "ScheduleId": Id, "Name": Name, "DBType": GlobalVarDBType };

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
            var Emails = ""
            var ScheduleInterval = parseInt($('#lstScheduleInterval').val());
            var ScheduleValue = $('#ddScheduleValue option:selected').text();
            var ScheduleTimeHours = $('#ddScheduleTimeHours option:selected').val();
            var ScheduleTimeMinutes = $('#ddScheduleTimeMinutes option:selected').val();
            var ReportType = $("input[type='radio']:checked").val();
            $('#lstScheduleEmails option').each(function () {

                Emails += "" + $(this).val() + ";"
            });

            Param = { "Id": Id, "Name": Name, "ScheduleInterval": ScheduleInterval, "ScheduleValue": ScheduleValue, "ScheduleTimeHours": ScheduleTimeHours, "ScheduleTimeMinutes": ScheduleTimeMinutes, "Emails": Emails, "ReportType": ReportType };

            $.ajax({
                type: "POST",
                url: "Schedules.aspx/UpdateIntoSchedules",
                data: "{ReportScheduleEntityObj:" + JSON.stringify(Param) + ", DBType: '" + GlobalVarDBType + "' }",

                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d === true) {
                        $().toastmessage('showSuccessToast', 'Schedule updated successfully.');
                        $('#AddEditScheduleModal').modal('hide');
                        GetScheduleOnReportId(GlobalVarReportId, GlobalVarDBType);
                    }
                    else {
                        $().toastmessage('showErrorToast', 'Error updating  Schedule.');
                    }
                }
            });
            $('#txbScheduleName').val();
        }
        function showModalDeleteFromSchedules(ScheduleId){
            $('#ModalDeleteFromSchedules').modal('show');
            $('#hidDeleteScheduleId').val(ScheduleId);
            
        }
        function DeleteFromSchedules() {
            var ScheduleId = $('#hidDeleteScheduleId').val();
            var Param = { "ScheduleId": ScheduleId, "DBType": GlobalVarDBType }
            //var ConfirmDelete = confirm("Are you sure you want to delete?");
            //if (ConfirmDelete == false) {
            //    return;
            //}
            $.ajax({
                type: "POST",
                url: "Schedules.aspx/DeleteFromSchedules",
                data: JSON.stringify(Param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d === true) {
                        GetScheduleOnReportId(GlobalVarReportId, GlobalVarDBType);
                        //$('#AddEditScheduleModal').modal('hide');
                        $().toastmessage("showSuccessToast", "Schedule deleted successfully.");
                    }
                    else {
                        $().toastmessage("showErrorToast", "Failed to delete Schedule.");
                    }
                    $('#ModalDeleteFromSchedules').modal('hide');
                    $('#hidDeleteScheduleId').val(null);
                }
            });
            DrawLeftACDPanel();
            DrawLeftLoggingPanel();
            DrawLeftRecorderPanel();
        }
        function ShowAddEditScheduleModal(type, ReportName) {

            if (type == "Edit") {

                $('#ReportNameOnScheduleAddEdit').text(ReportName);
                $('#ErrorDivAdd').hide();
                $('#AddEditScheduleModal').modal('show');
                $('#divAddSchedule').hide();
                $('#divEditSchedule').show();

            }
            else {
                $('#ReportNameOnScheduleAddEdit').text($('#ReportNameOnFilter').text());

                $('#ErrorDivAdd').hide();
                $('#txbScheduleEmail').val("");
                $('#txbScheduleName').val("");
                $('#lstScheduleInterval').val(1);
                $("#ddScheduleTimeHours").val(0);
                $("#ddScheduleTimeMinutes").val(0);
                $('#lstScheduleEmails option').remove();
                $("#SchedulValueRow").hide();
                $('#divAddSchedule').show();
                $('#divEditSchedule').hide();
                $('#AddEditScheduleModal').modal('show');
            }

        }


        function DrawLeftCustomPanel() {
            if (<%=System.Configuration.ConfigurationManager.AppSettings["SectionTitle"].Trim().Length %>> 0) {
                var Param = { "ReportType": "", "DBType": "vBoard" }
                $.ajax({
                    type: "POST",
                    url: "ReportsCommonMethods.aspx/GetReports",
                    data: JSON.stringify(Param),
                    //3516async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        $("#divCustomReports").html('');
                        if (data.d) {
                            //Naqi//
                            ACDReportsLicense = 1;
                            if (ACDReportsLicense == 0) {
                                var $divACDReports = $("#divCustomReports");
                                var $Row = $("<div/>", { id: "CustomRow" + i, "class": "row" });
                                $Row.css({ "background-color": "white", "padding": "0px 5px 0px 10px" });
                                $Row.appendTo($divACDReports);
                                var $Col1 = $("<div/>", { id: "CustomCol1" + i, "class": "col-md-12" });
                                $Col1.css({ "padding": "0px" });
                                $Col1.appendTo($Row);

                                var $span = $("<span/>", { id: "Customspan" + i });
                                if (IsOXO == "0") {
                                    $span.text("CCD Reports License Expired.");
                                }
                                else {
                                    $span.text("Custom Reports License Expired.");
                                }
                                $span.css({ "font-size": "12px", "color": "#f91627" });
                                $span.appendTo($Col1);
                            }
                            else if (data.d.length > 0) {

                                for (var i = 0; i < data.d.length; i++) {
                                    var $divACDReports = $("#divCustomReports");
                                    var $Row = $("<div/>", { id: "CustomRow" + i, "class": "row" });
                                    $Row.css({ "background-color": "white", "padding": "0px 5px 0px 10px" });
                                    $Row.appendTo($divACDReports);
                                    var $Col1 = $("<div/>", { id: "CustomCol1" + i, "class": "col-md-8" });
                                    $Col1.css({ "padding": "0px" });
                                    $Col1.appendTo($Row);
                                    var $Col2 = $("<div/>", { id: "CustomCol2" + i, "class": "col-md-2" });
                                    $Col2.css({ "text-align": "right", "padding": "0px", "width": "19.667%" });
                                    $Col2.appendTo($Row);
                                    var $Col3 = $("<div/>", { id: "CustomCol3" + i, "class": "col-md-2" });
                                    $Col3.css({ "text-align": "right", "padding": "0px", "width": "13.667%" });
                                    $Col3.appendTo($Row);
                                    var $ul = $("<ul/>", { id: "Customul" + i });
                                    $ul.css({ "list-style": "none", "padding": "0px" });
                                    $ul.appendTo($Col1);
                                    var $li = $("<li/>", { id: "Customli" + i });
                                    $li.appendTo($ul);
                                    var $i = $("<i/>", { id: "Customi" + i, "class": "fa fa-file-pdf-o" });
                                    $i.css({ "color": "#f91627" });
                                    $i.appendTo($li);
                                    var $span = $("<span/>", { id: "Customspan" + i });
                                    var ReportName = data.d[i].ReportName;
                                    //if (ReportName.length>30)
                                    //{
                                    ReportName = ReportName.replace("Extension", "Ext.");
                                    //}
                                    $span.text(ReportName);


                                    $span.css({ "font-size": "11px" });
                                    $span.appendTo($li);
                                    var $btnSchedule = $("<input id=btnScheduleCustomReport_" + data.d[i].RID + " type=button value=Schedule class=btnLeftPanelGrey />");

                                    $btnSchedule.appendTo($Col2);
                                    if (data.d[i].ScheduleCount == 0) {
                                        $btnSchedule.prop('disabled', true);
                                    }
                                    $btnSchedule.click(function () {
                                        $('#ViewScheduleModal').modal('show');


                                        GlobalVarReportId = this.id.split('_')[1];
                                        GetScheduleOnReportId(GlobalVarReportId, "vBoard");

                                    });
                                    var $btnRunReport = $("<input id=btnRunCustomReport_" + data.d[i].RID + " data-reportname='" + data.d[i].ReportName + "' type=button value=Run class=btnLeftPanel />");

                                    $btnRunReport.appendTo($Col3);


                                    $btnRunReport.click(function () {
                                        //  debugger;
                                        GlobalVarReportId = this.id.split('_')[1];
                                        if (parseInt(GlobalVarReportId) > 0) {

                                            GetSectionsOnReportId(GlobalVarReportId, $(this).attr("data-reportname"), "vBoard", "Add");
                                        }
                                        else {
                                            $("#Sections").hide();
                                        }
                                    });
                                }
                            }
                        }
                        else {

                            var $divCustomReports = $("#divCustomReports");
                            var $Row = $("<div/>", { id: "CustomRow" + i, "class": "row" });
                            $Row.css({ "background-color": "white", "padding": "0px 5px 0px 10px" });
                            $Row.appendTo($divCustomReports);
                            var $Col1 = $("<div/>", { id: "CustomCol1" + i, "class": "col-md-12" });
                            $Col1.css({ "padding": "0px" });
                            $Col1.appendTo($Row);


                            var $span = $("<span/>", { id: "Customspan" + i });
                            $span.text("vBoard App Not Installed.");
                            $span.css({ "font-size": "12px", "color": "#f91627" });
                            $span.appendTo($Col1);

                        }
                    }
                });
            }
            else {
                $("#CustomSection").css("display", "none");
            }
        }



        function DrawLeftACDPanel() {
            
            var Param = { "ReportType": "ACD", "DBType": "vBoard" }
            $.ajax({
                type: "POST",
                url: "ReportsCommonMethods.aspx/GetReports",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divACDReports").html('');
                    if(data.d)
                    {
                        //Naqi//
                        ACDReportsLicense = 1;
                        if(ACDReportsLicense==0)
                        {
                            var $divACDReports = $("#divACDReports");
                            var $Row = $("<div/>", { id: "ACDRow" + i, "class": "row" });
                            $Row.css({ "background-color": "white", "padding": "0px 5px 0px 10px" });
                            $Row.appendTo($divACDReports);
                            var $Col1 = $("<div/>", { id: "ACDCol1" + i, "class": "col-md-12" });
                            $Col1.css({ "padding": "0px" });
                            $Col1.appendTo($Row);

                            var $span = $("<span/>", { id: "ACDspan" + i });
                            if(IsOXO == "0")
                            {
                                $span.text("CCD Reports License Expired.");
                            }
                            else
                            {
                                $span.text("ACD Reports License Expired.");
                            }                            
                            $span.css({ "font-size": "12px","color":"#f91627" });
                            $span.appendTo($Col1);
                        }
                        else if (data.d.length > 0) {

                            for (var i = 0; i < data.d.length; i++) {
                                var $divACDReports = $("#divACDReports");
                                var $Row = $("<div/>", { id: "ACDRow" + i, "class": "row" });
                                $Row.css({ "background-color": "white", "padding": "0px 5px 0px 10px" });
                                $Row.appendTo($divACDReports);
                                var $Col1 = $("<div/>", { id: "ACDCol1" + i, "class": "col-md-8" });
                                $Col1.css({ "padding": "0px" });
                                $Col1.appendTo($Row);
                                var $Col2 = $("<div/>", { id: "ACDCol2" + i, "class": "col-md-2" });
                                $Col2.css({ "text-align": "right", "padding": "0px", "width": "19.667%" });
                                $Col2.appendTo($Row);
                                var $Col3 = $("<div/>", { id: "ACDCol3" + i, "class": "col-md-2" });
                                $Col3.css({ "text-align": "right", "padding": "0px", "width": "13.667%" });
                                $Col3.appendTo($Row);
                                var $ul = $("<ul/>", { id: "ACDul" + i });
                                $ul.css({ "list-style": "none", "padding": "0px" });
                                $ul.appendTo($Col1);
                                var $li = $("<li/>", { id: "ACDli" + i });
                                $li.appendTo($ul);
                                var $i = $("<i/>", { id: "ACDi" + i, "class": "fa fa-file-pdf-o" });
                                $i.css({ "color": "#f91627" });
                                $i.appendTo($li);
                                var $span = $("<span/>", { id: "ACDspan" + i });
                                var ReportName = data.d[i].ReportName;
                                //if (ReportName.length>30)
                                //{
                                ReportName = ReportName.replace("Extension", "Ext.");
                                //}
                                $span.text(ReportName);


                                $span.css({ "font-size": "11px" });
                                $span.appendTo($li);
                                var $btnSchedule = $("<input id=btnScheduleACDReport_" + data.d[i].RID + " type=button value=Schedule class=btnLeftPanelGrey />");

                                $btnSchedule.appendTo($Col2);
                                if (data.d[i].ScheduleCount == 0) {
                                    $btnSchedule.prop('disabled', true);
                                }
                                $btnSchedule.click(function () {
                                    $('#ViewScheduleModal').modal('show');


                                    GlobalVarReportId = this.id.split('_')[1];
                                    GetScheduleOnReportId(GlobalVarReportId, "vBoard");

                                });
                                var $btnRunReport = $("<input id=btnRunACDReport_" + data.d[i].RID + " data-reportname='" + data.d[i].ReportName + "' type=button value=Run class=btnLeftPanel />");

                                $btnRunReport.appendTo($Col3);

                                
                                $btnRunReport.click(function () {
                                  //  debugger;
                                    GlobalVarReportId = this.id.split('_')[1];
                                    if (parseInt(GlobalVarReportId) > 0) {

                                        GetSectionsOnReportId(GlobalVarReportId, $(this).attr("data-reportname"), "vBoard", "Add");
                                    }
                                    else {
                                        $("#Sections").hide();
                                    }
                                });
                            }
                        }
                    }
                    else
                    {
                       
                        var $divACDReports = $("#divACDReports");
                        var $Row = $("<div/>", { id: "ACDRow" + i, "class": "row" });
                        $Row.css({ "background-color": "white", "padding": "0px 5px 0px 10px" });
                        $Row.appendTo($divACDReports);
                        var $Col1 = $("<div/>", { id: "ACDCol1" + i, "class": "col-md-12" });
                        $Col1.css({ "padding": "0px" });
                        $Col1.appendTo($Row);
                            

                        var $span = $("<span/>", { id: "ACDspan" + i });
                        $span.text("vBoard App Not Installed.");
                        $span.css({ "font-size": "12px","color":"#f91627" });
                        $span.appendTo($Col1);
                          
                    }
                }
            });
        }
        function DrawLeftLoggingPanel() {

            

            var Param = { "ReportType": "Logging", "DBType": "vBoard" }
            $.ajax({
                type: "POST",
                url: "ReportsCommonMethods.aspx/GetReports",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divLoggingReports").html('');
                    if(data.d)
                    {
                        //Naqi
                        LoggingReportsLicense = 1;
                        if(LoggingReportsLicense==0)
                        {
                            var $divLoggingReports = $("#divLoggingReports");
                            var $Row = $("<div/>", { id: "LoggingRow" + i, "class": "row" });
                            $Row.css({ "background-color": "white", "padding": "0px 5px 0px 10px" });
                            $Row.appendTo($divLoggingReports);
                            var $Col1 = $("<div/>", { id: "LoggingCol1" + i, "class": "col-md-12" });
                            $Col1.css({ "padding": "0px" });
                            $Col1.appendTo($Row);
                            
                            var $span = $("<span/>", { id: "Loggingspan" + i });
                            $span.text("Logging Reports License Expired.");
                            $span.css({ "font-size": "12px","color":"#f91627" });
                            $span.appendTo($Col1);
                        }
                        else if (data.d.length > 0) {
                            for (var i = 0; i < data.d.length; i++) {
                                var $divLoggingReports = $("#divLoggingReports");
                                var $Row = $("<div/>", { id: "LoggingRow" + i, "class": "row" });
                                $Row.css({ "background-color": "white", "padding": "0px 5px 0px 10px" });
                                $Row.appendTo($divLoggingReports);
                                var $Col1 = $("<div/>", { id: "LoggingCol1" + i, "class": "col-md-8" });
                                $Col1.css({ "padding": "0px" });
                                $Col1.appendTo($Row);
                                var $Col2 = $("<div/>", { id: "LoggingCol2" + i, "class": "col-md-2" });
                                $Col2.css({ "text-align": "right", "padding": "0px", "width": "19.667%" });
                                $Col2.appendTo($Row);
                                var $Col3 = $("<div/>", { id: "LoggingCol3" + i, "class": "col-md-2" });
                                $Col3.css({ "text-align": "right", "padding": "0px", "width": "13.667%" });
                                $Col3.appendTo($Row);
                                var $ul = $("<ul/>", { id: "Loggingul" + i });
                                $ul.css({ "list-style": "none", "padding": "0px" });
                                $ul.appendTo($Col1);
                                var $li = $("<li/>", { id: "Loggingli" + i });
                                $li.appendTo($ul);
                                var $i = $("<i/>", { id: "Loggingi" + i, "class": "fa fa-file-pdf-o" });
                                $i.css({ "color": "#f91627" });
                                $i.appendTo($li);
                                var $span = $("<Loggingspan>", { id: "Loggingspan" + i });

                                var ReportName = data.d[i].ReportName;
                                ReportName = ReportName.replace("Extension", "Ext.");
                                $span.text(ReportName);
                                $span.css({ "font-size": "11px" });
                                $span.appendTo($li);
                                var $btnSchedule = $("<input id=btnScheduleLogReport_" + data.d[i].RID + " type=button value=Schedule class=btnLeftPanelGrey />");
                                $btnSchedule.appendTo($Col2);
                                if (data.d[i].ScheduleCount == 0) {
                                    $btnSchedule.prop('disabled', true);
                                }
                                $btnSchedule.click(function () {
                                    $('#ViewScheduleModal').modal('show');
                                    GlobalVarReportId = this.id.split('_')[1];
                                    GetScheduleOnReportId(GlobalVarReportId, "vBoard");
                                });
                                var $btnRunReport = $("<input id=btnRunLogReport_" + data.d[i].RID + " data-reportname='" + data.d[i].ReportName + "' type=button value=Run class=btnLeftPanel />");
                                $btnRunReport.appendTo($Col3);
                                $btnRunReport.click(function () {
                                    GlobalVarReportId = this.id.split('_')[1];
                                    if (parseInt(GlobalVarReportId) > 0) {
                                        GetSectionsOnReportId(GlobalVarReportId, $(this).attr("data-reportname"), "vBoard", "Add");
                                    }
                                    else {
                                        $("#Sections").hide();
                                    }

                                });

                            }
                        }
                    }
                    else
                    {
                        var $divLoggingReports = $("#divLoggingReports");
                        var $Row = $("<div/>", { id: "LoggingRow" + i, "class": "row" });
                        $Row.css({ "background-color": "white", "padding": "0px 5px 0px 10px" });
                        $Row.appendTo($divLoggingReports);
                        var $Col1 = $("<div/>", { id: "LoggingCol1" + i, "class": "col-md-12" });
                        $Col1.css({ "padding": "0px" });
                        $Col1.appendTo($Row);
                            
                        var $span = $("<span/>", { id: "Loggingspan" + i });
                        $span.css({ "font-size": "12px","color":"#f91627" });
                        $span.text("vBoard App Not Installed.");
                        $span.appendTo($Col1);
                    }
                }
            });
        }
        function DrawLeftRecorderPanel() {
            
            var Param = { "ReportType": "Activity", "DBType": "vRecord" }
            $.ajax({
                type: "POST",
                url: "ReportsCommonMethods.aspx/GetReports",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divRecorderReports").html('');
                    if(data.d)
                    {
                        if (data.d.length > 0) 
                        {
                            for (var i = 0; i < data.d.length; i++) {
                                var $divRecorderReports = $("#divRecorderReports");
                                var $Row = $("<div/>", { id: "RecorderRow" + i, "class": "row" });
                                $Row.css({ "background-color": "white", "padding": "0px 5px 0px 10px" });
                                $Row.appendTo($divRecorderReports);
                                var $Col1 = $("<div/>", { id: "RecorderCol1" + i, "class": "col-md-8" });
                                $Col1.css({ "padding": "0px" });
                                $Col1.appendTo($Row);
                                var $Col2 = $("<div/>", { id: "RecorderCol2" + i, "class": "col-md-2" });
                                $Col2.css({ "text-align": "right", "padding": "0px", "width": "19.667%" });
                                $Col2.appendTo($Row);
                                var $Col3 = $("<div/>", { id: "RecorderCol3" + i, "class": "col-md-2" });
                                $Col3.css({ "text-align": "right", "padding": "0px", "width": "13.667%" });
                                $Col3.appendTo($Row);
                                var $ul = $("<ul/>", { id: "Recorderul" + i });
                                $ul.css({ "list-style": "none", "padding": "0px" });
                                $ul.appendTo($Col1);
                                var $li = $("<li/>", { id: "Recorderli" + i });
                                $li.appendTo($ul);
                                var $i = $("<i/>", { id: "Recorderi" + i, "class": "fa fa-file-pdf-o" });
                                $i.css({ "color": "#f91627" });
                                $i.appendTo($li);
                                var $span = $("<span/>", { id: "Recorderspan" + i });

                                var ReportName = data.d[i].ReportName;
                                ReportName = ReportName.replace("Extension", "Ext.");
                                $span.text(ReportName);


                                $span.css({ "font-size": "11px" });
                                $span.appendTo($li);
                                var $btnSchedule = $("<input id=btnScheduleRecorderReport_" + data.d[i].RID + " type=button value=Schedule class=btnLeftPanelGrey />");

                                $btnSchedule.appendTo($Col2);
                                if (data.d[i].ScheduleCount == 0) {
                                    $btnSchedule.prop('disabled', true);
                                }
                                $btnSchedule.click(function () {
                                    $('#ViewScheduleModal').modal('show');
                                    GlobalVarReportId = this.id.split('_')[1];
                                    GetScheduleOnReportId(GlobalVarReportId, "vRecord");

                                });
                                var $btnRunReport = $("<input id=btnRunRecorderReport_" + data.d[i].RID + " data-reportname='" + data.d[i].ReportName + "' type=button value=Run class=btnLeftPanel />");
                                $btnRunReport.appendTo($Col3);
                                $btnRunReport.click(function () {
                                    GlobalVarReportId = this.id.split('_')[1];
                                    if (parseInt(GlobalVarReportId) > 0) {
                                        GetSectionsOnReportId(GlobalVarReportId, $(this).attr("data-reportname"), "vRecord", "Add");
                                    }
                                    else {
                                        $("#Sections").hide();
                                    }
                                });
                            }
                        }
                    }
                    else
                    {
                        var $divRecorderReports = $("#divRecorderReports");
                        var $Row = $("<div/>", { id: "RecorderRow" + i, "class": "row" });
                        $Row.css({ "background-color": "white", "padding": "0px 5px 0px 10px" });
                        $Row.appendTo($divRecorderReports);
                        var $Col1 = $("<div/>", { id: "RecorderCol1" + i, "class": "col-md-12" });
                        $Col1.css({ "padding": "0px" });
                        $Col1.appendTo($Row);
                            
                        var $span = $("<span/>", { id: "Loggingspan" + i });
                        $span.text("vRecord App Not Installed");
                        $span.css({ "font-size": "12px","color":"#f91627" });
                        $span.appendTo($Col1);
                    }
                }
            });
        }

        function GetExtension() {

            //$.ajax({
            //    type: "POST",
            //    url: "ReportsCommonMethods.aspx/GetExtensions",
            //    data: {},
            //    //3516async: false,
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (data) {
            //        if (data.d.length > 0) {
            //            for (var i = 0; i < data.d.length; i++) {
            //                $("#lstExtensions").append("<option value='" + data.d[i].Extension + "'>" + data.d[i].Extension + "</option>");
            //            }
            //        }
            //    }
            //});

            // Extensions, Groups, Agents Dropdown make multiselect
            $("#lstExtensions").multiselect({
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

        }
        function GetBoards() {

            //$.ajax({
            //    type: "POST",
            //    url: "ReportsCommonMethods.aspx/GetBoards",
            //    data: {},
            //    //3516async: false,
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

            $('#lstGroups').multiselect({
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
            $("#lstGroups").multiselect('selectAll', false);
            $("#lstGroups").multiselect('updateButtonText');
        }

        function GetDDIs() {

            //$.ajax({
            //    type: "POST",
            //    url: "ReportsCommonMethods.aspx/GetBoards",
            //    data: {},
            //    //3516async: false,
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

            $('#lstDDIs').multiselect({
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
            $("#lstDDIs").multiselect('selectAll', false);
            $("#lstDDIs").multiselect('updateButtonText');
        }

        function GetAgents() {

            //$.ajax({
            //    type: "POST",
            //    url: "ReportsCommonMethods.aspx/GetAgents",
            //    data: {},
            //    //3516async: false,
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

            $('#lstAgents').multiselect({
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
            $("#lstAgents").multiselect('selectAll', false);
            $("#lstAgents").multiselect('updateButtonText');
        }

        function GetStatus() {

            //$.ajax({
            //    type: "POST",
            //    url: "ReportsCommonMethods.aspx/GetAgents",
            //    data: {},
            //    //3516async: false,
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

            $('#lstStatus').multiselect({
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
            $("#lstStatus").multiselect('selectAll', false);
            $("#lstStatus").multiselect('updateButtonText');
        }
        function GetExtRouting() {     
            
            //$.ajax({
            //    type: "POST",
            //    url: "ReportsCommonMethods.aspx/GetExternalRouting",
            //    data: {},
            //    //3516async: false,
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (data) {
            //        if(data.d){
            //            if (data.d.length > 0) {
            //                $("#lstExternalRouting").find('option').remove().end();
            //                for (var i = 0; i < data.d.length; i++) {
            //                    var Number = data.d[i].Number;
            //                    var Name = data.d[i].Name;
            //                    $("#lstExternalRouting").append("<option value=" + Number + ">" + Name + "</option>");
            //                }
            //            }
            //        }
            //    }
            //});

            $('#lstExternalRouting').multiselect({
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
            $("#lstExternalRouting").multiselect('selectAll', false);
            $("#lstExternalRouting").multiselect('updateButtonText');
        }
        function GetRecorderUsers() {

            //$.ajax({
            //    type: "POST",
            //    url: "ReportsCommonMethods.aspx/GetUsers",
            //    data: {},
            //    //3516async: false,
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (data) {
            //        if(data.d){
            //            if (data.d.length > 0) {
            //                $("#lstUsers").find('option').remove().end();
            //                for (var i = 0; i < data.d.length; i++) {
            //                    $("#lstUsers").append("<option value='" + data.d[i].UserId + "'>" + data.d[i].UserName + "</option>");
            //                }
            //            }}
            //    }
            //});

            $('#lstUsers').multiselect({
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
            $("#lstUsers").multiselect('selectAll', false);
            $("#lstUsers").multiselect('updateButtonText');
        }
        function GetWeekDays() {

            $('#lstWeekDays').multiselect({
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
            $("#lstWeekDays").multiselect('selectAll', false);
            $("#lstWeekDays").multiselect('updateButtonText');
        }
        
        function FillHourMinuteDropdowns() {
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

        }
        function DatePickersDefaultSet() {
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
        }
        function RegEventDateRangeChange() {
            $('#lstDateRangeOption').val(1);
            $('#lstDateRangeOption').on('change', function () {
                var DateRangeOption = this.value;
                if (parseInt(DateRangeOption) == 7) {
                    $("#CustomTimePickerRow").hide();
                    $("#CustomDatePickerRow").show();
                }
                else {
                    $("#CustomTimePickerRow").show();
                    $("#CustomDatePickerRow").hide();
                }
            });
        }
        function RegEventScheduleIntervalChange() {
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
        }
        function RegEventddlGraphDurationChange() {
            $("#ddlGraphDuration").change(function () {
                DrawCenterMenuGraphs();
                DrawRightMenuGraphs();
            });
        }
        function RegEventddlCallsOptionChange() {
            $("#ddlCallsOption").change(function () {
                DrawCenterMenuGraphs();
                DrawRightMenuGraphs();
            });
        }
        function RegEventRightMenuButton1Click() {
            //alert(RightMenuChart1Type);
            $("#btnInboundCallsByHourChart").click(function () {          
                var CallsOption = $("#ddlCallsOption").val();
               
                if (RightMenuChart1Type == 0) {
                    RightMenuChart1Type = 1;
                    CreateInboundCallsByHourBarChart(CallsOption);

                }
                else {
                    RightMenuChart1Type = 0;
                    CreateInboundCallsByHourLineChart(CallsOption);

                }

            });
        }
        function RegEventRightMenuButton2Click() {
          //  alert(RightMenuChart2Type);
            $("#btnOutboundCallsByHourChart").click(function () {            
                var CallsOption = $("#ddlCallsOption").val();
               
                if (RightMenuChart2Type == 0) {
                    RightMenuChart2Type = 1;
                    CreateOutboundCallsByHourBarChart(CallsOption);

                }
                else {
                    RightMenuChart2Type = 0;
                    CreateOutboundCallsByHourLineChart(CallsOption);

                }

            });
        }
        function RegEventRightMenuButton3Click() {
          //  alert(RightMenuChart3Type);
            $("#btnCallsLostByHourChart").click(function () {
                var CallsOption = $("#ddlCallsOption").val();
              
                if (RightMenuChart3Type == 0) {
                    RightMenuChart3Type = 1;
                    CreateCallsLostByHourBarChart(CallsOption);

                }
                else {
                    RightMenuChart3Type = 0;
                    CreateCallsLostByHourLineChart(CallsOption);

                }

            });
        }
        function RegEventRightMenuButton4Click() {
        //    alert(RightMenuChart4Type);
            $("#btnLongestRingTimeByHourChart").click(function () {            
                var CallsOption = $("#ddlCallsOption").val();
                
                if (RightMenuChart4Type == 0) {
                    RightMenuChart4Type = 1;
                    CreateLongestRingTimeByHourBarChart(CallsOption);

                }
                else {
                    RightMenuChart4Type = 0;
                    CreateLongestRingTimeByHourLineChart(CallsOption);

                }

            });
        }
        function DrawCenterMenuGraphs() {
            
            var Duration = $("#ddlGraphDuration").val();
            var CallsOption = $("#ddlCallsOption").val();

            CreateCallSummaryByDayChart(Duration, CallsOption);
            CreateOutBoundCallsByRegionChart(Duration, CallsOption);
            CreateCallsServicedByDayChart(Duration, CallsOption);
            GetCallSummaryByExtensionGraphReport(Duration, CallsOption);
            
            //3516CreateCostSummaryChart(Duration);
            CreateCostSummaryChartWithNoData();//Temporarily Created Null Chart
            
            clearTimeout(Timer1);
            Timer1=setTimeout(DrawCenterMenuGraphs, 60000);

        }
        function DrawRightMenuGraphs() {

            var CallsOption = $("#ddlCallsOption").val();

            CreateInboundCallsByHourLineChart(CallsOption);
            CreateOutboundCallsByHourLineChart(CallsOption);

            CreateCallsLostByHourLineChart(CallsOption);
            CreateLongestRingTimeByHourLineChart(CallsOption);

            clearTimeout(Timer2);
            Timer2 = setTimeout(DrawRightMenuGraphs, 60000);
        }
        function CreateCallSummaryByDayChart(Duration, CallsOption) {
            
            var Param = { "DateRangeOption": Duration, "CallsOption": CallsOption }
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetCallSummaryGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divCallSummaryByDayChart").empty();
                    if(data.d){
                        var BarChart_CallSummaryByDay = AmCharts.makeChart("divCallSummaryByDayChart", {
                            "type": "serial",
                            "theme": "light",
                            "autoMargins": false,
                            "marginTop": 0,
                            "marginBottom": 5,
                            "marginLeft": 5,
                            "marginRight": 0,
                            "pullOutRadius": 0,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            "dataProvider": data.d,
                            "categoryField": "Date",
                            "legend": {
                                "divId": "LegendCallSummaryByDayChart",
                                "useGraphSettings": true,
                                "markerSize": 5,
                            },
                            "categoryAxis": {
                                "labelsEnabled": false,
                            },
                            "valueAxes": [{
                                "stackType": "regular",
                                "axisAlpha": 0,
                                "gridAlpha": 0.3,
                                "integersOnly": true

                            }],
                            "graphs": [{
                                "id": "g1",
                                "type": "column",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                                "bullet": "none",
                                "bulletBorderAlpha": 1,
                                "fillColors": '#f91627',
                                "lineColor": '#f91627',
                                "bulletColor": "#FFFFFF",
                                "hideBulletsCount": 50,
                                "title": "Inbound Calls",
                                "valueField": "IncomingCalls",
                                "useLineColorForBulletBorder": true,
                                "fillAlphas": 1,
                            }, {
                                "id": "g2",
                                "type": "column",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",

                                "bullet": "none",
                                "bulletBorderAlpha": 1,
                                "fillColors": '#6a6a6a',
                                "lineColor": '#6a6a6a',
                                "bulletColor": "#FFFFFF",
                                "hideBulletsCount": 50,
                                "title": "Outbound Calls",
                                "valueField": "OutgoingCalls",
                                "useLineColorForBulletBorder": true,
                                "fillAlphas": 1,
                            }],
                            //"chartScrollbar": {
                            //    "autoGridCount": false,
                            //    "graph": "g1",
                            //    "scrollbarHeight": 1,
                            //    "oppositeAxis": false,
                            //},
                            //"chartCursor": {
                            //    "limitToGraph": "g1",
                            //    "valueLineAlpha": 1,
                            //},
                        });
                        checkEmptyDataBarChart(BarChart_CallSummaryByDay);
                    } else{

                    }
                }
            });
        }
        function CreateOutBoundCallsByRegionChart(Duration, CallsOption) {
          //  alert(1);
            var Param = { "DateRangeOption": Duration, "CallsOption": CallsOption }
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetOutboundCallsByRegionGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
               //     alert(data.d);
                    $("#divOutBoundCallsByRegionChart").empty();
                    if (data.d) {
                        var PieChart_OutBoundCallsByRegion = AmCharts.makeChart("divOutBoundCallsByRegionChart", {
                            "type": "pie",
                            "theme": "light",
                            "labelsEnabled": false,
                            "autoMargins": false,
                            "marginTop": 5,
                            "marginBottom": 0,
                            "marginLeft": 0,
                            "marginRight": 0,
                            "pullOutRadius": 0,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            //"legend": {
                            //    "divId": "LegendOutBoundCallsByRegionChart",
                            //    "markerSize": 2,
                            //    "valueText": '',
                            //    "verticalGap": 0,

                            //},
                            "dataProvider": data.d,
                            "titleField": "Region",
                            "valueField": "TotalCalls",
                            "colorField": "color",


                        });
                   //     alert(data.d);
                        checkEmptyDataPieChart(PieChart_OutBoundCallsByRegion);
                    }

                }

            });
        }
        function CreateCallsServicedByDayChart(Duration, CallsOption) {
            
            var Param = { "DateRangeOption": Duration, "CallsOption": CallsOption }
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetCallsServicedByDayGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divCallsServicedByDayChart").empty();
                    if(data.d){
                        var BarChart_CallsServicedByDay = AmCharts.makeChart("divCallsServicedByDayChart", {
                            "type": "serial",
                            "theme": "light",
                            "labelsEnabled": false,
                            "autoMargins": false,
                            "marginTop": 5,
                            "marginBottom": 0,
                            "marginLeft": 5,
                            "marginRight": 5,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            "dataProvider": data.d,
                            "legend": {
                                "divId": "LegendCallsServicedByDayChart",
                                "useGraphSettings": true,
                                "markerSize": 2,
                                "valueText": '',
                                "verticalGap": 0,

                            },
                            "categoryField": "Date",
                            "categoryAxis": {
                                "labelsEnabled": false,
                            },
                            "valueAxes": [{
                                "stackType": "regular",
                                "axisAlpha": 0,
                                "gridAlpha": 0.3,
                                "integersOnly": true
                            }],
                            "graphs": [{
                                "id": "CallsServicedByDayChart1",
                                "type": "column",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",

                                "bullet": "none",
                                "bulletBorderAlpha": 1,
                                "fillColors": '#f91627',
                                "lineColor": '#f91627',
                                "bulletColor": "#FFFFFF",
                                "hideBulletsCount": 50,
                                "title": "Abandoned",
                                "valueField": "LostCalls",
                                "useLineColorForBulletBorder": true,
                                "fillAlphas": 1,

                            }, {
                                "id": "CallsServicedByDayChart2",
                                "type": "column",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",

                                "bullet": "none",
                                "bulletBorderAlpha": 1,
                                "fillColors": '#6a6a6a',
                                "lineColor": '#6a6a6a',
                                "bulletColor": "#FFFFFF",
                                "hideBulletsCount": 50,
                                "title": "Answered",
                                "valueField": "AnsweredCalls",
                                "useLineColorForBulletBorder": true,
                                "fillAlphas": 1,

                            }],
                            //"chartScrollbar": {
                            //    "autoGridCount": false,
                            //    "graph": "g5",
                            //    "scrollbarHeight": 1,
                            //    "oppositeAxis": false,
                            //},
                            //"chartCursor": {
                            //    "limitToGraph": "g1",
                            //    "valueLineAlpha": 1,
                            //},
                        });

                        //  if (data.d.length == 0) {
                        checkEmptyDataBarChart(BarChart_CallsServicedByDay);
                    } else{

                    }
                }
                //}
            });


        }
        function CreateCostSummaryChart(Duration) {
            
            var Param = { "DateRangeOption": Duration }
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetCostSummaryGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divCostSummaryChart").html('');
                    if(data.d){
                        var PieChart_CostSummary = AmCharts.makeChart("divCostSummaryChart", {
                            "type": "pie",
                            "theme": "light",
                            "labelsEnabled": false,
                            "autoMargins": false,
                            "marginTop": 5,
                            "marginBottom": 0,
                            "marginLeft": 0,
                            "marginRight": 0,
                            "pullOutRadius": 0,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            //"legend": {
                            //    "divId": "LegendCostSummaryChart",
                            //    "markerSize": 1,
                            //    "valueText": '',
                            //    "verticalGap": 0,
                            //    "autoMargins": false,

                            //},
                            "dataProvider": data.d,
                            "titleField": "CostType",
                            "valueField": "Cost",

                            "colorField": "color",
                            valueAxesSettings: {
                                unit: "$",
                                unitPosition: "right"
                            }
                        });
                        checkEmptyDataPieChart(PieChart_CostSummary);
                    } else{
                        
                        
                    }
                    
                }
            });
        }
        function CreateCostSummaryChartWithNoData(){
            var PieChart_CostSummary = AmCharts.makeChart("divCostSummaryChart", {
                "type": "pie",
                "theme": "light",
                "labelsEnabled": false,
                "autoMargins": false,
                "marginTop": 5,
                "marginBottom": 0,
                "marginLeft": 0,
                "marginRight": 0,
                "pullOutRadius": 0,
                "mouseWheelZoomEnabled": false,
                "mouseWheelScrollEnabled": true,
                //"legend": {
                //    "divId": "LegendCostSummaryChart",
                //    "markerSize": 1,
                //    "valueText": '',
                //    "verticalGap": 0,
                //    "autoMargins": false,

                //},
                "dataProvider": [],
                "titleField": "CostType",
                "valueField": "Cost",

                "colorField": "color",
                valueAxesSettings: {
                    unit: "$",
                    unitPosition: "right"
                }
            });
            checkEmptyDataPieChart(PieChart_CostSummary);
        }
        //right menu
        function CreateInboundCallsByHourLineChart(CallsOption) {
            
            var Param = { "CallsOption": CallsOption }
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetInboundCallsByHourGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divInboundCallsByHourChart").empty();
                    if(data.d)
                    {
                        if (data.d.length == 0) {
                          //  $('#btnInboundCallsByHourChart').unbind("click");
                            $("#spanInboundCallsByHourChartMaxVal").text(" 0 ");
                            $("#spanInboundCallsByHourChartMinVal").text(" 0 ");
                        }
                        else {
                            var maxval = Math.max.apply(Math, data.d.map(function (o) { return o.Calls; }));
                            var minval = Math.min.apply(Math, data.d.map(function (o) { return o.Calls; }));

                            $("#spanInboundCallsByHourChartMaxVal").text(maxval);
                            $("#spanInboundCallsByHourChartMinVal").text(minval);
                        }
                        var LineChart_InboundCallsByHour = AmCharts.makeChart("divInboundCallsByHourChart", {
                            "type": "serial",
                            "theme": "light",
                            "marginTop": 5,
                            "marginBottom": 5,
                            "marginLeft": 5,
                            "marginRight": 5,
                            "dataProvider": data.d,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            "valueAxes": [{
                                "axisAlpha": 0,
                                "position": "left",
                                "integersOnly": true
                            }],

                            "graphs": [{

                                "id": "LineChart_InboundCallsByHour",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                                "bullet": "round",
                                "bulletSize": 8,
                                "lineColor": "#f91627",
                                "lineThickness": 2,
                                "negativeLineColor": "#637bb6",
                                "type": "smoothedLine",
                                "title": "Calls",
                                "valueField": "Calls"
                            }],
                            "categoryField": "Hour",
                            "categoryAxis": {
                                "labelsEnabled": false,
                                "minorGridAlpha": 0.1,
                                "minorGridEnabled": true,
                            },


                        });
                        checkEmptyDataBarChart(LineChart_InboundCallsByHour);
                    }
                    else
                    {

                    }
                }
            });
        }
        function CreateOutboundCallsByHourLineChart(CallsOption) {
            
            var Param = { "CallsOption": CallsOption }
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetOutboundCallsByHourGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divOutboundCallsByHourChart").empty();
                    if(data.d){
                        if (data.d.length == 0) {
                        //    $('#btnOutboundCallsByHourChart').unbind("click");
                            $("#spanOutboundCallsByHourChartMaxVal").text(" 0 ");
                            $("#spanOutboundCallsByHourChartMinVal").text(" 0 ");
                        }
                        else {
                            var maxval = Math.max.apply(Math, data.d.map(function (o) { return o.Calls; }));
                            var minval = Math.min.apply(Math, data.d.map(function (o) { return o.Calls; }));


                            $("#spanOutboundCallsByHourChartMaxVal").text(maxval);
                            $("#spanOutboundCallsByHourChartMinVal").text(minval);
                        }
                        var LineChart_OutboundCallsByHour = AmCharts.makeChart("divOutboundCallsByHourChart", {
                            "type": "serial",
                            "theme": "light",
                            "marginTop": 5,
                            "marginBottom": 5,
                            "marginLeft": 5,
                            "marginRight": 5,
                            "dataProvider": data.d,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            "valueAxes": [{
                                "axisAlpha": 0,
                                "position": "left",
                                "integersOnly": true
                            }],

                            "graphs": [{

                                "id": "LineChart_OutboundCallsByHour",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                                "bullet": "round",
                                "bulletSize": 8,
                                "lineColor": "#5b595a",
                                "lineThickness": 2,
                                "negativeLineColor": "#5b595a",
                                "type": "smoothedLine",
                                "title": "Calls",
                                "valueField": "Calls"
                            }],
                            "categoryField": "Hour",
                            "categoryAxis": {
                                "labelsEnabled": false,
                                "minorGridAlpha": 0.1,
                                "minorGridEnabled": true,
                            },


                        });
                        checkEmptyDataBarChart(LineChart_OutboundCallsByHour);}
                    else
                    {

                    }
                }
            });
        }
        function CreateCallsLostByHourLineChart(CallsOption) {
            
            var Param = { "CallsOption": CallsOption }
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetCallsLostByHourGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divCallsLostByHourChart").empty();
                    if(data.d)
                    {
                        if (data.d.length == 0) {
                        //    $('#btnCallsLostByHourChart').unbind("click");
                            $("#spanCallsLostByHourChartMaxVal").text(" 0 ");
                            $("#spanCallsLostByHourChartMinVal").text(" 0 ");
                        }
                        else {
                            var maxval = Math.max.apply(Math, data.d.map(function (o) { return o.Calls; }));
                            var minval = Math.min.apply(Math, data.d.map(function (o) { return o.Calls; }));

                            $("#spanCallsLostByHourChartMaxVal").text(maxval);
                            $("#spanCallsLostByHourChartMinVal").text(minval);
                        }
                        var LineChart_CallsLostByHour = AmCharts.makeChart("divCallsLostByHourChart", {
                            "type": "serial",
                            "theme": "light",
                            "marginTop": 5,
                            "marginBottom": 5,
                            "marginLeft": 5,
                            "marginRight": 5,
                            "dataProvider": data.d,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            "valueAxes": [{
                                "axisAlpha": 0,
                                "position": "left",
                                "integersOnly": true
                            }],

                            "graphs": [{

                                "id": "LineChart_CallsLostByHour",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                                "bullet": "round",
                                "bulletSize": 8,
                                "lineColor": "#f91627",
                                "lineThickness": 2,
                                "negativeLineColor": "#637bb6",
                                "type": "smoothedLine",
                                "title": "Calls",
                                "valueField": "Calls"
                            }],
                            "categoryField": "Hour",
                            "categoryAxis": {
                                "labelsEnabled": false,
                                "minorGridAlpha": 0.1,
                                "minorGridEnabled": true,
                            },


                        });
                        checkEmptyDataBarChart(LineChart_CallsLostByHour);
                    }
                    else
                    {

                    }
                }
            });
        }
        function CreateLongestRingTimeByHourLineChart(CallsOption) {
            
            var Param = { "CallsOption": CallsOption }
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetLongestRingTimeByHourGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divLongestRingTimeByHourChart").empty();
                    if(data.d){
                        if (data.d.length == 0) {
                        //    $('#btnLongestRingTimeByHourChart').unbind("click");
                            $("#spanLongestRingTimeByHourChartMaxVal").text(" 0 ");
                            $("#spanLongestRingTimeByHourChartMinVal").text(" 0 ");
                        }
                        else {
                            var maxval = Math.max.apply(Math, data.d.map(function (o) { return o.MaxRing; }));
                            var minval = Math.min.apply(Math, data.d.map(function (o) { return o.MaxRing; }));

                            $("#spanLongestRingTimeByHourChartMaxVal").text(maxval);
                            $("#spanLongestRingTimeByHourChartMinVal").text(minval);
                        }

                        var LineChart_LongestRingTimeByHour = AmCharts.makeChart("divLongestRingTimeByHourChart", {
                            "type": "serial",
                            "theme": "light",
                            "marginTop": 5,
                            "marginBottom": 5,
                            "marginLeft": 5,
                            "marginRight": 5,
                            "dataProvider": data.d,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            "valueAxes": [{
                                "axisAlpha": 0,
                                "position": "left",
                                "integersOnly": true
                            }],

                            "graphs": [{

                                "id": "LineChart_LongestRingTimeByHour",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                                "bullet": "round",
                                "bulletSize": 8,
                                "lineColor": "#5b595a",
                                "lineThickness": 2,
                                "negativeLineColor": "#5b595a",
                                "type": "smoothedLine",
                                "title": "Max Ring",
                                "valueField": "MaxRing"
                            }],
                            "categoryField": "Hour",
                            "categoryAxis": {
                                "labelsEnabled": false,
                                "minorGridAlpha": 0.1,
                                "minorGridEnabled": true,
                            },


                        });
                        checkEmptyDataBarChart(LineChart_LongestRingTimeByHour);}
                    else
                    {

                    }
                }
            });
        }
        function CreateInboundCallsByHourBarChart(CallsOption) {
            var Param = { "CallsOption": CallsOption }
            
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetInboundCallsByHourGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divInboundCallsByHourChart").empty();
                    if(data.d){
                        var BarChart_InboundCallsByHour = AmCharts.makeChart("divInboundCallsByHourChart", {
                            "type": "serial",
                            "theme": "light",
                            "marginTop": 5,
                            "marginBottom": 5,
                            "marginLeft": 5,
                            "marginRight": 5,
                            "dataProvider": data.d,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            "valueAxes": [{
                                "stackType": "regular",
                                "axisAlpha": 0,
                                "gridAlpha": 0.3,
                                "position": "left",
                                "integersOnly": true
                            }],

                            "graphs": [{

                                "id": "BarChart_InboundCallsByHour",
                                "type": "column",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                                "bullet": "none",
                                "fillColors": '#f91627',
                                "lineColor": '#f91627',
                                "title": "Calls",
                                "valueField": "Calls",
                                "useLineColorForBulletBorder": true,
                                "fillAlphas": 1,
                            }],
                            "categoryField": "Hour",
                            "categoryAxis": {
                                "labelsEnabled": false,

                            },


                        });
                    }
                    else{
                      //  alert(CallsOption + ":" + RightMenuChart1Type);
                    }
                     checkEmptyDataBarChart(BarChart_InboundCallsByHour);

                }
            });
        }
        function CreateOutboundCallsByHourBarChart(CallsOption) {
            var Param = { "CallsOption": CallsOption }
            
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetOutboundCallsByHourGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divOutboundCallsByHourChart").empty();
                    if (data.d) {
                        var BarChart_OutboundCallsByHour = AmCharts.makeChart("divOutboundCallsByHourChart", {
                            "type": "serial",
                            "theme": "light",
                            "marginTop": 5,
                            "marginBottom": 5,
                            "marginLeft": 5,
                            "marginRight": 5,
                            "dataProvider": data.d,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            "valueAxes": [{
                                "axisAlpha": 0,
                                "position": "left",
                                "integersOnly": true
                            }],
                            "graphs": [{

                                "id": "BarChart_OutboundCallsByHour",
                                "type": "column",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                                "bullet": "none",
                                "fillColors": '#5b595a',
                                "lineColor": '#5b595a',
                                "title": "Calls",
                                "valueField": "Calls",
                                "useLineColorForBulletBorder": true,
                                "fillAlphas": 1,
                            }],
                            "categoryField": "Hour",
                            "categoryAxis": {
                                "labelsEnabled": false,
                                "minorGridAlpha": 0.1,
                                "minorGridEnabled": true,
                            },


                        });
                        checkEmptyDataBarChart(BarChart_OutboundCallsByHour);
                    }
                    else{

                    }
                }
            });
        }
        function CreateCallsLostByHourBarChart(CallsOption) {
            var Param = { "CallsOption": CallsOption }
            
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetCallsLostByHourGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divCallsLostByHourChart").empty();
                    if (data.d) {
                        var BarChart_CallsLostByHour = AmCharts.makeChart("divCallsLostByHourChart", {
                            "type": "serial",
                            "theme": "light",
                            "marginTop": 5,
                            "marginBottom": 5,
                            "marginLeft": 5,
                            "marginRight": 5,
                            "dataProvider": data.d,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            "valueAxes": [{
                                "axisAlpha": 0,
                                "position": "left",
                                "integersOnly": true
                            }],
                            "graphs": [{

                                "id": "BarChart_CallsLostByHour",
                                "type": "column",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                                "bullet": "none",
                                "fillColors": '#f91627',
                                "lineColor": '#f91627',
                                "title": "Calls",
                                "valueField": "Calls",
                                "useLineColorForBulletBorder": true,
                                "fillAlphas": 1,
                            }],
                            "categoryField": "Hour",
                            "categoryAxis": {
                                "labelsEnabled": false,
                                "minorGridAlpha": 0.1,
                                "minorGridEnabled": true,
                            },


                        });
                        checkEmptyDataBarChart(BarChart_CallsLostByHour);
                    }
                    else{

                    }
                }
            });
        }
        function CreateLongestRingTimeByHourBarChart(CallsOption) {
            var Param = { "CallsOption": CallsOption }
            
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetLongestRingTimeByHourGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#divLongestRingTimeByHourChart").empty();
                    if (data.d) {
                        var BarChart_LongestRingTimeByHour = AmCharts.makeChart("divLongestRingTimeByHourChart", {
                            "type": "serial",
                            "theme": "light",
                            "marginTop": 5,
                            "marginBottom": 5,
                            "marginLeft": 5,
                            "marginRight": 5,
                            "dataProvider": data.d,
                            "mouseWheelZoomEnabled": false,
                            "mouseWheelScrollEnabled": true,
                            "valueAxes": [{
                                "axisAlpha": 0,
                                "position": "left",
                                "integersOnly": true
                            }],
                            "graphs": [{

                                "id": "BarChart_LongestRingTimeByHour",
                                "type": "column",
                                "balloonText": "<b>[[title]]</b><br><span style='font-size:14px'>[[category]]: <b>[[value]]</b></span>",
                                "bullet": "none",
                                "fillColors": '#5b595a',
                                "lineColor": '#5b595a',
                                "title": "Max Ring",
                                "valueField": "MaxRing",
                                "useLineColorForBulletBorder": true,
                                "fillAlphas": 1,
                            }],
                            "categoryField": "Hour",
                            "categoryAxis": {
                                "labelsEnabled": false,
                                "minorGridAlpha": 0.1,
                                "minorGridEnabled": true,
                            },


                        });
                        checkEmptyDataBarChart(BarChart_LongestRingTimeByHour);
                    }
                    else{

                    }
                }
            });
        }
        function GetCallSummaryByExtensionGraphReport(Duration, CallsOption) {

            //$("#tblCallSummaryByExtension").empty();
            var Param = {"DateRangeOption": Duration, "CallsOption": CallsOption }
            $.ajax({
                type: "POST",
                url: "Dashboard.aspx/GetCallSummaryByExtensionGraphReport",
                data: JSON.stringify(Param),
                //3516async: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $('#tblCallSummaryByExtension').empty();
                    var thHTML = '<thead><tr><th>Extension</th><th class="extension-name">Name</th><th>Inbound Calls</th><th>Duration</th><th>Outbound Calls</th><th>Duration</th><th style="white-space: nowrap;">Total Calls</th><th> </th><th>Total Duration</th></tr></thead><tbody></tbody>';
                    $('#tblCallSummaryByExtension').append(thHTML);
                    
                    if(data.d){
                        
                        if (data.d.length > 0) {
                            for (j = 0; j < data.d.length; j++) {
                                var trHTML = '';
                                trHTML += '<tr><td>' + data.d[j].Extension + '</td>';
                                trHTML += '<td  class="extension-name">' + data.d[j].Name + '</td>';
                                trHTML += '<td>' + data.d[j].IncomingCalls + '</td>';
                                trHTML += '<td>' + data.d[j].IncomingCallDuration + '</td>';
                                trHTML += '<td>' + data.d[j].OutgoingCalls + '</td>';
                                trHTML += '<td>' + data.d[j].OutgoingCallDuration + '</td>';
                                trHTML += '<td>' + data.d[j].TotalCalls + '</td>';
                                trHTML += '<td></td>';
                                trHTML += '<td>' + data.d[j].TotalDuration + '</td></tr>';
                                $('#tblCallSummaryByExtension').append(trHTML);
                            }
                        }
                        var height = window.innerHeight;
                        var pageLength = 20;
                        if (height < 800){
                            pageLength = 5;
                        }else if (height < 980){
                            pageLength = 10;
                        }else if (height < 1080){
                            pageLength = 13;
                        }else if (height < 1180){
                            pageLength = 20;
                        }
                        //console.log("pageLength height : "+ height +" "+pageLength);
                        $("#tblCallSummaryByExtension").show();
                         pageLength = 30;
                        if ($("#btnShowHideCallSummary").hasClass("fa-angle-up")) {
                            pageLength = 10;
                        }
                        if (localStorage.PageSize != undefined && localStorage.PageSize > 0) {
                            pageLength = localStorage.PageSize;
                        }
                       
                        $('#tblCallSummaryByExtension').dataTable({
                            "pagingType": "full_numbers",
                            destroy: true,
                            "bLengthChange": true,
                            "bPaginate": true,
                            "bFilter": false,
                            "aaSorting": [],
                            "pageLength": pageLength,
                            "info": false,
                            "language": {
                                "lengthMenu": "Rows Per Page _MENU_"
                            },
                            lengthMenu: [[5, 10, 15, 20, 25, 30, 35, 40, 45, 50], [5, 10, 15, 20, 25, 30, 35, 40, 45, 50]],
                            dom: '<"float-left"B><"float-right"f>rt<"row table-options"<"col-sm-4 table-pager"l><"col-sm-4 w-0"i><"col-sm-4"p>>',

                        });
                        $('#tblCallSummaryByExtension_paginate').parent().addClass("col-md-12").removeClass("col-md-7");
                    }
                    else
                    {

                    }
                }
            });
        }

        $(document).on('change','select[name=tblCallSummaryByExtension_length]', function () {
           
            localStorage.PageSize = $(this).val();
        });

        function checkEmptyDataBarChart(chart) {
            if (chart.dataProvider === undefined || chart.dataProvider.length == 0) {
                // chart.clear();
                //disable angle down buttons

                // set min/max on the value axis
                chart.valueAxes[0].minimum = 0;
                chart.valueAxes[0].maximum = 100;

                // add dummy data point
                var chartData =
                // Data set #1
                [
                    { IncomingCalls: 0, OutgoingCalls: 0 },
                ];
                chart.dataProvider = chartData//[dataPoint];

                //alert(dataPoint);
                // add label
                chart.addLabel(0, '50%', 'The chart contains no data ', 'center', 12, "black");

                // set opacity of the chart div
                chart.chartDiv.style.opacity = 0.9;
                chart.alpha = 0.2;

                // redraw it
                chart.validateData();
                //chart.validateNow();

            }
        }
        function checkEmptyDataPieChart(chart) {

            if (chart.dataProvider === undefined || chart.dataProvider.length === 0) {


                //  chart.clear();
                chart.balloon.enabled = false;
                // add some bogus data
                var dp = {};
                dp[chart.titleField] = "";
                dp[chart.valueField] = 1;
                dp[chart.colorField] = '#000000';



                chart.dataProvider.push(dp)

                var dp = {};
                dp[chart.titleField] = "";
                dp[chart.valueField] = 1;
                dp[chart.colorField] = "#57565c";

                chart.dataProvider.push(dp)

                var dp = {};
                dp[chart.titleField] = "";
                dp[chart.valueField] = 1;
                dp[chart.colorField] = "#f91627";
                chart.dataProvider.push(dp)

                // disable slice labels
                chart.labelsEnabled = false;

                // add label to let users know the chart is empty
                chart.addLabel("50%", "50%", "The chart contains no data", "middle", 12, "#000000");

                // dim the whole chart
                chart.chartDiv.style.opacity = 0.9;
                chart.alpha = 0.2;
                chart.validateData();

            }
        }
        function ClearReportFilters() {
            $("#lstDateRangeOption").val(1);
            $("#lstCallsOption").val(2);
            DatePickersDefaultSet();
            $("#CustomTimePickerRow").show();
            $("#CustomDatePickerRow").hide();

            $("#lstAgents").multiselect('selectAll', false);
            $("#lstExtensions").multiselect('selectAll', false);
            $("#lstGroups").multiselect('selectAll', false);
            $("#lstDDIs").multiselect('selectAll', false);
            $("#lstUsers").multiselect('selectAll', false);
            $("#lstWeekDays").multiselect('selectAll', false);

            $("#lstAgents").multiselect("refresh");
            $("#lstExtensions").multiselect("refresh");
            $("#lstGroups").multiselect("refresh");
            $("#lstDDIs").multiselect("refresh");
            $("#lstUsers").multiselect("refresh");
            $("#lstWeekDays").multiselect("refresh");
            $("#ReportFilter").modal("hide");
        }
    </script>

</asp:Content>
