function CustomerBar(type, customer,container) {

    var dt = $("#hdfPeriod").val();
   

    if (type == "bar") {

        var chart = new Highcharts.Chart({
            chart: {
                type: 'column',
                renderTo: container// 'divCustomer'

            },
            title: {
                text: 'Customer Performance'
            },
            subtitle: {
                text: dt
            },
            credits: {
                enabled: false
            },
            plotOptions: {
                column: {

                    colorByPoint: true

                },
                series: {
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {

                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Customer=cust";

                            }
                        }
                    }
                }
            },
            xAxis: {
                type: 'category',
                labels: {
                    rotation: -45,
                    align: 'right',
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Amount of sale %'
                }
            },
            legend: {
                enabled: false
            },
            tooltip: {
                pointFormat: 'Sale %:<b>{point.y:.2f}%</b><br/> Amount:<b>{point.amount:.3f}'
            },
            series: [{
                name: 'customer',
                data: customer,
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#fcfdd6',
                    align: 'right',
                    x: 4,
                    y: 10,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }]
        });
    }
    else {
        var chart = new Highcharts.Chart({
            chart: {
                type: 'pie',
                renderTo: container,
                options3d: {
                    enabled: true,
                    alpha: 45
                }
            },
            title: {
                text: 'Customer Performance'
            },
            subtitle: {
                text: dt
            },
            credits: {
                enabled: false
            },
            tooltip: {
                pointFormat: 'Sale %:<b>{point.y:.2f}%</b><br/> Amount:<b>{point.amount:.3f}'
            },
            plotOptions: {
                pie: {
                    innerSize: 100,
                    depth: 45,

                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {

                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Customer=Cust";
                            }
                        }
                    }

                }

            },
            series: [{
                name: 'Customer',
                data: customer
            }]
        });

    }
}
/********Sales****/
function SalesBarOrPie(type, eventsInline,container) {




    var dt = $("#hdfPeriod").val();


    if (type == 'pie') {
        var chart = new Highcharts.Chart({
            chart: {
                type: 'pie',
                renderTo: container,
                options3d: {
                    enabled: true,
                    alpha: 45,
                    beta: 0
                }
            },
            title: {
                text: 'Store Wise Sales'
            },
            subtitle: {
                text: dt
            },
            credits: {
                enabled: false
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.2f}%</b><br/>Amount :<b>{point.amount:.2f} '
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    depth: 35,
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}'
                    },
                    point: {
                        events: {
                            click: function () {
                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Store=Store";
                            }
                        }
                    }
                }


            },
            series: [{
                type: 'pie',
                name: 'Store Sale %',
                data: eventsInline
            }]
        });
    }
    else {
        var chart = new Highcharts.Chart({
            chart: {
                renderTo: container,
                type: 'column'
            },
            title: {
                text: 'Store Wise Sales'
            },
            subtitle: {
                text: dt
            },
            credits: {
                enabled: false
            },
            plotOptions: {
                column: {

                    colorByPoint: true
                },
                series: {
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {
                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Store=Store";
                            }
                        }
                    }
                }
            },
            xAxis: {
                type: 'category',
                labels: {
                    rotation: -45,
                    align: 'right',
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Amount of sales %'
                }
            },
            legend: {
                enabled: false
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.y:.2f}%</b><br/>Amount :<b>{point.amount:.2f} '
            },
            series: [{
                name: 'Sales',
                data: eventsInline,
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#fcfdd6',
                    align: 'right',
                    x: 4,
                    y: 10,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }]
        });

    }

}

