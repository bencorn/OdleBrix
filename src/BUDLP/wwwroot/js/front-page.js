$(function () {
    $('.sign-in.process').click(function () {
        var username = $('.email-field').val();
        var password = $('.password-field-inp').val();

        var data = { Username: username, Password: password };

        $.ajax({
            url: 'api/user/login',
            datatype: 'json',
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(data),
            success: function (data) {
                window.location.replace(data);
            }
        });
    });
})

