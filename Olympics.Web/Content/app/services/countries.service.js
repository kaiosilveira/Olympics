(function () {

    'use strict';

    angular
        .module('app')
        .factory('countriesService', countriesService);

    countriesService.$inject = ['$q', '$http'];

    function countriesService($q, $http) {

        var svc = {};

        svc.get = get;
        svc.list = list;
        svc.save = save;
        svc.update = update;
        svc.delete = del;

        return svc;

        function list() {

            var deferred = $q.defer();
            var promise = deferred.promise;

            $http.get('/api/Countries').then(
                function (result) {
                    console.log(result.data);
                    deferred.resolve(result.data);
                },
                function (err) {
                    console.log(err);
                    deferred.reject(err);
                }
            );

            return promise;
        }

        function get(id) {
            var deferred = $q.defer();
            var promise = deferred.promise;

            $http.get('/api/Countries/' + id).then(
                function (result) {
                    deferred.resolve(result.data);
                },
                function (err) {
                    deferred.reject(err);
                }
            );

            return promise;
        }

        function save(country) {
            var deferred = $q.defer();
            var promise = deferred.promise;
            
            $http.post('/api/Countries/', country).then(
                function (result) {
                    deferred.resolve(result.data);
                },
                function (err) {
                    deferred.reject(err);
                }
            );

            return promise;
        }

        function update(country) {
            var deferred = $q.defer();
            var promise = deferred.promise;

            $http.put('/api/Countries/' + country.Id, country).then(
                function (result) {
                    deferred.resolve(result.data);
                },
                function (err) {
                    deferred.reject(err);
                }
            );

            return promise;
        }

        function del(id) {
            var deferred = $q.defer();
            var promise = deferred.promise;

            $http.delete('/api/Countries/' + id).then(
                function (result) {
                    deferred.resolve(result.data);
                },
                function (err) {
                    deferred.reject(err);
                }
            );

            return promise;
        }

    }

})();