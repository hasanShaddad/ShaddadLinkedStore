function loadWoodgate() {

    var totalItems = 0;
    for (var i = 0; i < window.localStorage.length; i++) {


        var checkProdid = window.localStorage.key(i);


        //get product id from localStorage
        if ((checkProdid.indexOf("details0122523430") > 0)) {

           
            totalItems = totalItems + 1;


        }
    }
    document.getElementById("totalItems").innerHTML = totalItems;

} ;
 

window.addEventListener("load", loadWoodgate, false);