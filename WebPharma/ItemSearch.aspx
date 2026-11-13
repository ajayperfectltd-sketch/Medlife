<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemSearch.aspx.cs" EnableEventValidation="false" Inherits="ItemSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
       <script src="js/toastr.js"></script>

    <script type="text/javascript">
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
       
        function BlursubCat() {

            $("#<%#btnSubCat.ClientID%>").click();
             $("#<%#ddlSearchManufacturer.ClientID%>").focus();

        }
      

        
    </script>
  
</head>
<body>
    <form id="form1" runat="server"  autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                  <div class="btn_bg5"> 
                       <div class="heading2">

                            <div class="user">Items</div>
                           </div>
                    <div class="clear"></div>
                    
                    <div class="group_bg2">
                     
                        <div id="divView" runat="server"  style="height:750px;" >
                           
                              <div  style="width:950px">
                            <div class="divL">
                                <ul class="contentul">
                                   
                                         <li style="display:none;">
                                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="BarCode"></asp:Label>
                                             <asp:TextBox ID="txtBarcode" runat="server" CssClass="textbox" TabIndex="1" ></asp:TextBox>
                                        </li>
                                        <li style="display:none;">
                                            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Short Name"></asp:Label>
                                            <asp:TextBox ID="txtSearchShortName" CssClass="textbox" runat="server" TabIndex="2"></asp:TextBox>
                                        </li>
                                
                                        <li>
                                            <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                            <asp:TextBox ID="txtSearchName" CssClass="textbox" runat="server"    TabIndex="3"></asp:TextBox>
                                        </li>
                                       
                                   
                                 <li class="clear"></li>
                                  <li>
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Unit"></asp:Label>
                                        <asp:DropDownList ID="ddlsearchUnit" runat="server"   CssClass="dropdown" TabIndex="4"></asp:DropDownList>
                                    </li>
                                  
                                         <li>
                                            <asp:Label ID="Label12" runat="server" CssClass="label" Text="Subunit"></asp:Label>
                                            <asp:DropDownList ID="ddlSerachSubUnit" runat="server" CssClass="dropdown" TabIndex="4"></asp:DropDownList>
                                        </li>
                                    </ul>
                                        </div>
                                    
                                   
                              
                           <%-- </div>--%>
                           
                        <div class="divR">
                               <ul class="contentul">
                                    <li>
                                            <asp:Label ID="Label10" runat="server" CssClass="label" Text="Category"></asp:Label>
                                            <asp:DropDownList ID="ddlSerachCategory" runat="server" onblur="BlursubCat();" OnSelectedIndexChanged="ddlSerachCategory_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdown" TabIndex="4"></asp:DropDownList>
                                        </li>
                                     <li >
                                            <asp:Label ID="Label7" runat="server" CssClass="label" Text="Subcategory"></asp:Label>
                                            <asp:DropDownList ID="ddlSeachSubCategory" runat="server" CssClass="dropdown" TabIndex="4"></asp:DropDownList>
                                        </li>
                                    <li style="display:none;">
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Text="Category Second"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchCategorySecond" runat="server" CssClass="dropdown" TabIndex="5"></asp:DropDownList>
                                    </li>
                                    <li style="display:none;">
                                        <asp:Label ID="Label11" runat="server" CssClass="label" Text="Category Third"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchCategoryThird" runat="server" CssClass="dropdown" TabIndex="6"></asp:DropDownList>
                                    </li>
                                   
                                    <li>
                                        <asp:Label ID="Label32" runat="server" CssClass="label" Text="Manufacturer"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchManufacturer" runat="server" CssClass="dropdown" TabIndex="7"></asp:DropDownList>
                                    </li>
                                         <li style="display:none">
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Supplier"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchSupplier" runat="server" CssClass="dropdown" TabIndex="8"></asp:DropDownList>
                                    </li>
                                  
                                  </ul>
                             
                           </div>
                                         <li class="clear"></li>
                                        <li class="btnMargin"  style="padding-left:335px;padding-top:15px">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="10" />
                                            <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearchClick();return false;"  TabIndex="11" />
                                        </li>
                                    </div>
                           
                            <li class="clear"></li>
                            <ul class="contentul">
                                <asp:GridView ID="gvItems" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" Width="900px"  BorderStyle="Solid" AllowPaging="true" OnPageIndexChanging="gvItems_PageIndexChanging" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10" DataKeyNames="ID_Items" >
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                                       <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server"  OnClientClick="return SetName(id);" ToolTip="Select" ImageUrl="~/images/icon.png" Width="20px"  Height="20px" Style="outline-color:red;  outline-width:3px;   "  />
                                                <asp:HiddenField ID="hdfValue" runat="server"  Value='<%#Eval("ID_Items") %>'/>
                                                 <asp:HiddenField ID="hdfItem" runat="server"  Value='<%#Eval("ItemName") %>'/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" Visible="false"  ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Barcode" HeaderText="Barcode" Visible="false" />
                                        <asp:BoundField DataField="ItemName" HeaderText="Name" />
                                         <asp:BoundField DataField="ShortName" HeaderText="Short Name" />
                                        <asp:BoundField DataField="UnitName" HeaderText="Unit" />
                                        <asp:BoundField DataField="SubUnitName" HeaderText="Subunit" />
                                        <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                                          <asp:BoundField DataField="SubCategoryName" HeaderText="Subcategory" />
                                          <asp:BoundField DataField="ManufactureName" HeaderText="Manufacture" />
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle CssClass="gridviewHeaderStyle"  Font-Bold="True" ForeColor="White" Height="30px" />
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
                                <li class="clear"></li>
                              
                        </div>
                           
                        </div>

                    </div>
                    <asp:HiddenField ID="hfClient" runat="server" />
                    <asp:HiddenField ID="hfUser" runat="server" />
                      <asp:Button ID="btnSubCat" Text="Hide" runat="server" Style="display: none;" CausesValidation="false"   OnClick="btnSubCat_Click" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
         <script src="js/jquery-1.10.2.js"></script>
         <script src="js/Custom.js"></script>

      <script type="text/javascript">
          function ClearSearch()
          {
            try {   
                $("#txtSearchShortName").val('');
                $("#ddlSearchCategorySecond").val("0");
                $("#ddlSearchCategoryThird").val("0");
                $("#ddlSearchManufacturer").val("0");
                $("#ddlSearchBrand").val("0");
                $("#txtBarcode").val('');
                $("#ddlSearchSize").val('0');
                $("#ddlSearchColor").val('0');
                $("#ddlSearchSupplier").val('0');
                $("#ddlSearchStorage").val('0');            
                $("#txtMRP").val(''); 
            }
              catch (ex)
              {
                alert(ex);
                }
        }
        function ClearSearchClick() {
            try {
                $("#txtSearchShortName").val('');
                $("#txtSearchName").val('');
                $("#ddlsearchUnit").val("0");
                $("#ddlSerachSubUnit").val("0");
                $("#ddlSearchManufacturer").val("0");
                $("#ddlSerachCategory").val("0");
                $("#ddlSeachSubCategory").val("0");
            }
            catch (ex) {
                alert(ex);
            }
        }


        function SetName(id) {
            try {
                
                if (window.opener != null && !window.opener.closed) {
                    var ddlItem = window.opener.document.getElementById("ddlItem");
                    var hfItemValue = window.opener.document.getElementById("hfItemValue");      
                    var hdfValue = $("#" + id).attr("id").replace("ImageButton1", "hdfValue");
                    var hdfItem = $("#" + id).attr("id").replace("ImageButton1", "hdfItem");

                    if (ddlItem != null)
                           ddlItem.value =  document.getElementById(hdfValue).value;
                    if (hfItemValue != null)
                        hfItemValue.value = document.getElementById(hdfValue).value;
                    var HfItemsID = window.opener.document.getElementById("HfItemsID");
                    if (HfItemsID != null)
                        HfItemsID.value = document.getElementById(hdfValue).value;
                    var txtName = window.opener.document.getElementById("txtName");
                    
                    var txtUnit = window.opener.document.getElementById("txtUnit");
                    var btnItem = window.opener.document.getElementById("btnItem");

                    var hfFK_Items = window.opener.document.getElementById("hfFK_Items");
                    
                    var hfItem = window.opener.document.getElementById("hfItem");
                 
                    var txtitem = window.opener.document.getElementById("txtitem");
                    var txtQuantity = window.opener.document.getElementById("txtQuantity");
                 
                    var hfType = window.opener.document.getElementById("hfType");

                    //  var txtitemConv = window.opener.document.getElementById("txtitemConv");
                    //  var txtQuantityConv = window.opener.document.getElementById("txtQuantityConv");

                    var txtSearchItem = window.opener.document.getElementById("txtSearchItem");

                    if (txtName != null && txtUnit != null) {
                        txtName.value = $("#" + id).closest('tr').find('td:eq(2)').text();
                        txtUnit.value = $("#" + id).closest('tr').find('td:eq(4)').text();
                    }
                    if (hfItem != null)
                        hfItem.value = document.getElementById(hdfItem).value;
                   
                    if (ddlItem != null)
                        ddlItem.focus();
                   
                    if (hfFK_Items != null) {
                       
                        hfFK_Items.value = document.getElementById(hdfValue).value;
                        if (hfItem != null)
                            hfItem.value = document.getElementById(hdfItem).value;
                      
                        //if (hfType != null && window.opener.document.getElementById("hfType").value == 2) {
                           
                        //    txtitemConv.value = hfItem.value;

                        //    txtQuantityConv.focus();
                        //}
                        // else
                       
                        if (txtitem != null && hfItem != null)
                            txtitem.value = hfItem.value;
                      
                        if (txtQuantity != null) {
                            txtQuantity.focus();
                        }
                      
                        try
                        {
                            if (hfType != null && hfType.value != null && hfType.value == '2')
                            {
                                
                               
                                if (txtSearchItem != null && hfItem != null)
                                    txtSearchItem.value = hfItem.value
                                if (txtitem != null)
                                    txtitem.value = "";
                                
                            }
                        }
                        catch (e) {
                        }
                       
                    }
                    if (btnItem != null) {
                       
                        btnItem.click();
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

    </script>
    </form>
</body>
</html>
