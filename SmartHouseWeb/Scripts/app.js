(function () {
    'use strict';

    angular.module('app', ['underscore', 'ui.router']);

    angular.module('app').config(function ($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise("/state1");

        $stateProvider
        .state('state1', {
            url: "/state1",
            templateUrl: "templates/state1.html"
        })
        .state('state2', {
            url: "/state2",
            templateUrl: "templates/state2.html"
        });
    });
})();