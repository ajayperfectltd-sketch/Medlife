<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Store.aspx.cs" Inherits="Store" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
       <script src="../../js/slidereveal.js"></script>
     <script src="../../js/slide.js"></script>
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
        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

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
            $("#<%#txtDate.ClientID%>").val('');
            $("#<%#hfStoreId.ClientID%>").val('0');
            $("#<%#ddlType.ClientID%>").val('0');
            $("#<%#ddlbranchcode.ClientID%>").val('0');
            $("#<%#txtbranchcode.ClientID%>").val('');
            $("#<%#txtOpeningliabitlty.ClientID%>").val('');
            $("#<%#txtopeningbalance.ClientID%>").val('');
            $("#<%#txtAddress1.ClientID%>").val('');
            $("#<%#txtAddress2.ClientID%>").val('');
            $("#<%#txtAddress3.ClientID%>").val('');
            $("#<%#txtPhone.ClientID%>").val('');
            $("#<%#txtEmail.ClientID%>").val('');
            $("#<%#txtWebsite.ClientID%>").val('');
            $('#<%#chkActive.ClientID%>').prop('checked', true);
            $("#<%#txtShortName.ClientID%>").focus();
        }
        function ClearSearch() {
            $("#<%#txtSearchShortName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchShortName.ClientID%>").focus();
            
        }
        function ShowAddDiv() {
            //ClearAdd();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#txtShortName.ClientID%>").focus();
        }
    
        function PopupOnReason() {
            try {

                $("#divPopupReason").css('display', 'block');
            }
            catch (ex) {
                //alert('popup' + ex);  
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"   autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="12000"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5"> 
                    <div class="heading2">
                    <div class="user">Store</div>
                         <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                         <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle"  OnClick="ImageButton1_Click" runat="server"  OnClientClick="ShowAddDiv(); ClearAdd(); releasebtn();" />
                       
                        <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle"    runat="server" OnClick="imgbtnView_Click" />
                         </div>
                    </asp:Panel>  
                        </div>
                        
                  
                    <div class="clear"></div>
                     <div class="group_bg2">
                 
                    <div class="main_bg">  
                           <div id="divAdd" runat="server" style="   display: block; ">    
                                      
                        <ul class="contentul">
                            <li>
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                <asp:TextBox ID="txtShortName" CssClass="textboxMandatory" runat="server" MaxLength="5" TabIndex="1"  ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Short name required" ControlToValidate="txtShortName" Display="None" SetFocusOnError="True"  ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                <asp:TextBox ID="txtName" CssClass="textboxMandatory" runat="server" MaxLength="250" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                </cc:ValidatorCalloutExtender>
                            </li>
                            <li>
                                 <label class="label">Start Date</label>
                                 <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtDate" TabIndex="3" AutoComplete="off" />
                                 <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                  <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                  <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                  </cc:ValidatorCalloutExtender>
                              </li>
                             <li>
                                <asp:Label ID="lblopeningbalance"   runat="server" CssClass="label" Text="Opening Balance"></asp:Label>
                                <asp:TextBox ID="txtopeningbalance" Style="text-align: right;" CssClass="textboxMandatory" runat="server" onkeypress="CheckNumWithDecimal(event);"  MaxLength="16" TabIndex="4"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Opening balance required" ControlToValidate="txtopeningbalance" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                </cc:ValidatorCalloutExtender>
                            </li>
                                   <li>
                                <asp:Label ID="lblOpeningliability"   runat="server" CssClass="label" Text="Opening Liability"></asp:Label>
                                <asp:TextBox ID="txtOpeningliabitlty" Style="text-align: right;" CssClass="textbox" runat="server" onkeypress="CheckNumWithDecimal(event);"  MaxLength="16" TabIndex="5"></asp:TextBox>
                             
                            </li>



                            <li>
                                 <asp:Label ID="Label7" runat="server" CssClass="label" Text="Type"></asp:Label>
                                <asp:DropDownList ID="ddlType" CssClass="dropdown" runat="server" TabIndex="6">
                                    <asp:ListItem Text="Main Office" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Branch Office" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </li>
                             <li>
                                 <asp:Label ID="lblbranchcode" runat="server" CssClass="label" Text="Branch Code"></asp:Label>
                 <%--               <asp:TextBox ID="txtbranchcode" MaxLength="50"  CssClass="textbox" TabIndex="5"  runat="server" Visible="false"></asp:TextBox>  --%>  
                                  <asp:TextBox runat="server" ID="txtbranchcode" onkeypress="CheckInteger(event);"  TabIndex="7" CssClass="textbox" MaxLength="3"  />                                                                        
                               <asp:DropDownList ID="ddlbranchcode" runat="server" CssClass="dropdown"  Visible="false">
                                   
                               </asp:DropDownList>
                                        
                            </li>
                  
                         
                               <li>
                                 <asp:Label ID="lbladdress1" runat="server" CssClass="label" Text="Address1"></asp:Label>
                                  <asp:TextBox runat="server" ID="txtAddress1"   TabIndex="8" CssClass="textbox" MaxLength="100"  />    
                            </li>
                                 <li>
                                 <asp:Label ID="lblAddress2" runat="server" CssClass="label" Text="Address2"></asp:Label>
                                  <asp:TextBox runat="server" ID="txtAddress2"  TabIndex="9" CssClass="textbox" MaxLength="100"  />    
                            </li>
                                 <li>
                                 <asp:Label ID="lblAddress3" runat="server" CssClass="label" Text="Address3"></asp:Label>
                                  <asp:TextBox runat="server" ID="txtAddress3"  TabIndex="10" CssClass="textbox" MaxLength="100" />    
                            </li>
                                 <li>
                                 <asp:Label ID="lblPhone" runat="server" CssClass="label" Text="Phone Number"></asp:Label>
                                  <asp:TextBox runat="server" ID="txtPhone"   TabIndex="11" CssClass="textbox" MaxLength="50" />    
                            </li>
                                 <li>
                                 <asp:Label ID="lblEmail" runat="server" CssClass="label" Text="Email"></asp:Label>
                                  <asp:TextBox runat="server" ID="txtEmail"   TabIndex="12" CssClass="textbox" MaxLength="50" />
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid email format" ControlToValidate="txtEmail" Display="None" ValidationGroup="submit" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                  <cc:ValidatorCalloutExtender ID="RegularExpressionValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RegularExpressionValidator1">
                                    </cc:ValidatorCalloutExtender>    
                            </li>
                                 <li>
                                 <asp:Label ID="lblWebsite" runat="server" CssClass="label" Text="Website"></asp:Label>
                                  <asp:TextBox runat="server" ID="txtWebsite"   TabIndex="13" CssClass="textbox" MaxLength="50" />    
                            </li>
                               <li>
                                   <asp:Label ID="lblActive" runat="server" CssClass="label" Text="Active"></asp:Label>
                                     <asp:CheckBox ID="chkActive" runat="server" Width="15"  TabIndex="14" /> 

                               </li>
                              <li>
                                 <asp:Label ID="lblSalesMan" runat="server" CssClass="label" Text="Sales Man"></asp:Label>
                                   <asp:DropDownList ID="ddlAgent" runat="server"  CssClass="dropdown" TabIndex="15"  ></asp:DropDownList>
                            </li>
                           
                            <li class="btnMargin">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="16"  OnClientClick="disablebutn();" />
                                <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="17" />
                                <asp:HiddenField ID="hfStoreId" runat="server" />
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
                                    <asp:Label ID="Label5" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchShortName" MaxLength="50" CssClass="textbox" runat="server" TabIndex="19"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchName" MaxLength="250" CssClass="textbox" runat="server" TabIndex="20"></asp:TextBox>
                                </li>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" TabIndex="21" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnSearchClear" runat="server" TabIndex="22" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                </li>
                            </li>
                        </ul>
                        <ul class="contentul">
                            <asp:GridView ID="gvStore" runat="server" CssClass="grid"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                BackColor="White"  BorderStyle="Solid"  AllowPaging="true" OnPageIndexChanging="gvStore_PageIndexChanging" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="ID_Store" OnRowDeleting="gvStore_RowDeleting" OnSelectedIndexChanging="gvStore_SelectedIndexChanging" PageSize="10">
                                <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                    <asp:BoundField DataField="StoName" HeaderText="Name" />
                                    <asp:BoundField DataField="Type" HeaderText="Type" />
                                      <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance" ItemStyle-HorizontalAlign="Right" />
                                   <%-- <asp:BoundField DataField="SalesMan" HeaderText="SalesMan" />
                                    <asp:BoundField DataField="Active" HeaderText="Active" />--%>
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
                                            <asp:ImageButton ID="ImageButton2" runat="server"  OnClick="ImageButton2_Click" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                         </div>
                 <%--   <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                    <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
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
    </form>
</body>
</html>
