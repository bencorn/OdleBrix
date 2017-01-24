(function () {

    "use strict";

    // Retrieving inventory module
    angular.module("dlp")
        .controller("signInController", signInController);

    // code for controller itself
    function signInController($http, $timeout, $routeParams) {

        var vm = this;

        vm.SignIn = function(){
            $http.post("/api/user/signin")
            .then(function (result) {

            },
            function () {

            })
            .finally(function () {

            });
        }

    }

})();