/********Items****/
function ItemBarOrPie(type, items,container) {
    var dt = $("#hdfPeriod").val();
 
    if (type == 'pie') {

        var chart = new Highcharts.Chart({
           
            chart: {
                size:'100%',
                type: 'pie',
                renderTo: container,
                options3d: {
                    enabled: true,
                    alpha: 45,
                    beta: 0
                }
            },
            title: {
                text: 'Top Selling Items'
            },
            subtitle: {
                text: dt
            },
            credits: {
                enabled: false
            },
            tooltip: {
                pointFormat: 'Total Sale: <b>{point.y:.2f}</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    depth: 35,
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}'
                    },
                    point: {
                        events: {
                            click: function () {
                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&item=Item";

                            }
                        }
                    }
                }


            },
            series: [{
                type: 'pie',
                name: 'Item',
                data: items
            }]
        });
    }
    else {

        var chart = new Highcharts.Chart({
            chart: {
              
                renderTo: container,
                type: 'column'

            },
            title: {
                text: 'Top Selling Items'
            },
            subtitle: {
                text: dt
            },
            credits: {
                enabled: false
            },
            plotOptions: {
                column: {
                    size: '100%',
                    colorByPoint: true
                },
                series: {
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {
                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&item=Item";
                            }
                        }
                    }
                }
            },
            xAxis: {
                type: 'category',
                labels: {
                    rotation: -45,
                    align: 'right',
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'No of sale'
                }
            },
            legend: {
                enabled: false
            },
            tooltip: {
                pointFormat: 'Total Sale:<b>{point.y:.2f}</b>'
            },
            series: [{
                name: 'Items',
                data: items,
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#fcfdd6',
                    align: 'right',
                    x: 4,
                    y: 10,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }]
        });

    }
}
/********MinStock****/
function MinStockBarorpie(type, items,container1,container2) {

    if (type == 'bar') {

        $("#"+container2).attr('style', 'display:none;');
        $("#" + container1).attr('style', 'display:block;');
        if (container1 == 'divPopup')
            $("#divPopup").attr("style", "display:block;width:1437px;height:640px;");
            
        var chart = new Highcharts.Chart({
            chart: {
                renderTo: container1,
                type: 'column'
            },
            title: {
                text: 'Minimum stock Variation'
            },
            subtitle: {
                text: 'Items'
            },
            credits: {
                enabled: false
            },
            xAxis: {
                type: 'category',
                labels: {
                    rotation: -45,
                    align: 'right',
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'

                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Stock Variance %'
                }
            },
            legend: {
                enabled: false
            },
            tooltip: {
                pointFormat: '<b>{point.categories}</b><br/>Batch:{point.batch}<br/>SV:<b>{point.y:.2f}%</b><br/>ML:{point.Min}<br/>CS:{point.CS}'
            },
            plotOptions: {
                column: {
                    color: '#FF0000'


                },
                series: {
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {
                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Stock=stock";
                            }
                        }
                    }
                }
            },
            series: [{
                name: 'Items',
                data: items,
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#000000',
                    align: 'right',
                    x: 4,
                    y: 10,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }]
        });
    }
    else {
     
        $("#" + container1).attr('style', 'display:none; ');
        if (container1 == 'divPopup')
            $("#divPopup").attr("style", "display:none;width:1437px;height:640px;");
        $("#"+container2).attr('style', 'display:block;');
    }
}
/****Credit Supplier**/
function CreditSupplier(type, supplier,Container) {

    var dt = $("#hdfPeriod").val();

    if (type == "bar") {

        var chart = new Highcharts.Chart({
            chart: {
                type: 'column',
                renderTo: Container

            },
            title: {
                text: 'Credit Supplier'
            },
            subtitle: {
                text: dt
            },
            credits: {
                enabled: false
            },
            plotOptions: {
                column: {

                    colorByPoint: true

                },
                series: {
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {
                                return false;
                              //  window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Supplier=Supplier";

                            }
                        }
                    }
                }
            },
            xAxis: {
                type: 'category',
                labels: {
                    rotation: -45,
                    align: 'right',
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Credit Amount %'
                }
            },
            legend: {
                enabled: false
            },
            tooltip: {
                pointFormat: 'CR Amount %:<b>{point.y:.2f}%</b><br/> Amount:<b>{point.amount:.3f}</b> '
            },
            series: [{
                name: 'supplier',
                data: supplier,
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#fcfdd6',
                    align: 'right',
                    x: 4,
                    y: 10,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }]
        });
    }
    else {
        var chart = new Highcharts.Chart({
            chart: {
                type: 'pie',
                renderTo: Container,
                options3d: {
                    enabled: true,
                    alpha: 45
                }
            },
            title: {
                text: 'Credit Supplier'
            },
            subtitle: {
                text: dt
            },
            credits: {
                enabled: false
            },
            tooltip: {
                pointFormat: 'CR Amount %:<b>{point.y:.2f}%</b><br/> Amount:<b>{point.amount:.3f}</b> '
            },
            plotOptions: {
                pie: {
                    innerSize: 100,
                    depth: 45,

                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {

                                /// window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Supplier=Supplier";
                                return false;
                            }
                        }
                    }

                }

            },
            series: [{
                name: 'Supplier',
                data: supplier
            }]
        });

    }
}
/****Credit Customer**/
function CreditCustomer(type, creditcust,container) {
    var dt = $("#hdfPeriod").val();

    if (type == 'pie') {

        var chart = new Highcharts.Chart({
            chart: {
                type: 'pie',
                renderTo: container,
                options3d: {
                    enabled: true,
                    alpha: 45,
                    beta: 0
                }
            },
            title: {
                text: 'Credit Customer'
            },
            subtitle: {
                text: dt
            },
            credits: {
                enabled: false
            },
            tooltip: {
                pointFormat: ' CR Amount %:<b>{point.y:.2f}%</b><br/> Amount:<b>{point.amount:.3f}'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    depth: 35,
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}'
                    },
                    point: {
                        events: {
                            click: function () {
                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&CRCUS=CRCUS";

                            }
                        }
                    }
                }


            },
            series: [{
                type: 'pie',
                name: 'Customer',
                data: creditcust
            }]
        });
    }
    else {

        var chart = new Highcharts.Chart({
            chart: {
                renderTo: container,
                type: 'column'

            },
            title: {
                text: 'Credit Customer'
            },
            subtitle: {
                text: dt
            },
            credits: {
                enabled: false
            },
            plotOptions: {
                column: {
                    colorByPoint: true
                },
                series: {
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {
                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&CRCUS=CRCUS";
                            }
                        }
                    }
                }
            },
            xAxis: {
                type: 'category',
                labels: {
                    rotation: -45,
                    align: 'right',
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Cr Amount'
                }
            },
            legend: {
                enabled: false
            },
            tooltip: {
                pointFormat: ' CR Amount %:<b>{point.y:.2f}%</b><br/> Amount:<b>{point.amount:.3f}'
            },
            series: [{
                name: 'customer',
                data: creditcust,
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#fcfdd6',
                    align: 'right',
                    x: 4,
                    y: 10,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }]
        });

    }
}
/****DbConnection****/
function GetComparableDataWeb(type, json, title, yaxis, container, main) {
    // var customer = eval($("#hdfCustomer").val());

    var qtsring ="";

    var dt = $("#hdfPeriod").val();
    if (main == "Sales") {
        qtsring = title == "Store Wise" ? "SASTR" : (title == "Item Wise"?"SAITM":"SACUS")
    }
    else {
         qtsring = title == "Store Wise" ? "PURSTORE" : "PURITM";
    }
    if (type == "bar") {

        var chart = new Highcharts.Chart({
            chart: {
                type: 'column',
                renderTo: container

            },
            title: {
                text: main
            },
            subtitle: {
                text: title + ' ' + main
            },
            credits: {
                enabled: false
            },
            plotOptions: {

                series: {
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {

                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val()+ "&" +qtsring+"=" +main;

                            }
                        }
                    }
                }
            },
            xAxis: {
                type: 'category',
                labels: {
                    rotation: -45,
                    align: 'right',
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: yaxis
                }
            },
            legend: {
                enabled: true, reversed: true
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px"><b>' + title + '</b></span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0;font-size:12px; ">{point.name}: </td>' +
            '<td style="padding:0;font-size:12px;">{point.Amount:.1f}({point.y:.2f}%)</td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            series: json
        });


        //var json = [{ "name": "TRUCKLOAD", "y": 32323348.4 }, { "name": "NON-SYNDICATED:QUALITATIVE & QUANTITATIVE", "y": 23270306.54 }, { "name": "SAMPLING & EVENTERVICES", "y": 18924795.75 }];


    }

}

