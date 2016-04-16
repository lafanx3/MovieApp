//Angular service creation
(function () {
    "use strict"

    angular.module("movieModule")
        .factory("$movieService", MovieService);

    function MovieService() {
        var dataToBeSent = {};

        function set(data) {
            dataToBeSent = data;
        }

        function get() {
            return dataToBeSent;
        }

        return {
            set: set,
            get: get
        }
    }
})();