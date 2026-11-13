<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonPopupTransferToAccount.ascx.cs" Inherits="Popup_CommonPopupTransferToAccount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>




<style>
   
    

        .gridp th {
            width: auto;
            font-family:Arial, Helvetica, sans-serif;
        }

    .txtSearch {
        border-radius: 5px;
        font-family: Arial;
        padding: 2px 3px 2px 3px;
        margin-top: 16px;
        margin-left: 15px;
    }

    .btnSearchPopup {
        /*background-color: #0E3B62;
        color: white;*/

        padding: 2px,3px,2px,3px;
        font-family: Arial;
        font-weight: bold;
        border-radius: 5px;
        background-color: #0E3B62;
        color: #FFF;
        width: 86px;
        height: 25px;
        text-align: center;
        border: none;
        cursor: pointer;

        float:right;
    }

    .popupdiv {
        float: left;
        padding-top: 10px;
        padding-left: 16px;
        width:95%

    }
    

    .divSeparation {
        width: 96%;
        background-color: #0D3B5F;
        margin: 5px 0px;
        height: 2px;
    }

    .lblHeader { width:100px; margin-top:0px;  padding:0px 0px 0px 0px; float:right;  }
       
  

    .cbxHeader { width:auto; height:10px;
        margin:0px 0px 0px 0px; float:left;
        padding: 0px 0px 0px 0px; position:relative;
         
    }
	.divContainer
	{
	width:97%;
	height:320px;
	overflow:auto;
	}
	.popuplabel{ float:left; margin-left:0px;    font-family:Arial, Helvetica, sans-serif; font-size:12px; font-weight:bold; padding-top:5px; padding-right:6px; padding-bottom:14px;  }
	
</style>
<script>
    function ClearAdd() {

        var toDate = new Date();
        $("#CommonPopup1_txtAhstChequeDate").val(toDate.format("dd/MM/yyyy"));
        document.getElementById('CommonPopup1_ddlBank').value = "0";
        document.getElementById('CommonPopup1_txtpoptrsfrAccntDetails').value = "";
        document.getElementById('CommonPopup1_txtCheqNo').value = "";
        $("#divAdd").modal({ "backdrop": "static" });

    }
    //function ClearAdds() {
    //    alert('hi');
    //    var toDate = new Date();
    //    $("#CommonPopup1_txtAhstChequeDate").val(toDate.format("dd/MM/yyyy"));
    //    document.getElementById('CommonPopup1_ddlBank').value = "0";
    //    document.getElementById('CommonPopup1_txtpoptrsfrAccntDetails').value = "";
    //    document.getElementById('CommonPopup1_txtCheqNo').value = "";


    //}
    function GetSelectedTextValue(ddlBank) {

        var selectedText = ddlBank.options[ddlBank.selectedIndex].innerHTML;

        var selectedValue = ddlBank.value;
        //alert(" Bank: " + selectedText + " Code: " + selectedValue);

        var a = document.getElementById("CommonPopup1_txtpoptrsfrAccntDetails");
        a.value = " Bank: " + selectedText + "\n" + " Bank Code: " + selectedValue

    }

</script>
<div align="center" style="border: 1px solid #88D2FF; height: 140px; width: 598px; padding-bottom: 0px; background-color: #bad4e1; border: 1px solid #0f3a5d; padding-bottom: 50px; box-shadow: 10px 10px 32px #888; border-radius: 0px 0px 2px 2px;">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <%-- <div id="divouterline" align="left">--%>

   
   <%-- <div id="divAdd" class="popupdiv">--%>
       <div id="divAdd" runat="server">  
        
     
        <li>
               <asp:Label ID="popuplabel" runat="server" Text="Bank" CssClass="popuplabel" Style="margin-left: 1%;"></asp:Label>         
                 <asp:TextBox ID="txtpopTrsfBranchcode" runat="server" AutoPostBack="false" CssClass="textbox" onKeypress="CheckInteger(event);" MaxLength="8" Width="5%" Style="margin-left: -14%;"></asp:TextBox>   
             <asp:DropDownList ID="ddlBank" runat="server" CssClass="dropdownMandatory" TabIndex="1" Width="61%" onchange="GetSelectedTextValue(this)" ></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select a bank" ControlToValidate="ddlBank" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submited"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                    </cc:ValidatorCalloutExtender>                                 
             <%--  <asp:TextBox ID="txtpopTrsfAccno" runat="server" CssClass="textbox" MaxLength="225" Width="56%"></asp:TextBox>
