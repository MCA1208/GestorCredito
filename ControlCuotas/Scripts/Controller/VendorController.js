var param = null;

$(document).ready(function () {


    GetAllVendor();


});

function GetAllVendor() {

    $.blockUI();

    $.post(directories.vendor.GetAllVendor)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblVendor > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                if (!data[0]) {
                    return;
                }
                //data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    _html += '<tr><td>' + value.id + '</td><td>' + value.name + '</td><td>' + value.nickName + '</td><td>' + value.dni + '</td><td>' + value.birthday + '</td><td >' + '<button type="button" class="btn btn-primary" onclick="ShowModalEditVendor(' + value.id + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td><td>'
                        + '<button class="btn btn-danger" id="" type="button" onclick="DeleteVendor(' + value.id + ', ' + `'${value.name}'` + ');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

                });

                _html += '</tbody >';

                $('#tblVendor').append(_html);


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

function showModalAddVendor() {

    $('#AddVendorModal').modal('show');

}


function ShowModalEditVendor(id) {

    param = {
        IdVendor: id
    };

    $.post(directories.vendor.GetVendorById, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                $('#ModifyVendorModal').modal('show');
                $('#txtIdVendor').val(data[0].id);
                $('#txtName').val(data[0].name);
                $('#txtDNI').val(data[0].dni);
                $('#txtBirthday').val(data[0].birthday);
                $('#txtNick').val(data[0].nickName);
      
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}

function AddVendor() {

    if ($('#txtNameAdd').val() === "" || $('#txtDNIAdd').val() === "" || $('#txtBirthdayAdd').val() === "" || $('#txtNickAdd').val() == "" ) {

        alertify.alert("Alterta vendedor","Todos los campos son obligatorios");
        return;
    }

    param = {
        name: $('#txtNameAdd').val(),
        nick: $('#txtNickAdd').val(),
        dni: $('#txtDNIAdd').val(),
        birthDay: $('#txtBirthdayAdd').val()
        
    };

    $.post(directories.vendor.AddVendor, param)
        .done(function (data) {
            if (data.status !== "error") {
                alertify.success(data.message);
                GetAllVendor();
                $('#AddVendorModal').modal('hide');
                $('#txtNameAdd').val('');
                $('#txtNickAdd').val('');
                $('#txtDNIAdd').val('');
                $('#txtBirthdayAdd').val('');
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}

function DeleteVendor(id, name) {

    alertify.confirm('VENDEDOR', 'Confirma eliminar al vendedor ' + name.bold() + '?', function () {

        param = {
            IdVendor: id

        };

        $.post(directories.vendor.DeleteVendor, param)
            .done(function (data) {
                if (data.status !== "error") {

                    alertify.success(data.message);
                    GetAllVendor();

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

function ModifyVendor() {
    if ($('#txtName').val() === "" || $('#txtDNI').val() === "" || $('#txtBirthday').val() === "" || $('#txtNick').val() == "") {

        alertify.alert("Alterta vendedor", "Todos los campos son obligatorios");
        return;
    }
    param = {
        IdVendor: $('#txtIdVendor').val(),
        name: $('#txtName').val(),
        nick: $('#txtNick').val(),
        dni: $('#txtDNI').val(),
        birthday: $('#txtBirthday').val()
    };

    $.post(directories.vendor.ModifyVendor, param)
        .done(function (data) {
            if (data.status !== "error") {

                alertify.success(data.message);
                GetAllVendor();
                $('#ModifyVendorModal').modal('hide');

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}