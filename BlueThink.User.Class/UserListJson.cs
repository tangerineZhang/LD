//******************************************************************************
//文 件 名： UserListJson
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 获取部门成员返回的数据
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
    /// <summary>
    /// 获取部门成员返回的数据
    /// </summary>
    public class UserListJson
    {

        public UserListJson()
        {
            this.userlist = new List<UserJson>();
        }

        /// <summary>
        /// 返回的错误消息
        /// </summary>
        public string errcode { get; set; }

        /// <summary>
        /// 对返回码的文本描述内容
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 成员列表
        /// </summary>
        public List<UserJson> userlist { get; set; }

    }
    
}
