﻿(function () {
    'use strict';

    angular
        .module('app')
        .controller('SensorController', SensorController);

    SensorController.$inject = ['$location', 'SensorFactory'];

    function SensorController($location, SensorFactory) {
        /* jshint validthis:true */
        var vm = this;
        vm.states = [];

        activate();

        function activate() {
            return SensorFactory.getStates()
                .then(function (data) {
                    vm.states = data;
                });
        }
    }
})();
