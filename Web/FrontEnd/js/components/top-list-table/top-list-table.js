define(["vue", "text!components/top-list-table/top-list-table.html"],
    function(vue, html) {
        "use strict";

        return vue.component("top-list-table", {
            template: html,
            props: [],
            created: function () {
                var x = 0;
            },
            data: function() {
                return {
                    orderBy: "winnings",
                    currencyFormat: "",
                    players: [
                        {
                            rank: 1,
                            name: "Henrik S",
                            winnings: 3727,
                            buyin: 18210,
                            cashout: 21937,
                            time: 21209,
                            gameCount: 38,
                            winRate: 10
                        },
                        {
                            rank: 2,
                            name: "jB",
                            winnings: -2283,
                            buyin: 13900,
                            cashout: 11617,
                            time: 18034,
                            gameCount: 26,
                            winRate: -10
                        }
                    ]
                }
            }
        });
    }
);