<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Licensing.aspx.cs" Inherits="Presentation_Licensing_Licensing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Medlife-Login Page</title>
    <link type="image/ico" rel="shortcut icon" href="/SCORE/Images/FavIcon/favicon.ico" />
    <link type="image/x-icon" rel="shortcut icon" href="/SCORE/Images/FavIcon/favicon.ico" />
    <link href="css/perfect.css" rel="stylesheet" />
    <link href="css/Licensing.css" rel="stylesheet" />
    <script  type="text/javascript" src="js/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
                $("#btnLogin").click(function(){                    
                        window.location.replace("Default.aspx");
                    });                                    
                $("#btnActivateNow").click(function(){ 
                        $("#ActivationPopup").fadeIn(1000);
                        positionPopup();
                        $(window).bind('resize',positionPopup);
                });
                $("#close").click(function(){
	                    $("#ActivationPopup").fadeOut(500);
                });
            });                
             function positionPopup(){
                  if(!$("#ActivationPopup").is(':visible')){
                    return;
                  }                  
                  $("#ActivationPopup").css({
                      left: $('#divErrorBox').position().left,
                      top: $('#divErrorBox').position().top,
                      position:'absolute'
                  });
             }            
             function Close()
             {
                $("#ActivationPopup").fadeOut(500);
             }
             function ActivateClick()
             {
                var file=$('#FileUpload').val();        
                if(file!='')
                {
                    var extension = file.substr( (file.lastIndexOf('.') +1) );
                    if(extension=="License")
                    {
                        return true;
                    }        
                    else
                    {
                        alert("Please select a valid License file");
                        return false;
                    }    
                }               
                else
                {
                    alert("Please select License file");
                    return false;
                }        
                return true;
            }                       
    </script>

    <style type="text/css">
        .home
        {
            position: absolute;
            top: 90%;
            left: 88%;
            color: #007CB9;
        }
        .home:hover
        {
            color: #008ACC;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnActivate" />
        </Triggers>
        <ContentTemplate>
            <div id="container">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%">
                    <tr>
                        <td height="420" style="background-image: url(/SCORE/Images/Login/logo-left.png);
                            background-repeat: no-repeat;" width="256">
                        </td>
                        <td style="background-image: url(/SCORE/Images/Login/expand-tile.jpg)" valign="top">
                            <table id="bg-log" border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                                <tr>
                                    <td height="417" valign="top" align="center" style="position: relative">
                                        <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 629px;
                                            height: 330px;">
                                            <tr>
                                                <td style="height: 20px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <div id="divErrorBox" class="successBox" runat="server">
                                                        <table style="margin: 10px; width:520px" bgcolor="#CCEEFF">
                                                            <tr>
                                                                <td style="background-color: #FF5024;height:25px; vertical-align: middle;" align="center">
                                                                    <span style="color:White;font-weight:bold">Product License Error !</span>     
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: middle; height: 40px" align="center">
                                                                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="#B92500" Text="Error" Font-Names="Tahoma"
                                                                        Font-Size="9pt" Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: middle; height: 40px">
                                                                    <div id="btnActivateNow" runat="server" class="ActivateNow" style="border: medium solid #007CB9;
                                                                        cursor: pointer; text-align: center; width: 127px; background-color: #008ACC;">
                                                                        Update License
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: middle;" align="center">
                                                                    <img src="images/sep.jpg" alt="Seperator" style="width: 100%; height: 12px;" />
                                                                    <span id="Span1" style="color: #2094BD; font-family: Tahoma; font-size: 10pt;">Or Contact
                                                                        the Software vendor and buy new License</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="vertical-align: middle; height: 40px" align="center">
                                                                    <asp:LinkButton ID="lnkGoHome" runat="server" style="color:#007CB9" onclick="lnkGoHome_Click" 
                                                                        >Home</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div id="divSuccessBox" class="successBox" runat="server">
                                                        <table style="margin: 10px; height: 90%;width:520px" bgcolor="#CCEEFF">
                                                            <tr>
                                                                <td>
                                                                    <img src="images/tick.png" alt="SCORE Licensed Successfully" />
                                                                </td>
                                                                <td style="vertical-align: middle;" align="center">
                                                                    <h1 style="font-weight: bold; color: #237856;">
                                                                        <asp:Label ID="Label6" runat="server" Text="Medlife License Updated Successfully !"></asp:Label></h1>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="vertical-align: middle;" align="center">
                                                                    <img src="images/sep.jpg" alt="Seperator" 
                                                                        style="height: 12px; width: 100%;" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div id="btnLogin" class="ActivateNow" style="border: medium solid #007CB9;
                                                                        cursor: pointer; text-align: center; width: 127px; background-color: #008ACC;">
                                                                        Login
                                                                    </div>                                                                   
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>                                                                                                         
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="ActivationPopup" style="display:none;">
                                            <div id="heading">
                                                License Activation
                                            </div>
                                            <div>
                                                <div style="margin-top: 30px" align="center">
                                                    <asp:Label ID="Label1" runat="server" Text="License File "></asp:Label>
                                                    <asp:FileUpload ID="FileUpload" runat="server" size="45" />
                                                </div>
                                                <div>
                                                    <asp:Button ID="btnActivate" CssClass="btn1 Activate" runat="server" OnClick="btnActivate_Click"
                                                        OnClientClick="if(!ActivateClick()){return}" Text="Activate" BorderStyle="Solid"
                                                        UseSubmitBehavior="false" />
                                                    <div class="btn1 Close" style="cursor: pointer; text-align: center" href="#" id="close">
                                                        Close</div>
                                                </div>
                                            </div>
                                        </div>                                        
                                        <asp:HiddenField ID="hidProductID" runat="server" ></asp:HiddenField>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td align="center" height="21">
                            <span style="color: #C9E4EF">Copyright © 2008 Perfect Software Solutions (Clt) Pvt.
                                Ltd, Calicut. Ph: +91 495 2365556. mail: <a href="mailto:contact@perfectlimited.com">
                                    contact@perfectlimited.com</a>. web: <a href="http://www.perfectlimited.com">www.perfectlimited.com</a>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="21">
                            <asp:Label ID="lblresolution" runat="server" ForeColor="White" Font-Bold="False"
                                Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
