$(document).ready(function () {
    LoadCategory();
    LoadSubCategory();
});

function LoadCategory() {
    $.ajax({
        type: "GET",
        url: "/QA/GetCategory",
        data: "{}",
        success: function (data) {
            //var render = '';
            var render = '<option value="-1">-----------Chọn-----------</option>';
            for (var i = 0; i < data.length; i++) {
                render += '<option value="' + data[i].Id + '">' + data[i].CategoryName + '</option>';
            }
            $("#listCategory").html(render);
        }
    });
}

function LoadSubCategory() {
    $.ajax({
        type: "GET",
        url: "/QA/GetSubCategory",
        data: { id: 11 },
        success: function (data) {
            var s = '<option value="-1">-----------Chọn-----------</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '">' + data[i].CategoryName + '</option>';
            }
            $("#subCategory").html(s);
        }
    });
}

$("#listCategory").on('change', function () {
    var id = $(this).val();
    $.ajax({
        type: "GET",
        url: "/QA/GetSubCategory",
        data: { id: id },
        success: function (data) {
            var s = '<option value="-1">-----------Chọn-----------</option>';
            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].Id + '">' + data[i].CategoryName + '</option>';
            }
            $("#subCategory").html(s);
        }
    });
});