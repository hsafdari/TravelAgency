$(document).ready(function () {
    if (tour) {
        tourDetailsLoader();
        autoRoll();
    }
});
$(window).on('resize', function () {
    $('body, html').scrollTop($(window).scrollTop() + 1)
});

//// General
hotelSelected = false
daysName = ['شنبه', 'یکشنبه', 'دوشنبه', 'سه‌شنبه', 'چهارشنبه', 'پنج‌شنبه', 'جمعه'];
monthName = ['فروردین', 'اردیبهشت', 'خرداد', 'تیر', 'مرداد', 'شهریور', 'مهر', 'آبان', 'آذر', 'دی', 'بهمن', 'اسفند'];
currency = '<i> تومان</i>';
sliderKeys = '<div class="btn next icon">&#xE315;</div><div class="btn prev icon">&#xE314</div>';
sliderCollapse = '<div class="btn fade-in collapse icon">&#xE5CD;</div>';
letsgo_desable = 'لطفا یکی از هتل‌های پیشنهادی را انتخاب کنید';
letsgo_enable = 'بزن بریم';
function removeRepeated(array) {
    var array = array.filter(function (elem, index, self) {
        return index == self.indexOf(elem);
    })
    return array.sort()
}
function firstDayonMonth(w, d) {
    for (var i = d; i >= 1; i--) {
        w = w - 1
        if (w === 0) { w = 7 }
        if (i === 1) {
            return w
        }
    }
}
//// Tour Details Loader
function tourDetailsLoader() {
    $('header').after('<div class="modal contactModal" role="dialog" tabindex="-1" aria-labelledby="mySmallModalLabel" style="direction:RTL;"> <div class="modal-dialog modal-lg" role="document"> <div class="modal-content"> <div class="modal-header"> <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button> <h4 class="modal-title" id="mySmallModalLabel">رزرو تور</h4> </div> <div class="modal-body" style="padding:50px;"> مسافر گرامی جهت رزرو تور ، لطفا با شماره های 02188341060 و 02188341070 تماس حاصل فرمایید </div> </div> </div> </div>');
    $('header').after('<div id="tour_details" class="clearfix"></div>');
    $('#tour_details').after('<div id="coverfooter"></div>');
    $('#tour_details').append('<h1 class="tour_title">' + tour.name + '</h1>');
    $('#tour_details').append('<div class="tour_images slider active auto_roll">' + sliderKeys + '</div>');
    for (var i in tour.images) {
        if (i == 0) {
            $('.tour_images').append('<div style="background: url(' + tour.images[i].ImageUrl + ')" class="thumbnail active th-' + i + '"></div>');
        } else {
            $('.tour_images').append('<div style="background: url(' + tour.images[i].ImageUrl + ')" class="thumbnail th-' + i + '"></div>');
        }
    }
    //$('#tour_details').append('<h2 class="tour_duration">'+ tour.duration +'</h2>');
    $('#tour_details').append('<div class="tour_description">' + tour.description + '</div>');
    tourSelector()
    $('#tour_details').append('<ul class="tour_navigator clearfix"><li class="flights btn">پرواز‌ها<li><li class="hotels btn">هتل‌ها<li><li class="itinerary btn">برنامه سفر<li><li class="calendar btn">تاریخ سفر</li></ul>');
    packagedetailsLoader(0)
    $('.tour_selector').append('<div class="tour_essentials"><h2 class="pannel_title">مدارک مورد نیاز</h2><ul></ul></div>')
    priceCalculator()
    essentialItems()
}

// Essential items
function essentialItems() {
    if (tour.Essentials.length > 0) {
        //list
        for (var i in tour.Essentials) {
            $('.tour_selector .tour_essentials ul').append('<li><i class="icon bullet">&#xE5CA;</i>' + tour.Essentials[i].toString() + '</li>')
        }
    }
}

