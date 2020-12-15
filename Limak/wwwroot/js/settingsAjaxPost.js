$(function () {
    //Profil info update start
    $(document).on("click", "#profile-submit-btn", function (e) {
        e.preventDefault();

        var phone = $('#profile input[name="phone"]').val().trim();
        var deliveryOffice = $("#profile #delivery-office").text().trim();
        var dayOfBirth = $("#profile #dayOfBirth").text().trim();
        var monthOfBirth = $("#profile #month-name").text().trim();
        var yearOfBirth = $("#profile #year").text().trim();
        
        var data_ = {

            PhoneNumber: phone,
            DeliveryOffice: deliveryOffice,
            DayOfBirth: dayOfBirth,
            MonthOfBirth: monthOfBirth,
            YearOfBirth: yearOfBirth
        };

        if (phone) {
            $.ajax({
                url: "/User/UpdateProfileInformation",
                data: data_,
                dataType: "json",
                type: "post",
                success: function (d) {
                    if (d.success) {
                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Tamamlandı',
                            text:"Müvəffəqiyytlə əlavə edildi!",
                            showConfirmButton: true,

                        })
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
        //else {
        //    Swal.fire({
        //        position: 'center',
        //        icon: 'error',
        //        title: 'Error appeared',
        //        showConfirmButton: true,

        //    })
        //}

    })
    $(document).on("keyup", "#phone", function () {

        var value = $(this).val().trim();
        if (!value) {
            $("#number-validation").show();
        }
        else {
            $("#number-validation").hide();
        }

    })
    //Profil info update end
    //Id data update start
    $(document).on("click", "#id-info-submit-btn", function (e) {
        e.preventDefault();
        var serialNumber = $('#passport_data input[name="serialNumber"]').val().trim();
        var citizenship = $('#passport_data input[name="nationality"]').val().trim();
        var idInfoGender = $('#passport_data #id-info-gender').text().trim();
        var address = $('#passport_data input[name="address"]').val().trim();
        var data_ = {

            SeriaNumber: serialNumber,
            CitizenShip: citizenship,
            Gender: idInfoGender,
            IdAddress: address
        };

        if (serialNumber && citizenship && address) {

            $.ajax({
                url: "/User/UpdateIdInformation",
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
        //else {
        //    Swal.fire({
        //        position: 'center',
        //        icon: 'error',
        //        title: 'Error appeared',
        //        showConfirmButton: true,

        //    })
        //}

    })


    $(document).on("keyup", "#passport_data input", function () {

        var value = $(this).val().trim();
        if (!value) {
            $(this).parent().next(".password-data-validation").show();
        }
        else {
            $(this).parent().next(".password-data-validation").hide();
        }
    })
    //Id data update end
    //Update Passwrod start

    $(document).on("click", "#update-password-submit-btn", function (e) {
        e.preventDefault();
        
        var currentPassword = $('#update-password input[name="currentPassword"]').val().trim();
        var newPassword = $('#update-password input[name="password"]').val().trim();
        var cPassword = $('#update-password input[name="cpassword"]').val().trim();
        
        //var pattern = /^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%&]).*$/;
        if (!currentPassword) {
            $('#update-password input[name="currentPassword"]').parent().parent().find(".password-data-validation").first().show(); 
        }
        if (!newPassword) {
            $('#update-password input[name="password"]').parent().parent().find(".password-data-validation").first().show();
        }
        if (!cPassword) {
            $('#update-password input[name="cpassword"]').parent().parent().find(".password-data-validation").first().show();
        }
        if (cPassword && newPassword && currentPassword) {
            if (cPassword == newPassword) {
                if (newPassword.length >= 6) {
                    var data_ = {
                        CurrentPassword: currentPassword,

                        NewPassword: newPassword,
                        ConfirmPassword: cPassword
                    };


                    $.ajax({
                        url: "/User/ChangePassword",
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
                    })
                }
                else {
                    $('#update-password input[name="password"]').parent().parent().find(".password-requirement-error").show();
                   
                }

            }
           
            else {
                
           
                $('#update-password input[name="cpassword"]').parent().parent().find(".not-same-error").show();
                $('#update-password input[name="cpassword"]').parent().parent().find(".password-data-validation").first().hide();

            }
        }
        
        
    })
    $(document).on("keyup", "#update-password input", function () {

        var value = $(this).val().trim();
        if (!value) {
            $(this).parent().parent().find(".password-data-validation").hide();
            $(this).parent().parent().find(".password-data-validation").first().show();

        }
        else {
            $(this).parent().next(".password-data-validation").hide();
            $('#update-password input[name="cpassword"]').parent().parent().find(".not-same-error").hide();
            $('#update-password input[name="password"]').parent().parent().find(".password-requirement-error").hide(); 
        }
    })
})