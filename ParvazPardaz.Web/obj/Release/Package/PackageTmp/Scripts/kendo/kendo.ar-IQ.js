/*
* Kendo UI Localization Project for v2012.3.1114
* Copyright 2012 Telerik AD. All rights reserved.
*
* Persian (fa-IR) Language Pack
*
* Project home : https://github.com/loudenvier/kendo-global
* Kendo UI home : http://kendoui.com
* Author : Bahman Nikkhahan
*
*
* This project is released to the public domain, although one must abide to the
* licensing terms set forth by Telerik to use Kendo UI, as shown bellow.
*
* Telerik's original licensing terms:
* -----------------------------------
* Kendo UI Web commercial licenses may be obtained at
* https://www.kendoui.com/purchase/license-agreement/kendo-ui-web-commercial.aspx
* If you do not own a commercial license, this file shall be governed by the
* GNU General Public License (GPL) version 3.
* For GPL requirements, please review: http://www.gnu.org/copyleft/gpl.html
*/
kendo.ui.Locale = "Arabic (ar-IQ)";
kendo.ui.ColumnMenu.prototype.options.messages =
$.extend(kendo.ui.ColumnMenu.prototype.options.messages, {

	/* COLUMN MENU MESSAGES 
	****************************************************************************/
	sortAscending: "ترتيب تصاعدي",
	sortDescending: "ترتيب تنازلي",
	filter: "فلتر",
	columns: "الأعمدة"
	/***************************************************************************/
});

kendo.ui.Groupable.prototype.options.messages =
$.extend(kendo.ui.Groupable.prototype.options.messages, {

	/* GRID GROUP PANEL MESSAGES 
	****************************************************************************/
	empty: "وضع أعمدة لتجميع هنا"
	/***************************************************************************/
});

kendo.ui.FilterMenu.prototype.options.messages =
$.extend(kendo.ui.FilterMenu.prototype.options.messages, {

	/* FILTER MENU MESSAGES 
	***************************************************************************/
	info: "تظهر فيها:",        // sets the text on top of the filter menu
	filter: "فلتر",      // sets the text for the "Filter" button
	clear: "نظيف",        // sets the text for the "Clear" button
	// when filtering boolean numbers
	isTrue: "صحیح", // sets the text for "isTrue" radio button
	isFalse: "غیر صحیح",     // sets the text for "isFalse" radio button
	//changes the text of the "And" and "Or" of the filter menu
	and: "و",
	or: "أو",
	selectValue: "-اختر-"
	/***************************************************************************/
});

kendo.ui.FilterMenu.prototype.options.operators =
$.extend(kendo.ui.FilterMenu.prototype.options.operators, {

	/* FILTER MENU OPERATORS (for each supported data type) 
	****************************************************************************/
	string: {
		eq: "مساو",
		neq: "مخالف",
		startswith: "بدأ",
		contains: "شمل",
		doesnotcontain: "لا شمل",
		endswith: "ينتهي"
	},
	number: {
		eq: "مساو",
		neq: "مخالف",
		gte: "أكبر من أو يساوي",
		gt: "أكبر من",
		lte: "أقل من أو يساوي",
		lt: "أصغر من"
	},
	date: {
		eq: "مساو",
		neq: "مخالف",
		gte: "أكبر من أو يساوي",
		gt: "أكبر من",
		lte: "أقل من أو يساوي",
		lt: "أصغر من"
	},
	enums: {
		eq: "مساو",
		neq: "مخالف"
	}
	/***************************************************************************/
});

kendo.ui.Pager.prototype.options.messages =
$.extend(kendo.ui.Pager.prototype.options.messages, {

	/* PAGER MESSAGES 
	****************************************************************************/
	display: "{0} - {1} از {2}",
	empty: "شيء غير موجود",
	page: "صفحة",
	of: "من {0}",
	itemsPerPage: "عدد العناصر في الصفحة",
	first: "أول",
	previous: "سابق",
	next: "تالی",
	last: "آخر",
	refresh: "إعادة"
	/***************************************************************************/
});

