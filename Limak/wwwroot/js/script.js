$(function () {
    //Banner slider
    var owlBSlider = $('#banner-slider');
    owlBSlider.owlCarousel({
        items:1,
        loop:true,
        margin:10,
        autoplay:true,
        autoplayTimeout:4000,
        autoplayHoverPause:true
    });
    
    //logo slider
    var owlLogo = $('#logo-slider');
    owlLogo.owlCarousel({
        items:5,
        loop:true,
        margin:10,
        autoplay:true,
        autoplayTimeout:4000,
        autoplayHoverPause:true,
        responsive: {
            0: {
                items: 1
            },
            500: {
                items: 2
            },
            700: {
                items: 3
            },
            1000: {
                items: 4
            },
            1300: {
                items: 5
            },
            2000: {
                items: 5
            }
        }
    });
   
})