/***DbConnection***/
function GetGraphDetail(ChartType) {
  
    var stDate = new Date().format("dd/MM/yyyy");
    var clientid = $("#hfClient").val();
    var dtFromDate = $("#txtFromDate").val();
    var dtToDate = $("#txtToDate").val();  
    var Period = $('#ddlType').find("option:selected").val();
    var storeid = $('#ddlItemStore').find("option:selected").val();
    if (Period != '6') {
        dtFromDate = stDate;
    }   
    var eventsInline;
    $.ajax({
        type: "POST",
        url: "Textile.asmx/GetGraphDetail",
        data: "{'ChartType':'" + ChartType + "','Period':'" + Period + "','stDate':'" + dtFromDate + "','endDate':'" + dtToDate + "','ClientId':'" + clientid + "','storeid':'" + storeid + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {          
            if (ChartType == "NOSL") {
                $("#hdfPeriod").val(data.d);               
                
            }
            else {
                eventsInline = eval(data.d);
            }
          

            if (Period == 1) {
                var Newdate = new Date(), y = Newdate.getFullYear(), m = Newdate.getMonth(); d = Newdate.getDate();
                var firstDay = new Date(y, m, d - 7).format('dd/MM/yyyy');
                var lastDay = new Date(y, m, d).format('dd/MM/yyyy');
                $("#hdfPeriod").val(firstDay + "-" + lastDay);
            }
            if (Period == 2) {
                var Newdate = new Date(), y = Newdate.getFullYear(), m = Newdate.getMonth(); d = Newdate.getDate();
                var firstDay = new Date(y, m, d - 14).format('dd/MM/yyyy');
                var lastDay = new Date(y, m, d).format('dd/MM/yyyy');
                $("#hdfPeriod").val(firstDay + "-" + lastDay);
            }
            if (Period == 3) {
                var Newdate = new Date(), y = Newdate.getFullYear(), m = Newdate.getMonth(); d = Newdate.getDate();
                var firstDay = new Date(y, m-3, d).format('dd/MM/yyyy');
                var lastDay = new Date(y, m, d).format('dd/MM/yyyy');
                $("#hdfPeriod").val(firstDay + "-" + lastDay);
            }
            if (Period == 4) {
                var Newdate = new Date(), y = Newdate.getFullYear(), m = Newdate.getMonth(); d = Newdate.getDate();
                var firstDay = new Date(y, m - 6, d).format('dd/MM/yyyy');
                var lastDay = new Date(y, m, d).format('dd/MM/yyyy');
                $("#hdfPeriod").val(firstDay + "-" + lastDay);
            }
            if (Period == 5) {
                var Newdate = new Date(), y = Newdate.getFullYear(), m = Newdate.getMonth(); d = Newdate.getDate();
                var firstDay = new Date(y-1, m , d).format('dd/MM/yyyy');
                var lastDay = new Date(y, m, d).format('dd/MM/yyyy');
                $("#hdfPeriod").val(firstDay + "-" + lastDay);
            }
         
            switch (ChartType) {
                
                case 'ITM':
                    if ($("#divItem").length > 0) {
                        $("#hdfCounter").val(data.d);
                        if ($("#cbxBar2").hasClass("ActiveChek")) {
                            ItemBarOrPie('bar', eventsInline, 'divItem');
                        }
                        else {
                            ItemBarOrPie('pie', eventsInline, 'divItem');
                        }
                    }
                    break;
                case 'CUST':
                    if ($("#divCustomer").length > 0) {
                        $("#hdfCustomer").val(data.d);
                        if ($("#cbxBar1").hasClass("ActiveChek")) {
                            CustomerBar('bar', eventsInline, 'divCustomer');
                        }
                        else {
                            CustomerBar('pie', eventsInline, 'divCustomer');
                        }
                    }
                    break;
                case 'SLMW':                  
                    if ($("#divSupplier").length > 0) {
                        $("#hdfSalesMan").val(data.d);                      
                        if ($("#cbxBar8").hasClass("ActiveChek")) {
                            GetSupplierDetails('bar', eventsInline, 'divSupplier');
                        }
                        else {
                            GetSupplierDetails('pie', eventsInline, 'divSupplier');
                        }
                    }
                    break;
                case 'STR':
                    if ($("#divSale").length > 0) {
                        $("#hdfSale").val(data.d);
                        if ($("#cbxPie3").hasClass("ActiveChek")) {
                            SalesBarOrPie('pie', eventsInline, 'divSale');
                        }
                        else {
                            SalesBarOrPie('bar', eventsInline, 'divSale');
                        }
                    }
                    break;
                case 'CRPW':
                    if ($("#divCreditSupplier").length > 0) {

                        $("#hdfCrPurchase").val(data.d);
                        if ($("#cbxBar5").hasClass("ActiveChek")) {
                            CreditSupplier('bar', eventsInline, 'divCreditSupplier');
                        }
                        else {
                            CreditSupplier('pie', eventsInline, 'divCreditSupplier');
                        }
                    }
                    break;
                case 'CRCW':
                    if ($("#divCreditCustomer").length > 0) {
                        $("#hdfCrCustomer").val(data.d);
                        if ($("#cbxBar6").hasClass("ActiveChek")) {
                            CreditCustomer('bar', eventsInline, 'divCreditCustomer');
                        }
                        else {
                            CreditCustomer('pie', eventsInline, 'divCreditCustomer');
                        }
                    }
                    break;
                case 'MSW':
                    if ($("#divMinStock").length > 0) {
                        $("#hdfMinStock").val(data.d);
                        if ($("#cbxPie4").hasClass("ActiveChek")) {
                            MinStockBarorpie('pie', eventsInline, 'divMinStock', 'divRepeater');
                        }
                        else
                            MinStockBarorpie('bar', eventsInline, 'divMinStock', 'divRepeater');
                        BindjsontoHmlTable(eventsInline);
                    }
                    break;
                  

            }
        },
        error: function (error) {
        }
    });
    return false;


}


