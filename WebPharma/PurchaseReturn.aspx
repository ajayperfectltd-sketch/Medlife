<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseReturn.aspx.cs" Inherits="PurchaseReturn" EnableEventValidation="false" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopUpGridSelection.ascx" TagName="CommonPopupGrid" TagPrefix="uc3" %>
<%@ Register Src="Popup/PurchaseReturn.ascx" TagName="PopupPurchaseReturn" TagPrefix="uc5" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Purchase Return::</title>
    <link href="css/main.css" rel="stylesheet" />

    <link href="css/toastr.css" rel="stylesheet" />
    <style>
        .hiddencol
        {
            display: none;
        }

       .remark-container {
    display: inline-block;   /* keeps things grouped neatly */
}

.fixed-remark {
    width: 400px;
    height: 100px;
    resize: none;
    display: block;
    overflow-y: auto;
}

.remark-counter {
    display: block;          /* forces it below textbox */
    font-size: 11px;
    color: gray;
    margin-top: 4px;
    text-align: right;       /* optional, looks cleaner */
}
    </style>
</head>

<body>
    <form id="frmpurchaseReturn" runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="user">Purchase Return</div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <asp:ImageButton ID="ImageButton1" CssClass="imagebuttonStyle" ImageUrl="images/ADD.png" TabIndex="32" runat="server" CausesValidation="false" OnClientClick="ShowAddDiv();  releasebtn();" OnClick="ImageButton1_Click" />
                                <asp:ImageButton ID="imgbtnView" CssClass="imagebuttonStyle" ImageUrl="images/view.png" TabIndex="31" runat="server" OnClick="imgbtnView_Click" />
                            </div>
                        </asp:Panel>


                    </div>


                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                            <div id="divAdd" runat="server" style="display: block;">

                                <ul class="contentul" id="Container">
                                    <div style="width: 1500px">
                                        <div class="divL" style="width: 295px;">


                                            <li>
                                                <label class="label">Date</label>
                                                <asp:TextBox runat="server" Width="127px" CssClass="textboxMandatory" ID="txtDate" TabIndex="1" AutoComplete="off" />
                                                <cc:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            <li>


                                                <label class="label">Supplier</label>
                                                <div style="display: none">
                                                    <asp:TextBox ID="txtSupRefno" runat="server" AutoPostBack="false" onblur="txtSupRefno_onblur()" CssClass="textboxMandatory" onKeypress="CheckInteger(event);" Width="70px"></asp:TextBox>

                                                    <asp:CustomValidator ID="cvSupplierRef" runat="server" ErrorMessage="Please select a valid Supplier"
                                                        ControlToValidate="txtSupRefno" Display="None" SetFocusOnError="True"
                                                        ClientValidationFunction="ValidateSupplierRef" ValidationGroup="submit"
                                                        OnServerValidate="cvSupplierRef_ServerValidate" />
                                                    <cc:ValidatorCalloutExtender ID="vceSupplierRef" runat="server" Enabled="True" TargetControlID="cvSupplierRef" />
                                                </div>


                                                <asp:TextBox ID="txtSupplier" runat="server" Width="100px" CssClass="textboxMandatory" TabIndex="2"></asp:TextBox>
                                                <asp:ImageButton ID="imgbtnSupplier" runat="server" OnClick="imgbtnSupplier_Click" TabIndex="3" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 0px 0px; position: absolute" ValidationGroup="img"  />
                                                <asp:RequiredFieldValidator ID="rfvSuppName" runat="server" ErrorMessage="Supplier Name required"
                                                    ControlToValidate="txtSupplier" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="rfvSuppName">
                                                </cc:ValidatorCalloutExtender>

                                            </li>
                                            <li>

                                                <asp:Label ID="lblCredit" runat="server" CssClass="label" Text="Credit"></asp:Label>
                                                <asp:CheckBox ID="cbxIsCredit" runat="server" TabIndex="4" />
                                            </li>

                                        </div>
                                        <div class="divL" style="width: 500px;">

                                            <li>

                                                <asp:Label class="label" ID="lbl" runat="server" Text="Store" Width="136px"></asp:Label>
                                                <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore" Width="150px" TabIndex="5">
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="reqvalStore" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="add"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqvalStore">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="imgPurchase"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                </cc:ValidatorCalloutExtender>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="img"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                </cc:ValidatorCalloutExtender>

                                                <asp:RequiredFieldValidator ID="reqvalStoreSubmit" runat="server" ErrorMessage="Store required"  ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit" />
                                                <cc:ValidatorCalloutExtender ID="vceStoreSubmit" runat="server" Enabled="True" TargetControlID="reqvalStoreSubmit" />

                                            </li>
                                            <li>
                                                <asp:Label ID="Label186" runat="server" CssClass="label" Text="Return Method"></asp:Label>
                                                <asp:Label ID="lblItemWise" runat="server" CssClass="label" Width="80px" Text="Item Wise">
                                                    <asp:RadioButton ID="rdItemPurchase" runat="server" Width="10px" GroupName="a" AutoPostBack="true" TabIndex="6" OnCheckedChanged="rdItemPurchase_CheckedChanged" />
                                                </asp:Label>
                                                <asp:Label ID="Label6" runat="server" CssClass="label" Width="80px" Text="Pur.Wise">
                                                    <asp:RadioButton ID="rdPurchase" runat="server" Width="10px" AutoPostBack="true" GroupName="a" TabIndex="7" OnCheckedChanged="rdMaterialPurchase_CheckedChanged" />
                                                </asp:Label>
                                                <asp:Label ID="Label10" runat="server" CssClass="label" Width="100px" Text="Expiry" Visible="false">
                                                    <asp:RadioButton ID="rdExpiry" runat="server" Enabled="false" Width="10px" AutoPostBack="true" GroupName="a" TabIndex="7" OnCheckedChanged="rdExpiry_CheckedChanged"  Visible="false" />
                                                </asp:Label>
                                            </li>

                                        </div>

                                        <div style="float: left; width: 285px;">

                                            <li id="liBillType" runat="server">

                                                <asp:Label class="label" ID="Label2" runat="server" Text="Bill Type"></asp:Label>
                                                <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlBillType" Width="117px" TabIndex="8"></asp:DropDownList>
                                                <asp:Button ID="btnBillType" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Bill Type required" ControlToValidate="ddlBillType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="imgPurchase"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                                </cc:ValidatorCalloutExtender>
                                              <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Bill Type required" ControlToValidate="ddlBillType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                                </cc:ValidatorCalloutExtender>--%>

                                            </li>
                                            <li id="liPurchase" runat="server">

                                                <asp:Label class="label" ID="Label3" runat="server" Text="Purchase" Width="136px"></asp:Label>
                                                <div style="display: none">
                                                    <asp:TextBox ID="txtPurRefno" runat="server" AutoPostBack="false" onblur="txtPurRefno_onblur()" CssClass="textboxMandatory" TabIndex="8" onKeypress="CheckInteger(event);" Width="40px"></asp:TextBox>
                                                </div>

                                                <asp:TextBox ID="txtPurBillNo" runat="server" Width="80px" CssClass="textboxMandatory" TabIndex="9"></asp:TextBox>
                                                <asp:ImageButton ID="imgbtnPurBill" runat="server" OnClick="imgbtnPurBill_Click" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 0px 0px; position: absolute" ValidationGroup="imgPurchase" />

                                                <asp:RequiredFieldValidator ID="rfvPurRefno" runat="server" ErrorMessage="Purchase Refno required" ControlToValidate="txtPurRefno"
                                                    Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfvPurRefno">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:RequiredFieldValidator ID="rfvPurBillNo" runat="server" ErrorMessage="Bill No required"
                                                    ControlToValidate="txtPurBillNo" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="rfvPurBillNo">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                        </div>
                                    </div>



                                    <div class="clear"></div>

                                    <div class="divSeparation" style="width: 106%"></div>



                                    <div class="clear"></div>
                                    <li class="clear"></li>
                                    <div id="divItemPurchaseReturn" runat="server">
                                        <div style="width: 1000px" id="divItems" runat="server">
                                            <div class="divL" style="width: 400px;">
                                                <li id="liCategory" runat="server">
                                                    <asp:Label ID="lblcat" runat="server" class="label" Width="138" Text="Category"></asp:Label>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="11" CssClass="dropdown" Width="150px"></asp:DropDownList>

                                                </li>
                                                <li>
                                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item" Width="138px"></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="textboxMandatory" TabIndex="12" ID="txtitem" MaxLength="20" AutoComplete="off" Width="116px" onblur="return popUpStockSelect();" />

                                                    <asp:ImageButton ID="ImageButton3" runat="server" onkeypress="return popUpStockSelectClick();" OnClientClick="return popUpStockSelectClick();" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 0px 0px; position: absolute" ValidationGroup="img" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Item Required " ControlToValidate="txtitem" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender117" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>



                                                <li class="clear"></li>

                                            </div>
                                            <div class="divL" style="width: 170px; ">

                                                <li >
                                                    <asp:Label ID="Label9" runat="server" CssClass="label" Text="Qty" Width="60px"></asp:Label>
                                                    <asp:TextBox ID="txtQuantity" CssClass="textboxMandatory" Style="text-align: right;" Width="80px" runat="server" TabIndex="13" MaxLength="11" onkeypress="DecimalOrIntValidation('txtQuantity',event,'items'); " onKeyup="CalculateNetAmount(); ValidateActQty(); " AutoComplete="off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                                    </cc:ValidatorCalloutExtender>
                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" SetFocusOnError="True" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                                    </cc:ValidatorCalloutExtender>
                                                    <asp:CompareValidator ID="cmpquantity" runat="server" ErrorMessage="cannot greater than current stock" ControlToCompare="txtCurrentStock" SetFocusOnError="True" ControlToValidate="txtQuantity" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ValidationGroup="add"></asp:CompareValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="cmpquantity">
                                                    </cc:ValidatorCalloutExtender>

                                                    <asp:CustomValidator ID="cvQuantity" runat="server"
                                                        ClientValidationFunction="ValidateActQty" ControlToValidate="txtQuantity" Display="None"
                                                        CssClass="validator"
                                                        ErrorMessage="Invalid Stock" SetFocusOnError="True" ValidationGroup="add"></asp:CustomValidator>

                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="cvQuantity">
                                                    </cc:ValidatorCalloutExtender>
                                                    <asp:HiddenField ID="hdfPacked" runat="server" />
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label7" runat="server" CssClass="label" Width="60px" Text="Pur.Rate"></asp:Label>
                                                    <asp:TextBox ID="txtPurchaseRateItem" Style="text-align: right;" CssClass="textboxMandatory" Width="80px" runat="server" TabIndex="14" MaxLength="14" onKeypress="CheckNumWithDecimal(event);" onkeyup="CalculateNetAmount();" AutoComplete="off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtPurchaseRateItem" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                                    </cc:ValidatorCalloutExtender>
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPurchaseRateItem" ErrorMessage="Enter a valid Rate" Display="None" SetFocusOnError="True" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>


                                                 <li>
                                                    <asp:Label ID="Label12" runat="server" CssClass="label" Width="60px" Text="Dis .Amt"></asp:Label>
                                                    <asp:TextBox ID="txtitemdisAmt" CssClass="textbox" TabIndex="16" Width="80px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);" onkeyup="CalculateNetAmount();"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnSalesPrice" Value="" runat="server" />
                                                        <asp:HiddenField ID="hdnMRP" Value="" runat="server" />
                                                </li>
                                                <%--  <asp:Label ID="Label10" runat="server" CssClass="label" Width="50px" Text="Current Stock"></asp:Label>
                                        <asp:TextBox runat="server" ID="TextBox1" TabIndex="16" Width="80px"  CssClass="textbox" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"/>
                                        </li>--%>
                                            </div>

                                            <div style="float: left; width: 200px;">

                                                <li style="width: 165px; float: left;">
                                                    <asp:Label ID="Label4" runat="server" CssClass="label" Width="70px" Text="Free"></asp:Label>
                                                    <asp:TextBox ID="txtFree" Style="text-align: right;" CssClass="textbox" Width="80px" runat="server" TabIndex="15" MaxLength="12" onKeyup="ValidateActQty();" onkeypress="DecimalOrIntValidation(id,event,'items');" AutoComplete="off"></asp:TextBox>


                                                    <asp:CompareValidator ID="cmpFree" runat="server" ErrorMessage="cannot greater than current stock" ControlToCompare="txtCurrentStock" ControlToValidate="txtFree" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Double" ValidationGroup="add"></asp:CompareValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server" Enabled="True" TargetControlID="cmpFree">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label31" runat="server" CssClass="label" Width="70px" Text="Tax.Amt"></asp:Label>
                                                    <asp:TextBox ID="txttaxAmount" CssClass="textbox" TabIndex="16" Width="45px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                                    <asp:Button Text="PTax" onkeydown="EnterTab(event,'btnAddItems');" ToolTip="PurchaseTax" ID="btnS" Style="padding: 0px; width: 40px; height: 18px; margin: 0px 0px 0px 0px;" CssClass="button" TabIndex="17" OnClick="btnS_Click" runat="server" />
                                                </li>
                                                


                                            </div>
                                            <div style="float: left; width: 190px; margin-right: 10px">
                                                <li>
                                                    <asp:Label ID="Label5" runat="server" CssClass="label" Width="90px" Text="Current Stock"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtCurrentStock" TabIndex="18" Width="80px" CssClass="textbox" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);" />
                                                </li>

                                                <li>
                                                    <asp:Label ID="Label8" runat="server" CssClass="label" Width="90px" Text="Tot.Amt"></asp:Label>
                                                    <asp:TextBox ID="txtTotalAmountItem" Style="text-align: right;" CssClass="textbox" Width="80px" runat="server" TabIndex="19" MaxLength="12" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>
                                                </li>

                                            </div>
                                             <%--<div style="float: left; width: 190px; margin-right: 10px">
                                                <li>
                                                    <asp:Label ID="Label11" runat="server" CssClass="label" Width="70px" Text="Dis. Amt"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtitemdisAmt" TabIndex="18" Width="80px" CssClass="textbox" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);" onKeyup="CalculateItemDiscountDetails(); " AutoComplete="off" />
                                                
                                                </li>
                                            </div>--%>
                                        </div>

                                        <div class="clear"></div>
                                        <%--      <div>
                                        <li>
                                            <asp:Button Text="Add" ID="btnAddItems" runat="server" OnClick="btnAddItems_Click" TabIndex="18" ValidationGroup="add" CssClass="button" />

                                        </li>
                                    </div>
                                    <div style="float: left; width: 80px; margin-right: 4px">
                                        <li>
                                            <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="19" OnClientClick="ItemClear();return false;" />
                                        </li>
                                    </div>--%>
                                        <li class="btnMargin" id="liinsert" runat="server">
                                            <asp:Button Text="Insert" onkeydown="EnterTab(event,'btnClearItems');" ID="btnAddItems" runat="server" OnClick="btnAddItems_Click" TabIndex="20" ValidationGroup="add" CssClass="button" />
                                            <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="21" OnClientClick="ItemClear();return false;" />
                                        </li>

                                        <div class="clear"></div>
                                        <div class="divSeparation" style="width: 106%"></div>
                                        <li style="width: 100%; min-height: 310px; max-height: 510px; overflow-y: auto;">
                                            <asp:GridView ID="gvItems" runat="server" Width="96%"
                                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                                BackColor="White" BorderStyle="Solid" CssClass="grid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items,FK_Unit" OnRowDeleting="gvItems_RowDeleting" PageSize="100">

                                                <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                                <Columns>

                                                    <asp:BoundField DataField="slno" HeaderText="SlNo" HeaderStyle-Width="5px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="gridviewItemStyle" />
                                                    <asp:BoundField HeaderText="Item" DataField="Item" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="300px" />

                                                    <asp:BoundField HeaderText="Batch" ItemStyle-CssClass="gridviewItemStyle" DataField="BatchNo" HeaderStyle-Width="70px" />
                                                    <asp:BoundField HeaderText="Exp.Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="gridviewItemStyle" DataField="ExpiryDate" HeaderStyle-Width="50px" />
                                                    <asp:BoundField HeaderText="Qty" DataField="Quantity" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                                    <%-- <asp:BoundField HeaderText="ActQty" DataField="ActQty" ItemStyle-HorizontalAlign="Right" />--%>
                                                    <asp:BoundField HeaderText="Free" DataField="Free" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" />
                                                    <asp:BoundField HeaderText="Pur.Rate" DataField="PurchaseRate" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                                      <asp:BoundField HeaderText="MRP" DataField="MRP" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                                      <asp:BoundField HeaderText="Sale Price" DataField="SalesPrice" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                                    <asp:BoundField HeaderText="Tot.Amt" DataField="TotalAmt" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />

                                                    <asp:TemplateField HeaderText="Reason" HeaderStyle-Width="10px" ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlReason" runat="server" TabIndex="70" CssClass="dropdownMandatory" Width="150px"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select a Reason"
                                                                ControlToValidate="ddlReason" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                            </cc:ValidatorCalloutExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:BoundField HeaderText="Disc.Amt" DataField="PurchaseDiscAmt" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton255" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                        </ItemTemplate>
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

                                        <asp:Button ID="hiddendiscountAmount" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="hiddendiscountAmount_Click" />


                                    </div>

                                    <div class="clear"></div>
                                    <div id="gvpi" runat="server" visible="false">
                                        <li id="gvpi1" runat="server" style="width: 95%; min-height: 235px; max-height: 235px; overflow: auto">

                                            <asp:GridView ID="gvPurchaseItems" runat="server" DataKeyNames="FK_Items,FK_Units,FK_Stock,ExpiryDate,ActQty,Free,MRP,FreeQty,PurchaseDiscPercentage"
                                                EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="1200px"
                                                BackColor="White" CssClass="grid" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10"
                                                OnRowDeleting="gvPurchaseItems_RowDeleting" OnRowDataBound="gvPurchaseItems_RowDataBound">
                                                <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                                <Columns>
                                                    <asp:BoundField DataField="SLNO" HeaderText="SlNo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                                                        <HeaderStyle Width="30px" />
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Item" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="gridviewItemStyle" ItemStyle-Width="50px">

                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblItem" Style="width: 200px;" Text='<%# Eval("Item") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="200px" />
                                                        <ItemStyle Width="200px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" ItemStyle-Width="40px">

                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtQuantity"  onBlur="CalculateAmount(id,'txtQuantity');" AutoComplete="off" CssClass="textbox" Style="width: 40px; text-align: right;" onkeypress="DecimalOrIntValidation(id,event,'txtQuantity');" Text='<%# Eval("Quantity") %>' />
                                                          <%--    <asp:TextBox runat="server" ID="TextBox1" ReadOnly="true" onBlur="CalculateAmount(id,'txtQuantity');" AutoComplete="off" CssClass="textbox" Style="width: 40px; text-align: right;" onkeypress="DecimalOrIntValidation(id,event,'txtQuantity');" Text='<%# Eval("Quantity") %>' />--%>
                                                            <%--  <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quatity" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValtxidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                                    </cc:ValidatorCalloutExtender>--%>

                                                            <asp:HiddenField ID="hfStockID" runat="server" Value='<%# Eval("FK_Stock") %>' />
                                                            <asp:HiddenField ID="hdfQty" runat="server" Value='<%# Eval("Quantity") %>' />
                                                            <asp:HiddenField ID="hdfPacked" runat="server" Value='<%# Eval("Packed") %>' />
                                                            <asp:HiddenField ID="hdfBalanceQuantity" runat="server" Value='<%# Eval("BalanceQuantity") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Free" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">

                                                        <ItemTemplate>
                                                              <asp:TextBox runat="server" ID="txtFreeQty" onBlur="CalculateAmount(id,'txtFreeQty');" onkeypress="DecimalOrIntValidation(id,event,'txtFreeQty');" AutoComplete="off" CssClass="textbox" Style="width: 50px; text-align: right;"
                                                                Text='<%# Eval("Free") %>' />
                                                          <%--  <asp:TextBox runat="server" ID="txtFreeQty" onBlur="CalculateAmount(id,'txtFreeQty');" onkeypress="DecimalOrIntValidation(id,event,'txtFreeQty');" AutoComplete="off" CssClass="textbox" Style="width: 50px; text-align: right;"
                                                                Text='<%# Eval("Free") %>' />--%>

                                                            <%--                                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtFreeQty" ErrorMessage="Enter a valid Quatity" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValtxidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RangeValidator3">
                                                    </cc:ValidatorCalloutExtender>--%>

                                                            <asp:HiddenField ID="hdfFree" runat="server" Value='<%#Eval("Free") %>' />
                                                            <asp:HiddenField ID="hdfBalanceFree" runat="server" Value='<%# Eval("BalanceFree") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Act.Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtActQty"   onBlur="CalculateAmount(id,'txtActQty');" onkeypress="DecimalOrIntValidation(id,event,'txtActQty');" AutoComplete="off" CssClass="textbox" Style="width: 50px; text-align: right;"
                                                                Text='<%# Eval("ActQty") %>' />
                                                            <%--  <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtActQty" ErrorMessage="Enter a valid Quatity" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValtxidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RangeValidator4">
                                                    </cc:ValidatorCalloutExtender>--%>
                                                            <asp:CustomValidator ID="cv1" runat="server" ControlToValidate="txtActQty" ErrorMessage="Enter a valid Quantity"
                                                                Display="None" ValidationGroup="submit" ClientValidationFunction="ClientValidate"></asp:CustomValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="cv1">
                                                            </cc:ValidatorCalloutExtender>
                                                            <asp:HiddenField ID="hdfActQty" runat="server" Value='<%#Eval("ActQty") %>' />
                                                            <asp:HiddenField ID="hdfNoofUnits" runat="server" Value='<%# Eval("NoofUnits") %>' />

                                                            <%--<asp:HiddenField ID="hdtaxAmount" runat="server" Value='<%# Eval("PurchasetaxAmount")%>' />--%>
                                                            <asp:HiddenField ID="hdfBalanceActQty" runat="server" Value='<%# Eval("BalanceActQty") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>



                                                    <asp:BoundField HeaderText="Unit" DataField="Unit" HeaderStyle-Width="30px" ItemStyle-CssClass="gridviewItemStyle" ItemStyle-Width="30px">

                                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="30px" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Batch" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="70px" ItemStyle-Width="70px">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBatchNo" runat="server"><%#Eval("BatchNo") %></asp:Label>
                                                            <asp:HiddenField ID="hdfBatchNo" runat="server" Value='<%#Eval("BatchNo") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="70px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Pur.Rate" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">

                                                        <ItemTemplate>

                                                            <asp:Label ID="lblPurchaseRate" runat="server" Style="float: right;" Text='<%#Eval("PurchaseRate") %>'></asp:Label>



                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tot.Amt" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAmt" runat="server"><%#Eval("TotalAmt") %></asp:Label>
                                                            <asp:HiddenField ID="hdfTotalAmt" runat="server" Value='<%#Eval("TotalAmt") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="MRP" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRP" runat="server" Style="float: right;" Text='<%#Eval("MRP") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdfMRP" runat="server" Value='<%#Eval("MRP") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sal.Amt" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSalesPrice" runat="server" Style="float: right;" Text='<%#Eval("SalesPrice") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdfSalesPrice" runat="server" Value='<%#Eval("SalesPrice") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px" ItemStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlReason" runat="server" CssClass="dropdownMandatory" Width="150px">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlReason" Display="None" ErrorMessage="Select a Reason" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                            </cc:ValidatorCalloutExtender>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                        <ItemStyle HorizontalAlign="Left" Width="40px" />
                                                    </asp:TemplateField>

                                                    <%--                                                 <asp:BoundField DataField="PurchaseDiscPercentage" HeaderText="PurchaseDiscPercentage" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" ItemStyle-Width="30px" Visible="false" >

                                                <HeaderStyle Width="30px" />
                                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:BoundField>--%>
                                                    <asp:TemplateField HeaderText="Discount" Visible="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDisc" runat="server" Style="float: right;" Text='<%#Eval("PurchaseDiscPercentage") %>'></asp:Label>

                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Disc.Amt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10px" ItemStyle-Width="10px">

                                                        <ItemTemplate>
                                                           <%--  <asp:Label ID="lblDiscAmta" runat="server" Style="float: right;" Text='<%#Eval("PurchaseDiscAmnt") %>'></asp:Label>--%>
                                                            <asp:TextBox ID="lblDiscAmt"  Width="50px" runat="server" Style="float: right;" Text='<%#Eval("PurchaseDiscAmnt") %>'>
                                                            </asp:TextBox>
                                                            <asp:HiddenField ID="hdfDiscAmnt" runat="server" Value='<%# Eval("PurchaseDiscAmnt") %>' />

                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Width="0px">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSingleDiscount" runat="server" Style="float: right; display: none" Text='<%#Eval("SingleDisAmnt") %>'></asp:Label>
                                                            <%--<asp:HiddenField ID="hdfDiscAmnt" runat="server" Value='<%# Eval("PurchaseDiscAmnt") %>' />--%>
                                                        </ItemTemplate>

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
                                    <%--  </ContentTemplate>
                                        
                                    </asp:UpdatePanel>--%>

                                    <div class="divSeparation" style="width: 106%"></div>

                                    <div class="divL">


                                      <li>
    <label class="label">Remark</label>

   <div class="remark-container">
    <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine"
        CssClass="multiline fixed-remark"
        MaxLength="500" TabIndex="22" AutoComplete="off"
        onkeyup="updateRemarkCounter();"
        onpaste="setTimeout(updateRemarkCounter, 0);" />

    <span id="<%= txtRemark.ClientID %>_counter" class="remark-counter">
        500 characters remaining
    </span>
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
                                        <asp:HiddenField ID="hfFK_unit" runat="server" />

                                        <asp:HiddenField ID="hdfFKStock" runat="server" />


                                        <asp:HiddenField ID="hdfFkItem" runat="server" />
                                        <asp:HiddenField ID="hfItem" runat="server" />
                                        <asp:HiddenField ID="hfBillNo" runat="server" />
                                        <asp:HiddenField ID="hfInvNo" runat="server" />
                                        <asp:HiddenField ID="hfItemId" runat="server" />
                                        <asp:HiddenField ID="hfGroupID" runat="server" />
                                        <asp:HiddenField ID="hfStockID" runat="server" />
                                        <asp:HiddenField ID="hfQtyValidation" runat="server" />
                                          <asp:HiddenField ID="hfIsApprove" runat="server" />
                                        <asp:HiddenField ID="hfeditpurchaase" runat="server" />
                                      
                                        <asp:Button ID="btnItem" runat="server" Text="Button" OnClick="btnItem_Click" CausesValidation="false" Style="display: none;" />
                                    </div>

                                    <div class="divR" style="padding-right: 0px">


                                        <li>
                                            <label class="label">Total Amount</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtTotalAmount" onKeypress="CheckNumWithDecimal(event);" Style="text-align: right;" TabIndex="24" AutoComplete="off" />




                                        </li>
                                        <li>
                                            <label class="label">Discount %</label>
                                            <asp:TextBox runat="server" CssClass="textbox" onkeyup="return Total();" ID="txtDiscount" Style="text-align: right;" TabIndex="25" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />

                                           <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtDiscount" ErrorMessage="Discount % must be between 0 and 100" Display="None" MaximumValue="100" MinimumValue="0" Type="Double" ValidationGroup="submit" />
                                        </li>
                                        <li>
                                            <label class="label">Discount Amount</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtDiscountAmt" onkeyup="return Total();" Style="text-align: right;" TabIndex="26" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />
                                            <asp:CompareValidator ID="cmpDiscAmt" runat="server" ControlToValidate="txtDiscountAmt" ControlToCompare="txtTotalAmount" Operator="LessThanEqual" Type="Double"  ErrorMessage="Discount Amount cannot exceed Total Amount" Display="None" ValidationGroup="submit" />
                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtDiscountAmt" ErrorMessage="Enter a valid Discount Amount" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                        </li>
                                        <label class="label">Round Off</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtRoundOff" Style="text-align: right;" TabIndex="27" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />



                                        <li>
                                            <label class="label">Net Amount</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtNetAmount" Style="text-align: right;" TabIndex="28" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />


                                        </li>
                                         <li style="display: none">
                                                <div class="rbtn">
                                                    <asp:Label ID="Label22" runat="server" CssClass="label" Width="80px" Text="Inter State Purchase"></asp:Label>
                                                    <asp:CheckBox ID="chkInterState" runat="server" AutoPostBack="true" OnCheckedChanged="chkInterState_CheckedChanged" Style="width: 100px; padding-right: 50px;" TabIndex="7"  />
                                                </div>
                                            </li>
                                     <li style="display: none">

                                                    <asp:Label ID="lblIncdTax" runat="server" CssClass="label" Width="138px" Text="Include Tax"></asp:Label>
                                                    <%--<asp:TextBox runat="server" ID="TextBox1" TabIndex="16" Width="143px" CssClass="textbox" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);" />--%>
                                                    <asp:CheckBox ID="cbxIncludeTax" onkeydown="return EnterTab(event,'cbxDiscountTax');" runat="server" TabIndex="17" onclick="getpurchaseamount();getSalesPrice(0);" />
                                                 

                                                </li>
                                        <li class="clear"></li>


                                    </div>
                                    <div class="clear"></div>
                                    <li class="btnMargin">
                                        <asp:HiddenField ID="hfPurchaseReturn" runat="server" />
                                     <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="29" OnClientClick="return ValidateBeforeSubmit();"  />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" CausesValidation="false" TabIndex="30" OnClick="btnClear_Click1" />
                                            <asp:Button Text="Approve" runat="server" CssClass="button" ID="btnApprove" ValidationGroup="submit" OnClick="btnApprove_Click" TabIndex="31" OnClientClick="ConfirmApprove();" />
                                        <%--<asp:Button ID="btnStoreBlurTrigger" runat="server" Text="Item" OnClick="ddlStore_SelectedIndexChanged" Style="display: none;" />--%>

                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfuserName" runat="server" />
                                        <asp:HiddenField ID="hfUnitQuantity" runat="server" />
                                          <asp:HiddenField ID="hfTotTaxPercentage" runat="server" />
                                           <asp:HiddenField ID="hdnapproveddelet" runat="server" />
                                    </li>
                            </div>


                            <div class="clear"></div>
                            <div class="main_bg">
                                <div id="divView" runat="server" style="display: none; height: 485px">



                                    <ul class="contentul">
                                        <div class="divL">
                                            <li>
                                                <label class="label">Supplier</label>
                                                <asp:DropDownList ID="ddlSearchSupplier" runat="server" CssClass="dropdown" TabIndex="33"></asp:DropDownList>
                                            </li>

                                            <li style="display: none">
                                                <label class="label">Store</label>
                                                <asp:DropDownList ID="ddlSearchStore" runat="server" CssClass="dropdown"></asp:DropDownList>

                                            </li>
                                            <li>
                                                <label class="label">From Date</label>
                                                <asp:TextBox ID="txtFromDate" CssClass="textbox" runat="server" TabIndex="34"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                </cc:CalendarExtender>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                                </cc:ValidatorCalloutExtender>
                                            </li>

                                            <li>
                                                <label class="label">To Date</label>
                                                <asp:TextBox ID="txtTodate" CssClass="textbox" runat="server" TabIndex="35"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtTodate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                </cc:CalendarExtender>
                                            </li>

                                            <li class="btnMargin">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="36" />
                                                <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="37" />
                                            </li>
                                        </div>
                                        <div class="divR">
                                        </div>

                                    </ul>
                                    <br class="clear" />
                                    <ul class="contentul">
                                        <asp:GridView ID="gvPurchaseReturn" runat="server" CssClass="grid" Style="margin-left: 1px;" Width="95%"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                            BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" OnPageIndexChanging="gvPurchaseReturn_PageIndexChanging" DataKeyNames="ID_PurchaseReturn,Approved" OnSelectedIndexChanging="gvPurchaseReturn_SelectedIndexChanging"
                                             OnRowDeleting="gvPurchaseReturn_RowDeleting" OnRowCommand="gvPurchaseReturn_RowCommand" OnRowDataBound="gvPurchaseReturn_RowDataBound">
                                            <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                            <Columns>
                                                <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                                                <asp:BoundField DataField="BillDate" HeaderText="Date" />
                                                <asp:BoundField DataField="NetAmount" HeaderText="NetAmount" />
                                                <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                                <asp:BoundField DataField="Store" HeaderText="Store" />

                                                 <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnApprovedStatus" CssClass="button" Width="75px" Text='<%# Bind("ApprovedStatus") %>' runat="server" OnClientClick="ConfirmPurchaseApprove();" CommandName="ApprovedStatus" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                                        <div class="Pager">
                                            <asp:Repeater ID="rptPager" runat="server">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" OnClick="Page_Changed" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
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
                            <asp:Button ID="btnPurRet" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                            <asp:Panel ID="divpopP" runat="server" Style="display: none;" CssClass="DivStyle">
                                <div id="divH" runat="server" class="PopupHeader">
                                    <asp:Label ID="lblpurr" runat="server" Text="Account Head" CssClass="lblPopupHeader"></asp:Label>
                                    <asp:ImageButton ID="imcls" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                </div>

                                <uc5:PopupPurchaseReturn ID="PopUpPurRet" runat="server" />
                            </asp:Panel>
                            <cc:ModalPopupExtender ID="mpPurre" runat="server" PopupControlID="divpopP" TargetControlID="btnPurRet"
                                CancelControlID="imcls" Drag="true" PopupDragHandleControlID="divH">
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
                            <asp:Button ID="btnP" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                            <asp:Panel ID="pnlPrvlg" runat="server" Style="display: none;" CssClass="DivStyle">
                                <div id="divPrvlg" runat="server" class="PopupHeader">
                                    <asp:Label ID="Label15" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
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
                <%--                    <div class="loading" align="center" >
    Processing. Please wait.<br />
    <br />
      <asp:Image id="Image1" runat="server" style="margin-top:50px;"
           AlternateText="Image text"
           ImageAlign="Middle"
           ImageUrl="~/images/loader2.gif"/>           
