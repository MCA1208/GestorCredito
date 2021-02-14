var param = null;

$(document).ready(function () {


    GetAllUser();

    //$( '.mycheckbox' ).on( 'click', function() {
    //    if( $(this).is(':checked') ){
    //        // Hacer algo si el checkbox ha sido seleccionado
    //        alert("El checkbox con valor " + $(this).val() + " ha sido seleccionado");
    //    } else {
    //        // Hacer algo si el checkbox ha sido deseleccionado
    //        alert("El checkbox con valor " + $(this).val() + " ha sido deseleccionado");
    //    }
    //});

});

function GetAllUser() {

    $.blockUI();

    $.post(directories.user.GetAllUser)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblUser > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                if (!data[0]) {
                    return;
                }
                $.each(data, function (key, value) {

                    if (value.active === true) {                                              

                        _html += '<tr><td>' + value.name + '</td><td>' + value.userDescription + '</td><td>' + value.profile + '</td><td style="text-align: center;"><a href="javascript:void(0)" data-toggle="tooltip" data-placement="top" ><span class="" ><i class="fas fa-check-circle"></i></span></a>' + '</td ><td>' + '<button type="button" class="btn btn-primary" onclick="ShowModalEditUser(' + value.id + ');"><i class="fas fa-edit"></i> Editar usuario </button>' + '</td><td>' + '<button type="button" class="btn btn-primary" onclick="ShowModalEditPermits(' + value.id + ', ' + `'${value.name}'` + ');"><i class="fas fa-edit"></i> Permisos </button>' + '</td><td>'
                            + '<button class="btn btn-danger" id="" type="button" onclick="DeleteUser(' + value.id + ', ' + `'${value.name}'` + ');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';
                       
                    }
                    else {

                        _html += '<tr><td>' + value.name + '</td><td>' + value.userDescription + '</td><td>' + value.profile + '</td><td style="text-align: center;"><a href="javascript:void(0)" data-toggle="tooltip" data-placement="top" ><span class="" ><i class="fas fa-ban"></i></span></a>' + '</td ><td>' + '<button type="button" class="btn btn-primary" onclick="ShowModalEditUser(' + value.id + ');"><i class="fas fa-edit"></i> Editar usuario </button>' + '</td><td>' + '<button type="button" class="btn btn-primary" onclick="ShowModalEditPermits(' + value.id + ', ' + `'${value.name}'` + ');"><i class="fas fa-edit"></i> Permisos </button>' + '</td><td>'
                            + '<button class="btn btn-danger" id="" type="button" onclick="DeleteUser(' + value.id + ', ' + `'${value.name}'` + ');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

                       
                    }

                });

                _html += '</tbody >';

                $('#tblUser').append(_html);


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

function ShowModalAddUser() {

    $('#AddUserModal').modal('show');

}


function ShowModalEditUser(id) {

    param = {
        idUser: id
    };

    $.post(directories.user.GetUserById, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                $('#ModifyUserModal').modal('show');
                $('#txtIdUser').val(data[0].id);
                $('#txtUserName').val(data[0].name);
                $('#txtDescriptionUser').val(data[0].userDescription);
                $('#cboTypeUser').val(data[0].idProfile);
                $('#cbxActive').prop('checked', data[0].active)
                $('#txtPass').val('');
                $('#txtRepeatPass').val('');


            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}

function AddUser() {

    if ($('#txtUserNameAdd').val() == "" || $('#cboTypeUserAdd').val() == 0 || $('#txtPassAdd').val() == ""){

        alertify.alert("Usuario","Nombre, tipo de usuario y contraseña son campos requeridos");
        return;
    }
    if ( $('#txtPassAdd').val() != $('#txtRepeatPassAdd').val() ) {

        alertify.alert("Usuario","las contraseña no son iguales");
        return;
    }
  
    param = {
        name: $('#txtUserNameAdd').val(),
        descriptionUser: $('#txtDescriptionUserAdd').val(),
        idProfile: $('#cboTypeUserAdd').val(),
        active: $('#cbxActiveAdd').prop('checked'),
        pass: $('#txtPassAdd').val()

    };

    $.post(directories.user.AddUser, param)
        .done(function (data) {
            if (data.status !== "error") {
                alertify.success(data.message);
                GetAllUser();
                $('#AddUserModal').modal('hide');
                $('#txtUserNameAdd').val('');
                $('#txtDescriptionUserAdd').val('');                
                $('#cboTypeUserAdd').val('');
                $('#txtActiveAdd').val('');
                $('#txtPassAdd').val('');
                $('#txtRepeatPassAdd').val('');
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}

function ModifyUser() {

    if ($('#txtUserName').val() == "" || $('#cboTypeUser').val() == 0){

        alertify.alert("Usuario","Nombre, tipo de usuario son campos requeridos");
        return;
    }
    if ($('#txtPass').val() != "") {
        if ($('#txtPass').val() != $('#txtRepeatPass').val()) {

            alertify.alert("Usuario","las contraseña no son iguales");
            return;
        }
    }
    param = {
        idUser: $('#txtIdUser').val(),
        name: $('#txtUserName').val(),
        UserDescription: $('#txtDescriptionUser').val(),
        idProfile: $('#cboTypeUser').val(),
        active: $('#cbxActive').prop('checked'),
        pass: $('#txtPass').val()

    };

    $.post(directories.user.ModifyUser, param)
        .done(function (data) {
            if (data.status !== "error") {
                alertify.success(data.message);
                GetAllUser();
                $('#ModifyUserModal').modal('hide');
                $('#txtUserName').val('');
                $('#txtDescriptionUser').val('');
                $('#cboTypeUser').val('');
                $('#txtActive').val('');
                $('#txtPass').val('');
                $('#txtRepeatPass').val('');
            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });
}


function ShowModalEditPermits(idUser, name) {

    $('#titlePermits').text('Editar permisos de ' + name);
    $('#txtIdUserPermits').val(idUser);
    GetAllUserPermits(idUser);
    $('#EditPermitsModal').modal('show');
}

function GetAllUserPermits(id) {

    //$.blockUI();

    param = {
        idUser: id

    };

    $.post(directories.user.GetAllUserPermits, param)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblPermitsEdit > tbody').html('');

                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';

                data = JSON.parse(data.result);
                if (!data[0]) {
                    return;
                }
                var i = 0;
                $.each(data, function (key, value) {

                    var app = value[0].ItemArray[2]; 

                    if (value[0].ItemArray[3] == true) 
                        _html += '<tr style="background-color:black; color:white;"><td class="idPermit">' + value[0].ItemArray[0] + '</td><td class="app">' + app + '</td><td class="typePermits">general</td><td style="text-align:center;"><input style="max-width:100%; width: 20px;height: 20px;" id="cbxApplication" type="checkbox" checked ></td>';
                    else
                        _html += '<tr style="background-color:black; color:white;"><td class="idPermit">' + value[0].ItemArray[0] + '</td><td class="app">' + app + '</td><td class="typePermits">general</td><td style="text-align:center;"><input style="max-width:100%; width: 20px;height: 20px;" id="cbxApplication" type="checkbox" ></td>';


                    $.each(data[i], function (key, value) {  

                        if (value.ItemArray[5] == true)
                            _html += '<tr><td class="idPermit">' + value.ItemArray[1] + '</td><td class="app">' + app + '</td><td class="typePermits">' + value.ItemArray[4] + '</td><td style="text-align:center;"><input style="max-width:100%; width: 20px;height: 20px;" id="cbxPermits" type="checkbox" checked ></td>';
                        else
                            _html += '<tr><td class="idPermit">' + value.ItemArray[1] + '</td><td class="app">' + app + '</td><td class="typePermits">' + value.ItemArray[4] + '</td><td style="text-align:center;"><input style="max-width:100%; width: 20px;height: 20px;" id="cbxPermits" type="checkbox"></td>';
                   
                       
                    });

                    i++;

                });

                _html += '</tbody >';
                $('#tblPermitsEdit').append(_html);

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        })
        .always(function () {
          /*  $.unblockUI()*/;
        });
}


function ModifyPermits() {
    $.blockUI();

    $('#tblPermitsEdit tr').each(function () {

        var idPermit = $(this).find('td.idPermit').text();
        var permits = $(this).find('td.typePermits').text();
        var active = $(this).find('td input').prop('checked');
        var idUser = $('#txtIdUserPermits').val();

        if (idPermit != ""  && permits != "" && idUser != "") {

            param = {
                IdPermit: idPermit,
                Permits: permits,
                Active: active,
                IdUser: idUser

            };

            $.post(directories.user.ModifyPermits, param)
                .done(function (data) {
                    if (data.status !== "error") {
                        //se muestra el mensaje al final
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
    })
    alertify.success("Se módifico correctamente");
    GetAllUserPermits(idUser);
}


