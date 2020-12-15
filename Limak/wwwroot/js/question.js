$(function () {
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
                img.attr("src", this.result);
                $preview.append(img);

            });
            reader.readAsDataURL(file);

        }
    }
    $('#file1').on("change", previewImages);
})