<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonPopUpGridSelection.ascx.cs" Inherits="Popup_CommonPopUpGridSelection" %>

<div align="center" style="border: 1px solid #88D2FF; height: 250px; width: 508px;padding-bottom: 0px; background-color: #d9e0da; border: 1px solid #12361D; padding-bottom: 50px; box-shadow: 10px 10px 32px #888; border-radius: 0px 0px 2px 2px;">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <%-- <div id="divouterline" align="left">--%>


    <div  class="popupdiv">
        <asp:TextBox ID="txtRefno" runat="server" CssClass="textbox" Width="145" Style="display: none;"></asp:TextBox>
        
    </div>
    <div class="clear"></div>
    <br />
    <div class="divContainer">
    <asp:GridView ID="gdvSearch" runat="server" CssClass="gridp" Style="width: 100%; margin-left: 0px;"
        AutoGenerateColumns="False"  Font-Size="10" font-family="Arial, Helvetica, sans-serif"
        BackColor="White" BorderColor="Black" BorderStyle="Solid" BorderWidth="0px"  CellPadding="3" OnSelectedIndexChanging="gdvSearch_SelectedIndexChanging" OnPageIndexChanging="gdvSearch_PageIndexChanging" ForeColor="Black" GridLines="Vertical" PageSize="5"    EmptyDataText="No Data Found!" AllowPaging="True">
        <AlternatingRowStyle BackColor="#E5EFE4" />
        <Columns>

            <asp:TemplateField HeaderText="SlNo" HeaderStyle-HorizontalAlign="Center">
                 <ItemTemplate>
               
                     <asp:Label ID="lblSlno" runat="server" Text=" <%#Container.DataItemIndex+1 %>"></asp:Label>
                    </ItemTemplate>
                <ItemStyle HorizontalAlign="center"></ItemStyle>
                </asp:TemplateField>

            <asp:TemplateField HeaderText="Tax Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true">

                <ItemTemplate>
                    <asp:HiddenField ID="hdfIDValue" Value='<%# Eval("ID_TaxType") %>' runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfTaxInterState" Value='<%# Eval("TaxInterState") %>' runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hfTaxIntraState" Value='<%# Eval("TaxIntraState") %>' runat="server"></asp:HiddenField>
                    <asp:Label ID="lbtnName" runat="server" CausesValidation="false" CommandName="Select" Text='<%# Eval("TaxType") %>' OnClientClick="return true;"></asp:Label>
                </ItemTemplate>

                <ItemStyle Wrap="True"></ItemStyle>
            </asp:TemplateField>

                   <asp:TemplateField HeaderText="Percentage"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:TextBox ReadOnly="true" runat="server" ID="txtPurchaseRategv" onBlur="OnItemsPurchaseRateChange(id);" onchange="CalculateTotalAmount();" onkeypress="CheckNumWithDecimal(event);" AutoComplete="off" CssClass="textbox" Style="width: 100px; text-align: right;"
                                                            Text='<%# Eval("TaxPercentage") %>' />
                                                    <%--    <asp:RequiredFieldValidator ID="rqvalPurchaseRate" runat="server" ErrorMessage="Enter Purchase Rate"  Enabled="false"
                                                            ControlToValidate="txtPurchaseRategv"  Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="rqvalPurchaseRate">
                                                        </cc:ValidatorCalloutExtender>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:TextBox ReadOnly="true" runat="server" ID="txtAmount"   AutoComplete="off" CssClass="textbox" Style="width: 100px; text-align: right;"
                                                            Text='<%# Eval("TaxAmount") %>' />
                                                    <%--    <asp:RequiredFieldValidator ID="rqvalPurchaseRate" runat="server" ErrorMessage="Enter Purchase Rate"  Enabled="false"
                                                            ControlToValidate="txtPurchaseRategv"  Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="rqvalPurchaseRate">
                                                        </cc:ValidatorCalloutExtender>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle HorizontalAlign="Center" Width="100%" VerticalAlign="Middle"  BackColor="#ffffff"  />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="#07928f" Font-Bold="True" ForeColor="White" Height="25px" BorderColor="Black" />
        <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    </div>
    <br />
    <div style="width: 450px; margin-left: 0px;">
        <asp:Button ID="btnSumbit" OnClick="btnSumbit_Click"  Visible="false" CssClass="button" Text="Save" runat="server" />
         <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" CausesValidation="false"  CssClass="button" OnClientClick="return ClearPopu8SearchReason();" />

        <ul class="contentul" style="width: 800px; height: auto; padding: 0px; overflow-y: auto;">
            
            <asp:HiddenField ID="hfColumn" runat="server" />

            <li class="clear"></li>
            <br />
        </ul>
    </div>
 
</div>

