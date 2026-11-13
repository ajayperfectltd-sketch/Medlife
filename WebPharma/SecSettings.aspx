<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SecSettings.aspx.cs" Inherits="SecSettings" %>

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


    <style>
        #rdbItemListField {
            height: 60%;
            width: 100%;
            /*margin-left: 37.5%;*/
            font-size: 14px;
            font-weight: 700;
            padding-right: 175px;
            color: #0C3C64;
            font-family: tahoma;
        }
    </style>

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

                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                            <div id="divAdd" runat="server" style="">
                                <div class="divL" style="width: 250px;">
                                    <ul class="contentul" style="width: 250px;">
                                        <asp:TreeView ID="tvSettings" runat="server" CollapseImageUrl="~/images/down2.png" ExpandImageUrl="~/images/side2.png" LeafNodeStyle-ImageUrl="~/images/side2.png" Style="border-radius: 14px;" BorderStyle="Solid" OnSelectedNodeChanged="tvSettings_SelectedNodeChanged" ExpandDepth="0" BackColor="#3a7277">
                                            <HoverNodeStyle Font-Underline="True" ForeColor="#88c01d" />
                                            <Nodes>
                                                <asp:TreeNode Text="Sales" Value="SLS"></asp:TreeNode>
                                                <asp:TreeNode Text="Purchase" Value="PUR"></asp:TreeNode>
                                                <asp:TreeNode Text="POS" Value="POS"></asp:TreeNode>
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
                                            <asp:View ID="vwSLS" runat="server">
                                                <div style="float: left; direction: ltr;">
                                                    <ul class="contentul">
                                                        <%--<li>
                                                            <li style="width: 100%; height: 30px; background-color: #D5EFC9; font-weight: bold;">Sales Settings</li>

                                                        </li>--%>
                                                        <li id="LIAccPost" runat="server" style="height: 60%; width: 100%; margin-left: 37.5%;">
                                                            <asp:CheckBox ID="chkAcountPost" runat="server" CssClass="chckbox" Text="Acc Posting Sale and SR only in Counter close" TabIndex="1" />
                                                        </li>
                                                        <%--<li id="LIExpiry" runat="server" style="height: 60%; width: 100%; margin-left: 37.5%;">
                                                            <asp:Label ID="Label1" runat="server" Text="Expiry warning Before" CssClass="label"></asp:Label>
                                                            <asp:TextBox ID="txtExpiryWarning" runat="server" CssClass="textboxMandatory" MaxLength="3" onkeypress="CheckNumWithDecimal(event);" onkeydown="return (event.keyCode!=13);" Text="0" TabIndex="1"></asp:TextBox>

                                                        </li>--%>
                                                        <li class="btnMargin" runat="server" style="margin-left: 12.5%">
                                                            <asp:Button ID="btnSubmitSL" runat="server" Text="Submit" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="2" ValidationGroup="sec" OnClick="btnSubmitSL_Click" />
                                                            <%--<asp:Button ID="btnClearSL" runat="server" CausesValidation="false" Text="Clear" CssClass="button" OnClientClick="ClearSL(); return false;" />--%>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="vwPurchase" runat="server">
                                                <div style="float: left; direction: ltr; width: 100%;">
                                                    <ul class="contentul">
                                                        <%-- <li>
                                                            <li style="width: 100%; height: 30px; background-color: #D5EFC9;">Purchase Settings</li>

                                                        </li>--%>

                                                        <li id="liStock" runat="server" style="height: 60%; width: 100%; margin-left: 37.5%;">
                                                            <asp:CheckBox ID="chkStockUpdation" runat="server" CssClass="chckbox" Text="Stock updation before purchase approve" TabIndex="1" />
                                                        </li>

                                                        <br />
                                                        <li class="btnMargin">
                                                            <asp:Button ID="btnSubmitPurchase" runat="server" Text="Submit" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="2" ValidationGroup="submit" OnClick="btnSubmitPurchase_Click" />
                                                            <%--<asp:Button ID="btnClearPOS" CausesValidation="false" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClear" OnClientClick="ClearPUR(); return false;" />--%>
                                                        </li>
                                                    </ul>

                                                </div>
                                            </asp:View>
                                            <asp:View ID="vwPOS" runat="server">
                                                <div style="float: left; direction: ltr;">
                                                    <ul class="contentul">
                                                        <%--<li>
                                                            <li style="width: 100%; height: 19px; background-color: #D5EFC9; font-weight: bold;">POS Settings</li>

                                                        </li>--%>

                                                        <li id="liField" runat="server" style="height: 60%; width: 100%; margin-left: 37.5%;">

                                                         <%--   <asp:Label ID="Label20" runat="server" Text="Field in Item List (Sales POS)"></asp:Label>

                                                            <asp:RadioButtonList ID="rdbItemListField" runat="server">
                                                                <asp:ListItem Value="M">Manufacturer</asp:ListItem>
                                                                <asp:ListItem Value="B">BIN</asp:ListItem>

                                                            </asp:RadioButtonList>--%>
                                                                   <asp:CheckBox ID="chkItemListField" runat="server" CssClass="chckbox" Text="Bin Instead of Manufacture" TabIndex="1" ToolTip="Shows Bin in POS instead of Manufacture" />
                                                   


                                                        </li>
                                                        <br />
                                                        <li class="btnMargin" style="margin-left: 12.5%">
                                                            <asp:Button ID="btnSubmitPOS" runat="server" Text="Submit" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="2" ValidationGroup="submit" OnClick="btnSubmitPOS_Click" />
                                                            <%--<asp:Button ID="btnClearPOS" CausesValidation="false" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClear" OnClientClick="ClearPOS(); return false;" />--%>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>

                                        <li class="btnMargin">
                                            <asp:HiddenField ID="hfUser" runat="server" />
                                            <asp:HiddenField ID="hfClient" runat="server" />

                                        </li>
                                        <li style="width: 450px; float: left;">
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

            function ClearSL() {
                $("#chkAcountPost").prop('checked', false);
            }
            function ClearPUR() {
                $("#chkStockUpdation").prop('checked', false);
            }
        </script>
    </form>
</body>

</html>
