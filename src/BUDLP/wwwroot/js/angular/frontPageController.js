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
    function frontPageController($http, $timeout, $scope) {

        // Reference to scope of controller
        var vm = this;

        // On page load book-keeping
        $(function () {
            vm.Topics = {};
            vm.UserProfile = {};
            vm.UserProfile.TargetLanguage = 1;

            // Init dropdown to C
            $('.ui.target.language.dropdown')
                .dropdown('set selected', '1')
            ;

            // Click event on login button
            $('.login-modal')
                .modal('attach events', '.login-button')
            ;
        });

        // Detect changes to target language dropdown
        $scope.$watch('vm.UserProfile.TargetLanguage', function () {
            // Display loading spinner while we work
            $.blockUI({ message: '<div class="ui active dimmer"><div class="ui loader"></div></div>' });

            // Craft payload with target language
            var payload = { Language: vm.UserProfile.TargetLanguage };

            // Retrieve topic list with unique ordering
            $http.post("/api/topics/get", payload)
                        .then(function (result) {
                            vm.Topics = JSON.parse(result.data);

                            // Wait for scope to digest our t(r)opical food
                            $timeout(function () {
                                // Init dropdown and set default to none
                                $('.ui.dropdown.experience').dropdown('set selected', 'None');
                            })
                            // Let'em roam free again
                            $.unblockUI();
                        },
                        function () {

                        })
                        .finally(function () {

                        });
        })

        // Function for click event on sign up form
        vm.CreateProfile = function () {
            // Craft payload object for consumption
            var payload = {
                FullName: vm.UserProfile.FullName,
                Email: vm.UserProfile.Email,
                Password: vm.UserProfile.Password,
                ProfileTopics: vm.Topics,
                TargetLanguage: vm.UserProfile.TargetLanguage
            };

                // Request an account to be created
                $http.post("/api/profile/create", payload)
                    .then(function (result) {
                        // Reset profile form
                        vm.UserProfile = {};

                        // Hide login modal
                        $('.login-modal')
                            .modal('hide')
                        ;

                        // Teleport user to their custom course
                        window.location.href = "/course";
                    },
                    function () {

                    })
                    .finally(function () {

                    });
            
        }

    }

})();