$(function () {

    $(document).on("click", ".settings-from .dropdown-toggle", function () {
        $(this).next(".dropdown-menu").toggle();
        
    })
    $(document).on("click", ".settings-from .dropdown-menu li a", function (e) {
        e.preventDefault();
    
        var closeDropdownToggle = $(this).parent().parent().prev(".dropdown-toggle");
        var clsoeSelectTag = closeDropdownToggle.find(".selected-tag")
        //Change selected
        clsoeSelectTag.text(`${$(this).text()}`)
        closeDropdownToggle.next().hide();

    })

    //Date validation
    $(document).on("click", ".invoice-month .month-scroll-list li", function () {
        var monthName = $(this).find("a").attr("data-value");
        var year = $("#year").text().trim();
        var day = $("#dayOfBirth").text().trim();
        var isExists = $(`.invoice-day .day-scroll-list li a[data-value="31"]`).closest("li").length;
        var isFeb29Exists = $(`.invoice-day .day-scroll-list li a[data-value="29"]`).closest("li").length;
        if (isExists == 0) {
            var day30 = $(`<li> <a data-value="30">  <span class="text">30</span></a></li>`);
            var day31 = $(`<li> <a data-value="31"> <span class="text">31</span></a></li>`);
            if (isFeb29Exists == 0) {
                var day29 = $(`<li> <a data-value="29"> <span class="text">29</span></a></li>`);
                $(".invoice-day .day-scroll-list").append(day29);
            }
            if ($(`.invoice-day .day-scroll-list li a[data-value="30"]`).closest("li").length == 0) {
                $(".invoice-day .day-scroll-list").append(day30);
            }

            if ($(`.invoice-day .day-scroll-list li a[data-value="31"]`).closest("li").length == 0) {
                $(".invoice-day .day-scroll-list").append(day31);
            }
          
        }
        if (monthName == "Fevral") {
            if (!(((parseInt(year) % 4 == 0) && (parseInt(year) % 100 != 0)) || (parseInt(year) % 400 == 0))) {
                $(`.invoice-day .day-scroll-list li a[data-value="29"]`).closest("li").remove();
                if (day == "29" || day == "30" || day == "31") {

                    $("#dayOfBirth").text("28");
                }
            }
            else {

                if (day == "30" || day == "31") {
                    $("#dayOfBirth").text("29");
                }

            }
            $(`.invoice-day .day-scroll-list li a[data-value="31"]`).closest("li").remove();
            $(`.invoice-day .day-scroll-list li a[data-value="30"]`).closest("li").remove();
         
        }
        else if (monthName == "Aprel" || monthName == "İyun" || monthName == "Sentyabr" || monthName == "Noyabr") {
            if ($("#dayOfBirth").text().trim() == "31") {
                $("#dayOfBirth").text("30");
            }
            if ($(`.invoice-day .day-scroll-list li a[data-value="31"]`).closest("li").length > 0) {
                $(`.invoice-day .day-scroll-list li a[data-value="31"]`).closest("li").remove();
            }
            if ($(`.invoice-day .day-scroll-list li a[data-value="30"]`).closest("li").length == 0) {

                $(".invoice-day .day-scroll-list").append(day30);
            }
        }


    })
    $(document).on("click", ".invoice-year .year-scroll-list li", function () {
        var year = $(this).find("a").attr("data-value");
        var isExists = $(`.invoice-day .day-scroll-list li a[data-value="29"]`).closest("li").length;
        var monthName = $("#month-name").text().trim();
        var day = $("#dayOfBirth").text().trim();
        if (((parseInt(year) % 4 == 0) && (parseInt(year) % 100 != 0)) || (parseInt(year) % 400 == 0)) {
           
            if (monthName == "Fevral") {
              
                if (isExists == 0) {
                    
                    var day29 = $(`<li> <a data-value="29"> <span class="text">29</span></a></li>`);
                    $(".invoice-day .day-scroll-list").append(day29);
                }

            }
        }
        else {
            if (monthName == "Fevral") {
                if (isExists == 1) {
                    $(`.invoice-day .day-scroll-list li a[data-value="29"]`).closest("li").remove();
                    if (day == "29") {

                    $("#dayOfBirth").text("28");
                    }
                }

            }

        }
    })
    //Phone input mask
    $(function () {
        $("#phone").inputmask("(\\099\\)999-99-99", {

            greedy: true,
            showMaskOnFocus: true,
            showMaskOnHover: false
        });
    })
})