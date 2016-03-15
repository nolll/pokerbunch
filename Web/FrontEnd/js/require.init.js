define(["browser"],
    function (browser) {
        "use strict";

        if (!browser.isCapable()) {
            alert("PokerBunch requires a better browser");
        }

        function initRequiredModules(el, callback) {
            var modulesToRequire = el.getAttribute("data-require"),
                modules = modulesToRequire.split(",");

            require(modules, function () {
                var i;

                for (i = 0; i < arguments.length; i++) {
                    arguments[i].init.apply(el);
                }

                callback(el);
            });
        }

        function getElementsWithDataRequireAttribute() {
            var elements = [],
                i,
                nodeList = document.querySelectorAll("[data-require]");

            for (i = 0; i < nodeList.length; i++) {
                elements.push(nodeList[i]);
            }

            return elements;
        }

        function initDataRequire(callback) {
            var elements = getElementsWithDataRequireAttribute();

            if (elements.length === 0) {
                callback();
            }

            var i;
            for (i = 0; i < elements.length; i++) {
                initRequiredModules(elements[i], function (e) {
                    elements.splice(elements.indexOf(e), 1);

                    if (elements.length === 0) {
                        callback();
                    }
                });
            }
        }

        function init(callback) {
            initDataRequire(function () {
                if (callback !== undefined) {
                    callback();
                }
            });
        }

        return {
            init: init
        };
    }
);