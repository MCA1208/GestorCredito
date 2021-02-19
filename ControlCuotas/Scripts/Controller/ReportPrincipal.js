var param = null;


$(document).ready(function () {


    getzona();

    getClientCombo();


});

function SearchReportprincipal() {

    $.blockUI();

    param = {
        IdClient: $('#cboCliente').val(),
        IdZone: $('#cboZona').val(),
        dateFrom: $('#dateFrom').val(),
        DateUp: $('#dateUp').val(),


    };

    $.post(directories.report.GetReportPrincipal, param)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblReportPrincipal > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {
                    //cl.[name], z.[description], p.id, p.amount as amountPrestamo, p.amountInterest, c.number, c.amount as amountCuota,case when paymentDate  is null then 'Pendiente' else 'Pagado' end as Estado, paymentDate, c.observation
                    _html += '<tr><td>' + value.name + '</td><td>' + value.description + '</td><td >' + value.id + '</td><td >' + value.amountPrestamo + '</td><td >' + value.amountInterest + '</td><td>' + value.number + '</td><td>' + value.amountCuota + '</td><td>' + value.Estado + '</td><td>' + value.paymentDate + '</td><td>' + value.observation + '</td>';

                });

                _html += '</tbody >';

                $('#tblReportPrincipal').append(_html);

                $('#tblReportPrincipal').DataTable({
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

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}