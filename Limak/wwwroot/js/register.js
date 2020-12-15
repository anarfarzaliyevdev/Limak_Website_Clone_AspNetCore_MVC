$(function () {
    $(document).on("click", ".bootstrap-select .dropdown-menu li a", function () {
        var dataValue = $(this).attr("data-value");

        $(this).closest(".border-radius").find(".select-value-input").attr("value", `${dataValue}`);
        if (dataValue != "") {
            $(this).closest(".border-radius").find(".text-danger").remove();
        }
    })
    $(document).on("click", ".checkmark", function () {
        $("#checkBoxValMes").toggleClass("display-block");
        $("#checkBoxValMes").toggleClass("display-none");
    })
    //Date validation
    //Month
    $(document).on("click", ".invoice-month .month-scroll-list li", function () {
        var monthName = $(this).find("a").attr("data-value");
        var year = $("#year").text().trim();

        var day = $("#dayOfBirth").text().trim();
        var isExists = $(`.invoice-day .day-scroll-list li a[data-value="31"]`).closest("li").length;
        var isFeb29Exists = $(`.invoice-day .day-scroll-list li a[data-value="29"]`).closest("li").length;
        var day30 = $(`<li> <a data-value="30"> <span class="glyphicon glyphicon-ok check-mark"></span> <span class="text">30</span></a></li>`);
        var day31 = $(`<li> <a data-value="31"> <span class="glyphicon glyphicon-ok check-mark"></span> <span class="text">31</span></a></li>`);
        if (isExists == 0) {
            
            if (isFeb29Exists == 0) {
                var day29 = $(`<li> <a data-value="29"> <span class="glyphicon glyphicon-ok check-mark"></span> <span class="text">29</span></a></li>`);
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
    //Year
    $(document).on("click", ".invoice-year .year-scroll-list li", function () {
        var year = $(this).find("a").attr("data-value");
        var day = $("#dayOfBirth").text().trim();
        var isExists = $(`.invoice-day .day-scroll-list li a[data-value="29"]`).closest("li").length;
        var monthName = $("#month-name").text().trim();
        if (((parseInt(year) % 4 == 0) && (parseInt(year) % 100 != 0)) || (parseInt(year) % 400 == 0)) {
            if (monthName == "Fevral") {
                if (isExists == 0) {
                    var day29 = $(`<li> <a data-value="29"> <span class="glyphicon glyphicon-ok check-mark"></span> <span class="text">29</span></a></li>`);
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
    //Validate serianumber input
    $(document).on("keypress", "#SeriaNumber", function () 
    {
        var idType = $("#IdType").val();
        if (idType == "AZE") {
            if ($(this).val().length == 8) {

                return false;
            }
        }
        else if (idType == "AA") {
            if ($(this).val().length == 7) {

                return false;
            }
        }
        
    })
    //Clear idtype input by id type selection
    $(document).on("click", ".idType-select li", function () {

        $("#SeriaNumber").val("");
       
   
    })


})