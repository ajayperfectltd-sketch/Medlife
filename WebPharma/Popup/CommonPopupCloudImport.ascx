<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonPopupCloudImport.ascx.cs" Inherits="Popup_CommonPopupCloudImport" %>
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
        /*background-color: #0E3B62;*/
        background-color:CadetBlue;
        color: #FFF;
        width: 86px;
        height: 25px;
        text-align: center;
        border: none;
        cursor: pointer;
        float: right;
        margin-top: 30px;
        margin-right: 50px;
        margin-bottom: 30px;
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
        font-weight: 600;
    }

    .colgroup:nth-child(1) {
        width: 25%;
        border: 0 none;
    }

    .colgroup:nth-child(2) {
        width: 25%;
        border: 1px solid #ccc;
    }

    .colgroup:nth-child(3) {
        width: 25%;
         border: 1px solid #ccc;
    }

    .colgroup:nth-child(4) {
        width: 25%;
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
            font-weight: 300;
            font-size: 2.4em;
            line-height: 1.2;
            color: #59c7fb;
        }

            .hvthead th h2 + p {
                font-size: 1.25em;
                line-height: 1.4;
            }

        .thead th:nth-child(3) h2 {
            font-size: 3.6em;
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
        /*color:#59c7fb !important;*/
    }
    .trunchecked 
    {
       background-color:#FFF !important;
        font-weight:900;
        /*color:#FFF !important;*/
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
      .lstIMscroll
        {
            width: 800px;
        height: 416px;
        overflow-y: scroll;
        }

</style>

<script type="text/javascript">
    $(document).ready(function () {

        $("#CommonPopupImport_gdvSearch_chkImpSelect").prop('checked', false);
        $("#CommonPopupImport_lblImportErrMsg").text('');
        $("#CommonPopupImport_lblagreemsg").text('');
        $("#CommonPopupImport_lblImsucessmessage").text('');
        $("#CommonPopupImport_lblImrmsg").text('');
        $("#CommonPopupImport_HiddenField1").val('False');

    });
    EnableImportSubmit = function (val) {
        if (val.checked == true) {

            $("#CommonPopupImport_btnSumbit").prop('disabled', false);
            $("#CommonPopupImport_lblImportErrMsg").text('');
            $("#CommonPopupImport_lblagreemsg").text('');
            $("#CommonPopupImport_lblImsucessmessage").text('');
            $("#CommonPopupImport_lblImrmsg").text('');
        }
        else {

            $("#CommonPopupImport_btnSumbit").prop('disabled', true);

        }
        $("#CommonPopupImport_HiddenField1").val(val.checked);
    }
   

    function ClearPopu8Search() {
        $("#ContentPlaceHolder1_CommonPopup1_txtName").val('');
        return false;
    }
 


    function SelectImportAll(headerCheckBox) {


        var id = headerCheckBox;
        var listView = document.querySelector('.tableImp'); // Select the ListView by its CSS class.
        var checkboxes = listView.querySelectorAll('[id*="chklistview"]');

        var checkboxes = listView.querySelectorAll('[id*="chklistview"]');

        for (var i = 0; i < checkboxes.length; i++) {

            checkboxes[i].checked = id.checked;

            if (checkboxes[i].checked == true) {
                $("#CommonPopupImport_lblImportErrMsg").text('');
                $("#CommonPopupImport_lblagreemsg").text('');
                $("#CommonPopupImport_lblImsucessmessage").text('');
                $("#CommonPopupImport_lblImrmsg").text('');
                $("#CommonPopupImport_btnProcess").prop('disabled', false);
                //$('#CommonPopupImport_divshowhideprocess').attr('style', 'display:block;');
            }
            else {
                $("#CommonPopupImport_btnProcess").prop('disabled', true);
            }
        }

    }
    function SelectImportOne(chk) {

        var id = chk;
        var row = chk.closest('tr');
        var checkboxes1 = row.querySelectorAll('input[type="checkbox"]');
        var headerCheckbox1 = document.getElementById('CommonPopupImport_gdvSearch_chkSelect');
        var checked = true;

        // Loop through the checkboxes in the row.
        for (var i = 0; i < checkboxes1.length; i++) {
            if (!checkboxes1[i].checked) {
                checked = false;
                headerCheckbox1.checked = false;
                break;

            }
            else {
                $("#CommonPopupImport_lblImportErrMsg").text('');
                $("#CommonPopupImport_lblagreemsg").text('');
                $("#CommonPopupImport_lblImsucessmessage").text('');
                $("#CommonPopupImport_lblImrmsg").text('');
                $("#CommonPopupImport_btnProcess").prop('disabled', false);
                //$('#CommonPopupImport_divshowhideprocess').attr('style', 'display:block;');

            }
        }

        // Update the header checkbox based on the checked state.

    }
    function FunbutImpsubmit() {
        $("#CommonPopupImport_btnSumbit").prop('disabled', true);
       // alert('Transaction Already Done.');
    }
</script>

<div align="center" style="border: 1px solid #88D2FF;  width: 998px; padding-bottom: 0px; background-color:#FFF; border: 1px solid #0f3a5d; padding-bottom: 50px; box-shadow: 10px 10px 32px #888; border-radius: 0px 0px 2px 2px;font-family: Open Sans, sans-serif; z-index: auto;overflow: auto;height:810px;max-height:810px">
    
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
            <div class="lstIMscroll">
                <h1>Import Details</h1>
               <div> 

                

                     <asp:ListView ID="gdvSearch" runat="server" ItemPlaceholderID="itemPlaceHolder1" 
                         OnDataBound="gdvSearch_DataBound" OnItemDataBound="gdvSearch_ItemDataBound"  OnSelectedIndexChanged="gdvSearch_SelectedIndexChanged" >
                        <LayoutTemplate>
                            <table class="table tableImp" id="gvtable" >
                                 <colgroup  class="colgroup" style="width:auto "></colgroup>
                                <colgroup class="colgroup" style="width:auto "></colgroup>
                                <colgroup class="colgroup" style="width:auto "></colgroup>
                                  <colgroup class="colgroup" style="width:auto "></colgroup>
                                <thead class="thead" style="color: #1a5d8a;">
                                <tr >
                                    <th style="color: #1a5d8a;">
                                     Table Name
                                    </th>
                                    <th style="color: #1a5d8a;">
                                      <h4>New </h4>  
                                    </th>
                                    <th style="color: #1a5d8a;">
                                      <h4> Edited </h4> 
                                    </th>
                                    <th style="color: #d5652e;">
                                      <h2>Select All 
                                          <asp:CheckBox ID="chkSelect" runat="server"  onClick="SelectImportAll(this);" ToolTip="Click here for Selection"  />
                                      </h2> 
                                    </th>
                                   <%-- <th></th>--%>
                                </tr>
                                    </thead>
                              
                               <tbody class="hvbody" style="font-family: Open Sans, sans-serif; ">
                                     <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                   </tbody>
                            </table>
                        </LayoutTemplate>
                        
                        <ItemTemplate>
                             
                             <tr >
                            <td style="font-weight:bold;color: #1a5d8a; text-align:left;">
                                <%# Eval("TableName") %>
                            </td>
                            <td style="text-align:right">
                                <%# Eval("NewMasters") %>
                            </td>
                            <td style="text-align:right">
                                <%# Eval("EditedMasters") %>
                            </td>
                                
                                 <td>
                                       <asp:CheckBox ID="chklistview" runat="server" onClick="SelectImportOne(this);" Checked='<%#Convert.ToBoolean(Eval("Link").ToString()=="0"?"false":"true")%>'  />
                                       <asp:HiddenField ID="hdFK_Client" Value='<%# Eval("FK_Client") %>'  runat="server" />
                                       <asp:HiddenField ID="hdClID" runat="server" Value='<%# Eval("ID") %>' />
                                      <asp:HiddenField ID="hdStatusCode" runat="server" Value='<%# Eval("StatusCode") %>' />
                                      <asp:HiddenField ID="hdnTableName" Value='<%# Eval("TableName") %>'  runat="server" />
                                      <asp:HiddenField ID="hdnProcessid" Value='<%# Eval("Processid") %>'  runat="server" />
                                       <asp:HiddenField ID="hdnUserCode" Value='<%# Eval("UserCode") %>'  runat="server" />   
                                       <asp:HiddenField ID="hdnNewMasters" Value='<%# Eval("NewMasters") %>'  runat="server" />      
                                 </td>
                                 </tr>
                        </ItemTemplate>
                        </asp:ListView>
             </div>
            </div>
            <br />
             <div class="col-md-12 col-xs-12 col-sm-12" style ="text-align :right">
                    <asp:Label runat="server" ID="lblImportErrMsg" style="color:red"  ></asp:Label>
                  <asp:Label runat="server" ID="lblImsucessmessage" style="color:green" ></asp:Label>     
                   <asp:Label runat="server" ID="lblImrmsg" style="color:red"  ></asp:Label>
                       
                 </div>
               <div class="col-md-12 col-xs-12 col-sm-12" style ="text-align :right">
                 
                        <asp:Button ID="btnProcess" CssClass="btnProcesspopup"   OnClick="btnProcess_Click" Text="Process" runat="server" />
                 </div>
           
          
            <div class=" col-md-12 col-xs-12 col-sm-12 DivHead" style="display:block">
               
                <asp:HiddenField ID="hfColumn" runat="server" />
                <asp:TextBox runat="server" CssClass="textbox" ID="txtTo" Style="text-align: right;  width: 130px;" AutoComplete="off" Visible="false" MaxLength="10" onKeypress="CheckNumWithDecimal(event);" />
       
                <div style="background-color:#FFF; font-family :'Open Sans', sans-serif ;font-weight : bold ;font-size :13px; text-align :left;border-color:black "  >
                   
                    <pre style="line-height: 7px;">  Confirmation 
                     
                             <p> &#x09; This Will Import All Data From Cloud Server To Local Server. Click Agree To Continue</p>
                         <p style="color:red;"> &#x09; NB:- Once All Other Tables Are Inserted, AccountHeadSub Table Data Should Be Selected.</p>
                    </pre>
                    <div class="col-md-12 col-xs-12 col-sm-12">
                         <asp:Label runat="server" ID="lblagreemsg" style="color:red"  ></asp:Label>
                    </div>
               <div class="col-md-12 col-xs-12 col-sm-12 " style ="text-align: left;padding-right: 1px;margin-left: 2px; ">
                         <asp:CheckBox ID="chkAgree" runat="server"  text="&nbsp;I Agree &nbsp;" class="chkcheck" onClick="EnableImportSubmit(this)" />

                   
                    </div>
                      <div class="col-md-12 col-xs-12 col-sm-12" style ="text-align :right;padding-bottom: 10px;">
                       <asp:Button ID="btnSumbit"  CssClass="btnSearchPopup"  OnClick="btnSumbit_Click" Text="Save" runat="server" />
                 </div>

                </div>
                <div class="Pager" style="float: left; margin: -50px 0px 0px 220px;">
                 
            </div>

               
                <asp:HiddenField ID="hdfimpclient" runat="server" />
                <asp:HiddenField ID="hdfusercode" runat="server" />
            </div>
          <%--  <li class="clear"></li>--%>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="hfTotalCharge" runat="server" /> <asp:HiddenField ID="hdnidprocess" runat="server" />

            <asp:Button ID="btnTotalAmount" runat="server" Style="display: none;" CausesValidation="false" Text="Hide" />
        </ContentTemplate>
         <Triggers>
        
        <asp:AsyncPostBackTrigger ControlID="btnSumbit" EventName="click" />
    </Triggers>
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                AssociatedUpdatePanelID="UpdatePanel1" >
                <ProgressTemplate>
                    <img id="imgimportimage" alt="Please wait.." src="<%= ResolveUrl("~/images/Progressbar/Loading.gif") %>"
                        style="width: 354px; height: 45px" />
                    <p>Please wait...</p>
                </ProgressTemplate> 
            </asp:UpdateProgress>
</div>
