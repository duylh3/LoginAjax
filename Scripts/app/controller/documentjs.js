$(document).ready(function () {
    $("#listCategory").on('change', function () {
        var id = $(this).val();
        var url = "/QA";
        var address = window.location.href;

        if (address.indexOf(url) == -1) {
            window.location.href = "/QA/SendFeedback";
        }
    });

    var Config = {
        pageSize: 15,
        pageIndex: 1,
    }
    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        return results[1] || 0;
    }
    var cate = $.urlParam('cate');
    LoadData(cate);

    $("#Law, #Labor, #RuleInsurrance,#RuleTax,#RuleCompany, #RuleHR, #RuleSafety, #RuleSercurityInfo, #RuleEthic").click(function (e) {
        e.preventDefault();
        cate = $(this).attr("id");
        LoadData(cate);
    });

    function LoadData(cate) {
        var template = $("#table-template").html();
        var render = "";
        $.ajax({
            type: "GET",
            data: {
                pageSize: 15,
                page: 1,
                cate: cate
            },
            url: "/Document/GetData",
            dataType: "json",
            success: function (response) {
                if (response.data.length === 0) {
                    $("#categoryName").html("");
                    $("#tbl-content").html("");
                    $("#tbl-content").append($("<tr>").append($("<td>").attr("colspan", 8).html("Không có tài liệu liên quan!!").addClass("text-center")));
                }
                if (response.status) {
                    var data = response.data;
                    var render = '';
                    $.each(data, function (i, item) {
                        render += Mustache.render(template, {
                            Sequence: i + 1,
                            Id: item.Id,
                            DocumentName: item.DocumentName,
                        });

                        if (render) {
                            $("#tbl-content").html(render);
                            $("#lblTotalRecord").html(response.RowCount);
                            $("#categoryName").html(response.categoryName);
                        }
                    })
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

    //---------------------- Cau Tra loi ---------------------------------------------//
    $("body").on("click", ".btnDetail", function (e) {
        e.preventDefault();
        var that = $(this).data("id");
        $('#myModal').modal('show');
        $.ajax({
            type: "GET",
            url: "/Document/GetDocumentDetail",
            data: { id: that },
            dataType: "json",
            success: function (response) {
                ClearData();
                var data = response;
                var document = '<h5 class="modal-title" id="modalTitle">' + data.DocumentName + '</h5>';
                $("#iContent").attr("src", data.Url);
                $("#question").append(document);
                $('#modalDetail').modal('show');
            },
            error: function (status) {
                console.log(status);
            }
        });
    });
    //---------------------- End ---------------------------------------------//
    function ClearData() {
        $("#question").text("");
        $("#content").text("");
    }
});