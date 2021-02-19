var param = null;

$(document).ready(function () {


    getzona();


});

function getzona() {

    $.post('/Client/GetComboZona')
        .done(function (data) {
            if (data.status !== "error") {

                var ComboZona = $('#cboZona');

                $("#cboZona").empty();

                data = JSON.parse(data.result);
                var i = 0;
                ComboZona.append($("<option />").val('').text('Seleccione una zona'));
                $.each(data, function (key, value) {

                    ComboZona.append($("<option />").val(data[i].ItemArray[0]).text(data[i].ItemArray[1]));
                    i++;
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

function SearchReportCobranza() {

    if ($('#cboZona').val() === "") {

        alertify.alert('Mensaje de Alerta', 'Seleccione una zona para la búsqueda');
        return;
    }

    $.blockUI();

    param = {
        IdZone: $('#cboZona').val()
    };
    var zone = $('#cboZona :selected').text();
    var CantidadCupon = 0;
    var TotalCupon = 0;

    $.post(directories.reportProduct.GetReportProductCobranza, param)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblReportCobranza > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {
                    _html += '<tr><td>' + value.IdPrestamo + '</td><td>' + value.name + '</td><td >' + Math.round(value.amount) + '</td><td >' + Math.round(value.saldo) + '</td><td></td><td >';

                    CantidadCupon++;
                    TotalCupon = TotalCupon + value.amount;
                });

                _html += '</tbody >';

                $('#lblZona').text(zone);
                $('#lblCant').text(CantidadCupon);
                $('#lblTotal').text(Math.round(TotalCupon));
                $('#tblReportCobranza').append(_html);
                $('#btnPrint').css('display', 'block');

            }
            else {
                alertify.error(data.message);
                $('#tblReportCobranza > tbody').html('');
                $('#btnPrint').css('display', 'none');

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        })
        .always(function () {
            $.unblockUI();
        });

}

function printDiv() {

    var contenido = document.getElementById('areaPrint').innerHTML;
    var contenidoOriginal = document.body.innerHTML;
    document.body.innerHTML = contenido;
    window.print();
    document.body.innerHTML = contenidoOriginal;

}
