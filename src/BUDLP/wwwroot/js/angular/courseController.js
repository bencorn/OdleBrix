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
    function courseController($http, $timeout, $routeParams, $scope, $rootScope, $sce) {

        var vm = this;

        $scope.vm.ShowSidebarMenu = true;
        $rootScope.videoFinished = false;
        $rootScope.ToLearnOverride = false;
        $rootScope.topicModule = true;
        $rootScope.moduleId = $routeParams.moduleId;
        $rootScope.TopicModuleTitle = "";
        $rootScope.NextModuleLink = "";
        $rootScope.PrevModuleLink = "";
        $rootScope.contentModuleId = $routeParams.contentModuleId; // topic module
        $rootScope.topicContentModuleId = $routeParams.topicContentModuleId; // topic module content

        vm.Module = {};
        vm.moduleId = $routeParams.moduleId; // topic
        vm.contentModuleId = $routeParams.contentModuleId; // topic module
        vm.topicContentModuleId = $routeParams.topicContentModuleId; // topic module content
        vm.ContentModules = {};

        var player;

        vm.ToggleSideBar = function () {
            if (!vm.ShowSidebarMenu) {
                $('.ui.sidebar.visible ~ .pusher').css({ 'width': '100%', 'transform' : 'inherit' });
            }
            else {
                $('.ui.sidebar.visible ~ .pusher').css({ 'width': 'calc(100% - 260px)', 'transform': 'translate3d(260px,0,0)' });
            }
        }

        $scope.$watchCollection('[vm.moduleId]', function () {
            var payload = { ModuleId: vm.moduleId, ContentModuleId: vm.contentModuleId }

            if (vm.moduleId !== undefined && vm.contentModuleId !== undefined) {
                $rootScope.topicModule = false;
                vm.topicModule = false;
            }

            if (vm.moduleId !== undefined) {
                var topicModuleLoad = { TopicModuleId: vm.contentModuleId }
                $http.post("/api/topics/topicmodules", topicModuleLoad)
                .then(function (result) {
                    $scope.$parent.vm.ContentModules = JSON.parse(result.data);
                    if (vm.topicContentModuleId === undefined)
                        vm.topicContentModuleId = $scope.$parent.vm.ContentModules[0].TopicModuleContentId;
                },
                function () {

                })
                .finally(function () {

                });
            }
        });

        $scope.$watchCollection('[vm.topicContentModuleId]', function () {

            if (vm.topicContentModuleId !== undefined) {
                var payload = { ContentModuleId: vm.topicContentModuleId }

                $http.post("/api/topics/contentmodule", payload)
                .then(function (result) {
                    vm.Module = JSON.parse(result.data);

                    $timeout(function () {
                        $rootScope.TopicModuleTitle = vm.Module.ModuleTitle;

                        for (var i = 0, len = $scope.$parent.vm.ContentModules.length; i < len; i++) {
                            if ($scope.$parent.vm.ContentModules[i].TopicModuleContentId === parseInt($rootScope.topicContentModuleId)) {
                                if (i === 0) {
                                    $rootScope.NextModuleLink = "/course/" + $rootScope.moduleId + "/" + $rootScope.contentModuleId + "/" + $scope.$parent.vm.ContentModules[i + 1].TopicModuleContentId;
                                    $rootScope.PrevModuleLink = "/course/";
                                }
                                else if (i === len - 1) {
                                    $rootScope.NextModuleLink = "/course/";
                                    $rootScope.PrevModuleLink = "/course/" + $rootScope.moduleId + "/" + $rootScope.contentModuleId + "/" + $scope.$parent.vm.ContentModules[i - 1].TopicModuleContentId;
                                }
                                else {
                                    $rootScope.NextModuleLink = "/course/" + $rootScope.moduleId + "/" + $rootScope.contentModuleId + "/" + $scope.$parent.vm.ContentModules[i + 1].TopicModuleContentId;
                                    $rootScope.PrevModuleLink = "/course/" + $rootScope.moduleId + "/" + $rootScope.contentModuleId + "/" + $scope.$parent.vm.ContentModules[i - 1].TopicModuleContentId;
                                }
                            }
                        }
                        switch (vm.Module.TopicModuleContentType) {
                            // Video ContentType
                            case 1:
                                if (vm.Module.UserLearningState === null && (!$rootScope.ToLearnOverride && vm.Module.Class != 1)) {
                                    // create user learning state for module
                                    $http.post("/api/state/create", { ContentModuleId: $rootScope.topicContentModuleId, TopicId: $routeParams.moduleId, TopicModuleId: $rootScope.contentModuleId })
                                        .then(function (result) {
                                            vm.Module.UserLearningState = [];
                                            vm.Module.UserLearningState[0] = JSON.parse(result.data);
                                        })
                                }
                                else if (vm.Module.UserLearningState[0].LearningState !== 3 && (!$rootScope.ToLearnOverride && vm.Module.Class != 1)) {
                                    $http.post("/api/state/set", { ContentModuleId: $rootScope.topicContentModuleId, State: 1, TopicId: $routeParams.moduleId })
                                        .then(function (result) { console.log('Content state set to started.') });
                                }

                                $('.ui.embed').embed();
                                vm.videoEnd = false;

                                $('.pusher').css('cssText', 'top: 68px !important');

                                var video = $('iframe')[0];
                                player = new YT.Player(video, {
                                    events: {
                                        'onStateChange': onPlayerStateChange
                                    }
                                });
                                break;
                            // Text ContentType
                            case 2:
                                if (vm.Module.UserLearningState === null && (!$rootScope.ToLearnOverride && vm.Module.Class != 1)) {
                                    // create user learning state for module
                                    $http.post("/api/state/create", { ContentModuleId: $rootScope.topicContentModuleId, TopicId: $routeParams.moduleId, TopicModuleId: $rootScope.contentModuleId })
                                        .then(function (result) {
                                            vm.Module.UserLearningState = [];
                                            vm.Module.UserLearningState[0] = JSON.parse(result.data);
                                        })
                                }
                                else if (vm.Module.UserLearningState[0].LearningState !== 3 && (!$rootScope.ToLearnOverride && vm.Module.Class != 1)) {
                                    $http.post("/api/state/set", { ContentModuleId: $rootScope.topicContentModuleId, State: 3, TopicId: $routeParams.moduleId })
                                        .then(function (result) { console.log('Content state set to started.') });
                                }
                                $('.pusher').css('cssText', '');
                                break;
                            case 3:
                                if (vm.Module.UserLearningState === null && (!$rootScope.ToLearnOverride && vm.Module.Class != 1)) {
                                    // create user learning state for module
                                    $http.post("/api/state/create", { ContentModuleId: $rootScope.topicContentModuleId, TopicId: $routeParams.moduleId, TopicModuleId: $rootScope.contentModuleId })
                                        .then(function (result) {
                                            vm.Module.UserLearningState = [];
                                            vm.Module.UserLearningState[0] = JSON.parse(result.data);
                                        })
                                }
                                else if (vm.Module.UserLearningState[0].LearningState !== 3 && (!$rootScope.ToLearnOverride && vm.Module.Class != 1)) {
                                    $http.post("/api/state/set", { ContentModuleId: $rootScope.topicContentModuleId, State: 3, TopicId: $routeParams.moduleId })
                                        .then(function (result) { console.log('Content state set to started.') });
                                }
                                $('.pusher').css('cssText', '');
                                vm.Module.ModuleContentUrl = $sce.trustAsResourceUrl(vm.Module.ModuleContent);
                                break;
                        }
                    })
                },
                function () {

                })
                .finally(function () {

                });
            }

        });

        vm.reloadQuiz = function () {
            vm.Quiz.Correct = false;
            vm.Quiz.ShowDialog = false;
        }

        vm.CheckQuizAnswer = function (q) {
            if (q.QuizType == 0) {
                // radio
                if (q.SelectedAnswer == q.QuizAnswer) {
                    q.Correct = true;
                    $('.green.button')
                        .transition('tada')
                    ;
                    $http.post("/api/quiz/submit", { QuizId: q.QuizId, Response: q.SelectedAnswer, Correct:  q.Correct})
                        .then(function (result) {

                        });
                }
                else {
                    q.Correct = false;
                    $('.green.button')
                        .transition('pulse')
                    ;

                    $http.post("/api/quiz/submit", { QuizId: q.QuizId, Response: q.SelectedAnswer, Correct: q.Correct })
                        .then(function (result) {

                        });
                }
                return;
            }
            else {
                // multiple
                for (var i = 0; i < q.QuizOptions.length; i++) {
                    if (q.Checked[q.QuizOptions[i].QuizOptionId] == q.QuizOptions[i].Answer) {

                    }
                    else {
                        q.Correct = false;
                        $('.green.button')
                            .transition('pulse')
                        ;

                        $http.post("/api/quiz/submit", { QuizId: q.QuizId, Response: JSON.stringify(q.Checked), Correct: q.Correct })
                            .then(function (result) {

                            });

                        return;
                    }
                }
                q.Correct = true;
                $http.post("/api/quiz/submit", { QuizId: q.QuizId, Response: JSON.stringify(q.Checked), Correct: q.Correct })
                    .then(function (result) {

                    });
                $('.green.button')
                    .transition('tada')
                ;
                return;
            }


        };

        vm.replayVideo = function () {
            $('.content.module').removeClass('ui dimmable dimmed');
            player.playVideo();
        };

        vm.replayVideoMarker = function () {
            $('.content.module').removeClass('ui dimmable dimmed');
            player.seekTo(120);
            player.playVideo();
        };

        function onPlayerStateChange(event) {
            if (event.data === YT.PlayerState.ENDED) {
                // if module has a quiz
                if (vm.Module.Quiz) {
                    // get any quizzes for module
                    $http.post("/api/quiz/get", { TopicModuleContentId: vm.Module.TopicModuleContentId })
                        .then(function (result) {
                            vm.Quizzes = JSON.parse(result.data);
                            $timeout(function () {
                                $('.ui.checkbox').checkbox('enable');

                                $('.menu .item').tab();

                                // if quiz response exists, get it and set quiz
                                vm.Quizzes.forEach(function(quiz) {
                                    $http.post("/api/quiz/response", { QuizId: quiz.QuizId })
                                        .then(function (result) {
                                            var response = result.data;
                                            if (response !== "EMPTY") {
                                                response = JSON.parse(response);
                                                quiz.Correct = {};
                                                quiz.Correct = response.Correct;
                                                if (quiz.QuizType == 0) {
                                                    quiz.SelectedAnswer = response.Response;
                                                }
                                                else {
                                                    quiz.Checked = JSON.parse(response.Response);
                                                }
                                            }

                                        });
                                })
                            });
                        });
                }



                // Sets video content module to completed when video has ended
                if ((!$rootScope.ToLearnOverride && vm.Module.Class != 1) && vm.Module.UserLearningState[0].LearningState !== 3) {
                    $http.post("/api/state/set", { ContentModuleId: $rootScope.topicContentModuleId, State: 3, TopicId: $routeParams.moduleId })
                        .then(function (result) { console.log('Video finished: content state set to 3.') });
                }


                for (var i = 0; i < $scope.$parent.vm.ContentModules.length; i++) {
                    if ($scope.$parent.vm.ContentModules[i].TopicModuleContentId === $rootScope.topicContentModuleId) {
                        $scope.$parent.vm.ContentModules[i].UserLearningState[0].LearningState = 3;
                        break;
                    }
                }

                $('.content.module').addClass('ui dimmable dimmed');

                $('.ui.checkbox').checkbox('enable');
            }
        }

        vm.EnableTopicModule = function () {
            $rootScope.ToLearn = true;
            $rootScope.ToLearnOverride = true;
            /* POST change to user profile topic */
            var payload = { TopicId: $rootScope.moduleId };
            $http.post("/api/module/enable", payload);
        }

        $(function () {

            $('.logout_button').click(function () {
                $('#logout_form').submit();
            });

            // Load Course Topics Unique to Logged in User
            $http.get("/api/modules/get")
                .then(function (result) {
                    vm.Topics = JSON.parse(result.data);

                    $timeout(function () {
                        $('.ui.accordion')
                            .accordion()
                        ;

                        for (var i = 0, len = vm.Topics.length; i < len; i++) {
                            if (vm.Topics[i].TopicId == $rootScope.moduleId) {
                                if (vm.Topics[i].ToLearn) {
                                    $rootScope.ToLearn = true;
                                    $rootScope.ToLearnOverride = false;
                                }
                                else if (vm.Topics[i].ToLearnOverride) {
                                    $rootScope.ToLearn = true;
                                    $rootScope.ToLearnOverride = true;
                                }
                                else {
                                    $rootScope.ToLearn = false;
                                    $rootScope.ToLearnOverride = false;
                                }
                                return;
                            }
                        }
                    });
                },
                function () {

                })
                .finally(function () {

                });
        });
    }

})();