var path = "/img/user/";
$(
    function () {
        var $img = $("#frameHeadImg");
        var uri = $img.attr("src");
        $.ajax({
            url: uri,
            type: 'HEAD',
            error: function () {
                $img.attr("src", path + "default.png");
            },
            success: function () {
            }
        });
    }
);