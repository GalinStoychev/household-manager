$('.search-field').ready(function () {

    $.connection.hub.start();

    var notification = $.connection.notification;

    $(document).on("click", '#send-notif', function (e) {
        var username = $("#value").val();
        $("#username").remove();
        $(e.target).remove();
        notification.server.sendNotification(username);
    });

    notification.client.addNotification = addNotification;
    notification.client.addMessage = addMessage;
});

function addNotification() {
    $('#received-notif').removeClass('hide');
    $('#received-notif').text("You have received new household invitation. Check your profile.");
}

function addMessage(message) {
    $('#messages').append('<p>' + message + '</p>');
}
