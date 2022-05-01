const stripe = Stripe("pk_test_51KidVZLdvjUTqFPT98xoNOoadXuLcfN5zm5LXxx0iOr4TKs7ILpObTpAcwGniE5cTCJaCHNaU2fMmGvjhHKN3HoE00by7OVcII");
// get from localstorage

const currentCart = localStorage.getItem("cart");
const items = [];
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
        body: JSON.stringify({ items }),
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
    console.log("testing");
    setLoading(true);
    

    const { error } = await stripe.confirmPayment({
        elements,
        confirmParams: {
            // Make sure to change this to your payment completion page
            return_url: "https://localhost:44339/Checkout/PaymentCompleted",
        },
    });

    // This point will only be reached if there is an immediate error when
    // confirming the payment. Otherwise, your customer will be redirected to
    // your `return_url`. For some payment methods like iDEAL, your customer will
    // be redirected to an intermediate site first to authorize the payment, then
    // redirected to the `return_url`.
    if (error.type === "card_error" || error.type === "validation_error") {
        showMessage(error.message);
    } else {
        showMessage("An unexpected error occured.");
    }

    setLoading(false);
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
        //document.querySelector("#spinner").classList.remove("hidden");
        //document.querySelector("#button-text").classList.add("hidden");
    } else {
        document.querySelector("#submit").disabled = false;
        //document.querySelector("#spinner").classList.add("hidden");
        //document.querySelector("#button-text").classList.remove("hidden");
    }
}

function drawCart(items) {
    $("#payment-details").html("");
    totalPrice = 0;
    for (let item of items) {
        $("#payment-details").append(`
        <tr>
        <td> ${item.productName} <strong>x ${item.quantity} </strong> </td>
        <td> ${item.price * item.quantity } </td>
        </tr>
        `)
        totalPrice += item.price * item.quantity

    }
    $("#payment-details").append("<tr> <td><strong>Order Total</strong> </td> <td> " + totalPrice+" </td> </tr>")
   
}