$(document).ready(function () {
    GetStates();
    GetCities();
});
$("#countryDropdown").change(function () {
    GetStates();
});
var GetStates = function () {
    $.ajax({
        url: '/Employee/GetStates',
        type: "GET",
        data: {
            countryId: $("#countryDropdown").val(),
        },
        success: function (data) {
            $('#stateDropdown').find('option').remove();
            $(data).each(
                function (index, item) {
                    $('#stateDropdown').append('<option value="' + item.stateId + '">' + item.stateName + '</option>')
                }
            );
        }
    })
};

$("#stateDropdown").change(function () {
    GetCities();
});
var GetCities = function () {
    $.ajax({
        url: '/Employee/GetCities',
        type: "GET",
        data: {
            stateId: $("#stateDropdown").val(),
        },
        success: function (data) {
            $('#cityDropdown').find('option').remove();
            $(data).each(
                function (index, item) {
                    $('#cityDropdown').append('<option value="' + item.cityId + '">' + item.cityName + '</option>')
                }
            );
        }
    })
};