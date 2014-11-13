requirejs.config({
    baseUrl: "/FrontEnd/js",
    paths: {
        async: "lib/async",
        propertyParser: "lib/propertyParser",
        goog: "lib/goog",
        pubsub: "lib/jquery.pubsub",
        metadata: "lib/jquery.metadata",
        debouncedresize: "lib/jquery.debouncedresize",
        jquery: "lib/jquery-1.11.1.min"
    },
    shim: {
        "pubsub": {
            deps: ["jquery"]
        },
        "metadata": {
            deps: ["jquery"]
        },
        "debouncedresize": {
            deps: ["jquery"]
        }
    },
    waitSeconds: 60
});