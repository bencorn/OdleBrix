(function () {

    // Creating module
    var dlp = angular.module("dlp", ['ngRoute', 'static-include']).
    config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/', {
            templateUrl: '/partials/index.html',
            controllerAs: "vm",
            controller: 'frontPageController'
        });
        $routeProvider.when('/course/', {
            templateUrl: '/partials/learn.html',
            controllerAs: "vm",
            controller: 'courseController'
        });
        $routeProvider.when('/course/:moduleId/:contentModuleId/', {
            templateUrl: '/partials/module.html',
            controllerAs: "vm",
            controller: 'courseController'


        });

        $locationProvider.html5Mode(true);
    });

})();