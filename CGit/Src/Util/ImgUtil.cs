using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace CGit.Src.Util
{
    public static class ImgUtil
    {
        public const int TYPE_JPG = 1;
        public const int TYPE_BMP = 2;
        public const int TYPE_GIF = 3;
        public const int TYPE_PNG = 4;

        public static object PhysicalApplicationPath { get; private set; }

        public static void Base64StringToImage(string base64Img,string filePath,int imgType)
        {
            try
            {
                base64Img = base64Img.Split(',')[1];
                //base64Img.Replace(' ', '+');
                //string base64 = "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";
                byte[] arr = Convert.FromBase64String(base64Img);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                switch (imgType)
                {
                    case TYPE_JPG:
                        bmp.Save(filePath + ".jpg", ImageFormat.Jpeg);
                        break;
                    case TYPE_BMP:
                        bmp.Save(filePath + ".bmp", ImageFormat.Bmp);
                        break;
                    case TYPE_GIF:
                        bmp.Save(filePath + ".gif", ImageFormat.Gif);
                        break;
                    case TYPE_PNG:
                        bmp.Save(filePath + ".png", ImageFormat.Png);
                        break;
                }
                ms.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("图片转换失败");
            }
        }
        public static void saveImg(string path,string content)
        {
           
            System.IO.StreamWriter sw = new System.IO.StreamWriter(path);
            sw.Write(content);
            sw.Close();
        }
    }
}