$(function () {




    $(document).on("click", "#payment", function (e) {
        var userBalance = $($("#TRYBalance")[0]).text().trim();
        var userBalanceParsed = parseFloat(userBalance).toFixed(2);
        e.preventDefault();
        //Validation
        let isproductLink = false;
        let isprice = false;
        let isproductnote = false;
        let ischecked = false;
        let inputs = $(".left-content input");

        let count = 0;
        let counttrue = 0;

        for (var i = 0; i < inputs.length; i++) {
            count++;
            if (inputs[i].id == "productLink") {
                if ($(inputs[i]).val() == '') {
                    $(inputs[i]).next().next().show(200);
                    $(inputs[i]).addClass("alertborder");
                    isproductLink = false;
                } else {
                    $(inputs[i]).next().next().hide(0);
                    $(inputs[i]).removeClass("alertborder");
                    isproductLink = true;
                    counttrue++;
                }
            } else if (inputs[i].id == "orderNote") {
                if ($(inputs[i]).val() == '') {
                    $(inputs[i]).next().next().show(200);
                    $(inputs[i]).addClass("alertborder");
                    isproductnote = false;
                } else {
                    $(inputs[i]).next().next().hide(0);
                    $(inputs[i]).removeClass("alertborder");
                    isproductnote = true;
                    counttrue++;
                }
            } else if (inputs[i].id == "orderPrice") {
                if ($(inputs[i]).val() == '') {
                    $(inputs[i]).next().next().show(200);
                    $(inputs[i]).addClass("alertborder");
                    isprice = false;
                } else {
                    $(inputs[i]).next().next().hide(0);
                    $(inputs[i]).removeClass("alertborder");
                    isprice = true;
                    counttrue++;
                }
            }

        }

        var data_checked = $("#btn-privacy input").attr("data-checked");
        if (data_checked == 'true') {
            ischecked = true;

        }
        else {
            ischecked = false;
            Swal.fire({
                position: 'center',
                icon: 'error',

                title: 'Xəta',
                text: 'Zəhmət olmasa məsafəki satış şərtlərini qəbul edin',
                showConfirmButton: true,

            })
        }


        if (counttrue == ($(".left-content").length * 3) && ischecked) {
            if (userBalanceParsed > 0) {

                var totalCashAmount = $($("#totalCashAmount")).text().trim();

                var totalCashAmountParsed = parseFloat(totalCashAmount).toFixed(2);



                if (Number(`${userBalanceParsed}`) > Number(`${totalCashAmountParsed}`) || Number(`${userBalanceParsed}`) == Number(`${totalCashAmountParsed}`)) {


                    var links = $(".left-content");
                    var orders = [];
                    $(links).each(function (index, element) {

                        var productLink = $(element).find('#productLink').val().trim();
                        var orderPrice = $(element).find("#orderPrice").val().trim();
                        var orderCount = $(element).find("#orderCount").attr("value");

                        var orderNote = $(element).find("#orderNote").val().trim();
                        var turkeyCargo = $(element).find("#turkey-cargo-input").attr("value");

                        var order = {
                            ProductLink: productLink,
                            Price: orderPrice,
                            OrderCount: orderCount,
                            OrderNote: orderNote,
                            TurkeyCargoPrice: turkeyCargo

                        };
                        orders.push(order);
                    });



                    var data_ = {
                        orders: orders,
                        TotalOrderPrice: totalCashAmountParsed
                    }
                    $.ajax({
                        url: "/User/AddOrders",
                        type: 'POST',

                        data: data_,
                        success: function (d) {
                            if (d.success) {
                                Swal.fire({
                                    position: 'center',
                                    icon: 'info',
                                    title: 'Tamamlandı',
                                    text: "Müvəffəqiyytlə əlavə edildi!",
                                    showConfirmButton: true,

                                })
                                $("#TRYBalance").text(d.updatedUserBalance);
                                $(".left-content input").val("");
                                $("#totalCashAmount").text("0.00");
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
                else {
                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'Zehmet olmasa balansinizi artirin',
                        showConfirmButton: true,

                    })
                }
            }
            else {
                Swal.fire({
                    position: 'center',
                    icon: 'error',
                    title: 'Zehmet olmasa balansinizi artirin',
                    showConfirmButton: true,

                })
            }
        }

    })
})