<asp:ImageButton ID="btnpoptrsfrSearch" runat="server" ImageUrl="~/images/serach_icon.png" CausesValidation="false" OnClick="btntrsfrSearch_Click" Style="margin-bottom: -8px;" TabIndex="1" Height="24px" Width="26px" />--%>
         </li>
        
           <li>   
                                
          <asp:TextBox ID="txtpoptrsfrAccntDetails" CssClass="textbox"  runat="server"  Height="43px"   Width="66%" Style="margin-left: -14%;" Font-Size="Smaller" TextMode="MultiLine"  ></asp:TextBox>
          </li>
     
         <div class="divL" style="width: 50%;">
                                        

         <li>
              <asp:Label ID="lblCheqNo" runat="server" Text="Cheque No" CssClass="popuplabel" Style="margin-left: 2%;"></asp:Label>   

               <asp:TextBox ID="txtCheqNo"   CssClass="textbox"  onKeypress="CheckInteger(event);"  Width="44%"  runat="server" Style="margin-left: -26%;" MaxLength="12"></asp:TextBox>
  
   </li>
        </div>
             
      <div class="divR" style="width: 50%;"> 
   <li>
   <asp:Label ID="lblCheqDate" runat="server" Text="Cheque Date" CssClass="popuplabel" Style="margin-left: -13%;"></asp:Label>   
                <asp:TextBox ID="txtAhstChequeDate" CssClass="textbox" TabIndex="9" runat="server" Width="44%" Style="margin-left: -39%;"></asp:TextBox>
         <cc:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtAhstChequeDate">
         </cc:CalendarExtender>     
                  
   </li>
   </div>
           
         <asp:Button ID="btnApply" runat="server"  ValidationGroup="submited" Text="Apply" UseSubmitBehavior="False" CssClass="button" onclick="btnApply_Click"  SkinID="button" />
         <asp:Button ID="btnReset" runat="server" OnClientClick="ClearAdd();"  Text="Reset"  CssClass="button" UseSubmitBehavior="False" SkinID="button" />
        <asp:HiddenField ID="hfClient" runat="server" />
                    <asp:HiddenField ID="hfUser" runat="server" />
       <%-- <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="false" OnClick="btnSearch_Click" CssClass="btnSearchPopup" Style="margin-left:10px;" />
        <asp:Button ID="btnClearSearchPopup" runat="server" Text="Clear" CausesValidation="false"  CssClass="btnSearchPopup" OnClientClick="return ClearPopu8Search();" />--%>
        <%--<asp:Label ID="lblErrorMsg" runat="server" Text="Invalid Input " ForeColor="Red"></asp:Label>--%>
    </div>
    <div class="clear"></div>
    
    <br />
    <div style="width: 450px; margin-left: 0px;">


        <ul class="contentul" style="width: 800px; height: auto; padding: 0px; overflow-y: auto;">
            <%--overflow: auto; overflow-y:auto; overflow-x:auto;--%>
            <asp:HiddenField ID="hfColumn" runat="server" />

            <li class="clear"></li>
            <br />
        </ul>
    </div>
    

    <li class="clear"></li>





    
<%--                        <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopup" runat="server" Style="display: none; float: right; margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 450px; height: 300px; height: 370px;">
                            <div id="divheader" runat="server" class="PopupHeader">
                                <asp:Label ID="lblPopupHeader" runat="server" Text="Bank Head" CssClass="lblPopupHeader"></asp:Label>
                                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>

                            <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                        </cc:ModalPopupExtender>--%>










    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</div>
