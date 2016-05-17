/// <binding />
var gulp = require("gulp");
var less = require("gulp-less");
var requirejsOptimize = require("gulp-requirejs-optimize");

var paths = {
    styles: "./Frontend/Css/styles.less",
    allLess: "./Frontend/Css/*.less",
    assetFolder: "./assets/",
    jsFolder: "./Frontend/js/",
    allJs: "./Frontend/js/**/*.js",
    allHtml: "./Frontend/js/**/*.html",
    scripts: "./Frontend/js/require.loader.js",
    requireConfig: "./Frontend/js/require.config.js",
    requireLoader: "require.loader"
};

gulp.task("default", ["build"]);
gulp.task("build", ["styles", "scripts"]);
gulp.task("styles", taskStyles);
gulp.task("scripts", taskScripts);
gulp.task("watch", taskWatch);

function taskStyles() {
    return gulp.src(paths.styles)
        .pipe(less({ paths: ["./"] }))
        .pipe(gulp.dest(paths.assetFolder));
}

function taskScripts() {
    return gulp.src(paths.scripts)
        .pipe(requirejsOptimize({
            mainConfigFile: paths.requireConfig,
            name: paths.requireLoader,
            out: "scripts.js",
            useStrict: true,
            include: [
                "lib/require.js",
                "cancel-button",
                "cashgame-action-chart",
                "cashgame-chart",
                "cashgame-game-chart",
                "currency-form",
                "delete-confirmation",
                "focus-text-selector",
                "heading-nav",
                "linechart"
            ]
        }))
        .pipe(gulp.dest(paths.assetFolder));
}

function taskWatch() {
    function onChanged(event) {
        console.log("File " + event.path + " was " + event.type + ", running tasks...");
    }

    gulp.watch(paths.allLess, ["styles"]).on("change", onChanged);
    gulp.watch(paths.allJs, ["scripts"]).on("change", onChanged);
    gulp.watch(paths.allHtml, ["scripts"]).on("change", onChanged);
}