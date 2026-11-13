<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupOneVoucher.aspx.cs" Inherits="GroupOneVoucher" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>


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
            submit = 0;
            $("#<%#txtGroupOneCode.ClientID%>").val('');
            $("#<%#txtGroupOneName.ClientID%>").val('');
            $("#<%#txtDescrption.ClientID%>").val('');
            $("#<%#ddlAcSubHead.ClientID%>").val('0');
            $('#<%#chkActive.ClientID%>').prop('checked', true);
            $("#<%#txtGroupOneCode.ClientID%>").focus();
        }
        function ClearSearch() {
            $("#<%#txtSearchGroupOneCode.ClientID%>").val('');
            $("#<%#txtSearchGroupOneName.ClientID%>").val('');
            $("#<%#ddlSearchAccountSubHead.ClientID%>").val('0');
            $("#<%#txtSearchGroupOneCode.ClientID%>").focus();
        }
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtGroupOneCode.ClientID%>").focus();
        }
        function clearbl() {
            $("#blMessage").empty();
        }
        function EnterKeyFilter() {



            $(document).keypress(
                function (event) {
                    if (event.which == '13') {
                        //alert(8);
                        event.preventDefault();
                    }


                });

        }
    </script>
</head>

<body onkeydown="javascript:EnterKeyFilter();">
    <form id="frmGroupOneVoucher" runat="server"  autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                         <div class="search" style="width:670px; " >GroupOn Voucher</div>
                          <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" TabIndex="10"  runat="server" OnClientClick="ShowAddDiv(); ClearAdd(); clearbl();  return false;  releasebtn();" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" TabIndex="9" OnClick="imgbtnView_Click" />
                        </div>
                      
                    </div>
                    <%--<div class="user_bg">
                        <div class="user1">GroupOn Voucher</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: none;">
                            <%--style="display: none;"--%>
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Group On Code"></asp:Label>
                                    <asp:TextBox ID="txtGroupOneCode" CssClass="textboxMandatory" runat="server" MaxLength="10" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="GroupOneCode required" ControlToValidate="txtGroupOneCode" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Group On Name"></asp:Label>
                                    <asp:TextBox ID="txtGroupOneName" CssClass="textboxLongMandatory" runat="server" MaxLength="250" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="GroupOneName required" ControlToValidate="txtGroupOneName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li style="display:none">
                                    <label class="label">Account Sub Head</label>
                                    <asp:DropDownList ID="ddlAcSubHead" runat="server" CssClass="dropdownMandatory" TabIndex="3"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Enabled="True" ErrorMessage="Sub Head required" ControlToValidate="ddlAcSubHead" Display="None" InitialValue="0" SetFocusOnError="True" ValidationGroup="submit1"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label7" runat="server" CssClass="label" Text="Description"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtDescrption" TextMode="MultiLine" CssClass="multiline" TabIndex="4" MaxLength="500" />
                                </li>
                                <li class="btnMargin">

                                    <asp:CheckBox ID="chkActive" runat="server" CssClass="chckbox" TabIndex="5" Text="Active" />
                                </li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="6" ValidationGroup="submit" OnClick="btnSubmit_Click"  OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="7" OnClientClick="ClearAdd(); return false;" />
                                    <asp:HiddenField ID="hdfGroupOneVoucher" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                </li>
                            </ul>
                        </div>
                        <div id="divView" runat="server" style="display: none;">
                            <%--style="display: none;"--%>
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Group On Code"></asp:Label>
                                        <asp:TextBox ID="txtSearchGroupOneCode" CssClass="textbox" TabIndex="11" runat="server"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Group On Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchGroupOneName" CssClass="textbox" TabIndex="12" runat="server"></asp:TextBox>
                                    </li>
                                    <li style="display:none">
                                        <label class="label">Account Sub Head</label>
                                        <asp:DropDownList runat="server" CssClass="dropdown" ID="ddlSearchAccountSubHead" TabIndex="13"></asp:DropDownList>
                                    </li>

                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="14" />
                                        <asp:Button ID="btnSearchClear" TabIndex="15" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvGroupOneVoucher" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_GroupOneVoucher" OnRowDeleting="gvGroupOneVoucher_RowDeleting" OnSelectedIndexChanging="gvGroupOneVoucher_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="GroupOneCode" HeaderText="Group On Code" />
                                        <asp:BoundField DataField="GroupOneName" HeaderText="Group On Name" />
                                       <%-- <asp:BoundField DataField="AccountName" HeaderText="Account Sub Head" />--%>
                                        <%--  <asp:BoundField DataField="SortOrder" HeaderText="Sort Order" ItemStyle-HorizontalAlign="Right" />--%>
                                        <asp:BoundField DataField="Active" HeaderText="Active" />

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClick="Page_Changed" Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ul>
                        </div>
                      <%--  <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>
    </form>

</body>
</html>
