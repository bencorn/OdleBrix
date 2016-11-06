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
    function courseController($http, $timeout, $routeParams, $scope, $rootScope) {

        var vm = this;

        vm.quiz = {
                "QuizType": 0, "QuizAnswerId": 2, "QuizOptions": [
                { "QuizOption": "Numbers", "QuizOptionId": "1" },
                { "QuizOption": "Strings", "QuizOptionId": "2" },
                { "QuizOption": "Characters", "QuizOptionId": "3" },
                { "QuizOption": "Booleans", "QuizOptionId": "4" }
            ]
        }

        $rootScope.videoFinished = false;
        $rootScope.topicModule = true;
        $rootScope.moduleId = $routeParams.moduleId;
        $rootScope.contentModuleId = $routeParams.contentModuleId; // topic module
        $rootScope.topicContentModuleId = $routeParams.topicContentModuleId; // topic module content

        vm.Module = {};
        vm.moduleId = $routeParams.moduleId; // topic
        vm.contentModuleId = $routeParams.contentModuleId; // topic module
        vm.topicContentModuleId = $routeParams.topicContentModuleId; // topic module content
        vm.ContentModules = {};

        var player;

        $scope.$watchCollection('[vm.moduleId, vm.contentModuleId]', function () {
            var payload = { ModuleId: vm.moduleId, ContentModuleId: vm.contentModuleId }
            
            if (vm.moduleId !== undefined && vm.contentModuleId !== undefined) {
                $rootScope.topicModule = false;
                vm.topicModule = false;
            }
            else {
            }

            if (vm.moduleId !== undefined) {
                var topicModuleLoad = { TopicModuleId: vm.contentModuleId }
                $http.post("/api/topics/topicmodules", topicModuleLoad)
                .then(function (result) {
                    $scope.$parent.vm.ContentModules = JSON.parse(result.data);
                },
                function () {

                })
                .finally(function () {

                });

                $http.post("/api/module/load", payload)
                .then(function (result) {
                    vm.Module = JSON.parse(result.data);

                    $timeout(function () {
                        $('.ui.embed').embed();
                        vm.videoEnd = false;

                        var video = $('iframe')[0];
                        player = new YT.Player(video, {
                            events: {
                                'onStateChange': onPlayerStateChange
                            }
                        });
                    })
                },
                function () {

                })
                .finally(function () {

                });
            }
        });

        $scope.$watchCollection('[vm.topicContentModuleId]', function () {

        });

        function onPlayerStateChange(event) {
            if (event.data == YT.PlayerState.ENDED) {
                $('.content.module').addClass('ui dimmable dimmed');

                $('.ui.checkbox').checkbox('enable')

                //$('.quiz-overlay-close').click(function () {
                //    $('.quiz.overlay-dimmer').dimmer('hide');
                //});
            }
        }

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