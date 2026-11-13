<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationSettings.aspx.cs" Inherits="ApplicationSettings" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />

    <link href="css/toastr.css" rel="stylesheet" />


</head>

<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">Application Settings</div>
                       <div class="add"></div>
                        <div class="add_icon">
                         <%--   <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" OnClientClick="ShowAddDiv(); ClearAdd(); clearbl();  return false; releasebtn();" />--%>
                        </div>
                        <div class="search"></div>
                        <div class="search_icon">
                           <%-- <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" TabIndex="9" OnClick="imgbtnView_Click" />--%>
                        </div>

                    </div>
                  <%--  <div class="user_bg">
                        <div class="user1">Application Settings</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="">


                            <div class="divL" style="width: 65%;">
                                <ul class="contentul">

                                    <div style="float: left; direction: ltr;">
                                        <ul class="contentul">
                                            <li>
                                                <asp:Label ID="Label19" runat="server" Text="Currency" CssClass="label"></asp:Label>
                                                <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="dropdownMandatory" TabIndex="1">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" Style="float: left;" runat="server" ErrorMessage="Required field" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" SetFocusOnError="True" ToolTip="Required field" ValidationGroup="gcfa" ControlToValidate="ddlCurrency" InitialValue="0">*</asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            <li>
                                                <asp:Label ID="Label20" runat="server" Text="Decimal Place" CssClass="label"></asp:Label>
                                                <asp:TextBox ID="txtDecimalPlace" runat="server" Style="text-align: right;" CssClass="textboxMandatory" MaxLength="1" TabIndex="2" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Required Field" ControlToValidate="txtDecimalPlace" ForeColor="Red" Width="140px" SetFocusOnError="True" Display="None" ValidationGroup="gcfa"></asp:RequiredFieldValidator>
                                                <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtDecimalPlace">
                                                </cc:FilteredTextBoxExtender>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Maximum 3 Point And Minimum 1 point" ForeColor="Red" Type="Integer" ControlToValidate="txtDecimalPlace" MinimumValue="1" MaximumValue="3" ValidationGroup="gcfa" Display="None"></asp:RangeValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                                </cc:ValidatorCalloutExtender>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                                </cc:ValidatorCalloutExtender>

                                            </li>
                                            <li>
                                                <asp:Label ID="Label21" runat="server" Text="No of Users" CssClass="label"></asp:Label>
                                                <asp:TextBox ID="txtNoUsers" Style="text-align: right;" runat="server" CssClass="textboxMandatory" MaxLength="3" TabIndex="3" onkeydown="return (event.keyCode!=13);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="No of Concurrent Users" ControlToValidate="txtNoUsers" Display="None" ForeColor="Red" Width="140px" SetFocusOnError="True" ValidationGroup="gcfa"></asp:RequiredFieldValidator>
                                                <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtNoUsers">
                                                </cc:FilteredTextBoxExtender>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="Maximum 999  And Minimum 1 user" ForeColor="Red" Type="Integer" ControlToValidate="txtNoUsers" MinimumValue="1" MaximumValue="999" ValidationGroup="gcfa" Display="None"></asp:RangeValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                                </cc:ValidatorCalloutExtender>

                                            </li>

                                        </ul>
                                        <ul class="contentul">
                                            <li>
                                                <asp:Label ID="Label1" runat="server" Text="Voucher No Generation" CssClass="label"></asp:Label>
                                                <asp:CheckBoxList ID="chkSYSVoucher" runat="server" CssClass="chckbox" TabIndex="4" Style="padding-right: 0px;">
                                                    <asp:ListItem Value="0">Module wise</asp:ListItem>
                                                    <asp:ListItem Value="1">Year wise</asp:ListItem>
                                                </asp:CheckBoxList>

                                            </li>
                                            <li>
                                                <asp:Label ID="Label2" runat="server" Text="Invoice No Generation" CssClass="label"></asp:Label>
                                                <asp:CheckBoxList ID="chkSYSInvoice" runat="server" CssClass="chckbox" TabIndex="5" Style="padding-right: 0px;">
                                                    <asp:ListItem Value="0">Category wise</asp:ListItem>
                                                    <asp:ListItem Value="1">Salesman wise</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </li>

                                        </ul>
                                        <br />
                                        <li class="btnMargin">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" TabIndex="6" meta:resourcekey="btnSubmit" ValidationGroup="gcfa" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="btnClear" runat="server" TabIndex="7" CausesValidation="false" Text="Clear" CssClass="button" meta:resourcekey="btnClear" OnClientClick="Clear(); return false;" />
                                        </li>
                                    </div>


                                    <li class="btnMargin">

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


            function Clear() {
                $('#txtDecimalPlace').val('');
                $('#txtNoUsers').val('');
                $('#ddlCurrency').val('0');
                ClearCheckList();
                $("#ddlCurrency").focus();
            }

            function ClearCheckList() {



                $('#chklstSTKCodeGen input:checkbox').removeAttr('checked');

                $('#chkSYSInvoice input:checkbox').removeAttr('checked');
                $('#chkSYSVoucher input:checkbox').removeAttr('checked');

            }




        </script>
    </form>
</body>

</html>
