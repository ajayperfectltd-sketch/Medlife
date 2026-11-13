function GetKeyboardQty() {
    //Modified by Nithin Das H
    // var abc = event.target.id;


    //$("#hftempqty").val($("#" + abc).val());

    $('.qwerty:eq(1)').bind('visible.keyboard', function (e, keyboard, el) {
        $('.ui-keyboard-overlay').remove(); // remove overlay because clicking on it will close the keyboard... we set "openOn" to null to prevent closing.
    });


   
   

    $("#txtMppQuantity,#txtMppChangePriceSalesPrice,#txtmppDiscAmount,#txtmppDiscPercentage,#txtPaidAmount,#txtCardAmount,#txtCashAmount,#txtGroupOneVoucherAmount").keyboard({
        layout: 'custom',
        customLayout: {
            'default': [
                '1 2 3',
                '4 5 6',
                '7 8 9',
                '0 . {bksp} ',
                '{a} {c} '


            ]
        },
        position: {
            
            my: 'center top',
            at: 'center top'

        },
        restrictInput: true, // Prevent keys not in the displayed keyboard from being typed in
        useCombos: false,// don't want A+E to become a ligature
        preventPaste: true,  // prevent ctrl-v and right click
        autoAccept: true


    });
    // Custom: iPad by gitaarik
    // Console showing callback messages
    // ********************
    $('.ui-keyboard-input').bind('visible.keyboard hidden.keyboard beforeClose.keyboard accepted.keyboard canceled.keyboard', function (e, keyboard, el, status) {
       
        var c = $('#txtMppQuantity'),
            t = '<li><span class="keyboard">' + $(el).parent().find('h2').text() + '</span>';

        switch (e.type) {
            case 'visible': t += ' keyboard is <span class="event">visible</span>'; break;
            case 'hidden': t += ' keyboard is now <span class="event">hidden</span>'; break;
            case 'accepted': t += ' content "<span class="content">'
                + el.value + '</span>" was <span class="event">accepted</span>' + ($(el).is('[type=password]') ? ', yeah... not so secure :(' : '');
                QtyUpdate1($(el).attr('name'), el.value); break;
               
              //  if (el.value.length > 0 && el.value.length <=25) {
                   
                //}
                //else {
                    //  QtyUpdate1($(el).attr('name'), $("#hftempqty").val());
                    // $("#txtMppQuantity").val($("#hftempqty").val());
                    //toastr.error('Invalid Entry');
               // }

                
            case 'canceled': t += ' content was <span class="event ignored">ignored</span>'; break;
            case 'beforeClose': t += ' keyboard is about to <span class="event">close</span>, contents were <span class="event ' + (status ? 'accepted">accepted' : 'ignored">ignored') + '</span>'; break;
        }
        t += '</li>';
        c.append(t);
        if (c.find('li').length > 3) { c.find('li').eq(0).remove(); }


    });
    $('.ui-keyboard-input').addTyping();

    // simulate typing into the keyboard (\t = tab, \b = backspace, \r or \n = enter)
    $('#inter-type').click(function () {
        $('#inter').getkeyboard().reveal().typeIn("\tHell'o\bo \r\tWorld", 500);
        return false;
    });

    $('#meta-type').click(function () {
        var meta = $('#meta').getkeyboard();
        meta.reveal().typeIn('aBcD1112389\u2648\u2649', 700, function () { meta.accept();  });
        return false;
    });
    // Show code
    // ********************
    //$('h2 span').click(function () {
    //    var t = '<h2>' + $(this).parent().text() + ' Code</h2>' + $(this).closest('.block').find('.code').html();
    //    $('#showcode').html(t).show();
    //    $("#showcode .js").chili();
    //    $("#showcode .html").chili();
    //});

    // add tooltips
    // ********************


    // ********************
    // Extension demos
    // ********************

    // Set up typing simulator extension on ALL keyboards



    //  var Containerid = document.getElementsByName("default");

    // Autocomplete demo
    //var test = $("#" + abc).val();
    //$("#" + abc).val(test);
    // $("#txtnumpad").focus();
}
function QtyUpdate1(id, value) {

    try {
        
        if (id === "txtmppDiscAmount" || id === "txtmppDiscPercentage" || id == "txtPaidAmount" || id == "txtCardAmount" || id == "txtCashAmount") {
            var DiscAmnt;
            var Price;
            if (id == "txtmppDiscAmount") {
                var Price = $("#txtmppSalesPrice").val() == '' ? '0' : $("#txtmppSalesPrice").val();
                var DiscAmnt = value; //$("#txtmppDiscAmount").val() == '' ? '0' : $("#txtmppDiscAmount").val();

                Price = CheckDecimalpoints(Price);
                DiscAmnt = CheckDecimalpoints(DiscAmnt);

                if (parseFloat(DiscAmnt) > parseFloat(Price)) {
                    DiscAmnt = 0;
                    $("#txtmppDiscPercentage").val(0);
                    $("#txtmppDiscAmount").val(0);
                }
                else if (parseFloat(DiscAmnt) > 0) {
                    var DiscPer = $("#txtmppDiscPercentage").val() == '' ? '0' : $("#txtmppDiscPercentage").val();
                    var Dis = (parseFloat(DiscAmnt) * 100) / parseFloat(Price);
                    $("#txtmppDiscPercentage").val(Dis);
                }
                else {
                    $("#txtmppDiscPercentage").val(0);
                    $("#txtmppDiscAmount").val(0);
                }

            }
            else if (id == "txtmppDiscPercentage") {
                var Price = $("#txtmppSalesPrice").val() == '' ? '0' : $("#txtmppSalesPrice").val();
                var DiscPerct = value; //$("#txtmppDiscPercentage").val() == '' ? '0' : $("#txtmppDiscPercentage").val();


                if (parseFloat(DiscPerct) > 100) {
                    DiscPerct = 0;
                    $("#txtmppDiscPercentage").val(0);
                    $("#txtmppDiscAmount").val(0);
                }
                else if (parseFloat(DiscPerct) > 0) {
                    var DiscAmnt = (parseFloat(DiscPerct) * parseFloat(Price) / 100);

                    $("#txtmppDiscAmount").val(DiscAmnt);
                }
                else {
                    $("#txtmppDiscPercentage").val(0);
                    $("#txtmppDiscAmount").val(0);
                }

            }
            else if (id == "txtPaidAmount") {
             
                var cashAmount = $("#txtCashAmount").val() == '' ? '0' : $("#txtCashAmount").val();
                var paidAmount = value;

                //alert(value);
                //cashAmount = CheckDecimalpoints(cashAmount);
                //paidAmount = CheckDecimalpoints(paidAmount);
                if (parseFloat(paidAmount) >= parseFloat(cashAmount)) {
                    $("#txtBalanceAmount").val((parseFloat(paidAmount) - parseFloat(cashAmount)));
                }
                else {
                    $("#txtBalanceAmount").val("0");
                }
            }
            else if (id == "txtCardAmount") {
               
                CalculateCashCardKeypad(id);
            }
            else if (id == "txtCashAmount") {
                 CalculateCardCashKeypad(id);
            }
            

        }
    }
    catch (e) {
        alert(e);
    }
}







