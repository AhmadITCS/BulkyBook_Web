﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    if (!$.fn.DataTable.isDataTable("#tblData")) { // تحقق من أن الجدول لم يتم تحميله مسبقًا
        dataTable = $('#tblData').DataTable({
            "ajax": {
                url: '/admin/Product/getall',
                type: "GET",
                dataType: "json" // تأكد من أن "T" في "dataType" كبيرة
            },
            "columns": [
                { data: 'title', "width": "25%" },
                { data: 'isbn', "width": "15%" },
                { data: 'listPrice', "width": "10%" },
                { data: 'author', "width": "20%" },
                { data: 'category.name', "width": "15%" },
                {
                    data: 'id',
                    "render": function (data) {
                        return `
                            <div class="w-75 btn-group" role="group">
                                <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2">
                                    <i class="bi bi-pencil-square"></i> Edit
                                </a>
                                <a onClick=Delete('/admin/product/${data}')class="btn btn-danger mx-2">
                                    <i class="bi bi-trash-fill"></i> Delete
                                </a>
                            </div>
                        `;
                    },
                    "width": "25%"
                }
            ]
        });
    }
}
function Delete(url) {
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
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message)
                }
            })
        }
    });

}