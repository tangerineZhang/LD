//******************************************************************************
//文 件 名： TagUserCreate
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 标签成员创建Json
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
    public class TagUserCreate
    {
        public TagUserCreate()
        {
            this.userlist = new List<string>();
            this.partylist = new List<Int64>();
        }

        /// <summary>
        /// 标签id 
        /// </summary>
        public string tagid { get; set; }

        /// <summary>
        /// 企业成员ID列表，注意：userlist、partylist不能同时为空 
        /// </summary>
        public List<string> userlist { get; set; }

        /// <summary>
        /// 企业部门ID列表，注意：userlist、partylist不能同时为空 
        /// </summary>
        public List<Int64> partylist { get; set; }
    }
}
