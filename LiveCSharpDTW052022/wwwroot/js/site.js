$(document).ready(function () {
    $('.datepicker').datepicker({
        format: 'dd-mm-yyyy',
        autoclose: true
    });
});


$(document).ready(function () {
    $(".wish-icon i").click(function () {
        $(this).toggleClass("fa-heart fa-heart-o");
    });
});	