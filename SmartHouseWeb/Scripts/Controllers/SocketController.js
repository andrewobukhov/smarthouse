(function () {
    "use strict";

    angular
        .module('app')
        .controller('SocketController', SocketController);

    SocketController.$inject = ['SocketFactory', "$interval", "_"];

    function SocketController(SocketFactory, $interval,  _) {
        /* jshint validthis:true */
        var vm = this;
        vm.changeState = changeState;

        activate();

        function activate() {
            return SocketFactory.getStates().then(function(data) {
                vm.states = data;
                updateStatus();
                $interval(updateStatus, 1000);
            });
        }

        function changeState(index) {
            SocketFactory.changeState(index).then(function (data) {
                _.extend(_.findWhere(vm.states, { Index: data.Index }), data);
                updateStatus();
            });
        }

        function updateStatus() {
            _.each(vm.states, function (x) {
                var s = x.IsTurnOn ? "Включен" : "Выключен";
                x.status = s.concat(" в течение: ", SocketFactory.getInterval(x.Date));
            });
        }
    }
})();
