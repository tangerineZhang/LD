//******************************************************************************
//文 件 名： UserJsonAttrsInfo
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 用户的扩展信息明细
//--------------------------------------------------------------------------------
//修改人：
//修改原因：
//修改日期:

//******************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueThink.User.Class
{
    public class UserJsonAttrsInfo
    {
        /// <summary>
        /// 扩展 name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 扩展 value
        /// </summary>
        public string value { get; set; }
    }
}
