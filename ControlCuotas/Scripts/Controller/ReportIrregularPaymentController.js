var param = null;


$(document).ready(function () {


    getzona();

    getClientCombo();


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

function getClientCombo() {

    $.post('/Prestamo/GetClientCombo')
        .done(function (data) {
            if (data.status !== "error") {

                var ComboCliente = $('#cboCliente');

                $("#cboCliente").empty();

                data = JSON.parse(data.result);

                ComboCliente.append($("<option />").val('').text('Seleccione un cliente'));

                $.each(data, function (key, value) {

                    ComboCliente.append($("<option />").val(value.id).text(value.name));

                });

            }
            else {
                alertify.error(data.message);
                $('#tblReporIrregularPayment').empty();
            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}

function ReportIrregularPayment() {

    $.blockUI();

    param = {
        IdClient: $('#cboCliente').val(),
        IdZone: $('#cboZona').val(),
    };


    $.post(directories.report.GetReportIrregularPayment, param)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblReporIrregularPayment > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.name + '</td><td>' + value.zone + '</td><td >' + value.idPrestamo + '</td><td >' + value.number + '</td><td >' + value.amount + '</td><td>' + value.observation + '</td><td>' + value.observationPartial + '</td>';

                });

                _html += '</tbody >';

                $('#tblReporIrregularPayment').append(_html);

                $('#tblReporIrregularPayment').DataTable({
                    destroy: true,
                    retrieve: true,
                    dom: 'Bfrtip',
                    buttons: [
                        { "extend": 'excel', "text": '<span data-toggle="tooltip" data-placement="top" title="Exportar Excel" class="fas fa-file-excel fa-2x"></span>' },
                        { "extend": 'pdf', "text": '<span data-toggle="tooltip" data-placement="top" title="Exportar PDF" class="fas fa-file-pdf fa-2x" ></span>' },
                        { "extend": 'print', "text": '<span data-toggle="tooltip" data-placement="top" title="Imprimir" class="fas fa-print fa-2x"></span>' }
                    ]

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