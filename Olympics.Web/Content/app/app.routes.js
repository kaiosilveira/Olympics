(function () {

    'use strict';

    angular
        .module('app')
        .config(routesConfig);

    routesConfig.$inject = ['$routeProvider'];

    function routesConfig($routeProvider) {

        $routeProvider
        .when('/', {
            templateUrl: '/Content/app/views/home.html',
            controller: 'countryController',
            controllerAs: 'countryCtrl'
        })
        .when('/form', {
            templateUrl: '/Content/app/views/add.html',
            controller: 'addCountryController',
            controllerAs: 'addCountryCtrl'
        })
        .when('/form/:id', {
            templateUrl: '/Content/app/views/add.html',
            controller: 'addCountryController',
            controllerAs: 'addCountryCtrl'
        })
        .otherwise({
            templateUrl: '/Content/app/views/404.html'
        });

    }

})();