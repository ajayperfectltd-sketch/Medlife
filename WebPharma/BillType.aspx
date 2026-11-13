<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillType.aspx.cs" Inherits="BillType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>

<%@ Register src="Popup/CommonPopup.ascx" tagname="CommonPopup" tagprefix="uc1" %>
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

        function pageLoad() {
            $("#<%#chkCredit.ClientID%>").change(function () {
                if (this.checked) {

                    ValidatorEnable($("[id$='rfvcreditAccHead']")[0], true);
                    ValidatorEnable($("[id$='ValidatorCalloutExtendercreditAccHead']")[0], true);
                }
                else {
                    ValidatorEnable($("[id$='rfvcreditAccHead']")[0], false);
                    ValidatorEnable($("[id$='ValidatorCalloutExtendercreditAccHead']")[0], false);

                }
            });
        }
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

        function BlurSubHead() {
            $("#btnSubHeadBlurTrigger").click();

        }
        
        function BlurCardSubHead() {
            $("#btnCardSubHeadBlurTrigger").click();

        }
        function BlurChequeSubHead() {
            $("#btnChequeSubHeadBlurTrigger").click();

        }
        
        function BlurVATSubHead() {
            $("#btnVatSubHeadBlurTrigger").click();

        }
        function BlurCGSTSubHead() {
            $("#btnCGSTSubHeadBlurTrigger").click();
        }
        function BlurSGSTSubHead() {
            $("#btnSGSTSubHeadBlurTrigger").click();
        }
        function BlurIGSTSubHead() {
            $("#btnIGSTSubHeadBlurTrigger").click();
        }
        function BlurCESSSubHead() {
            $("#btnCESSSubHeadBlurTrigger").click();
        }
        function BlurRoundingSubHead() {
            $("#btnRoundingSubHeadBlurTrigger").click();
        }
        function BlurFrightSubHead() {
            $("#btnFrightSubHeadBlurTrigger").click();
        }
        function BlurDiscountSubHead() {
            $("#btnDiscountSubHeadBlurTrigger").click();
        }


        function BlurLoadingSubHead() {
            $("#btnLoadingSubHeadBlurTrigger").click();

        }
        function BlurUnloadingSubHead() {
            $("#btnUnloadingSubHeadBlurTrigger").click();

        }
        function BlurDisplaySubHead() {
            $("#btnDisplaySubHeadBlurTrigger").click();

        }
        function BlurOthersSubHead() {
            $("#btnOthersSubHeadBlurTrigger").click();

        }
        function BlurAccountSubReturn() {
            $("#btnAccountSubReturn").click();

        }
        
        function ClearAdd() {
            submit = 0;
           

            $("#<%#txtRefno.ClientID%>").val('');
            $("#<%#txtRefno1.ClientID%>").val('');
      
            $("#<%#txtRefno28.ClientID%>").val('');
            $("#<%#txtRefno29.ClientID%>").val('');
            $("#<%#txtRefno30.ClientID%>").val('');
            $("#<%#txtRefno31.ClientID%>").val('');
            $("#<%#txtRefno32.ClientID%>").val('');
            $("#<%#txtRefno24.ClientID%>").val('');
            $("#<%#txtRefno25.ClientID%>").val('');
  

            $("#<%#txtAccountHead.ClientID%>").val('');
            $("#<%#txtAccountHeadSub.ClientID%>").val('');
        

            $("#<%#txtAccountHead.ClientID%>").val('');

        

            $("#<%#txtreturnacchead.ClientID%>").val('');
            $("#<%#txtreturnaccheadsub.ClientID%>").val('');

            $("#<%#txtDiscountAccHead.ClientID%>").val('');
            $("#<%#txtlDiscountAccSubHead.ClientID%>").val('');
            $("#<%#txtRoundingAccountHead.ClientID%>").val('');
             $("#<%#txtRoundingAccountHeadsub.ClientID%>").val('');
            $("#<%#txtcreditAccHead.ClientID%>").val('');
            $("#<%#txttypeName.ClientID%>").val('');
            $("#<%#hfBillTypeId.ClientID%>").val('0');
            $("#<%#ddlBillType.ClientID%>").val('0');
            $('#<%#chkGovtSuppliers.ClientID%>').prop('checked', false);
            $('#<%#chkCredit.ClientID%>').prop('checked', false);
          
           
            $('#txttypeName').focus();
        }
       
        function ClearSearch() {

            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").focus();
        }
        function ShowAddDiv() {
            //ClearAdd();
            
            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();

            
            $('#txttypeName').focus();
           
        }
        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);

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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <div class="btn_bg5"> 
                  <div class="heading2">
                    <div class="user">Bill Type</div>
                   <asp:Panel ID="divMenuButtons" runat="server">
                        <div style="float: right; margin-top: -10px">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png"  CssClass="imagebuttonStyle"      TabIndex="19" runat="server"  OnClientClick=" EnterKeyFilter();  ClearAdd();  return true; releasebtn(); ShowAddDiv(); " OnClick="ImageButton1_Click1" />
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="imagebuttonStyle"     TabIndex="18"  runat="server"  OnClick="imgbtnView_Click" />
                        </div>
                       </asp:Panel>

                    </div>


                    <div class="clear"></div>
                    <div class="group_bg2">
                         <div class="main_bg">  
                        <div id="divAdd" runat="server" >
                            <div class="divL">
                            <ul class="contentul">

                                <li>
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Style="width:160px;" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txttypeName" CssClass="textboxLongMandatory" runat="server" MaxLength="75" Width="250" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage=" Name required" ControlToValidate="txttypeName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Style="width:160px;" Text="Type"></asp:Label>
                                    <asp:DropDownList ID="ddlBillType" runat="server" CssClass="dropdownMandatory" TabIndex="2"  Style="width:255px;"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqvalddlBillType" runat="server" ErrorMessage="Type required" ControlToValidate="ddlBillType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalddlBillType">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <%--<li>
                                    <asp:Label ID="Label17" runat="server" CssClass="label" Style="width:160px;" Text="Account Head"></asp:Label>
                                    <asp:DropDownList ID="ddlAccountHead" runat="server" onblur="BlurSubHead();" TabIndex="3" CssClass="dropdownMandatory"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select an account  head" ControlToValidate="ddlAccountHead" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>--%>
                                 <li>
                                    <asp:Label ID="lblAcvountHead" runat="server" CssClass="label" Text="Account Head" Width="160px"></asp:Label>
                                    <asp:TextBox ID="txtRefno" runat="server" AutoPostBack="true" CssClass="textboxMandatory" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="btnGo" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="btnGo_Click" style="margin-bottom:-8px;" TabIndex="3" />
                                         
                             
                                    <asp:TextBox ID="txtAccountHead" runat="server" Width="160px" CssClass="textboxMandatory" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="rfvAccountHead" runat="server" ErrorMessage="Account Head Refno required" ControlToValidate="txtAccountHead" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfvAccountHead">
                                    </cc:ValidatorCalloutExtender>
                                 <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Account Head required" ControlToValidate="txtAccountHead" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc:ValidatorCalloutExtender>--%>

                                </li>
                                <li>
                                    <asp:Label ID="Label18" runat="server" CssClass="label" Style="width:160px;" Text="Acc Sub Head"></asp:Label>
                                     <asp:TextBox ID="txtRefno1" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonheadsub" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonheadsub_Click" style="margin-bottom:-8px;" TabIndex="4" />
                                         
                             
                                    <asp:TextBox ID="txtAccountHeadSub" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                    <%--<asp:DropDownList ID="ddlAccountHeadSub" runat="server" CssClass="dropdown" TabIndex="4"></asp:DropDownList>--%>
                                  
                                </li>
                              
                              


                                 <li>
                                    <asp:Label ID="Label32" runat="server" CssClass="label" Style="width:160px;" Text="Discount AccHead"></asp:Label>
                                      <asp:TextBox ID="txtRefno28" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonDiscountAccHead" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonDiscountAccHead_Click" style="margin-bottom:-8px;" TabIndex="5" />
                                         
                             
                                    <asp:TextBox ID="txtDiscountAccHead" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                   
                                </li>
                                <li>
                                    <asp:Label ID="Label33" runat="server" CssClass="label" Style="width:160px;" Text="Discount AccSubHead"></asp:Label>
                                       <asp:TextBox ID="txtRefno29" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonDiscountAccHeadsub" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonDiscountAccHeadsub_Click" style="margin-bottom:-8px;" TabIndex="6" />
                                         
                             
                                    <asp:TextBox ID="txtlDiscountAccSubHead" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                   
                                     </li>
                                 <li>
                                    <asp:Label ID="Label30" runat="server" CssClass="label" Style="width:160px;" Text="Rounding AccHead"></asp:Label>
                                          <asp:TextBox ID="txtRefno30" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonroundingAccHead" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonroundingAccHead_Click" style="margin-bottom:-8px;" TabIndex="7" />
                                         
                             
                                    <asp:TextBox ID="txtRoundingAccountHead" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                   
                                 </li>
                                <li>
                                    <asp:Label ID="Label31" runat="server" CssClass="label" Style="width:160px;" Text="Rounding AccSubHead"></asp:Label>
                                       <asp:TextBox ID="txtRefno31" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonroundingAccHeadsub" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonroundingAccHeadsub_Click" style="margin-bottom:-8px;" TabIndex="8" />
                                         
                             
                                    <asp:TextBox ID="txtRoundingAccountHeadsub" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                    <%--<asp:DropDownList ID="ddlRoundingAccountHeadSub" runat="server" CssClass="dropdown" TabIndex="34"></asp:DropDownList>--%>
                                </li>
                                 <li>
                                    <asp:Label ID="Label14" runat="server" CssClass="label" Style="width:160px;" Text="Return AccHead"></asp:Label>
                                         <asp:TextBox ID="txtRefno24" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonreturnAccHead" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonreturnAccHead_Click" style="margin-bottom:-8px;" TabIndex="9" />
                                         
                             
                                    <asp:TextBox ID="txtreturnacchead" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                   
                                </li>
                                 <li>
                                    <asp:Label ID="Label15" runat="server" CssClass="label" Style="width:160px;" Text="Return AccSubHead"></asp:Label>
                                           <asp:TextBox ID="txtRefno25" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonreturnAccHeadsub" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonreturnAccHeadsub_Click" style="margin-bottom:-8px;" TabIndex="10" />
                                         
                             
                                    <asp:TextBox ID="txtreturnaccheadsub" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                   
                                </li>
                              
                         <%--       <li class="btnMargin" style="padding-left:160px">
                                    <asp:CheckBox ID="chkGovtSuppliers" Width="160px" runat="server" CssClass="chckbox" TabIndex="12" Text="Govt Suppliers" />
                                     <asp:CheckBox ID="chkCredit" runat="server" CssClass="chckbox" OnCheckedChanged="chkCredit_CheckedChanged" Style="float:left" TabIndex="13" Text="Credit" />
                                    
                                </li>--%>
                                  <li>
                                    <asp:Label ID="Label4" runat="server" CssClass="label" Style="width:160px;" Text="Card AccHead"></asp:Label>
                                         <asp:TextBox ID="txtRefno2" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonCardAccHead" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonCardAccHead_Click" style="margin-bottom:-8px;" TabIndex="11" />
                                         
                             
                                    <asp:TextBox ID="txtAccountHeadCard" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                   
                                </li>
                                 <li>
                                    <asp:Label ID="Label5" runat="server" CssClass="label" Style="width:160px;" Text="Card AccSubHead"></asp:Label>
                                           <asp:TextBox ID="txtRefno3" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonCardAccSubHead" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonCardAccHeadsub_Click" style="margin-bottom:-8px;" TabIndex="12" />
                                         
                             
                                    <asp:TextBox ID="txtAccountHeadSubCard" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                   
                                </li>
                                 <li>
                                    <asp:Label ID="Label7" runat="server" CssClass="label" Style="width:160px;" Text="Cheque AccHead"></asp:Label>
                                         <asp:TextBox ID="txtRefno4" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonChequeAccHead" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonchequeAccHead_Click" style="margin-bottom:-8px;" TabIndex="13" />
                                         
                             
                                    <asp:TextBox ID="txtAccountHeadCheque" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                   
                                </li>
                                 <li>
                                    <asp:Label ID="Label8" runat="server" CssClass="label" Style="width:160px;" Text="Cheque AccSubHead"></asp:Label>
                                           <asp:TextBox ID="txtRefno5" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonChequeAccSubHead" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonchequeAccHeadsub_Click" style="margin-bottom:-8px;" TabIndex="14" />
                                         
                             
                                    <asp:TextBox ID="txtAccountHeadSubCheque" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                                   
                                </li>
                                 <li>

                                    <asp:Label ID="Label36" runat="server" CssClass="label" Style="width:160px;" Text="Credit AccHead"></asp:Label>
                                            <asp:TextBox ID="txtRefno32" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="100" onKeypress="CheckInteger(event);" Width="40px" onblur="txtInput_onblur();" MaxLength="8" ReadOnly="True"></asp:TextBox>
                                    <asp:ImageButton ID="ImageButtonCreditAccHead" runat="server" ImageUrl="images/serach_icon.png" CausesValidation="false" OnClick="ImageButtonCreditAccHead_Click" style="margin-bottom:-8px;" TabIndex="15" />
                                         
                             
                                    <asp:TextBox ID="txtcreditAccHead" runat="server" Width="160px" CssClass="textbox" TabIndex="101" MaxLength="40" ReadOnly="True"></asp:TextBox>
                               
                                     <asp:RequiredFieldValidator ID="rfvcreditAccHead" runat="server" ErrorMessage="Credit Account Head Refno required" Enabled="false" ControlToValidate="txtcreditAccHead"  Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtendercreditAccHead" runat="server" Enabled="True" TargetControlID="rfvcreditAccHead">
                                    </cc:ValidatorCalloutExtender>
                                    <%--<asp:DropDownList ID="ddlcreditAccHead" runat="server" CssClass="dropdown" TabIndex="35"></asp:DropDownList>--%>
                                </li>
                                     <li>
                                   <asp:Label ID="lblgvs" Style="width:160px;" runat="server" CssClass="label" Text="Govt Suppliers"></asp:Label>
                                     <asp:CheckBox ID="chkGovtSuppliers" runat="server" Width="15"  TabIndex="16" /> 

                               </li>
                                        <li>
                                   <asp:Label ID="lblCredit" runat="server" Style="width:160px;" CssClass="label" Text="Credit"></asp:Label>
                                     <asp:CheckBox ID="chkCredit" runat="server" Width="15" Style="float:left" OnCheckedChanged="chkCredit_CheckedChanged"  TabIndex="17" /> 

                               </li>


                                <div class="clear"></div>

                                <li class="btnMargin"  style="padding-left:160px">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" TabIndex="18" OnClick="btnSubmit_Click" OnClientClick="disablebutn();" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); " TabIndex="19" OnClick="btnClear_Click" />   
                               
                                    <asp:HiddenField ID="hfBillTypeId" runat="server" />
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
                              <div class="divR">
                                   <ul class="contentul">
                                       
                               
                                    </ul>
                                  </div>
                        </div>
                             </div>
                         <div class="main_bg">  
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <%-- <li>
                                 <li>
                                <asp:Label ID="Label5" runat="server" CssClass="label" Text="ShortName"></asp:Label>
                                <asp:TextBox ID="txtSearchShortName" CssClass="textbox" runat="server"></asp:TextBox>
                            </li>--%>
                                <li>
                                    <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server" TabIndex="16"></asp:TextBox>
                                </li>
                                <div class="clear"></div>
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="17" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="12" />
                                </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvBillType" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvBillType_PageIndexChanging"
                                    BackColor="White"  BorderStyle="Solid" Width="825px" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ID_BillType" OnRowDeleting="gvBillType_RowDeleting" OnSelectedIndexChanging="gvBillType_SelectedIndexChanging">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-HorizontalAlign="Right" />

                                        <asp:TemplateField HeaderText="Name ">
                                            <ItemTemplate>
                                                <div style="width: 200px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                    <%# Eval("TypeName") %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BillTypeName" HeaderText="BillType" ItemStyle-Width="200px" />

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                       
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                          
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageBtnDelete" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" OnClick="ImageBtnDelete_Click" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
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
                        <%--<asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />--%>
                        <%--<asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
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
                                <asp:Label ID="Label3" runat="server" Text="Reason"></asp:Label>
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
