
//*******************************************************************************************
//GizleGöster Kodları
function gizleGoster(ID) {
    var secilenID = document.getElementById(ID);
    if (secilenID.style.display == "") {
        secilenID.style.display = "none";
    } else {
        secilenID.style.display = "";
    }
}




//*******************************************************************************************


var utilities = {
    alert: function (header, body) {
        $('#alertmodal .modal-header .modal-title').html(header);
        $('#alertmodal .modal-body').html(body);
        $('#alertmodal').modal();
    },
    dil_ceviri: function (text, key) {
        return text;
    }
    ,
    sqldecimal: function (value) {
        if (value == "")
            return 0;
        else
            return parseFloat(value.replace(/\./gi, "").replace(/\,/, "."));
    },
    jsdecimal: function (number, decimal) {
        if (decimal == null)
            decimal = 2;
        return number.toFixed(decimal).replace(/\./, ",")
    },
    onscreen: function (text, bilgi) {
        toastr.success(text, bilgi == null ? 'Bilgi' : bilgi);
    }
};

var loader = {
    start: function (title) {
        $(".preloader").fadeIn();
    },
    stop: function () {
        $(".preloader").fadeOut();
    }
};

$(function () {
    $("[data-type=date]").attr("data-provide", "datepicker").attr("data-date-format", "dd.mm.yyyy").attr("data-date-start-date", "0d").attr("data-date-language", "tr");
});



function jq_alert(text) {
    utilities.alert("Dikkat", text);
    $("#alertmodal .modal-footer").html("<a href='javascript:;' class='btn btn-dark' data-dismiss='modal'>Kapat</a>");
}

function jq_confirm(title, text, func) {
    $('#confirmmodal .modal-header .modal-title').html(title);
    $('#confirmmodal .modal-body').html(text);
    $('#confirmmodal').modal();
    $("#confirmmodal .modal-footer").html("<a href='javascript:;' class='btn btn-success btnok'>Tamam</a><a href='javascript:;' class='btn btn-dark' data-dismiss='modal'>Vazgeç</a>");

    $("[data-type=date]").attr("data-provide", "datepicker").attr("data-date-format", "dd.mm.yyyy").attr("data-date-start-date", "0d").attr("data-date-language", "tr");
    //$("[data-type=datetime]").datetimepicker({
    //    format : "DD.MM.YYYY HH:mm"
    //});

    $(".btnok").off().click(function () {
        func();
    });
}

function jq_dialog(title, text, beforefunction, myfunction, modalclass) {
    $('#confirmmodal .modal-header .modal-title').html(title);

    if (modalclass != null || modalclass != "") {
        $('#confirmmodal .modal-dialog').addClass("modal-lg");
    }
    else
        $('#confirmmodal .modal-dialog').removeClass("modal-lg");


    $('#confirmmodal .modal-body').html(text);
    $('#confirmmodal').modal();
    $("#confirmmodal .modal-footer").html("<a href='javascript:;' class='btn btn-success btnok'>Tamam</a><a href='javascript:;' class='btn btn-dark' data-dismiss='modal'>Vazgeç</a>");

    $("[data-type=date]").attr("data-provide", "datepicker").attr("data-date-format", "dd.mm.yyyy").attr("data-date-start-date", "0d").attr("data-date-language", "tr");

    //$("[data-type=datetime]").datetimepicker({
    //    format: "DD.MM.YYYY HH:mm"
    //});

    beforefunction();

    $(".btnok").off().click(function () {
        var status = myfunction();
        if (status === true)
            $('#confirmmodal').modal("toggle");
    });

}

function getParameterByName(name, url) {

    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$(function () {
    $(document).keypress(function (e) {
        if ($(e.target).is(".onlynumber")) {
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                return false;
            }
        }
    });


});


function logout() {
    //$.ajax({
    //    type: "POST",
    //    url: "/admin/logout",
    //    contentType: "application/json; charset=utf-8",
    //    success: function (data) {
    //        if (data.status)
    //            window.location.href = "/admin";
    //    }
    //});
}

