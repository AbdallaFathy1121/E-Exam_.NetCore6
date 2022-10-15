function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "Delete This User",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        location.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    });
}

function Block(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "Block This User",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, BLock it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'POST',
                success: function (data) {
                    if (data.success) {
                        location.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    });
}

function UnBlock(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "UnBlock This User",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, UnBlock it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'POST',
                success: function (data) {
                    if (data.success) {
                        location.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    });
}