//    Price = CheckDecimalpoints(Price);
//    DiscAmnt = CheckDecimalpoints(DiscAmnt);

//    if (parseFloat(DiscAmnt) > parseFloat(Price)) {
//        DiscAmnt = 0;
//        $("#txtmppDiscPercentage").val(0);
//        $("#txtmppDiscAmount").val(0);
//        $(this).val(0);
//    }
//    else if (parseFloat(DiscAmnt) > 0) {
//        var DiscPer = $("#txtmppDiscPercentage").val() == '' ? '0' : $("#txtmppDiscPercentage").val();
//        var Dis = (parseFloat(DiscAmnt) * 100) / parseFloat(Price);
//        $("#txtmppDiscPercentage").val(Dis);
//    }
//    else {
//        $("#txtmppDiscPercentage").val(0);
//        $("#txtmppDiscAmount").val(0);
//        $(this).val(0);
//    }







//var SellingRate = txtQty.replace("txtQty_", "hfSellingRate_");
//var divTotalamt = txtQty.replace("txtQty_", "ItemTotal_");
//
//var inc = value;
//inc += 1;
//    alert(SellingRate);
//var SellingRate = $("#" + SellingRate).val();
//var SellingRate = parseFloat(lblSellRate.val() == '' ? '0' : SellingRate.val());
//var Total = inc * SellingRate;

// $("#" + txtQty).val(inc)
// $("#" + divTotalamt).text(Total);
// document.getElementById(divTotalamt).text = Total;
//total();
// return false;






