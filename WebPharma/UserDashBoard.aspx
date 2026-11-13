<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserDashBoard.aspx.cs" Inherits="UserDashBoard" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">

    <title>In Style :: Dash Board</title>
    <script src="js/jquery.min.js"></script>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/General.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <link href="DashBoardJs/JsDash/jquery-ui.css" rel="stylesheet" />
    <link href="DashBoardJs/JsDash/TableDesign.css" rel="stylesheet" />


    <script type="text/javascript">
        function getMonday(d, vid) {
            var day = d.getDay(),
            diff = d.getDate() - day + (day == 0 ? -6 : vid);
            return new Date(d.setDate(diff));
        }
        /********Customere****/
        function CustomerBar(type) {
            if ($("#divCustomer").length > 0) {
                var customer = eval($("#hdfCustomer").val());
                var dt = $("#hdfPeriod").val();

                if (type == "bar") {

                    var chart = new Highcharts.Chart({
                        chart: {
                            type: 'column',
                            renderTo: 'divCustomer'

                        },
                        title: {
                            text: 'Customer Performance'
                        },
                        subtitle: {
                            text: dt
                        },
                        credits: {
                            enabled: false
                        },
                        plotOptions: {
                            column: {

                                colorByPoint: true

                            },
                            series: {
                                cursor: 'pointer',
                                point: {
                                    events: {
                                        click: function () {

                                            window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Customer=cust";

                                        }
                                    }
                                }
                            }
                        },
                        xAxis: {
                            type: 'category',
                            labels: {
                                rotation: -45,
                                align: 'right',
                                style: {
                                    fontSize: '13px',
                                    fontFamily: 'Verdana, sans-serif'
                                }
                            }
                        },
                        yAxis: {
                            min: 0,
                            title: {
                                text: 'Amount of sale %'
                            }
                        },
                        legend: {
                            enabled: false
                        },
                        tooltip: {
                            pointFormat: '{series.name}%: <b>{point.y:.1f}%</b><br/>Amount:<b>{point.amount}</b>'
                        },
                        series: [{
                            name: 'customer',
                            data: customer,
                            dataLabels: {
                                enabled: true,
                                rotation: -90,
                                color: '#fcfdd6',
                                align: 'right',
                                x: 4,
                                y: 10,
                                style: {
                                    fontSize: '13px',
                                    fontFamily: 'Verdana, sans-serif',
                                    textShadow: '0 0 3px black'
                                }
                            }
                        }]
                    });
                }
                else {
                    var chart = new Highcharts.Chart({
                        chart: {
                            type: 'pie',
                            renderTo: 'divCustomer',
                            options3d: {
                                enabled: true,
                                alpha: 45
                            }
                        },
                        title: {
                            text: 'Customer Performance'
                        },
                        subtitle: {
                            text: dt
                        },
                        credits: {
                            enabled: false
                        },
                        tooltip: {
                            pointFormat: '{series.name}%: <b>{point.y:.1f}%</b><br/>Amount:<b>{point.amount}</b>'
                        },
                        plotOptions: {
                            pie: {
                                innerSize: 100,
                                depth: 45,

                                cursor: 'pointer',
                                point: {
                                    events: {
                                        click: function () {

                                            window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Customer=Cus";
                                        }
                                    }
                                }

                            }

                        },
                        series: [{
                            name: 'Total Sale',
                            data: customer
                        }]
                    });

                }
            }

        }
        /********Sales****/
        function SalesBarOrPie(type) {
            if ($("#divSale").length > 0) {


                var eventsInline = eval($("#hdfSale").val());
                var dt = $("#hdfPeriod").val();


                if (type == 'pie') {
                    var chart = new Highcharts.Chart({
                        chart: {
                            type: 'pie',
                            renderTo: "divSale",
                            options3d: {
                                enabled: true,
                                alpha: 45,
                                beta: 0
                            }
                        },
                        title: {
                            text: 'Store Wise Sales'
                        },
                        subtitle: {
                            text: dt
                        },
                        credits: {
                            enabled: false
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.y:.2f}%</b><br/>Amount :<b>{point.amount:.2f} '
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                depth: 35,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.name}'
                                },
                                point: {
                                    events: {
                                        click: function () {
                                            window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Store=Store";
                                        }
                                    }
                                }
                            }


                        },
                        series: [{
                            type: 'pie',
                            name: 'Store Sale',
                            data: eventsInline
                        }]
                    });
                }
                else {
                    var chart = new Highcharts.Chart({
                        chart: {
                            renderTo: "divSale",
                            type: 'column'
                        },
                        title: {
                            text: 'Store Wise Sales'
                        },
                        subtitle: {
                            text: dt
                        },
                        credits: {
                            enabled: false
                        },
                        plotOptions: {
                            column: {

                                colorByPoint: true
                            },
                            series: {
                                cursor: 'pointer',
                                point: {
                                    events: {
                                        click: function () {
                                            window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Store=Store";
                                        }
                                    }
                                }
                            }
                        },
                        xAxis: {
                            type: 'category',
                            labels: {
                                rotation: -45,
                                align: 'right',
                                style: {
                                    fontSize: '13px',
                                    fontFamily: 'Verdana, sans-serif'
                                }
                            }
                        },
                        yAxis: {
                            min: 0,
                            title: {
                                text: 'Amount of sales %'
                            }
                        },
                        legend: {
                            enabled: false
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.y:.2f}%</b><br/>Amount :<b>{point.amount:.2f} '
                        },
                        series: [{
                            name: 'Store Sale',
                            data: eventsInline,
                            dataLabels: {
                                enabled: true,
                                rotation: -90,
                                color: '#fcfdd6',
                                align: 'right',
                                x: 4,
                                y: 10,
                                style: {
                                    fontSize: '13px',
                                    fontFamily: 'Verdana, sans-serif',
                                    textShadow: '0 0 3px black'
                                }
                            }
                        }]
                    });

                }
            }

        }

        /********Items****/
        function ItemBarOrPie(type) {
            if ($("#divItem").length > 0) {
                var dt = $("#hdfPeriod").val();
                var items = eval($("#hdfCounter").val());
                if (type == 'pie') {

                    var chart = new Highcharts.Chart({
                        chart: {
                            type: 'pie',
                            renderTo: "divItem",
                            options3d: {
                                enabled: true,
                                alpha: 45,
                                beta: 0
                            }
                        },
                        title: {
                            text: 'Top Selling Items'
                        },
                        subtitle: {
                            text: dt
                        },
                        credits: {
                            enabled: false
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.y:.1f}</b>'
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                depth: 35,
                                dataLabels: {
                                    enabled: true,
                                    format: '{point.name}'
                                },
                                point: {
                                    events: {
                                        click: function () {
                                            window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&item=Item";

                                        }
                                    }
                                }
                            }


                        },
                        series: [{
                            type: 'pie',
                            name: 'Item',
                            data: items
                        }]
                    });
                }
                else {

                    var chart = new Highcharts.Chart({
                        chart: {
                            renderTo: "divItem",
                            type: 'column'

                        },
                        title: {
                            text: 'Top Selling Item '
                        },
                        subtitle: {
                            text: dt
                        },
                        credits: {
                            enabled: false
                        },
                        plotOptions: {
                            column: {
                                colorByPoint: true
                            },
                            series: {
                                cursor: 'pointer',
                                point: {
                                    events: {
                                        click: function () {
                                            window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&item=Item";
                                        }
                                    }
                                }
                            }
                        },
                        xAxis: {
                            type: 'category',
                            labels: {
                                rotation: -45,
                                align: 'right',
                                style: {
                                    fontSize: '13px',
                                    fontFamily: 'Verdana, sans-serif'
                                }
                            }
                        },
                        yAxis: {
                            min: 0,
                            title: {
                                text: 'No of sale'
                            }
                        },
                        legend: {
                            enabled: false
                        },
                        tooltip: {
                            pointFormat: 'Total Sale:<b>{point.y:.1f}</b>'
                        },
                        series: [{
                            name: 'Items',
                            data: items,
                            dataLabels: {
                                enabled: true,
                                rotation: -90,
                                color: '#fcfdd6',
                                align: 'right',
                                x: 4,
                                y: 10,
                                style: {
                                    fontSize: '13px',
                                    fontFamily: 'Verdana, sans-serif',
                                    textShadow: '0 0 3px black'
                                }
                            }
                        }]
                    });

                }
            }
        }

        function MinStockBarorpie(type) {
            if ($("#divMinStock").length > 0) {
                var items = eval($("#hdfMinStock").val());
                if (type == 'bar') {

                    $("#divRepeater").attr('style', 'display:none;height: 297px; width:  410px;');
                    $("#divMinStock").attr('style', 'display:block;height: 297px; width: 410px;');
                    var chart = new Highcharts.Chart({
                        chart: {
                            renderTo: 'divMinStock',
                            type: 'column'
                        },
                        title: {
                            text: 'Minimum stock Variation'
                        },
                        subtitle: {
                            text: 'Items'
                        },
                        credits: {
                            enabled: false
                        },
                        xAxis: {
                            type: 'category',
                            labels: {
                                rotation: -45,
                                align: 'right',
                                style: {
                                    fontSize: '13px',
                                    fontFamily: 'Verdana, sans-serif'

                                }
                            }
                        },
                        yAxis: {
                            min: 0,
                            title: {
                                text: 'Stock Variance %'
                            }
                        },
                        legend: {
                            enabled: false
                        },
                        tooltip: {
                            pointFormat: '<b>{point.categories}</b><br/>Batch:{point.batch}<br/>SV:<b>{point.y:.1f}%</b><br/>ML:{point.Min}<br/>CS:{point.CS}'
                        },
                        plotOptions: {
                            column: {
                                color: '#FF0000'


                            },
                            series: {
                                cursor: 'pointer',
                                point: {
                                    events: {
                                        click: function () {
                                            window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Stock=stock";
                                        }
                                    }
                                }
                            }
                        },
                        series: [{
                            name: 'Items',
                            data: items,
                            dataLabels: {
                                enabled: true,
                                rotation: -90,
                                color: '#000000',
                                align: 'right',
                                x: 4,
                                y: 10,
                                style: {
                                    fontSize: '13px',
                                    fontFamily: 'Verdana, sans-serif',
                                    textShadow: '0 0 3px black'
                                }
                            }
                        }]
                    });
                }
                else {
                    //$("#divMinStock").highcharts().series[0].update({ type: 'line' });
                    //$("#divMinStock").highcharts().series[0].update({ color: "#FF0000" });
                    //$("#divMinStock").highcharts().series[0].graph.attr({ stroke: '#FF0000' });
                    $("#divMinStock").attr('style', 'display:none; height: 297px; width: 410px;');
                    $("#divRepeater").attr('style', 'display:block; height: 297px; width: 410px;');
                }
            }
        }

        function pageLoad() {










            /*********************Customer**********************/

            if ($("#txtDate").val() != "") {
                var dt = $("#hdfPeriod").val();
                $("#cbxBar1").click(function () {

                    CustomerBar('bar');
                    $('#cbxpie1').removeClass('ActiveChek');
                    $("#cbxBar1").addClass('ActiveChek');


                });



                $("#cbxpie1").click(function () {

                    CustomerBar('pie');
                    $('#cbxBar1').removeClass('ActiveChek');
                    $("#cbxpie1").addClass('ActiveChek');


                });
                CustomerBar('bar');
                /************************Item***************/


                $("#cbxBar2").click(function () {


                    //    $('#cbxpie2').attr('checked', false);
                    ItemBarOrPie('bar');

                    // $(this).css('ActiveChek');
                    $('#cbxpie2').removeClass('ActiveChek');
                    $("#cbxBar2").addClass('ActiveChek');



                });



                $("#cbxpie2").click(function () {



                    ItemBarOrPie('pie');
                    $('#cbxBar2').removeClass('ActiveChek');
                    $("#cbxpie2").addClass('ActiveChek');

                });

                ItemBarOrPie('bar');


                /*********************Sale**********************/
                $("#cbxBar3").click(function () {


                    SalesBarOrPie('bar');
                    $('#cbxPie3').removeClass('ActiveChek');
                    $("#cbxBar3").addClass('ActiveChek');

                });
                $("#cbxPie3").click(function () {


                    SalesBarOrPie('pie');
                    $('#cbxBar3').removeClass('ActiveChek');
                    $("#cbxPie3").addClass('ActiveChek');

                });
                SalesBarOrPie('pie');
                /**********************Min Stock*******************/
                $("#cbxBar4").click(function () {


                    MinStockBarorpie('bar');
                    $('#cbxPie4').removeClass('ActiveChek');
                    $("#cbxBar4").addClass('ActiveChek');

                });



                $("#cbxPie4").click(function () {


                    MinStockBarorpie('pie');
                    $('#cbxBar4').removeClass('ActiveChek');
                    $("#cbxPie4").addClass('ActiveChek');

                });
                MinStockBarorpie('bar');

                /*Customer*/



            }
            $(".column").sortable({
                connectWith: ".column",
                handle: ".portlet-header",
                cancel: ".portlet-toggle",
                placeholder: "portlet-placeholder ui-corner-all"
            });
            $(".portlet")
            .addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
            .find(".portlet-header")
            .addClass("ui-widget-header ui-corner-all")
            .prepend("<span class='ui-icon ui-icon-minusthick portlet-toggle'></span>");
            $(".portlet-toggle").click(function () {
                var icon = $(this);
                icon.toggleClass("ui-icon-minusthick ui-icon-plusthick");
                icon.closest(".portlet").find(".portlet-content").toggle();
            });
            $(".highcharts-background").css("opacity", "0.8");
            $('table tr:not(:first)').click(function () {

                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Stock=stock";
            });
            CustomDateSerarch();

        }
        //);


        $(document).ready(function () {

            ClearSearch();


        });
        function ClearSearch() {
            var dt = new Date().format('dd/MMM/yyyy');

            $("#txtDate").val(dt);

            $("#btnHidden").click();

        }
        function CustomDateSerarch() {
            var Period = $('#ddlType').find("option:selected").val();
            if (Period == '6') {

                $("#divdateWiseFilteraton").show();
            }
            else {
                $("#divdateWiseFilteraton").hide();
            }
        }

        function ClientValidate(source, arguments) {
            try {
                var date = document.getElementById("txtFromDate").value;
                var arr = date.split('/');
                var start = new Date(arr[2], arr[1], arr[0]);
                var arr2 = arguments.Value.split('/');

                var endDate = new Date(arr2[2], arr2[1], arr2[0]);

                if (start > endDate) {
                    $("#lblInfo").attr('style', 'display:block');
                    arguments.IsValid = false;
                }
                else {
                    $("#lblInfo").attr('style', 'display:none');
                    arguments.IsValid = true;
                }

            }
            catch (e) {
                alert(e);
            }

        }
    </script>
    <script src="js/jquery-1.10.2.js"></script>
    <script src="DashBoardJs/JsDash/jquery-ui.js"></script>
    <style>
        .column {
            padding-bottom: 0px;                       
        }
        .portlet {
            width: 415px;
            margin: 0px 0px 25px 0px;
            border-radius: 15px;
            background: rgba(255,255,255,0.5);
            -webkit-box-shadow: 4px -7px 32px -6px rgba(0,0,0,0.75);
            -moz-box-shadow: 4px -7px 32px -6px rgba(0,0,0,0.75);
            box-shadow: 4px -7px 32px -6px rgba(0,0,0,0.75);
        }
        .portlet-header {
            width: 415px;
            height: 25px;
            margin: 0px;
            padding: 0px;
            background:#12361D;
            margin-top: 0px;
            margin-left: 0px;
            font-size: 16px;
            color: #FFFFFF;
            text-align: center;
            font-weight: normal;
            position: relative;
            cursor: pointer;
        }
            .portlet-header:active {
                cursor: all-scroll;
            }

        .portlet-toggle {
            top: 50%;
            right: 0;
            margin-top: 10px;
            margin-right: 20px;
            float: right;
        }
        .portlet-content {
            margin: 0px;
            padding: 0px;
            margin-top: -1px;
            margin-left: 0px;
            float: left;
            width: 100%;
        }

        .portlet-placeholder {
            border: 1px dotted black;
            margin: 0 1em 1em 0;
            height: 30px;
        }

        .ActiveChek {
            margin-left: 10px;
            float: left;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            background: #FFFFFF;
            height: 25px;
            color: #368bba;
        }
        .highcharts-container {
            margin-left: 2px;
            border-bottom-right-radius: 15px;
            border-bottom-left-radius: 15px;
            background: rgba(255,255,255,0.8);
        }
        .labelDash {
            margin-left: 0px;
            float: left;
            padding-left: 2px;
            font-size: 14px;
            font-weight: bold;
            color: #0C3C64;
            font-family: Arial;
        }
        .drpdown {
            width: 150px;
            border-radius: 9px;
            height: 24px;
            font-family: Arial;
            background-color: #779379;
            outline: none;
        }

        .txtCal {
            width: 116px;
            height: 22px;
            float: left;
            color: #333333;
            padding-left: 1px;
            margin-top: 11px;
            margin-left: 2px;
            font-family: Arial, Helvetica, sans-serif;
            background: rgb(173, 216, 230);
            border: none;
        }
        .cal {
            width: 24px;
            margin: 0px;
            padding: 0px;
            height: 24px;
            float: right;
            margin-right: 54px;
            margin-top: 11px;
            background: rgb(173, 216, 230);
        }
        .btnSearchDash {
            width: 65px;
            height: 26px;
            font-size: 12px;
            color: #333333;
            padding-left: 12px;
            margin-top: 15px;
            font-family: Arial, Helvetica, sans-serif;
            background: #b7d1e0;
            border-radius: 0px;
            margin-left: 0px;
        }
        .setting_top {
            width: 160px;
            float: left;
            font-size: 28px;
            color: #333333;
            padding-left: 0px;
            margin-top: 0px;
            margin-left: 0px;
            font-family: Arial, Helvetica, sans-serif;
        }
        .text_label {
            width: 54px;
            height: 30px;
            margin-top: 6px;
            padding: 0px;
            font-family: Arial, Helvetica, sans-serif;
            color: #333333;
            padding-left: 12px;
            float: left;
            padding-left: 0px;
            padding-top: 6px;
        }
        .sera_bg {
            width: 290px;
            height: 40px;
            margin: 0px;
            padding: 0px;
            float: left;
        }

        .settings {           
            width: 220px;
            height: auto;
            margin: 0px;
            padding: 0px;
            padding-bottom: 15px;
            background: #f5fafd;
            margin-top: 18px;
            margin-left: 0px;
            margin-right: 65px;
            border-radius: 14px;
            float: right;
            box-shadow: 8px -7px 36px -6px rgba(0,0,0,0.75);
            background:rgba(15, 15, 14, 0.3);       
        }

        .btnSearch {
            margin-left: 80px;
        }

        .lblInfo {
            color: red;
        }
    </style>



