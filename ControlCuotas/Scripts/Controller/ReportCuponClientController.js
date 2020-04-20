var param = null;


$(document).ready(function () {

    getClientCombo();

});

function getClientCombo() {

    $.post('/Prestamo/GetClientCombo')
        .done(function (data) {
            if (data.status !== "error") {

                var ComboCliente = $('#cboCliente');
                var ComboCliente2 = $('#cboCliente2');
                var ComboCliente3 = $('#cboCliente3');
                var ComboCliente4 = $('#cboCliente4');

                $("#cboCliente").empty();
                $("#cboCliente2").empty();
                $("#cboCliente3").empty();
                $("#cboCliente4").empty();

                data = JSON.parse(data.result);

                ComboCliente.append($("<option />").val('').text('Seleccione un cliente'));
                ComboCliente2.append($("<option />").val('').text('Seleccione un cliente'));
                ComboCliente3.append($("<option />").val('').text('Seleccione un cliente'));
                ComboCliente4.append($("<option />").val('').text('Seleccione un cliente'));

                $.each(data, function (key, value) {

                    ComboCliente.append($("<option />").val(value.id).text(value.name));
                    ComboCliente2.append($("<option />").val(value.id).text(value.name));
                    ComboCliente3.append($("<option />").val(value.id).text(value.name));
                    ComboCliente4.append($("<option />").val(value.id).text(value.name));

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

function SearchCuponClient(numCbo) {

    var IdClientCbo = "";

    if (numCbo == 1) {
        IdClientCbo = $('#cboCliente').val();
        if ($('#cboCliente').val() === "") {
            alertify.alert('Mensaje de Alerta', 'Seleccione un cliente para la búsqueda');
            return;
        }
    }
    if (numCbo == 2) {
        IdClientCbo = $('#cboCliente2').val();
        if ($('#cboCliente2').val() === "") {
            alertify.alert('Mensaje de Alerta', 'Seleccione un cliente para la búsqueda');
            return;
        }
    }
    if (numCbo == 3) { 
    IdClientCbo = $('#cboCliente3').val();
        if ($('#cboCliente3').val() === "") {
            alertify.alert('Mensaje de Alerta', 'Seleccione un cliente para la búsqueda');
            return;
        }
    }
    if (numCbo == 4) {
        IdClientCbo = $('#cboCliente4').val();
        if ($('#cboCliente4').val() === "") {
            alertify.alert('Mensaje de Alerta', 'Seleccione un cliente para la búsqueda');
            return;
        }
    }
    $.blockUI();

    param = {
        IdClient: IdClientCbo

    };


    $.post(directories.report.GetReportCuponClient, param)
        .done(function (data) {
            if (data.status !== "error") {

                //$('#tblCuponCliente > tbody').html('');
                //$('#tblCuponCliente2 > tbody').html('');
                //$('#tblCuponCliente3 > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);

                if (numCbo == 1) {
                    $('#pNumPrestamo').text(data[0].id);
                    $('#pNameClient').text(data[0].name);
                    $('#pTelClient').text(data[0].phone);
                    $('#pDomicilioClient').text(data[0].address);
                    $('#pZoneClient').text(data[0].zone);
                    $('#pCuota').text(data[0].totalCuota);
                    $('#pNumPrestamo1').text(data[0].id);
                    $('#pNameClient1').text(data[0].name);
                    $('#pTelClient1').text(data[0].phone);
                    $('#pDomicilioClient1').text(data[0].address);
                    $('#pZoneClinet1').text(data[0].zone)
                    $('#pCuota1').text(data[0].totalCuota);

                }
                if (numCbo == 2) {
                    $('#pNumPrestamo2').text(data[0].id);
                    $('#pNameClient2').text(data[0].name);
                    $('#pTelClient2').text(data[0].phone);
                    $('#pDomicilioClient2').text(data[0].address);
                    $('#pZoneClient2').text(data[0].zone);
                    $('#pCuota2').text(data[0].totalCuota);
                    $('#pNumPrestamo2_2').text(data[0].id);
                    $('#pNameClient2_2').text(data[0].name);
                    $('#pTelClient2_2').text(data[0].phone);
                    $('#pDomicilioClient2_2').text(data[0].address);
                    $('#pZoneClinet2_2').text(data[0].zone)
                    $('#pCuota2_2').text(data[0].totalCuota);
                }
                if (numCbo == 3) {
                    $('#pNumPrestamo3').text(data[0].id);
                    $('#pNameClient3').text(data[0].name);
                    $('#pTelClient3').text(data[0].phone);
                    $('#pDomicilioClient3').text(data[0].address);
                    $('#pZoneClient3').text(data[0].zone);
                    $('#pCuota3').text(data[0].totalCuota);
                    $('#pNumPrestamo3_3').text(data[0].id);
                    $('#pNameClient3_3').text(data[0].name);
                    $('#pTelClient3_3').text(data[0].phone);
                    $('#pDomicilioClient3_3').text(data[0].address);
                    $('#pZoneClinet3_3').text(data[0].zone)
                    $('#pCuota3_3').text(data[0].totalCuota);
                }
                if (numCbo == 4) {
                    $('#pNumPrestamo4').text(data[0].id);
                    $('#pNameClient4').text(data[0].name);
                    $('#pTelClient4').text(data[0].phone);
                    $('#pDomicilioClient4').text(data[0].address);
                    $('#pZoneClient4').text(data[0].zone);
                    $('#pCuota4').text(data[0].totalCuota);
                    $('#pNumPrestamo4_4').text(data[0].id);
                    $('#pNameClient4_4').text(data[0].name);
                    $('#pTelClient4_4').text(data[0].phone);
                    $('#pDomicilioClient4_4').text(data[0].address);
                    $('#pZoneClinet4_4').text(data[0].zone)
                    $('#pCuota4_4').text(data[0].totalCuota);

                }
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

                    _html += '<tr><td>' + value.dateStart + '</td><td>' + value.dateEnd + '</td><td>' + totalPrestamo + '</td><td >' + totalPagado + '</td><td >' + saldo + '</td><td >' + value.cuotaPayment + '</td>'
                        + '<td>' + value.dateStart + '</td><td>' + value.dateEnd + '</td><td>' + totalPrestamo + '</td><td>' + totalPagado + '</td><td>' + saldo + '</td><td>' + value.cuotaPayment + '</td>';

                });
                _html += '</tbody >';
                if (numCbo == 1) {
                    $('#tblCuponCliente > tbody').html('');
                    $('#tblCuponCliente').append(_html);
                }
                if (numCbo == 2) {
                    $('#tblCuponCliente2 > tbody').html('');
                    $('#tblCuponCliente2').append(_html);
                }
                if (numCbo == 3) { 
                    $('#tblCuponCliente3 > tbody').html('');
                    $('#tblCuponCliente3').append(_html);
                }
                if (numCbo == 4) {
                    $('#tblCuponCliente4 > tbody').html('');
                    $('#tblCuponCliente4').append(_html);
                }
;
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

  