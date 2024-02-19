var Details = function (id) {
    var url = "/PersonalInfo/Details?id=" + id;
    $('#titleBigModal').html("Personal Info Details");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/PersonalInfo/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Personal Info");
    }
    else {
        $('#titleBigModal').html("Add Personal Info");
    }
    loadBigModal(url);
};

var SavePersonalInfo = function () {
    if (!$("#frmPersonalInfo").valid()) {
        return;
    }

    var _frmCategories = $("#frmPersonalInfo").serialize();
    $.ajax({
        type: "POST",
        url: "/PersonalInfo/AddEdit",
        data: _frmCategories,
        success: function (result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $('#tblPersonalInfo').DataTable().ajax.reload();
            });
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var Delete = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "DELETE",
                url: "/PersonalInfo/Delete?id=" + id,
                success: function (result) {
                    var message = "Personal Info has been deleted successfully. Personal Info ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblPersonalInfo').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};