<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockItems.aspx.cs" Inherits="StockItems" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Stock Items::</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script>
        function ShowImage(id) {
            if (window.opener != null && !window.opener.closed) {
                var hfId_Itemspos = window.opener.document.getElementById("hfId_Itemspos");
                var hfId_Items = $("#" + id).attr("id").replace("imgbtnAttachment", "hfAttachment");
                var attachment = document.getElementById(hfId_Items).value;
                url = "ItemImage.aspx" + "?attach=" + attachment.toString();
                popUpObjNew = window.open(url, "PopupNew", "toolbar=no,scrollbars=no,location=no" +
                 ",statusbar=no,menubar=no,resizable=0,width=305," +
                 "height=305,left = 550,top = 200");
                popUpObjNew.focus();
            }
        }

    </script>
    <style>
        .hidden {
            display: none;
        }
    </style>

</head>
<body>
    <form id="frmItemsView" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <div class="btn_bg5"> 
                      <div class="heading2">
                        <div class="user">Stock Items</div>
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">

                         <div id="divView" runat="server">
                               <div  style="width:950px">
                                  <div class="divL">
                                <ul class="contentul">
                                    <li style="display: none;">
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="BarCode"></asp:Label>
                                        <asp:TextBox ID="txtBarCode" CssClass="textbox" MaxLength="8" runat="server" TabIndex="1"></asp:TextBox>
                                    </li>

                                    <li style="display: none;">

                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchShortName" CssClass="textbox" MaxLength="10" TabIndex="2" runat="server"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" CssClass="textbox" MaxLength="14" TabIndex="3" runat="server"></asp:TextBox>
                                    </li>
                                     <li class="clear"></li>
                                    <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Batch"></asp:Label>
                                        <asp:TextBox ID="txtBatch" CssClass="textbox" MaxLength="8" runat="server" TabIndex="4"></asp:TextBox>

                                    </li> 
                                  
                                </ul>
                            </div>
                            <div class="divR">
                                <ul class="contentul">
                                    <li>
                                        <li >
                                            <asp:Label ID="Label12" runat="server" CssClass="label" Text="Supplier"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchSupplier" runat="server" CssClass="dropdown" TabIndex="4"></asp:DropDownList>
                                        </li>
                                        <li style="display: none;">
                                            <asp:Label ID="Label10" runat="server" CssClass="label" Text="Storage"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchStorage" runat="server" CssClass="dropdown" TabIndex="4"></asp:DropDownList>
                                        </li>
                                        
                                        <li style="display: none;">
                                            <asp:Label ID="Label7" runat="server" CssClass="label" Text="Category first"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchCategoryFirst" runat="server" CssClass="dropdown" TabIndex="4"></asp:DropDownList>
                                        </li>
                                        <li style="display: none;">
                                            <asp:Label ID="Label4" runat="server" CssClass="label" Text="Storage"></asp:Label>
                                            <asp:TextBox ID="txtSearchExpDate" runat="server" CssClass="textbox" TabIndex="4"></asp:TextBox>
                                        </li>
                                        <%--   <li style="display: none;">
                                            <asp:Label ID="Label9" runat="server" CssClass="label" Text="Category Second"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchCategorySecond" runat="server" CssClass="dropdown" TabIndex="5"></asp:DropDownList>
                                        </li>
                                        <li style="display: none;">
                                            <asp:Label ID="Label11" runat="server" CssClass="label" Text="Category Third"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchCategoryThird" runat="server" CssClass="dropdown" TabIndex="6"></asp:DropDownList>
                                        </li>--%>
                                        <li>
                                            <asp:Label ID="Label32" runat="server" CssClass="label" Text="Manufacturer"></asp:Label>
                                            <asp:DropDownList ID="ddlSearchManufacturer" runat="server" CssClass="dropdown" TabIndex="7"></asp:DropDownList>
                                        </li>
                             <li class="clear"></li>
                                         <li>
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Text="MRP"></asp:Label>
                                        <asp:TextBox ID="txtMRP" CssClass="textbox" MaxLength="8" runat="server" TabIndex="9"  onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                    </li>
                                          <li style="display:none">
                                        <asp:CheckBox ID="chkZeroStock" runat="server" Text="zero Stock" Checked="false" Style="margin-left: 130px" TabIndex="10" />

                                    </li>

                                        <li style="display: none;">
                                            <asp:Label ID="Label3" runat="server" CssClass="label" Text="Sales Price"></asp:Label>
                                            <asp:TextBox ID="txtSalesPrice" CssClass="textbox" MaxLength="8" runat="server"></asp:TextBox>
                                            <cc:FilteredTextBoxExtender ID="txtROQuantity_FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtSalesPrice" FilterType="Custom , Numbers" ValidChars=".">
                                            </cc:FilteredTextBoxExtender>
                                        </li>
                                    </li>
                                </ul>
                            </div>
                            
                            <li class="clear"></li>
                               <li class="btnMargin"  style="padding-left:335px;padding-top:15px">
                                     <asp:Button ID="btnSearch" runat="server" CssClass="button" TabIndex="5" Text="Search" OnClick="btnSearch_Click" />
                                     <asp:Button ID="btnSearchClear" runat="server" CssClass="button" TabIndex="6" Text="Clear" OnClientClick="ClearAll();return false;" />
                                 </li>
                                    </div>
                                    <asp:HiddenField ID="hfddlSearchSupplier" Value="0" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfFk_Shift" runat="server" />
                                    <asp:HiddenField ID="hfStore" runat="server" />
                                    <asp:HiddenField ID="hfNotReturnable" runat="server" />
                               <asp:HiddenField ID="hfMode" runat="server" />
                             <li class="clear"></li>
                            <ul class="contentul">
                                <asp:GridView ID="gvItems" runat="server" CssClass="grid" Style="width: 900px;"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White"  BorderStyle="Solid" OnPageIndexChanging="gvItems_PageIndexChanging" AllowPaging="true"  BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ID_Items,ID_Stock"
                                    OnSelectedIndexChanging="gvItems_SelectedIndexChanging">
                                    <AlternatingRowStyle CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnSelectID" runat="server" OnClientClick="SetData(id);" CommandName="Select" ToolTip="Select" ImageUrl="~/images/icon.png" Width="20px" Height="20px"   Style="outline-color:red;  outline-width:3px;" />

                                                <div style="display: none;">
                                                    <asp:HiddenField ID="hfId_Stock" Value='<%#Eval("ID_Stock") %>' runat="server" />
                                                    <asp:HiddenField ID="hfItems" Value='<%#Eval("ID_Items") %>' runat="server" />
                                                    <asp:HiddenField ID="hfBatchNo" Value='<%#Eval("FK_Store") %>' runat="server" />
                                                    <asp:HiddenField ID="hfCurrentStock" Value='<%#Eval("MRP") %>' runat="server" />
                                                    <asp:HiddenField ID="hfCategory" Value='<%#Eval("FK_Category") %>' runat="server" />
                                                    <asp:HiddenField ID="hfFK_Unit" Value='<%#Eval("FK_Units") %>' runat="server" />
                                                    <asp:HiddenField ID="hfCs" Value='<%#Eval("CurrentStock") %>' runat="server" />
                                                    <asp:HiddenField ID="hdfPurRate" Value='<%#Eval("PurRate") %>' runat="server" />
                                                    <asp:HiddenField ID="hdfShortName" Value='<%#Eval("ShortName") %>' runat="server" />
                                                     <asp:HiddenField ID="hdfSaleprice" Value='<%#Eval("SalPrice") %>' runat="server" />
                                                     <asp:HiddenField ID="hfItemName" Value='<%#Eval("ItemName") %>' runat="server" />
                                                   
                                                </div>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" Visible="false" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Name" />
                                        <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                        <asp:BoundField DataField="BatchNo" HeaderText="Batch" />


                                        <asp:BoundField DataField="ExpiryDate" HeaderText="Exp.Date" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="CurrentStock" HeaderText="Cur.Stock" ItemStyle-HorizontalAlign="Right" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="Small" />

                                        <asp:BoundField DataField="MRP" HeaderText="MRP" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="Store" HeaderText="Store" />
                                      

                                        <%--  <asp:TemplateField ItemStyle-Wrap="true">
                                            <ItemTemplate>
                                                <%--<asp:ImageButton ID="imgbtnAttachment" ImageUrl="~/images/1400410121_photo-24.png" OnClientClick="ShowImage(id);" Width="25px" Height="22px" runat="server" ToolTip="Show Image" />--%>
                                        <%--  <asp:HiddenField ID="hfAttachment" Value='<%#Eval("Attachment") %>' runat="server" />--%>
                                        <%--</ItemTemplate>
                                        </asp:TemplateField>--%--%>
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
                            <li class="clear"></li>
                      
                            <li style="width: 450px; float: left;">
                                <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                                <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>

                            </li>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script type="text/javascript">
            function ClearSearch() {
                try {

                    $("#txtSearchShortName").val('');
                    $("#txtBatch").val('');
                    $("#txtBarCode").val('');
                    $("#txtMRP").val('');
                    
                    //  $("#txtSearchName").val('');
                    // $("#ddlSearchCategoryFirst").val("0");
                    $("#ddlSearchCategorySecond").val("0");
                    $("#ddlSearchCategoryThird").val("0");
                    $("#ddlSearchManufacturer").val("0");
                    $("#ddlSearchBrand").val("0");
                    $("#txtBarcode").val('');
                    $("#ddlSearchSize").val('0');
                    $("#ddlSearchColor").val('0');
                    $("#ddlSearchSupplier").val('0');
                    $("#ddlSearchStorage").val('0');
                    $("#ddlFromStore").val('0');
                    $("#chkZeroStock").prop("checked", false);


                }
                catch (ex) {
                    alert(ex);
                }
            }
            function ClearAll() {
                try {

                    $("#txtSearchShortName").val('');
                    $("#txtBatch").val('');
                    $("#txtBarCode").val('');
                    $("#txtSearchName").val('');
                    $("#txtMRP").val('');
                    //$("#ddlSearchCategoryFirst").val("0");
                    $("#ddlSearchCategorySecond").val("0");
                    $("#ddlSearchCategoryThird").val("0");
                    $("#ddlSearchManufacturer").val("0");
                    $("#ddlSearchBrand").val("0");
                    $("#txtBarcode").val('');
                    $("#ddlSearchSize").val('0');
                    $("#ddlSearchColor").val('0');
                    $("#ddlSearchSupplier").val('0');
                    $("#ddlSearchStorage").val('0');
                    $("#ddlFromStore").val('0');


                }
                catch (ex) {
                    alert(ex);
                }
            }




            function SetData(id) {

                try {
                 
                    
                    if (window.opener != null && !window.opener.closed) {
                      
                        var hdfShortName = $("#" + id).attr("id").replace("imgbtnSelectID", "hdfShortName");
                        var hdfItem = $("#" + id).attr("id").replace("ImageButton1", "hdfItem");
                        var Iten = document.getElementById(hdfItem).value;
                        var IteNamrID = $("#" + id).attr("id").replace("imgbtnSelectID", "hfItemName");
                        var ItemName = document.getElementById(IteNamrID).value;
                        var shortName = document.getElementById(hdfShortName).value;
                       // alert(ItemName);
                        var hfItemStockValue = window.opener.document.getElementById("hfItemStockValue");
                        var txtItemShortName = window.opener.document.getElementById("txtItemShortName");
                        var txtItemShortNameCopy = window.opener.document.getElementById("txtItemShortNameCopy");
                        if (txtItemShortName != null)
                            txtItemShortName.value = shortName;
                        if (txtItemShortNameCopy != null)
                            txtItemShortNameCopy.value = shortName;


                       
                       
                        var hdfPurRate = $("#" + id).attr("id").replace("imgbtnSelectID", "hdfPurRate");
                        var txtPurChaseRate = window.opener.document.getElementById("txtPurChaseRate");
                        if (txtPurChaseRate != null)
                            txtPurChaseRate.value = document.getElementById(hdfPurRate).value;

                        var hdfSaleprice = $("#" + id).attr("id").replace("imgbtnSelectID", "hdfSaleprice");
                        var txtSalesPrice = window.opener.document.getElementById("txtSalesPrice");
                        if (txtSalesPrice != null)
                            txtSalesPrice.value = document.getElementById(hdfSaleprice).value;


                  


                        var cs = $("#" + id).attr("id").replace("imgbtnSelectID", "hfCs");
                        var hdfCurrentStock = window.opener.document.getElementById("hdfCurrentStock");
                        if (hdfCurrentStock != null)
                            hdfCurrentStock.value = document.getElementById(cs).value;

                       
                        var hfId_Stock = $("#" + id).attr("id").replace("imgbtnSelectID", "hfId_Stock");
                        var hdfFKStock = window.opener.document.getElementById("hdfFKStock");

                        var txtfkIssuedItems = window.opener.document.getElementById("txtfkIssuedItems");
                        if (hdfFKStock != null)
                            hdfFKStock.value = document.getElementById(hfId_Stock).value;

                        if (txtfkIssuedItems != null)
                            txtfkIssuedItems.value = document.getElementById(hfId_Stock).value;

                        if (hfItemStockValue != null) {
                            hfItemStockValue.value = document.getElementById(hfId_Stock).value;
                            
                        }
                       // Category

                        var hfCategory = $("#" + id).attr("id").replace("imgbtnSelectID", "hfCategory");
                        var hdfcategory = window.opener.document.getElementById("hdfcategory");
                        if (hdfcategory != null)
                            hdfcategory.value = document.getElementById(hfCategory).value;
                        var hfItems = $("#" + id).attr("id").replace("imgbtnSelectID", "hfItems");
                        var hdfFkItem = window.opener.document.getElementById("hdfFkItem");
                        var txtfkIssuedItems = window.opener.document.getElementById("txtfkIssuedItems");
                        if (txtfkIssuedItems != null)
                            txtfkIssuedItems.value = document.getElementById(hfItems).value;
                        if (hdfFkItem != null)
                            hdfFkItem.value = document.getElementById(hfItems).value;
              

                        var btnItem = window.opener.document.getElementById("btnItem");
                      
                        var hfItem = window.opener.document.getElementById("hfItem");
                        var hfItemName = window.opener.document.getElementById("hfItemName");
                        if (hfItemName != null)
                            hfItemName.value = ItemName;
                        var txtitem = window.opener.document.getElementById("txtitem");
                        var txtQuantity = window.opener.document.getElementById("txtQuantity");
                        var txtCurrentStock = window.opener.document.getElementById("txtCurrentStock");
                                          
                        if (txtitem != null)
                            txtitem.value = ItemName;
                     var ddlItem = window.opener.document.getElementById("ddlItem");
                       
                     //  alert(ddlItem); alert(hfItem);
                       if (ddlItem != null)
                           ddlItem.value = document.getElementById(hfItems).value;
                     
                        if (hfItem != null) {

                           if (txtQuantity != null)
                               txtQuantity.focus();
                           if (txtitem != null)
                               txtitem.value = ItemName;
                           hfItem.value = ItemName;
                          //  alert(txtitem.value);

                            try {
                                if (txtCurrentStock != null)
                                    txtCurrentStock.value = document.getElementById(cs).value;

                                var FK_Units = $("#" + id).attr("id").replace("imgbtnSelectID", "hfFK_Unit");
                                var hfFK_Unit = window.opener.document.getElementById("hfFK_unit");
                                if (hfFK_Unit != null)
                                    hfFK_Unit.value = document.getElementById(FK_Units).value;
                            }
                            catch (ex) {
                            }

                        }

                        if (btnItem != null) {
                            btnItem.click();
                           // alert('h');
                        }
                    }


                    window.close();
                    return false;
                }
                catch (e) {
                    alert(e);
                }
                return false;
            }
            //function pageLoad() {

            //    //var ItemName = document.getElementById("txtSearchShortName");
            //    //ItemName.focus();
            //}
            //function closeModel() {

            //    window.close();
            //}



        </script>
    </form>
</body>
</html>
