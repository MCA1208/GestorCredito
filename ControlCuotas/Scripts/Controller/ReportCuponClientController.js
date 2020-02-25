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

function SearchCuponClient() {

    if ($('#cboCliente').val() === "") {

        alertify.alert('Mensaje de Alerta', 'Seleccione un cliente para la búsqueda');
        return;
    }

    $.blockUI();

    param = {
        IdClient: $('#cboCliente').val()

    };


    $.post(directories.report.GetReportCuponClient, param)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblCuponCliente > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $('#pNumPrestamo').text(data[0].id);
                $('#pNameClient').text(data[0].name);
                $('#pDniClient').text(data[0].dni);
                $('#pDomicilioClient').text(data[0].address);
                $('#pZoneClient').text(data[0].zone);

                var totalPrestamo = 0;
                var totalPagado = 0;
                var saldo = 0;

                $.each(data, function (key, value) {

                    if (value.totalPrestamo !== null)
                        totalPrestamo = value.totalPrestamo.toFixed(2);

                    if (value.totalPagado !== null)
                        totalPagado = value.totalPagado.toFixed(2);

                    if (value.Saldo !== null)
                        saldo = value.Saldo.toFixed(2);

                    _html += '<tr><td>' + value.dateStart + '</td><td>' + value.dateEnd + '</td><td  >' + totalPrestamo + '</td><td >' + value.cuotaPayment + '</td><td >' + totalPagado + '</td><td >' + saldo + '</td>';

                });
                _html += '</tbody >';
                $('#tblCuponCliente').append(_html);


                $('#tblCuponCredito > tbody').html('');
                _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                $('#pNumPrestamo2').text(data[0].id);
                $('#pNameClient2').text(data[0].name);
                $('#pDniClient2').text(data[0].dni);
                $('#pDomicilioClient2').text(data[0].address);
                $('#pZoneClinet2').text(data[0].zone);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.dateStart + '</td><td>' + value.dateEnd + '</td><td >' + totalPrestamo + '</td><td >' + value.cuotaPayment + '</td><td >' + totalPagado + '</td><td >' + saldo + '</td>';

                });
                _html += '</tbody >';
                $('#tblCuponCredito').append(_html);


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