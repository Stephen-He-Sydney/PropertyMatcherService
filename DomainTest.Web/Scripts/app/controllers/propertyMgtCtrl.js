(function () {
    'use strict';

    window.app.controller('propertyMgtCtrl', propertyMgtCtrl);
    propertyMgtCtrl.$inject = ['$scope', 'httpService', '$rootScope'];

    function propertyMgtCtrl($scope, httpService, $rootScope) {
        var vm = this;
        vm.model = vm.model || {};
        vm.init = function () {
            httpService.get(api_propertyApi_getAgencyProperty.replace('{agencyCode}', 'OTBRE'))
            .then(function (response) {
                vm.model = response.data;
            });
        }

        vm.chooseAgencyCode = function () {
            httpService.get(api_propertyApi_getAgencyProperty.replace('{agencyCode}', vm.SelectedAgencyCode))
           .then(function (response) {
               vm.model = response.data;
           });
        }

        vm.verifyProperty = function () {
            //console.log(vm.model);
            httpService.post(api_propertyApi_isPropertyMatched, vm.model)
           .then(function (response) {
               if (response.data) {
                   $rootScope.showModal("IsMatched");
               }
               else {
                   $rootScope.showModal("IsNotMatched");
               }
           });
        }
    }
})();