(function() {
    angular.module('app').controller('checkOutController', checkOutController);

    function checkOutController($cookies, $http) {

        var vm = this;
        var dataService = $http;
        vm.GetCities = getCities;
        vm.GetArea = getAreas;
        vm.cartItems = [];
        vm.Countries = [];
        vm.Cities = [];
        vm.Areas = [];
        vm.Country = { Id:'' ,CnCountryName:''};
        vm.City = { Id:'', CtCityName:'' };
        vm.Area = { Id:'', AAreaName:'' };
        vm.CheckData = {
            Orders: {
                CCountryId: '',
                CCityId: '',
                CAreaId: '',
                ZipCode: '',

                CAddress: '',
                DeliveryOption: '',
                PaymentType: '',
                Total: 0
            },
            OrderDetails: [
            ]

        };
      
        vm.CheckOutClick = checkOutClick;
        var orderDetail = {
            ProductId: 0,
            OrderQuantity: 0,
            LineTotal: 0
        };
        vm.DeliveryOptions = ['Fast', 'Regular'];
        vm.PaymentTypes = ['visa', 'Cash'];
        var cookieName = 'StoreCart';
        // hook up events 
        getCountries();

     
        function getCountries() {
            dataService.get("/api/User/GetCountry")
                 .then(function (result) {
                     vm.Countries = result.data;
                     vm.Cities = null;
                        vm.Areas = null;
                    },
                     function (error) {
                         handelException(error);
                     });
        }
        function getCities() {
            dataService.get("/api/User/GetCity/" + vm.Country.Id)
                .then(function(result) {
                    vm.Cities = result.data;
                    vm.Areas = null;
                    },
                    function(error) {
                        handelException(error);
                    });
        } function getAreas() {
            debugger;
            dataService.get("/api/User/GetAria/" + vm.City.Id)
                .then(function(result) {
                        vm.Areas = result.data;
                    },
                    function(error) {
                        handelException(error);
                    });
        }

        function checkOut() {
            debugger;
            dataService.post("/api/Orders/checkOut/", vm.CheckData)
                .then(function (result) {
                    $cookies.remove(cookieName);
                    window.location = "#/Thanks";

                    },
                    function (error) {
                        handelException(error);
                    });
        }
        function checkOutClick() {
            getCartItems();
            getOrderDetails();
           
        }

        function getOrder() {
            vm.CheckData.Orders.CCountryId = vm.Country.Id;
            vm.CheckData.Orders.CAreaId = vm.Area.Id;
            vm.CheckData.Orders.CCityId = vm.City.Id;
         
             checkOut();

        }

        function getOrderDetails() {
            var total = 0;
            for (var i = 0; i < vm.cartItems.length; i++) {
                orderDetail.ProductId = vm.cartItems[i].ProductId;
                orderDetail.OrderQuantity = vm.cartItems[i].Quntity;
                orderDetail.LineTotal = vm.cartItems[i].Quntity * vm.cartItems[i].UnitPrice;
                    total = orderDetail.LineTotal + total;
                    vm.CheckData.OrderDetails.push(orderDetail);
            }
          
            vm.CheckData.Orders.Total = total;
            getOrder();
        }
        function getCartItems() {
     
            vm.cartItems = $cookies.getObject(cookieName) || [];
        }
        function handelException(error) {
            alert(error.data.ExptionMessage);
        }
    }
})();