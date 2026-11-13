<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockAdjustment.aspx.cs" Inherits="StockAdjustment" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register src="Popup/CommonPopup.ascx" tagname="CommonPopup" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Stock Adjustment::</title>
    <link href="css/main.css" rel="stylesheet" />

    <link href="css/toastr.css" rel="stylesheet" />
    <script src="AssetLogin/js/jquery.min.js"></script>
    <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        padding-top:250px;
        min-height: 100%;
        width: 100%;
        display: none;
        position: fixed;
        opacity:  0.5;
        filter: alpha(opacity=50);
        -moz-opacity: 0.5;
        background-color: White;
        z-index: 999;
    }
</style>
</head>
<body>
    <form id="frmItemConversion" runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        

                         <div class="search" style="width:670px; " >Stock Adjustment</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/add1.png" TabIndex="25" Width="37" Height="38" runat="server" CausesValidation="false" OnClientClick="ShowAddDiv();  releasebtn();" OnClick="imgbtnAdd_Click" />
                        </div>

                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="35" runat="server" OnClick="imgbtnView_Click" />
                        </div>
                    </div>
                 <%--   <div class="user_bg">
                        <div class="user1">Item Conversion</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: none; height: 485px">




                            <ul class="contentul" id="Container">
                                <div class="divL" style="width: 150px;">
                                    <li>
                                        <label class="label" style="width: 40px;">Date</label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory"  style="width: 80px;" ID="txtDate" TabIndex="1" AutoComplete="off" />
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                        </cc:ValidatorCalloutExtender>
                                    </li>


                                  
                                    <li class="clear"></li>




                                </div>
                                <div class="divL" style="width: 200px;">
                                  
                                    <li>
                                        <label class="label" style="width: 50px;">Store</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" Width="120" ID="ddlStore" TabIndex="2"  >
                                        </asp:DropDownList>
                                       
<asp:RequiredFieldValidator ID="rfvStore" runat="server" ErrorMessage="Select a store" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit">

</asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfvStore">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                 

                                </div>
                                <div style="float: left; width: 220px;">
                                    <li>

                                              <label class="label" style="width: 70px;">Category</label>
                                             <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="3" Width="120px" CssClass="dropdown"  ></asp:DropDownList>
                                      
                                    </li>

                                 

                                </div>
                                <div style="float: left; width: 220px;">

                                        <li>
                                    <asp:Label ID="Label8" runat="server"  style="width: 40px;" CssClass="label" Text="Item"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtItemShortName" onBlur="popUpStockSelect();" Width="140px" MaxLength="150" TabIndex="3" />
                                    <asp:Button ID="btnItem" runat="server" Text="Item" Style="display: none;" />
                                    <asp:ImageButton ID="imgPopUp" runat="server" OnClientClick="return  popUpStockSelectClick();" TabIndex="4" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 0px 0px 0px 6px; position: absolute" />

                                </li>

                                </div>
                                    <div style="float: right; width: 150px;">
                                    <li>

                                                <asp:Button Text="Process" ID="btnProcess" runat="server" ValidationGroup="submit" OnClick="btnProcess_Click" TabIndex="4" CssClass="button" />
                                      
                                    </li>

                                 

                                </div>
                                <li class="clear"></li>

                                <div class="divSeparation"></div>



                                <div class="clear"></div>
                                <li class="clear"></li>

                                    <li style="width: 920px; padding-left:5px; height: 430px; max-height: 540px; overflow-y: auto;">


                                        <asp:GridView ID="gvItems" runat="server" CssClass="grid" DataKeyNames="ID_StockAdjustment,FK_Module,AdjustmentID"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="95%"
                                            BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical"
                                             PageSize="100" OnRowCommand="gvItems_RowCommand" >
                                            <AlternatingRowStyle BackColor="#CCE0EB" />
                                            <Columns>
                                               
                                                 <asp:TemplateField >
                                                     <HeaderTemplate>
                                                          <asp:CheckBox ID="chkSelect" onchange="CheckAll()"  ToolTip="Check All" runat="server" />
                                                     </HeaderTemplate>
                                                    <ItemTemplate>
                                                   <asp:CheckBox ID="chkSelectitem" onchange="CheckIsAdjustable(this)"  runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                               <asp:TemplateField HeaderText="Item" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdfItem" Value='<%#Eval("ID_Items") %>' runat="server" />

                                                        
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Module" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModule" runat="server" Text='<%#Eval("Module") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdfFK_Details" Value='<%#Eval("FK_Details") %>' runat="server" />
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                                  <asp:TemplateField HeaderText="Sales Price"  ItemStyle-HorizontalAlign="Right" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSalePriceTo" runat="server" Text='<%#Eval("SalesPrice") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Quantity"  HeaderStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQunatity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Batch No" ItemStyle-Width="20%" HeaderStyle-BackColor="#86c0f0" >
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtbatch" ReadOnly="true" Width="110px" runat="server" Text='<%#Eval("BatchNo") %>'></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" CommandName="PopUp" TabIndex="5" ImageUrl="~/images/serach_icon.png" Style="padding-left: 2px;width:16px;height:16px; margin-top: 0px;" ValidationGroup="img" />
                                                   <asp:HiddenField ID="hdfStockFrom" Value='<%#Eval("ID_Stock") %>' runat="server" />
                                                              <asp:HiddenField ID="hdfStockTo" Value='<%#Eval("ID_Stock") %>' runat="server" />
                                                           </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Price" HeaderStyle-BackColor="#86c0f0">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSalePriceFrom" runat="server" Text='<%#Eval("NewSalesPrice") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Adjustment Quantity" HeaderStyle-BackColor="#86c0f0" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAdjustmentQuantity" runat="server" Text='<%#Eval("AdjustmentQuantity") %>'></asp:Label>
                                                         <asp:HiddenField ID="hdfStoreFROM" Value='<%#Eval("FK_Store") %>' runat="server" />
                                                         <asp:HiddenField ID="hdfStoreTO" Value='<%#Eval("FK_Store") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                              
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                            <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>



                                    </li>




                     
                               <div class="divR">


                                    <li class="clear"></li>

                                    <li class="btnMargin">
                                        <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="6"  OnClientClick=" disablebutn(); ShowProgress();" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" CausesValidation="false" TabIndex="7" OnClick="btnClear_Click1" />
                                        
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfUserType" runat="server" />
                                    </li>
                                </div>

                            </ul>
                        </div>

                        <div class="clear"></div>
                        <div id="divView" runat="server" style="display: none; height: 485px">



                            <ul class="contentul">
                                <div class="divL">


                                    <li>
                                        <label class="label">Store</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStoreSearch" TabIndex="8">
                                        </asp:DropDownList>

                                            
