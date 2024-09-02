var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    console.log("Starting DataTable")

    dataTable = $('#tblCategory').DataTable({
        "ajax": {
            "url": "/admin/category/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "name", "width": "50%" },
            { "data": "order", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="/Admin/Category/Edit/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                <i class="far fa-edit"></i> Edit
                            </a>
                            &nbsp; 
                            <a onclick=Delete("/Admin/Category/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                <i class="far fa-trash-alt"></i> Delete
                            </a>
                        </div>
                    `;
                }, "width": "25%"
            },
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