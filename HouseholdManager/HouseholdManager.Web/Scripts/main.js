$(function () {
    $('.trigger-loading').click(function () {
        setTimeout(function () {
            $('.loading-main').removeClass("hide");
        }, 1000)
    });
});

var url = window.location;
$('ul.nav a[href="' + url + '"]').parents('.nav-link').addClass('nav-active');
$('ul.nav a').filter(function () {
    return this.href == url;
}).parents('.nav-link').addClass('nav-active');