<asp:RequiredFieldValidator ID="rfvStoreSearch" runat="server" ErrorMessage="Select a store" ControlToValidate="ddlStoreSearch" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="search">

</asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="rfvStoreSearch">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li>
                                        <label class="label">From Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtFromDate" TabIndex="10" AutoComplete="off" />
                                        <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>

                                    </li>
                                    <br class="clear" />
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" ValidationGroup="search" Text="Search" OnClick="btnSearch_Click" TabIndex="12" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="13" />
                                    </li>
                                </div>
                                <div class="divR">

                                    <li>

                                        <label class="label">Category</label>
                                         <asp:DropDownList ID="ddlCategorySearch" runat="server" TabIndex="9" CssClass="dropdown" ></asp:DropDownList>
                                    </li>

                                    <li>
                                        <label class="label">To Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtToDate" TabIndex="11" AutoComplete="off" />
                                        <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtToDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>

                                    </li>

                                </div>

                            </ul>
                            <br class="clear" />
                            <ul class="contentul" style="padding-left:50px">
                             <asp:GridView ID="gvStockAdjustment" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="90%"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="AdjustmentID,Transdate,FK_Store,FK_Category"
                                    OnSelectedIndexChanging="gvStockAdjustment_SelectedIndexChanging" >
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>

                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Category" HeaderText="Category" />
                                        <asp:BoundField DataField="Store" HeaderText="Store" />
                                        <asp:BoundField DataField="Transdate" HeaderText="Trans Date" />
                                   

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="View" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    
                                   
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                                <div class="Pager">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" OnClick="Page_Changed" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ul>
                        </div>
                        <div class="clear"></div>
                        
                        <asp:HiddenField ID="hdfDate" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                           <asp:HiddenField ID="hfgvItemsRowIndex" runat="server" />
                          <asp:HiddenField ID="hfItemValue" runat="server" OnValueChanged="hfItemValue_ValueChanged" />

                           <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                         <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 300px; height: 370px;">
                            <div id="divheader" runat="server" class="PopupHeader" >
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head"  CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>

                            <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                        </asp:Panel>
                          <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                        </cc:ModalPopupExtender>
                       </div>
                </div>
               
                    <div class="loading" align="center" >
    Processing. Please wait.<br />
    <br />
      <asp:Image id="Image1" runat="server" style="margin-top:50px;"
           AlternateText="Image text"
           ImageAlign="Middle"
           ImageUrl="~/images/loader2.gif"/>           
</div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <script src="js/jquery-1.10.2.js"></script>
        <script src="js/Custom.js"></script>
        <script src="js/toastr.js"></script>
        <script src="js/alertify.min.js"></script>
        <link href="css/alertify.core.css" rel="stylesheet" />
        <link href="css/alertify.default.css" rel="stylesheet" />
        <script src="js/Navigation.js"></script>
        <script src="AssetLogin/js/jquery.min.js"></script>
    <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        padding-top:180px;
        min-height: 100%;
        width: 100%;
        display: none;
        position: fixed;
        opacity:  0.5;
        filter: alpha(opacity=50);
        -moz-opacity: 0.5;
        background-color: White;
        z-index: 999;
    }
