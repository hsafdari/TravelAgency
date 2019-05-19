$('.tsp-owl-carousel').each(function() {
     var item_lg = $(this).data('item-lg');
     var item_md = $(this).data('item-md');
     var item_sm = $(this).data('item-sm');
     if (item_lg || item_md) {
         $(this).owlCarousel({
             loop: true,
             margin: 30,
             nav: true,
             rtl:true,
             responsive: {
                 0: {
                     items: 1
                 },
                 400: {
                     items: item_sm
                 },
                 600: {
                     items: item_md
                 },
                 1000: {
                     items: item_lg
                 }
             }
         });
     }
 });


 $("#home-slider").owlCarousel({
   items : 1,
   pagination	: true,
   autoplay	: true,
   singleItem	: true,
   dots: true,
   loop:true,

   stopOnHover	: true,
   rtl:true,
 });


 $("#latest-news").owlCarousel({
   // items : 4,
   pagination	: true,
   autoplay	: true,
   singleItem	: true,
   dots: true,
   loop:true,
   stopOnHover	: true,
   rtl:true,
   responsive:{
    0:{
        items:1,
    },
    600:{
        items:2,
    },
    1000:{
        items:4,
    }
}
 });



 var currentImage,
row = $(".first"),
activeImage,
left;

$('docuemnt').ready(function() {

  $('.thumbnail_').click(function(e) {

      currentImage = $(this).attr('id');

      if((activeImage == currentImage) && activeImage) {
        return false;
      }

      $(".th_" + currentImage).css({ zIndex: 5 });

      $(".dimmer").velocity("transition.slideDownIn");


      if(currentImage > 1 && currentImage < 5) {

        left = (currentImage * 25) - 25

        row = $(".first")

      } else {
        left = 0;
      }

      $(".th_" + currentImage).velocity({
        width: "100%",
        left: "0%"
      }, {duration: 200, easing: "easeInCubic "}).velocity({
        height: "100%"
      }, {delay: 400, easing: "easeOutExpo"});

      $(".thumb_meta").velocity("transition.slideDownIn", { delay: "500"});

      row.velocity({
          height: "100%"
      }, {delay:300})

      activeImage = currentImage
  })

  $('.thumb_meta a').click(function() {

    $(".thumb_meta").velocity("transition.slideUpOut", {duration: 100});

    $(".th_" + currentImage).velocity({
        width: "25%",
        left:left + "%"
    }, {duration: 200, easing: "easeInCubic "}).velocity({
        height: "250px"
    }, {delay: 400, easing: "easeOutExpo"});

    $(".dimmer").velocity("transition.slideUpOut", {
      delay: 400,
      complete: function(elements) {
        $(".th_" + currentImage).css({ zIndex: 0 });
      }
    });

    activeImage = false

  })

});



$('[data-fancybox="gallery"]').fancybox({
// Options will go here
});
