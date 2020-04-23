//******************************************************************************
//文 件 名： Useridlist
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 用户账号列表
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
   public  class Useridlist
    {

       public Useridlist()
        {
            this.useridlist = new List<string>();
        }
        /// <summary>
        /// 成员UserID。对应管理端的帐号
        /// </summary>
       public List<string> useridlist { get; set; }

    }
}
