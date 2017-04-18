var path = "/img/user/";
$(
    function () {
        var $img = $("#frameHeadImg");
        var uri = $img.attr("src");
        $.ajax({
            url: uri,
            type: 'HEAD',
            error: function () {
                console.info("file not");
                $img.attr("src", path + "default.png");
            },
            success: function () {
                console.info("file exists");
            }
        });
    }
);