
 function loadcart() {
    var qtyt = 0;
    var price = 0;
    var item = new Array();
    var counterDetails = 0;
    var counterlabel = 0;
    var counterprice = 0;
    var s = '<div class="form-group">';
    var labelProductName = "";
    var inventoryString = "";
    var imgString = "";
    var labelString = "";
    var priceString = "";
    var prodid = null;
    var details = [];
    var imgid = null;
    var oPrice = null;
    var cartExist = sessionStorage.getItem("cartExist");
    var CartLang = localStorage.getItem("CartLang");
    var inventoryStock = 0;
    var productName = "";
    var drqty = 0;
    var imgUrl = "";
    var finalprice = 0;
    for (var i = 0; i < window.localStorage.length; i++) {


        var checkProdid = window.localStorage.key(i);


        //get product id from localStorage
        if (checkProdid.indexOf("details0122523430") < 0) {

            prodid = checkProdid;


        } else if ((checkProdid.indexOf("details0122523430") > 0)) {

            details = JSON.parse(localStorage.getItem(checkProdid));

            inventoryStock = 0;
            productName = "";
            price = details[0];
            finalprice = details[3];
            inventoryStock = details[4];
            productName = details[5];
            prodid = details[6];
            drqty = details[2];
            imgUrl = details[1];
            qtyt = localStorage.getItem(prodid);

            //get qty from localStorage
            if (qtyt >= drqty) {
                price = finalprice;
            }

        }


        //create the toolboxes and labels
        if (checkProdid.indexOf("details0122523430") > -1) {

            counterDetails = 1;
            imgString = '<img src="' + imgUrl + '" style="width:80px"/>';
            if (CartLang === "Ar") {
                priceString = ' <label>للقطعةٍ</label>' +
                    ' <label id="price' +
                    prodid +
                    '"> ' +
                    price * qtyt +
                    '</label>' +
                    ' <label>بسعر</label>';
                inventoryString += 'يوجد عدد:' + inventoryStock + '  قطعة ';
                inventoryString += "انت اشتريت";
            } else {
                priceString = ' <label>price:</label>' +
                    ' <label id="price' +
                    prodid +
                    '"> ' +
                    price * qtyt +
                    '</label>';
                inventoryString += 'there is :' + inventoryStock + '  items ';
            }

            var qtystring = "";
            var stock = parseInt(inventoryStock) + 1;
            //quantity list = inventory stock quantity
            for (var q = 1; q < stock; q++) {
                if (q === qtyt) {
                    qtystring += '<option value="' + q + ' " selected>' + q + '</option>';
                } else {
                    qtystring += '<option value="' + q + ' ">' + q + '</option>';
                }
            }

            priceString += '<select class="CartControlOption"  id="changeSelect' +
                prodid +
                '" onchange="changefunction(this.id,' +
                prodid +
                ')">' +
                qtystring +
                ' </select>';
            //product name
            labelProductName = '<h4>' + productName + '</h4>';
            if (CartLang === "Ar") {
                counterlabel = 1;
                labelString += '<button type="button" class="btn btn-warning" onclick="remove(' +
                    prodid +
                    ')" id ="remove' +
                    prodid +
                    '">x</button>';


            } else {
                counterlabel = 1;
                labelString += '<button type="button" class="btn btn-warning" onclick="remove(' +
                    prodid +
                    ')" id ="remove' +
                    prodid +
                    '">x</button>';
                labelString += '<br\>';
            }


        }

        if (counterDetails > 0 && counterlabel > 0) {
            counterDetails = 0;
            counterlabel = 0;
            oPrice += price * qtyt;
            if (CartLang === "Ar") {
                s += '<div class="row">' +
                    '<div class="col-sm-4 " align="center">' +
                    labelProductName +
                    '</div></div>' +
                    '<div class="row">' +
                    '<div class="col-sm-8 nopadding">' +
                    labelString +
                    priceString +
                    ' ' +
                    inventoryString +
                    imgString +
                    '<div/><div/>' +
                    '<br\>';
                s += '</div>';
            } else {
                s += imgString + "  " + inventoryString + priceString + "  " + labelString;
                s += '</div>';
            }
            labelProductName = "";
            imgString = "";
            labelString = "";
            priceString = "";
            inventoryString = "";
        }

    }
    s += '<br/>' + '                    ' + '<h3>total price is : ' + oPrice + '</h3>';
    if (cartExist < 1) {

      window.document.getElementById("screen").innerHTML = s;

    }

};

function changefunction(changedId, prodid) {
  
    var qty;
    qty = document.getElementById(changedId).value;


    localStorage.setItem(prodid, qty);
    window.loadcart();
};

 
function remove(prodid) {
    alert("you will remove product no : "+prodid);
    localStorage.removeItem(prodid);
    localStorage.removeItem(prodid + "details0122523430");
    //update woodgate
    var totalitems = 0;
    totalitems = document.getElementById("totalItems").textContent;
    document.getElementById("totalItems").innerHTML = parseInt(totalitems) - 1;
   
    window.loadcart();
    };
window.addEventListener("load", loadcart, false);

