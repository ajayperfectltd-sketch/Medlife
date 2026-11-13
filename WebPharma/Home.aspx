<%@ page language="C#" autoeventwireup="true"  CodeFile="Home.aspx.cs" inherits="Home" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">  
    <script src="js/jquery-1.10.2.js"></script>
<script src="js/Custom.js"></script>
    <link href="css/main.css" rel="stylesheet" />
      <script src="js/jquery.min.js"></script>
    <script src="js/jquery.easing.js"></script>
    <script src="js/jquery.dimensions.js"></script>
   
   
<script src="js/slidereveal.js"></script>
<script src="js/jquery.mCustomScrollbar.concat.min.js"></script>
<script src="js/jquery.mCustomScrollbar.js"></script>
<script src="js/jquery.mousewheel-3.0.6.js"></script>
<script src="js/jquery.mousewheel.js"></script>
           <%--<script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>--%>


   <script src="js/jquery.accordion.js"></script>

    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="css/ddmenu.css" rel="stylesheet" type="text/css" />

    <link href="css/slide.css" rel="stylesheet" />
    <link href="css/Styles.css" rel="stylesheet" />
    <link href="css/toastr.css" rel="stylesheet" />
    <%--<link href="css/Company.css" rel="stylesheet" />--%>

    <link href="css/CallOut.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
     
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/toastr.js"></script>
    <script src="js/Custom.js"></script>
    <script src="js/slidereveal.js"></script>
    <script src="js/ddmenu.js" type="text/javascript"></script>
    <script src="js/Company.js" type="text/javascript"></script>

  <%--  <script src="js/formcheck/lang/en.js"></script>--%>

    <script src="js/slide.js"></script>
    <script src="js/DashBoardJs/JsDash/jquery-ui.js"></script>
  <%--  <script src="js/jquery-1.11.1.min.js"></script>--%>
    <script src="js/DashBoardJs/JsDash/jquery-ui.min.js"></script>
    <script src="js/alertify.min.js"></script>

    <%-- ForLeft Menu --%>
    
   
 
    <script src="js/jquery.easing.js"></script>
    <script src="js/jquery.dimensions.js"></script>

    <script src="js/jquery.accordion.js"></script>

    <script type="text/javascript">
        function ChangePw() {
            if (typeof Page_ClientValidate === 'function' && !Page_ClientValidate('Password')) {
                return false;
            }
            $("#btnChange").click();
            return false;
        }
        function passwordUrl() {
            try {

                $("#myIframe").attr('src', "ChangePassword.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val());
                window.scrollTo(0, 0);
                { $("div#panel").slideUp("slow") }
            }
            catch (e) {
                alert('passwordUrl');
            }
            //  $("#btnChange").click();
        }

        $(document).ready(function () {

            try {
                $('#navigation').accordion({
                    active: false,
                    alwaysOpen: false,
                    header: '.head',
                    navigation: true,
                    event: 'click',
                    fillSpace: false,
                    animated: 'easeslide'
                });
            }
            catch (ex) {
                alert(ex);
            }
        });

        function UploadComplete1(sender, args) {
            $("#<%#BtnFsAttch.ClientID%>").click();
        }

    </script>
      <script>
          $(function () {
              $("#accordion").accordion();
          });
  </script>
  
    <style>

    .slider-nav-area {
        width: 241px;
        margin-top: 75px;
        height: calc(100vh - 95px);
        overflow-y: auto;
        overflow-x: hidden;
    }

   #navigation {
    margin: 0px;
    padding: 0px;
    text-indent: 0px;
    background-color: #FFF;
    width: 230px;
    height: auto;
    overflow: visible;
  
}

    #navigation a.head {
        cursor: pointer;
        background: #FFF; font-family:Arial, Helvetica, sans-serif;
        /*height: 18px;*/
        
        color: #666;
        display: block;
        font-weight: bold; border:solid 1px #f9f9f9; 
        margin: 0px;
        font-size: 15PX;
        padding-top: 12px;
        text-indent: 14px;
        text-decoration: none;
        /*background-image: url(../../images/SliderMn1.png);*/
        background-repeat: repeat;
        padding-bottom :12px
    }
        #navigation a.head:hover 
    {
        color: #000;
    }

    #navigation a.selected {
        /*background-image: url(../../images/SliderMn1.png);*/
         background-repeat: repeat;
    }

    #navigation a.current {
        background-color: #088409;
    }

    #navigation ul {
        border-width: 0px;
        font-size: 14px;
        margin: 0px;
        max-height: none;
        height: auto;
        overflow-y: visible;
        text-indent: 0px;
        background-color: #f7f7f7;
        padding-left: 17px;
    }

    #navigation li {
        list-style: none outside none;
        display: inline;
    }

        #navigation li li a {
           
            color: #666;
            display: block;
            padding-top: 10px;
            padding-bottom: 10px;
            text-indent: 10px; border: solid 1px #f3f3f3;
            text-decoration: none;
            font-family: Verdana;
            font-size: 13px;
        }

        #navigation li li a:hover {
            color: #333;
           

        }

        #navigation li li a.active {
            font-weight:600;
        }



    </style>

    <%-- End Left Menu --%>

