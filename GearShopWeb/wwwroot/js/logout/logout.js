const handleLogout = () => {

    $.ajax({
        url: '/Account/Logout',
        type: "POST",
        success: function (data) {
            sessionStorage.removeItem("username");
            window.location.href = "/";
        },
        error: function () {
        }
    });
}