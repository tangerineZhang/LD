//******************************************************************************
//文 件 名： HttpHelper
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： Http GET与POST操作相关

//--------------------------------------------------------------------------------
//修改人：
//修改原因：
//修改日期:

//******************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace BlueThink.Comm
{
    /// <summary>
    /// GET与POST操作相关
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// GET请求与获取结果
        /// </summary>
        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request ;
            if (string.IsNullOrEmpty(postDataStr))
            {
                request = (HttpWebRequest)WebRequest.Create(Url);
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            }
                
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }


        /// <summary>
        /// POST请求与获取结果
        /// </summary>
        public static string HttpPost(string Url, string postDataStr)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            StreamWriter writer = new StreamWriter(request.GetRequestStream()); 
            writer.Write(postDataStr);
            writer.Flush();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;

        }

    }
}
