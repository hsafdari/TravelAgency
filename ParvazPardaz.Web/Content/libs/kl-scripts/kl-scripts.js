

/*
* Closure for Page Load
*/
(function($, window, document) {
	"use strict";


// Responsive Main-Menu
	var page_wrapper = $('#page_wrapper'),
		responsive_trigger = $('.toggle-nav'),
		zn_back_text = 'بازگشت',
		back_text = '<li class="zn_res_menu_go_back"><span class="zn_res_back_icon glyphicon glyphicon-chevron-right"></span><a href="#">'+zn_back_text+'</a><a href="#" class="zn-close-menu-button"><span class="glyphicon glyphicon-remove"></span></a></li>',
		cloned_menu = $('#main-menu > ul').clone().attr({id:"zn-res-menu", "class":""});

	var start_responsive_menu = function(){

		var responsive_menu = cloned_menu.prependTo(page_wrapper);
		var set_height = function(){
			var _menu = $('.zn-menu-visible').last(),
				height = _menu.css({height:'auto'}).outerHeight(true),
				window_height  = $(window).height(),
				adminbar_height = 0,
				admin_bar = $('#wpadminbar');

			// CHECK IF WE HAVE THE ADMIN BAR VISIBLE
			if(height < window_height) {
				height = window_height;
				if ( admin_bar.length > 0 ) {
					adminbar_height = admin_bar.outerHeight(true);
					height = height - adminbar_height;
				}
			}
			_menu.attr('style','');
			page_wrapper.css({'height':height});
		};



		// BIND OPEN MENU TRIGGER
		responsive_trigger.click(function(e){
			e.preventDefault();

			responsive_menu.addClass('zn-menu-visible');
			set_height();

		});

		// Close the menu when a link is clicked
		responsive_menu.find( 'a:not([rel*="mfp-"])' ).on('click',function(e){
			$( '.zn_res_menu_go_back' ).first().trigger( 'click' );
		});

		// ADD ARROWS TO SUBMENUS TRIGGERS
		responsive_menu.find('li:has(> ul)').addClass('zn_res_has_submenu').prepend('<span class="zn_res_submenu_trigger glyphicon glyphicon-chevron-left"></span>');
		// ADD BACK BUTTONS
		responsive_menu.find('.zn_res_has_submenu > ul').addBack().prepend(back_text);

		// REMOVE BACK BUTTON LINK
		$( '.zn_res_menu_go_back' ).click(function(e){
			e.preventDefault();
			var active_menu = $(this).closest('.zn-menu-visible');
			active_menu.removeClass('zn-menu-visible');
			set_height();
			if( active_menu.is('#zn-res-menu') ) {
				page_wrapper.css({'height':'auto'});
			}
		});

		// OPEN SUBMENU'S ON CLICK
		$('.zn_res_submenu_trigger').click(function(e){
			e.preventDefault();
			$(this).siblings('ul').addClass('zn-menu-visible');
			set_height();
		});

		var closeMenu = function(){
			cloned_menu.removeClass('zn-menu-visible');
			responsive_trigger.removeClass('is-active');
			removeHeight();
		};
	}

	// MAIN TRIGGER FOR ACTIVATING THE RESPONSIVE MENU
	var menu_activated = false,
		triggerMenu = function(){
			if ( $(window).width() < 1200 ) {
				if ( !menu_activated ){
					start_responsive_menu();
					menu_activated = true;
				}
				page_wrapper.addClass('zn_res_menu_visible');
			}
			else{
				// WE SHOULD HIDE THE MENU
				$('.zn-menu-visible').removeClass('zn-menu-visible');
				page_wrapper.css({'height':'auto'}).removeClass('zn_res_menu_visible');
			}
		};

	$(document).ready(function() {
		triggerMenu();
	});

	$( window ).on( 'load resize' , function(){
	   triggerMenu();
		var is = false;
		if ( $(window).width() < 1200 ) {
			if(is) return;
			//@wpk
			// Close button for the responsive menu
			var closeMenuSender = $('.zn-close-menu-button');
			if(closeMenuSender){
				closeMenuSender.on('click', function(e){
					e.preventDefault();
					e.stopPropagation();
					var parent = $('#zn-res-menu');
					parent.removeClass('zn-menu-visible');
					//parent.removeClass('zn-menu-visible');
					$('.zn-menu-visible', parent).removeClass('zn-menu-visible');
					$('#page_wrapper').css({'height':'auto'});
				});
			}
			is = true;
		}
	});
// END Responsive Main-Menu
})(window.jQuery, window, document);
