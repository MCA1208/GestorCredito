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

                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);

                var totalPrestamo = 0;
                var totalPagado = 0;
                var saldo = 0;
                if (data[0].totalPrestamo !== null)
                    totalPrestamo = data[0].totalPrestamo.toFixed(2);

                if (data[0].totalPagado !== null)
                    totalPagado = data[0].totalPagado.toFixed(2);

                if (data[0].Saldo !== null)
                    saldo = data[0].Saldo.toFixed(2);

                if (numCbo == 1) {
                    $('#pNumPrestamo').text(data[0].id);
                    $('#pNameClient').text(data[0].name);
                    $('#pTelClient').text('3764185336');
                    $('#pDomicilioClient').text(data[0].address);
                    $('#pZoneClient').text(data[0].zone);
                    $('#pCuota').text(data[0].totalCuota);
                    $('#pNumPrestamo1').text(data[0].id);
                    $('#pNameClient1').text(data[0].name);
                    $('#pTelClient1').text(data[0].phone);
                    $('#pDomicilioClient1').text(data[0].address);
                    $('#pZoneClinet1').text(data[0].zone)
                    $('#pCuota1').text(data[0].totalCuota);
                    $('#pDateStart1').text(data[0].dateStart);
                    $('#pDateEnd1').text(data[0].dateEnd);
                    $('#pTotal1').text(totalPrestamo);
                    $('#pPagado1').text(totalPagado);
                    $('#pSaldo1').text(saldo);
                    $('#pCuo1').text(data[0].cuotaPayment);
                    $('#pDateStart11').text(data[0].dateStart);
                    $('#pDateEnd11').text(data[0].dateEnd);
                    $('#pTotal11').text(totalPrestamo);
                    $('#pPagado11').text(totalPagado);
                    $('#pSaldo11').text(saldo);
                    $('#pCuo11').text(data[0].cuotaPayment);

                }
                if (numCbo == 2) {
                    $('#pNumPrestamo2').text(data[0].id);
                    $('#pNameClient2').text(data[0].name);
                    $('#pTelClient2').text('3764185336');
                    $('#pDomicilioClient2').text(data[0].address);
                    $('#pZoneClient2').text(data[0].zone);
                    $('#pCuota2').text(data[0].totalCuota);
                    $('#pNumPrestamo22').text(data[0].id);
                    $('#pNameClient22').text(data[0].name);
                    $('#pTelClient22').text(data[0].phone);
                    $('#pDomicilioClient22').text(data[0].address);
                    $('#pZoneClinet22').text(data[0].zone)
                    $('#pCuota22').text(data[0].totalCuota);
                    $('#pDateStart2').text(data[0].dateStart);
                    $('#pDateEnd2').text(data[0].dateEnd);
                    $('#pTotal2').text(totalPrestamo);
                    $('#pPagado2').text(totalPagado);
                    $('#pSaldo2').text(saldo);
                    $('#pCuo2').text(data[0].cuotaPayment);
                    $('#pDateStart22').text(data[0].dateStart);
                    $('#pDateEnd22').text(data[0].dateEnd);
                    $('#pTotal22').text(totalPrestamo);
                    $('#pPagado22').text(totalPagado);
                    $('#pSaldo22').text(saldo);
                    $('#pCuo22').text(data[0].cuotaPayment);
                }
                if (numCbo == 3) {
                    $('#pNumPrestamo3').text(data[0].id);
                    $('#pNameClient3').text(data[0].name);
                    $('#pTelClient3').text('3764185336');
                    $('#pDomicilioClient3').text(data[0].address);
                    $('#pZoneClient3').text(data[0].zone);
                    $('#pCuota3').text(data[0].totalCuota);
                    $('#pNumPrestamo33').text(data[0].id);
                    $('#pNameClient33').text(data[0].name);
                    $('#pTelClient33').text(data[0].phone);
                    $('#pDomicilioClient33').text(data[0].address);
                    $('#pZoneClinet33').text(data[0].zone)
                    $('#pCuota33').text(data[0].totalCuota);
                    $('#pDateStart3').text(data[0].dateStart);
                    $('#pDateEnd3').text(data[0].dateEnd);
                    $('#pTotal3').text(totalPrestamo);
                    $('#pPagado3').text(totalPagado);
                    $('#pSaldo3').text(saldo);
                    $('#pCuo3').text(data[0].cuotaPayment);
                    $('#pDateStart33').text(data[0].dateStart);
                    $('#pDateEnd33').text(data[0].dateEnd);
                    $('#pTotal33').text(totalPrestamo);
                    $('#pPagado33').text(totalPagado);
                    $('#pSaldo33').text(saldo);
                    $('#pCuo33').text(data[0].cuotaPayment);
                }
                if (numCbo == 4) {
                    $('#pNumPrestamo4').text(data[0].id);
                    $('#pNameClient4').text(data[0].name);
                    $('#pTelClient4').text('3764185336');
                    $('#pDomicilioClient4').text(data[0].address);
                    $('#pZoneClient4').text(data[0].zone);
                    $('#pCuota4').text(data[0].totalCuota);
                    $('#pNumPrestamo44').text(data[0].id);
                    $('#pNameClient44').text(data[0].name);
                    $('#pTelClient44').text(data[0].phone);
                    $('#pDomicilioClient44').text(data[0].address);
                    $('#pZoneClinet44').text(data[0].zone)
                    $('#pCuota44').text(data[0].totalCuota);
                    $('#pDateStart4').text(data[0].dateStart);
                    $('#pDateEnd4').text(data[0].dateEnd);
                    $('#pTotal4').text(totalPrestamo);
                    $('#pPagado4').text(totalPagado);
                    $('#pSaldo4').text(saldo);
                    $('#pCuo4').text(data[0].cuotaPayment);
                    $('#pDateStart44').text(data[0].dateStart);
                    $('#pDateEnd44').text(data[0].dateEnd);
                    $('#pTotal44').text(totalPrestamo);
                    $('#pPagado44').text(totalPagado);
                    $('#pSaldo44').text(saldo);
                    $('#pCuo44').text(data[0].cuotaPayment);

                }
               
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

  