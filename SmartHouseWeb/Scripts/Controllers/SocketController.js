(function () {
    "use strict";

    angular
        .module('app')
        .controller('SocketController', SocketController);

    SocketController.$inject = ['SocketFactory', "_"];

    function SocketController(SocketFactory, _) {
        /* jshint validthis:true */
        var vm = this;
        vm.changeState = changeState;

        activate();

        function activate() {
            return SocketFactory.getStates().then(function(data) {
                vm.states = data;
            });
        }

        function changeState(index) {
            SocketFactory.changeState(index).then(function (data) {
                var state = _.find(vm.states, function (s) { return s.Index === index; });
                state.IsTurnOn = data.IsTurnOn;
                state.StateName = data.StateName;
            });
        }
    }
})();
