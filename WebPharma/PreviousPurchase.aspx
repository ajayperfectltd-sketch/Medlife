<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreviousPurchase.aspx.cs" Inherits="PreviousPurchase" %>
<%--<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>

 <%--   <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
     <script src="js/Navigation.js"></script>
       <script src="js/toastr.js"></script>--%>
 
       <link href="DashBoardJs/JsDash/jquery-ui.css" rel="stylesheet" />
    <link href="DashBoardJs/JsDash/TableDesign.css" rel="stylesheet" />
        <script src="js/jquery.min.js"></script>
      <script src="DashBoardJs/modules/no-data-to-display.js"></script>
        <script src="DashBoardJs/highcharts.js"></script>
        <script src="DashBoardJs/highcharts-3d.js"></script>
        <script src="DashBoardJs/modules/no-data-to-display.src.js"></script>
        <script src="DashBoardJs/modules/drilldown.js"></script>
        <script type="text/javascript">
            function SearhGraph() {
                MinStockBarorpie('bar');
                Custom();
                
            }
            function SearhGraphSot() {
                $('#cbxBar4').removeClass('ActiveChek');
                $("#cbxPie4").addClass('ActiveChek');
                MinStockBarorpie('pie');
            }
            function pageLoad() {
                if ($("#txtDate").val() != "") {
                    var dt = $("#hdfPeriod").val();
                    $("#cbxBar4").click(function () {
                        //SupplierDetails('bar');
                        
                        MinStockBarorpie('bar');
                        $('#cbxPie4').removeClass('ActiveChek');
                        $("#cbxBar4").addClass('ActiveChek');


                    });
                 
                  //  MinStockBarorpie('bar');
                    $("#cbxPie4").click(function () {
                     
                        MinStockBarorpie('pie');
                        $('#cbxBar4').removeClass('ActiveChek');
                        $("#cbxPie4").addClass('ActiveChek');

                    });
                  
                   
                
                   // MinStockBarorpie('pie');
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
                ////$('table tr:not(:first)').click(function () {

                ////    window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Stock=stock";
                ////});
            
            }
           
            
            function ClearSearchClick() {
                try {

                    $("#txtSearchName").val('');
                    $("#ddlsearchUnit").val("0");
                    $("#ddlSerachSubUnit").val("0");
                    $("#ddlSerachCategory").val("0");
                    $("#txtBatchNo").val('');
                    $("#ddlSearchManufacturer").val("0");
                    $("#ddlSearchSupplier").val("0");
                    $("#ddlGeneric").val("0");
                    
                    $("#ddlType").val("3");
                    
                    CustomDateSerarch();
                }
                catch (ex) {
                    alert(ex);
                }
            }
            $(document).ready(function () {

                ClearSearch();


            });
            function Custom()
            {
                var Period = $('#ddlType').find("option:selected").val();
              
                if (Period == '6') {
                    $("#divdateWiseFilteraton").show();
                }
                else {
                    $("#divdateWiseFilteraton").hide();
                }
            }
            function CustomDateSerarch() {
                var Period = $('#ddlType').find("option:selected").val();
              
                if (Period == '6') {
                  //  txtDate.Text = Convert.ToString(txtDate.Text);
                    //  txtTodate.Text = Convert.ToString(txtTodate.Text);
                    var dt = new Date();
                    $("#txtDate").val(dt.format("dd/MM/yyyy"));
                    $("#txtTodate").val(dt.format("dd/MM/yyyy"));
                    $("#divdateWiseFilteraton").show();
                }
                else {
                    $("#divdateWiseFilteraton").hide();
                }
            }
            function Collapse() {
                // $('#cbxPie4').addClass ('ActiveChek');
                
                $("#cbxBar4").removeClass('ActiveChek');
                $("#cbxPie4").addClass('ActiveChek');

              //  $("#divMinStock").attr('style', 'display:none; height: 297px; width: 410px;');
               // $("#divRepeater").attr('style', 'display:block; height: 297px; width: 410px;');
               
               // $('#cbxPie4').removeClass('ActiveChek');
            }
            function ClearSearch() {
                var dt = new Date().format('dd/MMM/yyyy');
                $("#txtDate").val(dt);
                $("#txtTodate").val(dt);
                $("#btnHidden").click();

            }
            function MinStockBarorpie(type) {
                if ($("#divMinStock").length > 0) {
                    var items = eval($("#hdfMinStock").val());
                    if (type == 'bar') {
                       
                        $("#divRepeater").attr('style', 'display:none;height: 297px; width:  410px;');
                        $("#divMinStock").attr('style', 'display:block;height: 297px; width: 410px;');
                        var chart = new Highcharts.Chart(

                            {

                           
                                chart: {
                                    renderTo: 'divMinStock',
                                    type: 'column',
                                    
                            },
                            title: {
                                text: 'Purchase History'
                            },
                            subtitle: {
                                text: ''
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
                                    text: 'Purchase Rate '
                                }
                            },
                            legend: {
                                enabled: false
                            },
                            tooltip: {
                                pointFormat: '<b>Batch:{point.Batch}</b><br/>Profit%:{point.Profit}</b><br/>ProfitAmnt:{point.ProfitAmnt}</b><br/>MRP:{point.MRP}<br/>SalesPrice:{point.SalesPrice}'
                            },
                            colors: [


                   '#0f635d',
                   '#42f4e8',

                            ],
                            plotOptions: { column: { colorByPoint: true } },
                            //plotOptions: {
                            //    column: {
                            //        color: '#2d9988'


                            //    },
                            //    series: {
                            //        cursor: 'pointer',
                            //        point: {
                            //            events: {
                            //                click: function () {
                            //                    window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Stock=stock";
                            //                }
                            //            }
                            //        }
                            //    }
                            //},
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
           
            function SupplierDetails(type) {
                if ($("#divMinStock").length > 0) {
                    alert('j');
                    var items = eval($("#hdfMinStock").val());
                    if (type == 'bar') {
                        alert('h3');
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
                                    text: 'Purchase Rate %'
                                }
                            },
                            legend: {
                                enabled: false
                            },
                            tooltip: {
                                pointFormat: '<b>{point.Supplier Name}}</b><br/>Supplier Name:{point.Supplier Name}<br/>SV:<b>{point.y:.1f}%</b><br/>ML:{point.Min}<br/>CS:{point.CS}'
                            },
                            plotOptions: {
                                column: {
                                    color: '#FF0000'


                                },
                                series: {
                                    cursor: 'pointer',
                                    point: {
                                        events: {
                                            //click: function () {
                                            //    window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Stock=stock";
                                            //}
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
                        alert('g');
                    }
                    else {
                       
                        $("#divMinStock").attr('style', 'display:none; height: 297px; width: 410px;');
                        $("#divRepeater").attr('style', 'display:block; height: 297px; width: 410px;');
                    }
                }
            }
            //function pageLoad() {

            //    var ctrContainer = "form";
            //    setKeynavigation(ctrContainer);

            //}
           

        

    </script>
    
    <script src="DashBoardJs/JsDash/jquery-ui.js"></script>

    <style>
        .column {
            padding-bottom: 0px;
        }

        .portlet {
            width: 100%;
            margin: 0px 0px 25px 50px;
            border-radius: 15px;
            background: rgba(255,255,255,0.5);
            -webkit-box-shadow: 4px -7px 32px -6px rgba(0,0,0,0.75);
            -moz-box-shadow: 4px -7px 32px -6px rgba(0,0,0,0.75);
            box-shadow: 4px -7px 32px -6px rgba(174, 174, 174, 0.8);
        }

        .portlet-header {
            width: 100%;
            height: 25px;
            margin: 0px;
            padding: 0px;
            background: #07928f;
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
            color: #12361D;
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
            width: 450px;
            
            height: 24px;
            font-family: Arial;
            background-color: #fafafa
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
            padding-left: 0px;
            float: left;
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
            width: 520px;
            height: auto;
            margin: 0px;
            padding: 0px;
            padding-bottom: 15px;
            background: #FFFFFF;
            margin-top: 18px;
            margin-left: 0px;
            margin-right: 65px;
            border-radius: 14px;
            float: right;
           box-shadow: 2px -2px 13px -6px rgba(0,0,0,0.75);
                     
        }

        .btnSearch {
            margin-left: 80px;
        }
        .lblInfo {
            color:red;
        }
        .gridvieaderStyle  
         
   
             {  font-size:15px;  text-decoration:none;outline:none;

        }
   
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                  <div class="btn_bg5"> 
                       <div class="heading2">

                            <div class="user">Purchase History</div>
                           </div>
                    <div class="clear"></div>
                    
                    <div class="group_bg2">
                     
                        <div id="divView" runat="server"  style="height:750px;" >
                               <div  style="width:950px">
                            <div class="divL">
                                <ul class="contentul">
                                
                                        <li>
                                            <asp:Label ID="Label6" runat="server" CssClass="label"  Text="Name"></asp:Label>
                                            <asp:TextBox ID="txtSearchName" CssClass="textbox" TabIndex="1" runat="server" ></asp:TextBox>
                                        </li>
                                       
                                   
                                 <li class="clear"></li>
                                  <li>
                                        <asp:Label ID="Label4" runat="server" CssClass="label"  Text="Unit"></asp:Label>
                                        <asp:DropDownList ID="ddlsearchUnit" runat="server"  TabIndex="2" CssClass="dropdown" ></asp:DropDownList>
                                    </li>
                                  
                                         <li>
                                            <asp:Label ID="Label12" runat="server" CssClass="label" Text="Subunit"></asp:Label>
                                            <asp:DropDownList ID="ddlSerachSubUnit" runat="server" CssClass="dropdown" TabIndex="3"></asp:DropDownList>
                                        </li>
                                      <li>
                                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="Generic"></asp:Label>
                                            <asp:DropDownList ID="ddlGeneric" runat="server" CssClass="dropdown" TabIndex="4"></asp:DropDownList>
                                        </li>
                                     
                                        </li> 
                                    </ul>
                                        </div>
                                    
                                   
                              
                           <%-- </div>--%>
                           
                        <div class="divR">
                               <ul class="contentul">
                                    <li>
                                            <asp:Label ID="Label10" runat="server" CssClass="label" Text="Category"></asp:Label>
                                            <asp:DropDownList ID="ddlSerachCategory" runat="server"  AutoPostBack="true" CssClass="dropdown" TabIndex="5"></asp:DropDownList>
                                        </li>
                                     <li >
                                            <asp:Label ID="Label7" runat="server" CssClass="label" Text="Batchno"></asp:Label>
                                          <asp:TextBox ID="txtBatchNo"   CssClass="textbox" TabIndex="6" runat="server"  ></asp:TextBox>
                                            
                                        </li>
                                  <li class="clear"></li>
                                   
                                    <li>
                                        <asp:Label ID="Label32" runat="server" CssClass="label" Text="Manufacturer"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchManufacturer" runat="server" CssClass="dropdown" TabIndex="7"></asp:DropDownList>
                                    </li>
                                            <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Supplier"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchSupplier" runat="server" CssClass="dropdown" TabIndex="8"></asp:DropDownList>
                                    </li>
                                    <li>
                                         <asp:Label ID="Label3" runat="server" CssClass="label" Text="Type"></asp:Label>
                                        <asp:DropDownList ID="ddlType" onchange="CustomDateSerarch();" TabIndex="9" runat="server" CssClass="drpdown" Width="222px"  OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                               <%-- <asp:ListItem Value="0">Last Month</asp:ListItem>--%>
                                             <%--   <asp:ListItem Value="1">Last Week</asp:ListItem>--%>
                                                <%--<asp:ListItem Value="2">Last FortNight</asp:ListItem>--%>
                                                <asp:ListItem Value="3">Last Quarter</asp:ListItem>
                                                <asp:ListItem Value="4">Last Half Year</asp:ListItem>
                                                <asp:ListItem Value="5">Yearly</asp:ListItem>
                                                <asp:ListItem Value="6">Custom Date</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                      <li>
                                    
                                        </li> 
                                   <li class="clear"></li>
                                    <div id="divdateWiseFilteraton" style="display: none;" runat="server">
                                       <li>
                                           <label class="label">From Date</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtDate" TabIndex="10" AutoComplete="off" />
                                    <cc:CalendarExtender ID="txtDate_CalendarExtender" Format="dd/MM/yyyy" runat="server" Enabled="True" TargetControlID="txtDate"  ></cc:CalendarExtender>
                                        </li>
                                        <li class="clear"></li>
                                   
                                          <li>
                                        <label class="label" style="margin-top:8px">To Date</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtTodate" TabIndex="11" AutoComplete="off" />
                                    <cc:CalendarExtender ID="txtToDate_CalendarExtender" Format="dd/MM/yyyy" runat="server" Enabled="True" TargetControlID="txtTodate"></cc:CalendarExtender>
                                        </li>


                                    </div>
                                  
                                  
                                  </ul>
                             
                           </div>
                                         <li class="clear"></li>
                                        <li class="btnMargin"  style="padding-left:160px;padding-top:5px">
                                            <asp:Button ID="btnSearch" runat="server" OnClientClick="ChangeValue();" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="12" />
                                            <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearchClick();"   OnClick="btnSearchClear_Click" TabIndex="13" />
                                        </li>
                                      <li class="clear"></li>
                                    <li class="clear"></li>
                                   <br />
                        <div id="column4" class="column" runat="server" visible="false">
                                    <div class="portlet">
                                        <div class="portlet-header">
                                            <div id="cbxPie4" style="width: 100px; margin: 0px 0px 0px 5px; padding: 0px 0px 0px 0px; float: left;">
                                                <span>Grid</span>
                                            </div>
                                           <div id="cbxBar4" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 10px; padding: 0px 0px 0px 0px; float: left;">
                                                <span>Bar</span>
                                            </div>
                                            
                                             
                                            <%--  <asp:CheckBox ID="cbxBar4" runat="server" Text="Bar" />
                                            <asp:CheckBox ID="cbxPie4" runat="server" Text="Pie" /></div>--%>
                                        </div>
                                        <div class="portlet-content">
                                            
                                            <div id="divMinStock" style="height: 297px; width: 410px;"></div>
                                            <div id="divRepeater" style="display: none;height:590px; overflow-x:hidden; overflow-y:auto;"">
                                                <text x="205" text-anchor="middle" class="highcharts-title" m zindex="4" style="color: #333333; font-size: 18px; fill: #333333; width: 346px; margin-left: 105px; padding-bottom: 10px; margin-top: 10px;"><tspan>Purchase Details</tspan></text>

                                                <div id="PreviewTableClassDiv"   style="overflow-y: scroll; height:270px;width:950px;" >
                                             <asp:GridView ID="gvPriviousPurchase" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" Width="900px"  BorderStyle="Solid"  EnableViewState="true"    OnPageIndexChanging="gvPriviousPurchase_PageIndexChanging"   OnSorting="gvPriviousPurchase_Sorting"   BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowSorting="True">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                                   <RowStyle HorizontalAlign="Left" Wrap="False" />
                                            <EmptyDataRowStyle BorderStyle="Solid" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" Visible="false"  ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="SupplierName" HeaderText="Supplier" SortExpression="SupplierName" />
                                          <asp:BoundField DataField="ItemName" HeaderText="Item" SortExpression="ItemName" />
                                        <asp:BoundField DataField="InvoiceDate" HeaderText="Date" Visible="false" />
                                        <asp:BoundField DataField="BatchNo" HeaderText="Batch" SortExpression="BatchNo" />
                                         <asp:BoundField DataField="ExpiryDate" HeaderText="Exp.Date" SortExpression="ExpiryDate" />
                                         <asp:BoundField DataField="PurchaseRate"  ItemStyle-HorizontalAlign="Right" HeaderText="Pur.Rate" SortExpression="PurchaseRate" />
                                        <asp:BoundField DataField="ProfitPer"  ItemStyle-HorizontalAlign="Right" HeaderText="Prof%" SortExpression="ProfitPer" />
                                            <asp:BoundField DataField="ProfitAmount"  ItemStyle-HorizontalAlign="Right" HeaderText="Prof.Amt" SortExpression="ProfitAmount" />
                                          <asp:BoundField DataField="DiscountAmount" ItemStyle-HorizontalAlign="Right" HeaderText="Disc.Amt" SortExpression="DiscountAmount" />
                                        <asp:BoundField DataField="MRP" ItemStyle-HorizontalAlign="Right" HeaderText="MRP" SortExpression="MRP" />
                                         <asp:BoundField DataField="SalesPrice" ItemStyle-HorizontalAlign="Right" HeaderText="Sal.Amt" SortExpression="SalesPrice" />
                                        
                                        
                                        
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle CssClass="hederdesgn" ForeColor="White" HorizontalAlign="Center" Font-Bold="true"  Height="30px"/>
                                    <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                                            
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                    <asp:Button ID="btnHidden" runat="server" Text="btnHidden" CausesValidation="false" Visible="false" OnClick="btnHidden_Click" />
                                     
                                    </div>
                            <ul class="contentul">
                              
                             
                            </ul>
                                <li class="clear"></li>
                             <div class="settings" id="divSearch" runat="server" visible="false">
                              
                        </div>
                         
<%--<cc:BarChart ID="BarChart1" runat="server" ChartHeight="300" ChartWidth = "350"
    ChartType="Column" ChartTitleColor="#67AB9C" Visible = "false"
    CategoryAxisLineColor="##7AB9C" ForeColor="#ccccff" ValueAxisLineColor="#67AB9C" BaseLineColor="#67AB9C">
</cc:BarChart>--%>
<%--                             <div id="chart" style="width: 900px; height: 500px;">
</div>--%>
                        
                           
                        </div>

                    </div>
                    <asp:HiddenField ID="hfClient" runat="server" />
                    <asp:HiddenField ID="hfUser" runat="server" />
                 <asp:HiddenField ID="hdfValue" runat="server" />
                  <asp:HiddenField ID="hdfMinStock" runat="server" />
                  <asp:HiddenField ID="hdfPeriod" runat="server" />
               
                   
                </div>
        
<%-- <asp:Chart ID="Chart1" runat="server" Height="400px" Width="600px">
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
                <AxisY>
                    <MajorGrid LineColor="DarkGray" LineDashStyle="Dot" />
                    <LabelStyle Format="C2" />
                </AxisY>
                <AxisX>
                    <MajorGrid LineColor="DarkGray" LineDashStyle="Dot" />
                </AxisX>
            </asp:ChartArea>
        </ChartAreas>
        <Legends>
            <asp:Legend Name="Legend1">
            </asp:Legend>
        </Legends>
    </asp:Chart>--%>
<%--                 <asp:Chart ID="Chart2" runat="server" Height="300px" Width="400px" Visible="false">
    <Titles>
        <asp:Title ShadowOffset="3" Name="Items" />
    </Titles>
    <Legends>
        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
            LegendStyle="Row" />
    </Legends>
    <Series>
        <asp:Series Name="Default" />
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
    </ChartAreas>

</asp:Chart>--%>
              <%--        <asp:Chart ID="Chart2" runat="server" Visible = "False" Palette="Pastel">
            <Titles>
                <asp:Title ShadowOffset="3" Name="Items" />
            </Titles>
            <Legends>
                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Column" />
            </Legends>
            <Series>
                <asp:Series Name="Default" ChartType="Line" />
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
            </ChartAreas>
        </asp:Chart>--%>
     
            <%--     <cc:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc:ToolkitScriptManager>--%>
          
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
