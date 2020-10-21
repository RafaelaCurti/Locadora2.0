function formatMoney(n, c, d, t) {
    c = isNaN(c = Math.abs(c)) ? 2 : c,
    d = d == undefined ? "," : d,
    t = t == undefined ? "." : t,
    s = n < 0 ? "-" : "",
    i = parseInt(n = Math.abs(+n || 0).toFixed(c)) + "",
    j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
}

function formatarValor(valor, pais) {
    if (pais == 221) //EUA
        return "U$ " + formatMoney(parseFloat(valor), ",", ".");
    return "R$ " + formatMoney(parseFloat(valor), ".", ",");
}

function formatValuePrice(value, country, culture) {
    culture = culture == undefined ? "pt-br" : culture;
    if (culture == "en")
        return country == 27 ? value.replace('.', '').replace(',', '.') : value.replace(/\,/g, "");
    return country == 27 ? value.replace(/\./g, "") : value.replace(',', '').replace('.', ',');
}

function getDate(element) {
    var date;
    try { date = $.datepicker.parseDate("dd/mm/yy", element.value); } catch (error) { date = null; }
    return date;
}

function isMobileDevice() {
    var check = false;
    (function (a) { if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino/i.test(a) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(a.substr(0, 4))) check = true; })(navigator.userAgent || navigator.vendor || window.opera);
    return check;
};

var addAlertToPage = function (message, type) {
    if (!toastr)
        return;

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": isMobileDevice() ? "toast-top-right" : "toast-bottom-right",
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    var $toast = toastr[type](message, null);
    return false;
}

window.onload = function () {
    x = document.getElementById('view-password');
    if (x != null) {
        x.addEventListener("touchstart", touch);
        x.addEventListener("touchend", touch);
        x.addEventListener("mousedown", mouse);
        x.addEventListener("mouseup", mouse);

        function touch(event) {
            if (event.type == 'touchstart')
                $("#Password, #senha, input[name=Password]").prop("type", "text");
            if (event.type == 'touchend')
                $("#Password, #senha, input[name=Password]").prop("type", "password");
        }
        function mouse(event) {
            if (event.type == 'mousedown')
                $("#Password, #senha, input[name=Password]").prop("type", "text");
            if (event.type == 'mouseup')
                $("#Password, #senha, input[name=Password]").prop("type", "password");
        }
    }
}

