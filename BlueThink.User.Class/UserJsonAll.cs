//******************************************************************************
//文 件 名： UserJsonAll
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 详细用户信息的Json
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
    public class UserJsonAll
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
        public List<int> department { get; set; }  

        /// <summary>
        /// 职位信息
        /// </summary>
        public string position { get; set; }

        /// <summary>
        /// 手机号码 
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 性别。0表示未定义，1表示男性，2表示女性 
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// 邮箱 
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 微信号 
        /// </summary>
        public string weixinid { get; set; }

        /// <summary>
        /// 头像url。注：如果要获取小图将url最后的"/0"改成"/64"即可 
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 关注状态: 1=已关注，2=已冻结，4=未关注 
        /// </summary>
        public int status { get; set; }

        /// <summary>
        ///  扩展属性   "extattr": {"attrs":[{"name":"爱好","value":"旅游"},{"name":"卡号","value":"1234567234"}]} 
        /// </summary>
        public UserJsonAttrs extattr { get; set; }
    }
}
