$(document).ready(function () {
    //---------------------------- Search Data -------------------------------//
    $("#btnSearch").click(function (e) {
        e.preventDefault();
        var template = $("#table-template").html();
        var keyword = $("#txtSearch").val();
        $.ajax({
            type: "POST",
            data: {
                pageSize: Config.pageSize,
                page: Config.pageIndex,
                keyword: keyword
            },
            url: "/QA/SearchData",
            dataType: "json",
            success: function (response) {
                if (response.data.length === 0) {
                    $("#tbl-content").html("");
                    $("#tbl-content").append($("<tr>").append($("<td>").attr("colspan", 8).html("Không có câu hỏi liên quan!!").addClass("text-center")));
                }
                if (response.status) {
                    var data = response.data;
                    var render = '';
                    $.each(data, function (i, item) {
                        render += Mustache.render(template, {
                            Sequence: i + 1,
                            //  Id: item.Id,
                            Question: item.Question,
                            CategoryName: item.CategoryName,
                            EmpId: item.EmpId,
                        });
                        $("#tbl-content").html(render);

                        paging(response.total, function () {
                            LoadData(cate);
                        });
                    })
                }
            },
            error: function (status) {
                console.log(status);
            }
        });

    });

    //---------------------- End ---------------------------------------------//

});