</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="frmUserDashBoard" runat="server">
        <asp:ScriptManager ID="Scriptmanager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <div class="btn_bg5"> 
                    <div class="heading2">
                    <div class="user">Dash Board</div>
                        </div>
                

                <div class="clear"></div>

                <%--<script src="js1/modules/exporting.js"></script>--%>
                <asp:Button ID="btnHidden" runat="server" Text="btnHidden" CausesValidation="false" OnClick="btnHidden_Click" Style="display: none;" />
                <asp:TextBox ID="txtDate" runat="server" Style="display: none;"></asp:TextBox>




                <div class="group_bg2" style="width: 1200px; min-height:900px; height:100%;">




                    <div class="con">

                        <div>
                            <div class="divL" style="padding: 14px 7px 2px 4px; float: left; width: 410px;">
                                <div id="column2" class="column" runat="server" visible="false">
                                    <div class="portlet">
                                        <div class="portlet-header">
                                            <div id="cbxBar1" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 10px; padding: 0px 0px 0px 0px; float: left;">
                                                <span style="padding-top: 10px;">Bar</span>
                                            </div>
                                            <div id="cbxpie1" style="width: 100px; margin: 0px 0px 0px 5px; padding: 0px 0px 0px 0px; float: left;">
                                                <span style="padding-top: 10px;">Pie</span>
                                            </div>


                                        </div>
                                        <div class="portlet-content">
                                            <div id="divCustomer" style="height: 297px; width: 410px;" />
                                        </div>
                                    </div>
                                </div>

                                <div id="column1" class="column" runat="server" visible="false">
                                    <div class="portlet">
                                        <div class="portlet-header">
                                            <div id="cbxBar2" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 10px; padding: 0px 0px 0px 0px; float: left;">

                                                <span>Bar</span>


                                            </div>
                                            <div id="cbxpie2" style="width: 100px; margin: 0px 0px 0px 5px; padding: 0px 0px 0px 0px; float: left;">

                                                <span>Pie</span>

                                            </div>

                                        </div>
                                        <div class="portlet-content">
                                            <div id="divItem" style="height: 297px; width: 410px;"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>


                            <div class="divR" style="padding: 14px 7px 2px 4px; float: left; width: 410px; margin-left: 12px;">
                                <div id="column3" class="column" runat="server" visible="false">
                                    <div class="portlet">
                                        <div class="portlet-header">
                                            <div id="cbxBar3" style="width: 100px; margin: 0px 0px 0px 10px; padding: 0px 0px 0px 0px; float: left;">
                                                <span>Bar</span>
                                            </div>
                                            <div id="cbxPie3" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 5px; padding: 0px 0px 0px 0px; float: left;">
                                                <span>Pie</span>
                                            </div>
                                        </div>
                                        <div class="portlet-content">
                                            <div id="divSale" style="height: 297px; width: 410px;"></div>
                                        </div>
                                    </div>
                                </div>

                                <%--  Settings--%>


                                <%--End--%>
                                <div id="column4" class="column" runat="server" visible="false">
                                    <div class="portlet">
                                        <div class="portlet-header">
                                            <div id="cbxBar4" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 10px; padding: 0px 0px 0px 0px; float: left;">
                                                <span>Bar</span>
                                            </div>
                                            <div id="cbxPie4" style="width: 100px; margin: 0px 0px 0px 5px; padding: 0px 0px 0px 0px; float: left;">
                                                <span>Grid</span>
                                            </div>
                                            <%--  <asp:CheckBox ID="cbxBar4" runat="server" Text="Bar" />
                                            <asp:CheckBox ID="cbxPie4" runat="server" Text="Pie" /></div>--%>
                                        </div>
                                        <div class="portlet-content">
                                            <div id="divMinStock" style="height: 297px; width: 410px;"></div>
                                            <div id="divRepeater" style="display: none; height: 290px; overflow-x: hidden; overflow-y: auto;">
                                                <text x="205" text-anchor="middle" class="highcharts-title" m zindex="4" style="color: #333333; font-size: 18px; fill: #333333; width: 346px; margin-left: 105px; padding-bottom: 10px; margin-top: 10px;"><tspan>Stock Report</tspan></text>

                                                <div id="PreviewTableClassDiv" class="CSSTableGenerator">
                                                    <table>
                                                        <tr style="background-color: #368BBA; color: white;">


                                                            <td></td>
                                                            <td>Current Stock</td>
                                                            <td>Min Level</td>

                                                        </tr>


                                                        <asp:Repeater ID="rptMinstock" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td><%#Eval("ItemName") %></td>
                                                                    <td><%#Eval("CurrentStock") %></td>
                                                                    <td><%#Eval("MinLevel") %></td>

                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>

                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>

                        <div>
                            <%--
                            <div class="settings" id="divSearch" runat="server" visible="false">
                                <div class="setting_top" style="width: 160px; height: 26px; font-size: 28px; color: #333333; padding-left: 0px; padding-bottom: 21px; margin-top: 21px; margin-left: 100px; font-family: Arial, Helvetica, sans-serif; background: ">
                                    <img src="DashBoardJs/JsDash/images/search.jpg" width="34" height="29" />Search
                                </div>
                                <div class="label1" style="width: 300px; height: auto; margin: 0px; padding: 0px; float: left; padding-left: 10px;">

                                    <div class="sera_bg">
                                        <div class="text_label">Store</div>
                                        <div class="setting_top">
                                            <asp:DropDownList ID="ddlItemStore" runat="server" CssClass="drpdown" OnSelectedIndexChanged="ddlItemStore_SelectedIndexChanged1"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="sera_bg">
                                        <div class="text_label">Period</div>
                                        <div class="setting_top">
                                            <asp:DropDownList ID="ddlType" onchange="CustomDateSerarch();" runat="server" CssClass="drpdown" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Last Month</asp:ListItem>
                                                <asp:ListItem Value="1">Last Week</asp:ListItem>
                                                <asp:ListItem Value="2">Last FortNight</asp:ListItem>
                                                <asp:ListItem Value="3">Last Quarter</asp:ListItem>
                                                <asp:ListItem Value="4">Last Half Year</asp:ListItem>
                                                <asp:ListItem Value="5">Yearly</asp:ListItem>
                                                <asp:ListItem Value="6">Custom Date</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="divdateWiseFilteraton" style="display: none; margin-top: -10px;">                                      
                                        <div class="sera_bg">
                                            <div class="text_label">From Date</div>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtCal"></asp:TextBox>
                                            <div class="cal">
                                                <img id="btnCl" src="images/cal.png" width="23" height="18" border="0" align="center" />
                                                <asp:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFromDate" PopupButtonID="btnCl" Format="dd/MM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>


                                        <div class="sera_bg">
                                            <div class="text_label">To Date</div>
                                            <div class="cal">
                                                <img id="img1" src="images/cal.png" width="23" height="18" border="0" align="center" />
                                                <asp:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtToDate" PopupButtonID="img1" Format="dd/MM/yyyy">
                                                </asp:CalendarExtender>

                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtCal"></asp:TextBox>
                                            <asp:CustomValidator ID="cvDatecheck" runat="server"
                                                ClientValidationFunction="ClientValidate" ControlToValidate="txtToDate" Display="None"
                                                CssClass="validator"
                                                ErrorMessage="To date must be greater than from date" SetFocusOnError="True" ValidationGroup="submit"></asp:CustomValidator>
					    <asp:ValidatorCalloutExtender ID="vce_cvDatecheck" CssClass="customCalloutStyle" runat="server" Enabled="True" TargetControlID="cvDatecheck"></asp:ValidatorCalloutExtender>


                                        </div>

                                    </div>
                                </div>
                                <div class="clear"></div>
                                <div class="btnSearch">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnSearchDash" ValidationGroup="submit" OnClick="btnSearch_Click" />

                                    <asp:Button ID="btnClearSearch" runat="server" Text="Clear" CssClass="btnSearchDash" CausesValidation="false" OnClick="btnClearSearch_Click" />
                                </div>
                                <label id="lblInfo" style="display: none;" class="lblInfo">To date must be greater than from date</label>
                                <div class="clear"></div>
                                <div class="clear"></div>
                            </div>  --%> 

                            <div class="settings" id="divSearch" runat="server" visible="false">
                               <div class="setting_top" style=" width: 220px;font-size: 28px; color: rgb(51, 51, 51); padding-left: 0px;padding-bottom: 21px;font-family: Arial,Helvetica,sans-serif; background:#12361D;border-radius: 8px;height: 12px;margin-top: 1px;">
                                    <img src="DashBoardJs/JsDash/images/Search.png" style="width:213px;margin-left:7px;" height="29" />
                                </div>
                                <div class="label1" style="width: 300px; height: auto; margin: 0px; padding: 0px; float: left; padding-left: 10px;">
                                    <div class="sera_bg">
                                        <div class="text_label">Store</div>
                                        <div class="setting_top">
                                            <asp:DropDownList ID="ddlItemStore" TabIndex="1" runat="server" CssClass="drpdown" OnSelectedIndexChanged="ddlItemStore_SelectedIndexChanged1"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="sera_bg">
                                        <div class="text_label">Period</div>
                                        <div class="setting_top">
                                            <asp:DropDownList ID="ddlType" onchange="CustomDateSerarch();" TabIndex="2" runat="server" CssClass="drpdown" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Last Month</asp:ListItem>
                                                <asp:ListItem Value="1">Last Week</asp:ListItem>
                                                <asp:ListItem Value="2">Last FortNight</asp:ListItem>
                                                <asp:ListItem Value="3">Last Quarter</asp:ListItem>
                                                <asp:ListItem Value="4">Last Half Year</asp:ListItem>
                                                <asp:ListItem Value="5">Yearly</asp:ListItem>
                                                <asp:ListItem Value="6">Custom Date</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="divdateWiseFilteraton" style="display: none; margin-top: -10px;">
                                        <%--style="display: none;"--%>
                                        <div class="sera_bg">
                                            <div class="text_label">From Date</div>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtCal" TabIndex="3"></asp:TextBox>
                                            <div class="cal">
                                                <img id="btnCl" src="images/cal.png" width="23" height="18" border="0" align="center" />
                                                <asp:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFromDate" PopupButtonID="btnCl" Format="dd/MM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="sera_bg">
                                            <div class="text_label">To Date</div>
                                            <div class="cal">                                                
                                                <img id="img1" src="images/cal.png" width="23" height="18" border="0" align="center" />                                              
                                                <asp:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtToDate" PopupButtonID="img1" Format="dd/MM/yyyy">
                                                </asp:CalendarExtender>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtCal" TabIndex="4"></asp:TextBox>
                                            <asp:CustomValidator ID="cvDatecheck" runat="server"
                                                ClientValidationFunction="ClientValidate" ControlToValidate="txtToDate" Display="None"
                                                CssClass="validator"
                                                ErrorMessage="To date must be greater than from date" SetFocusOnError="True" ValidationGroup="submit"></asp:CustomValidator>                                            
                                        </div>
                                    </div>
                                </div>
                                <div class="clear"></div>
                                <div class="btnSearch">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" style="width:63px" CssClass="button"  TabIndex="5" ValidationGroup="submit"  OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnClearSearch" runat="server" Text="Clear" style="width:63px" CssClass="button" TabIndex="6" CausesValidation="false" OnClick="btnClearSearch_Click" />
                                </div>
                                <label id="lblInfo" style="display:none;" class="lblInfo">To date must be greater than from date</label>
                                <div class="clear"></div>
                                <div class="clear"></div>                               
                            </div>




                           
                        </div>
                    </div>

                </div>
                </div>
                <asp:HiddenField ID="hdfCounter" runat="server" />
                <asp:HiddenField ID="hdfSale" runat="server" />
                <asp:HiddenField ID="hdfCustomer" runat="server" />
                <asp:HiddenField ID="hdfMinStock" runat="server" />
                <asp:HiddenField ID="hfClient" runat="server" />
                <asp:HiddenField ID="hfUser" runat="server" />
                <asp:HiddenField ID="hdfTest" runat="server" />
                <asp:HiddenField ID="hdfPeriod" runat="server" />



            </ContentTemplate>
        </asp:UpdatePanel>

        <script src="DashBoardJs/modules/no-data-to-display.js"></script>
        <script src="DashBoardJs/highcharts.js"></script>
        <script src="DashBoardJs/highcharts-3d.js"></script>
        <script src="DashBoardJs/modules/no-data-to-display.src.js"></script>
        <script src="DashBoardJs/modules/drilldown.js"></script>
    </form>

</body>
</html>

