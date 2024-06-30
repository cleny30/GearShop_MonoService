$(document).ready(function () {
    // Function to connect to SignalR hub
    function connectToSignalR() {
        var username = sessionStorage.getItem("username");
        if (username) {
            var connection = new signalR.HubConnectionBuilder()
                .withUrl("/signalrServer?username=" + username)
                .build();

            connection.on("LoadOrder", function () {
                LoadOrderData();
            });

            connection.start().then(function () {
                console.log("Connected to SignalR hub with Connection ID: " + connection.connectionId);
            }).catch(function (err) {
                console.error(err.toString());
            });

            function LoadOrderData() {
                $.ajax({
                    url: '/Account/GetOrderData?username=' + username, // Replace with your actual URL
                    method: 'GET',
                    success: function (data) {
                        // Assuming 'data' is a JSON array of orders
                        $('#orderTable tbody').empty();
                        data.result.forEach(function (item) {
                            var status = '';
                            switch (item.status) {
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
                        <td class="align-middle">${item.orderId}</td>
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
            }
        } else {
            console.log("Username is not set in session storage.");
        }
    }

    // Automatically connect to SignalR hub if username is already set in session storage
    if (sessionStorage.getItem("username")) {
        connectToSignalR();
    }
});

function formatDate(dateString) {
    var date = new Date(dateString);
    var day = String(date.getDate()).padStart(2, '0');
    var month = String(date.getMonth() + 1).padStart(2, '0');
    var year = date.getFullYear();
    return `${day}/${month}/${year}`;
}