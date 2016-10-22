(function () {

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
    function courseController($http, $timeout) {

        var vm = this;

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