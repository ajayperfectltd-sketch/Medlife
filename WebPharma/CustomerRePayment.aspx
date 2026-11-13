<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerRePayment.aspx.cs" Inherits="CustomerRePayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register src="Popup/CommonPopup.ascx" tagname="CommonPopup" tagprefix="uc1" %>
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
            submit = 0;
            $("#<%#txtcustomer.ClientID%>").val('');
            $("#<%#lblTransType.ClientID%>").text('');
            $("#<%#txtAmount.ClientID%>").val('0');
            $("#<%#txtBalance.ClientID%>").val('0'); 
            $("#<%#hfCustRepayId.ClientID%>").val('0');
            $("#<%#hfFK_Customer.ClientID%>").val('0');
            $("#<%#ddlTransType.ClientID%>").val('P');
            $("#<%#ddlStore.ClientID%>").val('0');
            $("#<%#ddlCounter.ClientID%>").val('0');
            ClearSearch();
            $("#<%#txtcustomer.ClientID%>").focus();
        }
        
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtcustomer.ClientID%>").focus();
        }
        function ClearSearch() {
            $("#<%#txtSearchCustName.ClientID%>").val('');
            $("#<%#ddlSearchTransType.ClientID%>").val('');
            $("#<%#txtSearchCustName.ClientID%>").focus();
        }
        function popUpCustomerSelect() {

            try {

                var customerName = $("#txtcustomer").val();
                if (customerName.length > 0) {
                    $("#ImageButton4").click();
                }

            }
            catch (e) {
                alert(e);
            }

        }
        function LoadStore() {
            $("#btnHiddenLoadStore").click();

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
                        <div class="search" style="width: 670px;">Re-Payment</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" TabIndex="11" OnClientClick="ShowAddDiv(); ClearAdd();   return false; releasebtn();" OnClick="ImageButton1_Click" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="10" runat="server" OnClick="imgbtnView_Click" />
                        </div>

                    </div>
                    <%--  <div class="user_bg">
                        <div class="user1">Color</div>
                    </div>--%>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: block;">
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label61" runat="server" CssClass="label"  Text="Customer"></asp:Label>
                                        <asp:TextBox ID="txtcustomer" runat="server" AutoPostBack="false" CssClass="textboxLongMandatory" onBlur="popUpCustomerSelect();" TabIndex="1" MaxLength="50" Width="300px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" TabIndex="2" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 0px 0px 0px 6px; position: absolute" />
                                        <asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ErrorMessage="Select a Customer" ControlToValidate="txtcustomer" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender59" runat="server" Enabled="True" TargetControlID="rfvCustomer">
                                        </cc:ValidatorCalloutExtender>                                    
                                          <asp:RequiredFieldValidator ID="rfvCustomer1" runat="server" ErrorMessage="Select a Supplier" ControlToValidate="txtcustomer" Display="None" SetFocusOnError="True" ValidationGroup="smartfill"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="rfvCustomer1">
                                        </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Balance"></asp:Label>
                                    <asp:TextBox ID="txtBalance" CssClass="textboxMandatory" runat="server" Enabled="false" onKeypress="CheckNumWithDecimal(event);" MaxLength="250" TabIndex="3"></asp:TextBox>
                                    <asp:Label ID="lblTransType" runat="server" Text="Label"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name required" ControlToValidate="txtBalance" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label26" runat="server" CssClass="label" Text="Counter"></asp:Label>
                                    <asp:DropDownList ID="ddlCounter" runat="server" Onblur="LoadStore();" CssClass="dropdownMandatory" TabIndex="4"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Select a Counter" ControlToValidate="ddlCounter" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender37" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator22">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:Button ID="btnHiddenLoadStore" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnHiddenLoadStore_Click" />

                                </li>
                                <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Store"></asp:Label>
                                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="dropdownMandatory" TabIndex="5"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select a Store" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                        <label class="label">Trans Type</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlTransType" TabIndex="6" Width="161px">
                                            <asp:ListItem Value="P">Payment</asp:ListItem>
                                            <asp:ListItem Value="R">Receipt</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Amount"></asp:Label>
                                    <asp:TextBox ID="txtAmount" CssClass="textboxMandatory" runat="server" onKeypress="CheckNumWithDecimal(event);" MaxLength="250" TabIndex="7"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Amount required" ControlToValidate="txtAmount" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li style="margin-left:135px">
                                    <asp:CheckBox ID="chkPrint" runat="server" Text="Print"/>
                                </li>

                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="8" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="9" />
                                    <asp:HiddenField ID="hfCustRepayId" runat="server" />
                                    <asp:HiddenField ID="hfFK_Customer" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                </li>
                            </ul>
                        </div>
                        <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                            <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 300px; height: 370px;">
                                <div id="divheader" runat="server" class="PopupHeader">
                                    <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head" CssClass="lblPopupHeader"></asp:Label>
                                    <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                </div>

                                <uc1:commonpopup id="CommonPopup1" runat="server" />
                            </asp:Panel>
                            <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                                CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                            </cc:ModalPopupExtender>


                        <div class="clear"></div>
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Customer Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchCustName" CssClass="textbox" runat="server" MaxLength="10" TabIndex="12"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Trans Type"></asp:Label>
                                       <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlSearchTransType" TabIndex="13" Width="161px">
                                           <asp:ListItem Value="">Select</asp:ListItem>
                                            <asp:ListItem Value="P">Payment</asp:ListItem>
                                            <asp:ListItem Value="R">Receipt</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="14" Text="Search" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" TabIndex="15" OnClick="btnSearchClear_Click" />
                                        
                                    </li>
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />

                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvRepayment" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_CustomerRePayment"  OnRowCommand="gvRepayment_RowCommand">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="CustName" HeaderText="Customer Name" />
                                        <asp:BoundField DataField="TransType" HeaderText="Trans Type" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                                        <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>

                                                <asp:ImageButton ID="ImageButton343" runat="server" CommandName="Print" ToolTip="Print" ImageUrl="~/images/1430738477_report.png" Width="20px" Height="20px" />

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
                       <%-- <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>