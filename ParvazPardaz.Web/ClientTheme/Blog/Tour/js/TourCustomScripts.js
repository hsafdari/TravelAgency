function HideLoginModal() {
    $('body').removeClass('freez');
    $('.fullframe').css("opacity", "0");
    $('.fullframe').css("display", "none");
    $('.dim').css("opacity", "0");
    $('.dim').css("display", "none");
    $('.modal .wrapper').removeClass('signup').removeClass('passrestore').addClass('signin');
}

function ShowLoginModal() {
    $.ajax({
        type: "POST",
        url: "/IsLoggedIn",
        success: function (result) {
            if (result == false) {
                $('body').addClass('freez');
                $('.fullframe').css("opacity", "1");
                $('.fullframe').css("display", "block");
                $('.dim').css("opacity", "1");
                $('.dim').css("display", "block");
            } else {
                if (window.location.pathname == "/TourSearch") {
                    $("#frm-reserve").submit();
                }
            }
        },
        error: function (textStatus, errorThrown) {
            $('body').addClass('freez');
            $('.fullframe').css("opacity", "1");
            $('.fullframe').css("display", "block");
            $('.dim').css("opacity", "1");
            $('.dim').css("display", "block");
            $('.modal .wrapper').removeClass('signup').removeClass('passrestore').addClass('signin');
        }
    });

}

$(document).on('click', function (e) {
    if ($('.otherPassengerSection').length && !$(e.target).closest('.otherPassengerSection').length && !$(e.target).closest('[name=op]').length) {
        $('.otherPassengerSection').css("display", "none");
    }
})



/********** مواردی که باید از جاوااسکریپت اصلی حذف شن *********/
$(document).on('click', '.datepicker', function (e) {
    if ($(this).hasClass('g')) {
        calendar_g(true, 0, this)
        $(this).addClass('focus')
    } else {
        calendar_j(true, 0, this)
        $(this).addClass('focus')
    }
})
$(document).on('click', '.profile-modal-toggle', function (e) {
    e.preventDefault();
    $('.profile-dropdown').toggleClass('active')
    ShowLoginModal();
})
$(document).on('click', ' .dim', function (e) {
    $('body').removeClass('freez')
    HideLoginModal();
})
$(document).on('click', '.updown *', function (e) {
    e.preventDefault();
    if ($(this).attr('name') === 'up') {
        var t = parseInt($(this).parent().eq(0).prev('.number').val()) + 1
        var t = isNaN(t) ? 1 : t
        var max = parseInt($(this).parent().eq(0).prev('.number').attr('max'))
        var val = isNaN(max) || (t < max) ? t : max
        $(this).parent().eq(0).prev('.number').val(val)
        //محاسبه قیمت کل
        CalculateTotalPrice();
    } else {
        var t = parseInt($(this).parent().eq(0).prev('.number').val()) - 1
        var t = isNaN(t) ? 0 : t
        var min = parseInt($(this).parent().eq(0).prev('.number').attr('min'))
        var val = isNaN(min) || (t > min) ? t : min
        $(this).parent().eq(0).prev('.number').val(val)
        //محاسبه قیمت کل
        CalculateTotalPrice();
    }
})
/********** مواردی که باید از جاوااسکریپت اصلی حذف شن *********/
