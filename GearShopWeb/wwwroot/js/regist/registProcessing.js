let username = "";
let password = "";
let fullname = "";
let phone = "";
let email = "";
let rePassword = "";
let userExist = false;

$('#txtUsername').on('change', function () {
    hideError("errUsername");
    username = getValueById("txtUsername").trim();
    checkExist();
})

const checkExist = () => {
    console.log("start of check");
    $.ajax({
        url: '/Register/CheckUserExist',
        type: "POST",
        data: {
            username: username,
        },
        success: function (data) {
            if (data === "False") {
                console.log("user exist");
                userExist = true;
                showError("errUsername", "Username exist");
            } else {
                console.log("user free to use");
                userExist = false;
            }
        },
        error: function () {
            $('#registError').text('An error occurred. Please try again later.').show();
        }
    });
}
$('#txtFullname').on('change', function () {
    hideError("errFullname");
    fullname = getValueById("txtFullname").trim();
})

$('#txtPhone').on('change', function () {
    hideError("errPhone");
    phone = getValueById("txtPhone").trim();
})
$('#txtEmail').on('change', function () {
    hideError("errEmail");
    email = getValueById("txtEmail").trim();
})
$('#pwdPassword').on('change', function () {
    hideError("errPassword");
    password = getValueById("pwdPassword").trim();
})
$('#pwdRePassword').on('change', function () {
    hideError("errRePassword");
    rePassword = getValueById("pwdRePassword").trim();
})
$('#btnSignup').on('click', function () {
    validate();
})


const validate = () => {
    var isValid = true;
    //username
    if (username === "") {
        showError("errUsername", "This information is required");
        isValid = false;
    } else if (username.length < 6 || username.length > 50) {
        showError("errUsername", "Username must be between 6 and 50 characters");
        isValid = false;
    } else {
        hideError("errUsername");
    }
    //fullname
    if (fullname === "") {
        showError("errFullname", "This information is required");
        isValid = false;
    } else if (fullname.length > 100) {
        showError("errFullname", "Fullname can't be longer than 100 characters");
        isValid = false;
    } else {
        hideError("errFullname");
    }
    //phone
    if (phone === "") {
        showError("errPhone", "This information is required");
        isValid = false;
    } else if (!validatePhone()) {
        showError("errPhone", "Phone number not in the correct format");
        isValid = false;
    } else {
        hideError("errPhone");
    }
    //email
    if (email === "") {
        showError("errEmail", "This information is required");
        isValid = false;
    } else if (!validateEmail()) {
        showError("errEmail", "Email not in the correct format");
        isValid = false;
    } else {
        hideError("errEmail");
    }
    //password
    if (password === "") {
        showError("errPassword", "This information is required");
        isValid = false;
    } else if (password.length < 6 || password.length > 32) {
        showError("errPassword", "Password must be between 6 and 32 characters");
        isValid = false;
    } else {
        hideError("errPassword");
    }
    //rePassword
    if (rePassword === "") {
        showError("errRePassword", "This information is required");
        isValid = false;
    } else if (rePassword !== password) {
        showError("errRePassword", "You have entered a different password");
        isValid = false;
    } else {
        hideError("errRePassword");
    }
    //Run regist
    if (isValid && !userExist) {
        handleRegist();
    }
}

function validatePhone() {
    phoneRegex = /([0]{1})([0-9]{9})/g;
    return phone.match(phoneRegex);
}

function validateEmail() {
    emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/g
    return email.match(emailRegex);
}
const handleRegist = () => {
    $.ajax({
        url: '/Register/OnPostRegister',
        type: "POST",
        data: {
            username: username,
            fullname: fullname,
            phone: phone,
            email: email,
            password: password,
            rePassword: rePassword
        },
        success: function (data) {
            if (data.success === false) {
                console.log("cook");
            } else {
                window.location.href = '/Login';
            }
        },
        error: function () {
            $('#registError').text('An error occurred. Please try again later.').show();
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
