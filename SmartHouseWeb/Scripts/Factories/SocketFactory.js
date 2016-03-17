(function () {
    'use strict';

    angular
        .module('app')
        .factory('SocketFactory', SocketFactory);

    SocketFactory.$inject = ['$http'];

    function SocketFactory($http) {
        var service = {
            getStates: getStates,
            changeState: changeState
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

        function changeState(index) {

            var data = {
                Index: index
            };

            return $http.post("/api/socket/change", data)
                .then(postComplete)
                .catch(postFailed);

            function postComplete(response) {
                return response.data;
            }

            function postFailed() {
                console.error('XHR Failed: ' + error.data);
            }
        }
    }
})();