function GetPurchaseDetails() {
    var stDate = new Date().format("dd/MM/yyyy");
    var clientid = $("#hfClient").val();

    // var FK_Store = $('#ddlItemStore').find("option:selected").val();
    var dtFromDate = $("#txtPurStFromDate").val();
    var dtToDate = $("#txtPurStEnDate").val();
    var titile = "";
    var yaxis = "";
    var CompareFrom = $("#txtPurFrmDate").val();
    var dtCompareTo = $("#txtPurToDate").val();

    var subID = $('#ddlPurchaseTypes').find("option:selected").val();
    if (subID == "PUSTR") {
        titile = "Store Wise";
        yaxis = "Amount %";
    }
    else if (subID == "PUITM") {
        titile = "Item Wise";
        yaxis = "Quantity %";
    }
    else {
        titile = "Supplier Wise";
        yaxis = "Amount %";
    }
    var eventsInline;
    $.ajax({
        type: "POST",
        url: "Textile.asmx/GetComparableData",
        data: "{'SubID':'" + subID + "','dtFromDate':'" + dtFromDate + "','dtToDate':'" + dtToDate + "','FK_Client':'" + clientid + "','CompareFrom':'" + CompareFrom + "','dtCompareTo':'" + dtCompareTo + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            eventsInline = eval(data.d);
            if ($("#divPurchase").length > 0) {
                GetComparableDataWeb('bar', eventsInline, titile, yaxis, 'divPurchase', 'Purchase')
            }
        },
        error: function (error) {
        }
    });
    return false;
}


function SaleCompare() {
    var stDate = new Date().format("dd/MM/yyyy");
    var clientid = $("#hfClient").val();

    // var FK_Store = $('#ddlItemStore').find("option:selected").val();
    var dtFromDate = $("#txtPurStFromDate").val();
    var dtToDate = $("#txtPurStEnDate").val();
    var titile = "";
    var yaxis = "";
    var CompareFrom = $("#txtPurFrmDate").val();
    var dtCompareTo = $("#txtPurToDate").val();

    var subID = $('#ddlModeWiseSale').find("option:selected").val();
    if (subID == "SASTR") {
        titile = "Store Wise";
        yaxis = "Amount %";
    }
    else if (subID == "SAITM") {
        titile = "Item Wise";
        yaxis = "Quantity %";
    }
    else {
        titile = "Customer Wise";
        yaxis = "Amount %";
    }
    var eventsInline;
    $.ajax({
        type: "POST",
        url: "Textile.asmx/GetComparableData",
        data: "{'SubID':'" + subID + "','dtFromDate':'" + dtFromDate + "','dtToDate':'" + dtToDate + "','FK_Client':'" + clientid + "','CompareFrom':'" + CompareFrom + "','dtCompareTo':'" + dtCompareTo + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            eventsInline = eval(data.d);
            if ($("#divSaleDetailed").length > 0) {
                GetComparableDataWeb('bar', eventsInline, titile, yaxis, 'divSaleDetailed', 'Sales')
            }
        },
        error: function (error) {
        }
    });
    return false;

}
/************Supplier***************/

