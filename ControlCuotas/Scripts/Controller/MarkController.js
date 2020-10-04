var param = null;

$(document).ready(function () {

    GetAllMark();
    GetTypeProductCombo() 

});

function GetAllMark() {

    $.blockUI();

    $.post(directories.mark.GetAllMark)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblMark > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.id + '</td><td>' + value.name + '</td><td>' + value.nameTypeProduct + '</td><td>' + '<button type="button" class="btn btn-primary" onclick="ShowModalEditMark(' + value.id + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td><td>'
                        + '<button class="btn btn-danger" id="" type="button" onclick="DeleteMark(' + value.id + ', ' + `'${value.name}'` + ');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

                });

                _html += '</tbody >';

                $('#tblMark').append(_html);
                $('#tblMark').DataTable({
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


function AddMark() {

    if ($('#txtNameAdd').val() == "" || $('#cboTypeProductAdd').val() == "") {

        alertify.alert("Agregar Marca","Todos los campos son obligatorios");

        return;
    }

    param = {
        name: $('#txtNameAdd').val(),
        idTypeProduct: $('#cboTypeProductAdd').val()
    };

    $.post(directories.mark.AddMark, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                alertify.success(data.message);
                GetAllMark();
                $('#txtNameAdd').val('');
                $('#cboTypeProductAdd').val('');
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
                $('#cboTypeProduct').val(data[0].idTypeProduct);
                
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

    if ($('#txtName').val() == "" || $('#cboTypeProduct').val() == "") {

        alertify.alert("Modificar Marca", "Todos los campos son obligatorios");

        return;
    }
    param = {
        idMark: $('#txtIdMark').val(),
        name: $('#txtName').val(),
        idTypeProduct: $('#cboTypeProduct').val()
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

function GetTypeProductCombo() {

    $.post('/TypeProduct/GetAllTypeProduct')
        .done(function (data) {
            if (data.status !== "error") {

                var ComboTypeProdAdd = $('#cboTypeProductAdd');
                var ComboTypeProd = $('#cboTypeProduct');
                $("#cboTypeProductAdd").empty();
                $("#cboTypeProduct").empty();

                data = JSON.parse(data.result);

                ComboTypeProdAdd.append($("<option />").val('').text('Seleccione un tipo de producto'));
                ComboTypeProd.append($("<option />").val('').text('Seleccione un tipo de producto'));

                $.each(data, function (key, value) {

                    ComboTypeProdAdd.append($("<option />").val(value.id).text(value.name));
                    ComboTypeProd.append($("<option />").val(value.id).text(value.name));
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