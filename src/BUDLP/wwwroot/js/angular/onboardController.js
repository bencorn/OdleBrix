(function () {

    "use strict";

    angular.module("dlp")
        .controller("onboardController", onboardController)
        .directive('checkbox', function () {
            return {
                restrict: 'E',
                replace: true,
                transclude: true,
                scope: {
                    checked: '&?',
                    disabled: '&?',
                    ngModel: '=ngModel'
                },
                controller: function () {
                    var vm = this;
                    if (angular.isFunction(vm.checked)) { vm.ngModel = !!vm.checked(); }
                    vm.toggle = function () {
                        if (angular.isFunction(vm.disabled) && vm.disabled()) return;
                        vm.ngModel = !vm.ngModel;
                    }
                },
                controllerAs: 'vm',
                bindToController: true,
                require: 'ngModel',
                template: '<div class="ui checkbox">' +
                  '<input type="checkbox" ng-model="vm.ngModel" ng-disabled="vm.disabled()"/>' +
                  '<label ng-click="vm.toggle()" ng-transclude></label>' +
                  '</div>',
                link: function () { }
            };
        });

    function onboardController($http, $timeout, $routeParams, $scope, $rootScope, $sce) {
        var vm = this;
        vm.Step = 1;
        vm.Languages = ['C', 'C++', 'MATLAB', 'Java', 'Python', 'C#'];

        $(function () {

        });

        vm.SetTargetLanguage = function (lang) {
            vm.TargetLanguge = lang;
            var index = vm.Languages.indexOf(lang);
            vm.Languages.splice(index, 1);

            vm.Step = 2;
            $('.pusher').css('background-image', 'url(//d7mj4aqfscim2.cloudfront.net/images/splash-2014/star-pattern1.svg),linear-gradient(to bottom, #0d64b0, #209cf4)')
        };
 
    }
})();