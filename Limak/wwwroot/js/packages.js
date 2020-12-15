$(function () {

    $(".looksection").hide();
    $(".modal").hide();
    $(".modal-backdrop").hide();
    var trackid;
    var date;
    var storename;
    var status;
    $(document).on("click", ".buttonlook", function (e) {
      
        e.preventDefault();
        $(".looksection").hide();
        $(this).closest("tr").next().toggle(200);
       
        trackid = $(this).parent().prev().prev().prev().prev().find("span").text();
        date = $(this).parent().prev().prev().prev().find("span").text();
        storename = $(this).parent().prev().prev().find("span").text();
        status = $(this).parent().prev().find("span").text();
        

    })
    $(document).on("click", ".follow_order", function (e) {
        e.preventDefault();
        
        $(".modalTrackid").text(trackid);
        $(".modalDate").text(date);
        $(".modalStorename").text(storename);
        $(".modalStatus").text(status);

    })
    //Filter status
    $(document).on("click", ".filter-status li", function () {

        var status = $(this).find("a .order-status").text().trim();

        $(this).parent().prev().find(".filter-current-status").text(status);
        var table = $(this).closest(".block").find(".block-inner table");
        var rows = $(this).closest(".block").find(".block-inner table").find("tr[data-order-status]");
        
        $(this).closest(".block").find(".block-inner table").find("tr").hide();
        for (var i = 0; i < rows.length; i++) {
            var currentStatus = $(rows[i]).attr("data-order-status").trim();
           
            if (status == "Hamısı") {
                $(this).closest(".block").find(".block-inner table").find("tr[data-order-status]").show();
                break;
            }
            if (currentStatus == status) {
                $(rows[i]).show();

            }
        }
    })
    //Turkey packages badges
    var allCount = $("#turkey-records tr[data-order-status]").length;
    var orderedCount = $(`#turkey-records tr[data-order-status="Sifariş verildi"]`).length;
    var foreignWarehouseCount = $(`#turkey-records tr[data-order-status="Xaricdəki anbar"]`).length;
    var onWayCount = $(`#turkey-records tr[data-order-status="Yoldadır"]`).length;
    var customsControlCount = $(`#turkey-records tr[data-order-status="Gömrük yoxlanışı"]`).length;
    var warehouseCount = $(`#turkey-records tr[data-order-status="Bakı anbarı"]`).length;
    var courierCount = $(`#turkey-records tr[data-order-status="Kuryer çatdırma"]`).length;
    var returnCount = $(`#turkey-records tr[data-order-status="İade"]`).length;
    var completedCount = $(`#turkey-records tr[data-order-status="Tamamlanmış"]`).length;
    $("#turkey-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Hamısı"]`).text(allCount);
    $("#turkey-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Sifariş verildi"]`).text(orderedCount);
    $("#turkey-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Xaricdəki anbar"]`).text(foreignWarehouseCount);
    $("#turkey-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Yoldadır"]`).text(onWayCount);
    $("#turkey-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Gömrük yoxlanışı"]`).text(customsControlCount);
    $("#turkey-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Bakı anbarı"]`).text(warehouseCount);
    $("#turkey-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Kuryer çatdırma"]`).text(courierCount);
    $("#turkey-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="İade"]`).text(returnCount);
    $("#turkey-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Tamamlanmış"]`).text(completedCount);
    //America packages badges
    var allCount = $("#america-records tr[data-order-status]").length;
    var orderedCount = $(`#america-records tr[data-order-status="Sifariş verildi"]`).length;
    var foreignWarehouseCount = $(`#america-records tr[data-order-status="Xaricdəki anbar"]`).length;
    var onWayCount = $(`#america-records tr[data-order-status="Yoldadır"]`).length;
    var customsControlCount = $(`#america-records tr[data-order-status="Gömrük yoxlanışı"]`).length;
    var warehouseCount = $(`#america-records tr[data-order-status="Bakı anbarı"]`).length;
    var courierCount = $(`#america-records tr[data-order-status="Kuryer çatdırma"]`).length;
    var returnCount = $(`#america-records tr[data-order-status="İade"]`).length;
    var completedCount = $(`#america-records tr[data-order-status="Tamamlanmış"]`).length;
    $("#america-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Hamısı"]`).text(allCount);
    $("#america-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Sifariş verildi"]`).text(orderedCount);
    $("#america-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Xaricdəki anbar"]`).text(foreignWarehouseCount);
    $("#america-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Yoldadır"]`).text(onWayCount);
    $("#america-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Gömrük yoxlanışı"]`).text(customsControlCount);
    $("#america-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Bakı anbarı"]`).text(warehouseCount);
    $("#america-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Kuryer çatdırma"]`).text(courierCount);
    $("#america-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="İade"]`).text(returnCount);
    $("#america-records").closest(".block").find(`.filter-status li a .badge[data-badge-status="Tamamlanmış"]`).text(completedCount);

    //Increase package count
    $(document).on("click", ".btn-increase-pack", function () {
        var package_count = $(this).closest(".input-group-container").find("#ProductCountEdit");
     
        var package_countVal = package_count.val();
        package_countVal++;
        package_count.val(package_countVal)

    })
    //Decrease package count
    $(document).on("click", ".btn-decrease-pack", function () {
 
        var package_count = $(this).closest(".input-group-container").find("#ProductCountEdit");
        var package_countVal = package_count.val();
        if (package_countVal < 1 || package_countVal == 1) {
            package_package_countValcount = 1
        }
        else {
            package_countVal--;
        }
        package_count.val(package_countVal)
    })
})