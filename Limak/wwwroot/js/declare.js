$(function () {
    $(document).on("click", ".dropdown-toggle", function (e) {
        $(this).next(".dropdown-menu").toggleClass("dropdown-menu-active");

    })
    $(document).on("click", ".dropdown-menu li a", function (e) {
        var closeDropdownToggle = $(this).parent().parent().prev(".dropdown-toggle");
        var clsoeSelectTag = closeDropdownToggle.find(".selected-tag")
        //Change selected
        clsoeSelectTag.text(`${$(this).text()}`)
        closeDropdownToggle.next().hide();

    })
    //Increase package count
    $(document).on("click", ".btn-increase-pack", function () {
        var package_count = $(this).parent().prev(".package-count");
        var package_countVal = package_count.val();
        package_countVal++;
        package_count.val(package_countVal)

    })
    //Decrease package count
    $(document).on("click", ".btn-decrease-pack", function () {
        var package_count = $(this).parent().next(".package-count");
        var package_countVal = package_count.val();
        if (package_countVal < 1 || package_countVal == 1) {
            package_package_countValcount = 1
        }
        else {
            package_countVal--;
        }
        package_count.val(package_countVal)
    })

 
    //Date validation
    $(document).on("click", ".invoice-month .month-scroll-list li", function () {
        var monthName = $(this).find("a").attr("data-value");
        var yearoforder = $(this).closest(".date-container").find(".invoice-year .selected-tag").text().trim();
       
        var day = $(this).closest(".date-container").find(".invoice-day .selected-tag").text().trim();
        var isExists = $(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="31"]`).closest("li").length;
        var isFeb29Exists = $(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="29"]`).closest("li").length;
        var day30 = $(`<li> <a data-value="30">  <span class="text">30</span></a></li>`);
        var day31 = $(`<li> <a data-value="31"> <span class="text">31</span></a></li>`);
        if (isExists == 0) {
            
            if (isFeb29Exists == 0) {
                var day29 = $(`<li> <a data-value="29"> <span class="text">29</span></a></li>`);
                $(this).closest(".date-container").find(".invoice-day .day-scroll-list").append(day29);
            }
            if ($(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="30"]`).closest("li").length == 0) {
                $(this).closest(".date-container").find(".invoice-day .day-scroll-list").append(day30);
            }

            if ($(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="31"]`).closest("li").length == 0) {
                $(this).closest(".date-container").find(".invoice-day .day-scroll-list").append(day31);
            }
         
        }
        if (monthName == "Fevral") {
            if (!(((parseInt(yearoforder) % 4 == 0) && (parseInt(yearoforder) % 100 != 0)) || (parseInt(yearoforder) % 400 == 0))) {
                $(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="29"]`).closest("li").remove();
                if (day == "29" || day == "30" || day == "31") {

                    $(this).closest(".date-container").find(".invoice-day .selected-tag").text("28");
                }
            }
            else {

                if (day == "30" || day == "31") {
                    $(this).closest(".date-container").find(".invoice-day .selected-tag").text("29");
                }

            }
            $(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="31"]`).closest("li").remove();
            $(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="30"]`).closest("li").remove();

        }
        else if (monthName == "Aprel" || monthName == "İyun" || monthName == "Sentyabr" || monthName == "Noyabr") {
            if ($(this).closest(".date-container").find(".invoice-day .selected-tag").text().trim() == "31") {
                $(this).closest(".date-container").find(".invoice-day .selected-tag").text("30");
            }
            if ($(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="31"]`).closest("li").length > 0) {
                $(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="31"]`).closest("li").remove();
            }
            if ($(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="30"]`).closest("li").length == 0) {

                $(this).closest(".date-container").find(".invoice-day .day-scroll-list").append(day30);
            }
        }


    })
    $(document).on("click", ".invoice-year .year-scroll-list li", function () {
        var yearoforder = $(this).find("a").attr("data-value");
        var isExists = $(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="29"]`).closest("li").length;
        var monthName = $(this).closest(".date-container").find(".invoice-month .selected-tag").text().trim();
        var day = $(this).closest(".date-container").find(".invoice-day .selected-tag").text().trim();
        if (((parseInt(yearoforder) % 4 == 0) && (parseInt(yearoforder) % 100 != 0)) || (parseInt(yearoforder) % 400 == 0)) {
           
            if (monthName == "Fevral") {

                if (isExists == 0) {
                
                    var day29 = $(`<li> <a data-value="29"> <span class="text">29</span></a></li>`);
                    $(this).closest(".date-container").find(".invoice-day .day-scroll-list").append(day29);
                  
                }

            }
        }
        else {
            if (monthName == "Fevral") {
                if (isExists == 1) {
                    $(this).closest(".date-container").find(`.invoice-day .day-scroll-list li a[data-value="29"]`).closest("li").remove();
                    if (day == "29") {

                        $(this).closest(".date-container").find(".invoice-day .selected-tag").text("28");
                    }
                }

            }

        }
    })

})