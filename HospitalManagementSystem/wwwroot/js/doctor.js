var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Doctor/GetAll"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "email", "width": "30%" },
            { "data": "phoneNumber", "width": "10%" },
            { "data": "residentOf", "width": "10%" },
            //{ "data": "gender", "width": "10%" },
            //{ "data": "hasLicense", "width": "10%" },
            {
                "data": "imageUrl",
                "render": function (data) {
                    return `
                    <img src=${data} class="avatar" width="100" height="100"/>
                    `;
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                     <div class="text-center">
                    <a href="/Admin/Doctor/Upsert/${data}" class="btn btn-info">
                    <i class="fas fa-edit"></i>
                    </a>
                    <a class="btn btn-danger" onClick=Delete("/Admin/Doctor/Delete/${data}")>
                    <i class="fas fa-trash-alt"></i>
                    </a>
                    </div>
                    `;
                }
            }
        ]
    })
}

function Delete(url) {
    swal({
        title: "Want to delete Data?",
        text: "Delete Information !",
        icon: "error",
        buttons: true,
        dangerModel: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}