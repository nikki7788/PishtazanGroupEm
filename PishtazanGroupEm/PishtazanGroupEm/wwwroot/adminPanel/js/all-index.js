

//این کد کلی است و برای همه مودال ها جواب می دهد نیاز نیست که باری هر مودال جداگانه بنویسیم
$(document).ready(function () {

    $("a").click(function () {
        //e.preventDefault();
        var item = $(this);
        
        //data-target
        var id = $(this).data("target");
        $(id).on("show.bs.modal", function () {

            //$(this) = index view
            $(this).find(".modal-content").load(item.attr("href"));
        });
    });

});