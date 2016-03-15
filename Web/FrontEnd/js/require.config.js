requirejs.config({
    baseUrl: "/FrontEnd/js",
    paths: {
        async: "lib/async",
        propertyParser: "lib/propertyParser",
        goog: "lib/goog",
        jquery: "lib/jquery-1.11.1.min",
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