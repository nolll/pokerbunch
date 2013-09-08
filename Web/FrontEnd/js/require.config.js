requirejs.config({
    baseUrl: "/FrontEnd/js",
    paths: {
        async: "lib/async",
        propertyParser: "lib/propertyParser",
        goog: "lib/goog",
        pubsub: "lib/jquery.pubsub",
        metadata: "lib/jquery.metadata",
        tinynav: "lib/tinynav.min",
        debouncedresize: "lib/jquery.debouncedresize"
    },
    shim: {
        "pubsub": {
            deps: ["jquery"]
        },
        "metadata": {
            deps: ["jquery"]
        },
        "tinynav": {
            deps: ["jquery"]
        },
        "debouncedresize": {
            deps: ["jquery"]
        }
    },
    waitSeconds: 60
});