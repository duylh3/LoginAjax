$(document).ready(function () {
    LoadData();
});

var Config = {
    pageSize: 15,
    pageIndex: 1,
}

function LoadData() {
    var template = $("#table-template").html();
    var render = "";
    $.ajax({
        type: "GET",
        data: {
            pageSize: Config.pageSize,
            page: Config.pageIndex
            // cate: cate
        },
        url: "/QA/GetHistory",
        dataType: "json",
        success: function (response) {
            //if (response.Results.length === 0) {
            //    $("#tbl-content").html("");
            //    $("#tbl-content").append($("<tr>").append($("<td>").attr("colspan", 8).html("Không có câu hỏi liên quan!!").addClass("text-center")));
            //}

            if (response.data.length === 0) {
                    $("#tbl-content").html("");
                    $("#tbl-content").append($("<tr>").append($("<td>").attr("colspan", 8).html("Không có câu hỏi liên quan!!").addClass("text-center")));
            }

            if (response.status) {
                var data = response.data;
                var html = '';
                $.each(data, function (i, item) {
                    render += Mustache.render(template, {
                        Sequence: i + 1,
                        Question: item.Question,
                        LoginId: item.LoginId,
                        Name: item.Name,
                        ENTER_DATE: dateFormatJson(item.EnterDate),
                        WORKGROUP: item.WorkGroup,
                        CreatedDate: dateFormatJson(item.CreatedDate)
                    });
                })
                $("#tbl-content").html(render);
                //   $("#lblTotalRecord").html(response.RowCount);

                paging(response.total, function () {
                    LoadData();
                });
            }
        },
        error: function (status) {
            console.log(status);
        }
    });
}

function paging(totalRow, callback) {
    var totalPage = Math.ceil(totalRow / Config.pageSize);
    $('#pagination').twbsPagination({
        totalPages: totalPage,
        visiblePages: 10,
        onPageClick: function (event, page) {
            Config.pageIndex = page;
            setTimeout(callback, 200);
            LoadData()
        }
    });
}


function dateFormatJson(dateTime) {
    if (dateTime === null || dateTime === "")
        return "";
    var newDate = new Date(parseInt(dateTime.substr(6)));
    var month = newDate.getMonth() + 1;
    var day = newDate.getDate();
    var year = newDate.getFullYear();

    if (month < 10)
        month = "0" + month;
    if (day < 10)
        day = "0" + day;

    return year + "." + month + "." + day;
}


//function LoadData() {
//    var template = $("#table-template").html();
//    var render = "";
//    $.ajax({
//        type: "GET",
//        data: {
//            pageSize: 1,
//            page: 1
//            // cate: cate
//        },
//        url: "/QA/GetHistory",
//        dataType: "json",
//        success: function (response) {
//            //if (response.Results.length === 0) {
//            //    $("#tbl-content").html("");
//            //    $("#tbl-content").append($("<tr>").append($("<td>").attr("colspan", 8).html("Không có câu hỏi liên quan!!").addClass("text-center")));
//            //}

//            $.each(response.Results, function (i, item) {
//                render += Mustache.render(template, {
//                    Sequence: i + 1,
//                    Question: item.Question,
//                    LoginId: item.LoginId,
//                    Name: item.Name,
//                    ENTER_DATE: dateFormatJson(item.EnterDate),
//                    WORKGROUP: item.WorkGroup,
//                    CreatedDate: dateFormatJson(item.CreatedDate)
//                });
//                if (render != "") {
//                    $("#tbl-content").html(render);
//                    $("#lblTotalRecord").html(response.RowCount);
//                }
//                paging(response.total, function () {
//                    LoadData();
//                });
//            })
//        },
//        error: function (status) {
//            console.log(status);
//        }
//    });
//}