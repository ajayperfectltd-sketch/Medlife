<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SlidingMenu.aspx.cs" Inherits="Common_SlidingMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script src="../js/jquery-1.10.2.js"></script>
    <script src="../js/toastr.js"></script>
    <script src="../js/Custom.js"></script>
    <script src="../js/slidereveal.js"></script>
    <script src="../js/ddmenu.js" type="text/javascript"></script>
    <script src="../js/Company.js" type="text/javascript"></script>

   <%-- <script src="../js/formcheck/lang/en.js"></script>--%>

    <script src="../js/slide.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <script src="../js/jquery-1.11.1.min.js"></script>
    <script src="../js/jquery-ui.min.js"></script>
    <script src="../js/alertify.min.js"></script>

    <%-- ForLeft Menu --%>
    
   
 
    <script src="../js/jquery.easing.js"></script>
    <script src="../js/jquery.dimensions.js"></script>

    <script src="../js/jquery.accordion.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            try {
                $('#navigation').accordion({
                    //active: false,
                    //header: '.head',
                    //navigation: true,
                    //event: 'click',
                    //fillSpace: false,
                    //animated: 'easeslide'
                });
                alert('sdasd');
                // FreequentMenuContent();
                CallMenuLeft('xLfk1UdRNIq6l69CF5DBZQ%3d%3d', 'tVU6dWv%2fCCVyUSkCeUlcfg%3d%3d');
            }
            catch (ex) {
                alert(ex);
            }
        });



    </script>
    <style>

   #navigation {
    margin: 0px;
    padding: 0px;
    text-indent: 0px;
    background-color: #102C52;
    width: 230px;
    height: 500px;
    overflow: hidden;
  
}

    #navigation a.head {
        cursor: pointer;
        background: #102C52 ;
        height: 28px;
        font-family: Verdana;
        color: #FFFFFF;
        display: block;
        font-weight: bold;
        margin: 0px;
        font-size: 13PX;
        padding-top: 10px;
        text-indent: 14px;
        text-decoration: none;
        background-image: url(../../images/SliderMn.png);
        background-repeat: repeat;
    }
        #navigation a.head:hover 
    {
        color: #02B8F0;
    }

    #navigation a.selected {
        background-image: url(../../images/SliderMn.png);
         background-repeat: repeat;
    }

    #navigation a.current {
        background-color: #FFFF99;
    }

    #navigation ul {
        border-width: 0px;
        font-size: 14px;
        margin: 0px;
        max-height: 300px;
        height: auto;
        overflow-y: scroll;
        text-indent: 0px;
        background-color: #102C52;
        padding-left: 17px;
    }

    #navigation li {
        list-style: none outside none;
        display: inline;
    }

        #navigation li li a {
           
            color: #02B8F0;
            display: block;
            padding-top: 10px;
            padding-bottom: 10px;
            text-indent: 10px;
            text-decoration: none;
            font-family: Verdana;
            font-size: 13px;
        }

        #navigation li li a:hover {
            color: #fff;
            font-weight:600;

        }

        #navigation li li a.active {
            font-weight:600;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <div class="left_navi">
                <div style="height: auto; margin-bottom: 1em;overflow-y: auto; overflow-x: hidden;">
                    <ul id="navigation">
                       
                         </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
