0<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminDashBoardDetailed.aspx.cs" Inherits="AdminDashBoardDetailed" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=In Style :: Dash Board" />
    <title></title>

    <link href="mainAdmin.css" rel="stylesheet" />
    <link href="StyleSheet1.css" rel="stylesheet" />
    <link href="DashBoardJs/JsDash/TableDesign.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/General.js"></script>
    <script src="AdminDashBoard.js"></script>
    <script src="js/jquery-1.10.2.js"></script>
    <script src="DashBoardJs/JsDash/jquery-ui.js"></script>
    <style>
        #divRepeater table {
            background-color: #0066FF;
            height: auto;
            margin-top: 5px;
            padding: 0px;
            font-family: Arial, Helvetica, sans-serif;
        }

        #divRepeater span {
            color: #333333;
            font-size: 22px;
            fill: #333333;
            width: 346px;
            padding-top: 30px;
            margin-left: -70px;
            padding-bottom: 10px;
            margin-top: 10px;
        }

        #PreviewTableClassDiv table {
            background-color: #0066FF;
        }

        #PreviewTableClassDiv span {
            color: #333333;
            font-size: 22px;
            width: 346px;
            padding-top: 30px;
            margin-left: -70px;
            padding-bottom: 10px;
            margin-top: 10px;
        }
    </style>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="frmUserDashBoard" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="wrappar">
            <div class="clear">
            </div>
            <div class="top_header">
                <div class="admin">
                    Admin Dash Board
                </div>
            </div>
            <div class="middle">
                <asp:TextBox ID="txtDate" runat="server" Style="display: none;"></asp:TextBox>
                <div>
                    <div style="padding: 14px 7px 2px 4px; float: left; width: 30%">
                        <!--Item -->
                        <div id="column1" class="column" runat="server" visible="false">
                            <div class="portlet">
                                <div class="portlet-header">
                                    <div id="cbxBar2" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">

                                        <span>Bar</span>


                                    </div>
                                    <div id="cbxpie2" style="width: 100px; margin: 0px 0px 0px 0px; padding: 10px 0px 0px 0px; float: left;">

                                        <span>Pie</span>

                                    </div>
                                    <div id="divzoom1" class="divzoom">
                                    </div>

                                </div>
                                <div class="portlet-content">
                                    <div id="divItem" class="container"></div>
                                </div>
                            </div>
                        </div>
                        <!--Customer -->
                        <div id="column2" class="column" runat="server" visible="false">
                            <div class="portlet">
                                <div class="portlet-header">

                                    <div id="cbxBar1" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span style="padding-top: 10px;">Bar</span>
                                    </div>
                                    <div id="cbxpie1" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span style="padding-top: 10px;">Pie</span>
                                    </div>
                                    <div id="divzoom2" class="divzoom">
                                    </div>

                                </div>
                                <div class="portlet-content">
                                    <div id="divCustomer" class="container" />
                                </div>
                            </div>
                        </div>
                        <!--Purchase -->
                        <div id="column7" class="column" runat="server" visible="false">
                            <div class="portlet">
                                <div class="portlet-header">

                                    <div id="divzoom9" class="divzoom">
                                    </div>
                                    <%-- <div id="Div2" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 10px; padding: 0px 0px 0px 0px; float: left;">
                                                </div>--%>
                                </div>
                                <div class="portlet-content">
                                    <div id="divPurchase" class="container"></div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
                <div>
                    <!---DivC--->
                    <div style="padding: 14px 7px 2px 4px; float: left; width: 410px; margin-left: 12px; width: 30%">
                        <!--Sale -->
                        <div id="column3" class="column" runat="server" visible="false">
                            <div class="portlet">
                                <div class="portlet-header">

                                    <div id="cbxBar3" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span>Bar</span>
                                    </div>
                                    <div id="cbxPie3" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span>Pie</span>

                                    </div>
                                    <div id="divzoom3" class="divzoom">
                                    </div>

                                </div>
                                <div class="portlet-content">
                                    <div id="divSale" class="container"></div>
                                </div>
                            </div>
                        </div>
                        <!--MinStock -->
                        <div id="column4" class="column" runat="server" visible="false">
                            <div class="portlet">
                                <div class="portlet-header">
                                    <div id="cbxBar4" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span>Bar</span>
                                    </div>
                                    <div id="cbxPie4" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span>Grid</span>
                                    </div>

                                    <div id="divzoom4" class="divzoom">
                                    </div>

                                </div>
                                <div class="portlet-content">


                                    <div id="divMinStock" class="container"></div>


                                    <div id="divRepeater" style="display: none;">
                                        <text x="205" text-anchor="middle" class="highcharts-title" m zindex="4" style="color: #333333; font-size: 18px; fill: #333333; width: 346px; margin-left: 105px; padding-bottom: 10px; margin-top: 10px;"><tspan>Stock Report</tspan></text>

                                        <div id="PreviewTableClassDiv" class="CSSTableGenerator">
                                            <table id="tblReport">
                                                <tr style="background-color: #368BBA; color: white;">


                                                    <td></td>
                                                    <td>Current Stock</td>
                                                    <td>Min Level</td>

                                                </tr>




                                            </table>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <!--Sales Detail -->
                        <div id="column8" class="column" runat="server" visible="false">
                            <div class="portlet">
                                <div class="portlet-header">
                                    <div id="divzoom8" class="divzoom">
                                    </div>
                                </div>
                                <div class="portlet-content">
                                    <div id="divSaleDetailed" class="container" />
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
                <div>


                    <!---DivL--->
                    <div style="padding: 14px 7px 2px 4px; float: left; width: 410px; margin-left: 12px; width: 30%">
                        <!--Credit Supplier -->
                        <div id="column5" class="column" runat="server" visible="false">
                            <div class="portlet">
                                <div class="portlet-header">

                                    <div id="cbxBar5" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span>Bar</span>
                                    </div>
                                    <div id="cbxPie5" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span>Pie</span>

                                    </div>
                                    <div id="divzoom5" class="divzoom">
                                    </div>
                                </div>
                                <div class="portlet-content">
                                    <div id="divCreditSupplier" class="container"></div>
                                </div>
                            </div>
                        </div>
                        <!--Credit Customer -->
                        <div id="column6" class="column" runat="server" visible="false">
                            <div class="portlet">
                                <div class="portlet-header">

                                    <div id="cbxBar6" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span>Bar</span>
                                    </div>
                                    <div id="cbxPie6" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span>Pie</span>

                                    </div>
                                    <div id="divzoom6" class="divzoom">
                                    </div>
                                </div>
                                <div class="portlet-content">
                                    <div id="divCreditCustomer" class="container"></div>
                                </div>
                            </div>
                        </div>
                        <!--Salesman -->
                        <div id="column9" class="column" runat="server" visible="false">
                            <div class="portlet">
                                <div class="portlet-header">

                                    <div id="cbxBar8" class="ActiveChek" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span style="padding-top: 10px;">Bar</span>
                                    </div>
                                    <div id="cbxPie8" style="width: 100px; margin: 0px 0px 0px 0px; padding-top: 10px; float: left;">
                                        <span style="padding-top: 10px;">Pie</span>
                                    </div>
                                    <div id="divzoom7" class="divzoom">
                                    </div>

                                </div>
                                <div class="portlet-content">
                                    <div id="divSupplier" class="container" />
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
                <!--Next Six -->
                <asp:HiddenField ID="hdfCounter" runat="server" />
                <asp:HiddenField ID="hdfSale" runat="server" />
                <asp:HiddenField ID="hdfCustomer" runat="server" />
                <asp:HiddenField ID="hdfMinStock" runat="server" />
                <asp:HiddenField ID="hfClient" runat="server" />

                <asp:HiddenField ID="hfUser" runat="server" />
                <asp:HiddenField ID="hdfCrCustomer" runat="server" />
                <asp:HiddenField ID="hdfCrPurchase" runat="server" />


                <asp:HiddenField ID="hdfPeriod" runat="server" />
                <asp:HiddenField ID="hdfSalesMan" runat="server" />
                <asp:HiddenField ID="hdfChart" runat="server" />


            </div>
            <div class="left_me"></div>


            <div id="divAnimate" class="close_icon" style="margin-top: 5px;margin-left:90%;width:31px;border-radius:4px;">
                <img id="closebtn" src="images/buttons.png" style="height: 30px;width: 29px;margin-top: 1px;" />
            </div>
            <div id="leftNav" class="left_navi"  style="display: none;">
                <div id="divGeneral" runat="server" style="display: none;">
                    <div class="general" style="border-radius: 6px;width: 320px;font-family:initial;margin-top:1px;">
                        General
                    </div>
                    <div class="genera2">
                        <div class="store" style="font-family:initial;">
                            Graph
                        </div>
                        <asp:DropDownList ID="ddlGraphType1" runat="server" CssClass="dropdown" onchange="hideperiod();">
                            <asp:ListItem Value="0">All</asp:ListItem>

                        </asp:DropDownList>

                    </div>
                    <div class="genera2" id="divSearchStore">
                        <div class="store" style="font-family:initial;">
                            Store
                        </div>
                        <asp:DropDownList ID="ddlItemStore" runat="server" CssClass="dropdown"></asp:DropDownList>
                    </div>
                    <div class="genera2" id="divPeriodSearch">
                        <div class="store" style="font-family:initial;">
                            Period
                        </div>
                        <asp:DropDownList ID="ddlType" onchange="CustomDateSerarch();" runat="server" CssClass="dropdown">
                            <asp:ListItem Value="0">Last Month</asp:ListItem>
                            <asp:ListItem Value="1">Last Week</asp:ListItem>
                            <asp:ListItem Value="2">Last FortNight</asp:ListItem>
                            <asp:ListItem Value="3">Last Quarter</asp:ListItem>
                            <asp:ListItem Value="4">Last Half Year</asp:ListItem>
                            <asp:ListItem Value="5">Yearly</asp:ListItem>
                            <asp:ListItem Value="6">Custom Date</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                    <div id="divdateWiseFilteraton" style="display: none;">
                        <div class="genera2">
                            <div class="store" style="font-family:initial;">
                                Fromdate
                            </div>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="text_box"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </div>
                        <div class="genera2">
                            <div class="store">
                                To date
                            </div>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="text_box"></asp:TextBox>
                            <asp:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtToDate" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <asp:CustomValidator ID="cvDatecheck" runat="server"
                                ClientValidationFunction="ClientValidate" ControlToValidate="txtToDate" Display="None"
                                CssClass="validator"
                                ErrorMessage="To date must be greater than from date" SetFocusOnError="True" ValidationGroup="submit"></asp:CustomValidator>
			    <asp:ValidatorCalloutExtender ID="vce_cvDatecheck" CssClass="customCalloutStyle" runat="server" Enabled="True" TargetControlID="cvDatecheck"></asp:ValidatorCalloutExtender>

                        </div>
                    </div>
                    <br class="clear" />
                    <div>
                        <label id="lblInfo" style="display: none;" class="lblInfo">To date must be greater than from date</label>
                    </div>
                    <div class="genera2" style="width: 202px;margin-left: 96px;border-radius: 44px;">
                        <asp:Button ID="btnSearchGeneral" runat="server" Text="Search" class="btn1" ValidationGroup="submit" OnClientClick="return GeneralSearch();" />
                        <asp:Button ID="btnClearGeneral" runat="server" Text="Clear" Style="margin-left: 6px;" class="btn1" CausesValidation="false" OnClientClick="return ClearCommon();" />

                    </div>


                </div>

                <div id="divCompare" runat="server" style="display: none;">


                    <div class="genera5" style="border-radius: 6px;width: 320px;">
                        Compare
                    </div>
                    <div class="genera2">
                        <div class="store">
                            Graph
                        </div>
                        <asp:DropDownList ID="ddlComparisonal" runat="server" CssClass="dropdown" onchange="GetSaleDiv();">
                        </asp:DropDownList>
                    </div>
                    <div id="divSaleMode" style="display: none;">

                        <div class="genera2">
                            <div class="store">
                                Mode
                            </div>
                            <asp:DropDownList ID="ddlModeWiseSale" runat="server" CssClass="dropdown">
                                <asp:ListItem Selected="True" Value="SASTR">Store Wise</asp:ListItem>
                                <asp:ListItem Value="SAITM">Item Wise</asp:ListItem>
                                <asp:ListItem Value="SACU">Customer Wise</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="divPurchaseMode">
                        <div class="genera2">
                            <div class="store">
                                Mode
                            </div>
                            <asp:DropDownList ID="ddlPurchaseTypes" runat="server" CssClass="dropdown">

                                <asp:ListItem Selected="True" Value="PUSTR">Store Wise</asp:ListItem>

                                <asp:ListItem Value="PUITM">Item Wise</asp:ListItem>
                            </asp:DropDownList>


                        </div>
                    </div>
                    <div class="genera7">
                        Period 1
                    </div>
                    <div class="genera2">
                        <div class="store">
                            From date
                        </div>
                        <asp:TextBox ID="txtPurStFromDate" runat="server" CssClass="text_box"></asp:TextBox>
                        <asp:CalendarExtender ID="txtPurStFromDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtPurStFromDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </div>
                    <div class="genera2">
                        <div class="store">
                            To Date
                        </div>
                        <asp:TextBox ID="txtPurStEnDate" runat="server" CssClass="text_box"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtPurStEnDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        <asp:CustomValidator ID="CustomValidator2" runat="server"
                            ClientValidationFunction="ClientValidate2" ControlToValidate="txtPurStEnDate" Display="None"
                            CssClass="validator"
                            ErrorMessage="To date must be greater than from date" SetFocusOnError="True" ValidationGroup="Pur"></asp:CustomValidator>
			<asp:ValidatorCalloutExtender ID="vce_CustomValidator2" CssClass="customCalloutStyle" runat="server" Enabled="True" TargetControlID="CustomValidator2"></asp:ValidatorCalloutExtender>

                    </div>
                    <div class="genera7">
                        Period 2
                    </div>
                    <div class="genera2">
                        <div class="store">
                            From Date
                        </div>
                        <asp:TextBox ID="txtPurFrmDate" runat="server" CssClass="text_box"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtPurFrmDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                    </div>
                    <div class="genera2">
                        <div class="store">
                            To Date
                        </div>
                        <asp:TextBox ID="txtPurToDate" runat="server" CssClass="text_box"></asp:TextBox>
                        <asp:CalendarExtender ID="TextBox2_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtPurToDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        <asp:CustomValidator ID="CustomValidator3" runat="server"
                            ClientValidationFunction="ClientValidate3" ControlToValidate="txtPurToDate" Display="None"
                            CssClass="validator"
                            ErrorMessage="Comparison To date must be greater than Comparison from date" SetFocusOnError="True" ValidationGroup="Pur"></asp:CustomValidator>
			<asp:ValidatorCalloutExtender ID="vce_CustomValidator3" CssClass="customCalloutStyle" runat="server" Enabled="True" TargetControlID="CustomValidator3"></asp:ValidatorCalloutExtender>
                    </div>
                    <br class="clear" />
                    <div>
                        <label id="lblInfo1" style="display: none;" class="lblInfo">Comparison To date must be greater than comparison from date</label>
                    </div>
                    <div class="genera2" style="width: 202px;margin-left: 96px;border-radius: 44px;">
                        <asp:Button ID="btnSearchComp" runat="server" Text="Search" CssClass="btn1" OnClientClick="return ComparisonSearch();" ValidationGroup="Pur" />
                        <asp:Button ID="btnCmpClear" runat="server" Text="Clear" CssClass="btn1" CausesValidation="false" OnClientClick="return ClearSearch();" />

                    </div>
                    <br class="clear" />
                    <br class="clear" />
                </div>

            </div>





        </div>
        <!-----popupDiv--->












        <div class="popup" id="divPopupContainer" style="display: none;">
            <div id="popup_hesd">
                <div class="bar_text">
                    <div id="divZoomBar" class="bar3">Bar</div>
                    <div id="divZoomPie">Pie</div>
                    <div id="divZoomGrid" style="display: none;">Grid</div>
                    <div class="close1" onclick="DestroyZoom();">
                        <img src="images/vrio-logo-2.png" width="45" height="41" />
                    </div>
                </div>
            </div>
            <div class="clear"></div>

            <div id="p_content">

                <div id="divPopup" style="width: 1437px; height: 640px;"></div>
                <div id="divGrid" style="display: none;">
                </div>


            </div>

        </div>




        <div class="clear">
        </div>

        <div class="footer">
            <div class="footer_text">
                <img src="images/footer.jpg" width="133" height="29" />
            </div>
        </div>

        <script src="DashBoardJs/modules/no-data-to-display.js"></script>
        <script src="DashBoardJs/highcharts.js"></script>
        <script src="DashBoardJs/highcharts-3d.js"></script>
        <script src="DashBoardJs/modules/no-data-to-display.src.js"></script>

    </form>

</body>
</html>

