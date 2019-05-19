// global variables
var isIE8 = false;
var isIE9 = false;
var JqwindowWidth;
var JqwindowHeight;
var JqpageArea;
// Debounce Function
(function (Jq, sr) {
    // debouncing function from John Hann
    // http://unscriptable.com/index.php/2009/03/20/debouncing-javascript-methods/
    var debounce = function (func, threshold, execAsap) {
        var timeout;
        return function debounced() {
            var obj = this,
                args = arguments;

            function delayed() {
                if (!execAsap)
                    func.apply(obj, args);
                timeout = null;
            };

            if (timeout)
                clearTimeout(timeout);
            else if (execAsap)
                func.apply(obj, args);

            timeout = setTimeout(delayed, threshold || 100);
        };
    };
    // smartresize
    jQuery.fn[sr] = function (fn) {
        return fn ? this.bind('resize', debounce(fn)) : this.trigger(sr);
    };

})(jQuery, 'clipresize');

//Main Function
var Main = function () {
    //function to detect explorer browser and its version
    var Jq = jQuery.noConflict();
    var runInit = function () {
        if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
            var ieversion = new Number(RegExp.Jq1);
            if (ieversion == 8) {
                isIE8 = true;
            } else if (ieversion == 9) {
                isIE9 = true;
            }
        }
    };
    //function to adjust the template elements based on the window size
    var runElementsPosition = function () {
        JqwindowWidth = Jq(window).width();
        JqwindowHeight = Jq(window).height();
        JqpageArea = JqwindowHeight - Jq('body > .navbar').outerHeight() - Jq('body > .footer').outerHeight();
        Jq('.sidebar-search input').removeAttr('style').removeClass('open');
        runContainerHeight();

    };
    //function to adapt the Main Content height to the Main Navigation height
    var runContainerHeight = function () {
        mainContainer = Jq('.main-content > .container');
        mainNavigation = Jq('.main-navigation');
        if (JqpageArea < 760) {
            JqpageArea = 760;
        }
        if (mainContainer.outerHeight() < mainNavigation.outerHeight() && mainNavigation.outerHeight() > JqpageArea) {
            mainContainer.css('min-height', mainNavigation.outerHeight());
        } else {
            mainContainer.css('min-height', JqpageArea);
        };
        if (JqwindowWidth < 768) {
            mainNavigation.css('min-height', JqwindowHeight - Jq('body > .navbar').outerHeight());
        }
    };
    //function to activate the ToDo list, if present
    var runToDoAction = function () {
        if (Jq(".todo-actions").length) {
            Jq(".todo-actions").click(function () {
                if (Jq(this).find("i").hasClass("fa-square-o") || Jq(this).find("i").hasClass("icon-check-empty")) {
                    if (Jq(this).find("i").hasClass("fa")) {
                        Jq(this).find("i").removeClass("fa-square-o").addClass("fa-check-square-o");
                    } else {
                        Jq(this).find("i").removeClass("icon-check-empty").addClass("fa fa-check-square-o");
                    };
                    Jq(this).parent().find("span").css({
                        opacity: .25
                    });
                    Jq(this).parent().find(".desc").css("text-decoration", "line-through");
                } else {
                    Jq(this).find("i").removeClass("fa-check-square-o").addClass("fa-square-o");
                    Jq(this).parent().find("span").css({
                        opacity: 1
                    });
                    Jq(this).parent().find(".desc").css("text-decoration", "none");
                }
                return !1;
            });
        }
    };
    //function to activate the Tooltips, if present
    var runTooltips = function () {
        if (Jq(".tooltips").length) {
            Jq('.tooltips').tooltip();
        }
    };
    //function to activate the Popovers, if present
    var runPopovers = function () {
        if (Jq(".popovers").length) {
            Jq('.popovers').popover();
        }
    };
    //function to allow a button or a link to open a tab
    var runShowTab = function () {
        if (Jq(".show-tab").length) {
            Jq('.show-tab').bind('click', function (e) {
                e.preventDefault();
                var tabToShow = Jq(this).attr("href");
                if (Jq(tabToShow).length) {
                    Jq('a[href="' + tabToShow + '"]').tab('show');
                }
            });
        };
        if (getParameterByName('tabId').length) {
            Jq('a[href="#' + getParameterByName('tabId') + '"]').tab('show');
        }
    };
    var runPanelScroll = function () {
        if (Jq(".panel-scroll").length) {
            Jq('.panel-scroll').perfectScrollbar({
                wheelSpeed: 50,
                minScrollbarLength: 20,
                suppressScrollX: true
            });
        }
    };
    //function to extend the default settings of the Accordion
    var runAccordionFeatures = function () {
        if (Jq('.accordion').length) {
            Jq('.accordion .panel-collapse').each(function () {
                if (!Jq(this).hasClass('in')) {
                    Jq(this).prev('.panel-heading').find('.accordion-toggle').addClass('collapsed');
                }
            });
        }
        Jq(".accordion").collapse().height('auto');
        var lastClicked;

        Jq('.accordion .accordion-toggle').bind('click', function () {
            currentTab = Jq(this);
            Jq('html,body').animate({
                scrollTop: currentTab.offset().top - 100
            }, 1000);
        });
    };
    //function to reduce the size of the Main Menu
    var runNavigationToggler = function () {
        Jq('.navigation-toggler').bind('click', function () {
            if (!Jq('body').hasClass('navigation-small')) {
                Jq('body').addClass('navigation-small');
            } else {
                Jq('body').removeClass('navigation-small');
            };
        });
    };
    //function to activate the panel tools
    var runModuleTools = function () {
        Jq('.panel-tools .panel-expand').bind('click', function (e) {
            Jq('.panel-tools a').not(this).hide();
            Jq('body').append('<div class="full-white-backdrop"></div>');
            Jq('.main-container').removeAttr('style');
            backdrop = Jq('.full-white-backdrop');
            wbox = Jq(this).parents('.panel');
            wbox.removeAttr('style');
            if (wbox.hasClass('panel-full-screen')) {
                backdrop.fadeIn(200, function () {
                    Jq('.panel-tools a').show();
                    wbox.removeClass('panel-full-screen');
                    backdrop.fadeOut(200, function () {
                        backdrop.remove();
                    });
                });
            } else {
                Jq('body').append('<div class="full-white-backdrop"></div>');
                backdrop.fadeIn(200, function () {
                    Jq('.main-container').css({
                        'max-height': Jq(window).outerHeight() - Jq('header').outerHeight() - Jq('.footer').outerHeight() - 100,
                        'overflow': 'hidden'
                    });
                    backdrop.fadeOut(200);
                    backdrop.remove();
                    wbox.addClass('panel-full-screen').css({
                        'max-height': Jq(window).height(),
                        'overflow': 'auto'
                    });
                });
            }
        });
        Jq('.panel-tools .panel-close').bind('click', function (e) {
            Jq(this).parents(".panel").remove();
            e.preventDefault();
        });
        Jq('.panel-tools .panel-refresh').bind('click', function (e) {
            var el = Jq(this).parents(".panel");
            el.block({
                overlayCSS: {
                    backgroundColor: '#fff'
                },
                message: '<img src="assets/images/loading.gif" /> Just a moment...',
                css: {
                    border: 'none',
                    color: '#333',
                    background: 'none'
                }
            });
            window.setTimeout(function () {
                el.unblock();
            }, 1000);
            e.preventDefault();
        });
        Jq('.panel-tools .panel-collapse').bind('click', function (e) {
            e.preventDefault();
            var el = jQuery(this).parent().closest(".panel").children(".panel-body");
            if (Jq(this).hasClass("collapses")) {
                Jq(this).addClass("expand").removeClass("collapses");
                el.slideUp(200);
            } else {
                Jq(this).addClass("collapses").removeClass("expand");
                el.slideDown(200);
            }
        });
    };
    //function to activate the 3rd and 4th level menus
    var runNavigationMenu = function () {
        Jq('.main-navigation-menu li.active').addClass('open');
        Jq('.main-navigation-menu > li a').bind('click', function () {
            if (Jq(this).parent().children('ul').hasClass('sub-menu') && ((!Jq('body').hasClass('navigation-small') || JqwindowWidth < 767) || !Jq(this).parent().parent().hasClass('main-navigation-menu'))) {
                if (!Jq(this).parent().hasClass('open')) {
                    Jq(this).parent().addClass('open');
                    Jq(this).parent().parent().children('li.open').not(Jq(this).parent()).not(Jq('.main-navigation-menu > li.active')).removeClass('open').children('ul').slideUp(200);
                    Jq(this).parent().children('ul').slideDown(200, function () {
                        runContainerHeight();
                    });
                } else {
                    if (!Jq(this).parent().hasClass('active')) {
                        Jq(this).parent().parent().children('li.open').not(Jq('.main-navigation-menu > li.active')).removeClass('open').children('ul').slideUp(200, function () {
                            runContainerHeight();
                        });
                    } else {
                        Jq(this).parent().parent().children('li.open').removeClass('open').children('ul').slideUp(200, function () {
                            runContainerHeight();
                        });
                    }
                }
            }
        });
    };
    //function to activate the Go-Top button
    var runGoTop = function () {
        Jq('.go-top').bind('click', function (e) {
            Jq("html, body").animate({
                scrollTop: 0
            }, "slow");
            e.preventDefault();
        });
    };
    //function to avoid closing the dropdown on click
    var runDropdownEnduring = function () {
        if (Jq('.dropdown-menu.dropdown-enduring').length) {
            Jq('.dropdown-menu.dropdown-enduring').click(function (event) {
                event.stopPropagation();
            });
        }
    };
    //function to return the querystring parameter with a given name.
    var getParameterByName = function (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    };
    //function to activate the iCheck Plugin
    var runCustomCheck = function () {
        if (Jq('input[type="checkbox"]').length || Jq('input[type="radio"]').length) {
            Jq('input[type="checkbox"].grey, input[type="radio"].grey').iCheck({
                checkboxClass: 'icheckbox_minimal-grey',
                radioClass: 'iradio_minimal-grey',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].red, input[type="radio"].red').iCheck({
                checkboxClass: 'icheckbox_minimal-red',
                radioClass: 'iradio_minimal-red',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].green, input[type="radio"].green').iCheck({
                checkboxClass: 'icheckbox_minimal-green',
                radioClass: 'iradio_minimal-green',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].teal, input[type="radio"].teal').iCheck({
                checkboxClass: 'icheckbox_minimal-aero',
                radioClass: 'iradio_minimal-aero',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].orange, input[type="radio"].orange').iCheck({
                checkboxClass: 'icheckbox_minimal-orange',
                radioClass: 'iradio_minimal-orange',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].purple, input[type="radio"].purple').iCheck({
                checkboxClass: 'icheckbox_minimal-purple',
                radioClass: 'iradio_minimal-purple',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].yellow, input[type="radio"].yellow').iCheck({
                checkboxClass: 'icheckbox_minimal-yellow',
                radioClass: 'iradio_minimal-yellow',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].square-black, input[type="radio"].square-black').iCheck({
                checkboxClass: 'icheckbox_square',
                radioClass: 'iradio_square',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].square-grey, input[type="radio"].square-grey').iCheck({
                checkboxClass: 'icheckbox_square-grey',
                radioClass: 'iradio_square-grey',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].square-red, input[type="radio"].square-red').iCheck({
                checkboxClass: 'icheckbox_square-red',
                radioClass: 'iradio_square-red',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].square-green, input[type="radio"].square-green').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].square-teal, input[type="radio"].square-teal').iCheck({
                checkboxClass: 'icheckbox_square-aero',
                radioClass: 'iradio_square-aero',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].square-orange, input[type="radio"].square-orange').iCheck({
                checkboxClass: 'icheckbox_square-orange',
                radioClass: 'iradio_square-orange',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].square-purple, input[type="radio"].square-purple').iCheck({
                checkboxClass: 'icheckbox_square-purple',
                radioClass: 'iradio_square-purple',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].square-yellow, input[type="radio"].square-yellow').iCheck({
                checkboxClass: 'icheckbox_square-yellow',
                radioClass: 'iradio_square-yellow',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].flat-black, input[type="radio"].flat-black').iCheck({
                checkboxClass: 'icheckbox_flat',
                radioClass: 'iradio_flat',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].flat-grey, input[type="radio"].flat-grey').iCheck({
                checkboxClass: 'icheckbox_flat-grey',
                radioClass: 'iradio_flat-grey',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-red',
                radioClass: 'iradio_flat-red',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].flat-green, input[type="radio"].flat-green').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].flat-teal, input[type="radio"].flat-teal').iCheck({
                checkboxClass: 'icheckbox_flat-aero',
                radioClass: 'iradio_flat-aero',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].flat-orange, input[type="radio"].flat-orange').iCheck({
                checkboxClass: 'icheckbox_flat-orange',
                radioClass: 'iradio_flat-orange',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].flat-purple, input[type="radio"].flat-purple').iCheck({
                checkboxClass: 'icheckbox_flat-purple',
                radioClass: 'iradio_flat-purple',
                increaseArea: '10%' // optional
            });
            Jq('input[type="checkbox"].flat-yellow, input[type="radio"].flat-yellow').iCheck({
                checkboxClass: 'icheckbox_flat-yellow',
                radioClass: 'iradio_flat-yellow',
                increaseArea: '10%' // optional
            });
        };
    };
    //Search Input function
    var runSearchInput = function () {
        var search_input = Jq('.sidebar-search input');
        var search_button = Jq('.sidebar-search button');
        var search_form = Jq('.sidebar-search');
        search_input.attr('data-default', Jq(search_input).outerWidth()).focus(function () {
            Jq(this).animate({
                width: 200
            }, 200);
        }).blur(function () {
            if (Jq(this).val() == "") {
                if (Jq(this).hasClass('open')) {
                    Jq(this).animate({
                        width: 0,
                        opacity: 0
                    }, 200, function () {
                        Jq(this).hide();
                    });
                } else {
                    Jq(this).animate({
                        width: Jq(this).attr('data-default')
                    }, 200);
                }
            }
        });
        search_button.bind('click', function () {
            if (Jq(search_input).is(':hidden')) {
                Jq(search_input).addClass('open').css({
                    width: 0,
                    opacity: 0
                }).show().animate({
                    width: 200,
                    opacity: 1
                }, 200).focus();
            } else if (Jq(search_input).hasClass('open') && Jq(search_input).val() == '') {
                Jq(search_input).removeClass('open').animate({
                    width: 0,
                    opacity: 0
                }, 200, function () {
                    Jq(this).hide();
                });
            } else if (Jq(search_input).val() != '') {
                return;
            } else
                Jq(search_input).focus();
            return false;
        });
    };
    //Set of functions for Style Selector
    var runStyleSelector = function () {
        Jq('.style-toggle').bind('click', function () {
            if (Jq(this).hasClass('open')) {
                Jq(this).removeClass('open').addClass('close');
                Jq('#style_selector_container').hide();
            } else {
                Jq(this).removeClass('close').addClass('open');
                Jq('#style_selector_container').show();
            }
        });
        setColorScheme();
        setLayoutStyle();
        setHeaderStyle();
        setFooterStyle();
        setBoxedBackgrounds();
    };
    Jq('.drop-down-wrapper').perfectScrollbar({
        wheelSpeed: 50,
        minScrollbarLength: 20,
        suppressScrollX: true
    });
    Jq('.navbar-tools .dropdown').on('shown.bs.dropdown', function () {
        Jq(this).find('.drop-down-wrapper').scrollTop(0).perfectScrollbar('update');
    });
    var setColorScheme = function () {
        Jq('.icons-color a').bind('click', function () {
            Jq('.icons-color img').each(function () {
                Jq(this).removeClass('active');
            });
            Jq(this).find('img').addClass('active');
            if (Jq('#skin_color').attr("rel") == "stylesheet/less") {
                Jq('#skin_color').next('style').remove();
                Jq('#skin_color').attr("rel", "stylesheet");

            }
            Jq('#skin_color').attr("href", "assets/css/theme_" + Jq(this).attr('id') + ".css");

        });
    };
    var setBoxedBackgrounds = function () {
        Jq('.boxed-patterns a').bind('click', function () {
            if (Jq('body').hasClass('layout-boxed')) {
                var classes = Jq('body').attr("class").split(" ").filter(function (item) {
                    return item.indexOf("bg_style_") === -1 ? item : "";
                });
                Jq('body').attr("class", classes.join(" "));
                Jq('.boxed-patterns img').each(function () {
                    Jq(this).removeClass('active');
                });
                Jq(this).find('img').addClass('active');
                Jq('body').addClass(Jq(this).attr('id'));
            } else {
                alert('Select boxed layout');
            }
        });
    };
    var setLayoutStyle = function () {
        Jq('select[name="layout"]').change(function () {
            if (Jq('select[name="layout"] option:selected').val() == 'boxed')
                Jq('body').addClass('layout-boxed');
            else
                Jq('body').removeClass('layout-boxed');
        });
    };
    var setHeaderStyle = function () {
        Jq('select[name="header"]').change(function () {
            if (Jq('select[name="header"] option:selected').val() == 'default')
                Jq('body').addClass('header-default');
            else
                Jq('body').removeClass('header-default');
        });
    };
    var setFooterStyle = function () {
        Jq('select[name="footer"]').change(function () {
            if (Jq('select[name="footer"] option:selected').val() == 'fixed')
                Jq('body').addClass('footer-fixed');
            else
                Jq('body').removeClass('footer-fixed');
        });
    };
    var runColorPalette = function () {
        if (Jq('.colorpalette').length) {
            Jq('.colorpalette').colorPalette().on('selectColor', function (e) {
                Jq(this).closest('ul').prev('a').children('i').css('background-color', e.color).end().closest('div').prev('input').val(e.color);
                runActivateLess();
            });
        };
    };

    //function to activate Less style
    var runActivateLess = function () {
        Jq('		.icons-color img').removeClass('active');
        if (Jq('#skin_color').attr("rel") == "stylesheet") {
            Jq('#skin_color').attr("rel", "stylesheet/less").attr("href", "assets/less/styles.less");
            less.sheets.push(Jq('link#skin_color')[0]);
            less.refresh();
        };
        less.modifyVars({
            '@base': Jq('.color-base').val(),
            '@text': Jq('.color-text').val(),
            '@badge': Jq('.color-badge').val()
        });
    };

    //Window Resize Function
    var runWIndowResize = function (func, threshold, execAsap) {
        //wait until the user is done resizing the window, then execute
        Jq(window).clipresize(function () {
            runElementsPosition();
        });
    };
    //function to save user settings
    var runSaveSetting = function () {
        Jq('.save_style').bind('click', function () {
            var clipSetting = new Object;
            if (Jq('body').hasClass('rtl')) {
                clipSetting.rtl = true;
            } else {
                clipSetting.rtl = false;
            };
            if (Jq('body').hasClass('layout-boxed')) {
                clipSetting.layoutBoxed = true;
                Jq("body[class]").filter(function () {
                    var classNames = this.className.split(/\s+/);
                    for (var i = 0; i < classNames.length; ++i) {
                        if (classNames[i].substr(0, 9) === "bg_style_") {
                            clipSetting.bgStyle = classNames[i];
                        }
                    }

                });
            } else {
                clipSetting.layoutBoxed = false;
            };
            if (Jq('body').hasClass('header-default')) {
                clipSetting.headerDefault = true;
            } else {
                clipSetting.headerDefault = false;
            };
            if (Jq('body').hasClass('footer-fixed')) {
                clipSetting.footerDefault = false;
            } else {
                clipSetting.footerDefault = true;
            };
            if (Jq('#skin_color').attr('rel') == 'stylesheet') {
                clipSetting.useLess = false;
            } else if (Jq('#skin_color').attr('rel') == 'stylesheet/less') {
                clipSetting.useLess = true;
                clipSetting.baseColor = Jq('.color-base').val();
                clipSetting.textColor = Jq('.color-text').val();
                clipSetting.badgeColor = Jq('.color-badge').val();
            };
            clipSetting.skinClass = Jq('#skin_color').attr('href');

            Jq.cookie("clip-setting", JSON.stringify(clipSetting));

            var el = Jq('#style_selector_container');
            el.block({
                overlayCSS: {
                    backgroundColor: '#fff'
                },
                message: '<img src="assets/images/loading.gif" /> Just a moment...',
                css: {
                    border: 'none',
                    color: '#333',
                    background: 'none'
                }
            });
            window.setTimeout(function () {
                el.unblock();
            }, 1000);
        });
    };
    //function to load user settings
    var runCustomSetting = function () {
        if (Jq.cookie("clip-setting")) {
            var loadSetting = jQuery.parseJSON(Jq.cookie("clip-setting"));
            if (loadSetting.layoutBoxed) {

                Jq('body').addClass('layout-boxed');
                Jq('#style_selector select[name="layout"]').find('option[value="boxed"]').attr('selected', 'true');
            };
            if (loadSetting.headerDefault) {
                Jq('body').addClass('header-default');
                Jq('#style_selector select[name="header"]').find('option[value="default"]').attr('selected', 'true');
            };
            if (!loadSetting.footerDefault) {
                Jq('body').addClass('footer-fixed');
                Jq('#style_selector select[name="footer"]').find('option[value="fixed"]').attr('selected', 'true');
            };
            if (Jq('#style_selector').length) {
                if (loadSetting.useLess) {

                    Jq('.color-base').val(loadSetting.baseColor).next('.dropdown').find('i').css('background-color', loadSetting.baseColor);
                    Jq('.color-text').val(loadSetting.textColor).next('.dropdown').find('i').css('background-color', loadSetting.textColor);
                    Jq('.color-badge').val(loadSetting.badgeColor).next('.dropdown').find('i').css('background-color', loadSetting.badgeColor);
                    runActivateLess();
                } else {
                    Jq('.color-base').val('#FFFFFF').next('.dropdown').find('i').css('background-color', '#FFFFFF');
                    Jq('.color-text').val('#555555').next('.dropdown').find('i').css('background-color', '#555555');
                    Jq('.color-badge').val('#007AFF').next('.dropdown').find('i').css('background-color', '#007AFF');
                    Jq('#skin_color').attr('href', loadSetting.skinClass);
                };
            };
            Jq('body').addClass(loadSetting.bgStyle);
        } else {
            runDefaultSetting();
        };
    };
    //function to clear user settings
    var runClearSetting = function () {
        Jq('.clear_style').bind('click', function () {
            Jq.removeCookie("clip-setting");
            Jq('body').removeClass("layout-boxed header-default footer-fixed");
            Jq('body')[0].className = Jq('body')[0].className.replace(/\bbg_style_.*?\b/g, '');
            if (Jq('#skin_color').attr("rel") == "stylesheet/less") {
                Jq('#skin_color').next('style').remove();
                Jq('#skin_color').attr("rel", "stylesheet");

            }

            Jq('.icons-color img').first().trigger('click');
            runDefaultSetting();
        });
    };
    //function to restore user settings
    var runDefaultSetting = function () {
        Jq('#style_selector select[name="layout"]').val('default');
        Jq('#style_selector select[name="header"]').val('fixed');
        Jq('#style_selector select[name="footer"]').val('default');
        Jq('		.boxed-patterns img').removeClass('active');
        Jq('.color-base').val('#FFFFFF').next('.dropdown').find('i').css('background-color', '#FFFFFF');
        Jq('.color-text').val('#555555').next('.dropdown').find('i').css('background-color', '#555555');
        Jq('.color-badge').val('#007AFF').next('.dropdown').find('i').css('background-color', '#007AFF');
    };
    return {
        //main function to initiate template pages
        init: function () {
            runWIndowResize();
            runInit();
            runStyleSelector();
            runSearchInput();
            runElementsPosition();
            runToDoAction();
            runNavigationToggler();
            runNavigationMenu();
            runGoTop();
            runModuleTools();
            runDropdownEnduring();
            runTooltips();
            runPopovers();
            runPanelScroll();
            runShowTab();
            runAccordionFeatures();
            runCustomCheck();
            runColorPalette();
            runSaveSetting();
            runCustomSetting();
            runClearSetting();
        }
    };
}();