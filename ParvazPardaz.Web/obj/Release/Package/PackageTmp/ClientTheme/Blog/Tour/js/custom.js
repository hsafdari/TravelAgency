jQuery(document).ready(function($) {
  slider(0);
  priceStick();
  profileTabs()
  searchTabs()
  homeSlider()
  today()
  tabs()
  checkboxReplacer()
  inputNumber()
  heightSet()
});
$(document).scroll(function(e){
  priceStick();
})
$(document).on('mousewheel DOMMouseScroll','.main.tour-hotel .details .slider, .scrollLeft',function(e){
  if ( this.scrollWidth > this.clientWidth) {
    e.preventDefault();
    if (navigator.userAgent.toLowerCase().indexOf('firefox') > -1) {
      delta = (e.originalEvent.detail > 0) ? 20 : -20 ;
    } else {
      delta = (e.originalEvent.wheelDelta < 0) ? 20 : -20 ;
    }
    this.scrollLeft -= (delta );
  }
})
$(window).on('hashchange',function(){
  profileTabs()
})
$(window).on('resize',function(){
  heightSet()
})
var _nexttimer = "";
$(document).on('click', 'ul.slider-details .dots li,.home-display button', function(e) {
    clearTimeout(_nexttimer);
    var active = $('ul.slider-details .dots li.active').index();
    var next = 0;
    if ($(this).prop('name') == 'next') {
        var dir = 1;
        var length = $('ul.slider-details .dots li').length;
        next = (active + dir + 1)

        if (next <= 0) {
            next = length
        }
        if (next >= length + 1) {
            next = 1
        }
    } else {
        next = $(this).index() + 1;
    }

    $('ul.slider li, ul.slider-details > li, ul.slider-details .dots li').removeClass('active');
    $('ul.slider li:nth-child(' + next + '), ul.slider-details > li:nth-child(' + next + '), ul.slider-details .dots li:nth-child(' + next + ')').addClass('active');
    _nexttimer = setTimeout(function () {
        $('.home-display button[name=next]').click()
    }, 5000)
})
nexttimer =""
$(document).on('click','.main.slider button', function(){
  clearTimeout(nexttimer)
  $('.main.slider div').remove()
  $('.main.slider').append('<div></div>')
  var active = $('.main.slider a.active, .fullframe li.active ').index()
  if ($(this).prop('name') == 'next') {
    var dir = 1;
  }else {
    var dir = -1;
  }
  var length = $('.slider').children('a,  .fullframe li').length
  var next = (active + dir +1)

  if (next <= 0) {
    var next = length
  }
  if (next >= length + 1) {
    var next = 1
  }
  $('.main.slider a.active, .main.slider li.active ').removeClass('active');
  $('.main.slider a:nth-child(' + next + '), .main.slider li:nth-of-type(' + next + ')').addClass('active');
  nexttimer = setTimeout(function () {
    $('.main.slider button[name=next]').click()
  }, 10000);
})
$(document).on('click','.main.slider li', function(){
  clearTimeout(nexttimer)
  $('.main.slider div').remove()
  $('.main.slider').append('<div></div>')
  var next = $(this).index() + 1
  $('.main.slider a.active, .main.slider li.active ').removeClass('active');
  $('.main.slider a:nth-child(' + next + '), .main.slider li:nth-child(' + next + ')').addClass('active');
  nexttimer = setTimeout(function () {
    $('.main.slider button[name=next]').click()
  }, 10000);
})
$(document).on('click','.tour-description button[name=toggle]',function(){
  $(this).toggleClass("active")
  $('.tour-description').toggleClass("active")
})
$(document).on('click','.tour-hotel span[name=toggle]',function(){
  $(this).toggleClass("active")
  $(this).closest('label').eq(0).toggleClass('active').children('.details').eq(0).toggleClass("active")
})
$(document).on('click',' .foldingCards dt',function(e){
  if ($(this).next('dd').length) {
    $(this).toggleClass('active')
    $(this).children('.btn-5').toggleClass('active')
    $(this).next('dd').toggleClass('active')
  }
})
profileDropdownToggle = false
$(document).on('click',' .profile-dropdown-toggle > *',function(e){
  e.preventDefault();
  $('.profile-dropdown').toggleClass('active')
  profileDropdownToggle = (profileDropdownToggle) ? false : true
})
$(document).on('click','.datepicker', function(e){
  if($(this).hasClass('g')){
    calendar_g(true, 0, this)
    $(this).addClass('focus')
  }else {
    calendar_j(true, 0, this)
    $(this).addClass('focus')
  }
})
$(document).on('click', 'body .dp-body .dp-m i', function(e) {
  var selected = $(this).attr('d').split(',')
  $('.dp-body').remove()
  $('.datepicker.focus').parent().children('span:nth-of-type(1)').removeClass().addClass($('.datepicker.focus').hasClass('g') ? 'en' : 'fa').html(selected[2])
  $('.datepicker.focus').parent().children('span:nth-of-type(2)').removeClass().addClass($('.datepicker.focus').hasClass('g') ? 'en' : 'fa').html(selected[3])
  $('.datepicker.focus').removeClass('focus').val( selected )
  $('input:hidden[name=FlightDate]').val(selected[0] + "/" + selected[1] + "/" + selected[2]);
})
$(document).on('click', '.tab-bar li', function(e) {
  $(this).closest('.tab-bar').children().removeClass('active')
  $(this).addClass('active')
  $(this).closest('.tab-bar').parent().children('.tab').removeClass('active')
  $(this).closest('.tab-bar').parent().children('.tab:nth-of-type(' + ($(this).index() + 1) + ')').addClass('active')
})
$(document).on('click' ,function(e){
  if (profileDropdownToggle && !$(e.target).eq(0).hasClass('profile-dropdown-toggle')) {
    $('.profile-dropdown-toggle').click()
  }
  if ($('.dp-body').length && !$(e.target).closest('.dp-body').length && !$(e.target).closest('[name=go-date]').length && !$(e.target).closest('.datepicker').length) {
    $('.dp-body').remove()
    $('.datepicker').removeClass('focus')
  }
  if ($('.otherPassengers').length && !$(e.target).closest('.otherPassengers').length && !$(e.target).closest('[name=op]').length) {
    $('.otherPassengers').remove()
  }
})
$(document).on('click', '.sort-bar > *', function(){
  var target = $(this).closest('.sort-bar').attr('sorter');
  var attribute = $(this).attr('sorter');
  var nodes = [];
  var order = $(this).hasClass('sort-ascend') ? false :  !$(this).hasClass(/sort-^/) || $(this).hasClass('sort-descend') ? true : false;
  $(this).closest('.sort-bar').children().removeClass('sort-descend').removeClass('sort-ascend');
  $(this).addClass(order ?  'sort-ascend' : 'sort-descend')
  $('[sortable-target="' + target + '"] [sortable-' + attribute + ']').each(function(i,e) {
    nodes.push({val: isNaN(parseInt($(this).attr('sortable-' + attribute))) ? $(this).attr('sortable-' + attribute) :  parseInt($(this).attr('sortable-' + attribute)), node: $(this)[0]});
  });
  nodes = sortByKey(nodes, 'val');
  nodes = order ? nodes : nodes.reverse()
  $('[sortable-target="' + target + '"] [sortable-' + attribute + ']').remove();
  for (var i in nodes) {
    $('[sortable-target="' + target + '"]').append(nodes[i].node)
  }
})
$(document).on('touchstart ',function(e){
  touchS = e.touches[0].clientX
})
$(document).on('touchmove ',function(e){

  touchM = e.touches[0].clientX
  if ($(e.target).eq(0).closest('.cards').eq(0).length > 0) {
    var x = (touchM - touchS) /5
    $(e.target).eq(0).closest('.cards').eq(0).children('li').css({
      'transform': 'translateX('+ x + 'px)',
    })
  }
})
$(document).on('touchend ',function(e){
  if ($(e.target).eq(0).closest('.cards').eq(0).length > 0) {
    if (touchM > touchS) {
      $(e.target).eq(0).closest('.roll').children('[name="next"]').click()
    }else {
      $(e.target).eq(0).closest('.roll').children('[name="prev"]').click()
    }
    $(e.target).eq(0).closest('.cards').eq(0).children('li').css({
      'transform': 'translateX(0px)',
    })
  }
})

