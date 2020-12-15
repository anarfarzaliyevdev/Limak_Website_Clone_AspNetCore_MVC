$(function () {
    $(document).on("keyup", "#currency-form #from", function () {
        var typeFrom = $(this).closest(".border-radius").find(".bootstrap-select button .filter-option  .filter-option-inner .filter-option-inner-inner").text().trim();
        var typeTo = $("#currency-form #to").closest(".border-radius").find(".bootstrap-select button .filter-option  .filter-option-inner .filter-option-inner-inner").text().trim()
        var valueUSD_AZN = $(this).attr("data-USD_AZN");
        var valueUSD_TRY = $(this).attr("data-USD_TRY");
        var valueAZN_TRY = $(this).attr("data-AZN_TRY");
        var valueAZN_USD = $(this).attr("data-AZN_USD");
        var valueTRY_AZN = $(this).attr("data-TRY_AZN");
        var valueTRY_USD = $(this).attr("data-TRY_USD");
        var inputValue = $(this).val();
       
        if (inputValue > 0) {
            switch (typeFrom) {
                case "AZN":
                    if (typeTo == "TRY") {
                        var result = (parseFloat(valueAZN_TRY) * parseFloat(inputValue)).toFixed(2);
                        $("#currency-form #to").val(result);
                    }
                    else if (typeTo == "USD") {
                        var result = (parseFloat(valueAZN_USD) * parseFloat(inputValue)).toFixed(2);
                        $("#currency-form #to").val(result);
                    }
                    else {
                        $("#currency-form #to").val("");
                    }
                    break;
                case "USD":
                    if (typeTo == "AZN") {
                        var result = (parseFloat(valueUSD_AZN) * parseFloat(inputValue)).toFixed(2);
                        $("#currency-form #to").val(result);
                    }
                    else if (typeTo == "TRY") {
                        var result = (parseFloat(valueUSD_TRY) * parseFloat(inputValue)).toFixed(2);
                        $("#currency-form #to").val(result);
                    }
                    else {
                        $("#currency-form #to").val("");
                    }
                    break;
                case "TRY":
                    if (typeTo == "AZN") {
                        var result = (parseFloat(valueTRY_AZN) * parseFloat(inputValue)).toFixed(2);
                        $("#currency-form #to").val(result);
                    }
                    else if (typeTo == "USD") {
                        var result = (parseFloat(valueTRY_USD) * parseFloat(inputValue)).toFixed(2);
                        $("#currency-form #to").val(result);
                    }
                    else {
                        $("#currency-form #to").val("");
                    }
                    break;
                default:
                // code block
            }
        }
        else {
            
            $("#currency-form #to").val("");
        }
        
    })
    $(document).on("click", "#currency-form  .dropdown .dropdown-menu .currency-types li", function () {

        $("#currency-form #from").trigger("keyup");
     
    })
    $(document).on("change", "#currency-form #from", function () {

        $("#currency-form #from").trigger("keyup");
    })
})