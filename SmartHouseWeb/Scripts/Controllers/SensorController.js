(function () {
    'use strict';

    angular
        .module('app')
        .controller('SensorController', SensorController);

    SensorController.$inject = ['$location']; 

    function SensorController($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SensorController';

        activate();

        function activate() { }
    }
})();
