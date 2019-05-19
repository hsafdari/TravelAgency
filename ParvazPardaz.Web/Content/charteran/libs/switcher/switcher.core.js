/*
 * Smart Demo Switcher v2.0
 * http://www.smartplugins.info/plugin/javascript/smart-demo-switcher/
 * 
 * Copyright 2008 - 2014 Milan Petrovic (email: milan@gdragon.info)
 *
 * http://www.dev4press.com
 * http://www.millan.rs
 *
 */

/*jslint regexp: true, nomen: true, undef: true, sloppy: true, eqeq: true, vars: true, white: true, plusplus: true, maxerr: 50, indent: 4 */

var smartDemoSwitcher;

;(function ($, window, document, undefined) {
    smartDemoSwitcher = function() {
        return new smartDemoSwitcher.Core();
    };

    smartDemoSwitcher.Base = Base.extend({
        version: "2.0",

        constructor: function(_default, options) {
            if (typeof _default !== "object") {
                _default = {};
            }

            if (typeof options !== "object") {
                options = {};
            }

            this.setOptions($.extend(true, {}, _default, options));
        },
        setOption: function(index, value) {
            this[index] = value;
        },
        getOption: function(index) {
            if (this[index]) {
                return this[index];
            }

            return false;
        },
        setOptions: function(options) {
            var key;

            for (key in options) {
                if (typeof options[key] !== "undefined") {
                    this.setOption(key, options[key]);
                }
            }
        },
        getOptions: function() {
            return this;
        },
        callback: function(method) {
            if (typeof method === "function") {
                var args = [], i;

                for (i = 1; i <= arguments.length; i++) {
                    if (arguments[i]) {
                        args.push(arguments[i]);
                    }
                }

                method.apply(this, args);
            }
        }
    });

    smartDemoSwitcher.Core = smartDemoSwitcher.Base.extend({
        $mobile: false,
        $load: false,
        $skin: false,
        $cookie: false,
        $obj: false,
        $events: false,

        constructor: function() {
            this._isMobile();

            $("body").append("<div id='smart-demo-switcher'></div>");

            this.$obj = $("#smart-demo-switcher");

            this.$load = this._loadLoader();
            this.$cookie = this._loadCookies(this.$load.cookies);

            this.$load.core = this;

            this.$skin = this._loadSkin(this.$load.skin, this.$load.display, this.$cookie.defaults());

            this.$cookie.init = false;
        },
        _loadLoader: function() {
            return smartDemoSwitcher.Loader(this);
        },
        _loadSkin: function(name, options, defaults) {
            if (!smartDemoSwitcher[name + "Skin"]) {
                name = "Default";
            }

            return new smartDemoSwitcher[name + "Skin"](this, options, defaults);
        },
        _loadCookies: function(options) {
            return new smartDemoSwitcher.Cookie(this, options);
        },
        _isMobile: function() {
            var devices = "3ds|android|bada|bb10|hpwos|iemobile|kindle fire|opera mini|opera mobi|opera tablet|rim|silk|wiiu|ipad|ipod|iphone";
            var devicesRegEx = new RegExp(devices, "gi");

            this.$mobile = devicesRegEx.test(navigator.userAgent);
        },
        saveToCookie: function(type, name, value) {
            this.$cookie.store(type, name, value);
        }
    });

    smartDemoSwitcher.Skin = smartDemoSwitcher.Base.extend({
        core: false,
        defaults: false,

        name: "",
        code: "",
        css: "",

        $classes: {
            prefixSkin: "sds-skin-",
            prefixStyle: "sds-style-",
            prefixLocation: "sds-location-",
            prefixPosition: "sds-position-",
            prefixStatus: "sds-status-",
            buttonID: "sds-slide-button",
            formID: "sds-slide-form",
            formHeader: "sds-form-header",
            formNotice: "sds-form-notice",
            rtypePrefix: "sds-rtype-",
            stylesheetsID: "sds-form-sheets",
            stylesheets: "sds-stylesheets",
            stylesheetsPrefix: "sds-stylesheets-",
            stylesheet: "sds-stylesheet",
            stylesheetInner: "sds-stylesheet-inner",
            stylesheetActive: "sds-stylesheet-active",
            shapePrefix: "sds-shape-",
            variantsID: "sds-form-variants",
            variants: "sds-variants",
            variantsPrefix: "sds-variants-",
            variant: "sds-variant",
            variantInner: "sds-variant-inner",
            variantActive: "sds-variant-active"
        },

        style: "light",
        location: "left",
        position: "fixed",
        border: 2,
        buttonContent: '<i class="fa fa-arrows-alt"></i>',
        buttonContentClose: '<i class="fa fa-arrows-alt"></i>',
        buttonTop: "112px",
        buttonWidth: 44,
        buttonHeight: 40,
        formTop: "90px",
        formWidth: 220,
        formHeader: true,
        formHeaderContent: "<img src='images/brand-logo.png' />",
        formNotice: true,
        formNoticeContent: "",
        initOpen: false,
        extraClass: "",

        animateOpenDuration: 600,
        animateOpenEffect: "swing",
        animateCloseDuration: 300,
        animateCloseEffect: "swing",

        constructor: function(core, options, defaults) {
            this.base(options);
            this.core = core;
            this.defaults = defaults;

            this.init();

            this.render();
            this.events();
        },
        init: function() { },
        refresh: function() { },
        events: function() {
            var $t = this, run, value, block, sheet, variant;

            $("#" + this.$classes.buttonID).click(function(e) {
                e.preventDefault();

                var open = $(this).parent().hasClass($t.$classes.prefixStatus + "open"),
                    marginButton = $t.formWidth - $t.border, marginForm = $t.formWidth;

                if (open) {
                    if ($t.location === "left") {
                        $("#" + $t.$classes.buttonID).animate({"marginLeft": "-=" + marginButton + "px"}, $t.animateCloseDuration, $t.animateCloseEffect);
                        $("#" + $t.$classes.formID).animate({"marginLeft": "-=" + marginForm + "px"}, $t.animateCloseDuration, $t.animateCloseEffect);
                    } else {
                        $("#" + $t.$classes.buttonID).animate({"marginRight": "-=" + marginButton + "px"}, $t.animateCloseDuration, $t.animateCloseEffect);
                        $("#" + $t.$classes.formID).animate({"marginRight": "-=" + marginForm + "px"}, $t.animateCloseDuration, $t.animateCloseEffect);
                    }

                    $("#" + $t.$classes.buttonID + " span").html($t.buttonContent);
                } else {
                    if ($t.location === "left") {
                        $("#" + $t.$classes.buttonID).animate({"marginLeft": "+=" + marginButton + "px"}, $t.animateOpenDuration, $t.animateOpenEffect);
                        $("#" + $t.$classes.formID).animate({"marginLeft": "+=" + marginForm + "px"}, $t.animateOpenDuration, $t.animateOpenEffect);
                    } else {
                        $("#" + $t.$classes.buttonID).animate({"marginRight": "+=" + marginButton + "px"}, $t.animateOpenDuration, $t.animateOpenEffect);
                        $("#" + $t.$classes.formID).animate({"marginRight": "+=" + marginForm + "px"}, $t.animateOpenDuration, $t.animateOpenEffect);
                    }

                    $("#" + $t.$classes.buttonID + " span").html($t.buttonContentClose);
                }

                if (open) {
                    $(this).parent()
                           .removeClass($t.$classes.prefixStatus + "open")
                           .addClass($t.$classes.prefixStatus + "closed");
                } else {
                    $(this).parent()
                           .removeClass($t.$classes.prefixStatus + "closed")
                           .addClass($t.$classes.prefixStatus + "open");
                }
            });

            $("." + this.$classes.stylesheets + " select").change(function() {
                var value = $(this).val(),
                    sheet = $(this).parent().data("sheet");

                $t.core.$load.switchStylesheet(sheet, value);
            });

            $("." + this.$classes.stylesheetInner).click(function(e) {
                e.preventDefault();

                if (!$(this).hasClass($t.$classes.stylesheetActive)) {
                    value = $(this).data("sheet");
                    block = $(this).parent().parent();
                    sheet = block.data("sheet");

                    block.find("." + $t.$classes.stylesheetInner)
                         .removeClass($t.$classes.stylesheetActive);

                    $(this).addClass($t.$classes.stylesheetActive);

                    $t.core.$load.switchStylesheet(sheet, value);
                }
            });

            $("." + this.$classes.variants + " select").change(function() {
                var value = parseInt($(this).val()),
                    variant = $(this).parent().data("variant");

                $t.core.$load.switchVariant(variant, value);
            });

            $("." + this.$classes.variantInner).click(function(e) {
                e.preventDefault();

                if (!$(this).hasClass($t.$classes.variantActive)) {
                    value = $(this).data("variant");
                    block = $(this).parent().parent();
                    variant = block.data("variant");

                    block.find("." + $t.$classes.variantInner)
                         .removeClass($t.$classes.variantActive);

                    $(this).addClass($t.$classes.variantActive);

                    $t.core.$load.switchVariant(variant, value);
                }
            });

            $("." + this.$classes.stylesheets + " select, ." + this.$classes.variants + " select").trigger("change");

            run = $("." + this.$classes.stylesheetInner + "." + this.$classes.stylesheetActive);

            if (run.length > 0) {
                value = run.data("sheet");
                block = run.parent().parent();
                sheet = block.data("sheet");

                this.core.$load.switchStylesheet(sheet, value);
            }

            run = $("." + this.$classes.variantInner + "." + this.$classes.variantActive);

            if (run.length > 0) {
                value = run.data("variant");
                block = run.parent().parent();
                variant = block.data("variant");

                this.core.$load.switchVariant(variant, value);
            }

            this.core.$events = true;
        },
        render: function() {
            this.core.$obj.addClass(this.$classes.prefixSkin + this.code);
            this.core.$obj.addClass(this.$classes.prefixLocation + this.location);
            this.core.$obj.addClass(this.$classes.prefixPosition + this.position);
            this.core.$obj.addClass(this.$classes.prefixStatus + (this.initOpen ? "open" : "closed"));

            if (this.style !== "") {
                this.core.$obj.addClass(this.$classes.prefixStyle + this.style);
            }

            if (this.css !== "") {
                this.core.$obj.addClass(this.css);
            }

            if (this.extraClass !== "") {
                this.core.$obj.addClass(this.extraClass);
            }

            this.core.$obj.append(this._renderButton());
            this.core.$obj.append(this._renderForm());

            var $t = this;

            $("." + this.$classes.stylesheets + "." + this.$classes.rtypePrefix + "box").each(function() {
                var code = $(this).data("sheet"),
                    sheet = $t.core.$load.stylesheets[code],
                    factor = sheet.boxFactor ? sheet.boxFactor : 1;
                    
                $("." + $t.$classes.stylesheet, this).each(function() {
                    $(this).height($(this).width() * factor);
                });

                $("." + $t.$classes.stylesheetInner, this).each(function() {
                    var height = $(this).width() * factor;

                    $(this).height(height);
                    $("span", this).height(height);
                });
            });

            $("." + this.$classes.variants + "." + this.$classes.rtypePrefix + "box").each(function() {
                var code = $(this).data("variant"),
                    variant = $t.core.$load.variants[code],
                    factor = variant.boxFactor ? variant.boxFactor : 1;

                $("." + $t.$classes.variant, this).each(function() {
                    $(this).height($(this).width() * factor);
                });

                $("." + $t.$classes.variantInner, this).each(function() {
                    var height = $(this).width() * factor;

                    $(this).height(height);
                    $("span", this).height(height);
                });
            });

            $(window).bind("load resize orientationchange", {$t: this}, this.refresh);
        },
        _getDefault: function(type, code, sheet) {
            var def = sheet.default;

            if (this.defaults[type]) {
                if (this.defaults[type][code]) {
                    def = this.defaults[type][code];
                }
            }

            return def;
        },
        _renderButton: function() {
            var render = "", margin = this.initOpen ? this.formWidth - this.border : 0,
                buttonContent = this.initOpen ? this.buttonContentClose : this.buttonContent,
                style = 'top: ' + this.buttonTop + '; width: ' + this.buttonWidth + 'px; height: ' + this.buttonHeight + 'px; margin-' + this.location + ': ' + margin + 'px',
                styleSpan = 'width: ' + (this.buttonWidth - this.border) + 'px; height: ' + (this.buttonHeight - 2 * this.border) + 'px';

            render+= '<div id="' + this.$classes.buttonID + '" style="' + style + '">';
            render+= '<span style="' + styleSpan + '">' + buttonContent + "</span>";
            render+= "</div>";

            return render;
        },
        _renderForm: function() {
            var render = "", margin = this.initOpen ? 0 : 0 - this.formWidth,
                style = "top: " + this.formTop + "; width: " + this.formWidth + "px; margin-" + this.location + ": " + margin + "px";
            
            render+= '<div id="' + this.$classes.formID + '" style="' + style + '">';

            if (this.formHeader) {
                render+= this._renderFormHeader();
            }

            if (this.core.$load.stylesheets !== false) {
                render+= '<div id="' + this.$classes.stylesheetsID + '">';

                var $ts = this;
                $.each(this.core.$load.stylesheets, function(idx, obj){
                    render+= $ts._renderStylesheets(idx, obj);
                });

                render+= "</div>";
            }

            if (this.core.$load.variants !== false) {
                render+= '<div id="' + this.$classes.variantsID + '">';

                var $tv = this;
                $.each(this.core.$load.variants, function(idx, obj){
                    render+= $tv._renderVariants(idx, obj);
                });

                render+= "</div>";
            }

            if (this.formNotice) {
                render+= this._renderFormNotice();
            }

            render+= "</div>";

            return render;
        },
        _renderFormHeader: function() {
            return '<div class="' + this.$classes.formHeader + '">' + this.formHeaderContent + '</div>';
        },
        _renderFormNotice: function() {
            return '<div class="' + this.$classes.formNotice + '">' + this.formNoticeContent + '</div>';
        },
        _renderSwitchBox: function(id, sheet, active, clsInner, clsActive, dataAttr) {
            var render = '', classes = clsInner, i, 
                span = sheet.span ? sheet.span : "colors";

            if (active === true) {
                classes+= " " + clsActive;
            }

            render+= '<div class="' + classes + '" data-' + dataAttr + '="' + id + '" title="' + sheet.name + '">';

            if (span === "colors") {
                var colors = sheet.colors.length, width = 100 / colors;

                for (i = 0; i < colors; i++) {
                    render+= '<span style="background: ' + sheet.colors[i] + '; width: ' + width + '%;"></span>';
                }
            } else if (span === "css") {
                render+= '<span class="' + sheet.css + '" style="width: 100%;"></span>';
            }

            render+= '</div>';

            return render;
        },
        _renderSwitchLink: function(id, sheet, active, clsInner, clsActive, dataAttr) {
            var render = "", classes = clsInner, styles = "";

            if (active === true) {
                classes+= " " + clsActive;
            }

            if (sheet.background) {
                styles+= "background: " + sheet.background + ";";
            }

            if (sheet.color) {
                styles+= "color: " + sheet.color + ";";
            }

            render+= '<div class="' + classes + '" data-' + dataAttr + '="' + id + '">';
            render+= '<span style="' + styles + '">' + sheet.name + '</span>';
            render+= '</div>';

            return render;
        },
        _renderSwitchSelect: function(sheets, selected) {
            var render = "<select>";

            for (i = 0; i < sheets.length; i++) {
                var active = selected === sheets[i].class ? ' selected="selected"' : "";

                render+= '<option value="' + i + '"' + active + '>' + sheets[i].name + '</option>';
            }

            render+= "</select>";

            return render;
        },
        _renderStylesheets: function(code, sheet) {
            var columns = sheet.columns, sheets = sheet.list, 
                render = '', width = 100 / columns, i,
                def = this._getDefault("stylesheets", code, sheet),
                shape = sheet.boxShape ? sheet.boxShape : "sqaure",
                rType = sheet.type ? sheet.type : "box";

            render+= '<div data-sheet="' + code + '" class="' + this.$classes.stylesheets + " " + this.$classes.rtypePrefix + rType + " " + this.$classes.stylesheetsPrefix + code + '">';

            if (sheet.title) {
                render+= sheet.titleContent;
            }

            if (rType === "select") {
                render+= this._renderSwitchSelect(sheets, def);
            } else {
                for (i = 0; i < sheets.length; i++) {
                    var active = def === sheets[i].file,
                        classes = this.$classes.stylesheet;

                    if (rType === "box") {
                        classes+= " " + this.$classes.shapePrefix + shape;
                    }

                    render+= '<div class="' + classes + '" style="width: ' + width + '%">';

                    if (rType === "box") {
                        render+= this._renderSwitchBox(i, sheets[i], active, this.$classes.stylesheetInner, this.$classes.stylesheetActive, "sheet");
                    } else {
                        render+= this._renderSwitchLink(i, sheets[i], active, this.$classes.stylesheetInner, this.$classes.stylesheetActive, "sheet");
                    }

                    render+= '</div>';
                }
            }

            render+= '</div>';

            return render;
        },
        _renderVariants: function(code, variant) {
            var columns = variant.columns, sheets = variant.list, 
                render = '', width = 100 / columns, i,
                def = this._getDefault("variants", code, variant),
                shape = variant.boxShape ? variant.boxShape : "square",
                rType = variant.type ? variant.type : "box";

            render+= '<div data-variant="' + code + '" class="' + this.$classes.variants + " " + this.$classes.rtypePrefix + rType + " " + this.$classes.variantsPrefix + code + '">';

            if (variant.title) {
                render+= variant.titleContent;
            }

            if (rType === "select") {
                render+= this._renderSwitchSelect(sheets, def);
            } else {
                for (i = 0; i < sheets.length; i++) {
                    var active = def === sheets[i].class,
                        classes = this.$classes.variant;

                    if (rType === "box") {
                        classes+= " " + this.$classes.shapePrefix + shape;
                    }

                    render+= '<div class="' + classes + '" style="width: ' + width + '%">';

                    if (rType === "box") {
                        render+= this._renderSwitchBox(i, sheets[i], active, this.$classes.variantInner, this.$classes.variantActive, "variant");
                    } else {
                        render+= this._renderSwitchLink(i, sheets[i], active, this.$classes.variantInner, this.$classes.variantActive, "variant");
                    }

                    render+= '</div>';
                }
            }

            render+= '</div>';

            return render;
        }
    });

    smartDemoSwitcher.DefaultSkin = smartDemoSwitcher.Skin.extend({
        name: "Default",
        code: "default"
    });

    smartDemoSwitcher.Cookie = smartDemoSwitcher.Base.extend({
        core: false,
        init: true,

        $cookie: false,

        active: true,
        expires: 365,
        path: "/",
        name: "sds-cookie-storage",

        constructor: function(core, options) {
            this.base(options);
            this.core = core;

            this.initCookie();
        },
        initCookie: function() {
            if ($.cookie) {
                $.cookie.json = true;

                this.$cookie = $.cookie(this.name);

                if (this.$cookie == undefined) {
                    this.$cookie = {
                        stylesheets: {},
                        variants: {}
                    };
                }
            } else {
                this.active = false;
            }
        },
        defaults: function() {
            var def = {}, list = this.core.$load, item;

            if (this.active) {
                $.each(this.$cookie, function(type, obj) {
                    def[type] = {};

                    item = type === "stylesheets" ? "file" : "class";

                    $.each(obj, function(name, id){
                        if (list[type][name].list[id] && list[type][name].list[id][item]) {
                            def[type][name] = list[type][name].list[id][item];
                        }
                    });
                });
            }

            return def;
        },
        store: function(type, name, value) {
            if (this.init === false && this.active) {
                this.$cookie[type][name] = value;

                $.cookie(this.name, this.$cookie, {expires: this.expires, path: this.path})
            }
        }
    });

    smartDemoSwitcher.Load = smartDemoSwitcher.Base.extend({
        core: false,

        $variantClasses: {},

        skin: "Default",

        display: {},
        cookies: {},
        stylesheets: false,
        variants: false,

        constructor: function(core) {
            this.core = core;
        },
        initVariants: function() {
            if (this.variants !== false) {
                var i, $txr = this;

                $.each(this.variants, function(idx, obj){
                    $txr.$variantClasses[idx] = [];

                    for (i = 0; i < obj.list.length; i++) {
                        if (obj.list[i].class) {
                            $txr.$variantClasses[idx].push(obj.list[i].class);
                        }
                    }
                });
            }
        },
        switchStylesheet: function(code, id) {
            var current = this.stylesheets[code],
                sheet = current.list[id];

            $(current.selector).attr("href", sheet.file);

            this.core.saveToCookie("stylesheets", code, id);

            if (this.core.$events && current.onSwitch) {
                this.callback(current.onSwitch, current, sheet, code, id);
            }
        },
        switchVariant: function(code, id) {
            var current = this.variants[code],
                variant = current.list[id];

            if (!this.$variantClasses.hasOwnProperty(code)) {
                this.initVariants();
            }

            if ((variant.method && variant.method === "class") || !variant.method) {
                $(current.selector).removeClass(this.$variantClasses[code].join(" "))
                                   .addClass(variant.class);
            } else if (variant.method && variant.method === "call" && variant.call) {
                this.callback(variant.call, current, variant, code, id);
            } else {
                this.customVariant(current, variant, code, id);
            }

            this.core.saveToCookie("variants", code, id);

            if (this.core.$events && current.onSwitch) {
                this.callback(current.onSwitch, current, variant, code, id);
            }
        },
        customVariant: function(current, variant, code, id) { }
    });

    String.prototype.ucfirst = function() {
        return this.substr(0, 1).toUpperCase() + this.substr(1);
    };

    String.prototype.pad = function(length, character) {
        var tmp = this;

        if (!character) {
            character = "0";
        }

        while (tmp.length < length) {
            tmp = character + tmp;
        }

        return tmp;
    };

    $.fn.smartDemoSwitcher = function() {
        return new smartDemoSwitcher();
    };
})(jQuery, window, document);
