<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Popup/CommonPopup.ascx" TagPrefix="uc1" TagName="CommonPopup" %>
<%@ Register Src="~/Popup/CommonPopupOnStore.ascx" TagPrefix="uc3" TagName="CommonPopupOnStore" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--META-->
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
    <title>Medlife :: Login</title>

    <!--===============================================================================================-->

    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="css/login_new.css" />
    <link rel="stylesheet" type="text/css" href="css/util.css" />
    <link rel="stylesheet" type="text/css" href="css/login_fr.css" />
    <!--===============================================================================================-->

    <!--SCRIPTS-->
    <script src="js/alertify.min.js"></script>
    <!--STYLESHEETS-->



    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <!--Slider-in icons-->
    <link href="css/Licensing.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="images/localpharmacy.jpg" />
    <link rel="icon" type="image/ico" href="images/localpharmacy.jpg" />
    <script type="text/javascript" src="js/General.js"></script>
    <script type="text/javascript" src="js/jquery-1.10.2.js"></script>
    <script type="text/javascript" async="" src="images/LoginE Form Using jQuery - Demo Preview_files/ga.js"></script>
    <script src="images/LoginE Form Using jQuery - Demo Preview_files/jquery.min.js"></script>
    <script type="text/javascript" src="images/LoginE Form Using jQuery - Demo Preview_files/login.js"></script>
    <script src="AssetLogin/js/jquery.min.js"></script>
      <script type="text/javascript">

        $(function () {
            $(".showpassword").each(function (index, input) {
                var $input = $(input);
                $("<p class='opt'/>").append(
                    $("<input type='checkbox' class='showpasswordcheckbox' id='showPassword' />").click(function () {
                        var change = $(this).is(":checked") ? "text" : "password";
                        var rep = $("<input placeholder='Password' type='" + change + "' />")
                            .attr("id", $input.attr("id"))
                            .attr("name", $input.attr("name"))
                            .attr('class', $input.attr('class'))
                            .val($input.val())
                            .insertBefore($input);
                        $input.remove();
                        $input = rep;
                    })
                ).append($("<label for='showPassword'/>").text("Show password")).insertAfter($input.parent());
            });

            $('#showPassword').click(function () {
                if ($("#showPassword").is(":checked")) {
                    $('.icon-lock').addClass('icon-unlock');
                    $('.icon-unlock').removeClass('icon-lock');
                } else {
                    $('.icon-unlock').addClass('icon-lock');
                    $('.icon-lock').removeClass('icon-unlock');
                }
            });
        });

        function CheckResolution() {

            var width = screen.width;
            var height = screen.height;

            //document.getElementById("lblresolution").innerText = "Current Screen Resolution :" + width + " X " + height + ". Best viewed in 1600 X 900";
            //document.getElementById("lblresolution").value = "Screen Resolution :" + width + " X " + height;
            if (width != 1366 || height != 768) {
                // alertify.alert("This Software is best viewed with a screen resolution of 1366 X 768. Your current resolution is " + width + " X " + height + ". Please change your resolution");
            }

        }

        //$(document).bind("contextmenu", function (e) {
        //    e.preventDefault();
        //});

        //$(document).keydown(function (e) {
        //    if (e.which === 123) {
        //        return false;
        //    }
        //    else if ((e.ctrlKey && e.shiftKey && e.keyCode == 73) || (e.ctrlKey && e.shiftKey && e.keyCode == 74) || (e.ctrlKey && e.shiftKey && e.keyCode == 67)) {

        //        return false;
        //    }
        //});
        function pageLoad() {
            CheckResolution();
        }
        function UsernameFocus() {
            $(".user-icon").css("left", "-48px");
        }
        function UsernameBlur() {
            $(".user-icon").css("left", "0px");
        }
        function PasswordFocus() {
            $(".pass-icon").css("left", "-48px");
        }
        function PasswordBlur() {
            $(".pass-icon").css("left", "0px");
        }
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
        /*------------------------------------New Script Added for Licensing--------------------------------------*/

        //function StockAwayLogo() {
        //    $('#logo1').css({ 'background-image': 'url(images/StockAway_logo.jpg)' });
        //    $('#PerfectLogo').css({ 'background-image': 'url(images/NCODE-LOGO2.png)' });
        //}

        function GoLogin() {
            window.location.replace("Default.aspx");
        }
        function Successpopup() {
            $("#divSuccessBox").fadeIn(1000);
            //$("#divSuccessBox").css({
            //    left: $('#divContainer').position().left - 80,
            //    top: $('#divContainer').position().top - 43,
            //    position: 'absolute'
            //});
        }
        function SerialKeyPopup() {
           
            $('#txtBox1').val(''); $('#txtBox2').val('');
            $('#txtBox3').val(''); $('#txtBox4').val('');
            $('#txtBox5').val('');
            $('#txtBox1').focus();
            $("#divProductkey").fadeIn(1000);

            //$("#divProductkey").css({
            //    left: $('#divContainer').position().left - 80,
            //    top: $('#divContainer').position().top - 43,
            //    position: 'absolute'
            //});
           
            $("#lblProductID").text($("#hidProductID").val());
        }
        function ValidateSerialNo() {
            var box1 = $('#txtBox1').val();
            var box2 = $('#txtBox2').val();
            var box3 = $('#txtBox3').val();
            var box4 = $('#txtBox4').val();
            var box5 = $('#txtBox5').val();
            if (box1.length != 4) {
                alert("Invalid Serial Key! Please Fill Box 1 ");
                $('#txtBox1').focus();
                return false;
            }
            if (box2.length != 4) {
                alert("Invalid Serial Key! Please Fill Box 2 ");
                $('#txtBox2').focus();
                return false;
            }
            if (box3.length != 4) {
                alert("Invalid Serial Key! Please Fill Box 3 ");
                $('#txtBox3').focus();
                return false;
            }
            if (box4.length != 4) {
                alert("Invalid Serial Key! Please Fill Box 4 ");
                $('#txtBox4').focus();
                return false;
            }
            if (box5.length != 4) {
                alert("Invalid Serial Key! Please Fill Box 5 ");
                $('#txtBox5').focus();
                return false;
            }
            return true;
        }
        var interval = null;
        function WaitSeconds(seconds) {
            $("#divWaitSeconds").fadeIn(1000);
            //$("#divWaitSeconds").css({
            //    left: $('#divContainer').position().left - 80,
            //    top: $('#divContainer').position().top - 43,
            //    position: 'absolute'
            //});
            interval = setInterval(function () {
                $('#lblseconds').text('You have to wait ' + seconds + ' Seconds..');
                seconds = seconds - 1;
                if (seconds == 0) {
                    clearInterval(interval);
                    $('#btnScoreLogin').click();
                }
            }, 1000);
        }
        function InsertKeyNow() {
            clearInterval(interval);
            $("#divWaitSeconds").fadeOut(1000);
            SerialKeyPopup();
        }
        function AlertMsg() {
            alert('Invalid Serial Key !');
            return false;
        }
        function SuccessEvaluationpopup() {

            $("#divEvaluation").fadeIn(1000);
            //$("#divEvaluation").css({
            //    left: $('#divContainer').position().left - 80,
            //    top: $('#divContainer').position().top - 43,
            //    position: 'absolute'
            //});
        }
        function AlertEvaluationMsg() {
            alert('Evaluation Period Expired !');
            return false;
        }
        function PopupOnStore() {
            try {

                //$("#ContentPlaceHolder1_divPopupOnstores").css('display', 'block');
                $("#NewLogin").css('display', 'none');
                $("#divPopupOnstores").css('display', 'block');
            }
            catch (ex) {
                //alert('popup' + ex);  
            }
        }


        /*------------------------------------------------------------------------------------------------------- */
    </script>

