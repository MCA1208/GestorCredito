var param = null;

$(document).ready(function () {


    GetAllTypeProduct();


});

function GetAllTypeProduct() {

    //$.blockUI();

    $.post(directories.typeProduct.GetAllTypeProduct)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblTypeProduct > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.id + '</td><td>' + value.name + '</td><td >' + '<button type="button" class="btn btn-primary" onclick="ShowModalEditTypeProduct(' + value.id + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td><td>'
                        + '<button class="btn btn-danger" id="" type="button" onclick="DeleteTypeProduct(' + value.id + ', ' + `'${value.name}'` + ');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

                });

                _html += '</tbody >';

                $('#tblTypeProduct').append(_html);


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


function AddTypeProduct() {

    if ($('#txtNameAdd').val() === "") {

        alertify.alert("Debe ingresar un nombre");

        return;
    }

    param = {
        name: $('#txtNameAdd').val()
    };

    $.post(directories.typeProduct.AddTypeProduct, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                alertify.success(data.message);
                GetAllMark();
                $('#AddTypeProductModal').modal('hide');
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}


function ShowModalEditTypeProduct(id) {

    param = {
        IdTypeProduct: id
    };

    $.post(directories.typeProduct.GetTypeProductById, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                $('#ModifyTypeProductModal').modal('show');
                $('#txtIdTypeProduct').val(data[0].id);
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

function ModifyTypeProduct() {

    param = {
        IdTypeProduct: $('#txtIdTypeProduct').val(),
        name: $('#txtName').val()
    };

    $.post(directories.typeProduct.ModifyTypeProduct, param)
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
                $('#ModifyTypeProductModal').modal('hide');

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });




}

function showModalAddTypeProduct() {

    $('#AddTypeProductModal').modal('show');

}

function DeleteTypeProduct(idTypeProduct, name) {

    alertify.confirm('TIPO PRODUCTO', 'Confirma eliminar el tipo de producto ' + name.bold() + '?', function () {

        param = {
            idTypeProduct: idTypeProduct
        };

        $.post(directories.typeProduct.DeleteTypeProduct, param)
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