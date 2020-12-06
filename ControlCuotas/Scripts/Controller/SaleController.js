var param = null;
var total = 0;
$(document).ready(function () {

    var toDate = new Date();
    var strDate = toDate.getDate() + "/" + (toDate.getMonth() + 1) + "/" + toDate.getFullYear();

    GetAllProduct();
    GetClientCombo();
    GetVendorCombo();
    $('#lblDate').text(strDate);


});

function GetAllProduct() {

    $.blockUI();

    $.post('/Product/GetAllProduct')
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblSearchProduct > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {
                    _html += '<tr><td>' + value.id + '</td><td>' + value.name + '</td><td>' + value.description + '</td><td>' + value.stock + '</td><td>' + value.salePrice + '</td><td >' + '<button type="button" class="btn btn-primary" onclick="AddProduct(' + value.id + ',' + `'${value.name}'` + ',' + `'${value.description}'` + ',' + value.salePrice + ',' + value.stock +');"><i class="fa fa-plus"></i> Agregar </button>' + '</td>';
                });

                _html += '</tbody >';

                $('#tblSearchProduct').append(_html);
                $('#tblSearchProduct').DataTable({
                    destroy: true,
                    retrieve: true,
                    lengthMenu: [[1], [1]]
                });


            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        })
        .always(function () {
            $.unblockUI();
        });
}


function AddSale() {

    if ($('#txtDate').val() == "" || $('#cboClient').val() == "" || $('#cboVendor').val() == "" || $('#txtSubTotal').text() == "" || $('#txtTotal').text() == "" || $('#txtDateEnd').val() =="" )
    {
        alertify.alert('Venta de artículo', 'Falta cargar campos obligatorios: Fechas, cliente, vendedor, importes' );
        return;
    }

    var _productString = "";
    $("#tblSaleProduct tbody tr").each(function () {
        _productString += $(this).attr('id') + ',';
    });
    var quote="";
    if ($('#txtQuantityQuota').val() == "") {
        quote = 1;
    }
    else {
        quote = $('#txtQuantityQuota').val();
    }

    var QuotaPrice = ($('#txtTotal').text() / quote).toFixed(2);
    param = {
        saleDate: $('#txtDate').val(),
        idClient: $('#cboClient').val(),
        idVendor: $('#cboVendor').val(),
        subTotalSale: $('#txtSubTotal').text(),
        totalSale: $('#txtTotal').text(),
        quotaSale: quote,
        interest: $('#txtInteres').val(),
        discount: $('#txtDesc').val(),
        dateEnd: $('#txtDateEnd').val(),
        productString: _productString,
        quotaPrice: QuotaPrice
    };

    $.post(directories.sale.AddSale, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                //alertify.confirm('Confirm Title', 'Confirm Message', function () {window.location.reload();});

                alertify.alert('Venta exitosa', data.message, function () {
                    window.location.reload();
                });

                //window.location.reload();
                //$('#tblSearchProduct > tbody').html('');
                //$('#cboClient').val('');
                //$('#cboVendor').val('');
                //$('#txtDate').val(),
                //$('#txtSubTotal').text('');
                //$('#txtTotal').text('');
                //$('#txtQuantityQuota').val('');
                //$('#txtInteres').val('');
                //$('#txtDesc').val('');
                //$('#txtDateEnd').val('');
                //GetAllProduct();
                //GetClientCombo();
                //GetVendorCombo();

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}

function GetClientCombo() {

    $.post('/Prestamo/GetClientCombo')
        .done(function (data) {
            if (data.status !== "error") {

                var ComboClient = $('#cboClient');

                $("#cboClient").empty();

                data = JSON.parse(data.result);

                ComboClient.append($("<option />").val('').text('Seleccione un cliente'));

                $.each(data, function (key, value) {

                    ComboClient.append($("<option />").val(value.id).text(value.name));

                });

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}
function GetVendorCombo() {

    $.post('/Vendor/GetAllVendor')
        .done(function (data) {
            if (data.status !== "error") {

                var ComboVendor = $('#cboVendor');
                $("#cboVendor").empty();
                data = JSON.parse(data.result);
                ComboVendor.append($("<option />").val('').text('Seleccione un vendedor'));
                $.each(data, function (key, value) {
                    ComboVendor.append($("<option />").val(value.id).text(value.nickName));
                });
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}

function AddProduct(id, name, description, salePrice, stock) {

    if (stock == 0) {
        alertify.alert('Control de stock', 'El articulo no está disponible en el stock');
        return;
    }
    var subAmount = salePrice;
    var quantity = 1;
    total = 0;
    if ($('#txtQuantity').val() != '') {
        quantity = $('#txtQuantity').val();
        subAmount = subAmount * quantity;
    }
    $('#tblSaleProduct > tbody');
    var _html = '';
    _html += '<tbody class="customtable" style= text-align:left;>';
    _html += '<tr class='+ id +' id=' + id +'-'+ quantity + '-'+ salePrice +'-'+ subAmount +'><td>' + id + '</td><td >' + name + '</td><td>' + description + '</td><td>' + quantity + '</td><td>' + salePrice + '</td><td id=' + 'idSubPrice' + '>' + subAmount + '</td><td >' + '<button type="button" class="btn btn-danger" onclick="QuitProduct(' + id + ');"><i class="fas fa-ban"></i> Quitar </button>' + '</td>';
    _html += '</tbody >';

    $('#tblSaleProduct').append(_html);
    $('#txtQuantity').val('');
    $('#txtSubTotal').val();

    $("#tblSaleProduct tr #idSubPrice ").each(function () {
        total += parseFloat($(this).text());
    });
    $('#txtSubTotal').text(total.toFixed(2));
    $('#txtTotal').text(total.toFixed(2));
}

function QuitProduct(idRow) {

    $('table#tblSaleProduct tr.'+ idRow+'').remove();

    $('#txtSubTotal').text('');
    $('#txtTotal').text('');
    total = 0;
    $("#tblSaleProduct tr #idSubPrice ").each(function () {
        total += parseFloat($(this).text());
    });
    $('#txtSubTotal').text(total.toFixed(2));
    $('#txtTotal').text(total.toFixed(2));
}

function Recalculate() {
    var interes = 0;
    var subTotal = 0;
    var totalInteres = 0;
    var descuento = 0;

    if ($('#txtInteres').val() != "" && $('#txtSubTotal').text() != "") {
        interes = parseFloat($('#txtInteres').val());
        subTotal = parseFloat($('#txtSubTotal').text());
        totalInteres = (((interes / 100) * subTotal) + subTotal);
        $('#txtTotal').text(totalInteres.toFixed(2));
    }
   
    if ($('#txtDesc').val() != "" && $('#txtSubTotal').text() != "") {
        descuento = parseFloat($('#txtDesc').val());
        if (totalInteres == 0) {
            totalInteres = parseFloat($('#txtSubTotal').text());
        }
        totalInteres = totalInteres -((descuento / 100) * totalInteres);
        $('#txtTotal').text(totalInteres.toFixed(2));
    }
    if ($('#txtInteres').val() == "" && $('#txtDesc').val() =="") {
        $('#txtTotal').text($('#txtSubTotal').text());
    }
}

function Clear(){
    $('#txtTotal').text($('#txtSubTotal').text());
    $('#txtInteres').val('');
    $('#txtDesc').val('');
    $('#txtQuantityQuota').val('');
}

