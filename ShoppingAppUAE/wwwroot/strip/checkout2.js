const stripe = Stripe("pk_test_51KidVZLdvjUTqFPT98xoNOoadXuLcfN5zm5LXxx0iOr4TKs7ILpObTpAcwGniE5cTCJaCHNaU2fMmGvjhHKN3HoE00by7OVcII");
// get from localstorage
const items = [{ id: "xl-tshirt" }];

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