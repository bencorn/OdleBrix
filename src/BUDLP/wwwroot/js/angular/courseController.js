﻿(function () {

    "use strict";

    // Retrieving inventory module
    angular.module("dlp")
        .controller("courseController", courseController)
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

                    // TODO: assert this is usefull ?
                    // if(angular.isUndefined(vm.ngModel)) { vm.ngModel = !!vm.ngModel; }

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

    // code for controller itself
    function courseController($http, $timeout, $routeParams, $scope) {

        var vm = this;

        vm.Module = {};
        vm.moduleId = $routeParams.moduleId;
        vm.contentModuleId = $routeParams.contentModuleId;

        $scope.$watchCollection('[vm.moduleId, vm.contentModuleId]', function () {
            var payload = { ModuleId: vm.moduleId, ContentModuleId: vm.contentModuleId }

            if (vm.moduleId !== undefined) {
                $http.post("/api/module/load", payload)
                .then(function (result) {
                    vm.Module = JSON.parse(result.data);

                    $timeout(function () {
                        $('.ui.embed').embed();
                    })
                },
                function () {

                })
                .finally(function () {

                });
            }
        });

        $(function () {
            // Load Course Topics Unique to Logged in User
            $http.get("/api/modules/get")
                .then(function (result) {
                    vm.Topics = JSON.parse(result.data);

                    $timeout(function () {
                        $('.ui.accordion')
                            .accordion()
                        ;
                    })
                },
                function () {

                })
                .finally(function () {

                });
        });
    }

})();