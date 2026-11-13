<%@ Page Language="C#" AutoEventWireup="true" CodeFile="District.aspx.cs" Inherits="District" EnableEventValidation="false" %>

<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc2" TagName="MedLifePageHelp" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::District</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <script src="js/Navigation.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
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
            $("#<%#ddlCountry.ClientID%>").val("0");
            $("#<%#ddlState.ClientID%>").val("0");

            $("#<%#txtName.ClientID%>").val('');


            $("#<%#hdfAccountHead.ClientID%>").val('');


            $("#<%#txtName.ClientID%>").focus();

        }
        function ClearSearch() {


            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#ddlCountrySearch.ClientID%>").val("0");
            $("#<%#ddlStateSearch.ClientID%>").val("0");

            $("#<%#txtSearchName.ClientID%>").focus();
        }
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();

            $("#<%#txtName.ClientID%>").focus();
        }


        function ButtonClick() {


            $("#<%#btnGridFill.ClientID%>").click();
            $("#<%#ddlState.ClientID%>").focus();

            return false;




        }
        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

        }
        function clearbl() {
            //$("#blMessage").empty();
        }
        function SearchClick() {

            $("#<%#btnDlFill.ClientID%>").click();
            return false;
        }


        function PopupOnReason() {
            try {

                //$("#ContentPlaceHolder1_divPopupOnstores").css('display', 'block');
                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {
                //alert('popup' + ex);  
            }
        }
    </script>
</head>
<body>
    <form id="frmAccountHead" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="user">District</div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                                <uc2:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                                <asp:ImageButton ID="ImageButton1" ImageUrl="images/add.png" CssClass="imagebuttonStyle" runat="server" TabIndex="13" OnClientClick="ShowAddDiv(); ClearAdd();releasebtn();" OnClick="ImageButton1_Click" />

                                <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle" runat="server" TabIndex="12" OnClick="imgbtnView_Click" />
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
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="District Name"></asp:Label>
                                        <asp:TextBox ID="txtName" CssClass="textboxMandatory" runat="server" MaxLength="75" TabIndex="2"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="District Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Country"></asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlCountry" CssClass="dropdownMandatory" TabIndex="3" onBlur="return ButtonClick();">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Country required" ControlToValidate="ddlCountry" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li>
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="State"></asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlState" CssClass="dropdownMandatory" TabIndex="4">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="State required" ControlToValidate="ddlState" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li class="btnMargin">


                                        <asp:Button ID="btnGridFill" runat="server" Text="Hide" OnClick="btnAccountSubFill_Click" Style="display: none;" CausesValidation="false" />
                                    </li>
                                    <li class="btnMargin">
                                        <asp:HiddenField ID="hdfAccountHead" runat="server" />
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="10" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="11" OnClientClick="ClearAdd();" OnClick="btnClear_Click" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:HiddenField ID="hfUsername" runat="server" />
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
                                    <li>

                                        <li>
                                            <asp:Label ID="Label6" runat="server" CssClass="label" Text="District Name"></asp:Label>
                                            <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" TabIndex="15"></asp:TextBox>
                                        </li>

                                        <li>
                                            <label class="label">Country </label>
                                            <asp:DropDownList ID="ddlCountrySearch" runat="server" CssClass="dropdown" TabIndex="16" onBlur="return SearchClick();"></asp:DropDownList>
                                        </li>
                                        <li>
                                            <label class="label">State</label>

                                            <asp:DropDownList runat="server" ID="ddlStateSearch" TabIndex="17" CssClass="dropdown">
                                            </asp:DropDownList>

                                        </li>

                                        <li class="btnMargin">
                                            <asp:Button ID="btnDlFill" runat="server" Text="Hide" OnClick="btnDlFill_Click" CausesValidation="false" Style="display: none;" />
                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="18" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" TabIndex="19" OnClientClick="ClearSearch(); return false;" />
                                        </li>
                                    </li>
                                </ul>
                                <ul class="contentul">
                                    <asp:GridView ID="gvAccountHead" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvAccountHead_PageIndexChanging"
                                        BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ID_District" OnRowDeleting="gvAccountHead_RowDeleting" OnSelectedIndexChanging="gvAccountHead_SelectedIndexChanging">
                                        <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />

                                            <asp:BoundField DataField="DistrictName" HeaderText="District" />
                                            <asp:BoundField DataField="StateName" HeaderText="State" />
                                            <asp:BoundField DataField="CountryName" HeaderText="Country" />


                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageBtnDelete" runat="server" OnClick="ImageBtnDelete_Click" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                        <%--   <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>

                        <asp:Button ID="btnReasonShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divReasonPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divReasonheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="Label1" runat="server" Text="Reason"></asp:Label>
                                <asp:ImageButton ID="btnReasonClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                                <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                            </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divReasonPopup" TargetControlID="btnReasonShow"
                            CancelControlID="btnReasonClose" Drag="true" PopupDragHandleControlID="divReasonheader">
                        </cc:ModalPopupExtender>
                        <asp:Button ID="btnP" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="pnlPrvlg" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divPrvlg" runat="server" class="PopupHeader">
                                <asp:Label ID="Label15" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="btnCP" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <uc1:CommonPopupPrivilege runat="server" ID="CommonPopupPrivilege" />
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpPrivilege" runat="server" PopupControlID="pnlPrvlg" TargetControlID="btnP"
                            CancelControlID="btnCP" Drag="true" PopupDragHandleControlID="divPrvlg">
                        </cc:ModalPopupExtender>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>
    </form>
</body>
</html>
