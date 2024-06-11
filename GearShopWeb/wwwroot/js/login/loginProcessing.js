﻿let username = "";
let password = "";

$('#txtUsername').on('change', function () {
    hideError("errUsername");
    username = getValueById("txtUsername");
})

$('#txtPassword').on('change', function () {
    hideError("errPassword");
    password = getValueById("txtPassword");
})

$('#btnLogin').on('click', function () {
    validate();
})


const validate = () => {
    var isValid = true;
    if (username.trim() === "") {
        showError("errUsername","This information is required")
        isValid = false;
    } else if (username.length > 20) {
        showError("errUsername", "Username can not more than 20 characters!")
        isValid = false;
    } else {
        hideError("errUsername");
    }

    if (password.trim() === "") {
        showError("errPassword", "This information is required")
        isValid = false;
    } else if (password.length < 0) {
        showError("errPassword", "Password must be at least 8 characters long")
        isValid = false;
    } else {
        hideError("errPassword");
    }

    if (isValid) {
        handleLogin();
    }
}

const handleLogin = () => {
    $.ajax({
        url: '/Login/OnPostLogin',
        type: "POST",
        data: {
            username: username,
            password: password
        },
        success: function (data) {
            if (data.success === false) {
                console.log("cook");
            } else {
                window.location.href = '/Home';
            }
        },
        error: function () {
            $('#loginError').text('An error occurred. Please try again later.').show();
        }
    });
}

// Lấy giá tin input
function getValueById(id) {
    return $('#' + id).val();
}

// Hiển thị thông báo lỗi cho một trường
function showError(id, message) {
    $('#_' + id).css('display', 'block');
    $('#' + id).text(message);
}

// Ẩn thông báo lỗi của một trường
function hideError(id) {
    $('#_' + id).css('display', 'none');
    $('#' + id).text('');
}