//// Tour selector
function tourSelector() {
    $('#tour_details').append('<div class="tour_selector clearfix"></div>')
    //contact
    $('.tour_selector').append('<a href="tel:02188341070" class="contact section clearfix">تماس با راهنما:<i>021-88341070-1060<i></a>')
    //list
    $('.tour_selector').append('<div class="packages section clearfix"><h2 class="pannel_title">پکیج‌های موجود</h2></div>')
    for (var i in tour.packages) {
        $('.tour_selector .packages').append('<div class="package btn clearfix package-' + i + '"></div>')
        $('.tour_selector .packages .package-' + i).append('<div class="price"><span>شروع قیمت از:</span><span>' + tour.packages[i].lowestPrice + ' ' + currency + '</span></div>')
        $('.tour_selector .packages .package-' + i).append('<div class="date"><i class="icon bullet">&#xE8DF;</i></div>')
        $('.tour_selector .packages .package-' + i + ' .date').append('<span>' + tour.packages[i].DateTitle + '</span>')
        $('.tour_selector .packages .package-' + i).append('<div class="flights"></div>')
        for (var e in tour.packages[i].flights) {
            $('.tour_selector .packages .package-' + i + ' .flights').append('<span>' + tour.packages[i].flights[e].name + '</span>')
        }
        //$('.tour_selector .packages .package-' + i + ' .date').append('<span>' + tour.packages[i].start[1]  + '</span>' + '<span>' + monthName[tour.packages[i].start[0] - 1]  + '</span>' + '<span>تا</span><span>' + tour.packages[i].end[1]  + '</span>' + '<span>' + monthName[tour.packages[i].end[0] - 1]  + '</span>' )
    }
}