</style>
        <script type="text/javascript">


            function ClearAdd() {
                submit = 0;
                $("[id*='txt']").not("txtDate").val('');
                var d = new Date();
                $("#txtDate").val(d.format('dd/MM/yyyy'));
                $("#hfItemValue").val('0');
                $("#txtFromDate,#txtToDate").val(d.format('dd/MM/yyyy'));
                $("[id*='ddl']").val('0');
              

                $("#txtDate").focus();
                //  return false;
            }

            function popUpStockSelectClick() {

                try {
                 
                    var ItemName = $("#txtItemShortName").val();
                    url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#ddlStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val();
                    // popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=0' + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val(), "Popup", "width=1000,height=800") + '&ctgry=' + $("#ddlCategory").val();

                    popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=yes,location=no" +
                                         ",statusbar=no,menubar=no,resizable=0,width=1015," +
                                         "height=650,left = 200,top = 100");

                    popUpObj.focus();

                    //   $("#txtItemShortName").focus();
                }
                catch (e) {
                    alert(e);
                }

            }


            function ShowProgress() {
                if (Page_ClientValidate("submit") == true) {

                    var chk = false;
                    
                    $.each($("table[id*=" + 'gvItems' + "] tr"), function (index, item) {
                         
                        if ($(this).find("[id*='gvItems_chkSelectitem_']").is(':checked')) {
                            chk = true;
                        }

                    });
                        if (chk == true) {

                            setTimeout(function () {
                                //  var modal = $('<div />');
                                //  modal.addClass("modal");
                                ////  $('body').append(modal);

                                var loading = $(".loading");
                                loading.show();
                                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                                loading.css({ top: top, left: left });
                            }, 200);

                            return;
                        }
                }
            }

            function HideProgress() {

                var loading = $(".loading");
                loading.hide();

            }

            var submit = 0;
            function disablebutn() {
                try {

                    window.setTimeout('delaysubmit()', 50);
                }
                catch (e) {
                    alert(e);
                }
            }

            function releasebtn() {


                $("#btnSave").prop('disabled', false);

            }
            function delaysubmit() {
                try {
                    if (submit == 1) {
                        return false;

                    }

                    $("#btnSave").prop('disabled', true);

                    window.setTimeout('releasebtn()', 3000);
                    submit = 1;
                }
                catch (e) {
                    alert(e);
                }
            }

        

            function CheckKeys() {
                alert(e.keyCode);
            }

            function ClearSearch() {
                $("#ddlStoreSearch ").val("0");
                $("#ddlCategorySearch").val("0");
                var d = new Date();
                $("#txtFromDate,#txtToDate").val(d.format('dd/MM/yyyy'));

            }
            function ShowAddDiv() {

                $("#divAdd").show();
                $("#divView").hide();
            }
       
           
      
            function pageLoad() {
                Check();
                var ctrContainer = "form";
                setKeynavigation(ctrContainer);
               
            }




            function blockPostOnTab() {
                if (e.keyCode == 9)
                    return false;

            }
         
        
           

            function CheckAll() {

                var chk = $("#gvItems_chkSelect").prop('checked');
                var msg = '';
                $.each($("table[id*=" + 'gvItems' + "] tr"), function (index, item) {
                    if (chk == true) {
                        if ($(item).find("input[type=hidden]").length > 0) {
                            var qty = $(item).find("[id*='gvItems_lblQunatity_']").text();
                            var CurrentStock = $(this).find("[id*='gvItems_lblAdjustmentQuantity_']").text();

                            if (parseFloat(CurrentStock)==0) {
                                $(this).find("[id*='gvItems_chkSelectitem_']").prop('checked', false); 
                                msg = 'Item with Current Stock Zero Cannot Check';
                               // alertify.alert(" Cannot Check, Current Stock Zero");
                            }
                            else {
                                $(this).find("[id*='gvItems_chkSelectitem_']").prop('checked', chk);
                            }
                        }
                    }
                    else {
                        $(this).find("[id*='gvItems_chkSelectitem_']").prop('checked', chk);
                    }
                });
                if (msg != '') {
                    alertify.alert(msg);
                }

            }


            function CheckIsAdjustable(objRef) {
              
               
                var msg = '';
                $.each($("table[id*=" + 'gvItems' + "] tr"), function (index, item) {
                   
                        if ($(item).find("input[type=hidden]").length > 0) {
                        //    var qty = $(item).find("[id*='gvItems_lblQunatity_']").text();
                            var CurrentStock = $(this).find("[id*='gvItems_lblAdjustmentQuantity_']").text();

                            if (parseFloat(CurrentStock) == 0) {
                               
                               
                                if ($(this).find("[id*='gvItems_chkSelectitem_']").is(':checked')) {
                                    $(this).find("[id*='gvItems_chkSelectitem_']").prop('checked', false);
                                    msg = 'Item with Current Stock Zero Cannot Check';
                                }
                            }
                        }
                });
                if (msg != '') {
                    alertify.alert(msg);
                }
            }

        


        </script>


    </form>
</body>
</html>

