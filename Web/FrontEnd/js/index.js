define(['./dom-hookup', './components', './styles'],
    function (domHookup, components) {

        function domReady(callback) {
            document.readyState === 'interactive' || document.readyState === 'complete' ? callback() : document.addEventListener("DOMContentLoaded", callback);
        }

        domReady(function () {
            domHookup.init();
            components.init();
        });
    }
);