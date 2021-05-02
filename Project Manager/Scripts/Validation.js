$(document).ready(function () {
    $("#signin_btn").click(function () {
        var email = $("#email").val();
        $.ajax({
            type: "POST",
            url: '@Url.Action("Login","Acc")',
            dataType: "json",
            data: { Username: email },
            success: function () {

            }
        });
    });
});
