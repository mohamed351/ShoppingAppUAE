
        function CalculateShipping() {
            let $selector = $("input[type='radio']:checked")
            if ($selector.length !=0 && doICalculateShipping()) {
                console.log($selector);
                let totalWeight = calculateTotalWeight();
                const firstCostLimitInWeight = +$selector.attr("data-price").replace("٫", ".");
                const firstWeightCost = +$selector.attr("data-weight-limit").replace("٫", ".");
                const eachKg = +$selector.attr("data-weight-afterExisting").replace("٫", ".");
                const eachPrice = +$selector.attr("data-price-afterExisting").replace("٫", ".");
                let processExchnageRate = +$("#currency-app option:selected").attr("data-rate").replace("٫", ".")
                let shippingCost = 0;
                if (totalWeight > firstWeightCost) {
                    const remaingKgs = totalWeight - firstWeightCost;


                    shippingCost = (IncreseTheWeigthPrice(remaingKgs, eachKg, eachPrice, firstCostLimitInWeight) * processExchnageRate)
                    $("#shippingPirce").text(shippingCost.toFixed(2) + $("#currency-app").val());
                }
                else {
                    shippingCost = (firstCostLimitInWeight * processExchnageRate);
                    $("#shippingPirce").text((firstCostLimitInWeight * processExchnageRate).toFixed(2) + $("#currency-app").val());
                }
                return shippingCost;
            }
            return 0;

        }

        function calculateTotalWeight() {
            let currentArray = JSON.parse(localStorage.getItem("cart"));
            let totalWeight = 0;
            for (let item of currentArray.filter(a=> a.paiedShipping === true)) {
                totalWeight += (item.weight * item.Quantity);
            }
            return totalWeight;
        }

        function IncreseTheWeigthPrice(remaning, each, priceAfter, totalPrice) {
            let processExchnageRate = +$("#currency-app option:selected").attr("data-rate").replace("٫", ".")
            console.log("Remaning", remaning);
            console.log("EachKg", each);
            console.log("EachPrice", priceAfter);
            console.log("TotalPrice", totalPrice);
            return (((remaning / each) * priceAfter) + totalPrice * processExchnageRate);
        }

        function setShipping(shippingId) {
            localStorage.setItem("shippingId", shippingId);
        }
        function getShipping(shippingId) {
            return localStorage.getItem("shippingId")
        }


        $(document).ready(function () {



            if (!getShipping()) {
                $(".shipping-cart-item-radio[data-isdefault='True']").attr("checked", "checked");
            }
            else {

                if ($(".shipping-cart-item-radio[data-id='" + (+getShipping()) + "']").length == 0) {
                    $(".shipping-cart-item-radio[data-isdefault='True']").attr("checked", "checked");
                }
                else {
                    $(".shipping-cart-item-radio[data-id='" + (+getShipping()) + "']").attr("checked", "checked");
                }
              
            }
            CalculateShipping();

            $("#checkout_accordion_1").on("shown.bs.collapse", function (e) {

                if ($("#checkout_accordion_1 .show").attr("data-id") == 1) {
                    initialize();
                }
            });
            $(".shipping-cart-item-radio").on("change", function () {

                CalculateShipping();
            });
        });


function calculateTotal() {
    let currentArray = JSON.parse(localStorage.getItem("cart"));
    let sumSubTotals = 0;
    for (let item of currentArray) {
        sumSubTotals += (item.Quantity * item.price)
    }
    return sumSubTotals;
}