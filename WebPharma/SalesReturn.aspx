<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesReturn.aspx.cs" Inherits="SalesReturn" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopUpGridSelection.ascx" TagName="CommonPopupGrid" TagPrefix="uc3" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%--<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc1" TagName="MedLifePageHelp" %>--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>:: Sales Return ::</title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <style>
        .hiddencol {
            display: none;
        }
        .fixed-remark {
    width: 400px;
    height: 100px;
    resize: none; /* stops user from dragging it into another dimension */
}

.remark-count {
    font-size: 11px;
    color: gray;
    margin-top: 4px;
}
    </style>
</head>

<body>
    <form id="frmSalesReturn" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="user">Sales Return</div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <%--<uc1:MedLifePageHelp runat="server" ID="MedLifePageHelp" />--%>
                                <asp:ImageButton ID="ImageButton1" CssClass="imagebuttonStyle" ImageUrl="images/ADD.png" TabIndex="29" runat="server" CausesValidation="false" OnClientClick="ShowAddDiv();  releasebtn();" OnClick="ImageButton1_Click" />
                                <asp:ImageButton ID="imgbtnView" CssClass="imagebuttonStyle" ImageUrl="images/view.png" TabIndex="30" runat="server" OnClick="imgbtnView_Click" />
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                            <div id="divAdd" runat="server" style="display: block;">

                                <ul class="contentul" id="Container">
                                    <div class="divL" style="width: 400px;">
                                        <li>
                                            <label class="label">Date</label>
                                            <asp:TextBox runat="server" Width="70px" CssClass="textboxMandatory" ID="txtDate" TabIndex="1" AutoComplete="off" />
                                            <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:Label ID="lblCredit" runat="server"
                                                Style="font-family: Tahoma; font-size: 13px; padding-left: 2px; width: 135px; padding-left: 32px;" Text="Credit"></asp:Label>
                                            <asp:CheckBox ID="cbxIsCredit" runat="server" Style="padding-left: 15px;" TabIndex="2" />
                                        </li>
                                        <li>
                                            <label class="label">Customer</label>
                                            <div style="display: none">
                                                <asp:TextBox ID="txtCustRefno" runat="server" AutoPostBack="false" onblur="txtCustRefno_onblur()"
                                                    CssClass="textbox" TabIndex="3" onKeypress="CheckInteger(event);" Width="70px"></asp:TextBox>
                                            </div>
                                            <asp:TextBox ID="txtCustomer" runat="server" Width="190px" CssClass="textbox" TabIndex="5"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnCustomer" runat="server" OnClick="imgbtnCustomer_Click" TabIndex="4"
                                                ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 0px 0px; position: absolute" />
                                             <asp:CustomValidator ID="cvCustomerCredit" runat="server" ErrorMessage="Customer required when Credit is checked"
                                                Display="None" SetFocusOnError="True" ValidationGroup="submit"
                                                ClientValidationFunction="ValidateCustomerCredit" OnServerValidate="cvCustomerCredit_ServerValidate">
                                            </asp:CustomValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtenderCustCredit" runat="server" Enabled="True" TargetControlID="cvCustomerCredit">
                                            </cc:ValidatorCalloutExtender>
                                        </li>
                                        <li>
                                            <asp:Label class="label" ID="lbl" runat="server" Text="Store" Width="136px"></asp:Label>
                                            <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore" TabIndex="6">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="reqvalStore" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqvalStore">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="imgSales"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                            </cc:ValidatorCalloutExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="img"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                            </cc:ValidatorCalloutExtender>

                                        </li>
                                        </li>
                                    </div>
                                    <div class="divL" style="width: 450px;">
                                        <li>
                                            <asp:Label ID="Label186" runat="server" CssClass="label" Text="Return Method"></asp:Label>
                                            <asp:Label ID="lblItemWise" runat="server" CssClass="label" Text="Item Wise">
                                                <asp:RadioButton ID="rdItemSales" runat="server" Width="10px" GroupName="a" TabIndex="7" AutoPostBack="true" OnCheckedChanged="rdItemSales_CheckedChanged" />
                                            </asp:Label>
                                            <asp:Label ID="Label6" runat="server" CssClass="label" Text="Sales Wise">
                                                <asp:RadioButton ID="rdSales" runat="server" Width="10px" AutoPostBack="true" GroupName="a" TabIndex="8" OnCheckedChanged="rdSales_CheckedChanged" />
                                            </asp:Label>
                                        </li>
                                        <li id="liBillType" runat="server">
                                            <asp:Label class="label" ID="Label2" runat="server" Text="Bill Type"></asp:Label>
                                            <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlBillType" TabIndex="9"></asp:DropDownList>
                                            <asp:Button ID="btnBillType" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Bill Type required" ControlToValidate="ddlBillType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="imgSales"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Bill Type required" ControlToValidate="ddlBillType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                            </cc:ValidatorCalloutExtender>
                                        </li>
                                        <li id="liCounter" runat="server">
                                            <asp:Label class="label" ID="Label10" runat="server" Text="Counter"></asp:Label>
                                            <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlCounter" TabIndex="10"></asp:DropDownList>
                                            <asp:Button ID="btnCounter" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Counter required" ControlToValidate="ddlCounter" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                            </cc:ValidatorCalloutExtender>
                                        </li>
                                        <li id="liSales" runat="server">
                                            <asp:Label class="label" ID="Label3" runat="server" Text="Sales" Width="136px"></asp:Label>
                                            <div style="display: none">
                                                <asp:TextBox ID="txtSalesRefno" runat="server" AutoPostBack="false" onblur="txtSalesRefno_onblur()" CssClass="textboxMandatory" TabIndex="11" onKeypress="CheckInteger(event);" Width="40px"></asp:TextBox>
                                            </div>
                                            <asp:TextBox ID="txtSalesBillNo" runat="server" CssClass="textboxMandatory" Width="188px" TabIndex="13"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnSalesBill" runat="server" OnClick="imgbtnSalesBill_Click" ImageUrl="~/images/serach_icon.png" TabIndex="12" Style="padding: 0px; margin: 4px 0px 0px 0px; position: absolute" ValidationGroup="imgSales" />
                                          <asp:RequiredFieldValidator ID="rfvSaleRefno" runat="server" ErrorMessage="At least one Sale is required." ControlToValidate="txtSalesBillNo"
                                                Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfvSaleRefno">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RequiredFieldValidator ID="rfvSaleBillNo" runat="server" ErrorMessage="Bill No required"
                                                ControlToValidate="txtSalesBillNo" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="rfvSaleBillNo">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RequiredFieldValidator ID="rfvSaleRefnoSubmit" runat="server" ErrorMessage="At least one Sale is required." ControlToValidate="txtSalesRefno" Display="None" SetFocusOnError="True" ValidationGroup="submit">
                                            </asp:RequiredFieldValidator>
                                           <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender_SalesRefno" runat="server" Enabled="True" TargetControlID="rfvSaleRefnoSubmit">
                                           </cc:ValidatorCalloutExtender>
                                        </li>
                                        <li class="clear"></li>
                                        <li class="clear"></li>
                                    </div>
                                    <div class="clear"></div>
                                    <div class="divR" style="width: 170px;">
                                    </div>
                                    <li class="clear"></li>
                                    <div class="divSeparation"></div>
                                    <div class="clear"></div>
                                    <li class="clear"></li>
                                    <div id="divItemSalesReturn" runat="server">
                                        <div class="divL" style="width: 400px;">
                                            <li>
                                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item" Width="138px"></asp:Label>
                                                <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtitem" MaxLength="20" AutoComplete="off" Width="190px" TabIndex="14" onblur="return popUpStockSelect();" />
                                                <%--   <asp:Button ID="btnItem" runat="server" Text="Item" OnClick="btnItem_Click" Style="display: none;" />--%>
                                                <asp:ImageButton ID="ImageButton3" runat="server" onkeypress="return popUpStockSelectClick();" TabIndex="15" OnClientClick="return popUpStockSelectClick();" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 0px 0px; position: absolute" ValidationGroup="img" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Item Required " ControlToValidate="txtitem" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender117" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            <li>
                                                <asp:Label ID="Label31" runat="server" CssClass="label" Width="138px" Text="Tax Amount"></asp:Label>
                                                <asp:TextBox ID="txttaxAmount" CssClass="textbox" TabIndex="18" Width="155px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                                <asp:Button Text="STax" ToolTip="SalesTax" ID="btnSTax" Style="padding: 0px; width: 50px; height: 18px; margin: 0px 0px 0px 6px;" CssClass="button" OnClick="btnSTax_Click" runat="server" />
                                            </li>
                                            <li class="clear"></li>
                                        </div>
                                        <div class="divL" style="width: 190px;">
                                            <li style="width: 150px; float: left;">
                                                <asp:Label ID="Label9" runat="server" CssClass="label" Text="Qty" Width="60px"></asp:Label>
                                                <asp:TextBox ID="txtQuantity" CssClass="textboxMandatory" Style="text-align: right;" Width="80px" runat="server" TabIndex="16" MaxLength="11" onkeypress="DecimalOrIntValidation('txtQuantity',event,'items'); " onKeyup="CalculateNetAmount();" AutoComplete="off"></asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" SetFocusOnError="True" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:HiddenField ID="hdfPacked" runat="server" />
                                            </li>
                                            <li>
                                                <asp:Label ID="Label8" runat="server" CssClass="label" Width="60px" Text="Tot.Amt"></asp:Label>
                                                <asp:TextBox ID="txtTotalAmountItem" Style="text-align: right;" CssClass="textbox" Width="80px" runat="server" TabIndex="19" MaxLength="12" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>
                                            </li>
                                        </div>
                                        <div style="float: left; width: 190px; margin-right: 10px">
                                            <li>
                                                <asp:Label ID="Label7" runat="server" CssClass="label" Width="70px" Text="Sales.Rate"></asp:Label>
                                                <asp:TextBox ID="txtSalesRateItem" Style="text-align: right;" CssClass="textboxMandatory" Width="90px" runat="server" TabIndex="17" MaxLength="14" onKeypress="CheckNumWithDecimal(event);" onkeyup="CalculateNetAmount();" AutoComplete="off"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtSalesRateItem" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtSalesRateItem" ErrorMessage="Enter a valid Rate" Display="None" SetFocusOnError="True" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            <li>
                                                <asp:Button Text="Insert" ID="btnInsertItems" runat="server" OnClick="btnInsertItems_Click" TabIndex="20" ValidationGroup="add" CssClass="button" />
                                                <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="21" OnClientClick="ItemClear();return false;" />
                                            </li>
                                        </div>
                                        <div class="clear"></div>
                                        <div class="clear"></div>
                                        <div class="divSeparation"></div>
                                        <li style="width: 100%; min-height: 310px; max-height: 510px; overflow-y: auto;">

                                            <asp:GridView ID="gvItems" runat="server" Width="95%"
                                                EmptyDataText="No Data Found!" AutoGenerateColumns="False" CssClass="grid"
                                                BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items"
                                                OnRowDeleting="gvItems_RowDeleting" PageSize="100">
                                                <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                                <Columns>
                                                    <asp:BoundField DataField="slno" HeaderText="SlNo" HeaderStyle-Width="5px" ItemStyle-CssClass="gridviewItemStyle" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField HeaderText="Item" DataField="Item" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="Batch No" ItemStyle-CssClass="gridviewItemStyle" DataField="BatchNo" HeaderStyle-Width="70px" />
                                                    <asp:BoundField HeaderText="ExpiryDate" ItemStyle-CssClass="gridviewItemStyle" DataField="ExpiryDate" HeaderStyle-Width="50px" />
                                                    <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                                    <%-- <asp:BoundField HeaderText="ActQty" DataField="ActQty" ItemStyle-HorizontalAlign="Right" />--%>
                                                    <%--<asp:BoundField HeaderText="Free" DataField="Free"  ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" />--%>
                                                    <asp:BoundField HeaderText="Sales Rate" DataField="SalesRate" ItemStyle-CssClass="gridviewItemStyle"
                                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="90px" ItemStyle-Width="90px" />
                                                    <asp:BoundField HeaderText="Total Amount" DataField="TotalAmt" ItemStyle-CssClass="gridviewItemStyle"
                                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="90px" ItemStyle-Width="90px" />
                                                    <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlReason" runat="server" TabIndex="21" CssClass="dropdownMandatory" Width="100px"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select a Reason"
                                                                ControlToValidate="ddlReason" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                            </cc:ValidatorCalloutExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton255" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="1px" />
                                                <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                            </asp:GridView>
                                        </li>
                                    </div>
                                    <div class="clear"></div>
                                    <div id="gvpi" runat="server" visible="false">
                                        <li id="gvpi1" runat="server" style="width: 95%; min-height: 235px; max-height: 235px; overflow: auto">
                                            <asp:GridView ID="gvSalesItems" runat="server" DataKeyNames="FK_Items,FK_Stock,ExpiryDate,MRP,ActSalPrice,BatchNo"
                                                EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="95%" CssClass="grid"
                                                BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100"
                                                OnRowDeleting="gvSalesItems_RowDeleting" OnRowDataBound="gvSalesItems_RowDataBound">
                                                <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                                <Columns>
                                                    <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-CssClass="gridviewItemStyle" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Item" HeaderStyle-Width="50px" ItemStyle-CssClass="gridviewItemStyle" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblItem" Style="width: 200px;" Text='<%# Eval("Item") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="200px" />
                                                        <ItemStyle Width="200px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtQuantity" onBlur="CalculateAmount(id,'txtQuantity');" AutoComplete="off" CssClass="textbox" Style="width: 50px; text-align: right;" onkeypress="DecimalOrIntValidation(id,event,'txtQuantity');" Text='<%# Eval("Quantity") %>' />
                                                            <asp:HiddenField ID="hfStockID" runat="server" Value='<%# Eval("FK_Stock") %>' />
                                                            <asp:HiddenField ID="hdfQty" runat="server" Value='<%# Eval("Quantity") %>' />
                                                            <asp:HiddenField ID="hdfBalanceQuantity" runat="server" Value='<%# Eval("BalanceQuantity") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Batch No" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="70px" ItemStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBatchNo" runat="server"><%#Eval("BatchNo") %></asp:Label>
                                                            <asp:HiddenField ID="hdfBatchNo" runat="server" Value='<%#Eval("BatchNo") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="70px" />
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Expiry Date" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="50px" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExpiryDate" runat="server"><%#Eval("ExpiryDate") %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRP" ItemStyle-CssClass="gridviewItemStyle" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRP" runat="server"><%#Eval("MRP") %></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false" HeaderText="Actual Sales Rate" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActSalesRate" runat="server" Style="float: right;" Text='<%#Eval("ActSalPrice") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Rate" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSalesRate" runat="server" Style="float: right;" Text='<%#Eval("SalesPrice") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Amount" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAmt" runat="server"><%#Eval("TotalAmt") %></asp:Label>
                                                            <asp:HiddenField ID="hdfTotalAmt" runat="server" Value='<%#Eval("TotalAmt") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" Width="50px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" ItemStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlReason" runat="server" CssClass="dropdownMandatory" Width="100px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlReason" Display="None" ErrorMessage="Select a Reason" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                            </cc:ValidatorCalloutExtender>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" ItemStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                    </asp:TemplateField>
                                                </Columns>

                                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                                <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                            </asp:GridView>
                                        </li>
                                    </div>
                                    <li class="clear"></li>
                                    <br class="clear" />
                                    <div class="divSeparation"></div>
                                    <div class="divL">
                                        <li>
                                            <label class="label">Remark</label>
                                           <div style="display: inline-block;">
                                            <asp:TextBox runat="server" ID="txtRemark" 
                                                TextMode="MultiLine"
                                                CssClass="multiline fixed-remark"
                                                MaxLength="500"
                                                TabIndex="22"
                                                AutoComplete="off"
                                                onkeyup="EnforceMaxLength(this, 500); updateRemarkCount();"
                                                onpaste="setTimeout(updateRemarkCount, 0);" />
    
                                            <div id="spnRemarkCount" class="remark-count">
                                                0 / 500 characters
                                            </div>
                                        </div>
                                        </li>
                                        <li id="cbx" style="display: none;">
                                            <label class="label">Close Remark</label>
                                            <asp:TextBox ID="txtClosedRemark" runat="server" TextMode="MultiLine" MaxLength="500" TabIndex="23" AutoComplete="off" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                            <asp:HiddenField ID="HiddenField3" runat="server" />
                                            <asp:HiddenField ID="HiddenField4" runat="server" />
                                        </li>
                                        <asp:HiddenField ID="hdfDate" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:HiddenField ID="hfStore" runat="server" />
                                        <asp:HiddenField ID="hdfFKStock" runat="server" />
                                        <asp:HiddenField ID="hdfFkItem" runat="server" />
                                        <asp:HiddenField ID="hfItem" runat="server" />
                                        <asp:HiddenField ID="hfBillNo" runat="server" />
                                        <asp:HiddenField ID="hfInvNo" runat="server" />
                                        <asp:HiddenField ID="hfItemId" runat="server" />
                                        <asp:HiddenField ID="hfActualSalPrice" runat="server" />
                                        <asp:HiddenField ID="hfGroupID" runat="server" />
                                        <asp:HiddenField ID="hfStockID" runat="server" />
                                        <asp:HiddenField ID="hfMasterID" runat="server" />
                                         <asp:HiddenField ID="hdfMode" runat="server" />
                                        <asp:Button ID="btnItem" runat="server" Text="Button" OnClick="btnItem_Click" CausesValidation="false" Style="display: none;" />
                                    </div>
                                    <div class="divR">
                                        <li>
                                            <label class="label">Total Amount</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtTotalAmount" onKeypress="CheckNumWithDecimal(event);" Style="text-align: right;" TabIndex="24" AutoComplete="off" />
                                        </li>
                                        <label class="label">Round Off</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtRoundOff" Style="text-align: right;" TabIndex="25" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />
                                        <li>
                                            <label class="label">Net Amount</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtNetAmount" Style="text-align: right;" TabIndex="26" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />
                                        </li>
                                        <li class="clear"></li>
                                    </div>
                                    <div class="clear"></div>
                                    <li class="btnMargin">
                                        <asp:HiddenField ID="hfSalesReturn" runat="server" />
                                        <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="27" OnClientClick="return ValidateSalesWiseQty();"  />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" CausesValidation="false" TabIndex="28" OnClick="btnClear_Click1" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfuserName" runat="server" />
                                    </li>
                            </div>
                            <div class="clear"></div>
                            <div class="main_bg">
                                <div id="divView" runat="server" style="display: none; height: 485px">
                                    <ul class="contentul">
                                        <div class="divL">
                                            <li>
                                                <label class="label">Customer</label>
                                                <asp:DropDownList ID="ddlSearchCustomer"  onkeydown="EnterTab(event,'txtFromDate');" runat="server" CssClass="dropdown" TabIndex="31"></asp:DropDownList>
                                            </li>
                                            <li style="display: none">
                                                <label class="label">Store</label>
                                                <asp:DropDownList ID="ddlSearchStore" runat="server" CssClass="dropdown"></asp:DropDownList>
                                            </li>
                                            <br class="clear" />
                                            <li class="btnMargin">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="34" ValidationGroup="search" />
                                                <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="35" />
                                            </li>
                                        </div>
                                        <div class="divR">
                                            <li>
                                                <label class="label">From Date</label>
                                                <asp:TextBox ID="txtFromDate" CssClass="textbox" onkeydown="EnterTab(event,'txtTodate');"  runat="server" TabIndex="32"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                </cc:CalendarExtender>
                                                <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>--%>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="search"></asp:CompareValidator>

                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            <li>
                                                <label class="label">To Date</label>
                                                <asp:TextBox ID="txtTodate" onkeydown="EnterTab(event,'btnSearch');"  CssClass="textbox" runat="server" TabIndex="33"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtTodate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                </cc:CalendarExtender>
                                            </li>
                                        </div>
                                    </ul>
                                    <br class="clear" />
                                    <ul class="contentul">
                                        <asp:GridView ID="gvSalesReturn" runat="server" CssClass="grid" Style="margin-left: 1px;" Width="95%"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False" AllowPaging="true" 
                                            BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" OnPageIndexChanging="gvSalesReturn_PageIndexChanging" DataKeyNames="ID_SalesReturn" OnSelectedIndexChanging="gvSalesReturn_SelectedIndexChanging" OnRowDeleting="gvSalesReturn_RowDeleting" OnRowCommand="gvSalesReturn_RowCommand">
                                            <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                            <Columns>
                                                <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                                                <asp:BoundField DataField="BillDate" HeaderText="Date" />
                                                <asp:BoundField DataField="NetAmount" HeaderText="NetAmount" />
                                                <asp:BoundField DataField="Customer" HeaderText="Customer" />
                                                <asp:BoundField DataField="Store" HeaderText="Store" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" OnClick="ImageBtnDelete_Click" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                                            <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                            <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </ul>
                                </div>
                            </div>
                            <div class="clear"></div>
                            <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                            <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                                <div id="divheader" runat="server" class="PopupHeader">
                                    <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head" CssClass="lblPopupHeader"></asp:Label>
                                    <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                </div>
                                <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                            </asp:Panel>
                            <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                                CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                            </cc:ModalPopupExtender>
                            <asp:Button ID="btS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                            <asp:Panel ID="divPop" runat="server" Style="display: none;" CssClass="DivStyle">
                                <div id="divhead" runat="server" class="PopupHeader">
                                    <asp:Label ID="lblPopupH" runat="server" CssClass="lblPopupHeader"></asp:Label>
                                    <asp:ImageButton ID="btnClse" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                </div>
                                <uc3:CommonPopupGrid ID="CommonPopupGrid1" runat="server" />
                            </asp:Panel>
                            <cc:ModalPopupExtender ID="mpGrid" runat="server" PopupControlID="divPop" TargetControlID="btS"
                                CancelControlID="btnClse" Drag="true" PopupDragHandleControlID="divhead">
                            </cc:ModalPopupExtender>


                            <asp:Button ID="btnReasonShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                            <asp:Panel ID="divReasonPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                                <div id="divReasonheader" runat="server" class="PopupHeaderReason">
                                    <asp:Label ID="Label5" runat="server" Text="Reason"></asp:Label>
                                    <asp:ImageButton ID="btnReasonClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                </div>
                                <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                                    <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                                </div>
                            </asp:Panel>
                            <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divReasonPopup" TargetControlID="btnReasonShow"
                                CancelControlID="btnReasonClose" Drag="true" PopupDragHandleControlID="divReasonheader">
                            </cc:ModalPopupExtender>


                            <asp:Button ID="btnP" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                            <asp:Panel ID="pnlPrvlg" runat="server" Style="display: none;" CssClass="DivStyle">
                                <div id="divPrvlg" runat="server" class="PopupHeader">
                                    <asp:Label ID="Label4" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
                                    <asp:ImageButton ID="btnCP" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                </div>
                                <uc1:CommonPopupPrivilege runat="server" ID="CommonPopupPrivilege" />
                            </asp:Panel>
                            <cc:ModalPopupExtender ID="mpPrivilege" runat="server" PopupControlID="pnlPrvlg" TargetControlID="btnP"
                                CancelControlID="btnCP" Drag="true" PopupDragHandleControlID="divPrvlg">
                            </cc:ModalPopupExtender>


                            
                        </div>
                    </div>
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
            .modal {
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

            .loading {
                font-family: Arial;
                font-size: 10pt;
                border: 5px solid #67CFF5;
                padding-top: 180px;
                min-height: 100%;
                width: 100%;
                display: none;
                position: fixed;
                opacity: 0.5;
                filter: alpha(opacity=50);
                -moz-opacity: 0.5;
                background-color: White;
                z-index: 999;
            }
        </style>
        <script type="text/javascript">

            function ShowProgress() {
                if (Page_ClientValidate("submit")) {
                }
            }

            function ClearAdd() {
                submit = 0;
                $("[id*='txt']").not("txtDate").val('');
                $("#txtRefNo").val('');
                $("#ddlCustomer").val('0');
                var d = new Date();
                $("#txtDate").val(d.format('dd/MM/yyyy'));
                $("#txtTotalAmount").val('0');

                $("#txtNetAmount").val('0');
                $("#txtRemark").val("");
                $("#cbxIsCredit").prop("checked", false);
                $("#cbxAccountNeeded").prop("checked", true);
                $("#hfSalesReturn").val('');
                $("#hfBillNo").val('0');
                $("#txtFromDate,#txtTodate").val(d.format('dd/MM/yyyy'));
                $("#ddlSales").val('0');
                $("#ddlBillType").val('0');
                $("#ddlCounter").val('0');
                $("#txtRoundOff").val("0");
                $("#hfInvNo").val('');
                ItemClear();
            }

            function CalculateAmount(id, type) {
                try {
                    var lblPr = id.replace(type, 'lblSalesRate');
                    var lblTotal = id.replace(type, 'lblTotalAmt');
                    var hdfTotalAmt = id.replace(type, 'hdfTotalAmt');
                    var hdfBalQty = id.replace(type, 'hdfBalanceQuantity');
                    var txtQuantity = id.replace(type, 'txtQuantity');

                    var qty = parseFloat($("#" + txtQuantity).val()) || 0;
                    var pr = parseFloat($("#" + lblPr).text()) || 0;
                    var balQty = parseFloat($("#" + hdfBalQty).val()) || 0;

                    // Block quantity exceeding balance
                    if (qty > balQty) {
                        alertify.alert("Quantity cannot exceed available quantity of " + balQty);
                        $("#" + txtQuantity).val(balQty.toFixed(3));
                        qty = balQty;
                    }

                    if (qty <= 0) {
                        $("#" + txtQuantity).val("0");
                    }

                    // Recalculate total
                    var amt = (qty * pr).toFixed(4);
                    $("#" + lblTotal).text(amt);
                    $("#" + hdfTotalAmt).val(amt);

                    CalculateTotalAmount();
                }
                catch (e) {
                    alert(e);
                }
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

            function CalculateTotalAmount() {
                var Totalamt = 0;
                $.each($("table[id*=" + 'gvSalesItems' + "] tr"), function (index, item) {
                    if ($(item).find("input[type=hidden]").length > 0) {
                        var SalesPrice = $(item).find("[id*=gvSalesItems_lblSalesRate_]").text();
                        var Qty = $(item).find("[id*=txtQuantity_]").val();
                        var ActQty = parseFloat(Qty);
                        var Amt = (parseFloat(SalesPrice) * parseFloat(Qty))
                        $(this).find("[id*='lblTotalAmt_']").text((Amt).toFixed(4));
                        Totalamt += Amt;
                        if ($('#rdSales').is(":checked")) {
                            var Qty = $(item).find("[id*=txtQuantity_]").val();
                        }
                    }
                });

                $("#txtTotalAmount").val(Totalamt.toFixed(4));
                Total();
            }

            function CheckKeys() {
                alert(e.keyCode);
            }

            function ClearSearch() {
                $("#ddlSearchCustomer ").val("0");
                $("#ddlSearchStore").val("0");
                var d = new Date();
                $("#txtFromDate,#txtTodate").val(d.format('dd/MM/yyyy'));

            }
            function ShowAddDiv() {

                $("#divAdd").show();
                $("#divView").hide();
            }
            function btnClick() {
                $("#btnHidden2").click();
                return false;
            }
            function ddlBillTypeClick() {
                $("#btnBillType").click();
                return false;
            }
            function ddlCounterClick() {
                $("#btnCounter").click();
                return false;
            }

            function buttonClick() {
                $("#btnHidden").click();
                return false;
            }

            function BlurItem() {

                $("#btnItemBlurTrigger").click();

            }
            function SetStoreDetail() {
                var store = $("#ddlStore option:selected").val();
                $("#hfStore").val(store);
            }

            function pageLoad() {

                var ctrContainer = "form";
                setKeynavigation(ctrContainer);
                Check();
            }
            function ItemClear() {
                $("#ddlItem").val("0");
                $("#ddlUnit").val("0");
                $("#txtBatch").val("");
                $("#txtitem").val("");
                $("#txtActQty").val("0");
                $("#txtQuantity").val("0");
                $("#ddlUnit").val('');
                $("#txtSalesRate").val("0");
                $("#txtItemVat").val('0');
                $("#txtItemVatAmount").val('0');
                $("#txtSalesRateItem").val('0');
                $("#txtTotalAmountItem").val('0');
                $("#txttaxAmount").val('0');
            }



            var popup;
            function popUpStockSelectClick() {

                if (Page_ClientValidate("img") == true) {
                    var popup;
                    var ItemName = $("#txtitem").val();
                    var hfUser = $("#hfUser").val();
                    var hfClient = $("#hfClient").val();
                    var hfNtRtrn = "True";

                    if ($("#hfStore").val() == '') {
                        SetStoreDetail();
                    }
                    var hfStore = $("#hfStore").val();
                    var txtCustRefno = $("#txtCustRefno").val();
                    var mode = $("#hdfMode").val();
                    $.ajax({
                        type: "POST",
                        url: 'Textile.asmx/GetUrlEncryptStockSR',
                        data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','txtCustRefno':'" + txtCustRefno + "','hfStore':'" + hfStore + "','hfClient':'" + hfClient + "','hfNtRtrn':'" + hfNtRtrn + "','mode':'" + mode + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (MenuHtml) {
                            SetStoreDetail();
                            url = "StockItems.aspx?" + MenuHtml.d;

                            popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                            ",statusbar=no,menubar=no,resizable=0,width=1000," +
                            "height=800,left = 200,top = 100");
                            popUpObj.focus();
                        },
                        error: function (MenuHtml) {
                        }
                    });

                }

                return false;
            }
            function popUpStockSelect() {

                if (Page_ClientValidate("img") == true) {

                    var popup;
                    var ItemName = $("#txtitem").val();
                    var hfUser = $("#hfUser").val();
                    var hfClient = $("#hfClient").val();
                    var hfNtRtrn = "True";
                    if ($("#hfStore").val() == '') {
                        SetStoreDetail();
                    }
                    var hfStore = $("#hfStore").val();

                    var txtCustRefno = $("#txtCustRefno").val();
                    if (ItemName.length > 0) {
                        if ($("#hfStore").val() == '') {
                            SetStoreDetail();
                        }
                        $.ajax({
                            type: "POST",
                            url: 'Textile.asmx/GetUrlEncryptStockSR',
                            data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','txtCustRefno':'" + txtCustRefno + "','hfStore':'" + hfStore + "','hfClient':'" + hfClient + "','hfNtRtrn':'" + hfNtRtrn + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (MenuHtml) {
                                SetStoreDetail();
                                url = "StockItems.aspx?" + MenuHtml.d;

                                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                                ",statusbar=no,menubar=no,resizable=0,width=1000," +
                                "height=800,left = 200,top = 100");
                                popUpObj.focus();
                            },
                            error: function (MenuHtml) {
                            }
                        });
                    }
                }

                return false;
            }

            function EnterTab(e, Id1) {

                var intKey = window.Event ? e.which : e.KeyCode;


                if (intKey == 13) {
                    e.returnValue = false;
                    $("#" + Id1).focus();
                }
            }
            function CalculateNetAmount() {
                try {

                    var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
                    var ItemVat = $("#txtItemVat").val() == "" ? "0" : $("#txtItemVat").val();
                    var SalesRateItem = $("#txtSalesRateItem").val() == "" ? "0" : $("#txtSalesRateItem").val();
                    var TotalAmountItem = $("#txtTotalAmountItem").val() == "" ? "0" : $("#txtTotalAmountItem").val();
                    var ItemVatAmount = $("#txtItemVatAmount").val() == "" ? "0" : $("#txtItemVatAmount").val();
                    var CESS = $("#txtCESS").val() == "" ? "0" : $("#txtCESS").val();
                    var CESSAmt = 0;

                    if (parseFloat(Quantity) > parseFloat(0)) {
                        if (parseFloat(SalesRateItem) > parseFloat(0)) {
                            if (parseFloat(ItemVat) > parseFloat(0)) {
                                ItemVatAmount = parseFloat(Quantity) * parseFloat(SalesRateItem) * parseFloat(ItemVat) / parseFloat(100);
                            }
                            else {
                                ItemVatAmount = parseFloat(0);
                            }

                            if (parseFloat(CESS) > parseFloat(0)) {
                                CESSAmt = parseFloat(Quantity) * parseFloat(SalesRateItem) * parseFloat(CESS) / parseFloat(100);
                            }
                            TotalAmountItem = parseFloat(Quantity) * parseFloat(SalesRateItem) + parseFloat(ItemVatAmount) + parseFloat(CESSAmt);

                            $("#txtItemVatAmount").val(ItemVatAmount.toFixed(2));
                            $("#txtTotalAmountItem").val(TotalAmountItem.toFixed(2));
                            $("#txtCESSAmt").val(CESSAmt.toFixed(2));
                        }
                        else {

                            $("#txtItemVatAmount").val('0');
                            $("#txtTotalAmountItem").val('0');
                        }
                    }
                    else {
                        $("#txtItemVatAmount").val('0');
                        $("#txtTotalAmountItem").val('0');
                    }
                }

                catch (e) {
                    alert(e);
                }


            }
            function Total() {
                try {

                    var TotalAmount = $("#txtTotalAmount").val();
                    var NetAmount = $("#txtNetAmount").val();
                    var Amount = ((parseFloat(TotalAmount)));
                    var total = Amount;

                    if (total >= 0) {
                        var fracpart = (((total < 1.0) ? total : (total % Math.floor(total))) * 100).toFixed(2);
                        if (parseFloat(fracpart) >= 50) {
                            var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);
                            var totalnetRoundamount = parseFloat(total) + parseFloat(Balancefract);
                            var balfract = parseFloat(fracpart / 100).toFixed(2);
                            $("#txtNetAmount").val(totalnetRoundamount.toFixed(2));
                            $("#txtRoundOff").val(Balancefract.toFixed(2));
                        }
                        else if (fracpart < 50) {
                            var Balancefract = (parseFloat(100) - parseFloat(fracpart)) / parseFloat(100);
                            var totalnetRoundamount = parseFloat(total) - parseFloat(fracpart / 100);
                            var balfract = parseFloat(fracpart / 100).toFixed(2);
                            $("#txtNetAmount").val(totalnetRoundamount.toFixed(2));
                            $("#txtRoundOff").val(-balfract);
                        }
                    }
                    else {
                        // alert('Invalid entry');
                        Total();
                    }
                }
                catch (ex) {
                    alert(ex.message);
                }
            }


            function DecimalOrIntValidation(id, eventRef, type) {
                var hdfpacked = 'hdfPacked';

                if (type != 'items') {
                    hdfpacked = id.replace(type, 'hdfPacked');
                }

                if ($("#" + hdfpacked).val() == "False") {
                    CheckNumWithDecimal(eventRef);
                }
                else {
                    CheckInteger(eventRef);
                }
            }
            function txtCustRefno_onblur() {
                $("#txtCustomer").val('');
                return false;
            }
            function txtSalesRefno_onblur() {
                if ($("#txtSalesBillNo").val() == "") {
                    $("#txtSalesBillNo").val('');
                }
                return false;
            }


            function ClientValidate(source, arguments) {
                try {
                    var id = source.id;
                    var qty = arguments.Value;
                    if (qty == 0) {
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

            function PopupOnReason() {
                try {
                    $("#divPopupReason").css('display', 'block');
                }
                catch (ex) {
                    //alert('popup' + ex);  
                }
            }

            function ValidateSalesWiseQty() {
                var salesReturnId = $("#hfSalesReturn").val();
                if (salesReturnId && $.trim(salesReturnId) !== "") {
                    toastr.error("Submit is blocked. This sales return is opened for edit.");
                    return false;
                }

                $("[id*='rfvSaleRefno']").hide();

                if (!Page_ClientValidate("submit")) {
                    return false;
                }

                if ($("[id$='rdSales']").is(":checked")) {

                    var salesRefno = $("#txtSalesRefno").val();
                    var salesBillNo = $("#txtSalesBillNo").val();
                    if (!salesRefno || salesRefno.trim() === "" || !salesBillNo || salesBillNo.trim() === "") {
                        alertify.alert("At least one Sale is required.");
                        return false;
                    }

                    var rows = $("table[id*='gvSalesItems'] tr input[id*='txtQuantity']");
                    if (rows.length === 0) {
                        alertify.alert("At least one item is required.");
                        return false;
                    }


                    var hasValidQty = false;
                    rows.each(function () {
                        var qty = parseFloat($(this).val()) || 0;
                        if (qty > 0) {
                            hasValidQty = true;
                            return false;
                        }
                    });
                    if (!hasValidQty) {
                        alertify.alert("Quantity must be greater than zero for at least one item.");
                        return false;
                    }
                }

                if ($("[id$='rdItemSales']").is(":checked")) {
                    var itemRows = $("table[id*='gvItems'] tr input[type!='hidden']");
                    if ($("table[id*='gvItems'] tbody tr").length === 0 ||
                        $("table[id*='gvItems'] tbody tr td").first().text() === "No Data Found!") {
                        alertify.alert("At least one item is required.");
                        return false;
                    }
                }

                return true;
            }


            function EnforceMaxLength(textarea, maxLength) {
                try {
                    if (textarea.value.length > maxLength) {
                        textarea.value = textarea.value.substring(0, maxLength);
                    }
                    var remaining = textarea.value.length;
                    var counter = document.getElementById("spnRemarkCount");
                    if (counter) {
                        counter.innerHTML = remaining + " / " + maxLength + " characters";
                        counter.style.color = remaining >= maxLength ? "red" : "gray";
                    }
                } catch (e) {
                    alert(e);
                }
            }

            function ValidateCustomerCredit(source, args) {
                var isCredit = document.getElementById('<%= cbxIsCredit.ClientID %>').checked;
                var cust = document.getElementById('<%= txtCustomer.ClientID %>').value.trim();
                args.IsValid = !(isCredit && cust === "");
            }
        </script>


    </form>
</body>
</html>
