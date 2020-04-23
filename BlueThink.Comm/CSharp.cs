using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.IO;




namespace BlueThink.Comm
{
    class CSharp
    {
    }
    public partial class VerificationCode
    {

        //产生验证码的字符集
        private static string[] ValidateCharArray = new string[] { "2", "3", "4", "5", "6", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "M", "N", "P", "R", "S", "U", "W", "X", "Y", "a", "b", "c", "d", "e", "f", "g", "h", "j", "k", "m", "n", "p", "r", "s", "u", "w", "x", "y" };
        //产生验证码的算术符号
        private static string[] ArithmeticSymbol = new string[] { "＋", "－", "×", "÷" };
        //64位图片字符串开头描述
        private const string imageStr = "data:image/{0};base64,";

        ///<summary>
        ///生成随机验证码（数字字母）
        ///</summary>
        ///<param name="length">验证码的长度</param>
        ///<returns></returns>
        public string CreateValidateNumber(int length)
        {
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(ValidateCharArray.Length);
                if (temp != -1 && temp == t)
                {
                    return CreateValidateNumber(length);
                }
                temp = t;
                randomCode += ValidateCharArray[t];
            }
            return randomCode;
        }

        /// <summary>
        /// 生成随机验证码（常用汉字）
        /// </summary>
        /// <param name="length">要产生常用汉字的个数</param>
        /// <returns></returns>
        public static string CreateValidateChinese(int length)
        {
            /*gb2312中文编码表由区码和位码表示*/
            string randomCode = "";
            Random rand = new Random();
            Encoding encoding = Encoding.GetEncoding("gb2312");//从gb2312中文编码表提取汉字
            for (int i = 0; i < length; i++)
            {
                int regionCode = rand.Next(16, 56); // 获取区码(常用汉字的区码范围为16-55)    
                int locationCode = rand.Next(1, (regionCode == 55 ? 90 : 95));// 获取位码(位码范围为1-94 由于55区的90,91,92,93,94为空,故将其排除)

                randomCode += encoding.GetString(new byte[] { (byte)(regionCode + 160), (byte)(locationCode + 160) });//区码位码分别加上A0H（160）的方法转换为存储码
            }
            return randomCode;
        }

        ///<summary>
        ///生成随机验证码（算术等式）
        ///</summary>
        ///<param name="length">验证码的长度</param>
        ///<returns></returns>
        public static ArithmeticEquation CreateValidateArithmetic()
        {
            Random rand = new Random();
            ArithmeticEquation ai = new ArithmeticEquation();
            ai.text = new List<string>();
            ai.text.Add("");
            ai.text.Add("");
            ai.text.Add("");
            ai.text.Add("＝");

            ai.text[1] = ArithmeticSymbol[rand.Next(0, 4)];

            if (ai.text[1] == ArithmeticSymbol[0])//加
            {
                int len = rand.Next(1, 3);
                if (len == 1)
                {
                    ai.text[0] = rand.Next(0, 10).ToString();
                }
                else
                {
                    ai.text[0] = rand.Next(1, 10).ToString();
                    ai.text[0] += rand.Next(0, 10).ToString();
                }

                if (len == 1)
                {
                    ai.text[2] = rand.Next(1, 10).ToString();
                    ai.text[2] += rand.Next(0, 10).ToString();
                }
                else
                {
                    ai.text[2] = rand.Next(0, 10).ToString();
                }
            }
            else if (ai.text[1] == ArithmeticSymbol[1])//减
            {
                int len = rand.Next(1, 3);
                if (len == 1)
                {
                    ai.text[0] = rand.Next(0, 10).ToString();
                }
                else
                {
                    ai.text[0] = rand.Next(1, 10).ToString();
                    ai.text[0] += rand.Next(0, 10).ToString();
                }

                if (len == 1)
                {
                    ai.text[2] = rand.Next(0, int.Parse(ai.text[0]) + 1).ToString();
                }
                else
                {
                    ai.text[2] = rand.Next(0, 10).ToString();
                }
            }
            else if (ai.text[1] == ArithmeticSymbol[2])//乘
            {
                ai.text[0] = rand.Next(1, 10).ToString();

                ai.text[2] = rand.Next(1, 10).ToString();
            }
            else
            {
                ai.text[2] = rand.Next(1, 10).ToString();

                ai.text[0] = (int.Parse(ai.text[2]) * rand.Next(1, 10)).ToString();

            }

            ai.value = calculate(ai.text[1], int.Parse(ai.text[0]), int.Parse(ai.text[2])).ToString();

            return ai;
        }

