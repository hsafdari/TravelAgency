(function ($) {
  'use strict';

  $(document).ready(function () {

   $('.pgwSlider').pgwSlider({
        intervalDuration : 8000,
        // transitionEffect: false,
        displayList : true,
        listPosition : 'right',
        verticalCentering: true,

    });
    //slide show verticle
    $('.ps-list').owlCarousel({
        dots: false,
        responsiveClass:true,
        nav: true,
        rtl:true,
        navText:['<i class=\'fa fa-chevron-left\'></i>','<i class=\'fa fa-chevron-right\'></i>'],
        responsive: {
            0: {
                items: 1
            },
            1000: {
                items: 4
            }
        },
        // items:1,
    });
    $('.ps-list').each(function() {

        $('.tsp-pre').on('click', function(e) {
            e.preventDefault();
            $('.ps-list').animate({'margin-top':'+=130'}, 100, function(){
                $('.ps-list .owl-item:first').appendTo('.ps-list');
                $('.ps-list').css('margin-top',0);
           });
        });
        $('.tsp-next').on('click', function(e) {
            e.preventDefault();
            $('.ps-list').animate({'margin-top':'-=130'}, 100, function(){
                $('.ps-list .owl-item:first').appendTo('.ps-list');
                $('.ps-list').css('margin-top',0);
            });
        });
    });
  });
})(jQuery);
