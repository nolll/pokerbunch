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
        moment: "lib/moment.min",
        vue: "lib/vue",
        text: "lib/text",
        fetch: "lib/fetch.min"
    },
    shim: {
        "debouncedresize": {
            deps: ["jquery"]
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