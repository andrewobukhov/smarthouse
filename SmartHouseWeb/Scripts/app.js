(function () {
    "use strict";

    var app = angular.module("app", ["chart.js", "ui.bootstrap"]);

    app.controller('tempController', function ($scope, $interval, tempFactory) {
        //getTemps();
        getLastTemp();

        //$interval(getLastTemp, 10000);

        function getLastTemp() {
            tempFactory.getLastTemp().success(function(temp) {
                if (temp != null) {
                    temp.Date = new Date(temp.Date);
                    $scope.lastTemp = temp;
                }
            }).error(function(error) {
                console.log(error);
            });
        }

        function getTemps() {
            tempFactory.getTemps().success(function (temps) {

                var labels = [];
                $scope.series = ["Temp"];

                var data = [];

                for (var i = 0; i < temps.length; i++) {

                    temps[i].Date = new Date(temps[i].Date);
                    labels.push(temps[i].Date.toLocaleTimeString());
                    data.push(temps[i].Temp);
                }

                $scope.temps = temps;
                $scope.labels = labels;
                $scope.data = [data];

                $scope.options = {
                    scaleOverride: true,
                    scaleSteps: 14,
                    scaleStepWidth: 2,
                    scaleStartValue: 0
                };

            }).error(function (error) {
                console.log(error.message);
            });
        };
    });

    app.factory("tempFactory", ["$http", function ($http) {
        var service = {};
        service.getTemps = function () {
            return $http.get("/api/sensor/get");
        }

        service.getLastTemp = function() {
            return $http.get("/api/sensor/1");
        }
        return service;
    }]);

})();