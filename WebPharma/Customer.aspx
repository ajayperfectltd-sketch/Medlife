<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="Customer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<%@ Register Src="~/UC/MedLifePageHelp.ascx" TagPrefix="uc1" TagName="MedLifePageHelp" %>

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
        function ButtonClick() {
            $("#btnGridFill").click();
            $("#<%#ddlState.ClientID%>").focus();
            return false;
        }
            function ButtonClickState() {

            $("#btnDistictFill").click();
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
           
            $("#<%#txtCustCode.ClientID%>").val(''); 
            $("#<%#txtName.ClientID%>").val('');
            $("#<%#txtArea.ClientID%>").val('');
            $("#<%#txtPlace.ClientID%>").val('');
            $("#<%#txtPin.ClientID%>").val('');
            $("#<%#txtPost.ClientID%>").val('');
            $("#<%#txtLandmark.ClientID%>").val('');
            $("#<%#txtPhone.ClientID%>").val('');
            $("#<%#txtMobile.ClientID%>").val('');
            $("#<%#txtEmail.ClientID%>").val('');
            $("#<%#txtInactiveRemarks.ClientID%>").val('');
            $("#<%#ddlCountry.ClientID%>").val('0');
            $("#<%#ddlState.ClientID%>").val('0');
            $("#<%#ddlDistrict.ClientID%>").val('0');
            $("#<%#txtGSTTIN.ClientID%>").val('');
            $("#<%#ddlIdpType.ClientID%>").val('0');
            $("#<%#ddlCustomerGroup.ClientID%>").val('0');
            $("#<%#Textcardnumber.ClientID%>").val('');
            $("#<%#hfCustomerId.ClientID%>").val('0');
            $("#<%#txtCreditDays.ClientID%>").val('0');
            $("#<%#txtDiscountPercentage.ClientID%>").val('0');
            $("#<%#txtCreditLimit.ClientID%>").val('0');
            $("#ddlEmployee").val('0');
            $('#<%#chkActive.ClientID%>').prop('checked', true);
            var date = new Date().format("dd/MM/yyyy");
            $("#<%#txtDob.ClientID%>").val(date);
            $('#<%#chkISEmployee.ClientID%>').prop('checked', false);

            $("#liCustShortName").attr('style', 'display:none');
            var toDate = new Date();
            $("#txtCurrentDate").val(toDate.format("dd/MM/yyyy"));
            $('#<%#lblCustomerBalance.ClientID%>').prop('visible', false);
            $('#<%#liCustomerBalance.ClientID%>').hide();
            Remarks();
            $('#<%#ChkCreditSale.ClientID%>').prop('checked', false);
            $('#<%#ChkCardPrice.ClientID%>').prop('checked', false);
            $("#<%#ddlCustomerGroup.ClientID%>").focus();
           
        }
    
  

        function ClearSearch() {
            $("#<%#txtSearchCustCode.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchMobile.ClientID%>").val('');
            $("#ddlEmployee").val('0');
            $('#<%#txtSearchCustCode.ClientID%>').focus();
          $("#ddlactive").val('2');
          

        }
        function ShowAddDiv() {
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
        }
        function ShowAddDiv1() {
          
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $('#<%#liCustomerBalance.ClientID%>').hide();
          
        }

        function BlurCreditlimit() {
         
            $("#<%#btBlurCreditlimit.ClientID%>").click();
            $("#<%#txtCreditDays.ClientID%>").focus();

            

        }
        function BlurCreditDays() {
            $("#<%#btBlurCreditDays.ClientID%>").click();
            $("#<%#txtDiscountPercentage.ClientID%>").focus();

            }
        
        function Remarks() {
            if ($('#chkActive').is(":checked")) {
                $("#liInactiveRemarks").attr('style', 'display:none;');
                $("#liInactiveRemarks").hide();

            }
            else {
                $("#liInactiveRemarks").attr('style', 'display:block;');

                $("#liInactiveRemarks").show();
            }

        }
 
        function pageLoad() {
           // alert('i');
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

    <form id="frmCustomer" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               <div class="btn_bg5"> 
                         <div class="heading2">
                               <div class="user">Customer</div>
                              <asp:Panel ID="divMenuButtons" runat="server">
                                 
                    <div style="float: right; margin-top: -10px">

                        <uc1:MedLifePageHelp runat="server" ID="MedLifePageHelp" />

                           <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle"  onmouseout="this.src='images/ADD.png'"   onmouseover="this.src='images/ADD.png'" TabIndex="39" runat="server" OnClientClick="ShowAddDiv1();  ClearAdd(); releasebtn();" OnClick="ImageButton1_Click" />
                        <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png"  CssClass="imagebuttonStyle" TabIndex="38" onmouseout="this.src='images/view.png'"   onmouseover="this.src='images/view.png'" runat="server" OnClick="imgbtnView_Click" />
                          </div>
                                  </asp:Panel>
                             </div>
                   
                      

                        <div class="search" style="display: none;">&nbsp&nbsp Export</div>

                        <div class="search_icon" style="display: none;">
                            <asp:ImageButton ID="imgbtnExport" ImageUrl="~/images/Export.png" ToolTip="Export" Width="37" Height="38" runat="server" OnClientClick="PopUp();return false;" />
                            <asp:HiddenField ID="hdfSearchCriteria" runat="server" />
                        </div>
              
                    <div class="clear"></div>
                    <div class="group_bg2">
                          <div class="main_bg">  

                        <div id="divAdd" runat="server">
                            <%--style="display: none;   --%>
                            <div class="divL">
                                <ul class="contentul">
                                  
                                       <li>
                                        <asp:Label ID="Label17" runat="server" CssClass="label" Text="Customer Group"></asp:Label>
                                        <asp:DropDownList ID="ddlCustomerGroup" runat="server" CssClass="dropdownMandatory" TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomerGroup_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select a customer group" ControlToValidate="ddlCustomerGroup" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                    <li>
                                        <asp:Label ID="liCustCode" runat="server" CssClass="label" Text="Customer Code"></asp:Label>
                                        <asp:TextBox ID="txtCustCode" CssClass="textboxMandatory" runat="server" AutoComplete="off" MaxLength="20" TabIndex="12"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="reqcode" runat="server" ErrorMessage="code required" ControlToValidate="txtCustCode" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqcode">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtName" CssClass="textboxMandatory" ClientIDMode="Static" onkeypress="return EnterEvent(event)" runat="server" MaxLength="25" AutoComplete="off" TabIndex="13"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name required" ControlToValidate="txtName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                          <li>
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Area"></asp:Label>
                                        <asp:TextBox ID="txtArea" CssClass="textbox"   runat="server" MaxLength="100"  TabIndex="14" ToolTip="Area"></asp:TextBox>
                                      
                                    </li>
                                         <li>
                                        <asp:Label ID="Label27" runat="server" CssClass="label" Text="Place"></asp:Label>
                                        <asp:TextBox ID="txtPlace" CssClass="textbox"  runat="server" MaxLength="100"  TabIndex="15"></asp:TextBox>
                                    </li>
                                        <li>
                                        <asp:Label ID="Label28" runat="server" CssClass="label" Text="Post"></asp:Label>
                                        <asp:TextBox ID="txtPost" CssClass="textbox"  runat="server" MaxLength="50"  TabIndex="16"></asp:TextBox>
                                    </li>
                                          <li>
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Pin"></asp:Label>
                                        <asp:TextBox ID="txtPin" CssClass="textbox"  runat="server" MaxLength="6"  TabIndex="17"></asp:TextBox>
                                           <cc:FilteredTextBoxExtender ID="txtPin_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Numbers" TargetControlID="txtPin">
                                        </cc:FilteredTextBoxExtender>
                                             <asp:RegularExpressionValidator ID="revPin" runat="server" ErrorMessage="Pin should be 6 digits" ControlToValidate="txtPin" Display="None" SetFocusOnError="True" ValidationGroup="submit" ValidationExpression="\d{6}"></asp:RegularExpressionValidator>
                                            <cc:ValidatorCalloutExtender ID="vcePin" runat="server" Enabled="True" TargetControlID="revPin">
                                            </cc:ValidatorCalloutExtender>
                                    </li>
                                        <li>
                                        <asp:Label ID="Label25" runat="server" CssClass="label" Text="Landmark"></asp:Label>
                                        <asp:TextBox ID="txtLandmark" CssClass="textbox" runat="server" MaxLength="100"  TabIndex="18"></asp:TextBox>
                                    </li>
                                 
                                    <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Phone"></asp:Label>
                                        <asp:TextBox ID="txtPhone" CssClass="textbox" runat="server" MaxLength="50" AutoComplete="off" TabIndex="19"></asp:TextBox>
                                        <cc:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtPhone" ValidChars="+, -">
                                        </cc:FilteredTextBoxExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="Mobile"></asp:Label>
                                        <asp:TextBox ID="txtMobile" CssClass="textbox" runat="server" MaxLength="25" AutoComplete="off" TabIndex="20"></asp:TextBox>
                                        <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtMobile" ValidChars="+, -">
                                        </cc:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="revMobile" runat="server" ErrorMessage="Enter a valid mobile number" ControlToValidate="txtMobile" Display="None" SetFocusOnError="True" ValidationGroup="submit" ValidationExpression="[0-9+\-]{7,15}"></asp:RegularExpressionValidator>
                                        <cc:ValidatorCalloutExtender ID="vceMobile" runat="server" Enabled="True" TargetControlID="revMobile">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Text="Email"></asp:Label>
                                        <asp:TextBox ID="txtEmail" CssClass="textbox" runat="server" MaxLength="100" AutoComplete="off" TabIndex="21"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid email format" ControlToValidate="txtEmail" Display="None" ValidationGroup="submit" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        <cc:ValidatorCalloutExtender ID="RegularExpressionValidator1_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RegularExpressionValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label11" runat="server" CssClass="label" Text="DOB"></asp:Label>
                                        <asp:TextBox ID="txtDob" CssClass="textbox" runat="server" AutoComplete="off" TabIndex="22"></asp:TextBox>
                                        <asp:CompareValidator ID="cmpCurrentDate" runat="server" ErrorMessage="DOB cannot be greater than current date" ControlToCompare="txtCurrentDate" ControlToValidate="txtDob" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="cmpCurrentDate">
                                        </cc:ValidatorCalloutExtender>
                                        <cc:CalendarExtender ID="txtDob_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtDob">
                                        </cc:CalendarExtender>
                                          <asp:RangeValidator runat="server" ID="RangeValidator4" Type="Date"
                                                                        ControlToValidate="txtDob"
                                                                        MaximumValue="9999/12/28"
                                                                        MinimumValue="1000/12/28"
                                                                        ErrorMessage="Enter valid date"
                                                                        Display="None" ValidationGroup="submit" />
                                                                    <cc:ValidatorCalloutExtender
                                                                        ID="ValidatorCalloutExtender3"
                                                                        runat="server"
                                                                        Enabled="True"
                                                                        TargetControlID="RangeValidator4">
                                                                    </cc:ValidatorCalloutExtender>
                                    </li>
                                
                                    <li>
                                    <asp:Label ID="Label22" runat="server" CssClass="label" Text="Identity Proof"></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlIdpType" CssClass="dropdown" TabIndex="23" Width="223px">
                                    </asp:DropDownList>
                                   
                                   
                                </li>
                                     <li>
                                        <asp:Label ID="Label23" runat="server" CssClass="label" Text="Card Number"></asp:Label>
                                        <asp:TextBox ID="Textcardnumber" CssClass="textbox" runat="server" AutoComplete="off" MaxLength="50" TabIndex="24"></asp:TextBox>
                                         
                                    </li>
                               
                                </ul>
                            </div>
                            <div id="divPassDate" style="display: none">
                                <asp:TextBox ID="txtCurrentDate" runat="server" CssClass="textbox" ></asp:TextBox>
                            </div>
                            <div class="divR">
                                <ul class="contentul">
                                    <li id="liCustomerBalance" style="margin-bottom: 15px;" runat="server">
                                        <asp:Label ID="lblCustomerBalance" Text="" CssClass="lblbalance" runat="server" Visible="false" />
                                    </li>
                                    <li>
                                        <asp:Label ID="Label16" runat="server" CssClass="label" Text="Country"></asp:Label>
                                        <asp:DropDownList ID="ddlCountry" runat="server" onBlur="return ButtonClick();" CssClass="dropdown" TabIndex="25"></asp:DropDownList>
                                    </li>
                                        <li>
                                        <asp:Label ID="Label12" runat="server" CssClass="label" Text="State"></asp:Label>
                                        <asp:DropDownList ID="ddlState" runat="server" onBlur="return ButtonClickState();" CssClass="dropdown" TabIndex="26"></asp:DropDownList>
                                    </li>
                                        <li>
                                        <asp:Label ID="Label19" runat="server" CssClass="label" Text="District"></asp:Label>
                                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="dropdown" TabIndex="27"></asp:DropDownList>
                                    </li>
                                    <li class="clear"></li>
                               
                                    <li>
                                        <asp:Label ID="Label20" runat="server" CssClass="label" Text="Credit Limit"></asp:Label>
                                        <asp:TextBox ID="txtCreditLimit" CssClass="textbox" TabIndex="28" onblur=" return BlurCreditlimit();" Style="text-align: right;" onkeydown="EnterTab(event,'txtCreditDays');"  runat="server" MaxLength="10"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label21" runat="server" CssClass="label" Text="Credit Days"></asp:Label>
                                        <asp:TextBox ID="txtCreditDays" CssClass="textbox" TabIndex="29" Style="text-align: right;" onblur=" return BlurCreditDays();" onkeydown="EnterTab(event,'txtDiscountPercentage');"  runat="server" MaxLength="4"></asp:TextBox>
                                        <asp:RangeValidator ID="rvDiscount" runat="server" ErrorMessage="Discount must be between 0 and 100" ControlToValidate="txtDiscountPercentage" Type="Double" MinimumValue="0" MaximumValue="100" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="vceDiscount" runat="server" Enabled="True" TargetControlID="rvDiscount">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                     <li>
                                        <asp:Label ID="Label35" Text="Discount %" runat="server" CssClass="label" />
                                        <asp:TextBox runat="server" ID="txtDiscountPercentage" CssClass="textbox" Style="text-align: right;"  onkeypress="CheckNumWithDecimal(event);" MaxLength="14" TabIndex="30" />
                                    </li>

                                    <li>
                                        <asp:Label ID="Label18" Text="GSTIN No" runat="server" CssClass="label" />
                                        <asp:TextBox runat="server" ID="txtGSTTIN" CssClass="textbox"  MaxLength="25" TabIndex="31" />
                                        <asp:RegularExpressionValidator ID="revGSTIN" runat="server" ErrorMessage="Enter a valid GSTIN (15 characters)" ControlToValidate="txtGSTTIN" Display="None" SetFocusOnError="True" ValidationGroup="submit" ValidationExpression="[0-9]{2}[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}[1-9A-Za-z]{1}Z[0-9A-Za-z]{1}"></asp:RegularExpressionValidator>
                                        <cc:ValidatorCalloutExtender ID="vceGSTIN" runat="server" Enabled="True" TargetControlID="revGSTIN">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li >
                                    <asp:Label ID="lblPacked" runat="server" CssClass="label" Text="Enable Credit Sale"></asp:Label>
                                <asp:CheckBox ID="ChkCreditSale" runat="server"  TabIndex="32" />
                                   </li>
                                      <li >
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Enable Card Price"></asp:Label>
                                <asp:CheckBox ID="ChkCardPrice" runat="server"  TabIndex="33" />
                                   </li>
                                            <li >
                                    <asp:Label ID="Label10" runat="server" CssClass="label" Text="Active"></asp:Label>
                                <asp:CheckBox ID="chkActive" runat="server"  onchange="Remarks();" TabIndex="34" />
                                   </li>
                                             <li >
                                    <asp:Label ID="Label13" runat="server" CssClass="label" Text="Employee"></asp:Label>
                                <asp:CheckBox ID="chkISEmployee" runat="server"  TabIndex="35" />
                                   </li>

                                   <%-- <li class="btnMargin">
                                        <asp:CheckBox ID="ChkCreditSale" runat="server" CssClass="chckbox" Width="150px" Text="Enable CreditSale" TabIndex="30" />
                                    </li>
                                    <li class="btnMargin">
                                        <asp:CheckBox ID="ChkCardPrice" runat="server"  CssClass="chckbox" Width="150px" Text="Enable CardPrice" TabIndex="31" />
                                    </li>
                                     <li class="btnMargin">
                                        <asp:CheckBox ID="chkActive" runat="server" CssClass="chckbox" Text="Active" TabIndex="32" onchange="Remarks();" />
                                    </li>
                                      <li class="btnMargin">
                                        <asp:CheckBox ID="chkISEmployee" runat="server" CssClass="chckbox" Text="Employee" TabIndex="33" />
                                    </li>--%>
                                    <li id="liInactiveRemarks" style="display: none;" runat="server">
                                        <asp:Label ID="Label14" runat="server" CssClass="label" Text="Inactive Remarks"></asp:Label>
                                        <asp:TextBox ID="txtInactiveRemarks" MaxLength="150" CssClass="multiline" TextMode="MultiLine"  runat="server"></asp:TextBox>
                                    </li>
                                   

                                </ul>
                            </div>
                            <div style="width: 188px; padding-left: 64px;" class="divL">


                             <%--   <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" Visible="false">
                                    <ContentTemplate>


                                        <li>
                                            <asp:Image ID="imgAttachment" Width="150" CssClass="AttachImage" ImageUrl="~/images/DefaultImage.jpg" Height="150" runat="server" Style="float: left;" />
                                            <br />
                                            <asp:LinkButton ID="lbtnRemoveAttachment" runat="server" OnClick="lbtnRemoveAttachment_Click" Style="display: none;">Remove</asp:LinkButton>

                                        </li>
                                        <li class="clear"></li>
                                        <li class="clear"></li>
                                        <li>
                                            <cc:AsyncFileUpload ID="fupImage" runat="server" OnClientUploadStarted="uploadStarted" CssClass="imageUploaderField" TabIndex="30" OnUploadedComplete="fupImage_UploadedComplete" OnClientUploadComplete="UploadComplete1" />
                                            <asp:Button ID="btnHiddenImage" runat="server" Text="Image" OnClick="btnHiddenImage_Click" CausesValidation="false" Style="display: none;" />
                                            <asp:HiddenField ID="hdfImage" runat="server" />
                                        </li>
                                        <li class="clear"></li>
                                       
                                        <li>
                                       <asp:Button ID="btnHidden" Style="display: none; float: right" runat="server" Text="Button" OnClick="btnHidden_Click" />
                                        </li>
                                        <li class="clear"></li>
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                  <asp:Button ID="btnDistictFill" runat="server" Text="Hide" OnClick="btnDistictFill_Click"  Style="display: none;" CausesValidation="false"  />
                                    <asp:Button ID="btnGridFill"  runat="server" Text="Hide" OnClick="btnGridFill_Click" Style="display: none;" CausesValidation="false"  />

                            </div>
                            <div class="clear">
                            </div>
                            <div>
                                <ul class="contentul">
                                    <li class="btnMargin">
                                         <asp:Button ID="btnCustomerLinking" runat="server" CssClass="button" Text="Customer Linking"  Width="150"  OnClick="btnCustomerLinkingt_Click" />
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" TabIndex="36"  OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd();" TabIndex="37" OnClick="btnClear_Click" />

                                        <asp:HiddenField ID="hfCustomerId" runat="server" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:HiddenField ID="hfUrl" runat="server" />
                                        <asp:HiddenField ID="HdValuestore" runat="server" />
                                        <asp:HiddenField ID="hfuserName" runat="server" />
                                         <asp:HiddenField ID="hfMasterID" runat="server" />
                                           <asp:Button ID="btBlurCreditlimit" Text="Hide" runat="server" Style="display: none;" CausesValidation="false" OnClick="btBlurCreditlimit_Click"/>
                                        <asp:Button ID="btBlurCreditDays" Text="Hide" runat="server" Style="display: none;" CausesValidation="false"  OnClick="btBlurCreditDays_Click"/>

                                    </li>
                                </ul>
                            </div>
                        </div>
                              </div>
                          <div class="main_bg">  

                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <li style="display:none">
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Customer Code"></asp:Label>
                                        <asp:TextBox ID="txtSearchCustCode" CssClass="textbox" runat="server" TabIndex="40" MaxLength="10"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" TabIndex="41" MaxLength="25"></asp:TextBox>
                                    </li>
                                       <li>
                                        <asp:Label ID="Label24" runat="server" CssClass="label" Text="Mobile"></asp:Label>
                                        <asp:TextBox ID="txtSearchMobile" CssClass="textbox" runat="server" MaxLength="25" AutoComplete="off" TabIndex="42"></asp:TextBox>
                                        <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" FilterType="Custom, Numbers" TargetControlID="txtSearchMobile" ValidChars="+, -">
                                        </cc:FilteredTextBoxExtender>
                                    </li>

                                      <li>
                                        <asp:Label ID="lblactive" runat="server" CssClass="label" Text="Status"></asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlactive" CssClass="dropdown" TabIndex="43">
                                       
                                        <asp:ListItem Text="ACTIVE"  Value="1" />
                                          <asp:ListItem Text="INACTIVE" Value="0" />  
                                        <asp:ListItem Text="ALL" Value="2" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                    </li>
                                       





                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="44" Text="Search" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" TabIndex="45" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                    </li>
                                </li>
                            </ul>
                            <div class="clear"></div>
                            <ul class="contentul">
                                <asp:GridView ID="gvCustomer" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White"  BorderColor="Teal" BorderStyle="Solid"  BorderWidth="1px" CellPadding="3" ForeColor="Black"  OnPageIndexChanging="gvCustomer_PageIndexChanging" AllowPaging="true" GridLines="Vertical" PageSize="10" DataKeyNames="ID_Customer" OnRowDeleting="gvCustomer_RowDeleting" OnSelectedIndexChanging="gvCustomer_SelectedIndexChanging" OnRowCommand="gvCustomer_RowCommand">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SlNo" HeaderText="SlNo" ItemStyle-HorizontalAlign="Center"  />
                                         <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" ItemStyle-HorizontalAlign="left" />
                                        <asp:TemplateField HeaderText="Name ">
                                            <ItemTemplate>
                                                <div style="width: 200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                    <%# Eval("CustomerName") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                      
                              
                                         <asp:BoundField DataField="Mobile" HeaderText="Mobile" ItemStyle-HorizontalAlign="left" />

                                        <asp:BoundField DataField="Active" HeaderText="Active" ItemStyle-HorizontalAlign="left" />

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
                                                <asp:ImageButton ID="ImageButton2" OnClick="ImageButton2_Click" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                         </div>
                 
                      <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server"  CssClass="DivStyle" Style="display: none;">
                            <div id="divheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader1" runat="server" Text="Reason" ></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores">
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