kendo.ui.Validator.prototype.options.messages =
$.extend(kendo.ui.Validator.prototype.options.messages, {

	/* VALIDATOR MESSAGES 
	****************************************************************************/
	required: "أدخل {0} مطلوب.",
	pattern: "أدخل {0} الصحيح",
	min: "{1} يجب أن تكون أكبر من {2}",
	max: "{1} يجب أن تكون أصغر من {2}",
	step: "{0} غیر صحیح.",
	email: "{0} ليس عنوان بريد إلكتروني صالح.",
	url: "{0} ليس URL صالح",
	date: "{0} ليس تاريخا صالحا"
	/***************************************************************************/
});

kendo.ui.ImageBrowser.prototype.options.messages =
$.extend(kendo.ui.ImageBrowser.prototype.options.messages, {

	/* IMAGE BROWSER MESSAGES 
	****************************************************************************/
	uploadFile: "تحميل الملف",
	orderBy: "الترتيب حسب",
	orderByName: "الترتيب حسب الاسم",
	orderBySize: "الترتيب حسب حجم",
	directoryNotFound: "مسار لم يتم العثور.",
	emptyFolder: "إفراغ مجلد",
	deleteFile: 'هل أنت متأكد "{0}" تمحى ؟',
	invalidFileType: "اختيار ملف \"{0}\" غير صالح . وتشمل الملفات المدعومة: {1}",
	overwriteFile: "هناك ملف يسمى \"{0}\" في الاتجاه المطلوب . مكتوب عليه ؟",
	dropFilesHere: "وضع الملفات الخاصة بك هنا"
	/***************************************************************************/
});

kendo.ui.Editor.prototype.options.messages =
$.extend(kendo.ui.Editor.prototype.options.messages, {

	/* EDITOR MESSAGES 
	****************************************************************************/
    bold: "جريء",
    italic: "قطري",
    underline: "أكد",
    strikethrough: "يتوسطه",
    superscript: "حرف فوقي",
    subscript: "عناوين فرعية",
    justifyCenter: "فرز مركز",
    justifyLeft: "فرز اليسار",
    justifyRight: "فرز الیمین",
    justifyFull: "فرز كامل",
    insertUnorderedList: "إدراج قائمة غير مرتبة",
    insertOrderedList: "قائمة مرتبة إدراج",
    indent: "زيادة المسافة",
    outdent: "سد الفجوة",
    createLink: "إنشاء رابط",
    unlink: "حذف الرابط",
    insertImage: "إدراج صورة",
    insertHtml: "إدراج HTML",
    fontName: "اسم الخط",
	fontNameInherit: "الخط",
	fontSize: "حجم الخط",
	fontSizeInherit: "حجم الخط",
	formatBlock: "سمات",
	foreColor: "اللون",
	backColor: "لون الخلفية",
	style: "الرسم",
	emptyFolder: "مسار فارغة",
	uploadFile: "تحميل الملف",
	orderBy: "الترتيب حسب:",
	orderBySize: "الترتيب حسب حجم",
	orderByName: "الترتیب حسب الاسم",
	invalidFileType: "اختيار ملف \"{0}\" غير صالح . و تشمل الملفات المدعومة: {1} .",
	deleteFile: 'هل أنت متأكد "{0}" تمحى ؟',
	overwriteFile: "هناك ملف يسمى \"{0}\" في الاتجاه المطلوب . مكتوب عليه ؟",
	directoryNotFound: "أعطيت ناش مسارد",
	imageWebAddress: "عنوان الصورة",
	imageAltText: "نص بديل",
	dialogInsert: "إدراج",
	dialogButtonSeparator: "أو",
	dialogCancel: "نصح بالعدول"
	/***************************************************************************/
});