$(function () {
    $("[data-toggle='tooltip']").livequery(function () { $(this).tooltip(); });

    //$('.formatData').livequery(function () { $(this).attr("alt", "date"); });

    $('.alphanumeric').livequery(function () { $(this).alphanumeric(); });

    $('.numeric').livequery(function () { $(this).numeric(); });

    $('.alpha').livequery(function () { $(this).alpha({ allow: " " }); });

    $('.numericWithDot').livequery(function () { $(this).numeric({ allow: ",.-" }); });

    $(".btn-show-calender").livequery("click", function () {
        $(this).parents("div.input-group").find(".ui-datepicker-trigger").click();
    });

    $("img.lazy").livequery(function () { $(this).lazyload({ effect: "fadeIn" }) });

    $('ul.dropdown-menu.extended li a.not-read').livequery("click", function (e) {
        $(this).removeClass('not-read');
        var parent = $("li.dropdown.open");
        if ($(parent).prop("id") == "header_inbox_bar") {
            var cont = parseInt($("li#header_inbox_bar span.badge").text());
            if (cont == 1)
                $("li#header_inbox_bar span.badge").remove();
            else
                $("li#header_inbox_bar span.badge").text(cont - 1);
        }
        else {
            var cont = parseInt($("li#header_notification_bar span.badge").text());
            if (cont == 1)
                $("li#header_notification_bar span.badge").remove();
            else
                $("li#header_notification_bar span.badge").text(cont - 1);
        }
    });

    String.prototype.trim = function () { return this.replace(/(^\s*)|(\s*$)/g, ""); }
    
    $('form .submit').click(function (e) {
        $(this).parents('form').submit();
        e.preventDefault();
    });

    $.fn.uniformValidate = function () {
        $(this).find('input.invalid').removeClass('.invalid');

        $(this).find('input:visible, textarea:visible, select:visible').each(function () {
            $(this).focus();
            $(this).blur();
        });

        if ($(this).find('input.invalid:visible').length) {
            return false;
        }

        return true;
    }

    var tabContainers = $('div.tabs > div');
    tabContainers.hide().filter(':first').show();

    $(".ajax-request").click(function (e) {
        e.preventDefault();
        $.ajax({
            type: 'GET',
            url: $(this).attr('href'),
            cache: false,
            success: function (data) {
                $("#dialog").empty().html(data);
            }
        });
    });

    //$('input[type=text], input[type=tel]').livequery(function () {
      //  $(this).setMask();
    //});

    var requiredLabel = $('.required').parent().find('label');
    requiredLabel.each(function () {
        if (!$(this).hasClass("error"))
            $(this).text($(this).text() + " *");
    });

    jQuery.ajaxSettings.traditional = true;

    $(".ajax-modal").livequery(function () {
        $(this).click(function (e) {
            e.preventDefault();
            var address = $(this).attr("href");
            if (address != null && address != "") {
                $.get(address, function (data) {
                    if (data != false) {
                        if (data.match("^redirecionar")) {
                            var url = data.split('=')[1];
                            window.location.href = url;
                        }
                        $("#modal").empty().html(data);
                        $("#myModal").modal("show");
                    }
                });
            }
        });
    });

    $.fn.autoComplete = function (urlController, textProperty, valueProperty, objHidden, formattedProperty, extraParams) {
        $(this).complete(urlController, {
            multiple: false,
            matchContains: true,
            cacheLength: 1,
            mustMatch: true,
            extraParams: extraParams,
            matchCase: false,
            parse: function (data) {
                return $.map(data, function (row) {
                    if (data.length > 0) {
                        return {
                            data: row,
                            value: row[valueProperty].toString(),
                            result: row[textProperty]
                        }
                    }
                });
            },
            formatItem: function (row, i, max) {
                if (row[formattedProperty])
                    return row[formattedProperty];
                return row[textProperty];
            }
        });

        $(this).result(function (event, data, formatted) {
            if (data)
                objHidden.val(data[valueProperty]);
        });
    }

    $('.panel .tools .fa').livequery("click", function () {
        var el = $(this).parents(".panel").children(".panel-body");
        if ($(this).hasClass("fa-chevron-down")) {
            $(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
            el.slideUp(200);
        } else {
            $(this).removeClass("fa-chevron-up").addClass("fa-chevron-down");
            el.slideDown(200);
        }
    });

    $('.panel .tools .fa-times').livequery("click", function () {
        $(this).parents(".panel").parent().remove();
    });

    $('div.form-alterar-quantidade .diminuir').livequery("click", function () {
        var quantidade = parseInt($(this).parents("div.form-alterar-quantidade").find("input.form-control").val());
        if (quantidade > 0) {
            $(this).parents("div.form-alterar-quantidade").find("input.form-control").val(--quantidade);
            $(this).parents("div.form-alterar-quantidade").find("input.form-control").change();
        }
    });

    $('div.form-alterar-quantidade .aumentar').livequery("click", function () {
        var quantidade = parseInt($(this).parents("div.form-alterar-quantidade").find("input.form-control").val());
        $(this).parents("div.form-alterar-quantidade").find("input.form-control").val(++quantidade);
        $(this).parents("div.form-alterar-quantidade").find("input.form-control").change();
    });

    $("[help-info]").each(function (i, e) {
        $(e).after("<div class='help-tip'>?<p>" + $(e).attr("help-info") + "</p></div>");
    });

    $("[help-info-left]").each(function (i, e) {
        $(e).after("<div class='help-tip left'>?<p>" + $(e).attr("help-info-left") + "</p></div>");
    });
});