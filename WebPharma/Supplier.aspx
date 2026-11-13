<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Supplier.aspx.cs" Inherits="Supplier" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc2" TagName="MedLifePageHelp" %>
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
        function BlurSubHead() {
            $("#btnSubHeadBlurTrigger").click();

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
           $("#<%#txtSuppliername.ClientID%>").val('');
            $("#<%#txtArea.ClientID%>").val('');
            $("#<%#txtPlace.ClientID%>").val('');
            $("#<%#txtPost.ClientID%>").val('');
            $("#<%#txtPin.ClientID%>").val('');
            $("#<%#txtLandmark.ClientID%>").val('');
            $("#<%#txtPost.ClientID%>").val('');
            $("#<%#ddlState.ClientID%>").val('0');
            $("#<%#ddlDistrict.ClientID%>").val('0');
            $("#<%#txtPhone.ClientID%>").val('');
            $("#<%#txtMobile.ClientID%>").val('');
            $("#<%#txtTinNo.ClientID%>").val('');
            $("#<%#txtKgstNo.ClientID%>").val('');
            $("#<%#txtCstNo.ClientID%>").val('');
            $("#<%#lblTInMsg.ClientID%>").attr('style', 'display:none;');
            $("#<%#txtEmail.ClientID%>").val('');
            $("#<%#txtFax.ClientID%>").val('');
            $("#<%#txtContractPerson.ClientID%>").val('');
           
            $("#<%#ddlCountry.ClientID%>").val('0');
            $("#<%#hfSupplierId.ClientID%>").val('0');
            $("#<%#txtWebsite.ClientID%>").val('');
            
            $('#<%#chkActive.ClientID%>').prop('checked', true);
            $('#<%#chkGovSupplier.ClientID%>').prop('checked', false);
           
           <%-- $("#<%#liSupplierName.ClientID%>").attr('style', 'display:none;')--%>
            $('#<%#liSupplierBalance.ClientID%>').hide();
          
                $("#<%#txtSuppliername.ClientID%>").focus();
        }
        function ClearSearch() {
           
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").focus();
        }
        function ShowAddDiv() {

           
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#txtSuppliername.ClientID%>").focus();
           
        }
    

        function ButtonClick() {


            $("#<%#btnGridFill.ClientID%>").click();
            $("#<%#ddlState.ClientID%>").focus();

            return false;

        }
        function ButtonClickState() {


            $("#<%#btnDistictFill.ClientID%>").click();
                 $("#<%#ddlDistrict.ClientID%>").focus();

                 return false;

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

        function EnterTab(e, Id1) {

            var intKey = window.Event ? e.which : e.KeyCode;


            if (intKey == 13) {
                e.returnValue = false;
                $("#" + Id1).focus();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"  autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5"> 
               <div class="heading2">
                    <div class="user">Supplier</div>
                         <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                        <uc2:MedLifePageHelp runat="server" ID="MedLifePageHelp" />
                         <asp:ImageButton ID="ImageButton1" ImageUrl="images/add.png" CssClass="imagebuttonStyle" OnClick="ImageButton1_Click"  TabIndex="25" runat="server" OnClientClick="ShowAddDiv();ClearAdd(); return false; releasebtn();" />
                       
                        <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle"  TabIndex="24"  runat="server" OnClick="imgbtnView_Click" />
                         </div>
                    </asp:Panel>  
                        
                    </div>
           
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                        <div id="divAdd" runat="server" style="display: none;">
                            <div class="divL">
                                <ul class="contentul">
                                 <li id="liSupplierName" runat="server">
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Supplier Name"></asp:Label>
                                        <asp:TextBox ID="txtSuppliername"  onkeydown="EnterTab(event,'txtContractPerson');" TabIndex="1" CssClass="textboxMandatory" runat="server" MaxLength="150"  ToolTip="Supplier" ></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtSuppliername" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                     <li>
                                        <asp:Label ID="Label19" runat="server" CssClass="label" Text="Contact Person"></asp:Label>
                                        <asp:TextBox ID="txtContractPerson" onkeydown="EnterTab(event,'txtArea');" CssClass="textbox" runat="server" MaxLength="75"  TabIndex="2"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Area"></asp:Label>
                                        <asp:TextBox ID="txtArea" onkeydown="EnterTab(event,'txtPlace');" CssClass="textbox"   runat="server" MaxLength="100"  TabIndex="3" ToolTip="Area"></asp:TextBox>
                                      
                                    </li>
                                   
                                    <li>
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Place"></asp:Label>
                                        <asp:TextBox ID="txtPlace"  onkeydown="EnterTab(event,'txtPost');" CssClass="textbox"  runat="server" MaxLength="100"  TabIndex="4"></asp:TextBox>
                                    </li>
                                        <li>
                                        <asp:Label ID="Label17" runat="server" CssClass="label" Text="Post"></asp:Label>
                                        <asp:TextBox ID="txtPost"  onkeydown="EnterTab(event,'txtPin');"  CssClass="textbox"  runat="server" MaxLength="50"  TabIndex="5"></asp:TextBox>

                                    </li>
                                         <li>
                                        <asp:Label ID="Label18" runat="server" CssClass="label" Text="Pin"></asp:Label>
                                        <asp:TextBox ID="txtPin" onkeydown="EnterTab(event,'txtLandmark');" CssClass="textbox"  runat="server" MaxLength="10"  TabIndex="6"></asp:TextBox>
                                             <asp:RegularExpressionValidator ID="revPin" runat="server" ErrorMessage="Pin must be a 6-digit number" ControlToValidate="txtPin" ValidationExpression="^\d{6}$" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                <cc:ValidatorCalloutExtender ID="vcePin" runat="server" Enabled="True" TargetControlID="revPin"></cc:ValidatorCalloutExtender>
                                    </li>
                                       <li>
                                        <asp:Label ID="Label21" runat="server" CssClass="label" Text="Landmark"></asp:Label>
                                        <asp:TextBox ID="txtLandmark"  onkeydown="EnterTab(event,'ddlCountry');" CssClass="textbox" runat="server" MaxLength="100"  TabIndex="7"></asp:TextBox>
                                    </li>
                                        <li>
                                    <asp:Label ID="Label3" runat="server" CssClass="label" Text="Country"></asp:Label>
                                    <asp:DropDownList ID="ddlCountry" onkeydown="EnterTab(event,'ddlState');" runat="server" CssClass="dropdown" onBlur="return ButtonClick();" TabIndex="8"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ErrorMessage="Country is required" ControlToValidate="ddlCountry" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="vceCountry" runat="server" Enabled="True" TargetControlID="rfvCountry"></cc:ValidatorCalloutExtender>
                                </li>
                                          <li>
                                    <asp:Label ID="Label23" runat="server" CssClass="label" Text="State"></asp:Label>
                                    <asp:DropDownList ID="ddlState"  onkeydown="EnterTab(event,'ddlDistrict');" runat="server" onBlur="return ButtonClickState();"  CssClass="dropdown" TabIndex="9"></asp:DropDownList>
                                              <asp:RequiredFieldValidator ID="rfvState" runat="server" ErrorMessage="State is required" ControlToValidate="ddlState" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="vceState" runat="server" Enabled="True" TargetControlID="rfvState"></cc:ValidatorCalloutExtender>
                                </li>
                                           <li>
                                    <asp:Label ID="Label22" runat="server" CssClass="label" Text="District"></asp:Label>
                                    <asp:DropDownList ID="ddlDistrict"  onkeydown="EnterTab(event,'txtFax');" runat="server" CssClass="dropdown" TabIndex="10"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDistrict" runat="server" ErrorMessage="District is required" ControlToValidate="ddlDistrict" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="vceDistrict" runat="server" Enabled="True" TargetControlID="rfvDistrict"></cc:ValidatorCalloutExtender>
                                </li>
                               
                                 
                                       <li>
                                        <asp:Label ID="Label20" runat="server" CssClass="label" Text="Fax"></asp:Label>
                                        <asp:TextBox ID="txtFax" onkeydown="EnterTab(event,'txtPhone');" CssClass="textbox" runat="server" MaxLength="50"  TabIndex="11"></asp:TextBox>
                                        <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtFax" ValidChars="+, -">
                                        </cc:FilteredTextBoxExtender>
                                    </li>   
                                      
                                    <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Phone"></asp:Label>
                                        <asp:TextBox ID="txtPhone"  onkeydown="EnterTab(event,'txtMobile');" CssClass="textbox" runat="server" MaxLength="50"  TabIndex="12"></asp:TextBox>
                                        <cc:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtPhone" ValidChars="+, -">
                                        </cc:FilteredTextBoxExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="Mobile"></asp:Label>
                                        <asp:TextBox ID="txtMobile"  onkeydown="EnterTab(event,'txtEmail');"  CssClass="textbox" runat="server" MaxLength="25"  TabIndex="13"></asp:TextBox>
                                        <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtMobile" ValidChars="+, -">
                                        </cc:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="revMobile" runat="server" ErrorMessage="Mobile number format is invalid" ControlToValidate="txtMobile" ValidationExpression="^[0-9+\-]{7,15}$" Display="None" ValidationGroup="submit" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                        <cc:ValidatorCalloutExtender ID="vceMobile" runat="server" Enabled="True" TargetControlID="revMobile"></cc:ValidatorCalloutExtender>
                                    </li>   
                                     <li>
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Text="Email"></asp:Label>
                                        <asp:TextBox ID="txtEmail" onkeydown="EnterTab(event,'txtWebsite');" CssClass="textbox" runat="server" MaxLength="100"  TabIndex="14"></asp:TextBox>
                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid email format" ControlToValidate="txtEmail" Display="None" ValidationGroup="submit" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <cc:ValidatorCalloutExtender ID="RegularExpressionValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RegularExpressionValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                          <li>
                                        <asp:Label ID="Label24" runat="server" CssClass="label" Text="Website"></asp:Label>
                                        <asp:TextBox ID="txtWebsite"  onkeydown="EnterTab(event,'txtTinNo');" CssClass="textbox" runat="server" MaxLength="100"  TabIndex="15"></asp:TextBox>
                                              <asp:RegularExpressionValidator ID="reqWebsite" runat="server" ErrorMessage="Invalid  format" ControlToValidate="txtWebsite" Display="None" ValidationGroup="submit" SetFocusOnError="True" ValidationExpression="^(http://)?\w+\.\w+\.\w+$"></asp:RegularExpressionValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqWebsite">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                                                     
                                </ul>
                            </div>
                            <div class="divR">
                                <ul class="contentul">
                                     <li id="liSupplierBalance" style="margin-bottom:15px;" runat="server">
                                        <asp:Label ID="lblSupplierBalance" Text="" CssClass="lblbalance" runat="server" />
                                    </li> 
                                      <li>
                                        <asp:Label ID="Label12" runat="server" CssClass="label" Text="GST No"></asp:Label>
                                        <asp:TextBox ID="txtTinNo"  onkeydown="EnterTab(event,'btnCheckTinNumber');" CssClass="textbox" runat="server" MaxLength="50"   TabIndex="16"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="GST Number required" ControlToValidate="txtTinNo" Display="None" SetFocusOnError="True" ValidationGroup="CheckTinNumber"></asp:RequiredFieldValidator>
                                           <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                        </cc:ValidatorCalloutExtender>
                                      
                                    </li> 
                                     <li class="btnMargin">
                                         <asp:Button Text="Availablity" ID="btnCheckTinNumber" onkeydown="EnterTab(event,'txtKgstNo');"  runat="server" TabIndex="17" ValidationGroup="CheckTinNumber" CssClass="button"  onclick="btnCheckTinNumber_Click" />
                                         <asp:Label ID="lblTInMsg"  Font-Size="15px" runat="server"   Font-Bold="true"  Text=""></asp:Label>
                                         
                                    </li>
                                     <li>
                                        <asp:Label ID="Label13" runat="server" CssClass="label" Text="KGST No"></asp:Label>
                                        <asp:TextBox ID="txtKgstNo" CssClass="textbox" onkeydown="EnterTab(event,'txtCstNo');" runat="server" MaxLength="50"  TabIndex="18"></asp:TextBox>
                                      
                                    </li> 
                                     <li>
                                        <asp:Label ID="Label15" runat="server" CssClass="label" Text="CST No"></asp:Label>
                                        <asp:TextBox ID="txtCstNo" CssClass="textbox" onkeydown="EnterTab(event,'chkGovSupplier');" runat="server" MaxLength="50"  TabIndex="19"></asp:TextBox>
                                     
                                    </li>    
                                     <li>
                                    <asp:Label ID="Label10" runat="server" CssClass="label" Text="Government Supplier"></asp:Label>
                                <asp:CheckBox ID="chkGovSupplier" onkeydown="EnterTab(event,'chkActive');" runat="server" TabIndex="20" />
                                   </li>  
                                    <%-- <li class="btnMargin">
                                        <asp:CheckBox ID="chkGovSupplier"  Width="175px" runat="server" CssClass="chckbox" Text="Government Supplier"   TabIndex="20"/>
                                    </li>  --%>  
                                    <li>
                                        <asp:Label ID="lblActive" runat="server" CssClass="label" Text="Active"></asp:Label>
                                        <asp:CheckBox ID="chkActive" onkeydown="EnterTab(event,'btnGridFill');"  runat="server" CssClass="chckbox"   TabIndex="21"/>
                                        </li>
                                       <asp:Button ID="btnGridFill" runat="server" Text="Hide" OnClick="btnSubFill_Click" Style="display: none;" CausesValidation="false"  />
                                        <asp:Button ID="btnDistictFill" runat="server" Text="Hide" OnClick="btnDistictFill_Click" Style="display: none;" CausesValidation="false"  />
                                         
                                       <li class="clear"></li>
                                 
                                </ul>
                            </div>
                            <div class="clear"></div>
                            <div>
                                <ul class="contentul">
                                    <li class="btnMargin">
                                          <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="22"   />
                                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;"  TabIndex="23" />
                                        <asp:HiddenField ID="hfSupplierId" runat="server" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                         <asp:HiddenField ID="hfIsDelete" runat="server" />
                                         <asp:HiddenField ID="hfuserName" runat="server" />
                                         <asp:HiddenField ID="hfMasterID" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                         <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    </li>
                                </ul>
                            </div>
                        </div>
                            </div>
                        <div class="main_bg">
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                  
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" CssClass="textbox" MaxLength="150" runat="server" TabIndex="26"></asp:TextBox>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="27" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="28" />
                                     <asp:Button ID="btnSubHeadBlurTrigger" CausesValidation="false" Text="text" runat="server" Style="display: none;" OnClick="btnSubHeadBlurTrigger_Click" />
                                           
                                         </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvSupplier" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" OnPageIndexChanging="gvSupplier_PageIndexChanging" DataKeyNames="ID_Supplier" OnRowDeleting="gvSupplier_RowDeleting" OnSelectedIndexChanging="gvSupplier_SelectedIndexChanging" AllowPaging="True">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SlNo" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center" />
                                        
                                          <asp:TemplateField HeaderText="Name " >
                                            <ItemTemplate>
                                                <div style="width: 200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                    <%# Eval("SupplierName") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Email" HeaderText="Email" />
                                        <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
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
                                                <asp:ImageButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle  CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
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
                       <%-- <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                    </div>

   <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none; " CssClass="DivStyle">
                            <div id="divheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Reason"></asp:Label>
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
                                    <asp:Label ID="Label5" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
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
       
        <script>
            function SetTabIndex() {
                try{
                    var fInd = 0;
                    $("input[type='submit'],input[type='image'],input[type='button'],input[type='text'],input[type='select'],textarea").each(function (index, value) {
                        if ($(value).attr("type") != "hidden") {
                            $(value).attr("tabindex", fInd.toString());
                       
                            if ($(value).attr("type") == "text") {
                                $(value).attr("autocomplete", "off");
                            }
                            fInd++;
                        }
                    });
                }
                catch (e) {
                    alert(e);
                }
            }
        </script>
    </form>
</body>
</html>
