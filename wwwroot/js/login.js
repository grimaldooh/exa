$(document).ready(function () {
    // Función para enviar el formulario
    $("#loginForm").submit(function (event) {
        event.preventDefault(); // Evita que el formulario se envíe normalmente
        validarInicioSesion();
    });

    function validarInicioSesion() {
        console.log("Enviando login")

        var formData = {
        Name : $("#Name").val(),
        Password : $("#Password").val(),
        Id: 10
        };
        console.log(formData);
        $.ajax({
            type: "POST",
            url: "/Home/ValidateUser",
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (respuesta) {
                if (respuesta==1) {
                    alert("Bienvenido");
                } else {
                    alert("Inicio de sesión incorrecto. Verifica tu Nombre y contraseña.");
                }
            },
            error: function (error) {
                console.error(error);
                alert("Hubo un error en el inicio de sesión. Por favor, inténtalo de nuevo.");
            }
        });
    }
});


