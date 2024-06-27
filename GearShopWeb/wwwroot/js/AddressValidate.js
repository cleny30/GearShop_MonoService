﻿$('form.update_address_form').submit(function (event) {
    var fullname = getValueById('fullnameUpdate');
    var phone = getValueById('phonenumUpdate');
    var address = getValueById('addressUpdate');

    var noError = true;


    if (!fullname) {
        showError('fullnameUpdate', 'Please enter your full name');
        noError = false;
    } else if (fullname.length > 100) {
        showError('fullnameUpdate', 'You full name must be less than 100 characters!');
        noError = false;
    } else {
        hideError('fullnameUpdate');
    }


    if (!phone) {
        showError('phonenumUpdate', 'Please enter your phone number!');
        noError = false;
    } else if (!isValidPhoneNumber(phone)) {
        showError('phonenumUpdate', 'Invalid phone number!');
        noError = false;
    } else {
        hideError('phonenumUpdate');
    }


    if (!address) {
        showError('addressUpdate', 'Please enter address');
        noError = false;
    } else if (address.length < 0) {
        showError('addressUpdate', 'Address must be more than 20 character');
        noError = false;
    } else {
        hideError('addressUpdate');
    }

    // Nếu không có lỗi, kiểm tra sự tồn tại của username bằng cách gửi request AJAX đến server
    if (!noError) {
        event.preventDefault();
    }


});
$('form.add_address_form').submit(function (event) {
    var fullname = getValueById('fullName');
    var phone = getValueById('phoneNumber');
    var address = getValueById('address');

    var noError = true;
  

    if (!fullname) {
        showError('fullName', 'Please enter your full name');
        noError = false;
    } else if (fullname.length > 100) {
        showError('fullName', 'You full name must be less than 100 characters!');
        noError = false;
    } else {
        hideError('fullName');
    }


    if (!phone) {
        showError('phoneNumber', 'Please enter your phone number!');
        noError = false;
    } else if (!isValidPhoneNumber(phone)) {
        showError('phoneNumber', 'Invalid phone number!');
        noError = false;
    } else {
        hideError('phoneNumber');
    }


    if (!address) {
        showError('address', 'Please enter address');
        noError = false;
    } else if (address.length < 0) {
        showError('address', 'Address must be more than 20 character');
        noError = false;
    } else {
        hideError('address');
    }
    if (!noError) {
        event.preventDefault();
    }

});


// Lấy giá tin input
function getValueById(id) {
    return $('#' + id).val();
}

// Hiển thị thông báo lỗi cho một trường
function showError(id, message) {
    $('#' + id).next('p').text(message);
}

// Ẩn thông báo lỗi của một trường
function hideError(id) {
    $('#' + id).next('p').text('');
}

function isValidPhoneNumber(phoneNum) {
    const phonePattern = /^0\d{9}$/;
    return phonePattern.test(phoneNum);
}