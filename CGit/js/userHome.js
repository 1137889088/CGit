/**
 * Created by chen on 2017/3/11.
 */
/**
 * 加载图片裁剪的组件
 */
$("#imgPreview").cropper({
    aspectRatio: 1 / 1,
    guides: false,
    background: true,
    minContainerHeight: 400,
    minContainerWidth: 400,
    minCanvasWidth: 400,
    minCanvasHeight: 400,
    modal: false,
    crop: function (data) {
    }
});
/**
 * 头像上传预览
 */
$("#imgSelect").change(function () {
    var files = this.files;
    var file = files[0];
    ;
    if (!(/^image\/\w+/.test(file.type))) {
        window.alert('请选择图片');
        return;
    }
    $("#imgPreview").cropper("destroy");
    var imgUrl = getObjectURL(this.files[0]);
    if (imgUrl) {
        $("#imgPreview").attr("src", imgUrl);
    }
    $('.container > img').cropper({
        aspectRatio: 1 / 1,
        guides: false,
        background: true,
        minContainerHeight: 400,
        minContainerWidth: 400,
        minCanvasWidth: 400,
        minCanvasHeight: 400,
        modal: true,
        crop: function (data) {
            var croppedCanvas = $('.container > img').cropper('getCroppedCanvas');
            var dataURL = croppedCanvas.toDataURL();
            //console.log(dataURL);
        }
    });
});
/**
 * 取到文件上传路径
 * @param file 上传的文件
 * @returns {*} 上传的文件路径
 */
function getObjectURL(file) {
    var url = null;
    if (window.createObjectURL != undefined) { // basic
        url = window.createObjectURL(file);
    } else if (window.URL != undefined) { // mozilla(firefox)
        url = window.URL.createObjectURL(file);
    } else if (window.webkitURL != undefined) { // webkit or chrome
        url = window.webkitURL.createObjectURL(file);
    }
    return url;
}
