$(document).ready(function () {
  // init carousel
  $("#slick").slick({
    dots: false,
    infinity: true,
    slidesToShow: 1,
    arrows: false,
    autoplay: true,
  });

  // Event slick carousel before change
  $("#slick").on(
    "beforeChange",
    function (event, slick, currentSlide, nextSlide) {
      $(".extra-dots-item").removeClass("active");
      $(`.extra-dots-item[data-extra=${nextSlide}]`).addClass("active");
    }
  );

  // Event click extra dots of carousel
  $(".extra-dots-item").click(function (e) {
    e.preventDefault();
    $("#slick").slick("slickGoTo", $(this).data("extra"));
  });

  // Click to show input header search
	$(".header-search-btn").click(function() {
		const inputGroup = $(this).prev();
		if (!inputGroup.hasClass('open')) {
			inputGroup.addClass('open');
		}
	});

  // Window scroll event
	$(window).scroll(function() {
		if ($(this).scrollTop() > 0) {
			$('.header').addClass('fixed');
		} else {
			$('.header').removeClass('fixed');
		}
	});

  // Trigger sort product list
  $('.product-list .btn-sort').click(function() {
    $(this).toggleClass('degrease');
  });

  // init prefer carousel
  $(".prefer-js-slick").slick({
    dots: false,
    infinity: true,
    slidesToShow: 3,
    slidesToScroll: 3,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2,
          infinite: true,
          dots: false
        }
      },
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          infinite: true,
          dots: false
        }
      }
    ]
  });
});
