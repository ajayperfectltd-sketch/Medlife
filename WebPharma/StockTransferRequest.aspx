<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockTransferRequest.aspx.cs" Inherits="StockTransferRequest" EnableEventValidation="false" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
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
                        <div class="search" style="width: 670px;">Stock Transfer Request </div>
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
                                    <%-- <asp:Button ID="btn" CssClass="button" runat="server" OnClientClick="return SelectName();" />--%>


                                    <li class="btnMargin" style="display: none;">
                                        <asp:Button Text="Smart Fill" ID="btnSmartFill" runat="server" TabIndex="4" ValidationGroup="smartfill" CssClass="button" OnClick="btnSmartFill_Click" />
                                    </li>
                                </div>
                                <div class="divR">
                                    <li>
                                        <label class="label">Store</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore" TabIndex="2">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqvalStore" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqvalStore">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="smartfill"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li style="display: none">
                                        <label class="label">Delivery Date</label>
                                        <asp:TextBox ID="txtDelivaryDate" runat="server" CssClass="textbox" TabIndex="7" AutoComplete="off"></asp:TextBox>
                                        <cc:CalendarExtender ID="txtDelivaryDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDelivaryDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Delivery Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="csvDatecheck" runat="server" ErrorMessage="Delivery date can not be less than the Date" ClientValidationFunction="ClientValidate" ControlToValidate="txtDelivaryDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:CustomValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="csvDatecheck">
                                        </cc:ValidatorCalloutExtender>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <div style="width: 940px; overflow: auto; height: 30px; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">
                                        <li id="liSupplierBalance" runat="server" style="margin-top: 4px;">
                                            <asp:Label ID="lblSupplierBalance" Text="" CssClass="lblbalance" runat="server" />
                                        </li>
                                    </div>
                                </div>
                                <li class="clear"></li>
                                <div class="divSeparation"></div>
                                <li class="clear"></li>
                                <div class="divL">
                                    <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtItemShortName" onBlur="popUpStockSelect();" Width="130px" MaxLength="150" TabIndex="8" />
                                        <asp:Button ID="Button1" runat="server" Text="Item" Style="display: none;" />
                                        <asp:ImageButton ID="ImageButton3" runat="server" OnClientClick="return  popUpStockSelectClick();" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 0px 0px 0px 6px; position: absolute" />

                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Items</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlItem" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" Style="margin-top: 0px;">
                                        </asp:DropDownList>
                                        <asp:Button ID="btnItem" runat="server" Text="Item" OnClick="btnItem_Click" Style="display: none;" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Item required" ControlToValidate="ddlItem" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:ImageButton ID="imgPopUp" runat="server" OnClientClick="return SelectName();" ImageUrl="~/images/POS/icons_bg.jpg" Style="padding-left: 2px; margin-top: 0px; width: 30px; height: 25px;" />
                                    </li>

                                    <li style="display: none;">
                                        <label class="label">Sales Price 1</label>
                                        <asp:TextBox ID="txtSalesPrice" runat="server" Style="text-align: right;" CssClass="textbox" MaxLength="10" onKeypress="CheckNumWithDecimal(event);" TabIndex="188" AutoComplete="off"></asp:TextBox>
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Sales Price 2</label>
                                        <asp:TextBox ID="txtSalesPrice2" Style="text-align: right;" runat="server" CssClass="textbox" MaxLength="10" onKeypress="CheckNumWithDecimal(event);" TabIndex="199" AutoComplete="off"></asp:TextBox>
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Batch</label>
                                        <asp:TextBox ID="txtBatch" runat="server" CssClass="textbox" TabIndex="1000" AutoComplete="off"></asp:TextBox>
                                    </li>
                                </div>
                                <div class="divR">
                                    <li>
                                        <label class="label">Quantity</label>
                                        <asp:TextBox ID="txtQunatity" runat="server" CssClass="textboxMandatory" Style="text-align: right;" onKeypress="DecimalOrIntValidation(event);" MaxLength="10" TabIndex="10" AutoComplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQunatity" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQunatity" ErrorMessage="Enter valid Quantity" Display="None" MaximumValue="999999999999" ValidationGroup="Add" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li style="display: none;">
                                        <asp:Label ID="lblCurrentStock" runat="server" CssClass="label" Width="300px" Font-Bold="true" ForeColor="Red" Text="Current Stock=0"></asp:Label>
                                    </li>
                                    <li style="display: none;">
                                        <asp:Label ID="lblOrderedQuantity" runat="server" CssClass="label" Width="300px" Font-Bold="true" ForeColor="Red" Text="Ordered Quantity=0"></asp:Label>
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Discount</label>
                                        <asp:TextBox ID="txtDiscount" runat="server" Style="text-align: right;" CssClass="textbox" MaxLength="10" AutoComplete="off" onKeypress="CheckNumWithDecimal(event);" TabIndex="1743"></asp:TextBox>
                                    </li>

                                    <li style="display: none;">
                                        <label class="label">Notes</label>
                                        <asp:TextBox ID="txtNotes" runat="server" CssClass="multiline" TextMode="MultiLine" AutoComplete="off" MaxLength="250" TabIndex="1474"></asp:TextBox>
                                    </li>
                                </div>
                                <br class="clear" />
                                <li class="btnMargin">
                                    <asp:Button Text="Add" ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click" ValidationGroup="Add" TabIndex="12" OnClientClick="CalculateTotal(); releasebtn();" />
                                    <asp:Button Text="Clear" ID="btnClearAdd" runat="server" CssClass="button" OnClientClick="ItemClear();return false;" TabIndex="16" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfPacked" runat="server" />
                                </li>
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
                                                <asp:BoundField HeaderText="Quantity" DataField="Quantity" />

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
                                        <li>
                                            <label class="label">Remark</label>
                                            <asp:TextBox runat="server" ID="txtRemark" AutoComplete="off" TextMode="MultiLine" MaxLength="500" TabIndex="17" />
                                        </li>
                                        <li class="btnMargin">
                                            <asp:CheckBox ID="cbxIsClosed" runat="server" Text="Closed" TabIndex="18" />
                                            <asp:CheckBox ID="cbxPrint" runat="server" Text="Print" Checked="true" TabIndex="19" />
                                        </li>
                                        <li id="cbx" style="display: none;">
                                            <label class="label">Close Remark</label>
                                            <asp:TextBox ID="txtClosedRemark" runat="server" AutoComplete="off" TextMode="MultiLine" MaxLength="500" TabIndex="19" />
                                        </li>
                                        <%--<li class="btnMargin">
                                            <asp:CheckBox ID="cbxPrint" runat="server" Text="Print" Checked="true" TabIndex="20" />
                                        </li>--%>

                                    </ul>
                                </div>
                                <div class="divR">
                                    <li style="display: none;">
                                        <label class="label">Total Amount</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtTotalAmount" Style="text-align: right;" AutoComplete="off" MaxLength="10" onKeypress="CheckNumWithDecimal(event);" TabIndex="20" />
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Discount</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtDiscountLast" Style="text-align: right;" AutoComplete="off" MaxLength="10" onKeypress="CheckNumWithDecimal(event);" TabIndex="21" />
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Round Amount</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtRoundAmount" MaxLength="10" Style="text-align: right;" AutoComplete="off" onkeypress="CheckNumWithDecimalNegative(event);" TabIndex="24" />
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Net Amount</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtNetAmount" MaxLength="10" Style="text-align: right;" AutoComplete="off" onKeypress="CheckNumWithDecimal(event);" TabIndex="22" />
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Cash Amount</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtCashAmount" MaxLength="10" Style="text-align: right;" AutoComplete="off" onKeypress="CheckNumWithDecimal(event);" TabIndex="-1" />
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Advance</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtAdvance" MaxLength="10" Style="text-align: right;" AutoComplete="off" onKeypress="CheckNumWithDecimal(event);" TabIndex="25" />
                                    </li>
                                    <li class="btnMargin">
                                        <asp:HiddenField ID="hdfPurcahseOrder" runat="server" />
                                        <asp:HiddenField ID="hdfFkItem" runat="server" />
                                        <asp:HiddenField ID="hdfFKStock" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="26" OnClientClick="disablebutn();" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" OnClick="btnClear_Click" TabIndex="27" />
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
                                <div class="divR">
                                    <li>
                                        <label class="label">Status</label>
                                        <asp:DropDownList runat="server" CssClass="dropdown" ID="ddlClosedSearch" TabIndex="35">
                                            <asp:ListItem Text="select" Value="" />
                                            <asp:ListItem Text="Available" Value="false" />
                                            <asp:ListItem Text="Closed" Value="true" />
                                        </asp:DropDownList>
                                    </li>
                                    <br class="clear" />
                                </div>
                            </ul>
                            <br class="clear" />
                            <ul class="contentul">
                                <asp:GridView ID="gvPurchaseOrder" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100"
                                    DataKeyNames="ID_IndentRequest" OnRowDeleting="gvPurchaseOrder_RowDeleting" OnRowDataBound="gvPurchaseOrder_RowDataBound" OnSelectedIndexChanging="gvPurchaseOrder_SelectedIndexChanging"
                                    OnRowCommand="gvPurchaseOrder_RowCommand">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PONo" HeaderText="PO No" />

                                        <asp:BoundField DataField="Store" HeaderText="Store" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" />

                                        <asp:BoundField DataField="Status" HeaderText="Status" Visible="false" />
                                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnStatus" CssClass="button" Width="85px" Text='<%# Bind("Status") %>' OnClientClick="ConfirmUpdatePurchaseOrder()" runat="server" CommandName="StatusUpdate" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton145" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton269" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>

                                                <asp:ImageButton ID="ImageButton343" runat="server" CommandName="Print" ToolTip="Print" ImageUrl="~/images/1430738477_report.png" Width="20px" Height="20px" />

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
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/jquery-1.10.2.js"></script>
        <script src="js/Custom.js"></script>
        <script src="js/toastr.js"></script>
        <script type="text/javascript">
            function ClearAdd() {
                submit = 0;
                $("#Container").find("[id*='txt']").val('');
                $("#hdfPurcahseOrder").val('');
                var date = new Date().format("dd/MM/yyyy");
                $("#txtDate,#txtDelivaryDate").val(date);
                $("#ddlStore").val('0');
                $("#cbxPrint").prop("checked", true);

                //$("#cbxIsClosed").prop("checked", "");
                ItemClear();
                $('#liSupplierBalance').hide();
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

            function ItemClear() {
                $("#txtItemShortName").val("");
                $("#txtSalesPrice,#txtDiscount,#txtBatch,#txtQunatity,#txtNotes,#txtSalesPrice2").val('');
                $("#ddlItem").val("0");
                $("#cbxPrint").prop("checked", true);


                CalculateTotal();

            }

            function ClearSearch() {

                $("#<%#ddlStoreSearch.ClientID%>").val('0');

                $("#ddlClosedSearch").val("");


                $("#ddlStoreSearch").val('0');

                var date = new Date().format("dd/MM/yyyy");
                $("#txtFromDate,#txtTodate,#txtDate,#txtDelivaryDate").val(date);




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
                    // CalculateTotal();r

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
                //Check();


                //$("#cbxIsClosed").bind("change", function () {

                //    if ($("#cbxIsClosed").prop("checked") == true) {
                //        $("#cbx").fadeIn();
                //    }
                //    else {
                //        $("#cbx").fadeOut();
                //    }
                //});
                //if ($("#cbxIsClosed").prop("checked") == true) {
                //    $("#cbx").fadeIn();
                //}
                //else {
                //    $("#cbx").fadeOut();
                //}
                ////    CalculateTotal();
                //$("#txtRoundAmount").bind("change", function () {
                //    // CalculateNetAmt();
                //});
                //AmountAlert();
            }

            function CalculateTotal() {

                var sum = 0;
                $.each($("table[id*=" + 'gvItems' + "] tr"), function (index, item) {


                    var val = $(item).find("[id*=gvItems_lblAmount_]").text();

                    sum += parseFloat(val != null ? (val != '' ? val : '0') : '0');

                });

                $("#txtTotalAmount").val(sum.toFixed(3));

                CalculateNetAmt();
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
                    url = "StockItems.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=0&str=0';
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
                    var ItemName = $("#txtItemShortName").val();
                    if (ItemName.length > 0) {
                        url = "StockItems.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=0&str=0';
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




        </script>
    </form>
</body>
</html>
