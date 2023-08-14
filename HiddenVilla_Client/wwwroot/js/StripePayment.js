redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51KM9FiIWqFO89aiGxR7Y4HNLJcqyxqWxBKcVhorekLkPHK2XwAtQIu5AnfjTic1GbzmeineyscZ3OaVBvW4y2Goo00UATT4h3a');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};