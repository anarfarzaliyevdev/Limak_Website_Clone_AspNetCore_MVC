$(function () {


    //Increase AZN balance
    $(document).on("keyup", "#increase-azn-balance-usd", function () {
        var valueUsd = $(this).val()
        if (valueUsd > 0) {
            var valueWithAzn = parseFloat((parseFloat(valueUsd)) * 1.7).toFixed(2)
            $("#increase-azn-balance-azn").val(`${valueWithAzn}`)
        }
        else {
            $("#increase-azn-balance-azn").val("")
        }

    })

    $(document).on("click", "#increase-balance .btn_pay", function (e) {
        e.preventDefault();
        var value = $("#increase-balance #increase-azn-balance-azn").val().trim();
        if (value>0) {
            var data_ = {
                amount: value
            }
            $.ajax({
                url: "/User/IncreaseAznBalance",
                data: data_,
                dataType: "json",
                type: "post",
                success: function (d) {
                    if (d.success) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Tamamlandı',
                            text: "Müvəffəqiyytlə əlavə edildi!",
                            showConfirmButton: true,

                        })
                        $("#myBalance").html(`${d.azn_balance} <sup> ₼</sup> `);
                        $("#increase-balance input").val("");

                        var row = `<tr>
                                <td>Mədaxil</td>
                                <td>${d.amount}</td>
                                <td>${d.date}</td>
                                </tr>`;



                        $(row).appendTo("#AznOpreations tbody");

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
        else {
            Swal.fire({
                position: 'center',
                icon: 'info',
                title: 'Bos ola bilmez',
                showConfirmButton: true,

            })
        }
        
    })
    
})