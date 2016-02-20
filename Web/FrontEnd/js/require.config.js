requirejs.config({
    baseUrl: "/FrontEnd/js",
    paths: {
        async: "lib/async",
        propertyParser: "lib/propertyParser",
        goog: "lib/goog",
        pubsub: "lib/jquery.pubsub",
        metadata: "lib/jquery.metadata",
        debouncedresize: "lib/jquery.debouncedresize",
        jquery: "lib/jquery-1.11.1.min",
        "knockout-raw": "lib/knockout-3.2.0",
        knockout: "knockout-extended",
        "select-on-focus": "lib/knockout.selectOnFocus",
        moment: "lib/moment.min",
        vue: "lib/vue",
        text: "lib/text",
        fetch: "lib/fetch.min"
    },
    shim: {
        "debouncedresize": {
            deps: ["jquery"]
        },
        "select-on-focus": {
            deps: ["knockout"]
        },
        "fetch": {
            exports: "fetch"
        }
    },
    config: {
        moment: {
            noGlobal: true
        }
    },
    waitSeconds: 60
});