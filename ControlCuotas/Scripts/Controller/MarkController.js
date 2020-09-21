var param = null;

$(document).ready(function () {


    GetAllMark();


});

function GetAllMark() {

    //$.blockUI();

    $.post(directories.mark.GetAllMark)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblMark > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.id + '</td><td>' + value.name + '</td><td >' + '<button type="button" class="btn btn-primary" onclick="ShowModalEditMark(' + value.id + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td><td>'
                        + '<button class="btn btn-danger" id="" type="button" onclick="DeleteMark(' + value.id + ', ' + `'${value.name}'` + ');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

                });

                _html += '</tbody >';

                $('#tblMark').append(_html);


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


function AddMark() {

    if ($('#txtNameAdd').val() === "") {

        alertify.alert("Debe ingresar un nombre");

        return;
    }

    param = {
        name: $('#txtNameAdd').val()
    };

    $.post(directories.mark.AddMark, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                alertify.success(data.message);
                GetAllMark();
                $('#AddMarkModal').modal('hide');
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}


function ShowModalEditMark(id) {

    param = {
        IdMark: id
    };

    $.post(directories.mark.GetMarkById, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                $('#ModifyMarkModal').modal('show');
                $('#txtIdMark').val(data[0].id);
                $('#txtName').val(data[0].name);
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });


}

function ModifyMark() {

    param = {
        idMark: $('#txtIdMark').val(),
        name: $('#txtName').val()
    };

    $.post(directories.mark.ModifyMark, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                if (data.result == 1) {
                    alertify.success(data.message);
                }
                else {
                    alertify.error(data.message);
                }

                GetAllMark();
                $('#ModifyMarkModal').modal('hide');

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });




}

function showModalAddMark() {

    $('#AddMarkModal').modal('show');

}

function DeleteMark(idMark, name) {

    alertify.confirm('MARCA', 'Confirma eliminar la marca ' + name.bold() + '?', function () {

        param = {
            idMark: idMark
        };

        $.post(directories.mark.DeleteMark, param)
            .done(function (data) {
                if (data.status !== "error") {

                    alertify.success(data.message);
                    GetAllMark();

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