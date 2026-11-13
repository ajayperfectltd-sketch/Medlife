<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Machine.aspx.cs" Inherits="Machine" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
     <script src="js/Navigation.js"></script>
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

        function LoadStore() {
            $("#btnHiddenLoadStore").click();

        }
        function PopupOnReason() {
            try {

                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {
                
            }
        }

        function ClearAdd() {
            submit = 0;
            $("#<%#txtMecName.ClientID%>").val('');
            $("#<%#txtMecIp.ClientID%>").val('');
            $("#<%#txtMecAddress.ClientID%>").val('');
            $('#<%#chkAutoPrint.ClientID%>').prop('checked', true);
            $("#ddlCounter").val("0");
            $("#ddlStore").val("0");
            $("#ddlShift").val("0");
            $("#ddlBillType").val("0");
            $("#ddlPrint").val("0");

            $("#<%#hfMachineId.ClientID%>").val('0');
            
            $("#<%#txtMecName.ClientID%>").focus();
        }
        function ClearSearch() {
            $("#<%#txtSearchMecName.ClientID%>").val('');
            $("#<%#txtSearchMecIp.ClientID%>").val('');
            $("#<%#txtSearchMecName.ClientID%>").focus();
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtMecName.ClientID%>").focus();
        }



        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }
        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5"> 
                    <div class="heading2">
                  <%--  <div class="icon_bg">--%>
                        <div class="user">Machine</div>
                          <div style="float: right; margin-top: -10px">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/add.png" CssClass="imagebuttonStyle"  runat="server" TabIndex="12" OnClientClick="ShowAddDiv();ClearAdd(); return false;  releasebtn();" />
                        
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png"  CssClass="imagebuttonStyle" runat="server" TabIndex="11" OnClick="imgbtnView_Click" OnClientClick=" ClearSearch();" />
                        </div>
                        </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                         <div class="main_bg">  
                        <div id="divAdd" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Machine"></asp:Label>
                                    <asp:TextBox ID="txtMecName" CssClass="textboxMandatory" runat="server" MaxLength="40" TabIndex="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Machine Name required" ControlToValidate="txtMecName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="IP"></asp:Label>
                                    <asp:TextBox ID="txtMecIp" CssClass="textboxMandatory" runat="server" MaxLength="30" TabIndex="4"></asp:TextBox>
                                    <cc:FilteredTextBoxExtender ID="txtROQuantity_FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtMecIp" FilterType="Custom , Numbers" ValidChars=".">
                                    </cc:FilteredTextBoxExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Machine IP required" ControlToValidate="txtMecIp" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Address"></asp:Label>
                                    <asp:TextBox ID="txtMecAddress" onkeyDown="checkTextAreaMaxLength(this,event,'45');" CssClass="textboxMandatory" TextMode="MultiLine" runat="server" MaxLength="45" TabIndex="5"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Machine Address required" ControlToValidate="txtMecAddress" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                    </cc:ValidatorCalloutExtender>

                                </li>
                                <li>
                                    <asp:Label ID="Label26" runat="server" CssClass="label" Text="Counter"></asp:Label>
                                    <asp:DropDownList ID="ddlCounter" runat="server" Onblur="LoadStore();" CssClass="dropdownMandatory" TabIndex="6"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Select a Counter" ControlToValidate="ddlCounter" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender37" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator22">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:Button ID="btnHiddenLoadStore" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnHiddenLoadStore_Click" />

                                </li>
                                <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Store"></asp:Label>
                                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="dropdownMandatory" TabIndex="7"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select a Store" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                 <li>
                                    <asp:Label ID="Label8" runat="server" CssClass="label" Text="Shift"></asp:Label>
                                    <asp:DropDownList ID="ddlShift" runat="server" CssClass="dropdown" TabIndex="7"></asp:DropDownList>
                                </li>
                                 <li>
                                    <asp:Label ID="Label9" runat="server" CssClass="label" Text="Bill Type"></asp:Label>
                                    <asp:DropDownList ID="ddlBillType" runat="server" CssClass="dropdown" TabIndex="7"></asp:DropDownList>
                                </li>
                                <li>
                                    <asp:Label ID="Label7" runat="server" CssClass="label" Text="Print Layout"></asp:Label>
                                    <asp:DropDownList ID="ddlPrint" runat="server" CssClass="dropdownMandatory" TabIndex="8"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select a Print Layout" ControlToValidate="ddlPrint" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                
                                <li class="btnMargin">
                                    <asp:CheckBox ID="chkAutoPrint" runat="server" TabIndex="10" CssClass="chckbox" Text="Auto Print" />
                                </li>

                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="10" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" TabIndex="11" Text="Clear" OnClientClick="ClearAdd(); return false;" />
                                    <asp:HiddenField ID="hfMachineId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                     <asp:HiddenField ID="hfuserName" runat="server" />
                                </li>
                            </ul>
                        </div>
                             </div>
                         <div class="main_bg">  
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Machine"></asp:Label>
                                        <asp:TextBox ID="txtSearchMecName" CssClass="textbox" runat="server" MaxLength="40" TabIndex="13"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="IP"></asp:Label>
                                        <asp:TextBox ID="txtSearchMecIp" CssClass="textbox" MaxLength="30" runat="server" TabIndex="14"></asp:TextBox>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="15" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" TabIndex="16" OnClientClick="ClearSearch(); return false;" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvMachine" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_Machine" OnRowDeleting="gvMachine_RowDeleting" OnSelectedIndexChanging="gvMachine_SelectedIndexChanging">
                                    <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="MecName" HeaderText="Machine" />
                                        <asp:BoundField DataField="MecIP" HeaderText="IP" />
                                        <asp:BoundField DataField="MecAddress" HeaderText="Address" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
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
                        <%--  <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                    <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                    </div>
                </div>
                    <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Reason" CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopup1" runat="server" />
                                </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                        </cc:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
