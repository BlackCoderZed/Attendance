EmployeeListView = function () {
    var table = null;
    this.Initialize = function () {
        InitializeGridview();

        $('#BT_Employee_Register').on('click', function () {
            ShowRegDlg(false, null);
        });

        $('#BT_Employee_Refresh').on('click', function () {
            Refresh();
        });
    }

    var InitializeGridview = function () {
        table = $('#GridviewEmployeeList').DataTable({
            responsive: true,
            "scrollY": '50vh',
            "processing": true,
            "serverSide": true,
            //"filter": false, // remove search box
            "orderMulti": false, // disable multiple columns at once
            "ajax": {
                "url": commonUtil.AppRootDir + '/Employee/GetEmployeeInfoList',
                "type": 'POST',
                "datatype": 'json',
                "dataSrc": function (response) {
                    if (commonUtil.HandleGridviewResponseError(response) === false) {
                        return response.data;
                    }
                },
                "complete": function (response) {
                    //commonUtil.HandleGridviewResponseError(response);
                },
                "error": function (jqXHR, textStatus, errorThrown) {
                    // Handle the error event (called when there is an error)
                    alert('Failed to load data: ' + textStatus + ' - ' + errorThrown);
                }
            },
            "columns": [
                { "data": "ID", "autoWidth": true },
                { "data": "Name", "autoWidth": true },
                { "data": "NRC", "autoWidth": true },
                { "data": "Gender", "autoWidth": true },
                { "data": "PhoneNumber", "autoWidth": true },
                { "data": "Email", "autoWidth": true },
                { "data": "Address", "autoWidth": true },
                {
                    "data": "RegistDateTime", "autoWidth": true,
                    "render": function (data, row, type) {
                        var dateVal = new Date(data);
                        return dateVal.toLocaleString('en-GB');
                    }
                },
                {
                    "data": null, "autoWidth": false, "orderable": false,
                    "render": function (data, type, row) {
                        var editButton = '<button class="btn btn-sm btn-outline-warning employee-update" data-employeeid="' + row.ID + '"><i class="fa fa-edit"></i>' + resources.LB_UPDATE + '</button>';
                        var deleteButton = '<button class="btn btn-sm btn-outline-danger employee-delete" data-employeeid="' + row.ID + '"><i class="fa fa-edit"></i>' + resources.LB_DELETE + '</button>';

                        return editButton + '&nbsp' + deleteButton;
                    }
                }
            ],
            "order": [[0, "desc"]]
        });

        $('#GridviewEmployeeList').on('click', '.employee-update', function () {
            var employeeId = $(this).data('employeeid');
            ShowRegDlg(true, employeeId);
        });

        $('#GridviewEmployeeList').on('click', '.employee-delete', function () {

        });
    }

    this.Reload = function () {
        Refresh();
    }

    var ShowRegDlg = function (isUpdate, employeeId) {

    }

    var Refresh = function () {
        table.ajax.reload();
    }
}

var employeeListView = new EmployeeListView();