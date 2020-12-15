$(function () {
    $(document).on("click", ".internal-cargo li", function () {
        var data_value = $(this).attr("data-value");
        switch (data_value) {
            case "0":
                $(this).closest(".row").find(".internal-cargo-container").find(".cargo-amount").hide();
                break;
            case "1":
                $(this).closest(".row").find(".internal-cargo-container").find(".cargo-amount").show();
                break;

            default:

                break;
        }
    });

    $(document).on("click", ".invoice-address  .dropdown-toggle", function () {
        $(this).next(".dropdown-menu").toggleClass("active-dropdown");
    })
    $(document).on("click", ".invoice-address  .dropdown-menu li a", function (e) {
        var closeDropdownToggle = $(this).parent().parent().prev(".dropdown-toggle");
        var clsoeSelectTag = closeDropdownToggle.find(".selected-tag")
        //Change selected
        clsoeSelectTag.text(`${$(this).text()}`)
        closeDropdownToggle.next().hide();

    })
    $(document).on("click", "#btn-privacy input", function (e) {
        e.preventDefault();
    })
    //Privacy cancel button
    $(document).on("click", "#cancel-privacy", function () {
        var data_checked = $("#btn-privacy input").attr("data-checked");


        if (data_checked == 'true') {
            $("#btn-privacy input").attr("data-checked", "false");
            $("#btn-privacy .checkmark").trigger("click");

        }

    })
    //Privacy accept button
    $(document).on("click", "#accept-privacy", function () {
        var data_checked = $("#btn-privacy input").attr("data-checked");


        if (data_checked == 'false') {

            $("#btn-privacy input").attr("data-checked", "true");
            $("#btn-privacy .checkmark").trigger("click");
        }

    })
    //Add new order button
    $(document).on("click", ".add-order-btn", function () {

        var newOrderElement = $(this).closest(".left-content").clone();
        var inputs = newOrderElement.find("input");
        for (let index = 0; index < inputs.length; index++) {
            const element = inputs[index];

            $(element).val("");
        }
        $(newOrderElement).find(".package-count").attr("value", "1");
        $(newOrderElement).find(".order-kargo-amount-input").attr("value", 0);
        
        newOrderElement.appendTo(".order-list");
    })
  

    // 
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
    //Calculate on keyup
    $(document).on("keyup", ".order-amount-input, .order-kargo-amount-input", function () {
        
        $(this).attr("value", `${$(this).val()}`);
       
        calculate(this);
    })

    //Calculate package count
    $(document).on("click", ".btn-increase-pack", function () {

        var package_countVal = $(this).closest(".left-content").find(".package-count").val();
        calculate(this);
        if (parseInt(package_countVal) > 0) {
            $(this).parent().prev(".package-count").attr("value", `${package_countVal}`)
        }
        else {
            $(this).parent().prev(".package-count").attr("value", `${1}`)
        }
       
    })
    //Claculate on decrese btn
    $(document).on("click", ".btn-decrease-pack", function () {

        var package_countVal = $(this).closest(".left-content").find(".package-count").val();
        calculate(this);
        if (parseInt(package_countVal) > 0) {
            $(this).parent().next(".package-count").attr("value", `${package_countVal}`)
        }
        else {
            $(this).parent().next(".package-count").attr("value", `${1}`)
        }
     
    })
    //Calculate on package count keyup
    $(document).on("keyup", ".package-count", function () {

        var package_countVal = $(this).val();
        calculate(this);
        if (parseInt(package_countVal) > 0) {

            $(this).attr("value", `${package_countVal}`)
        }
        else {
            $(this).attr("value", `${1}`)
        }
       
    })
    //Remove link
    $(document).on("click", ".btn-remove", function () {
        $(this).closest(".left-content").remove();
        getTotal();
    })
   
    $(document).on("click", ".internal-cargo li", function () {

        if ($(this).attr("data-value") == "0") {
            
            $(this).closest(".left-content").find(".order-kargo-amount-input").val("");
            $(this).closest(".left-content").find(".order-kargo-amount-input").attr("value", `${0}`)
            calculate(this);
        }
     
    })
    //Total amount calculate function
    function getTotal() {
        var sum = 0;
        var orderAmounts = $(".order-total-amount-input");
        for (let index = 0; index < orderAmounts.length; index++) {
            const element = orderAmounts[index];
            var value = $(element).attr("value");
            if (value > 0) {
                sum += parseFloat(value);
            }
        }
       
        $("#totalCashAmount").text(`${sum.toFixed(2)}`);
    }
    function calculate(elem) {
        var orderAmount = $(elem).closest(".left-content").find(".order-amount-input").val();
        var cargoAmount = $(elem).closest(".left-content").find(".order-kargo-amount-input").val();
        var package_countVal = $(elem).closest(".left-content").find(".package-count").val();
     
        var totalAmount = 0;
        var parsedTotal = 0;
        if (orderAmount > 0) {
            totalAmount += parseFloat(orderAmount);
        }
        if (cargoAmount > 0) {
            totalAmount += parseFloat(cargoAmount);
        }

        if (package_countVal > 0) {

            totalAmount += parseInt(package_countVal - 1) * parseFloat(totalAmount);
        }

        if (totalAmount > 0) {
            parsedTotal = (((parseFloat(totalAmount) * 5) / 100) + parseFloat(totalAmount)).toFixed(2);

            $(elem).closest(".left-content").find(".order-total-amount-input").val("");
            $(elem).closest(".left-content").find(".order-total-amount-input").val(`${parsedTotal}`);
            $(elem).closest(".left-content").find(".order-total-amount-input").attr("value", `${parsedTotal}`)

        } else {
            $(elem).closest(".left-content").find(".order-total-amount-input").val("");
            $(elem).closest(".left-content").find(".order-total-amount-input").attr("value", `${0}`)
        }
        getTotal();
    }

});

