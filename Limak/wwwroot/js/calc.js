$(function () {
    $(document).on("click", "#calculate-btn", function () {

        const data = {
            'Türkiyə': {
                minWeightPrice: 2,
                halfWeightPrice: 3,
                bigHalfWeightPrice: 4,
                weightPrice: 4.5,
            },
            'Amerika': {
                minWeightPrice: 1.99,
                halfWeightPrice: 3.99,
                bigHalfWeightPrice: 4.99,
                weightPrice: 5.99,
            }
        };

        // calculate weight
        var weightValue = $("#weight").val();
        var weightType = $("#filter-weight").text().trim();
        var weightResult = null;
        switch (weightType) {
            case "kq":
                weightResult = weightValue;
                break;
            case "qram":
                weightResult = weightValue / 1000;
                break;
            default:
                break;
        }

        // count of packets
        var count = $("#count").val();

        let country = $(".country .selected .active > span").text();
        let result = 0;

        
        if (weightResult == 0) {
            result = (count * data[country].minWeightPrice).toFixed(2);
        } else if (weightResult > 0 && weightResult <= 0.25) {
            result = (count * data[country].minWeightPrice * weightResult).toFixed(2);
        } else if (weightResult > 0.25 && weightResult <= 0.5) {
            result = (count * data[country].halfWeightPrice * weightResult).toFixed(2);
        } else if (weightResult > 0.5 && weightResult <= 0.7) {
            result = (count * data[country].bigHalfWeightPrice * weightResult).toFixed(2);
        } else {
            result = (count * data[country].weightPrice * weightResult).toFixed(2);
        }
        $(".count-result").text(`$${result}`);
    });

    $(document).on("click", "#reset-calc", function () {
        $("#height").val("");
        $("#weight").val("");
        $("#width").val("");
        $("#length").val("");
        $("#count").val("");
        $(".count-result").text("$0,00");
    })
});