$(document).ready(function () {
    // Función para enviar el formulario
    $("#userForm").submit(function (event) {
        event.preventDefault(); // Evita que el formulario se envíe normalmente
        console.log("1");
        // Llama a la función enviarFormulario() cuando se envíe el formulario
        enviarFormulario();
    });

    function enviarFormulario() {
        console.log("Enviando formulario");
        var formData = {
            Name: $("#Name").val(),
            Password: $("#Password").val()
        };
        console.log(formData);
        $.ajax({
            type: "POST",
            url: "/Home/add", // Cambia la URL según tu configuración
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                // Mostrar el modal o mensaje de registro exitoso
                if (data == -1) {
                    $('#usuarioError').modal('show');
                    console.log("El mail ya existe")
                }
                else {
                    $('#usuarioExito').modal('show');
                    console.log(data);
                }
            },
            error: function (error) {
                console.log("El E-mail ya existe 3");
                // Manejar errores aquí
                console.error(error);
                // Puedes realizar acciones adicionales en caso de error.
            }
        });
    }
});
