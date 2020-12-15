$(function () {
    //dropdown menu option
    $(document).on("click", ".dropdown-menu .inner a", function (e) {
        e.preventDefault();
        var selectedOption = $(this).text();
        $(this).closest(".input-group").find(".filter-option-inner-inner").text(selectedOption);
        $(this).closest(".dropdown-menu").find("li").removeClass("selected active");
        $(this).closest(".dropdown-menu").find("li > a").removeClass("active");
        $(this).parent().addClass("selected active");
        $(this).closest(".dropdown-menu").find("li > a").addClass("active");
    })

})