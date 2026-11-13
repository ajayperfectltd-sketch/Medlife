//Author: Remin


var ctrContainer;
function setKeynavigation(table) {
   // alert('g');
    ctrContainer = table;
    var ctrlKeyDown = "doNavigation(event,id);";
    $(ctrContainer).find("input[type='submit'],input[type='image'],input[type='button'],input[type='text'],input[type='password'],textarea,select").each(function (index, value) {
        if ($(value).attr("type") == "text") {
           // alert('g');
            $(value).attr("autocomplete", "off");
        }
        var keyDown = $(value).attr("onkeydown");
        if (keyDown == null) {
          //  alert('g1');
            $(value).attr("onkeydown", ctrlKeyDown);
        }
        else if (keyDown.indexOf(ctrlKeyDown) == -1) {
           // alert('g2');
            $(value).attr("onkeydown", $(value).attr("onkeydown") + ctrlKeyDown);
        }
    });
}

function doNavigation(event, id) {
    
    //For nvaigating using enter key
    var isOk = true;
    if (event.keyCode == 13) {
       // alert('h');
        var ctrlthis = $("#" + id);
        
        if (ctrlthis[0].tagName == "TEXTAREA") {
            isOk = false;
            var input = document.getElementById(id);
            var lines = input.value.split('\n');
            if (lines[lines.length-1] == '') {
                isOk = true;
            }
        }
        if (isOk) {
            if ((event.keyCode == 13 && event.shiftKey == false && $(ctrlthis).attr("type") != "submit" && $(ctrlthis).attr("type") != "button" && $(ctrlthis).attr("type") != "image") || (event.keyCode == 13 && event.ctrlKey == true)) {
                var inputs = $(ctrContainer).find("input[type='submit'],input[type='image'],input[type='button'],input[type='text'],input[type='password'],textarea,select");
                var idx = inputs.index(ctrlthis);
                if (idx == inputs.length - 1) {
                    inputs[0].select()
                }
                else {
                    for (var i = 1; i < inputs.length - 1; i++) {
                        var ctrlNew = inputs[idx + i];
                        if ($(ctrlNew).is(":visible") && $(ctrlNew).is(":enabled") /*&& ($(ctrlNew).attr("readonly") == null || $(ctrlNew).attr("readonly") == "")*/ && $(ctrlNew).attr("tabstop") != "true") {
                            ctrlNew.focus();
                            break;
                        }
                    }
                    if (inputs[idx + 1].type != "select-one") {
                        inputs[idx + 1].select();
                    }
                }
//                return false;
                event.preventDefault();
            }

            //For nvaigating using shift + enter key
            if (event.keyCode == 13 && event.shiftKey == true) {
                var inputs = $(ctrContainer).find("input[type='submit'],input[type='image'],input[type='button'],input[type='text'],input[type='password'],textarea,select");
                var idx = inputs.index(ctrlthis);
                for (var i = inputs.length - 1; i > 0; i--) {
                    var ctrlOld = inputs[idx - i];
                    if (idx > 0 && $(ctrlOld).is(":visible") && $(ctrlOld).is(":enabled") /*&& ($(ctrlOld).attr("readonly") == null || $(ctrlOld).attr("readonly") == "")*/ && $(ctrlOld).attr("tabstop") != "true") {
                        ctrlOld.focus();
                        if (inputs[idx - 1].type != "select-one") {
                            inputs[idx - 1].select();
                        }
                    }
                }
                event.preventDefault();
            }
        }
    }
}
