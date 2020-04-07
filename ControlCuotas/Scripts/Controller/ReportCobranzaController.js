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
                ComboZona.append($("<option />").val('').text('Seleccione una zona'));
                $.each(data, function (key, value) {

                    ComboZona.append($("<option />").val(value.id).text(value.description));

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
    if ($('#dateStart').val() === "" || $('#dateEnd').val() === "") {

        alertify.alert('Mensaje de Alerta', 'Las fechas son requeridas');
        return;
    }

    $.blockUI();

    param = {
        IdZone: $('#cboZona').val(),
        DateStart: $('#dateStart').val(),
        DateEnd: $('#dateEnd').val(),
    };
    var zone = $('#cboZona :selected').text();
    var CantidadCupon = 0;
    var TotalCupon = 0;

    $.post(directories.report.GetReportCobranza, param)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblReportCobranza > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.IdPrestamo + '</td><td>' + value.name + '</td><td >' + value.amount + '</td><td ></td><td >';

                    CantidadCupon++;
                    TotalCupon = TotalCupon + value.amount;
                });

                _html += '</tbody >';

                $('#lblZona').text(zone);
                $('#lblCant').text(CantidadCupon);
                $('#lblTotal').text(TotalCupon);
                $('#tblReportCobranza').append(_html);
                $('#btnPrint').css('display', 'block');

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

function printDiv() {

    var contenido = document.getElementById('areaPrint').innerHTML;
    var contenidoOriginal = document.body.innerHTML;
    document.body.innerHTML = contenido;
    window.print();
    document.body.innerHTML = contenidoOriginal;

}
