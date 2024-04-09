$(function (e) {
    
    $("li").click(function (e) {
        $("li").removeAttr("class");
        $(this).attr("class", "active");
    });
});