function GetSupplierDetails(type, salesMan,container) {

    var dt = $("#hdfPeriod").val();
    
    if (type == 'pie') {

        var chart = new Highcharts.Chart({
            chart: {
                type: 'pie',
                renderTo: container,
                options3d: {
                    enabled: true,
                    alpha: 45,
                    beta: 0
                }
            },
            title: {
                text: 'Sales Man Performance'
            },
            subtitle: {
                 text: dt
            },
            credits: {
                enabled: false
            },
            tooltip: {
                pointFormat: '<b>{point.ename}</b><br/>Amount:<b>{point.y:.2f}</b><br/>No of Sale: <b>{point.amount:.2f}</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    depth: 35,
                    dataLabels: {
                        enabled: true,
                        format: '{point.name}'
                    },
                    point: {
                        events: {
                            click: function () {
                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&SalesMan=Itemss";

                            }
                        }
                    }
                }


            },
            series: [{
                type: 'pie',
                name: 'SalesMan',
                data: salesMan
            }]
        });
    }
    else {

        var chart = new Highcharts.Chart({
            chart: {
                renderTo:container,
                type: 'column'

            },
            title: {
                text: 'Sales Man Performance'
            },
            subtitle: {
                text: dt
            },
            credits: {
                enabled: false
            },
            plotOptions: {
                column: {
                    colorByPoint: true
                },
                series: {
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: function () {
                                window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&SalesMan=Itemss"; 
                            }
                        }
                    }
                }
            },
            xAxis: {
                type: 'category',
                labels: {
                    rotation: -45,
                    align: 'right',
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Amount of sale'
                }
            },
            legend: {
                enabled: false
            },
            tooltip: {
                pointFormat: '<b>{point.ename}</b><br/>Amount:<b>{point.y:.2f}</b><br/>No of Sale: <b>{point.amount:.2f}</b>'
            },
            series: [{
                name: 'SalesMan',
                data: salesMan,
                dataLabels: {
                    enabled: true,
                    rotation: -90,
                    color: '#fcfdd6',
                    align: 'right',
                    x: 4,
                    y: 10,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif',
                        textShadow: '0 0 3px black'
                    }
                }
            }]
        });

    }
}


$(document).mouseup(function (e) {
    var container = $("#leftNav");
    var Notcontainer = $("#divAnimate");
    if (!container.is(e.target) && container.has(e.target).length === 0 && !Notcontainer.is(e.target) && Notcontainer.has(e.target).length === 0) {
        $("#leftNav").hide("slide", { direction: "right" }, 0);
                      
         
        $("#closebtn").attr("src", "images/buttons.png");
       
    }
    
    

   
  
});

function DisabledFrom() {
    $("#frmUserDashBoard").attr("disabled", true);
}
function Enableform() {
    $("#frmUserDashBoard").attr("disabled", false);
}
function zoom1() {


    $("#divzoom1").click(function () {
        ConstructZoomGraph('divItem');




    });
    $("#divzoom2").click(function () {


        ConstructZoomGraph('divCustomer');




    });

    $("#divzoom3").click(function () {

        ConstructZoomGraph('divSale');

    });
    $("#divzoom4").click(function () {


        ConstructZoomGraph("divMinStock");

    });


    $("#divzoom5").click(function () {

        ConstructZoomGraph("divCreditSupplier");

    });
    $("#divzoom6").click(function () {

        ConstructZoomGraph("divCreditCustomer");



    });

    $("#divzoom7").click(function () {

        ConstructZoomGraph("divSupplier");
    });
    $("#divzoom8").click(function () {

        ConstructZoomGraph('divSaleDetailed');

    });
    $("#divzoom9").click(function () {

        ConstructZoomGraph("divPurchase");

    });

}


