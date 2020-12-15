$(function () {

    $(document).on("click", "#modalDeleteBtn", function () {
        var dataId = $(this).attr("data-id");
        var data_ = {
            id: dataId
        }
        $.ajax({
            url: "/Admin/Admins/DeleteAdmin",
            type: "post",
            dataType: "json",
            data: data_,
            success: function (d) {
                if (d.success) {
                    $(`#rowDataDeleteBtn[data-id=${dataId}]`).parent().parent().remove();
                    $("#exampleModal").removeClass("show");
                    $(".modal-backdrop").removeClass("show");
                    alert(d.message)
                }
                else {
                    alert(d.message)
                }

            },
            error: function (d) {
                console.log("error")
            }
        })
    })
})