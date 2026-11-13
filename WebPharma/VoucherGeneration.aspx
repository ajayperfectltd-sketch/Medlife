<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VoucherGeneration.aspx.cs" Inherits="VoucherGeneration" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Voucher Generation::</title>
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <script src="js/toastr.js"></script>
    <link href="css/main.css" rel="stylesheet" />

    <link href="css/toastr.css" rel="stylesheet" />
    <script>
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


            $("#btnSave").prop('disabled', false);

        }
        function delaysubmit() {
            try {
                if (submit == 1) {
                    return false;

                }

                $("#btnSave").prop('disabled', true);

                window.setTimeout('releasebtn()', 3000);
                submit = 1;
            }
            catch (e) {
                alert(e);
            }
        }


        function ClearAdd() {
            submit = 0;
            $("#ddlVoucher").val('0');
            $("#txtValidFrom").val(new Date().format("dd/MM/yyyy"));
            $("#txtValidUpto").val("");
            $("#txtNoofCopies").val("");
            $("#hdfDate").val("0");

            ClearSearch();
        }
        function ClearSearch() {
            $("#txtDateFrom").val(new Date().format("dd/MM/yyyy"));
            $("#txtDateTo").val(new Date().format("dd/MM/yyyy"));
            $("#txtVoucherNo").val('');
            $("#ddlUsed").val('0');
            $("#ddlDiscountType").val("0");
            $("#ddlVoucherMasterSearch").val("0");
        }
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            return false;
        }
        function ShowVeiwDiv() {
            $("#divAdd").hide();
            $("#divView").show();
        }
        function GetValidUpto() {
            $("#btnHiddenFromDateTrigger").click();
        }
        function textChange() {
            var hdfDate = $("#hdfDate").val() == "" || $("#hdfDate").val() == null ? "0" : $("#hdfDate").val();
            var nos = hdfDate.split(',')[0];
            if (nos != '') {
                var txtDate = $("#txtValidUpto").val();
                var st = txtDate.split('/');
                var dt = new Date(st[2], st[1], st[0]);
                var dts = dt.setDate(dt.getDate() + parseInt(nos)).format("dd/MM/yyyy");
                $("#txtValidUpto").val(dts);



            }
        }
        function TextTriggerClick() {
            $("#btnTextTrigger").click();
        }
    </script>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="frmVoucherGeneration" runat="server"  autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="icon_bg">
                   <div class="search" style="width:670px; " >Voucher Generation</div>
                    <div class="add">Add</div>

                    <div class="add_icon">
                        <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" TabIndex="8" Width="37" Height="38" runat="server" CausesValidation="false" OnClientClick="ShowAddDiv(); ClearAdd(); return false; releasebtn();" />
                    </div>
                     <div class="search">View</div>
                    <div class="search_icon">
                        <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="7" runat="server" CausesValidation="false" OnClick="imgbtnView_Click" />
                    </div>

                </div>
               <%-- <div class="user_bg">
                    <div class="user1">Voucher Generation</div>
                </div>--%>
                <div class="clear"></div>
                <div class="group_bg2">
                    <div id="divAdd" runat="server" style="display: none;">
                        <ul class="contentul" id="Container">
                            <li>
                                <label class="label">Voucher</label>
                                <asp:DropDownList ID="ddlVoucher" runat="server" CssClass="dropdownMandatory" TabIndex="1" onBlur="GetValidUpto();"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Enabled="True" ErrorMessage="Voucher required" ControlToValidate="ddlVoucher" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                </cc:ValidatorCalloutExtender>

                            </li>
                            <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>


                            <li>
                                <label class="label">Valid From</label>
                                <asp:TextBox ID="txtValidFrom" runat="server" CssClass="textbox" TabIndex="2" onchange="TextTriggerClick();"></asp:TextBox>
                                <cc:CalendarExtender ID="txtValidFrom_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtValidFrom" Format="dd/MM/yyyy">
                                </cc:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Enabled="True" ErrorMessage="Date required" ControlToValidate="txtValidFrom" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </cc:ValidatorCalloutExtender>
                               
                            </li>
                            <li class="clear"></li>
                            <li>
                                <label class="label">Valid Upto</label>
                                <asp:TextBox ID="txtValidUpto" runat="server" CssClass="textbox" TabIndex="3"></asp:TextBox>
                                <asp:HiddenField ID="hdfDate" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Enabled="True" ErrorMessage="Date Upto required" ControlToValidate="txtValidUpto" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <asp:Button Text="Hidden" runat="server" ID="btnHiddenFromDateTrigger" OnClick="btnHiddenFromDateTrigger_Click" CausesValidation="false" Style="display: none;" />
                            <asp:Button Text="Hidden" runat="server" ID="btnTextTrigger" OnClick="btnTextTrigger_Click" CausesValidation="false" Style="display: none;" />
                            <%-- </ContentTemplate>
                            </asp:UpdatePanel>--%>
                            <li class="clear"></li>
                            <li>
                                <label class="label">
                                    No of Copies
                                </label>
                                <asp:TextBox runat="server" ID="txtNoofCopies" CssClass="textboxMandatory" onkeypress="CheckInteger(event);" MaxLength="4" TabIndex="4" AutoComplete="off" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Enabled="True" ErrorMessage="No of copies required" ControlToValidate="txtNoofCopies" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                </cc:ValidatorCalloutExtender>

                                <asp:RangeValidator ID="rgvDiscAmt" Enabled="True" runat="server" ControlToValidate="txtNoofCopies" ErrorMessage="Enter valid no of copies Between 1 and 1000" Display="None" MaximumValue="1000" ValidationGroup="submit" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="rgvDiscAmt">
                                </cc:ValidatorCalloutExtender>
                            </li>

                            <li class="btnMargin">
                                <asp:HiddenField ID="hdfVG" runat="server" />
                                <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="5"   OnClientClick="disablebutn();" />
                                <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="return ClearAdd();" CausesValidation="false" TabIndex="6" />
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
                            <div class="divL">


                                <li>
                                    <label class="label">Date From</label>
                                    <asp:TextBox runat="server" ID="txtDateFrom" CssClass="textbox" AutoComplete="off" TabIndex="9" />
                                    <cc:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDateFrom" Format="dd/MM/yyyy">
                                    </cc:CalendarExtender>
                                     <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtDateTo" ControlToValidate="txtDateFrom" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="search"></asp:CompareValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                </cc:ValidatorCalloutExtender>
                                </li>

                                <li>
                                    <label class="label">Voucher No</label>
                                    <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="textbox" MaxLength="25" TabIndex="10"></asp:TextBox>
                                </li>
                                <li>
                                    <label class="label">Voucher</label>
                                    <asp:DropDownList ID="ddlVoucherMasterSearch" CssClass="dropdown" runat="server" TabIndex="11"></asp:DropDownList>
                                </li>

                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="16" ValidationGroup="search"/>
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="17" />
                                </li>
                            </div>

                            <div class="divR">

                                <li>
                                    <label class="label">Date To</label>
                                    <asp:TextBox runat="server" ID="txtDateTo" CssClass="textbox" AutoComplete="off" TabIndex="13" />
                                    <cc:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDateTo" Format="dd/MM/yyyy">
                                    </cc:CalendarExtender>
                                </li>

                                <li>
                                    <label class="label">Used</label>
                                    <asp:DropDownList runat="server" ID="ddlUsed" CssClass="dropdown" TabIndex="14">
                                        <asp:ListItem Text="select" Value="0" />
                                        <asp:ListItem Text="NotUsed" Value="1" />
                                        <asp:ListItem Text="Used" Value="2" />

                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <label class="label">Voucher Type</label>
                                    <asp:DropDownList ID="ddlDiscountType" CssClass="dropdown" runat="server" TabIndex="15">
                                        <asp:ListItem Value="0">select</asp:ListItem>
                                        <asp:ListItem Value="1">Discount</asp:ListItem>
                                        <asp:ListItem Value="2">Gift</asp:ListItem>
                                        <asp:ListItem Value="3">Return</asp:ListItem>
                                    </asp:DropDownList>

                                </li>
                            </div>


                        </ul>
                        <br class="clear" />
                        <ul class="contentul">
                            <asp:GridView ID="gvVoucherGenration" runat="server" CssClass="grid"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_VoucherGeneration" OnRowDeleting="gvVoucherGenration_RowDeleting" OnSelectedIndexChanging="gvVoucherGenration_SelectedIndexChanging">
                                <AlternatingRowStyle BackColor="#CCE0EB" />
                                <Columns>
                                    <asp:BoundField DataField="SlNo" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" />
                                    <asp:BoundField DataField="ValidFrom" HeaderText="Valid From" />
                                    <asp:BoundField DataField="ValidUpto" HeaderText="Valid Upto" />
                                    <asp:BoundField DataField="IsUsed" HeaderText="Used" />
                                    <asp:BoundField DataField="VType" HeaderText="Voucher Type" />
                                    <asp:BoundField DataField="VoucherName" HeaderText="Voucher" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />

                                    <%-- <asp:TemplateField>

                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
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
                        </ul>
                    </div>
                    <div class="clear"></div>
                 <%--   <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                    <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
