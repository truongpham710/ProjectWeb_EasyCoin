$('#authenticate').click(function () {
    var key = $('#user')[0].value;
    var secret = $('#secretKey')[0].value;

    $.ajax({
        url: "/token",
        type: "post",
        contenttype: 'x-www-form-urlencoded',
        data: "grant_type=password&username=" + key + "&password=" + secret,
        success: function (response) {           
            var bearerToken = 'Bearer ' + response.access_token;
            window.swaggerUi.api.clientAuthorizations.add('key', new SwaggerClient.ApiKeyAuthorization('Authorization', bearerToken, 'header'));
            alert('authenticated success');
            //location.reload();
            $('#user').css('display', 'none');
            $('#secretKey').css('display', 'none');
            $('#authenticate').css('display', 'none');
        },
        error: function (xhr, ajaxoptions, thrownerror) {           
            alert('User and password incorrect! Please contact enquiry@easybook.com!');
        }
    });
});