// package details loader
$(document).on('click', '.tour_selector .packages .package', function () {
    // debugger;
    var p = parseInt($(this).attr('class').split('package-').pop().split(' ').shift());
    packagedetailsLoader(p)
    priceCalculator()
    hotelSelected = false
});
function packagedetailsLoader(p) {
    //remove
    $('.tour_package_details').remove();
    $('.tour_selector .packages .package').removeClass('active')
    //
    $('.tour_selector .packages .package-' + p).addClass('active');
    //
    $('#tour_details').append('<div class="tour_package_details fade_in clearfix"></div>')
    //
    var scrl = $('.tour_package_details').offset().top - 100
    $('body, html').animate({ scrollTop: scrl }, 1000);
    //flights
    $('.tour_package_details').append('<div class="section_title"><i class="icon">&#xE539;</i>پرواز‌ها:</div>');
    $('.tour_package_details').append('<div class="flights active clearfix"></div>')
    $('.tour_package_details .flights').append('<div class="checker  icon">&#xE5CA;</div>')
    for (var i in tour.packages[p].flights) {
        $('.tour_package_details .flights').append('<div class="flight clearfix flight-' + i + '"></div>')
        $('.tour_package_details .flights .flight-' + i).append('<div class="flight_logo thumbnail" style="background: url(' + tour.packages[p].flights[i].logo + ')"></div>')
        $('.tour_package_details .flights .flight-' + i).append('<div class="flight_name">' + tour.packages[p].flights[i].name + '</div>')
        $('.tour_package_details .flights .flight-' + i).append('<div class="flight_airline">' + tour.packages[p].flights[i].airline + '</div>')
        var time = '<i class="icon">&#xE905;</i><span>' + tour.packages[p].flights[i].from + '</span><i class="icon">&#xE904;</i><span>' + tour.packages[p].flights[i].to + '</span><span> تاریخ پرواز:</span><span>' + tour.packages[p].flights[i].FlightDate + '</span>'
        if (tour.packages[p].flights[i].FlightNumber != "") {
            time = time + '<span> شماره پرواز:</span><span>' + tour.packages[p].flights[i].FlightNumber + '</span>';
        }
        if (tour.packages[p].flights[i].BaggageAmount != "") {
            time = time + '<span> بار مجاز:</span><span>' + tour.packages[p].flights[i].BaggageAmount + '</span>';
        }
        $('.tour_package_details .flights .flight-' + i).append('<div class="flight_time">' + time + '</div>')
    }
    //hotels
    $('.tour_package_details').append('<div class="section_title"><i class="icon">&#xE53A;</i>هتل‌ها:</div>');
    $('.tour_package_details').append('<div class="section_alert">لطفا یکی از هتل‌های پیشنهادی زیر را انتخاب فرمایید.</div>');
    for (var i in tour.packages[p].hotels) {
        $('.tour_package_details').append('<div class="hotels clearfix hotels-' + i + '"></div>')
        $('.tour_package_details .hotels-' + i).append('<div class="checker btn icon">&#xE5CA;</div>')
        for (var e in tour.packages[p].hotels[i].hotelInPackage) {
            var btnClassName = "btn";
            if (tour.packages[p].hotels[i].hotelInPackage[e].IsSummary == true) {
                btnClassName = "";
            }
            $('.tour_package_details .hotels-' + i).append('<div class="hotel ' + btnClassName + ' clearfix hotel-' + e + '"></div>')
            $('.tour_package_details .hotels-' + i + ' .hotel-' + e).append('<div class="hotel_logo" style="background:url(' + tour.packages[p].hotels[i].hotelInPackage[e].images[0].ImageUrl + ')"></div>')
            $('.tour_package_details .hotels-' + i + ' .hotel-' + e).append('<div class="hotel_name">' + tour.packages[p].hotels[i].hotelInPackage[e].hotel + '</div>')
            $('.tour_package_details .hotels-' + i + ' .hotel-' + e).append('<div class="hotel_stars icon"></div>')
            $('.tour_package_details .hotels-' + i + ' .hotel-' + e + ' .hotel_stars').append('<img class="pull-right" src="' + tour.packages[p].hotels[i].hotelInPackage[e].stars + '"/>')
            //for (var d = 0; d < tour.packages[p].hotels[i].hotelInPackage[e].stars; d++) {
            //  $('.tour_package_details .hotels-' + i + ' .hotel-' + e + ' .hotel_stars').append('<i>&#xE838;</i>')
            //}
            var htmlOfHotelService = '';
            if (tour.packages[p].hotels[i].hotelInPackage[e].service.length > 0) {
                htmlOfHotelService = '<span><span data-toggle="tooltip" data-placement="top" title="' + tour.packages[p].hotels[i].hotelInPackage[e].ServiceTooltip + '">' + tour.packages[p].hotels[i].hotelInPackage[e].service + '</span><i class="icon bulletService">&#xEB49;</i></span>';
                
            }
            $('.tour_package_details .hotels-' + i + ' .hotel-' + e).append('<div class="hotel_location"><i class="icon bullet">&#xE0C8;</i>' + tour.packages[p].hotels[i].hotelInPackage[e].location + htmlOfHotelService + '</div>')
            //$('.tour_package_details .hotels-' + i + ' .hotel-' + e).append('<div class="hotel_service "><i class="icon bullet">&#xEB49;</i>' + tour.packages[p].hotels[i].hotelInPackage[e].service + '</div>')
            $('.tour_package_details .hotels-' + i + ' .hotel-' + e).append('<div class="hotel_facilities "></div>')
            for (var d in tour.packages[p].hotels[i].hotelInPackage[e].facilities) {
                $('.tour_package_details .hotels-' + i + ' .hotel-' + e + ' .hotel_facilities').append('<span><i class="icon bullet">&#xE5CA;</i>' + tour.packages[p].hotels[i].hotelInPackage[e].facilities[d] + '</span>')
            }
            var readMore = '<a href="' + tour.packages[p].hotels[i].hotelInPackage[e].url + '" title="' + tour.packages[p].hotels[i].hotelInPackage[e].hotel + '" target="_blank">مطالعه بیشتر</a>'
            $('.tour_package_details .hotels-' + i + ' .hotel-' + e).append('<div class="hotel_description ">' + tour.packages[p].hotels[i].hotelInPackage[e].description + readMore + '</div>')

            $('.tour_package_details .hotels-' + i + ' .hotel-' + e).append('<div class="hotel_images active slider ">' + sliderKeys + '</div>')
            for (var d in tour.packages[p].hotels[i].hotelInPackage[e].images) {
                $('.tour_package_details .hotels-' + i + ' .hotel-' + e + ' .hotel_images').append('<div class="thumbnail th-' + d + '" style="background: url(' + tour.packages[p].hotels[i].hotelInPackage[e].images[d].ImageUrl + ')"></div>')
            }
            $('.tour_package_details .hotels-' + i + ' .hotel-' + e + ' .hotel_images .th-0').addClass('active')
        }
        $('.tour_package_details .hotels-' + i).append('<div class="hotel_prices clearfix"></div>')
        for (var e in tour.packages[p].hotels[i].price) {
            var currentprice = tour.packages[p].hotels[i].price[e].price;
            if (parseInt(currentprice) == 0) {
                $('.tour_package_details .hotels-' + i + ' .hotel_prices').append('<div class="price clearfix"><i class="icon bullet">&#xE227;</i><span>' + tour.packages[p].hotels[i].price[e].description + '</span><span>' + tour.packages[p].hotels[i].price[e].otherCurrencyPrice + tour.packages[p].hotels[i].price[e].otherCurrencyTitle + '</span></div>')
            }
            else if (tour.packages[p].hotels[i].price[e].otherCurrencyPrice != "" && parseInt(currentprice) > 0) {
                $('.tour_package_details .hotels-' + i + ' .hotel_prices').append('<div class="price clearfix"><i class="icon bullet">&#xE227;</i><span>' + tour.packages[p].hotels[i].price[e].description + '</span><span>' + tour.packages[p].hotels[i].price[e].price + currency + '+' + tour.packages[p].hotels[i].price[e].otherCurrencyPrice + tour.packages[p].hotels[i].price[e].otherCurrencyTitle + '</span></div>')
            }
            else if (parseInt(currentprice) > 0 && tour.packages[p].hotels[i].price[e].otherCurrencyPrice == "") {
                $('.tour_package_details .hotels-' + i + ' .hotel_prices').append('<div class="price clearfix"><i class="icon bullet">&#xE227;</i><span>' + tour.packages[p].hotels[i].price[e].description + '</span><span>' + tour.packages[p].hotels[i].price[e].price + currency + '</span></div>')
            }

        }
    }
    //itinerary
    if (tour.itinerary.length > 0) {

        $('.tour_package_details').append('<div class="section_title"><i class="icon">&#xE87A;</i>برنامه سفر:</div>');
        $('.tour_package_details').append('<div class="itineraries active clearfix"></div>')
        $('.tour_package_details .itineraries').append('<div class="checker  icon">&#xE5CA;</div>')
        for (var i in tour.itinerary) {
            $('.tour_package_details .itineraries').append('<div class="itinerary clearfix itinerary-' + i + '"></div>')
            $('.tour_package_details .itinerary-' + i).append('<div class="itinerary_name">' + tour.itinerary[i].name + '</div>')
            $('.tour_package_details .itinerary-' + i).append('<div class="itinerary_slides slider active">' + sliderKeys + '</div>')
            for (var e in tour.itinerary[i].slides) {
                $('.tour_package_details .itinerary-' + i + ' .itinerary_slides').append('<div class="itinerary_slide thumbnail th-' + e + '"></div>')
                $('.tour_package_details .itinerary-' + i + ' .itinerary_slides .th-' + e).append('<div class="itinerary_description"><h3>' + tour.itinerary[i].slides[e].ActivityTitle + '</h3><br />' + tour.itinerary[i].slides[e].description + '</div>')
                $('.tour_package_details .itinerary-' + i + ' .itinerary_slides .th-' + e).append('<div style="background:url(' + tour.itinerary[i].slides[e].image + ')" class="itinerary_image"></div>')
            }
        }
        $('.itinerary_slides .th-0').each(function () {
            $(this).addClass('active')
        });
    }

    //calendar
    $('.tour_package_details').append('<div class="section_title"><i class="icon">&#xE8DF;</i>تاریخ سفر:</div>');
    $('.tour_package_details').append('<div class="calendar active clearfix"></div>');
    var monthes = [];
    for (var i in tour.packages[0].start) {
        monthes.push(tour.packages[0].start[i][0])
    }
    var monthes = removeRepeated(monthes)
    for (var i in monthes) {
        if (monthes[i] <= 6) { var numOfDays = 31; }
        if (monthes[i] > 6) { var numOfDays = 30; }
        if (monthes[i] > 6 && monthes[i] === 12) { var numOfDays = 29; }
        //
        $('.tour_package_details .calendar').append('<div class="month clearfix month-' + monthes[i] + '"></div>')
        $('.tour_package_details .month-' + monthes[i]).append('<div class="month_name">' + monthName[monthes[i] - 1] + '</div>')
        for (var e = 1; e <= numOfDays; e++) {
            $('.tour_package_details .month-' + monthes[i]).append('<div class="day day-' + e + '">' + e + '</div>')
        }
        //
        //for (var e in tour.packages[p].start) {
        //    debugger;
        //    if (tour.packages[p].start[e][0] == monthes[i]) {
        //        var fd = firstDayonMonth(tour.packages[p].start[e][2], tour.packages[p].start[e][1]);
        //        $('.tour_package_details .month-' + monthes[i] + ' .day-1').addClass('firstday-' + fd);
        //        //
        //        $('.tour_package_details .month-' + monthes[i] + ' .day-' + tour.packages[p].start[e][0]).addClass('active');
        //    }
        //}
        //$('.tour_package_details .month-' + tour.packages[p].start[0][0] + ' .day-' + tour.packages[p].start[0][1]).addClass('selected btn anim-pulse');
        ////for (var i = 0; i < tour.packages[0].start; i++) {
        ////$('.tour_package_details .month-' + tour.packages[p].start[e][0] + ' .day-' + tour.packages[p].start[e][1]).addClass('selected btn anim-pulse');
        ////}
        for (var e in tour.packages[p].start) {
            if (tour.packages[p].start[e][0] == monthes[i]) {
                var fd = firstDayonMonth(tour.packages[p].start[e][2], tour.packages[p].start[e][1])
                $('.tour_package_details .month-' + monthes[i] + ' .day-1').addClass('firstday-' + fd)
                //
                $('.tour_package_details .month-' + monthes[i] + ' .day-' + tour.packages[p].start[e][1]).addClass('active selected btn anim-pulse')
            }
        }
        // $('.tour_package_details .month-' + tour.packages[p].start[0][0] + ' .day-' + tour.packages[p].start[0][1]).addClass('selected btn anim-pulse')
    }
}
// slider
$(document).on('click', '.hotel.btn', function () {
    $(this).removeClass('btn').addClass('active')
    $(this).children('.hotel_name').prepend(sliderCollapse)
})
$(document).on('click', '.hotel.active .collapse', function () {
    $(this).closest('.hotel').removeClass('active').addClass('btn')
    $(this).remove()
})
$(document).on('click', '.next', function () {
    var current = parseInt($(this).closest('.slider').children('.thumbnail.active').attr('class').split('th-').pop().split(' ').shift())
    var length = $(this).closest('.slider').children('.thumbnail').length
    if (current < length - 1) {
        var next = current + 1
    } else {
        var next = 0
    }
    $(this).closest('.slider').children('.thumbnail').removeClass('active');
    $(this).closest('.slider').children('.thumbnail.th-' + next).addClass('active');
})
$(document).on('click', '.prev', function () {
    var current = parseInt($(this).closest('.slider').children('.thumbnail.active').attr('class').split('th-').pop().split(' ').shift())
    var length = $(this).closest('.slider').children('.thumbnail').length
    if (current === 0) {
        var prev = length - 1
    } else {
        var prev = current - 1
    }
    $(this).closest('.slider').children('.thumbnail').removeClass('active');
    $(this).closest('.slider').children('.thumbnail.th-' + prev).addClass('active');
})
//
$(document).on('click', '.slider.btn', function () {
    $(this).addClass('active').removeClass('btn');
})
$(document).on('click', '.slider .btn.collapse', function () {
    $(this).closest('.slider').addClass('btn').removeClass('active');
})
//auto roll slider
function autoRoll() {
    $('.auto_roll .time').remove()
    $('.auto_roll').append('<div class="auto_roll time"></div>')
    slider_timer = setTimeout(function () {
        //
        var current = parseInt($('.auto_roll').closest('.slider').children('.thumbnail.active').attr('class').split('th-').pop().split(' ').shift())
        var length = $('.auto_roll').closest('.slider').children('.thumbnail').length
        if (current < length - 1) {
            var next = current + 1
        } else {
            var next = 0
        }
        $('.auto_roll').closest('.slider').children('.thumbnail').removeClass('active');
        $('.auto_roll').closest('.slider').children('.thumbnail.th-' + next).addClass('active');
        //
        $('.auto_roll .time').remove()
        autoRoll()

    }, 5000);
}
$(document).on('click', '.auto_roll .next , .auto_roll .prev', function () {
    clearTimeout(slider_timer);
    autoRoll()
})
// Lets Go
$(document).on('click', '.calendar .day.selected', function () {
    if (hotelSelected) {
        var sm = parseInt($(this).closest('.month').attr('class').split('month-').pop().split(' ').shift())
        var sd = parseInt($(this).attr('class').split('day-').pop().split(' ').shift())
        var p = tour.packages[parseInt($('.packages .package.active').attr('class').split('package-').pop().split(' ').shift())].Id
        var h = parseInt($('.hotels.active').attr('class').split('hotels-').pop().split(' ').shift())
        //alert('month: ' + sm + ' day: ' + sd + ' - package: ' + p + ' -  hotels: ' + h);
        $('.contactModal').modal('toggle');
        //alert('مسافر گرامی جهت رزرو تور ، لطفا با شماره های 02188341060 و 02188341070 تماس حاصل فرمایید.');
    } else {
        var scrl = $('.tour_package_details .hotels ').offset().top - 109
        $('body, html').animate({ scrollTop: scrl }, 1000);
        $('.section_alert').addClass('anim-alert')
        setTimeout(function () {
            $('.section_alert').removeClass('anim-alert')
        }, 3000);
    }
})
//nav bar
$(document).on('scroll', function () {
    stikynavbar();
})
$(document).on('click', '.tour_navigator .flights', function () {
    var scrl = $('.tour_package_details').offset().top - 60
    $('body, html').animate({ scrollTop: scrl }, 1000);
})
$(document).on('click', '.tour_navigator .hotels', function () {
    var scrl = $('.tour_package_details .hotels ').offset().top - 109
    $('body, html').animate({ scrollTop: scrl }, 1000);
})
$(document).on('click', '.tour_navigator .itinerary', function () {
    var scrl = $('.tour_package_details .itineraries ').offset().top - 109
    $('body, html').animate({ scrollTop: scrl }, 1000);
})
$(document).on('click', '.tour_navigator .calendar, .calc_lowestPrice .book', function () {
    var scrl = $('.tour_package_details .calendar ').offset().top - 109
    $('body, html').animate({ scrollTop: scrl }, 1000);
})
//
function stikynavbar() {
    if ($(window).scrollTop() > $('.tour_selector').offset().top) {
        $('.tour_selector .contact').addClass('sticky').css({
            'left': $('#tour_details').offset().right,
            'width': (($('#tour_details').width() / 100) * 30) - 5
        });
    } else {
        $('.tour_selector .contact').removeClass('sticky').removeAttr('style')
    }
    if ($(window).scrollTop() > $('.tour_package_details').offset().top - 60) {
        $('.tour_navigator').addClass('sticky').css({
            'left': $('#tour_details').offset().left,
            'width': (($('#tour_details').width() / 100) * 70) - 5
        });
    } if ($(window).scrollTop() < $('.tour_package_details').offset().top) {
        $('.tour_navigator').removeClass('sticky').removeAttr('style')
    }
    if ($(window).scrollTop() > $('.tour_essentials').offset().top + $('.tour_essentials').height() - 10) {
        $('.calc_lowestPrice').addClass('sticky').css({
            'left': $('#tour_details').offset().right,
            'width': (($('#tour_details').width() / 100) * 30) - 5
        });
    } else {
        $('.calc_lowestPrice').removeClass('sticky').removeAttr('style')
    }

    if ($(window).scrollTop() < $('.tour_package_details .hotels').offset().top) {
        $('.tour_navigator *').removeClass('active')
        $('.tour_navigator .flights').addClass('active')
    }
    if ($(window).scrollTop() > ($('.tour_package_details .hotels').offset().top - 110)) {
        $('.tour_navigator *').removeClass('active')
        $('.tour_navigator .hotels').addClass('active')
    }
    if (tour.itinerary.length > 0) {
        //در صورت وجود برنامه سفر
        if ($(window).scrollTop() > ($('.tour_package_details .itineraries').offset().top - 110)) {
            $('.tour_navigator *').removeClass('active')
            $('.tour_navigator .itinerary').addClass('active')
        }
        if ($(window).scrollTop() > ($('.tour_package_details .calendar').offset().top - 110)) {
            $('.tour_navigator *').removeClass('active')
            $('.tour_navigator .calendar').addClass('active')
        }
    } else {
        //در صورت عــدم وجود برنامه سفر
        if ($(window).scrollTop() > ($('.tour_package_details .calendar').offset().top - 220)) {
            $('.tour_navigator *').removeClass('active')
            $('.tour_navigator .calendar').addClass('active')
        }
    }
}
//price calculator

