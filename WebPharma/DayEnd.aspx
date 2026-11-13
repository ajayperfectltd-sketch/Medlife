<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DayEnd.aspx.cs" Inherits="DayClose1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title ::DayClose::> </title>
       
         <link href="css/main.css" rel="stylesheet" />

    <link href="css/toastr.css" rel="stylesheet" />
    
  
    <style>
      .hiddencol
    {
        display:none;
    }
      .auto-style1 {
          height: 22px;
      }
      .auto-style2 {
          
      }
      .auto-style3 {
          height: 32px;
          width: 130px;
      }
      .auto-style4 {
          height: 26px;
      }
      .auto-style5 {
          
      }
      .auto-style6 {
          height: 26px;
          width: 130px;
      }
       .style7
        {
            height: 114px;
        }

        .style8
        {
            height: 22px;
        }

        .style9
        {
            width: 100%;
            height: 436px;
        }

        .style10
        {
            width: 100%;
            height: 434px;
        }

        .style11
        {
            width: 100%;
            height: 435px;
        }

        .style12
        {
            width: 100%;
            height: 434px;
        }

        .auto-style1
        {
            width: 666px;
        }

        .auto-style4
        {
            height: 19px;
        }
        .auto-style5 {
            height: 431px;
        }
        .auto-style6 {
            height: 32px;
        }
  </style>
