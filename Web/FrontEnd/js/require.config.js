requirejs.config({
    baseUrl: "/FrontEnd/js",
    paths: {
        async: "lib/async",
        fetch: "lib/fetch.min",
        goog: "lib/goog",
        moment: "lib/moment.min",
        propertyParser: "lib/propertyParser",
        vue: "lib/vue.min",
        text: "lib/text"
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
    }
});