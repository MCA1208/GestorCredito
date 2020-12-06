var param = null;

$(document).ready(function () {

    GetAllProduct();
    GetTypeProductCombo();  
});

function GetAllProduct() {

    $.blockUI();

    $.post(directories.product.GetAllProduct)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblProduct > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.id + '</td><td>' + value.name + '</td><td>' + value.description + '</td><td>' + value.productName + '</td><td>' + value.markName + '</td><td>' + value.costPrice + '</td><td>' + value.salePrice + '</td><td>' + value.stock + '</td><td >' + '<button type="button" class="btn btn-primary" onclick="ShowModalEditProduct(' + value.id + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td><td>'
                        + '<button class="btn btn-danger" id="" type="button" onclick="DeleteProduct(' + value.id + ', ' + `'${value.name}'` + ');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

                });

                _html += '</tbody >';

                $('#tblProduct').append(_html);
                $('#tblProduct').DataTable({
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


function AddProduct() {
    
    if ($('#txtNameAdd').val() == "" || $('#cboTypeProductAdd').val() == "" || $('#cboMarkAdd').val() == ""
        || $('#txtCostPriceAdd').val() == "" || $('#txtSalePriceAdd').val() == "" || $('#txtStockAdd').val() == "" || $('#txtDescriptionAdd').val() == "") {

        alertify.alert("Agregar Producto","Todos los campos son obligatorios");

        return;
    }

    param = {
        name: $('#txtNameAdd').val(),
        idTypeProduct: $('#cboTypeProductAdd').val(),
        idMark: $('#cboMarkAdd').val(),
        costPrice: $('#txtCostPriceAdd').val(),
        salePrice: $('#txtSalePriceAdd').val(),
        stock: $('#txtStockAdd').val(),
        description: $('#txtDescriptionAdd').val()
    };

    $.post(directories.product.AddProduct, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                alertify.success(data.message);
                GetAllProduct();
                $('#txtNameAdd').val('');
                $('#txtDescriptionAdd').val('');
                $('#cboTypeProductAdd').val('');
                $('#cboMarkAdd').val('');
                $('#txtCostPriceAdd').val('');
                $('#txtSalePriceAdd').val('');
                $('#txtStockAdd').val('');
                $('#AddProductModal').modal('hide');
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}


function ShowModalEditProduct(id) {

    param = {
        IdProduct: id
    };

    $.post(directories.product.GetProductById, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                $('#ModifyProductModal').modal('show');
                $('#txtIdProduct').val(data[0].id);
                $('#txtName').val(data[0].name);
                $('#txtDescription').val(data[0].description);
                $('#cboTypeProduct').val(data[0].idTypeProduct);
                GetComboMarkAnidadoMod(data[0].idTypeProduct, data[0].idMark);
                $('#txtCostPrice').val(data[0].costPrice);
                $('#txtSalePrice').val(data[0].salePrice);
                $('#txtStock').val(data[0].stock);
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });


}

function ModifyProduct() {

    if ($('#txtName').val() == "" || $('#cboTypeProduct').val() == "" || $('#cboMark').val() == ""
        || $('#txtCostPrice').val() == "" || $('#txtSalePrice').val() == "" || $('#txtStock').val() == "" || $('#txtDescription').val() == "") {

        alertify.alert("Modificar Producto","Todos los campos son obligatorios");

        return;
    }
    param = {
        idProduct: $('#txtIdProduct').val(),
        name: $('#txtName').val(),
        idTypeProduct: $('#cboTypeProduct').val(),
        idMark: $('#cboMark').val(),
        costPrice : $('#txtCostPrice').val(),
        salePrice: $('#txtSalePrice').val(),
        stock: $('#txtStock').val(),
        description: $('#txtDescription').val()
    };

    $.post(directories.product.ModifyProduct, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                if (data.result == 1) {
                    alertify.success(data.message);
                }
                else {
                    alertify.error(data.message);
                }

                GetAllProduct();
                $('#ModifyProductModal').modal('hide');

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });




}

function showModalAddProduct() {

    $('#AddProductModal').modal('show');

}

function DeleteProduct(IdProduct, name) {

    alertify.confirm('PRODUCTO', 'Confirma eliminar el producto ' + name.bold() + '?', function () {

        param = {
            idProduct: IdProduct
        };

        $.post(directories.product.DeleteProduct, param)
            .done(function (data) {
                if (data.status !== "error") {

                    alertify.success(data.message);
                    GetAllProduct();

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

$('#cboTypeProductAdd').change(function () {

    if ($('#cboTypeProductAdd').val() != "") {
        GetComboMarkAnidadoAdd($('#cboTypeProductAdd').val());
    }
    else {
        $('#cboMarkAdd').val('').text('Seleccione una marca');
    }
});
$('#cboTypeProduct').change(function () {

    if ($('#cboTypeProduct').val() != "") {
        GetComboMarkAnidadoMod($('#cboTypeProduct').val(), 0);
    }
    else {
        $('#cboMark').val('').text('Seleccione una marca');
    }
});

function GetComboMarkAnidadoAdd(IdTypeProduct) {
    param = {
        idTypeProduct: IdTypeProduct
    }; 
    $.post('/Mark/GetComboMarkAnidado', param)
    .done(function (data) {
        if (data.status !== "error") {

            var ComboMarkAdd = $('#cboMarkAdd');
            $("#cboMarkAdd").empty();

            data = JSON.parse(data.result);
            ComboMarkAdd.append($("<option />").val('').text('Seleccione una marca'));
            $.each(data, function (key, value) {
                ComboMarkAdd.append($("<option />").val(value[1]).text(value[2]));
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
function GetComboMarkAnidadoMod(IdTypeProduct, idMark) {
    param = {
        idTypeProduct: IdTypeProduct
    };
    $.post('/Mark/GetComboMarkAnidado', param)
        .done(function (data) {
            if (data.status !== "error") {

                var ComboMark = $('#cboMark');
                $("#cboMark").empty();

                data = JSON.parse(data.result);
                ComboMark.append($("<option />").val('').text('Seleccione una marca'));
                $.each(data, function (key, value) {
                    ComboMark.append($("<option />").val(value[1]).text(value[2]));
                });

                if (idMark != 0) {
                    $("#cboMark").val(idMark);
                }

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}
