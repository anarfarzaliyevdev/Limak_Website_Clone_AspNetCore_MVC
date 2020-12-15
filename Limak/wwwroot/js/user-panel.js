$(function () {
    $(document).on("click",".panel-nav-link",function () {
        var data_content= $(this).attr("data-content");
        $(".panel-right").hide();
        $(`.panel-right[data-content="${data_content}"]`).show();

        $(".link-change").parent().removeClass("active");
        $(`.link-change[data-content="${data_content}"]`).parent().addClass("active");
    })
    $(document).on("click",".dropdown-toggle",function () {
        $(this).next(".dropdown-menu").toggle();
    })
    $(document).on("click",".dropdown-menu li a",function (e) {
        var closeDropdownToggle= $(this).parent().parent().prev(".dropdown-toggle");
        var clsoeSelectTag=closeDropdownToggle.find(".selected-tag")
        //Change selected
        clsoeSelectTag.text(`${$(this).text()}`)
        closeDropdownToggle.next().hide();

    })

    

    //Selected images
    function previewImages() {

        var $preview = $('.box').empty();
        if (this.files) $.each(this.files, readAndPreview);

        function readAndPreview(i, file) {

            if (!/\.(jpe?g|png|gif)$/i.test(file.name)) {
                return alert(file.name + " is not an image");
            }
            var reader = new FileReader();
            $(reader).on("load", function () {
                var img = $("<img />");
                img.attr("style", "width: 150px; height: 120px; overflow: hidden; margin-right:15px;margin-top:5px;");
                img.attr("src",  this.result);
                $preview.append(img);

            });
            reader.readAsDataURL(file);

        }
    }
    $('#file1').on("change", previewImages);
})