        ///<summary>
        ///创建验证码的图片
        ///</summary>
        ///<param name="bitmapParam">验证码参数</param>
        ///<param name="isGif">Gif样式</param>
        //public static Bitmap CreateValidateImage(BitmapParam bitmapParam, BitmapStyle bitmapStyle)
        //{
        //    Bitmap result = null;
        //    Random random = new Random();
        //    string text = String.Concat(bitmapParam.textarr);
        //    int count = bitmapParam.textarr.Count;

        //    Bitmap bmp_template = new Bitmap(bitmapParam.width, bitmapParam.height);
        //    Graphics g_template = Graphics.FromImage(bmp_template);
        //    #region 绘制干扰线

        //    g_template.Clear(bitmapParam.backColor);
        //    using (Pen pen = new Pen(Color.Silver))
        //    {
        //        for (int i = 0; i < 25; i++)
        //        {
        //            int x1 = random.Next(bmp_template.Width);
        //            int x2 = random.Next(bmp_template.Width);
        //            int y1 = random.Next(bmp_template.Height);
        //            int y2 = random.Next(bmp_template.Height);
        //            g_template.DrawLine(pen, x1, y1, x2, y2);
        //        }
        //    }

        //    #endregion

        //    List<Dot> dotlist = new List<Dot>();
        //    #region 计算干扰点坐标

        //    for (int i = 0; i < 100; i++)
        //    {
        //        dotlist.Add(new Dot() { gyd = new Point(random.Next(bmp_template.Width), random.Next(bmp_template.Height)), color = Color.FromArgb(random.Next()) });
        //    }
        //    #endregion

        //    int defaultfontsize = 7;
        //    int defaultfontmargin = 3;
        //    #region 根据RECT计算文本字体大小

        //    for (int i = defaultfontsize; i < 300; i++)
        //    {
        //        using (Font f = new Font(bitmapParam.fontName, i, bitmapParam.fontStyle))
        //        {
        //            if (g_template.MeasureString(text, f).Width > bitmapParam.width - defaultfontmargin * 2)
        //            {
        //                defaultfontsize = i;
        //                break;
        //            }
        //        }
        //    }
        //    for (int i = defaultfontsize; i > 1; i--)
        //    {
        //        using (Font f = new Font(bitmapParam.fontName, i, bitmapParam.fontStyle))
        //        {
        //            if (g_template.MeasureString(text, f).Height < bitmapParam.height - defaultfontmargin * 2)
        //            {
        //                defaultfontsize = i;
        //                break;
        //            }
        //        }
        //    }

        //    #endregion
        //    Font text_font = new Font(bitmapParam.fontName, defaultfontsize, bitmapParam.fontStyle);
        //    SizeF text_size = g_template.MeasureString(text, text_font);
        //    LinearGradientBrush text_brush = new LinearGradientBrush(new Rectangle(0, 0, bmp_template.Width, bmp_template.Height), Color.FromArgb(193, 0, 91), Color.FromArgb(136, 193, 0), 1.2f, true);

        //    float x_align = ((float)bitmapParam.width - text_size.Width) / 2.0f;
        //    float y_align = ((float)bitmapParam.height - text_size.Height) / 2.0f;
        //    List<BitmapFrame> bitmapframelist = new List<BitmapFrame>();
        //    if (!bitmapStyle.IsGif || (bitmapStyle.IsGif && bitmapStyle.FrameType == GifFrameType.FullFrame))
        //    {
        //        bitmapframelist.Add(new BitmapFrame() { frame = (Bitmap)bmp_template.Clone() });
        //        using (Graphics graphics = Graphics.FromImage(bitmapframelist[0].frame))
        //        {
        //            graphics.DrawString(text, text_font, text_brush, x_align, y_align);
        //            foreach (Dot dot in dotlist)
        //            {
        //                bitmapframelist[0].frame.SetPixel(dot.gyd.X, dot.gyd.Y, dot.color);
        //            }
        //        }
        //        if (!bitmapStyle.IsGif)
        //        {
        //            result = bitmapframelist[0].frame;
        //        }
        //        else
        //        {
        //            GifCore gif = new GifCore(bitmapStyle.Delay, bitmapStyle.Repeat);
        //            foreach (Dot dot in dotlist)
        //            {
        //                bmp_template.SetPixel(dot.gyd.X, dot.gyd.Y, dot.color);
        //            }
        //            gif.AddFrame(bmp_template, delay: -1);
        //            gif.AddFrame((Image)bitmapframelist[0].frame, delay: -1);
        //            result = gif.Finish();
        //            foreach (BitmapFrame item in bitmapframelist)
        //            {
        //                item.frame.Dispose();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < count; i++)
        //        {
        //            bitmapframelist.Add(new BitmapFrame() { frame = (Bitmap)bmp_template.Clone() });
        //        }

