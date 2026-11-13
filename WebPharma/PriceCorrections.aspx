<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PriceCorrections.aspx.cs" Inherits="PriceCorrections" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <script src="js/Navigation.js"></script>
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <script src="AssetLogin/js/jquery.min.js"></script>
       <script type="text/javascript">
        var popup;
        function popUpStockSelectClick(p) {
            
            $("#hfType ").val(p);
           
            if (Page_ClientValidate("img") == true) {
               
                var popup;
                if (p == 1) {
                    var ItemName = $("#txtitem").val();

                    //  $("#txtitemConv").val('');
                }
                else {
                    var ItemName = $("#txtItmShortName").val();
                    // $("#txtitem").val('');
                }
                //     if ($("#hfFromStore").val() == '') {
                SetStoreDetail();
               
                //  }
                //if (($("#hfFromStore").val().length) > 0) {
                if (p == 1) {
                    
                    var ItemName = $("#txtitem").val();
                    var hfUser = $("#hfUser").val();
                    var hfClient = $("#hfClient").val();
                    var ddlCategory = $("#ddlCategory").val();
                    var hfStore = $("#hfStore").val();
                    var txtSupRefno = $("#txtSupRefno").val();
                    $.ajax({
                        type: "POST",
                        url: 'Textile.asmx/GetUrlEncryptStock',
                        data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','txtSupRefno':'" + txtSupRefno + "','hfStore':'" + hfStore + "','hfClient':'" + hfClient + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (MenuHtml) {
                            
                            url = "StockItems.aspx?" + MenuHtml.d;

                            popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                            ",statusbar=no,menubar=no,resizable=0,width=1000," +
                            "height=800,left = 200,top = 100");
                            popUpObj.focus();
                            //popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfStore").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val() + '&suplr=' + $("#txtSupRefno").val(), "Popup", "width=1000,height=800");
                            //popup = window.open("StockItems.aspx?" + +MenuHtml.d, "Popup", "width=1000,height=800");
                            //popup.focus();
                        },
                        error: function (MenuHtml) {
                            // alert('GetMinItemStockName');
                            //alert('webservice_error');
                            // $("#hfEncryptvalue").val("");
                        }
                    });

                   
                }
                else {
                    url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName;

                    popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=yes,location=no" +
                    ",statusbar=no,menubar=no,resizable=0,width=1015," +
                    "height=650,left = 200,top = 100");
                    popUpObj.focus();

                }

                //}
            }

            return false;
        }
        function SetStoreDetail() {
            var store = $("#ddlStore option:selected").val();
            $("#hfStore").val(store);

            //  ItemClear();

        }

        function EnterTab(e, Id1) {

            var intKey = window.Event ? e.which : e.KeyCode;


            if (intKey == 13) {
                e.returnValue = false;
                $("#" + Id1).focus();
            }
        }
        function ItemClear() {

            
            var toDate = new Date();
            $("#txtDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtMRP").val("0");
            $("#txtQuantity").val("0");
            $("#txtSalesPrice").val("0");
            $("#txtNewSalePrice").val("0");
            $("#txtitem").val("");
            alert('f');




        }
    </script>
