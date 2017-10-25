(function () {

    var app = angular.module("app", ["ngRoute", "ngCookies"]);

    app.config(function ($routeProvider, $locationProvider) {
        $locationProvider.hashPrefix('');
        $routeProvider
            .when("/main", {
                templateUrl: "Templates/Products.html",
                controller: "CartController",
                controllerAs: 'Mainvm'
            })     .when("/Cart", {
                templateUrl: "Templates/Cart.html",
                controller: "CartController",
                controllerAs: 'Cartvm'
            })    .when("/CheckOut", {
                templateUrl: "Templates/CheckOut.html",
                controller: "checkOutController",
                controllerAs: 'Checkvm'
            }).when("/Thanks", {
                templateUrl: "Templates/Thanks.html"
            
            })      .otherwise({ redirectTo: "/main" });
        
     
    });

}());