        //        #region 计算每一帧文字坐标


        //        if (count == 1)
        //        {
        //            bitmapframelist[0].x = x_align;
        //            bitmapframelist[0].y = y_align;
        //            bitmapframelist[0].z_width = 0;
        //        }
        //        else
        //        {
        //            for (int k = 0; k < count; k++)
        //            {
        //                if (k == 0)
        //                {
        //                    bitmapframelist[0].x = x_align;
        //                    bitmapframelist[0].y = y_align;
        //                    bitmapframelist[0].z_width = g_template.MeasureString(GetScopeString(bitmapParam.textarr, 0, k + 2), text_font).Width - g_template.MeasureString(bitmapParam.textarr[k + 1], text_font).Width;
        //                }
        //                else
        //                {
        //                    bitmapframelist[k].x = bitmapframelist[k - 1].x + bitmapframelist[k - 1].z_width;
        //                    bitmapframelist[k].y = y_align;
        //                    bitmapframelist[k].z_width = g_template.MeasureString(GetScopeString(bitmapParam.textarr, 0, k + 1), text_font).Width - g_template.MeasureString(GetScopeString(bitmapParam.textarr, 0, k), text_font).Width;
        //                }
        //            }
        //        }

        //        #endregion

        //        #region 绘制每一帧文字坐标

        //        for (int k = 0; k < count; k++)
        //        {
        //            using (Graphics graphics = Graphics.FromImage(bitmapframelist[k].frame))
        //            {
        //                graphics.DrawString(bitmapParam.textarr[k], text_font, text_brush, bitmapframelist[k].x, bitmapframelist[k].y);
        //                foreach (Dot dot in dotlist) //画图片的前景干扰点
        //                {
        //                    bitmapframelist[k].frame.SetPixel(dot.gyd.X, dot.gyd.Y, dot.color);
        //                }
        //            }
        //        }

        //        #endregion

        //        #region 生成Gif

        //        GifCore gif = new GifCore(bitmapStyle.Delay, bitmapStyle.Repeat);
        //        if (count == 1)
        //        {
        //            foreach (Dot dot in dotlist)
        //            {
        //                bmp_template.SetPixel(dot.gyd.X, dot.gyd.Y, dot.color);
        //            }
        //            gif.AddFrame(bmp_template, delay: -1);
        //        }
        //        foreach (BitmapFrame item in bitmapframelist)
        //        {
        //            gif.AddFrame((Image)item.frame, delay: -1);
        //        }
        //        result = gif.Finish();
        //        foreach (BitmapFrame item in bitmapframelist)
        //        {
        //            item.frame.Dispose();
        //        }

        //        #endregion
        //    }

        //    text_font.Dispose();
        //    text_brush.Dispose();
        //    g_template.Dispose();
        //    bmp_template.Dispose();
        //    return result;

        //}

        //
        ///<summary>
        ///创建验证码的图片
        ///</summary>
        ///<param name="bitmapParam">验证码参数</param>
        ///<param name="isGif">Gif样式</param>
        //public static byte[] CreateValidateImageBytes(BitmapParam bitmapParam, BitmapStyle bitmapStyle)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        Bitmap image = CreateValidateImage(bitmapParam, bitmapStyle);
        //        image.Save(ms, bitmapStyle.IsGif ? ImageFormat.Gif : ImageFormat.Jpeg);
        //        if (image != null)
        //            image.Dispose();
        //        return ms.ToArray();
        //    }
        //}

        //
        ///<summary>
        ///创建验证码的图片
        ///</summary>
        ///<param name="bitmapParam">验证码参数</param>
        ///<param name="isGif">Gif样式</param>
        //public static string CreateValidateImageBase64String(BitmapParam bitmapParam, BitmapStyle bitmapStyle)
        //{
        //    return String.Format(imageStr, bitmapStyle.IsGif ? "gif" : "jpeg") + Convert.ToBase64String(CreateValidateImageBytes(bitmapParam, bitmapStyle));
        //}

