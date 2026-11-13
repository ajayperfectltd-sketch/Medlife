<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bank.aspx.cs" Inherits="Bank" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            submit = 0;
            $("#<%#txtShortName.ClientID%>").val('');
            $("#<%#txtName.ClientID%>").val('');
            $("#<%#txtAddress1.ClientID%>").val('');
            $("#<%#txtAddress2.ClientID%>").val('');
            $("#<%#txtAddress3.ClientID%>").val('');
            $("#<%#hfBankId.ClientID%>").val('0');
            $('#<%#chkActive.ClientID%>').prop('checked', true);
            $("#<%#txtShortName.ClientID%>").focus();
        }
        function ClearSearch() {
            $("#<%#txtSearchShortName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchShortName.ClientID%>").focus();
        }
        function ShowAddDiv() {
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtShortName.ClientID%>").focus();
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
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div> 
                      

                    <div>
                        <div class="icon_bg">
                            <div class="search" style="width: 670px;">Bank</div>
                            <div class="add">Add</div>
                            <div class="add_icon">
                                <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" TabIndex="10" OnClientClick="ShowAddDiv(); ClearAdd();   return false; releasebtn();" />
                            </div>
                            <div class="search">View</div>
                            <div class="search_icon">
                                <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" TabIndex="9" OnClick="imgbtnView_Click" />
                            </div>

                        </div>
                        <%--    <div class="user_bg">
                        <div class="user1">Bank</div>
                    </div>--%>
                        <div class="clear"></div>
                        <div class="group_bg2">
                            <div id="divAdd" runat="server" style="display: none;">
                                <ul class="contentul">
                                    <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                        <asp:TextBox ID="txtShortName" CssClass="textboxMandatory" runat="server" MaxLength="10" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Short name required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtName" CssClass="textboxLongMandatory" runat="server" MaxLength="220" TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Address1"></asp:Label>
                                        <asp:TextBox ID="txtAddress1" CssClass="textboxLong" runat="server" MaxLength="90" TabIndex="3"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Address2"></asp:Label>
                                        <asp:TextBox ID="txtAddress2" CssClass="textboxLong" runat="server" MaxLength="90" TabIndex="4"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Address3"></asp:Label>
                                        <asp:TextBox ID="txtAddress3" CssClass="textboxLong" runat="server" MaxLength="90" TabIndex="5"></asp:TextBox>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:CheckBox ID="chkActive" runat="server" CssClass="chckbox" Text="Active" TabIndex="6" />
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" TabIndex="7" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="8" />
                                        <asp:HiddenField ID="hfBankId" runat="server" />
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
                                            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                            <asp:TextBox ID="txtSearchShortName" CssClass="textbox" runat="server" MaxLength="10" TabIndex="11"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                            <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" MaxLength="220" TabIndex="12"></asp:TextBox>
                                        </li>
                                        <li class="btnMargin">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="13" />
                                            <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="14" />
                                        </li>
                                    </li>
                                </ul>
                                <ul class="contentul">
                                    <asp:GridView ID="gvBank" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_Bank" OnRowDeleting="gvBank_RowDeleting" OnSelectedIndexChanging="gvBank_SelectedIndexChanging">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center"/>
                                            <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                            <asp:TemplateField HeaderText="Name ">
                                                <ItemTemplate>
                                                    <div style="width: 200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                        <%# Eval("Name") %>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Active" HeaderText="Active" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px"  />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                         <%--   <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                            <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>

                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