function pdfIt(element) {
  var pdf = new jsPDF('p', 'mm', [$(element).innerWidth(), $(element).innerHeight()]);
  html2canvas($(element)[0], {
    width: $(element).innerWidth(),
    windowWidth: $(element).innerWidth(),
    scrollY: true,
    windowHeight:$(element).innerHeight(),
    allowTaint: true,
    logging:false
  }).then(function(canvas){
    pdf.addImage(canvas.toDataURL('image/jpeg', 1.0), 'JPEG', 0,0, $(element).innerWidth(), $(element).innerHeight());
    pdf.viewerPreferences({
      'CenterWindow':true,
      'PickTrayByPDFSize':true
    })
    pdf.setDisplayMode('fullwidth', 'single', 'UseThumbs')
    var dt = new DateExtractor(new Date(), true)
    var dt = `(${dt.y}-${dt.m}-${dt.d})`
    pdf.output('save', 'voucher' + dt + '.pdf')
  })
}
function today() {
  if ($('.datepicker').length) {
    var flightDateElement = $('input:hidden[name=FlightDate]');
    if (flightDateElement.length && flightDateElement.val() != undefined && flightDateElement.val() != '') {
      var g = (flightDateElement.next().val() == 'georgian' ? true : false);
      var selectedDate = flightDateElement.val().split('/');
      var fa_m = ['فروردین','اردیبهشت','خرداد','تیر','مرداد','شهریور','مهر','آبان','آذر','دی','بهمن','اسفند']
      var selectedMonth = fa_m[parseInt(selectedDate[1])-1];
      $('.datepicker').val( [selectedDate[0], selectedDate[1], selectedDate[2],selectedMonth].toString() )
      $('[name=go-date] span:nth-of-type(1)').removeClass().addClass(g ? 'en' : 'fa').html(selectedDate[2])
      $('[name=go-date] span:nth-of-type(2)').removeClass().addClass(g ? 'en' : 'fa').html(selectedMonth)
    }else {
      var j = ($('.datepicker').hasClass('g') ? false : true)
      var today = new DateExtractor(new Date(), j);
      $('.datepicker').val( [today.y, today.m, today.d,today.mn].toString() )
      $('[name=go-date] span:nth-of-type(1)').removeClass().addClass($('.datepicker').hasClass('g') ? 'en' : 'fa').html(today.d)
      $('[name=go-date] span:nth-of-type(2)').removeClass().addClass($('.datepicker').hasClass('g') ? 'en' : 'fa').html(today.mn)
    }
  }
}
function calendar_g(autoDistance, distance, trigger) {
  var distance = distance < 0 ? 0 : distance
  var d = new Date()
  var picker = $(trigger).val().split(',');
  if (autoDistance) {
    var picker = [parseInt(picker[0]),parseInt(picker[1] - 1),parseInt(picker[2])]
    var firstDate = new Date(picker[0],picker[1],picker[2]);
    var distance = Math.round(((picker[0] - d.getFullYear()) * 12) + (picker[1] - (d.getMonth() + 1)))
  }
  var m1 = new DateExtractor(new Date(d.getFullYear(),d.getMonth() + 0 + distance, d.getDay()), false);
  var m2 = new DateExtractor(new Date(d.getFullYear(),d.getMonth() + 1 + distance, d.getDay()), false);
  var today = new DateExtractor(d, false);
  var pos = $(trigger).offset()
  var m1Days = '';
  var m2Days = '';
  var type = 'en'
  for (var i = 1; i <= m1.l; i++) {
    m1Days += '<i d="' + m1.y + ',' + m1.m + ',' + i + ',' + m1.mn + '">' + i + '</i>'
  }
  for (var i = 1; i <= m2.l; i++) {
    m2Days += '<i d="' + m2.y + ',' + m2.m + ',' + i + ',' + m2.mn + '">' + i + '</i>'
  }
  var template = `
  <div class="dp-body ` + type + `" style="top:` + pos.top + `px; left:` + (pos.left - 350) +`px;">
  <div class="dp-nav">
  <button class="btn-3" type="button" name="prev" onclick=" var distance='` + distance + `'; calendar_g(false, parseInt(distance) - 2, $('.datepicker.focus'));"></button>
  <span>` + m1.y +'<br>'+ m1.mn + `</span>
  <span>` + m2.y +'<br>'+ m2.mn + `</span>
  <button class="btn-3" type="button" name="next"  onclick=" var distance='` + distance + `'; calendar_g(false, parseInt(distance) + 2, $('.datepicker.focus'));"></button>
  </div>
  <div class="dp-days"><i>Sa</i><i>Su</i><i>Mo</i><i>Tu</i><i>We</i><i>Th</i><i>Fr</i></div>
  <div class="dp-days"><i>Sa</i><i>Su</i><i>Mo</i><i>Tu</i><i>We</i><i>Th</i><i>Fr</i></div>
  <div class="dp-m dp-m1">` + m1Days + `</div>
  <div class="dp-m dp-m2">` + m2Days + `</div>
  <div class="dp-toolbox">
  <!--<button class="btn-1 " type="button" name="today-en" onclick="calendar_g(false, 0, $('.datepicker.focus'));">امروز</button>-->
  <button class="btn-1" type="button" name="jalali" onclick="$('.datepicker').removeClass('g').addClass('j'); calendar_j(false, 0, $('.datepicker.focus'));">تقویم شمسی</button>
  </div>
  </div>
  `
  $('.dp-body').remove()
  $('body').append(template)
  if ($('.dp-body').offset().left < 10) {
    $('.dp-body').css({
      'left':  10
    })
  }
  $('body .dp-body .dp-m i[d^="'+ [today.y,today.m,today.d, today.mn].toString() + '"]').addClass('today')
  for (var i = today.d -1; i > 0; i--) {
    $('body .dp-body .dp-m i[d^="'+ [today.y,today.m,i,today.mn].toString() + '"]').addClass('disable')
  }
  $('body .dp-body .dp-m1 i:nth-child(1)').attr('style','grid-column-start:' + m1.fd)
  $('body .dp-body .dp-m2 i:nth-child(1)').attr('style','grid-column-start:' + m2.fd)
  $('body .dp-body .dp-m i[d^="'+ [picker[0],parseInt(picker[1] +1),picker[2],picker[3]].toString() + '"]').addClass('active')
  $('input:hidden[name=Calendertype]').val('gregorian');
}
function calendar_j(autoDistance, distance, trigger) {
  var distance = distance < 0 ? 0 : distance
  var t = new Date()
  var today = new DateExtractor(t, true);
  var picker = $(trigger).val().split(',');
  if (autoDistance) {
    var picker = [parseInt(picker[0]),parseInt(picker[1]),parseInt(picker[2]), picker[3]]
    var distance = Math.round(((picker[0] - today.y) * 12) + (picker[1] - today.m))
  }
  var m1 = new DateExtractor(new Date(t.getFullYear(),t.getMonth() + 0 + distance, t.getDay() ), true);
  var m2 = new DateExtractor(new Date(t.getFullYear(),t.getMonth() + 1 + distance, t.getDay()), true);
  var pos = $(trigger).offset()
  var m1Days = '';
  var m2Days = '';
  var type = 'fa'
  for (var i = 1; i <= m1.l; i++) {
    m1Days += '<i d="' + m1.y + ',' + m1.m + ',' + i + ',' + m1.mn + '">' + i + '</i>'
  }
  for (var i = 1; i <= m2.l; i++) {
    m2Days += '<i d="' + m2.y + ',' + m2.m + ',' + i + ',' + m2.mn + '">' + i + '</i>'
  }
  var template = `
  <div class="dp-body ` + type + `" style="top:` + pos.top + `px; left:` + (pos.left - 350) +`px;">
  <div class="dp-nav">
  <button class="btn-3" type="button" name="prev" onclick=" var distance='` + distance + `'; calendar_j(false, parseInt(distance) - 2, $('.datepicker.focus') );"></button>
  <span>` + m1.y +'<br>'+ m1.mn + `</span>
  <span>` + m2.y +'<br>'+ m2.mn + `</span>
  <button class="btn-3" type="button" name="next"  onclick=" var distance='` + distance + `'; calendar_j(false, parseInt(distance) + 2, $('.datepicker.focus') );"></button>
  </div>
  <div class="dp-days"><i>شنبه</i><i>۱شنبه</i><i>۲شنبه</i><i>۳شنبه</i><i>۴شنبه</i><i>۵شنبه</i><i>جمعه</i></div>
  <div class="dp-days"><i>شنبه</i><i>۱شنبه</i><i>۲شنبه</i><i>۳شنبه</i><i>۴شنبه</i><i>۵شنبه</i><i>جمعه</i></div>
  <div class="dp-m dp-m1">` + m1Days + `</div>
  <div class="dp-m dp-m2">` + m2Days + `</div>
  <div class="dp-toolbox">
  <!--<button class="btn-1 " type="button" name="today-en" onclick="calendar_j(false, 0, $('.datepicker.focus'));">امروز</button>-->
  <button class="btn-1" type="button" name="jalali" onclick="$('.datepicker').removeClass('j').addClass('g'); calendar_g(false, 0, $('.datepicker.focus'));">میلادی</button>
  </div>
  </div>
  `
  $('.dp-body').remove()
  $('body').append(template)
  if ($('.dp-body').offset().left < 10) {
    $('.dp-body').css({
      'left':  10
    })
  }
  $('body .dp-body .dp-m i[d^="'+ [today.y,today.m,today.d,today.mn].toString() + '"]').addClass('today')
  for (var i = today.d -1; i > 0; i--) {
    $('body .dp-body .dp-m i[d^="'+ [today.y,today.m,i,today.mn].toString() + '"]').addClass('disable')
  }
  $('body .dp-body .dp-m1 i:nth-child(1)').attr('style','grid-column-start:' + m1.fd)
  $('body .dp-body .dp-m2 i:nth-child(1)').attr('style','grid-column-start:' + m2.fd)
  $('body .dp-body .dp-m i[d^="'+ [picker[0],picker[1],picker[2],picker[3]].toString() + '"]').addClass('active')
  $('input:hidden[name=Calendertype]').val('persian');
}
function DateExtractor(date, jalali) {
  if (!jalali) {
    this.y = date.getFullYear();
    this.m = date.getMonth() +1;
    this.d = date.getDate();
    var en_m = ['January','February','March','April','May','June','July','August','September','October','November','December']
    this.mn = en_m[date.getMonth()];
    var l = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    this.l = l.getDate();
    this.w = (date.getDay() + 2 <= 7) ? date.getDay() + 2 : 1 ;
    var fd = new Date(date.getFullYear(), date.getMonth(), 1);
    this.fd = (fd.getDay() + 2 <= 7) ? fd.getDay() + 2 : 1 ;
  }else{
    var gy = date.getFullYear();
    var gm = date.getMonth() +1;
    var gd = date.getDate();
    var g_d_m=[0,31,59,90,120,151,181,212,243,273,304,334];
    if(gy > 1600){
      jy=979;
      gy-=1600;
    }else{
      jy=0;
      gy-=621;
    }
    gy2=(gm > 2)?(gy+1):gy;
    days=(365*gy) +(parseInt((gy2+3)/4)) -(parseInt((gy2+99)/100)) +(parseInt((gy2+399)/400)) -80 +gd +g_d_m[gm-1];
    jy+=33*(parseInt(days/12053));
    days%=12053;
    jy+=4*(parseInt(days/1461));
    days%=1461;
    kabiseh = true
    if(days > 365){
      jy+=parseInt((days-1)/365);
      days=(days-1)%365;
      kabiseh = false
    }
    jm=(days < 186)?1+parseInt(days/31):7+parseInt((days-186)/30);
    jd=1+((days < 186)?(days%31):((days-186)%30));
    this.y = jy;
    this.m = jm;
    this.d = jd;
    var fa_m = ['فروردین','اردیبهشت','خرداد','تیر','مرداد','شهریور','مهر','آبان','آذر','دی','بهمن','اسفند']
    this.mn = fa_m[jm - 1];
    this.l = (days < 186) ? 31 : (jm < 12) ? 30 : (kabiseh) ? 30 : 29
    this.w = (date.getDay() + 2 <= 7) ? date.getDay() + 2 : 1 ;
    var fd = new Date(date.getFullYear(),date.getMonth() ,date.getDate() - this.d + 1 )
    this.fd = (fd.getDay() + 2 <= 7) ? fd.getDay() + 2 : 1 ;
  }
}
function tabs() {
  $('body .tab-bar').each(function(i,e) {
    $(this).children().removeClass('active')
    $(this).children().eq(0).addClass('active')
    $(this).parent().children('tab').removeClass('active')
    $(this).parent().children('tab').eq(0).addClass('active')
  });
}
function roll(direction, trigger) {
  var cards = $(trigger).parent().children('.cards')
  cards.w = cards[0].clientWidth;
  cards.scrl = cards[0].scrollLeft
  var items = $(cards).children();
  items.w = $(items).eq(0).outerWidth() + 20
  var move = direction > 0 ? cards.scrl - items.w : cards.scrl + items.w
  $(cards).animate({'scrollLeft': move}, 500)
}
function profileTabs() {
  if ($('#account-settings .profile').length) {
    if (window.location.hash.length) {
      $('.buttons-bar a').removeClass('active')
      $('.buttons-bar a[href="'+ window.location.hash + '"]').addClass('active')
    }else {
      $('.buttons-bar a')[0].click()
      profileTabs()
    }
  }
}
function searchTabs() {
  if ($('.home-display .search').length) {
    $('.home-display .search .btn-6').click(function(e){
      e.preventDefault()
      var hash = this.hash
      $('.home-display .search .btn-6').removeClass('active')
      $(this).addClass('active')
      $('.home-display .search .tab-cont').removeClass('active')
      $(hash).addClass('active')
    })
  }
}
function priceStick() {
  if ($('.tour-price').length > 0 && $(document).height() > $(window).height()) {
    var fix = $('.tour-hotel').offset().top  + $('.tour-hotel').height() + 72 +  $('.tour-price').height();
    var scrl = $(document).scrollTop() + $(window).height()
    if (scrl < fix && $('.tour-price').hasClass('stick') == false) {
      $('.tour-price').addClass('stick')
      $('.tour-hotel').css({
        'margin-bottom': $('.tour-price').height() + 48
      })
    }
    if (scrl > fix && $('.tour-price').hasClass('stick') ) {
      $('.tour-price').removeClass('stick')
      $('.tour-hotel').css({
        'margin-bottom': 24
      })
    }
  }
}
function slider(target) {
  if ($('.main.slider').length > 0) {
    $('.main.slider a, .fullframe li').removeClass('active');
    $('.main.slider a:nth-child(' + target  + '), .fullframe li:nth-of-type(' + target   + ')').addClass('active');

    $('.main.slider').append('<button name="next" class="btn-3"></button><button name="prev" class="btn-3"></button><ul></ul>')
    $('.main.slider a').each(function() {
      $('.main.slider ul').append('<li></li>')
    });
    $('.main.slider button[name=next]').click()
  }
}
function homeSlider() {
  if ($('.home-display .slider').length && $('.home-display .slider-details').length) {
    $('ul.slider-details').append('<ul class="dots"></ul>')
    $('ul.slider-details > li').each(function() {
      $('ul.slider-details .dots').append('<li></li>')
    });
    $('ul.slider li, ul.slider-details li').removeClass('active');
    $('ul.slider li:first-child, ul.slider-details > li:first-child, ul.slider-details .dots li:first-child').addClass('active');
    _nexttimer = setTimeout(function () {
        $('.home-display button[name=next]').click()
    }, 5000)
  }
}
function sorter(a, b){
  return a - b;
}
function sortByKey(array, key) {
  return array.sort(function(a, b) {
    var x = a[key]; var y = b[key];
    return ((x < y) ? -1 : ((x > y) ? 1 : 0));
  });
}
function checkboxReplacer() {
  if ($('body .checkbox-replace').length) {
    $('body .checkbox-replace').each(function(i,e) {
      var par = $(e).parent()
      var lastnode = $(e).prev().eq(0).attr('type').toLowerCase() == "checkbox" ||  $(e).prev().eq(0).attr('type').toLowerCase() == "radio"  ? true : false
      var inputpos = $(par).children('[type="checkbox"]').length ? $(par).children('[type="checkbox"]').index() : $(par).children('[type="radio"]').length ? $(par).children('[type="radio"]').index() : null
      var thispos = $(e).index()
      var parent = [];
      if (inputpos != null && thispos != inputpos + 1) {
        $(par).children().each(function(i,e) {
          parent.push($(e)[0])
        });
        $(par).children().remove();
        var t = parent[thispos]
        parent[thispos] = parent[inputpos + 1]
        parent[inputpos + 1] = t
        for (var i in parent) {
          $(par).eq(0).append(parent[i])
        }
      }
    });
  }
}
function inputNumber() {
  if ($('[type="number"]').length) {
    $('body [type="number"]').each(function(i,e) {
      $(this).attr('type','text').addClass('number')
      $(this).after('<div class="updown"><span onclick="CalculateTotalPrice()" name="up"></span><span onclick="CalculateTotalPrice()" name="down"></span></div>')
    });
  }
}
function heightSet() {
  var h1 = $('header').height() + $('footer').height();
  var h2 = $(window).height();
  var h3 = 0
  $('body > *').each(function(){
    h3 += $(this).height()
  })
  $('footer').css({
    'top': (h2 - h3 - 55) > 0 ? (h2 - h3 - 55) : 50
  })
}
