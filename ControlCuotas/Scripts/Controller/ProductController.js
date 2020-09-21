var param = null;

$(document).ready(function () {


    GetAllProduct();


});

function GetAllProduct() {

    //$.blockUI();

    $.post(directories.product.GetAllProduct)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblProduct > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.id + '</td><td>' + value.name + '</td><td>' + value.idTypeProduct + '</td><td>' + value.idMark + '</td><td>' + value.costPrice + '</td><td>' + value.salePrice + '</td><td>' + value.stock + '</td><td >' + '<button type="button" class="btn btn-primary" onclick="ShowModalEditProduct(' + value.id + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td><td>'
                        + '<button class="btn btn-danger" id="" type="button" onclick="DeleteProduct(' + value.id + ', ' + `'${value.name}'` + ');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

                });

                _html += '</tbody >';

                $('#tblProduct').append(_html);


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


function AddProduct() {

    if ($('#txtNameAdd').val() === "") {

        alertify.alert("Debe ingresar un nombre");

        return;
    }

    param = {
        name: $('#txtNameAdd').val(),
        idTypeProduct: $('#cboTypeProductAdd').val(),
        idMark: $('#cboMarkAdd').val(),
        costPrice: $('#txtCostPriceAdd').val(),
        salePrice: $('#txtSalePriceAdd').val(),
        stock: $('#txtStockAdd').val()
    };

    $.post(directories.product.AddProduct, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                alertify.success(data.message);
                GetAllMark();
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
                $('#cboTypeProduct').val(data[0].idTypeProduct);
                $('#cboMarkAdd').val(data[0].idMark);
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

    param = {
        idProduct: $('#txtIdProduct').val(),
        name: $('#txtName').val(),
        idTypeProduct: $('#cboTypeProduct').val(data[0].idTypeProduct),
        idMark: $('#cboMark').val(data[0].idMark),
        costPrice : $('#txtCostPrice').val(data[0].costPrice),
        salePrice: $('#txtSalePrice').val(data[0].salePrice),
        stock: $('#txtStock').val(data[0].stock)
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

                GetAllMark();
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

function DeleteProduct(idProduct, name) {

    alertify.confirm('PRODUCTO', 'Confirma eliminar el producto ' + name.bold() + '?', function () {

        param = {
            idTypeProduct: idTypeProduct
        };

        $.post(directories.product.DeleteProduct, param)
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