<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>



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
    <script type="text/javascript">
        function ClearAdd() {
            $("#txtCurrentPassword").val('');
            $("#txtNewPassword").val('');
            $("#txtConfirmPassword").val('');
        }



    </script>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <div class="btn_bg5"> 

                    <div class="heading2">
                        <div class="user">Change Password</div>
                </div>

                    
                 
                    <div class="clear"></div>
                    <div class="group_bg2">
                          <div class="main_bg">  
                        <div id="divAdd" runat="server">
                            <ul class="contentul">
                                 <li style="display:none;">
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Current Password"></asp:Label>
                                    <asp:TextBox ID="TextBox1" CssClass="textbox" runat="server" TextMode="Password"  MaxLength="20"></asp:TextBox>

                                </li>
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Current Password"></asp:Label>
                                    <asp:TextBox ID="txtCurrentPassword" CssClass="textbox" runat="server" TextMode="Password"  MaxLength="20"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="rfvCurrentPassword" runat="server" ErrorMessage="Current Password required" ControlToValidate="txtCurrentPassword" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rfvCurrentPassword">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label9" runat="server" CssClass="label" Text="New Password"></asp:Label>
                                    <asp:TextBox ID="txtNewPassword" CssClass="textbox" TextMode="Password" AutoCompleteType="Disabled" runat="server" MaxLength="20"></asp:TextBox>
                                    <cc:PasswordStrength ID="PasswordStrength1" TextStrengthDescriptions="Weak;Average;Good;Excellent" TextStrengthDescriptionStyles="WeakStrength;AverageStrength;GoodStrength;ExcellentStrength" StrengthIndicatorType="Text" runat="server" Enabled="True" TargetControlID="txtNewPassword" MinimumSymbolCharacters="1" MinimumUpperCaseCharacters="1" PreferredPasswordLength="8">
                                    </cc:PasswordStrength>
                                    <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="Password required" ControlToValidate="txtNewPassword" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="rfvNewPassword">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label8" runat="server" CssClass="label" Text="Confirm Password"></asp:Label>
                                    <asp:TextBox ID="txtConfirmPassword" CssClass="textbox" TextMode="Password" runat="server" MaxLength="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Confirm Password required" ControlToValidate="txtConfirmPassword" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmPassword" Display="None" ErrorMessage="Password Mismatch"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>



                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" CausesValidation="false" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfSEC01" runat="server" />
                                    <asp:HiddenField ID="hfSEC02" runat="server" />
                                </li>
                            </ul>
                        </div>
                              </div>

                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