</head>
<body>
    
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
    <div>
   <div class="btn_bg5"   >
                 <div  class="heading2">
                    <div class="user">DayClose</div>

             <table style="text-align: center; height: 100%; width: 100%;">
                        <tr>
                            <td align="middle" align="center">
                                <table style="width: 985px; height: 595px">
                                    <tr style="width: 100%; height: 100%">
                                        <td style="background-image: url('../../Images/DAYEND.jpg'); background-repeat: no-repeat; width: 100%; height: 100%"
                                            align="left" valign="top">
                                            <table class="style3">
                                              
                                                 
                                                 <tr>
                                                    <td class="auto-style2">&nbsp;
                                                    </td>
                                                    <td class="auto-style1">&nbsp;
                                                    </td>
                                                      <tr>
                                                    <td class="auto-style2">&nbsp;
                                                    </td>
                                                    <td class="auto-style1">&nbsp;
                                                    </td>
                                                </tr>

                                                </tr>
                                                <tr>
                                                    <td class="auto-style2">&nbsp;
                                            
                                                                      

                                                    </td>
                                                    <td class="auto-style1">&nbsp;
                                                         <asp:Label ID="lblcompanyname" runat="server"  Font-Size="Medium" Font-Bold="False" Font-Italic="False" ForeColor="#12B4B0"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="trMessage" runat="server">
                                                    <td class="auto-style2">&nbsp;
                                                    </td>
                                                    <td style="border-width: 1px; border-color: #000000; border-top-style: solid; border-right-style: solid; border-left-style: solid;" class="auto-style1">

                                                        <div id="divaccountlinking" style="height: 436px" runat="server" >
                                                            <table align="left" class="style3">
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <table id="Table1" class="style2">
                                                                          
                                                                            <tr>
                                                                                <td class="auto-style4">;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">;<asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="#CC0000" Height="18PX"
                                                                                    Text="Warning :" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#CC0000" Height="18px"
                                                                                        Text="Account linking is Enabled, Day End Process Not Done " Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style8"></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>



                                                        <div id="Divdayend" style="height: 436px" runat="server" >
                                                            <table align="left" class="style3">
                                                                <tr>
                                                                    <td align="center" valign="middle" class="auto-style5">
                                                                        <table id="tblMessage" class="style2">
                                                                            <tr>
                                                                                <td align="left">;<asp:Label ID="Label11" runat="server" ForeColor="Black" Text="    This Process Will Lock All Previous Transactions" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Label ID="Label12" runat="server" Text="Changes The Business Dates To Current Date" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="auto-style4">;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">;<asp:Label ID="Label13" runat="server" Font-Bold="True" ForeColor="#CC0000" Height="12px"
                                                                                    Text="Warning :" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="auto-style1">
                                                                                    <asp:Label ID="Label14" runat="server" Font-Bold="True" ForeColor="#CC0000" Height="18px"
                                                                                        Text="Please Make Sure That Day Book For The Previous day Is Taken." Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style8"></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="divDateDetails" style="height: 436px;width:100%"  runat="server">
                                                            <table class="style9" style="width:100%">
                                                                <tr>
                                                                    <td align="left">
                                                                        <table id="Table3" style="width:100%" >
               
                                                                            <tr>
  <td>
                                                                                    <asp:Label ID="Label10" runat="server" Text="Store" Width="90px" ForeColor="#12B4B0"></asp:Label>
                                                                                   <asp:DropDownList ID="ddlBranch" CssClass="dropdownMandatory" runat="server" Width="270px"  TabIndex="1" onchange="griddetails();" EnableViewState="true" ></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfddlBranch" Style="float: right;" runat="server" ErrorMessage="Required Store" Display="None" Font-Bold="True" Font-Size="Large" SetFocusOnError="True" ToolTip="Required field" ValidationGroup="submit" InitialValue="0" ControlToValidate="ddlBranch">*</asp:RequiredFieldValidator>
                                                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rfddlBranch">
                                                                                    </cc:ValidatorCalloutExtender>
                                                                                </td>
                                                                              
                                                                            </tr>
                                                                            <tr>
                                                                             
                                                                     
                                                                           <td class="auto-style6">
                                                                                    <asp:Label ID="Label15" runat="server" Height="16px" Text="Date" Width="91px" ForeColor="#12B4B0"></asp:Label>
                                                                                      <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtCloseBalanceDate"  AutoPostBack="true" onchange="griddetails();"  TabIndex="5" AutoComplete="off"  MaxLength="9" />
                                                                                      <cc:CalendarExtender ID="txtCloseBalanceDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtCloseBalanceDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                                                                                <asp:RequiredFieldValidator ID="reqvalCloseBalanceDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtCloseBalanceDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalCloseBalanceDate">
                                                                                                </cc:ValidatorCalloutExtender>
                                                                                </td>
                                                                             
                                                                     
                                                                                   
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="divbranch" style="height: 436px" runat="server">
                                                            <table class="style9">
                                                                <tr>
                                                                    <td align="left">
                                                                        <table id="tblOpcashBalance">
               
                                                                            <tr>
                                                                                <td> 
                                                                           <asp:GridView ID="gvTransactionDetails" style="overflow-x :scroll" runat="server" CssClass="grid"
                                                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                                                            BackColor="white" BorderColor="white" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" GridLines="Vertical" >
                                                                            <AlternatingRowStyle BackColor="#CCE0EB" />
                                                                            <Columns>
                                                                          
                                                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                                                <asp:BoundField DataField="NetAmount" HeaderText="NetAmount" />
                                                                                <asp:BoundField DataField="Liability" HeaderText="Liability" />
                                                                            </Columns>
                                                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <FooterStyle BackColor="#CCCCCC" />
                                                                            <HeaderStyle  CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True"  ForeColor="White" Height="30px" />
                                                                            <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                                                        </asp:GridView>
                                                                                </td>
                                                                               <td>
                                                                      <asp:GridView ID="grdIndent"  runat="server" CssClass="grid"
                                                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                                                            BackColor="white" BorderColor="white" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" GridLines="Vertical" >
                                                                            <AlternatingRowStyle BackColor="#CCE0EB" />
                                                                            <Columns>
                                                                                <asp:BoundField  HeaderText="Name"  DataField="Name" />
                                                                                <asp:BoundField DataField="CurrentLiability" HeaderText="CurrentLiability" />
                                                                                  <asp:BoundField DataField="PreviousLiability" HeaderText="PreviousLiabilty" />
                                                                            </Columns>
                                                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <FooterStyle BackColor="#CCCCCC" />
                                                                            <HeaderStyle CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                                                            <PagerStyle CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                                                        </asp:GridView>
                                                                        </td>
                                                                                   
                                                                            </tr>
                                                                               <tr>
                                                                                 <td>
                                                                                    <asp:Label ID="Label8" runat="server" Height="16px" Text="Liability" Width="91px" Font-Size="Medium" ForeColor="#12B4B0"></asp:Label>
                                                                               
                                                                                
                                                                                   <%-- <table align="left" cellpadding="0" cellspacing="0">
                                                                                        <tr>--%>
                                                                                             
                                                                                                <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtLiabilty"  TabIndex="5" AutoComplete="off" Width="80px" MaxLength="9" />

                                                                                           
                                                                                      <%--  </tr>
                                                                                    </table>--%>
                                                                                </td>
                                                                            </tr>
                                                                                   <tr>
                                                                                 <td>
                                                                                    <asp:Label ID="Label9" runat="server" Height="16px" Text="Stock Values" Width="91px" Font-Size="Medium" ForeColor="#12B4B0"></asp:Label>
                                                                              
                                                                               
                                                                                  
                                                                                            
                                                                                                <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtStock"  TabIndex="5" AutoComplete="off" Width="80px" MaxLength="9" />
                                                                                             
                                                                                                
                                                                                            </td>
                                                                                      
                                                                             
                                                                            </tr>
                                                                            <tr>
                                                                                 <td>
                                                                                    <asp:Label ID="lblDifference" runat="server"  Height="16px"  Width="91px"></asp:Label>
                                                                                     </td>
                                                                            </tr>
                                                                           

                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                         <div id="divCashdetails" style="height: 436px" runat="server">
                                              <table class="style9">
                                             <tr>
                                              <td align="center">
                                                                        <table id="Table2">
  
                                                                                  <tr>
                                                                                <td>
                                                                                   
                                                                                </td>
                                                                            </tr>
                                                                             <tr>
                                                                                 <td  align="Left">
                                                                                    <asp:Label ID="Label1"  ForeColor="#12B4B0" runat="server" Height="16px" Text="Opening cash balance" Width="181px" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <table align="Left" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                             <td>
                                                                                                <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtOpenCashbalance" TabIndex="5" AutoComplete="off" Width="88%" MaxLength="9" />
                                                                                                
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>

                                                                                 <td align="Left">
                                                                                    <asp:Label ID="Label3" runat="server"  ForeColor="#12B4B0" Text="Total Receipt" Width="208px" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                                <td>

                                                                               <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtTotalRecepit" TabIndex="5" AutoComplete="off" Width="88%" MaxLength="9" />
                                                                                </td>

                                                                            </tr>
                                                                               <tr>

                                                                                 <td align="Left">
                                                                                    <asp:Label ID="Label6"  ForeColor="#12B4B0" runat="server" Text="Total Payment" Width="153px" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                                <td>

                                                                               <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtTotPayment" TabIndex="5" AutoComplete="off" Width="88%" MaxLength="9" />
                                                                                </td>

                                                                            </tr>
                                                                             <tr>

                                                                                 <td align="Left">
                                                                                    <asp:Label ID="Label7"  ForeColor="#12B4B0" runat="server" Text="Closing Balance" Width="146px" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                               <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtClosingbalance" TabIndex="5" AutoComplete="off" Width="88%" MaxLength="9" />
                                                                                </td>

                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                              
                                                              </div>
                                                      <%--  <div id="div1" runat="server" style="height: 436px; overflow: auto; width: 603px;" visible="false"  >
                                                            <table id="tblAttandanceMarking">
                                                                <tr>
                                                                    <td class="HeaderStyle">
                                                                        <asp:Label ID="lblattendancemarking" runat="server" Height="18px" Text="Attandance Marking"
                                                                            Width="240px" Font-Size="Medium"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                     
                                                                        <asp:GridView ID="gdvAttendance" runat="server" CssClass="grid"
                                                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                                                            BackColor="white" BorderColor="white" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" GridLines="Vertical" OnRowDataBound="gdvAttendance_RowDataBound">
                                                                            <AlternatingRowStyle BackColor="#CCE0EB" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="True" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkMark" runat="server" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="UserCode" HeaderText="UserCode" />
                                                                                <asp:BoundField DataField="UserName" HeaderText="UserName" />
                                                                                <asp:BoundField DataField="ID_User" HeaderText="ID_User" />
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
                                                                    </td>
                                                                </tr>
                                                               
                                                            </table>
                                                        </div>--%>
                                                      <%--  <div id="divgrids" runat="server" style="height: 436px; overflow: auto;" visible="false">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style11">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <asp:GridView ID="gdvMessage" runat="server" Font-Size="12pt" OnRowDataBound="gdvMessage_RowDataBound"
                                                                            SkinID="gridview">
                                                                            <RowStyle Font-Size="10pt" />
                                                                            <Columns>
                                                                                <asp:ImageField DataImageUrlField="Progress">
                                                                                </asp:ImageField>
                                                                            </Columns>
                                                                            <HeaderStyle Font-Size="12pt" ForeColor="Black" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>--%>
                                                        <div id="divdayfinished" runat="server" style="height: 436px" visible="True">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style12">
                                                                <tr>
                                                                    <td align="center">
                                                                      
                                                                        <asp:Label ID="lblFinishMessage" runat="server" Font-Italic="True" Font-Size="20pt"
                                                                            ForeColor="Red" Text="DAY END PROCESS FINISHED"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        
                                                  


                                                    </td>
                                                </tr>

                                                <tr>

                                                    <td align="right" class="auto-style2">
                                                        <asp:Button ID="btnProcess" runat="server" Style="display: none"
                                                            Text="Process" />
                                                        
                                                    </td>
                                                    <td align="right" style="border: 1px solid #000000;" class="auto-style1">


                                                        <asp:Button ID="btnScriptFill" runat="server" OnClick="btnScriptFill_Click" Style="display: none"
                                                            Text="Process" />
                                                        <asp:Button ID="btnSave" runat="server" BorderColor="#000000" OnClick="btnSave_Click" ValidationGroup="Submit"
                                                           OnClientClick=" ShowProgress();" SkinID="button" TabIndex="22" Text="Add" style="margin-left: 0px" Font-Size="Medium" ForeColor="#12B4B0"
                                                        />
                                                           <asp:Button ID="btnClose" runat="server" BorderColor="#000000"  OnClick="btnClose_Click" 
                                                           SkinID="button" TabIndex="22" Text="Close"
                                                            UseSubmitBehavior="False" Font-Size="Medium" ForeColor="#12B4B0" />
                                                        

                                                    </td>

                                                </tr>
                                                <tr id="tr2" runat="server">
                                                    <td class="auto-style2">&nbsp;
                                                    </td>
                                                    <td class="auto-style1">
                                                        <asp:BulletedList ID="bltErrorList" runat="server" Font-Bold="true" ForeColor="#FF3300" Font-Size="Medium">
                                                        </asp:BulletedList>
                                                    </td>
                                                </tr>
                                                <caption>
                                                </caption>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
        <asp:HiddenField ID="hdfDayend" runat="server" />
        <asp:HiddenField ID="hfClient" runat="server" />
        <asp:HiddenField ID="hfUser" runat="server" />
        <asp:HiddenField ID="hfIsAdd" runat="server" />
        <asp:HiddenField ID="hfIsUpdate" runat="server" />
        <asp:HiddenField ID="hfIsDelete" runat="server" />
                     </div>
                </div>
    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
    </form>
</body>
</html>