function DestroyZoom() {
    $('#divPopup').highcharts().destroy();
    $("#divPopupContainer").attr("style", "display:none;");
    Enableform();
    return false;
}
function ConstructZoomGraph(divContainer) {
    try {
        var chart = $('#' + divContainer + '').highcharts();
        $("#hdfChart").val(divContainer);

        $("#divPopup").attr("style", "display:block;width:1437px;height:640px;");
        $("#divGrid").empty();
        $("#divGrid").attr("style", "display:none;width:1437px;height:640px;");

        $("#divZoomGrid").attr("style", "display:none;");

        if (divContainer == "divMinStock" || divContainer == "divSaleDetailed" || divContainer == "divPurchase") {
            if (divContainer == "divMinStock") {


                $("#divGrid").append(function () {
                    return $("#divRepeater").clone().html();
                });

                $("#divZoomPie").attr("style", "display:none;");
                $("#divZoomBar").removeAttr("style");
                $("#divZoomGrid").attr("style", "display:block;");
                if ($("#cbxBar4").hasClass("ActiveChek")) {
                    $("#divZoomBar").addClass("bar3");
                    $("#divZoomGrid").removeClass("bar3");

                    $("#divGrid").attr("style", "display:none;");

                }
                else {
                    $("#divZoomGrid").addClass("bar3");
                    $("#divZoomBar").removeClass("bar3");
                    $("#divPopup").attr("style", "display:none;width:1437px;height:640px;");
                    $("#divGrid").attr("style", "display:block;");
                }

            }
            else {
                $("#divZoomPie").attr("style", "display:none;");
                $("#divZoomBar").attr("style", "display:none;");
                $("#divZoomGrid").attr("style", "display:none;");


            }


        }
        else {

            $("#divZoomPie,#divZoomBar").removeAttr("style");
            if (chart.options.chart.type == "column") {
                $("#divZoomBar").addClass("bar3");
                $("#divZoomPie").removeClass("bar3");
            }
            else {

                $("#divZoomPie").addClass("bar3");
                $("#divZoomBar").removeClass("bar3");
            }
        }
        chart.options.chart.backgroundColor = {
            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
            stops: [
               [0, 'rgba(228, 245, 252, 0.8)'],
               [1, 'rgba(228, 245, 252, 0.48)']
            ]
        }


        chart.options.chart.renderTo = "divPopup";



        var chart2 = new Highcharts.Chart(chart.options);
        $("#divPopupContainer").attr("style", "display:block;");
    }
    catch (e) {
        alert(e);
    }
    DisabledFrom()
}
function BarZoom() {
    var points;
    $("#divPopup").attr("style", "display:block;width:1437px;height:640px;");
    switch ($("#hdfChart").val()) {
        case "divItem":
            points = eval($("#hdfCounter").val());
            ItemBarOrPie('bar', points, 'divPopup');
            break;
        case "divCustomer":
            points = eval($("#hdfCustomer").val());
            CustomerBar('bar', points, 'divPopup');
            break;
        case "divSale": points = eval($("#hdfSale").val());
            SalesBarOrPie('bar', points, 'divPopup');
            break;
        case "divCreditSupplier": points = eval($("#hdfCrPurchase").val());
            CreditSupplier('bar', points, 'divPopup');
            break;
        case "divCreditCustomer": points = eval($("#hdfCrCustomer").val());
            CreditCustomer('bar', points, 'divPopup');
            break;

        case "divSupplier": points = eval($("#hdfSalesMan").val());

            GetSupplierDetails('bar', points, 'divPopup');
            break;

        case "divMinStock":
            points = eval($("#hdfMinStock").val());
            MinStockBarorpie('bar', points, 'divPopup', 'divGrid');
            $("#divZoomBar").addClass("bar3");
            $("#divZoomGrid").removeClass("bar3");
            break;








    }

    var chart = $('#divPopup').highcharts();
    chart.options.chart.backgroundColor = {
        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
        stops: [
           [0, 'rgba(228, 245, 252, 0.8)'],
           [1, 'rgba(228, 245, 252, 0.48)']
        ]
    }
    chart.options.chart.height = $('#divPopup').height();
    chart.options.chart.width = $('#divPopup').width();
    var chart2 = new Highcharts.Chart(chart.options);
    $("#divZoomBar").addClass("bar3");
    $("#divZoomPie").removeClass("bar3");
}
function PieZoom() {
    try {
        $("#divPopup").attr("style", "display:block;height: 640px; width:1437px;");
        var points;

        switch ($("#hdfChart").val()) {
            case "divItem":

                points = eval($("#hdfCounter").val());
                ItemBarOrPie('pie', points, 'divPopup');
                break;
            case "divCustomer":
                points = eval($("#hdfCustomer").val());
                CustomerBar('pie', points, 'divPopup');
                break;
            case "divSale": points = eval($("#hdfSale").val());
                SalesBarOrPie('pie', points, 'divPopup');
                break;
            case "divCreditSupplier": points = eval($("#hdfCrPurchase").val());
                CreditSupplier('pie', points, 'divPopup');
                break;
            case "divCreditCustomer": points = eval($("#hdfCrCustomer").val());
                CreditCustomer('pie', points, 'divPopup');
                break;

            case "divSupplier": points = eval($("#hdfSalesMan").val());

                GetSupplierDetails('pie', points, 'divPopup');
                break;
            case "divMinStock":
                points = eval($("#hdfMinStock").val());
                MinStockBarorpie('pie', points, 'divPopup', 'divGrid');
                $("#divZoomGrid").addClass("bar3");
                $("#divZoomBar").removeClass("bar3");
                break;






        }
        var chart = $('#divPopup').highcharts();
        chart.options.chart.backgroundColor = {
            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
            stops: [
               [0, 'rgba(228, 245, 252, 0.8)'],
               [1, 'rgba(228, 245, 252, 0.48)']
            ]
        }
        chart.options.chart.height = $('#divPopup').height();
        chart.options.chart.width = $('#divPopup').width();
        var chart2 = new Highcharts.Chart(chart.options);
        $("#divZoomPie").addClass("bar3");
        $("#divZoomBar").removeClass("bar3");
    }
    catch (e) {
        alert(e);
    }
}


