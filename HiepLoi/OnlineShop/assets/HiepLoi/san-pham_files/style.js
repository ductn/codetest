'use strict'
$('subMnLeft .accordion-item:first-child a').attr('aria-expanded','true');
$('subMnLeft .accordion-item:first-child a').removeClass('collapsed');

$('subMnLeft .accordion-item:first-child .accordion-heading + div').addClass('in');
$('subMnLeft .accordion-item:first-child .accordion-heading + div').attr('aria-expanded','true');

$('#back-to-top').on("click",function(){
	$('html, body').animate({scrollTop : 0},500);
	return false;
});

$(window).scroll(function() {
if ($(this).scrollTop() > 300) {
	$('#back-to-top').fadeIn();
} else {
	$('#back-to-top').fadeOut();
}
});

$(window).on('load resize', function() {
	var c = $('header.main-header').outerHeight(); 
	$('.content ').css('margin-top',c); 
	 
});

$(window).on('load scroll resize', function() {
	if ($(window).scrollTop() > 100) {
	$('.main-header').addClass('headroom--unpinned');
	} else {
	$('.main-header').removeClass('headroom--unpinned');
	}
}); 

$(document).ready(function(){ 
	//Insert see more button
	$('<button class="seeMore">Xem thêm</button>').insertAfter('.subSliderBanner .carousel-items');
	 
	 $('.subSliderBanner .seeMore').click(function(){ 
		 var text = $(this).text();
		 if(text === 'Xem thêm')
		 {	
			 $('.subSliderBanner .flickity-viewport').css('max-height','inherit'); 
		 	$(this).text('Thu gọn');
		 }
		 else
		 {
			 $('.subSliderBanner .flickity-viewport').css('max-height','100px');
			 $(this).text('Xem thêm');
		 }
		 
	 });
	


});

// For relate post page start
var divs = $(".subSliderBanner .carousel-items > div");
for(var i = 0; i < divs.length; i+=20) {
  divs.slice(i, i+20).wrapAll("<div class='lqd-column carousel-item w-100'></div>");
} 
//Button plus - minus
 var count = 1;
    var countEl = $(".quantity-input");
    function plus(){
        count++;
        countEl.val(count);
    }
    function minus(){
      if (count > 1) {
        count--;
        countEl.val(count);
      }  
	  else{
		  $('.down-btn').addClass('is-disabled');
	  }
    }
$('.up-btn').click(function(){
	plus(); 
	$('.down-btn').removeClass('is-disabled');
});
$('.down-btn').click(function(){
	minus();
}); 

//Scrolling to anchor
$(document).ready(function(){
  // Add smooth scrolling to all links
  $("a.seeDetail").on('click', function( ) { 
    var hash = this.hash; 
      $('html, body').animate({
        scrollTop: $(hash).offset().top - 150
      }, 800, function(){
    
        window.location.hash = hash;
      });
  });
});
 
//Submenu
$('.submenu-expander').click(function(){ 
	$(this).parents('li').find('.nav-item-children').toggleClass('is-show');
});

$(window).on('click', function(event)
{   event.stopPropagation(); 
   if($('.topheader .ld-module-dropdown').hasClass('in') || $('.search-form .mz-dropdown-menu').hasClass('is-show'))
   {
	   $('.topheader .ld-module-dropdown').removeClass('in'); 
	   $('.topheader .ld-module-trigger').attr('aria-expanded',false); 
	   $('.search-form .mz-dropdown-menu').removeClass('is-show');
   }
  
});
  //Search all
$('.search-form .dropdown-head').click(function(event){ 
	event.stopPropagation(); 
	$(this).parent().find('.mz-dropdown-menu').toggleClass('is-show');
	$('.topheader .ld-module-dropdown').removeClass('in'); 
	$('.topheader .ld-module-trigger').attr('aria-expanded',false); 
});


