<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>




</head>

<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="users">Settings</div>
                    </div>
                    <%--  <div class="user_bg">
                        <div class="user1">Settings</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                            <div id="divAdd" runat="server" style="">
                                <div class="divL" style="width: 250px;">
                                    <ul class="contentul" style="width: 250px;">
                                        <asp:TreeView ID="tvSettings" runat="server" CollapseImageUrl="~/images/down2.png" ExpandImageUrl="~/images/side2.png" LeafNodeStyle-ImageUrl="~/images/side2.png" Style="border-radius: 14px;" BorderStyle="Solid" OnSelectedNodeChanged="tvSettings_SelectedNodeChanged" ExpandDepth="0" BackColor="#3a7277">
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#88c01d" />
                                            <Nodes>
                                                <asp:TreeNode Text="Security" Value="SEC"></asp:TreeNode>

                                                <asp:TreeNode Text="General" Value="GEN"></asp:TreeNode>
                                                <asp:TreeNode Text="Masters" Value="MAS"></asp:TreeNode>
                                                <asp:TreeNode Text="WebPharma" Value="INV">
                                                    <asp:TreeNode Text="Goods Receipt" Value="GRN"></asp:TreeNode>
                                                    <%-- <asp:TreeNode Text="Goods Issue" Value="GDI" ></asp:TreeNode>--%>
                                                    <%--<asp:TreeNode Text="Item Conversion" Value="ICN"></asp:TreeNode>--%>
                                                    <asp:TreeNode Text="Stock" Value="STK"></asp:TreeNode>
                                                    <asp:TreeNode Text="Sales" Value="SLS"></asp:TreeNode>
                                                    <asp:TreeNode Text="Purchase" Value="PUR"></asp:TreeNode>
                                                    <asp:TreeNode Text="POS" Value="POS"></asp:TreeNode>
                                                    <asp:TreeNode Text="WeighMachineBarcode" Value="WMB"></asp:TreeNode>
                                                    <%--<asp:TreeNode Text="Sales Return" Value="SRT"></asp:TreeNode>--%>
                                                </asp:TreeNode>
                                                <asp:TreeNode Text="Accounts" Value="ACC"></asp:TreeNode>
                                            </Nodes>
                                            <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="White" HorizontalPadding="5px" Font-Bold="true" NodeSpacing="3px" VerticalPadding="0px" />
                                            <ParentNodeStyle Font-Bold="False" />
                                            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#B7D1E0" />
                                        </asp:TreeView>

                                    </ul>

                                </div>

                                <div class="divL" style="width: 65%;">
                                    <ul class="contentul">
                                        <asp:MultiView ID="mvSettings" runat="server">
                                            <asp:View ID="vwSEC" runat="server">
                                                <div style="float: left; direction: ltr;">
                                                    <ul class="contentul">
                                                        <li>
                                                            <asp:Label ID="Label1" runat="server" Text="Min Password Length" CssClass="label" Style="width: 200px"></asp:Label>
                                                            <asp:TextBox ID="txtMinPwdLen" runat="server" CssClass="textboxMandatory" MaxLength="2" onkeydown="return (event.keyCode!=13);" TabIndex="1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required field" ControlToValidate="txtMinPwdLen" ForeColor="Red" Width="140px" SetFocusOnError="True" ValidationGroup="sec" Display="None"></asp:RequiredFieldValidator>
                                                            <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtMinPwdLen">
                                                            </cc:FilteredTextBoxExtender>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                            </cc:ValidatorCalloutExtender>
                                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Max length 18 and Min length is 1" ForeColor="Red" Type="Integer" ControlToValidate="txtMinPwdLen" MinimumValue="1" MaximumValue="18" ValidationGroup="sec" Display="None"></asp:RangeValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" Enabled="True" TargetControlID="RangeValidator4">
                                                            </cc:ValidatorCalloutExtender>
                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label2" runat="server" Text="Max Password Length" CssClass="label" Style="width: 200px"></asp:Label>
                                                            <asp:TextBox ID="txtMaxPwdLen" runat="server" CssClass="textboxMandatory" MaxLength="2" onkeydown="return (event.keyCode!=13);" TabIndex="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required field" ControlToValidate="txtMaxPwdLen" ForeColor="Red" Width="140px" SetFocusOnError="True" ValidationGroup="sec" Display="None"></asp:RequiredFieldValidator>
                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Min pwd should be less than max pwd" ControlToCompare="txtMinPwdLen" ControlToValidate="txtMaxPwdLen" Display="None" Operator="GreaterThanEqual" ValidationGroup="sec" SetFocusOnError="True" Type="Integer" Font-Bold="False" ForeColor="Red" ToolTip="Min pwd should be less than max pwd"></asp:CompareValidator>
                                                            <cc:FilteredTextBoxExtender ID="TextBox2_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtMaxPwdLen">
                                                            </cc:FilteredTextBoxExtender>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                                            </cc:ValidatorCalloutExtender>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                            </cc:ValidatorCalloutExtender>
                                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Max length 18 and Min length is 1" ForeColor="Red" Type="Integer" ControlToValidate="txtMaxPwdLen" MinimumValue="1" MaximumValue="18" ValidationGroup="sec" Display="None"></asp:RangeValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" Enabled="True" TargetControlID="RangeValidator3">
                                                            </cc:ValidatorCalloutExtender>
                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label3" runat="server" Text="Password Change Period" CssClass="label" Style="width: 200px"></asp:Label>
                                                            <asp:TextBox ID="txtPwdChngPeriod" runat="server" CssClass="textboxMandatory" onkeydown="return (event.keyCode!=13);" MaxLength="4" TabIndex="3"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required field" Display="None" ControlToValidate="txtPwdChngPeriod" ForeColor="Red" Width="140px" SetFocusOnError="True" ValidationGroup="sec"></asp:RequiredFieldValidator>
                                                            <cc:FilteredTextBoxExtender ID="TextBox3_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtPwdChngPeriod">
                                                            </cc:FilteredTextBoxExtender>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                                            </cc:ValidatorCalloutExtender>
                                                            <asp:RangeValidator ID="RangeValidator5" runat="server" ErrorMessage="Max value is 9999 and Min Value is 1" ForeColor="Red" Type="Integer" ControlToValidate="txtPwdChngPeriod" MinimumValue="1" MaximumValue="9999" ValidationGroup="sec" Display="None"></asp:RangeValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" Enabled="True" TargetControlID="RangeValidator5">
                                                            </cc:ValidatorCalloutExtender>


                                                        </li>

                                                        <li id="LIAccountslink" runat="server" style="height: 60%; width: 95%; margin-left: 37.5%; display: none">
                                                            <asp:CheckBox ID="chkAcountlnking" runat="server" CssClass="chckbox" Text="Accounts Linking" TabIndex="4" onchange="chkactive();" />
                                                        </li>


                                                        <li id="lidbname" runat="server" style="height: 60%; width: 95%; display: none">
                                                            <asp:Label ID="Label31" runat="server" Text="Database Name" CssClass="label" Style="width: 200px"></asp:Label>
                                                            <asp:TextBox ID="txtdatabase" runat="server" CssClass="textbox" MaxLength="150" TabIndex="5"></asp:TextBox>
                                                        </li>


                                                        <li id="licustomertype" runat="server" style="height: 60%; width: 95%; display: none">
                                                            <asp:Label ID="lblCustomertype" runat="server" CssClass="label" Text="Customer Type"></asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlcustomertype" CssClass="dropdown" TabIndex="5" Width="175px" Style="margin-left: 13%">
                                                                <asp:ListItem Text="Select" Value="0" />
                                                                <asp:ListItem Text="Customer" Value="1" />
                                                                <asp:ListItem Text="Share" Value="2" />
                                                                <asp:ListItem Text="A Class Share " Value="3">
                                                                </asp:ListItem>
                                                            </asp:DropDownList>

                                                        </li>
                                                        <li id="LIStckUpdation" runat="server" style="height: 60%; width: 95%; margin-left: 37.5%; display: none">
                                                            <asp:CheckBox ID="chkStockUpdation" runat="server" CssClass="chckbox" Text="Stock Updation Without Purchase" TabIndex="5"   />
                                                        </li>

                                                        <br />
                                                        <li class="btnMargin" style="margin-left: 12.5%">
                                                            <asp:Button ID="btnSubmitSEC" runat="server" Text="Submit" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="6" ValidationGroup="sec" OnClick="btnSubmitSEC_Click" />
                                                            <asp:Button ID="btnClearSEC" runat="server" CausesValidation="false" Text="Clear" CssClass="button" OnClientClick="ClearSEC(); return false;" />
                                                        </li>
                                                    </ul>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="vwSLS" runat="server">
                                                <div style="float: left; direction: ltr;">
                                                    <ul class="contentul">
                                                        <li>
                                                            <asp:Label ID="Label17" runat="server" Text="Pole Port No" CssClass="label"></asp:Label>
                                                            <asp:TextBox ID="txtPortNo" runat="server" CssClass="textbox" MaxLength="10" onkeydown="return (event.keyCode!=13);" TabIndex="1"></asp:TextBox>
                                                            <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtPortNo">
                                                            </cc:FilteredTextBoxExtender>


                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label18" runat="server" Text="BillPrint (No of Copies)" CssClass="label"></asp:Label>
                                                            <asp:TextBox ID="TxtBillPrint" runat="server" CssClass="textboxMandatory" MaxLength="2" onkeydown="return (event.keyCode!=13);" TabIndex="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Required field" ControlToValidate="TxtBillPrint" ForeColor="Red" Width="140px" SetFocusOnError="True" ValidationGroup="sls" Display="None"></asp:RequiredFieldValidator>
                                                            <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="TxtBillPrint">
                                                            </cc:FilteredTextBoxExtender>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                                            </cc:ValidatorCalloutExtender>
                                                            <asp:RangeValidator ID="RangeValidator6" runat="server" ErrorMessage="Max 99 and Min 1" ForeColor="Red" Type="Integer" ControlToValidate="TxtBillPrint" MinimumValue="1" MaximumValue="99" ValidationGroup="sls" Display="None"></asp:RangeValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server" Enabled="True" TargetControlID="RangeValidator6">
                                                            </cc:ValidatorCalloutExtender>
                                                        </li>


                                                        <li class="btnMargin" style="padding-left: -250px">
                                                            <asp:Label ID="Label22" runat="server" Text="Sales date changes " CssClass="label" Style="margin-left: -136px;"></asp:Label>
                                                            <MKB:TimeSelector ID="TmsSales" runat="server" AllowSecondEditing="True" ClientIDMode="Inherit" BorderStyle="Solid" BackColor="White" BorderColor="White"></MKB:TimeSelector>
                                                        </li>


                                                        <%-- <li style="width: 100%; height: 19px; background-color: #D8EBF6; font-weight: bold; font-size: 14px; color: #0C3C64;">Remarks In Bill</li>--%>
                                                        <li class="clear"></li>
                                                        <li class="btnMargin" style="display: none;">
                                                            <asp:CheckBox ID="cbxSalesRemark" runat="server" Text="Show Remarks In Bill" />
                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label19" runat="server" Text="Seasonal greetings" CssClass="label"></asp:Label>
                                                            <asp:TextBox ID="txtSeasonalGreetings" CssClass="textboxLong" MaxLength="50" runat="server" />
                                                        </li>

                                                        <li>
                                                            <asp:Label ID="Label32" runat="server" Text="Sales Account Posting" CssClass="label"></asp:Label>

                                                            <asp:DropDownList runat="server" ID="ddlSalesAccountPosting" CssClass="dropdownMandatory" TabIndex="5" Width="175px" Style="margin-left: 0%">
                                                                <asp:ListItem Text="All Sales" Value="1" />
                                                                <asp:ListItem Text="Counter wise " Value="2">
                                          
                                                                </asp:ListItem>
                                                            </asp:DropDownList>


                                                        </li>


                                                        <li class="btnMargin" style="padding-left: 130px">
                                                            <asp:CheckBox ID="chksalediscountaccount" runat="server" CssClass="chckbox" Text="Sale Discount Accounting" TabIndex="1" onchange="javascript:GeCheckedOne();" Font-Size="Small" />


                                                        </li>
                                                        <li class="btnMargin">
                                                            <asp:Button ID="btnSubmitSLS" runat="server" Text="Submit" CssClass="button" meta:resourcekey="btnSubmit" ValidationGroup="sls" OnClick="btnSubmitSLS_Click" />
                                                            <asp:Button ID="btnClearSLS" runat="server" CausesValidation="false" Text="Clear" CssClass="button" meta:resourcekey="btnClear" OnClientClick="ClearSLS(); return false;" />

                                                        </li>
                                                    </ul>
                                                </div>
                                            </asp:View>

                                            <asp:View ID="vwINV" runat="server">
                                                <div style="float: left; direction: ltr;">
                                                    <ul class="contentul">
                                                        <li>
                                                            <asp:Label ID="Label4" runat="server" Text="Rounding" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlRoundingINV" runat="server" CssClass="dropdown" TabIndex="1">
                                                            </asp:DropDownList>
                                                        </li>
                                                        <br />
                                                        <li>
                                                            <asp:Label ID="Label10" runat="server" Text="Costing Method" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlCostingMethod" runat="server" CssClass="dropdownMandatory" TabIndex="1">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" Style="float: left;" runat="server" ErrorMessage="Required field" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" SetFocusOnError="True" ToolTip="Required field" ValidationGroup="grcpt" ControlToValidate="ddlCostingMethod" InitialValue="0">*</asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator12">
                                                            </cc:ValidatorCalloutExtender>
                                                        </li>
                                                        <br />
                                                        <li class="btnMargin">
                                                            <asp:HiddenField ID="hfINVSubModule" runat="server" />

                                                            <asp:Button ID="btnSubmitINV" runat="server" Text="Submit" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="2" OnClick="btnSubmitINV_Click" ValidationGroup="grcpt" />
                                                            <asp:Button ID="btnClearINV" runat="server" Text="Clear" CausesValidation="false" CssClass="button" meta:resourcekey="btnClear" OnClientClick="ClearINV(); return false;" />
                                                        </li>
                                                    </ul>
                                                </div>
                                            </asp:View>

                                            <asp:View ID="vwSTK" runat="server">
                                                <div style="float: left; direction: ltr; width: 100%;">
                                                    <ul class="contentul">
                                                        <li>
                                                            <asp:Label ID="Label7" runat="server" Text="Stock Code Generation" CssClass="label"></asp:Label>
                                                            <asp:CheckBoxList ID="chklstSTKCodeGen" runat="server" TabIndex="1" CssClass="chckbox" Style="padding-right: 0px;">
                                                                <asp:ListItem Value="1">Item Wise</asp:ListItem>
                                                                <asp:ListItem Value="2">Sales Rate</asp:ListItem>
                                                                <asp:ListItem Value="3">BatchNo</asp:ListItem>
                                                                <asp:ListItem Value="4">Unit</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                        </li>
                                                        <br />
                                                        <li class="btnMargin">
                                                            <asp:Button ID="btnSubmitSTK" runat="server" Text="Submit" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="2" OnClick="btnSubmitSTK_Click" />
                                                            <asp:Button ID="btnClearSTK" CausesValidation="false" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClear" OnClientClick="ClearCheckList(); return false;" />
                                                        </li>
                                                    </ul>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="vwPUR" runat="server">
                                                <div style="float: left; direction: ltr;">
                                                    <ul class="contentul">
                                                        <%-- <li>--%>

                                                        <asp:Label ID="Label23" runat="server" CssClass="label" Text="Discount (%)"></asp:Label>
                                                        <li class="btnMargin" style="padding-left: 180px">
                                                            <asp:RadioButton ID="rbtnPrePurch" runat="server" Text="From Previous Purchase" GroupName="a" TabIndex="1" />
                                                            <asp:RadioButton ID="rbtnCategory" runat="server" Text="From Category " GroupName="a" TabIndex="2" />
                                                        </li>
                                                        <li class="btnMargin" style="padding-left: 180px">
                                                            <asp:CheckBox ID="chkpurhasediscountaccount" runat="server" CssClass="chckbox" Text="Purchase Discount Accounting" TabIndex="1" onchange="javascript:GeCheckedOne();" Font-Size="Small" />


                                                        </li>
                                                        <li class="btnMargin" style="padding-left: 180px">
                                                            <asp:CheckBox ID="chkcnvertigst" runat="server" CssClass="chckbox" Text="Split IGST To CGST And SGST" TabIndex="1" onchange="javascript:GeCheckedOne();" Font-Size="Small" />


                                                        </li>

                                                        <br />
                                                        <li class="btnMargin">
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />

                                                            <asp:Button ID="btnSubmitPurchase" runat="server" Text="Submit" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="2" OnClick="btnSubmitPurchase_Click" />
                                                        </li>
                                                    </ul>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="vwPOS" runat="server">
                                                <div style="float: left; direction: ltr; width: 100%;">
                                                    <ul class="contentul">
                                                        <li>
                                                            <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold;">POS Settings</li>

                                                        </li>
                                                        <br />
                                                        <li class="btnMargin" style="padding-left: 180px">
                                                            <asp:CheckBox ID="chkDisableRoundOff" runat="server" CssClass="chckbox" Text="Disable RoundOff" TabIndex="1" onchange="javascript:GeCheckedOne();" />


                                                        </li>
                                                        <li class="btnMargin" style="padding-left: 180px">
                                                            <asp:CheckBox ID="chkDsblNegstk" runat="server" CssClass="chckbox" Text="Disable Negative Stock" TabIndex="2" onchange="javascript: ShowTextBox();" />
                                                        </li>
                                                        <li class="btnMargin" style="padding-left: 180px">
                                                            <asp:CheckBox ID="chkDateEnable" runat="server" CssClass="chckbox" Text="Enable Date" TabIndex="3" onchange="javascript: ShowTextBox();" />
                                                        </li>
                                                        <li class="btnMargin" style="padding-left: 180px">
                                                            <asp:CheckBox ID="chkPosDiscEnable" runat="server" CssClass="chckbox" Text="Enable POS Discount" TabIndex="4" onchange="javascript: ShowTextBox();" />
                                                        </li>
                                                        <li class="btnMargin" style="padding-left: 180px">
                                                            <asp:CheckBox ID="chkCustBarcode" runat="server" CssClass="chckbox" Text="Costumer selection through Barcode" TabIndex="5" onchange="javascript: ShowTextBox();" />
                                                        </li>
                                                        <li class="btnMargin" style="padding-left: 180px">
                                                            <asp:CheckBox ID="chkRecoveryEnable" runat="server" CssClass="chckbox" Text="Enable Recovery" TabIndex="3" onchange="javascript: ShowTextBox();" />
                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label20" runat="server" Text="Barcode Split" CssClass="label" Style="width: 180px"></asp:Label>
                                                            <asp:TextBox ID="txtBarcodeSplit1" runat="server" CssClass="textbox" MaxLength="1" Width="20px" onkeydown="return (event.keyCode!=13);" TabIndex="6"></asp:TextBox>
                                                            <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtBarcodeSplit1">
                                                            </cc:FilteredTextBoxExtender>
                                                            <asp:RangeValidator ID="rvBarcodeSplit1" runat="server" ControlToValidate="txtBarcodeSplit1" ErrorMessage="Enter only 0 or 1" Display="None" MaximumValue="1" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                                            <cc:ValidatorCalloutExtender ID="vceBarcodeSplit1" runat="server" Enabled="True" TargetControlID="rvBarcodeSplit1">
                                                            </cc:ValidatorCalloutExtender>
                                                            <asp:TextBox ID="txtBarcodeSplit2" runat="server" CssClass="textbox" MaxLength="2" Width="20px" onkeydown="return (event.keyCode!=13);" TabIndex="7"></asp:TextBox>
                                                            <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtBarcodeSplit2">
                                                            </cc:FilteredTextBoxExtender>
                                                            <asp:TextBox ID="txtBarcodeSplit3" runat="server" CssClass="textbox" MaxLength="2" Width="20px" onkeydown="return (event.keyCode!=13);" TabIndex="8"></asp:TextBox>
                                                            <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtBarcodeSplit3">
                                                            </cc:FilteredTextBoxExtender>
                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label21" runat="server" Text="Customer Image Path" CssClass="label" Style="width: 180px"></asp:Label>
                                                            <asp:TextBox ID="txtCustImagePath" runat="server" CssClass="textbox" TabIndex="9" Width="300px"></asp:TextBox>
                                                            <asp:Button ID="btnCustImagePath" runat="server" Text="?" TabIndex="11" ToolTip="Customer Image Path" OnClick="btnCustImagePath_Click" Style="margin-left: -2px; width: 35px;" />

                                                        </li>


                                                        <br />
                                                        <li class="btnMargin">
                                                            <asp:Button ID="btnSubmitPOS" runat="server" Text="Submit" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="2" ValidationGroup="submit" OnClick="btnSubmitPOS_Click" />
                                                            <asp:Button ID="btnClearPOS" CausesValidation="false" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClear" OnClientClick="ClearPOS(); return false;" />
                                                        </li>
                                                    </ul>
                                                    <div id="divImagePath" runat="server" class="divShadow" style="display: none; background-color: black; border-radius: 3px; width: 250px; margin-left: 375px; margin-top: -90px; border: none;">
                                                        <div style="left: 0px">
                                                            <asp:Label ID="Label27" CssClass="label" runat="server" Text="Medlife Says :" Style="width: 250px; margin-right: 0px; background-color: rgb(255, 241, 193); font-family: 'Segoe Print'; font-weight: bold; color: black; height: 22px; padding-top: 3px; text-align: left; margin-top: 30px"></asp:Label>
                                                            <asp:ImageButton ID="ibtnImagePathClose" runat="server" Width="20px" Height="20px" TabIndex="18" Style="float: right; margin-top: -21px;" ToolTip="Close" ImageUrl="~/images/cross.png" OnClick="ibtnImagePathClose_Click" />
                                                        </div>
                                                        <div style="left: 0px">
                                                            <asp:Label ID="Label28" CssClass="label" runat="server" Text="Customer Image Path ,(for eg: http://192.168.1.100/EASYSHOPPLUS)"
                                                                Style="width: 250px; margin-right: 0px; background-color: rgb(255, 241, 193); color: black; font-family: 'Segoe Print'; font-size: 11px; height: 40px; padding-top: 3px; text-align: left; margin-top: 0px"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:View>

                                            <asp:View ID="vwWMB" runat="server">
                                                <div style="float: left; direction: ltr; width: 100%;">
                                                    <ul class="contentul">
                                                        <li>
                                                            <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold;">WeighMachineBarcode Settings</li>

                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label25" runat="server" Text="Barcode Location Path" CssClass="label" Style="width: 180px"></asp:Label>
                                                            <asp:TextBox ID="txtBarcodeLocationPath" runat="server" CssClass="textbox" TabIndex="9" Width="300px"></asp:TextBox>
                                                            <asp:Button ID="btnBarcodeLoc" runat="server" Text="?" TabIndex="11" ToolTip="Barcode Location" OnClick="btnBarcodeLoc_Click" Style="margin-left: -2px; width: 35px;" />
                                                        </li>


                                                        <br />
                                                        <li class="btnMargin">
                                                            <asp:Button ID="btnSubmitWeighMachineBarcode" runat="server" Text="Submit" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="2" OnClick="btnSubmitWeighMachineBarcode_Click" />
                                                            <asp:Button ID="btnClearWeighMachineBarcode" CausesValidation="false" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClear" OnClientClick="ClearWeighMachineBarcode(); return false;" />
                                                        </li>
                                                    </ul>
                                                    <div id="divBarcodeLocation" runat="server" class="divShadow" style="display: none; background-color: black; border-radius: 3px; width: 250px; margin-left: 375px; margin-top: -90px; border: none;">
                                                        <div style="left: 0px">
                                                            <asp:Label ID="Label24" CssClass="label" runat="server" Text="Medlife Says :" Style="width: 250px; margin-right: 0px; background-color: rgb(255, 241, 193); font-family: 'Segoe Print'; font-weight: bold; color: black; height: 22px; padding-top: 3px; text-align: left; margin-top: 30px"></asp:Label>
                                                            <asp:ImageButton ID="ibtnBarcodeLocClose" runat="server" Width="20px" Height="20px" TabIndex="18" Style="float: right; margin-top: -21px;" ToolTip="Close" ImageUrl="~/images/cross.png" OnClick="ibtnBarcodeLocClose_Click" />
                                                        </div>
                                                        <div style="left: 0px">
                                                            <asp:Label ID="Label26" CssClass="label" runat="server" Text="Barcode Location Path ,(for eg: D://MyDocuments//) ,Generated only in published machine!"
                                                                Style="width: 250px; margin-right: 0px; background-color: rgb(255, 241, 193); color: black; font-family: 'Segoe Print'; font-size: 11px; height: 70px; padding-top: 3px; text-align: left; margin-top: 0px"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:View>

                                            <asp:View ID="vwACC" runat="server">
                                                <div style="float: left; direction: ltr; width: 100%;">
                                                    <ul class="contentul">
                                                        <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold; font-size: 14px; color: #0C3C64;">Account Code Generation</li>
                                                        <br />
                                                        <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold;">Sales Account</li>
                                                        <li>

                                                            <asp:Label ID="Label8" runat="server" Text="Account Head" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlSalesAccountHead" runat="server" onblur="BlurSale();" CssClass="dropdownMandatory" TabIndex="1">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Style="float: left;" runat="server" ErrorMessage="Required field" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" SetFocusOnError="True" ToolTip="Required field" ValidationGroup="acc" ControlToValidate="ddlSalesAccountHead" InitialValue="0">*</asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                                            </cc:ValidatorCalloutExtender>
                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label9" runat="server" Text="Account SubHead" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlSalesAccountSubHead" runat="server" CssClass="dropdown" TabIndex="2">
                                                            </asp:DropDownList>

                                                        </li>
                                                        <br />
                                                        <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold;">Purchase Account</li>
                                                        <li>
                                                            <asp:Label ID="Label11" runat="server" Text="Account Head" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlPurchaseAccountHead" runat="server" onblur="BlurPurchase();" CssClass="dropdownMandatory" TabIndex="3">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Style="float: left;" runat="server" ErrorMessage="Required field" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" SetFocusOnError="True" ToolTip="Required field" ValidationGroup="acc" ControlToValidate="ddlPurchaseAccountHead" InitialValue="0">*</asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                                            </cc:ValidatorCalloutExtender>
                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label12" runat="server" Text="Account SubHead" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlPurchaseAccountSubHead" runat="server" CssClass="dropdown" TabIndex="4">
                                                            </asp:DropDownList>

                                                        </li>
                                                        <br />
                                                        <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold;">Cash Account</li>
                                                        <li>
                                                            <asp:Label ID="Label13" runat="server" Text="Account Head" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlCashAccountHead" runat="server" onblur="BlurCash();" CssClass="dropdownMandatory" TabIndex="5">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Style="float: left;" runat="server" ErrorMessage="Required field" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" SetFocusOnError="True" ToolTip="Required field" ValidationGroup="acc" ControlToValidate="ddlCashAccountHead" InitialValue="0">*</asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                                            </cc:ValidatorCalloutExtender>
                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label14" runat="server" Text="Account SubHead" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlCashAccountSubHead" runat="server" CssClass="dropdown" TabIndex="6">
                                                            </asp:DropDownList>
                                                        </li>
                                                        <br />
                                                        <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold;">Card Account</li>
                                                        <li>
                                                            <asp:Label ID="Label15" runat="server" Text="Account Head" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlCardAccountHead" runat="server" onblur="BlurCard();" CssClass="dropdownMandatory" TabIndex="7">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" Style="float: left;" runat="server" ErrorMessage="Required field" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" SetFocusOnError="True" ToolTip="Required field" ValidationGroup="acc" ControlToValidate="ddlCardAccountHead" InitialValue="0">*</asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator11">
                                                            </cc:ValidatorCalloutExtender>
                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label16" runat="server" Text="Account SubHead" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlCardAccountSubHead" runat="server" CssClass="dropdown" TabIndex="8">
                                                            </asp:DropDownList>
                                                        </li>
                                                        <br />
                                                        <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold;">Cheque Account</li>
                                                        <li>
                                                            <asp:Label ID="Label5" runat="server" Text="Account Head" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlChequeAccountHead" runat="server" onblur="BlurCheque();" CssClass="dropdownMandatory" TabIndex="7">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Style="float: left;" runat="server" ErrorMessage="Required field" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" SetFocusOnError="True" ToolTip="Required field" ValidationGroup="acc" ControlToValidate="ddlCardAccountHead" InitialValue="0">*</asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator11">
                                                            </cc:ValidatorCalloutExtender>
                                                        </li>
                                                        <li>
                                                            <asp:Label ID="Label6" runat="server" Text="Account SubHead" CssClass="label"></asp:Label>
                                                            <asp:DropDownList ID="ddlChequeAccountSubHead" runat="server" CssClass="dropdown" TabIndex="8">
                                                            </asp:DropDownList>
                                                        </li>




                                                        <br />

                                                        <br />
                                                        <li class="btnMargin">
                                                            <asp:Button ID="btnAccSubmit" runat="server" Text="Submit" ValidationGroup="acc" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="12" OnClick="btnAccSubmit_Click" />
                                                            <asp:Button ID="btnAccClose" CausesValidation="false" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClear" OnClientClick="ClearACC(); return false;" />
                                                        </li>
                                                    </ul>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="vwTemp" runat="server">
                                                <div style="float: right; direction: rtl; width: 100%;">
                                                </div>
                                            </asp:View>
                                            <asp:View ID="vwGEN" runat="server">
                                                <div style="float: left; direction: ltr; width: 100%;">
                                                    <ul class="contentul">

                                                        <li>
                                                            <li style="width: 100%; background-color: #49BFC3; color: white; font-weight: bold;">Barcode Generation</li>

                                                        </li>
                                                        <li class="btnMargin">
                                                            <asp:CheckBox ID="chkshortName" runat="server" CssClass="chckbox" Text="Item Short Name" onchange="javascript:GeCheckedOne();" />


                                                        </li>
                                                        <li class="btnMargin">
                                                            <asp:CheckBox ID="chkCustom" runat="server" CssClass="chckbox" Text="User Defined Code" onchange="javascript: ShowTextBox();" />
                                                        </li>
                                                        <li id="licustom" style="display: none" class="btnMargin">

                                                            <asp:TextBox runat="server" ID="txtCustom" CssClass="textboxMandatory" MaxLength="10" />
                                                            <asp:RequiredFieldValidator ID="reqvalCustom" runat="server" ErrorMessage="Barcode required" ControlToValidate="txtCustom" Display="None" ValidationGroup="Gen" Enabled="false"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" Enabled="True" TargetControlID="reqvalCustom">
                                                            </cc:ValidatorCalloutExtender>
                                                        </li>
                                                        <li class="clear"></li>
                                                        <li style="width: 100%; background-color: #49BFC3; color: white; font-weight: bold;">Barcode Settings</li>
                                                        <li class="clear"></li>
                                                        <li>
                                                            <label class="label">Page Height</label>
                                                            <asp:TextBox ID="txtPageHeight" runat="server" CssClass="textbox" MaxLength="15" onkeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                                        </li>
                                                        <li>
                                                            <label class="label">Page  Width</label>
                                                            <asp:TextBox ID="txtPageWidth" runat="server" CssClass="textbox" MaxLength="15" onkeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                                        </li>
                                                        <li>
                                                            <label class="label">Vertical Numbers</label>
                                                            <asp:TextBox ID="txtVerticalNos" runat="server" CssClass="textbox" MaxLength="15" onkeypress="CheckInteger(event);"></asp:TextBox>
                                                        </li>
                                                        <li>
                                                            <label class="label">Horizontal Numbers</label>
                                                            <asp:TextBox ID="txtHorizontalNos" runat="server" CssClass="textbox" MaxLength="15" onkeypress="CheckInteger(event);"></asp:TextBox>
                                                        </li>
                                                        <li>

                                                            <asp:Label ID="Label186" runat="server" CssClass="label" Text="Print Method"></asp:Label>
                                                            <asp:RadioButton ID="rdHtmlPrint" runat="server" Text="HTML Print " GroupName="a" TabIndex="15" />
                                                            <asp:RadioButton ID="rdtxtPrint" runat="server" Text="txt Print" GroupName="a" TabIndex="16" />

                                                        </li>
                                                        <li style="display: none;"><%--style="display:none;"--%>
                                                            <label class="label">Height</label>
                                                            <asp:TextBox ID="txtHeight" runat="server" CssClass="textbox" MaxLength="15" onkeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                                            <label>Pixel</label>
                                                        </li>
                                                        <li style="display: none;"><%--style="display:none;"--%>
                                                            <label class="label">Width</label>
                                                            <asp:TextBox ID="txtWidth" runat="server" CssClass="textbox" MaxLength="15" onkeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                                            <label>Pixel</label>
                                                        </li>
                                                        <li style="display: none;"><%--style="display:none;"--%>
                                                            <label class="label">Margin</label>
                                                            <asp:TextBox ID="txtMargin" runat="server" CssClass="textbox" MaxLength="25" onkeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                                            <label>Pixel</label>
                                                        </li>
                                                        <li class="clear"></li>
                                                        <li>
                                                            <li style="width: 100%; background-color: #49BFC3; color: white; font-weight: bold;">Active Modules</li>

                                                        </li>
                                                        <li class="btnMargin">
                                                            <asp:CheckBox ID="chkPO" runat="server" CssClass="chckbox" TabIndex="13" Text="Purchase Order" onchange="javascript:PurchaseOrderCbxChange();" />
                                                        </li>
                                                        <li class="btnMargin">
                                                            <asp:CheckBox ID="chkMR" runat="server" CssClass="chckbox" TabIndex="14" onchange="MaterialChanged();" Text="Material Receipt" />
                                                        </li>

                                                        <li class="clear"></li>
                                                        <li>
                                                            <li style="width: 100%; background-color: #49BFC3; color: white; font-weight: bold;">Financial year</li>

                                                        </li>
                                                        <li>

                                                            <asp:TextBox ID="txtFromDateFinYear" runat="server" CssClass="textboxMandatory" MaxLength="15" onkeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                                            <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFromDateFinYear" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="From Date required" ControlToValidate="txtFromDateFinYear" Display="None" SetFocusOnError="True" ValidationGroup="Gen"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                                            </cc:ValidatorCalloutExtender>
                                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="To Date should be greater than From date" ControlToCompare="txtFromDateFinYear" ControlToValidate="txtToDateFinYear" Display="None" Operator="GreaterThan" ValidationGroup="Gen" SetFocusOnError="True" Type="Date" Font-Bold="False" ForeColor="Red" ToolTip="From Date should be less than to date"></asp:CompareValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server" Enabled="True" TargetControlID="CompareValidator2">
                                                            </cc:ValidatorCalloutExtender>

                                                            <label>To</label>

                                                            <asp:TextBox ID="txtToDateFinYear" runat="server" CssClass="textboxMandatory" MaxLength="15" onkeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                                            <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtToDateFinYear" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="To Date required" ControlToValidate="txtToDateFinYear" Display="None" SetFocusOnError="True" ValidationGroup="Gen"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                                            </cc:ValidatorCalloutExtender>

                                                        </li>
                                                        <li style="width: 100%; background-color: #49BFC3; color: white; font-weight: bold;">Time Zone</li>
                                                        <li class="clear"></li>
                                                        <li>
                                                            <label class="label">Minutes</label>
                                                            <asp:TextBox ID="txtMinutes" runat="server" CssClass="textboxMandatory" MaxLength="5" TabIndex="16" onkeypress="CheckIntegerAndNeg(event);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Minutes required" ControlToValidate="txtMinutes" Display="None" SetFocusOnError="True" ValidationGroup="Gen"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                                            </cc:ValidatorCalloutExtender>
                                                            <asp:RangeValidator ID="rvMinutes" runat="server" ErrorMessage="Minutes should be in between -1440 to 1440" ForeColor="Red" Type="Integer" ControlToValidate="txtMinutes" MinimumValue="-1440" MaximumValue="1440" ValidationGroup="Gen" Display="None"></asp:RangeValidator>
                                                            <cc:ValidatorCalloutExtender ID="vceMinutes" runat="server" Enabled="True" TargetControlID="rvMinutes"></cc:ValidatorCalloutExtender>
                                                            <asp:Button ID="btnTimeZone" runat="server" Text="?" TabIndex="17" ToolTip="Time Zone" Style="margin-left: -2px; width: 35px;" OnClientClick="ShowTimezone(); return false;" />
                                                        </li>
                                                        <li class="clear"></li>
                                                        <li style="width: 100%; background-color: #49BFC3; color: white; font-weight: bold;">Monthly Process</li>
                                                        <li class="clear"></li>
                                                        <li>
                                                            <label class="label">Grace Period (Days)</label>
                                                            <asp:TextBox ID="txtMonthlyProcessGracePeriod" runat="server" CssClass="textboxMandatory" MaxLength="4" TabIndex="18" onkeypress="CheckInteger(event);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvMonthlyProcessGracePeriod" runat="server" ErrorMessage="Monthly Process Grace Period required" ControlToValidate="txtMonthlyProcessGracePeriod" Display="None" SetFocusOnError="True" ValidationGroup="Gen"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="vceMonthlyProcessGracePeriodReq" runat="server" Enabled="True" TargetControlID="rfvMonthlyProcessGracePeriod">
                                                            </cc:ValidatorCalloutExtender>
                                                            <cc:FilteredTextBoxExtender ID="fteMonthlyProcessGracePeriod" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtMonthlyProcessGracePeriod">
                                                            </cc:FilteredTextBoxExtender>
                                                            <asp:RangeValidator ID="rvMonthlyProcessGracePeriod" runat="server" ErrorMessage="Grace Period must be a valid non-negative number (0 to 9999)" ForeColor="Red" Type="Integer" ControlToValidate="txtMonthlyProcessGracePeriod" MinimumValue="0" MaximumValue="9999" ValidationGroup="Gen" Display="None"></asp:RangeValidator>
                                                            <cc:ValidatorCalloutExtender ID="vceMonthlyProcessGracePeriod" runat="server" Enabled="True" TargetControlID="rvMonthlyProcessGracePeriod"></cc:ValidatorCalloutExtender>
                                                        </li>
                                                        <br />
                                                        <li class="btnMargin">
                                                            <asp:Button ID="btnSubmitGen" runat="server" Text="Submit" ValidationGroup="Gen" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="19" OnClick="btnSubmitGEN_Click" />
                                                            <asp:Button ID="btnClearGen" runat="server" Text="Clear" CssClass="button" OnClientClick="javascript:return ClearGEN();" CausesValidation="false" />
                                                        </li>
                                                        <li class="clear"></li>

                                                        <div id="divTimeDescription" runat="server" class="divShadow" style="display: none; background-color: black; border-radius: 3px; width: 250px; margin-left: 375px; border: none; margin-top: -180px;">
                                                            <div style="left: 0px">
                                                                <asp:Label ID="Label29" CssClass="label" runat="server" Text="Medlife Says :" Style="width: 250px; margin-right: 0px; background-color: rgb(255, 241, 193); font-family: 'Segoe Print'; font-weight: bold; color: black; height: 22px; padding-top: 3px; text-align: left; margin-top: 30px"></asp:Label>
                                                                <asp:ImageButton ID="ibtndescription" runat="server" Width="20px" Height="20px" TabIndex="18" Style="float: right; margin-top: -21px;" ToolTip="Close" ImageUrl="~/images/cross.png" OnClientClick="CloseTimezone(); return false;" />
                                                            </div>
                                                            <div style="left: 0px">
                                                                <asp:Label ID="Label30" CssClass="label" runat="server" Text="Enter Time (in Minutes) between -1440 to 1440"
                                                                    Style="width: 250px; margin-right: 0px; background-color: rgb(255, 241, 193); color: black; font-family: 'Segoe Print'; font-size: 11px; height: 40px; padding-top: 3px; text-align: left; margin-top: 0px"></asp:Label>

                                                            </div>
                                                        </div>





                                                        <li class="clear"></li>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="vwMAS" runat="server">
                                                <div style="float: left; direction: ltr; width: 100%;">
                                                    <ul class="contentul">
                                                        <li>
                                                            <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold;">Company Name</li>

                                                        </li>

                                                        <br />
                                                        <li class="btnMargin">
                                                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="textboxMandatory" MaxLength="150" />
                                                            <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ErrorMessage="Customer Short name required"
                                                                ControlToValidate="txtCompanyName" Display="None" ValidationGroup="MAS" Enabled="True"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="vceCompanyName" runat="server" Enabled="True" TargetControlID="rfvCompanyName">
                                                            </cc:ValidatorCalloutExtender>

                                                        </li>
                                                        <li>
                                                            <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold;">Customer Short name Format</li>

                                                        </li>

                                                        <br />
                                                        <li class="btnMargin">
                                                            <asp:TextBox ID="txtCustomerShortname" runat="server" CssClass="textboxMandatory" MaxLength="10" />
                                                            <asp:RequiredFieldValidator ID="rfvCustomerShortname" runat="server" ErrorMessage="Customer Short name required"
                                                                ControlToValidate="txtCustomerShortname" Display="None" ValidationGroup="MAS" Enabled="True"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="rfvCustomerShortname">
                                                            </cc:ValidatorCalloutExtender>

                                                        </li>
                                                        <li>
                                                            <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold;">Supplier Short name Format</li>

                                                        </li>
                                                        <li class="btnMargin">
                                                            <asp:TextBox ID="txtSupplierShortName" runat="server" CssClass="textboxMandatory" MaxLength="11" />
                                                            <asp:RequiredFieldValidator ID="rfvSuplierShort" runat="server" ErrorMessage="Supplier Short name required"
                                                                ControlToValidate="txtSupplierShortName" Display="None" ValidationGroup="MAS" Enabled="True"></asp:RequiredFieldValidator>
                                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="rfvSuplierShort">
                                                            </cc:ValidatorCalloutExtender>

                                                        </li>
                                                        <li class="clear"></li>
                                                        <br />

                                                        <br />
                                                        <li class="btnMargin">
                                                            <asp:Button ID="btnSubmitMAS" runat="server" Text="Submit" ValidationGroup="MAS" CssClass="button" meta:resourcekey="btnSubmit" OnClick="btnSubmitMAS_Click" />
                                                            <asp:Button ID="btnClearMAS" runat="server" Text="Clear" CssClass="button" OnClientClick="javascript:return ClearMAS();" CausesValidation="false" />
                                                        </li>
                                                        <li class="clear"></li>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>

                                        <li class="btnMargin">
                                            <%--  <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" OnClick="btnClear_Click" />--%>
                                            <asp:HiddenField ID="hfItemsId" runat="server" />
                                            <asp:HiddenField ID="hfUpdatePermission" runat="server" />
                                            <asp:HiddenField ID="hfAddPermission" runat="server" />
                                            <asp:HiddenField ID="hfIsAdd" runat="server" />
                                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                            <asp:HiddenField ID="hfIsDelete" runat="server" />
                                            <asp:HiddenField ID="hfClient" runat="server" />
                                            <asp:HiddenField ID="hfUser" runat="server" />


                                        </li>
                                        <li style="width: 450px; float: left;">
                                            <%--<asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server"  ValidationGroup="submit"  />--%>
                                            <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>
                                            <asp:Button ID="btnSaleBlurTrigger" Text="text" CausesValidation="false" runat="server" OnClick="btnSaleBlurTrigger_Click" Style="display: none;" />
                                            <asp:Button ID="btnPurchaseBlurTrigger" Text="text" runat="server" OnClick="btnPurchaseBlurTrigger_Click" Style="display: none;" />
                                            <asp:Button ID="btnCashBlurTrigger" Text="text" runat="server" OnClick="btnCashBlurTrigger_Click" Style="display: none;" />
                                            <asp:Button ID="btnCardBlurTrigger" Text="text" runat="server" OnClick="btnCardBlurTrigger_Click" Style="display: none;" />
                                            <asp:Button ID="btnChequeBlurTrigger" Text="text" runat="server" OnClick="btnChequeBlurTrigger_Click" Style="display: none;" />
                                        </li>



                                    </ul>
                                </div>
                            </div>
                        </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/jquery-1.10.2.js"></script>
        <script src="js/Custom.js"></script>
        <script src="js/toastr.js"></script>
        <script type="text/javascript">

            function ClearSEC() {
                $('#txtMinPwdLen').val('');
                $('#txtMaxPwdLen').val('');
                $('#txtPwdChngPeriod').val('');
                $('#txtdatabase').val('');
                $('#ddlcustomertype').val('0');
                document.getElementById("chkAcountlnking").checked = false;
                $("#lidbname").attr('style', 'display:none');
                $("#licustomertype").attr('style', 'display:none');


            }
            function ClearSLS() {
                $('#txtPortNo').val('');
                $('#TxtBillPrint').val('');
                $('#txtSeasonalGreetings').val('');
                $('#ddlSalesAccountPosting').val('1');
            }

            function ClearINV() {
                $('#ddlRoundingINV').val('0');
                $('#ddlCostingMethod').val('0');

            }
            function ClearACC() {
                $('#ddlCardAccountHead').val('0');
                $('#ddlCardAccountSubHead').val('0');
                $('#ddlCashAccountHead').val('0');
                $('#ddlCashAccountSubHead').val('0');
                $('#ddlPurchaseAccountHead').val('0');

                $('#ddlPurchaseAccountSubHead').val('0');
                $('#ddlSalesAccountHead').val('0');
                $('#ddlSalesAccountSubHead').val('0');
            }
            function ClearCheckList() {



                $('#chklstSTKCodeGen input:checkbox').removeAttr('checked');

                $('#chkSYSInvoice input:checkbox').removeAttr('checked');
                $('#chkSYSVoucher input:checkbox').removeAttr('checked');

            }
            function ClearPOS() {
                $("#chkDisableRoundOff").prop('checked', false);
                $("#chkDsblNegstk").prop('checked', false);
                $("#chkDateEnable").prop('checked', false);
                $("#chkPosDiscEnable").prop('checked', false);
                $("#chkCustBarcode").prop('checked', false);
                $('#txtBarcodeSplit1').val('');
                $('#txtBarcodeSplit2').val('');
                $('#txtBarcodeSplit3').val('');
                $('#txtCustImagePath').val('');
            }
            function ClearWeighMachineBarcode() {
                $('#txtBarcodeLocationPath').val('');
            }
            function BlurCash() {
                $('#btnCashBlurTrigger').click();
            }
            function BlurCard() {
                $('#btnCardBlurTrigger').click();
            }
            function BlurPurchase() {
                $('#btnPurchaseBlurTrigger').click();
            }
            function BlurSale() {
                $('#btnSaleBlurTrigger').click();
            }
            function BlurCheque() {
                $('#btnChequeBlurTrigger').click();
            }

            $(document).ready(function () {
                if ($("#chkshortName").is(':checked')) {
                    //$("#chkCustom").attr('checked', false);
                    //$("#chkshortName").attr('checked', 'checked');
                    // $("#licustom").hide();
                    $("#chkPO").prop('checked', true);
                    $("#chkMR").prop('checked', true);

                    //$("#chkPO").bind("onchange", function () {
                    //    PurchaseOrderCbxChange();

                    //});
                    //$("#chkMR").bind("onchange", function () {
                    //    MaterialChanged();
                    //});
                    ValidatorEnable(document.getElementById('reqvalCustom'), false);
                }
                else {
                    //$("#chkshortName").attr('checked', false);
                    //$("#chkshortName").attr('checked', 'checked');
                    //   $("#licustom").show();

                    ValidatorEnable(document.getElementById('reqvalCustom'), true);
                }
            });
            function GeCheckedOne() {
                if ($("#chkshortName").is(':checked')) {
                    //$("#chkCustom").attr('checked', false);
                    //$(this).attr('checked', 'checked');
                    //  $("#licustom").hide();
                    // $('#txtCustom').val('');
                    ValidatorEnable(document.getElementById('reqvalCustom'), false);
                }
                else {
                    $("#chkCustom").prop('checked', true);
                    $("#licustom").show();
                    ValidatorEnable(document.getElementById('reqvalCustom'), true);
                }



            }
            function PurchaseOrderCbxChange() {


                if ($("#chkPO").prop("checked")) {
                    $("#chkMR").prop('checked', true);
                }

            }
            function MaterialChanged() {
                if (!($("#chkMR").is(':checked'))) {
                    $("#chkPO").prop('checked', false);
                }
            }
            function ClearModule() {

                $("#chkPO").attr('checked', 'checked');
                $("#chkMR").attr('checked', 'checked');
            }
            function ShowTextBox() {
                if ($("#chkCustom").is(':checked')) {

                    //      $("#chkshortName").attr('checked', false);

                    $("#licustom").show();
                    //  $(this).attr('checked', 'checked');
                    ValidatorEnable(document.getElementById('reqvalCustom'), true);
                }
                else {

                    //ClearGEN();
                }

            }
            function ClearGEN() {
                $("#chkshortName").attr('checked', 'checked');

                $("#chkCustom").attr('checked', false);
                // $("#chkshortName").attr('checked', 'checked');
                $("#licustom").hide();
                $('#txtCustom').val('');
                ValidatorEnable(document.getElementById('reqvalCustom'), false);
                $("#chkshortName").prop('checked', true);


                $('#txtPageHeight').val('');
                $('#txtPageWidth').val('');
                $('#txtVerticalNos').val('');
                $('#txtHorizontalNos').val('');
                $('#txtFromDateFinYear').val('');
                $('#txtToDateFinYear').val('');
                $('#txtMinutes').val('');
                $('#txtMonthlyProcessGracePeriod').val('');
                $("#chkPO").prop('checked', false);
                $("#chkMR").prop('checked', false);
                $("#rdHtmlPrint").prop('checked', true);
                $("#rdtxtPrint").prop('checked', false);
                return false;

            }
            function ClearMAS() {
                $('#txtCustomerShortname').val('');
                $('#txtSupplierShortName').val('');
                $('#txtCompanyName').val('');
                return false;
            }
            function ShowTimezone() {
                try {
                    $("#divTimeDescription").show();
                }
                catch (ex) {

                    alert(ex);
                }
            }
            function CloseTimezone() {
                $("#divTimeDescription").hide();
            }


            function chkactive() {
                if ($('#chkAcountlnking').is(":checked")) {
                    $("#lidbname").show();
                    $("#licustomertype").show();
                }
                else {
                    $("#lidbname").hide();
                    $("#licustomertype").hide();
                }
            }
        </script>
    </form>
</body>

</html>
