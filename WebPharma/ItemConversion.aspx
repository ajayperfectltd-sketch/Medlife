<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemConversion.aspx.cs" Inherits="ItemConversion" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Item Conversion::</title>
    <link href="css/main.css" rel="stylesheet" />

    <link href="css/toastr.css" rel="stylesheet" />
</head>
<body>
    <form id="frmItemConversion" runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        

                         <div class="search" style="width:670px; " >Item Conversion</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/add1.png" TabIndex="25" Width="37" Height="38" runat="server" CausesValidation="false" OnClientClick="ShowAddDiv();  releasebtn();" OnClick="imgbtnAdd_Click" />
                        </div>

                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="35" runat="server" OnClick="imgbtnView_Click" />
                        </div>
                    </div>
               
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: block; height: 485px">




                            <ul class="contentul" id="Container">
                                <asp:Panel id="PanelAdd" runat="server"  >
                                <div class="divL" style="width: 325px;">
                                    <li>
                                        <label class="label" style="width: 60px;">Date</label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtDate" TabIndex="1" AutoComplete="off" />
                                        <cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                        </cc:ValidatorCalloutExtender>
                                    </li>


                                    <li></li>
                                    <li class="clear"></li>




                                </div>
                                <div class="divL" style="width: 325px;">
                               
                                    <li>
                                        <label class="label"style="margin-left: -66px;">From Store</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlFromStore" TabIndex="2" style="margin-left: -50px;" >
                                        </asp:DropDownList>
                                      

                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="From Store required" ControlToValidate="ddlfromStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="img"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                        </cc:ValidatorCalloutExtender>

                                    </li>
                                    <li id="liPurchase" runat="server">
                                       
                                    </li>
                                    <li id="liCategory" runat="server">
                                     

                                    </li>

                                </div>
                                <div style="float: left; width: 139px;">
                                    <li>

                                        <asp:Label class="label" ID="lbl" runat="server" Text="To Store" Width="100" style="margin-left: -109px;"></asp:Label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlToStore" TabIndex="3" style="margin-left: -39px;">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqvalStore" runat="server" ErrorMessage="To Store required" ControlToValidate="ddlToStore" Display="None" SetFocusOnError="True"
                                            InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="reqvalStore">
                                        </cc:ValidatorCalloutExtender>
                                        
                                    </li>

                                  


                                </div>
                                     <div style="float: left; width:146px;">
                                      <li>

                                            <asp:CheckBox ID="chkproduction" runat="server" Text="Production" Style="margin-left: 38px" TabIndex="4" AutoPostBack="true" OnCheckedChanged="chkproduction_CheckedChanged" Font-Bold="True" Font-Size="Small" />
                                       
                                    
                                    </li>
                                         
                                </div>
                                <li class="clear"></li>

                                <div class="divSeparation"></div>



                                <div class="clear"></div>
                                <li class="clear"></li>
                                <div id="divItemConversion" runat="server">
                                    <div class="divL" style="width: 420px;">
                                        <li>
                                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item" Width="60px"></asp:Label>
                                            <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtitem" MaxLength="20" AutoComplete="off" Width="300px" TabIndex="5" />

                                             <%--  <asp:TextBox runat="server" CssClass="textboxMandatory" ID="TextBox1" MaxLength="20" AutoComplete="off" Width="300px" TabIndex="4" onblur="return popUpStockSelect(1);" />--%>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Item required" ControlToValidate="txtitem" Display="None" SetFocusOnError="True" ValidationGroup="add">

                                            </asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                            </cc:ValidatorCalloutExtender>
                                          
                                            <asp:ImageButton ID="ImageButton3" runat="server"  OnClientClick="return popUpStockSelectClick(1);" TabIndex="5" ImageUrl="~/images/serach_icon.png" Style="padding-left: 2px; margin-top: 0px;position: absolute" ValidationGroup="img" />
                                          
                                             
                                        </li>



                                        <li class="clear"></li>

                                    </div>
                                    <div class="divL" style="width: 225px;">

                                        <li>

                                            
                                            <asp:Label ID="Label9" runat="server" CssClass="label" Text="Quantity" Width="70px"></asp:Label>
                                            <asp:TextBox ID="txtQuantity" CssClass="textboxMandatory" Style="text-align: right;" Width="100px" runat="server" TabIndex="6" MaxLength="8" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True"
                                                ValidationGroup="add"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="rfvQuantity">
                                            </cc:ValidatorCalloutExtender>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" SetFocusOnError="True" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                            </cc:ValidatorCalloutExtender>

                                            <asp:CompareValidator ID="cmpquantity" runat="server" ErrorMessage="cannot greater than current stock" ControlToCompare="txtCurrentStock" SetFocusOnError="True" ControlToValidate="txtQuantity" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ValidationGroup="add"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="cmpquantity">
                                            </cc:ValidatorCalloutExtender>


                                        </li>
                                         <li >
                                          <asp:Label ID="lblCurrentStock" runat="server" CssClass="label" Width="200px" Font-Bold="true" ForeColor="Red" Text="Current Stock=0"></asp:Label>
                                </li>
                                        <li style="display: none;">


                                            <asp:TextBox runat="server" ID="txtCurrentStock" />
                                        </li>


                                    </div>
                                <div style="float: left; width: 90px; margin-right:4px">
                                           <li>
                                            <asp:Button Text="Add" ID="btnAddItems" runat="server" OnClick="btnAddItemsFrom_Click" TabIndex="7" ValidationGroup="add" CssClass="button" />
                                            </li>
                                        </div>
                                       <div style="float: left; width: 80px; margin-right:4px">
                                           <li>
                                            <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" OnClick="btnClearItems_Click" TabIndex="8" OnClientClick="ItemClear();" />
                                         </li>
                                                </div>


                                


                                    <div class="clear"></div>
                                    <div class="divSeparation"></div>
                                    <li style="width: 920px; height: 130px; max-height: 140px; overflow-y: auto;">


                                        <asp:GridView ID="gvItems" runat="server" CssClass="grid" OnRowDeleting="gvItems_RowDeleting"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px"
                                            BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical"
                                            DataKeyNames="FK_Items,FK_Stock,slno" PageSize="100">
                                            <AlternatingRowStyle BackColor="#CCE0EB" />
                                            <Columns>
                                               

                                                <asp:BoundField DataField="slno" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="Item" HeaderText="Item"  />
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right"/>
                                                 <asp:BoundField DataField="PurRate" HeaderText="PurRate" HeaderStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="CardPrice" HeaderText="Card Price" HeaderStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right" />
                                                <asp:BoundField DataField="WholeSalePrice" HeaderText="Whole Sale Price" HeaderStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right"/>
                                                <asp:BoundField DataField="RetailPrice" HeaderText="Retail Price" HeaderStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right"/>
                                                <asp:BoundField DataField="MRP" HeaderText="MRP" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                                                <asp:BoundField DataField="BatchNo" HeaderText="BatchNo" />
                                                <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-HorizontalAlign="Right"/>
                                                   <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton255" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                        
                                        <div class="divR"  style="width: 320px;">
                                            
                                            <label class="label"  >Total Amount</label>
                                            <asp:TextBox runat="server" CssClass="textbox" ID="txtFromAmount" ReadOnly="true" Style="text-align: right;float:right;" TabIndex="190" AutoComplete="off" />
                                               
                                        </div>


                                    </li>




                                </div>

                                  


                                <div>
                                    <div class="divL">


                                        <li class="clear"></li>

                                        <li class="btnMargin" style="display:none;">

                                            <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnTransfer" OnClick="btnTransfer_Click" TabIndex="9" OnClientClick="disablebutn();" />

                                        </li>




                                    </div>


                                    <div class="divR">
                                        <li class="clear"></li>
                                        <li class="btnMargin">
                                            <asp:CheckBox ID="cbxTransfer" runat="server" Text="Transfer" Style="margin-left: 100px" TabIndex="8" AutoPostBack="true" OnCheckedChanged="cbxTransfer_CheckedChanged" />
                                        </li>
                                    </div>


                                </div>
                                <%--------------------------------------------------------------------------------------(  End  )------------------------------------------------------------------------- --%>

                                <li class="clear"></li>

                                <div class="divSeparation"></div>

                                <div class="divL" style="width: 250px;">
                                    <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Item" Width="70px"></asp:Label>
                                       <%-- <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtitemConv" MaxLength="20" AutoComplete="off" Width="100px" TabIndex="10" onblur="return popUpStockSelect(2);" />--%>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtitemConv" MaxLength="20" AutoComplete="off" Width="100px" TabIndex="10" />

                                        <asp:RequiredFieldValidator ID="rfItemConv" runat="server" ErrorMessage="Item required" ControlToValidate="txtitemConv" Display="None" SetFocusOnError="True"
                                            ValidationGroup="addConv">
                                        </asp:RequiredFieldValidator>
                                           <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="rfItemConv">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:ImageButton ID="ImageButton1" runat="server"  OnClientClick="return popUpStockSelectClick(2);" TabIndex="11" ImageUrl="~/images/serach_icon.png" Style="padding-left: 2px; margin-top: 0px;position: absolute" ValidationGroup="img" />

                                    </li>


                                    <li>
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Quantity" Width="70px"></asp:Label>
                                        <asp:TextBox ID="txtQuantityConv" CssClass="textboxMandatory" Style="text-align: right;" Width="100px" runat="server" TabIndex="11" MaxLength="8" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="rfvQuantityConv" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantityConv" Display="None" SetFocusOnError="True"
                                            ValidationGroup="addConv"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="rfvQuantityConv">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQuantityConv" ErrorMessage="Enter a valid Quantity" Display="None" SetFocusOnError="True"
                                            MaximumValue="999999999999" ValidationGroup="addConv" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                        </cc:ValidatorCalloutExtender>
                                        

                                    </li>
                                    <li class="clear"></li>

                                </div>
                                <div class="divL" style="width: 250px;">

                                     <li>
                                         <asp:Label ID="Label10" runat="server" CssClass="label" Text="Purchase Rate" Width="100px"></asp:Label>
                                        <asp:TextBox ID="txtPurChaseRate" CssClass="textboxMandatory" Style="text-align: right;width: 100px;" runat="server" TabIndex="12" MaxLength="8" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>
                                      
                                              <asp:RequiredFieldValidator ID="rfvPurChaseRate" runat="server" ErrorMessage="PurChase Rate required" ControlToValidate="txtPurChaseRate" Display="None" SetFocusOnError="True"
                                            ValidationGroup="addConv"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server" Enabled="True" TargetControlID="rfvPurChaseRate">
                                        </cc:ValidatorCalloutExtender>


                                        

                                     

                                    </li>
                                   
                                 

                                      <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="MRP" Width="100px"></asp:Label>
                                        <asp:TextBox ID="txtMRP" CssClass="textboxMandatory" Style="text-align: right;" Width="100px" runat="server" TabIndex="13" MaxLength="8" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>
                                    

                                        
                                         <asp:RequiredFieldValidator ID="rfvMRP" runat="server" ErrorMessage="MRP required" ControlToValidate="txtMRP" Display="None" SetFocusOnError="True"
                                            ValidationGroup="addConv"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server" Enabled="True" TargetControlID="rfvMRP">
                                        </cc:ValidatorCalloutExtender>

                                            <asp:CompareValidator ID="cmpPurChaseRate" runat="server" ErrorMessage="Cannot less than PurChase Rate " ControlToCompare="txtPurChaseRate" SetFocusOnError="True"  ControlToValidate="txtMRP" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="GreaterThanEqual" Type="Double" ValidationGroup="addConv"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="cmpPurChaseRate">
                                            </cc:ValidatorCalloutExtender>

                       

                                    </li>
                                       <div id="divproductioncost" runat="server"  style="display: block;" >

                                       <li >
                                        <asp:Label ID="Label13" runat="server" CssClass="label" Text="Production Cost" Width="100px"></asp:Label>
                                        <asp:TextBox ID="txtproductioncost" CssClass="textbox" Style="text-align: right;" Width="100px" runat="server" TabIndex="13" MaxLength="8" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>

                       

                                    </li>

                                    </div>
                                </div>
                                <div class="divL" style="width: 230px;">

                                    <li>
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Retail Price " Width="100px"></asp:Label>
                                        <asp:TextBox ID="txtRetailPriceConv" CssClass="textboxMandatory" Style="text-align: right;" Width="100px" runat="server" TabIndex="14" MaxLength="8" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>
                                         <asp:CompareValidator ID="cmpRetailPrice" runat="server" ErrorMessage="Cannot greater than MRP" ControlToCompare="txtMRP" SetFocusOnError="True" ControlToValidate="txtRetailPriceConv" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ValidationGroup="addConv"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" Enabled="True" TargetControlID="cmpRetailPrice">
                                            </cc:ValidatorCalloutExtender>

                                        

                                         <asp:RequiredFieldValidator ID="rfvRetailPrice" runat="server" ErrorMessage="Retail Price required" ControlToValidate="txtRetailPriceConv" Display="None" SetFocusOnError="True"
                                            ValidationGroup="addConv"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" Enabled="True" TargetControlID="rfvRetailPrice">
                                        </cc:ValidatorCalloutExtender>

                                             <asp:CompareValidator ID="cmpRetailPrice1" runat="server" ErrorMessage="Cannot less than PurChase Rate " ControlToCompare="txtPurChaseRate" SetFocusOnError="True" ControlToValidate="txtRetailPriceConv" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="GreaterThanEqual" Type="Double" ValidationGroup="addConv"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server" Enabled="True" TargetControlID="cmpRetailPrice1">
                                            </cc:ValidatorCalloutExtender>

                                    </li>

                                  
                                     <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Card Price" Width="100px"></asp:Label>
                                        <asp:TextBox ID="txtCardPrice" CssClass="textboxMandatory" Style="text-align: right;" Width="100px" runat="server" TabIndex="15" MaxLength="8" onKeypress="CheckNumWithDecimal(event);"  AutoComplete="off"></asp:TextBox>
                                         <asp:CompareValidator ID="cmpCardPrice" runat="server" ErrorMessage="Cannot greater than MRP" ControlToCompare="txtMRP" SetFocusOnError="True" ControlToValidate="txtCardPrice" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ValidationGroup="addConv"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" Enabled="True" TargetControlID="cmpCardPrice">
                                            </cc:ValidatorCalloutExtender>
                                        
                                        <asp:CompareValidator ID="cmpCardPrice1" runat="server" ErrorMessage="Cannot greater than Retail Price " ControlToCompare="txtRetailPriceConv" SetFocusOnError="True" ControlToValidate="txtCardPrice" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ValidationGroup="addConv"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" Enabled="True" TargetControlID="cmpCardPrice1">
                                            </cc:ValidatorCalloutExtender>

                                           <asp:RequiredFieldValidator ID="rfvCardPrice" runat="server" ErrorMessage="Card Price required" ControlToValidate="txtCardPrice" Display="None" SetFocusOnError="True"
                                            ValidationGroup="addConv"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server" Enabled="True" TargetControlID="rfvCardPrice">
                                        </cc:ValidatorCalloutExtender>

                                             <asp:CompareValidator ID="cmpCardPrice2" runat="server" ErrorMessage="Cannot less than PurChase Rate " ControlToCompare="txtPurChaseRate" SetFocusOnError="True" ControlToValidate="txtCardPrice" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="GreaterThanEqual" Type="Double" ValidationGroup="addConv"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender24" runat="server" Enabled="True" TargetControlID="cmpCardPrice2">
                                            </cc:ValidatorCalloutExtender>

                                    </li>
                                     <li>
                                        <asp:Label ID="Label12" runat="server" CssClass="label" Text="BarCode " Width="100px"></asp:Label>
                                        
                                             <asp:TextBox ID="txtBarCode" CssClass="textbox"  Width="100px" runat="server" TabIndex="18"  AutoComplete="off"></asp:TextBox>
                                 
                                    </li>


                                         














                                </div>
                                <div class="divL" style="width: 230px;">

                                 

                                    
                                     <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Whole Sale" Width="80px"></asp:Label>
                                        <asp:TextBox ID="txtWholeSale" CssClass="textboxMandatory" Style="text-align: right;" Width="100px" runat="server" TabIndex="16" MaxLength="8" onKeypress="CheckNumWithDecimal(event);"  AutoComplete="off"></asp:TextBox>

                                       <asp:CompareValidator ID="cmpWholeSale1" runat="server" ErrorMessage="Cannot greater than Retail Price " ControlToCompare="txtRetailPriceConv" SetFocusOnError="True" ControlToValidate="txtWholeSale" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ValidationGroup="addConv"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="cmpWholeSale1">
                                            </cc:ValidatorCalloutExtender>
                                       

                                        <asp:CompareValidator ID="cmpWholeSale" runat="server" ErrorMessage="Cannot greater than MRP " ControlToCompare="txtMRP" SetFocusOnError="True" ControlToValidate="txtWholeSale" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" Type="Double" ValidationGroup="addConv"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="cmpWholeSale">
                                            </cc:ValidatorCalloutExtender>

                                           <asp:CompareValidator ID="cmpWholeSale2" runat="server" ErrorMessage="Cannot less than PurChase Rate " ControlToCompare="txtPurChaseRate" SetFocusOnError="True" ControlToValidate="txtWholeSale" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="GreaterThanEqual" Type="Double" ValidationGroup="addConv"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender25" runat="server" Enabled="True" TargetControlID="cmpWholeSale2">
                                            </cc:ValidatorCalloutExtender>

                                         <asp:RequiredFieldValidator ID="rfvWholeSale" runat="server" ErrorMessage="Whole Sale Price Required" ControlToValidate="txtWholeSale" Display="None" SetFocusOnError="True"
                                            ValidationGroup="addConv"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" Enabled="True" TargetControlID="rfvWholeSale">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

















                                </div>
                                    <div class="divL" style="width: 200px;">

                                     <li>
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="SLNO " Width="80px"></asp:Label>
                                        <asp:DropDownList ID="ddlSlno" runat="server" style="width: 110px;" AutoPostBack="true" TabIndex="17" CssClass="dropdownMandatory"  OnSelectedIndexChanged="ddlSlno_SelectedIndexChanged">
                                        </asp:DropDownList>

                                        <asp:RequiredFieldValidator ID="rfvSlno" runat="server" ErrorMessage="Slno required" ControlToValidate="ddlSlno" InitialValue="0" Display="None" SetFocusOnError="True"
                                            ValidationGroup="addConv">
                                        </asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="rfvSlno">
                                        </cc:ValidatorCalloutExtender>


                                    </li>


                                </div>
                                 
                                     <div class="divL" style="width: 230px;">

                                     <li>
                                        <asp:Label ID="Label11" runat="server" CssClass="label" Text="BatchNo " Width="80px"></asp:Label>
                                        
                                             <asp:TextBox ID="txtBatchNo" CssClass="textbox"  Width="100px" runat="server" TabIndex="18"  AutoComplete="off"></asp:TextBox>

                                    </li>


                                </div>
                                         
                                     <div class="divL" >


                                    <li class="clear"></li>

                                    <li class="btnMargin">
                                         <asp:Button Text="Add" ID="btnAddItemsConv" runat="server" OnClick="btnAddItemsTo_Click" TabIndex="19" ValidationGroup="addConv" CssClass="button" />
                                         
                                            <asp:Button Text="Clear" ID="btnClearItemsConv" runat="server" CssClass="button" TabIndex="19" OnClientClick="ItemClear();" OnClick="btnClearItemsConv_Click"  />
                                         
                                    </li>
                                </div>


                                <li class="clear"></li>
                                <div class="divSeparation"></div>
                                     <div id="DivPreviousPurchase" runat="server" style=" width:930px; overflow:auto; scrollbar-face-color: blue; height:150px;">
                                   Previous purchases
                                      <asp:GridView ID="GvPreviousPurchase" runat="server" CssClass="grid" 
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px"  Style=" margin-left:5px;"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical"  PageSize="100">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>
                                        
                                        <asp:BoundField HeaderText="Fk_Item" DataField="Fk_Items" Visible="false" />
                                               <asp:BoundField HeaderText="BillNo" DataField="BillNo" />
                                               <asp:BoundField HeaderText="BillDate" DataField="BillDate" />
                                       
                                        <asp:BoundField HeaderText="Pur. Rate" DataField="PurchaseRate" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField HeaderText="MRP" DataField="MRP"  ItemStyle-HorizontalAlign="Right"  />
                                                <asp:BoundField HeaderText="Pur.Vat" DataField="PurchaseVat"  ItemStyle-HorizontalAlign="Right"  />
                                              <asp:BoundField HeaderText="Disc %" DataField="DiscountPer"  ItemStyle-HorizontalAlign="Right"  />
                                             <asp:BoundField HeaderText="Disc Amt" DataField="DiscountAmount"  ItemStyle-HorizontalAlign="Right"  />
                                                <asp:BoundField HeaderText="Profit %" DataField="ProfitPer"  ItemStyle-HorizontalAlign="Right"  />
                                             <asp:BoundField HeaderText="R.Price" DataField="RetailPrice"  ItemStyle-HorizontalAlign="Right"  />
                                             <asp:BoundField HeaderText="W.Price" DataField="WholeSalePrice"  ItemStyle-HorizontalAlign="Right"  />
                                             <asp:BoundField HeaderText="C.Price" DataField="CardPrice"  ItemStyle-HorizontalAlign="Right"  />
                                        <asp:BoundField HeaderText="Stock" DataField="Stock" ItemStyle-HorizontalAlign="left"  />
                                     <asp:BoundField HeaderText="BatchNo" DataField="BatchNo" />
                                        <asp:BoundField HeaderText="ExpiryDate" DataField="ExpiryDate"  Visible="false"/>
                                           
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
                                <li style="width: 920px; min-height: 70px; max-height: 140px; overflow-y: auto;">

                                    <asp:GridView ID="gvItemsConv" runat="server" CssClass="grid" style="margin:0;padding:0"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical"
                                        DataKeyNames="FK_Items" OnRowDeleting="gvItemsConv_RowDeleting" PageSize="100" OnRowDataBound="gvItemsConv_RowDataBound">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>
                                           
                                              <asp:TemplateField HeaderText="SLNO" ItemStyle-HorizontalAlign="Center">
                                                 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSLNO" runat="server" Text='<%# Bind("SLNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="Item" DataField="Item" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"   />
                                            <asp:BoundField DataField="PurRate" HeaderText="PurRate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"   />
                                            <asp:BoundField DataField="CardPrice" HeaderText="Card Price" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField DataField="WholeSalePrice" HeaderText="Whole Sale Price" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField DataField="RetailPrice" HeaderText="Retail Price" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField DataField="MRP" HeaderText="MRP" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField HeaderText="BatchNo" DataField="BatchNo" />
                                                  <asp:BoundField HeaderText="BarCode" DataField="BarCode" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"/>
                                            <asp:BoundField HeaderText="SlNo From" DataField="SlNoFrom" ItemStyle-HorizontalAlign="Center"/>
                                              <asp:BoundField HeaderText="Productcost" DataField="Productcost" ItemStyle-HorizontalAlign="Center" Visible="true"/>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton255" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
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


                                </li>


                                <li class="clear"></li>
                                <br class="clear" />
                                <%--  </ContentTemplate>
                                        
                                    </asp:UpdatePanel>--%>




                                <div class="divL">

                                    <label class="label">Remark</label>
                                    <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" CssClass="multiline" MaxLength="500" TabIndex="21" AutoComplete="off" />

                                    <li id="cbx" style="display: none;">

                                        <asp:TextBox ID="txtClosedRemark" runat="server" TextMode="MultiLine" MaxLength="500" TabIndex="163" AutoComplete="off" />
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                        <asp:HiddenField ID="HiddenField4" runat="server" />
                                    </li>
                                    <asp:HiddenField ID="hdfDate" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                    <asp:HiddenField ID="hfToStore" runat="server" />
                                    <asp:HiddenField ID="hfFromStore" runat="server" />
                                    <asp:HiddenField ID="hdfFkItem" runat="server" />
                                    <asp:HiddenField ID="hfItem" runat="server" />
                                    <asp:HiddenField ID="hf_Item" runat="server" />
                                    <asp:HiddenField ID="hf_ItemConv" runat="server" />
                                    <asp:HiddenField ID="hfType" runat="server" />
                                    <asp:HiddenField ID="hdfFKStock" runat="server" />
                                    <asp:HiddenField ID="hdfFKStockConv" runat="server" />
                                    <asp:HiddenField ID="hdfFKStockItem" runat="server" />
                                    <asp:HiddenField ID="hdfPurRate" runat="server" />
                                    <asp:HiddenField ID="hfFK_Items" runat="server" />
                                     <asp:HiddenField ID="hdfCurrentStock" runat="server" />
                                    
                                  
                                    <%-- <li style="display: none;">


                                        <asp:TextBox runat="server" ID="txtPurChaseRate" />
                                    </li>--%>


                                    <li style="display: none;">


                                        <asp:TextBox runat="server" ID="txtCurrentStockTo" />
                                    </li>

                                </div>
                                <div class="divR" style="width: 360px;">

                                    <label class="label" >Total Amount</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtToAmount" ReadOnly="true" Style="text-align: right;" TabIndex="678" AutoComplete="off" />

                                </div>
                                
                                </asp:Panel>
                                <div class="divR">


                                    <li class="clear"></li>

                                    <li class="btnMargin">
                                        <asp:HiddenField ID="hfitemConversion" runat="server" />
                                        <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="22" OnClientClick="disablebutn(); ShowProgress();" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" CausesValidation="false" TabIndex="23" OnClick="btnClear_Click1" />
                                       
                                        <%-- <asp:Button ID="btnItemBlurTrigger" Text="text" runat="server" CausesValidation="false" Style="display: none;" OnClick="btnItemBlurTrigger_Click" />--%>
                                        <asp:Button ID="btnItem" runat="server" Text="Item" OnClick="btnItemBlurTrigger_Click" Style="display: none;" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                    </li>
                                </div>

                            </ul>
                        </div>

                        <div class="clear"></div>
                        <div id="divView" runat="server" style="display: none; height: 485px">



                            <ul class="contentul">
                                <div class="divL">


                                    <li>
                                        <label class="label">From Store</label>
                                        <asp:DropDownList runat="server" CssClass="dropdown" ID="ddlFromStoreSearch" TabIndex="24">
                                        </asp:DropDownList>


                                    </li>

                                    <li>
                                        <label class="label">From Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtFromDate" TabIndex="26" AutoComplete="off" />
                                        <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>

                                    </li>
                                    <br class="clear" />
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="28" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="29" />
                                    </li>
                                </div>
                                <div class="divR">

                                    <li>

                                        <label class="label">To Store</label>
                                        <asp:DropDownList runat="server" CssClass="dropdown" ID="ddlToStoreSearch" TabIndex="25" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </li>

                                    <li>
                                        <label class="label">To Date</label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtToDate" TabIndex="27" AutoComplete="off" />
                                        <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtToDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>

                                    </li>

                                </div>

                            </ul>
                            <br class="clear" />
                            <ul class="contentul">
                                <asp:GridView ID="gvItemConversion" runat="server" CssClass="grid" Width="95%"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_Indent,FK_StoreFrom,FK_StoreTo,Approved"
                                    OnSelectedIndexChanging="gvItemConversion_SelectedIndexChanging" OnRowDataBound="gvItemConversion_RowDataBound" OnRowDeleting="gvItemConversion_RowDeleting" OnRowCommand="gvItemConversion_RowCommand">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>

                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center"  />
                                        <asp:BoundField DataField="StoreFrom" HeaderText="Store From" />
                                        <asp:BoundField DataField="StoreTo" HeaderText="Store To" />
                                        <asp:BoundField DataField="FromAmount" HeaderText="From Amount" />
                                        <asp:BoundField DataField="ToAmount" HeaderText="To Amount" />

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%--<asp:ImageButton ID="ImageButtonConfirm" runat="server" CommandName="Approve" ToolTip="Approve" OnClientClick="ConfirmApprove()" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />--%>
                                          <asp:Button ID="btnApprove" runat="server" CssClass="button" TabIndex="35"  Text='<%#Eval("Approved")%>'  CommandName="Approve"  OnClientClick="ConfirmApprove()" />
                                                   </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                         
                                              <asp:ImageButton ID="ImageButton343" runat="server" CommandName="Print" ToolTip="Print"  ImageUrl="~/images/1430738477_report.png" Width="20px" Height="20px" />
                                             
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                <div class="Pager">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" OnClick="Page_Changed" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ul>
                        </div>
                        <div class="clear"></div>
                        
                        


                    </div>
                </div>
               <%-- <div class="loading" align="center" >
    Processing. Please wait.<br />
    <br />
      <asp:Image id="Image1" runat="server" style="margin-top:50px;"
           AlternateText="Image text"
           ImageAlign="Middle"
           ImageUrl="~/images/loader2.gif"/>           
