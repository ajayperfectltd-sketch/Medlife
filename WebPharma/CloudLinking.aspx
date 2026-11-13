<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CloudLinking.aspx.cs" Inherits="CloudLinking"   %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="~/Popup/CommonPopupCloudImport.ascx" TagName="CommonPopupCloudImport" TagPrefix="Uc2" %>
<%@ Register Src="~/Popup/CommonPopupCloudExport.ascx" TagName="CommonPopupCloudExport" TagPrefix="Uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <%-- <link href="AssetLogin/css/bootstrap-grid.min.css" rel="stylesheet" />--%>
      <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="AssetLogin/css/CloudLinking/main.css" rel="stylesheet" />
   <link href="AssetLogin/css/CloudLinking/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href ="AssetLogin/css/CloudLinking/themify-icons/css/themify-icons.css" rel="stylesheet" />   
      <link href="css/toastr.css" rel="stylesheet" />
  <script src="js/jquery.min.js"></script>
    <script src="js/toastr.js"></script>
         <script src="js/toastrpos.js"></script>
  
    <title></title>
    <style>
        .user {
        width: 40%;
        margin: -2px;
        height: auto;
        padding: 0;
        float: left;
        color: #12b4b0;
        font-family: tahoma,Helvetica,sans-serif;
        }
        .heading2 {
        width: 99%;
        height: 42px;
        padding-top: 12px;
        font-family: tahoma,Helvetica,sans-serif;
        font-size: 23px;
        padding-left: 20px;
        color: #FFF;
        border-radius: 10px 10px 10px 10px;
        -moz-border-radius: 10px;
        -webkit-border-radius: 8px 8px 8px 8px;
        margin: 4px 0 0 23px;
        background: #FFFFFF;
        -webkit-box-shadow: 1px 1px 27px -15px rgba(0,0,0,0.58);
        -moz-box-shadow: 1px 1px 27px -15px rgba(0,0,0,0.58);
        box-shadow: 1px 1px 27px -15px rgba(0,0,0,0.58);
        padding-right: 7px;
        }
        .container
        {
               max-width: 1200px;
            /*width:1821px !important;*/
        }
        .divL {
            float: left;
            width: 48%;
        }
        .pricing-box:not(:first-child) {
            padding-left: 7px;
        }

        .pricing-box:not(:last-child) {
            padding-right: 7px;
            padding-left: 0px;
        }

        .pricing-box .price {
            font-size: 22px;
            line-height: 20px;
            height: 28px;
            text-align: center;
            color: #555;
        }

            .pricing-box .price small {
                font-size: 22px;
            }

        .pricing-box .btn {
            font-size: 16px;
        }

        .pricing-box .widget-header {
            text-align: center;
            padding-left: 0;
        }

        .widget-color-dark {
            border-color: #5a5a5a;
        }

        .widget-box {
            padding: 0;
            box-shadow: none;
            margin: 3px 0;
            border: 1px solid #CCC;
            border-top-color: rgb(204, 204, 204);
            border-right-color: rgb(204, 204, 204);
            border-bottom-color: rgb(204, 204, 204);
            border-left-color: rgb(204, 204, 204);
        }

            .widget-box[class*="widget-color-"] > .widget-header {
                color: #FFF;
                filter: progid:DXImageTransform.Microsoft.gradient(enabled=false);
            }

            .widget-box[class*="widget-color-"] > .widget-headerHor {
                color: #FFF;
                filter: progid:DXImageTransform.Microsoft.gradient(enabled=false);
            }

        .pricing-box .widget-header {
            text-align: center;
            padding-left: 0;
        }

        .pricing-box .widget-headerHor {
            text-align: center;
            padding-left: 0;
        }

        .widget-color-dark > .widget-header {
            border-color: #666;
            background: #6d9b6f;
        }

        .widget-color-dark > .widget-headerHor {
            border-color: #666;
            background: #d5652e;
        }

        .widget-header {
            background-position: 0% 0%;
            -webkit-box-sizing: content-box;
            -moz-box-sizing: content-box;
            box-sizing: content-box;
            position: relative;
            min-height: 38px;
            color: #669FC7;
            border-bottom: 1px solid rgb(221, 221, 221);
            padding-left: 12px;
            background-image: linear-gradient(to bottom,#FFF 0,#EEE 100%);
            background-color: #f7f7f7;
            background-repeat: repeat-x;
            background-attachment: scroll;
        }

        .widget-headerHor {
            background-position: 0% 0%;
            -webkit-box-sizing: content-box;
            -moz-box-sizing: content-box;
            box-sizing: content-box;
            position: relative;
            min-height: 38px;
            color: #669FC7;
            border-bottom: 1px solid rgb(221, 221, 221);
            padding-left: 12px;
            background-image: linear-gradient(to bottom,#FFF 0,#EEE 100%);
            background-color: #f7f7f7;
            background-repeat: repeat-x;
            background-attachment: scroll;
        }

        .widget-header > .widget-title {
            line-height: 36px;
            padding: 0;
            margin: 0;
            display: inline;
        }

        .widget-headerHor > .widget-title {
            line-height: 36px;
            padding: 0;
            margin: 0;
            display: inline;
        }

        h5.bigger {
            font-size: 16px;
        }

        .lighter {
            font-weight: lighter;
        }

        .widget-body {
            background-color: #FFF;
        }

        .list-inline > li > .ace-icon:first-child, .list-unstyled > li > .ace-icon:first-child {
            width: 18px;
            text-align: center;
        }

        .green {
            color: #69AA46 !important;
        }

        .ace-icon {
            text-align: center;
        }

        .fa {
            display: inline-block;
            font: normal normal normal 14px/1 FontAwesome;
            font-size: 14px;
            font-size: inherit;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

        .fa-2x {
            font-size: 17px;
        }
        .fa-check:before {
    content: "\f00c";
            }
        .pricing-box .price {
            font-size: 22px;
            line-height: 20px;
            height: 28px;
            text-align: center;
            color: #555;
        }

        .pricing-box .btn {
            font-size: 16px;
        }

        .btn-inverse, .btn-inverse.focus, .btn-inverse:focus {
            background-color: #69949d !important;
            border-color: #69949d;
        }

        element {
        }

        *, ::after, ::before {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        .pricing-box .btn {
            font-size: 16px;
        }

        .btn {
            color: #FFF !important;
            text-shadow: 0 -1px 0 rgba(0,0,0,.25);
        }

        btn-inverse, .btn-inverse.focus, .btn-inverse:focus {
            background-color: #69949d !important;
            border-color: #69949d;
        }

        .impExp {
            font-size: 14px;
        }

        .btn-successImport {
            color: #fff;
            background-color: #6d9b6f;
            border-color: #6d9b6f;
            border-radius: 9px;
            cursor: pointer;
        }

        .btn-successExport {
            color: #fff;
            background-color: #d5652e;
            border-color: #d5652e;
            border-radius: 9px;
            cursor: pointer;
        }

        .lastImport {
            padding-left: 2px;
            margin-left: 20px;
            font-size: 15px;
            margin-top: 10px;
        }

        .GridSearchWithoutAlgnImport th {
            background-color: #6d9b6f;
            padding: 7px;
            color: rgb(255, 255, 255);
            border: 1px solid #bed7f5;
            font-size: 13px;
            font-weight: 600;
        }

        .th {
            text-align: left;
        }

        *, ::after, ::before {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        .PopupHeaderExport {
            background-color: #0F3A5D;
            width: 871px;
            height: 28px;
            border-radius: 2px;
        }
        .PopupHeaderImport{
            background-color: #0F3A5D;
            width: 998px;
            height: 26px;
            border-radius: 2px;
        }
        .center-container {
            position: relative;
            top: 1%;
            left: 1%;
            transform: translate(-20%, -18%);
            }
        .center-containerExport {
            position: absolute;
            top: 1%;
            left: 1%;
            transform: translate(1%, -15%);
            }
        .GridSearchWithoutAlgn {
    width: 100%;
    border: 1px solid #dbdec0;
    }
        .GridSearchWithoutAlgn th {
        background-color: #1b4f72; /*#438EB9;*/
        padding: 7px;
        color: rgb(255, 255, 255);
        border: 1px solid #bed7f5; /*#2e729a;*/
        font-size: 13px;
        font-weight: 600;
        text-align: left;
        top: 0;
        position: sticky;
        outline: 1px solid #bed7f5;
    }

    .GridSearchWithoutAlgn td {
        padding: 5px 2px;
        border: 1px solid #bed7f5; /*#9BAFBA;*/
        color: #34383c;
    }

    .GridSearchWithoutAlgn tr:hover {
        background-color: rgb(236,236,236);
    }

    .GridSearchWithoutAlgn .pgr td {
        padding: 0px;
    }

        .GridSearchWithoutAlgn .pgr td:hover {
            background-color: #ffffff;
        }

        .GridSearchWithoutAlgn .pgr td td {
            padding: 5px;
            background-color: #e9ebec;
            font-weight: bold;
            color: #ff5454;
        }

   .GridSearchWithoutAlgn .pgr td td a {
                color: #34583C;
            }
     
        .green {
    color: #69AA46 !important;
}
  ul.remove-markers {
  list-style-type: none;
}    
  .contentul {
    width: 100%;
    padding: 3px 0 0 5px;
    margin: 15px 0 0 20px;
}
  .imgPopup {
    margin: -4px 0px 0px 100px;
    float: right;
}
     .thead th h2 {
        font-weight: 100;
        font-size: 1.3em;
        line-height: 1.2;
        color: #1a5d8a;
        }
     /*.toast-middle-right {
    right: 50px !important;
    bottom: 30px !important;
}*/
    /*.toast-middle-right {
        right: 50% !important;
        bottom: 50% !important;
}*/
        /*.toast  .toast-error
        {
            top:70% !important;
            left:1600px !important;
        }*/
       .toast
        {
            margin-left:83% !important;
            top:-690px !important;
            /*left: 200px !important;*/
            bottom: 1px !important;
        }
       .fixed-header {
    position: fixed;
    top: 0;
    background-color: #f2f2f2; /* Add your desired background color */
}
    </style>
    <script type="text/javascript">
        toastr.options = {
            positionClass: "toast-top-center", // or "toast-top-left"
            // Other configuration options
        };
        function loadexpData() {
    
            $("#btnimpimpex").click();

            return true;
        }
    </script>
</head>
<body style="background-color:#fff" >
    <form id="form1" runat="server"  autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                  <div class="container">
                 <div class="btn_bg5"> 
                     <div class="heading2">
                                    <div class="user">Cloud Linking</div>
                               <asp:Panel ID="divMenuButtons" runat="server">
                                     <div style="float: right; margin-top: -10px; display:none">
                                         <asp:ImageButton ID="ImageButton2" ImageUrl="images/ADD.png" Width="37" Height="20" onmouseout="this.src='../../images/ADD.png'"   onmouseover="this.src='../../images/ADDOver.png'"  TabIndex="13" runat="server"  />
                                         <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="20" TabIndex="12" onmouseout="this.src='../../images/view.png'"   onmouseover="this.src='../../images/viewOver.png'" runat="server" />
                                            </div>
                                   </asp:Panel>
                            
                        </div>
                      <%--<div class="heading2">
                           <div class="user">Cloud Linking</div>
                    <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                     
                    </div>
                        </asp:Panel>
                         </div>--%>
                     <div class="clear"></div>
                      <%-- <div class="group_bg2">--%>
                      <div  class="group_bg2">
                        <div class="main_bg"> 
                    
                        <div id="divAdd" runat="server" style="display: block;">
                        <ul class="contentul " id="Container">
                      <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 pricing-box ">

                     
                            <div class=" widget-box widget-color-dark">
                                        <div class="widget-header">
                                            <h5 class="widget-title bigger lighter">Import</h5>
                                        </div>
                                          <asp:HiddenField ID="hdnIDProcessid" runat="server" />
                                     <asp:HiddenField ID="hdnEXpIDProcessid" runat="server" />
                                  <asp:HiddenField ID="hdnErrorCode" runat="server" />
                                <div style="display:none">  <asp:Button ID="btnimpimpex"  runat="server" OnClick="btnimpimpex_Click" /> </div> 
                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                             
                                                        <spam style="padding-left: 4%; font-size: 20px;">Masters</spam>
                                                       
                                                        <ul class="remove-markers">
                                                            <li class=" impExp">
                                                                <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                                Customer
                                                            </li>
                                                            <li class="impExp">
                                                                <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                                Account Head
                                                            </li>
                                                            <li class="impExp">
                                                                <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                                Account Group And Sub Group
                                                            </li>
                                                            <li class="impExp">
                                                                <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                                Other Masters
                                                            </li>
                                                            <li class="impExp">
                                                                  <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                                Supplier
                                                            </li>
                                                             <li class="impExp">
                                                                  <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                                Agent
                                                            </li>
                                                           
                                                            <li class="impExp">&nbsp;
                                                                 &nbsp;
                                                            </li>
                                                        </ul>
                                                 


                                                </ul>
                                                <hr />
                                              
                                                <div>
                                                    <spam style="padding-left: 4%; font-size: 20px;">History</spam>
                                                </div>
                                                <div style="margin-top: 20px;">

                                                    <div id="divHtmlTable" class="col-lg-12 col-md-12 col-xs-12 col-sm-12" style="float: left; height: 310px;padding-left: 20px;">
                                                        <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12" style="overflow: auto; height: 300px; scrollbar-face-color: blue; scrollbar-arrow-color: blue; overflow-y: scroll;">

                                                            <asp:GridView ID="gvImport" runat="server" CssClass="GridSearchWithoutAlgn " OnRowDataBound="gvImport_RowDataBound"
                                                                EmptyDataText="No Data Found!" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                CellPadding="3" ForeColor="Black" GridLines="Vertical" HeaderStyle-CssClass="GVFixedHeader"
                                                                PageSize="100">

                                                                <Columns>
                                                  
                                                                    <asp:TemplateField HeaderText="SL NO"  ItemStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox21" runat="server" Text='<%# Bind("SLNO") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSLNO" runat="server" Text='<%# Bind("SLNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox41" runat="server" Text='<%# Bind("Date") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                           
                                                                            <asp:Label ID="lblgvDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Time">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox68" runat="server" Text='<%# Bind("Time") %>' Style="text-align: left;"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvTime" runat="server" Text='<%# Bind("Time") %>' Style="text-align: left; float: left;"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="By">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox67" Style="width: 20px; text-align: left;" runat="server" Text='<%# Bind("By") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblgvBy" runat="server" Text='<%# Bind("By") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Process ID">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtImpID" Style="width: 20px; text-align: left;" runat="server" Text='<%# Bind("ProcessId") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblgvImpID" runat="server" Text='<%# Bind("ProcessId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtgvStatus" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdStatusbitImport" runat="server" Value='<%# Bind("Statusbit") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>



                                                                </Columns>
                                                                 
                                                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                                </div>


                                            </div>
                                            <div style="text-align: left; font-size: 20px; margin: 4%;">
                                                <i class='fa fa-file-import' style='color: #f3da35'></i>
                                                <asp:Button ID="btnImport" runat="server" Text="Import" CssClass="btn-successImport" OnClick="btnImport_Click" />
                                            </div>
                                            
                                        </div>
                                       <%-- <div class=" btn btn-block btn-inverse" style="padding: 0px;">
                                        </div>--%>
                              </div>      
                        </div>
                       
                             <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6 pricing-box ">

                                 <div class="widget-box widget-color-dark">
                                  
                                        <div class="widget-headerHor">
                                            <h5 class="widget-title bigger lighter">Export</h5>
                                        </div>

                                        <div class="widget-body">
                                            <div class="widget-main">
                                                <ul class="list-unstyled spaced2">
                                                    <li>
                                                        <spam style="padding-left: 4%; font-size: 20px;">Accounts Entry</spam>
                                                        <ul class="remove-markers">
                                                            <li class="impExp">
                                                                <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                                Purchase
                                                            </li>
                                                            <li class="impExp">
                                                                <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                                Sale (Counter Close)
                                                            </li>
                                                            <li class="impExp">
                                                                <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                                Purchase Return
                                                            </li>
                                                            <li class="impExp">
                                                                <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                                Sales Return
                                                            </li>
                                                            <li class="impExp">
                                                                <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                                Credit Sales
                                                            </li>
                                                            <li class="impExp">
                                                                <i class="fa fa-2x fa-check green   " aria-hidden="true"></i>
                                                               Supplier
                                                            </li>
                                                          
                                                              <li class="impExp">&nbsp;
                                                                 &nbsp;
                                                            </li>
                                                        </ul>
                                                        <div class="clearfix"></div>
                                                    </li>




                                                </ul>

                                                <hr />
                                                <div>
                                                    <spam style="padding-left: 4%; font-size: 20px;">History</spam>
                                                </div>
                                                <div style="margin-top: 20px;">
                                                    <div id="div1" class="col-lg-12 col-md-12 col-xs-12 col-sm-12" style="float: left; height: 310px;">
                                                        <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12" style="overflow: auto; height: 300px; scrollbar-face-color: blue; scrollbar-arrow-color: blue; overflow-y: scroll;">

                                                            <asp:GridView ID="gvExport" runat="server" CssClass="GridSearchWithoutAlgn " OnRowDataBound="gvExport_RowDataBound"
                                                                EmptyDataText="No Data Found!" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                CellPadding="3" ForeColor="Black" GridLines="Vertical" HeaderStyle-CssClass="GVFixedHeader"
                                                                PageSize="100">

                                                                <Columns>
                                                                   
                                                                    <asp:TemplateField HeaderText="SL NO" ItemStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox21" runat="server" Text='<%# Bind("SLNO") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvSLNO" runat="server" Text='<%# Bind("SLNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox41" runat="server" Text='<%# Bind("Date") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                       
                                                                            <asp:Label ID="lblgvDate" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Time">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox68" runat="server" Text='<%# Bind("Time") %>' Style="text-align: left;"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvTime" runat="server" Text='<%# Bind("Time") %>' Style="text-align: left; float: left;"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="By">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox67" Style="width: 30px; text-align: left;" runat="server" Text='<%# Bind("By") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblgvBy" runat="server" Text='<%# Bind("By") %>' Style="text-align: left; float: left;"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Process ID">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtExpID" Style="width: 20px; text-align: left;" runat="server" Text='<%# Bind("ProcessId") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:Label ID="lblgvExpID" runat="server" Text='<%# Bind("ProcessId") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtgvStatus" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>

                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdStatusbitExport" runat="server" Value='<%# Bind("Statusbit") %>' />


                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>



                                                                </Columns>
                                                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>
                                            <div style="text-align: left; font-size: 20px; margin: 4%;">
                                                <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn-successExport" OnClick="btnExport_Click" />
                                            </div>
                                           <%-- <div class=" btn btn-block btn-inverse" style="padding: 0px;">--%>
                                            </div>
                                        </div>
                                    </div>
                            </div>
                            <div class="clear"></div>
                            <%-- <div class="divSeparation"></div>--%>
                            <div class="clear"></div>
                             <%-- <li class="clear"></li>   --%>
                            <div class="clear"></div>
                          <%--  <div class="divSeparation"></div>--%>
                            <asp:HiddenField ID="hfClient" runat="server" />
                              <asp:HiddenField ID="hfUser" runat="server" />
                             </ul>
                          </div>
                              
                         

                        </div>
                          
                          <%--  <div class="main_bg">     
                        </div>--%>


                      <div class="clearfix" ></div>
                                <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                                <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 798px; height: 520px; height: 540px;">
                                    <div id="divheader" runat="server" class="PopupHeader">
                                        <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head" CssClass="lblPopupHeader"></asp:Label>
                                        <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                    </div>

                                    <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                                </asp:Panel>
                                <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                                    CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                                </cc:ModalPopupExtender>
                                  <div class="clearfix" ></div>
                                <asp:Button ID="btnImportpopup" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                               
                                <asp:Panel ID="divImportCharge" runat="server" CssClass="center-container"  Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 798px; height: 760px; max-height:810px ">
                                    <div id="divCloudImport" runat="server" class="PopupHeaderImport">
                                        <asp:Label ID="lblCloudImport" runat="server" Text="" CssClass="lblPopupHeader"></asp:Label>
                                        <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                    </div>

                                     <Uc2:CommonPopupCloudImport ID="CommonPopupImport" runat="server" />
                                  
                                </asp:Panel>
                                <cc:ModalPopupExtender ID="mpCloudImport" runat="server" PopupControlID="divImportCharge" TargetControlID="btnImportpopup"
                                    CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divImportCharge">
                                </cc:ModalPopupExtender>
                                  <div class="clearfix" ></div>
                                 <asp:Button ID="btnExportpopup" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                              <asp:Panel ID="divExportCharge"  runat="server" CssClass="center-containerExport" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 900px; max-height:780px;height:810px;  ">
                                    <div id="divCloudExport" runat="server" class="PopupHeaderExport ">
                                        <asp:Label ID="lblCloudExport" runat="server" Text="" CssClass="lblPopupHeader"></asp:Label>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                    </div>

                                    <Uc3:CommonPopupCloudExport ID="CommonPopupExport" runat="server" />
                                  
                                </asp:Panel>

                                
                                 <cc:ModalPopupExtender ID="mpCloudExport" runat="server" PopupControlID="divExportCharge" TargetControlID="btnExportpopup"
                                    CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divExportCharge">
                                </cc:ModalPopupExtender>

                                <div class="clearfix"></div>



                                  <asp:Button ID="btnMessagepopup" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                                <asp:Panel ID="divImportMessage" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 598px; height: 520px; ">
                                    <div id="divMessageImport" runat="server" class="PopupHeaderExport" style ="width: 598px;">
                                        <asp:Label ID="lblMsgImport" runat="server" Text="" CssClass="lblPopupHeader"></asp:Label>
                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                    </div>

                                 

                                </asp:Panel>
                                <cc:ModalPopupExtender ID="mpMsgImport" runat="server" PopupControlID="divImportMessage" TargetControlID="btnMessagepopup"
                                    CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divImportMessage">
                                </cc:ModalPopupExtender>
                                <div class="clearfix"></div>

                           </div>


                         </div>
                                

                 



                             </div>  
                </ContentTemplate>

              </asp:UpdatePanel>
          
    
    </form>
    <script type="text/javascript">
        function hideModalPopup() {
            var cloudExportPopup = $find('<%= mpCloudExport.ClientID %>');
              if (cloudExportPopup) {
                  cloudExportPopup.hide();
              }
          }
    </script>
</body>
</html>
