(function() {
    'use srtict';
    angular.module('app').controller('SideConfigMenuController', sideConfigMenuController);

    function sideConfigMenuController($http) {
        var vm = this;
        var dataService = $http;
        vm.categ = [];
        
    };

    categlist();
    function categlist() {
        dataService.get("/api/CategSideMenuApi")
            .then(function(result) {
                vm.categ = result.data;
                    debugger;
                },
                function(error) {
                    handelException(error);
                });
    }
    function handelException(error) {
        alert(error.data.ExptionMessage);
    }

})();