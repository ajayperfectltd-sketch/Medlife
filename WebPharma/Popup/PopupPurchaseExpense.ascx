<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopupPurchaseExpense.ascx.cs" Inherits="Popup_PopupPurchaseExpense" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <script src="js/Navigation.js"></script>
    <script src="js/toastr.js"></script>
    <script src="js/Custom.js"></script>
  <script src="js/jquery-1.10.2.js"></script>
<script src="../AssetLogin/js/jquery.min.js"></script>
  
         <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
      <script src="js/toastr.js"></script>
<style>

    .gridp  {
            width: auto;
            font-family:tahoma;
            border:solid 3px #66babd;
        }
    	
</style>
<script>

        function CalculateTotal() {
            try {
        
                // CommonPopupExpense_btnTotalAmount
              
                $("#CommonPopupExpense_btnTotalAmount").click();
               // $("#btnQuation").click();
            }
            catch (e) {
                alert(e);
            }
        }
        //function CalculateTotal() {
        //    // alert('d');
        //    var sum = 0;
        //    $.each($("table[id*=" + 'gdvSearch' + "] tr"), function (index, item) {
        //        var val = $(item).find("[id*=gdvSearch_txtAmount_]").text();
        //        alert(val);
        //        sum += parseFloat(val != null ? (val != '' ? val : '0') : '0');
        //        alert(sum);
        //    });

        //    $("#txtTo").val(sum.toFixed(4));

            
        //}

      
        //$("#CommonPopupExpense_btnTotalAmount").click();
        //alert('c');
       
    
    //function OnItemsPurchaseRateChange(id) {
    //    try {
    //        var b = id.replace("gvItems_txtPurchaseRategv_", "");
    //        var c = parseInt(b) + 1;
    //        var d = 'gvItems_txtQuantity_' + c;
    //        var gvItems = document.getElementById("gvItems");
    //        var lastRow = gvItems.rows.length;
    //        var k = 'gvItems_txtPurchaseRategv_' + (lastRow - 2).toString();
    //        if (id == k) {
    //            document.getElementById("hfQtyId").value = 'LastRow';
    //        }
    //        else {
    //            document.getElementById("hfQtyId").value = d;
    //        }
    //        //var reqval = id.replace('txtPurchaseRategv', 'rqvalPurchaseRate');
    //        var txtqty = id.replace('txtPurchaseRategv', 'txtQuantity');
    //        var txtPurRate = id.replace('txtPurchaseRategv', 'txtPurchaseRategv');
    //        // if ($("#" + hdfpacked).val() == "False") {
    //        var qty = $("#" + txtqty).val() == "" ? "0" : $("#" + txtqty).val();
    //        var PurRate = $("#" + txtPurRate).val() == "" ? "0" : $("#" + txtPurRate).val();
    //        $("#" + txtqty).val(qty);
    //        $("#" + txtPurRate).val(PurRate);

    //        $("#btnHiddenItemsPurchaseRateChange").click();

    //    }
    //    catch (ex)
    //    { alert(ex) }
    //}
    </script>

<div align="center" style="border: 1px solid #88D2FF; height: 350px; width: 570px; padding-bottom: 0px; background-color: #d9e0da; border: 1px solid #12361D; padding-bottom: 50px; box-shadow: 10px 10px 32px #888; border-radius: 0px 0px 2px 2px;">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <%-- <div id="divouterline" align="left">--%>


    <div  class="popupdiv">
        <asp:TextBox ID="txtRefno" runat="server" CssClass="textbox" Width="145" Style="display: none;"></asp:TextBox>
        
    </div>
    <div class="clear"></div>
    <div  style="overflow-y: scroll; height:200px">
    <asp:GridView ID="gdvSearch" runat="server" CssClass="gridp" Style="width: 550px; margin-left: 0px;"
        AutoGenerateColumns="False"  Font-Size="10pt" font-family="Arial, Helvetica, sans-serif"
        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"  CellPadding="3"   OnSelectedIndexChanging="gdvSearch_SelectedIndexChanging"   ForeColor="Black" GridLines="Vertical"    EmptyDataText="No Data Found!">
        <AlternatingRowStyle BackColor="#E5EFE4" />
        <Columns>

            <asp:TemplateField HeaderText="SlNo" HeaderStyle-HorizontalAlign="Center">
                 <ItemTemplate>
               
                     <asp:Label ID="lblSlno"  runat="server" Text=" <%#Container.DataItemIndex+1 %>"></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

            <asp:TemplateField HeaderText="Exp.Type" ItemStyle-Wrap="true">

                <ItemTemplate>
                    <asp:HiddenField ID="hdfIDValue" Value='<%# Eval("ID_ExpenseType") %>' runat="server"></asp:HiddenField>
                    <%--<asp:HiddenField ID="hfTaxInterState" Value='<%# Eval("TaxInterState") %>' runat="server"></asp:HiddenField>--%>
                    <asp:HiddenField ID="hfAddLess" Value='<%# Eval("EtAddorLess") %>' runat="server"></asp:HiddenField>
                    <asp:Label ID="lbtnName" runat="server" CausesValidation="false" CommandName="Select" Text='<%# Eval("ExpenseType") %>' OnClientClick="return true;"></asp:Label>
                </ItemTemplate>

                <ItemStyle Wrap="True"></ItemStyle>
            </asp:TemplateField>
            

                   <asp:BoundField DataField="EtAddorLess" HeaderText="Add/Less" />
            

                   <asp:TemplateField HeaderText="Amount"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtAmount"  onblur="CalculateTotal();" onkeypress="CheckNumWithDecimal(event);"  CssClass="textbox" Style="width: 100px; text-align: right;"
                                                            Text='<%# Eval("Amount") %>' />
                                                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle HorizontalAlign="Center" Width="100%" VerticalAlign="Middle"  BackColor="#ffffff"  />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="#07928f" Font-Bold="True" ForeColor="White" Height="25px" BorderColor="Black" />
        <PagerStyle BackColor="#3A7D78" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    </div>
            <br />
            <div class="divR">
       <li>
            <label style="width: 270px; font-family:Tahoma;font-size:12px;" >Total Amount</label>
           <asp:TextBox runat="server" CssClass="textbox" ID="txtTo" Style="text-align: right; width:130px;" AutoComplete="off" MaxLength="10" onKeypress="CheckNumWithDecimal(event);" />
           </li>
                </div>
    <asp:Button ID="btnTotalAmount" runat="server"   Style="display: none;" CausesValidation="false" Text="Hide"   OnClick="btnTotalAmount_Click" />
    <div  class="popupdiv" style="padding-left:100px">
        <asp:Button ID="btnSumbit"  OnClick="btnSumbit_Click"  CssClass="button" Text="Save" runat="server" />
           <asp:Button ID="btnCancel" runat="server"  Visible="false" Text="Cancel" CausesValidation="false"  CssClass="button" OnClientClick="return ClearPopu8SearchReason();" />
        
        <ul class="contentul" style="width: 800px; height: auto; padding: 0px; overflow-y: auto;">
            <asp:HiddenField ID="hfColumn" runat="server" />
              <asp:HiddenField ID="hfTotalExpense" runat="server" />
            <li class="clear"></li>
           
        </ul>
    </div>
 
</div>
</ContentTemplate>
         </asp:UpdatePanel>