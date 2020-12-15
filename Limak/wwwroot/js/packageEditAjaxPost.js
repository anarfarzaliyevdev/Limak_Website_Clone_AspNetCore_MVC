
$(function () {

    //Edit modal
    $(document).on("click", ".editpackage", function (e) {
        e.preventDefault();
        var packageid = $(this).closest("tr").attr("data-declaration");
        var data_ = { id: packageid };
        var value=0;
        $.ajax({
            url: "/User/GetPackage",
            data: data_,
            type: 'Get',
            contentType: "application/json",
            dataType: "json",
            success: function (d) {
                if (d.success) {
                    $("#storeNameEdit").val(d.storename);
                    $("#ProductTypeEdit").val(d.productType);
                    $("#PriceEdit").val(d.price);
                    $("#TrackIdEdit").val(d.trackid);
                    $("#ProductCountEdit").val(d.productCount);
                    $(".commentEdit").text(d.comment);
                    if (d.deliveryOffice == "Baki") {
                        value = 1;
                    } else if (d.deliveryOffice == "Gence") {
                        value = 2;
                    } else {
                        value = 3;
                    }
                    $("#DeliveryOfficeEdit").val(value);
                    $("#packageid").val(d.packageid);
                }
            },
            error: function (d) {
                console.log("error")
            }
        });
    })

    //UpdatePackage

    $(document).on("click", ".sendbutton", function (e) {
        e.preventDefault();
        var packageNote;
        var deliveryOffice;
        var trackId;
        var price;
        var packageProductNumber;
        var packageProductType;
        var storeName;
        var id;
        var deliveryOfficeName;

        storeName = $('#storeNameEdit').val().trim();
        packageProductType = $("#ProductTypeEdit").val().trim();
        packageProductNumber = $("#ProductCountEdit").val().trim();
        price = $("#PriceEdit").val().trim();
        trackId = $("#TrackIdEdit").val().trim();
        deliveryOffice = $("#DeliveryOfficeEdit").val();
        packageNote = $(".commentEdit").text();
        id = $("#packageid").val();

        if (deliveryOffice == 1) {
            deliveryOfficeName = "Baki";
        } else if (deliveryOffice == 2) {
            deliveryOfficeName = "Gence";
        } else {
            deliveryOfficeName = "Sumqayit";
        }

        var data_ = {
            StoreName: storeName,
            PackageProductType: packageProductType,
            PackageProductNumber: packageProductNumber,
            Price: price,
            TrackId: trackId,
            DeliveryOffice: deliveryOfficeName,
            PackageNote: packageNote,
            Id:id
        };

        $.ajax({
            url: "/User/UpdatePackage",
            data: data_,
            dataType: "json",
            type: "post",
            success: function (d) {
                if (d.success) {
                    Swal.fire({
                        position: 'center',
                        icon: 'info',
                        text: "Bəyannamə yükləndi!",
                        showConfirmButton: true,

                    })

                    var row = $(`tr[data-declaration="${id}"]`);
                    $(row).find(".package-store-name").text(storeName);
                    $(row).next().find(".product-track-id").text(trackId);
                    $(row).next().find(".product-count").text(packageProductNumber);
                    var getCountry = $(row).closest("tbody").attr("id");
                    if (getCountry == "america-records") {
                        $(row).next().find(".product-price").text(price + ".00 USD");
                    }
                    else if (getCountry =="turkey-records") {
                        $(row).next().find(".product-price").text(price + ".00 TL");
                    }
                   

                }
                else {
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'Error appeared',
                        showConfirmButton: true,

                    })
                }
            },
            error: function (d) {
                console.log("error")
            }
        })
    })
})