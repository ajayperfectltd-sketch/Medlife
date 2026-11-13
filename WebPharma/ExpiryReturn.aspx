<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpiryReturn.aspx.cs" Inherits="PurchaseReturn" EnableEventValidation="false" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopUpGridSelection.ascx" TagName="CommonPopupGrid" TagPrefix="uc3" %>
<%@ Register Src="Popup/PurchaseReturn.ascx" TagName="PopupPurchaseReturn" TagPrefix="uc5" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Expiry Return::</title>
    <link href="css/main.css" rel="stylesheet" />

    <link href="css/toastr.css" rel="stylesheet" />
    <style>
        .hiddencol {
            display: none;
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
                        <div class="user">Expiry Return</div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <asp:ImageButton ID="ImageButton1" CssClass="imagebuttonStyle" ImageUrl="images/ADD.png" TabIndex="32" runat="server" CausesValidation="false" OnClientClick="ShowAddDiv();  releasebtn();" OnClick="ImageButton1_Click" />
                                <asp:ImageButton ID="imgbtnView" CssClass="imagebuttonStyle" ImageUrl="images/view.png" TabIndex="31" runat="server" OnClick="imgbtnView_Click" />
                            </div>
                        </asp:Panel>



                    </div>
                     <asp:HiddenField ID="hfMRP" runat="server" />
                       <asp:HiddenField ID="hfSalPrice" runat="server" />
                    <asp:HiddenField ID="hfPurchaseValue" runat="server" />
                    <asp:HiddenField ID="hfEditedRow" runat="server" />
                    <asp:HiddenField ID="hfSlNo" runat="server" />
                    <asp:HiddenField ID="hfSingleQty" runat="server" />
                    <asp:HiddenField ID="hfSubUnitQuantity" runat="server" />
                    <asp:HiddenField ID="hfTotTaxPercentage" runat="server" />
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                            <div id="divAdd" runat="server" style="display: block;">

                                <ul class="contentul" id="Container">
                                    <div style="width: 1500px">
                                        <div class="divL" style="width: 350px;">
                                            <li>

                                                <label class="label">Expiry Method</label>
                                                <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlExpiryMethod" onkeydown="return EnterTab(event,'txtExpiry');" Width="150px" TabIndex="1">
                                                    <%--<asp:ListItem Value="0">-Select-</asp:ListItem>--%>
                                                    <asp:ListItem Value="IER">Item wise</asp:ListItem>
                                                    <asp:ListItem Value="SER">Supplier wise</asp:ListItem>

                                                </asp:DropDownList>

                                            </li>
                                            <li>
                                                <asp:Label class="label" ID="Label12" runat="server" Text="Expiry Date"></asp:Label>

                                                <asp:TextBox runat="server" Width="143px" CssClass="textboxMandatory" ID="txtExpiry" TabIndex="2" AutoComplete="off" onkeydown=" return EnterTab(event,'txtSupplier');" />
                                                <cc:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" TargetControlID="txtExpiry" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Expiry Date required" ControlToValidate="txtExpiry" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                                </cc:ValidatorCalloutExtender>





                                            </li>
                                            <li>


                                                <label class="label">Supplier</label>
                                                <div style="display: none">
                                                    <asp:TextBox ID="txtSupRefno" runat="server" AutoPostBack="false" onblur="txtSupRefno_onblur()" CssClass="textboxMandatory" onKeypress="CheckInteger(event);" Width="70px"></asp:TextBox>
                                                </div>


                                                <asp:TextBox ID="txtSupplier" runat="server" Width="117px" CssClass="textboxMandatory" TabIndex="2"   oninput="clearOnDelete(event)" onkeydown="return EnterTab(event,'txtGSTINT');"></asp:TextBox>
                                                <asp:ImageButton ID="imgbtnSupplier" runat="server" OnClick="imgbtnSupplier_Click" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 0px 0px; position: absolute" ValidationGroup="img" />
                                                <asp:RequiredFieldValidator ID="rfvSuppName" runat="server" ErrorMessage="Supplier Name required"
                                                    ControlToValidate="txtSupplier" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="rfvSuppName">
                                                </cc:ValidatorCalloutExtender>

                                            </li>


                                        </div>
                                        <div class="divL" style="width: 350px;">
                                            <li>

                                                <asp:Label ID="lblGst" runat="server" CssClass="label" Text="GSTIN"></asp:Label>
                                                <asp:TextBox ID="txtGSTINT" runat="server" Width="143px" CssClass="textboxMandatory" TabIndex="3" onkeydown="return EnterTab(event,'txtDate');"></asp:TextBox>
                                            </li>

                                            <li>
                                                <label class="label">Bill Date</label>
                                                <asp:TextBox runat="server" Width="143px" CssClass="textboxMandatory" ID="txtDate" TabIndex="4" AutoComplete="off" onkeydown="return EnterTab(event,'txtinvoice');" />
                                                <cc:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            <li id="liinvoice" runat="server">

                                                <asp:Label class="label" ID="Label3" runat="server" Text="Invoice Number" Width="136px"></asp:Label>

                                                <asp:TextBox ID="txtinvoice" runat="server" AutoPostBack="false" CssClass="textboxMandatory" TabIndex="5" MaxLength="25" Width="143px" onkeydown="return EnterTab(event,'txtInvoicedate');"></asp:TextBox>

                                            </li>



                                        </div>

                                        <div style="float: left; width: 350px;">
                                            <li>
                                                <asp:Label class="label" ID="lbl" runat="server" Text="Store" Width="136px"></asp:Label>
                                                <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore" Width="150px">
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

                                            </li>
                                            <li id="liInvoiceDate" runat="server">

                                                <asp:Label class="label" ID="Label2" runat="server" Text="Invoice Date"></asp:Label>
                                                <asp:TextBox runat="server" Width="143px" CssClass="textboxMandatory" ID="txtInvoicedate" TabIndex="6" AutoComplete="off" onkeydown="EnterTab(event,'ddlCategory');" />
                                                <cc:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" TargetControlID="txtInvoicedate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="reqvalinvDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtInvoicedate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="reqvalinvDate">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            <li style="display: none">
                                                <div class="rbtn">
                                                    <asp:Label ID="Label22" runat="server" CssClass="label" Width="80px" Text="Inter State Purchase"></asp:Label>
                                                    <asp:CheckBox ID="chkInterState" runat="server" AutoPostBack="true" OnCheckedChanged="chkInterState_CheckedChanged" Style="width: 100px; padding-right: 50px;" TabIndex="7" onkeydown="EnterTab(event,'ddlCategory');" />
                                                </div>
                                            </li>
                                            <li id="liBillType" runat="server">

                                                <asp:Label class="label" ID="Label24" runat="server" Text="Bill Type"></asp:Label>
                                                <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlBillType" Width="150px" TabIndex="26"></asp:DropDownList>
                                                <asp:Button ID="btnBillType" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Bill Type required" ControlToValidate="ddlBillType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="imgPurchase"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Bill Type required" ControlToValidate="ddlBillType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                                </cc:ValidatorCalloutExtender>

                                            </li>
                                            <%--<li id="liPurchase" runat="server">

                                            <asp:Label class="label" ID="Label3" runat="server" Text="Purchase" Width="136px"></asp:Label>
                                            <div style="display: none">
                                                <asp:TextBox ID="txtPurRefno" runat="server" AutoPostBack="false" onblur="txtPurRefno_onblur()" CssClass="textboxMandatory" TabIndex="8" onKeypress="CheckInteger(event);" Width="40px"></asp:TextBox>
                                            </div>

                                            <asp:TextBox ID="txtPurBillNo" runat="server" Width="117px" CssClass="textboxMandatory" TabIndex="9"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnPurBill" runat="server" OnClick="imgbtnPurBill_Click" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 0px 0px; position: absolute" ValidationGroup="imgPurchase" />

                                            <asp:RequiredFieldValidator ID="rfvPurRefno" runat="server" ErrorMessage="Purchase Refno required" ControlToValidate="txtPurRefno"
                                                Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfvPurRefno">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RequiredFieldValidator ID="rfvPurBillNo" runat="server" ErrorMessage="Bill No required"
                                                ControlToValidate="txtPurBillNo" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="rfvPurBillNo">
                                            </cc:ValidatorCalloutExtender>
                                        </li>--%>

                                        </div>
                                    </div>



                                    <div class="clear"></div>

                                    <div class="divSeparation" style="width: 106%"></div>



                                    <div class="clear"></div>
                                    <li class="clear"></li>
                                    <div id="divItemPurchaseReturn" runat="server">
                                        <div style="width: 1500px" id="divItems" runat="server">
                                            <div class="divL" style="width: 350px;">
                                                <li id="liCategory" runat="server">
                                                    <asp:Label ID="lblcat" runat="server" class="label" Width="138" Text="Category"></asp:Label>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="7" CssClass="dropdown" Width="150px" onkeydown="return EnterTab(event,'txtitem');"></asp:DropDownList>

                                                </li>
                                                <li>
                                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item" Width="138px"></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="textboxMandatory" TabIndex="8" ID="txtitem" MaxLength="20" onkeydown="return EnterTab(event,'ddlUnit');" AutoComplete="off" Width="116px" />

                                                    <asp:ImageButton ID="ImageButton3" runat="server" onkeypress="return popUpStockSelectClick(); " OnClientClick="return popUpStockSelectClick(); " ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 0px 0px; position: absolute" ValidationGroup="img" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Item Required " ControlToValidate="txtitem" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender117" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>

                                                <li id="li1" runat="server">
                                                    <asp:Label ID="Label6" runat="server" class="label" Width="138" Text="Unit"></asp:Label>
                                                    <asp:DropDownList ID="ddlUnit" runat="server" TabIndex="9" onkeydown="return EnterTab(event,'ddlSubunit');" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdown" Width="150px"></asp:DropDownList>

                                                </li>
                                                <li id="li2" runat="server">
                                                    <asp:Label ID="Label10" runat="server" class="label" Width="138" Text="Subunit"></asp:Label>
                                                    <asp:DropDownList ID="ddlSubunit" runat="server" onkeydown="return EnterTab(event,'txtBatch');" TabIndex="10" CssClass="dropdown" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlSubunit_SelectedIndexChanged"></asp:DropDownList>

                                                </li>
                                                <li>
                                                    <asp:Label ID="Label11" runat="server" CssClass="label" Text="Batch" Width="138px"></asp:Label>
                                                    <asp:TextBox runat="server" CssClass="textboxMandatory" onkeydown="return EnterTab(event,'txtHSN');" TabIndex="11" ID="txtBatch" MaxLength="20" AutoComplete="off" Width="143px" />
                                                </li>


                                                <li>
                                                    <asp:Label class="label" ID="Label13" runat="server" Text="HSN/SAC"></asp:Label>
                                                    <asp:TextBox runat="server" Width="143px" onkeydown="return EnterTab(event,'txtQuantity');" CssClass="textboxMandatory" ID="txtHSN" TabIndex="12" AutoComplete="off" />
                                                </li>


                                                <li class="clear"></li>

                                            </div>
                                            <div class="divL" style="width: 350px;">

                                                <li>
                                                    <asp:Label ID="Label9" runat="server" CssClass="label" Text="Quantity" Width="136px"></asp:Label>
                                                    <asp:TextBox ID="txtQuantity" CssClass="textboxMandatory" Style="text-align: right;" Width="143px" runat="server" TabIndex="13" MaxLength="11" onkeypress="DecimalOrIntValidation('txtQuantity',event,'items'); " onKeyup=" calculateActualQuantity();getpurchaseamount(); " onkeydown="return EnterTab(event,'txtFree');" AutoComplete="off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                                    </cc:ValidatorCalloutExtender>
                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" SetFocusOnError="True" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                                    </cc:ValidatorCalloutExtender>
                                                    <asp:CompareValidator ID="cmpquantity" runat="server" ErrorMessage="cannot greater than current stock" ControlToCompare="txtCurrentStock" SetFocusOnError="True" ControlToValidate="txtQuantity" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ValidationGroup="add"></asp:CompareValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="cmpquantity">
                                                    </cc:ValidatorCalloutExtender>

                                                    <%--<asp:CustomValidator ID="cvQuantity" runat="server"
                                                        ClientValidationFunction="ValidateActQty" ControlToValidate="txtQuantity" Display="None"
                                                        CssClass="validator"
                                                        ErrorMessage="Invalid Stock" SetFocusOnError="True" ValidationGroup="add"></asp:CustomValidator>--%>

                                                    <%--<cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="cvQuantity">
                                                    </cc:ValidatorCalloutExtender>--%>
                                                    <asp:HiddenField ID="hdfPacked" runat="server" />
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label4" runat="server" CssClass="label" Width="138px" Text="Free"></asp:Label>
                                                    <asp:TextBox ID="txtFree" Style="text-align: right;" onkeydown="return EnterTab(event,'txtPurchaseRateItem');" CssClass="textbox" Width="143px" runat="server" TabIndex="14" MaxLength="12" onKeyup="  calculateActualQuantity(); " onkeypress="DecimalOrIntValidation(id,event,'items');" AutoComplete="off"></asp:TextBox>


                                                    <asp:CompareValidator ID="cmpFree" runat="server" ErrorMessage="cannot greater than current stock" ControlToCompare="txtCurrentStock" ControlToValidate="txtFree" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Double" ValidationGroup="add"></asp:CompareValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server" Enabled="True" TargetControlID="cmpFree">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label14" runat="server" CssClass="label" Width="138px" Text="Act Qty"></asp:Label>
                                                    <asp:TextBox ID="txtActqty" onkeydown="return EnterTab(event,'txtPurchaseRateItem');" Style="text-align: right;" CssClass="textbox" Width="143px" runat="server" TabIndex="15" MaxLength="12" onkeypress="DecimalOrIntValidation(id,event,'items');" AutoComplete="off"></asp:TextBox>


                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="cannot greater than current stock" ControlToCompare="txtCurrentStock" ControlToValidate="txtActQty" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Double" ValidationGroup="add"></asp:CompareValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="CompareValidator2">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>

                                                <li>
                                                    <asp:Label ID="Label7" runat="server" CssClass="label" Width="138px" Text="Pur.Rate"></asp:Label>
                                                    <asp:TextBox ID="txtPurchaseRateItem" onkeydown="return EnterTab(event,'cbxDiscountTax');" Style="text-align: right;" CssClass="textboxMandatory" Width="143px" runat="server" TabIndex="16" MaxLength="14" onKeypress="checkNumAndDecimal(event,this);" onkeyup="getpurchaseamount();" onblur="getSalesPrice(0);" AutoComplete="off"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtPurchaseRateItem" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                                    </cc:ValidatorCalloutExtender>
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPurchaseRateItem" ErrorMessage="Enter a valid Rate" Display="None" SetFocusOnError="True" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                                    </cc:ValidatorCalloutExtender>
                                                </li>

                                                <li class="aspNetDisabled">

                                                    <asp:Label ID="lblIncdTax" runat="server" CssClass="label" Width="138px" Text="Include Tax"></asp:Label>
                                                    <%--<asp:TextBox runat="server" ID="TextBox1" TabIndex="16" Width="143px" CssClass="textbox" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);" />--%>
                                                    <asp:CheckBox ID="cbxIncludeTax" onkeydown="return EnterTab(event,'cbxDiscountTax');" runat="server" TabIndex="17" onclick="getpurchaseamount();getSalesPrice(0);" />
                                                    <asp:Button Text="PTax" onkeydown="EnterTab(event,'cbxDiscountTax');" ToolTip="PurchaseTax" ID="btnS" Style="padding: 0px; width: 40px; height: 18px; margin: 0px 0px 0px 0px;" CssClass="button" TabIndex="17" OnClick="btnS_Click" runat="server" />

                                                </li>
                                                <li>
                                                    <asp:Label ID="Label31" runat="server" CssClass="label" Width="138px" Text="Tax.Amt"></asp:Label>
                                                    <asp:TextBox ID="txttaxAmount" onkeydown="EnterTab(event,'cbxDiscountTax');" CssClass="textbox" TabIndex="18" Width="143px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                                </li>
                                                <li>

                                                    <asp:Label ID="Label16" runat="server" CssClass="label" Width="138px" Text="Discount on Tax"></asp:Label>
                                                    <%--<asp:TextBox runat="server" ID="TextBox1" TabIndex="16" Width="143px" CssClass="textbox" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);" />--%>
                                                    <asp:CheckBox ID="cbxDiscountTax" runat="server" TabIndex="19" onclick="getCheckboxValue(0);" onkeydown="EnterTab(event,'txtPurDiscPer');" />
                                                </li>
                                            </div>


                                            <div style="float: left; width: 350px;">
                                                <li>
                                                    <asp:Label ID="Label17" runat="server" CssClass="label" Width="138px" Text="Pur Disc %"></asp:Label>
                                                    <asp:TextBox ID="txtPurDiscPer" onkeydown="EnterTab(event,'txtPurDiscAmnt');" CssClass="textbox" TabIndex="20" Width="143px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this);" onblur="getpurchaseamount();getCheckboxValue(1);"></asp:TextBox>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label18" runat="server" CssClass="label" Width="138px" Text="Pur Disc Amount"></asp:Label>
                                                    <asp:TextBox ID="txtPurDiscAmnt" onkeydown="EnterTab(event,'txtNetPur');" CssClass="textbox" TabIndex="21" Width="143px" onblur="getpurchaseper();getCheckboxValue(2)" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this);"></asp:TextBox>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label19" runat="server" CssClass="label" Width="138px" Text="Net Pur"></asp:Label>
                                                    <asp:TextBox ID="txtNetPur" onkeydown="EnterTab(event,'txtMRP');" CssClass="textbox" TabIndex="22" Width="143px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this);"></asp:TextBox>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label20" runat="server" CssClass="label" Width="138px" Text="MRP"></asp:Label>
                                                    <asp:TextBox ID="txtMRP" onkeydown="EnterTab(event,'txtSalesPrice');" CssClass="textbox" TabIndex="23" Width="143px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this);" onblur="getSalesPrice(0);TaxAmount();"></asp:TextBox>
                                                </li>
                                                <li>
                                                    <asp:Label ID="Label21" runat="server" CssClass="label" Width="138px" Text="Sales Price"></asp:Label>
                                                    <asp:TextBox ID="txtSalesPrice" onkeydown="EnterTab(event,'txtItemExpDate');" CssClass="textbox" TabIndex="24" Width="143px" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="checkNumAndDecimal(event,this);"></asp:TextBox>
                                                </li>
                                                <li style="display: none;">
                                                    <asp:Label ID="Label5" runat="server" CssClass="label" Width="138px" Text="Current Stock"></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtCurrentStock" TabIndex="25" Width="143px" CssClass="textbox" Style="text-align: right;" />
                                                </li>
                                                <li>
                                                    <asp:Label class="label" ID="Label23" runat="server" Text="Expiry Date"></asp:Label>

                                                    <asp:TextBox runat="server" Width="143px" onkeydown="EnterTab(event,'btnAddItems');" CssClass="textboxMandatory" ID="txtItemExpDate" TabIndex="25" AutoComplete="off" />
                                                </li>
                                                <li>
                                                    <asp:Label class="label" ID="Label25" runat="server" Text="Pur. Invoice No"></asp:Label>
                                                  
                                                     <asp:TextBox runat="server" ID="txtpurinvoiceNo" TabIndex="25" Width="143px" CssClass="textbox"  Enabled="false" />
                                                </li>
                                                  <li>
                                                    <asp:Label class="label" ID="Label26" runat="server" Text="Pur. Invoice Date"></asp:Label>
                                                  
                                                     <asp:TextBox runat="server" ID="txtpurinvoiceDate" TabIndex="25" Width="143px" CssClass="textbox"  Enabled="false" />
                                                </li>
                                                <%--<li>
                                                <asp:Label ID="Label8" runat="server" CssClass="label" Width="138px" Text="Tot.Amt"></asp:Label>
                                                <asp:TextBox ID="txtTotalAmountItem" Style="text-align: right;" CssClass="textbox" Width="143px" runat="server" TabIndex="19" MaxLength="12" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>
                                            </li>--%>
                                            </div>

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
                                            <asp:Button Text="Insert" ID="btnAddItems" runat="server" OnClick="btnAddItems_Click" TabIndex="26" ValidationGroup="add" CssClass="button" />
                                            <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="27" OnClientClick="ItemClear();return false;" />
                                        </li>
                                        <div style="float: right; margin-right: 145px">
                                            <li>
                                                <asp:Label ID="Label8" runat="server" CssClass="label" Width="138px" Text="Reason"></asp:Label>
                                                <asp:DropDownList ID="ddlReasons" runat="server" TabIndex="28" onkeydown="EnterTab(event,'txtTotalAmount');" OnSelectedIndexChanged="ddlReasons_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdown" Width="150px"></asp:DropDownList>


                                            </li>
                                        </div>
                                        <div class="clear"></div>
                                        <div class="divSeparation" style="width: 106%"></div>
                                        <li style="width: 100%; min-height: 310px; max-height: 325px; overflow-y: auto;">
                                            <asp:GridView ID="gvItems" runat="server" Width="96%"
                                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                                BackColor="White" BorderStyle="Solid" CssClass="grid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items,FK_Unit,FK_Stock" OnRowDeleting="gvItems_RowDeleting" PageSize="100" OnSelectedIndexChanging="gvItems_SelectedIndexChanging" OnRowDataBound="gvItems_RowDataBound">

                                                <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                                <Columns>

                                                    <asp:BoundField DataField="slno" HeaderText="SlNo" HeaderStyle-Width="5px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="gridviewItemStyle" />
                                                    <asp:BoundField HeaderText="Item" DataField="Item" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="300px" />
                                                    <asp:BoundField HeaderText="Unit" ItemStyle-CssClass="gridviewItemStyle" DataField="Unit" HeaderStyle-Width="70px" />

                                                    <asp:BoundField HeaderText="Batch" ItemStyle-CssClass="gridviewItemStyle" DataField="BatchNo" HeaderStyle-Width="70px" />
                                                    <asp:BoundField HeaderText="Exp.Date" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="gridviewItemStyle" DataField="ExpiryDate" HeaderStyle-Width="50px" />
                                                    <asp:BoundField HeaderText="Qty" DataField="Quantity" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />

                                                    <asp:TemplateField HeaderText="ActQty" HeaderStyle-Width="10px" ItemStyle-Width="10px" Visible="false" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <div style="overflow: auto; width: 60px;">
                                                                <asp:Label ID="lblgvFK_Units" runat="server" Text='<%# Bind("FK_Unit") %>'></asp:Label>
                                                                <%--<asp:Label ID="lblgvFK_Category" runat="server" Text='<%# Bind("FK_Category") %>'></asp:Label>--%>
                                                                <%--<asp:Label ID="lblgvhsn" runat="server" Text='<%# Bind("HSN") %>'></asp:Label>--%>
                                                                <%--<asp:Label ID="lblgvNetPurchase" runat="server" Text='<%# Bind("NetPurchase") %>' Style="text-align: right; float: right;"></asp:Label>--%>
                                                                <asp:Label ID="lblgvMRP" runat="server" Text='<%# Bind("MRP") %>' Style="text-align: right; float: right;"></asp:Label>
                                                                <asp:Label ID="lblgvSalesPrice" runat="server" Text='<%# Bind("SalesPrice") %>' Style="text-align: right; float: right;"></asp:Label>
                                                            </div>
                                                            <asp:Label ID="lblgvActQty" runat="server" Text='<%# Bind("ActQty") %>' Style="text-align: right; float: right;"></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hdnPurDisc" Value='<%# Eval("PurchaseDiscPercentage") %>' />
                                                            <asp:HiddenField ID="hfStockID" runat="server" Value='<%# Eval("FK_Stock") %>' />
                                                            <asp:Label ID="lblgvIncludeptax" runat="server" Text='<%# Bind("IncludePtax") %>'></asp:Label>
                                                            <%--<asp:Label ID="lblgvPurchaseTax" runat="server" Text='<%# Bind("PurchaseTaxAmount") %>' Style="text-align: right; float: right;"></asp:Label>--%>
                                                            <asp:Label ID="lblgvDiscountOntax" runat="server" Text='<%# Bind("DiscountOnTax") %>'></asp:Label>
                                                            <asp:Label ID="lblgvPurDiscPer" runat="server" Text='<%# Bind("PurchaseDiscPercentage") %>' Style="text-align: right; float: right;"></asp:Label>
                                                            <%--<asp:Label ID="lblgvPurchaseDiscAmount" runat="server" Text='<%# Bind("PurchaseDiscAmount") %>' Style="text-align: right; float: right;"></asp:Label>--%>
                                                            <asp:Label ID="lblgvItem" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                                                            <%--<asp:Label ID="lblgvActualPurRate" runat="server" Text='<%# Bind("ActualPurRate") %>' Style="text-align: right; float: right;"></asp:Label>--%>
                                                            <%--<asp:Label ID="lblgvFK_SubUnit" runat="server" Text='<%# Bind("FK_SubUnit") %>'></asp:Label>--%>
                                                             <asp:Label ID="lblgvpurInvoiceno" runat="server" Text='<%# Bind("PurInvoiceNo") %>' Style="display:none"></asp:Label >
                                                              <asp:Label ID="lblgvpurInvoiceDate" runat="server" Text='<%# Bind("PurInvoiceDate") %>'></asp:Label>
                                                             <asp:Label ID="lblPurchaseDiscAmt" runat="server" Text='<%# Bind("PurchaseDiscAmt") %>' Style="display:none"></asp:Label >
                                                               <asp:Label ID="lblPurchaserate" runat="server" Text='<%# Bind("PurchaseRate") %>' Style="display:none"></asp:Label >
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField HeaderText="ActQty" DataField="ActQty" ItemStyle-HorizontalAlign="Right"  Visible="f"/>--%>
                                                    <asp:BoundField HeaderText="Free" DataField="Free" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" />
                                                    <asp:BoundField HeaderText="Pur.Rate" DataField="PurchaseRate" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                                     <asp:BoundField HeaderText="MRP" DataField="MRP" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                                     <asp:BoundField HeaderText="Sales Price" DataField="SalesPrice" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />
                                                    <asp:BoundField HeaderText="Tot.Amt" DataField="TotalAmt" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />

                                                    <asp:TemplateField HeaderText="Reason" HeaderStyle-Width="10px" ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlReason" runat="server" TabIndex="70" CssClass="dropdownMandatory rsns" AutoPostBack="true" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged" Width="150px"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select a Reason"
                                                                ControlToValidate="ddlReason" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                            </cc:ValidatorCalloutExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                        <ItemTemplate>

                                                            <asp:ImageButton ID="ImageButton1q2" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Edit_bg1.png" Width="20px" Height="20px" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
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

                                        <asp:Button ID="hiddendiscountAmount" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" />


                                    </div>

                                    <div class="clear"></div>
                                    <%--<div id="gvpi" runat="server" visible="false">
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
                                                        <asp:TextBox runat="server" ID="txtQuantity" ReadOnly="true" onBlur="CalculateAmount(id,'txtQuantity');" AutoComplete="off" CssClass="textbox" Style="width: 40px; text-align: right;" onkeypress="DecimalOrIntValidation(id,event,'txtQuantity');" Text='<%# Eval("Quantity") %>' />

                                                       

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

                                                                                                                 <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtFreeQty" ErrorMessage="Enter a valid Quatity" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValtxidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RangeValidator3">
                                                    </cc:ValidatorCalloutExtender> 

                                                        <asp:HiddenField ID="hdfFree" runat="server" Value='<%#Eval("Free") %>' />
                                                        <asp:HiddenField ID="hdfBalanceFree" runat="server" Value='<%# Eval("BalanceFree") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Act.Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtActQty" onBlur="CalculateAmount(id,'txtActQty');" onkeypress="DecimalOrIntValidation(id,event,'txtActQty');" AutoComplete="off" CssClass="textbox" Style="width: 50px; text-align: right;"
                                                            Text='<%# Eval("ActQty") %>' />
                                                           <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtActQty" ErrorMessage="Enter a valid Quatity" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValtxidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RangeValidator4">
                                                    </cc:ValidatorCalloutExtender> 
                                                        <asp:CustomValidator ID="cv1" runat="server" ControlToValidate="txtActQty" ErrorMessage="Enter a valid Quatity"
                                                            Display="None" ValidationGroup="submit" ClientValidationFunction="ClientValidate"></asp:CustomValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="cv1">
                                                        </cc:ValidatorCalloutExtender>
                                                        <asp:HiddenField ID="hdfActQty" runat="server" Value='<%#Eval("ActQty") %>' />
                                                        <asp:HiddenField ID="hdfNoofUnits" runat="server" Value='<%# Eval("NoofUnits") %>' />

                                                        <%--<asp:HiddenField ID="hdtaxAmount" runat="server" Value='<%# Eval("PurchasetaxAmount")%>' /> 
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
                                                </asp:BoundField >
                                                <asp:TemplateField HeaderText="Discount" Visible="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDisc" runat="server" Style="float: right;" Text='<%#Eval("PurchaseDiscPercentage") %>'></asp:Label>

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Disc.Amt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10px" ItemStyle-Width="10px">

                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblDiscAmt" runat="server" Style="float: right;" Text='<%#Eval("PurchaseDiscAmnt") %>'></asp:Label>
                                                        <asp:TextBox ID="lblDiscAmt" ReadOnly="true" Width="50px" runat="server" Style="float: right;" Text='<%#Eval("PurchaseDiscAmnt") %>'>
                                                        </asp:TextBox>
                                                        <asp:HiddenField ID="hdfDiscAmnt" runat="server" Value='<%# Eval("PurchaseDiscAmnt") %>' />

                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="0px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSingleDiscount" runat="server" Style="float: right; display: none" Text='<%#Eval("SingleDisAmnt") %>'></asp:Label>
                                                        <%--<asp:HiddenField ID="hdfDiscAmnt" runat="server" Value='<%# Eval("PurchaseDiscAmnt") %>' />
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
                                </div>--%>
                                    <li class="clear"></li>
                                    <br class="clear" />
                                    <%--  </ContentTemplate>
                                        
                                    </asp:UpdatePanel>--%>

                                    <div class="divSeparation" style="width: 106%"></div>

                                    <%--                                <div class="divL">--%>


                                    <%--  <li>
                                        <label class="label">Remark</label>
                                        <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" CssClass="multiline" MaxLength="500" TabIndex="22" AutoComplete="off" />
                                    </li>

                                    <li id="cbx" style="display: none;">
                                        <label class="label">Close Remark</label>
                                        <asp:TextBox ID="txtClosedRemark" runat="server" TextMode="MultiLine" MaxLength="500" TabIndex="23" AutoComplete="off" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                        <asp:HiddenField ID="HiddenField4" runat="server" />
                                    </li>--%>
                                    <asp:HiddenField ID="hdfDate" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                    <asp:HiddenField ID="hfIsApprove" runat="server" />
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
                                     <asp:HiddenField ID="hdfMode" runat="server" />
                                    <asp:Button ID="btnItem" runat="server" Text="Button" OnClick="btnItem_Click" CausesValidation="false" Style="display: none;" />
                                    <%--</div>--%>
                                    <div style="width: 1500px">
                                        <div class="divL" style="width: 350px;">

                                            <li>
                                                <label class="label">Net Purchase</label>
                                                <asp:TextBox runat="server" CssClass="textbox" ID="txtTotalAmount" onkeydown="EnterTab(event,'txtOtherChrg');" onKeypress="CheckNumWithDecimal(event);" Style="text-align: right; width: 143px;" TabIndex="29" AutoComplete="off" />




                                            </li>
                                            <li>
                                                <label class="label">Oth.Charges</label>
                                                <asp:TextBox runat="server" CssClass="textbox" onkeydown="EnterTab(event,'txtDiscount');" onblur="return Total();" ID="txtOtherChrg" Style="text-align: right; width: 143px;" TabIndex="30" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />

                                                <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtOtherChrg" ErrorMessage="Enter a valid Discount%" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </li>
                                        </div>
                                        <div class="divL" style="width: 350px;">
                                            <li style="display: none">
                                                <label class="label">Discount % </label>
                                                <asp:TextBox runat="server" CssClass="textbox" onkeydown="EnterTab(event,'txtDiscountAmt');" onkeyup="return Total();" ID="txtDiscount" Style="text-align: right; width: 143px;" TabIndex="31" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />

                                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtDiscount" ErrorMessage="Enter a valid Discount%" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </li>

                                            <li>
                                                <label class="label">Bill Discount</label>
                                                <asp:TextBox runat="server" CssClass="textbox" onkeydown="EnterTab(event,'txtRoundOff');" ID="txtDiscountAmt" onblur="return Total();" Style="text-align: right; width: 143px;" TabIndex="32" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />

                                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtDiscountAmt" ErrorMessage="Enter a valid Discount Amountt" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </li>
                                            <li>
                                                <label class="label">Round Off</label>
                                                <asp:TextBox runat="server" CssClass="textbox" onkeydown="EnterTab(event,'txtNetAmount');" ID="txtRoundOff" Style="text-align: right; width: 143px;" TabIndex="33" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />
                                            </li>
                                        </div>
                                        <div class="divL" style="width: 350px;">



                                            <li>
                                                <label class="label">Net Amount</label>
                                                <asp:TextBox runat="server" onkeydown="EnterTab(event,'btnSave');" CssClass="textbox" ID="txtNetAmount" Style="text-align: right; width: 143px;" TabIndex="34" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />


                                            </li>
                                        </div>
                                    </div>
                                    <li class="clear"></li>



                                    <div class="clear"></div>
                                    <li class="btnMargin">
                                        <asp:HiddenField ID="hfPurchaseReturn" runat="server" />
                                        <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="29" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" CausesValidation="false" TabIndex="30" OnClick="btnClear_Click1" />
                                        <%--<asp:Button ID="btnStoreBlurTrigger" runat="server" Text="Item" OnClick="ddlStore_SelectedIndexChanged" Style="display: none;" />--%>
                                        <asp:Button Text="Approve" runat="server" CssClass="button" ID="btnApprove" ValidationGroup="submit" OnClick="btnApprove_Click" TabIndex="31" OnClientClick="ConfirmApprove();" />
                                        <%--<asp:Button Text="Approve" runat="server" CssClass="button" ID="btnApprove" ValidationGroup="submit" Visible="false" OnClick="this.disabled = true;__doPostBack('btnApprove','')"   OnClientClick="ConfirmApprove();" UseSubmitBehavior="false"  TabIndex="31" />--%>
                                        <%--<asp:Button runat="server" ID="BtnSubmit"  OnClientClick="this.disabled = true; this.value = 'Submitting...';"   UseSubmitBehavior="false"  OnClick="BtnSubmit_Click"  Text="Submit Me!" />--%>

                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfuserName" runat="server" />
                                        <asp:HiddenField ID="hfUnitQuantity" runat="server" />

                                    </li>
                            </div>

                        </div>
                        <div class="clear"></div>

                        <div class="main_bg">
                            <div id="divView" runat="server" style="display: none; height: 485px">



                                <ul class="contentul">
                                    <div class="divL">
                                        <li>
                                            <label class="label">Supplier</label>
                                            <asp:DropDownList ID="ddlSearchSupplier" runat="server" CssClass="dropdown" TabIndex="35"></asp:DropDownList>
                                        </li>

                                        <li style="display: none">
                                            <label class="label">Store</label>
                                            <asp:DropDownList ID="ddlSearchStore" runat="server" CssClass="dropdown"></asp:DropDownList>

                                        </li>
                                        <li>
                                            <label class="label">From Date</label>
                                            <asp:TextBox ID="txtFromDate" CssClass="textbox" runat="server" TabIndex="35"></asp:TextBox>
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
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False"  AllowPaging="true"  
                                        BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" OnPageIndexChanging="gvPurchaseReturn_PageIndexChanging" DataKeyNames="ID_PurchaseReturn,Approved" OnSelectedIndexChanging="gvPurchaseReturn_SelectedIndexChanging" OnRowDeleting="gvPurchaseReturn_RowDeleting" OnRowCommand="gvPurchaseReturn_RowCommand"  OnRowDataBound="gvPurchaseReturn_RowDataBound">
                                        <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                                            <asp:BoundField DataField="BillDate" HeaderText="Date" />
                                            <asp:BoundField DataField="NetAmount" HeaderText="NetAmount" />
                                            <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                            <asp:BoundField DataField="Store" HeaderText="Store" />


                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()"  ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                    <div style="display: none">
                        <asp:TextBox ID="txtKey" runat="server"></asp:TextBox>
                    </div>
                    <div style="display: none">
                        <asp:TextBox ID="txtKeyDis" runat="server"></asp:TextBox>
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
            .modal {
                position: fixed;
                top: 0;
                left: 0;
                backgrou d-co z-index: 99;
                city: 0.8;
                alpha(opaci y=80) -moz- pacit ight: 10 % width: 100% .lo ding font-f mil: Aril;
                ize: 10pt;
                px solid #6 CFF5;
                paddin -top: m n-hei width: di play osition: fixed;
                city: 0.5;
                alpha(opacity=50) pacity: .5;
                -color: White;
                z-in z-index: 999;
            }
        </style>
        <script type="text/javascript">

            function ShowProgress() {
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
                $("#txtExpiry").val(d.format('dd/MM/yyyy'));
                $("#txtInvoicedate").val(d.format('dd/MM/yyyy'));

                $("#txtTotalAmount").val('0');
                $("#txtOtherChrg").val('0');

                $("#txtDiscount").val('0');
                $("#txtDiscountAmt").val('0');
                $("#txtNetAmount").val('0');
                $("#txtRemark").val("");
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
                $("#ddlReasons").val("0");

                ItemClear();
                //  return false;
            }

            //$('#hdfFKStock').val('send this');
            //__doPostBack('hdfFKStock');


            function CalculateAmount(id, type) {



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
                var lblTotal = id.replace(type, 'lblTotalAmt');
                var hdfTotalAmt = id.replace(type, 'hdfTotalAmt');
                var hdfQty = id.replace(type, 'hdfQty');
                var hdfFree = id.replace(type, 'hdfFree');
                var hdfActQty = id.replace(type, 'hdfActQty');

                var qty = $("#" + txtQuantity).val() == '' || $("#" + txtQuantity).val() == null ? 0 : parseFloat($("#" + txtQuantity).val());
                var Free = $("#" + txtFreeQty).val() == '' || $("#" + txtFreeQty).val() == null ? 0 : parseFloat($("#" + txtFreeQty).val());
                var ActQty = $("#" + txtActQty).val() == '' || $("#" + txtActQty).val() == null ? 0 : parseFloat($("#" + txtActQty).val());
                var pr = $("#" + lblPr).text() == '' || $("#" + lblPr).text() == null ? 0 : parseFloat($("#" + lblPr).text());
                //alert(qty);
                var prevqty = $("#" + hdfQty).val();
                //  var prevFreeqty = 0;

                //  var prevActqty = 0;

                //if (qty > prevqty) {
                //    alertify.alert("Quantity Cannot Exceed Available Stock");
                //    $("#" + txtQuantity).val(0);
                //        return false;
                //}
                if (qty > prevqty) {
                    alertify.alert("Quantity Cannot Exceed Available Stock");
                    $("#" + txtQuantity).val(0);
                    return false;
                }




                // if ($("#hfPurchaseReturn").val() == '') {
                var prevqty = $("#" + hdfQty).val();

                var prevFreeqty = $("#" + hdfFree).val();
                var prevActqty = $("#" + hdfActQty).val();
                if (Free > prevFreeqty) {
                    alertify.alert("Quantity Cannot Exceed Available Stock");
                    $("#" + txtFreeQty).val(0);
                    return false;
                }
                if (ActQty > prevActqty) {
                    alertify.alert("Quantity Cannot Exceed Available Stock");
                    $("#" + txtQuantity).val(0);
                    return false;
                }

                //  }
                //var hdfDiscAmnt = id.replace(type, 'hdfDiscAmnt');
                //var hdfDiscAmn = $("#" + hdfDiscAmnt).val();
                //var Value = parseFloat(hdfDiscAmn) / parseFloat(prevqty);
                // alert(Value);
                //var qty = $("#" + txtQuantity).val() == '' || $("#" + txtQuantity).val() == null ? 0 : parseFloat($("#" + txtQuantity).val());
                //var DisAmnt = parseFloat(qty) * parseFloat(Value);
                //alert(DisAmnt);
                //$("#btnHiddenItemsQuantityChange").click();
                // $("#" + lblDiscAmt).val(DisAmnt);

                if ($("#hfPurchaseReturn").val() != '' || $("#" + txtQuantity).val() != null) {

                    var hdfBalQty = id.replace(type, 'hdfBalanceQuantity');
                    var hdfBalFree = id.replace(type, 'hdfBalanceFree');
                    var hdfBalActQty = id.replace(type, 'hdfBalanceActQty');

                    prevqty = parseFloat(prevqty) + parseFloat($("#" + hdfBalQty).val());
                    prevFreeqty = parseFloat(prevFreeqty) + parseFloat($("#" + hdfBalFree).val());
                    prevActqty = parseFloat(prevActqty) + parseFloat($("#" + hdfBalActQty).val());

                }

                var NoofUnits = id.replace(type, 'hdfNoofUnits');

                var ActQty_ = $("#" + txtActQty).val();
                //((parseFloat(qty)) + (parseFloat(Free)));
                //* parseFloat($("#" + NoofUnits).val());
                $("#" + txtActQty).val(parseFloat(ActQty_));
                // alet($("#" + txtActQty).val());
                //alert(parseFloat(qty));
                //alert(parseFloat(prevqty));
                //alert(parseFloat(Free));
                //alert(parseFloat(prevFreeqty));
                // alert(parseFloat(ActQty));
                //alert(parseFloat(prevActqty));
                //  alert(ActQty_);

                if (qty == 0) {
                    $("#" + txtQuantity).val(qty);
                }
                if (Free == 0) {
                    $("#" + txtFreeQty).val(Free);
                }
                if (ActQty == 0) {
                    $("#" + txtActQty).val(ActQty);
                }

                if (parseFloat(qty) > parseFloat(prevqty) || parseFloat(Free) > parseFloat(prevFreeqty) || parseFloat(ActQty) > parseFloat(prevActqty) && (type == 'txtActQty')) {
                    alert(ActQty); alert(prevActqty);
                    alertify.alert("Quantity Cannot Exceed Available Stock");

                    if (type == 'txtFreeQty') {
                        $("#" + txtFreeQty).val(parseFloat(prevFreeqty));
                    }
                    else if (type == 'txtActQty') {
                        ActQty_ = $("#" + txtActQty).val();
                        //(parseFloat(qty) + parseFloat(Free));
                        //* parseFloat($("#" + NoofUnits).val());
                        if (parseFloat(ActQty) > ActQty_) {
                            $("#" + txtActQty).val(parseFloat(ActQty_));
                        }
                        else {
                            $("#" + txtActQty).val(parseFloat(prevActqty));
                        }
                    }
                    else {
                        $("#" + txtQuantity).val(parseFloat(prevqty));
                        $("#" + txtActQty).val(parseFloat(prevActqty));
                    }
                }
                else {

                    // if (type != 'txtFreeQty') {

                    CalculateTotalAmount(type);
                    //}
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

            function CalculateTotalAmount(Type) {


                var Totalamt = 0;
                var TotalDisc = 0;
                var VATTotal = 0;
                var Tot;
                var CESSTotal = 0;

                $.each($("table[id*=" + 'gvPurchaseItems' + "] tr"), function (index, item) {
                    if ($(item).find("input[type=hidden]").length > 0) {


                        var PurPrice = $(item).find("[id*=gvPurchaseItems_lblPurchaseRate_]").text();

                        var Qty = $(item).find("[id*=txtQuantity_]").val();
                        var Free = $(item).find("[id*=txtFreeQty_]").val();

                        var ActQty = $(item).find("[id*=txtActQty_]").val();

                        var NoofUnits = $(item).find("[id*=hdfNoofUnits_]").val();
                        // alert(Type);

                        // ActQty = (parseFloat(Qty) * parseFloat(NoofUnits));
                        // alert($("#hfQtyValidation").val());
                        if ($("#hfQtyValidation").val() == "1") {

                            //  alert(ActQty); alert(Free);
                            Tot = (parseFloat(ActQty) - parseFloat(Free));
                        }
                        else {

                            Tot = (parseFloat(ActQty));
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

                        var Amt = (parseFloat(PurPrice) * parseFloat(Tot));

                        //+ parseFloat(hdtaxAmount);

                        $(this).find("[id*='lblTotalAmt_']").text((Amt).toFixed(4));
                        Totalamt += Amt;
                        // var Disc = $(item).find("[id*=gvPurchaseItems_lblDiscAmt_]").text();

                        // if ($("#rdPurchase").prop("checked", true));
                        if ($('#rdPurchase').is(":checked")) {

                            var Qty = $(item).find("[id*=txtQuantity_]").val();

                            var SinglAmount = parseFloat($(item).find("[id*=lblSingleDiscount_]").text());
                            var DiscAmount = parseFloat(Qty) * parseFloat(SinglAmount)
                            $(this).find("[id*='lblDiscAmt_']").val((DiscAmount).toFixed(4));
                            //alert(Qty); alert(DiscAmount); alert(SinglAmount);
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
            //function ddlBillTypeClick() {
            //    $("#btnBillType").click();
            //    return false;
            //}

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
                $("#btnS").val("PTax");
                $("#txtActqty").val("0");
                $("#txtFree").val("0");

                $("#txtQuantity").val("0");
                $("#txtCurrentStock").val("0");
                $("#ddlUnit").val('');
                $("#txtPurchaseRateItem").val("0");
                $("#txtItemVat").val('0');
                $("#txtItemVatAmount").val('0');
                $("#txtPurchaseRateItem").val('0');
                $("#txtTotalAmountItem").val('0');
                $("#txttaxAmount").val('0');
                //$("#ddlBillType").val('0');
                $("#txtPurDiscPer").val('0');
                $("#txtPurDiscAmnt").val('0');
                $("#txtNetPur").val('0');
                $("#txtMRP").val('0');
                $("#txtSalesPrice").val('0');
                $("#txtItemExpDate").val('');
                $("#cbxIncludeTax").attr("checked", false);
                $("#cbxDiscountTax").attr("checked", false);
                $("#ddlCategory").val("0");
                $("#txtpurinvoiceNo").val("");
                $("#txtpurinvoiceDate").val("");
                $("#hdfFkItem").val("");
                $("#hfEditedRow").val("");
               
            }

            var popup;
            function popUpStockSelectClick() {
                $("#txtQuantity").val("0");
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
                    
                    var expdt = $("#txtExpiry").val();
                    var mode = $("#hdfMode").val();
                    $.ajax({
                        type: "POST",
                        url: 'Textile.asmx/GetUrlEncryptStockReturn',
                        data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','txtSupRefno':'" + txtSupRefno + "','hfStore':'" + hfStore + "','hfClient':'" + hfClient + "','expdt':'" + expdt + "','mode':'" + mode + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                   
                        success: function (MenuHtml) {
                         
                            // url = "PreviousPurchase.aspx?" + MenuHtml.d;
                          
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
            function getpurchaseper() {

                var PurchaseRate = $("#txtPurchaseRateItem").val() == "" ? "0" : $("#txtPurchaseRateItem").val();
                var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
                Quantity = Quantity * parseFloat($("#hfUnitQuantity").val() == "" ? "0" : $("#hfUnitQuantity").val());
                //var Quantity = $("#txtActqty").val() == "" ? "0" : $("#txtActqty").val();

                var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
                if ($('#cbxIncludeTax').is(":checked")) {


                    var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
                    if ($('#cbxDiscountTax').is(":checked")) {

                        var ExcludeDis = parseFloat(PurchaseRate) * parseFloat(DiscPer) / parseFloat(100);
                        var ActPur = parseFloat(PurchaseRate) - parseFloat(ExcludeDis);
                        var taxAmount = ((parseFloat(ActPur)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

                    }
                    else {
                        var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));
                    }
                    //  var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

                    $("#txttaxAmount").val(parseFloat(taxAmount).toFixed(4));
                    var PurchaseValue = (parseFloat(PurchaseRate) * parseFloat(Quantity)) - (parseFloat(taxAmount));
                    $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));
                }
                else {
                    var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
                    if ($('#cbxDiscountTax').is(":checked")) {

                        var ExcludeDis = parseFloat(PurchaseRate) * parseFloat(DiscPer) / parseFloat(100);
                        var ActPur = parseFloat(PurchaseRate) - parseFloat(ExcludeDis);
                        var taxAmount = ((parseFloat(ActPur) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
                        // var singleAmnt = ((parseFloat(ActPur) * parseFloat(TotalTaxPercetage)) / parseFloat(100));
                    }
                    else {
                        var taxAmount = ((parseFloat(PurchaseRate) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
                        //  var singleAmnt = ((parseFloat(PurchaseRate) * parseFloat(TotalTaxPercetage)) / parseFloat(100));
                    }

                    // var taxAmount = ((parseFloat(PurchaseRate) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
                    $("#txttaxAmount").val(taxAmount.toFixed(4));
                    var PurchaseValue = parseFloat(PurchaseRate) * parseFloat(Quantity);
                    $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));

                }
                if ($('#cbxDiscountTax').is(":checked")) {

                    var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                    var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
                    var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();

                    var DiscPer = isNaN(parseFloat((DiscountAmount)) * (100) / parseFloat(PurchaseValue)) ? 0 : parseFloat((DiscountAmount)) * (100) / parseFloat(PurchaseValue);

                    $("#txtPurDiscPer").val(parseFloat(DiscPer).toFixed(4));
                    // $("#txtPurDiscPer").val(isNaN(DiscPer) ? 0 : DiscPer).toFixed(4);
                    //alert(DiscPer);
                }

                else {


                    var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                    var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
                    var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();

                    var DiscPer = isNaN(parseFloat((DiscountAmount) * (100)) / parseFloat(PurchaseValue)) ? 0 : parseFloat((DiscountAmount) * (100)) / parseFloat(PurchaseValue);
                    // alert(DiscPer);
                    $("#txtPurDiscPer").val(DiscPer.toFixed(4));
                    // $("#txtPurDiscPer").val(isNaN(DiscPer) ? 0 : DiscPer).toFixed(4);

                    // else
                    //  $("#txtPurDiscPer").val(0);

                    //var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
                    //var DiscountAmount = (parseFloat(PurchaseValue) * parseFloat(DiscPer)) / parseFloat(100);
                    //$("#txtPurDiscAmnt").val(DiscountAmount.toFixed(4));

                }

                var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
                var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
                var NetPurchase = (parseFloat(PurchaseValue) + parseFloat(taxAmount)) - parseFloat(DiscountAmount);
                $("#txtNetPur").val(NetPurchase.toFixed(4));


            }

            function TaxAmount() {

                var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                //var SalesPrice = $("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val();

                var DiscountAmt = 0;
                var SalesPrice = $("#txtSalesPrice").val();//MRP - DiscountAmt
                var TaxPercentage = "0";

                //if ($('#ChkIncStax').is(":checked")) {

                //    var taxAmount = ((parseFloat(SalesPrice)) * (parseFloat(TaxPercentage))) / ((parseFloat(TaxPercentage) + parseFloat(100)));
                //    if (taxAmount != null)
                //        //$("#txtSTaxAmount").val(parseFloat(taxAmount).toFixed(4));
                //    $("#txtSalesPrice").val(parseFloat(SalesPrice).toFixed(4));
                //    // alert(SalesPrice);

                //}
                //else {
                var taxAmount = (parseFloat(SalesPrice) * parseFloat(TaxPercentage)) / parseFloat(100);
                if (taxAmount != null)
                    //$("#txtSTaxAmount").val(parseFloat(taxAmount).toFixed(4));
                    var Sale = parseFloat(SalesPrice) + parseFloat(taxAmount)
                // alert(Sale);
                //$("#txtSalesPrice").val(parseFloat(Sale).toFixed(4));
                //if (parseFloat(MRP).toFixed(4) == parseFloat(SalesPrice)) {
                //    alert('Sales Price not greater than MRP');
                //    return false;
                //}
                //}
            }
            function CalculateNetAmount() {
                try {

                    var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
                    var ItemVat = $("#txtItemVat").val() == "" ? "0" : $("#txtItemVat").val();
                    var PurchaseRateItem = $("#txtPurchaseRateItem").val() == "" ? "0" : $("#txtPurchaseRateItem").val();
                    var TotalAmountItem = $("#txtTotalAmountItem").val() == "" ? "0" : $("#txtTotalAmountItem").val();
                    var ItemVatAmount = $("#txtItemVatAmount").val() == "" ? "0" : $("#txtItemVatAmount").val();


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

                            TotalAmountItem = parseFloat(Quantity) * parseFloat(PurchaseRateItem) + parseFloat(ItemVatAmount) + parseFloat(CESSAmt);

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

            function getSalesPrice(parameter1) {

                var a = $('#txtKey').val();
                if (a != 0 && parameter1 == "0")
                    parameter1 = a;

                if (a == 0)
                    parameter1 = parameter1


                if (parameter1 == "0") {

                    var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                    var DiscountAmt = "0";
                    var SalesPrice = isNaN((parseFloat(MRP)) - (parseFloat(DiscountAmt))) ? "0" : (parseFloat(MRP)) - (parseFloat(DiscountAmt));
                    //$("#txtSalesPrice").val(parseFloat(SalesPrice).toFixed(4));
                    //  $("#txtSalesPrice").val(isNaN(SalesPrice) ? 0 : SalesPrice).toFixed(4);
                    //ProfitDiscnCalculation1();
                    $('#txtKey').val(parameter1);

                }
                if (parameter1 == "1") {

                    var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                    var ProfitPer = "0";
                    // alert(ProfitPer);
                    var TaxPercentage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
                    // alert(TaxPercentage);
                    var PurchaseRate = $("#txtPurchaseRateItem").val() == "" ? "0" : $("#txtPurchaseRateItem").val();
                    SalesPriceCal();
                    Discount1();
                    $('#txtKey').val(parameter1);
                    //var ui = $('#txtKey').val();
                    //alert(ui);
                }
                if (parameter1 == "3") {


                    var TaxPercentage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
                    var PurchaseRate = $("#txtPurchaseRateItem").val() == "" ? "0" : $("#txtPurchaseRateItem").val();
                    var txtProfitAmount = "0";
                    var TaxAmount = parseFloat((PurchaseRate) * parseFloat(TaxPercentage)) / parseFloat(100);
                    if ($('#cbxIncludeTax').is(":checked")) {

                        var PurchaseRateNew = (parseFloat(PurchaseRate));
                    }
                    else {
                        var PurchaseRateNew = (parseFloat(PurchaseRate)) + (parseFloat(TaxAmount));
                    }
                    // var PurchaseRateNew = parseFloat(PurchaseRate) + parseFloat(TaxAmount);
                    var ProfitPercentage = isNaN((parseFloat(txtProfitAmount) * parseFloat(100)) / parseFloat(PurchaseRateNew)) ? 0 : (parseFloat(txtProfitAmount) * parseFloat(100)) / parseFloat(PurchaseRateNew);
                    //$("#txtProfitPer").val(parseFloat(ProfitPercentage).toFixed(4));
                    // $("#txtProfitPer").val(isNaN(ProfitPercentage) ? 0 : ProfitPercentage).toFixed(4);


                    SalesPriceCal();
                    Discount1();
                    $('#txtKey').val(parameter1);
                }
                if (parameter1 == "2") {

                    SalesPriceDisc();
                    //ProfitDiscnCalculation1();
                    $('#txtKey').val(parameter1);

                }
                if (parameter1 == "4") {

                    var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                    var DiscountAmt = "0";
                    var DiscountPer = isNaN((parseFloat(DiscountAmt) * parseFloat(100)) / parseFloat(MRP)) ? 0 : (parseFloat(DiscountAmt) * parseFloat(100)) / parseFloat(MRP);
                    $("#txtDiscountPer").val(parseFloat(DiscountPer).toFixed(4));
                    // $("#txtDiscountPer").val(isNaN(DiscountPer) ? 0 : DiscountPer).toFixed(4);

                    SalesPriceDisc();
                    //ProfitDiscnCalculation1();
                    $('#txtKey').val(parameter1);
                }

            }




            function ValidateActQty(source, arguments) {

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
            }


            function Total() {
                try {
                  
                    var TotalAmount = $("#txtTotalAmount").val();
                    var DiscountAmt = $("#txtDiscountAmt").val();
                    var Discount = $("#txtDiscount").val();
                    var NetAmount = $("#txtNetAmount").val();
                    var otherchrge = $("#txtOtherChrg").val();
                    TotalAmount = parseFloat(TotalAmount) + parseFloat(otherchrge);
                    if (TotalAmount >= 0) {//&& Discount >= 0


                        //var dAmt = cAmt - Amount;
                        //if ($('#rdPurchase').is(":checked")) {
                        //    // var dAmt = (parseFloat(TotalAmount) * parseFloat(Discount)) / 100;
                        //    var Amount = ((parseFloat(TotalAmount)) - parseFloat(DiscountAmt));
                        //    var DiscountPer = (DiscountAmt) * (100) / (TotalAmount)
                        //    $("#txtDiscount").val(DiscountPer.toFixed(4));
                        //    var total = Amount;
                        //}
                        //else {
                        var dAmt = $("#txtDiscountAmt").val();// (parseFloat(TotalAmount) * parseFloat(Discount)) / (100);
                        //alert(dAmt);
                        var Amount = ((parseFloat(TotalAmount)) - parseFloat(dAmt));
                        //  alert(dAmt); alert(Amount)
                        var total = Amount;
                        //alert(total);
                        //}

                        if (total >= 0) {

                            //if ($('#rdPurchase').is(":checked")) {
                            //}
                            //else
                            //$("#txtDiscountAmt").val(dAmt.toFixed(4));
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
                            $("#txtDiscountAmt").val(0); 
                            $("#txtDiscount").val(0);
                            Total(); $("#txtDiscountAmt").select();
                        }
                    }
                    else {
                        $("#txtDiscountAmt").val(0);
                        $("#txtNetAmount").val(0);
                    }
                }
                catch (ex) {
                    alert(ex.message);
                }
            }

            function EnterTab(e, Id1) {

                var intKey = window.Event ? e.which : e.KeyCode;


                if (intKey == 13) {

                    $("#" + Id1).focus();
                    return false;
                }
            }




            function calculateActualQuantity() {
                var qty = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
                var free = $("#txtFree").val() == "" ? "0" : $("#txtFree").val();

                var nounit = $("#hfUnitQuantity").val() == "" ? "0" : $("#hfUnitQuantity").val();
                var ActQty = parseFloat(nounit) * parseFloat(qty);
                var SubunitNo = $("#hfSubUnitQuantity").val() == "" ? "0" : $("#hfSubUnitQuantity").val();
                $("#txtCheckActQuantity").val(ActQty.toFixed(4));

                if (SubunitNo != 0)
                    var Actquantity = (parseFloat(qty) + parseFloat(free)) * (parseFloat(nounit)) * (parseFloat(SubunitNo));
                else
                    var Actquantity = (parseFloat(qty) + parseFloat(free)) * (parseFloat(nounit));


                $("#txtActqty").val(Actquantity.toFixed(4));



            } function getpurchaseamount() {

                var PurchaseRate = $("#txtPurchaseRateItem").val() == "" ? "0" : $("#txtPurchaseRateItem").val();
                //var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
                //var Quantity = $("#txtActqty").val() == "" ? "0" : $("#txtActqty").val();
                var Quantity = $("#txtQuantity").val() == "" ? "0" : $("#txtQuantity").val();
                Quantity = Quantity * parseFloat($("#hfUnitQuantity").val() == "" ? "0" : $("#hfUnitQuantity").val());
                var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
                if ($('#cbxIncludeTax').is(":checked")) {

                    var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
                    var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));
                    var PurchaseValue = (parseFloat(PurchaseRate) * parseFloat(Quantity)) - (parseFloat(taxAmount));
                    $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));
                    if ($('#cbxDiscountTax').is(":checked")) {

                        var ExcludeDis = parseFloat(PurchaseRate) * parseFloat(DiscPer) / parseFloat(100);
                        var ActPur = parseFloat(PurchaseRate) - parseFloat(ExcludeDis);
                        var taxAmount = ((parseFloat(ActPur)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

                    }
                    //else {
                    //    var taxAmount = ((parseFloat(PurchaseRate)) * (parseFloat(Quantity)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));
                    //}

                    var singleAmnt = ((parseFloat(PurchaseRate)) * (parseFloat(TotalTaxPercetage))) / ((parseFloat(TotalTaxPercetage) + parseFloat(100)));

                    $("#hfSingleQty").val(parseFloat(singleAmnt).toFixed(4));
                    $("#txttaxAmount").val(parseFloat(taxAmount).toFixed(4));

                }
                else {

                    var TotalTaxPercetage = $("#hfTotTaxPercentage").val() == "" ? "0" : $("#hfTotTaxPercentage").val();
                    if ($('#cbxDiscountTax').is(":checked")) {

                        var ExcludeDis = parseFloat(PurchaseRate) * parseFloat(DiscPer) / parseFloat(100);
                        var ActPur = parseFloat(PurchaseRate) - parseFloat(ExcludeDis);
                        var taxAmount = ((parseFloat(ActPur) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
                        var singleAmnt = ((parseFloat(ActPur) * parseFloat(TotalTaxPercetage)) / parseFloat(100));
                    }
                    else {
                        var taxAmount = ((parseFloat(PurchaseRate) * parseFloat(Quantity)) * parseFloat(TotalTaxPercetage)) / parseFloat(100);
                        var singleAmnt = ((parseFloat(PurchaseRate) * parseFloat(TotalTaxPercetage)) / parseFloat(100));
                    }


                    $("#hfSingleQty").val(parseFloat(singleAmnt).toFixed(4));
                    $("#txttaxAmount").val(taxAmount.toFixed(4));
                    var PurchaseValue = parseFloat(PurchaseRate) * parseFloat(Quantity);
                    $("#hfPurchaseValue").val(parseFloat(PurchaseValue).toFixed(4));

                }
                if ($('#cbxDiscountTax').is(":checked")) {

                    var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                    var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
                    var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
                    //  var DiscountAmount = isNaN(((parseFloat(PurchaseValue) + parseFloat(taxAmount)) * (DiscPer)) / parseFloat(100)) ? 0 : ((parseFloat(PurchaseValue) + parseFloat(taxAmount)) * (DiscPer)) / parseFloat(100);
                    var DiscountAmount = isNaN(((parseFloat(PurchaseValue)) * (DiscPer)) / parseFloat(100)) ? 0 : ((parseFloat(PurchaseValue)) * (DiscPer)) / parseFloat(100);
                    $("#txtPurDiscAmnt").val(parseFloat(DiscountAmount).toFixed(4));


                }

                else {


                    var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                    var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
                    var DiscPer = $("#txtPurDiscPer").val() == "" ? "0" : $("#txtPurDiscPer").val();
                    var DiscountAmount = isNaN((parseFloat(PurchaseValue) * parseFloat(DiscPer)) / parseFloat(100)) ? 0 : (parseFloat(PurchaseValue) * parseFloat(DiscPer)) / parseFloat(100);

                    $("#txtPurDiscAmnt").val(DiscountAmount.toFixed(4));

                }
                var PurchaseValue = $("#hfPurchaseValue").val() == "" ? "0" : $("#hfPurchaseValue").val();
                var taxAmount = $("#txttaxAmount").val() == "" ? "0" : $("#txttaxAmount").val();
                var DiscountAmount = $("#txtPurDiscAmnt").val() == "" ? "0" : $("#txtPurDiscAmnt").val();
                var NetPurchase = (parseFloat(PurchaseValue) + parseFloat(taxAmount)) - parseFloat(DiscountAmount);
                $("#txtNetPur").val(NetPurchase.toFixed(4));
                var mrp = parseFloat($("#hfMRP").val()).toFixed(4);
                //$("#txtMRP").val((mrp * Quantity).toFixed(4));//nj
                getSalesPrice(0);//nj

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
            function reasonChange() {
                var reasonval = $("#ddlReasons").val();
                $(".rsns").val(reasonval);
            }
            $("#ddlExpiryMethod").change(function () {
                if ($(this).val() == "SER") {
                    $("#liinsert").hide(); $("#divItems").hide();
                }
                else {
                    $("#liinsert").show(); $("#divItems").show();
                }

            }); function getCheckboxValue(para1) {

                var a = $('#txtKeyDis').val();
                if (a != 0 && para1 == "0")
                    para1 = a;
                if (a == 0)
                    para1 = para1
                if (para1 == "0") {

                    getpurchaseamount();
                    $('#txtKeyDis').val(para1);
                }
                if (para1 == "1") {
                    // alert(para1);
                    getpurchaseamount();
                    $('#txtKeyDis').val(para1);
                }
                if (para1 == "2") {

                    getpurchaseper();
                    $('#txtKeyDis').val(para1);
                }
            } function checkNumAndDecimal(evnt, txtbox) {
                //var input =[0];
                //alert(window.getSelection().toString().trim()
                //if (window.getSelection().toString().trim() != "") {

                //if (evnt.target.selectionEnd > 1) {
                //    evnt.target.selectionEnd = evnt.target.value.length + 1
                //CheckNumWithDecimalwithReplaceReplace(evnt);
                //}
                //else {
                //    CheckNumWithDecimalwithReplaceReplace(evnt, false);
                //}
                //CheckNumWithDecimal(evnt);
                var selectAll = false;
                if (window.getSelection) {

                    var ta = $(txtbox).get(0);
                    if (ta.value.substring(ta.selectionStart, ta.selectionEnd) != "") {
                        selectAll = true;
                    }

                }
                // For IE
                if (document.selection && document.selection.type != "Control") {
                    if (document.selection.createRange().text != "") {
                        selectAll = true;
                    }
                }
                CheckNumWithDecimalwithReplace(evnt, selectAll);




            }

        
            function clearOnDelete(event) {
                
                // Check if the key pressed is backspace (keyCode 8) or delete (keyCode 46)
                if (event.inputType === 'deleteContentBackward' || event.inputType === 'deleteContentForward') {
                    event.target.value = '';
                    $("#txtSupRefno").val("0");
                }
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
            function txtQuantityClear() {
                $("#txtQuantity").val('0');
                return false;
            }
        </script>


    </form>
</body>
</html>
