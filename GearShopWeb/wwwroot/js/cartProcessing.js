const DeleteItem = (element) => {
    var proId = element.getAttribute('data-proId');
    $.ajax({
        url: '/Cart/Delete',
        type: "POST",
        data: {
            ProId: proId
        },
        success: function (data) {
            window.location.reload();
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
    var input = button.parent().parent().find('input');
    UpdateCart(input[0]);
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
    var input = button.parent().parent().find('input');
    UpdateCart(input[0]);
}

const UpdateCart = (element) => {
    var ProId = element.getAttribute('data-proID');
    var amount = $(element).val();
    var ProPrice = parseFloat($('#dp-' + ProId).text().replace('$', ''));
    $('#total-' + ProId).text(amount * ProPrice);
    $('#cartPrice').text()
    $.ajax({
        url: '/Cart/UpdateCartData',
        type: "POST",
        data: {
            ProId: ProId,
            amount: amount
        },
        success: function (data) {
            if (data.isSuccess === true) {
                console.log("ok");
                updateTotalPrice();
            } else {
                console.log(data);

            }
        }
    });
}

const updateTotalPrice = () => {
    let totalPrice = 0;
    let selectedProIds = [];
    $('.check-box-child:checked').each(function () {
        var proId = $(this).data('priceid');
        var price = parseFloat($('#total-' + proId).text());
        totalPrice += price;
        selectedProIds.push(proId);
    });
    $('#cartTotalPrice').text('$' + totalPrice);
    console.log(selectedProIds);
    sessionStorage.setItem('ProId', selectedProIds.join(','));
    $.ajax({
        url: '/Order/StoreCheckedProduct',
        type: "POST",
        data: {
            proIds: selectedProIds
        },
        success: function (data) {

        }
    });
}

$('#all').click(function () {
    $('.check-box-child').prop('checked', $(this).prop('checked'));
    updateTotalPrice();
});

$('.check-box-child').click(function () {
    if ($('.check-box-child:checked').length == $('.check-box-child').length) {
        $('#all').prop('checked', true);
    } else {
        $('#all').prop('checked', false);
    }
    updateTotalPrice();
});