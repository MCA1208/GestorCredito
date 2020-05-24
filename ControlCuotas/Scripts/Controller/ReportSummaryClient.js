var param = null;


$(document).ready(function () {

    getClientCombo();

});

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

function SearchSummaryClient() {

    if ($('#cboCliente').val() === "") {

        alertify.alert('Mensaje de Alerta', 'Seleccione un cliente para la búsqueda');
        return;
    }

    $.blockUI();

    param = {
        IdClient: $('#cboCliente').val()
    };


    $.post(directories.report.GetReportSummaryClient, param)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblReportSummaryClient > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.idPrestamo + '</td><td>' + value.name + '</td><td >' + value.dateStart + '</td><td >' + value.dateEnd + '</td><td >' + value.totalPrestamo + '</td><td>' + value.totalPagado + '</td><td>' + value.Saldo + '</td><td>' + value.cuotaPayment + '</td><td>' + '<button type="button" class="btn btn-info" onclick="showModalSummaryDetail(' + value.id + ',' + `'${value.idPrestamo}'` +',' + `'${value.name}'` + ',' + `'${value.dateStart}'` + ',' + `'${value.dateEnd}'` + ',' + value.totalPrestamo + ',' + value.totalPagado + ',' + value.Saldo + ',' + `'${value.cuotaPayment}'` +');"><i style="size:9x" class="fas fa-eye"></i> Pre visualizar</button>' + '</td>';

                });

                _html += '</tbody >';

                $('#tblReportSummaryClient').append(_html);

                /*$('#tblReportSummaryClient').DataTable({
                    destroy: true,
                    retrieve: true,
                    dom: 'Bfrtip',
                    buttons: [
                        { "extend": 'excel', "text": '<span data-toggle="tooltip" data-placement="top" title="Exportar Excel" class="fas fa-file-excel fa-2x"></span>' },
                        { "extend": 'pdf', "text": '<span data-toggle="tooltip" data-placement="top" title="Exportar PDF" class="fas fa-file-pdf fa-2x" ></span>' },
                        { "extend": 'print', "text": '<span data-toggle="tooltip" data-placement="top" title="Imprimir" class="fas fa-print fa-2x"></span>' }
                    ]

                });*/

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

function showModalSummaryDetail(Id, IdPrestamo, name, dateStart, dateEnd, total, pagado, saldo, cuota) {

    $.blockUI();

    param = {
        IdPrest: Id
    };

    $.post(directories.report.GetReportSummaryDetail, param)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblSummaryDetail > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {
                    
                    _html += '<tr><td>' + value.number + '</td><td>' + value.statusPago + '</td><td >' + value.paymentDate + '</td><td >' + value.observation + '</td>';

                });

                _html += '</tbody >';
                 
                $('#tblSummaryDetail').append(_html);   

                $('#PrintSummaryModal').modal('show');
                $('#lblNumero').text(IdPrestamo);
                $('#lblCliente').text(name);
                $('#lblDateStart').text(dateStart);
                $('#lblDateEnd').text(dateEnd);
                $('#pTotal').text(total);
                $('#pPagado').text(pagado);
                $('#pSaldo').text(saldo);
                $('#pCuota').text(cuota);

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

    $('#PrintSummaryModal').css('display', 'none')

    $('#PrintSummaryModal').modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();

}
