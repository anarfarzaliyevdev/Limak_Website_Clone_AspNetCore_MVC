$(function () {


    $(document).on("click", "#declarebtn", function (e) {
        e.preventDefault();
        let isname = false;
        let ispackagetype = false;
        let ispackagecount = false;
        let isprice = false;
        let istrackid = false;

        var packageNote;
        var yearOfOrder;
        var monthOfOrder;
        var dayOfOrder;
        var deliveryOffice;
        var trackId;
        var price;
        var packageProductNumber;
        var packageProductType;
        var storeName;
        var countryName = $('.country .active').text().trim();
        if (countryName == "Türkiyə") {

            if ($("#storename").val() == '') {
                $('.add-storename').show(200);
                $("#storename").addClass("alertborder");
            }
            else {
                $('.add-storename').hide(0);
                $("#storename").removeClass("alertborder");
                isname = true;
            }

            if ($("#producttype").val() == '') {
                $('.add-packagetype').show(200);
                $("#producttype").addClass("alertborder");
            }
            else {
                $('.add-packagetype').hide(0);
                $("#producttype").removeClass("alertborder");
                ispackagetype = true;
            }

            if ($("#productnumber").val() == '') {
                $('.add-packagecount').show(200);
                $("#productnumber").addClass("alertborder");
            }
            else {
                $('.add-packagecount').hide(0);
                $("#productnumber").removeClass("alertborder");
                ispackagecount = true;
            }

            if ($("#price").val() == '') {
                $('.add-packageprice').show(200);
                $("#price").addClass("alertborder");
            }
            else {
                $('.add-packageprice').hide(0);
                $("#price").removeClass("alertborder");
                isprice = true;
            }

            if ($("#trackid").val() == '') {
                $('.add-trackid').show(200);
                $("#trackid").addClass("alertborder");
            }
            else {
                $('.add-trackid').hide(0);
                $("#trackid").removeClass("alertborder");
                istrackid = true;
            }


            storeName = $('#storename').val().trim();
            packageProductType = $("#producttype").val().trim();
            packageProductNumber = $("#productnumber").val().trim();
            price = $("#price").val().trim();
            trackId = $("#trackid").val().trim();
            deliveryOffice = $("#deliveryoffice").text().trim();
            dayOfOrder = $("#dayoforder1").text().trim();
            monthOfOrder = $("#monthoforder1").text().trim();
            yearOfOrder = $("#yearoforder1").text().trim();
            packageNote = $("#comment").val().trim();

        }
        else {
            if ($("#storename2").val() == '') {
                $('.add-storename2').show(200);
                $("#storename2").addClass("alertborder");
            }
            else {
                $('.add-storename2').hide(0);
                $("#storename2").removeClass("alertborder");
                isname = true;
            }

            if ($("#producttype2").val() == '') {
                $('.add-packagetype2').show(200);
                $("#producttype2").addClass("alertborder");
            }
            else {
                $('.add-packagetype2').hide(0);
                $("#producttype2").removeClass("alertborder");
                ispackagetype = true;
            }

            if ($("#productnumber2").val() == '') {
                $('.add-packagecount2').show(200);
                $("#productnumber2").addClass("alertborder");
            }
            else {
                $('.add-packagecount2').hide(0);
                $("#productnumber2").removeClass("alertborder");
                ispackagecount = true;
            }

            if ($("#price2").val() == '') {
                $('.add-packageprice2').show(200);
                $("#price2").addClass("alertborder");
            }
            else {
                $('.add-packageprice2').hide(0);
                $("#price2").removeClass("alertborder");
                isprice = true;
            }

            if ($("#trackid2").val() == '') {
                $('.add-trackid2').show(200);
                $("#trackid2").addClass("alertborder");
            }
            else {
                $('.add-trackid2').hide(0);
                $("#trackid2").removeClass("alertborder");
                istrackid = true;
            }

            storeName = $('#storename2').val().trim();
            packageProductType = $("#producttype2").val().trim();
            packageProductNumber = $("#productnumber2").val().trim();
            price = $("#price2").val().trim();
            trackId = $("#trackid2").val().trim();
            deliveryOffice = $("#deliveryoffice2").text().trim();
            dayOfOrder = $("#dayoforder2").text().trim();
            monthOfOrder = $("#monthoforder2").text().trim();
            yearOfOrder = $("#yearoforder2").text().trim();
            packageNote = $("#comment2").val().trim();
        }

        if (isname && ispackagetype && ispackagecount && isprice && istrackid) {
            var packageNote;
            var yearOfOrder;
            var monthOfOrder;
            var dayOfOrder;
            var deliveryOffice;
            var trackId;
            var price;
            var packageProductNumber;
            var packageProductType;
            var storeName;
            var countryName = $('.country .active').text().trim();
            if (countryName == "Türkiyə") {
                storeName = $('#storename').val().trim();
                packageProductType = $("#producttype").val().trim();
                packageProductNumber = $("#productnumber").val().trim();
                price = $("#price").val().trim();
                trackId = $("#trackid").val().trim();
                deliveryOffice = $("#deliveryoffice").text().trim();
                dayOfOrder = $("#dayoforder1").text().trim();
                monthOfOrder = $("#monthoforder1").text().trim();
                yearOfOrder = $("#yearoforder1").text().trim();
                packageNote = $("#comment").val().trim();
            }
            else {
                storeName = $('#storename2').val().trim();
                packageProductType = $("#producttype2").val().trim();
                packageProductNumber = $("#productnumber2").val().trim();
                price = $("#price2").val().trim();
                trackId = $("#trackid2").val().trim();
                deliveryOffice = $("#deliveryoffice2").text().trim();
                dayOfOrder = $("#dayoforder2").text().trim();
                monthOfOrder = $("#monthoforder2").text().trim();
                yearOfOrder = $("#yearoforder2").text().trim();
                packageNote = $("#comment2").val().trim();
            }
            
            var data_ = {
                CountryName: countryName,
                StoreName: storeName,
                PackageProductType: packageProductType,
                PackageProductNumber: packageProductNumber,
                Price: price,
                TrackId: trackId,
                DeliveryOffice: deliveryOffice,
                DayOfOrder: dayOfOrder,
                MonthOfOrder: monthOfOrder,
                YearOfOrder: yearOfOrder,
                PackageNote: packageNote
            };

            $.ajax({
                url: "/User/AddDeclares",
                data: data_,
                dataType: "json",
                type: "post",
                success: function (d) {
                    if (d.success) {
                        Swal.fire({
                            position: 'center',
                            icon: 'info',
                            title: 'Tamamlandı',
                            text: "Müvəffəqiyytlə əlavə edildi!",
                            showConfirmButton: true,

                        })
                        $("#turkey input").val("");
                        $("#turkey textarea").val("");
                        $("#usa input").val("");
                        $("#usa textarea").val("");
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
        }
       

    })
})