function pageLoad() {


    zoom1();
    /*********************Customer**********************/

    if ($("#txtDate").val() != "") {

        $("#divZoomBar").click(function () {
            BarZoom();
        });
        $("#divZoomPie").click(function () {

            PieZoom();
        });
        $("#divZoomGrid").click(function () {
            $("#divZoomGrid").addClass("bar3");
            $("#divZoomBar").removeClass("bar3");
            $("#divPopup").attr("style", "display:none;width:1437px;height:640px;");
            $("#divGrid").attr("style", "display:block;");
        });

        $("#cbxBar1").click(function () {
            var customer = eval($("#hdfCustomer").val());
            CustomerBar('bar', customer, 'divCustomer');
            $('#cbxpie1').removeClass('ActiveChek');
            $("#cbxBar1").addClass('ActiveChek');


        });

        GetGraphDetail("NOSL");
        var dt = $("#hdfPeriod").val();
        GetGraphDetail('CUST');


        $("#cbxpie1").click(function () {
            var customer = eval($("#hdfCustomer").val());
            CustomerBar('pie', customer, 'divCustomer');
            $('#cbxBar1').removeClass('ActiveChek');
            $("#cbxpie1").addClass('ActiveChek');


        });

        /************************Item***************/


        $("#cbxBar2").click(function () {

            var pts = eval($("#hdfCounter").val());

            ItemBarOrPie('bar', pts, 'divItem');


            $('#cbxpie2').removeClass('ActiveChek');
            $("#cbxBar2").addClass('ActiveChek');



        });
        var dtss = $("#hdfPeriod").val();


        GetGraphDetail('ITM');
        $("#cbxpie2").click(function () {


            var pts = eval($("#hdfCounter").val());
            ItemBarOrPie('pie', pts, 'divItem');
            $('#cbxBar2').removeClass('ActiveChek');
            $("#cbxpie2").addClass('ActiveChek');

        });

        /***********************Sales Man*************************/
        $("#cbxBar8").click(function () {

            var salesMan = eval($("#hdfSalesMan").val());

            GetSupplierDetails('bar', salesMan, 'divSupplier');


            $('#cbxPie8').removeClass('ActiveChek');
            $("#cbxBar8").addClass('ActiveChek');



        });

        GetGraphDetail('SLMW');

        $("#cbxPie8").click(function () {
            var salesMan = eval($("#hdfSalesMan").val());


            GetSupplierDetails('pie', salesMan, 'divSupplier');
            $('#cbxBar8').removeClass('ActiveChek');
            $("#cbxPie8").addClass('ActiveChek');

        });

        // GetSupplierDetails('bar');


        /*********************Sale**********************/
        $("#cbxBar3").click(function () {

            var eventsInline = eval($("#hdfSale").val());
            SalesBarOrPie('bar', eventsInline, 'divSale');
            $('#cbxPie3').removeClass('ActiveChek');
            $("#cbxBar3").addClass('ActiveChek');

        });

        $("#cbxPie3").click(function () {

            var eventsInline = eval($("#hdfSale").val());

            SalesBarOrPie('pie', eventsInline, 'divSale');
            $('#cbxBar3').removeClass('ActiveChek');
            $("#cbxPie3").addClass('ActiveChek');

        });
        GetGraphDetail('STR');
        /**********************Min Stock*******************/
        $("#cbxBar4").click(function () {

            var items = eval($("#hdfMinStock").val());
            MinStockBarorpie('bar', items, 'divMinStock', 'divRepeater');
            $('#cbxPie4').removeClass('ActiveChek');
            $("#cbxBar4").addClass('ActiveChek');

        });



        $("#cbxPie4").click(function () {
            var items = eval($("#hdfMinStock").val());

            MinStockBarorpie('pie', items, 'divMinStock', 'divRepeater');
            $('#cbxBar4').removeClass('ActiveChek');
            $("#cbxPie4").addClass('ActiveChek');

        });
        GetGraphDetail('MSW');



        $("#cbxBar5").click(function () {
            var supplier = eval($("#hdfCrPurchase").val());
            CreditSupplier('bar', supplier, 'divCreditSupplier');

            $('#cbxBar5').addClass('ActiveChek');
            $("#cbxPie5").removeClass('ActiveChek');
        });
        $("#cbxPie5").click(function () {

            var supplier = eval($("#hdfCrPurchase").val());
            CreditSupplier('pie', supplier, 'divCreditSupplier');

            $('#cbxBar5').removeClass('ActiveChek');
            $("#cbxPie5").addClass('ActiveChek');
        });
        GetGraphDetail('CRPW');


        $("#cbxBar6").click(function () {
            var creditcust = eval($("#hdfCrCustomer").val());
            CreditCustomer('bar', creditcust, 'divCreditCustomer');

            $('#cbxBar6').addClass('ActiveChek');
            $("#cbxPie6").removeClass('ActiveChek');
        });
        $("#cbxPie6").click(function () {
            var creditcust = eval($("#hdfCrCustomer").val());
            CreditCustomer('pie', creditcust, 'divCreditCustomer');

            $('#cbxBar6').removeClass('ActiveChek');
            $("#cbxPie6").addClass('ActiveChek');
        });

        GetGraphDetail('CRCW');
        //Comparison Chart
        GetPurchaseDetails();
        SaleCompare();


    }



    CustomDateSerarch();
    GetSaleDiv();

}
//);

$(document).ready(function () {

    ClearSearch();
    ClearCommon();
    $('#divAnimate').click(function (event) {

        $("#leftNav").toggle('slide', { direction: 'right' }, 0);
        $("#closebtn").attr("src", function () {
            return $(this).attr('src') == 'images/buttons.png' ? 'images/buttons3.png' : 'images/buttons.png';
        });

    });

});


function BindjsontoHmlTable(json) {

    $("#tblReport").find("tr:gt(0)").remove();
    var tr;

    for (var i = 0; i < json.length; i++) {
        tr = $('<tr/>');
        tr.append("<td>" + json[i].name + "</td>");
        tr.append("<td>" + json[i].CS + "</td>");
        tr.append("<td>" + json[i].Min + "</td>");
        tr.click(function () {
            window.location.href = "WebPharmaReport.aspx?uid=" + $("#hfUser").val() + "&clnt=" + $("#hfClient").val() + "&Stock=stock";
        });
        $('#tblReport').append(tr);

    }
    if (json.length <= 0) {
        tr = $('<tr/>');
      
        tr.append("<td style='width:100%;' colspan='3'>No Data Found</td>");
       
        $('#tblReport').append(tr);
    }


}


