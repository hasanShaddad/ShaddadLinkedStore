 

function AddToCart(prodId, imgUrl, orPrice, productName, drqty, finalprice,inventoryStock) {
 
     
        var checkinventoryStock;
        if (drqty === null) {
            drqty = 0;

        }
    doFirst();

//add script savecrap to the add button when page load
    function doFirst() {
       
        checkinventoryStock = inventoryStock;
        var button = document.getElementById("addtocart");
       saveCrap();
        //button.addEventListener("click", saveCrap, false);
    };






//save details in localStorage
         function saveCrap() {
            var qty;
            var lqty = parseInt(localStorage.getItem(prodId));
            if (isNaN(lqty)) {
                lqty = 0;
            }
            //if the item in stock
            if (checkinventoryStock > 0) {
                if (lqty === inventoryStock) {

                    qty = lqty;
                    this.value = "stock is null";
                } else {
                    checkinventoryStock = checkinventoryStock - 1;

                    qty = 1 + lqty;
                }
            } else {
                qty = lqty;
            }
            if (qty > 0) {
                //update woodgate
                if (qty === 1) {
                    var totalitems = 0;

                    totalitems = document.getElementById("totalItems").textContent;

                    document.getElementById("totalItems").innerHTML = parseInt(totalitems) + 1;
                }

                localStorage.setItem(prodId, qty);
                var details = [];
                details[0] = orPrice;
                details[1] = imgUrl;
                details[2] = drqty;
                details[3] = finalprice;
                details[4] = inventoryStock;
                details[5] = productName;
                details[6] = prodId;


                localStorage.setItem(prodId + "details0122523430", JSON.stringify(details));

                sessionStorage.setItem("cartExist", 0);

            } else {
                this.value = "stock is null";
            }

            loadInercart();

        };


        function loadInercart() {

            var qtyt = localStorage.getItem(prodId);
            document.getElementById('cartid').innerHTML = document.getElementById('ProductId').value;
            document.getElementById("cartqty").innerHTML = qtyt;

        };


    } ;