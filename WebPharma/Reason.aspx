<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reason.aspx.cs" Inherits="Reason" %>

<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopup" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc1" TagName="MedLifePageHelp" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>


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
            $("#<%#txtReason.ClientID%>").val('');
            $("#<%#hfReasonId.ClientID%>").val('0');
            $("#<%#ddlReason.ClientID%>").val('0');
            
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtReason.ClientID%>").focus();
        }
        function ClearSearch() {
            $("#<%#txtSearchReason.ClientID%>").val('');
            $("#<%#txtSearchReason.ClientID%>").focus();
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
                    <div class="user">Reason</div>
                            <asp:Panel ID="divMenuButtons" runat="server">
                        <div style="float: right; margin-top: -10px">
                            <uc1:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                            <asp:ImageButton ID="ImageButton1" CssClass="imagebuttonStyle" ImageUrl="images/ADD.png"   OnClick="ImageButton1_Click"  TabIndex="6" runat="server" OnClientClick="ShowAddDiv(); ClearAdd();  releasebtn();" />
                       
                            <asp:ImageButton ID="imgbtnView" CssClass="imagebuttonStyle" ImageUrl="images/view.png"     TabIndex="5" runat="server" OnClick="imgbtnView_Click"   />
                        </div>
                     </asp:Panel>
                       
                    </div>
                   
                    <div class="clear"></div>
                      <div class="group_bg2">
                            <div class="main_bg">  
                    <div id="divAdd" runat="server" >
                        <ul class="contentul">
                      
                            <li>
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Reason" ></asp:Label>
                                <asp:TextBox ID="txtReason" Width="214px" CssClass="textboxMandatory" TabIndex="1" runat="server" ToolTip="Reason"  MaxLength="200"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Reason required" ControlToValidate="txtReason" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </cc:ValidatorCalloutExtender>
                            </li> 
                                <li>
                                 <asp:Label ID="Label7" runat="server" CssClass="label" Text="Type"></asp:Label>
                                <asp:DropDownList ID="ddlReason" CssClass="dropdown" runat="server" TabIndex="2">
                                    <asp:ListItem Text="Delete" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Return" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="General" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Damage" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </li>      
                                           
                            <li class="btnMargin">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="button" ToolTip="Submit" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="3"   OnClientClick="disablebutn();"/>
                                <asp:Button ID="btnClear" runat="server" CssClass="button" ToolTip="Clear" TabIndex="4" Text="Clear" OnClientClick="ClearAdd(); " OnClick="btnClear_Click" />
                                <asp:HiddenField ID="hfReasonId" runat="server" />
                                <asp:HiddenField ID="hfClient" runat="server" />
                                <asp:HiddenField ID="hfUser" runat="server" />
                                 <asp:HiddenField ID="hfuserName" runat="server" />
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
                        <ul class="contentul">
                            <li>
                             <li>
                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Reason"></asp:Label>
                                <asp:TextBox ID="txtSearchReason" MaxLength="200" CssClass="textboxLong" ToolTip="Reason" TabIndex="7" runat="server"></asp:TextBox>
                            </li>
                            <li class="btnMargin">
                                 <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" TabIndex="8" ToolTip="Search" OnClick="btnSearch_Click"  />
                                <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" ToolTip="Clear" TabIndex="9" OnClientClick="ClearSearch(); return false;" />
                            </li>
                            </li>
                        </ul>           
                        <ul class="contentul">
                            <asp:GridView ID="gvReason" runat="server" CssClass="grid"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" OnPageIndexChanging="gvReason_PageIndexChanging" DataKeyNames="ID_Reason" OnRowDeleting="gvReason_RowDeleting" OnSelectedIndexChanging="gvReason_SelectedIndexChanging" AllowPaging="True">
                                <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="SLNO" HeaderText="SlN0" ItemStyle-HorizontalAlign="Center" />
                                   
                                    <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                     <asp:BoundField DataField="ReasonType" HeaderText="Reason Type" />                                                              
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
                                <HeaderStyle  CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                <PagerStyle   CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
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
                          <asp:Panel ID="divPopup1" runat="server" Style="display: none;" CssClass="DivStyle">
                    <div id="divheader" runat="server" class="PopupHeaderReason">
                        <asp:Label ID="lblPopupHeader" runat="server" Text="Reason"></asp:Label>
                        <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                    </div>
                          <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                    <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                              </div>
                </asp:Panel>
                <asp:Button ID="btnShow1" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
             <cc:ModalPopupExtender ID="mpComm" runat="server" PopupControlID="divPopup1" TargetControlID="btnShow1"
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
