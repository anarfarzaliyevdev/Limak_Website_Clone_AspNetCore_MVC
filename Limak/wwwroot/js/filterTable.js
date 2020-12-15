$(function () {
    $(document).on("click", ".filter .invoice-address  .dropdown-toggle", function () {
        if ($(this).next(".dropdown-menu").hasClass("deactive-dropdown")) {
            $(this).next(".dropdown-menu").removeClass("deactive-dropdown");
            $(this).next(".dropdown-menu").addClass("active-dropdown");
        }
        else {
            $(this).next(".dropdown-menu").addClass("deactive-dropdown");
            $(this).next(".dropdown-menu").removeClass("active-dropdown");
        }
        
    })
    $(document).on("click", ".filter .invoice-address  .dropdown-menu li a", function (e) {
       
        var closeDropdownToggle = $(this).parent().parent().prev(".dropdown-toggle");
        var clsoeSelectTag = closeDropdownToggle.find(".selected-tag")
        //Change selected
        clsoeSelectTag.text(`${$(this).text()}`)
        $(".filter .invoice-address .select-tag-container  .dropdown-menu").removeClass("active-dropdown");
        $(".filter .invoice-address .select-tag-container  .dropdown-menu").addClass("deactive-dropdown");
        var type = $(this).text().trim();
        var trs = $(".saveExcelTable tbody tr");
        if (type == "Hamısı") {
            for (var i = 0; i < trs.length; i++) {
                    $(trs[i]).show();
            }
        }
        else {
            for (var i = 0; i < trs.length; i++) {
                var trType = $(trs[i]).find("td:first-child").text().trim();
                if (type == trType) {
                    $(trs[i]).show();
                }
                else {
                    $(trs[i]).hide();
                }
                
            }
        }
       

    })
})