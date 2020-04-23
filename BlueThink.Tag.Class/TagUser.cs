//******************************************************************************
//文 件 名： TagUser
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 标签成员Json
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
    public class TagUser
    {
        /// <summary>
        /// 成员UserID。对应管理端的帐号
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 成员名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 成员所属部门id列表
        /// </summary>
        public List<Int64> department { get; set; }  
    }
}
