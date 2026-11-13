<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Units.aspx.cs" Inherits="Units" EnableEventValidation="false" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc2" TagName="MedLifePageHelp" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Unit::</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/alertify.min.js"></script>
     <script src="js/Navigation.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
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
            
            $("#<%#txtName.ClientID%>").val('');
            $("#<%#txtNofUnits.ClientID%>").val('');
            $("#<%#ddlCategoryFirst.ClientID%>").val('0');
            $("#<%#hdfUnitId.ClientID%>").val('');
            $("#<%#cbxIsBaseUnit.ClientID%>").attr("Checked", false);
            $("#<%#txtName.ClientID%>").focus();
        }
        function ClearSearch() {
            
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#ddlCtaegorySearch.ClientID%>").val('0');
            $("#<%#txtSearchName.ClientID%>").focus();
        }
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#cbxIsBaseUnit").change(function () {

                ChangeValue();
            });
            $("#<%#txtSearchName.ClientID%>").focus();
        }
        function PopupOnReason() {
            try {


                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {

            }
        }

        $(document).ready(function () {
            $("#cbxIsBaseUnit").change(function () {

                ChangeValue();
            });
        });
        function ChangeValue() {
            if ($("#cbxIsBaseUnit").prop("checked")) {

                if ($("#ddlCategoryFirst :selected").val() != '0') {
                    GetDefaultunit();
                }

            }

        }
        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

        }

        function GetDefaultunit() {
            var FK_CategoryFirst = $("#ddlCategoryFirst :selected").val();

            try {
                $.ajax({
                    type: "POST",
                    url: 'Textile.asmx/GetCategoryBaseUnit',
                    data: "{FK_CategoryFirst :'" + FK_CategoryFirst + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.d != '') {

                            var arr = (data.d).split('-');
                            if ($("#hdfUnitId").val() != '') {
                                if ($("#txtName").val() != arr[0]) {
                                    if (alertify.confirm(data.d + "  has already been set  as  base unit under the category do you want to change it?")) {
                                        $("#cbxIsBaseUnit").attr("checked", "checked");
                                    }
                                    else {
                                        $("#cbxIsBaseUnit").prop("checked", false);
                                    }
                                }
                            }
                            else {
                                if (alertify.confirm(data.d + "  has already been set  as  base unit under the category do you want to change it?")) {
                                    $("#cbxIsBaseUnit").attr("checked", "checked");
                                }
                                else {
                                    $("#cbxIsBaseUnit").prop("checked", false);
                                }
                            }

                        }

                    },
                    error: function (error) {

                    }
                });
            }
            catch (e) {
                alert(e);
            }
        }

    
    </script>
</head>
<body>
    <form id="frmUnit" runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               <div class="btn_bg5"> 
                  <div class="heading2">
                       <div class="user">Units</div>
                         <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                        <uc2:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                         <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/ADD.png"  CssClass="imagebuttonStyle"  OnClick="imgbtnAdd_Click"  TabIndex="6" runat="server" OnClientClick="ShowAddDiv();  ClearAdd();  releasebtn();" />
                       
                        <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle"  TabIndex="5" runat="server" OnClick="imgbtnView_Click" />
                         </div>
                    </asp:Panel>  
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                         <div class="main_bg">  
                        <div id="divAdd" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Unit"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="textboxMandatory" runat="server" MaxLength="100" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqvalName" runat="server" ErrorMessage="Unit Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqvalName">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="No of Units"></asp:Label>
                                    <asp:TextBox ID="txtNofUnits" CssClass="textboxMandatory" Style="text-align: right;" runat="server" MaxLength="6" onkeypress="CheckInteger(event);" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Valid No. required" ControlToValidate="txtNofUnits" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Value should be greater than zero" InitialValue="0" ControlToValidate="txtNofUnits" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li style="display: none;">
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Category"></asp:Label>
                                    <asp:DropDownList ID="ddlCategoryFirst" runat="server" CssClass="dropdownMandatory" onchange="ChangeValue();"></asp:DropDownList>
                                    <%--  <asp:RequiredFieldValidator ID="reqvalCategory" runat="server" ErrorMessage="Category required" ControlToValidate="ddlCategoryFirst" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalCategory">
                                    </cc:ValidatorCalloutExtender>--%>
                                </li>
                                <li class="btnMargin">
                                    <asp:CheckBox ID="cbxIsBaseUnit" Visible="false" runat="server" Text="Base Unit"  />
                                </li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="3" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="4" />
                                    <asp:HiddenField ID="hdfUnitId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                     <asp:HiddenField ID="hfuserName" runat="server" />
                                     <asp:HiddenField ID="hfMasterID" runat="server" />
                                </li>
                            </ul>
                        </div>
                             </div>
                            <div class="main_bg"> 
                        <div id="divView" runat="server" style="display: none;">
                            <%--style="display: none;"--%>
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" MaxLength="100" CssClass="textbox" runat="server" TabIndex="7"></asp:TextBox>
                                    </li>

                                    <li style="display: none;">
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Category"></asp:Label>
                                        <asp:DropDownList ID="ddlCtaegorySearch" runat="server" CssClass="dropdown" TabIndex="8"></asp:DropDownList>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="9" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" TabIndex="10" OnClientClick="ClearSearch(); return false; " />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvUnits" runat="server" OnPageIndexChanging="gvUnits_PageIndexChanging" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ID_Units" OnRowDeleting="gvUnits_RowDeleting" OnSelectedIndexChanging="gvUnits_SelectedIndexChanging" AllowPaging="True">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" ItemStyle-Width="75px" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:BoundField DataField="UnitName" HeaderText="Name" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete"  OnClick="ImageButton2_Click" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                    </div>
                   <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Reason" ></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopup1" runat="server" />
                                </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>
           
    </form>
</body>
</html>
