<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Generic.aspx.cs" Inherits="Generic" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc2" TagName="MedLifePageHelp" %>

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


            function ClearAdd() {
                submit = 0;
                $("#<%#txtShortName.ClientID%>").val('');
                $("#<%#txtGeneric.ClientID%>").val('');
                $("#<%#hfGenericID.ClientID%>").val('0');
            }
            function ClearSearch() {

                $("#<%#txtSearchShortName.ClientID%>").val('');
                $("#<%#txtSearchName.ClientID%>").val('');

            }

            function ShowAddDiv() {

                $("#<%#divView.ClientID%>").hide();
                $("#<%#divAdd.ClientID%>").show();
                $("#<%#txtShortName.ClientID%>").focus();
            }
            function pageLoad() {

                var ctrContainer = "form";
                setKeynavigation(ctrContainer);

            }
            function EnterTab(e, Id1) {

                var intKey = window.Event ? e.which : e.KeyCode;


                if (intKey == 13) {
                    e.returnValue = false;
                    $("#" + Id1).focus();
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
    </script>
</head>
<body>
 <form id="form1" runat="server" autocomplete="off">
    <div>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5"> 
                     <div class="heading2">
                                 <div class="user">Generic</div>
                         <asp:Panel ID="divMenuButtons" runat="server">
                  <div style="float: right; margin-top: -10px">
                      <uc2:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                            <asp:ImageButton ID="ImageButton1" CssClass="imagebuttonStyle" ImageUrl="images/add.png"  TabIndex="6"  OnClick="ImageButton1_Click" runat="server"    OnClientClick="ShowAddDiv(); ClearAdd();  releasebtn();" />
                      
                            <asp:ImageButton ID="imgbtnView" CssClass="imagebuttonStyle" ImageUrl="images/view.png"    TabIndex="5" runat="server"   OnClick="imgbtnView_Click"/>
                        </div>
                             </asp:Panel>
                    </div>
                    <div class="clear"></div>
                     <div class="group_bg2">
                          <div class="main_bg">  
                    <div id="divAdd" runat="server" style="display: none;">         
                        <ul class="contentul">
                           
                              <li>
                                <asp:Label ID="Label3" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                <asp:TextBox ID="txtShortName" CssClass="textboxMandatory" onkeydown="EnterTab(event,'txtGeneric');" runat="server" MaxLength="10" TabIndex="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="ShortName required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Generic Name"></asp:Label>
                                <asp:TextBox ID="txtGeneric"  onkeydown="EnterTab(event,'btnSubmit');" CssClass="textboxMandatory" runat="server" MaxLength="75" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtGeneric" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </cc:ValidatorCalloutExtender>
                            </li>
                        
                          
                       
                           
                            <li class="btnMargin">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit"   OnClick="btnSubmit_Click" ValidationGroup="submit"  TabIndex="3" OnClientClick="disablebutn();" />
                                <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="4" />
                                <asp:HiddenField ID="hfGenericID" runat="server" />
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
                        <ul class="contentul">
                            <li>
                                 <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchShortName" CssClass="textbox" MaxLength="10" runat="server" TabIndex="7"></asp:TextBox>
                                </li>
                               
                                <li>
                                    <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchName" CssClass="textbox" MaxLength="75" runat="server" TabIndex="8"></asp:TextBox>
                                </li>
                               
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="9" Text="Search"   OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" TabIndex="10" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                     
                                </li>
                            </li>
                        </ul>
                        <ul class="contentul">
                            <asp:GridView ID="grdGeneric" runat="server" CssClass="grid"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10"   OnPageIndexChanging="grdGeneric_PageIndexChanging" DataKeyNames="ID_Generic"  OnRowDeleting="grdGeneric_RowDeleting"   OnSelectedIndexChanging="grdGeneric_SelectedIndexChanging" AllowPaging="True">
                                <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="SlNo" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                     <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                    <asp:BoundField DataField="GenericName" HeaderText="Name" />
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
                                <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                        </ul>
                    </div>
                  
                          </div>
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
            </ContentTemplate>
        </asp:UpdatePanel>
         
    </div>
         
    </form>
</body>
</html>
