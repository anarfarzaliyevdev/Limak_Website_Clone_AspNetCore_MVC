$(function () {

    $(document).on("click", ".removepackage", function (e) {
        var packageid = $(this).closest("tr").attr("data-declaration");
        var data_ = { id: packageid };
        Swal.fire({
            title: 'Əminsizmi?',
            text: "Bəyənnamənin silinməsi",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Bəli!',
            cancelButtonText: 'Xeyr!'
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    url: "/User/DeletePackage",
                    data: data_,
                    dataType: "json",
                    type: "post",
                    success: function (d) {
                        if (d.success) {
                            Swal.fire(
                                'Silindi!',
                                'success'
                            )
                            $(`tr[data-declaration=${packageid}]`).next(".looksection").remove();
                            $(`tr[data-declaration=${packageid}]`).remove();
                        }
                        else {
                            Swal.fire({
                                position: 'center',
                                icon: 'error',
                                title: 'Error appeared',
                                showConfirmButton: true

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
})