<%--  <script src="js/jquery.min.js"></script>--%>
<%-- Green Style Start --%>

    <%-- Old Style Start --%>
 
    
    
    <title>:: Pharma ::</title>
      <link rel="shortcut icon" type="image/x-icon" href="images/localpharmacy.jpg" />
    <link rel="icon" type="image/ico" href="images/localpharmacy.jpg" />

    
      <script type="text/javascript">

          $(document).ready(function () {
              try {
                  // CallMenu();
                  //GetNotiFicationCount();
                  //GetReminder();
                  //Clear();

                  AutoResizeiFrame();

                  var url = window.location.href;
                  // alert(url);
                  var field = 'uid';
                  var index = 0;
                  var field2 = 'clnt';
                  var index2 = 0;
                  var field3 = 'chgpwd';
                  var index3 = 0;

                  index = url.indexOf('&' + field + '=');
                  index2 = url.indexOf('&' + field2 + '=');
                  index3 = url.indexOf('&' + field3 + '=');
                  $('#hfUrlUser').val(url.substring(index + 5, index2));
                  $('#hfUrlClient').val(url.substring(index2 + 6, index3));
                  //alert(index)  , alert(index2), alert(index3);
                  CallMenu($('#hfUrlUser').val(), $('#hfUrlClient').val());
                  // GetReminderDetails($("#hfUrlUser").val(), $("#hfUrlClient").val());
                  CallMenuLeft($('#hfUrlUser').val(), $('#hfUrlClient').val());
                  // QuickMenu($('#hfUrlUser').val(), $('#hfUrlClient').val());
                  if ($('#hfUserType').val() == 'True') {

                      var url = "AdminDashBoard.aspx";
                      $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&nme=' + $("#hfUserName").val());
                      window.scrollTo(0, 0);
                  }
                  else {

                      var url = "UserDashBoard.aspx";
                      $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&Username=' + $("#hfUserName").val());
                      window.scrollTo(0, 0);
                  }


                  //$('.aaf').live('click', function () {
                  $('body').on('click', '.aaf', function () {
                      slider = $("#slider").slideReveal({
                          push: true,
                          position: "left",
                          speed: 600,
                          width: 250
                      });
                      var url = $(this).attr("id")
                      if (url.toString() == "POS.aspx") {
                          var url = "PosStartScreen.aspx";
                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&nme=' + $("#hfUserName").val());
                          window.scrollTo(0, 0);
                          //window.open("POS.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val());
                      }
                      else if (url.toString() == "CashTransaction.aspx") {
                          url = "AccountHeadSubTransactionpartial.aspx";
                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&nme=' + $("#hfUserName").val() + '&key=1');
                          window.scrollTo(0, 0);
                      }
                      else if (url.toString() == "BankTransaction.aspx") {
                          url = "AccountHeadSubTransactionpartial.aspx";
                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&key=2');
                          window.scrollTo(0, 0);
                      }
                      else if (url.toString() == "StockAdjustment.aspx") {

                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&utyp=' + $("#hfUserType").val());
                          window.scrollTo(0, 0);
                      }
                      else if (url.toString() == "DayBeginProcess.aspx") {                 //modified by bibin

                          window.location = "DayBeginProcess.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val();
                      }

                      else if (url.toString() == "DayEndProcess.aspx") {                 //modified by bibin

                          window.location = "DayEndProcess.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val();
                      }





                      else {

                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val());
                          window.scrollTo(0, 0);

                      }
                  });

              }
              catch (ex) {
                  alert(ex);
              }
          });
          function ShowPwdMsg() {
              try {
                  toastr.options = {
                      "closeButton": true,
                      "debug": false,
                      "positionClass": "toast-bottom-right",
                      "onclick": null,
                      "showDuration": "300",
                      "hideDuration": "1000",
                      "timeOut": "300000",
                      "extendedTimeOut": "300000",
                      "showEasing": "swing",
                      "hideEasing": "linear",
                      "showMethod": "fadeIn",
                      "hideMethod": "fadeOut"
                  }
                  toastr.info('Time to change your password. Please set a new password');
              }
              catch (e) {
                  alert('ChangePassword');
              }
          }
          function ShowMonthlyProcessMsg(message, isUrgent) {
              try {
                  if (!message) return;
                  if (window._mpWarnShown) return;
                  window._mpWarnShown = true;

                  var urgent = (isUrgent === true || isUrgent === 'true' || isUrgent === 1 || isUrgent === '1');
                  if (typeof toastr !== 'undefined') {
                      toastr.clear();
                      toastr.options = {
                          "closeButton": true,
                          "debug": false,
                          "newestOnTop": true,
                          "positionClass": "toast-top-full-width",
                          "onclick": null,
                          "showDuration": "300",
                          "hideDuration": "1000",
                          "timeOut": "0",
                          "extendedTimeOut": "0",
                          "tapToDismiss": true,
                          "showEasing": "swing",
                          "hideEasing": "linear",
                          "showMethod": "fadeIn",
                          "hideMethod": "fadeOut"
                      };
                      if (urgent) {
                          toastr.error(message, 'Monthly Process');
                      }
                      else {
                          toastr.warning(message, 'Monthly Process');
                      }
                  }
                  else {
                      alert(message);
                  }
              }
              catch (e) {
                  alert(message);
              }
          }

          function HideMonthlyProcessWarning() {
              try {
                  var pnl = document.getElementById('pnlMonthlyProcessWarning');
                  if (pnl) pnl.style.display = 'none';
              }
              catch (e) { }
          }

          function ShowMonthlyProcessWarningFromHidden() {
              try {
                  if (window._mpWarnShown) return;
                  var msgCtrl = document.getElementById('hfMonthlyProcessWarning');
                  var urgCtrl = document.getElementById('hfMonthlyProcessUrgent');
                  if (!msgCtrl || !msgCtrl.value) return;
                  ShowMonthlyProcessMsg(msgCtrl.value, urgCtrl ? urgCtrl.value : '0');
              }
              catch (e) { }
          }

          $(document).ready(function () {
              setTimeout(ShowMonthlyProcessWarningFromHidden, 700);
          });
          function pageRedirect() {

              try {

                  //    alert('d');

                  // var url = window.location.href;
                  //alert(url);
                  //var field = 'uid';
                  //var index = 0;
                  //var field2 = 'clnt';
                  //var index2 = 0;
                  //var field3 = 'chgpwd';
                  //var index3 = 0;
                  //index = url.indexOf('&' + field + '=');
                  //index2 = url.indexOf('&' + field2 + '=');
                  //index3 = url.indexOf('&' + field3 + '=');
                  //$('#hfUrlUser').val(url.substring(index + 5, index));
                  //$('#hfUrlClient').val(url.substring(index2 + 6, index2));
                  //  CallMenu($('#hfUrlUser').val(), $('#hfUrlClient').val());
                  //alert(index), alert(index2), alert(index3);
                  //$("#hfTaxValue").val() == "" ? "0" : $("#hfTaxValue").val();
                  //  alert($('#hfDecriptUid').val());

                  //QuickMenu($('#hfDecriptUid').val(), $('#hfDeclnt').val());
                  window.parent.location.href = "Items.aspx?uid=" + $("#hfUser").val().split(',')[0] + "&clnt=" + $("#hfClient").val().split(',')[0];
                  // CallMenuLeft($('#hfDecriptUid').val(), $('#hfDeclnt').val());
                  // CallMenu($('#hfDecriptUid').val(), $('#hfDeclnt').val());
                  // CallMenuLeft($('#hfDecriptUid').val(), $('#hfDeclnt').val());
                  // QuickMenu($('#hfDecriptUid').val(), $('#hfDeclnt').val());
                  //var url = "Items.aspx";
                  //$("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&nme=' + $("#hfUserName").val());
                  //window.scrollTo(0, 0);
                  //url = "Customer.aspx?uid=" + $("#hfUser").val().split(',')[0] + "&clnt=" + $("#hfClient").val().split(',')[0];
                  //url = "Items.aspx? &uid=" + $("#hfUser").val() + '&clnt=' + $("#hfClient").val();
                  //popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                  //",statusbar=no,menubar=no,resizable=0,width=1015," +
                  //"height=650,left = 200,top = 100");
                  //popUpObj.focus();
              }
              catch (e) {
                  alert(e);
              }

          }
          function pageLoad() {
              //alert('u');
              //alert('j');
              //$(document).keyup(function (e) {
              //     alert('h');
              //    var key = (e.keyCode ? e.keyCode : e.charCode);
              //    switch (key) {
              //        case 112:
              //            //  alert('g');
              //            pageRedirect();
              //            // navigateUrl($('a[id$=lnk1]'));
              //            break;
              //        case 113:
              //            // alert('m');
              //            navigateUrl($('a[id$=lnk2]'));
              //            break;
              //        case 114:
              //            //alert('mw');
              //            navigateUrl($('a[id$=lnk3]'));
              //            break;
              //        default:;
              //    }
              //});

              //function navigateUrl(jObj) {
              //    window.location.href = $(jObj).attr("href");
              //}

              var url = window.location.href;
              //  alert(url);
              $('.notif').live("click", function () {
                  var url = $(this).attr("id")
                  //  alert(url);
                  var pageType = "";
                  if (url == 'InventoryReport.aspx') {
                      pageType = 'MinStock';
                      $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&pgtpe=' + pageType.toString());
                      window.scrollTo(0, 0);
                  }

                  if (url == 'ReminderView.aspx') {
                      pageType = 'Reminder';
                      $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&pgtpe=' + pageType.toString());
                      window.scrollTo(0, 0);
                  }
                  //  var container = $("#divUserDropdown");
                  var Notcontainer = $("#divNotificationDropdown");
                  var Remcontainer = $("#divReminderNoti");
                  container.hide();
                  Notcontainer.hide();
                  Remcontainer.hide();
              });

          }




          $(document).ready(function () {

              try {



                  var url = window.location.href;
                  var field = 'uid';
                  var index = 0;
                  var field2 = 'clnt';
                  var index2 = 0;
                  var field3 = 'chgpwd';
                  var index3 = 0;
                  //var field4 = 'Username';
                  //var index4 = 0;
                  //var field5 = 'Usernamedummy';
                  //var index5 = 0;
                  // alert(url);
                  var a = new Date().format("dd/MM/yyyy");
                  $("#txtDate").val(a);
                  index = url.indexOf('&' + field + '=');
                  index2 = url.indexOf('&' + field2 + '=');
                  index3 = url.indexOf('&' + field3 + '=');
                  $('#hfUrlUser').val(url.substring(index + 5, index2));
                  $('#hfUrlClient').val(url.substring(index2 + 6, index3));
                  //  alert('#hfUrlUser');
                  //  $('#hfUserName').val(url.substring(index4 + 10, index5));
                  //aler('g');
                  // alert($("#hfUserName").val());
                  $(document).keyup(function (e) {
                      // alert('h');
                      var key = (e.keyCode ? e.keyCode : e.charCode);
                      switch (key) {
                          case 112:
                              //  alert('g');
                              pageRedirect();
                              // navigateUrl($('a[id$=lnk1]'));
                              break;
                          case 113:
                              // alert('m');
                              navigateUrl($('a[id$=lnk2]'));
                              break;
                          case 114:
                              //alert('mw');
                              navigateUrl($('a[id$=lnk3]'));
                              break;
                          default:;
                      }
                  });
                  // alert($("#hfUserName").val());
                  GetMinItemStockCount($("#hfUrlUser").val(), $("#hfUrlClient").val());


                  GetMinItemStockName($("#hfUrlUser").val(), $("#hfUrlClient").val());
                  //GetReminderNew($("#hfUrlUser").val(), $("#hfUrlClient").val());
                  //GetReminderDetails($("#hfUrlUser").val(), $("#hfUrlClient").val());

                  CallMenu($('#hfUrlUser').val(), $('#hfUrlClient').val());
                  QuickMenu($('#hfUrlUser').val(), $('#hfUrlClient').val());

                  if ($('#hfUserType').val() == 'True') {

                      var url = "AdminDashBoard.aspx";
                      $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&nme=' + $("#hfUserName").val());
                      window.scrollTo(0, 0);
                  }
                  else {

                      var url = "UserDashBoard.aspx";
                      $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&Username=' + $("#hfUserName").val());
                      window.scrollTo(0, 0);
                  }

                  //$('#navigation').accordion({
                  //    active: false,
                  //    header: '.head',
                  //    navigation: true,
                  //    event: 'click',
                  //    fillSpace: false,
                  //    animated: 'easeslide'
                  //});

                  $('.aaf').live("click", function () {
                      $('.aaf').removeAttr("style");

                      $(this).attr("style", " background-color: #7baf8e; color: #fbf7f7;");
                      var url = $(this).attr("id")
                      if (url.toString() == "POS.aspx") {
                          var url = "PosStartScreen.aspx";
                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&nme=' + $("#hfUserName").val());
                          window.scrollTo(0, 0);
                          //window.open("POS.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val());
                      }
                      else if (url.toString() == "CashTransaction.aspx") {
                          url = "AccountHeadSubTransactionpartial.aspx";
                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&nme=' + $("#hfUserName").val() + '&key=1');
                          window.scrollTo(0, 0);
                      }
                      else if (url.toString() == "BankTransaction.aspx") {
                          url = "AccountHeadSubTransactionpartial.aspx";
                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&key=2');
                          window.scrollTo(0, 0);
                      }
                      else if (url.toString() == "StockAdjustment.aspx") {

                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&utyp=' + $("#hfUserType").val());
                          window.scrollTo(0, 0);
                      }
                      else if (url.toString() == "DayBeginProcess.aspx") {                 //modified by bibin

                          window.location = "DayBeginProcess.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val();
                      }

                      else if (url.toString() == "DayEndProcess.aspx") {                 //modified by bibin

                          window.location = "DayEndProcess.aspx" + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val();
                      }


                      else {

                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val());
                          window.scrollTo(0, 0);

                      }
                  });
                  //GetReminder($("#hfUser").val(), $("#hfClient").val());

                  //$(document).mouseup(function (e) {
                  //    alert('nnn');
                  //    alert('h435345');
                  //    // var container = $("#divUserDropdown");

                  //    var Notcontainer = $("#divNotificationDropdown");
                  //    var Remcontainer = $("#divReminderNoti");
                  //    if (!container.is(e.target) // if the target of the click isn't the container...
                  //        && container.has(e.target).length === 0) // ... nor a descendant of the container
                  //    {
                  //        container.hide();
                  //    }
                  //    if (!Notcontainer.is(e.target) // if the target of the click isn't the container...
                  //       && Notcontainer.has(e.target).length === 0) // ... nor a descendant of the container
                  //    {
                  //        Notcontainer.hide();
                  //    }
                  //    if (!Remcontainer.is(e.target) // if the target of the click isn't the container...
                  //    && Remcontainer.has(e.target).length === 0) // ... nor a descendant of the container
                  //    {
                  //        Remcontainer.hide();
                  //    }
                  //});
                  $('.notif').live("click", function () {
                      //alert('h');
                      var url = $(this).attr("id")
                      //  alert(url);
                      var pageType = "";
                      if (url == 'InventoryReport.aspx') {
                          pageType = 'MinStock';
                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&pgtpe=' + pageType.toString());
                          window.scrollTo(0, 0);
                      }

                      if (url == 'ReminderView.aspx') {
                          pageType = 'Reminder';
                          $("#myIframe").attr('src', url.toString() + '?uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&pgtpe=' + pageType.toString());
                          window.scrollTo(0, 0);
                      }
                      //  var container = $("#divUserDropdown");
                      var Notcontainer = $("#divNotificationDropdown");
                      var Remcontainer = $("#divReminderNoti");
                      container.hide();
                      Notcontainer.hide();
                      Remcontainer.hide();
                  });



                  ForResolution();



              }
              catch (ex) {

              }


          });


          //Slider Manu 
          $(function () {

              slider = $("#slider").slideReveal({
                  push: true,
                  position: "left",
                  speed: 600,
                  width: 250,
                  trigger: $("#trigger")
              });

          });
          //Slider Manu Ends Here


          function ShowViewStateSize() {
              var buf = document.forms[0]["__VIEWSTATE"].value;
              alert("View state is " + buf.length + " bytes");
          }
          function GetMinItemStockCount(userid, clientid) {
              try {
                  $.ajax({
                      type: "POST",
                      url: 'Textile.asmx/GetMinItemStockCount',
                      data: "{'userid':'" + userid + "','clientid':'" + clientid + "'}",
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (data) {

                          if (data.d != '') {
                              var arr = (data.d);

                              $("#btnnoti").val(arr);

                          }
                          if (parseFloat(data.d) == 0) {

                              $("#btnnoti").hide();
                          }
                      },
                      error: function (error) {

                      }
                  });
              }
              catch (e) {
                  alert('minitemstockcount');
              }
          }
          function GetMinItemStockName(userid, clientid) {
              try {
                  $.ajax({
                      type: "POST",
                      url: 'Textile.asmx/GetMinItemStockName',
                      data: "{'userid':'" + userid + "','clientid':'" + clientid + "'}",
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function (MenuHtml) {
                          $('#unames').html(MenuHtml.d);
                      },
                      error: function (MenuHtml) {
                          // alert('GetMinItemStockName');
                          //alert('webservice_error');
                      }
                  });
              }
              catch (e) {
                  //   alert('MinItemStockName');
              }
          }


          function ShowChangePhoto() {

              try {
                  $("#PnlFsAttch").show();
                  $(".photo1 img").css("opacity", "0.2");
                  $(".photo1 img").css("filter", "alpha(opacity = 40)");
              }
              catch (ex) {
                  alert(ex);
              }
          }
          function ShowNotificationDropdown() {

              $("#divNotificationDropdown").show();
          }
          function HideNotificationDropdown() {
              var Notcontainer = $("#divNotificationDropdown");

              Notcontainer.hide();

          }
          function HideChangePhoto() {

              try {
                  $("#PnlFsAttch").hide();
                  $(".photo1 img").css("opacity", "10");
                  $(".photo1 img").css("filter", "alpha(opacity = 00)");
              }
              catch (ex) {
                  alert(ex);
              }
          }

          function Clear() {

              $("#txtCurrentPswd").val('');
          }



          function ShowPanel() {
              if ($('#divChangePassword').css('display') == 'none') {
                  $("#divChangePassword").hide();
              }
              else {
                  $("#divChangePassword").hide();
              }
          }
          function ShowUserDropdown() {

              $("#divChangePassword").hide();
          }

          // For loading freequnt menu redirection page
          function UrlLoad(page) {
              try {
                  var url = window.location.hostname;
                  document.location.href = page;
              }
              catch (ex) {
                  alert(ex + 'Freequent Menu page Redirection');
              }
          }
          // For loading freequnt menu redirection page END HERE

          // For Clearing menu clearing
          function FreequentMenuClear() {
              try {

                  ClearFreequentMenu();


              }
              catch (ex) {
                  alert(ex + 'Freequent Menu Clearing');
              }
          }
          // For Clearing menu clearing END HERE

          function OpenCompany() {
              $("#divCompany").fadeIn('slow');
          }
          function CloseCompany() {
              $("#divCompany").fadeOut('slow');
          }
          function ShowReminderDropdown() {
              $("#divReminder").show();
              //  alert('g');
              var url = window.location.href;
              var field = 'uid';
              var index = 0;
              var field2 = 'clnt';
              var index2 = 0;
              var field3 = 'chgpwd';
              var index3 = 0;

              index = url.indexOf('&' + field + '=');
              index2 = url.indexOf('&' + field2 + '=');
              index3 = url.indexOf('&' + field3 + '=');
              $('#hfUrlUser').val(url.substring(index + 5, index2));
              $('#hfUrlClient').val(url.substring(index2 + 6, index3));
              GetReminderDetails($("#hfUrlUser").val(), $("#hfUrlClient").val());
              //GetReminderDetails();


          }
          function HideReminderDropdown() {
              try {
                  $("#divReminder").hide();
              }
              catch (ex) {
                  alert(ex)
              }
          }
          function Authorizations() {
              $("#btnAuthorizations").click();
          }
          function Notifications() {

              $("#btnNotifications").click();
          }


          $(document).mouseup(function (e) {
              try {

                  var Remcontainer = $("#divReminder");

                  if (!Remcontainer.is(e.target) // if the target of the click isn't the container...
                  && Remcontainer.has(e.target).length === 0) // ... nor a descendant of the container
                  {
                      Remcontainer.hide();
                  }
                  // var container = $("#divUserDropdown");

                  var Notcontainer = $("#divNotificationDropdown");

                  if (!Notcontainer.is(e.target) // if the target of the click isn't the container...
                     && Notcontainer.has(e.target).length === 0) // ... nor a descendant of the container
                  {
                      Notcontainer.hide();
                  }
                  if (!Remcontainer.is(e.target) // if the target of the click isn't the container...
                  && Remcontainer.has(e.target).length === 0) // ... nor a descendant of the container
                  {
                      Remcontainer.hide();
                  }
              }
              catch (ex) {
                  alert('Hide Reminder :' + ex);
              }
          });

          //home
          function AutoResizeiFrame() {
              // alert("Ready");

              var newheight;
              var newwidth;
              if (document.getElementById('myIframe')) {
                  newheight = document.body.scrollHeight + 240;
                  newwidth = document.body.scrollWidth;
              }
              // alert(document.body.scrollHeight)
              document.getElementById('myIframe').height = (newheight) + "px";
              // document.getElementById('myIframe').width = (newwidth) + "px";
          }
          function ShowImage() {

              $("#btnImage").click();
          }



          window.addEventListener("pageshow", function (event) {
              if (event.persisted) {
                  window.location.reload();
              }
          });

    </script>
   