function priceCalculator() {
    $('.calc_lowestPrice').remove();
    $('<div class="calc_lowestPrice fade_in"><span class="price"></span><span class="book btn">رزرو</span></div>').insertAfter('.tour_essentials')
    var p = parseInt($('.packages .package.active').attr('class').split('package-').pop().split(' ').shift())
    if (hotelSelected) {
        var h = parseInt($('.tour_package_details  .hotels.active').attr('class').split('hotels-').pop().split(' ').shift())
        var lp = []
        for (var i in tour.packages[p].hotels) {
            for (var e in tour.packages[p].hotels[i].price) {
                lp.push(tour.packages[p].hotels[i].price[e].price)
            }
        }
        var lp = removeRepeated(lp)
        var lp = lp.sort()
        $('.calc_lowestPrice .price').html('<i>شروع قیمت از: ' + lp[0] + '</i>' + currency)

    } else {
        $('.calc_lowestPrice .price').html('<i>شروع قیمت از: ' + tour.packages[p].lowestPrice + '</i>' + currency)

    }
}
//hotel selector

$(document).on('click', '.checker.btn', function () {
    $('.tour_package_details  .hotels').removeClass('active')
    $(this).closest('.hotels').addClass('active')
    hotelSelected = true
    priceCalculator()
    stikynavbar();
})
$(document).on('click', '.active .checker', function () {
    $('.tour_package_details  .hotels').removeClass('active')
    hotelSelected = false
    priceCalculator()
    stikynavbar();

})

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});



