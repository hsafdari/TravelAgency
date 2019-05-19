function success(data) {
    if (data == "duplicate") {
        //$("#popupsignupform").css("display", "none");
        //$("#popupresultdup").css("display", "inline");
        $('#newslettermodal').modal('hide');
        toastr.options = { "positionClass": "toast-top-center" }
        toastr.error('ایمیل شما قبلا در سامانه ثبت شده است', 'ثبت ایمیل', { timeOut: 5000 })
    }
    if (data == "success") {
        //$("#popupsignupform").css("display", "none");
        //$("#popupresult").css("display", "inline");
        $('#newslettermodal').modal('hide');
        toastr.options = { "positionClass": "toast-top-center" }
        toastr.success('ایمیل شما با موفقیت ثبت شد از شما متشکریم', 'ثبت ایمیل', { timeOut: 5000 })
    }
    if (data == "fail") {
        //$("#popupsignupform").css("display", "none");
        //$("#popupresultfail").css("display", "inline");
        alert("لطفا ایمیل را درست وارد کنید")
    }
}
function gotError() {
    alert("خطا در ثبت اطلاعات");
}

//function readCookie(name) {
//    var nameEQ = name + "=";
//    var ca = document.cookie.split(';');
//    for (var i = 0; i < ca.length; i++) {
//        var c = ca[i];
//        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
//        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
//    }
//    return null;
//}

////مقداردهی در کوکی برای عدم نمایش پاپ آپ
//function SetHideInCookie() {
//    setCookie("KeySafarNewsLetter", true, 30);
//}

////نشاندن در کوکی
//function setCookie(cname, cvalue, exdays) {
//    var d = new Date();
//    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
//    var expires = "expires=" + d.toUTCString();
//    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
//}

////خواندن از کوکی
//function getCookie(cname) {
//    var name = cname + "=";
//    var decodedCookie = decodeURIComponent(document.cookie);
//    var ca = decodedCookie.split(';');
//    for (var i = 0; i < ca.length; i++) {
//        var c = ca[i];
//        while (c.charAt(0) == ' ') {
//            c = c.substring(1);
//        }
//        if (c.indexOf(name) == 0) {
//            return c.substring(name.length, c.length);
//        }
//    }
//    return "";
//}

////var newsLetter = jQuery.noConflict();
//$(document).ready(function () {

//    var isNewsLetterHide = getCookie("KeySafarNewsLetter");
//    if (isNewsLetterHide != "true") {

//        var url = $('#newslettermodal').data('url');
//        $.get(url, function (data) {
//            $('#newsletterContainer').html(data);
//        });
//        $('#newslettermodal').modal('show');

//        setTimeout(function () { $('#newslettermodal').modal('hide') }, 30000);
//    }

//    $(".political-world").owlCarousel(
//      {
//          autoPlay: 3000, //Set AutoPlay to 3 seconds

//          items: 4,
//          itemsDesktop: [1199, 3],
//          itemsDesktopSmall: [979, 2],
//          navigation: true,
//          pagination: false
//      });
//});

$(document).ready(function () {
    $(".political-world").owlCarousel(
      {
          autoPlay: 3000, //Set AutoPlay to 3 seconds
          items: 4,
          itemsDesktop: [1199, 3],
          itemsDesktopSmall: [979, 2],
          navigation: true,
          pagination: false
      });
});




