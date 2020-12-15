$(function () {
    $(document).on("click", "table .check-button", function (e) {
        //Beacuse of trigger twice 1 for input 1 for label
        if (e.target.tagName.toUpperCase() === "INPUT") {
            //
            calcTotalCargoAmount(this);
            if ($(this).hasClass("checked")) {
               
                $(this).find("input").prop('checked', false);
                $(this).removeClass("checked");
            }
            else {
                $(this).find("input").prop('checked', true);
                $(this).addClass("checked");
            }
            //
            calcTotalCargoAmount(this);
        }
    

    })
    
    $(document).on("click", ".select-all .check-button", function (e) {
        //Beacuse of trigger twice 1 for input 1 for label
        
        if (e.target.tagName.toUpperCase() === "INPUT") {
            $(this).toggleClass("checked");
            
            var individualChecks = $(this).closest(".block-inner").find("table .check-button");
           
            for (var i = 0; i < individualChecks.length; i++) {

                if ($(this).hasClass("checked")) {
                    $(individualChecks[i]).find("input").prop('checked', true);
                    $(individualChecks[i]).addClass("checked");
                }
                else {
                    $(individualChecks[i]).find("input").prop('checked', false);
                    $(individualChecks[i]).removeClass("checked");
                } 
            }
            //
            calcTotalCargoAmount(this);
        }
    })

    $(document).on("click", ".pay-cargo-btn", function (e) {
        e.preventDefault();
        var currentPayBtn = this;
        var allChecked = $(this).closest(".block").find("table .checked");
        var packageIds = [];
        var totalCargoAmount=0;
        for (var i = 0; i < allChecked.length; i++) {
            totalCargoAmount += parseFloat($(allChecked[i]).closest("tr").next("tr").find(".cargo-price-td .package-cargo-price").text().trim());
            packageIds.push(parseInt($(allChecked[i]).closest("tr").attr("data-declaration"))) 
        }
        totalCargoAmount = parseFloat(totalCargoAmount).toFixed(2);
        if (totalCargoAmount > 0) {
            var data_ = {
                packageIds: packageIds,
                totalCargoAmount: totalCargoAmount
            }
            $.ajax({
                url: "/User/PayPackageCargo",
                type: 'POST',

                data: data_,
                success: function (d) {
                    if (d.success) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Tamamlandı',
                            text: "Ugurlu odenis!",
                            showConfirmButton: true,

                        })
                        for (var i = 0; i < allChecked.length; i++) {
                            allChecked[i].remove();
                        }
                        $(currentPayBtn).closest(".block-inner").find(".sum .total-cargo-amount span").text(`0.00`);
                        
                    }
                    else {
                        Swal.fire({
                            position: 'center',
                            icon: 'error',
                            title: `${d.message}`,
                            showConfirmButton: true,

                        })
                    }
                },
                error: function (d) {
                    console.log("error")
                }
            });
            
        }
        
    })
    function calcTotalCargoAmount(elem) {
        var allChecked = $(elem).closest(".block").find("table .checked");
    
        var totalCargoAmount = 0;
        for (var i = 0; i < allChecked.length; i++) {
            totalCargoAmount += parseFloat($(allChecked[i]).closest("tr").next("tr").find(".cargo-price-td .package-cargo-price").text().trim());
            
        };
        $(elem).closest(".block-inner").find(".sum .total-cargo-amount span").text(`${parseFloat(totalCargoAmount).toFixed(2)}`);
       
    }
})