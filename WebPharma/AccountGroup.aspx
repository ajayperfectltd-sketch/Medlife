<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountGroup.aspx.cs" Inherits="AccountGroup" EnableEventValidation="false" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc1" TagName="MedLifePageHelp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
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
        function PopupOnAccountGroup() {
            try {

                //$("#ContentPlaceHolder1_divPopupOnstores").css('display', 'block');
                $("#divPopupAccountGroup").css('display', 'block');
            }
            catch (ex) {
                //alert('popup' + ex);  
            }
        }

        function ClearAdd() {
            submit = 0;
            $("#<%#txtName.ClientID%>").val('');
            $("#<%#txtName.ClientID%>").focus();
            $("#<%#hdfAccountGroup.ClientID%>").val('');
            $("#<%#ddlAgAccountType.ClientID%>").val("0");

            $("#<%#ddlGroupType.ClientID%>").val("0");
            $("#<%#txtSortOrder.ClientID%>").val('');
        }
        function ClearSearch() {
            $("#<%#txtSearchShortName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchShortName.ClientID%>").focus();
        }
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
           
        }
        function clearbl() {
            //$("#blMessage").empty();
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
    <form id="frmAccountGroup" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               <div class="btn_bg5"> 
                          <%--  <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" OnClientClick="ShowAddDiv(); ClearAdd();  return false; releasebtn();"  TabIndex="10"/>
                        
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="9" runat="server" OnClick="imgbtnView_Click" />--%>
                       
                      <div class="heading2">
                    <div class="user">Account Group</div>
                               <asp:Panel ID="divMenuButtons" runat="server">
                        <div style="float: right; margin-top: -10px">
                            <uc1:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle"    runat="server" OnClientClick="ShowAddDiv(); ClearAdd(); return false;  releasebtn();" TabIndex="7" />
                        
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle"   TabIndex="8" runat="server" OnClick="imgbtnView_Click" />
                        </div>
                       </asp:Panel>
                    </div>

                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">  
                        <div id="divAdd" runat="server"> 
                         
                            <%--style="display: none;"--%>
                            <ul class="contentul">
                               
                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="textboxMandatory" runat="server" MaxLength="250" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                 <li>
                                    <asp:Label ID="Label8" runat="server" CssClass="label" Text="Account Type"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlAgAccountType" CssClass="dropdownMandatory" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlAgAccountType_SelectedIndexChanged">
                                        <asp:ListItem Text="select" Value="0" />
                                        <asp:ListItem Text="Asset" Value="A" />
                                        <asp:ListItem Text="Liability" Value="L" />
                                        <asp:ListItem Text="Income" Value="I"/>
                                        <asp:ListItem Text="Expense" Value="E"></asp:ListItem>                                       
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Account Type required" ControlToValidate="ddlAgAccountType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>

                                </li>

                                <li>
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Financial Group"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlGroupType" CssClass="dropdownMandatory" TabIndex="3">
                                        <asp:ListItem Text="select" Value="0" />
                                        <asp:ListItem Text="Balance Sheet" Value="B" />
                                        <asp:ListItem Text="P & L" Value="P" />
                                        <asp:ListItem Text="Trade Item" Value="T"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Financial Group required" ControlToValidate="ddlGroupType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                    </cc:ValidatorCalloutExtender>


                                </li>
                               
                                <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Sort Order"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtSortOrder" Style="text-align: right;" onkeypress="CheckInteger(event);" CssClass="textbox" TabIndex="4" MaxLength="5" />
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Sort order required" ControlToValidate="txtSortOrder" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>--%>
                                </li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="5" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" TabIndex="6" OnClientClick="ClearAdd(); return false;" />
                                    <asp:HiddenField ID="hdfAccountGroup" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                    <asp:HiddenField ID="hfMasterID" runat="server" />
                                     <asp:HiddenField ID="hfuserName" runat="server" />
                                </li>
                            </ul>
                        </div>
                            </div>
                         <div class="main_bg">  
                        <div id="divView" runat="server" style="display: none;">
                            <%--style="display: none;"--%>
                            <ul class="contentul">
                                <li>
                                    <li style="display:none">
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Short Name" ></asp:Label>
                                        <asp:TextBox ID="txtSearchShortName" CssClass="textbox" TabIndex="11" runat="server"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" CssClass="textbox" TabIndex="12" runat="server"></asp:TextBox>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="13" />
                                        <asp:Button ID="btnSearchClear" TabIndex="14" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvAccountGroup" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="Teal" BorderStyle="Solid"  AllowPaging="true"  OnPageIndexChanging="gvAccountGroup_PageIndexChanging"  BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ID_AccountGroup" OnRowDeleting="gvAccountGroup_RowDeleting" OnSelectedIndexChanging="gvAccountGroup_SelectedIndexChanging">
                                   <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="GroupName" HeaderText="Name" />
                                        <asp:BoundField DataField="SortOrder" HeaderText="Sort Order" ItemStyle-HorizontalAlign="Right" />
                                         <asp:BoundField DataField="AccountTypes" HeaderText="Account Type" />
                                        <asp:BoundField DataField="GroupTypes" HeaderText="Group Type" />
                                       

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
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
                            </ul>
                        </div>
                              </div>
                        <%--<asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />--%>
                   <%--     <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                    </div>
                </div>
                     <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Reason" ></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupAccountGroup" runat="server" class="divPopupOnstores" style="display: block;">
                            <uc2:CommonPopupDelete ID="CommonPopup1" runat="server" />
                                </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                        </cc:ModalPopupExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>
    </form>

</body>
</html>
