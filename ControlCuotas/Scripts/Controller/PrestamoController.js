﻿var param = null;

$(document).ready(function () {
    
    getClientCombo();

    GetAllPrestamo();


});



function getClientCombo() {

    $.post(directories.prestamo.GetClientCombo)
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


function AddPrestamo() {

    param = {

        cboCliente: $('#cboCliente').val(),
        concepto: $('#txtConcepto').val(),
        amount: $('#txtAmount').val(),
        amountInterest: $('#txtAmountInterest').val(),
        quantity: $('#txtQuantity').val(),
        dateEnd: $('#txtDateEnd').val()
    };

    $.post(directories.prestamo.AddPrestamo, param)
        .done(function (data) {
            if (data.status !== "error") {

                alertify.success(data.message);
                GetAllPrestamo();

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}

function GetAllPrestamo() {

    $.blockUI();

    $.post(directories.prestamo.GetAllPrestamo)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblPrestamo > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.id + '</td><td>' + value.cliente + '</td><td >' + value.description + '</td><td>' + value.amount + '</td><td>' + value.amountInterest + '</td><td>' + value.cuotaPayment + '</td><td>' + value.dateEnd + '</td><td>' + '<button type="button" class="btn btn-primary" onclick="showModalEditProyect(' + value.id + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td>';

                });

                _html += '</tbody >';

                $('#tblPrestamo').append(_html);

                $('#tblPrestamo').DataTable({
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


function showModalEditProyect(id) {

    
    $('#EditPrestamoModal').modal('show');

    $('#txtIdPrestamo').val(id);

    GetPrestamoDetail(id);

}

function showModalAddProyect() {

    $('#AddPrestamoModal').modal('show');

}

function GetPrestamoDetail(id) {

    param = {

        IdPrestamo: id
    };
    $.post(directories.prestamo.GetPrestamoDetailById, param)
        .done(function (data) {
            if (data.status !== "error") {

                

                $('#tblPrestamoDetail > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    if (value.paymentDate === null) {

                        value.paymentDate = '-';
                        _html += '<tr><td>' + value.id + '</td><td>' + value.number + '</td><td >' + value.amount + '</td><td >' + value.status + '</td><td>' + value.paymentDate + '</td><td style="text-align: center;"><a href="javascript:void(0)" data-toggle="tooltip" data-placement="top" ><span class="" ><i class="fas fa-ban" onclick="changeEstatusCuota(' + value.id + ')"></i></span></a>' + '</td >';
                    }
                    else {

                        _html += '<tr><td>' + value.id + '</td><td>' + value.number + '</td><td >' + value.amount + '</td><td >' + value.status + '</td><td>' + value.paymentDate + '</td><td style="text-align: center;"><a href="javascript:void(0)" data-toggle="tooltip" data-placement="top" ><span class="" ><i class="fas fa-check-circle" onclick="changeEstatusCuota(' + value.id + ')"></i></span></a>' + '</td >';
                    }
                });

                _html += '</tbody >';

                $('#tblPrestamoDetail').append(_html);

                $('#tblPrestamoDetail').DataTable({
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
        });

}

function changeEstatusCuota(id) {

    param = {
        IdCuota: id
    };

    $.post(directories.prestamo.ChangeEstatusCuota, param)
        .done(function (data) {
            if (data.status !== "error") {

                alertify.success(data.message);
                GetPrestamoDetail($('#txtIdPrestamo').val());
                GetAllPrestamo();
               

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}