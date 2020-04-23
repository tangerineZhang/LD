using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Mvc;

using ThoughtWorks.QRCode.Codec;
using System.Drawing;
using System.IO;

namespace LD.DAL
{
    public partial class QRCode
    {
        public QRCode()
        { }

        #region 生成二维码
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        //private static string QRCode(string enCodeString)
        //{
        //    enCodeString = "http://www.baidu.com";

        //    System.Drawing.Bitmap bt;
        //    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        //    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
        //    qrCodeEncoder.QRCodeScale = 4;//大小(值越大生成的二维码图片像素越高)
        //    qrCodeEncoder.QRCodeVersion = 0;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
        //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//错误效验、错误更正(有4个等级)
        //    qrCodeEncoder.QRCodeBackgroundColor = Color.Yellow;//背景色
        //    qrCodeEncoder.QRCodeForegroundColor = Color.Green;//前景色

        //    bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);

        //    string filename = "code";
        //    string file_path = AppDomain.CurrentDomain.BaseDirectory + "QRCode\\";
        //    string codeUrl = file_path + filename + ".jpg";

        //    //根据文件名称，自动建立对应目录
        //    if (!System.IO.Directory.Exists(file_path))
        //        System.IO.Directory.CreateDirectory(file_path);

        //    bt.Save(codeUrl);//保存图片
        //    return codeUrl;
        //}
        #endregion

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="enCodeString">要生成二维码的文字</param>
        /// <returns></returns>
        //[HttpGet]
        //[AllowAnonymous]
        public string CreateQRCode(string enCodeString,string filename)
        {
            System.Drawing.Bitmap bt;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
            qrCodeEncoder.QRCodeScale = 10;//大小(值越大生成的二维码图片像素越高)
            qrCodeEncoder.QRCodeVersion = 0;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//错误效验、错误更正(有4个等级)
            qrCodeEncoder.QRCodeBackgroundColor = Color.White;//背景色
            qrCodeEncoder.QRCodeForegroundColor = Color.Black;//前景色
            bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);
            //string filename = enCodeString;// "code";
            string file_path = AppDomain.CurrentDomain.BaseDirectory + "QRCode\\";
            string codeUrl = file_path + filename + ".jpg";

            //根据文件名称，自动建立对应目录
            if (!System.IO.Directory.Exists(file_path))
            {
                System.IO.Directory.CreateDirectory(file_path);
            }
            ////防止内容重复，导致名称重复问题，
            ////若要每次更新，可去掉本段代码 ↓↓↓↓↓
            //int i = 1;
            //while (System.IO.File.Exists(codeUrl))
            //{               
            //    string _filename = filename + "("+i+")";
            //    codeUrl = file_path + _filename + ".jpg";
            //    i++;
            //}
            ////   ↑↑↑↑↑↑↑

            bt.Save(codeUrl);//保存图片
            return codeUrl;
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="enCodeString">要生成二维码的文字</param>
        /// <returns></returns>
        //[HttpGet]
        //[AllowAnonymous]
        public string CreateQRCode(string enCodeString)
        {
            System.Drawing.Bitmap bt;
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
            qrCodeEncoder.QRCodeScale = 10;//大小(值越大生成的二维码图片像素越高)
            qrCodeEncoder.QRCodeVersion = 0;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//错误效验、错误更正(有4个等级)
            qrCodeEncoder.QRCodeBackgroundColor = Color.White;//背景色
            qrCodeEncoder.QRCodeForegroundColor = Color.Black;//前景色
            bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8);
            string filename = enCodeString;// "code";
            string file_path = AppDomain.CurrentDomain.BaseDirectory + "QRCode\\";
            string codeUrl = file_path + filename + ".jpg";

            //根据文件名称，自动建立对应目录
            if (!System.IO.Directory.Exists(file_path))
            {
                System.IO.Directory.CreateDirectory(file_path);
            }
            ////防止内容重复，导致名称重复问题，
            ////若要每次更新，可去掉本段代码 ↓↓↓↓↓
            //int i = 1;
            //while (System.IO.File.Exists(codeUrl))
            //{               
            //    string _filename = filename + "("+i+")";
            //    codeUrl = file_path + _filename + ".jpg";
            //    i++;
            //}
            ////   ↑↑↑↑↑↑↑

            bt.Save(codeUrl);//保存图片
            return codeUrl;
        }


        /// <summary>
        /// 生成二维码【方法二】
        /// </summary>
        /// <param name="qrCodeScale">尺寸4-15</param>
        /// <param name="qrCodeVersion">复杂级别3-12</param>
        /// <param name="qrCodeErrorCorrect">容错量"H","L","M","Q"</param>
        /// <param name="enCodeString">二维码信息</param>        
        /// <returns></returns>
        //[HttpGet]
        //[AllowAnonymous]
        public string CreateEQcoder(int qrCodeScale, int qrCodeVersion, string qrCodeErrorCorrect, string enCodeString)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = qrCodeScale;
            qrCodeEncoder.QRCodeVersion = qrCodeVersion;
            switch (qrCodeErrorCorrect)
            {
                case "H":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
                case "L":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                    break;
                case "M":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    break;
                case "Q":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    break;
            }
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            Bitmap bmPhoto = qrCodeEncoder.Encode(enCodeString, System.Text.Encoding.GetEncoding("UTF-8"));
            string fileName = enCodeString + "_" + qrCodeScale + "_" + qrCodeVersion + "_" + qrCodeErrorCorrect;
            string file_path = AppDomain.CurrentDomain.BaseDirectory + "QRCode\\";
            string savePath = file_path + fileName + ".jpg";
            try
            {
                if (!Directory.Exists(file_path))
                {
                    Directory.CreateDirectory(file_path);
                }

                //防止内容重复，导致名称重复问题，若要每次更新，可去掉本段代码 ↓↓↓↓↓
                int i = 1;
                while (System.IO.File.Exists(savePath))
                {
                    string _filename = fileName + "(" + i + ")";
                    savePath = file_path + _filename + ".jpg";
                    i++;
                }
                //↑↑↑↑↑↑↑
                bmPhoto.Save(savePath);
                bmPhoto.Dispose();
                return savePath;
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                bmPhoto.Dispose();
            }
        }

    }
}
