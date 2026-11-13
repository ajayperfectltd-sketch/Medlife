<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountHeadSub.aspx.cs" Inherits="AccountHeadSub" %>

<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc1" TagName="MedLifePageHelp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register src="Popup/CommonPopup.ascx" tagname="CommonPopup" tagprefix="uc1" %>
<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>::Account Head Sub</title>
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
        function pageLoad() {

            var ctrContainer = "form";
            //alert('h');
            setKeynavigation(ctrContainer);
            //alert('k');
        }
        //function PopupOnAccountHeadSub() {
        //    try {

        //        //$("#ContentPlaceHolder1_divPopupOnstores").css('display', 'block');
        //        $("#divPopupOnAccountHeadSub").css('display', 'block');
        //    }
        //    catch (ex) {
        //        //alert('popup' + ex);  
        //    }
        //}
        function PopupOnReason() {
            try {

                //$("#ContentPlaceHolder1_divPopupOnstores").css('display', 'block');
                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {
                //alert('popup' + ex);  
            }
        }

        function ClearAdd() {
           
            submit = 0;
            var toDate = new Date();
            $("#txtopnBalanceDate").val(toDate.format("dd/MM/yyyy"));


            $("#<%#txtAccountHead.ClientID%>").val("");
           
    
            $("#<%#txtAccountSubHeadCode%>").val('');
            $("#<%#txtcustomername.ClientID%>").val('');
            $("#<%#Txtcusid.ClientID%>").val('');
            $("#<%#txtopnBalanceAmnt.ClientID%>").val('');
            
            $("#<%#txtSortOrder.ClientID%>").val('');
            $("#<%#ddlCreditDebit.ClientID%>").val("0");
            $("#<%#ddlStore.ClientID%>").val("0");
          
            $("#<%#hdfAccountSubHead.ClientID%>").val('');

            $("#<%#cbxPettyCash.ClientID%>").prop('checked', false);
            $("#<%#txtRefno.ClientID%>").val('');
            

            $("#<%#txtAccountSubHeadCode%>").val("");
            $("#txtAccountSubHeadCode").val('');

            $("#<%#txtcustomername.ClientID%>").focus();
            return false;
        }
        function ClearSearch() {

          
            $("#txtSearchName").val('');
            $("#<%#ddlAccountHeadSearch.ClientID%>").val("0");
            
        }
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
           
        }
        function GettAccountHeadCode() {
            if ($("#ddlAccountHead :selected").val() != '0') {
                $("#<%#btnHide.ClientID%>").click();
                $("#<%#txtAccountSubHeadCode.ClientID%>").focus();
            }
            return false;
        }
        function clearbl() {
            $("#blMessage").empty();
        }


        function openModal() {
            $('#myModal').modal('show');
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
<body onkeydown="javascript:EnterKeyFilter();" >
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5"> 
                         <div class="heading2">
                    <div class="user">Account SubHead</div>
                               <asp:Panel ID="divMenuButtons" runat="server">
                        <div style="float: right; margin-top: -10px">
                        <uc1:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                             <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" Width="37" Height="38" runat="server" TabIndex="14" OnClientClick="ShowAddDiv(); ClearAdd(); return false; releasebtn(); " />
 <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" TabIndex="11"  OnClick="imgbtnView_Click" />
                        </div>
                       </asp:Panel>
                    </div>
                    
                   
                    <div class="clear"></div>
                    <div class="group_bg2">
                            <div class="main_bg">  
                        <div id="divAdd" runat="server"> 
                            <ul class="contentul">

                                 <li>
                                    <asp:Label ID="lblAcvountHead" runat="server" CssClass="label" Text="Account Head" Width="140px"></asp:Label>
                                    <asp:TextBox ID="txtRefno" runat="server" AutoPostBack="false" CssClass="textboxMandatory" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8"></asp:TextBox>
                                    <asp:ImageButton ID="btnGo" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnGo_Click" style="margin-bottom:-8px;" TabIndex="2" />
                                         
                             
                                    <asp:TextBox ID="txtAccountHead" runat="server" Width="309px" CssClass="textboxMandatory" TabIndex="101" MaxLength="40"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="rfvAccountHead" runat="server" ErrorMessage="Account Head Refno required" ControlToValidate="txtAccountHead" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfvAccountHead">
                                    </cc:ValidatorCalloutExtender>
                                 <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Account Head required" ControlToValidate="txtAccountHead" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>--%>

                                </li>
                                  <li>
                                      <asp:Label ID="Label3" runat="server" CssClass="label" Text="SubHead Code" Width="140px"></asp:Label>
                                    
                                    <asp:TextBox ID="txtAccountSubHeadCode" CssClass="textboxLongMandatory" runat="server" MaxLength="40" TabIndex="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Account Sub Head Code required" ControlToValidate="txtAccountSubHeadCode" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                    </cc:ValidatorCalloutExtender>

                                </li>

                                  <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name" Width="140px"></asp:Label>
                                    <asp:TextBox ID="Txtcusid" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="102" onKeypress="CheckInteger(event);" Width="120px" onblur="txtInput_onblur();" MaxLength="8"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnGos_Click" style="margin-bottom:-8px;" TabIndex="4" />                                    
                                    <asp:TextBox ID="txtcustomername" runat="server" Width="227px" CssClass="textbox" TabIndex="103" MaxLength="500"></asp:TextBox>

                                

                                </li>
                                </li>


                                <%--<li>
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Account Head"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlAccountHead" CssClass="dropdownMandatory" onBlur="return GettAccountHeadCode();" TabIndex="3">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Account Head required" ControlToValidate="ddlAccountHead" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                    </cc:ValidatorCalloutExtender>
                                </li>--%>
                              
                               

                                <li style="display: none;">
                                    <asp:Label ID="Label8" runat="server" CssClass="label" Text="Store"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlStore" CssClass="dropdown" TabIndex="4">
                                    </asp:DropDownList>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Account Sub Group required" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                    </cc:ValidatorCalloutExtender>--%>
                                </li>
                                <%--<li class="clear"></li>--%>
                               <%-- <li>
                                    <label class="label">Account Head Code</label>
                                    <asp:TextBox ID="txtAccountHeadCode" CssClass="textboxLongMandatory" runat="server" MaxLength="50" TabIndex="5"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Account Head Code required" ControlToValidate="txtAccountHeadCode" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>
                                </li>--%>
                                <%--<li class="clear"></li>--%>
                              
                                 <li>
                                     <asp:Label ID="Label9" runat="server" CssClass="label" Text="Opening  Date" Width="140px"></asp:Label>
                                  
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtopnBalanceDate" TabIndex="5" AutoComplete="off" Width="188px" MaxLength="9" />
                                    <cc:CalendarExtender ID="txtopnbalanceDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtopnBalanceDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="reqvalOpenbalanceDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtopnBalanceDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalOpenbalanceDate">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <%-- <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtName" CssClass="textboxMandatory" runat="server" MaxLength="25" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>--%>
                                   <li>
                                    <asp:Label ID="Label7" runat="server" CssClass="label" Text="Opening  Amount" Width="140px"></asp:Label>
                               
                                    <asp:TextBox runat="server" ID="txtopnBalanceAmnt" onkeypress="CheckNumWithDecimal(event);"  Style="text-align: right;" TabIndex="6" CssClass="textbox" MaxLength="15" Width="115px" />

                                    
                                    <asp:DropDownList runat="server" ID="ddlCreditDebit" CssClass="dropdown" TabIndex="7" AutoPostBack="false" Width="65px">
                                       
                                        <asp:ListItem Text="Credit" Value="0" />
                                        <asp:ListItem Text="Debit" Value="1" />
                                                                           
                                    </asp:DropDownList>
                                  

                                </li>
                                
                            
                                <li>
                                    <asp:Label ID="Label10" runat="server" CssClass="label" Text="Sort Order" Width="140px"></asp:Label>
                               
                                    <asp:TextBox runat="server" ID="txtSortOrder" onkeypress="CheckInteger(event);" Style="text-align: right;" TabIndex="8" CssClass="textbox" MaxLength="4" Width="188px" />


                                </li>
                                    <li class="btnMargin" style="display:none">
                                      
                                    <asp:CheckBox Text="Show In Petty Cash" runat="server" ID="cbxPettyCash" CssClass="chckbox" TabIndex="11" />
                                        &nbsp;</li>
                                <li class="btnMargin">
                                    <asp:HiddenField ID="hdfAccountSubHead" runat="server" />
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" TabIndex="9" ValidationGroup="submit" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" TabIndex="10" Text="Clear" OnClientClick="ClearAdd(); return false;" />
                                    <asp:Button Text="Hide" runat="server" ID="btnHide" OnClick="btnHide_Click" Style="display: none;" />
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
                            <%--style="display: none;"--%>
                            <ul class="contentul">
                                <li>
                                 
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" CssClass="textbox" TabIndex="16" runat="server"></asp:TextBox>
                                    </li>
                                    <li>
                                        <label class="label">Account Head </label>
                                        <asp:DropDownList ID="ddlAccountHeadSearch" runat="server" TabIndex="17" CssClass="dropdown"></asp:DropDownList>
                                    </li>


                                    <li class="btnMargin">

                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="18" Text="Search" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" TabIndex="19" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvAccountHeadSub" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="Teal" BorderStyle="Solid" AllowPaging="true" OnPageIndexChanging="gvAccountHeadSub_PageIndexChanging" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ID_AccountHeadSub" OnRowDeleting="gvAccountHeadSub_RowDeleting" OnSelectedIndexChanging="gvAccountHeadSub_SelectedIndexChanging">
                                  <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                     <%--   <asp:BoundField DataField="ShortName" HeaderText="Short Name" />--%>
                                        <asp:BoundField DataField="HeadSubName" HeaderText="Name" />
                                        <asp:BoundField DataField="AhsCode" HeaderText="Code" />
                                        <asp:BoundField DataField="SortOrder" HeaderText="Sort Order" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="AccountName" HeaderText="Account Head" />
                                      
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
                                    <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                              
                            </ul>
                        </div>
                               </div>
                        <%--<asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
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
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
