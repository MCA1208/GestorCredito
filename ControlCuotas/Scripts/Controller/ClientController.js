var param = null;

$(document).ready(function () {


    getzona();

    GetAllClient();


});


function GetAllClient() {

    //$.blockUI();

    $.post(directories.client.GetAllClient)
        .done(function (data) {
            if (data.status !== "error") {

                var sitCred = "";

                $('#tblClient > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {

                    if (value.situationCred === 1)
                        sitCred = "class='fas fa-grin' style='font - size: 60px; color: aqua'";
                    if (value.situationCred === 2)
                        sitCred = "class='fas fa-meh'style='font - size: 60px; color: green'";
                    if (value.situationCred === 3)
                        sitCred = "class='fas fa-angry'style='font - size: 60px; color: red'";
                    
                    _html += '<tr><td>' + value.id + '</td><td>' + value.name + '</td><td>' + value.dni + '</td><td>' + value.address + '</td><td>' + value.phone + '</td><td>' + value.birthdate + '</td><td>' + value.married + '</td><td>' + value.conyuge + '</td><td>' + value.dniMarried + '</td><td>' + value.zone + '</td>><td>' + value.cantidadPrestamo + '</td><td style="text-align: center;"><a href="javascript:void(0)" data-toggle="tooltip" data-placement="top" ><span class="" ><i ' + sitCred + '></i></span></a>' + '</td ><td>' + '<button type="button" class="btn btn-primary" onclick="showModalEditProyect(' + value.id + ');"><i class="fas fa-edit"></i> Editar </button>' + '</td><td>'
                        + '<button class="btn btn-danger" id="" type="button" onclick="DeleteClient(' + value.id + ', ' + `'${ value.name }'` +');"><i class="fas fa-trash-alt"></i> Eliminar </button>' + '</td>';

                });

                _html += '</tbody >';

                $('#tblClient').append(_html);

                $('#tblClient').DataTable({
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
           // $.unblockUI();
        });

}


function AddClient() {


    if ($('#txtNameAdd').val() === "" || $('#txtDNIAdd').val() === "" || $('#txtAddressAdd').val() === ""
        || $('#txtPhoneAdd').val() === "" || $('#cboZonaAdd').val() === "") {

        alertify.alert("Todos los campos son requeridos");

        return;
    }


    param = {
        name: $('#txtNameAdd').val(),
        dni: $('#txtDNIAdd').val(),
        address: $('#txtAddressAdd').val(),
        phone: $('#txtPhoneAdd').val(),
        zone: $('#cboZonaAdd').val(),
        birthDate: $('#txtbirthDateAdd').val(),
        Married: $('#txtMarriedAdd').val(),
        conyuge: $('#txtConyugeAdd').val(),
        dniConyuge: $('#txtDniConyugeAdd').val(),
        cboSitCred: $("#cboSitcredAdd").val()

    };

    $.post(directories.prestamo.createClient, param)
        .done(function (data) {
            if (data.status !== "error") {

                alertify.success(data.message);
                $('#AddClientModal').modal('hide');
                $('#txtNameAdd').val();
                $('#txtDNIAdd').val();
                $('#txtAddressAdd').val();
                $('#txtPhoneAdd').val();
                $('#cboZonaAdd').val();
                $('#txtbirthDateAdd').val();
                $('#txtMarriedAdd').val();
                $('#txtConyugeAdd').val();

                GetAllClient();

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });


}

function getzona() {

    $.post(directories.zona.getComboZona)
        .done(function (data) {
            if (data.status !== "error") {

                var ComboZona = $('#cboZona');
                var ComboZonaAdd = $('#cboZonaAdd');

                $("#cboZona").empty();

                data = JSON.parse(data.result);
                ComboZona.append($("<option />").val('').text('Seleccione una zona'));
                ComboZonaAdd.append($("<option />").val('').text('Seleccione una zona'));
                $.each(data, function (key, value) {

                    ComboZona.append($("<option />").val(value.id).text(value.description));
                    ComboZonaAdd.append($("<option />").val(value.id).text(value.description));

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


function showModalEditProyect(IdClient) {

    param = {

        IdClient: IdClient
    };

    $.post(directories.client.GetClientById, param)
        .done(function (data) {
            if (data.status !== "error") {

                data = JSON.parse(data.result);

                $('#ModifyClientModal').modal('show');
                $('#txtIdClient').val(data[0].id);
                $('#txtName').val(data[0].name);
                $('#txtDNI').val(data[0].dni);
                $('#txtAddress').val(data[0].address);
                $('#txtPhone').val(data[0].phone);
                $('#cboZona').val(data[0].idZone);
                $('#txtbirthDate').val(data[0].birthdate);
                $('#txtMarried').prop('checked', data[0].married);
                $('#txtConyuge').val(data[0].conyuge);
                $('#txtDniConyuge').val(data[0].dniMarried);
                $("#cboSitcred").val(data[0].situationCred);

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}

function ModifyClient() {

    param = {
        IdClient: $('#txtIdClient').val(),
        name: $('#txtName').val(),
        dni: $('#txtDNI').val(),
        address: $('#txtAddress').val(),
        phone: $('#txtPhone').val(),
        zone: $('#cboZona').val(),
        birthDate: $('#txtbirthDate').val(),
        Married: $('#txtMarried').prop("checked"),
        conyuge: $('#txtConyuge').val(),
        dniConyuge: $('#txtDniConyuge').val(),
        cboSitCred: $("#cboSitcred").val()

    };

    $.post(directories.client.ModifyClient, param)
        .done(function (data) {
            if (data.status !== "error") {

                alertify.success(data.message);
                $('#ModifyClientModal').modal('hide');
                GetAllClient();

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        });

}


function showModalAddClient() {

    $('#AddClientModal').modal('show');

}


function DeleteClient(id, name) {

    alertify.confirm('CLIENTE', 'Confirma eliminar al cliente ' + name.bold() + '?', function () {

        param = {
            IdClient: id

        };
        
        $.post("DeleteCli", param)
            .done(function (data) {
                if (data.status !== "error") {

                    alertify.success(data.message);
                    GetAllClient();

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
