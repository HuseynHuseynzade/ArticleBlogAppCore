/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    sass = require("gulp-sass");

//Gulp gulp = new Gulp();

gulp.task("copy", function () {
    return gulp.src("node_modules/font-awesome/css/font-awesome.min.css")
        .pipe(gulp.dest("wwwroot/lib"));
});

gulp.task("gulp-sass", function () {
    return gulp.src("wwwroot/scss/style.scss")
        .pipe(sass())
        .pipe(gulp.dest("wwwroot/css"));
});


gulp.task("copy-bootstrap", function () {
    return gulp.src("node_modules/bootstrap/dist/css/bootstrap.css")
        .pipe(gulp.dest("wwwroot/lib"));
});
