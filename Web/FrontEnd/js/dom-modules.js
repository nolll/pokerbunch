define([
    "cancel-button",
    "cashgame-action-chart",
    "cashgame-chart",
    "cashgame-game-chart",
    "delete-confirmation",
    "heading-nav",
    "currency-form",
    "focus-text-selector"
],
    function (cancelButton, cashgameActionChart, cashgameChart, cashgameGameChart, deleteConfirmation, headingNav, currencyForm, focusTextSelector) {

        "use strict";

        var modules = {
            "cancel-button": cancelButton,
            "cashgame-action-chart": cashgameActionChart,
            "cashgame-chart": cashgameChart,
            "cashgame-game-chart": cashgameGameChart,
            "delete-confirmation": deleteConfirmation,
            "heading-nav": headingNav,
            "currency-form": currencyForm,
            "focus-text-selector": focusTextSelector
        };

        function get(name)
        {
            return modules[name];
        }

        return {
            get: get
        };
    }
);