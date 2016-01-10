define(["jquery", "signalr.hubs"],
    function ($) {
        "use strict";

        var slug = 'pokerpoker';

        function init() {
            var socket = $.connection.runningGameHub;
            socket.client.updateClient = function (message) {
                alert(message);
            };
            $.connection.hub.start().done(function () {
                socket.server.joinGame(slug);
            });
            setTimeout(sendMessage, 3000);
        }

        function sendMessage() {
            var socket = $.connection.runningGameHub;
            socket.server.dataUpdated(slug, 'message from client');
        }

        return {
            init: init
        };
    }
);