</head>
<body>
    
    <form id="frmPricecorrection" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="user">Replace</div>
                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">

                                <asp:ImageButton ID="ImageButton1" ImageUrl="images/ADD.png" TabIndex="58" Width="37" Height="38" runat="server" CausesValidation="false" OnClientClick="ClearAdd();ShowAddDiv();  releasebtn();" OnClick="ImageButton1_Click" />
                                <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" TabIndex="57" Width="37" Height="40" runat="server" OnClick="imgbtnView_Click" />

                            </div>
                        </asp:Panel>
                    </div>

                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg">
                            <div id="divAdd" runat="server" style="display: block; height: 985px">
                                <ul class="contentul" id="Container">

                                    <div class="divL" style="width: 400px;">
                                        <li>
                                            <asp:Label ID="Label7" runat="server" CssClass="label" Text="Store"></asp:Label>
                                            <asp:DropDownList ID="ddlStore" runat="server" onkeydown="EnterTab(event,'txtDate');" CssClass="dropdownMandatory" TabIndex="1"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqvalidatorStore" runat="server" ErrorMessage="Select a Store" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="reqvalidatorStore">
                                            </cc:ValidatorCalloutExtender>
                                        </li>
                                    </div>
                                    <div class="divR" style="width: 400px;">

                                        <li>
                                            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Date"></asp:Label>

                                            <asp:TextBox runat="server" CssClass="textboxMandatory" onkeydown="EnterTab(event,'ddlCategory');" Width="130px" ValidationGroup="submit" ID="txtDate" TabIndex="5" AutoComplete="off" />
                                            <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                            </cc:ValidatorCalloutExtender>




                                        </li>
                                    </div>
                                    <div class="clear"></div>
                                    <li class="clear"></li>
                                    <div class="divSeparation"></div>
                                    <div class="divL" style="width: 400px;">
                                        <li id="liCategory" runat="server">
                                            <asp:Label ID="lblcat" runat="server" class="label" Width="138" Text="Category"></asp:Label>
                                            <asp:DropDownList ID="ddlCategory" onkeydown="EnterTab(event,'ddlCategory');"  runat="server" CssClass="dropdown"></asp:DropDownList>

                                        </li>
                                        <li>
                                            <asp:Label ID="lblItem" runat="server" CssClass="label" Text="Item"></asp:Label>
                                            <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtitem" MaxLength="20" AutoComplete="off" Width="150px" TabIndex="10" onblur="return popUpStockSelect1();" />

                                            <asp:ImageButton ID="ImageButton3" onkeydown="EnterTab(event,'txtNewSalePrice');" runat="server" TabIndex="11" OnClientClick="return popUpStockSelectClick(1);" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute;" ValidationGroup="img" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Item Required " ControlToValidate="txtitem" Display="None" SetFocusOnError="True" ValidationGroup="addPurchase"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                            </cc:ValidatorCalloutExtender>
                                        </li>

                                        <li>
                                            <asp:Label ID="Label31" runat="server" CssClass="label" Text="Sales Price"></asp:Label>
                                            <asp:TextBox ID="txtSalesPrice" CssClass="textbox" TabIndex="15" Width="190px" runat="server" MaxLength="11" Style="text-align: right; font-weight: bold;" onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>

                                        </li>


                                        <li class="clear"></li>

                                    </div>
                                    <div class="divR" style="width: 400px;">
                                        <li>
                                            <asp:Label ID="Label2" runat="server" CssClass="label" Width="138px" Text="Quantity"></asp:Label>
                                            <asp:TextBox ID="txtQuantity" CssClass="textbox" TabIndex="15" Width="130px" runat="server" MaxLength="11" Style="text-align: right; font-weight: bold;" onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label3" runat="server" CssClass="label" Width="138px" Text="MRP"></asp:Label>
                                            <asp:TextBox ID="txtMRP" CssClass="textbox" TabIndex="15" Width="130px" runat="server" MaxLength="11" Style="text-align: right; font-weight: bold;" onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                        </li>
                                        <li>
                                            <asp:Label ID="Label4" runat="server" CssClass="label" Width="138px" Text="New SalesPrice"></asp:Label>
                                            <asp:TextBox ID="txtNewSalePrice" onkeydown="EnterTab(event,'btnAddItems');"  CssClass="textbox" TabIndex="15" Width="130px" runat="server" MaxLength="11" Style="text-align: right; font-weight: bold;" onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>
                                        </li>

                                    </div>



                                </ul>
                                <div class="clear"></div>
                                <div style="float: left; width: 90px;">
                                    <li>
                                        <asp:Button Text="Add" ID="btnAddItems" runat="server" OnClick="btnAddItems_Click" TabIndex="18" ValidationGroup="add" CssClass="button" />

                                    </li>
                                </div>
                                <div style="float: left; width: 80px; margin-right: 4px">
                                    <li>
                                        <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="19" OnClientClick="ItemClear();return false;" />
                                    </li>
                                </div>
                                <div class="clear"></div>

                                <div class="divSeparation"></div>
                                <li style="width: 100%; min-height: 210px; max-height: 210px; overflow-y: auto;">
                                    <asp:GridView ID="GvPriceData" runat="server" Width="95%"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items" OnRowDeleting="GvPriceData_RowDeleting" OnSelectedIndexChanging="GvPriceData_SelectedIndexChanging" PageSize="10">

                                        <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                        <Columns>
                                                 <asp:TemplateField HeaderText="SLNO" ItemStyle-HorizontalAlign="Center">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox21" runat="server" Text='<%# Eval("SLNO") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSLNO" runat="server" Text='<%# Eval("SLNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Items" ItemStyle-HorizontalAlign="Center">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox21" runat="server" Text='<%# Eval("Item") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSLNO" runat="server" Text='<%# Eval("Item") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                        
                                                         <asp:TemplateField HeaderText="Quantity">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox67" Style="width: 20px; text-align: right;" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblgvquty" runat="server" Text='<%# Eval("Quantity") %>' Style="text-align: right; float: right;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FK_Item" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox4322" runat="server" Text='<%# Eval("FK_Items") %>'></asp:TextBox>
                                                    </EditItemTemplate>

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvFK_Items" runat="server" Text='<%# Eval("FK_Items") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="SalePrice" >
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtSalePriceText" runat="server" Text='<%# Eval("SalesPrice") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSalesPrice" runat="server" Text='<%# Eval("SalesPrice") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="NewSalePrice" >
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtNewSalePriceText" runat="server" Text='<%# Eval("NewSalesPrice") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvNewSalesPrice" runat="server" Text='<%# Eval("NewSalesPrice") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


