let username = "";
let password = "";
let remember = false;

$('#txtUsername').on('change', function () {
    hideError("errUsername");
    username = getValueById("txtUsername");
})

$('#txtPassword').on('change', function () {
    hideError("errPassword");
    password = getValueById("txtPassword");
})

$('#cbRemember').on('change', function () {
    remember = document.getElementById('cbRemember').checked;
})

$('#btnLogin').on('click', function () {
    validate();
})


const validate = () => {
    var isValid = true;
    if (username.trim() === "") {
        showError("errUsername", "This information is required")
        isValid = false;
    } else if (username.length > 50) {
        showError("errUsername", "Username can't be more than 50 characters")
        isValid = false;
    } else {
        hideError("errUsername");
    }

    if (password.trim() === "") {
        showError("errPassword", "This information is required")
        isValid = false;
    } else if (password.length < 6) {
        showError("errPassword", "Password must be at least 6 characters long")
        isValid = false;
    } else if (password.length > 32) {
        showError("errPassword", "Password can't be more than 32 characters");
    } else {
        hideError("errPassword");
    }
    if (isValid) {
        hideError("errLogin");
        handleLogin();
    }
}

const handleLogin = () => {

    $.ajax({
        url: '/Login/OnPostLogin',
        type: "POST",
        data: {
            username: username,
            password: password,
            isRemember: remember
        },
        success: function (data) {
            if (data === "False") {
                showError("errLogin", "Username or password is incorrect");
            } else {
                sessionStorage.setItem("username", username);
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
