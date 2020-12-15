$(function () {
    $(document).on("click", ".buttonlook", function (e) {

        e.preventDefault();
        //Remove old active classes
        $("#status-modal-ul li").removeClass("active");

        $("#status-modal-ul li p .statusDate").text("");
        var declarationId_ = $(this).closest("tr").attr("data-declaration");
        var statuses = [];
        var statusDates = [];
        var lis = $("#status-modal-ul li");
        var liInnerPb = $("#status-modal-ul li p .statusDate");
        var data_ = {
            declarationId: declarationId_
        }
        $.ajax({
            url: "/User/GetDeclarationStatuses",
            data: data_,
            dataType: "json",
            type: "get",
            success: function (d) {
                if (d.success) {
                    
                    //Get status dates
                    statusDates.push(d.orderedDate);
                    statusDates.push(d.abroadWarehouseDate);
                    statusDates.push(d.onWayDate);
                    statusDates.push(d.customsControlDate);
                    statusDates.push(d.bakuWarehouseDate);
                    statusDates.push(d.courierDate);
                    statusDates.push(d.returnDate);
                    statusDates.push(d.completedDate);
                    // Get order statuses
                    statuses.push(d.ordered);
                    statuses.push(d.abroadWarehouse);
                    statuses.push(d.onWay);
                    statuses.push(d.customsControl);
                    statuses.push(d.bakuWarehouse);
                    statuses.push(d.courier);
                    statuses.push(d.return);
                    statuses.push(d.completed);
                    //Activate classes according to statuses and add dates
                    for (var i = 0; i < statuses.length; i++) {
                        if (statuses[i]) {
                            $(lis[i]).addClass("active");
                            
                            $(liInnerPb[i]).text(statusDates[i]);
                        }
                    }


                }
               
            },
            error: function (d) {
                console.log("error")
            }
        })


    })
})