        /// <summary>
        /// 拆分文本
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> StringToArray(string str)
        {
            str = str.Trim();
            List<string> result = new List<string>();
            for (int i = 0; i < str.Length; i++)
            {
                result.Add(str.Substring(i, 1));
            }
            return result;
        }

        /// <summary>
        ///获取数组指定范围字符 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetScopeString(List<string> arr, int start, int len)
        {
            string result = String.Empty;
            for (int i = 0; i < len; i++)
            {
                result += arr[start + i];
            }
            return result;
        }

        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        private static int calculate(string type, int value1, int value2)
        {
            if (type == ArithmeticSymbol[0])//加
            {
                return value1 + value2;
            }
            else if (type == ArithmeticSymbol[1])//减
            {
                return value1 - value2;
            }
            else if (type == ArithmeticSymbol[2])//乘
            {
                return value1 * value2;
            }
            else
            {
                return value1 / value2;
            }
        }

        /// <summary>
        /// 干扰点
        /// </summary>
        private class Dot
        {
            public Point gyd;
            public Color color;
        }

        /// <summary>
        /// 图片帧
        /// </summary>
        private class BitmapFrame
        {
            /// <summary>
            /// 帧
            /// </summary>
            public Bitmap frame;
            public float x;
            public float y;
            public float z_width;
        }

        /// <summary>
        /// 验证码（算术等式）
        /// </summary>
        public class ArithmeticEquation
        {
            /// <summary>
            /// 算术等式
            /// </summary>
            public List<string> text;
            /// <summary>
            /// 算术值
            /// </summary>
            public string value;
        }

        /// <summary>
        /// Gif帧类型
        /// </summary>
        public enum GifFrameType
        {
            /// <summary>
            /// 每个字符一帧
            /// </summary>
            EveryFrame,
            /// <summary>
            /// 全部字符一帧
            /// </summary>
            FullFrame
        }

        /// <summary>
        /// 验证码参数
        /// </summary>
        public class BitmapParam
        {
            public BitmapParam()
            {

            }

            public BitmapParam(List<string> _textarr, int _width, int _height, Color _backColor, string _fontName, Color _fontBrushStrat, Color _fontBrushEnd, FontStyle _fontStyle)
            {
                this.textarr = _textarr;
                this.width = _width;
                this.height = _height;
                this.backColor = _backColor;
                this.fontName = _fontName;
                this.fontBrushStrat = _fontBrushStrat;
                this.fontBrushEnd = _fontBrushEnd;
                this.fontStyle = _fontStyle;
            }

            /// <summary>
            /// 验证码字符集
            /// </summary>
            public List<string> textarr;
            /// <summary>
            /// 验证码图片宽
            /// </summary>
            public int width = 120;
            /// <summary>
            /// 验证码图片高
            /// </summary>
            public int height = 50;

            /// <summary>
            /// 图片背景颜色
            /// </summary>
            public Color backColor = Color.White;

            /// <summary>
            /// 字体
            /// </summary>
            public string fontName = "微软雅黑";

            /// <summary>
            /// 字体画笔颜色开始
            /// </summary>
            public Color fontBrushStrat = Color.FromArgb(193, 0, 91);

            /// <summary>
            /// 字体画笔颜色结束
            /// </summary>
            public Color fontBrushEnd = Color.FromArgb(136, 193, 0);

            /// <summary>
            /// 验证码字体样式
            /// </summary>
            public FontStyle fontStyle = FontStyle.Bold | FontStyle.Italic;
        }

        /// <summary>
        /// Gif样式
        /// </summary>
        public class BitmapStyle
        {
            public BitmapStyle()
            {

            }

            public BitmapStyle(bool _IsGif, GifFrameType _FrameType, int _Delay, int _Repeat)
            {
                this.IsGif = _IsGif;
                this.FrameType = _FrameType;
                this.Delay = _Delay;
                this.Repeat = _Repeat;
            }

            /// <summary>
            /// 是否生成Gif动画
            /// </summary>
            public bool IsGif = false;
            /// <summary>
            ///  Gif帧类型
            /// </summary>
            public GifFrameType FrameType = GifFrameType.EveryFrame;
            /// <summary>
            /// 帧之间的延迟(毫秒)
            /// </summary>
            public int Delay = 500;
            /// <summary>
            /// GIF重复计数(0表示永久)
            /// </summary>
            public int Repeat = 0;
        }
    }
}
