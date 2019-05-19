jQuery(document).ready(function($) {
  slider();
  roll();
  mobile()
});
$(window).on('resize',function(){
  roll()
  mobile()
})
function mobile() {
  if (window.innerWidth <= 719) {
    if (!$('header .btn-mobile').length) {
      $('header').append(`
        <div class="searchModule">
        <button class="btn-mobile" name="closeSearch"></button>
        <form>
        <input type="text" name="search"placeholder="جستجو">
        <input type="submit" name="submit" value="">
        </form>
        </div>
        <button class="btn-mobile" name="hamburger"></button>
        <button class="btn-mobile" name="search"></button>
        `)
      }
    }else {
      $('header > button, header .searchModule').remove()
      $('*').removeClass('freez no-freez')
      $('header nav, header .searchModule').removeClass('active')
    }
  }
  function slider() {
    if ($('.main.slider').length > 0) {
      $('.main.slider .slide').removeClass('active');
      $('.main.slider .slide:nth-child(1)').addClass('active');
      $('.main.slider').append('<button name="next" class="btn-3"></button><button name="prev" class="btn-3"></button>')
      if (!$('.main.slider').hasClass('no-dots')) {
        $('.main.slider').append('<ul class="dots"></ul>')
        $('.main.slider .slide').each(function() {
          $('.main.slider .dots').append('<li></li>')
        });
        $('.main.slider .dots li:nth-child(1)').addClass('active');
      }
      $('.main.slider button[name=next]').click()
    }
  }
  function roll() {
    if ($('.main.roll').length > 0) {
      $('.main.roll .cards').stop().scrollLeft(0)
      clearTimeout(nextslicetimer)
      var unitWidth = $('.main.roll .cards > *:first-child').width() + 10
      var capacity = window.innerWidth / unitWidth
      var slices = Math.ceil($('.main.roll .cards > a').length / capacity)
      $('.main.roll .dots').remove()
      $('.main.roll').append('<ul class="dots"></ul>')
      for (var i = 0; i < slices; i++) {
        $('.main.roll .dots').append('<li></li>')
      }
      $('.main.roll .dots li:first-child').click()
    }
  }
  nextslicetimer =""
  $(document).on('click','.main.roll .dots li',function(){
      clearTimeout(nextslicetimer)
    var unitWidth = $('.main.roll .cards > *:first-child').width() + 10
    var capacity = window.innerWidth / unitWidth
    var scrl = ($('.main.roll .dots li').length - 1 -  $(this).index()) * unitWidth *  Math.floor(capacity)
    $('.main.roll .dots li').removeClass('active');
    $(this).addClass('active');
    $('.main.roll .cards').stop()
    $('.main.roll .cards').animate({'scrollLeft':scrl}, 1000);
    nextslicetimer = setTimeout(function () {
      var next = $('.main.roll .dots li.active').next('li').index() + 1
      var next = (next == 0) ? 1 : next
      $('.main.roll .dots li:nth-child(' + next + ')').click()
    }, 10000);
  })
  nexttimer =""
  $(document).on('click','.main.slider button', function(){
    clearTimeout(nexttimer)
    $('.main.slider .timebar').remove()
    $('.main.slider').append('<div class="timebar"></div>')
    var active = $('.main.slider .slide.active').index()
    if ($(this).prop('name') == 'next') {
      var dir = 1;
    }else {
      var dir = -1;
    }
    var length = $('.slider').children('.slide').length
    var next = (active + dir +1)
    if (next == 0) {
      var next = length
    }
    if (next == length + 1) {
      var next = 1
    }
    $('.main.slider .slide.active, .main.slider .dots li.active ').removeClass('active');
    $('.main.slider .slide:nth-child(' + next + '), .main.slider .dots li:nth-child(' + next + ')').addClass('active');
    nexttimer = setTimeout(function () {
      $('.main.slider button[name=next]').click()
    }, 10000);
  })
  $(document).on('click','.main.slider .dots li', function(){
    clearTimeout(nexttimer)
    $('.main.slider .timebar').remove()
    $('.main.slider').append('<div class="timebar"></div>')
    var next = $(this).index() + 1
    $('.main.slider .slide.active, .main.slider .dots li.active ').removeClass('active');
    $('.main.slider .slide:nth-child(' + next + '), .main.slider .dots li:nth-child(' + next + ')').addClass('active');
    nexttimer = setTimeout(function () {
      $('.main.slider button[name=next]').click()
    }, 10000);
  })
  $(document).on('click','header button[name=hamburger]', function(){
    $(this).toggleClass('active')
    $('header nav').toggleClass('active no-freez')
    $('body header').toggleClass(' no-freez')
    $('body').toggleClass('freez')
  })
  $(document).on('click','header button[name=search], header button[name=closeSearch]', function(){
    ($('header button[name=hamburger]').hasClass('active')) ? $('header button[name=hamburger]').click() : null;
    $(this).toggleClass('active')
    $('header .searchModule').toggleClass('active no-freez')
    $('header .searchModule input[type="text"]').focus()
    $('body header').toggleClass(' no-freez')
    $('body').toggleClass('freez')
  })
