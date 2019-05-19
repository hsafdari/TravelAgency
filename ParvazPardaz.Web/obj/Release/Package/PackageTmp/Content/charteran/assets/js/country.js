
$(document).ready(function() {
  // $('.showmore-child:nth-child(7)').after( '<h3 class="showmore on"> نمایش بیشتر</h3>' );
  $('.showmore').click( function(){
    if ($(this).hasClass('on')) {
      $(this).removeClass('on');
      $(this).hide();
    }else {
      $(this).addClass('on');
    }
  } );
});
