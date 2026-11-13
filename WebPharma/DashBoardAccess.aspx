<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoardAccess.aspx.cs" Inherits="DashBoardAccess" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::User Access::</title>


    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />


</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="frmUserAccess" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div>
                      <div class="heading2">
                          <div class="user">Dash Board Access</div>
                       
                    </div> 
               
                          
                            <div class="group_bg2">
                                  <div class="main_bg">  
                                <ul class="contentul">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>--%>

                                            <li>

                                                <asp:Label ID="lblUserGp" runat="server" CssClass="label" Text="User Group"></asp:Label>
                                                <asp:DropDownList ID="ddlUserGroup" runat="server" AutoPostBack="True" CssClass="dropdown" OnSelectedIndexChanged="ddlUserGroup_SelectedIndexChanged" Style="width: 150px;" TabIndex="1">
                                                </asp:DropDownList>
                                                <%--  onBlur="FillDetails();"--%>
                                              <%--  <asp:Button ID="btnHidden" runat="server" CausesValidation="false" OnClick="btnHidden_Click" Style="display: none;" Text="Hide" />--%>
                                            </li>
                                            <li class="clear"></li>
                                            <div style="margin: 25px 0px 20px 132px; float: left; width: 70%; box-shadow: 0px 0px 2px 2px #888; border: 2px solid #FFF; border-radius: 7px; padding-right: 0px;">
                                                <ul style="margin: 0px 0px 0px 0px;">
                                                    <li class="clear"></li>
                                                    <li>
                                                        <%--<asp:DataList ID="DataList1" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Both" TabIndex="5" style="width: 100%; padding: 5px 0px 2px 0px; margin: 0px 0px 0px 0px; border-style: solid; " RepeatColumns="3">--%>
                                                       <asp:DataList ID="dlDashBoardAccess" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Horizontal" TabIndex="5" Style="width: 100%; padding: 5px 0px 2px 0px;" RepeatColumns="3">
                                                            <%--<AlternatingItemStyle BackColor="White" />--%>
                                                            
                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="#4A3C8C" />
                                                            <HeaderStyle BackColor="#249BB1" Font-Bold="True" ForeColor="#F7F7F7" />
                                                            <ItemStyle BackColor="White" ForeColor="Black" />
                                                            <ItemTemplate>
                                                                
                                                                <asp:HiddenField ID="hdfDashBoard" runat="server" Value='<%#Eval("ID_Dashboard") %>' />
                                                                <asp:CheckBox ID="chkAccess" runat="server" Checked='<%# Eval("Checked") %>' CssClass="chckboxNew" Style="margin-right: 5px; font-size: 16px; font-weight: bold; margin-left: 20px; margin-right: 20px; padding-left: 40px;" TabIndex='<%#(5+Container.ItemIndex +1) %>' Text='<%# Eval("Name") %>' />
                                                                    
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="btnSeparation">
                                    </div>
                                    <br />
                                    <li class="clear"></li>
                                    <li class="clear"></li>
                                    <li class="btnMargin" style="padding-right: 25px;">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" meta:resourcekey="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="s" TabIndex="3" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="button" TabIndex="4" meta:resourcekey="btnClear" CausesValidation="false" OnClientClick="Clear();" Text="Clear" OnClick="btnClear_Click"  />
                                      <%--  <asp:BulletedList ID="blMessage" runat="server" CssClass="MessageList"
                                        </asp:BulletedList>>--%>
                                        <asp:HiddenField ID="hfUpdatePermission" runat="server" />
                                        <asp:HiddenField ID="hfAddPermission" runat="server" />
                                    </li>
                                    <li class="clear"></li>
                                </ul>
                            </div>
                           

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/jquery-1.10.2.js"></script>
        <script src="js/Custom.js"></script>
        <script src="js/toastr.js"></script>
        <script type="text/javascript">
            function Clear() {
                try {
                   
                    $("[id*='ddl']").val("0");
                    $('input:checkbox').removeAttr('checked');
                    $("#ddlUserGroup").focus();


                }
                catch (e) {
                    alert(e);
                }
                
            }
           
            function isPostBack() {
                return document.getElementById('_ispostback') == 'True';
            }















        </script>
    </form>
</body>
</html>