//modenizer - TouchEvents
!function (e, n, t) { function o(e, n) { return typeof e === n } function s() { var e, n, t, s, a, i, r; for (var l in c) if (c.hasOwnProperty(l)) { if (e = [], n = c[l], n.name && (e.push(n.name.toLowerCase()), n.options && n.options.aliases && n.options.aliases.length)) for (t = 0; t < n.options.aliases.length; t++) e.push(n.options.aliases[t].toLowerCase()); for (s = o(n.fn, "function") ? n.fn() : n.fn, a = 0; a < e.length; a++) i = e[a], r = i.split("."), 1 === r.length ? Modernizr[r[0]] = s : (!Modernizr[r[0]] || Modernizr[r[0]] instanceof Boolean || (Modernizr[r[0]] = new Boolean(Modernizr[r[0]])), Modernizr[r[0]][r[1]] = s), f.push((s ? "" : "no-") + r.join("-")) } } function a(e) { var n = u.className, t = Modernizr._config.classPrefix || ""; if (p && (n = n.baseVal), Modernizr._config.enableJSClass) { var o = new RegExp("(^|\\s)" + t + "no-js(\\s|$)"); n = n.replace(o, "$1" + t + "js$2") } Modernizr._config.enableClasses && (n += " " + t + e.join(" " + t), p ? u.className.baseVal = n : u.className = n) } function i() { return "function" != typeof n.createElement ? n.createElement(arguments[0]) : p ? n.createElementNS.call(n, "http://www.w3.org/2000/svg", arguments[0]) : n.createElement.apply(n, arguments) } function r() { var e = n.body; return e || (e = i(p ? "svg" : "body"), e.fake = !0), e } function l(e, t, o, s) { var a, l, f, c, d = "modernizr", p = i("div"), h = r(); if (parseInt(o, 10)) for (; o--;) f = i("div"), f.id = s ? s[o] : d + (o + 1), p.appendChild(f); return a = i("style"), a.type = "text/css", a.id = "s" + d, (h.fake ? h : p).appendChild(a), h.appendChild(p), a.styleSheet ? a.styleSheet.cssText = e : a.appendChild(n.createTextNode(e)), p.id = d, h.fake && (h.style.background = "", h.style.overflow = "hidden", c = u.style.overflow, u.style.overflow = "hidden", u.appendChild(h)), l = t(p, e), h.fake ? (h.parentNode.removeChild(h), u.style.overflow = c, u.offsetHeight) : p.parentNode.removeChild(p), !!l } var f = [], c = [], d = { _version: "3.5.0", _config: { classPrefix: "", enableClasses: !0, enableJSClass: !0, usePrefixes: !0 }, _q: [], on: function (e, n) { var t = this; setTimeout(function () { n(t[e]) }, 0) }, addTest: function (e, n, t) { c.push({ name: e, fn: n, options: t }) }, addAsyncTest: function (e) { c.push({ name: null, fn: e }) } }, Modernizr = function () { }; Modernizr.prototype = d, Modernizr = new Modernizr; var u = n.documentElement, p = "svg" === u.nodeName.toLowerCase(), h = d._config.usePrefixes ? " -webkit- -moz- -o- -ms- ".split(" ") : ["", ""]; d._prefixes = h; var m = d.testStyles = l; Modernizr.addTest("touchevents", function () { var t; if ("ontouchstart" in e || e.DocumentTouch && n instanceof DocumentTouch) t = !0; else { var o = ["@media (", h.join("touch-enabled),("), "heartz", ")", "{#modernizr{top:9px;position:absolute}}"].join(""); m(o, function (e) { t = 9 === e.offsetTop }) } return t }), s(), a(f), delete d.addTest, delete d.addAsyncTest; for (var v = 0; v < Modernizr._q.length; v++) Modernizr._q[v](); e.Modernizr = Modernizr }(window, document);
