(function () {

    // Creating module
    var dlp = angular.module("dlp", ['ngRoute', 'ngSanitize', 'mgo-angular-wizard', 'checklist-model']).
    config(function ($routeProvider, $locationProvider) {
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
        $routeProvider.when('/course/:moduleId/:contentModuleId/:topicContentModuleId', {
            templateUrl: '/partials/module.html',
            controllerAs: "vm",
            controller: 'courseController'
        });

        $locationProvider.html5Mode(true);
    })
    .run($run);

    // Safely instantiate dataLayer
    var dataLayer = window.dataLayer = window.dataLayer || [];

    $run.$inject = ['$rootScope', '$location'];

    function $run($rootScope, $location) {

        $rootScope.$on('$routeChangeSuccess', function () {

            dataLayer.push({
                event: 'ngRouteChange',
                attributes: {
                    route: $location.path()
                }
            });

        });

    }
})();