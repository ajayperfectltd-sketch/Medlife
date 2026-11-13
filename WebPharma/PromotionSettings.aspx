<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PromotionSettings.aspx.cs" Inherits="PromotionSettings" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Promotion Settings::</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />

    <script type="text/javascript">
        var submit = 0;
        function disablebutn() {
            try {
                EnableValidate();
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

            $("#divAdd [id*='txt']").val('');
            $("#divAdd [id*='ddl']").val('0');
            $("#hdfPromotionSettings").val('');
            var date = new Date();
            $('#txtEffectedDate,#txtClosedDate').val(date.format("dd/MM/yyyy"));
            $("#cbxClosed").prop("checked", false);

            $("#LiItems").attr("style", "display:block;");
            $("#LiCategoryFirst").attr("style", "display:none;");
            $("#LiBrand").attr("style", "display:none;");

            //$("#rbtnlstPromotionType_0").attr('checked', 'checked');
            //  $("#rbtnlstPromotionType_2").prop('checked', false);
            $("#rbtnlstPromotionType_0").prop('checked', true);
            //  $("#rbtnlstPromotionType_0").select('checked')
            $("#<%#ddlBrand.ClientID%>").val('0');
            $("#<%#ddlCategoryFirst.ClientID%>").val('0');
            $("#<%#ddlItem.ClientID%>").val('0');
            $("#<%#ddlPromotionType.ClientID%>").focus();

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
        function Display() {

            $("#LiItems").attr("style", "display:none;");
            $("#LiCategoryFirst").attr("style", "display:none;");
            $("#LiBrand").attr("style", "display:none;");
            if ($("#rbtnlstPromotionType_1").is(':checked')) {
                $("#LiCategoryFirst").attr("style", "display:block;");
            }
            else if ($("#rbtnlstPromotionType_2").is(':checked')) {
                $("#LiBrand").attr("style", "display:block;");

            }
            else {
                $("#LiItems").attr("style", "display:block;");
            }

        }
        function ClearSearch() {
            $("#divView  [id*='ddl']").val('0');
            $("#ddlPromotionTypeSearch").focus();
        }
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#ddlPromotionType.ClientID%>").focus();
        }
        function clearbl() {
            $("#blMessage").empty();
        }
        function ClientValidate(source, arguments) {
            try {
                var date = document.getElementById("txtEffectedDate").value;
                var arr = date.split('/');
                var start = new Date(arr[2], arr[1], arr[0]);
                var arr2 = arguments.Value.split('/');

                var endDate = new Date(arr2[2], arr2[1], arr2[0]);

                if (start > endDate) {
                    arguments.IsValid = false;
                }
                else {
                    arguments.IsValid = true;
                }

            }
            catch (e) {
                alert(e);
            }

        }
        var popup;
        function SelectName() {
            popup = window.open("ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val(), "Popup", "width=1000,height=800,scrollbars=yes ");
            popup.focus();
            return false;
        }

    </script>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="frmPromotionSettings" runat="server" autocomplete="off">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">Promotion Settings</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" TabIndex="12" OnClientClick="ShowAddDiv(); ClearAdd(); clearbl(); return false;  releasebtn();" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" TabIndex="11" ImageUrl="images/view.png" Width="37" Height="40" runat="server" OnClick="imgbtnView_Click" />
                        </div>

                    </div>
                    <%-- <div class="user_bg">
                        <div class="user1">Promotion Settings</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server">
                            <%--"--%>
                            <%--style="display: none;"--%>
                            <ul class="contentul">

                                <li>
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Promotion Type"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlPromotionType" CssClass="dropdownMandatory" TabIndex="1">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Promotion type required" ControlToValidate="ddlPromotionType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li class="btnMargin">
                                    <asp:RadioButtonList ID="rbtnlstPromotionType" RepeatDirection="Horizontal" onchange="Display();" Font-Bold="true" ForeColor="#0C3C64" Font-Size="14px" runat="server" TabIndex="2">
                                        <asp:ListItem Text="Items" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Category" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Brand" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </li>
                                <li id="LiItems" runat="server">
                                    <asp:Label ID="Label8" runat="server" CssClass="label" Text="Items"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlItem" CssClass="dropdownMandatory" TabIndex="3">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvitem" runat="server" ErrorMessage="Items required" ControlToValidate="ddlItem" InitialValue="0" Display="None" Enabled="false" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="rfvitem">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:ImageButton ID="imgPopUp" runat="server" OnClientClick="return SelectName();" ImageUrl="~/images/Search_1.png" Style="padding-left: 2px; margin-top: 0px; width: 30px; height: 25px;" />
                                </li>
                                <li id="LiCategoryFirst" runat="server">
                                    <asp:Label ID="Label17" runat="server" CssClass="label" Text="Category First"></asp:Label>
                                    <asp:DropDownList ID="ddlCategoryFirst" runat="server" CssClass="dropdown" TabIndex="10"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCategoryFirst" runat="server" ErrorMessage="Category First required" Enabled="false" ControlToValidate="ddlCategoryFirst" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="rfvCategoryFirst">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li id="LiBrand" runat="server">
                                    <asp:Label ID="Label13" runat="server" CssClass="label" Text="Brand"></asp:Label>
                                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="dropdown" TabIndex="15"></asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="rfvBrand" runat="server" ErrorMessage="Brand required" ControlToValidate="ddlBrand" Enabled="false" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="rfvBrand">
                                    </cc:ValidatorCalloutExtender>
                                </li>







                                <li>
                                    <label class="label">Effect Date</label>
                                    <asp:TextBox ID="txtEffectedDate" CssClass="textbox" runat="server" TabIndex="4"></asp:TextBox>
                                    <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtEffectedDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Effected date required" ControlToValidate="txtEffectedDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:CompareValidator ID="cmpEffectDate" runat="server" ErrorMessage="Effect date cannot be greater than Close date" ControlToCompare="txtClosedDate" ControlToValidate="txtEffectedDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="cmpEffectDate">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li style="display: none;">
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Discount Percent"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtDiscountPercent" onkeypress="CheckNumWithDecimal(event);" CssClass="textbox" TabIndex="5" />
                                    <asp:RangeValidator ID="rgvDecimal" runat="server" Display="None" ErrorMessage="Value cannot be greater than 100" ValidationGroup="submit" ControlToValidate="txtDiscountPercent" SetFocusOnError="True" MaximumValue="100" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="rgvDecimal">
                                    </cc:ValidatorCalloutExtender>

                                </li>
                                <li>
                                    <label class="label">Buy</label>
                                    <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtBuy" Style="text-align: right;" onkeypress="CheckInteger(event);" TabIndex="6" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter buy quantity" ControlToValidate="txtBuy" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter quantity greater than 0" ControlToValidate="txtBuy" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <label class="label">Get</label>
                                    <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtGet" Style="text-align: right;" onkeypress="CheckInteger(event);" TabIndex="7" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter get quantity" ControlToValidate="txtGet" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter quantity greater than 0" ControlToValidate="txtGet" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li class="btnMargin">

                                    <asp:CheckBox ID="cbxClosed" runat="server" CssClass="chckbox" Text="Closed" TabIndex="7" />
                                </li>
                                <li>
                                    <label class="label">Close Date</label>
                                    <asp:TextBox ID="txtClosedDate" runat="server" CssClass="textbox" TabIndex="8"></asp:TextBox>
                                    <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtClosedDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                    <asp:CustomValidator ID="csvDatecheck" runat="server" ErrorMessage="Closed date cannot be less than the Effect Date" ClientValidationFunction="ClientValidate" ControlToValidate="txtClosedDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:CustomValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="csvDatecheck">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li class="clear"></li>
                                <li class="btnMargin">
                                    <asp:HiddenField ID="hdfPromotionSettings" runat="server" />
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" TabIndex="9" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="10" OnClientClick="ClearAdd(); return false;" />
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
                            <%--style="display: none;"--%>
                            <ul class="contentul">
                                <li>

                                    <li>
                                        <label class="label">Promotion Type </label>
                                        <asp:DropDownList ID="ddlPromotionTypeSearch" runat="server" CssClass="dropdown" TabIndex="13"></asp:DropDownList>
                                    </li>



                                    <li>
                                        <label class="label">Item </label>
                                        <asp:DropDownList ID="ddlItemearch" runat="server" CssClass="dropdown" TabIndex="14"></asp:DropDownList>
                                    </li>

                                    <li class="btnMargin">

                                        <asp:Button ID="btnSearch" runat="server" TabIndex="15" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchClear" runat="server" TabIndex="16" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                    </li>

                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvPromotionSettings" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_PromotionSettings" OnRowDeleting="gvPromotionSettings_RowDeleting" OnSelectedIndexChanging="gvPromotionSettings_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PromotionType" HeaderText="PromotionType" />
                                        <asp:BoundField DataField="PSType" HeaderText="Type" />
                                        <asp:BoundField DataField="Items" HeaderText="Items/Category/Brand" />
                                        <asp:BoundField DataField="EffectDate" HeaderText="Effect Date" />
                                        <asp:BoundField DataField="Buy" HeaderText="Buy" />
                                        <asp:BoundField DataField="Get" HeaderText="Get" />
                                        <asp:BoundField DataField="Closed" HeaderText="Closed" />
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
