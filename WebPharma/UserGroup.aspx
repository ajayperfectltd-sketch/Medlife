<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserGroup.aspx.cs" Inherits="UserGroup" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
     <script src="js/Navigation.js"></script>
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
            $("#<%#hfUserGroupId.ClientID%>").val('0');
            $('#<%#chkActive.ClientID%>').prop('checked', true);
            $('#<%#chkAdministrator.ClientID%>').prop('checked', false);
            $('#<%#chkAdd.ClientID%>').prop('checked', false);
            $('#<%#chkModify.ClientID%>').prop('checked', false);
            $('#<%#chkDelete.ClientID%>').prop('checked', false);
            $('#<%#chkView.ClientID%>').prop('checked', false);
            $('#<%#chkApprove.ClientID%>').prop('checked', false);
            $('#<%#chkPurchase.ClientID%>').prop('checked', false);
            $('#<%#chkSales.ClientID%>').prop('checked', false);
            $('#<%#chkPurchaseReturn.ClientID%>').prop('checked', false);
            $('#<%#chkSalesReturn.ClientID%>').prop('checked', false);
            $("#<%#txtShortName.ClientID%>").focus();
        }
        function ClearSearch() {
            $("#<%#txtSearchShortName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchShortName.ClientID%>").focus();
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtShortName.ClientID%>").focus();
        }
        function pageLoad() {
            $("#<%#chkAdministrator.ClientID%>").change(function () {
                if (this.checked) {
                    $('#<%#chkAdd.ClientID%>').prop('checked', true);
                    $('#<%#chkModify.ClientID%>').prop('checked', true);
                    $('#<%#chkDelete.ClientID%>').prop('checked', true);
                    $('#<%#chkView.ClientID%>').prop('checked', true);
                    $('#<%#chkApprove.ClientID%>').prop('checked', true);
                    $('#<%#chkPurchase.ClientID%>').prop('checked', true);
                    $('#<%#chkSales.ClientID%>').prop('checked', true);
                    $('#<%#chkPurchaseReturn.ClientID%>').prop('checked', true);
                    $('#<%#chkSalesReturn.ClientID%>').prop('checked', true);
                }
                else {
                    $('#<%#chkAdd.ClientID%>').prop('checked', false);
                    $('#<%#chkModify.ClientID%>').prop('checked', false);
                    $('#<%#chkDelete.ClientID%>').prop('checked', false);
                    $('#<%#chkView.ClientID%>').prop('checked', false);
                    $('#<%#chkApprove.ClientID%>').prop('checked', false);
                    $('#<%#chkPurchase.ClientID%>').prop('checked', false);
                    $('#<%#chkSales.ClientID%>').prop('checked', false);
                    $('#<%#chkPurchaseReturn.ClientID%>').prop('checked', false);
                    $('#<%#chkSalesReturn.ClientID%>').prop('checked', false);
                 
                }
            });
            $("#<%#chkAdd.ClientID%>").change(function () {
                CheckIfAdmin();
            });
            $("#<%#chkModify.ClientID%>").change(function () {
                CheckIfAdmin();
            });
            $("#<%#chkDelete.ClientID%>").change(function () {
                CheckIfAdmin();
            });
            $("#<%#chkView.ClientID%>").change(function () {
                CheckIfAdmin();
            });
            $("#<%#chkApprove.ClientID%>").change(function () {
                CheckIfAdmin();
            });
            $("#<%#chkPurchase.ClientID%>").change(function () {
                CheckIfAdmin();
            });
            $("#<%#chkSales.ClientID%>").change(function () {
                CheckIfAdmin();
            });
            $("#<%#chkPurchaseReturn.ClientID%>").change(function () {
                CheckIfAdmin();
            });
            $("#<%#chkSalesReturn.ClientID%>").change(function () {
                CheckIfAdmin();
            });
          
        }
        function CheckIfAdmin() {
            if (this.checked) { }
            else
            {
                $('#<%#chkAdministrator.ClientID%>').prop('checked', false);
            }
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
                           <div class="user">User Group</div>
                          <asp:Panel ID="divMenuButtons" runat="server">
                               <div style="float: right; margin-top: -10px">
                                    <asp:ImageButton ID="ImageButton1" ImageUrl="images/add.png" TabIndex="24"  CssClass="imagebuttonStyle" runat="server" OnClientClick="ShowAddDiv(); return false; releasebtn();" />
                                   <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png"  TabIndex="23" CssClass="imagebuttonStyle"  runat="server" OnClick="imgbtnView_Click" />
                                   </div>
                              </asp:Panel>

                    </div>
            
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg"> 
                        <div id="divAdd" runat="server" style="display: block;">
                            <ul class="contentul">
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                    <asp:TextBox ID="txtShortName" CssClass="textboxMandatory" runat="server" MaxLength="5" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Short name required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="textboxLongMandatory" runat="server" MaxLength="100" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li class="btnMargin">
                                    <asp:CheckBox ID="chkActive" runat="server" CssClass="chckbox" Text="Active" TabIndex="3"/>
                                </li>
                                <li class="clear"></li>
                                <li class="btnMargin">
                                    <asp:CheckBox ID="chkAdministrator" runat="server" CssClass="chckbox" Text="Administrator" TabIndex="4" />
                                </li>
                                <li class="clear"></li>
                                <li>
                                    <div>
                                        <ul style="margin-left: 200px;">
                                            <li>
                                                <asp:CheckBox ID="chkAdd" runat="server" CssClass="chckbox" Text="Add" TabIndex="5" />
                                            </li>
                                            <li>
                                                <asp:CheckBox ID="chkModify" runat="server" CssClass="chckbox" Text="Modify" TabIndex="6" />
                                            </li>
                                            <li>
                                                <asp:CheckBox ID="chkDelete" runat="server" CssClass="chckbox" Text="Delete" TabIndex="7" />
                                            </li>
                                            <li>
                                                <asp:CheckBox ID="chkView" runat="server" CssClass="chckbox" Text="View"  TabIndex="8"/>
                                            </li>
                                        
                                            <li> 
                                                <asp:CheckBox ID="chkApprove" runat="server" CssClass="chckbox" Text="Approve" TabIndex="9" />
                                            </li>
                                              <li Style="display:none">
                                                <asp:CheckBox ID="chkDayClose" runat="server" CssClass="chckbox" Text="Day Close" TabIndex="10"/>
                                            </li>
                                             <li >
                                                <asp:CheckBox ID="chkPurchase" runat="server" CssClass="chckbox" Text="Purchase" TabIndex="10" />
                                            </li>
                                             <li >
                                                <asp:CheckBox ID="chkSales" runat="server" CssClass="chckbox" Text="Sales" TabIndex="11" />
                                            </li>
                                             <li >
                                                <asp:CheckBox ID="chkPurchaseReturn" runat="server" CssClass="chckbox" Text="Purchase Return" TabIndex="12" />
                                            </li>
                                             <li>
                                                <asp:CheckBox ID="chkSalesReturn" runat="server" CssClass="chckbox" Text="Sales Return" TabIndex="13" />
                                            </li>     
                                         
                                          
                                        </ul>
                                    </div>
                                </li>
                                <li class="clear"></li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="17" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="18" OnClientClick="ClearAdd(); return false;" />
                                    <asp:HiddenField ID="hfUserGroupId" runat="server" />
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
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchShortName" CssClass="textbox" runat="server" TabIndex="19"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" TabIndex="20"></asp:TextBox>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="21"/>
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="22" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvUserGroup" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvUserGroup_PageIndexChanging"
                                    BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ID_UserGroup" OnSelectedIndexChanging="gvUserGroup_SelectedIndexChanging" OnRowDeleting="gvUserGroup_RowDeleting">
                                    <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle"/>
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                        <asp:BoundField DataField="GroupName" HeaderText="Group Name" />
                                        <asp:BoundField DataField="Active" HeaderText="Active" />
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
                                                <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandName="Delete" OnClick="ImageBtnDelete_Click" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
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
                       <%-- <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>

                         <asp:Button ID="btnReasonShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divReasonPopup" runat="server" Style="display: none;"  CssClass="DivStyle">
                            <div id="divReasonheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="Label3" runat="server" Text="Reason" CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="btnReasonClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                                </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divReasonPopup" TargetControlID="btnReasonShow"
                            CancelControlID="btnReasonClose" Drag="true" PopupDragHandleControlID="divReasonheader">
                        </cc:ModalPopupExtender>

                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
