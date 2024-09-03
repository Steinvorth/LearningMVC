var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblItem').DataTable({
        "ajax": {
            "url": "/admin/item/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "20%" },
            { "data": "category.name", "width": "15%" },         
            { "data": "imageURL", "render": function (image) { return `<img src="../${image}" width="120px">`}, "width": "15%" },
            { "data": "creationDate", "width": "15%" },

            {
                "data": "id",  
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Item/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i> Edit
                            </a>
                            &nbsp; 
                            <a onclick=Delete("/Admin/Item/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                <i class="far fa-trash-alt"></i> Delete
                            </a>
                        </div>
                    `;
                }, "width": "25%"
            }

        ],
        // this is for the language of the table, it will show the text in the language that we want and the buttons
        "language":
        {
            "decimal": "",
            "emptyTable": "No data found",
            "info": "Showing _START_ to _END_ of _TOTAL_ entries",
            "infoEmpty": "Showing 0 to 0 of 0 entries",
            "infoFiltered": "(filtered from _MAX_ total entries)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Show _MENU_ entries",
            "loadingRecords": "Loading...",
            "processing": "Processing...",
            "search": "Search:",
            "zeroRecords": "No matching records found",
            "paginate": {
                "first": "First",
                "last": "Last",
                "next": "Next",
                "previous": "Previous"
            }
        }, "width": "100%"
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message)
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message)
                    }
                }
            });
        }
    });
}