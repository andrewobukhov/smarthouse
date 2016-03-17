(function () {
    'use strict';

    angular
        .module('app')
        .factory('SensorFactory', SensorFactory);

    SensorFactory.$inject = ['$http'];

    function SensorFactory($http) {
        var service = {
            getStates: getStates
        };

        return service;

        function getStates() {
            return $http.get('/api/sensor/states')
               .then(getStatesComplete)
               .catch(getStatesFailed);

            function getStatesComplete(response) {
                return response.data;
            }

            function getStatesFailed(error) {
                console.error('XHR Failed for getStates.' + error.data);
            }
        }
    }
})();