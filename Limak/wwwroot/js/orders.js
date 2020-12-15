$(function () {

    $(".looksection").hide();
    $(".modal").hide();
    $(".modal-backdrop").hide();
    var productlink;
    var status;
    $(document).on("click", ".buttonlook", function (e) {

        e.preventDefault();
        $(".looksection").hide();
        $(this).parent().parent().next().toggle(200);
        productlink = $(this).parent().prev().prev().find("span").text();
        status = $(this).parent().prev().find("span").text();

    })
    $(document).on("click", ".follow_order", function (e) {
        e.preventDefault();


        $(".modalProductLink").text(productlink);
        $(".modalStatus").text(status);

    })

    //Filter status
    $(document).on("click", ".filter-status li", function () {

        var status = $(this).find("a .order-status").text().trim();
        $(".filter-current-status").text(status);
        var table = $(this).closest(".block").find("table")[0];
        var rows = $($(this).closest(".block").find("table")[0]).find("tr[data-order-status]");
        $($(this).closest(".block").find("table")[0]).find("tr").hide();
        for (var i = 0; i < rows.length; i++) {
            var currentStatus = $(rows[i]).attr("data-order-status").trim();
            
            if (status == "Hamısı") {
                $($(this).closest(".block").find("table")[0]).find("tr[data-order-status]").show();
                break;
            }
            if (currentStatus == status) {
                $(rows[i]).show();

            }
        }
    })

    //Order badges count
    var allCount = $("#order-records tr[data-order-status]").length;
    var orderedCount = $(`#order-records tr[data-order-status="Sifariş verildi"]`).length;
    var foreignWarehouseCount = $(`#order-records tr[data-order-status="Xaricdəki anbar"]`).length;
    var onWayCount = $(`#order-records tr[data-order-status="Yoldadır"]`).length;
    var customsControlCount = $(`#order-records tr[data-order-status="Gömrük yoxlanışı"]`).length;
    var warehouseCount = $(`#order-records tr[data-order-status="Bakı anbarı"]`).length;
    var courierCount = $(`#order-records tr[data-order-status="Kuryer çatdırma"]`).length;
    var returnCount = $(`#order-records tr[data-order-status="İade"]`).length;
    var completedCount = $(`#order-records tr[data-order-status="Tamamlanmış"]`).length;
    
    $("#order-records").closest(".block").find(`.packages-filter li a .badge[data-badge-status="Hamısı"]`).text(allCount);
    $("#order-records").closest(".block").find(`.packages-filter li a .badge[data-badge-status="Sifariş verildi"]`).text(orderedCount);
    $("#order-records").closest(".block").find(`.packages-filter li a .badge[data-badge-status="Xaricdəki anbar"]`).text(foreignWarehouseCount);
    $("#order-records").closest(".block").find(`.packages-filter li a .badge[data-badge-status="Yoldadır"]`).text(onWayCount);
    $("#order-records").closest(".block").find(`.packages-filter li a .badge[data-badge-status="Gömrük yoxlanışı"]`).text(customsControlCount);
    $("#order-records").closest(".block").find(`.packages-filter li a .badge[data-badge-status="Bakı anbarı"]`).text(warehouseCount);
    $("#order-records").closest(".block").find(`.packages-filter li a .badge[data-badge-status="Kuryer çatdırma"]`).text(courierCount);
    $("#order-records").closest(".block").find(`.packages-filter li a .badge[data-badge-status="İade"]`).text(returnCount);
    $("#order-records").closest(".block").find(`.packages-filter li a .badge[data-badge-status="Tamamlanmış"]`).text(completedCount);

})