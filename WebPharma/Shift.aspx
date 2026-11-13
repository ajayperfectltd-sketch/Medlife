<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Shift.aspx.cs" Inherits="Shift" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>
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
            $("#<%#hfShiftId.ClientID%>").val('0');
            $('#<%#chkActive.ClientID%>').prop('checked', true)
            $("#<%#txtShortName.ClientID%>").focus();
          
        }
        function ClearSearch() {
          
            $("#<%#txtSearchShortName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchShortName.ClientID%>").focus();
        }
        function BlurItem() {
            $("#<%#btnStoreBlurTrigger.ClientID%>").click();
              //alert('a');
          }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtShortName.ClientID%>").focus();
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
        function PopupOnShift() {
            try {

                //$("#ContentPlaceHolder1_divPopupOnstores").css('display', 'block');
                $("#divPopupShift").css('display', 'block');
            }
            catch (ex) {
                //alert('popup' + ex);  
            }
        }
    </script>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server"  autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
             <div class="btn_bg5"> 
                 <div class="heading2">
                      <div class="user">Shift</div>
                     
                   <asp:Panel ID="divMenuButtons" runat="server">
                        <div style="float: right; margin-top: -10px">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png"  CssClass="imagebuttonStyle" TabIndex="9" OnClick="ImageButton1_Click"   runat="server" OnClientClick="ShowAddDiv(); ClearAdd();   releasebtn();" />
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png"  CssClass="imagebuttonStyle" TabIndex="8"    runat="server" OnClick="imgbtnView_Click" />
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
                                <asp:TextBox ID="txtShortName" CssClass="textboxMandatory" runat="server" MaxLength="25" TabIndex="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="ShortName required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                <asp:TextBox ID="txtName" CssClass="textboxMandatory" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </cc:ValidatorCalloutExtender>
                            </li>

                             <li>
                               <asp:Label ID="Label22" runat="server" Text="Shift From " CssClass="label"></asp:Label>
                              <MKB:TimeSelector ID="TmsShiftFrom"  runat="server" TabIndex="3"     AllowSecondEditing="True" ClientIDMode="Inherit" BorderStyle="Solid" BackColor="White" BorderColor="White" MinuteIncrement="1"></MKB:TimeSelector>
                             </li>

                              <li>
                               <asp:Label ID="Label9" runat="server" Text="Shift To " CssClass="label"></asp:Label>
                              <MKB:TimeSelector ID="TmsShiftTo" runat="server" TabIndex="4"    AllowSecondEditing="True" ClientIDMode="Inherit" BorderStyle="Solid" BackColor="White" BorderColor="White" MinuteIncrement="1"></MKB:TimeSelector>
                             </li>
                             <li>
                                <asp:Label ID="lblActive" runat="server" CssClass="label" Text="Active"></asp:Label>
                                  <asp:CheckBox ID="chkActive" runat="server" TabIndex="5"  />
                               

                               </li>
                           
                            <li class="btnMargin">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="6" OnClientClick="disablebutn();" />
                                <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="7" />
                                <asp:HiddenField ID="hfShiftId" runat="server" />
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
                                    <asp:TextBox ID="txtSearchShortName" CssClass="textbox" MaxLength="25" runat="server" TabIndex="10"></asp:TextBox>
                                </li>
                               
                                <li>
                                    <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchName" CssClass="textbox" MaxLength="50" runat="server" TabIndex="11"></asp:TextBox>
                                </li>
                               
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="12" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" TabIndex="13" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                     <asp:Button ID="btnStoreBlurTrigger" Text="text" runat="server" TabIndex="14" Style="display: none;" OnClick="btnStoreBlurTrigger_Click" />
                                </li>
                            </li>
                        </ul>
                        <ul class="contentul">
                            <asp:GridView ID="gvShift" runat="server" CssClass="grid"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" AllowPaging="true" GridLines="Vertical" PageSize="10" OnPageIndexChanging="gvShift_PageIndexChanging"  DataKeyNames="ID_ShiftMaster" OnRowDeleting="gvShift_RowDeleting" OnSelectedIndexChanging="gvShift_SelectedIndexChanging">
                                <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                     <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                    <asp:BoundField DataField="ShiftName" HeaderText="Name" />
                                   <%-- <asp:BoundField DataField="SalesMan" HeaderText="SalesMan" />
                                    <asp:BoundField DataField="Active" HeaderText="Active" />--%>
                                     <asp:BoundField DataField="Active" HeaderText="Active" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">            
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" OnClick="ImageButton2_Click" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                  <%--  <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                    <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                          </div>
                </div>
                       <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Reason"></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupShift" runat="server" class="divPopupOnstores" style="display: none;">
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
       
    </form>
</body>
</html>
