

///این کد کلی است و برای همه مودال ها جواب می دهد نیاز نیست که باری هر مودال جداگانه بنویسیم
$(document).ready(function () {

    $("a[data-toggle='modal']").on('click', function (e) {
        e.preventDefault();

        //برای اسکرول کردن داینامیک
        //وقتی موس روی مودال است مودال با موس اسکرول شود وقتی روی صفحه اصلی صفحه اصلی اسکرول شود
        $("html").removeClass("perfect-scrollbar-on").addClass("perfect-scrollbar-off");


        var item = $(this);

        //data-target
        var id = $(this).data("target");
        $(id).on("show.bs.modal", function () {
            //$(this) = index view
            $('.modal').toggleClass('scroll-enable');
            $(this).find(".modal-content").load(item.attr("href"));
        });
    });
});

//$('body', ".admin-panel").addClass('scroll-disable'); // or whatever div is scrolling