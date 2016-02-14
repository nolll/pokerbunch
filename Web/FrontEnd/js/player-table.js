define(["vue", "text!player-table.html"],
    function(vue, html) {
        "use strict";

        return vue.extend({
            template: html,
            props: ['players'],
            methods: {
                totalBuyin: function () {
                    var sum = 0;
                    for (var i = 0; i < players.length; i++) {
                        sum += players[i].buyin;
                    }
                    return sum;
                }
            },
            computed: {
                formattedTotalBuyin: function() {
                    return this.totalBuyin();
                }
            },
            ready: function () {
                
            }
        });
    }
);