</head>
    <body>
    <div id="divDelete" style="display: none"></div>
    <form id="form1" name="SimpleDemo" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="20000"></asp:ScriptManager>
        <asp:HiddenField ID="hfMonthlyProcessWarning" runat="server" ClientIDMode="Static" Value="" />
        <asp:HiddenField ID="hfMonthlyProcessUrgent" runat="server" ClientIDMode="Static" Value="0" />
        <asp:Panel ID="pnlMonthlyProcessWarning" runat="server" ClientIDMode="Static" Visible="false"
            style="position:fixed; top:0; left:0; right:0; z-index:99999; padding:12px 16px; font-family:Segoe UI, Arial, sans-serif; font-size:14px; font-weight:600; text-align:center; box-shadow:0 2px 8px rgba(0,0,0,.25);">
            <asp:Label ID="lblMonthlyProcessWarning" runat="server" ClientIDMode="Static"></asp:Label>
            <a href="javascript:void(0);" onclick="HideMonthlyProcessWarning();"
               style="margin-left:18px; color:inherit; text-decoration:underline; font-weight:normal;">Dismiss</a>
        </asp:Panel>
        <div id="toppanel">
            <div id="panel">
                <!-- /login -->
                <div class="content clearfix">
                    <div class="left" style="width: 140px; margin-top: 55px; font-size: 20px; height: 50px; margin-left: -36px;">
                        <div class="logo" style="margin-left: -130px;">
                        </div>
                        <div class="login_Version" style="margin-left: -120px;">
                                    Ver :
                                    <asp:Label ID="lblVersion" Text="2.3.9" runat="server" />
                                </div>
                    </div>
                    <div class="left">
                        <div class="top_se">
                            <div class="top1">
                                <div class="photo1">
                                    <asp:Image ID="imgAttachment" ImageUrl="~/images/img-52.jpg" Width="108" Height="90" runat="server" Style="border: 3px solid #f9f9f9; height: 115px; width: 108px; margin-left: 3px" />
                                </div>
                                <asp:HiddenField runat="server" ID="hfdUserImage" />
                                <asp:HiddenField runat="server" ID="HfdMaster" />
                                <div class="user_name">
                                    NAME :
                                    <asp:Label ID="lblUserName" runat="server"> </asp:Label>
                                </div>
                                  <div class="login_Store">
                                    STORE :
                                    <asp:Label ID="lblStore" Text="" runat="server" />
                                </div>
                                 <div class="login_grp">
                                    USER GROUP :
                                    <asp:Label ID="lblUsergrup" Text="" runat="server" />
                                </div>
                                 
                                
                                <div class="login_date6">
                                    <asp:Panel ID="PnlFsAttch" CssClass="PnlFsAttchCS" Style="cursor: pointer;" runat="server"  onmouseover="ShowChangePhoto();" onmouseout="HideChangePhoto();">
                                        <cc:AsyncFileUpload
                                            runat="server" ID="fupImage" Width="135px" UploaderStyle="Traditional" CssClass="FileUploaderCS"
                                           
                                            OnClientUploadComplete="UploadComplete1"
                                            UploadingBackColor="Black" ThrobberID="LblUplProg01"
                                            Style="position: absolute; cursor: pointer; left: -20px; z-index: 2; opacity: 0; filter: alpha(opacity=0); right: 0px;" />
                                        <asp:Button ID="BtnFsAttch" CssClass="BtnFsAttchCS" BackColor="Transparent" BorderStyle="None" Style="cursor: pointer;" ForeColor="white" runat="server" Text="Change Image" Font-Size="x-Small" Width="105px" />
                                    </asp:Panel>
                                    <asp:HiddenField ID="hdfImage" runat="server"  />
                                </div>
                            </div>
                            <div class="settings">
                                <div class="btn1">
                                    <asp:LinkButton ID="LinkButton4"  Text="CHANGE PASSWORD" OnClientClick="passwordUrl(); return false;" Style="border-left: none;"  runat="server" />
                                </div>
                                <div class="btn_3" onClick="ShowPanel(); return false;" style="display: none">
                                    <asp:LinkButton ID="LinkButton1" Text="ADD NEW USER" Style="border-left: none;" runat="server" />
                                </div>

                                <div class="btn_4">
                                    <asp:LinkButton ID="LinkButton5" Text="LOGOUT" Style="border-left: none;" runat="server" OnClick="LinkButton5_Click"  />
                                </div>
                                <asp:TextBox ID="TextBox1" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="left1">
                        <div>
                               <div class="login_date">
                                   <span style="color:#717171"> Last Login :</span>
                                    <asp:Label ID="lblLastLogin" Text="" runat="server" />
                                </div>
                                <div class="login_date2">
                                   <span style="color:#717171"> Last pswd Change:</span>
                                    <asp:Label ID="lblLastPwdChange" Text="" runat="server" />
                                </div>
                                <div class="login_date2">
                                 <span style="color:#717171"> Failed Login:</span>
                                    <asp:Label ID="lblFaild" Text="" runat="server" />
                                </div>

                               <%--<div class="login_date2">
                                   Failed Login:
                                    <asp:Label ID="lblFaild" Text="" runat="server" />
                                </div>--%>

                        </div>
                        <div id="divChangePassword" class="top_se2" style="display: none;">
                            <div class="crnt_pwd">
                                <div class="crnt_name">
                                    <asp:Label ID="Label1" runat="server" Text="Current Password" Width="150px"></asp:Label>
                                </div>
                                <div class="crnt_name2">
                                    <asp:TextBox ID="txtCurrentPswd" runat="server" Text="" TextMode="Password" MaxLength="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage=" Current Password required" ControlToValidate="txtCurrentPswd" Display="None" SetFocusOnError="True" ValidationGroup="Password"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" CssClass="customCalloutStyle" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </div>
                            </div>
                            
                            <div class="crnt_pwd2">
                              
                                <div class="crnt_name4">
                                    <asp:Label ID="Label4" runat="server" Text="New Password" Width="150px"></asp:Label>
                                </div>
                                <div class="crnt_name3">
                                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage=" New Password required" ControlToValidate="txtNewPassword" Display="None" SetFocusOnError="True" ValidationGroup="Password"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" CssClass="customCalloutStyle" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                    </cc:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="crnt_pwd2">
                                <div class="crnt_name4">
                                    <asp:Label ID="Label3" runat="server" Text="Confirm Password" Width="150px"></asp:Label>
                                </div>
                                <div class="crnt_name3">
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage=" Confirm Password required" ControlToValidate="txtConfirmPassword" Display="None" SetFocusOnError="True" ValidationGroup="Password"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" CssClass="customCalloutStyle" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPassword" SetFocusOnError="true" ControlToCompare="txtNewPassword" Type="String" ValidationGroup="Password" Display="None" ErrorMessage="Password don't match" Operator="Equal"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" CssClass="customCalloutStyle" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="sbt"> 
                                <asp:LinkButton ID="btnPasswordChange" Style="border-left: none;" OnClientClick="return ChangePw();" runat="server" Text="CONFIRM" ValidationGroup="Password" CausesValidation="true" />
                            </div>
                          
                             
                            <div class="hide_arrows"><a href="#"></a></div>
                              <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>
                            <%-- </asp:Panel>--%>
                            <%-- </div>--%>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /login -->
            <!-- The tab on top -->
            <div class="tab">
                <ul class="login">
                    <li class="left">&nbsp;</li>
                    <li id="toggle">
                        <a id="open" class="open" href="#">
                            <div class="icon" onClick="ShowUserDropdown(); return false;" style="cursor: pointer;">
                                <asp:Image ID="Usericon" runat="server" Width="35px" Height="35px" Style="border-radius: 50%;" />
                            </div>
                            <asp:Label ID="lblName" runat="server" /></a>
                        <a id="close" style="display: none;" class="close" href="#">Close </a>
                    </li>
                    <li class="right">&nbsp;</li>
                </ul>
            </div>
            <!-- / top -->
        </div>
        <div class="wrappar">
            <div class="top_sec">
                <div class="logo">
                </div>
                <div class="logo_left">
                    <div Class="Notify" >
                        <asp:Button ID="btnAuthorizations" runat="server"  Text="Button" Style="display: none" />
                        <asp:Button ID="btnNotifications" runat="server"  Text="Button" Style="display: none" />
                       
                        <div id="Div2" class="Reminder" runat="server"  onclick="ShowReminderDropdown(); return false;" style="cursor: pointer;">
                            <asp:Button ID="btnReminder" runat="server" Text="0" CssClass="value" Style="display: none" ToolTip="Reminders" />
                        </div>
                        <div id="divReminder" runat="server" class="Reminder_bg" style="margin-top: 40px; display: none; height: 200px;">
                            <div id="div5" class="Reminderbox" style="margin-top: 18px;margin-left: 85px">
                                <div class="top1">
                                    <div id="DIV7" class="close" runat="server" style="width: 100%;">
                                        <asp:Image ID="Image2" ImageUrl="~/images/Reminder2.png" runat="server" Style="height: 38px; width: 38px;" />
                                        <asp:Image ID="Image3" onclick="HideReminderDropdown(); return false;" ImageUrl="~/images/close1.png" runat="server" Style="height: 28px; width: 28px;margin-left: 177px;cursor: pointer;padding-bottom: 6px;" />
                                    </div>
                                     <div class="password_left" style="border: none; height: 180px;overflow: auto;margin-left:6px;width: 237px;">
                                               
                                    <ul id="ulReminder" style="width: 97%; margin-top: -4px">
                                    </ul>
                                          </div>
                                </div>
                            </div>
                        </div>
                        <div id="Div3" class="Authorization" runat="server" onclick="ShowNotificationDropdown();return false;" style="cursor: pointer;">
                            <%--<asp:Button Text="0" ID="btnauthorization" runat="server" ToolTip="Authorizations" Style="display: none" CssClass="value" />--%>
                            <asp:Button Text="0" ID="btnnoti" runat="server" ToolTip="Notification" CssClass="value" Style="width: 24px; height: 24px;cursor:pointer; border-radius:10px;" OnClientClick="ShowNotificationDropdown(); return false;" />
                                </div>
                            <div id="divNotificationDropdown" runat="server" class="Reminderbox" style="margin-top: 12px; display: none; height: 400px; width: 410px; margin-left: -350px;margin-right: 22px;">
                                        <div class="last_login_bg">
                                            <div class="change_logout6" style="margin-left: 285px;"></div>

                                        </div>
                                        <div class="pass_word" style="padding: 0px 0px 0px -30px">
                                             <div id="DIV1" class="close" runat="server" style="width: 100%;margin-top: 5px;">
                                                 <asp:Image ID="Image4" ImageUrl="~/images/Notification2.png" runat="server" Style="height: 38px; width: 38px;margin-left: 10px;" />
                                        <asp:Image ID="Image1" onclick="HideNotificationDropdown(); return false;" ImageUrl="~/images/close1.png" runat="server"  Style="height: 28px; width: 28px;margin-left: 220px;cursor: pointer;float: right;padding-right: 4px;" />
                                    </div>
                                            <div class="password_left" style="border: none; height: 346px;overflow: auto;margin-left:6px;width: 401px;">
                                               
                                                  <li id="unames"></li>
                                            </div>
                                        </div>
                                    </div>
                      
                       <%-- <div id="Div4"  class="Notification" runat="server" onClick="Notifications();return false;" style="cursor: pointer;display:none">
                            <asp:Button Text="0" ID="btnNotification" runat="server" ToolTip="Notifications" Style="display: none" CssClass="value" />
                        </div>--%>
                    </div>
            </div>
            <div class="logo_left4">
                <div class="logo_name">
                </div>
                <div class="menu">
                    <div class="menu2">

                        <li id="navmenu"></li>
                        
                        

                    </div>
                </div>
            </div>
            <div class="clear"></div>
            <div class="bottom2">

                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div>
                                    <iframe name="myIframe" id="myIframe" class="iframe" style="overflow:hidden;"   runat="server" width="100%"    frameborder="0"  ></iframe>
                                </div>
                                <asp:HiddenField ID="hfUser" runat="server" />
                                <asp:HiddenField ID="hfClient" runat="server" />
                                <asp:HiddenField ID="hfUrlUser" runat="server" />
                                <asp:HiddenField ID="hfUrlClient" runat="server" />
                                <asp:HiddenField ID="hfUserType" runat="server" />
                                <asp:HiddenField ID="hfUserName" runat="server" />
                                <asp:HiddenField ID="HdValuestore" runat="server" />
                                    <asp:HiddenField ID="hfSEC01" runat="server" />
                                    <asp:HiddenField ID="hfSEC02" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                <div id="slider" class="btn3_left" style="">
                    <div class="logo2" style="margin: 9px 0px 0px 23px; width: 200px;">
                    </div>
                    <div class="menu_left">
                        <asp:ImageButton ID="trigger"  runat="server" OnClientClick="return false;" Width="30px" Height="60px"  ImageUrl="~/images/FreequentMenu.png" CssClass="imgPopupMaster" />
                    </div>
                    <div class="middle">
                        <div class="slider-nav-area">
                            <ul id="navigation">
                            </ul>
                        </div>
                          <%--  <div>--%>
                                <%--<ul id="navigation">--%>
                               
                             <%--<iframe name="myIframe" id="Iframe1" src="SlidingMenu.aspx"  style="width: 241px; margin-top: 130px; overflow-y: auto; overflow-x: hidden; height: 213px; border: none;" runat="server"></iframe>--%>
