$(document).ready(function () {
    $("#dataTableRegion").dataTable({
        "ajax": {
            url: "/Region/GetAll",
            type: "GET",
            dataSrc: "",
            dataType: "JSON"
        },
        "columns": [
            {
                "data": "",
                "render": function (data, type, row) {
                    console.log(row);
                    return `${row.name}`;
                }
            }
        ]
    });

});

//$.ajax({
//    url: "/Region/GetAll",
//            type: "GET",
//            dataSrc: "",
//            dataType: "JSON"
//}).done(res => {
//    var text = ""
//    $.each(res.data, function (key, val) {
//        text += `<tr>
//        <td>${val.name}</td>
//    </tr>`
//    })
//    $(`#tbodyregion`).cshtml(text);
//}).fail((error) => {
//    console.log(error);
//})