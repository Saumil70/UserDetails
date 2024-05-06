var dataTable;
$(document).ready(function () {

    $('#createBtn').click(function () {
        var data = $("form").serialize();
        $.ajax({
            url: '/Employee/CreateOrEdit',
            type: 'GET',
            data: data,
            success: function (response) {
                $('body').html(response);
                // Display Toastr notification upon success
                toastr.success('New country form loaded successfully!');
            },
            error: function () {
                alert('Error occurred while processing the request.');
            }
        });
    });

    $('.editBtn').click(function () {
        var data = $("form").serialize();
        var selectedEmployeeId = $(this).data('employee-id');
        if (selectedEmployeeId) {
            $.ajax({
                url: '/Employee/CreateOrEdit',
                type: 'GET',
                data: { id: selectedEmployeeId },
                success: function (response) {
                    $('body').html(response);
                    // Display Toastr notification upon success
                    toastr.success('Edit form loaded successfully!');
                },
                error: function () {
                    alert('Error occurred while processing the request.');
                }
            });
        } else {
            alert("Unable to determine the employee ID.");
        }
    });

    $('.deleteBtn').click(function () {
        var selectedEmployeeId = $(this).data('employee-id');
        if (selectedEmployeeId) {
            // Construct the URL for the delete action
            var url = '/Employee/Delete?id=' + selectedEmployeeId;

            // Display SweetAlert confirmation dialog
            Swal.fire({
                title: "Are you sure?",
                text: "You won't be able to revert this!",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#3085d6",
                cancelButtonColor: "#d33",
                confirmButtonText: "Yes, delete it!"
            }).then((result) => {
                if (result.isConfirmed) {
                    // If user confirms, send AJAX request to delete the country
                    $.ajax({
                        url: url,
                        type: 'POST', // Change type to POST
                        success: function (data) {
                            // Handle success response
                            Swal.fire("Deleted!", "The country has been deleted.", "success");
                            // You may want to perform additional actions after deletion, such as refreshing the page
                            location.reload();
                        },
                        error: function () {
                            // Handle error
                            Swal.fire("Error", "An error occurred while processing the request.", "error");
                        }
                    });
                }
            });
        } else {
            alert("Unable to determine the Employee ID.");
        }
    });
})