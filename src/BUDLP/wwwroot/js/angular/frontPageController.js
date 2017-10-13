(function () {

    "use strict";

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

    function frontPageController($http, $timeout, $scope) {

        var vm = this;

        $(function () {
            vm.Topics = {};
            vm.UserProfile = {};
            vm.UserProfile.TargetLanguage = 1;

            // Init dropdown to C
            $('.ui.target.language.dropdown')
                .dropdown('set selected', '1')
            ;

            $('.login-modal')
                .modal('attach events', '.login-button')
            ;
        });

        vm.AssessPastExperience = function(exp, checked, b){
            if (exp[0] == '0') {
                exp.splice(0, 1);
                exp.push(checked);
            }
            else if (exp.length == 1 && checked == exp[0]) {
                exp.push('0');
                var index = exp.indexOf(checked);
                exp.splice(index, 1);
            }
        }

        vm.CreateProfile = function () {
            var payload = {
                FirstName: vm.UserProfile.FirstName,
                LastName: vm.UserProfile.LastName,
                Email: vm.UserProfile.Email,
                Password: vm.UserProfile.Password
            };

            var strongRegex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})");

            if (strongRegex.test(vm.UserProfile.Password)) {
                $http.post("/api/profile/create", payload)
                    .then(function (result) {
                        // Reset profile form
                        vm.UserProfile = {};

                        // Teleport user to their custom course
                        window.location.href = "/onboard";
                    },
                    function () {
                    })
                    .finally(function () {
                    });
            }
            else {
                vm.UserProfile.PasswordWarning = "Password must be 8 characters or longer, contain at least 1 lowercase, 1 uppercase character, 1 number, and 1 special character.";
                $('.password.register.field').focus();
            }           
        }
    }
})();