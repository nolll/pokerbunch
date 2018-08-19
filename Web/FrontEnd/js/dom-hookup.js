define(["./browser", "./dom-modules"],
    function (browser, domModules) {
        "use strict";

        if (!browser.isCapable()) {
            alert("PokerBunch requires a better browser");
        }

        function init() {
            var elements = getElementsWithHookupAttribute();

            var i;
            for (i = 0; i < elements.length; i++) {
                hookupModulesForElement(elements[i], function (e) {
                    elements.splice(elements.indexOf(e), 1);
                });
            }
        }

        function getElementsWithHookupAttribute() {
            var elements = [],
                i,
                nodeList = document.querySelectorAll("[data-hookup]");

            for (i = 0; i < nodeList.length; i++) {
                elements.push(nodeList[i]);
            }

            return elements;
        }

        function hookupModulesForElement(el) {
            var modulesToHookup = el.getAttribute("data-hookup"),
                modules = modulesToHookup.split(",");

            var i;
            for (i = 0; i < modules.length; i++) {
                var moduleName = modules[i];
                var module = domModules.get(moduleName);
                module.init.apply(el);
            }
        }

        return {
            init: init
        };
    }
);