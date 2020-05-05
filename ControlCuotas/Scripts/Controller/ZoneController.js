var param = null;

$(document).ready(function () {


    GetAllZone();


});

function GetAllZone() {

    //$.blockUI();

    $.post(directories.zona.getAllZone)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblZone > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.id + '</td><td>' + value.description + '</td><td >' + '<button type="button" class="btn btn-primary" onclick="ShowModalEditZone(' + value.id + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td><td>'
                        + '<button class="btn btn-danger" id="" type="button" onclick="DeleteZone(' + value.id + ', ' + `'${value.description}'` + ');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

                });

                _html += '</tbody >';

                $('#tblZone').append(_html);


            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        })
        .always(function () {
            //$.unblockUI();
        });
}


function AddZone() {

    if ($('#txtDecriptionAdd').val() === "") {

        alertify.alert("Debe ingresar una descripción");

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
        Description: $('#txtDescription').val()
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

    alertify.confirm('ZONA', 'Confirma eliminar la zona ' + description.bold() + '?', function () {

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