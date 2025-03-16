
$(function () {
    $('.main-slider').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 3000,
        arrows: true,
        dots: true,
        adaptiveHeight: true,
        prevArrow: $('.prev'),
        nextArrow: $('.next'),
    });
});