function GeneralSearch() {

    if (Page_ClientValidate("submit")) {

        GetGraphDetail("NOSL");

        var vals = $("#ddlGraphType1").find("option:selected").val();

        if (vals == '0') {
            $("#ddlGraphType1 > option").not(":first").each(function () {
                GetGraphDetail(this.value);
            });
        }
        else {

            GetGraphDetail(vals);
        }
    }


    return false;
}
function ComparisonSearch() {
   
    if (Page_ClientValidate("Pur")) {
        var vals = $("#ddlComparisonal").find("option:selected").val();
        if (vals == 'Purchase') {
            GetPurchaseDetails();
        }
        else {
            SaleCompare();
        }
    }   
    return false;
}

function ClearCommon() {
    var dt = new Date().format('dd/MM/yyyy');   
    $("#txtFromDate,#txtToDate").val(dt);
    $("#ddlGraphType1").val("0");
    $("#ddlItemStore").val("0");
    $("#ddlType").val("0");
    $("#divSearchStore").show();
    $("#divPeriodSearch").show();
    CustomDateSerarch();
    return false;
}



function ClearSearch() {
    var dt = new Date().format('dd/MM/yyyy');
    $("#txtDate").val(dt);
    var date = new Date(), y = date.getFullYear(), m = date.getMonth();
    var firstDay = new Date(y, m - 1, 1).format('dd/MM/yyyy');
    var lastDay = new Date(y, m, 0).format('dd/MM/yyyy');
    var fd = new Date(y, m, 1).format('dd/MM/yyyy');
    var ld = new Date(y, m + 1, 0).format('dd/MM/yyyy');
    $("#txtPurStFromDate").val(fd);
    $("#txtPurStEnDate").val(ld);
    $("#txtPurFrmDate").val(firstDay);
    $("#txtPurToDate").val(lastDay);
    $("#ddlComparisonal").val("Purchase");
    $("#ddlModeWiseSale").val("SASTR");
    $("#ddlPurchaseTypes").val("PUSTR");
    GetSaleDiv();
    return false;

    //$("#btnHidden").click();

}
function GetSaleDiv() {
    if ($("#ddlComparisonal").find("option:selected").val() == "Purchase") {
        $("#divPurchaseMode").show();
        $("#divSaleMode").hide();

    }
    else {
        $("#divPurchaseMode").hide();
        $("#divSaleMode").show();
    }

}
function CustomDateSerarch() {
    var Period = $('#ddlType').find("option:selected").val();
    if (Period == '6') {

        $("#divdateWiseFilteraton").show();
    }
    else {
        $("#divdateWiseFilteraton").hide();
    }
}

function ClientValidate(source, arguments) {
    try {
        var date = document.getElementById("txtFromDate").value;
        var arr = date.split('/');
        var start = new Date(arr[2], arr[1], arr[0]);
        var arr2 = arguments.Value.split('/');

        var endDate = new Date(arr2[2], arr2[1], arr2[0]);

        if (start > endDate) {
            $("#lblInfo").attr('style', 'display:block');
            arguments.IsValid = false;
        }
        else {
            $("#lblInfo").attr('style', 'display:none');
            arguments.IsValid = true;
        }

    }
    catch (e) {
        alert(e);
    }

}
function ClientValidate2(source, arguments) {
    try {
        var date = document.getElementById("txtPurStFromDate").value;
        var arr = date.split('/');
        var start = new Date(arr[2], arr[1], arr[0]);
        var arr2 = arguments.Value.split('/');

        var endDate = new Date(arr2[2], arr2[1], arr2[0]);

        if (start > endDate) {
            $("#lblInfo1").attr('style', 'display:block');
            arguments.IsValid = false;
        }
        else {
            $("#lblInfo1").attr('style', 'display:none');
            arguments.IsValid = true;
        }

    }
    catch (e) {
        alert(e);
    }

}
function ClientValidate3(source, arguments) {
    try {
        var date = document.getElementById("txtPurFrmDate").value;
        var arr = date.split('/');
        var start = new Date(arr[2], arr[1], arr[0]);
        var arr2 = arguments.Value.split('/');

        var endDate = new Date(arr2[2], arr2[1], arr2[0]);

        if (start > endDate) {
            $("#lblInfo1").attr('style', 'display:block');
            arguments.IsValid = false;
        }
        else {
            $("#lblInfo1").attr('style', 'display:none');
            arguments.IsValid = true;
        }

    }
    catch (e) {
        alert(e);
    }

}
function hideperiod() {

    var Period = $('#ddlGraphType1').find("option:selected").val();
    var psd = $('#ddlGraphType1').find("option:selected").val();
    if (Period == 'MSW') {

        $("#divPeriodSearch").hide();
    }
    else {
        $("#divPeriodSearch").show();
    }
    if (psd == 'STR'||psd=='CRCW'||psd=='CRPW') {
        $("#divSearchStore").hide();
    }
    else {
        $("#divSearchStore").show();
    }
}