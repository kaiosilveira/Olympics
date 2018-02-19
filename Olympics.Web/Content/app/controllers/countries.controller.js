(function () {

    'use strict';

    angular
        .module('app')
        .controller('countryController', countryController);

    countryController.$inject = ['$location', 'countriesService'];

    function countryController($location, countriesService) {

        var vm = this;
        vm.countries = [];

        vm.add = add;
        vm.edit = edit;
        vm.remove = remove; 
        
        _activate();

        function edit(id) {
            $location.path('/form/' + id);
        }

        function add() {
            $location.path('/form');
        }

        function remove(id) {

            var success = () => {
                console.log('País excluído');
                vm.countries = vm.countries.filter(c => c.Id != id);
            };

            var error = err => console.log(err);

            countriesService.delete(id)
            .then(success, error);

        }

        function _activate() {
            countriesService
            .list()
            .then(
                function (data) {
                    vm.countries = data;
                },
                function (err) {
                    console.log(err);
                }
            );
        }
    }

})();