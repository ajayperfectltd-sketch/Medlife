<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonPopupCloudExport.ascx.cs" Inherits="Popup_CommonPopupCloudExport" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <style>
    .gridp th {
        width: auto;
        font-family: Arial, Helvetica, sans-serif;
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
        float: right;
          margin-top: 30px;
        margin-right: 5px;
    }

    .popupdiv {
        float: left;
        padding-top: 10px;
        padding-left: 16px;
        width: 95%;
    }


    .divSeparation {
        width: 96%;
        background-color: #0D3B5F;
        margin: 5px 0px;
        height: 2px;
    }

    .lblHeader {
        width: 100px;
        margin-top: 0px;
        padding: 0px 0px 0px 0px;
        float: right;
    }



    .cbxHeader {
        width: auto;
        height: 10px;
        margin: 0px 0px 0px 0px;
        float: left;
        padding: 0px 0px 0px 0px;
        position: relative;
    }

    .divContainer {
        width: 97%;
        height: 500px;
        overflow: auto;
    }

    .popuplabel {
        float: left;
        margin-left: 0px;
        font-family: Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-weight: bold;
        padding-top: 5px;
        padding-right: 6px;
        padding-bottom: 14px;
    }

     .hvbody {
        background: #fff;
        font: 400 1em/1.4 "Open Sans", "Helvetica Neue", sans-serif;
        color: #333;
        text-align: center;
        padding: 4em 2em;
    }

    .h1 {
        font-weight: 300;
        font-size: 5em;
        line-height: 1.35;
        margin: 0 0 .125em;
    }

        .h1 + .hvp {
            font-size: 1.5em;
            color: #999;
            max-width: 30em;
            margin: 0 auto 3em;
        }

    .table {
        width: 100%;
        text-align: left;
        border-spacing: 0;
        border-collapse: collapse;
        -webkit-box-sizing: border-box;
        -moz-box-sizing: border-box;
        box-sizing: border-box;
    }

    .th, .td {
        font-family: inherit;
        font-size: .875em;
        line-height: 1.45;
        color: #444;
        vertical-align: middle;
        padding: 1em;
    }

    .th {
        font-weight: bold;
    }

    .colgroup:nth-child(1) {
        width: 28%;
        border: 0 none;
    }

    .colgroup:nth-child(2) {
        width: 10%; 
        border: 1px solid #ccc;
    }

    .colgroup:nth-child(3) {
        width: 15%;
         border: 1px solid #ccc;
    }

    .colgroup:nth-child(4) {
        width: 15%;
        border: 1px solid #ccc;
    }
     .colgroup:nth-child(5) {
        width: 15%;
        border: 1px solid #ccc;
    }
       .colgroup:nth-child(6) {
        width: 15%;
        border: 1px solid #ccc;
    }
    /* Tablehead */

    .thead th {
        background: #def4fe;
        background: -moz-linear-gradient(top, #ffffff 0%, #f5f5f5 100%);
        background: -webkit-linear-gradient(top, #ffffff 0%,#f5f5f5 100%);
        background: -o-linear-gradient(top, #ffffff 0%,#f5f5f5 100%);
        background: -ms-linear-gradient(top, #ffffff 0%,#f5f5f5 100%);
        background: linear-gradient(to bottom, #ffffff 0%,#f5f5f5 100%);
        text-align: center;
        position: relative;
        border-bottom: 1px solid #ccc;
        padding: 3em 0 2em;
        font-weight: 400;
        color: #999;
        background: linear-gradient(to bottom, #e6b5b5  0%,#f5f5f5 100%);
    }

        .hvthead th:nth-child(1) {
            background: transparent;
        }

        .hvthead th:nth-child(3) {
            padding: 2em 0 5em;
        }

        .thead th h2 {
            font-weight: bold;
            font-size: 15px;
            line-height: 1.2;
            color: #1a5d8a;
        }

            .hvthead th h2 + p {
                font-size: 1.25em;
                line-height: 1.4;
            }

        .thead th:nth-child(3) h2 {
            font-size: 15px;
        }

            .hvthead th:nth-child(3) h2 + p {
                font-size: 1.5em;
            }

        .thead th p.promo {
            font-size: 1em;
            color: #fff;
            position: absolute;
            top: 9em;
            left: -17px;
            z-index: 1000;
            width: 100%;
            margin: 0;
            padding: .625em 17px .75em;
            background: #c00;
            box-shadow: 0 2px 4px rgba(0,0,0,.25);
            border-bottom: 1px solid #900;
        }

            .thead th p.promo:before {
                content: "";
                position: absolute;
                display: block;
                width: 0px;
                height: 0px;
                border-style: solid;
                border-width: 0 7px 7px 0;
                border-color: transparent #900 transparent transparent;
                bottom: -7px;
                left: 0;
            }

            .thead th p.promo:after {
                content: "";
                position: absolute;
                display: block;
                width: 0px;
                height: 0px;
                border-style: solid;
                border-width: 7px 7px 0 0;
                border-color: #900 transparent transparent transparent;
                bottom: -7px;
                right: 0;
            }

    /* Tablebody */

    .hvtbody th {
        background: #fff;
        border-left: 1px solid #ccc;
    }

        .tbody th span {
            font-weight: normal;
            font-size: 87.5%;
            color: #999;
            display: block;
        }

    .tbody td {
        background: #fff;
        text-align: center;
    }

    .tbody tr:nth-child(even) th,
    .tbody tr:nth-child(even) td {
        background: #f5f5f5;
        border: 1px solid #ccc;
        border-width: 1px 0 1px 1px;
    }

    .tbody tr:last-child td {
        border-bottom: 0 none;
    }

    /* Tablefooter */

    .foot th {
        padding: 2em 1em;
        border-top: 1px solid #ccc;
    }

    .foot td {
        text-align: center;
        padding: 2em 1em;
        border-top: 1px solid #ccc;
    }

    .foot a {
        font-weight: bold;
        color: #fff;
        text-decoration: none;
        text-transform: uppercase;
        display: block;
        padding: 1.125em 2em;
        background: #59c7fb;
        border-radius: .5em;
    }

    .trchecked 
    {
        
        background:linear-gradient(to bottom, #e6b5b5 0%,#f5f5f5 100%);
        font-weight:900;
        color:#59c7fb !important;
    }
    .trunchecked 
    {
       background-color:#FFF !important;
        font-weight:900;
        /*color:#FFF !important;*/
    }
     .lstscroll
        {
            width: 800px;
        height: 416px;
        overflow-y: scroll;
        }
      .btnProcesspopup {
      
        /*padding: 4px,5px,4px,5px;*/
        font-family: Arial;
        font-weight: bold;
        border-radius: 5px;
        /*background-color: #0E3B62;CadetBlue*/
          background-color: CadetBlue;
        color: #FFF;
        width: 86px;
        height: 25px;
        text-align: center;
        border: none;
        cursor: pointer;
       margin-bottom: 10px;
        /*float: right;*/
    }
</style>
<script type="text/javascript">
    $(document).ready(function ()
    {
      
        var dt = new Date();
        $("#CommonPopupExport_txtInvoiceDate").val(dt.format("dd/MM/yyyy"));
        $("#CommonPopupExport_gdvSearch_chkExpSelect").prop('checked', false);
        // $("#CommonPopupExport_btnSubmit").prop('visible', false);
        //document.getElementById("#CommonPopupExport_btnSubmit").style.display = "block";

        $("#CommonPopupExport_lblEXErrMsg").text('');
        $("#CommonPopupExport_lblagreexemsg").text('');
        $("#CommonPopupExport_lblsucessmessage").text('');
        $("#CommonPopupExport_lblrmsg").text('');
        $("#CommonPopupExport_hdfagreeex").val('False');
        
        //document.getElementById(" CommonPopupImport_gdvSearch_chkSelect").checked = false;
       
    });
    EnableExSubmit = function (val)
    {
       
        if (val.checked == true) {
            $("#CommonPopupExport_btnexSubmit").prop('disabled', false);
            $("#CommonPopupExport_lblsucessmessage").text('');
            $("#CommonPopupExport_lblrmsg").text('');
            $("#CommonPopupExport_lblagreexemsg").text('');
        }
        else {
            $("#CommonPopupExport_btnexSubmit").prop('disabled', true);

        }

        $("#CommonPopupExport_hdfagreeex").val(val.checked);
    }
    function SelectExpAll(headerCheckBox) {


        var id = headerCheckBox;
        var listView = document.querySelector('.tableExp');
        //var listView = document.querySelector('.table'); // Select the ListView by its CSS class.
        var checkboxes = listView.querySelectorAll('[id*="chklistExview"]');

        var checkboxes = listView.querySelectorAll('[id*="chklistExview"]');
        //if (checkboxes.length - 1 != checkboxes.length) {
        for (var i = 0; i < checkboxes.length; i++) {

            checkboxes[i].checked = id.checked;
            if (checkboxes[i].checked == true) {
                $("#CommonPopupExport_lblEXErrMsg").text('');
                $("#CommonPopupExport_lblagreexemsg").text('');
                $("#CommonPopupExport_lblsucessmessage").text('');
                $("#CommonPopupExport_lblrmsg").text('');
                $("#CommonPopupExport_btnExProcess").prop('disabled', false);
                //$('#ContentPlaceHolder1_CommonPopup2_divshowhideprocess').attr('style', 'display:block;');
            }
            else {
                $("#CommonPopupExport_btnExProcess").prop('disabled', true);
            }
            //checkboxes[i].checked = headerCheckbox.checked;
        }

    }
    function SelectExOne(chk) {

        var id = chk;
        var row = chk.closest('tr');
        var checkboxes = row.querySelectorAll('input[type="checkbox"]');
        var headerCheckbox = document.getElementById('CommonPopupExport_gdvSearch_chkExpSelect');
        var checked = true;

        // Loop through the checkboxes in the row.
        for (var i = 0; i < checkboxes.length; i++) {
            if (!checkboxes[i].checked) {
                checked = false;
                headerCheckbox.checked = false;
                break;

            }
            else {
                $("#CommonPopupExport_lblEXErrMsg").text('');
                $("#CommonPopupExport_lblagreexemsg").text('');
                $("#CommonPopupExport_lblsucessmessage").text('');
                $("#CommonPopupExport_lblrmsg").text('');
                $("#CommonPopupExport_btnExProcess").prop('disabled', false);

            }
        }

        // Update the header checkbox based on the checked state.

    }
    function Funbutsubmit() {
        $("#CommonPopupExport_btnexSubmit").prop('disabled', true);
      
    }

</script>


    <div align="center" style="border: 1px solid #88D2FF;  width: 870px; padding-bottom: 0px; background-color: #FFF; border: 1px solid #0f3a5d; padding-bottom: 50px; box-shadow: 10px 10px 32px  #888; border-radius: 0px 0px 2px 2px 2px;font-family: Open Sans, sans-serif;letter-spacing: 1px;z-index: auto;overflow: auto;max-height: 810px;height: 800px;">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="popupdiv">
                <asp:TextBox ID="txtRefno" runat="server" CssClass="form-control" Width="145" Style="display: none;"></asp:TextBox>

                <asp:Label ID="popuplabel" Style="display: none" runat="server" Text="Name" CssClass="popuplabel"></asp:Label>
                <asp:TextBox ID="txtName" Visible="false" runat="server" Width="400"></asp:TextBox>

                <asp:Button ID="btnSearch" runat="server" Text="Search" Visible="false" CausesValidation="false"  CssClass="btnSearchPopup" Style="margin-left: 10px;" />
                <asp:Button ID="btnClearSearchPopup" runat="server" Style="display: none;" Text="Clear" CausesValidation="false" CssClass="btnSearchPopup" OnClientClick="return ClearPopu8Search();" />
                <asp:Label ID="lblErrorMsg" runat="server" Style="display: none;" Text="Invalid Input " ForeColor="Red"></asp:Label>

            </div>
            <div class="clear"></div>
            <div class="lstscroll">
                   <h1>Export Details</h1>
               <div> 
             
                
                     <asp:ListView ID="gdvSearch"  runat="server" ItemPlaceholderID="itemPlaceHolder1" OnPagePropertiesChanging="gdvSearch_PagePropertiesChanging" 
                         OnDataBound="gdvSearch_DataBound" OnItemDataBound="gdvSearch_ItemDataBound"  OnSelectedIndexChanged="gdvSearch_SelectedIndexChanged" >
                        <LayoutTemplate>
                            <table class="table tableExp" id="gvtable">
                                <colgroup class="colgroup"></colgroup>
                                <colgroup class="colgroup"></colgroup>
                                <colgroup class="colgroup"></colgroup>
                                <colgroup class="colgroup"></colgroup>
                              <%--  <colgroup class="colgroup"></colgroup> --%>
                                <%--<colgroup class="colgroup"></colgroup>   
                                 <colgroup class="colgroup"></colgroup>      --%>           
                                <thead class="thead" style="color: #d5652e;">
                                <tr >
                                   
                                    <th style="color: #d5652e;">
                                    <h2> Module </h2>
                                    </th>
                                    <th style="color: #d5652e;">
                                      <h2>Date </h2>  
                                    </th>
                                    <%--<th style="color: #d5652e;">
                                      <h2>  Party </h2> 
                                    </th>
                                    <th style="color: #d5652e;">
                                      <h2>BillNo</h2> 
                                    </th>--%>
                                    <th style="color: #d5652e;">
                                      <h2>  Amount </h2> 
                                    </th>
                                     <th style="color: #d5652e;">
                                      <h2>Select All 
                                          <asp:CheckBox ID="chkExpSelect" runat="server"  onClick="SelectExpAll(this);" ToolTip="Click here for Selection"  />
                                      </h2> 
                                    </th>
                                   
                                </tr>
                                    </thead>
                              
                               <tbody class="hvbody" style="font-family: Open Sans, sans-serif; letter-spacing: 1px;">
                                     <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                   </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>

                             <tr>
                                <td style="font-weight:bold;color: #1a5d8a;text-align:left;"> <%# Eval("ID") %>
                                 <asp:HiddenField ID="hdnTransmode" Value='<%# Eval("Transmode") %>'  runat="server" />
                                </td>
                                <td style="text-align:left;"><%# Eval("TransDate","{0:dd/MM/yyyy}") %>
                                     <asp:HiddenField ID="hdntdate" Value='<%# Eval("TransDate","{0:dd/MM/yyyy}") %>'  runat="server" />
                                </td>
                             <%--   <td><%# Eval("Party") %>
                                    
                                </td>
                                 <td><%# Eval("billno") %>
                                    
                                 </td>--%>
                                 <td style="text-align:right;"><%# Eval("Amount") %> 
                                      <asp:HiddenField ID="hdnAmount" Value='<%# Eval("Amount") %>'  runat="server" />
                                 </td>
                                 <td>
                                      <asp:HiddenField ID="hdnbillno" Value='<%# Eval("billno") %>'  runat="server" />
                                      <asp:HiddenField ID="hdnparty" Value='<%# Eval("Party") %>'  runat="server" />
                                     <asp:HiddenField ID="hdFK_Client" Value='<%# Eval("FK_Client") %>'  runat="server" />
                                       <asp:HiddenField ID="hdClID" runat="server" Value='<%# Eval("ID") %>' />
                                      <asp:HiddenField ID="hdUserCode" Value='<%# Eval("UserCode") %>'  runat="server" />
                                         <asp:HiddenField ID="hdFK_Transaction" Value='<%# Eval("FK_Transaction") %>' runat="server" />
                                      <asp:CheckBox ID="chklistExview" runat="server" onClick="SelectExOne(this);" Checked='<%#Convert.ToBoolean(Eval("Exportbit").ToString()=="0"?"false":"true")%>'  />
                                 </td>
                             </tr>

                        </ItemTemplate>
                        </asp:ListView>      
        </div>
            </div>
            <br />

            <div class="Pager" style="float: left; margin: -50px 0px 0px 220px 220px;">             
            </div>
            <asp:HiddenField ID="hfColumn" runat="server" />
             <div class=" col-md-12 col-xs-12 col-sm-12 DivHead" style="display:block">
           
                <asp:TextBox runat="server" CssClass="textbox" ID="TextBox1" Style="text-align: right;  width: 130px;" AutoComplete="off" Visible="false" MaxLength="10" onKeypress="CheckNumWithDecimal(event);" />
                <div class="form-group row " >
                     <div class="col-sm-3">
                        <label style=" font-family: Open Sans, sans-serif; font-weight :bold; font-size: 14px; padding-top:5px ">Transaction date</label>
                         </div> 
                      <div class="col-sm-2">
                                    <asp:TextBox ID="txtInvoiceDate"  CssClass="form-controlNMandatory" Style="font-family: 'Open Sans', sans-serif;width: 130%;" TabIndex="5" runat="server" ></asp:TextBox>
                                      
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender78" TargetControlID="txtInvoiceDate"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"
                                        InputDirection="RightToLeft"

                                        AcceptNegative="None"
                                        DisplayMoney="None"
                                        AutoComplete="true"
                                        AutoCompleteValue="Date"
                                        runat="server">
                                    </cc1:MaskedEditExtender>
                                    <asp:RegularExpressionValidator ID="Invoicedatecheck" runat="server" ValidationGroup="Add" Display="None" SetFocusOnError="True"
                                        ErrorMessage="Please enter date in dd/mm/yyyy format" ForeColor="Red" Font-Bold="true" ControlToValidate="txtInvoiceDate"
                                        ValidationExpression="(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/\d{4}"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Invoice Date required" ControlToValidate="txtInvoiceDate" Display="None" SetFocusOnError="True" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                    </cc1:ValidatorCalloutExtender>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender57" runat="server" Enabled="True" TargetControlID="Invoicedatecheck">
                                    </cc1:ValidatorCalloutExtender>
                                   <%-- <asp:CustomValidator ID="cusInvoice" runat="server" Display="None" SetFocusOnError="true" ClientValidationFunction="ValidateDate" ControlToValidate="txtInvoiceDate"
                                        ErrorMessage="Please enter date in dd/mm/yyyy format" ValidationGroup="Add" />
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender67" runat="server" Enabled="True" TargetControlID="cusInvoice">
                                    </cc1:ValidatorCalloutExtender>
                                   --%>
                   </div>
                     <div class="col-sm-3">
                        <asp:Label ID="Label1" runat="server" Text="" ></asp:Label>
                        <asp:CheckBox ID="chkSupplierWise" Text="&nbsp Supplier Wise" runat="server" />  
                    </div> 

                      <div class="col-sm-3" style="display:none">
                     <asp:Label ID="Label2" runat="server" Text="" ></asp:Label>
                        <asp:CheckBox ID="chkRecovery" Text="&nbsp Recovery" runat="server" />  
                          </div> 

                                         <div class="col-sm-3">
                        <asp:Label ID="Label3" runat="server" Text="" ></asp:Label>
                        <asp:CheckBox ID="chkSalesMan" Text="&nbsp SalesMan" runat="server" />  
                    </div> 

                 </div>
                
                   <div class="col-md-12 col-xs-12 col-sm-12" style ="text-align :right">
             <asp:Label runat="server" ID="lblEXErrMsg" style="color:red"  ></asp:Label>
                        <asp:Label runat="server" ID="lblsucessmessage" style="color:green"  ></asp:Label>
                         <asp:Label runat="server" ID="lblrmsg" style="color:red"  ></asp:Label>
                        <asp:Button ID="btnExProcess" CssClass="btnProcesspopup"   OnClick="btnExProcess_Click" Text="Process" runat="server" ValidationGroup="Add"  /> 
                 </div>

                <div style=" font-family :'Open Sans', sans-serif ;font-weight : bold ;font-size :13px; text-align :left;border-color:black "  >
                    <pre style="line-height: 8px;"> Confirmation  <br /> 
                          <p> &#x09; This Will Export All Data From Local Server To Cloud Server. Click Agree To Continue</p>
                   </pre>
                             
                </div>
                  <div class="col-md-12 col-xs-12 col-sm-12">
                          <asp:Label runat="server" ID="lblagreexemsg" style="color:red"  ></asp:Label>
                    </div>
                 <div class="col-md-12 col-xs-12 col-sm-12 " style ="text-align: left;padding-right: 1px;margin-left: 2px; ">
                         
                         <asp:CheckBox ID="chkAgree" runat="server"  text="&nbsp;I Agree &nbsp;" class="chkcheck" onClick="EnableExSubmit(this)" />
                         
                  </div>    
                   <div class="col-md-12 col-xs-12 col-sm-12" style ="text-align :right">
                       <asp:Button ID="btnexSubmit"  CssClass="btnSearchPopup"  OnClick="btnexSubmit_Click"  Text="Save" runat="server" onblur="Funbutsubmit();" />
                           <asp:TextBox runat="server" CssClass="textbox" ID="txtTo" Style="text-align: right;  width: 130px;" AutoComplete="off" Visible="false" MaxLength="10" onKeypress="CheckNumWithDecimal(event);" />
                 </div> 
                 <%-- <div class="col-md-12 col-xs-12 col-sm-12" style ="text-align :right">
                   
                       
                 </div>--%>
            </div>
           <%-- <li class="clear"></li>--%>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="hfTotalCharge" runat="server" />
             <asp:HiddenField ID="hdfagreeex" runat="server" />
             <asp:HiddenField ID="hdfusercode" runat="server" />
            <asp:HiddenField ID="hdfimpclient" runat="server" />
            <asp:Button ID="btnTotalAmount" runat="server" Style="display: none;" CausesValidation="false" Text="Hide" />

        </ContentTemplate>
             <Triggers>
        
        <asp:AsyncPostBackTrigger ControlID="btnexSubmit" EventName="click" />
    </Triggers>
    </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1" >
                <ProgressTemplate>
                    <img alt="Please wait.." src="<%= ResolveUrl("~/images/Progressbar/Loading.gif") %>"
                        style="width: 354px; height: 45px" />
                    <p>Please wait...</p>
                </ProgressTemplate> 
            </asp:UpdateProgress>
</div>


