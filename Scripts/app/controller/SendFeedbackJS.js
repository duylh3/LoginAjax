$(document).ready(function () {

    $("#categoryName").html("Câu hỏi liên quan");

    $("#listCategory").on('change', function () {
        CheckSubCategory();
        var category = $("#listCategory option:selected").html().toLowerCase();

        if (category == "liên quan nhân sự") {
            category = "nhân sự";
        }
        if (category == "Câu hỏi khác") {
            category = "nhân sự khác";
        }

        var str = "Câu hỏi liên quan " + category;

        $("#categoryName").html(str);
    });

    $("#subCategory").on('change', function () {
        CheckSubCategory();
        var subCategory = $("#subCategory option:selected").html();
        var category = $("#listCategory option:selected").html().toLowerCase();

        if (category == "liên quan nhân sự") {
            category = "nhân sự";
        }
        if (category === "Câu hỏi khác") {
            category = "nhân sự khác";
        }

        var str = "Câu hỏi liên quan " + category + ' / ' + subCategory;

        $("#categoryName").html(str);
    });

    $("#sendEmail").click(function () {
        var category = parseInt($("#listCategory").val());
        var subCategory = parseInt($("#subCategory").val());
        var content = $("#txtFeedback").val();
        var id = $("#txtId").val();
        var password = $("#txtPassword").val();

        if ((category === 17 && subCategory === 21) || (category === 17 && subCategory === 22)) {
            SendData(category, subCategory, content);
        } else {
            Login(id, password, category, subCategory, content);
        }
    });

    function Login(id, password, category, subCategory, content) {
        $.ajax({
            type: "POST",
            data: {
                id: id,
                password: password
            },
            dataType: "json",
            url: "/User/UserLogin",
            success: function (res) {
                console.log(res);
                if (res.result === "OK") {
                    alert("success");
                    SendData(category, subCategory, content);
                } else {
                    alert("Please check your user name and password again!!!")
                }
            }
        });
    }
    function SendData(category, subCategory, content) {
        $.ajax({
            type: "GET",
            data: {
                category: category,
                subCategory: subCategory,
                content: content
            },
            url: "/QA/Feedback",
            dataType: "json",
            success: function (data) {
                alert("Cảm ơn bạn đã gởi câu hỏi cho chúng tôi.")
                $("#txtFeedback").val("");
            },
            error: function (xhr) {
                alert("Gởi không thành công.")
            }
        });
    }

    $("#txtFeedback").focus(function () {
        var category = $("#listCategory").val();
        if (category == -1) {
            alert('Vui lòng chọn câu hỏi');
            $("#listCategory").focus();
        }
    });

    function CheckSubCategory() {
        var category = parseInt($("#listCategory").val());
        var subCategory = parseInt($("#subCategory").val());
        if ((category === 17 && subCategory === 21) || (category === 17 && subCategory === 22)) {
            $("#divId").hide();
            $("#divPassword").hide();
        } else {
            $("#divId").show();
            $("#divPassword").show();
        }
    }
});