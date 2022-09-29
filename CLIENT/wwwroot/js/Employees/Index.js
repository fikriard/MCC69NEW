$(document).ready(function () {
    $("#dataTableEmployees").dataTable({
        "ajax": {
            url: "/Employees/GetAll",
            type: "GET",
            dataSrc: "",
            dataType: "JSON"
        },
        "columns": [
            {
                "data": "",
                "render": function (data, type, row) {
                    console.log(row);
                    return `${row.firstName}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    console.log(row);
                    return `${row.lastName}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    console.log(row);
                    return `${row.email}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    console.log(row);
                    return `${row.phoneNumber}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    console.log(row);
                    return `${row.hireDate}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    console.log(row);
                    return `${row.jobs.title}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    console.log(row);
                    return `${row.salary}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    console.log(row);
                    return `${row.manager}`;
                }
            },
            {
                "data": "",
                "render": function (data, type, row) {
                    console.log(row);
                    return `${row.departments.name}`;
                }
            }
        ]
    });

});