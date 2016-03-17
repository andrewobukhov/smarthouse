(function () {
    'use strict';

    angular
        .module('app')
        .factory('SocketFactory', SocketFactory);

    SocketFactory.$inject = ['$http'];

    function SocketFactory($http) {
        var service = {
            getStates: getStates
        };

        return service;

        function getStates() {
            return $http.get("/api/socket/states")
                .then(getStatesComplete)
                .catch(getStatesFailed);

            function getStatesComplete(response)
            {
                return response.data;
            }

            function getStatesFailed()
            {
                console.error('XHR Failed for getStates.' + error.data);
            }
        }
    }
})();