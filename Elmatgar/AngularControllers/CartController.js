(function() {
    angular.module('app').controller('CartController', CartController);
    function CartController($cookies, $http) {
        var vm = this;
        var dataService = $http;

        vm.cartItems = [];
        vm.cartItem = { StockQuntity: null, ItemId: null, ProductId: null, Quntity: 1, ImageUrl: '', UnitPrice: 0 };
        vm.product = {};
        vm.cartId = null;
        vm.Poducts = [];
        var cookieName = 'StoreCart';
        var pageMode = {
            LIST: 'List',
            CART: 'Cart'

        }
        vm.uiState = {
            isValid: true,
            isCartAreaVisible: false,
            isAddItemToCartAreaVisible: false,
            isListAreaVisible: true,
            IsItemAddedToCart: false,
            IsUpdateQuantityVisable: false,
            Mode: pageMode.LIST,
            messages: []
        }
        // hook up events 
        vm.UpdateQuantityClick = updateQuantityClick;
        vm.GoToCart = gotoCartClick;
        vm.BackClick = backClick;
        vm.AddToCartClick = addToCartClick;
        vm.RemoveCartItemClick = removeCartItemClick;
        vm.ChangeQuantityClick = changeQuantityClick;
        vm.ManageCart = manageCart;
        //vm.Go = go;

        //  getCookie();
        productList();
        loadCart();

        // creating functions 

        function productList() {
            dataService.get("/api/products")
                   .then(function (result) {
                       vm.Poducts = result.data;

                   },
                       function (error) {
                           handelException(error);
                       });
            setUiState(pageMode.LIST);
        }

        function manageCart(productId) {
            debugger;
            var results = vm.cartItems.filter(function (entry) { return entry.ProductId === productId; });
            if (results.length > 0) {
                vm.uiState.IsItemAddedToCart = true;
            } else {
                vm.uiState.IsItemAddedToCart = false;
            }
        }
        function backClick() {
            setUiState(pageMode.LIST);
        }
        function removeCookie() {
            $cookies.remove(cookieName);
        }

        function updateQuantity(itemId, quantity) {
            var index = vm.cartItems.map(function (p) {
                return p.ItemId;

            }).indexOf(itemId);
            vm.cartItems[index].Quntity = quantity;
            saveInCookie();
            loadCart();
            vm.uiState.IsUpdateQuantityVisable = false;
        }


        function addToCart() {

            setCartItemData();

            vm.cartItems = $cookies.getObject(cookieName) || [];

            vm.cartItems.push(vm.cartItem);
            saveInCookie();


        }

        function saveInCookie() {
            var currentdate = new Date();
            currentdate.setMonth(currentdate.getMonth() + 1);
            var expireDate = currentdate;
            $cookies.putObject(cookieName, vm.cartItems, { 'expires': expireDate });

        }
        function loadCart() {

            vm.cartItems = $cookies.getObject(cookieName) || [];

        }
        // need to be handeled 
        function removeCartItem(itemId) {
            debugger;
            var index = vm.cartItems.map(function (p) {
                return p.ItemId;

            }).indexOf(itemId);
            vm.cartItems.splice(index, 1);
            if (vm.cartItems.length > 0) {
                saveInCookie();
            } else {
                removeCookie();
            }

        }

        //function go(path) {

        //    $location.path(path);
        //}
        function setCartItemData() {

            vm.cartItem.StockQuntity = vm.product.StockQuntity;
            vm.cartItem.ImageUrl = vm.product.ImageUrl;
            vm.cartItem.ProductId = vm.product.ProductId;
            vm.cartItem.UnitPrice = vm.product.Price;
            vm.cartItem.Quntity = 1;
            vm.cartItem.ItemId = new Date().getTime();

        }

        function changeQuantityClick() {
            vm.uiState.IsUpdateQuantityVisable = true;
        }
        function removeCartItemClick(itemId) {
            removeCartItem(itemId);
        }
        function addToCartClick(product) {
            debugger;
            vm.product = product;
            if (vm.cartItems.length > 0) {
                var index = vm.cartItems.map(function (p) {
                    return p.ProductId;

                })
                    .indexOf(vm.product.ProductId);
                // the item is already exisit
                if (index >= 0) {
                    updateQuantity(vm.cartItems[index].ItemId, vm.cartItems[index].Quntity + 1);


                }
                else {
                    addToCart();
                }
            }
                // the item is new 
            else {
                addToCart();
            }

        }

        function updateQuantityClick(cartItem) {
            vm.cartItem = cartItem;
            updateQuantity(vm.cartItem.ItemId, vm.cartItem.Quntity);
        }
        function gotoCartClick() {
            loadCart();
            setUiState(pageMode.CART);
        }

        function setUiState(state) {

            vm.uiState.Mode = state;

            if (state == pageMode.LIST) {

                vm.uiState.isCartAreaVisible = false;
                vm.uiState.isAddItemToCartAreaVisible = false;
                vm.uiState.isListAreaVisible = true;
            }
            if (state == pageMode.CART) {

                vm.uiState.isCartAreaVisible = true;
                vm.uiState.isAddItemToCartAreaVisible = true;
                vm.uiState.isListAreaVisible = false;
            }

        }
        function handelException(error) {
            alert(error.data.ExptionMessage);
        }

        function checkOut() {
            
        }
    }

})();