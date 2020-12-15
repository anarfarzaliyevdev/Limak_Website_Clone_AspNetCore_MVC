$(function () {
 
    $.ajax({
        url: "/User/GetCurrencies",
        type: 'Get',
    
        contentType: "application/json",
        dataType: "json",
        success: function (d) {
            if (d.success) {
               
                $("#from").attr("data-USD_AZN", `${d.usD_AZN}`);
                $("#from").attr("data-USD_TRY", `${d.usD_TRY}`);
                $("#from").attr("data-AZN_TRY", `${d.azN_TRY}`);
                $("#from").attr("data-AZN_USD", `${d.azN_USD}`);
                $("#from").attr("data-TRY_AZN", `${d.trY_AZN}`);
                $("#from").attr("data-TRY_USD", `${d.trY_USD}`);
                $("#TL_AZN").text(parseFloat(d.trY_AZN).toFixed(4));
                $("#TL_USD").text(parseFloat(d.trY_USD).toFixed(4));
            }
            else {
             
            }
        },
        error: function (d) {
            console.log("error")
        }
    });
})