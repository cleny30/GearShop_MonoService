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

const handleIncrease = (button) => {
    var button = $(button);
    var input = button.parent().parent().find('input');
    var oldValue = parseInt(input.val());
    var stock = parseInt(input.attr('data-proQuan'));
    if (oldValue < stock) {
        var newVal = parseInt(parseInt(oldValue) + 1);
    } else {
        newVal = parseInt(stock);
    }
    input.val(newVal);
}

const handleDecrease = (button) => {
    var button = $(button);
    var input = button.parent().parent().find('input');
    var oldValue = parseInt(input.val());
    if (oldValue > 1) {
        var newVal = parseInt(parseInt(oldValue) - 1);
    } else {
        newVal = 1;
    }
    input.val(newVal);
}