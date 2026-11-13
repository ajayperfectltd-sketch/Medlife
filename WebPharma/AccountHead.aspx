<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountHead.aspx.cs" Inherits="AccountHead" EnableEventValidation="false" %>

<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc1" TagName="MedLifePageHelp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Account Head</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
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
        function pageLoad() {

            var ctrContainer = "form";
            //alert('h');
            setKeynavigation(ctrContainer);
            //alert('k');
        }
        function PopupOnAccountHead() {
            try {

                //$("#ContentPlaceHolder1_divPopupOnstores").css('display', 'block');
                $("#divPopupOnAccountHead").css('display', 'block');
            }
            catch (ex) {
                //alert('popup' + ex);  
            }
        }


        function ClearAdd() {
            submit = 0;
            $("#<%#ddlAccountGroup.ClientID%>").val("0");
            $("#<%#ddlAccountSubGroup.ClientID%>").val("0");


            $("#<%#txtShortName.ClientID%>").val('');
            $("#<%#txtName.ClientID%>").val('');
            $("#<%#ddlAccountType.ClientID%>").val("0");

            $("#<%#hdfAccountHead.ClientID%>").val('');
            $("#<%#txtSortOrder.ClientID%>").val('');
            $("#<%#txtShortName.ClientID%>").focus();

            $("#cbxCashInHand").prop("checked", false);
        }
        function ClearSearch() {

            $("#<%#txtSearchShortName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#ddlAccountGroupSearch.ClientID%>").val("0");
            $("#<%#ddlAccSubGroupSearch.ClientID%>").val("0");
            $("#<%#ddlAccountTypeSearch.ClientID%>").val("0");
            $("#<%#txtSearchShortName.ClientID%>").focus();
        }
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtShortName.ClientID%>").focus();
        }


        function ButtonClick() {


            $("#<%#btnGridFill.ClientID%>").click();
            $("#<%#ddlAccountSubGroup.ClientID%>").focus();

            return false;




        }
        function clearbl() {
            //$("#blMessage").empty();
        }
        function SearchClick() {

            $("#<%#btnDlFill.ClientID%>").click();
            return false;
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
    <form id="frmAccountHead" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5">

                    <div class="heading2">
                        <div class="user">Account Head</div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <uc1:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                                <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" Width="37" Height="38" runat="server" TabIndex="13" OnClientClick="ShowAddDiv(); ClearAdd();releasebtn();" OnClick="ImageButton1_Click" />

                                <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" TabIndex="12" OnClick="imgbtnView_Click" />
                            </div>
                        </asp:Panel>
                    </div>



                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                            <div id="divAdd" runat="server" style="display: block;">
                                <%--style="display: none;"--%>
                                <%--style="display: none;"--%>
                                <ul class="contentul">
                                    <li>
                                        <label class="label">Account Head Code</label>
                                        <asp:TextBox ID="txtAccountHeadCode" CssClass="textbox" runat="server" MaxLength="50" TabIndex="5" Width="215px" ReadOnly="True"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Account Head Code required" ControlToValidate="txtAccountHeadCode" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>--%>
                                    </li>

                                    <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                        <asp:TextBox ID="txtShortName" CssClass="textboxMandatory" runat="server" MaxLength="5" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Short name required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtName" CssClass="textboxMandatory" runat="server" MaxLength="25" TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Account Group"></asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlAccountGroup" CssClass="dropdownMandatory" TabIndex="3" onBlur="return ButtonClick();">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Account Group required" ControlToValidate="ddlAccountGroup" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li>
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="Account Sub Group"></asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlAccountSubGroup" CssClass="dropdown" TabIndex="4">
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Account Sub Group required" ControlToValidate="ddlAccountSubGroup" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                    </cc:ValidatorCalloutExtender>--%>
                                    </li>


                                    <li>
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Sort Order"></asp:Label>
                                        <asp:TextBox runat="server" ID="txtSortOrder" Style="text-align: right;" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="6" MaxLength="5" />


                                    </li>
                                    <%--    <li></li>--%>
                                    <li>
                                        <label class="label">Account Type</label>
                                        <asp:DropDownList runat="server" ID="ddlAccountType" CssClass="dropdownMandatory" TabIndex="6">
                                            <asp:ListItem Selected="True" Value="0">select</asp:ListItem>
                                            <asp:ListItem Value="1">NORMAL</asp:ListItem>
                                            <asp:ListItem Value="2">SYSTEM</asp:ListItem>
                                            <asp:ListItem Value="3">BANK</asp:ListItem>
                                            <asp:ListItem Value="4">CASH</asp:ListItem>
                                            <asp:ListItem Value="5">CUSTOMER</asp:ListItem>
                                            <asp:ListItem Value="6">SUPPLIER</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Account Type required" ControlToValidate="ddlAccountType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <label class="label">Cash in Hand</label>
                                        <asp:CheckBox ID="cbxCashInHand" runat="server" TabIndex="7" />
                                    </li>

                                    <asp:Button ID="btnGridFill" runat="server" Text="Hide" OnClick="btnAccountSubFill_Click" Style="display: none;" CausesValidation="false" />

                                    <li class="btnMargin">
                                        <asp:HiddenField ID="hdfAccountHead" runat="server" />
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="10" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="11" OnClientClick="ClearAdd();" OnClick="btnClear_Click" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:HiddenField ID="hfMasterID" runat="server" />

                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="main_bg">
                            <div id="divView" runat="server" style="display: none;">
                                <%--style="display: none;"--%>
                                <%--style="display: none;"--%>
                                <ul class="contentul">
                                    <div class="divL">
                                        <li>
                                            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                            <asp:TextBox ID="txtSearchShortName" TabIndex="14" CssClass="textbox" runat="server"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                            <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" TabIndex="15"></asp:TextBox>
                                        </li><li></li>
                                        <li class="btnMargin">
                                            <asp:Button ID="btnDlFill" runat="server" Text="Hide" OnClick="btnDlFill_Click" CausesValidation="false" Style="display: none;" />
                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="18" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" TabIndex="19" OnClientClick="ClearSearch(); return false;" />
                                        </li>
                                    </div>
                                    <div class="divR">
                                        <li>
                                            <label class="label">Account Group </label>
                                            <asp:DropDownList ID="ddlAccountGroupSearch" runat="server" CssClass="dropdown" TabIndex="16" onBlur="return SearchClick();"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <label class="label">Account Sub Group</label>

                                            <asp:DropDownList runat="server" ID="ddlAccSubGroupSearch" TabIndex="17" CssClass="dropdown">
                                            </asp:DropDownList>

                                        </li>
                                        <li>
                                            <label class="label">Account Type</label>
                                            <asp:DropDownList runat="server" ID="ddlAccountTypeSearch" CssClass="dropdown" TabIndex="18">
                                                <asp:ListItem Selected="True" Value="0">select</asp:ListItem>
                                                <asp:ListItem Value="1">NORMAL</asp:ListItem>
                                                <asp:ListItem Value="2">SYSTEM</asp:ListItem>
                                                <asp:ListItem Value="3">BANK</asp:ListItem>
                                                <asp:ListItem Value="4">CASH</asp:ListItem>
                                                <asp:ListItem Value="5">CUSTOMER</asp:ListItem>
                                                <asp:ListItem Value="6">SUPPLIER</asp:ListItem>
                                            </asp:DropDownList>
                                        </li>
                                        
                                    </div>
                                </ul>
                                <ul class="contentul">
                                    <asp:GridView ID="gvAccountHead" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False" 
                                        BackColor="White" BorderColor="Teal" BorderStyle="Solid" AllowPaging="true" OnPageIndexChanging="gvAccountHead_PageIndexChanging" 
                                        BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical"
                                         PageSize="10" DataKeyNames="ID_AccountHead" OnRowDeleting="gvAccountHead_RowDeleting" 
                                        OnSelectedIndexChanging="gvAccountHead_SelectedIndexChanging">
                                        <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                            <asp:BoundField DataField="HeadName" HeaderText="Name" />
                                            <asp:BoundField DataField="AhCode" HeaderText="Code" />
                                            <asp:BoundField DataField="SortOrder" HeaderText="Sort Order" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="AcGrpName" HeaderText="Account Group" />
                                            <asp:BoundField DataField="AcSubGrpName" HeaderText="Account Sub Group" />
                                            <asp:BoundField DataField="AccSubType" HeaderText="Account Type" />


                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClick="ImageButton2_Click" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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

                                </ul>
                            </div>
                        </div>
                        <%--   <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                    </div>
                </div>
                <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                    <div id="divheader" runat="server" class="PopupHeaderReason">
                        <asp:Label ID="lblPopupHeader" runat="server" Text="Reason"></asp:Label>
                        <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                    </div>
                    <div id="divPopupOnAccountHead" runat="server" class="divPopupOnstores" style="display: none;">
                        <uc2:CommonPopupDelete ID="CommonPopup1" runat="server" />
                    </div>
                </asp:Panel>
                <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                    CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                </cc:ModalPopupExtender>


                <asp:Button ID="btnP" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                <asp:Panel ID="pnlPrvlg" runat="server" Style="display: none;" CssClass="DivStyle">
                    <div id="divPrvlg" runat="server" class="PopupHeader">
                        <asp:Label ID="Label7" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
                        <asp:ImageButton ID="btnCP" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                    </div>
                    <uc1:CommonPopupPrivilege runat="server" ID="CommonPopupPrivilege" />
                </asp:Panel>
                <cc:ModalPopupExtender ID="mpPrivilege" runat="server" PopupControlID="pnlPrvlg" TargetControlID="btnP"
                    CancelControlID="btnCP" Drag="true" PopupDragHandleControlID="divPrvlg">
                </cc:ModalPopupExtender>

            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>
    </form>
</body>
</html>
