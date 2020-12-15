$(function () {

    $(document).on("click", "#rowDataDeleteBtn", function () {
        var dataId = $(this).attr("data-id");

        $("#modalDeleteBtn").attr("data-id", dataId);
    })


    
})