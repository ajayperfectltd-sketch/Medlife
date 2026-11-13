<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Counter.aspx.cs" Inherits="Counter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<%@ Register src="Popup/CommonPopup.ascx" tagname="CommonPopup" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/Custom.js"></script>
     <script src="js/toastr.js"></script>
    <script src="js/alertify.min.js"></script>
       <script src="js/Navigation.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
     <style>
            .Border {
            /*margin: 0.5em;*/
            border-style: groove;
            box-shadow: 0px 0px 2px 2px #888888;
            border: solid 2px #FFF;
            border-radius: 7px;
            padding-right: 5px;
        }
   #ddlStore {
    max-width: 260px;
    text-overflow: ellipsis;
    overflow: hidden;
}
          </style>
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

        function PopupOnReason() {
            try {


                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {

            }
        }
        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

        }

        function ClearAdd() {

            submit = 0;

            $("#txtName").val('');
            $("#hfCounterId").val('0');
            $("#ddlStore").val('0');

            $("#txtName").focus();

            $("#txtRefno").val('');
            $("#chkActive").prop('checked', true);

            $("#txtAccountHead").val('');

            $("#txtRefno1").val('');

            $("#txtAccountHeadSub").val('');
        }
        function ClearSearch() {

            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").focus();
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtName.ClientID%>").focus();
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
                   <div class="user">Counter</div>
                              <asp:Panel ID="divMenuButtons" runat="server">
                        <div style="float: right; margin-top: -10px">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle" onmouseout="this.src='images/ADD.png'"   onmouseover="this.src='images/ADD.png'" TabIndex="14"  runat="server"   OnClientClick="ShowAddDiv(); ClearAdd(); releasebtn();" OnClick="ImageButton1_Click" />
                           <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle" onmouseout="this.src='images/view.png'"   onmouseover="this.src='images/view.png'"  TabIndex="13"   runat="server" OnClick="imgbtnView_Click" OnClientClick=" ClearSearch();" />
                        </div>
                                  </asp:Panel>
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">  
                        <div id="divAdd" runat="server" style="display: block;">
                            <ul class="contentul">

                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="textboxLongMandatory" runat="server" MaxLength="75" TabIndex="1" Width="255px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label17" runat="server" CssClass="label" Text="Store"></asp:Label>
                                    <asp:DropDownList ID="ddlStore" width="260px" runat="server" CssClass="dropdownMandatory" TabIndex="2"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select a Store" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                 <li>
                                    <asp:Label ID="lblAcvountHead" runat="server" CssClass="label" Text="Account Head" Width="158px"></asp:Label>
                                    <asp:TextBox ID="txtRefno" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="3" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True" ToolTip="Counter Cash Balance"  style="margin-left:-23px;" ></asp:TextBox>
                                    <asp:ImageButton ID="btnGo" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnGo_Click" style="margin-bottom:-8px;" TabIndex="4"  ToolTip="Counter Cash Balance"   />
                                         
                             
                                    <asp:TextBox ID="txtAccountHead" runat="server" Width="161px" CssClass="textbox" TabIndex="5" MaxLength="40" ReadOnly="True"  ToolTip="Counter Cash Balance" ></asp:TextBox>

                                  
                             

                                </li>
                                   <li>
                                    <asp:Label ID="Label18" runat="server" CssClass="label" Style="width:158px;" Text="Acc Sub Head"></asp:Label>
                                     <asp:TextBox ID="txtRefno1" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="6"  style="margin-left:-22px;" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonheadsub" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonheadsub_Click" style="margin-bottom:-8px;" TabIndex="7" />
                                         
                             
                                    <asp:TextBox ID="txtAccountHeadSub" runat="server" Width="161px" CssClass="textbox" TabIndex="8" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                  
                                  
                                </li>
                                 <li>
                                  <asp:Label ID="lblchkActive" runat="server" CssClass="label" Text="Active"></asp:Label>
                                 <asp:CheckBox ID="chkActive" runat="server" TabIndex="9"  />
                               
                                 </li>
                                <li></li>
                               <%-- <li class="btnMargin">
                                    <asp:CheckBox Text="Active" ID="cbxAcctive" runat="server" CssClass="chckbox" TabIndex="7" />
                                </li>--%>
                              <div  style="width: 400px;">
                                     
                                        <div style="padding-left: 0px;">
                                 
                                            <li>
                                                <asp:Label ID="Label47" runat="server" CssClass="label" Font-Underline="true" Text="Shift Details"></asp:Label>
                                            </li>
                                            <br />
                                           
                                            <li>
                                                <asp:Label ID="Label61" runat="server" CssClass="label" Text="Shift"></asp:Label>
                                                <asp:TextBox ID="txtShift" runat="server"  AutoPostBack="false" CssClass="textboxMandatory"     style="margin-left:2px;" onBlur="popUpItemsSelect();" TabIndex="10" MaxLength="50"></asp:TextBox>
                                                <asp:ImageButton ID="IbtnItemAdd" runat="server"  OnClick="IbtnItemAdd_Click" TabIndex="11" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Select an Shift" ControlToValidate="txtShift" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender59" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator32">
                                                </cc:ValidatorCalloutExtender>
                                            </li>
                                            <li class="btnMargin">
                                                <asp:Button ID="btnAdd" ValidationGroup="Add" runat="server" CssClass="button" Width="80" Text="Insert" TabIndex="12"  OnClick="btnAdd_Click" />
                                            </li>
                                            <div  style="width: 400px;">
                                                <li>
                                                    <asp:GridView ID="grdShiftdetails" runat="server" CssClass="grid"
                                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                                        BackColor="White"  BorderStyle="Solid" BorderWidth="1px" Width="390px" CellPadding="3" ForeColor="Black"
                                                        GridLines="Vertical" PageSize="10" AllowPaging="true" OnPageIndexChanging="grdShiftdetails_PageIndexChanging" DataKeyNames="FK_Shift" OnRowDeleting="grdShiftdetails_RowDeleting">
                                                        <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle"/>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SL No" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           <%-- <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />--%>
                                                            <asp:BoundField DataField="ShiftName" HeaderText="Shift Name" ItemStyle-HorizontalAlign="left" />
                                                          <%--  <asp:BoundField DataField="ShiftFrom" HeaderText="ShiftFrom"  />
                                                             <asp:BoundField DataField="ShiftTo" HeaderText="ShiftTo"  />--%>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <FooterStyle BackColor="#CCCCCC" />
                                                        <HeaderStyle  CssClass="gridviewHeaderStyle"  HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                                        <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                                    </asp:GridView>
                                                </li>
                                            </div>

                                            <br />
                                        </div>
                                    </div>

                                <li class="btnMargin" style="padding-left:145px">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" TabIndex="15" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd();"  OnClick="btnClear_Click" TabIndex="16" />
                                    <asp:HiddenField ID="hfCounterId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                     <asp:HiddenField ID="hfSelectedItem" runat="server" />
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
                                    <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" TabIndex="15" MaxLength="75"></asp:TextBox>
                                </li>
                                <%-- <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchShortname" CssClass="textbox" runat="server" TabIndex="16" MaxLength="10"></asp:TextBox>
                                </li>--%>
                               <%-- <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Store"></asp:Label>
                                    <asp:DropDownList ID="ddlSearchStore" runat="server" CssClass="dropdown" TabIndex="13"></asp:DropDownList>
                                </li>--%>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="16" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="17" />
                                </li>

                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvCounter" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White"  BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvCounter_PageIndexChanging" DataKeyNames="ID_Counters" OnRowDeleting="gvCounter_RowDeleting" OnSelectedIndexChanging="gvCounter_SelectedIndexChanging">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="Sl No" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="CounterName" HeaderText="Name" />
                                        <%--<asp:BoundField DataField="ShortName" HeaderText="ShortName" />--%>
                                        <%--   <asp:BoundField DataField="SalesMan" HeaderText="SalesMan" />--%>
                                        <asp:BoundField DataField="Active" HeaderText="Active" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" OnClick="ImageButton2_Click" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle  CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
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
                      <%--  <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>



                            <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                           <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divheader" runat="server" class="PopupHeader" >
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head"  CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>

                            <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                        </asp:Panel>
                          <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                        </cc:ModalPopupExtender>
                        
                    <asp:Button ID="btnS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopUpdelete" runat="server" CssClass="DivStyle" Style="display: none;">
                            <div id="divPopheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader1" runat="server" Text="Reason"></asp:Label>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                                </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divPopUpdelete" TargetControlID="btnS"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divPopheader">
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
    </form>
</body>
</html>
