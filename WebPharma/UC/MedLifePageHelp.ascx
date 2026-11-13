<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MedLifePageHelp.ascx.cs" Inherits="UC_MedLifePageHelp" %>
  <link rel="stylesheet" href="Demo/dist/jBox.all.css"/>
<style>
    
/* Target elements */

/*.targets-wrapper {
  margin: 0 -5px;
  padding: 10px 0 20px;
}

.targets-wrapper:after,
.targets-wrapper:before {
  content: " ";
  display: table;
}

.targets-wrapper:after {
  clear: both;
}

.target,
.target-click,
.target-notice {
  cursor: default;
  font-size: 13px;
  line-height: 52px;
  height: 52px;
  border-radius: 2px;
  border: 1px solid #e2e2e2;
  text-align: center;
  background: #fafafa;
  position: relative;
  text-transform: uppercase;
  float: left;
  width: calc(25% - 10px);
  margin: 5px;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}

.target-click,
.target-notice {
  cursor: pointer;
}

.target.active,
.target-click.active,
.target-notice.active {
  color: #07d;
}

.demo-img {
  float: left;
  width: calc(25% - 10px);
  margin: 5px;
}

.demo-img > img {
  border: 4px solid #eee;
  border-radius: 2px;
  width: 100%;
  height: auto;
  filter: grayscale(100%);
  transition: filter .2s, border-color .2s;
}

.demo-img:hover > img {
  filter: none;
  border-color: #ddd;
}

@media (max-width: 768px) {

  .target,
  .target-click,
  .target-notice {
    width: calc(50% - 10px);
  }

  .demo-img > img {
    border-width: 2px;
  }

}*/

</style>

  <img id="Tooltip8"  alt="" src="images/Help.png"  style="display:none" class="imagebuttonStyle"  />

<script src="Demo/dist/jBox.all.js"></script>
<script>
    //$(document).ready(function () {

    //    try {
           
    //        var path = window.location.pathname;
    //        var page = path.split("/").pop();
            
    //        $.ajax({
    //            type: "POST",
    //            url: 'Textile.asmx/GetHelpContents',
    //            data: "{'strpath':'" + page + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (data) {
    //                if (data.d != '') {
    //                    var help11 = (data.d);

    //                   new jBox('Tooltip', {
    //                       attach: '#Tooltip8',
    //                       theme: 'TooltipBorder',
    //                       trigger: 'click',
    //                       width: 300,
    //                       height: ($(window).height() - 160),
    //                       adjustTracker: true,
    //                       closeOnClick: 'body',
    //                       closeOnEsc: true,
    //                       animation: 'move',
    //                       position: {
    //                           x: 'right',
    //                           y: 'center'
    //                       },
    //                       outside: 'x',
    //                       content: help11,
    //                       onOpen: function () {
    //                           // this.source.addClass('active').html('Now scroll');
    //                       },
    //                       onClose: function () {
    //                           // this.source.removeClass('active').html('Click me');
    //                       }
    //                   });

    //                }
    //            },
    //            error: function (error) {

    //            }
    //        });
           
          
          

    //    }
    //    catch (ex) {
    //        alert(ex);
    //    }
    //});




</script>



<script>
    function MyFunction() {
        try {

            var path = window.location.pathname;
            var page = path.split("/").pop();

            $.ajax({
                type: "POST",
                url: 'Textile.asmx/GetHelpContents',
                data: "{'strpath':'" + page + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != '') {
                        var help11 = (data.d);

                        new jBox('Tooltip', {
                            attach: '#Tooltip8',
                            theme: 'TooltipBorder',
                            trigger: 'click',
                            width: 300,
                            height: ($(window).height() - 160),
                            adjustTracker: true,
                            closeOnClick: 'body',
                            closeOnEsc: true,
                            animation: 'move',
                            position: {
                                x: 'right',
                                y: 'center'
                            },
                            outside: 'x',
                            content: help11,
                            onOpen: function () {
                                // this.source.addClass('active').html('Now scroll');
                            },
                            onClose: function () {
                                // this.source.removeClass('active').html('Click me');
                            }
                        });

                    }
                },
                error: function (error) {

                }
            });




        }
        catch (ex) {
            alert(ex);
        }
    }
    //Calling MyFunction when document is ready (Page loaded first time)
    $(document).ready(MyFunction);
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        // re-bind your jQuery events here
        MyFunction();
    });
    //Calling MyFunction when the page is doing postback (asp.net)
    //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(MyFunction);
</script>