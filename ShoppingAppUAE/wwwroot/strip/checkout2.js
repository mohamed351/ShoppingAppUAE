const stripe = Stripe($("#publicKeyPayment").val());
// get from localstorage
$("#currency-app").val(getCookie("currency"));
$("#currency-app").niceSelect("update");
const currentCart = localStorage.getItem("cart");
const items = [];

if (checkOutOfStock()) {
    $("#quantity-validation").html("<div style='height:20vh'><h2 class='text-danger mt-2 text-center'> Please Check your cart some items is out of stock </h2></div>")
}


 currencyCode = $("#currency-app").val().toLowerCase()
if (currentCart) {
    let cartArray = JSON.parse(currentCart);
    for (let item of cartArray) {

        items.push({ id: +item.productId, quantity: item.Quantity, price: item.price, productName:item.productName})
    }


}
drawCart(items);

document
    .querySelector("#payment-form")
    .addEventListener("submit", handleSubmit);



var elementStyles = {
    base: {
        color: '#32325D',
        fontWeight: 500,
        fontFamily: 'Source Code Pro, Consolas, Menlo, monospace',
        fontSize: '16px',
        fontSmoothing: 'antialiased',

        '::placeholder': {
            color: '#CFD7DF',
        },
        ':-webkit-autofill': {
            color: '#e39f48',
        },
    },
    invalid: {
        color: '#E25950',

        '::placeholder': {
            color: '#FFCCA5',
        },
    },
};


async function initialize() {
    const response = await fetch("/Checkout/Create", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ items, currencyCode: currencyCode, shippingFees: (+CalculateShipping().toFixed(2))}),
    });
    const { clientSecret } = await response.json();

    const appearance = {
        theme: 'stripe',
    };
    elements = stripe.elements({ appearance, clientSecret });

    const paymentElement = elements.create("payment");
    paymentElement.mount("#payment-element");
}


async function handleSubmit(e) {
    e.preventDefault();
    $(".loading-bar ").css("display", "flex");
    setLoading(true);
    document.querySelector("#submit").hidden = true;

    let userData = {
        firstName: $("#ApplicationUser_FirstName").val(),
        lastName: $("#ApplicationUser_LastName").val(),
        address: $("#ApplicationUser_Address").val(),
        city: $("#ApplicationUser_City").val(),
        orderNote: $("#Note").val(),
        currenyCode: currencyCode,
        shipmentID: +$("[name='shippingPirce']:checked").attr("data-id"),
        orderType: +$("#checkout_accordion_1 .show").attr("data-id"),
        PhonePrefix: $("#ApplicationUser.PhonePrefix").val(),
        email: $("#ApplicationUser_Email").val(),
        phoneNumber: $("#ApplicationUser_PhoneNumber").val(),
        shipmentFees: CalculateShipping(),
        paymentCarts: items.map(a => {
            return {
                productId: a.id,
                productName: a.productName,
                price: a.price,
                quantity: a.quantity
            }
        })

    };
    if (userData.orderType == 1) {
        setLoading(true);
        $.ajax({
            url: "/Checkout/SubmitCheckout",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(userData),
            type: "json",
            success: function (result) {
                $(".loading-bar ").css("display", "flex");
                let paymentData =  stripe.confirmPayment({
                    elements,
                    confirmParams: {
                        // Make sure to change this to your payment completion page
                        return_url: "https://localhost:44339/Checkout/PaymentCompleted?type=card&id="+result.id,
                    },
                }).then(a => {
                    document.querySelector("#submit").hidden = false;
                    if (a.error.type === "card_error" || a.paymentData.type === "validation_error") {
                        showMessage(error.message);
                    } else {
                        showMessage("An unexpected error occured.");
                    }
                    setLoading(false);
                    $(".loading-bar ").css("display", "none");
                })
               
                console.log(paymentData);
            }
        })

      
    

    }
    else if (userData.orderType == 0) {

        $.ajax({
            url: "/Checkout/SubmitCheckout",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(userData),
            type: "json",
            success: function (result) {
                
                window.location.href = window.location.origin + "/Checkout/PaymentCompleted?ID="+result.id;
                console.log(result);
            }
        })
    }

 
   

 
}

// Fetches the payment intent status after payment submission
async function checkStatus() {
    const clientSecret = new URLSearchParams(window.location.search).get(
        "payment_intent_client_secret"
    );

    if (!clientSecret) {
        return;
    }

    const { paymentIntent } = await stripe.retrievePaymentIntent(clientSecret);

    switch (paymentIntent.status) {
        case "succeeded":
            showMessage("Payment succeeded!");
            break;
        case "processing":
            showMessage("Your payment is processing.");
            break;
        case "requires_payment_method":
            showMessage("Your payment was not successful, please try again.");
            break;
        default:
            showMessage("Something went wrong.");
            break;
    }
}

// ------- UI helpers -------

function showMessage(messageText) {
    const messageContainer = document.querySelector("#payment-message");

    messageContainer.classList.remove("hidden");
    messageContainer.textContent = messageText;

    setTimeout(function () {
        messageContainer.classList.add("hidden");
        messageText.textContent = "";
    }, 4000);
}

// Show a spinner on payment submission
function setLoading(isLoading) {
    if (isLoading) {
        // Disable the button and show a spinner
        document.querySelector("#submit").disabled = true;
        $(".loading-bar ").css("display", "flex");

        //document.querySelector("#spinner").classList.remove("hidden");
        //document.querySelector("#button-text").classList.add("hidden");
    } else {
        document.querySelector("#submit").disabled = false;
        $(".loading-bar ").css("display", "none");
        //document.querySelector("#spinner").classList.add("hidden");
        //document.querySelector("#button-text").classList.remove("hidden");
    }
}

function drawCart(items) {
    let processExchnageRate = +$("#currency-app option:selected").attr("data-rate").replace("٫", ".")
    console.log(processExchnageRate);
    $("#payment-details").html("");
    totalPrice = 0;
    for (let item of items) {
        $("#payment-details").append(`
        <tr>
        <td> ${item.productName} <strong>x ${item.quantity} </strong> </td>
        <td> ${((item.price * item.quantity) * processExchnageRate)} ${$("#currency-app").val()} </td>
        </tr>
        `)
        totalPrice += item.price * item.quantity

    }
    $("#payment-details").append("<tr> <td><strong>Order Total</strong> </td> <td> " + (totalPrice * processExchnageRate) + " " + $("#currency-app").val()+"  </td> </tr>")
   
}