<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CessSettings.aspx.cs" Inherits="CessSettings" %>

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


            $("#btnSubmit").prop('disabled', false);

        }
        function delaysubmit() {
            try {
                if (submit == 1) {
                    return false;

                }

                $("#btnSubmit").prop('disabled', true);

                window.setTimeout('releasebtn()', 3000);
                submit = 1;
            }
            catch (e) {
                alert(e);
            }
        }


        function ClearAdd() {
            try {
                submit = 0;
                $("#<%#txtEffectFrom.ClientID%>").val(new Date().format('dd/MM/yyyy'));
                $("#<%#txtCGST.ClientID%>").val('0');
                $("#<%#txtSGST.ClientID%>").val('0');
                $("#<%#txtIGST.ClientID%>").val('0');
                $("#<%#txtValdt.ClientID%>").val(new Date().format('dd/MM/yyyy'));
                $("#<%#txtFromDate.ClientID%>").val(new Date().format('dd/MM/yyyy'));
                $("#<%#txtToDate.ClientID%>").val(new Date().format('dd/MM/yyyy'));
            }
            catch (e) {
                alert(e);
            }
        }
        function ClearSearch() {
            $("#<%#txtFromDate.ClientID%>").val(new Date().format('dd/MM/yyyy'));
            $("#<%#txtToDate.ClientID%>").val(new Date().format('dd/MM/yyyy'));
            $("#<%#txtFromDate.ClientID%>").focus();
        }
        function ShowAddDiv() {
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtEffectFrom.ClientID%>").focus();
        }
    </script>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">Cess Settings</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" TabIndex="8" OnClientClick="ShowAddDiv(); ClearAdd();   return false; releasebtn();" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" TabIndex="7" OnClick="imgbtnView_Click" />
                        </div>

                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label7" runat="server" CssClass="label" Text="Effect From"></asp:Label>
                                    <asp:TextBox ID="txtEffectFrom" CssClass="textboxMandatory" runat="server" TabIndex="2"></asp:TextBox>
                                    <cc:CalendarExtender ID="txtEffectFrom_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtEffectFrom">
                                    </cc:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="rfvEffectFrom" runat="server" ErrorMessage="Date required" ControlToValidate="txtEffectFrom" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="rfvEffectFrom">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:TextBox ID="txtValdt" CssClass="textboxMandatory" runat="server" Style="display: none"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator3" ControlToCompare="txtValdt" ControlToValidate="txtEffectFrom" Type="Date" Operator="GreaterThanEqual" ErrorMessage="Date should not be Past Date" runat="server" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="CompareValidator3">
                                        </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label49" Text="CGST" CssClass="label" runat="server" />
                                    <asp:TextBox runat="server" ID="txtCGST" TabIndex="3" Style="text-align: right;" CssClass="textbox" onkeypress="CheckNumWithDecimal(event);" MaxLength="14" />
                                </li>
                                <li>
                                    <asp:Label ID="Label50" Text="SGST" CssClass="label" runat="server" />
                                    <asp:TextBox runat="server" ID="txtSGST" TabIndex="4" Style="text-align: right;" CssClass="textbox" onkeypress="CheckNumWithDecimal(event);" MaxLength="14" />
                                </li>
                                <li>
                                    <asp:Label ID="Label51" Text="IGST" CssClass="label" runat="server" />
                                    <asp:TextBox runat="server" ID="txtIGST" TabIndex="5" Style="text-align: right;" CssClass="textbox" onkeypress="CheckNumWithDecimal(event);" MaxLength="14" />
                                </li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" TabIndex="6" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="7" />
                                    <asp:HiddenField ID="hfCessSettingsId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                </li>
                            </ul>
                        </div>
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="From Date"></asp:Label>
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" TabIndex="8"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                        </cc:CalendarExtender>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="To Date"></asp:Label>
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" TabIndex="9"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                        </cc:CalendarExtender>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="10" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="11" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvCessSettings" runat="server" CssClass="grid" EmptyDataText="No Data Found!" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Solid" 
                                    BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_CessSettings"  OnSelectedIndexChanging="gvCessSettings_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="EffectFrom" HeaderText="Effect From" />
                                        <asp:BoundField DataField="CGST" HeaderText="CGST" />
                                        <asp:BoundField DataField="SGST" HeaderText="SGST" />
                                        <asp:BoundField DataField="IGST" HeaderText="IGST" />
                                        <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="#12361D" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                                <div class="Pager">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClick="Page_Changed" Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ul>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
