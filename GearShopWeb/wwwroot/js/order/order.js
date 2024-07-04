const ShowListAddress = () => {
    $('#listAddress').css('display', 'block');
};

const CloseListAddress = () => {
    $('#listAddress').css('display', 'none');
}

const ChangeAddress = () => {
    var selectedRadio = $('input[name="addressId"]:checked');

    if (selectedRadio.length > 0) {
        var fullname = selectedRadio.data('fullname');
        var phone = selectedRadio.data('phone');
        var specific = selectedRadio.data('specific');
        var address = selectedRadio.data('address');

        $('#formFullname').text(fullname);
        $('#formPhone').text(phone);

        if (specific) {
            $('#formAddress').text(specific + ', ' + address);
            $('#ChosenADA').val(specific + ', ' + address);
        } else {
            $('#formAddress').text(address);
            $('#ChosenADA').val(address);
        }

        $('#ChosenFNA').val(fullname);
        $('#ChosenPNA').val(phone);
        $('#listAddress').css('display', 'none');
    } else {
        alert('Please select an address.');
    }
}

const UpdateAddress = (button)=>{
    // Get data from the button
    const $button = $(button);
    const fullname = $button.data('fullname');
    const phone = $button.data('phonenum');
    const address = $button.data('address');
    const addressID = $button.data('id');
    const isdefault = $button.data('isdefault');
    const specific = $button.data('specific');
    const $checkbox = $('#defaultAddress');

    // Populate the form fields with the existing data
    $('#idUpdate').val(addressID);
    $('#fullnameUpdate').val(fullname);
    $('#phonenumUpdate').val(phone);
    $('#addressUpdate').val(address);
    $('#specificAddressUpdate').val(specific);

    if (isdefault === true) {
        $checkbox.prop('checked', true).prop('disabled', true); // Disable the checkbox if it's checked
    } else {
        $checkbox.prop('checked', false).prop('disabled', false); // Enable the checkbox if it's not checked
    }

    $('#updateAddressForm').data('addressid', addressID);

    // Show the popup form
    $('#editAddress').css('display', 'block');
    $('#listAddress').css('display', 'none');
}

const CreateAddress = () => {
    $('#createAddress').css('display', 'block');
    $('#listAddress').css('display', 'none');
}

const CloseForm = (type) => {
    $('#listAddress').css('display', 'block');
    $('#' + type).css('display', 'none');
}
