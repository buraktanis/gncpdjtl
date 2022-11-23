


if (isyonetim == "True") {
    $.ajax({
        type: 'GET',
        url: '/Customer/GetUserAll',
        success: function (msg) {
          
            msg.data.forEach(function (item) {
                $('#selecttemsici').append('<option value="' + item.name + '">' + item.name + '</option>');
            });
        }

    });

}// else {
//    $('#selecttemsici').html("");;
//    $('#selecttemsici').append('<option value="' + $('#elementid').val() + '">' + $('#elementid').val() + '</option>');
//    }

function GetTips() {
    $.ajax({
        type: 'GET',
        url: '/ErpSales/GetTips/',
        success: function (msg) {
            $('#turselect').html('');
            $('#turselect').append('<option value="">Tüm Türler</option>');
            console.log(msg)
            msg.data.forEach(function (item) {

                $('#turselect').append('<option value=' + item.tip + '>' + item.tip + '</option>');
            });

        }


    });
}

function Getbrands() {
    $.ajax({
        type: 'GET',
        url: '/ErpSales/Getbrands/',
        success: function (msg) {
            $('#markaselect').html('');
            $('#markaselect').append('<option value="">Tüm Markalar</option>');
            console.log(msg)
            msg.data.forEach(function (item) {

                $('#markaselect').append('<option value=' + item.MARKA + '>' + item.MARKA + '</option>');
            });

        }


    });
}