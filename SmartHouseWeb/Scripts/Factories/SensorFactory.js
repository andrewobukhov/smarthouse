(function () {
    'use strict';

    angular
        .module('app')
        .factory('SensorFactory', SensorFactory);

    SensorFactory.$inject = ['$http'];

    function SensorFactory($http) {
        var service = {
            getData: getData
        };

        return service;

        function getData() { }
    }
})();