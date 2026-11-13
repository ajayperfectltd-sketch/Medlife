<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest = "false"  CodeFile="BarcodeGeneration.aspx.cs" Inherits="BarcodeGeneration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
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
        //function disablebutn() {
        //    try {

        //        window.setTimeout('delaysubmit()', 50);
        //    }
        //    catch (e) {
        //        alert(e);
        //    }
        //}

        //function releasebtn() {


        //    $("#btnSubmit").prop('disabled', false);

        //}
        //function delaysubmit() {
        //    try {
        //        if (submit == 1) {
        //            return false;

        //        }

        //        $("#btnSubmit").prop('disabled', true);

        //        window.setTimeout('releasebtn()', 3000);
        //        submit = 1;
        //    }
        //    catch (e) {
        //        alert(e);
        //    }
        //}

        function pageLoad() {

            var ctrContainer = "form";
            setKeynavigation(ctrContainer);
            Check();

        }

        function ClearAdd() {
            try {
                submit = 0;

                $("#hfBarcodeGenerationId").val('0');
                $("#hdfFkItem").val('0');
                $("#ddlPrint").val('0');

                $("#hfItemStockValue").val('0');

                $("#txtItemShortName").val('');
                $("#txtBarCode").val('');
                $("#txtCopy").val('');
            }
            // $("#txtBarCode").val('');
            catch (e) {
                alert(e);
            }
        }

        function ShowAddDiv() {
            $("#<%#divAdd.ClientID%>").show();


        }

        function popUpStockSelectClick() {

            try {
                var ItemName = $("#txtItemShortName").val();
                url = "StockItems.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=0' + '&itn=' + ItemName + '&ctgry=0';
                // popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=0' + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val(), "Popup", "width=1000,height=800") + '&ctgry=' + $("#ddlCategory").val();

                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=yes,location=no" +
                                     ",statusbar=no,menubar=no,resizable=0,width=1015," +
                                     "height=650,left = 200,top = 100");
              
                popUpObj.focus();

                //   $("#txtItemShortName").focus();
            }
            catch (e) {
                alert(e);
            }

        }

        function PrintSlip() {
            try {

                if ($("#hdfPrintField").length > 0) {
                    // var a = "<style>.offerTable{background-color: #496ba6;border:2px solid black; width:100%; margin-top:0px;margin-bottom:3.38 px; }.offerTable tr{boder:1px solid black;}.offerTable td{background-color: #d6e9ff;padding: 3px; }.offerTable th{color: White;padding: 3px;border: 1px solid white;text-align: center;} .table{background-color: #e2dfd6;}.table th{background-color: #003c74;color: White;font-weight: bold;padding: 3px;.table td{background-color: #f4f4f0;padding: 5px;}.table td table td{padding: 0px;} </style>";
                    var prtContent = document.getElementById("hdfPrintField");
                    var abcd = $("#hdfPrintField").val();
                    //alert(abcd);

                    var WinPrint = window.open('', '', 'left=0,top=0,width=500');
                    WinPrint.document.write(abcd);
                   WinPrint.document.close();
                    WinPrint.focus();
                    WinPrint.print();

                    WinPrint.close();
                  //  location.reload();
                }
            }
            catch (e) {
                alert(e);
            }

            //return $("#tblTest table").length > 0;
            return false;
        }
        function PrintButtonClick() {

            $("#Button1").click();

        }

    </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
                <div>


                    <div>
                        <div class="icon_bg">
                            <div class="search" style="width: 770px;">Barcode Generation</div>
                            <div class="add">Add</div>
                            <div class="add_icon">
                                <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" TabIndex="10" OnClientClick="ShowAddDiv(); ClearAdd();   return false; releasebtn();" />
                            </div>
                            <div class="search" style="display: none;">View</div>
                            <div class="search_icon" style="display: none;">
                                <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server"  OnClick="imgbtnView_Click" />
                            </div>

                        </div>
                        <%--    <div class="user_bg">
                        <div class="user1">BarcodeGeneration</div>
                    </div>--%>
                        <div class="clear"></div>
                        <div class="group_bg2">
                            <div id="divAdd" runat="server" style="display: block;">
                                <ul class="contentul">
                                    <li>
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtItemShortName" onBlur="popUpStockSelect();" Width="250px" MaxLength="150" TabIndex="1"  />
                                        <asp:Button ID="btnItem" runat="server" OnClick="hfItemValue_ValueChanged" Text="Item" Style="display: none;" />
                                        <asp:ImageButton ID="imgPopUp" runat="server" OnClientClick="return  popUpStockSelectClick();" TabIndex="2" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 0px 0px 0px 6px; position: absolute" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator140" runat="server" ErrorMessage="Item Required " ControlToValidate="txtItemShortName" Display="None" SetFocusOnError="True"  ValidationGroup="Print"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1787" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator140">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="BarCode"></asp:Label>
                                        <asp:TextBox ID="txtBarCode"  onkeypress="CheckInteger(event);"  CssClass="textboxMandatory" runat="server" MaxLength="15"  Width="250px" TabIndex="3"></asp:TextBox>
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="button"  Text="Generate & Save" ValidationGroup="Gen"  OnClick="btnSubmit_Click"  Style="padding: 0px; margin: 0px 0px 0px 6px; position: absolute; width:120px" />

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="BarCode  Required " ControlToValidate="txtBarCode" Display="None" SetFocusOnError="True" ValidationGroup="Print"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                        </cc:ValidatorCalloutExtender>

                                    </li>
                                    <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Print Layout"></asp:Label>
                                        <asp:DropDownList ID="ddlPrint" runat="server" CssClass="dropdownMandatory" TabIndex="4" Width="260px" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select a Print Layout" ControlToValidate="ddlPrint" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Print"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                  
                                     <li>
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="No of copies"></asp:Label>
                                        <asp:TextBox ID="txtCopy" CssClass="textboxMandatory" runat="server" MaxLength="10"  Width="250px" onkeypress="CheckInteger(event);"  TabIndex="5"></asp:TextBox>


                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="No of copies Required " ControlToValidate="txtCopy" Display="None" SetFocusOnError="True" ValidationGroup="Print"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator12">
                                        </cc:ValidatorCalloutExtender>

                                         
                                           <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtCopy" ErrorMessage="Enter a valid number" Display="None" MaximumValue="999999999" ValidationGroup="Print" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                                  <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                        </cc:ValidatorCalloutExtender>


                                           </li>




                                    <li class="clear"></li>
                                    <div class="divSeparation"></div>
                                     <li>
                                      <asp:Label ID="Label2" runat="server" CssClass="label" Style=" " Text="Item Details"></asp:Label>
                                      </li>
                                    <div id="DivPreviousPurchase" runat="server" style="width: 930px; overflow: auto; scrollbar-face-color: blue; height: 150px;">
                                     
                                      <asp:GridView ID="GvPreviousPurchase" runat="server" CssClass="grid"
                                          EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px" Style="margin-left: 5px;"
                                          BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100">
                                          <AlternatingRowStyle BackColor="#CCE0EB" />
                                          <Columns>

                                              <asp:BoundField DataField="SLNO" HeaderText="SLNO" Visible="false" ItemStyle-HorizontalAlign="Center" />
                                              <asp:BoundField DataField="Name" HeaderText="Name" />
                                              <asp:BoundField DataField="ShortName" HeaderText="ShortName" />
                                              <asp:BoundField DataField="BatchNo" HeaderText="BatchNo" />
                                              <asp:BoundField DataField="Barcode" HeaderText="Barcode" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="Large" />

                                              <asp:BoundField DataField="ExpiryDate" HeaderText="ExpiryDate" />
                                              <asp:BoundField DataField="CurrentStock" HeaderText="Current Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="Large" />

                                              <asp:BoundField DataField="MRP" HeaderText="MRP" ItemStyle-HorizontalAlign="Right" />
                                              <asp:BoundField DataField="RetailPrice" HeaderText="Retail Price" ItemStyle-HorizontalAlign="Right" />
                                              <asp:BoundField DataField="WholesalePrice" HeaderText="Wholesale Price" ItemStyle-HorizontalAlign="Right" />
                                              <asp:BoundField DataField="PrivilagePrice" HeaderText="Card Price" ItemStyle-HorizontalAlign="Right" />


                                          </Columns>
                                          <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                          <FooterStyle BackColor="#CCCCCC" />
                                          <HeaderStyle BackColor="#12361D" Font-Bold="True" ForeColor="White" Height="30px" />
                                          <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                          <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                          <SortedAscendingHeaderStyle BackColor="#808080" />
                                          <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                          <SortedDescendingHeaderStyle BackColor="#383838" />

                                      </asp:GridView>
                                    </div>



                                    <li class="btnMargin">
                                        <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" TabIndex="6" ValidationGroup="Print" OnClick="btnPrint_Click" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClick="btnClear_Click" TabIndex="7" />
                                        <asp:HiddenField ID="hfBarcodeGenerationId" runat="server" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                        <asp:HiddenField ID="hdfFkItem" runat="server" />
                                        <asp:HiddenField ID="hdfPrintField" runat="server" />

                                        <asp:Button ID="btnHiddenPrint" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClientClick="javascript:return PrintSlip(); return false; " />
                                        <input id="Button1" type="button" value="button" style="display: none;" onclick=" PrintSlip();" />

                                        <asp:HiddenField ID="hfItemStockValue" runat="server" />
                                    </li>
                                </ul>
                            </div>

                            <%--   <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                            <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
                        </div>
                    </div>
           <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </form>
</body>
</html>
