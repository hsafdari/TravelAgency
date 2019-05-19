
(function($){

  var isTouch = (('ontouchstart' in window) || (navigator.msMaxTouchPoints > 0));
  if(isTouch){
     $("html").addClass("touch");
   }
  if(!isTouch){
    $("html").addClass("notouch");
   }


   // Select all links with hashes
   $('a[href*="#"]')
     // Remove links that don't actually link to anything
     .not('[href="#"]')
     .not('[href="#0"]')
     .click(function(event) {
       // On-page links
       if (
         location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '')
         &&
         location.hostname == this.hostname
       ) {
         // Figure out element to scroll to
         var target = $(this.hash);
         target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
         // Does a scroll target exist?
         if (target.length) {
           // Only prevent default if animation is actually gonna happen
           event.preventDefault();
           $('html, body').animate({
             scrollTop: target.offset().top
           }, 1000, function() {
             // Callback after animation
             // Must change focus!
             var $target = $(target);
             $target.focus();
             if ($target.is(":focus")) { // Checking if the target was focused
               return false;
             } else {
               $target.attr('tabindex','-1'); // Adding tabindex for elements not focusable
               $target.focus(); // Set focus again
             };
           });
         }
       }
     });


     function printSomething(){
       var $attribute = $('[data-smart-affix]');
       $attribute.each(function() {
         $(this).affix({
           offset: {
             top: $(this).offset().top,
           }
         })
       })
       $(window).on("resize", function() {
         $attribute.each(function() {
           $(this).data('bs.affix').options.offset.top = $(this).offset().top
         })
       })
     }
     window.onload = printSomething;


})(jQuery); // End of use strict
