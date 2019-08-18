var common = {
    configs: {
        pageSize: 10,
        pageIndex: 1
    },

    confirm: function (message, okCallback) {
        bootbox.confirm({
            message: message,
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result === true) {
                    okCallback();
                }
            }
        });
    },
    dateFormatJson: function (dateTime) {
        if (dateTime === null || dateTime === "")
            return "";
        var newDate = new Date(parseInt(datetime.substr(6)));
        var month = newDate.getMonth() + 1;
        var day = newDate.getDate();
        var year = newDate.getFullYear();

        if (month < 10)
            month = "0" + month;
        if (day < 10)
            day = "0" + day;

        return year + "." + month + "." + day;
    },
    dateTimeFormatJson: function (dateTime) {
        if (dateTime === null || dateTime === "")
            return "";
        var newDate = new Date(parseInt(dateTime.substr(6)));
        var month = newDate.getMonth() + 1;
        var day = newDate.getDate();
        var year = newDate.getFullYear();
        var hh = newDate.getHours();
        var mm = newDate.getMinutes();
        var ss = newDate.getSeconds();

        if (month < 10)
            month = "0" + month;
        if (day < 10)
            day = "0" + day;
        if (hh < 10)
            hh = "0" + hh;
        if (mm < 10)
            mm = "0" + mm;
        if (ss < 10)
            ss = "0" + ss;

        return year + "." + month + "." + day + " " + hh + ":" + mm + ":" + ss;
    },
    isValidDate: function (source, dateDefault) {
        if (dateDefault === undefined || dateDefault === null)
            dateDefault = new Date();
        var comp = source.split('.');
        if (comp.length !== 3)
            return dateDefault;
        var y = parseInt(comp[0], 10);
        var m = parseInt(comp[1], 10);
        var d = parseInt(comp[2], 10);
        var date = new Date(y, m - 1, d);
        if (date.getFullYear() === y && date.getMonth() + 1 === m && date.getDate() === d) {
            return date;
        } else {
            return dateDefault;
        }
    },

    formatNumber: function (number, precision) {
        if (!isFinite(number)) {
            return number.toString();
        }
        var a = number.toFixed(precision).split(".");
        a[0] = a[0].replace(/\d(?=(\d{3})+$)/g, "$&,");
        return a.join(".");
    },

    setTableHeight: function ($table) {
        $table.css({ "height": $FOOTER.position().top - $table.offset().top - 29 });
    },
    createCookie: function (name, value, days) {
        var expireDate = "; expires=" + (new Date()).toGMTString();
        document.cookie = name + "=" + value + expireDate + "; path=/";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();
        }
        else expires = "";
        document.cookie = name + "=" + value + expires + "; path=/";
    },
    readCookie: function (name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) === 0)
                return c.substring(nameEQ.length, c.length);
        }
        return "";
    },
    checkIsNullOrEmpty: function ($element, mess) {
        if ($element.val() === "") {
            common.notify("Hyosung Motor", mess, "gray error");
            $element.focus();
            return false;
        }
        return true;
    }
};

$(document).ajaxSend(function (e, xhr, options) {
    if (options.type.toUpperCase() === "POST" || options.type.toUpperCase() === "PUT") {
        var token = $("form").find("input[name='__RequestVerificationToken']").val();
        xhr.setRequestHeader("RequestVerificationToken", token);
    }
});