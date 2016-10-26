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
    function frontPageController($http, $timeout, $routeParams) {

        // Init all dropdowns to Semantic UI dropdown
        var vm = this;
        vm.UserProfile = {};

        // Front-page login button toggle login modal
        $('.login-modal')
            .modal('attach events', '.login-button')
        ;

        vm.Topics = {};

        $(function () {
            $('.ui.target.language.dropdown')
                .dropdown('set selected', '1')
            ;

            var payload = { Language: "1" };

            $http.post("/api/topics/get", payload)
                        .then(function (result) {
                            vm.Topics = JSON.parse(result.data);

                            $timeout(function () {
                                $('.ui.dropdown.experience')
                                .dropdown()
                                ;

                                $('.ui.dropdown.experience').dropdown('set selected', 'None');
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
                ProfileTopics: vm.Topics}

                $http.post("/api/profile/create", payload)
                    .then(function (result) {
                        vm.UserProfile = {};
                        $('.login-modal')
                            .modal('hide')
                        ;
                        window.location.href = "/course";
                    },
                    function () {

                    })
                    .finally(function () {

                    });
            
        }

    }

})();