(function () {

    "use strict";

    // Retrieving inventory module
    angular.module("dlp")
        .controller("frontPageController", frontPageController)
        .directive('checkbox', function() {
        return {
            restrict: 'E',
            replace: true,
            transclude: true,
            scope: {
                checked: '&?',
                disabled: '&?',
                ngModel: '=ngModel'
            },
            controller: function() {
                var vm = this;

                // TODO: assert this is usefull ?
                // if(angular.isUndefined(vm.ngModel)) { vm.ngModel = !!vm.ngModel; }

                if(angular.isFunction(vm.checked)) { vm.ngModel = !!vm.checked(); }

                vm.toggle = function() {
                    if(angular.isFunction(vm.disabled) && vm.disabled()) return;
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
            link: function() { }
        };
    });

    // code for controller itself
    function frontPageController($http, $timeout) {

        var vm = this;
        vm.UserProfile = {};
        vm.SelectedLanguage = {};

        $(function () {
            var payload = { Language: "1" };

            $http.post("/api/topics/get", payload)
                        .then(function (result) {
                            vm.SelectedLanguage.Topics = JSON.parse(result.data);

                            $timeout(function () {
                                $('.ui.dropdown')
                                .dropdown()
                                ;
                            })
                        },
                        function () {

                        })
                        .finally(function () {

                        });
        });

        vm.CreateProfile = function(){
            var payload = {
                FullName: vm.UserProfile.FullName,
                Email: vm.UserProfile.Email,
                Password: vm.UserProfile.Password,
                ProfileTopics: vm.SelectedLanguage.Topics}

                $http.post("/api/profile/create", payload)
                    .then(function (result) {
                        vm.UserProfile = {};
                        $('.login-modal')
                            .modal('hide')
                        ;
                    },
                    function () {

                    })
                    .finally(function () {

                    });
            
        }

    }

})();