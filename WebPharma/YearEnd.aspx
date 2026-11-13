<%--<%@ Page Title="::Year End::" MasterPageFile="~/MasterPage.master" Language="C#" AutoEventWireup="true" CodeFile="YearEnd.aspx.cs" Inherits="YearEnd1" %>--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YearEnd.aspx.cs" Inherits="YearEnd1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

    
<head id="Head1" runat="server">
     <link href="./css/main.css" rel="stylesheet" />
    <link href="./css/bootstrap.min.css" rel="stylesheet" /> 
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
      <link href="./css/general.css" rel="stylesheet" />
    
        <style>
.custom-container {
  max-width: 900px;
  margin: 40px auto;
  border: 1px solid #ccc;
  padding: 25px;
  border-radius: 8px;
  background: #fff;
}
.YEnd {
  font-weight: bold;
  font-size: 18px;
  margin-bottom: 20px;
}
.panelG {
  margin-top: 20px;
  padding: 10px;
  background-color: #d4edda;
  color: #155724;
  border-radius: 5px;
}
#chkStockUpdate {
    margin-right: 10px;
}
#divAdd {
    display: block !important;  
    max-width: 900px;            
    margin-left: 35px;         
    float: none !important;      
}
.form-group.row {
    display: flex;
    align-items: center;
}

.form-group .col-sm-3 {
    white-space: nowrap;
}

    </style>
    <script type="text/javascript">
        var submit = 0;
        function RedirectToHome() {

            location.reload();
        }
        //function disablebutn() {


        //    try {

        //        window.setTimeout('delaysubmit()', 50);
        //    }
        //    catch (e) {
        //        alert(e);
        //    }
        //}

        function releasebtn() {


            $("#btnSubmit").prop('disabled', false);

        }
        function delaysubmit() {
            try {
                if (submit == 1) {
                    return false;

                }

                $("#btnSubmit").prop('disabled', true);

                window.setTimeout('releasebtn()', 3000);
                submit = 1;
            }
            catch (e) {
                alert(e);
            }
        }
        function ClientValidate(source, arguments) {
            try {
                var date = new Date().format('dd/MM/yyyy');
                var arr = date.split('/');




                var currentDate = new Date(arr[2], arr[1], arr[0]);


                var arr2 = arguments.Value.split('/');

                var txtDate = new Date(arr2[2], arr2[1], arr2[0]);

                if (txtDate >= currentDate) {

                    arguments.IsValid = false;
                }
                else {

                    arguments.IsValid = true;
                }

            }
            catch (e) {
                alert(e);
            }

        }

        function ClearAdd() {
            submit = 0;
            var toDate = new Date();
            var year = new Date().getFullYear();
            var lastyear = new Date().getFullYear() - 1;
            // alert(year);
            // alert(toDate.format("dd/MM/yyyy"));
            var ToYear = "31/03/" + year;
            var FromYear = "01/04/" + lastyear;
            var currentdate = toDate.format("dd/MM/yyyy");
            //

            $("#<%#txtCurrentDate.ClientID%>").val(currentdate);
            $("#<%#txtToDate.ClientID%>").val(ToYear);
            $("#<%#txtFromDate.ClientID%>").val(FromYear);

            //$("#ContentPlaceHolder1_txtCurrentDate").val(currentdate);
            //$("#ContentPlaceHolder1_txtToDate").val(ToYear);
            //$("#ContentPlaceHolder1_txtFromDate").val(FromYear);


            $("#<%#hfColorId.ClientID%>").val('0');
            ClearSearch();

        }
        function ClearSearch() {
            $("#<%#txtSearchShortName.ClientID%>").val('');
            $("#<%#txtSearchName.ClientID%>").val('');
            $("#<%#txtSearchShortName.ClientID%>").focus();
        }
        function ShowAddDiv() {
            //alert('g');
            //$("#divR").show();
            //ClearAdd();
            // $("#//<//%//#divAdd.ClientID%>").show();
            //  $("#<//%#divView.ClientID%>").hide();

        }
        function ProcessClick() {


            // $("#ContentPlaceHolder1_btnSubmit").click();
            document.getElementById("btnSubmit").click();
            return false;

        }
    </script>
    </head>


    <body onkeydown="javascript:EnterKeyFilter();">
        
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <div class="container-fluid">
                    <div class="custom-container">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 top-header">
                        <div class="col-lg-10 col-md-4 col-xs-12 col-sm-12 " style="float: left;">

                            <div class="heading">
                                <asp:Label ID="lblPageName" runat="server" Style="font-size: 18px"></asp:Label>
                            </div>
                        </div>

                        <div class="col-lg-1 col-md-1 col-xs-4 col-sm-12 " style="float: left; display: none">
                            <div class="btn1">
                                <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/add1.png" CssClass="AddButton" runat="server" TabIndex="5" />

                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-xs-4 col-sm-12 " style="float: left; display: none">
                            <div class="btn1">
                                <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CssClass="ViewButton" runat="server" TabIndex="6" />
                            </div>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2forYrEnd">
                        <div id="divAdd" class="PageContentWidth" runat="server" style="display: none;">
                         
                            <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12">
                                <div class="YEnd" >Year End Process</div>
                            </div>
                          
                            <div class="col-lg-12 col-md-12 col-xs-12 col-sm-12">
                                 <div class="row">
                                     <!-- LEFT COLUMN: DivHead (Form) -->
                                     <div class="col-md-6 left-column">
                              <div class="DivHead">

                                    <div class="form-group row">
                                        <asp:Label ID="Label99" runat="server" CssClass="col-sm-3 col-form-label" Text="Outlet"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlOutlet" runat="server" CssClass="form-control" TabIndex="1"></asp:DropDownList>
                                          <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select an Outlet" ControlToValidate="ddlOutlet" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                            <cc:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                            </cc:ValidatorCalloutExtender>--%>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label2" runat="server" Text="From Date" CssClass="col-sm-3 col-form-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                            <cc:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy">
                                            </cc:CalendarExtender>

                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                            </cc:ValidatorCalloutExtender>

                                        </div>
                                    </div>
                                    <div id="divPassDate" style="display: none">
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtCurrentDate" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group row">
                                        <asp:Label ID="Label3" runat="server" Text="To Date" CssClass="col-sm-3 col-form-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TabIndex="2"></asp:TextBox>
                                            <asp:CompareValidator ID="cmpCurrentDate" runat="server" ErrorMessage="Date cannot be greater than current date" ControlToCompare="txtCurrentDate" ControlToValidate="txtToDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                            <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="cmpCurrentDate">
                                            </cc:ValidatorCalloutExtender>
                                            <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtToDate" Format="dd/MM/yyyy">
                                            </cc:CalendarExtender>
                                        </div>
                                    </div>
                                     <div class="form-group row">
                                        <asp:Label ID="Label4" runat="server" CssClass="col-sm-3 col-form-label" Text=""></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:CheckBox ID="chkStockUpdate" runat="server" Text="Opening Stock As Zero" ToolTip="Previous Year Closing Stock Updated As Current year Opening Stock If Unchecked." />
                                           
                                            <%--<asp:DropDownList ID="ddlStockUpdate" runat="server" CssClass="Drop-control" TabIndex="2">
                                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                               <asp:ListItem Value="1" Text="Previous Year Closing Stock As Current year Opening Stock"></asp:ListItem>
                                               <asp:ListItem Value="2" Text="Opening Stock As Zero"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                            
                                        </div>
                      
                                    </div>
                                    <div class="form-group row">
                                        <asp:Label ID="Label1" runat="server" Text="" CssClass="col-sm-3 col-form-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-info" Text="Process" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="3" />
                                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-info" OnClick="btnClear_Click" Text="Clear" OnClientClick="ClearAdd(); ShowAddDiv() " TabIndex="4" />
                                            <asp:HiddenField ID="hfColorId" runat="server" />
                                            <asp:HiddenField ID="hfClient" runat="server" />
                                            <asp:HiddenField ID="hfUser" runat="server" />
                                            <asp:HiddenField ID="hftxtToDate" runat="server" />
                                            <asp:HiddenField ID="hftxtFromDate" runat="server" />
                                        </div>
                                    </div>
                                  </div>
                                    </div>
                                               <div class="col-md-6 right-column">

                                   
                               
