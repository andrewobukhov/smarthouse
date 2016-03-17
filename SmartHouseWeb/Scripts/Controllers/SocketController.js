(function () {
    'use strict';

    angular
        .module('app')
        .controller('SocketController', SocketController);

    SocketController.$inject = ['SocketFactory'];

    function SocketController(SocketFactory) {
        /* jshint validthis:true */
        var vm = this;
        vm.turnOn = turnOn;

        activate();

        function activate() {
            return SocketFactory.getStates().then(function (data) {
                vm.states = data;
            })
        }

        function turnOn(index)
        {
            vm.states[0].IsTurnOn = !vm.states[0].IsTurnOn
        }
    }
})();