if ($.fn.DataTable) {

    $.extend(true, $.fn.DataTable.defaults, {
        "initComplete": function (settings, json) {
            //$(".dataTables_filter input[type=search]").focus();
            $('[data-toggle="tooltip"], a.dt-button').tooltip();

            var api = this.api();
            api.columns.adjust();

            if (typeof initCompleteAfter_Local == "function") {
                initCompleteAfter_Local();
            }
            $('.dt-buttons button').tooltip();
        },
        "drawCallback": function (settings) {
            var api = this.api();
            api.columns.adjust();

            $('[data-toggle="tooltip"]').tooltip();

            if (typeof drawCallBackAfter_Local == "function") {
                drawCallBackAfter_Local(settings);
            }
        },
        buttons: [
            //{
            //    titleAttr: 'Yazdır',
            //    extend: 'print',
            //    text: '<i class="fa fa-print"></i>',
            //    className: "btn btn-warning",
            //    title: $("h4.page-title").html(),
            //    messageTop: moment().format("DD.MM.YYYY HH:mm")
            //},
            //{
            //    titleAttr: "Excel'e Aktar",
            //    extend: 'excelHtml5',
            //    text: '<i class="fa fa-file-excel"></i>',
            //    className: "btn btn-success",
            //    title: $("h4.page-title").html(),
            //    filename:  moment().format("YYYY.MM.DD_HH.mm")
            //}
        ],
        "stateSave": true,
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "Hepsi"]],
        "stateSaveParams": function (settings, data) {
            data.search.search = "";
        },
        "processing": true,
        "jQueryUI": false,
        "pagingType": "full_numbers",
        //"order": [[0, "ASC"]],
        //"dom": '<"H"CB<"clear">lfr>t<"F"ip>',//form-control
        "dom": 'Blfrtip',
        "language": {
            "url": "/admin/dist/tr.js"
        }
    });
}




function isEmail(emailAddress) {
    //var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
    //return pattern.test(emailAddress);

    return /^(?:[\w-]+\.?)*[\w-]+@(?:[\w-]+\.)+[\w]{2,3}$/.test(emailAddress);
}

function isTel(p) {
    var phoneRe = /^[2-9]\d{2}[2-9]\d{2}\d{4}$/;
    var digits = p.replace(/\D/g, "");
    return phoneRe.test(digits);
}

function isDate(value, userFormat) {
    var userFormat = userFormat || 'dd.mm.yyyy', // default format

        delimiter = /[^mdy]/.exec(userFormat)[0],
        theFormat = userFormat.split(delimiter),
        theDate = value.split(delimiter),

        isMyDate = function (date, format) {
            var m, d, y;
            for (var i = 0, len = format.length; i < len; i++) {
                if (/m/.test(format[i])) m = date[i];
                if (/d/.test(format[i])) d = date[i];
                if (/y/.test(format[i])) y = date[i];
            }
            return (
                m > 0 && m < 13 &&
                y && y.length === 4 &&
                d > 0 && d <= (new Date(y, m, 0)).getDate()
            );
        };

    return isMyDate(theDate, theFormat);
}


function jsonDateToTurkishTime(jDate) {
    try {
        date = new Date(parseInt(jDate.substr(6)));

        d = " " + (date.getHours() < 10 ? "0" + (date.getHours()) : date.getHours()) + ":" + (date.getMinutes() < 10 ? "0" + (date.getMinutes()) : date.getMinutes()) + ":" + (date.getSeconds() < 10 ? "0" + (date.getSeconds()) : date.getSeconds());
        return d;
    } catch (e) {
        console.log(e);
        return "";
    }

}
function IsDataExist(datatablename, datacolumn, value) {
    $.ajax({
        type: "POST",
        url: "/ajaxservices/settings.asmx/isdataexist",
        data: JSON.stringify({ "tablename": tablename, "columns": columns, "kosul": kosul }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var jsondata = JSON.parse(msg.d);
            if (jsondata.returnvalue != "") {
                return [false, "Aynı firma kodu ile kaydedilmiş kullanıcı mevcut"];;
            }
            else {
                return [true, ""];
            }

        }
    });
}

function jsonDateToTurkishDate(jDate, withTime) {
    try {
        date = new Date(parseInt(jDate.substr(6)));
        if (date.getFullYear() == 1) return "";
        d = (date.getDate() < 10 ? "0" + (date.getDate()) : date.getDate()) + "." + (date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1) + "." + date.getFullYear();
        if (withTime)
            d += " " + (date.getHours() < 10 ? "0" + (date.getHours()) : date.getHours()) + ":" + (date.getMinutes() < 10 ? "0" + (date.getMinutes()) : date.getMinutes()) + ":" + (date.getSeconds() < 10 ? "0" + (date.getSeconds()) : date.getSeconds());
        return d;
    } catch (e) {
        //console.log(e);
        return "";
    }

}

isNS = (document.layers || (document.getElementById && !document.all)) ? true : false;

function onlyNumber(e) {
    var keyCode = (isNS) ? e.which : e.keyCode;
    if ((keyCode < 48 || keyCode > 57) && keyCode != 8 && keyCode != 0 && keyCode != 44) {
        return false;
    }
}

Number.prototype.formatMoney = function (places, symbol, thousand, decimal) {
    places = !isNaN(places = Math.abs(places)) ? places : 2;
    symbol = symbol !== undefined ? symbol : "$";
    thousand = thousand || ",";
    decimal = decimal || ".";
    var number = this,
        negative = number < 0 ? "-" : "",
        i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
        j = (j = i.length) > 3 ? j % 3 : 0;
    return negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "") + symbol;
};

$.strPad = function (i, l, s) {
    var o = i.toString();
    if (!s) { s = '0'; }
    while (o.length < l) {
        o = s + o;
    }
    return o;
};