<%--                                            <asp:BoundField DataField="slno" HeaderText="SLNO" HeaderStyle-Width="5px"  ItemStyle-CssClass="gridviewItemStyle" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Item" DataField="Item" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-Width="300px" />
                                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />--%>

                                            <%--<asp:BoundField HeaderText="MRP" DataField="MRP" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px" />--%>
                                            <%--     <asp:TemplateField HeaderText="FK_Item" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox4322" runat="server" Text='<%# Bind("FK_Items") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvFK_Items" runat="server" Text='<%# Bind("FK_Items") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            <%--<asp:BoundField HeaderText="SalePrice" DataField="SalesPrice"  ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30px" />--%>
                                            <%--<asp:BoundField HeaderText="NewSalePrice" DataField="NewSalesPrice" ItemStyle-CssClass="gridviewItemStyle" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="50px" ItemStyle-Width="50px"/>--%>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton125" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton255" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="1px" />
                                        <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                </li>
                                <asp:Button ID="btnItem" runat="server" Text="Item" Style="display: none;" />
                                <li style="display: none;">
                                    <asp:Label ID="Label33" runat="server" Width="80px" CssClass="label" Text="Item"></asp:Label>
                                    <asp:DropDownList ID="ddlItem" runat="server" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdownMandatory"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select an Item" ControlToValidate="ddlItem" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <asp:HiddenField ID="hfClient" runat="server" />
                                <asp:HiddenField ID="hfUser" runat="server" />
                                <asp:HiddenField ID="hfStore" runat="server" />

                                <asp:HiddenField ID="hfuserName" runat="server" />
                                <asp:HiddenField ID="hfIsAdd" runat="server" />
                                <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                <asp:HiddenField ID="hfIsDelete" runat="server" />
                                <asp:HiddenField ID="hfFK_PurchaseReturn" runat="server" />
                                <asp:HiddenField ID="hdfFkItem" runat="server" />
                                <asp:HiddenField ID="hdfFKStock" runat="server" />
                                <asp:HiddenField ID="hfItem" runat="server" />
                                <asp:HiddenField ID="hfID_Price" runat="server" />
                                <asp:HiddenField ID="hfItemId" runat="server" />
                                <asp:HiddenField ID="hfType" runat="server" />
                                <asp:HiddenField ID="hfSlNo" runat="server" />
                                <asp:HiddenField ID="hdfItem" runat="server" />
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
