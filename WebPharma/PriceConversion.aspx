<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="PriceConversion.aspx.cs" Inherits="PriceConversion" %>

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
    <script src="AssetLogin/js/jquery.min.js"></script>
    <style type="text/css">
    .modal
    { 
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        padding-top:180px;
        min-height: 100%;
        width: 100%;
        display: none;
        position: fixed;
        opacity:  0.5;
        filter: alpha(opacity=50);
        -moz-opacity: 0.5;
        background-color: White;
        z-index: 999;
    }
</style>
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
                $("#hfItemValue").val('0');
                $("#hfItemStockValue").val('0');

                $("#txtItemShortName").val('');
                $("#txtBelowQuantity").val('0');
                $("#ddlStore").val("0");
                $("#ddlCategory").val("0");
                $("#ddlCriteria").val("1");

            }
            // $("#txtBarCode").val('');
            catch (e) {
                alert(e);
            }
        }

        function ShowAddDiv() {
            $("#<%#divAdd.ClientID%>").show();


        }
        function ShowProgress() {
            if (Page_ClientValidate("Save")) {
              //  alert('its valid');

                setTimeout(function () {
                    //  var modal = $('<div />');
                    //  modal.addClass("modal");
                    ////  $('body').append(modal);

                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);
            }
            else {
                //alert('its not valid');
            }
        }

        function popUpStockSelectClick() {

            try {
                var ItemName = $("#txtItemShortName").val();
                url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#ddlStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val();
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
                    location.reload();
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
    <script src="AssetLogin/js/jquery.min.js"></script>
 <script type="text/javascript">
    $("[id*=chkHeader]").live("click", function () {
        var chkHeader = $(this);
        var grid = $(this).closest("table");
        $("input[type=checkbox]", grid).each(function () {
            if (chkHeader.is(":checked")) {
                $(this).attr("checked", "checked");
                $("td", $(this).closest("tr")).addClass("selected");
            } else {
                $(this).removeAttr("checked");
                $("td", $(this).closest("tr")).removeClass("selected");
            }
        });
    });
    $("[id*=chkRow]").live("click", function () {
        var grid = $(this).closest("table");
        var chkHeader = $("[id*=chkHeader]", grid);
        if (!$(this).is(":checked")) {
            $("td", $(this).closest("tr")).removeClass("selected");
            chkHeader.removeAttr("checked");
        } else {
            $("td", $(this).closest("tr")).addClass("selected");
            if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                chkHeader.attr("checked", "checked");
            }
        }
    });
</script>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>--%>
        <div>


            <div>
                <div class="icon_bg">
                    <div class="search" style="width: 770px;">Price Conversion</div>
                    <div class="add">Add</div>
                    <div class="add_icon">
                        <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" TabIndex="14" OnClientClick="ShowAddDiv(); ClearAdd();   return false; releasebtn();" />
                    </div>
                    <div class="search" style="display: none;">View</div>
                    <div class="search_icon" style="display: none;">
                        <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" runat="server" OnClick="imgbtnView_Click" />
                    </div>

                </div>
                <%--    <div class="user_bg">
                        <div class="user1">BarcodeGeneration</div>
                    </div>--%>
                <div class="clear"></div>
                <div class="group_bg2" style="margin: 10px 0px 0px 10px;">
                    <div id="divAdd" runat="server" style="display: none;">
                        <ul class="contentul">

                            <div class="divL" style="width: 430px;">
                                <li>
                                    <asp:Label ID="Label7" runat="server" CssClass="label" Text="Store"></asp:Label>
                                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="dropdownMandatory" TabIndex="1" Width="260px"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqvalidatorStore" runat="server" ErrorMessage="Select a Store" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="reqvalidatorStore">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <li>
                                    <asp:Label ID="Label23" runat="server" CssClass="label" Text="Category"></asp:Label>
                                    <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="2" CssClass="dropdown" Width="260px" AutoPostBack="true"></asp:DropDownList>

                                </li>

                            </div>
                            <div class="divL" style="width: 430px;">

                                <li>
                                    <asp:Label ID="Label8" runat="server" CssClass="label" Text="Item"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtItemShortName" onBlur="popUpStockSelect();" Width="250px" MaxLength="150" TabIndex="3" />
                                    <asp:Button ID="btnItem" runat="server" Text="Item" Style="display: none;" />
                                    <asp:ImageButton ID="imgPopUp" runat="server" OnClientClick="return  popUpStockSelectClick();" TabIndex="4" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 0px 0px 0px 6px; position: absolute" />

                                </li>
                                  <li class="btnMargin">
                                <asp:Button ID="btnShow" runat="server" CssClass="button" Text="Show" TabIndex="5" ValidationGroup="Show" OnClick="btnShow_Click" />
                            </li>
                            </div>
                            <li class="clear"></li>
                          

                        </ul>
                    </div>
                    <ul class="contentul">
                        <li>
                            <div class="divSeparation"></div>
                            <li>
                                <asp:Label ID="Label2" runat="server" CssClass="label" Style="" Text="Item Details"></asp:Label>
                            </li>
                            <li>
                                <asp:Label ID="lblRowCount" runat="server" Width="300px" ForeColor="Red" CssClass="label" Style="" Text=""></asp:Label>
                            </li>
                            <div id="DivPreviousPurchase" runat="server" style="width: 930px; overflow: auto; scrollbar-face-color: blue; height: 150px;">

                                <asp:GridView ID="GvPreviousPurchase" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px" Style="margin-left: 5px;" DataKeyNames="ID_Items" OnRowCommand="GvPreviousPurchase_RowCommand"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100">
                                    <AlternatingRowStyle BackColor="#E5EFE4" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                             <HeaderTemplate  >
                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate  >
                                                <asp:CheckBox ID="chkBox" ItemStyle-HorizontalAlign="Center" runat="server" onclick="CheckBoxCheck(this);" CssClass="chkbx" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" Visible="false" ItemStyle-HorizontalAlign="Center" />
                                      <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkView" runat="server"   CommandName="ItemStockSelect" ><%#Eval("Name") %></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ShortName" HeaderText="ShortName" />
                                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                                     
                                        <%--   <asp:BoundField DataField="Barcode" HeaderText="Barcode" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="Large" />

                                              <asp:BoundField DataField="ExpiryDate" HeaderText="ExpiryDate" />
                                              <asp:BoundField DataField="CurrentStock" HeaderText="Current Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="Large" />

                                              <asp:BoundField DataField="MRP" HeaderText="MRP" ItemStyle-HorizontalAlign="Right" />
                                              <asp:BoundField DataField="RetailPrice" HeaderText="Retail Price" ItemStyle-HorizontalAlign="Right" />
                                              <asp:BoundField DataField="WholesalePrice" HeaderText="Wholesale Price" ItemStyle-HorizontalAlign="Right" />
                                              <asp:BoundField DataField="PrivilagePrice" HeaderText="Card Price" ItemStyle-HorizontalAlign="Right" />
                                        --%>
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
                                <div class="divSeparation"></div>
                            <div class="Pager">
                        
                           
                                    <asp:Repeater ID="rptPager" runat="server">
                                       
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClick="Page_Changed" Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                              
                                    </asp:Repeater>
                                </div>
                             
                            <div class="divSeparation"></div>
                        </li>
                    </ul>


                    <ul class="contentul">
                        <li>
                             <li>
                                <asp:Label ID="Label4" runat="server" CssClass="label" Style="" Text="Stock Details"></asp:Label>
                            </li>
                            <div id="DivCurrentStock" runat="server" style="width: 930px; overflow: auto; scrollbar-face-color: blue; height: 150px;">

                                <asp:GridView ID="GvCurrentStock" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px" Style="margin-left: 5px;" DataKeyNames="ID_Stock"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>




                           
                                  
                                    






                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" Visible="false" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Name" HeaderText="Name" Visible="false" />

                                        <asp:BoundField DataField="Unit" HeaderText="Unit" Visible="false" />

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
                                    <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />

                                </asp:GridView>



                            </div>
                             <div class="divSeparation"></div>
                        </li>
                    </ul>
                    <ul class="contentul">
                        <div class="divL" style="width: 430px;">
                            <li>
                                <asp:Label ID="lblCriteriaName" runat="server" CssClass="label" Text="Criteria"></asp:Label>
                                <asp:DropDownList runat="server" CssClass="dropdown" Width="260px" ID="ddlCriteria" AutoPostBack="true" TabIndex="3" OnSelectedIndexChanged="ddlCriteria_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Update last purchase price</asp:ListItem>
                                    <asp:ListItem Value="2">Adjust to negetive Stock</asp:ListItem>
                                    <asp:ListItem Value="4">Fix the Sales Price</asp:ListItem>

                                </asp:DropDownList>
                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select a Criteria" ControlToValidate="ddlCriteria" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                </cc:ValidatorCalloutExtender>--%>
                            </li>
                        </div>
                        <div class="divL" style="width: 460px;">
                            <li>
                                <asp:Label ID="lblCriteria" runat="server" CssClass="label" Text="Below Quantity"></asp:Label>
                                <asp:TextBox runat="server" CssClass="textbox" ID="txtBelowQuantity" Width="250px" MaxLength="12" TabIndex="3" onkeypress="CheckNumWithDecimal(event);" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Required field" ControlToValidate="txtBelowQuantity" Display="None" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                </cc:ValidatorCalloutExtender>

                            </li>
                        </div>
                        <li class="clear"></li>
                        <li class="btnMargin">
                            <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Process" TabIndex="6" ValidationGroup="Save"  OnClientClick=" ShowProgress();"  OnClick="btnSave_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClick="btnClear_Click" TabIndex="7" />
                            <asp:HiddenField ID="hfBarcodeGenerationId" runat="server" />
                            <asp:HiddenField ID="hfClient" runat="server" />
                            <asp:HiddenField ID="hfUser" runat="server" />
                            <asp:HiddenField ID="hfIsAdd" runat="server" />
                            <asp:HiddenField ID="hfIsUpdate" runat="server" />
                            <asp:HiddenField ID="hfIsDelete" runat="server" />
                            <asp:HiddenField ID="hfItemValue" runat="server" OnValueChanged="hfItemValue_ValueChanged" />
                            <asp:HiddenField ID="hdfPrintField" runat="server" />

                            <asp:Button ID="btnHiddenPrint" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClientClick="javascript:return PrintSlip(); return false; " />
                            <input id="Button1" type="button" value="button" style="display: none;" onclick="return PrintSlip();" />

                            <asp:HiddenField ID="hfItemStockValue" runat="server" />
                        </li>
                    </ul>
                </div>

                <%--   <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                            <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>
            </div>
        </div>
                
                 <div class="loading" align="center" >
    Processing. Please wait.<br />
    <br />
      <asp:Image id="Image1" runat="server" style="margin-top:50px;"
           AlternateText="Image text"
           ImageAlign="Middle"
           ImageUrl="~/images/loader2.gif"/>           
</div>
        <%-- </ContentTemplate>
        </asp:UpdatePanel>--%>
    </form>
</body>
</html>
