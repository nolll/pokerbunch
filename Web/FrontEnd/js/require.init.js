define(["jquery"],
    function ($) {

        "use strict";

        if (!Array.indexOf) {
            Array.prototype.indexOf = function (obj, start) {
                for (var i = (start || 0); i < this.length; i++) {
                    if (this[i] === obj) {
                        return i;
                    }
                }
                return -1;
            };
        }

        function hasDataRequireAttribute($elm) {
            var dataAttr = $elm.attr("data-require");
            return typeof dataAttr !== 'undefined' && dataAttr !== false;
        }

        function initRequiredModules(el, callback) {
            var $el = $(el),
                modulesToRequire = $el.attr("data-require"),
                modules = modulesToRequire.split(','),
                data = $el.data();

            delete data.require;
            require(modules, function () {
                var i;

                for (i = 0; i < arguments.length; i++) {
                    arguments[i].init.apply(el, [data]);
                }

                callback(el);
            });
        }

        function initDataClickEvent($data) {
            $data.on("click", "[data-click-event]", function (event) {
                var $me = $(this);
                var meta = $me.metadata();
                if (meta.preventDefaultAction) {
                    event.preventDefault();
                }

                $.publish($me.attr("data-click-event"), [$me.metadata()]);
            });
        }

        function initDataFocusEvent($data) {
            $data.on("submit", "[data-focus-event]", function () {

                var $me = $(this);
                $.publish($me.attr("data-focus-event"), [$me.metadata()]);
            });
        }

        function getElementsWithDataRequireAttribute($data) {
            var elements = [];

            $data.each(function () {
                if (hasDataRequireAttribute($(this))) {
                    elements.push(this);
                }
            });

            $data.find('[data-require]').each(function () {
                elements.push(this);
            });

            return elements;
        }

        function initDataRequire($data, callback) {

            var elements = getElementsWithDataRequireAttribute($data);

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

        function init($data, callback) {
            initDataRequire($data, function () {
                initDataClickEvent($data);
                initDataFocusEvent($data);

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