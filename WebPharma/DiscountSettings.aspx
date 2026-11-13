<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DiscountSettings.aspx.cs" Inherits="DiscountSettings" %>

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

            try {
                submit = 0;
                $("#<%#hfDiscountSettingsId.ClientID%>").val('0');

                $("#<%#ddlEmployee.ClientID%>").val('0');
                $("#<%#ddlCustomer.ClientID%>").val('0');
                $("#<%#ddlItems.ClientID%>").val('0');
                $("#<%#ddlSalesMan.ClientID%>").val('0');
                $("#<%#ddlCustomerGroup.ClientID%>").val("0");
                var toDate = new Date();
                $("#txtFromDate").val(toDate.format("dd/MM/yyyy"));
                $("#txtToDate").val(toDate.format("dd/MM/yyyy"));
                $("#txtPercentage").val("0");
                $("#cbxIsClosed").prop("checked", false);
                $("#txtAmount").val("0");
                $("#txtAmount").prop("visible", true);
                $("#txtPercentage").prop("visible", true);
                $("#txtDisName").val('');

                $("#<%#ddlBrand.ClientID%>").val('0');
                $("#<%#ddlCategoryFirst.ClientID%>").val('0');
                $("#txtDisName").focus();



                ClearSearch();
            }
            catch (e) {
                alert(e);
            }


        }
        function ClearSearch() {
            var toDate = new Date();
            $("#<%#ddlDiscountSearch.ClientID%>").val('0');
            $("#txtFromDateSearch").val(toDate.format("dd/MM/yyyy"));
            $("#txtTodateSearch").val(toDate.format("dd/MM/yyyy"));
            $("#txtDisnameSearch").val('');
            $("#ddlDiscountTypeSearch").val("0");
            $("#txtDisnameSearch").focus();
        }
        function EnableValidate() {
            ValidatorEnable(document.getElementById('rfvitem'), false);
            ValidatorEnable(document.getElementById('rfvCategoryFirst'), false);
            ValidatorEnable(document.getElementById('rfvBrand'), false);
            if ($("#rbtnlstPromotionType_0").is(':checked')) {
                ValidatorEnable(document.getElementById('rfvitem'), true);
            }
            else if ($("#rbtnlstPromotionType_1").is(':checked')) {
                ValidatorEnable(document.getElementById('rfvCategoryFirst'), true);
            }
            else if ($("#rbtnlstPromotionType_2").is(':checked')) {
                ValidatorEnable(document.getElementById('rfvBrand'), true);
            }
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
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">Discount Settings</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" CausesValidation="false" ImageUrl="images/add1.png" Width="37" TabIndex="25" Height="38" runat="server" OnClick="imgAdd_Click" OnClientClick="ShowAddDiv(); return false; releasebtn();" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" CausesValidation="false" ImageUrl="images/view.png" Width="37" TabIndex="24" Height="40" runat="server" OnClick="imgbtnView_Click" />
                        </div>
                    </div>
            
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server">
                            <%-- style="display: none;"--%>
                            <%-- --%>
                            <ul class="contentul">

                                <li>
                                    <asp:Label ID="Label21" runat="server" CssClass="label" Text="Discount"></asp:Label>
                                    <asp:TextBox ID="txtDisName" runat="server" TabIndex="1" CssClass="textboxMandatory" AutoComplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Discount Name Required" ControlToValidate="txtDisName"
                                        Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                    </cc:ValidatorCalloutExtender>

                                    <asp:DropDownList ID="ddlDiscount" Visible="false" runat="server" TabIndex="2" CausesValidation="false" CssClass="dropdownMandatory"></asp:DropDownList>

                                </li>
                                <li>
                                    <asp:Label ID="Label5" runat="server" CssClass="label" Text="Discount Type "></asp:Label>
                                    <asp:RadioButtonList ID="RdDiscountType" runat="server" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="RdDiscountType_SelectedIndexChanged" ValidationGroup="submit">
                                        <asp:ListItem Value="1"  >Employee</asp:ListItem>
                                        <asp:ListItem Value="2">Customer</asp:ListItem>
                                        <asp:ListItem Value="3">Sales Man</asp:ListItem>
                                        <asp:ListItem Value="4">Customer Group</asp:ListItem>
                                        <asp:ListItem Value="5">Items</asp:ListItem>
                                        <asp:ListItem Value="6">CategoryFirst</asp:ListItem>
                                        <asp:ListItem Value="7">Brand</asp:ListItem>
                                    </asp:RadioButtonList>
                                </li>
                                <li id="liEmployee" runat="server" visible="false">
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Employee"></asp:Label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" TabIndex="10" CssClass="dropdownMandatory"></asp:DropDownList>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select an Employee" ControlToValidate="ddlEmployee" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>--%>
                                </li>
                                <li id="liCustomer" runat="server" visible="false">
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Customer"></asp:Label>
                                    <asp:DropDownList ID="ddlCustomer" runat="server" TabIndex="11" CssClass="dropdownMandatory"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select a Customer" ControlToValidate="ddlCustomer" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li id="liItems" runat="server" visible="false">
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Items"></asp:Label>
                                    <asp:DropDownList ID="ddlItems" runat="server" TabIndex="12" CssClass="dropdownMandatory"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select an Items" ControlToValidate="ddlItems" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                    </cc:ValidatorCalloutExtender>
                                </li>


                                <li id="LiCategoryFirst" runat="server" visible="false">
                                    <asp:Label ID="Label17" runat="server" CssClass="label" Text="Category First"></asp:Label>
                                    <asp:DropDownList ID="ddlCategoryFirst" runat="server" CssClass="dropdown" TabIndex="13"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCategoryFirst" runat="server" ErrorMessage="Category First required" ControlToValidate="ddlCategoryFirst" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="rfvCategoryFirst">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li id="LiBrand" runat="server" visible="false">
                                    <asp:Label ID="Label9" runat="server" CssClass="label" Text="Brand"></asp:Label>
                                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="dropdown" TabIndex="44"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="rfvBrand" runat="server" ErrorMessage="Brand required" ControlToValidate="ddlBrand" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="rfvBrand">
                                    </cc:ValidatorCalloutExtender>
                                </li>


                                <li id="liSalesMan" runat="server" visible="false">
                                    <asp:Label ID="Label7" runat="server" CssClass="label" Text="Sales Man"></asp:Label>
                                    <asp:DropDownList ID="ddlSalesMan" runat="server" TabIndex="15" CssClass="dropdownMandatory"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select Sales Man" ControlToValidate="ddlSalesMan" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li id="liCustomerGroup" runat="server" visible="false">
                                    <label class="label">Customer Group</label>
                                    <asp:DropDownList ID="ddlCustomerGroup" CssClass="dropdownMandatory" TabIndex="16" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Select customer group" ControlToValidate="ddlCustomerGroup" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label13" runat="server" CssClass="label" Text="From Date"></asp:Label>
                                    <asp:TextBox ID="txtFromDate" CssClass="textbox" TabIndex="17" runat="server"></asp:TextBox>
                                    <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDate">
                                    </cc:CalendarExtender>
                                    <asp:CompareValidator ID="CompareValidator130" runat="server" ErrorMessage="From Date should be lesser than To Date" Display="None" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" ValidationGroup="submit" Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="CompareValidator130">
                                    </cc:ValidatorCalloutExtender>

                                </li>

                                <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="To Date"></asp:Label>
                                    <asp:TextBox ID="txtToDate" CssClass="textbox" TabIndex="18" runat="server"></asp:TextBox>
                                    <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtToDate">
                                    </cc:CalendarExtender>
                                </li>

                                <li class="btnMargin">
                                    <asp:CheckBox ID="cbxIsClosed" runat="server" Text="Closed" TabIndex="19" />
                                </li>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>


                                        <li id="liPercent" runat="server">
                                            <asp:Label ID="Label12" runat="server" CssClass="label" Text="Percentage"></asp:Label>
                                            <asp:TextBox ID="txtPercentage" CssClass="textbox" runat="server" TabIndex="20" MaxLength="5" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);" AutoPostBack="true" OnTextChanged="txtPercentage_TextChanged"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator41" runat="server" ErrorMessage="Max value 100 and Min value is 0.01" ForeColor="Red" Type="Double" ControlToValidate="txtPercentage" MinimumValue="0.01" MaximumValue="100" ValidationGroup="submit" Display="None"></asp:RangeValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" Enabled="True" TargetControlID="RangeValidator41">
                                            </cc:ValidatorCalloutExtender>
                                        </li>

                                        <li id="liAmount" runat="server">
                                            <asp:Label ID="Label8" runat="server" CssClass="label" Text="Amount"></asp:Label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtAmount" TabIndex="21" Style="text-align: right;" MaxLength="12" onKeypress="CheckNumWithDecimal(event);" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged" />
                                            <asp:RangeValidator ID="RangeValidator22" runat="server" ErrorMessage="Enter valid amount" ForeColor="Red" Type="Double" ControlToValidate="txtAmount" MinimumValue="0.01" MaximumValue="9999999999999" ValidationGroup="submit" Display="None"></asp:RangeValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator22">
                                            </cc:ValidatorCalloutExtender>
                                        </li>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="22" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" CausesValidation="false" Text="Clear" TabIndex="23" OnClientClick="ClearAdd();" OnClick="btnClear_Click" />
                                    <asp:HiddenField ID="hfDiscountSettingsId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                </li>
                            </ul>
                        </div>
                        <div id="divView" runat="server">
                            <%-- style="display: none;"   --%>
                            <ul class="contentul">
                                <li>
                                    <%-- <li>
                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="ShortName"></asp:Label>
                                <asp:TextBox ID="txtSearchShortName" CssClass="textbox" runat="server"></asp:TextBox>
                            </li>--%>
                                    <%--   <li>
                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server"></asp:TextBox>
                            </li>--%>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Discount"></asp:Label>
                                        <asp:TextBox ID="txtDisnameSearch" runat="server" TabIndex="26" CssClass="textbox"></asp:TextBox>
                                        <asp:DropDownList ID="ddlDiscountSearch" Visible="false" runat="server" TabIndex="27" CssClass="dropdown"></asp:DropDownList>
                                    </li>
                                    <li>
                                        <label class="label">From Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtFromDateSearch" TabIndex="28"/>
                                        <cc:CalendarExtender ID="txtFromDateSearch_CalendarExtender" runat="server"   Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFromDateSearch">
                                        </cc:CalendarExtender>
                                    </li>
                                    <li>
                                        <label class="label">To Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtTodateSearch" TabIndex="29"/>
                                        <cc:CalendarExtender ID="txtTodateSearch_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtTodateSearch">
                                        </cc:CalendarExtender>
                                    </li>
                                    <li>
                                        <label class="label">Discount Type</label>
                                        <asp:DropDownList ID="ddlDiscountTypeSearch" runat="server" CssClass="dropdown" TabIndex="30">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="1">Employee</asp:ListItem>
                                            <asp:ListItem Value="2"> Customer</asp:ListItem>
                                            <asp:ListItem Value="3">Items</asp:ListItem>
                                            <asp:ListItem Value="4">Sales Man</asp:ListItem>
                                            <asp:ListItem Value="5">Customer Group</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click"  TabIndex="31"/>
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="32" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvDiscount" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_DiscountSettings" OnRowDeleting="gvDiscount_RowDeleting" OnSelectedIndexChanging="gvDiscount_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Discount" HeaderText="Discount" />
                                        <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                        <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                        <asp:BoundField DataField="DiscountTypeName" HeaderText="Discount Type" />
                                        <asp:BoundField DataField="Percentage" HeaderText="Percentage" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                        <asp:BoundField DataField="Closed" HeaderText="Closed" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
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
                      <%--  <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>

                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
