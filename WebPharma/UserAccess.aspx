<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAccess.aspx.cs" Inherits="UserAccess" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::User Access::</title>


    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />


</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="frmUserAccess" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

               <div> 
                      <div class="heading2">
                          <div class="user">User Access</div>
                       
                    </div>

                    <%--<br class="clear">--%>
                    <div class="group_bg2">
                     <div class="main_bg">  
                      
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>--%>
                                      <ul class="contentul">

                                    <li>

                                        <asp:Label ID="lblUserGp" runat="server" CssClass="label" Text="User Group"></asp:Label>
                                        <asp:DropDownList ID="ddlUserGroup" runat="server" AutoPostBack="True" CssClass="dropdown" OnSelectedIndexChanged="ddlUserGroup_SelectedIndexChanged" Style="width: 150px;" TabIndex="1">
                                        </asp:DropDownList>
                                        <%--  onBlur="FillDetails();"--%>
                                        <%--<asp:Button ID="btnHidden" runat="server" CausesValidation="false" OnClick="btnHidden_Click" Style="display: none;" Text="Hide" />--%>
                                    </li>
                                    <li class="clear"></li>

                          <%--          <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="User"></asp:Label>
                                        <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="True" CssClass="dropdownMandatory" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged1" Style="width: 150px;" TabIndex="2">
                                        </asp:DropDownList>
                                       
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlUser" CssClass="Validator" Display="None" ErrorMessage="Select a user" InitialValue="0" SetFocusOnError="True" ToolTip="Select a user" ValidationGroup="s"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:Button ID="btnFill" runat="server" CausesValidation="false" OnClick="btnFill_Click" Style="display: none;" Text="Button" />
                                    </li>--%>
                           
                                   
                                    <div>
                                        <ul style="width: 100%; float: left; margin: 0px 0px 0px 0px;">
                                            <li style="display:none">
                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Quick Menu"></asp:Label>
                                            </li>
                                            <li style="width: auto;display:none">
                                                <asp:DropDownList ID="ddlPage1" runat="server" Style="width: 140px; border-radius: 3px; padding: 1px 3px 2px 3px; background-color: #FCFDD6;" TabIndex="3">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPage1" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:DropDownList ID="ddlPage2" runat="server" Style="width: 140px; border-radius: 3px; padding: 1px 3px 2px 3px; background-color: #FCFDD6;" TabIndex="4">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPage2" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:DropDownList ID="ddlPage3" runat="server" Style="width: 140px; border-radius: 3px; padding: 1px 3px 2px 3px; background-color: #FCFDD6;" TabIndex="5">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPage3" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:DropDownList ID="ddlPage4" runat="server" Style="width: 140px; border-radius: 3px; padding: 1px 3px 2px 3px; background-color: #FCFDD6;" TabIndex="6">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPage4" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:DropDownList ID="ddlPage5" runat="server" Style="width: 140px; border-radius: 3px; padding: 1px 3px 2px 3px; background-color: #FCFDD6;" TabIndex="7">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPage5" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:DropDownList ID="ddlPage6" runat="server" Style="width: 140px; border-radius: 3px; padding: 1px 3px 2px 3px; background-color: #FCFDD6;" TabIndex="8">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlPage6" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                        </ul>
                                    </div>
                                    <div style="height: 500px; overflow-y: scroll; margin: 25px 0px 20px 132px; float: left; width: 67%; box-shadow: 0px 0px 2px 2px #888; border: 2px solid #FFF; border-radius: 7px; padding-right: 0px;">
                                        <ul style="margin: 0px 0px 0px 0px;">
                                            <li class="clear"></li>
                                            <li>
                                                <asp:DataList ID="dlUserAccess" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Horizontal" Style="width: 110%; padding: 5px 0px 2px 0px;">
                                                    <AlternatingItemStyle BackColor="White" />
                                                    <FooterStyle  ForeColor="#4A3C8C" />
                                                    <HeaderStyle  Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <ItemStyle  />
                                                    <ItemTemplate>
                                                        <table>
                                                            <tr>
                                                        <%--        <td colspan="4">
                                                                    <div style="padding-top: 6px; padding-right: 5px;">
                                                                        <asp:Label ID="Label7" runat="server" CssClass="label" ForeColor="#104714" Style="font-weight: bold; font-size: 18px;" Text='<%# Eval("PageName") %>'></asp:Label>
                                                                    </div>
                                                                     <div>
                                                                          <asp:CheckBox ID="chkIsCheck" runat="server" Checked='<%# Eval("IsCheck") %>' CssClass="chckboxNew" Style="margin-right: 5px; font-size: 16px; font-weight: bold; margin-left: 20px; margin-right: 20px; padding-left: 40px;" TabIndex="<%#(12+Container.ItemIndex*4 +1) %>" />
                                                                         </div>
                                                                </td>--%>
                                                              
                                                               
                                                                 <asp:CheckBox ID="chkIsCheck" runat="server" style="font-size: 14px;font-weight: 700;color: #0C3C64;font-family: tahoma;" Checked='<%# Eval("IsCheck") %>' Text='<%# Eval("PageName") %>'  TabIndex="<%#(12+Container.ItemIndex*4 +1) %>" />
                                                                     <%--  <asp:Label ID="Label7" runat="server" CssClass="label" ForeColor="Teal" Style="font-weight: bold;width:225px" Text='<%# Eval("PageName") %>'></asp:Label>--%>
                                    <%--                                      <li>
                                    <asp:Label ID="lblStopSale" runat="server" CssClass="label" Text="Stop Sale"></asp:Label>
                                     <asp:CheckBox ID="chkstopsale" runat="server" Width="15"  TabIndex="17" /> 
                                                
                                            </li>--%>
                                                            </
                                                                tr>
                                                                <td colspan="4">
                                                                    <asp:HiddenField ID="hfPagesId" runat="server" Value='<%# Eval("ID_Pages") %>' />
                                                                </td>
                                                           
                                                       
                                                        </table>
                                                    </ItemTemplate>
                                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                </asp:DataList>
                                                <asp:HiddenField ID="hfUser" runat="server" />
                                                <asp:HiddenField ID="hfClient" runat="server" />
                                                <asp:HiddenField ID="hfIsAdd" runat="server" />
                                                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                                <asp:HiddenField ID="hfIsDelete" runat="server" />
                                                <asp:HiddenField ID="hfMasterId" Value="0" runat="server" />
                                            </li>
                                        </ul>
                                    </div>
                                          </ul>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="btnSeparation">
                            </div>
                            <br />
                            <li class="clear"></li>
                            <li class="btnMargin" style="padding-left: 155px;">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="button"  TabIndex="9" OnClick="btnSubmit_Click" Text="Submit"/>
                                <asp:Button ID="btnClear" runat="server" CssClass="button" meta:resourcekey="btnClear" TabIndex="10" OnClientClick="Clear(); return false;" Text="Clear" />
                                <%--  <asp:BulletedList ID="blMessage" runat="server" CssClass="MessageList">
                                        </asp:BulletedList>--%>
                                <asp:HiddenField ID="hfUpdatePermission" runat="server" />
                                <asp:HiddenField ID="hfAddPermission" runat="server" />
                            </li>
                            <%--<li class="clear"></li>--%>
                        
                         </div>
                    </div>
                    <%-- <br>
                                <br></br>
                                <br>
                                    <br></br>
                                    <br></br>
                                    <br></br>
                                    <br>
                                        <br></br>
                                    </br>
                                </br>
                            </br>
                        </br>
                    </br>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/jquery-1.10.2.js"></script>
        <script src="js/Custom.js"></script>
        <script src="js/toastr.js"></script>
        <script type="text/javascript">
            function Clear() {
                try {
                    $("#<%#ddlUserGroup.ClientID%>").val('0');
                    $("[id*='ddl']").val("0");
                    $('input:checkbox').removeAttr('checked');
 
                    $("#<%#ddlPage1.ClientID%>").val('0');
                    $("#<%#ddlPage2.ClientID%>").val('0');
                    $("#<%#ddlPage3.ClientID%>").val('0');
                    $("#<%#ddlPage4.ClientID%>").val('0');
                    $("#<%#ddlPage5.ClientID%>").val('0');
                    $("#<%#ddlPage6.ClientID%>").val('0');
                    $("#<%#ddlUserGroup.ClientID%>").focus();
                    $("#ddlUserGroup").focus();

                }
                catch (e) {
                    alert(e);
                }

            }
            function Fillddl() {
                if (!isPostBack()) {
                    $("#btnFill").click();
                }
                return false;
            }
            function FillDetails() {
                if (!isPostBack()) {

                    $("#btnHidden").click();
                }



                return false;
            }
            function isPostBack() {
                return document.getElementById('_ispostback') == 'True';
            }


            //$(function () {
            //    $('input:checkbox').each(function (index) {
            //        var bc=12+parseInt(index)
            //        $(this).prop('tabindex', bc);

            //    });


            //});












        </script>
    </form>
</body>
</html>
