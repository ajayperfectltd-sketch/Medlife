<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RackAllotment.aspx.cs" Inherits="RackAllotment" %>

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
    <script src="js/Navigation.js"></script>
    <script type="text/javascript">
        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

        }
     
        function PeriodChange() {
           
            Period();
        }
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
        function btnSearch_Click() {
            if (e.keyCode == 13)
                $("#btnSearch").Click();
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


            $("#<%#txtAllotmentDate.ClientID%>").val('');
            $("#<%#txtPeriod.ClientID%>").val('');
            $("#<%#txtDueDate.ClientID%>").val('');
            $("#<%#txtRent.ClientID%>").val('');
            $("#<%#txtRemarks.ClientID%>").val('');
            $("#<%#ddlRack.ClientID%>").val('0');
            $("#<%#ddlSupplier.ClientID%>").val('0');
            $("#<%#ddlPeriodType.ClientID%>").val('0');
            $("#<%#ddlRack.ClientID%>").val('0');
            $("#<%#hfRackAllotmentId.ClientID%>").val('0');
            var toDate = new Date();
            $("#txtAllotmentDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtDueDate").val(toDate.format("dd/MM/yyyy"));
            $("#ddlRack").focus();
            ClearSearch();
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#ddlRack").focus();
            ClearSearch();
        }
        function ClearSearch() {

            $("#<%#ddlSearchSupplier.ClientID%>").val('0');
            $("#<%#ddlSearchSupplier.ClientID%>").focus();
        }

        function Period() {
            try {
                //check type and calculate due date
                var Periodtype = $("#<%#ddlPeriodType.ClientID%>").val();
                var AllotmentDate = $("#<%#txtAllotmentDate.ClientID%>").val();
                var Period = $("#<%#txtPeriod.ClientID%>").val();
                var pp = AllotmentDate.substring(3, 5) + '/' + AllotmentDate.substring(0, 2) + '/' + AllotmentDate.substring(6, 10);
                // alert(pp);
                var _Date = new Date(pp);
                // var _Date = new Date();
                //  alert(_Date);
                //   alert(_Date12);
                // alert(AllotmentDate);
                //   var _Date1 = Date.parse(AllotmentDate);
                //  alert(Date.parse(AllotmentDate));
                //  var _Date = _Date1.format("dd/MM/yyyy");
                //  alert(_Date);
                if (parseInt(Period) >= 0) {
                    if (Periodtype == '1') {
                        _Date.setDate(_Date.getDate() + parseInt(Period));
                        $("#<%#txtDueDate.ClientID%>").val(_Date.format("dd/MM/yyyy"));
                    }

                    else if (Periodtype == '2') {
                        _Date.setMonth(_Date.getMonth() + parseInt(Period));
                        $("#<%#txtDueDate.ClientID%>").val(_Date.format("dd/MM/yyyy"));
                    }
            }
            else {
                $("#txtDueDate").val(_Date.format("dd/MM/yyyy"));

            }

        }
        catch (ex) {
            alert(ex.message);
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">Rack Allotment</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" TabIndex="12" runat="server" OnClientClick="ShowAddDiv(); return false;  releasebtn();" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="11" runat="server" OnClick="imgbtnView_Click" />
                        </div>
                    </div>
                    <%-- <div class="user_bg">
                        <div class="user1">Rack Allotment</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: none;">
                            <ul class="contentul">


                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Rack Name"></asp:Label>


                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlRack" TabIndex="1" AppendDataBoundItems="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Rack name required" ControlToValidate="ddlRack" Display="None"
                                        SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>


                                </li>
                                <li>
                                    <label class="label">Allotment Date</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtAllotmentDate" onchange="PeriodChange()" TabIndex="2" AutoComplete="off" />
                                    <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtAllotmentDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtAllotmentDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li>
                                    <label class="label">Supplier</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlSupplier" TabIndex="3">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqvalSupplier" runat="server" ErrorMessage="Supplier required" ControlToValidate="ddlSupplier" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="reqvalSupplier">
                                    </cc:ValidatorCalloutExtender>

                                </li>





                                <li>
                                    <label class="label">Period Type</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlPeriodType" onchange="PeriodChange()" TabIndex="4">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqvalPeriodType" runat="server" ErrorMessage="Period Type required" ControlToValidate="ddlPeriodType" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="reqvalPeriodType">
                                    </cc:ValidatorCalloutExtender>

                                </li>

                                <li>
                                    <label class="label">Period</label>
                                    <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtPeriod" Style="text-align:right" onkeyup="return Period();" TabIndex="5" AutoComplete="off" />

                                    <asp:RequiredFieldValidator ID="reqvalPeriod" runat="server" ErrorMessage="Period required" ControlToValidate="txtPeriod" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="reqvalPeriod">
                                    </cc:ValidatorCalloutExtender>

                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPeriod" ErrorMessage="Enter valid Period" Display="None" MaximumValue="999999999" SetFocusOnError="true" ValidationGroup="submit" MinimumValue="0" Type="Integer"></asp:RangeValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li>
                                    <label class="label">Due Date</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtDueDate"  TabIndex="6" AutoComplete="off" />
                                 <%--   <cc:CalendarExtender ID="txtDate_CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtDueDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="reqvalDueDate" runat="server" ErrorMessage="Due Date required" ControlToValidate="txtDueDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                    </cc:ValidatorCalloutExtender>--%>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Invalid Date" ControlToValidate="txtDueDate" ControlToCompare="txtAllotmentDate" Type="Date" Operator="GreaterThanEqual" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Rent"></asp:Label>
                                    <asp:TextBox ID="txtRent" TabIndex="7" CssClass="textboxMandatory" runat="server" Style="text-align:right" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqvalRent" runat="server" ErrorMessage="Rent required" ControlToValidate="txtRent" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="reqvalRent">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtRent" ErrorMessage="Enter  valid Rent Amount" Display="None" MaximumValue="999999999999" SetFocusOnError="true" ValidationGroup="submit" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Remarks"></asp:Label>
                                    <asp:TextBox ID="txtRemarks" TabIndex="8" CssClass="textbox" MaxLength="250" runat="server"></asp:TextBox>

                                </li>


                                <li class="btnMargin">


                                    <asp:HiddenField ID="hfRackAllotmentId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                </li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="9" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="10" OnClientClick="ClearAdd(); return false;" />
                                </li>
                        </div>
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>

                                    <li>
                                        <label class="label">Supplier</label>
                                        <asp:DropDownList ID="ddlSearchSupplier" runat="server" CssClass="dropdownMandatory" AutoPostBack="true" TabIndex="13"></asp:DropDownList>


                                    </li>

                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" OnClientClick="return btnSearch_Click();" TabIndex="14" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear"  OnClientClick="ClearSearch(); " OnClick="btnSearch_Click" TabIndex="15" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvRackAllotment" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100"
                                    DataKeyNames="ID_RackAllotment" OnRowDeleting="gvRackAllotment_RowDeleting" OnSelectedIndexChanging="gvRackAllotment_SelectedIndexChanging">

                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="RackName" HeaderText="Rack Name" />
                                        <asp:BoundField DataField="Rent" HeaderText="Rent" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="DueDate" HeaderText="DueDate" />
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
                        <%--<asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
