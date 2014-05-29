module.exports = function (grunt) {
    grunt.initConfig({
        concat: {
            options: {
                separator: ' '
            },
            basic_and_extras: {
                files: {
                    './../Dnevnik.Web/Scripts/compiled.js': ['./Views/*/*.js'],
                    './../Dnevnik.Web/Content/compiled.css': ['./Views/*/*.css']
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.registerTask('default', ['concat']);
}