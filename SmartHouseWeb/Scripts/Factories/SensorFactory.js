(function () {
    'use strict';

    angular
        .module('app')
        .factory('SensorFactory', SensorFactory);

    SensorFactory.$inject = ['$http', '_'];

    function SensorFactory($http, _) {
        var service = {
            getStates: getStates
        };

        return service;

        function getStates() {
            return $http.get('/api/sensor/states')
               .then(getStatesComplete)
               .catch(getStatesFailed);

            function getStatesComplete(response) {
               _.forEach(response.data, function(x) {
                    x.Date = new Date(x.Date).toLocaleTimeString();
                });
                return response.data;
            }

            function getStatesFailed(error) {
                console.error('XHR Failed for getStates.' + error.data);
            }
        }
    }
})();