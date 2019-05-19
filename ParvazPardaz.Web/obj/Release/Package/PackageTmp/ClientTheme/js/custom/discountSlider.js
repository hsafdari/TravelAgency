(function($)
{
	"use strict"

	//responsive code begin
	//you can remove responsive code if you don't want the slider scales while window resizes
	function ScaleSlider(jssor_slider1, options) {
		var parentWidth = jssor_slider1.$Elmt.parentNode.clientWidth;
		if (parentWidth)
			jssor_slider1.$ScaleWidth(Math.min(parentWidth, 1920));
		else
			window.setTimeout(ScaleSlider, 30);
	}	

	/* Event - Window Scroll */
	$(window).scroll(function()
	{
		var scroll	=	$(window).scrollTop();
		var height	=	$(window).height();

		/*** set sticky menu ***/
		if( scroll >= 200 )
		{
			$('.menu-block').addClass("navbar-fixed-top").delay( 2000 ).fadeIn();
		}
		else if ( scroll <= height )
		{
			$('.menu-block').removeClass("navbar-fixed-top");
		}
		else
		{
			$('.menu-block').removeClass("navbar-fixed-top");
		} // set sticky menu - end		

		if ($(this).scrollTop() >= 50)
		{
			// If page is scrolled more than 50px
			$('#back-to-top').fadeIn(200);    // Fade in the arrow
		}
		else
		{
			$('#back-to-top').fadeOut(200);   // Else fade out the arrow
		}
	});
	/* Event - Window Scroll /- */

	$('#back-to-top').click(function()
	{
		// When arrow is clicked
		$('body,html').animate(
		{
			scrollTop : 0 // Scroll to top of body
		},800);
	});
	
	/* Event - Document Ready */	
	$(document).ready(function($)
	{

		if (jQuery("#slider1_container").length)
		{
			"use strict"
			var win_width = $(window).width();
			var slider_width = 1200;
			var slider_spacing = 52;
			var parking_position = 352;
			var display_pices = 2;
			var slide_height = 760;
			
			var options = {
					$AutoPlay: true,
					$PauseOnHover: 1,                               //[Optional] Whether to pause when mouse over if a slideshow is auto playing, default value is false
					$ArrowKeyNavigation: true,   			            //Allows arrow key to navigate or not
					$SlideWidth: slider_width,                                   //[Optional] Width of every slide in pixels, the default is width of 'slides' container
					$SlideHeight: slide_height,                                  //[Optional] Height of every slide in pixels, the default is width of 'slides' container
					$SlideSpacing: slider_spacing, 					                //Space between each slide in pixels
					$DisplayPieces: display_pices,                                //Number of pieces to display (the slideshow would be disabled if the value is set to greater than 1), the default value is 1
					$ParkingPosition: parking_position,                                //The offset position to park slide (this options applys only when slideshow disabled).

				$ArrowNavigatorOptions: {                       //[Optional] Options to specify and enable arrow navigator or not
					$Class: $JssorArrowNavigator$,              //[Requried] Class to create arrow navigator instance
					$ChanceToShow: 2,                               //[Required] 0 Never, 1 Mouse Over, 2 Always
					$AutoCenter: 2,                                 //[Optional] Auto center arrows in parent container, 0 No, 1 Horizontal, 2 Vertical, 3 Both, default value is 0
					$Steps: 1                                       //[Optional] Steps to go for each navigation request, default value is 1
				}
			};
				
			if( win_width > 991 && win_width < 1200 ) {
				slider_width = 990;
				slider_spacing = 150;
				parking_position = 450;
				display_pices = 2;
				//alert(slider_width);
			}
			else if ( win_width > 768 && win_width < 990 ) {
				slider_width = 890;
				slider_spacing = 65;
				parking_position = 400;
				display_pices = 2;
				//alert(slider_width);
			}
			else if ( win_width > 640 && win_width < 767 ) {
				slider_width = 600;
				slider_spacing = 355;
				parking_position = 0;
				display_pices = 1;			
			}

			var jssor_slider1 = new $JssorSlider$("slider1_container", options);

			ScaleSlider( jssor_slider1, options );

			$(window).bind("resize", ScaleSlider);
			//responsive code end
		}

		var scroll	=	$(window).scrollTop();
		var height	=	$(window).height();
		
		/*** set sticky menu ***/
		if( scroll >= height -300 )
		{
			$('.menu-block').addClass("navbar-fixed-top").delay( 2000 ).fadeIn();
		}
		else if ( scroll <= height )
		{
			$('.menu-block').removeClass("navbar-fixed-top");
		}
		else
		{
			$('.menu-block').removeClass("navbar-fixed-top");
		} // set sticky menu - end

		$('.navbar-nav li a, .logo-block a').on('click', function(event)
		{
			var anchor = $(this);

			if( anchor == 'undefined' || anchor == null || anchor.attr('href') == '#' ) { return; }
			if ( anchor.attr('href').indexOf('#') === 0 )
			{
				if( $(anchor.attr('href')).length )
				{
					$('html, body').stop().animate( { scrollTop: $(anchor.attr('href')).offset().top - 72 }, 1500, 'easeInOutExpo' );					
				}
				event.preventDefault();
			}
		});

		$('.goto-next a').on('click', function(event)
		{
			var anchor = $(this);

			if( anchor == 'undefined' || anchor == null || anchor.attr('href') == '#' ) { return; }
			if ( anchor.attr('href').indexOf('#') === 0 )
			{
				if( $(anchor.attr('href')).length )
				{
					$('html, body').stop().animate( { scrollTop: $(anchor.attr('href')).offset().top - 150 }, 1500, 'easeInOutExpo' );			
				}
				event.preventDefault();
			}
		});		
		
		/* Skills */
		$('.about-skill-progres, .author-rating-progress ').each(function ()
		{
			var $this = $(this);
			var myVal = $(this).data("value");

			$this.appear(function()
			{			
				var skill_type2_item_count = 0;
				var skill_type2_count = 0;					
				skill_type2_item_count = $( "[id*='skill_type2_count-']" ).length;
				
				var skill_bar2_count = 0;
				var skills_bar2_count = 0;
				skill_bar2_count = $( "[id*='skill_bar2_count-']" ).length;
				
				for(var i=1; i<=skill_type2_item_count; i++)
				{
					skill_type2_count = $( "[id*='skill_type2_count-"+i+"']" ).attr( "data-skill2_percent" );
					$("[id*='skill_type2_count-"+i+"']").animateNumber({ number: skill_type2_count }, 2000);
				}			
				
				for(var j=1; j<=skill_bar2_count; j++)
				{
					skills_bar2_count = $( "[id*='skill_type2_count-"+j+"']" ).attr( "data-skill2_percent" );
					$("[id*='skill_bar2_count-"+j+"']").css({'width': skills_bar2_count+'%'});
				}
				
				for(var k=1; k<=skill_bar2_count; k++)
				{
					skills_bar2_count = $( "[id*='skill_bar2_count-"+k+"']" ).attr( "data-color" );
					$("[id*='skill_bar2_count-"+k+"']").css({'background-color': skills_bar2_count});
				}
			});
		});	
		/* Skills /- */	
		
		/* Author */
		$('.statistics').each(function ()
		{
			var $this = $(this);
			var myVal = $(this).data("value");

			$this.appear(function()
			{		
				var statistics_item_count = 0;
				var statistics_count = 0;					
				statistics_item_count = $( "[id*='statistics_1_count-']" ).length;
				//alert(statistics_item_count);

				for(var i=1; i<=statistics_item_count; i++)
				{
					statistics_count = $( "[id*='statistics_1_count-"+i+"']" ).attr( "data-statistics_percent" );
					$("[id*='statistics_1_count-"+i+"']").animateNumber({ number: statistics_count }, 2000);
					// $("[id*='skill_count-"+i+"']").css('width', skills_count);
				}				
			});
		});	 /* Author /- */
		
		/* Blog - excerpt display */	
		$("[id*='add_sign_count-']").on('click', function() {

			$("[id='add_sign_count-" + $(this).attr("id").split("-")[1] + "'] > img").css("display", "none");

			if( $("[id='add_sign_count-" + $(this).attr("id").split("-")[1] + "']").html() == '<i class="fa fa-angle-down"></i>' ) {

				$("[id='add_sign_count-" + $(this).attr("id").split("-")[1] + "']").html("<img src='/ClientTheme/images/icon/big-plus.png' alt='big-plus' />");
				$("#post-content-" + $(this).attr("id").split("-")[1] + "").css("display", "none");

			} else {

				$("[id='add_sign_count-" + $(this).attr("id").split("-")[1] + "']").html("<i class='fa fa-angle-down'></i>");
				$("#post-content-" + $(this).attr("id").split("-")[1] + "").css("display", "block");

			}
		}); /* Blog - excerpt display /- */

		/* marquee */
		$('.marquee').marquee({
			direction: 'right',
		    //speed: 1500,
		    duration: 20000,
			pauseOnHover: true,
			gap: 50,
			startVisible: true,
			duplicated: true
		});
		
		$('.marquee-vert').marquee({
			direction: 'up',
			speed: 500,
			pauseOnHover: true
		});
		
		
		/* Statistics */	
		$('.dial').each(function ()
		{
			var $this = $(this);
			var myVal = $(this).data("value");		

			$this.appear(function()
			{
				// alert(myVal);
				$this.knob({ });
				$({ value: 0 }).animate({ value: myVal },
				{
					duration: 2000,
					easing: 'swing',
					step: function ()
					{
						$this.val(Math.ceil(this.value)).trigger('change');
					}
				});
			});
		});		/* Statistics /- */	
		
		
		$("#political-world").owlCarousel(
		{
			autoPlay: 3000, //Set AutoPlay to 3 seconds

			items : 4,
			itemsDesktop : [1199,3],
			itemsDesktopSmall : [979,2],
			navigation : true,
			pagination: false
		});
		
		$("#widget-flicker").owlCarousel(
		{
			autoPlay: 3000, //Set AutoPlay to 3 seconds

			 itemsCustom : [
					[0, 1],
					[450, 1],
					[600, 1],
					[700, 1],
					[1000, 1],
					[1200, 1],
					[1400, 1],
					[1600, 1]
					],
			navigation : true,
			pagination: false
		});

		var wow = new WOW(
		{
			boxClass:     'wow',      // animated element css class (default is wow)
			animateClass: 'animated', // animation css class (default is animated)
			offset:       0,          // distance to the element when triggering the animation (default is 0)
			mobile:       true,       // trigger animations on mobile devices (default is true)
			live:         true        // act on asynchronously loaded content (default is true)
		});
		wow.init();		

		// The slider being synced must be initialized first
		$('#about-carousel').flexslider({
			animation: "slide",
			controlNav: false,
			animationLoop: false,
			slideshow: false,
			itemWidth: 210,
			itemMargin: 5,
			asNavFor: '#about-slider'
		});

		$('#about-slider').flexslider({
			animation: "slide",
			controlNav: false,
			animationLoop: false,
			slideshow: false,
			sync: "#about-carousel"
		});

	}); 	/* Event - Document Ready /- */	
		
	
	/* Event - Window Load /- */
	if (!$('html').is('.ie6, .ie7, .ie8'))
	{
		/* Event - Window Load */
		$(window).load(function()
		{		
			/* Loader */
			$("#site-loader").delay(1000).fadeOut("slow");
		});
		/* Event - Window Load /- */
	}
	else
	{
		$("#site-loader").css('display','none');
	}	
	
})(jQuery);