</div>--%>
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

                if (!ValidatePurchaseReturnDate()) {
                    return false;
                }
                if (Page_ClientValidate("submit")) {
                    //setTimeout(function () {


                    //    var loading = $(".loading");
                    //    loading.show();
                    //    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    //    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    //    loading.css({ top: top, left: left });
                    //},1000);
                }
            }
            //$('form').live("submit", function () {
            //    //ShowProgress();
            //});


            function ClearAdd() {
                submit = 0;
                $("[id*='txt']").not("txtDate").val('');
                $("#txtRefNo").val('');
                $("#ddlSupplier").val('0');
                //    $("#ddlStore").val('0');
                var d = new Date();
                $("#txtDate").val(d.format('dd/MM/yyyy'));
                $("#txtTotalAmount").val('0');
                $("#txtDiscount").val('0');
                $("#txtDiscountAmt").val('0');
                $("#txtNetAmount").val('0');
                $("#<%= txtRemark.ClientID %>").val("");
                updateRemarkCounter();
                $("#cbxIsCredit").prop("checked", false);
                $("#cbxAccountNeeded").prop("checked", true);
                $("#hfPurchaseReturn").val('');
                $("#hfBillNo").val('0');
                $("#txtFromDate,#txtTodate").val(d.format('dd/MM/yyyy'));
                // $("[id*='ddl']").val('0');
                $("#ddlPurchase").val('0');
                $("#txtVAT").val('0');
                $("#txtRoundOff").val("0");
                $("#hfInvNo").val('');
                $("#ddlBillType").val('0');

                ItemClear();
                //  return false;
            }

            //$('#hdfFKStock').val('send this');
            //__doPostBack('hdfFKStock');


            function CalculateAmount(id, type) {
                debugger;
                if (type == 'txtFreeQty') {
                    var txtFreeQty = id;
                    var txtQuantity = id.replace(type, 'txtQuantity');
                    var txtActQty = id.replace(type, 'txtActQty');
                }
                else if (type == 'txtActQty') {
                    var txtActQty = id;
                    var txtFreeQty = id.replace(type, 'txtFreeQty');
                    var txtQuantity = id.replace(type, 'txtQuantity');
                }
                else {
                    var txtQuantity = id;
                    var txtFreeQty = id.replace(type, 'txtFreeQty');
                    var txtActQty = id.replace(type, 'txtActQty');
                }

                var lblPr = id.replace(type, 'lblPurchaseRate');
                var hdfQty = id.replace(type, 'hdfQty');
                var hdfFree = id.replace(type, 'hdfFree');
                var hdfActQty = id.replace(type, 'hdfActQty');

                var qty = parseFloat($("#" + txtQuantity).val()) || 0;
                var Free = parseFloat($("#" + txtFreeQty).val()) || 0;
                var ActQty = parseFloat($("#" + txtActQty).val()) || 0;
                var pr = parseFloat($("#" + lblPr).text()) || 0;

                // ── Determine the ceiling for each field ──────────────────────────────
                // Edit mode: use Balance fields (remaining returnable qty, already
                // calculated by the SP to exclude what this return previously saved).
                // New mode:  use the original purchased qty stored in hdfQty etc.
                var maxQty, maxFree, maxActQty;

                if ($("#hfPurchaseReturn").val() !== '') {
                    // EDIT MODE — use balance fields returned by proPurchaseWiseItemsSelect
                    var hdfBalQty = id.replace(type, 'hdfBalanceQuantity');
                    var hdfBalFree = id.replace(type, 'hdfBalanceFree');
                    var hdfBalActQty = id.replace(type, 'hdfBalanceActQty');

                    var balQty = parseFloat($("#" + hdfBalQty).val());
                    var balFree = parseFloat($("#" + hdfBalFree).val());
                    var balActQty = parseFloat($("#" + hdfBalActQty).val());

                    // Balance fields hold "original qty MINUS what other returns took".
                    // Add back whatever THIS return currently has (hdfQty / hdfFree /
                    // hdfActQty) so the user can freely re-enter up to that combined limit.
                    maxQty = (isNaN(balQty) ? parseFloat($("#" + hdfQty).val()) : balQty) + (parseFloat($("#" + hdfQty).val()) || 0);
                    maxFree = (isNaN(balFree) ? parseFloat($("#" + hdfFree).val()) : balFree) + (parseFloat($("#" + hdfFree).val()) || 0);
                    maxActQty = (isNaN(balActQty) ? parseFloat($("#" + hdfActQty).val()) : balActQty) + (parseFloat($("#" + hdfActQty).val()) || 0);
                }
                else {
                    // NEW mode — ceiling is simply the original purchase qty
                    maxQty = parseFloat($("#" + hdfQty).val()) || 0;
                    maxFree = parseFloat($("#" + hdfFree).val()) || 0;
                    maxActQty = parseFloat($("#" + hdfActQty).val()) || 0;
                }

                // ── Validate and reset if over the ceiling ────────────────────────────
                if (qty > maxQty) {
                    alertify.alert("Quantity Cannot Exceed Available Stock");
                    $("#" + txtQuantity).val(0);
                    return false;
                }
                if (Free > maxFree) {
                    alertify.alert("Quantity Cannot Exceed Available Stock");
                    $("#" + txtFreeQty).val(0);
                    return false;
                }
                if (ActQty > maxActQty && type === 'txtActQty') {
                    alertify.alert("Quantity Cannot Exceed Available Stock");
                    $("#" + txtActQty).val(0);
                    return false;
                }

                // ── Zero-display normalisation ────────────────────────────────────────
                if (qty === 0) $("#" + txtQuantity).val(0);
                if (Free === 0) $("#" + txtFreeQty).val(0);
                if (ActQty === 0) $("#" + txtActQty).val(0);

                // ── All good — recalculate totals ─────────────────────────────────────
                CalculateTotalAmount(type);
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

            function CalculateTotalAmount(Type) {

                var Totalamt = 0;
                var TotalDisc = 0;
                var VATTotal = 0;
                var Tot;
                var CESSTotal = 0;
                var totqty = 0;

                $.each($("table[id*=" + 'gvPurchaseItems' + "] tr"), function (index, item) {
                    if ($(item).find("input[type=hidden]").length > 0) {


                        var PurPrice = parseFloat($(item).find("[id*=gvPurchaseItems_lblPurchaseRate_]").text()) || 0;

                        var Qty = $(item).find("[id*=txtQuantity_]").val();
                        var Free = $(item).find("[id*=txtFreeQty_]").val();

                        var ActQty = $(item).find("[id*=txtActQty_]").val();

                        var NoofUnits = $(item).find("[id*=hdfNoofUnits_]").val();



                        // alert($("#hfQtyValidation").val());
                        if ($("#hfQtyValidation").val() == "1") {

                            //Tot = (parseFloat(ActQty) - parseFloat(Free));

                            Tot = (parseFloat(ActQty))   //+ parseFloat(Free));
                            totqty = (parseFloat(ActQty) - (parseFloat(Free) * parseFloat(NoofUnits)))
                        }
                        else {
                            ActQty = ((parseFloat(Qty) + parseFloat(Free)) * parseFloat(NoofUnits));
                            Tot = (parseFloat(ActQty));
                            totqty = (parseFloat(ActQty) - (parseFloat(Free) * parseFloat(NoofUnits)))
                        }

                        $(item).find("[id*=txtActQty_]").val(Tot);
                        // alert(Tot);


                        if (Type != 'txtActQty') {

                            var prevActqty = 0;

                            if ($("#hfPurchaseReturn").val() == '') {

                                prevActqty = $(item).find("[id*=hdfActQty_]").val();
                            }
                            else {
                                prevActqty = parseFloat($(item).find("[id*=hdfActQty_]").val()) + parseFloat($(item).find("[id*=hdfBalanceActQty_]").val());
                            }


                            //if (ActQty <= parseFloat(prevActqty)) {

                            //  $(item).find("[id*=txtActQty_]").val(ActQty);
                            //}
                            //else {
                            //    $(item).find("[id*=txtActQty_]").val(prevActqty);

                            //}

                        }


                        //var hdtaxAmount = $(item).find("[id*=hdtaxAmount_]").val();

                        var Amt = (parseFloat(PurPrice) * parseFloat(totqty));

                        //+ parseFloat(hdtaxAmount);

                        $(this).find("[id*='lblTotalAmt_']").text((Amt).toFixed(4));
                        Totalamt += Amt;
                        // var Disc = $(item).find("[id*=gvPurchaseItems_lblDiscAmt_]").text();

                        // if ($("#rdPurchase").prop("checked", true));
                        if ($('#rdPurchase').is(":checked")) {

                            var Qty = $(item).find("[id*=txtQuantity_]").val();
                            // alert(Qty);

                            var SinglAmount = parseFloat($(item).find("[id*=lblSingleDiscount_]").text()) || 0;
                            // alert(SinglAmount);
                            var DiscAmount = (parseFloat(Qty) || 0) * SinglAmount;

                            $(this).find("[id*='lblDiscAmt_']").val((DiscAmount).toFixed(4));

                            TotalDisc += parseFloat(DiscAmount);
                            //$("#hiddendiscountAmount").click();

                            // var a = $(this).find("[id*='lblDiscAmt_']").text();
                            //alert(a);
                        }

                    }
                });
                $('#hfQtyValidation').val('0');

                $("#txtTotalAmount").val(Totalamt.toFixed(4));
                if ($('#rdPurchase').is(":checked")) {

                    $("#txtDiscountAmt").val(TotalDisc.toFixed(4));
                }

                Total();

            }

            function CheckKeys() {
                alert(e.keyCode);
            }

            function ClearSearch() {
                $("#ddlSearchSupplier ").val("0");
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

            function buttonClick() {
                $("#btnHidden").click();
                return false;
            }

            function BlurItem() {

                $("#btnItemBlurTrigger").click();

            }
            //function BlurStore() {

            //  //  $("#btnStoreBlurTrigger").click();

            //}
            function SetStoreDetail() {
                var store = $("#ddlStore option:selected").val();
                $("#hfStore").val(store);

                //  ItemClear();

            }

            function pageLoad() {

                var ctrContainer = "form";
                setKeynavigation(ctrContainer);
                Check();



            }


            function ItemClear() {

                $("#hfItemId").val("");
                $("#ddlItem").val("0");
                $("#ddlUnit").val("0");
                $("#txtBatch").val("");
                $("#txtitem").val("");

                $("#txtActQty").val("0");
                $("#txtFree").val("0");

                $("#txtQuantity").val("0");
                $("#txtCurrentStock").val("0");
                $("#ddlUnit").val('');
                $("#txtPurchaseRate").val("0");
                $("#txtItemVat").val('0');
                $("#txtItemVatAmount").val('0');
                $("#txtPurchaseRateItem").val('0');
                $("#txtTotalAmountItem").val('0');
                $("#txttaxAmount").val('0');

                $("#txtitemdisAmt").val('0');
                $("#hdnMRP").val('0');
                $("#hdnSalesPrice").val('0');


            }

            var popup;
            function popUpStockSelectClick() {

                if (Page_ClientValidate("img") == true) {

                    //var popup;
                    //var ItemName = $("#txtitem").val();
                    //SetStoreDetail();
                    //popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&suplr=' + $("#txtSupRefno").val(), "Popup", "width=1000,height=800");
                    //popup.focus();

                    var popup;
                    var ItemName = $("#txtitem").val();
                    var hfUser = $("#hfUser").val();
                    var hfClient = $("#hfClient").val();
                    var ddlCategory = $("#ddlCategory").val();
                    var hfStore = $("#hfStore").val();
                    var txtSupRefno = $("#txtSupRefno").val();
                    $.ajax({
                        type: "POST",
                        url: 'Textile.asmx/GetUrlEncryptStock',
                        data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','txtSupRefno':'" + txtSupRefno + "','hfStore':'" + hfStore + "','hfClient':'" + hfClient + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (MenuHtml) {

                            // url = "PreviousPurchase.aspx?" + MenuHtml.d;
                            SetStoreDetail();
                            url = "StockItems.aspx?" + MenuHtml.d;

                            popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                            ",statusbar=no,menubar=no,resizable=0,width=1000," +
                            "height=800,left = 200,top = 100");
                            popUpObj.focus();
                            //popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&suplr=' + $("#txtSupRefno").val(), "Popup", "width=1000,height=800");
                            //popup = window.open("StockItems.aspx?" + +MenuHtml.d, "Popup", "width=1000,height=800");
                            //popup.focus();
                        },
                        error: function (MenuHtml) {
                            // alert('GetMinItemStockName');
                            //alert('webservice_error');
                            // $("#hfEncryptvalue").val("");
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
                    var ddlCategory = $("#ddlCategory").val();
                    var hfStore = $("#hfStore").val();
                    var txtSupRefno = $("#txtSupRefno").val();
                    if (ItemName.length > 0) {
                        if ($("#hfStore").val() == '') {
                            SetStoreDetail();
                            //BlurStore();
                            //$("#hfStore").val($("#ddlStore").val())
                        }
                        $.ajax({
                            type: "POST",
                            url: 'Textile.asmx/GetUrlEncryptStock',
                            data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','txtSupRefno':'" + txtSupRefno + "','hfStore':'" + hfStore + "','hfClient':'" + hfClient + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (MenuHtml) {

                                // url = "PreviousPurchase.aspx?" + MenuHtml.d;
                                SetStoreDetail();
                                url = "StockItems.aspx?" + MenuHtml.d;

                                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                                ",statusbar=no,menubar=no,resizable=0,width=1000," +
                                "height=800,left = 200,top = 100");
                                popUpObj.focus();
                                //popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&suplr=' + $("#txtSupRefno").val(), "Popup", "width=1000,height=800");
                                //popup = window.open("StockItems.aspx?" + +MenuHtml.d, "Popup", "width=1000,height=800");
                                //popup.focus();
                            },
                            error: function (MenuHtml) {
                                // alert('GetMinItemStockName');
                                //alert('webservice_error');
                                // $("#hfEncryptvalue").val("");
                            }
                        });
                        //popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&suplr=' + $("#txtSupRefno").val(), "Popup", "width=1000,height=800") + '&ctgry=' + $("#ddlCategory").val();
                        //popup.focus();
                    }
                }

                return false;
            }




            function CalculateNetAmount() {
                try {

                    var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
                    var ItemVat = $("#txtItemVat").val() == "" ? "0" : $("#txtItemVat").val();
                    var PurchaseRateItem = $("#txtPurchaseRateItem").val() == "" ? "0" : $("#txtPurchaseRateItem").val();
                    var TotalAmountItem = $("#txtTotalAmountItem").val() == "" ? "0" : $("#txtTotalAmountItem").val();
                    var ItemVatAmount = $("#txtItemVatAmount").val() == "" ? "0" : $("#txtItemVatAmount").val();
                    var ItemDisAmount = $("#txtitemdisAmt").val() == "" ? "0" : $("#txtitemdisAmt").val();

                    var CESS = $("#txtCESS").val() == "" ? "0" : $("#txtCESS").val();
                    var CESSAmt = 0;

                    if (parseFloat(Quantity) > parseFloat(0)) {
                        if (parseFloat(PurchaseRateItem) > parseFloat(0)) {
                            if (parseFloat(ItemVat) > parseFloat(0)) {
                                ItemVatAmount = parseFloat(Quantity) * parseFloat(PurchaseRateItem) * parseFloat(ItemVat) / parseFloat(100);
                            }
                            else {

                                ItemVatAmount = parseFloat(0);
                            }

                            if (parseFloat(CESS) > parseFloat(0)) {
                                CESSAmt = parseFloat(Quantity) * parseFloat(PurchaseRateItem) * parseFloat(CESS) / parseFloat(100);
                            }



                            TotalAmountItem = parseFloat(Quantity) * parseFloat(PurchaseRateItem) + parseFloat(ItemVatAmount) + parseFloat(CESSAmt) - parseFloat(ItemDisAmount);

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



            function ValidateActQty(source, arguments) {
                debugger;
                var txtQuantity = $("#txtQuantity").val();
                var txtFree = $("#txtFree").val();
                var txtCurrentStock = $("#txtCurrentStock").val();
                var Total = parseFloat(txtQuantity) + parseFloat(txtFree);
                var CurrentStock = parseFloat(txtCurrentStock);
                if (Total > CurrentStock) {

                    arguments.IsValid = false;

                }
                else {
                    arguments.IsValid = true;
                }
                calculateActualQuantity();
                //getpurchaseamount();
            }


            function Total() {
                try {
                    var TotalAmount = parseFloat($("#txtTotalAmount").val()) || 0;
                    var DiscountAmt = parseFloat($("#txtDiscountAmt").val()) || 0;
                    var Discount = parseFloat($("#txtDiscount").val()) || 0;

                    if (TotalAmount <= 0) {
                        $("#txtDiscount").val('0');
                        $("#txtDiscountAmt").val('0');
                        $("#txtNetAmount").val('0');
                        $("#txtRoundOff").val('0');
                        return;
                    }

                    var total = 0;

                    if ($('#rdPurchase').is(":checked")) {
                        if (DiscountAmt > TotalAmount) {
                            DiscountAmt = TotalAmount;
                            $("#txtDiscountAmt").val(DiscountAmt.toFixed(4));
                        }
                        var DiscountPer = (DiscountAmt * 100) / TotalAmount;

                        // Guard against both NaN and Infinity
                        $("#txtDiscount").val(
                            (isFinite(DiscountPer) && !isNaN(DiscountPer))
                                ? DiscountPer.toFixed(4)
                                : '0'
                        );
                        total = TotalAmount - DiscountAmt;
                    }
                    else {
                        var dAmt = (TotalAmount * Discount) / 100;
                        dAmt = (isNaN(dAmt) || !isFinite(dAmt)) ? 0 : dAmt;  // guard here
                        $("#txtDiscountAmt").val(dAmt.toFixed(4));
                        total = TotalAmount - dAmt;
                    }

                    if (total < 0) {
                        $("#txtDiscount").val('0');
                        $("#txtDiscountAmt").val('0');
                        Total();
                        return;
                    }

                    var fracpart = parseFloat(((total % 1) * 100).toFixed(2));
                    if (isNaN(fracpart) || !isFinite(fracpart)) fracpart = 0;  // guard here

                    if (fracpart >= 50) {
                        var roundUp = (100 - fracpart) / 100;
                        $("#txtNetAmount").val((total + roundUp).toFixed(2));
                        $("#txtRoundOff").val(roundUp.toFixed(2));
                    }
                    else {
                        var roundDown = fracpart / 100;
                        $("#txtNetAmount").val((total - roundDown).toFixed(2));
                        $("#txtRoundOff").val(('-' + roundDown.toFixed(2)));
                    }
                }
                catch (ex) {
                    alert(ex.message);
                }
            }
            function EnterTab(e, Id1) {

                var intKey = window.Event ? e.which : e.KeyCode;


                if (intKey == 13) {
                    e.returnValue = false;
                    $("#" + Id1).focus();
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
            function txtSupRefno_onblur() {
                $("#txtSupplier").val('');
                return false;
            }
            function txtPurRefno_onblur() {
                $("#txtPurBillNo").val('');
                return false;
            }


            function ClientValidate(source, arguments) {
                try {
                    var id = source.id;
                    var txtFreeQty = id.replace("cv1", "txtFreeQty");
                    var FreeQty = document.getElementById(txtFreeQty).value

                    var qty = arguments.Value;
                    if (FreeQty == 0 && qty == 0) {
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


            function calculateActualQuantity() {
                var qty = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
                var free = $("#txtFree").val() == "" ? "0" : $("#txtFree").val();

                var nounit = $("#hfUnitQuantity").val() == "" ? "0" : $("#hfUnitQuantity").val();

                var ActQty = parseFloat(nounit) * parseFloat(qty);

                //var SubunitNo = $("#hfSubUnitQuantity").val() == "" ? "0" : $("#hfSubUnitQuantity").val();

                $("#txtCheckActQuantity").val(ActQty.toFixed(4));

                //if (SubunitNo != 0)
                //    var Actquantity = (parseFloat(qty) + parseFloat(free)) * (parseFloat(nounit)) * (parseFloat(SubunitNo));
                //else
                var Actquantity = (parseFloat(qty) + parseFloat(free)) * (parseFloat(nounit));


                $("#txtActqty").val(Actquantity.toFixed(4));
                getpurchaseamount();


            }
            function getpurchaseamount() {

                var PurchaseRate = $("#txtPurchaseRateItem").val() == "" ? "0" : $("#txtPurchaseRateItem").val();
                //var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
                //var Quantity = $("#txtActqty").val() == "" ? "0" : $("#txtActqty").val();
                var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();

                Quantity = Quantity * parseFloat($("#hfUnitQuantity").val() == "" ? "0" : $("#hfUnitQuantity").val());
                var DiscPer = $("#txtitemdisAmt").val() == "" ? "0" : $("#txtitemdisAmt").val();
                if ($('#cbxIncludeTax').is(":checked")) {

                    var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();

                    var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

                    var PurchaseValue = (parseFloat(PurchaseRate) * parseFloat(Quantity)) - (parseFloat(taxAmount));
                    $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));
                    //if ($('#cbxDiscountTax').is(":checked")) {

                    //    var ExcludeDis = parseFloat(PurchaseRate) * parseFloat(DiscPer) / parseFloat(100);
                    //    var ActPur = parseFloat(PurchaseRate) - parseFloat(ExcludeDis);
                    //    var taxAmount = ((parseFloat(ActPur)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

                    //}
                    //else {
                    //    var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));
                    //}

                    var singleAmnt = ((parseFloat(PurchaseRate)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

                    $("#hfSingleQty").val(parseFloat(singleAmnt).toFixed(4));
                    $("#txttaxAmount").val(parseFloat(taxAmount).toFixed(4));

                }
                else {

                    var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();

                    //if ($('#cbxDiscountTax').is(":checked")) {

                    //    var ExcludeDis = parseFloat(PurchaseRate) * parseFloat(DiscPer) / parseFloat(100);
                    //    var ActPur = parseFloat(PurchaseRate) - parseFloat(ExcludeDis);
                    //    var taxAmount = ((parseFloat(ActPur) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
                    //    var singleAmnt = ((parseFloat(ActPur) * parseFloat(TotalTaxPercetage)) / parseFloat(100));
                    //}
                    //else {

                    var taxAmount = ((parseFloat(PurchaseRate) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);

                    var singleAmnt = ((parseFloat(PurchaseRate) * parseFloat(TotalTaxPercetage)) / parseFloat(100));
                    //}


                    $("#hfSingleQty").val(parseFloat(singleAmnt).toFixed(4));
                    $("#txttaxAmount").val(taxAmount.toFixed(4));
                    var PurchaseValue = parseFloat(PurchaseRate) * parseFloat(Quantity);
                    $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));

                }
                //if ($('#cbxDiscountTax').is(":checked")) {

                //    var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                //    var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
                //    var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
                //    //  var DiscountAmount = isNaN(((parseFloat(PurchaseValue) + parseFloat(taxAmount)) * (DiscPer)) / parseFloat(100)) ? 0 : ((parseFloat(PurchaseValue) + parseFloat(taxAmount)) * (DiscPer)) / parseFloat(100);
                //    var DiscountAmount = isNaN(((parseFloat(PurchaseValue)) * (DiscPer)) / parseFloat(100)) ? 0 : ((parseFloat(PurchaseValue)) * (DiscPer)) / parseFloat(100);
                //    $("#txtPurDiscAmnt").val(parseFloat(DiscountAmount).toFixed(4));


                //}

                //else {


                //    var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                //    var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
                //    var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
                //    var DiscountAmount = isNaN((parseFloat(PurchaseValue) * parseFloat(DiscPer)) / parseFloat(100)) ? 0 : (parseFloat(PurchaseValue) * parseFloat(DiscPer)) / parseFloat(100);

                //    $("#txtPurDiscAmnt").val(DiscountAmount.toFixed(4));

                //}
                var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
                var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
                var NetPurchase = (parseFloat(PurchaseValue) + parseFloat(taxAmount)) - parseFloat(DiscountAmount);
                $("#txtNetPur").val(NetPurchase.toFixed(4));
                var mrp = parseFloat($("#hfMRP").val()).toFixed(4);
                //$("#txtMRP").val((mrp * Quantity).toFixed(4));//nj
                //  getSalesPrice(0);//nj

            }
            function funItemWiseSupplierClear() {

                if ($('#rdItemPurchase').is(':checked')) {

                    $('#imgbtnSupplier').prop('disabled', false);
                    //$('#imgbtnSupplier').removeAttr('disabled');

                }
            }


            function UpdateValidationGroups() {
                var isItemWise = $('#rdItemPurchase').is(':checked') || $('#rdExpiry').is(':checked');

                // Supplier is required for Item Wise / Expiry on Submit only.
                // Do not validate here: ValidatorEnable() checks immediately and
                // pops the callout as soon as the mode changes.
                var suppValidator = document.getElementById('<%= rfvSuppName.ClientID %>');
                if (suppValidator) {
                    suppValidator.enabled = isItemWise;
                    if (!isItemWise) {
                        suppValidator.isvalid = true;
                        if (typeof ValidatorUpdateDisplay === "function") {
                            ValidatorUpdateDisplay(suppValidator);
                        }
                    }
                }

                // Purchase validators stay disabled — purchase selection is
                // checked manually in ValidateBeforeSubmit() instead.
                var purRefnoValidator = document.getElementById('<%= rfvPurRefno.ClientID %>');
    if (purRefnoValidator) ValidatorEnable(purRefnoValidator, false);

    var purBillValidator = document.getElementById('<%= rfvPurBillNo.ClientID %>');
    if (purBillValidator) ValidatorEnable(purBillValidator, false);
}




function ValidateBeforeSubmit() {
    if (!ValidatePurchaseReturnDate()) {
        return false;
    }

    // Purchase Wise: a purchase must be selected before submitting
    if ($('#<%= rdPurchase.ClientID %>').is(':checked')) {
                    if ($('#<%= txtPurBillNo.ClientID %>').val().trim() === '') {
                        alertify.alert('Please select a Purchase before submitting.');
                        $('#<%= imgbtnPurBill.ClientID %>').focus();
            return false;
        }
    }

    return true;
}

function pageLoad() {
    var ctrContainer = "form";
    setKeynavigation(ctrContainer);
    Check();

    // Disable all purchase-related validators on every page load
    // They will be re-enabled only when the relevant fields are filled
    var purRefnoValidator = document.getElementById('<%= rfvPurRefno.ClientID %>');
                if (purRefnoValidator) {
                    ValidatorEnable(purRefnoValidator, false);
                }

                var purBillValidator = document.getElementById('<%= rfvPurBillNo.ClientID %>');
    if (purBillValidator) {
        ValidatorEnable(purBillValidator, false);
    }

    var suppValidator = document.getElementById('<%= rfvSuppName.ClientID %>');
    if (suppValidator) {
        ValidatorEnable(suppValidator, false);
    }

    UpdateValidationGroups();
    updateRemarkCounter();
}

Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
    UpdateValidationGroups();
});



function enforceMaxLength(textarea, maxLength) {
    if (textarea.value.length > maxLength) {
        textarea.value = textarea.value.substring(0, maxLength);
    }
    var remaining = maxLength - textarea.value.length;
    var counterId = textarea.id + '_counter';
    var counter = document.getElementById(counterId);
    if (counter) {
        counter.innerText = remaining + ' characters remaining';
    }
}

function updateRemarkCounter() {
    var remark = document.getElementById('<%= txtRemark.ClientID %>');
                    if (remark) {
                        enforceMaxLength(remark, 500);
                    }
                }




                function parseDate_DMY(str) {
                    // Parses "dd/MM/yyyy" → Date object (time zeroed)
                    if (!str || str.trim() === '') return null;
                    var parts = str.trim().split('/');
                    if (parts.length !== 3) return null;
                    var d = parseInt(parts[0], 10);
                    var m = parseInt(parts[1], 10) - 1; // JS months are 0-based
                    var y = parseInt(parts[2], 10);
                    if (isNaN(d) || isNaN(m) || isNaN(y)) return null;
                    var dt = new Date(y, m, d, 0, 0, 0, 0);
                    // Guard against JS rolling over invalid dates (e.g. 31/02)
                    if (dt.getFullYear() !== y || dt.getMonth() !== m || dt.getDate() !== d) return null;
                    return dt;
                }


                function getToday() {
                    var t = new Date();
                    return new Date(t.getFullYear(), t.getMonth(), t.getDate(), 0, 0, 0, 0);
                }

                function getFinYearStart() {
                    // Financial year starts April 1.
                    // If today is before April 1, fin year started last calendar year.
                    var today = getToday();
                    var yr = today.getFullYear();
                    var finStart = new Date(yr, 3, 1, 0, 0, 0, 0); // April = month 3
                    if (today < finStart) {
                        finStart = new Date(yr - 1, 3, 1, 0, 0, 0, 0);
                    }
                    return finStart;
                }

                function ValidatePurchaseReturnDate() {
                    var dateStr = $("#txtDate").val();

                    if (!dateStr || dateStr.trim() === '') {
                        alertify.alert("Please enter a Return Date.");
                        $("#txtDate").focus();
                        return false;
                    }

                    var selectedDate = parseDate_DMY(dateStr);
                    if (!selectedDate) {
                        alertify.alert("Invalid date format. Please enter date as dd/MM/yyyy.");
                        $("#txtDate").focus();
                        return false;
                    }

                    var today = getToday();
                    var finStart = getFinYearStart();

                    // Rule 1 – No future dates
                    if (selectedDate > today) {
                        alertify.alert("Purchase Return date cannot be a future date.");
                        $("#txtDate").val('');
                        $("#txtDate").focus();
                        return false;
                    }

                    // Rule 2 – No dates before current financial year
                    if (selectedDate < finStart) {
                        var finStartStr = ('0' + finStart.getDate()).slice(-2) + '/'
                                        + ('0' + (finStart.getMonth() + 1)).slice(-2) + '/'
                                        + finStart.getFullYear();
                        alertify.alert(
                            "Purchase Return date cannot be from a previous financial year.\n" +
                            "Earliest allowed date is " + finStartStr + "."
                        );
                        $("#txtDate").val('');
                        $("#txtDate").focus();
                        return false;
                    }

                    return true;
                }

                $(document).ready(function () {
                    // Use event delegation — safe inside UpdatePanel
                    $(document).on('blur', '[id$="txtDate"]', function () {
                        var val = $(this).val();
                        if (val !== '') {
                            ValidatePurchaseReturnDate();
                        }
                    });
                });



                function EnablePurchaseValidators() {
                    var purRefnoValidator = document.getElementById('<%= rfvPurRefno.ClientID %>');
                    if (purRefnoValidator) {
                        ValidatorEnable(purRefnoValidator, true);
                    }
                    var purBillValidator = document.getElementById('<%= rfvPurBillNo.ClientID %>');
                if (purBillValidator) {
                    ValidatorEnable(purBillValidator, true);
                }
            }
            $(document).ready(function () {

                // Existing date-blur handler …
                $(document).on('blur', '[id$="txtDate"]', function () {
                    var val = $(this).val();
                    if (val !== '') { ValidatePurchaseReturnDate(); }
                });

                // Purchase Wise: supplier is filled from the selected purchase.
                // Do not leave focus on txtSupplier, or alertify restores it when OK
                // is clicked and the message opens again.
                var supplierAlertOpen = false;

                function focusPurchaseSearch() {
                    var purchaseButton = document.getElementById('<%= imgbtnPurBill.ClientID %>');
                    if (purchaseButton && !purchaseButton.disabled) {
                        purchaseButton.focus();
                    }
                }

                function showSupplierPurchaseHint() {
                    if (!$('#<%= rdPurchase.ClientID %>').is(':checked') || supplierAlertOpen) {
                        return;
                    }
                    supplierAlertOpen = true;
                    focusPurchaseSearch();
                    alertify.alert(
                        'Supplier is populated automatically when you select a Purchase. ' +
                        'Please use the Purchase search button to select a Purchase first.',
                        function () {
                            focusPurchaseSearch();
                            window.setTimeout(function () {
                                supplierAlertOpen = false;
                            }, 200);
                        }
                    );
                    var okButton = document.getElementById('alertify-ok');
                    if (okButton) {
                        okButton.type = 'button';
                    }
                }

                $(document).on('mousedown', '#<%= txtSupplier.ClientID %>', function (e) {
                    if ($('#<%= rdPurchase.ClientID %>').is(':checked')) {
                        e.preventDefault();
                        showSupplierPurchaseHint();
                    }
                });

                $(document).on('focus', '#<%= txtSupplier.ClientID %>', function () {
                    if (!$('#<%= rdPurchase.ClientID %>').is(':checked')) {
                        return;
                    }
                    if (supplierAlertOpen) {
                        focusPurchaseSearch();
                        return;
                    }
                    showSupplierPurchaseHint();
                });
            });
            function ValidateSupplierRef(source, arguments) {
                if ($('#<%= rdPurchase.ClientID %>').is(':checked')) { arguments.IsValid = true; return; } // auto-filled from Purchase
                var refNo = $('#<%= txtSupRefno.ClientID %>').val();
                arguments.IsValid = (refNo !== "" && !isNaN(refNo) && parseInt(refNo, 10) > 0);
            }

        </script>


    </form>
</body>
</html>
