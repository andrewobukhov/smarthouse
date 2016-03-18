(function () {
    'use strict';

    angular
        .module('app')
        .factory('SocketFactory', SocketFactory);

    SocketFactory.$inject = ['$http'];

    function SocketFactory($http) {
        var service = {
            getStates: getStates,
            changeState: changeState,
            getInterval: getInterval
        };

        return service;

        function getInterval(date) {
            var msDiff = Date.now() - date;
            var secDiff = Math.floor(msDiff / 1000);
            var minDiff = Math.floor(msDiff / 60000);
            var hDiff = Math.floor(msDiff / 3600000);

            var result = "";

            hDiff && (result = result.concat(hDiff + " ч. "));
            minDiff && (result = result.concat((minDiff % 60) + " м. "));
            result = result.concat((secDiff % 60) + " с. ");

            return result;
        }

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