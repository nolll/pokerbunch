define(["jquery", "signalr.hubs"],
    function ($) {
        "use strict";

        function init() {
            var socket = $.connection.runningGameHub;
            socket.client.updateClient = function (message) {
                alert(message);
            };
            $.connection.hub.start().done(function () {
                socket.server.dataUpdated('message from client');
            });
        }

        return {
            init: init
        };
    }
);