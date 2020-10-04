var param = null;

$(document).ready(function () {

    GetAllProductSale();

    GetVendor();
});

function GetAllProductSale() {

    $.blockUI();

    $.post(directories.sale.GetAllProductSale)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblProductSale > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {
                    var nick = value.nick == null ? '' : value.nick
                    _html += '<tr><td>' + value.id + '</td><td>' + value.cliente + '</td><td>' + value.subTotal + '</td><td>' + value.totalSale + '</td><td>' + value.cuotaPayment + '</td><td>' + value.dateStart + '</td><td>' + value.dateEnd + '</td><td>' + nick + '</td><td>' + '<button type="button" class="btn btn-info" onclick="showModalEditSale(' + value.id + ');"><i class="fas fa-edit"></i> Editar venta</button>' + '</td><td>'
                        + '<button type = "button" class="btn btn-primary" onclick = "showModalEditQuotaSale(' + value.id + ');" > <i class="fas fa-edit"></i> Editar cuota </button > ' + '</td><td>'
                        + '<button class="btn btn-danger" id="" type="button" onclick="DeleteSale(' + value.id + ', ' + `'${value.cliente}'` + ');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

                });

                _html += '</tbody >';

                $('#tblProductSale').append(_html);

                $('#tblProductSale').DataTable({
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


function showModalEditQuotaSale(id) {


    $('#EditSaleModal').modal('show');

    $('#txtIdSale1').val(id);

    GetSaleDetail(id);

}

function GetSaleDetail(id) {

    param = {

        IdSale: id
    };
    $.post(directories.sale.GetSaleDetailById, param)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblSaleDetail > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    if (value.paymentDate === null) {

                        value.paymentDate = '-';
                        _html += '<tr><td>' + value.number + '</td><td >' + value.amount + '</td><td >' + value.status + '</td><td>' + value.paymentDate + '</td><td style="text-align: center;"><a href="javascript:void(0)" data-toggle="tooltip" data-placement="top" ><span class="" ><i class="fas fa-ban"></i></span></a>' + '</td ><td>' + value.PaymentObservation + '</td><td>' + '<button type="button" class="btn btn-primary" onclick="showModalEditCuota(' + value.id + ',' + value.number + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td>';
                    }
                    else {

                        _html += '<tr><td>' + value.number + '</td><td >' + value.amount + '</td><td >' + value.status + '</td><td>' + value.paymentDate + '</td><td style="text-align: center;"><a href="javascript:void(0)" data-toggle="tooltip" data-placement="top" ><span class="" ><i class="fas fa-check-circle"></i></span></a>' + '</td ><td>' + value.PaymentObservation + '</td><td>' + '<button type="button" class="btn btn-primary" onclick="showModalEditCuota(' + value.id + ',' + value.number + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td>';
                    }
                });

                _html += '</tbody >';

                $('#tblSaleDetail').append(_html);
                $('#editCuotaPrestamo').text('Editar Cuotas de Venta N°: ' + id);
                $('#tblSaleDetail').DataTable({
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

function showModalEditCuota(id, number) {

    $('#EditCuotaModal').modal('show');

    $('#txtIdCuota').val(id);

    GetCuotaDetail(id, number);

}

function GetCuotaDetail(id, number) {

    param = {
        IdCuota: id
    };

    $.post(directories.sale.GetCuotaDetail, param)
        .done(function (data) {
            if (data.status !== "error") {
                data = JSON.parse(data.result);
                var a = data[0].paymentDate;
                var b = data[0].observation;
                $('#txtFechaCuota').val(data[0].paymentDate);
                $('#txtObservation').val(data[0].PaymentObservation);
                $('#txtObservationPartial').val(data[0].observationPartial);
                $('#titleCuota').text('Editar cuota N°: ' + number);

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
        observation: $('#txtObservation').val(),
        observationPartial: $('#txtObservationPartial').val()

    };

    $.post(directories.sale.SaveCuotaForId, param)
        .done(function (data) {
            if (data.status !== "error") {

                alertify.success(data.message);
                $('#EditCuotaModal').modal('hide');
                GetSaleDetail($('#txtIdSale1').val());
                GetAllProductSale();


            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}

function showModalEditSale(id) {

    $('#EditSaleDateModal').modal('show');

    GetSaleById(id);

}

function GetSaleById(id) {

    param = {
        IdSale: id
    };

    $.post(directories.sale.GetSaleDetail, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);
                $('#txtDateStartSale').val(data[0].dateStart);
                $('#txtDateEndSale').val(data[0].dateEnd);
                $('#txtIdSale').val(id);
                $('#h5Title').text('Editar Venta N°: ' + id);
                $('#cboVendorEdit').val(data[0].idVendor);
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}

function SaveSaleById() {

    if ($('#txtDateStartSale').val() === "" || $('#txtDateEndSale').val() === "") {
        alertify.alert("Alterta", "las dos fechas son obligatorias");
        return;
    }

    param = {
        IdSale: $('#txtIdSale').val(),
        dateStart: $('#txtDateStartSale').val(),
        dateEnd: $('#txtDateEndSale').val(),
        idVendor: $('#cboVendorEdit').val()
    };

    $.post(directories.sale.SaveSaleById, param)
        .done(function (data) {
            if (data.status !== "error") {

                alertify.success(data.message);
                $('#EditSaleDateModal').modal('hide');
                GetAllProductSale();
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}


function DeleteSale(id, name) {

    alertify.confirm('VENTA', 'Confirma eliminar la venta ' + id + ' del cliente ' + name.bold() + '?', function () {

        param = {
            IdSale: id
        };

        $.post(directories.sale.DeleteSale, param)
            .done(function (data) {
                if (data.status !== "error") {

                    alertify.success(data.message);
                    GetAllProductSale();

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

function GetVendor() {

    $.post('/Vendor/GetAllVendor')
        .done(function (data) {
            if (data.status !== "error") {

                var ComboVendorAdd = $('#cboVendor');
                $("#cboVendor").empty();
                data = JSON.parse(data.result);
                ComboVendorAdd.append($("<option />").val('').text('Seleccione un vendedor'));
                $.each(data, function (key, value) {
                    ComboVendorAdd.append($("<option />").val(value.id).text(value.nickName));
                });

                var ComboVendorEdit = $('#cboVendorEdit');
                $("#cboVendorEdit").empty();
                ComboVendorEdit.append($("<option />").val('').text('Seleccione un vendedor'));
                $.each(data, function (key, value) {
                    ComboVendorEdit.append($("<option />").val(value.id).text(value.nickName));
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