</div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>

        <script src="js/jquery-1.10.2.js"></script>
        <script src="js/Custom.js"></script>
        <script src="js/toastr.js"></script>
        <script src="js/alertify.min.js"></script>
        <link href="css/alertify.core.css" rel="stylesheet" />
        <link href="css/alertify.default.css" rel="stylesheet" />
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
        <script src="js/Navigation.js"></script>
        <script type="text/javascript">

            function ClearAdd() {
                submit = 0;
                $("[id*='txt']").not("txtDate").val('');
                var d = new Date();
                $("#txtDate").val(d.format('dd/MM/yyyy'));
                $("#txtFromAmount").val('0');
                $("#txtToAmount").val('0');
                $("#txtRemark").val("");
                $("#txtFromDate,#txtToDate").val(d.format('dd/MM/yyyy'));
                $("[id*='ddl']").val('0');
                $("#hfitemConversion").val('');
                $("#hfFromStore").val('');
                ItemClear();
                $("#txtDate").focus();
                $("#DivPreviousPurchase").hide();
             
                //  return false;
            }




            var submit = 0;
            function disablebutn() {
                try {

                    window.setTimeout('delaysubmit()', 50);
                }
                catch (e) {
                    alert(e);
                }
            }

            function releasebtn() {


                $("#btnSave").prop('disabled', false);

            }
            function delaysubmit() {
                try {
                    if (submit == 1) {
                        return false;

                    }

                    $("#btnSave").prop('disabled', true);

                    window.setTimeout('releasebtn()', 3000);
                    submit = 1;
                }
                catch (e) {
                    alert(e);
                }
            }



            function CheckKeys() {
                alert(e.keyCode);
            }

            function ClearSearch() {
                $("#ddlFromStoreSearch ").val("0");
                $("#ddlToStoreSearch").val("0");
                var d = new Date();
                $("#txtFromDate,#txtToDate").val(d.format('dd/MM/yyyy'));

            }
            function ShowAddDiv() {

                $("#divAdd").show();
                $("#divView").hide();
            }
            function btnClick() {
                $("#btnHidden2").click();
                return false;
            }

            function buttonClick() {
                $("#btnHidden").click();
                return false;
            }

            function BlurItem() {

                $("#btnItemBlurTrigger").click();

            }

            function pageLoad() {

                var ctrContainer = "form";
                setKeynavigation(ctrContainer);


            }




            function blockPostOnTab() {
                if (e.keyCode == 9)
                    return false;

            }
            function ItemClear() {

                $("#txtBatch").val("");

                $("#txtitem").val("");
                $("#hfFK_Items").val("");
                $("#hf_ItemConv").val("");
                $("#hfFromStore").val("");

                $("#txtFree").val("0");

                $("#txtQuantity").val("0");
                $("#txtCurrentStock").val("0");
                $("#txtCurrentStockTo").val("0");
                $("#txtPurChaseRate").val("0");
                $("#hdfCurrentStock").val('');

                $("#txtPurchaseRate").val("");
                $("#txtitemConv").val("");

                $("#txtQuantityConv").val("0");
                $("#txtCardPrice").val("");
                $("#txtWholeSale").val('');
                $("#txtRetailPriceConv").val("");
                $("#txtMRP").val('');
                $("#txtBatchNo").val('');
                $("#txtBarCode").val('');
                $("#ddlSlno").val("0");
                $("#txtproductioncost").val('');
              

            }
            function SetStoreDetail() {
                var store = $("#ddlFromStore option:selected").val();

                $("#hfFromStore").val(store);
            }
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
                        var ItemName = $("#txtitemConv").val();
                        // $("#txtitem").val('');
                    }
                    //     if ($("#hfFromStore").val() == '') {
                    SetStoreDetail();

                    //  }
                    //if (($("#hfFromStore").val().length) > 0) {
                    if (p == 1) {

                        popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfFromStore").val() + '&itn=' + ItemName + '&ItemType=2', "Popup", "width=1015,height=650");
                        popup.focus();
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
            function ShowProgress() {
                if (Page_ClientValidate("submit")) {
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
            }
            function popUpStockSelect(p) {
                $("#hfType ").val(p);
                if (Page_ClientValidate("img") == true) {

                    var popup;
                    if (p == 1) {
                        var ItemName = $("#txtitem").val();
                        //   $("#txtitemConv").val('');
                    }
                    else {
                        var ItemName = $("#txtitemConv").val();
                        //   $("#txtitem").val('');
                    }

                    if (ItemName.length > 0) {
                        //   if ($("#hfFromStore").val() == '' ) {

                        SetStoreDetail();
                        //   }
                        if (($("#hfFromStore").val().length) > 0) {
                            if (p == 1) {
                                popup = window.open("StockItems.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&str=' + $("#hfFromStore").val() + '&itn=' + ItemName + '&ItemType=2', "Popup", "width=1000,height=800");
                                popup.focus();
                            }
                            else {

                                if ($("#hf_ItemConv").val() == '') {
                                    url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName;
                                    popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                                    ",statusbar=no,menubar=no,resizable=0,width=1015," +
                                    "height=650,left = 200,top = 100");
                                    popUpObj.focus();
                                }
                            }
                        }
                    }
                }

                return false;
            }






            function ConfirmApprove() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to Approve data?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }





        </script>


    </form>
</body>
</html>
