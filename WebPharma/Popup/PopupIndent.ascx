<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopupIndent.ascx.cs" Inherits="Popup_PopupIndent" %>
<style>

    .gridp th {
            width: auto;
            font-family:tahoma;
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
        background-color: #07928f;
        color: #FFF;
        width: 86px;
        height: 25px;
        text-align: center;
        border: none;
        cursor: pointer;

        float:inherit;
    }

    .popupdiv {
        float: left;
        /*padding-top: 10px;
        padding-left: 16px;*/
        width:95%

    }
    

    .divSeparation {
        width: 96%;
        background-color: #0D3B5F;
        margin: 5px 0px;
        height: 2px;
    }

    .lblHeader { width:100px; margin-top:0px;  padding:0px 0px 0px 0px;  }
       
  

    .cbxHeader { width:auto; height:10px;
        margin:0px 0px 0px 0px; float:left;
        padding: 0px 0px 0px 0px; position:relative;
         
    }
	.divContainer
	{
	width:97%;
	height:320px;
	overflow:auto;
	}
    .GridViewItemStyle
	{
	width:97%;
	height:320px;
	overflow:auto;
	}
	.popuplabel{ float:left; margin-left:0px;    font-family:tahoma; font-size:12px; font-weight:bold; padding-top:5px; padding-right:6px; padding-bottom:14px;  }
	
</style>
<script>

    function ClearPopu8Search() {
        $("#CommonPopup1_txtName").val('');
        return false;
    }


    function Check() {
        try {

            //$('[id^=CommonPopup1_gdvSearch_cbxGridSearch]').click(function () {
            $('[id^=CommonPopup1_gdvSearch_cbxGridSearch]').click(function () {


                $('[id^=CommonPopup1_gdvSearch_cbxGridSearch]').not(this).prop('checked', false);
                $(this).attr('checked', true);
                var id = $(this).attr('id');
                var lbl = id.replace('cbxGridSearch', 'lblGridName');
                var txt = $("#" + lbl).text();
                $("#CommonPopup1_popuplabel").text(txt);

                $("#CommonPopup1_hfColumn").val(txt);
            });
        }
        catch (e) {
            alert(e);
        }

        //if ($(this).is(':checked')) {
        //    $('[id^=CommonPopup1_gdvSearch_cbxGridSearch]').not(this).prop('checked', false);
        //}
        //});
    }

</script>
<div align="Center" style="border: 1px solid #88D2FF; height: 300px; width: 508px; padding-bottom: 0px; background-color: #d9e0da; border: 1px solid #12361D; padding-bottom: 50px; box-shadow: 10px 10px 32px #888; border-radius: 0px 0px 2px 2px;">
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <%-- <div id="divouterline" align="left">--%>


    <div style="padding-top: 10px; padding-left: 16px;width:95%">
        <asp:TextBox ID="txtRefno" runat="server" CssClass="textbox" Width="145" Style="display: none;"></asp:TextBox>

        <asp:Label ID="popuplabel" runat="server" Text="Name" CssClass="popuplabel"></asp:Label>
        <asp:TextBox ID="txtName" runat="server"   Width="200"></asp:TextBox>
        
        <asp:Button ID="btnSearch" runat="server" Text="Search" CausesValidation="false" CssClass="btnSearchPopup" OnClick="btnSearch_Click"   Style="margin-left:10px;" />
        <asp:Button ID="btnClearSearchPopup" runat="server" Text="Clear" CausesValidation="false"  CssClass="btnSearchPopup" OnClientClick="return ClearPopu8Search();" />
        <asp:Label ID="lblErrorMsg" runat="server" Text="Invalid Input " ForeColor="Red"></asp:Label>
            
    </div>
     
 <div class="clear"></div>
      <%-- <div class="clear"></div>
    <br />--%>
    <div class="divContainer">
    <asp:GridView ID="gdvSearch" runat="server" CssClass="gridp" Style="width: 100%;" Font-Size="10pt" font-family="Arial, Helvetica, sans-serif"
        BackColor="White" BorderColor="Black"  BorderWidth="0px"  CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="10"  OnPageIndexChanging="gdvSearch_PageIndexChanging" OnRowCreated="gdvSearch_RowCreated" OnSelectedIndexChanging="gdvSearch_SelectedIndexChanging" OnPreRender="gdvSearch_PreRender" EmptyDataText="No Data Found!" AllowPaging="True">
        <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
        <Columns>

            <asp:BoundField DataField="SLNO" HeaderText="SlNo" ItemStyle-Width="95px" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:BoundField>

            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="true">

                <ItemTemplate>
                   
                     <%--<ItemStyle ItemStyle-HorizontalAlign="left"></ItemStyle>--%>
                    <asp:HiddenField ID="hdfIDValue" Value='<%# Eval("ID") %>' runat="server"></asp:HiddenField>
                    <asp:LinkButton ID="lbtnName" runat="server" ItemStyle-HorizontalAlign="left"    CausesValidation="false" CommandName="Select" Text='<%# Eval("Name") %>' OnClientClick="return true;"></asp:LinkButton>
                </ItemTemplate>

               
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle HorizontalAlign="Center" Width="100%" VerticalAlign="Middle"  BackColor="#ffffff"  />
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="#3A7D78" Font-Bold="True" ForeColor="White"  BorderColor="Black" />
        <PagerStyle BackColor="#3A7D78" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    </div>
    <br />
    <div style="width: 450px; margin-left: 0px;">


        <ul class="contentul" style="width: 800px; height: auto; padding: 0px; overflow-y: auto;">
            <%--overflow: auto; overflow-y:auto; overflow-x:auto;--%>
            <asp:HiddenField ID="hfColumn" runat="server" />

            <li class="clear"></li>
            <br />
        </ul>
    </div>
    <div class="Pager" style="float: left; margin: -50px 0px 0px 220px;">
        <asp:Repeater ID="rptPager" runat="server">
            <ItemTemplate>
                <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CausesValidation="false" CommandArgument='<%# Eval("Value") %>' OnClick="Page_Changed" OnClientClick="Check();" Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <li class="clear"></li>

    <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</div>