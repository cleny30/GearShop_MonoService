const username = sessionStorage.getItem("username");
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/signalrServer?username=" + username)
    .build();

connection.start()
    .then(() => {
        console.log("Connected to SignalR hub with Connection ID: " + connection.connectionId);
    })
    .catch(err => {
        console.error("Error starting connection:", err.toString());
    });

const AddToCart = (element) => {
    var productData = element.getAttribute('data-model');
    var amount = element.getAttribute('data-amount');
    if (amount === null) {
        amount = $('#quan_input').val();
    }
    $.ajax({
        url: '/Cart/AddProductToCart',
        type: "POST",
        data: {
            data: productData,
            amount: amount
        },
        success: function (data) {
            if (data.isSuccess === true) {
                if (connection.state === signalR.HubConnectionState.Connected) {
                    connection.invoke("LoadCartData").catch(function (err) {
                        return console.error(err.toString());
                    });

                    $('#myModal-check').css('display', 'block');

                    setTimeout(function () {
                        $('#myModal-check').css('display', 'none');
                    }, 1500);

                } else {
                    console.error("Connection is not in the 'Connected' state.");
                }
            } else if (data.message === "Username") {
                window.location.href = "/Login";
            } else {
                if (connection.state === signalR.HubConnectionState.Connected) {
                    connection.invoke("LoadCartData").catch(function (err) {
                        return console.error(err.toString());
                    });

                    $('#myModal-x').css('display', 'block');

                    setTimeout(function () {
                        $('#myModal-x').css('display', 'none');
                    }, 1500);

                } else {
                    console.error("Connection is not in the 'Connected' state.");
                }
            }
        }
    });
}

connection.on("ReceiveLoadCardData", function (count) {
    $('#cart-header-count').empty(); 
    $('#cart-header-count').text(count);
});

connection.on("LoadOrder", function () {
    $.ajax({
        url: '/Account/GetOrderDataSignalR?username=' + username, // Replace with your actual URL
        method: 'GET',
        success: function (data) {
            // Assuming 'data' is a JSON array of orders
            $('#orderTable tbody').empty();
            data.result.forEach(function (item) {
                var status = '';
                switch (item.status) {
                    case 0:
                        status = 'Cancelled';
                        break;
                    case 1:
                        status = 'Pending';
                        break;
                    case 2:
                        status = 'Accepted';
                        break;
                    case 3:
                        status = 'Delivering';
                        break;
                    case 4:
                        status = 'Completed';
                        break;
                }

                var startDate = formatDate(item.startDate);

                var endDate = item.endDate ? formatDate(item.endDate) : 'Not Complete';

                var row = `
                    <tr>
                 
                        <td class="align-middle">${startDate}</td>
                        <td class="align-middle">${endDate}</td>
                        <td class="align-middle">${status}</td>
                        <td class="align-middle">$${item.totalPrice}</td>
                        <td class="align-middle">
                            <button data-orderId="${item.orderId}" onclick="openForm(this)" class="fa fa-eye"
                                    style="background-color:transparent; border: none;"></button>
                        </td>
                    </tr>
                `;

                $('#orderTable tbody').append(row);
            });
        },
        error: function (error) {
            console.error('Error fetching data', error);
        }
    });
});
function formatDate(dateString) {
    var date = new Date(dateString);
    var day = String(date.getDate()).padStart(2, '0');
    var month = String(date.getMonth() + 1).padStart(2, '0');
    var year = date.getFullYear();
    return `${day}/${month}/${year}`;
}