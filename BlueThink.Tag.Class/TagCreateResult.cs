//******************************************************************************
//文 件 名： TagCreateResult
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 标签创建的返回值

//--------------------------------------------------------------------------------
//修改人：
//修改原因：
//修改日期:

//******************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueThink.Tag.Class
{
    public class TagCreateResult
    {
        /// <summary>
        /// 返回的错误消息
        /// </summary>
        public string errcode { get; set; }

        /// <summary>
        /// 对返回码的文本描述内容
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 标签ID 
        /// </summary>
        public string tagid { get; set; }
    }
}
