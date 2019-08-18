$(document).ready(function () {
    //LoadCategory();

  //  LoadData(11);
    //$.urlParam = function (name) {
    //    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);

    //    return results[1] || 0;
    //}
   // var id = $.urlParam('id');


    //if (id == null) {
    //    LoadData(11);
    //}

    //if (parseInt(id, 10) !== 0) {
    //    alert(id);
    //    LoadData(id);
    //} else {
    //    LoadData(11);
    //}
    
});

var Config = {
    pageSize: 15,
    pageIndex: 1,
}

$("#listCategory").on('change', function () {
    var id = $(this).val();
    var url = "/QA";
    var address = window.location.href;

    if (address.indexOf(url) == -1) {
        window.location.href = "/QA/Feedback";
    }  
});

$("#subCategory").on('change', function () {
    var id = $(this).val();
  //  LoadData(id);
});

function LoadData(id) {
    var template = $("#table-template").html();
    //  var render = "";
    $.ajax({
        type: "GET",
        data: {
            pageSize: Config.pageSize,
            page: Config.pageIndex,
            id: id
        },
        url: "/QA/GetData",
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
                        LoadData(id);
                    });
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
$("body").on("click", ".btn-edit", function (e) {
    e.preventDefault();
    var that = $(this).data("id");

    $('#modalDetail').modal('show');
    $.ajax({
        type: "GET",
        url: "/QA/GetAnswer",
        data: { id: that },
        dataType: "json",
        success: function (response) {
            ClearData();
            var data = response;
            var question = '<h5 class="modal-title" id="modalTitle">' + data.Question + '</h5>';
            var content = data.Answer;

            $("#question").append(question);
            $("#content").append(content);

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
