function openForm(element) {
    var id = element.getAttribute('data-orderId');
    $.ajax({
        url: '/Account/OrderDetail',
        type: "POST",
        data: {
            id: id
        },
        success: function (data) {
            var listOrderD = $('#listOrderDetail');
            listOrderD.empty();
            var totalPrice = 0;
            $.each(data.orderDetails, function (index, item) {
                var htmlCode = '<div class="row">' +
                    '<div class="col-9">' +
                    ' <input type="hidden" id="orderId" value="' + item.orderId + '" />' +

                    '<span id="name">' + item.proName + " x " + item.quantity + '</span>' +
                    '</div>' +
                    '<div class="col-3">' +
                    '<span id="price">$' + item.price + '</span>' +
                    '</div>' +
                    '</div>';
                listOrderD.append(htmlCode);
                totalPrice += item.price;
            });

            var dancer = $('#ODDancer');
            dancer.empty();
            var htmlCode = "";
            switch (data.orderDick.status) {
                case 0:
                    htmlCode = '<ul id="progressbar">' +
                        '<li class="step0 " id="step1">Pending</li>' +
                        '<li class="step0 text-center" id="step2">Accepted</li>' +
                        '<li class="step0 text-right" id="step3">Delivering</li>' +
                        '<li class="step0 text-right" id="step4">Completed</li>' +
                        '</ul>';
                    dancer.append(htmlCode);
                    break;
                case 1:
                    htmlCode = '<ul id="progressbar">' +
                        '<li class="step0 active" id="step1">Pending</li>' +
                        '<li class="step0 text-center" id="step2">Accepted</li>' +
                        '<li class="step0 text-right" id="step3">Delivering</li>' +
                        '<li class="step0 text-right" id="step4">Completed</li>' +
                        '</ul>';
                    dancer.append(htmlCode);
                    break;
                case 2:
                    htmlCode = '<ul id="progressbar">' +
                        '<li class="step0 active" id="step1">Pending</li>' +
                        '<li class="step0 active text-center" id="step2">Accepted</li>' +
                        '<li class="step0 text-right" id="step3">Delivering</li>' +
                        '<li class="step0 text-right" id="step4">Completed</li>' +
                        '</ul>';
                    dancer.append(htmlCode);
                    break;
                case 3:
                    htmlCode = '<ul id="progressbar">' +
                        '<li class="step0 active" id="step1">Pending</li>' +
                        '<li class="step0 active text-center" id="step2">Accepted</li>' +
                        '<li class="step0 active text-right" id="step3">Delivering</li>' +
                        '<li class="step0 text-right" id="step4">Completed</li>' +
                        '</ul>';
                    dancer.append(htmlCode);
                    break;
                case 4:
                    htmlCode = '<ul id="progressbar">' +
                        '<li class="step0 active" id="step1">Pending</li>' +
                        '<li class="step0 active text-center" id="step2">Accepted</li>' +
                        '<li class="step0 active text-right" id="step3">Delivering</li>' +
                        '<li class="step0 active text-right" id="step4">Completed</li>' +
                        '</ul>';
                    dancer.append(htmlCode);
                    break;
            }

            totalPrice += 10;
            $('#totalPriceRecipt').empty();
            $('#totalPriceRecipt').append("<big>$" + totalPrice + "</big>");

            $('#orderDateOD').text(data.orderDick.startDate);
            $('#fullnameOD').text(data.orderDick.fullname);
            $('#phonenumOD').text(data.orderDick.phone);
            $('#addressOD').text(data.orderDick.address);
            $('#orderId').val(data.orderDick.orderId);
            // Hide the Cancel Order button if status is not 1
            if (data.orderDick.status !== 1) {
                $('#cancelOrderButton').hide();
            } else {
                $('#cancelOrderButton').show();
            }
        }
    });
    document.getElementById("myForm").style.display = "block";
}

function closeForm() {
    document.getElementById("myForm").style.display = "none";
}

function showConfirmationForm() {
    document.getElementById('confirmationForm').style.display = 'block';
}

function closeConfirmationForm() {
    document.getElementById('confirmationForm').style.display = 'none';
}

function confirmCancelOrder() {
    const orderId = document.getElementById('orderId').value;
    cancelOrder(orderId); // Call your cancel order function
    closeConfirmationForm();
}


function closeForm1() {
    document.getElementById('myForm').style.display = 'none';
}
function cancelOrder(orderId = '') {
    var status = 0; // Cancel status
    $.ajax({
        url: '/Account/CancelOrder',
        type: 'POST',
        data: { orderId: orderId, status: status },
        success: function (response) {
          
            window.location.href = response.redirectToUrl;
        },
    
    });
}

