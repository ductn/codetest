
//Mega menu left
$(document).ready(function () {
    var h_slider_pro = $('.slider-home .flickity-viewport img').height();

    //console.log(h_slider_pro);
    $('.WrapMnLeftPro').css("height", h_slider_pro);

});

var w_box_filter = $('.box-filter').width();
var screenWidth = $(window).width();
$('.filter-show.show-total').css("width", w_box_filter);

var w_filter_show = $('.filter-show.filter_item').width();
$('.range-slider').css("width", w_filter_show);

$('.filter-total').click(function (e) {
    e.stopPropagation();

    $('.filter-show.show-total').toggle();
    $(this).toggleClass('activeShow');
    if ($(this).hasClass('activeShow')) {
        $('.filter-total .arrow-filter').addClass('jsTitle_afShow');
    } else {
        $('.filter-total .arrow-filter').removeClass('jsTitle_afShow');
    }

    $('.filter-show.filter_item').hide();
    $('.filter-item').removeClass('activeShow');
});
$('.filter-item').click(function (e) {
    e.stopPropagation();
    $('.filter-show.show-total').hide();
    $('.filter-total').removeClass('activeShow');
    var idInfo = $(this).data('id') + "-info";
    var info = $('#' + idInfo);

    //console.log(idInfo);
    //console.log(info);

    $(info).toggle();

    $(this).toggleClass('activeShow');
   

    if ($(this).hasClass('activeShow')) {
        $('.arrow-filter').addClass('jsTitle_afShow');
        
    } else {
        $('.arrow-filter').removeClass('jsTitle_afShow');
    }

    $('.filter-show.filter_item').not($(info)).hide();
    $('.filter-show.filter_item').not($(info)).parent().removeClass('activeShow');

   
    if (screenWidth <= 1024) {
        $('.filter-show.filter_item').css("width", w_box_filter);
        var w_idInfo = $(this).position();

        $('.filter-show.filter_item').removeClass("filter-show-right");
        $('.short-width').css("width", w_box_filter);
        $('.filter-show.filter_item').css("width", w_box_filter);
        $('.filter-show.filter_item').css({
            "width": w_box_filter,
            "left": - w_idInfo.left
        });
        
    }

         
    $('.filter-show.filter_item').each(function () {

        $('.c-btnbox').click(function (e) {
            e.preventDefault();
            e.stopPropagation();
            $(this).addClass('check');
            $('.btn-filter-close, .filter-button').css('display','flex');
        })
        $('.btn-filter-close').click(function () {
            e.preventDefault();
            e.stopPropagation();
            $('.c-btnbox').removeClass('check')
        })
    });


});

//hide filter-show when outsite click
$(document).click(function (e) {
    $('.filter-show.filter_item').hide();
    $('.filter-show.show-total').hide();
});

$('.menu > li').click(function () {
    $('.menu > li > .megadrop').toggle();
})  

//rangeSlider
var rangeSlider = function () {
    var slider = $('.range-slider'),
        range = $('.range-slider__range'),
        value = $('.range-slider__value');

    slider.each(function () {

        value.each(function () {
            var value = $(this).prev().attr('value');
            $(this).html(value);
        });

        range.on('input', function () {
            $(this).next(value).html(this.value);
        });
    });
};

rangeSlider();

