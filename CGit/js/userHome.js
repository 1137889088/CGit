/**
 * Created by chen on 2017/3/11.
 */

/**
* 判断头像是否存在
*如果不存在就加载默认的头像
*/

var defaultImgPath = "/img/user/default.png";
$(
    function () {
        var $img = $("#imgPreview");
        var uri = $img.attr("src");
        $.ajax({//用ajax请求文件判断是否存在
            url: uri,
            type: 'HEAD',
            error: function () {
                //如果不存在
                $img.attr("src", defaultImgPath);
                $(".rounded").attr("src", defaultImgPath)
            },
            success: function () {

            }
        });
    }
); 
/**
 * 头像上传裁剪
 */
$("#imgSelect").change(function () {
    var file = this.files[0];
    var $img = $("#imgPreview");
    var windowURL = window.URL || window.webkitURL;
    if (!(/^image\/\w+/.test(file.type))) {//判断是否是图片
        window.alert('请选择图片');
        return;
    }
    //将图片加入预览
    var imgUrl = windowURL.createObjectURL(file);
    if (imgUrl) {
        $img.attr("src", imgUrl);
    }
    //初始化裁剪
    $img.cropper({
        aspectRatio: 1 / 1,
        guides: false,
        background: true,
        minContainerHeight: 400,
        minContainerWidth: 400,
        minCanvasWidth: 400,
        minCanvasHeight: 400,
        modal: true,
        crop: function (data) {
            /*var $imgData=$img.cropper('getCroppedCanvas')
             var dataurl = $imgData.toDataURL('image/png');
             $("#preview").attr("src", dataurl)*/
        }
    });
    //替换裁剪
    $img.cropper('replace', imgUrl)
    //上传图片
    $("#upload").click(function () {
        var $imgData = $img.cropper('getCroppedCanvas')
        var dataurl = $imgData.toDataURL('image/png');  //dataurl便是base64图片
        $.ajax({
            type: "post",
            url: "/User/imgUpload",
            data: "imgData=" + dataurl,
            success: function (msg) {
                alert(msg)
                $(".rounded").attr("src", dataurl)
            }   //操作成功后的操作！msg是后台传过来的值
        });
    })
});


/**
*弹出框
*/
$(function () {
    $('#oldPwd').popover({
        trigger: 'manual', //触发方式
        placement: 'right',
        title: "",//设置 弹出框 的标题
        html: true, // 为true的话，data-content里就能放html代码了
        content: "密码错误请重新输入",//这里可以直接写字符串，也可以 是一个函数，该函数返回一个字符串；
    })
    $('#confirmPwd').popover({
        trigger: 'manual', //触发方式
        placement: 'right',
        title: "",//设置 弹出框 的标题
        html: true, // 为true的话，data-content里就能放html代码了
        content: "两次的密码不一致请重新输入",//这里可以直接写字符串，也可以 是一个函数，该函数返回一个字符串；
    })
});

/*
* 旧密码检查
*/
var checkold = false;
var checksame = false;
$("#oldPwd").blur(function () {
    var oldPwd = $("#oldPwd").val();
    $.ajax({
        type: "post",
        url: "/User/checkPwd",
        data: "oldPwd=" + oldPwd,
        success: function (msg) {
            if (msg == "false") {
                $('#oldPwd').popover("show");
                checkold = false;
            } else {
                checkold = true;
            }
        }   //操作成功后的操作！msg是后台传过来的值
    });
})
$("#oldPwd").focus(function () {
    $('#oldPwd').popover("hide");
})
/*
* 俩次的密码检查
*/
$("#oldPwd").blur(function () {
    var newPwd = $("#newPwd").val();
    var confirmPwd = $("#confirmPwd").val();
    if (newPwd != confirmPwd) {//密码不一致
        $('#confirmPwd').popover("show");
        checksame = false;
    } else {
        checksame = true;
    }
})
$("#confirmPwd").blur(function () {
    var newPwd = $("#newPwd").val();
    var confirmPwd = $("#confirmPwd").val();
    if (newPwd != confirmPwd) {//密码不一致
        $('#confirmPwd').popover("show");
        checksame = false;
    } else {
        checksame = true;
    }
})
$("#confirmPwd").focus(function () {
    $('#confirmPwd').popover("hide");
})
/**
*  修改密码
*/
$("#updatePwd").click(function () {
    if (!checkold) {
        $('#oldPwd').popover("show");
        return;
    }
    if (!checksame) {
        $('#confirmPwd').popover("show");
        return;
    }
    var newPwd = $("#newPwd").val();
    var oldPwd = $("#oldPwd").val();
    //修改密码上传
    $.ajax({
        type: "post",
        url: "/User/modifyPwd",
        data: "oldPwd=" + oldPwd + "&newPwd=" + newPwd,
        success: function (msg) {
            alert(msg)
        }   //操作成功后的操作！msg是后台传过来的值
    });
    //清空填充
    $('#modifyPwd').modal('hide');
    $("#newPwd").val("");
    $("#oldPwd").val("");
    $("#confirmPwd").val("");
})


/*function dataURLtoBlob(dataurl) {  //将base64格式图片转换为文件形式
    var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new Blob([u8arr], { type: mime });
}*/

/**
 * 取到文件上传路径
 * @param file 上传的文件
 * @returns {*} 上传的文件路径
 */
/*function getObjectURL(file) {
    var url = null;
    if (window.createObjectURL != undefined) { // basic
        url = window.createObjectURL(file);
    } else if (window.URL != undefined) { // mozilla(firefox)
        url = window.URL.createObjectURL(file);
    } else if (window.webkitURL != undefined) { // webkit or chrome
        url = window.webkitURL.createObjectURL(file);
    }
    return url;
}*/