<%--</ul>
                                </div>
                        --%>
                                     <%-- <iframe name="myIframe" id="myIframe11" src="Common/SlidingMenu.aspx" style="width: 241px; margin-top: 130px; overflow-y: auto; overflow-x: hidden; height: 413px; border: none;" runat="server"></iframe>--%>

                       
                    </div>
                   
               


            </div>
        </div>
        </div>

        <div class="clear"></div>
        <div class="clear"></div>
        <div id="divCompany" runat="server" style="display: none; width: 400px; height: 152px; background-color: white; position: fixed; margin-top: -350px; margin-left: 500px; box-shadow: 5px 5px 60px #264A6E; border: solid; border-color: #0B2D56;">
            <asp:HiddenField ID="hfCompany" runat="server" />
            <div style="width: 100%; background-color: #0B2D56; height: 25px;">
                <asp:Label ID="lblCmp" runat="server" CssClass="label" Text="Company" Font-Bold="true" Width="80%" Height="25px" ForeColor="White" Style="text-align: center; margin-top: 0px; margin-left: 25px;" Font-Names="verdana" Font-Size="12pt"></asp:Label>
                <asp:ImageButton ID="imgbtnClose" runat="server" Width="25px" Height="23px" Style="float: right;" ToolTip="Close" ImageUrl="~/images/cross.png" OnClientClick="CloseCompany();return false;" />
            </div>
            <div style="height: 125px; overflow: auto;">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="ID_Company" GridLines="None" Width="100%"  ShowHeader="false">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lblgvDocNo" runat="server" Style="color: #0B2D56;" Width="100%" CommandArgument='<%# Bind("ID_Company") %>' CommandName='<%# Bind("CompanyName") %>' Text='<%# Bind("CompanyName") %>' TabIndex="5"  Font-Underline="false" Font-Names="verdana" Font-Size="10pt"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="footer" style="position: fixed;">
            <%--<asp:LinkButton ID="lblComapnay" Text="User Code" runat="server" Font-Underline="false" Style="cursor: pointer; width: 500px; position: absolute; color: white; margin-left: 10px;" OnClientClick="OpenCompany(); return false;" />--%>
            <div class="sitemap" style="float: right;">
                Powered By <span class="site_map2">
                    <img src='<%=ResolveUrl("images/footer_bg.jpg") %>' alt="" align="absmiddle" /></span>
            </div>
        </div>
                 <asp:Button Text="text" ID="btnChange" runat="server" Style="display: none;" OnClick="btnChange_Click" />
                  <li>
                <asp:Button ID="btnImage" runat="server" CausesValidation="false" Text="Hide" Style="display: none;"   OnClick="btnImage_Click"/>
                                    </li>
        <div class="site_map2"></div>
        <div class="clear"></div>
    </form>
</body>
    


         
</html>
