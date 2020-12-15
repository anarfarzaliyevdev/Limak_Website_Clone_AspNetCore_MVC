$(function () {
    $(document).on("click", "#saveExcelBtn", function () {
        $(".saveExcelTable").table2excel({

            // exclude CSS class

            exclude: ".noExl",
            name: "Worksheet Name",
            filename: "ExcelTable",//do not include extension
            fileext: ".xls" // file extension

        });

    })
})