</head>
<body>
    <form runat="server" id="form1">

        <asp:MultiView ID="mvLogin" runat="server">
            <asp:View ID="ViewLogin" runat="server">
                <div class="container">
                    <div class="row">

                        <div class="col-12 col-md-12 col-xs-12 col-sm-12">

                            <div class="wrappar">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <div class="login_fr_bg">

                                            <div class="wrap-login100">
                                                <div class="login100-form validate-form flex-sb flex-w">
                                                    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
                                                    </asp:ScriptManager>

                                                    <span class="login100-form-title p-b-51">Login
                                                    </span>


                                                    <div class="wrap-input100 validate-input m-b-16" data-validate="Username is required">

                                                        <asp:TextBox ID="txtUsername" CssClass="input100" onfocus="UsernameFocus();" onblur="UsernameBlur();" placeholder="Username" runat="server" TabIndex="1"></asp:TextBox>
                                                        <span class="focus-input100"></span>
                                                    </div>


                                                    <div class="wrap-input100 validate-input m-b-16" data-validate="Password is required">

                                                        <asp:TextBox ID="txtPassword" CssClass="input100" onfocus="PasswordFocus();" onblur="PasswordBlur();" placeholder="Password" runat="server" TextMode="Password" TabIndex="2"></asp:TextBox>
                                                        <span class="focus-input100"></span>
                                                    </div>
                                                    <div class="container-login100-form-btn m-t-17">

                                                        <asp:Button ID="btnSubmit" runat="server" Text="Login" CssClass="login100-form-btn" OnClick="btnSubmit_Click" TabIndex="3" />

                                                    </div>

                                                </div>
                                                <div style="height: 50px">
                                                    <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server" ForeColor="#711E1E" Style="padding-top: 20px"></asp:BulletedList>
                                                </div>

                                                <div class="fr_logo">

                                                    <img src="images/fr_logo.jpg" width="288" height="80">
                                                </div>
                                                <asp:Label ID="lblVersion" runat="server" Style="margin-left: 13px; color: #a59f9f;" Text="Version: 2.3.9"></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <div class="login3"></div>

                                        <div>
                                            <div id="divProductkey" style="display: none">
                                                <div id="heading">
                                                    Complete Activation
                                                </div>
                                                <div>
                                                    <div style="margin: 30px 0px 0px 65px; float: left;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label2" runat="server" Text="Product ID"></asp:Label>
                                                                </td>
                                                                <td>:<asp:Label ID="lblProductID" runat="server" Text="Product ID" Font-Bold="True" Font-Names="Verdana"
                                                                    Style="text-align: center; margin-left: 20px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <div style="margin: 20px 0px 0px 65px">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text="Serial Key : "></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBox1" runat="server" Width="50px" MaxLength="4" Font-Names="Verdana"
                                                                        Style="text-align: center; margin-left: 20px; border: 1px solid black;" TabIndex="4" Font-Bold="True"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBox2" runat="server" Width="50px" MaxLength="4" Font-Names="Verdana"
                                                                        Style="text-align: center;border: 1px solid black;" TabIndex="5" Font-Bold="True"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBox3" runat="server" Width="50px" MaxLength="4" Font-Names="Verdana"
                                                                        Style="text-align: center;border: 1px solid black;" TabIndex="6" Font-Bold="True"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBox4" runat="server" Width="50px" MaxLength="4" Font-Names="Verdana"
                                                                        Style="text-align: center;border: 1px solid black;" TabIndex="7" Font-Bold="True"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBox5" runat="server" Width="50px" MaxLength="4" Font-Names="Verdana"
                                                                        Style="text-align: center;border: 1px solid black;" TabIndex="8" Font-Bold="True"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </div>
                                                    <div>
                                                        <asp:Button ID="btnFinish" CssClass="btn1 Activate" runat="server" Text="Finish" TabIndex="9"
                                                            BorderStyle="Solid" UseSubmitBehavior="false" OnClientClick="if(!ValidateSerialNo()){return}"
                                                            OnClick="btnFinish_Click" />
                                                        <asp:Button ID="btnSkip" runat="server" Text="Skip" TabIndex="10" class="btn1 Close"
                                                            Style="cursor: pointer; text-align: center; height: 30px" href="#" OnClick="btnSkip_Click" />
                                                        <asp:Button ID="btnEvaluation" runat="server" Text="Evaluation" TabIndex="11" class="btn1 Later"
                                                            Style="cursor: pointer; text-align: center; height: 30px; width: 90px" UseSubmitBehavior="false" OnClick="btnEvaluation_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divSuccessBox" class="successBox" style="display: none">
                                                <table style="margin: 10px; height: 90%; width: 520px" bgcolor="#CCEEFF">
                                                    <tr>
                                                        <td>
                                                            <img src="images/tick.png" alt="SCORE Licensed Successfully" />
                                                        </td>
                                                        <td style="vertical-align: middle;" align="center">
                                                            <h1 style="font-weight: bold; color: #237856;">
                                                                <asp:Label ID="Label6" runat="server" Text="Serial Key Updated Successfully !"></asp:Label></h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="vertical-align: middle;" align="center">
                                                            <img src="images/sep.jpg" alt="Seperator" style="height: 12px; width: 100%;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div id="btnLogin" class="ActivateNow" style="border: medium solid #007CB9; cursor: pointer; text-align: center; width: 127px; background-color: #008ACC;"
                                                                onclick="GoLogin();">
                                                                Login
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div id="divEvaluation" class="successEvaluationBox" style="display: none">
                                                <table style="margin: 10px; height: 90%; width: 520px" bgcolor="#CCEEFF">
                                                    <tr>
                                                        <td>
                                                            <img src="images/tick.png" alt="Medlife Licensed Successfully" />
                                                        </td>
                                                        <td style="vertical-align: middle;" align="center">

                                                            <span id="Span2" style="color: #2094BD; font-family: Tahoma; font-size: 10pt;">License of Medlife Will be expired after 7 Days.<br />
                                                                Afer that You have to update License File...</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="vertical-align: middle;" align="center">
                                                            <img src="images/sep.jpg" alt="Seperator" style="height: 12px; width: 100%;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <div id="Div2" class="ActivateNow" style="border: medium solid #007CB9; cursor: pointer; text-align: center; width: 127px; background-color: #008ACC;"
                                                                onclick="GoLogin();">
                                                                Login
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div id="divWaitSeconds" class="successBox" runat="server" style="display: none;">
                                                <table style="margin: 10px; height: 90%; width: 520px" bgcolor="#CCEEFF">
                                                    <tr>
                                                        <td>
                                                            <img src="Jquery/images/exclamation.png" alt="Please Insert the Serial Key" />
                                                        </td>
                                                        <td style="vertical-align: middle;" align="left">
                                                            <h1 style="font-weight: bold; color: #B92500; margin-left: 80px;">
                                                                <asp:Label ID="lblseconds" runat="server" Text="You have to wait a few Seconds.."></asp:Label>
                                                            </h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="vertical-align: middle;" align="center">
                                                            <img src="images/sep.jpg" alt="Seperator" style="height: 12px; width: 100%;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center" style="vertical-align: top">
                                                            <span id="Span1" style="color: #2094BD; font-family: Tahoma; font-size: 10pt;">Please
                            Complete Product Activation by Updating Serial Key..<br />
                                                                Contact Software vendor to get your Serial Key..</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center" style="vertical-align: bottom">
                                                            <span id="KeyNow" onclick="InsertKeyNow()" style="color: #2094BD; cursor: pointer; font-family: Tahoma; font-weight: bold;">Insert Key Now</span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div id="divOTP" style="display: none;">
                                                <a onclick="" class="closeOTPdiv"></a>
                                                <table style="height: 200px">
                                                    <tr>
                                                        <td rowspan="6">
                                                            <img alt="One Time Password" src="Images/Login/otp.jpg" />
                                                        </td>
                                                        <td style="text-align: left; padding-left: 30px; height: 30px;">
                                                            <asp:Label ID="Label4" runat="server" Text="We sent a Code to" Font-Bold="True"></asp:Label>
                                                            <asp:Label ID="lblLastMobNo" runat="server" Text="xxx" Font-Bold="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; height: 5px;"></td>
                                                    </tr>

                                                </table>
                                            </div>
                                        </div>

                                    </div>
                                </div>

                            </div>



                        </div>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="ViewStore" runat="server">


                <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />

                <div id="divPopupOnstores" runat="server">
                    <uc3:CommonPopupOnStore runat="server" ID="CommonPopupOnStore" />
                </div>

                <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                    CancelControlID="btnClose" PopupDragHandleControlID="divheader">
                </cc:ModalPopupExtender>


            </asp:View>
            <%--  <asp:View ID="ViewLicense" runat="server">
                <div id="divProductkey" style="display: none">
                    <div id="heading">
                        Complete Activation
                    </div>
                    <div>
                        <div style="margin: 30px 0px 0px 65px; float: left;">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Product ID"></asp:Label>
                                    </td>
                                    <td>:<asp:Label ID="lblProductID" runat="server" Text="Product ID" Font-Bold="True" Font-Names="Verdana"
                                        Style="text-align: center; margin-left: 20px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <br />
                        <br />
                        <div style="margin: 20px 0px 0px 65px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Serial Key : "></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBox1" runat="server" Width="50px" MaxLength="4" Font-Names="Verdana"
                                            Style="text-align: center; margin-left: 20px" TabIndex="4" Font-Bold="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBox2" runat="server" Width="50px" MaxLength="4" Font-Names="Verdana"
                                            Style="text-align: center" TabIndex="5" Font-Bold="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBox3" runat="server" Width="50px" MaxLength="4" Font-Names="Verdana"
                                            Style="text-align: center" TabIndex="6" Font-Bold="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBox4" runat="server" Width="50px" MaxLength="4" Font-Names="Verdana"
                                            Style="text-align: center" TabIndex="7" Font-Bold="True"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBox5" runat="server" Width="50px" MaxLength="4" Font-Names="Verdana"
                                            Style="text-align: center" TabIndex="8" Font-Bold="True"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <asp:Button ID="btnFinish" CssClass="btn1 Activate" runat="server" Text="Finish" TabIndex="9"
                                BorderStyle="Solid" UseSubmitBehavior="false" OnClientClick="if(!ValidateSerialNo()){return}"
                                OnClick="btnFinish_Click" />
                            <asp:Button ID="btnSkip" runat="server" Text="Skip" TabIndex="10" class="btn1 Close"
                                Style="cursor: pointer; text-align: center; height: 30px" href="#" OnClick="btnSkip_Click" />
                            <asp:Button ID="btnEvaluation" runat="server" Text="Evaluation" TabIndex="11" class="btn1 Later"
                                Style="cursor: pointer; text-align: center; height: 30px; width: 90px" UseSubmitBehavior="false" OnClick="btnEvaluation_Click" />
                        </div>
                    </div>
                </div>
                <div id="divSuccessBox" class="successBox" style="display: none">
                    <table style="margin: 10px; height: 90%; width: 520px" bgcolor="#CCEEFF">
                        <tr>
                            <td>
                                <img src="images/tick.png" alt="SCORE Licensed Successfully" />
                            </td>
                            <td style="vertical-align: middle;" align="center">
                                <h1 style="font-weight: bold; color: #237856;">
                                    <asp:Label ID="Label6" runat="server" Text="Serial Key Updated Successfully !"></asp:Label></h1>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="vertical-align: middle;" align="center">
                                <img src="images/sep.jpg" alt="Seperator" style="height: 12px; width: 100%;" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="btnLogin" class="ActivateNow" style="border: medium solid #007CB9; cursor: pointer; text-align: center; width: 127px; background-color: #008ACC;"
                                    onclick="GoLogin();">
                                    Login
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divEvaluation" class="successEvaluationBox" style="display: none">
                    <table style="margin: 10px; height: 90%; width: 520px" bgcolor="#CCEEFF">
                        <tr>
                            <td>
                                <img src="images/tick.png" alt="Medlife Plus Licensed Successfully" />
                            </td>
                            <td style="vertical-align: middle;" align="center">

                                <span id="Span2" style="color: #2094BD; font-family: Tahoma; font-size: 10pt;">License of Medlife Plus Will be expired after 7 Days.<br />
                                    Afer that You have to update License File...</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="vertical-align: middle;" align="center">
                                <img src="images/sep.jpg" alt="Seperator" style="height: 12px; width: 100%;" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="Div2" class="ActivateNow" style="border: medium solid #007CB9; cursor: pointer; text-align: center; width: 127px; background-color: #008ACC;"
                                    onclick="GoLogin();">
                                    Login
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divWaitSeconds" class="successBox" runat="server" style="display: none;">
                    <table style="margin: 10px; height: 90%; width: 520px" bgcolor="#CCEEFF">
                        <tr>
                            <td>
                                <img src="Jquery/images/exclamation.png" alt="Please Insert the Serial Key" />
                            </td>
                            <td style="vertical-align: middle;" align="left">
                                <h1 style="font-weight: bold; color: #B92500; margin-left: 80px;">
                                    <asp:Label ID="lblseconds" runat="server" Text="You have to wait a few Seconds.."></asp:Label>
                                </h1>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="vertical-align: middle;" align="center">
                                <img src="images/sep.jpg" alt="Seperator" style="height: 12px; width: 100%;" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="vertical-align: top">
                                <span id="Span1" style="color: #2094BD; font-family: Tahoma; font-size: 10pt;">Please
                            Complete Product Activation by Updating Serial Key..<br />
                                    Contact Software vendor to get your Serial Key..</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center" style="vertical-align: bottom">
                                <span id="KeyNow" onclick="InsertKeyNow()" style="color: #2094BD; cursor: pointer; font-family: Tahoma; font-weight: bold;">Insert Key Now</span>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divOTP" style="display: none;">
                    <a onclick="" class="closeOTPdiv"></a>
                    <table style="height: 200px">
                        <tr>
                            <td rowspan="6">
                                <img alt="One Time Password" src="Images/Login/otp.jpg" />
                            </td>
                            <td style="text-align: left; padding-left: 30px; height: 30px;">
                                <asp:Label ID="Label4" runat="server" Text="We sent a Code to" Font-Bold="True"></asp:Label>
                                <asp:Label ID="lblLastMobNo" runat="server" Text="xxx" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; height: 5px;"></td>
                        </tr>

                    </table>
                </div>
            </asp:View>--%>
        </asp:MultiView>


        <asp:HiddenField ID="hidProductID" runat="server" />
        <asp:HiddenField ID="hidSkip" runat="server" />
        <asp:HiddenField ID="hidLoginArray" runat="server" />
        <asp:HiddenField ID="hidFinger" runat="server" />
        <asp:HiddenField ID="HdValuestore" runat="server" />


        <div id="dropDownSelect1"></div>



    </form>
    <!--===============================================================================================-->

    <!--===============================================================================================-->

    <script>
        $(".selection-2").select2({
            minimumResultsForSearch: 20,
            dropdownParent: $('#dropDownSelect1')
        });
    </script>

    <!--===============================================================================================-->


    <script type="text/javascript" src="AssetLogin/js/jquery.min.js"></script>
    <script type="text/javascript" src="AssetLogin/js/jquery.dropdown.js"></script>
    <script type="text/javascript">

        $(function () {

            $('#cd-dropdown').dropdown({
                gutter: 5
            });

        });

    </script>

</body>
</html>
