/* منو */
$(function () {
    var $window = $(window);
    if ($window.width() < 768) {
        $("#nonMobileSearch").remove();
        $(".searchform").addClass("active");
        $(".searchform").addClass("mobilesearch");
        $(".btnMenuSearch").css("height", "40px");
        $(".mobileInputGroup").css("direction", "RTL");
        $("#btnSearch").css("display", "block");
        $("#btnMenuTel").css("display", "block");
        //$(".searchform").css("display", "block");
    } else {
        $("#mobileSearch").remove();
        $(".searchform").css("display", "block");
    }
    $(document).on('click', '#btnSearch', function (event) {
        $(".searchform").css("display", "block");
    });
    function closeSearch() {
        var $form = $('.navbar-collapse form[role="search"].active')
        $form.find('input').val('');
        $form.removeClass('active');
    }
    // Show Search if form is not active // event.preventDefault() is important, this prevents the form from submitting
    $(document).on('click', '.navbar-collapse form[role="search"]:not(.active) button[type="submit"]', function (event) {
        event.preventDefault();
        var $form = $(this).closest('form'),
            $input = $form.find('input');
        $form.addClass('active');
        $input.focus();
    });
});
function closeSearch() {
    var $window = $(window);
    if ($window.width() < 768) {
        $(".searchform").css("display", "none");
    }
    var $form = $('.navbar-collapse form[role="search"].active')
    $form.find('input').val('');
    $form.removeClass('active');
}
function SearchAjaxSuccess(result) {
    closeSearch();
    if (result.status == "SearchContentRequired") {
        toastr.options = { "positionClass": "toast-top-center" }
        toastr.error('متن جستجو را وارد نمایید', 'جستجو', { timeOut: 5000 })
    } else if (result.status == "Success") {
        window.location.href = "/Search?q=" + result.title;
    }
}
function SearchFailureAjax() {
    closeSearch();
    toastr.options = { "positionClass": "toast-top-center" }
    toastr.error('متن جستجو را وارد نمایید', 'جستجو', { timeOut: 5000 })
}
/***********/
$(function () {
    $(window).scroll(function () {
        var a = $(window).scrollTop();
        var b = 10;
        if (a > b) {
            $("header.navbar").addClass("navbar-fixed-top");
        }
        else {
            $("header.navbar").removeClass("navbar-fixed-top");
        }
    });

    $(document).on('click', '#BtnSearch2', function (event) {
        $("#SearchSection").slideToggle(500);
    });
});

