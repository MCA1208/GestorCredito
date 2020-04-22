var param = null;

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
        dateStart: $('#txtDateStart').val(),
        dateEnd: $('#txtDateEnd').val()
    };

    $.post(directories.prestamo.AddPrestamo, param)
        .done(function (data) {
            if (data.status !== "error") {

                alertify.success(data.message);
                $('#cboCliente').val('');
                $('#txtConcepto').val('');
                $('#txtAmount').val('');
                $('#txtAmountInterest').val('');
                $('#txtQuantity').val('');
                $('#txtQuantity').val('');
                $('#txtDateEnd').val('');

                $("#AddPrestamoModal").modal("hide");
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

                    _html += '<tr><td>' + value.id + '</td><td>' + value.cliente + '</td><td >' + value.description + '</td><td>' + value.amount + '</td><td>' + value.amountInterest + '</td><td>' + value.cuotaPayment + '</td><td>' + value.dateStart + '</td><td>' + value.dateEnd + '</td><td>' + '<button type="button" class="btn btn-info" onclick="showModalEditPrestamo(' + value.id + ');"><i class="fas fa-edit"></i> Editar préstamo</button>' + '</td><td>'
                        + '<button type = "button" class="btn btn-primary" onclick = "showModalEditProyect(' + value.id + ');" > <i class="fas fa-edit"></i> Editar cuota </button > ' + '</td><td>'
                        + '<button class="btn btn-danger" id="" type="button" onclick="DeletePrestamo(' + value.id + ', ' + `'${value.cliente}'` + ');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

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
                        _html += '<tr><td>' + value.id + '</td><td>' + value.number + '</td><td >' + value.amount + '</td><td >' + value.status + '</td><td>' + value.paymentDate + '</td><td style="text-align: center;"><a href="javascript:void(0)" data-toggle="tooltip" data-placement="top" ><span class="" ><i class="fas fa-ban"></i></span></a>' + '</td ><td>' + value.observation + '</td><td>' + '<button type="button" class="btn btn-primary" onclick="showModalEditCuota(' + value.id + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td>';
                    }
                    else {

                        _html += '<tr><td>' + value.id + '</td><td>' + value.number + '</td><td >' + value.amount + '</td><td >' + value.status + '</td><td>' + value.paymentDate + '</td><td style="text-align: center;"><a href="javascript:void(0)" data-toggle="tooltip" data-placement="top" ><span class="" ><i class="fas fa-check-circle"></i></span></a>' + '</td ><td>' + value.observation + '</td><td>' + '<button type="button" class="btn btn-primary" onclick="showModalEditCuota(' + value.id + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td>';
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

function showModalEditCuota(id) {

    $('#EditCuotaoModal').modal('show');

    $('#txtIdCuota').val(id);

    GetCuotaDetail(id);

}

function GetCuotaDetail(id) {

    param = {
        IdCuota: id
    };

    $.post(directories.prestamo.GetCuotaDetail, param)
        .done(function (data) {
            if (data.status !== "error") {
                data = JSON.parse(data.result);
                var a = data[0].paymentDate;
                var b = data[0].observation;
                $('#txtFechaCuota').val(data[0].paymentDate);
                $('#txtObservation').val(data[0].observation);

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}

function SaveCuotaForId() {

    param = {                 
        IdCuota: $('#txtIdCuota').val(),
        fecha: $('#txtFechaCuota').val(),
        observation: $('#txtObservation').val()

    };

    $.post(directories.prestamo.SaveCuotaForId, param)
        .done(function (data) {
            if (data.status !== "error") {

                alertify.success(data.message);
                $('#EditCuotaoModal').modal('hide');
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

function showModalEditPrestamo(id) {

    $('#EditPrestamoDateModal').modal('show');

    GetPrestamnoById(id);

}

function GetPrestamnoById(id) {

    param = {
        IdPrestamo: id
    };

    $.post(directories.prestamo.GetPrestamoDetail, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);
                $('#txtDateStartPrestamo').val(data[0].dateStart); 
                $('#txtDateEndPrestamo').val(data[0].dateEnd);
                $('#txtIdPrestamo').val(id);
                $('#h5Title').text('Editar Prestamo N°: ' + id);
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}

function SavePrestamoForId() {

    if ($('#txtDateStartPrestamo').val() === "" || $('#txtDateEndPrestamo').val() === "") { 
        alertify.alert("Alterta", "las dos fechas son obligatorias");
        return;
    }

    param = {
        IdPrestamo: $('#txtIdPrestamo').val(),
        dateStart: $('#txtDateStartPrestamo').val(),
        dateEnd: $('#txtDateEndPrestamo').val()
    };

    $.post(directories.prestamo.SavePrestamoForId, param)
        .done(function (data) {
            if (data.status !== "error") {

                alertify.success(data.message);
                $('#EditPrestamoDateModal').modal('hide');
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


function DeletePrestamo(id, name) {

    alertify.confirm('CLIENTE', 'Confirma eliminar el préstamo ' + id + ' del cliente ' + name.bold() + '?', function () {

        param = {
            IdPrestamo: id
        };

        $.post(directories.prestamo.DeletePrestamo, param)
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

    },
        function () {
            alertify.error('Se canceló la operación');
        });

}