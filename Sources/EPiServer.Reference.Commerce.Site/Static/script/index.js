$(document).ready(function () {
  $("#slick").slick({
    dots: false,
    infinity: true,
    slidesToShow: 1,
    arrows: false,
    autoplay: true,
  });

  $("#slick").on(
    "beforeChange",
    function (event, slick, currentSlide, nextSlide) {
      $(".extra-dots-item").removeClass("active");
      $(`.extra-dots-item[data-extra=${nextSlide}]`).addClass("active");
    }
  );

  $(".extra-dots-item").click(function (e) {
    e.preventDefault();
    $("#slick").slick("slickGoTo", $(this).data("extra"));
  });

	$(".header-search-btn").click(function() {
		const inputGroup = $(this).prev();
		if (!inputGroup.hasClass('open')) {
			inputGroup.addClass('open');
		}
	});

	$(window).scroll(function() {
		if ($(this).scrollTop() > 0) {
			$('.header').addClass('fixed');
		} else {
			$('.header').removeClass('fixed');
		}
	})
});
