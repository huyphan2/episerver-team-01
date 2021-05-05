const gulp = require("gulp");
const sass = require("gulp-sass");
const browserSync = require("browser-sync").create();
const del = require('del');

//compile scss into css
function style() {
  return gulp
    .src("src/assets/scss/**/*.scss")
    .pipe(sass().on("error", sass.logError))
    .pipe(gulp.dest("dist/assets/css"))
    .pipe(browserSync.stream());
}

function html() {
  return gulp
    .src(["src/*.html", "src/**/*.html"])
    .pipe(gulp.dest("dist/"))
    .pipe(browserSync.reload({ stream: true }));
}

function script() {
  return gulp
    .src("src/assets/script/**/*.js")
    .pipe(gulp.dest("dist/assets/script"))
    .pipe(browserSync.reload({ stream: true }));
}

function images() {
  return gulp
    .src("src/assets/images/**/*")
    .pipe(gulp.dest("dist/assets/images"))
    .pipe(browserSync.reload({ stream: true }));
}

function libs() {
  return gulp
    .src("src/assets/libs/**/*")
    .pipe(gulp.dest("dist/assets/libs"))
    .pipe(browserSync.reload({ stream: true }));
}

function font() {
  return gulp
    .src("src/assets/fonts/**/*")
    .pipe(gulp.dest("dist/assets/fonts"))
    .pipe(browserSync.reload({ stream: true }));
}

function watch() {
  browserSync.init({
    server: {
      baseDir: "./dist",
      index: "/index.html",
    },
  });
  gulp.watch("src/assets/scss/**/*.scss", style);
  gulp.watch(["src/*.html", "src/**/*.html"], html);
  gulp.watch("src/assets/script/**/*.js", script);
  gulp.watch("src/assets/images/**/*", images);
  gulp.watch("src/assets/libs/**/*", libs);
  gulp.watch("src/assets/font/**/*", font);
}

gulp.task("clean", function() {
	return del("dist/**", {force: true});
})

gulp.task(
  "build",
  gulp.series("clean", gulp.parallel(style, html, script, images, libs, font))
);

exports.style = style;
exports.watch = watch;
