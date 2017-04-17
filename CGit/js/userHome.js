/**
 * Created by chen on 2017/3/11.
 */

/**
 * 头像上传预览
 */
$("#imgSelect").change(function () {
    var file = this.files[0];
    var $img = $("#imgPreview");
    var windowURL = window.URL || window.webkitURL;
    if (!(/^image\/\w+/.test(file.type))) {
        window.alert('请选择图片');
        return;
    }
    var imgUrl = windowURL.createObjectURL(file);
    if (imgUrl) {
        $img.attr("src", imgUrl);
    }
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
            /* var $imgData=$img.cropper('getCroppedCanvas')
             var dataurl = $imgData.toDataURL('image/png');
             $("#previewyulan").attr("src",dataurl)*/
        }
    });
    $img.cropper('replace', imgUrl)
   
    $("#confirm").click(function () {
        var $imgData = $img.cropper('getCroppedCanvas')
        var dataurl = $imgData.toDataURL('image/png');  //dataurl便是base64图片
        console.log(dataurl)
        /* $("#previewyulan").attr("src",dataurl)
         //下面两种方法需要用到那种使用哪种即可,或者都不使用直接上传base64文件给后台即可，哈哈
         blob = dataURLtoBlob(dataurl);   //将base64图片转化为blob文件方法*/
    })
});

function dataURLtoBlob(dataurl) {  //将base64格式图片转换为文件形式
    var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new Blob([u8arr], { type: mime });
}
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
