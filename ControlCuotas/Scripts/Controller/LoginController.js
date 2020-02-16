

function Login() {

    var User = $('#txtUser').val();
    var Pass = $('#txtPass').val();

    if (User === "" || Pass === "") {

        alertify.alert('<strong>Login</strong>', 'Debe cargar los campos de usuario y contraseña', function () { });

        return;
    }

    var data = {
        user: $('#txtUser').val(),
        pass: $('#txtPass').val()

    };

    $.blockUI();

    $.post("LoginUser", data)
        .done(function (data) {

            if (data.status !== "error") {
                location = data.url;

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