<div class="col-lg-6 col-md-6 col-xs-12 col-sm-12 DivHead" style="display:none" id="DivRH" runat="server">
                                    <table style="width: 100%">

                                       
                                        <tr>
                                            <td style="width: 30px; height: 30px;">
                                                <div runat="server" id="loop1" class="loader"></div>
                                            </td>
                                            <td>
                                                <div runat="server" id="Div1" style="">Purchase</div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 30px; height: 30px;">
                                                <div runat="server" id="loop2" class="loader "></div>
                                            </td>
                                            <td>
                                                <div id="Div2" style="display: inline-block">Purchase Return</div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 30px; height: 30px;">
                                                <div runat="server" id="loop3" class="loader "></div>
                                            </td>
                                            <td>
                                                <div id="Div3" style="display: inline-block">Sales</div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 30px; height: 30px;">
                                                <div runat="server" id="loop4" class="loader "></div>
                                            </td>
                                            <td>
                                                <div id="Div4" style="display: inline-block">Sales Return</div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 30px; height: 30px;">
                                                <div runat="server" id="loop5" class="loader "></div>
                                            </td>
                                            <td>
                                                <div id="Div5" style="display: inline-block">Indent</div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 30px; height: 30px;"></td>
                                            <td>

                                                <asp:Button ID="Btnfinish" runat="server" CssClass="buttonFinish" Text="Finish" OnClientClick="RedirectToHome()" TabIndex="4" Style="display: none" />
                                                <asp:Button ID="Btnclose" runat="server" CssClass="buttonClose" Text="Close"  OnClick="Btnclose_Click" TabIndex="4" Style="display: none" />
                                            </td>


                                        </tr>
                                    </table>

                                </div>
                                </div>
                              </div>
                                
                                  <div class="row error-container">
                                    <div class="col-md-12">
                                 <div>
                                        <div id="pan1" runat="server" class="panelG">
                                            Year End Process Completed Successfully
                                        </div>
                                    </div>

                                    <div>
                                        <div id="pan2" runat="server" class="panelR">
                                            Year End Process Failed!
                                        </div>
                                    </div>
                                    <div>
                                        <div id="pan3" runat="server" class="panelR">
                                            Financial Year Already Exist
                                        </div>
                                        <div id="pan4" runat="server" class="panelR">
                                            Transaction Already Exists For This Period 
                                        </div>
                                    </div>   
                                        </div>
                                </div>
                            



                       
                        <div id="divView" class="PageContentWidth" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="col-sm-3 col-form-label" Text="Short Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchShortName" CssClass="form-control" runat="server" MaxLength="10" TabIndex="7"></asp:TextBox>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="col-sm-3 col-form-label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchName" CssClass="form-control" runat="server" MaxLength="250" TabIndex="8"></asp:TextBox>
                                    </li>
                                    <li class="btnMargin">
                                        <%-- <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info" TabIndex="9" Text="Search" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="btn btn-info" Text="Clear" TabIndex="10" OnClientClick="ClearSearch(); return false;" />--%>
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />

                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                            </ul>
                        </div>

                    </div>
                </div>
                       </div>
            </ContentTemplate>
        </asp:UpdatePanel>

</form>
    </body>

