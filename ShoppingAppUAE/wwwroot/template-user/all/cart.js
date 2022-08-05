

function buildCart(currentCart) {
    let cartItems = JSON.parse(currentCart);
    $("#totalCount").text(cartItems.length);
    $("#totalCount-mobile").text(cartItems.length);
    $(".totalCount")
    console.log(currentCart);
    let cartItemsHtml = cartItems.map(function (data) {
      
        return `<div class="mini-cart-item clearfix">
                               <div class="mini-cart-img">
                                   <a href="#"><img src="/images/${data.imageURL}" alt="${data.productName}"></a>
                                                <span class="mini-cart-item-delete" data-id="${data.productId}"><i class="icon-trash"></i></span>
                                                           </div>
                                                               <div class="mini-cart-info ">
                                                                        <h6><a href="#">${data.productName}</a>   <span class='text-danger'> ${data.currentQuantity < 0 ? "Out Of Stock" : (data.currentQuantity < data.Quantity ? "Quantity is not Enougth" :"" ) } </span></h6>
                                                                          
                                                                         <span class="mini-cart-quantity" style='display:flex;'> <span class='product-qtu-data'>  ${data.Quantity} </span> <span> x </span> <span class="reloader product-price-data" data-id="${data.productId}" > <span> </span>
                                                                </div>
                                                          </div>`;
    });

    $("#cartInfo").html("");
    $("#cartInfo").html(cartItemsHtml);
    return cartItems;
}
$(document).ready(function () {
    let currentCulture = $("#language-app-hidden").val();
    $("#language-app").val(currentCulture);
    $("#language-app").niceSelect("update");

    if (getCookie("currency")) {
        $("#currency-app").val(getCookie("currency"));
        $("#currency-app").niceSelect("update");
    }

    getPrices();
});
$(document).on("click", ".mini-cart-item-delete", function () {

    let currentDeletedItem = +$(this).attr("data-id");
    let jsonData = localStorage.getItem("cart");
    if (jsonData) {
        let oldCart = JSON.parse(jsonData);
        let newCart = oldCart.filter(a => a.productId != currentDeletedItem);
        localStorage.setItem("cart", JSON.stringify(newCart));
        buildCart(localStorage.getItem("cart"));
        $("#totalCartSum").text(buildTotal());
        getPrices();

      
    }
});

function buildTotal() {
    let sumTotal = 0;
    $("#cartInfo .mini-cart-item").each(function () {
        sumTotal += +$(this).find(".product-qtu-data").text() * +$(this).find(".product-price-data").text();
    });
    return sumTotal;
}

function getPrices() {
    let currentCart = localStorage.getItem("cart");
    if (currentCart) {
        let cartItems = buildCart(currentCart);

        $.ajax({
            url: "/Product/GetProductPrices",
            method: "POST",
            contentType: "application/json",
            type: "json",
            data: JSON.stringify({ producIds: cartItems.map(a => a.productId) }),
            success: function (result) {
                 
                let myCart = JSON.parse(localStorage.getItem("cart"));
             
                let processExchnageRate = +$("#currency-app option:selected").attr("data-rate").replace("٫", ".")
                for (let item of result) {
                    
                  
                 
                    myCart.find(a => a.productId == item.productID).price = item.purchasingPrice;
                    myCart.find(a => a.productId == item.productID).weight = item.weight;
                    myCart.find(a => a.productId == item.productID).currentQuantity = item.quantity;
                    myCart.find(a => a.productId == item.productID).imageURL = item.imageURL;
                    myCart.find(a => a.productId == item.productID).paiedShipping = item.paidShipping;
                }
                localStorage.setItem("cart", JSON.stringify(myCart));
            
                buildCart(localStorage.getItem("cart"));
              
                if (!checkOutOfStock()) {
                    $("#checkout-button-validation").attr("href", "checkout");
                    $("#checkout-button-validation").removeAttr("href");
                }

                for (let item of result) {
                    $(`#cartInfo .product-price-data[data-id='${item.productID}']`).text(item.purchasingPrice * processExchnageRate);
                }
                $("#totalCartSum").text(buildTotal() + $("#currency-app").val());
              
               

            }
        });
    }

}

$(document).on("click", ".product-cart-item", function () {
    let productId = $(this).attr("data-id");
    $.ajax({
        url: "/Product/AddToCardModel",
        data: { id: productId },
        success: function (result) {
            $("#Card-Product").html(result);
            $("#add_to_cart_modal").modal("show");

        }
    })
});
$(document).on("click", ".add-cart-local", function () {

    const productData = {
        productId: $(this).attr("data-id"),
        imageURL: $(this).attr("data-image"),
        productName: $(this).attr("data-name")
    };

    let currentCart = localStorage.getItem("cart");
    if (currentCart == null) {
        localStorage.setItem("cart", "[]");
    }
    currentCart = JSON.parse(localStorage.getItem("cart"));
    let proceessCart = currentCart.find(a => a.productId == productData.productId);
    if (proceessCart) {
        proceessCart.Quantity++;
    }
    else {
        currentCart.push({ ...productData, Quantity: 1 });
    }

    localStorage.setItem("cart", JSON.stringify(currentCart));

    buildCart(localStorage.getItem("cart"));
    alertify.success("Item Add to Cart Successfully", 300);
    getPrices();
});


//language app

$("#language-app").on("change", function () {
    $(this).parents("form").submit();
});

$("#currency-app").on("change", function () {
    $(this).parents("form").submit();
});

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function checkOutOfStock() {
    currentValue = true;
    let currentProducts = localStorage.getItem("cart");
    currentProducts = JSON.parse(currentProducts);
    //return currentProducts.find(a => a.currentQuantity === 0);
    for (var item of currentProducts) {
        console.log(item);
        if (item.Quantity > item.currentQuantity) {
            return false;
        }
    }
    return currentValue;
   

}


function getPricesCart(callBackFunction) {
    let currentCart = localStorage.getItem("cart");
    if (currentCart) {
        let cartItems = buildCart(currentCart);

        $.ajax({
            url: "/Product/GetProductPrices",
            method: "POST",
            contentType: "application/json",
            type: "json",
            data: JSON.stringify({ producIds: cartItems.map(a => a.productId) }),
            success: function (result) {

                let myCart = JSON.parse(localStorage.getItem("cart"));

                let processExchnageRate = +$("#currency-app option:selected").attr("data-rate").replace("٫", ".")
                for (let item of result) {



                    myCart.find(a => a.productId == item.productID).price = item.purchasingPrice;
                    myCart.find(a => a.productId == item.productID).weight = item.weight;
                    myCart.find(a => a.productId == item.productID).currentQuantity = item.quantity;
                    myCart.find(a => a.productId == item.productID).imageURL = item.imageURL;
                }
                localStorage.setItem("cart", JSON.stringify(myCart));

                buildCart(localStorage.getItem("cart"));

                if (!checkOutOfStock()) {
                    $("#checkout-button-validation").attr("href", "checkout");
                    $("#checkout-button-validation").removeAttr("href");
                }

                for (let item of result) {
                    $(`#cartInfo .product-price-data[data-id='${item.productID}']`).text(item.purchasingPrice * processExchnageRate);
                }
                $("#totalCartSum").text(buildTotal() + $("#currency-app").val());
                callBackFunction(checkOutOfStock());



            }
        });
    }

}