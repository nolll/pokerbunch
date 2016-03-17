requirejs.config({
    baseUrl: "/FrontEnd/js",
    paths: {
        async: "lib/async",
        propertyParser: "lib/propertyParser",
        goog: "lib/goog",
        moment: "lib/moment.min",
        vue: "lib/vue",
        text: "lib/text",
        fetch: "lib/fetch.min"
    },
    shim: {
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