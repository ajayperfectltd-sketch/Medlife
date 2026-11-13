<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseOrder.aspx.cs" Inherits="PurchaseOrder" EnableEventValidation="false" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>


<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register src="Popup/CommonPopup.ascx" tagname="CommonPopup" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Purchase Order::</title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <script src="js/Navigation.js"></script>
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
            height:150px;
            width:940px;
        }  
      .fixed-remark {
    width: 400px;
    height: 60px;
    resize: none;
    display: block;
}

.remark-counter {
    display: block;
    font-size: 11px;
    color: gray;
    margin-top: 4px;
    text-align: right;
}
        
           
    </style>   
      
</head>
<body>
    <form id="frmPurchaseOrder" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                  <div class="btn_bg5"> 
                       <div class="heading2">
                    <div class="user">Purchase Order</div>
                           <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                  

                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle" TabIndex="123" runat="server" OnClientClick="ShowAddDiv();" OnClick="ImageButton1_Click" />
                      
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle"  TabIndex="122" runat="server" OnClick="imgbtnView_Click"   />
                        </div>
                               </asp:Panel>
                    </div>
         
                    <div class="clear"></div>
                    <div class="group_bg2">
                         <div class="main_bg">  
                        <div id="divAdd" runat="server" style="display: block;">
                            <ul class="contentul" id="Container">
                                <div class="divL">
                                    <li>
                                        <label class="label">Date</label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" onkeydown="EnterTab(event,'ddlStore');" ID="txtDate" TabIndex="100" AutoComplete="off" onchange="validateOrderDate();" />
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <%-- <asp:Button ID="btn" CssClass="button" runat="server" OnClientClick="return SelectName();" />--%>
                                    <li>
                                        <label class="label">Store</label>
                                        <asp:DropDownList runat="server" CssClass="dropdown" ID="ddlStore" onkeydown="EnterTab(event,'ddlDeliveryType');" TabIndex="101">
                                        </asp:DropDownList>
                                       <%-- <asp:RequiredFieldValidator ID="reqvalStore" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqvalStore">
                                        </cc:ValidatorCalloutExtender>--%>
                                   
                                    </li>
                                    <li>
                                        <label class="label">Delivery Type</label>
                                        <asp:DropDownList runat="server" onkeydown="EnterTab(event,'ImageButton4');" CssClass="dropdown" ID="ddlDeliveryType" TabIndex="102">
                                            <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                            <asp:ListItem Value="Urgent">Urgent</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                       <li style="display:none;">
                                        <asp:Label ID="lblPurchaseNo" runat="server" CssClass="label" Text="PurchaseNo"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtPurchaseNo"  Width="130px" MaxLength="150" TabIndex="103" />
                                        <asp:ImageButton ID="ImageButton5" runat="server" TabIndex="104" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 5px 0px 0px 5px; position: absolute" />
                                     </li>
                                  
                                </div>
                                <div class="divR">
                                    <li style="display:none;">
                                        <label class="label">Supplier</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlSupplier" AutoPostBack="true" TabIndex="104" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqvalSupplier" runat="server" ErrorMessage="Supplier required" ControlToValidate="ddlSupplier" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="reqvalSupplier">
                                        </cc:ValidatorCalloutExtender>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Supplier required" ControlToValidate="ddlSupplier" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="smartfill"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                        </cc:ValidatorCalloutExtender>--%>
                                           

                                    </li>
                                    <li>
                                        <asp:Label ID="Label61" runat="server" CssClass="label"  Text="Supplier"></asp:Label>
                                        <asp:TextBox ID="txtSupplierName" runat="server" AutoPostBack="false" CssClass="textboxMandatory" Width="185px" onBlur="popUpSupplierNameSelectSelect();" TabIndex="105" MaxLength="50"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton4" onkeydown="EnterTab(event,'ddlItemType');" runat="server" OnClick="ImageButton4_Click" TabIndex="106" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 1px 0px 0px 0px; position: absolute" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Select a Supplier" ControlToValidate="txtSupplierName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender59" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator32">
                                        </cc:ValidatorCalloutExtender>                                    
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Select a Supplier" ControlToValidate="txtSupplierName" Display="None" SetFocusOnError="True" ValidationGroup="smartfill"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                        </cc:ValidatorCalloutExtender>

                                    </li>


                                    <li>
                                        <label class="label">Order Type</label>
                                        <asp:DropDownList runat="server" CssClass="dropdown"  onkeydown="EnterTab(event,'txtDelivaryDate');" AutoPostBack="true" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged"   ID="ddlItemType" TabIndex="107">
                                              <asp:ListItem Text="All Item" Value="0"></asp:ListItem>
                                              <asp:ListItem Text="Supplier Wise" Value="1"></asp:ListItem>
                                              <asp:ListItem Text="Previous Purchase" Value="2"></asp:ListItem>
                                             <asp:ListItem Text="Quotation" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="label">Delivery Date</label>
                                        <asp:TextBox ID="txtDelivaryDate"  onkeydown="EnterTab(event,'btnSmartFill');" runat="server" CssClass="textbox" TabIndex="108" AutoComplete="off" onchange="validateDeliveryDate();"></asp:TextBox>
                                        <cc:CalendarExtender ID="txtDelivaryDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDelivaryDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy" >
                                        </cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Delivery Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="csvDatecheck" runat="server" ErrorMessage="Delivery date can not be less than the Date" ClientValidationFunction="ClientValidate" ControlToValidate="txtDelivaryDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:CustomValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="csvDatecheck">
                                        </cc:ValidatorCalloutExtender>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                     </div>
                                <br class="clear" />
                                      <li class="btnMargin">
                                        <asp:Button Text="Smart Fill" ID="btnSmartFill" onkeydown="EnterTab(event,'ImageButton3');" runat="server" TabIndex="109" CssClass="button" OnClick="btnSmartFill_Click"   ValidationGroup="smartfill" />
                                    </li>
                                    <div style="width: 940px; overflow: auto; height: 30px; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">
                                        <li id="liSupplierBalance" runat="server" style="margin-top: 4px;">
                                            <asp:Label ID="lblSupplierBalance" Text="" CssClass="lblbalance" runat="server" />
                                        </li>
                                    </div>
                               
                                <li class="clear"></li>
                                <div class="divSeparation" style="width:825px;"></div>
                                <li class="clear"></li>
                                <div class="divL">
                                    <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtItemShortName" onBlur="popUpStockSelect();" Width="182px" MaxLength="150" TabIndex="110" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select an Item" ControlToValidate="txtItemShortName"  Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:Button ID="Button1" runat="server" Text="Item" Style="display: none;" />
                                        <asp:ImageButton ID="ImageButton3" onkeydown="EnterTab(event,'ddlUnit');" runat="server" TabIndex="111" OnClientClick="return  popUpStockSelectClick();" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" />
                                     </li>
                                    <li style="display: none;" >
                                        <label class="label">Items</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlItem" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" Style="margin-top: 0px;">
                                        </asp:DropDownList>
                                        <asp:Button ID="btnItem" runat="server" Text="Item" OnClick="btnItem_Click" Style="display: none;" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Item required" ControlToValidate="ddlItem" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:ImageButton ID="imgPopUp" runat="server" OnClientClick="return SelectName();" ImageUrl="~/images/POS/icons_bg.jpg" Style="padding-left: 2px; margin-top: 0px; width: 30px; height: 25px;" />
                                    </li>
                                    <li>
                                        <asp:Label ID="Label161" runat="server" CssClass="label" Text="Unit"></asp:Label>
                                        <asp:DropDownList ID="ddlUnit"  onkeydown="EnterTab(event,'Subunit');" runat="server" CssClass="dropdownMandatory" TabIndex="112"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ErrorMessage="Select a Unit" ControlToValidate="ddlUnit" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender102" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator62">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                        <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Subunit"></asp:Label>
                                        <asp:DropDownList ID="ddlSubUnit" onkeydown="EnterTab(event,'txtPurchaseRate');" runat="server" CssClass="dropdown" TabIndex="113"></asp:DropDownList>
                                       
                                    </li>
                                        <li>
                                        <label class="label">Purchase Rate</label>
                                        <asp:TextBox ID="txtPurchaseRate" onkeydown="EnterTab(event,'txtQunatity');" runat="server" Style="text-align: right;" CssClass="textbox" onKeypress="CheckNumWithDecimal1(event);" MaxLength="10" AutoComplete="off" TabIndex="114"></asp:TextBox>
                                      <%-- <asp:RequiredFieldValidator ID="reqw" runat="server" ErrorMessage="Purchase rate required" ControlToValidate="txtPurchaseRate" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="reqw">
                                        </cc:ValidatorCalloutExtender>--%>
                                        <%--<cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" TargetControlID="txtPurchaseRate" FilterType="Custom , Numbers" ValidChars=".">
                                        </cc:FilteredTextBoxExtender>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPurchaseRate" ErrorMessage="Enter a valid Purchase Rate" Display="None" MaximumValue="999999999999" ValidationGroup="Add" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                        </cc:ValidatorCalloutExtender>--%>
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Sales Price 1</label>
                                        <asp:TextBox ID="txtSalesPrice" runat="server" Style="text-align: right;" CssClass="textbox" MaxLength="109" onKeypress="CheckNumWithDecimal(event);" TabIndex="188" AutoComplete="off"></asp:TextBox>
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Sales Price 2</label>
                                        <asp:TextBox ID="txtSalesPrice2" Style="text-align: right;" runat="server" CssClass="textbox" MaxLength="10" onchange="AmountAlert();" onKeypress="CheckNumWithDecimal(event);" TabIndex="199" AutoComplete="off"></asp:TextBox>
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Batch</label>
                                        <asp:TextBox ID="txtBatch" runat="server" CssClass="textbox" TabIndex="1000" AutoComplete="off"></asp:TextBox>
                                    </li>
                                </div>
                                <div class="divR">
                                    <li>
                                        <label class="label">Quantity</label>
                                        <asp:TextBox ID="txtQunatity" onkeydown="EnterTab(event,'btnAdd');" runat="server" CssClass="textboxMandatory" Style="text-align: right;" onKeypress="CheckInteger(event);" MaxLength="12" TabIndex="115" AutoComplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQunatity" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQunatity" ErrorMessage="Enter valid Quantity" Display="None" MaximumValue="999999999999" ValidationGroup="Add" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                
                                    <li>
                                        <label class="label">Current Stock</label>
                                        <asp:TextBox ID="lblCurrentStock" runat="server" ReadOnly="true" CssClass="textbox" Style="text-align: right;" MaxLength="10"   AutoComplete="off"></asp:TextBox>
                                        <%--<asp:Label ID="lblCurrentStock" runat="server" CssClass="label" Width="300px" Font-Bold="true" ForeColor="Red" Text="Current Stock=0"></asp:Label>--%>
                                    </li>
                                    <li>
                                        <label class="label">Ordered Quantity</label>
                                        <asp:TextBox ID="lblOrderedQuantity" runat="server" ReadOnly="true" CssClass="textbox" Style="text-align: right;" MaxLength="10"   AutoComplete="off"></asp:TextBox>
                                        <%--<asp:Label ID="lblOrderedQuantity" runat="server" CssClass="label" Width="300px" Font-Bold="true" ForeColor="Red" Text="Ordered Quantity=0"></asp:Label>--%>
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
                                    <asp:Button Text="Insert" ID="btnAdd"  runat="server" CssClass="button" OnClick="btnAdd_Click" ValidationGroup="Add" TabIndex="116" OnClientClick="CalculateTotal(); releasebtn();" />
                                    <asp:Button Text="Clear" ID="btnClearAdd" runat="server" CssClass="button" OnClientClick="ItemClear();return false;" TabIndex="117" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfPacked" runat="server" />
                                    <asp:HiddenField ID="hfQtyId" runat="server" />
                                      <asp:HiddenField ID="hfMasterID" runat="server" />
                                    <asp:HiddenField ID="hfuserName" runat="server" />
                                    <asp:Button ID="btnPurchaseNo" Text="Hide" runat="server" Style="display: none;" CausesValidation="false"   OnClick="btnPurchaseNo_Click"/>
                                </li>
                                <li class="clear"></li>
                             
                               <div style="width: 850px; height: 350px; overflow-y:auto; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">
                                   

                                        <asp:GridView ID="gvItems" runat="server" CssClass="grid"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                            BackColor="White"    BorderStyle="Solid"   BorderWidth="1px" Width="825px"  CellPadding="3" ForeColor="Black" GridLines="Vertical"  DataKeyNames="FK_Items"
                                            OnRowDeleting="gvItems_RowDeleting">
                                            <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                            <Columns>
                                                    <asp:TemplateField HeaderText="SlNo" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item" >
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblItemNameGv"      AutoComplete="off" CssClass="label"  Style="width: 150px;"
                                                            Text='<%# Eval("Item") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FK_Items" HeaderText="FK_Items" Visible="false" />
                                                <asp:BoundField  DataField="Units" HeaderText="Unit" />
                                                <asp:BoundField  DataField="SubUnit" HeaderText="Subunit" />
                                                  <asp:BoundField  HeaderText="Cur.Stock" ItemStyle-HorizontalAlign="Right" DataField="CurrentStock" />
                                                <asp:BoundField HeaderText="FK_Units"  Visible="false" />
                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtQuantity" onBlur="OnItemsQuantityChange(id);" onchange="CalculateTotalAmount();" onkeypress="CheckInteger(event);" AutoComplete="off" CssClass="textbox" Style="width: 50px; text-align: right;"
                                                            Text='<%# Eval("Quantity") %>' />
                                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0.00" Type="Double"></asp:RangeValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValtxidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                                        </cc:ValidatorCalloutExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SalesPrice" HeaderText="Sales Price 1" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                                <asp:BoundField DataField="SalesPrice2" HeaderText="Sales Price 2" ItemStyle-HorizontalAlign="Right" Visible="false" />                                         
                                                <asp:TemplateField HeaderText="Pur.Rate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtPurchaseRategv" onBlur="OnItemsPurchaseRateChange(id);" onchange="CalculateTotalAmount();" onkeypress="CheckNumWithDecimal1(event);" AutoComplete="off" CssClass="textbox" Style="width: 50px; text-align: right;"
                                                            Text='<%# Eval("PurchaseRate") %>' />
                                                        <asp:RangeValidator ID="RangeValidator25" runat="server" ControlToValidate="txtPurchaseRategv" ErrorMessage="Enter a valid Purchase Rate" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0.00" Type="Double"></asp:RangeValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValtxidatorCalloutExtender44" runat="server" Enabled="True" TargetControlID="RangeValidator25">
                                                        </cc:ValidatorCalloutExtender>
                                                        <%--<asp:RequiredFieldValidator ID="rqvalPurchaseRate" runat="server" ErrorMessage="Enter Purchase Rate"  Enabled="false"
                                                            ControlToValidate="txtPurchaseRategv"  Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="rqvalPurchaseRate">
                                                        </cc:ValidatorCalloutExtender>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Discount" DataField="Discount" ItemStyle-HorizontalAlign="Right" Visible="false" />                                            
                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>

                                                        <asp:Label ID="lblAmount" runat="server" Style="float: right;" Text='<%#Eval("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Batch No" DataField="BatchNo" Visible="false" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle  CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                            <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                   
                                </div>
                                <asp:Button ID="btnHiddenItemsQuantityChange" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnHiddenItemsQuantityChange_Click" />

                                <asp:Button ID="btnHiddenItemsPurchaseRateChange" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnHiddenItemsPurchaseRateChange_Click" />

                                <li class="clear"></li>
                                <div class="divSeparation" style="width:825px"></div>
                                <li class="clear"></li>
                                <div class="divL">
                                    <ul style="margin: 0px 0px 0px 0px;">
                                        <li>
                                            <label class="label">Remark</label>
                                            <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine"
                                                CssClass="multiline fixed-remark"
                                                MaxLength="500" TabIndex="118" AutoComplete="off"
                                                onkeyup="enforceMaxLength(this, 500); updateCharCounter(this, 'txtRemark_counter', 500);"
                                                onpaste="var self=this; setTimeout(function(){ enforceMaxLength(self, 500); updateCharCounter(self, 'txtRemark_counter', 500); }, 0);" />
                                            <span id="txtRemark_counter" class="remark-counter">
                                                500 characters remaining
                                            </span>
                                        </li>
                                        <li class="btnMargin">
                                            <asp:Label ID="lblActive" runat="server" CssClass="label" Text="Closed"></asp:Label>
                                            <asp:CheckBox ID="cbxIsClosed" runat="server" TabIndex="119" />
                                        </li>
                                        <li id="cbx" style="display: none;">
                                            <label class="label">Close Remark</label>
                                           <asp:TextBox ID="txtClosedRemark" runat="server" TextMode="MultiLine"
                                            CssClass="multiline fixed-remark"
                                            MaxLength="500" AutoComplete="off"
                                            onkeyup="enforceMaxLength(this, 500); updateCharCounter(this, 'txtClosedRemark_counter', 500);"
                                            onpaste="var self=this; setTimeout(function(){ enforceMaxLength(self, 500); updateCharCounter(self, 'txtClosedRemark_counter', 500); }, 0);" />
                                        <span id="txtClosedRemark_counter" class="remark-counter">
                                            500 characters remaining
                                        </span>
                                        </li>

                                    </ul>
                                </div>
                                <div class="divR">
                                    <li style="display: none;">
                                        <label class="label">Total Amount</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtTotalAmount" Style="text-align: right;" AutoComplete="off" MaxLength="10" onKeypress="CheckNumWithDecimal(event);" />
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Discount</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtDiscountLast" Style="text-align: right;" AutoComplete="off" MaxLength="10" onKeypress="CheckNumWithDecimal(event);"  />
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Round Amount</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtRoundAmount" MaxLength="10" Style="text-align: right;" AutoComplete="off" onkeypress="CheckNumWithDecimalNegative(event);" />
                                    </li>
                                    <li>
                                        <label class="label">Net Amount</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtNetAmount" MaxLength="10" Style="text-align: right;" AutoComplete="off" onKeypress="CheckNumWithDecimal(event);"  />
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Cash Amount</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtCashAmount" MaxLength="10" Style="text-align: right;" AutoComplete="off" onKeypress="CheckNumWithDecimal(event);"  />
                                    </li>
                                    <li style="display: none;">
                                        <label class="label">Advance</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtAdvance" MaxLength="10" Style="text-align: right;" AutoComplete="off" onKeypress="CheckNumWithDecimal(event);"  />
                                    </li>
                                   
                                    <asp:HiddenField ID="hdfPurcahseOrder" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                       <asp:HiddenField ID="hfItemvalue" runat="server" />
                                      <asp:HiddenField ID="HfItemsID" runat="server" />
                                     <asp:HiddenField ID="hfItem" runat="server" />
                                </div>
                                <div class="clear"></div>
                                 <li class="btnMargin">
                                        
                                        <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="120" OnClientClick="if(!validateDates()){ return false; } disablebutn();"/>
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" OnClick="btnClear_Click" TabIndex="121" />
                                    </li>
                            </ul>                           
                        </div>
                             </div>


                         <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                       
                            <asp:Panel ID="divPopup" Style="display: none;" CssClass="DivStyle" runat="server" >
                                <div id="divheader" runat="server" class="PopupHeader">
                                    <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head" CssClass="lblPopupHeader"></asp:Label>
                                    <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                </div>
                                
                                <uc1:commonpopup id="CommonPopup1" runat="server" />
                            </asp:Panel>
                           
                            <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                                CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                            </cc:ModalPopupExtender>


                        <div class="clear"></div>
                         <div class="main_bg">  
                        <div id="divView" runat="server" style="display: none;">                        
                            <ul class="contentul">
                                <div class="divL">
                                    <li>
                                        <label class="label">Store</label>
                                        <asp:DropDownList ID="ddlStoreSearch" onkeydown="EnterTab(event,'txtFromDate');" runat="server" CssClass="dropdown" TabIndex="30"></asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="label">From Date</label>
                                        <asp:TextBox ID="txtFromDate" onkeydown="EnterTab(event,'txtTodate');" CssClass="textbox" runat="server" TabIndex="31" AutoComplete="off"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <label class="label">To Date</label>
                                        <asp:TextBox ID="txtTodate" onkeydown="EnterTab(event,'ddlSupplierSearch');" CssClass="textbox" runat="server" TabIndex="32" AutoComplete="off"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtTodate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                    </li>
                                    <li class="clear"></li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch"  runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="36" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="37" />
                                    </li>
                                </div>
                                <div class="divR">
                                    <li>
                                        <label class="label">Supplier</label>
                                        <asp:DropDownList ID="ddlSupplierSearch" onkeydown="EnterTab(event,'ddlDeliveryTypeSearch');" runat="server" CssClass="dropdown" TabIndex="33"></asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="label">Delivery Type</label>
                                        <asp:DropDownList runat="server" onkeydown="EnterTab(event,'ddlClosedSearch');"  CssClass="dropdown" ID="ddlDeliveryTypeSearch" TabIndex="34">
                                            <asp:ListItem Value="" Selected="True">select</asp:ListItem>
                                            <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                            <asp:ListItem Value="Urgent">Urgent</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="label">Status</label>
                                        <asp:DropDownList runat="server"  CssClass="dropdown" ID="ddlClosedSearch" TabIndex="35">
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
                                    BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10"
                                    DataKeyNames="ID_PurchaseOrder" OnRowDeleting="gvPurchaseOrder_RowDeleting" OnPageIndexChanging="gvPurchaseOrder_PageIndexChanging" AllowPaging="true"  OnSelectedIndexChanging="gvPurchaseOrder_SelectedIndexChanging" OnRowDataBound="gvPurchaseOrder_RowDataBound"
                                    OnRowCommand="gvPurchaseOrder_RowCommand">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PONo" HeaderText="PO No" />
                                        <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                        <asp:BoundField DataField="Store" HeaderText="Store" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" />
                                        <asp:BoundField DataField="DeliveryType" HeaderText="Type" />
                                        <asp:BoundField DataField="NetAmount" HeaderText="Net.Amt" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Advance" HeaderText="Advance" ItemStyle-HorizontalAlign="Right" Visible="false" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" Visible="false" />
                                        <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button ID="btnStatus" CssClass="button" Width="65px" Text='<%# Bind("Status") %>' OnClientClick="ConfirmUpdatePurchaseOrder()" runat="server" CommandName="StatusUpdate" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" OnClick="ImageButton2_Click" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>                                             
                                              <asp:ImageButton ID="ImageButton343" runat="server" CommandName="Print" ToolTip="Print"  ImageUrl="~/images/1430738477_report.png" Width="20px" Height="20px" />                                             
                                            </ItemTemplate>
                                         </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle  CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                            </ul>
                        </div>
                             </div>
                        <div class="clear"></div>                   
                    </div>
                
                     <asp:Button ID="btnS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopUpdelete" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divPopheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader1" runat="server" Text="Reason" CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                                </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divPopUpdelete" TargetControlID="btnS"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divPopheader">
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/jquery-1.10.2.js"></script>
        <script src="js/Custom.js"></script>
        <script src="js/toastr.js"></script>
        <script type="text/javascript">
            function PopupOnReason() {
                try {


                    $("#divPopupReason").css('display', 'block');
                }
                catch (ex) {

                }
            }
            function BlurPurchaseNo() {

              //  alert('f');
                $("#<%#btnPurchaseNo.ClientID%>").click();
               // alert('d');

           }
            function ClearAdd() {
                submit = 0;
                $("#Container").find("[id*='txt']").val('');
                $("#hdfPurcahseOrder").val('');
                var date = new Date().format("dd/MM/yyyy");
                $("#txtDate,#txtDelivaryDate").val(date);
                $("#ddlSupplier,#ddlItemType").val('0');
                $("#txtPurchaseRate").val("0");
                $("#ddlDeliveryType").val("Normal");            
                ItemClear();
                $('#liSupplierBalance').hide();
                updateCharCounter(
                    document.getElementById('<%=txtRemark.ClientID%>'),
                    'txtRemark_counter', 500
                );
                                updateCharCounter(
                                    document.getElementById('<%=txtClosedRemark.ClientID%>'),
                    'txtClosedRemark_counter', 500
                );
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
                $("#ddlSubUnit").val("0");
                $("#txtPurchaseRate").val("0");
                $("#ddlUnit").val("0");
                $("#lblCurrentStock").val("0");
                $("#txtNetAmount").val("0");
                $("#lblOrderedQuantity").val("0");
                $("#HfItemsID").val("0");
                
                
                CalculateTotal();

            }

            function ClearSearch() {

                $("#<%#ddlStoreSearch.ClientID%>").val('0');
                $("#<%#ddlDeliveryTypeSearch.ClientID%>", "#<%#ddlSupplierSearch.ClientID%>").val("0");
                $("#ddlClosedSearch").val("");
                $("#ddlDeliveryTypeSearch").val('');             
                $("#ddlStoreSearch,#ddlSupplierSearch").val('0');
                $("#txtPurchaseRate").val("0");
                var date = new Date().format("dd/MM/yyyy");
                $("#txtFromDate,#txtTodate,#txtDate,#txtDelivaryDate").val(date);




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
            function AmountAlert() {
                if ($("#txtSalesPrice2").val() != "" && $("#txtPurchaseRate").val() != "") {
                    var sp2 = $("#txtSalesPrice2").val() == "" ? "0" : $("#txtSalesPrice2").val();
                    var PR = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
                    var sp1 = $("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val();

                    if (parseFloat(sp2) < parseFloat(PR) && parseFloat(sp1) < parseFloat(PR)) {
                        alertify.alert("Purchase Rate is greater than Sales Price !");
                    }
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
          
                $("#cbxIsClosed").bind("change", function () {
                    if ($("#cbxIsClosed").prop("checked") == true) {
                        $("#cbx").fadeIn();
                    }
                    else {
                        $("#cbx").fadeOut();
                    }
                });
                if ($("#cbxIsClosed").prop("checked") == true) {
                    $("#cbx").fadeIn();
                }
                else {
                    $("#cbx").fadeOut();
                }
                 $("#txtRoundAmount").bind("change", function () {
            
                });
             
            }

            function CalculateTotal() {
                var total = 0;
                $("[id*=txtQuantity]").each(function () {
                    var qty = parseFloat($(this).val()) || 0;
                    var rate = parseFloat($(this).closest("tr").find("[id*=txtPurchaseRategv]").val()) || 0;
                    var amt = Math.round((qty * rate) * 10000) / 10000;
                    $(this).closest("tr").find("[id*=lblAmount]").text(amt.toFixed(4));
                    total += amt;
                });

                $("#<%=txtTotalAmount.ClientID%>").val(total.toFixed(4));
    CalcNet();
}
            function CalcNet() {
                var total = parseFloat($("#<%=txtTotalAmount.ClientID%>").val()) || 0;
                var disc = parseFloat($("#<%=txtDiscountLast.ClientID%>").val()) || 0;
                var round = parseFloat($("#<%=txtRoundAmount.ClientID%>").val()) || 0;
                var advance = parseFloat($("#<%=txtAdvance.ClientID%>").val()) || 0;
                var net = (total - disc + round - advance);
                $("#<%=txtNetAmount.ClientID%>").val(net.toFixed(4));
}

// Bind on discount/round/advance change to update net live
$(document).ready(function () {
    $("#<%=txtDiscountLast.ClientID%>").on("input", function () { CalcNet(); });
    $("#<%=txtRoundAmount.ClientID%>").on("input", function () { CalcNet(); });
    $("#<%=txtAdvance.ClientID%>").on("input", function () { CalcNet(); });
});
            function CalculateNetAmt() {
                var sum = parseFloat($("#txtTotalAmount").val() || "0");
                var rounding = parseFloat($("#txtRoundAmount").val() || "0");
                var net = sum + rounding;

                if (net < 0) {
                    $("#txtRoundAmount").val("0.0000");
                    net = sum;
                }

                if (net > MAX_AMOUNT) {
                    alertify.alert("Net amount exceeds maximum allowed limit.");
                    $("#txtRoundAmount").val("0.0000");
                    $("#txtNetAmount").val(sum.toFixed(4));
                    return;
                }

                $("#txtNetAmount").val(net.toFixed(4));
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
                    var hfUser = $("#hfUser").val();
                    var hfClient = $("#hfClient").val();
                    var ddlCategory = $("#ddlCategory").val();
                    var ddlSupplier = $("#ddlSupplier").val();
                    var txtInvoiceDate = $("#txtDate").val();
                    var hfItemvalue = $("#hfItemvalue").val();
                    var ItemType = "2";
                    $.ajax({
                        type: "POST",
                        url: 'Textile.asmx/GetUrlEncrypt',
                        data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','ddlSupplier':'" + ddlSupplier + "','txtInvoiceDate':'" + txtInvoiceDate + "','hfItemvalue':'" + hfItemvalue + "','hfClient':'" + hfClient + "','ItemType':'" + ItemType + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (MenuHtml) {

                            //url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val() + '&Supplier=' + $("#ddlSupplier").val() + '&Date=' + $("#txtInvoiceDate").val()

                            url = "ItemSearch.aspx?" + MenuHtml.d;

                            popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                            ",statusbar=no,menubar=no,resizable=0,width=1015," +
                            "height=650,left = 200,top = 100");
                            popUpObj.focus();

                        },
                        error: function (MenuHtml) {

                        }
                    });
                    //url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=0';

                    //popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=yes,location=no" +
                    //",statusbar=no,menubar=no,resizable=0,width=1000," +
                    //"height=600,left = 200,top = 100");
                    //popUpObj.focus();

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
                        url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=0';

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
            function EnterTab(e, Id1) {

                var intKey = window.Event ? e.which : e.KeyCode;


                if (intKey == 13) {
                    e.returnValue = false;
                    $("#" + Id1).focus();
                }
            }
            function OnItemsQuantityChange(id) {
                document.getElementById("hfQtyId").value = id.replace("txtQuantity", "txtPurchaseRategv");
                // $("#"+a).value="assa";
                //alert(a);

                //var reqval = id.replace('txtQuantity', 'rqvalPurchaseRate');
                var txtqty = id.replace('txtQuantity', 'txtQuantity');
                var txtPurRate = id.replace('txtQuantity', 'txtPurchaseRategv');
                var qty = $("#" + txtqty).val() == "" ? "0" : $("#" + txtqty).val();
                var PurRate = $("#" + txtPurRate).val() == "" ? "0" : $("#" + txtPurRate).val();
                $("#" + txtqty).val(qty);
                $("#" + txtPurRate).val(PurRate);
                
                $("#btnHiddenItemsQuantityChange").click();
            }
            //function focusItem() {
            //    var a = document.getElementById("hfQtyId").value;
            //    document.getElementById(a).focus();
            //}
            function OnItemsPurchaseRateChange(id) {
                try{
                    var b = id.replace("gvItems_txtPurchaseRategv_", "");
                    var c = parseInt(b) + 1;
                    var d = 'gvItems_txtQuantity_' + c;
                    var gvItems = document.getElementById("gvItems");
                    var lastRow = gvItems.rows.length;
                    var k = 'gvItems_txtPurchaseRategv_' + (lastRow - 2).toString();
                    if (id == k) {
                        document.getElementById("hfQtyId").value = 'LastRow';
                    }
                    else {
                        document.getElementById("hfQtyId").value = d;
                    }
                    //var reqval = id.replace('txtPurchaseRategv', 'rqvalPurchaseRate');
                    var txtqty = id.replace('txtPurchaseRategv', 'txtQuantity');
                    var txtPurRate = id.replace('txtPurchaseRategv', 'txtPurchaseRategv');
                    // if ($("#" + hdfpacked).val() == "False") {
                    var qty = $("#" + txtqty).val() == "" ? "0" : $("#" + txtqty).val();
                    var PurRate = $("#" + txtPurRate).val() == "" ? "0" : $("#" + txtPurRate).val();
                    $("#" + txtqty).val(qty);
                    $("#" + txtPurRate).val(PurRate);
                   
                    $("#btnHiddenItemsPurchaseRateChange").click();
                
                }
                catch (ex)
                { alert(ex)}
            }

            var MAX_AMOUNT = 999999999999.9999; // 12 integer digits, 4 decimal places

            function CalculateTotalAmount() {
                try {
                    var overLimit = false;

                    $.each($("table[id*=" + 'gvItems' + "] tr"), function (index, item) {
                        var qtyRaw = $(item).find("[id*=gvItems_txtQuantity_]").val();
                        var rateRaw = $(item).find("[id*=gvItems_txtPurchaseRategv_]").val();

                        if (!qtyRaw && !rateRaw) return; // header/empty row

                        var qty = parseFloat(qtyRaw || "0");
                        var rate = parseFloat(rateRaw || "0");

                        // Validate individual inputs before multiplying
                        if (qty > 999999999999) {
                            alertify.alert("Quantity exceeds maximum allowed value (999999999999).");
                            $(item).find("[id*=gvItems_txtQuantity_]").val("0").focus();
                            overLimit = true;
                            return false; // break $.each
                        }
                        if (rate > 999999999999.9999) {
                            alertify.alert("Purchase rate exceeds maximum allowed value.");
                            $(item).find("[id*=gvItems_txtPurchaseRategv_]").val("0").focus();
                            overLimit = true;
                            return false;
                        }

                        var amount = safeMultiply(qty, rate);

                        if (amount > MAX_AMOUNT) {
                            alertify.alert("Row amount exceeds maximum allowed limit of 999,999,999,999.9999. Please reduce quantity or rate.");
                            $(item).find("[id*=gvItems_txtQuantity_]").val("0").focus();
                            overLimit = true;
                            return false;
                        }

                        var formatted = formatDecimal(amount, 4);
                        $(item).find("[id*='lblAmount_']").text(formatted);
                    });

                    if (!overLimit) {
                        CalculateTotal();
                    }

                } catch (e) {
                    alert(e + ' CalculateTotalAmount');
                }
            }

            function popUpSupplierNameSelectSelect() {

                try {

                    var ItemName = $("#txtSupplierName").val();
                    if (ItemName.length > 0) {
                        $("#ImageButton4").click();
                    }

                }
                catch (e) {
                    alert(e);
                }

            }
            function safeMultiply(a, b) {
                // Scale both to 4 decimal places, multiply as integers, scale back
                var SCALE = 10000;
                var ai = Math.round(parseFloat(a) * SCALE);
                var bi = Math.round(parseFloat(b) * SCALE);
                // Use string-based check before multiplying
                var result = (ai * bi) / (SCALE * SCALE);
                return result;
            }

            // Format a number to 4 decimal places, never scientific notation
            function formatDecimal(val, places) {
                places = places || 4;
                if (isNaN(val) || val === null) return "0.0000";
                // toFixed switches to scientific notation above ~1e21 — guard first
                if (Math.abs(val) > 999999999999.9999) return null; // over limit
                return val.toFixed(places);
            }
            function enforceMaxLength(textarea, maxLen) {
                try {
                    if (textarea.value.length > maxLen) {
                        textarea.value = textarea.value.substring(0, maxLen);
                    }
                } catch (e) { }
            }

            function updateCharCounter(textarea, spanId, maxLen) {
                try {
                    var remaining = maxLen - textarea.value.length;
                    var span = document.getElementById(spanId);
                    if (!span) return;

                    span.textContent = remaining + ' characters remaining';

                    // Warning colour when within 20 chars of limit
                    if (remaining <= 20) {
                        span.style.color = '#C04828';
                        span.style.fontWeight = '500';
                    } else {
                        span.style.color = '';
                        span.style.fontWeight = '';
                    }
                } catch (e) { }
            }




            //date


            // Returns the current financial year start date (April 1)
            // Indian financial year: April 1 of current year if month >= April,
            // else April 1 of previous year
            function getFinancialYearStart() {
                var today = new Date();
                var fyYear = (today.getMonth() >= 3)   // month is 0-indexed; 3 = April
                    ? today.getFullYear()
                    : today.getFullYear() - 1;
                return new Date(fyYear, 3, 1);         // April 1
            }

            // Parses a dd/MM/yyyy string into a Date (time stripped to midnight)
            function parseDMY(str) {
                if (!str || str.trim() === '') return null;
                var parts = str.split('/');
                if (parts.length !== 3) return null;
                var d = parseInt(parts[0], 10);
                var m = parseInt(parts[1], 10) - 1;    // month is 0-indexed
                var y = parseInt(parts[2], 10);
                if (isNaN(d) || isNaN(m) || isNaN(y)) return null;
                return new Date(y, m, d, 0, 0, 0, 0);
            }

            // Strips time from a Date so comparisons are date-only
            function stripTime(dt) {
                return new Date(dt.getFullYear(), dt.getMonth(), dt.getDate(), 0, 0, 0, 0);
            }

            // Validates the Order Date field.
            // Called onchange of txtDate and also from validateDates() before save.
            // Returns true if valid, false if not (and shows the appropriate message).
            function validateOrderDate() {
                var dateStr = document.getElementById('<%=txtDate.ClientID%>').value;

    if (!dateStr || dateStr.trim() === '') {
        toastr.warning('Order date is required.');
        document.getElementById('<%=txtDate.ClientID%>').focus();
        return false;
    }

    var orderDate = parseDMY(dateStr);
    if (!orderDate) {
        toastr.warning('Order date is not in a valid format (dd/MM/yyyy).');
        document.getElementById('<%=txtDate.ClientID%>').focus();
        return false;
    }

    var today = stripTime(new Date());
    var fyStart = getFinancialYearStart();

    if (orderDate > today) {
        toastr.warning('Order date cannot be a future date.');
        document.getElementById('<%=txtDate.ClientID%>').value = today.getDate().toString().padStart(2, '0') + '/' +
            (today.getMonth() + 1).toString().padStart(2, '0') + '/' + today.getFullYear();
        document.getElementById('<%=txtDate.ClientID%>').focus();
        return false;
    }

    if (orderDate < fyStart) {
        var fyMsg = 'Order date cannot be from a previous financial year. '
            + 'Allowed from: ' + fyStart.getDate().toString().padStart(2, '0') + '/'
            + (fyStart.getMonth() + 1).toString().padStart(2, '0') + '/'
            + fyStart.getFullYear() + '.';
        toastr.warning(fyMsg);
        document.getElementById('<%=txtDate.ClientID%>').focus();
        return false;
    }

    return true;
}

// Validates the Delivery Date field.
// Called onchange of txtDelivaryDate and also from validateDates() before save.
function validateDeliveryDate() {
    var orderStr = document.getElementById('<%=txtDate.ClientID%>').value;
    var delivStr = document.getElementById('<%=txtDelivaryDate.ClientID%>').value;

    if (!delivStr || delivStr.trim() === '') {
        toastr.warning('Delivery date is required.');
        document.getElementById('<%=txtDelivaryDate.ClientID%>').focus();
        return false;
    }

    var delivDate = parseDMY(delivStr);
    if (!delivDate) {
        toastr.warning('Delivery date is not in a valid format (dd/MM/yyyy).');
        document.getElementById('<%=txtDelivaryDate.ClientID%>').focus();
        return false;
    }

    var orderDate = parseDMY(orderStr);
    if (orderDate && delivDate < orderDate) {
        toastr.warning('Delivery date cannot be earlier than the order date.');
        document.getElementById('<%=txtDelivaryDate.ClientID%>').focus();
        return false;
    }

    return true;
}

// Master date validation — called from btnSave OnClientClick
function validateDates() {
    if (!validateOrderDate()) return false;
    if (!validateDeliveryDate()) return false;
    return true;
}

// Replaces the existing ClientValidate ASP.NET CustomValidator function
// so the existing ValidatorCalloutExtender still works
function ClientValidate(source, arguments) {
    try {
        var orderStr = document.getElementById('<%=txtDate.ClientID%>').value;
        var delivStr = arguments.Value;

        var arr1 = orderStr.split('/');
        var arr2 = delivStr.split('/');
        var orderDate = new Date(arr1[2], parseInt(arr1[1]) - 1, arr1[0]);
        var delivDate = new Date(arr2[2], parseInt(arr2[1]) - 1, arr2[0]);

        arguments.IsValid = (delivDate >= orderDate);
    } catch (e) {
        arguments.IsValid = true;  // never block on error
    }
}



        </script>
    </form>
</body>
</html>
