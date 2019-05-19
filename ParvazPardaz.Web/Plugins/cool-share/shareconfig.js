$(function (){
    var $ = jQuery.noConflict();
    var url = window.location.href;
    var pagetitle = $(".title").text();
    var imageurl = $("#zoom_01").prop('src');
    if (imageurl == "") {
        imageurl = "#";
    }
  
	var options = {

		twitter: {
		    text: pagetitle
		},

		facebook : true,
		googlePlus: true,
		telegram:true,
	};

	$('.socialShare').shareButtons(url, options);


/*

	// You can also share to pinterest and tumblr:

	var options = {

		// Pinterest requires a image to be "pinned"

		pinterest: {
			media: 'http://example.com/image.jpg',
			description: 'My lovely picture'
		},

		// Tumblr takes a name and a description

		tumblr: {
			name: 'jQuery Social Buttons Plugin!',
			description: 'There is a new article on tutorialzine.com page! Check out!'
		}
	};

*/

});
