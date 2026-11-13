$(document).ready(function () {

    try {
        var help = $('#hfHelpContent').val();

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
            content: help,
            onOpen: function () {
                // this.source.addClass('active').html('Now scroll');
            },
            onClose: function () {
                // this.source.removeClass('active').html('Click me');
            }
        });


    }
    catch (ex) {
        alert(ex);
    }
});