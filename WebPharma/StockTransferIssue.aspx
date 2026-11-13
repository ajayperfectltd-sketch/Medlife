<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockTransferIssue.aspx.cs" Inherits="StockTransferIssue" EnableEventValidation="false" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Purchase Order::</title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <script src="js/Navigation.js"></script>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="frmPurchaseOrder" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">Stock Transfer Issue</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" TabIndex="29" runat="server" OnClientClick="ShowAddDiv();" OnClick="ImageButton1_Click" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="39" runat="server" OnClick="imgbtnView_Click" CausesValidation="false" />
                        </div>
                    </div>
                    <%--  <div class="user_bg">
                        <div class="user1">Purchase Order</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: none;">
                            <ul class="contentul" id="Container">
                                <div class="divL">
                                    <li>
                                        <label class="label">Date</label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtDate" TabIndex="1" AutoComplete="off" />
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                  
                                     <li>
                                        <label class="label">From Store</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore" TabIndex="2" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged"  AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqvalStore" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqvalStore">
                                        </cc:ValidatorCalloutExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Request"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </cc:ValidatorCalloutExtender>
                                    
                                    </li>

                                   
                                </div>
                                <div class="divR">
                                   

                                    <li>
                                         <label class="label">Request No</label>
                                         <asp:TextBox ID="txtRequestRefNo" runat="server" AutoPostBack="false" CssClass="textboxMandatory" TabIndex="145" Style="display: none;" onKeypress="CheckInteger(event);" Width="10px" onblur="txtInput_onblur();"></asp:TextBox>
                                        <asp:TextBox ID="txtRequestNo" runat="server" CssClass="textboxMandatory" TabIndex="3" Style="margin-left: 0px;"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnRequestNo" runat="server" OnClick="imgbtnRequestNo_Click" CausesValidation="false" OnClientClick="ClearRequestNo();return true;" TabIndex="4" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: -78px 0px 0px 38px; position: absolute; top: 150px; left: 830px; height: 26px;" ValidationGroup="Request" />
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Stock Request No required" ControlToValidate="txtRequestNo" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                      <li>
                                        <label class="label">To Store</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlToStore" TabIndex="5" OnSelectedIndexChanged="ddlToStore_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Store required" ControlToValidate="ddlToStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="From store and To Store should be different" ControlToCompare="ddlToStore" ControlToValidate="ddlStore" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="NotEqual" SetFocusOnError="True" Type="Integer" ValidationGroup="submit"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="CompareValidator2">
                                        </cc:ValidatorCalloutExtender>
                                     
                                    </li>



                                </div>
                                <li class="clear"></li>
                                <div class="divSeparation"></div>
                                <li class="clear"></li>

                                <br class="clear" />

                                <li class="clear"></li>
                                <div style="width: 940px; overflow: auto; height: 150px; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">
                                    <li>
                                        <asp:GridView ID="gvItems" runat="server" CssClass="grid"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Width="925px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="FK_Items" OnRowDeleting="gvItems_RowDeleting">
                                            <AlternatingRowStyle BackColor="#CCE0EB" />
                                            <Columns>
                                                <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Item" HeaderText="Items" />
                                                <asp:BoundField DataField="FK_Items" HeaderText="FK_Items" Visible="false" />                                               
                                                <asp:BoundField DataField="FK_Stock" HeaderText="FK_Stock" Visible="false" />
                                                <asp:BoundField HeaderText="Request Quantity" DataField="RequestQuantity" />
                                                   <asp:BoundField HeaderText="Balance Quantity" DataField="Quantity" />
                                                <asp:TemplateField HeaderText="Return Quantity" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="gvtxtQuantity"   AutoComplete="off" CssClass="textbox" Style="width: 120px; text-align: right;"  onchange="CalculateQuantity(id);" onkeypress="CheckNumWithDecimal(event);"  TabIndex="6"  Text='<%# Eval("CurrentStock") %>' />
                                                    <asp:HiddenField ID="hdfQty" runat="server" Value='<%# Eval("CurrentStock") %>' />     
                                                     <asp:HiddenField ID="hdfFK_Items" runat="server" Value='<%# Eval("FK_Items") %>' />
                                                       <asp:HiddenField ID="hdfBalanceQuantity" runat="server" Value='<%# Eval("Quantity") %>' />     
                                                      
                                                                      
                                                </ItemTemplate>
                                               </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                                </div>

                                <li class="clear"></li>
                                <div class="divSeparation"></div>
                                <li class="clear"></li>
                                <div class="divL">
                                    <ul style="margin: 0px 0px 0px 0px;">
                                        <li style="display:none;">
                                            <label class="label">Remark</label>
                                            <asp:TextBox runat="server" ID="txtRemark" AutoComplete="off" TextMode="MultiLine" MaxLength="500" TabIndex="7" />
                                        </li>


                                    </ul>
                                </div>
                                <div class="divR">

                                    <li class="btnMargin">
                                        <asp:HiddenField ID="hdfPurcahseOrder" runat="server" />
                                        <asp:HiddenField ID="hdfFkItem" runat="server" />
                                        <asp:HiddenField ID="hdfFKStock" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="8" OnClientClick="disablebutn();" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" OnClick="btnClear_Click" TabIndex="9" />

                                    </li>
                                </div>
                            </ul>
                        </div>
                        <div class="clear"></div>
                        <div id="divView" runat="server" style="display: none;">
                            <%-- style="display: none;"--%>
                            <ul class="contentul">
                                <div class="divL">
                                    <li>
                                        <label class="label">Store</label>
                                        <asp:DropDownList ID="ddlStoreSearch" runat="server" CssClass="dropdown" TabIndex="30"></asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="label">From Date</label>
                                        <asp:TextBox ID="txtFromDate" CssClass="textbox" runat="server" TabIndex="31" AutoComplete="off"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <label class="label">To Date</label>
                                        <asp:TextBox ID="txtTodate" CssClass="textbox" runat="server" TabIndex="32" AutoComplete="off"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtTodate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                    </li>
                                    <li class="clear"></li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="36" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="37" />
                                    </li>
                                </div>
                                
                            </ul>
                            <br class="clear" />
                            <ul class="contentul">
                                <asp:GridView ID="gvPurchaseOrder" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100"
                                    DataKeyNames="ID_Indent" OnSelectedIndexChanging="gvPurchaseOrder_SelectedIndexChanging" OnRowCommand="gvPurchaseOrder_RowCommand" >
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Store" HeaderText="Store" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" />                                    
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton145" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>

                                                <asp:ImageButton ID="ImageButton343" runat="server" CommandName="Print" ToolTip="Print" ImageUrl="~/images/1430738477_report.png" Width="20px" Height="20px" />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton269" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                        <%-- <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server"  />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                    </div>



                     <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                       <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 300px; height: 370px;">
                        <div id="divheader" runat="server" class="PopupHeader">
                             <asp:Label ID="lblPopupHeader" runat="server" Text="Customer"  CssClass="lblPopupHeader"></asp:Label>
                            <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                        </div>

                        <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                    </asp:Panel>
                      <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                        CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                    </cc:ModalPopupExtender>

                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/jquery-1.10.2.js"></script>
        <script src="js/Custom.js"></script>
        <script src="js/toastr.js"></script>
        <script type="text/javascript">

            function ClearRequestNo() {
                $("#txtRequestRefNo").val('');
                $("#txtRequestNo").val('');
               
            }
            function ClearAdd() {
                submit = 0;
                $("#Container").find("[id*='txt']").val('');
                $("#hdfPurcahseOrder").val('');
                var date = new Date().format("dd/MM/yyyy");
                $("#txtDate").val(date);
                $("#ddlStore").val('0');
                $("#ddlToStore").val('0');
                
                return false;
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

          

            function ClearSearch() {
                try
                {
              
                $("#ddlStoreSearch").val('0');
                var date = new Date().format("dd/MM/yyyy");
                $("#txtDate").val(date);
                $("#txtFromDate").val(date);
                $("#txtTodate").val(date);
                
                }
                catch (e) {
                    alert(e);
                }

            }
            function ShowViewDiv() {
                $("#divAdd").hide();
                $("#divView").show();
            }
            function ShowAddDiv() {

                try {

                    $("#divAdd").show();
                    $("#divView").hide();

                }
                catch (e) {
                    alert(e);
                }
            }

            $(function () {
                try {
                 

                    var ctrContainer = "form";
                    setKeynavigation(ctrContainer);

                }
                catch (e) {
                    alert(e);
                }
            });
            function pageLoad() {
                var ctrContainer = "form";
                setKeynavigation(ctrContainer);
               
            }

          
            function CalculateNetAmt() {
                var sum = $("#txtTotalAmount").val() == '' ? '0' : $("#txtTotalAmount").val();
                var Disc = $("#txtRoundAmount").val() == '' ? '0' : $("#txtRoundAmount").val();

                if (((parseFloat(sum) + parseFloat(Disc))) < 0) {
                    $("#txtRoundAmount").val('0');
                    $("#txtNetAmount").val(sum);
                }
                else {
                    $("#txtNetAmount").val((parseFloat(sum) + parseFloat(Disc)).toFixed(3));
                }

            }

            function ClientValidate(source, arguments) {
                try {
                    var date = document.getElementById("txtDate").value;
                    var arr = date.split('/');
                    var start = new Date(arr[2], arr[1], arr[0]);
                    var arr2 = arguments.Value.split('/');

                    var endDate = new Date(arr2[2], arr2[1], arr2[0]);

                    if (start > endDate) {
                        arguments.IsValid = false;
                    }
                    else {
                        arguments.IsValid = true;
                    }

                }
                catch (e) {
                    alert(e);
                }

            }
            var popup;
            function SelectName() {
                popup = window.open("ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val(), "Popup", "width=1000,height=800");
                popup.focus();


                return false;
            }
            function DecimalOrIntValidation(eventRef) {
                if ($("#hfPacked").val() == "False") {
                    CheckNumWithDecimal(eventRef);
                }
                else {
                    CheckInteger(eventRef);
                }
            }



            function popUpStockSelectClick() {

                try {
                    var ItemName = $("#txtItemShortName").val();
                    var Store = $("#ddlStore").find("option:selected").val();

                    url = "StockItems.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=0&str=' + Store;

                    popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                    ",statusbar=no,menubar=no,resizable=0,width=1000," +
                    "height=600,left = 200,top = 100");
                    popUpObj.focus();

                    //   $("#txtItemShortName").focus();
                }
                catch (e) {
                    alert(e);
                }

            }
            function popUpStockSelect() {

                try {
                    var Cat = $("#ddlCategory").val();

                    var ItemName = $("#txtItemShortName").val();

                    if (ItemName.length > 0) {
                        url = "StockItems.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=0&str=' + Store;

                        popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                        ",statusbar=no,menubar=no,resizable=0,width=1000," +
                        "height=600,left = 200,top = 100");
                        popUpObj.focus();
                    }                   
                
                }
                catch (e) {
                    alert(e);
                }

            }

            function CalculateQuantity(id) {
                try {
                  
                    var txtQuantity = id;                
                    var hdfQty = id.replace('gvtxtQuantity', 'hdfQty');
                    var hdfBalanceQuantity = id.replace('gvtxtQuantity', 'hdfBalanceQuantity');
                    var qty = $("#" + txtQuantity).val() == '' || $("#" + txtQuantity).val() == null ? "0" : parseFloat($("#" + txtQuantity).val());
                    var prevqty = $("#" + hdfQty).val();
                    var Balqty = $("#" + hdfBalanceQuantity).val();
                  
                    if (parseFloat(qty) > parseFloat(prevqty)) {
                        alertify.alert("Quantity Cannot Exceed Current Stock");
                        $("#" + txtQuantity).val(parseFloat(prevqty));

                    }
                    else if (parseFloat(qty) > parseFloat(Balqty)) {
                        alertify.alert("Quantity Cannot Exceed Balance Quantity");
                        $("#" + txtQuantity).val(parseFloat(prevqty));

                    }
                }
                catch (e) {
                    alert(e);
                }
            }
        </script>
    </form>
</body>
</html>
