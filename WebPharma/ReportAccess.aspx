<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportAccess.aspx.cs" Inherits="ReportAccess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>::Report Access</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script type="text/javascript">
        function Clear() {
            $("[id*='ddl']").val("0");
            $('input:checkbox').removeAttr('checked');
            clearbl();
            $("#ddlUserGroup").focus();
        }
        function ClearItems() {

            $('input:checkbox').removeAttr('checked');

        }
        function clearbl() {
            $("#blMessage").empty();
        }
        function ReportType() {
            if ($('#rdobtnType_0').is(':checked')) {
                $("#LiWebPharma").attr("style", "display:block");
                $("#LiAccounts").attr("style", "display:none");
            }
            else {
                $("#LiAccounts").attr("style", "display:block");
                $("#LiWebPharma").attr("style", "display:none");
            }
        }
    </script>
    <style type="text/css">
        .Border {
            margin: 0.5em;
            border-style: groove;
            box-shadow: 0px 0px 2px 2px #888888;
            border: solid 2px #FFF;
            border-radius: 7px;
            padding-right: 0px;
            margin-right:250px;
        }

        .rptr {
            background-color:white ;
        }

        .rptr1 {
            background-color: white;
            border-top: solid;
            border-bottom: solid;
            border-width: 1px;
        }
    </style>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                     <div class="heading2">
                          <div class="user">Report Access</div>
                       
                    </div>
                    <div class="group_bg2">
                         <div class="main_bg">  

                       


                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                     <ul class="contentul">
                                    <li>
                                        <asp:Label ID="lblUserGp" runat="server" CssClass="label" Text="User Group"></asp:Label>

                                        <asp:DropDownList ID="ddlUserGroup" runat="server" AutoPostBack="True" CssClass="dropdown" OnSelectedIndexChanged="ddlUserGroup_SelectedIndexChanged" Style="width: 150px;" TabIndex="1">
                                        </asp:DropDownList>

                                    </li>
                                    <li class="clear"></li>
                                    <li class="clear"></li>
                                    
                                    <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Report Type"></asp:Label>
                                        <asp:RadioButtonList ID="rdobtnType" onchange="ReportType()" Font-Size="12px" runat="server" RepeatDirection="Horizontal" Style="float: left; width: 210px;">

                                            <asp:ListItem Value="1" Selected="True">Inventory</asp:ListItem>
                                            <asp:ListItem Value="2">Accounts</asp:ListItem>

                                        </asp:RadioButtonList>

                                    </li>
                                    <br />
                                      <%--<div style="height: 500px; overflow-y: scroll; margin: 25px 0px 20px 132px; float: left; width: 78%; box-shadow: 0px 0px 2px 2px #888; border: 2px solid #FFF; border-radius: 7px; padding-right: 0px;">--%>
                                    <div style="min-height: 300px; margin: 0px 0px 0px 0px; float: right; width: 95%;">
                                        <ul style="margin: 0px 0px 0px 0px;">
                                            <li class="clear"></li>
                                            <li id="LiWebPharma" class="Border">

                                                <asp:Repeater ID="RepeaterWebPharma" runat="server" OnItemDataBound="RepeaterReportCategory_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div class="<%#(Container.ItemIndex%2==0)?"rptr":"rptr1" %>">

                                                            <asp:HiddenField ID="hfID_ReportCategory" Value='<%#Eval("ID_ReportCategory") %>' runat="server" />
                                                            <h3 style="padding: 0px 0px 0px 10px; margin: 0px; font-weight: bold; font-size: 14px; color: teal"><%#Eval("Name") %> </h3>
                                                            <asp:DataList ID="dlReportAccess" runat="server" RepeatColumns="3" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="5" Style="width: 100%; padding: 5px 0px 2px 0px; margin: 0px 0px 0px 0px;">
                                                                <ItemStyle ForeColor="Teal" />
                                                                <ItemTemplate>

                                                                    <asp:HiddenField ID="hfID_ReportSubCategory" Value='<%#Eval("ID_ReportSubCategory") %>' runat="server" />
                                                                    <asp:CheckBox ID="chkReportSubCategory" Height="15px" runat="server" Checked='<%# ReportAcc(Eval("ReportAccess"))%>' CssClass="chckboxNew" Style="margin-right: 5px; margin-left: 20px; margin-right: 20px; font-size: 12px; padding: 10px 0px 0px 40px; font-weight: bold;"
                                                                        Text='<%#" "+Eval("Name")%>' />
                                                                </ItemTemplate>
                                                                <%--<ItemStyle Width="33.3%" />--%>

                                                            </asp:DataList>
                                                        </div>
                                                    </ItemTemplate>

                                                </asp:Repeater>
                                                <asp:HiddenField ID="hfUser" runat="server" />
                                                <asp:HiddenField ID="hfClient" runat="server" />
                                                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                            </li>
                                            <li id="LiAccounts" style="display: none" class="Border">

                                                <asp:Repeater ID="RepeaterAccounts" runat="server" OnItemDataBound="RepeaterAccounts_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div class="<%#(Container.ItemIndex%2==0)?"rptr":"rptr1" %>">

                                                            <asp:HiddenField ID="hfID_ReportCategory" Value='<%#Eval("ID_ReportCategory") %>' runat="server" />
                                                            <h3 style="padding: 0px 0px 0px 10px; margin: 0px; font-weight: bold; font-size: 14px; color: teal"><%#Eval("Name") %> </h3>
                                                            <asp:DataList ID="dlReportAccess" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="5" Style="width: 100%; padding: 5px 0px 2px 0px; margin: 0px 0px 0px 0px;">
                                                                <AlternatingItemStyle BackColor="White" />
                                                                <FooterStyle  ForeColor="#4A3C8C" />
                                                    <HeaderStyle  Font-Bold="True" ForeColor="#F7F7F7" />
                                                                <ItemTemplate>

                                                                    <asp:HiddenField ID="hfID_ReportSubCategory" Value='<%#Eval("ID_ReportSubCategory") %>' runat="server" />
                                                                    <asp:CheckBox ID="chkReportSubCategory" Height="35px" runat="server" Checked='<%# ReportAcc(Eval("ReportAccess"))%>' CssClass="chckboxNew" Style="margin-right: 5px; margin-left: 20px; margin-right: 20px; font-size: 14px; color:teal; padding: 10px 0px 0px 40px; font-weight: bold;"
                                                                        Text='<%#" "+Eval("Name")%>' />
                                                                </ItemTemplate>
                                                                <%--<ItemStyle Width="33.3%" />--%>

                                                            </asp:DataList>
                                                        </div>
                                                    </ItemTemplate>

                                                </asp:Repeater>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                                <asp:HiddenField ID="hfMasterId" Value="0" runat="server" />
                                                 <asp:HiddenField ID="hfuserName"  runat="server" />
                                            </li>
                                            
                                            <li class="btnMargin">
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="button" meta:resourcekey="btnSubmit" OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="s" />
                                            <asp:Button ID="btnClear" runat="server" CssClass="button" meta:resourcekey="btnClear" OnClientClick="Clear(); return false;" Text="Clear" />
                                            </li>
                                        </ul>
                                    </div>
                                       </ul>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                     




                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>
                    </div>
                        </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>
    </form>
</body>
</html>
