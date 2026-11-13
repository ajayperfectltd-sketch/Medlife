<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IssueOut.aspx.cs" Inherits="IssueOut" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <script src="js/Custom.js"></script>
      <script src="js/Navigation.js"></script>
  <style type="text/css">
      .FixedItemStyle{
         display:none;

      }
 </style>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <div class="btn_bg5"> 
                      <div class="heading2">
                           <div class="user">Damage</div>
                    <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                      <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/ADD.png"  CssClass="imagebuttonStyle" TabIndex="11" runat="server" CausesValidation="false" OnClientClick="ShowAddDiv(); ClearAdd();releasebtn();" OnClick="imgbtnAdd_Click" />
                        <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle" CausesValidation="false"  OnClientClick="SearchClear();" TabIndex="18" runat="server" OnClick="imgbtnView_Click" />
                        
                       
                    </div>
                        </asp:Panel>
                         </div>
                      <div class="clear"></div>
                     <div class="group_bg2">
                        <div class="main_bg"> 
              <%--  <div class="group_bg2" style="width: 980px;">--%>
                    <ul class="contentul" id="Container">
                        <div id="divAdd" runat="server" style="display: block;">

                            <div class="divL">
                                <li>
                                    <label class="label">Date</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtDate" onkeydown="EnterTab(event,'ddlStore');" TabIndex="1" AutoComplete="off" />
                                    <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>

                                    <asp:CustomValidator ID="cvDatecheck" runat="server"
                                        ClientValidationFunction="ClientValidate" ControlToValidate="txtDate" Display="None"
                                        CssClass="validator"
                                        ErrorMessage="Future date not allowed" SetFocusOnError="True" ValidationGroup="submit"></asp:CustomValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="cvDatecheck">
                                    </cc:ValidatorCalloutExtender>

                                </li>
                           
                              
                            </div>
                            <div class="divR">
                                <li>
                                    <label class="label">Store</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" onkeydown="EnterTab(event,'ddlCategory');" ID="ddlStore" TabIndex="2" AutoPostBack="true"  Style="margin-left: 10px;" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                                    </asp:DropDownList>


                                    <asp:RequiredFieldValidator ID="rfvStore" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="rfvStore">
                                    </cc:ValidatorCalloutExtender>


                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="search"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>


                                </li>
                              
                            </div>


                            <br class="clear" />
                            <br class="clear" />
                            <div class="divSeparation"></div>
                           
                            <div>
                                <div class="divL">
                                         <li>
                                    <label class="label">Category</label>
                                    <asp:DropDownList runat="server" onkeydown="EnterTab(event,'imgPopUp');" CssClass="dropdownMandatory" ID="ddlCategory" TabIndex="3" AutoPostBack="true"  OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="Category required"
                                            ControlToValidate="ddlCategory" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="vceCategory" runat="server" Enabled="True" TargetControlID="rfvCategory">
                                        </cc:ValidatorCalloutExtender>
                             
                                </li>
                                    <li>


                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtitem" Width="185px" MaxLength="150" AutoComplete="off"  onblur="popUpStockSelect();" />
                                        <asp:TextBox runat="server" ID="txtItemShortNameCopy" Width="130px" MaxLength="150" AutoComplete="off" Style="display: none;" />
                                        <asp:Button ID="btnItem" runat="server" Text="Item" OnClick="btnItem_Click" CausesValidation="false" Style="display: none;" />

                                        <asp:ImageButton ID="imgPopUp" onkeydown="EnterTab(event,'txtQuantity');" runat="server" OnClientClick="return  popUpStockSelectClick();" ImageUrl="images/serach_icon.png" TabIndex="5" style="margin-top:5px; position:absolute; margin-left:4px; "/>
                                        <asp:TextBox ID="txtfkIssuedItems" runat="server" Style="display: none;"></asp:TextBox>

                                   
                                    </li>
                                          <li>
                                     
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Text="Quantity"></asp:Label>
                                        <asp:TextBox ID="txtQuantity" onkeydown="EnterTab(event,'ddlIssueType');" CssClass="textboxMandatory"  onkeypress="DecimalOrIntValidation(event);" runat="server" TabIndex="6" MaxLength="10" style="text-align: right;" AutoComplete="off"></asp:TextBox>
                                        <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" TargetControlID="txtQuantity" FilterType="Custom , Numbers" ValidChars=".">
                                        </cc:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" SetFocusOnError="true" ValidationGroup="add" MinimumValue="0.001" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>


                                </div>
                                <div class="divR">
                                    
                                        <li style="display: none;">
                                            <span>PR</span>
                                            <asp:TextBox ID="txtPurChaseRate" runat="server"></asp:TextBox>

                                        </li>
                                      <li>
                                    <label class="label" style="margin-left: 2px;">Type</label>
                                    <asp:DropDownList runat="server" onkeydown="EnterTab(event,'btnAddItems');" CssClass="dropdownMandatory" ID="ddlIssueType" Style="margin-left: 8px;" TabIndex="7" onchange="BindReason();">
                                        <asp:ListItem Selected="True" Value="0">select</asp:ListItem>
                                        <asp:ListItem Value="D">Damage</asp:ListItem>
                                        <asp:ListItem Value="W">Write-Off</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvIssueMode" runat="server" ErrorMessage="Issue Type required" ControlToValidate="ddlIssueType" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rfvIssueMode">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                              
                                    <li style="display: none;">
                                        <span>RP</span>
                                        <asp:TextBox ID="txtSalesPrice" runat="server"></asp:TextBox>
                                    </li>
                                      <asp:Button ID="btnReason" runat="server" Text="Button"   OnClick="btnReason_Click" CausesValidation="false" Style="display: none;" />
                                      <li>
                                    <label class="label" id="lblReason" runat="server" style="margin-left: 2px;">Reason</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlReason"  Style="margin-left: 8px;" AutoPostBack="true" onkeydown="return KEyCode();" >
                                    </asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="rfvreason" runat="server" ErrorMessage="Reason required" ControlToValidate="ddlReason" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfvreason">
                                    </cc:ValidatorCalloutExtender>
                             
                                </li>
                                </div>
                                <li class="clear"></li>
                                <div class="divSeparation"></div>
                                <li style="height: 100px; overflow-y: auto; padding: 0px 0px 0px 0px; margin: 0px 0px 0px 0px;">
                                   
                                    <asp:GridView ID="gvIssuedItemDetails" runat="server" CssClass="grid"
                                        AutoGenerateColumns="False" Width="840px"
                                        BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items,ID_Stock" PageSize="100" EmptyDataText="No Data Found!" >
                                        <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                        <Columns>
                                            <asp:BoundField HeaderText="BatchNo" DataField="BatchNo" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date" />
                                             <asp:BoundField DataField="CurrentStock" HeaderText="CurrentStock" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="PurRate" HeaderText="Purchase Rate" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="MRP" HeaderText="MRP" ItemStyle-HorizontalAlign="Right" />
                                           <asp:BoundField DataField="SalPrice" HeaderText="SalesPrice" ItemStyle-HorizontalAlign="Right" />
