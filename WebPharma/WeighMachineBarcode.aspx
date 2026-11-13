<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="WeighMachineBarcode.aspx.cs" Inherits="WeighMachineBarcode" %>

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

                $("#hfWeighMachineBarcodeId").val('0');
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

                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
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
                    <div class="search" style="width: 770px;">Weighing Machine Barcode </div>
                    <div class="add">Add</div>
                    <div class="add_icon">
                        <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" TabIndex="10" OnClientClick="ShowAddDiv(); ClearAdd();   return false; releasebtn();" />
                    </div>
                    <div class="search" style="display: none;">View</div>
                    <div class="search_icon" style="display: none;">
                        <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" OnClick="imgbtnView_Click" />
                    </div>

                </div>
                <%--    <div class="user_bg">
                        <div class="user1">WeighMachineBarcode</div>
                    </div>--%>
                <div class="clear"></div>
                <div class="group_bg2">
                    <div id="divAdd" runat="server" style="display: none;">
                        <ul class="contentul">

                            <li style="display:none;">
                                <asp:Label ID="Label19" runat="server" CssClass="label" Width="80px" Text="Inv. Date"></asp:Label>
                                <asp:TextBox ID="txtDate" CssClass="textboxMandatory" Width="70px" TabIndex="4" runat="server"></asp:TextBox>
                        
                                <cc:MaskedEditExtender ID="MaskedEditExtender78" TargetControlID="txtDate"
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
                                </cc:MaskedEditExtender>
                                <asp:RegularExpressionValidator ID="Invoicedatecheck" runat="server" ValidationGroup="submit" Display="None" SetFocusOnError="True"
                                    ErrorMessage="Please enter date in dd/mm/yyyy format" ForeColor="Red" Font-Bold="true" ControlToValidate="txtDate"
                                    ValidationExpression="(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/\d{4}"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Invoice Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                </cc:ValidatorCalloutExtender>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender57" runat="server" Enabled="True" TargetControlID="Invoicedatecheck">
                                </cc:ValidatorCalloutExtender>
                            </li>

                            <li class="clear"></li>

                             <li class="btnMargin">
                                <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Generate" TabIndex="6" ValidationGroup="Print" OnClick="btnPrint_Click" />
                                <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClick="btnClear_Click" TabIndex="7" />
                                <asp:HiddenField ID="hfWeighMachineBarcodeId" runat="server" />
                                <asp:HiddenField ID="hfClient" runat="server" />
                                <asp:HiddenField ID="hfUser" runat="server" />
                                <asp:HiddenField ID="hfIsAdd" runat="server" />
                                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                <asp:HiddenField ID="hfIsDelete" runat="server" />
                                <asp:HiddenField ID="hdfFkItem" runat="server" />
                                <asp:HiddenField ID="hdfPrintField" runat="server" />

                                <asp:Button ID="btnHiddenPrint" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClientClick="javascript:return PrintSlip(); return false; " />
                                <input id="Button1" type="button" value="button" style="display: none;" onclick=" PrintSlip();" />

                                <asp:HiddenField ID="hfItemStockValue" runat="server" OnValueChanged="hfItemValue_ValueChanged" />
                            </li>
                            <div class="divSeparation"></div>
                            <li>
                                <asp:Label ID="Label2" runat="server" CssClass="label" Style="" Text="History"></asp:Label>
                            </li>
                            <div id="divLog" runat="server">
                            <ul class="contentul">
                                <li>
                                    <asp:GridView ID="gvLog" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" >
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SLNO" />
                                            <asp:BoundField DataField="Date" HeaderText="Date" />
                                            <asp:BoundField DataField="UserCode" HeaderText="User" />
                                         
                                          
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                        <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                    <div class="Pager">
                                        <asp:Repeater ID="rptPager" runat="server">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </li>
                            </ul>
                        </div>
                         



                           
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
