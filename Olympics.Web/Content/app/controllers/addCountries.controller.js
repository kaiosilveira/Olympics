(function () {

    'use strict';

    angular
        .module('app')
        .controller('addCountryController', addCountryController);

    addCountryController.$inject = ['$location', '$routeParams', 'countriesService'];

    function addCountryController($location, $routeParams, countriesService) {

        var vm = this;

        vm.cancel = cancel;
        vm.save = save;
        vm.country = {};

        _activate();

        function cancel() {
            $location.path('/');
        }

        function save() {

            var success = function() {
                console.log('Informações salvas');
                $location.path('/');
            };

            var error = err => console.log(err);

            (vm.country.Id ? countriesService.update(vm.country) : countriesService.save(vm.country))
            .then(success, error);
        }

        function _activate() {

            if ($routeParams.id) {
                countriesService.get($routeParams.id).then(
                function (data) {
                    vm.country = data;
                },
                function (err) {
                    console.log(err);
                });
            }

        }

    }

})();