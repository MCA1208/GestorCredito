var param = null;
var disabledEdit = "";
var disabledDelete = "";

$(document).ready(function () {


    GetAllZone();


});

function GetAllZone() {

    $.blockUI();
    param = { nameView: 'zona' };

    $.post('/Security/GetAllPermitsByUserProgram', param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                if (data[0][0].ItemArray[5] == false) {
                    $('#btnAddZone').attr('disabled', true);
                }
                if (data[0][1].ItemArray[5] == false) {
                    disabledEdit = "disabled";

                }
                if (data[0][2].ItemArray[5] == false) {
                    disabledDelete = "disabled";
                }

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });


        $.post(directories.zona.getAllZone)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblZone > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    if (value.active == true) {

                        _html += '<tr><td>' + value.id + '</td><td>' + value.description + '</td><td style="text-align: center;"><a href="javascript:void(0)" data-toggle="tooltip" data-placement="top" ><span class="" ><i class="fas fa-check-circle"></i></span></a>' + '</td ><td >' + '<button type="button" class="btn btn-primary btnEditZone" onclick="ShowModalEditZone(' + value.id + ');"'+ disabledEdit +'><i class="fas fa-edit"></i> Editar </button>' + '</td><td>'
                            + '<button class="btn btn-danger btnDeleteZone" id="" type="button" onclick="DeleteZone(' + value.id + ', ' + `'${value.description}'` + ');"'+ disabledDelete +'><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';
                    }
                    else {
                        _html += '<tr><td>' + value.id + '</td><td>' + value.description + '</td><td style="text-align: center;"><a href="javascript:void(0)" data-toggle="tooltip" data-placement="top" ><span class="" ><i class="fas fa-ban"></i></span></a>' + '</td ><td >' + '<button type="button" class="btn btn-primary btnEditZone" onclick="ShowModalEditZone(' + value.id + ');"' + disabledEdit +'><i class="fas fa-edit"></i> Editar </button>' + '</td><td>'
                            + '<button class="btn btn-danger btnDeleteZone" id="" type="button" onclick="DeleteZone(' + value.id + ', ' + `'${value.description}'` + ');"' + disabledDelete +'><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

                    }
                });

                _html += '</tbody >';

                $('#tblZone').append(_html);
                $('#tblZone').DataTable({
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


function AddZone() {

    if ($('#txtDecriptionAdd').val() === "") {

        alertify.alert("Agregar zona","Debe ingresar una descripción");

        return;
    }

    param = {
        description: $('#txtDecriptionAdd').val()
    };

    $.post(directories.zona.AddZone, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                alertify.success(data.message);
                GetAllZone();
                $('#AddZonatModal').modal('hide');
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}


function ShowModalEditZone(id) {

    param = {
        IdZone: id
    };

    $.post(directories.zona.GetZoneById, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                $('#ModifyZonaModal').modal('show');
                $('#txtIdZone').val(data[0].id);
                $('#txtDescription').val(data[0].description);
                $('#cbxActive').prop('checked', data[0].active )
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });


}

function ModifyZone() {

    param = {
        IdZone: $('#txtIdZone').val(),
        Description: $('#txtDescription').val(),
        Active: $('#cbxActive').prop('checked'),
    };

    $.post(directories.zona.ModifyZone, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                if (data.result == 1) {
                    alertify.success(data.message);
                }
                else {
                    alertify.error(data.message);
                }

                GetAllZone();
                $('#ModifyZonaModal').modal('hide');

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });




}

function showModalAddZone() {

    $('#AddZonatModal').modal('show');

}

function DeleteZone(idZone, description) {

    alertify.confirm('Eliminar zona', 'Confirma eliminar la zona ' + description.bold() + '?', function () {

        param = {
            IdZone: idZone
        };

        $.post(directories.zona.DeleteZone, param)
            .done(function (data) {
                if (data.status !== "error") {

                    alertify.success(data.message);
                    GetAllZone();

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