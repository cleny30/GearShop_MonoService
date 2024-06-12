const AddToCart = (element) => {
    var productData = JSON.parse(element.getAttribute('data-model'));
    var amount = element.getAttribute('data-amount')
    $.ajax({
        url: '/Cart/AddProductToCard',
        type: "POST",
        data: {
            data: productData,
            amount: amount
        },
        success: function (data) {
            if (data.isSuccess === true) {
                console.log("ok");
            } else {
                console.log(data);
            }
        },
        error: function () {
            $('#loginError').text('An error occurred. Please try again later.').show();
        }
    });
}