<%--                                         <asp:BoundField DataField="RetailPrice" HeaderText="RetailPrice" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="WholesalePrice" HeaderText="WholesalePrice" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="PrivilagePrice" HeaderText="PrivilagePrice" ItemStyle-HorizontalAlign="Right" />--%>

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
                                </li>
                                <li class="btnMargin">
                                    <asp:Button Text="Insert" ID="btnAddItems" runat="server" OnClick="btnAddItems_Click" TabIndex="8" OnClientClick="CalCulateSum();" ValidationGroup="add" CssClass="button" />
                                    <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="9" OnClick="btnClearItems_Click" CausesValidation="false" />
                                </li>
                          
                                <div class="divSeparation"></div>
                                <li>

                                    <div style="height: 370px; width: 890px; overflow-y: auto; margin: 0px 0px 0px 0px;">
                                        <asp:GridView ID="gvIssuedItems" runat="server" CssClass="grid"
                                           AutoGenerateColumns="False" Width="840px"
                                            BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items,FK_Stock,CurrentStock" PageSize="100" OnSelectedIndexChanging="gvIssuedItems_SelectedIndexChanging" OnRowDeleting="gvIssuedItems_RowDeleting" EmptyDataText="No Data Found!">
                                            <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />

                                            <Columns>
                                              
                                                <asp:TemplateField HeaderText="SlNo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="Item" HeaderText="Item" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="PRate" HeaderText="Purchase Rate" ItemStyle-HorizontalAlign="Right" />
                                               <asp:BoundField DataField="SalPrice" HeaderText="Sales Price" ItemStyle-HorizontalAlign="Right" />
                                              
                                                <asp:BoundField DataField="TotalAmt"  HeaderText="Total Amount" ItemStyle-HorizontalAlign="Right" />
                                                
                                                   <asp:BoundField DataField="Fk_Reason" HeaderText="Fk_Reason"    ItemStyle-HorizontalAlign="Right" />
                                                   
                                               
                                                 <asp:BoundField DataField="IssueType" HeaderText="IssueType"  ItemStyle-HorizontalAlign="Right" />
                                                
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

                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                            <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />

                                        </asp:GridView>
                                    </div>
                                </li>
                            </div>
                            <div>

                                <div class="divSeparation"></div>
                                <div class="divR">
                                    <li class="btnMargin" style="padding-left: 15px;">
                                        <label class="label">Total Amount</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtAmount" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);" TabIndex="23" AutoComplete="off" />


                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtAmount" ErrorMessage="Enter a valid Amount" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Total Amount required" ControlToValidate="txtAmount" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li class="clear"></li>
                                     </div>
                                    <li class="btnMargin">
                                       <%-- <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="9" CausesValidation="False" />--%>
                                        <asp:Button Text="Submit" runat="server" CssClass="button" ID="Button1" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="9" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" CausesValidation="false" TabIndex="10" OnClick="btnClear_Click1" />
                                    </li>
                                    <li class="clear"></li>
                                    <li class="clear"></li>
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                    <asp:HiddenField ID="hfIssueOut" runat="server" />
                                    <asp:HiddenField ID="hdfFKStock" runat="server" />
                                    <asp:HiddenField ID="hdfRowindex" runat="server" />
                                    <asp:HiddenField ID="hdfFkItem" runat="server" />
                                    <asp:HiddenField ID="hdfcategory" runat="server" />
                                    <asp:HiddenField ID="hdfCurrentStock" runat="server" />
                                    <asp:HiddenField ID="hdfPacked" runat="server" />
                                     <asp:HiddenField ID="hfuserName" runat="server" />
                                 <asp:HiddenField ID="hfItemName" runat="server" />
                                 <asp:HiddenField ID="hfItem" runat="server" />

                                    <%-- <asp:HiddenField ID="hdfPurChaseRate" runat="server" />--%>
                               

                            </div>
                        </div>
                          </div>
                        <div class="main_bg"> 
                        <div id="divView" runat="server" style="display: none;">
                            <div class="divL">
                                <li>
                                    <label class="label">Store</label>
                                    <asp:DropDownList ID="ddlSearchStore" onkeydown="EnterTab(event,'txtFromDate');" runat="server" CssClass="dropdown" TabIndex="12"></asp:DropDownList>


                                </li>
                                <li>
                                    <label class="label">From Date</label>
                                    <asp:TextBox ID="txtFromDate" onkeydown="EnterTab(event,'txtTodate');" CssClass="textbox" runat="server" TabIndex="13" AutoComplete="off"></asp:TextBox>
                                    <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                    </cc:CalendarExtender>
                                </li>
                                              <li>
                                    <label class="label">To Date</label>
                                    <asp:TextBox ID="txtTodate"  onkeydown="EnterTab(event,'btnSearch');" CssClass="textbox" runat="server" TabIndex="15" AutoComplete="off"></asp:TextBox>
                                    <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtTodate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                    </cc:CalendarExtender>

                                </li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="16" OnClick="btnSearch_Click" CausesValidation="false" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="17" CausesValidation="false" />
                                </li>
                            </div>
                            <div class="divR">
                                <li style="display:none">
                                    <label class="label">Type</label>
                                    <asp:DropDownList ID="ddlIssueTypeSearch" runat="server" CssClass="dropdown" TabIndex="14">
                                        <asp:ListItem Selected="True" Value="0">select</asp:ListItem>
                                        <asp:ListItem Value="D">Damage</asp:ListItem>
                                        <asp:ListItem Value="W">Wright Off</asp:ListItem>
                                       <%-- <asp:ListItem Value="I">Issue Out</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </li>
                  

                            </div>

                            <asp:HiddenField ID="hdfSearchCriteria" runat="server" />
                            <br class="clear" />
                            <li>
                                <%--EmptyDataText="No Data Found!"--%>
                                <asp:GridView ID="gvViewIssued" runat="server" CssClass="grid"
                                    AutoGenerateColumns="False" DataKeyNames="ID_IssueOut" EmptyDataText="No Data Found!"
                                    BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" style="width:840px;" ForeColor="Black" GridLines="Vertical" PageSize="100" OnRowDeleting="gvViewIssued_RowDeleting" OnSelectedIndexChanging="gvViewIssued_SelectedIndexChanging">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle"/>
                                    <Columns>
                                       
                                        <asp:BoundField DataField="SlNo" HeaderStyle-HorizontalAlign="Center" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="TransDate" HeaderText="TransDate" />
                                        <asp:BoundField DataField="Store" HeaderText="Store" />
                                       <%-- <asp:BoundField DataField="Category" HeaderText="Category" />--%>
                                        <asp:BoundField DataField="IssueMode" HeaderText="Issue Type" Visible="false" />
                                        <asp:BoundField DataField="TotalAmt" HeaderText="Total Amt" ItemStyle-HorizontalAlign="Right" />
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
                                <div class="Pager">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" OnClick="Page_Changed" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                            </li>
                        </div>
                            </div>
                   <%--     <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                    </ul>

                </div>
                     
                <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right;" CssClass="DivStyle">
                    <div id="divheader" runat="server" class="PopupHeader">
                        <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head" CssClass="lblPopupHeader"></asp:Label>
                        <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                    </div>
                    <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                </asp:Panel>
                <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                    CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                </cc:ModalPopupExtender>
                    <asp:Button ID="btnS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopUpdelete" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divPopheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader1" runat="server" Text="Reason" CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
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

            </ContentTemplate>
        </asp:UpdatePanel>
           <script>
               function ClearAdd() {
                   submit = 0;
                   $("#<%#txtDate.ClientID%>").val(new Date().format("dd/MM/yyyy"));

                   // _defaultStore is set by SetDefaultStore() via ScriptManager on page load
    var storeVal = (typeof _defaultStore !== 'undefined' && _defaultStore !== '')
                   ? _defaultStore : '0';
    $("#<%#ddlStore.ClientID%>").val(storeVal);

    $("#<%#ddlCategory.ClientID%>").val('0');
                   $("#<%#hfIssueOut.ClientID%>").val('0');
                   $("#<%#hdfPacked.ClientID%>").val('False');
                   ClearDetails();
               }
        function DecimalOrIntValidation(eventRef) {
            try {
               
                if ($("#hdfPacked").val() == "False") {
                    CheckNumWithDecimal(eventRef);
                }
                else {
                    CheckInteger(eventRef);
                }
            }
            catch (e) {
                alert(e);
            }
        }
        function BindReason() {

            $("#btnReason").click();
        }


        function ClearDetails() {
            $("#<%#txtitem.ClientID%>").val("");
            $("#<%#txtItemShortNameCopy.ClientID%>").val("");
            $("#<%#txtQuantity.ClientID%>").val("");
            $("#<%#txtfkIssuedItems.ClientID%>").val("");
            $("#<%#hdfFkItem.ClientID%>").val("");
            $("#<%#hdfFKStock.ClientID%>").val("");
            $("#<%#txtPurChaseRate.ClientID%>").val("");
            $("#<%#txtSalesPrice.ClientID%>").val("");
            $("#<%#hdfRowindex.ClientID%>").val("");
            $("#<%#hdfcategory.ClientID%>").val("");
            $("#<%#hdfCurrentStock.ClientID%>").val("0");
            ClearSearch();
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();

        }
        function ClearSearch() {


            $("#ddlSearchStore").val('0');
            $("#ddlIssueTypeSearch").val('0');
            $("#txtFromDate").val(new Date().format("dd/MM/yyyy"));
            $("#txtTodate").val(new Date().format("dd/MM/yyyy"));

        }
        function PopupOnReason() {
            try {


                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {

            }
        }
        function CalCulateSum() {
            var sum = 0;
            $("#gvIssuedItems tr:has(td)").each(function (index, item) {

                //var RetailPrice = $(this).find("td:eq(4)").html();
                var Amt = $(this).find("td:eq(5)").html();

                Amt = (Amt == '' || Amt == null ? 0 : Amt);
                // RetailPrice = (RetailPrice == '' || RetailPrice == null ? 0 : RetailPrice);

                sum = parseFloat(sum) + parseFloat(Amt);
                sum = sum.toFixed(4);
            });
            $("#txtAmount").val(sum);
        }
        function pageLoad() {
            CalCulateSum();
        }


        function popUpStockSelect() {

            try {
                var ItemName = $("#txtitem").val();

                if (ItemName.length > 0) {

                    if (Page_ClientValidate("search")) {


                        //var Store = $("#ddlStore").find("option:selected").val();
                        //var Category = $("#ddlCategory").find("option:selected").val();

                        var hfUser = $("#hfUser").val();
                        var hfClient = $("#hfClient").val();
                        var ddlCategory = $("#ddlCategory").find("option:selected").val();
                        var hfStore = $("#ddlStore").find("option:selected").val();
                        var txtSupRefno = "";

                        $.ajax({
                            type: "POST",
                            url: 'Textile.asmx/GetUrlEncryptStock',
                            data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','txtSupRefno':'" + txtSupRefno + "','hfStore':'" + hfStore + "','hfClient':'" + hfClient + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (MenuHtml) {

                             
                                url = "StockItems.aspx?" + MenuHtml.d;

                                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                                ",statusbar=no,menubar=no,resizable=0,width=1000," +
                                "height=800,left = 200,top = 100");
                                popUpObj.focus();

                            },
                            error: function (MenuHtml) {

                            }
                        });

                        //url = "StockItems.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=' + Category + '&str=' + Store;
                        //popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" + ",statusbar=no,menubar=no,resizable=0,width=1000," + "height=600,left = 200,top = 100");
                        //popUpObj.focus();
                    }
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

        function popUpStockSelectClick() {

            try {
                if (Page_ClientValidate("search")) {
                    var ItemName = $("#txtitem").val();
                    //var Store = $("#ddlStore").find("option:selected").val();
                    //var Category = $("#ddlCategory").find("option:selected").val();

                   
                    var hfUser = $("#hfUser").val();
                    var hfClient = $("#hfClient").val();
                    var ddlCategory = $("#ddlCategory").find("option:selected").val();
                    var hfStore = $("#ddlStore").find("option:selected").val();
                    var txtSupRefno ="";

                    $.ajax({
                        type: "POST",
                        url: 'Textile.asmx/GetUrlEncryptStock',
                        data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','txtSupRefno':'" + txtSupRefno + "','hfStore':'" + hfStore + "','hfClient':'" + hfClient + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (MenuHtml) {

                            // url = "PreviousPurchase.aspx?" + MenuHtml.d;
                          
                            url = "StockItems.aspx?" + MenuHtml.d;

                            popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                            ",statusbar=no,menubar=no,resizable=0,width=1000," +
                            "height=800,left = 200,top = 100");
                            popUpObj.focus();
                           
                        },
                        error: function (MenuHtml) {
                          
                        }
                    });

                    //url = "StockItems.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=' + Category + '&str=' + Store;
                    //popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=yes,location=no" + ",statusbar=no,menubar=no,resizable=0,width=1000," + "height=600,left = 200,top = 100");
                    //popUpObj.focus();
                }
            }
            catch (e) {
                alert(e);
            }

        }
        function ClientValidate(source, arguments) {
            try {
                var arr2 = arguments.Value.split('/');
                if (arr2.length !== 3) {
                    arguments.IsValid = false;
                    return;
                }

                var enteredDay = parseInt(arr2[0], 10);
                var enteredMonth = parseInt(arr2[1], 10);
                var enteredYear = parseInt(arr2[2], 10);

                // Build entered date (month is 0-based in JS Date)
                var enteredDate = new Date(enteredYear, enteredMonth - 1, enteredDay);

                // Today's date (time stripped)
                var today = new Date();
                today = new Date(today.getFullYear(), today.getMonth(), today.getDate());

                // ── 1. Block future dates ──────────────────────────────────────────────
                if (enteredDate > today) {
                    source.errormessage = "Future date not allowed.";
                    arguments.IsValid = false;
                    return;
                }

                // ── 2. Determine current financial year start (April 1) ───────────────
                //    Indian FY: Apr 1 of current year  →  Mar 31 of next year
                var todayMonth = today.getMonth() + 1;   // 1-based
                var fyStartYear = (todayMonth >= 4) ? today.getFullYear() : today.getFullYear() - 1;
                var fyStart = new Date(fyStartYear, 3, 1);   // April 1  (month index 3)

                // ── 3. Block dates before the current financial year ──────────────────
                if (enteredDate < fyStart) {
                    source.errormessage = "Backdated entry not allowed before the current financial year (from " +
                        "01/04/" + fyStartYear + ").";
                    arguments.IsValid = false;
                    return;
                }

                arguments.IsValid = true;
            }
            catch (e) {
                alert(e);
            }
        }
    </script>
    </form>
</body>
</html>
