<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Quickmenus.aspx.cs" Inherits="Quickmenus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Quick Menu::</title>


    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
          <script src="js/Navigation.js"></script>
     <script type="text/javascript">

         function pageLoad() {

             var ctrContainer = "form";
             setKeynavigation(ctrContainer);

         }
    </script>

</head>
<body>
    <form id="frmQuick" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="btn_bg5"> 
                      <div class="heading2">
                          <div class="user">Quick Menu</div>
                        
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">  
                        <ul class="contentul">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>--%>

                                    <li>

                                        <asp:Label ID="lblUserGp" runat="server" CssClass="label" Text="Users"></asp:Label>
                                        <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="True" CssClass="dropdown" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged1" Style="width: 150px;" TabIndex="1">
                                        </asp:DropDownList>
                                       
                                    </li>
                                    <li class="clear"></li>

                           
                                   
                                    <div>
                                        <ul style="width: 120%; float: left; margin: 0px 0px 0px 0px;">
                                            <li>
                                                <asp:Label ID="Label7" runat="server" CssClass="label" Text="Quick Menu"></asp:Label>
                                            </li>
                                            <li style="width: auto;">
                                                <asp:DropDownList ID="ddlPage1" runat="server" Style="width: 140px;  border-radius: 3px; padding: 1px 3px 2px 3px;"  CssClass="dropdownMandatory"  TabIndex="3">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPage1" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:DropDownList ID="ddlPage2" runat="server" Style="width: 140px; border-radius: 3px; padding: 1px 3px 2px 3px;"  CssClass="dropdownMandatory" TabIndex="4">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlPage2" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:DropDownList ID="ddlPage3" runat="server" Style="width: 140px; border-radius: 3px; padding: 1px 3px 2px 3px;" CssClass="dropdownMandatory" TabIndex="5">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPage3" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:DropDownList ID="ddlPage4" runat="server" Style="width: 140px; border-radius: 3px; padding: 1px 3px 2px 3px;" CssClass="dropdownMandatory" TabIndex="6">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPage4" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:DropDownList ID="ddlPage5" runat="server" Style="width: 140px; border-radius: 3px; padding: 1px 3px 2px 3px;" CssClass="dropdownMandatory" TabIndex="7">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPage5" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                                </cc:ValidatorCalloutExtender>
                                                <asp:DropDownList ID="ddlPage6" runat="server" Style="width: 140px; border-radius: 3px; padding: 1px 3px 2px 3px;"   CssClass="dropdownMandatory" TabIndex="8">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlPage6" Display="None" ErrorMessage="Select a page" ForeColor="Red" InitialValue="0" SetFocusOnError="True" ToolTip="Select a page" ValidationGroup="s"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                        </ul>
                                    </div>
                           
                                         <asp:HiddenField ID="hfUser" runat="server" />
                                         <asp:HiddenField ID="hfClient" runat="server" />
                                         <asp:HiddenField ID="hfIsAdd" runat="server" />
                                         <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                         <asp:HiddenField ID="hfIsDelete" runat="server" />
                                         <asp:HiddenField ID="hfMasterId" Value="0" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="btnSeparation">
                            </div>
                            <br />
                            <li class="clear"></li>
                            <li class="btnMargin" style="padding-left: 138px">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="9" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="s" />
                                <asp:Button ID="btnClear" runat="server" CssClass="button" meta:resourcekey="btnClear" TabIndex="10" OnClientClick="Clear(); return false;" Text="Clear" />
                                <%--  <asp:BulletedList ID="blMessage" runat="server" CssClass="MessageList">
                                        </asp:BulletedList>--%>
                                <asp:HiddenField ID="hfUpdatePermission" runat="server" />
                                <asp:HiddenField ID="hfAddPermission" runat="server" />
                            </li>
                            <%--<li class="clear"></li>--%>
                        </ul>
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
                    $("#<%#ddlUser.ClientID%>").val('0');
                    $("[id*='ddl']").val("0");
                    $('input:checkbox').removeAttr('checked');

                    $("#<%#ddlPage1.ClientID%>").val('0');
                    $("#<%#ddlPage2.ClientID%>").val('0');
                    $("#<%#ddlPage3.ClientID%>").val('0');
                    $("#<%#ddlPage4.ClientID%>").val('0');
                    $("#<%#ddlPage5.ClientID%>").val('0');
                    $("#<%#ddlPage6.ClientID%>").val('0');
                    $("#<%#ddlUser.ClientID%>").focus();
                    $("#ddlUser").focus();

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
