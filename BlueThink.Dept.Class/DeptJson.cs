//******************************************************************************
//文 件 名： DeptJson
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 部门的Json

//--------------------------------------------------------------------------------
//修改人：
//修改原因：
//修改日期:

//******************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueThink.Dept.Class
{
    public class DeptJson
    {
        /// <summary>
        /// 部门id 
        /// </summary>
        public Int64 id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 父亲部门id。根部门为1 
        /// </summary>
        public Int64 parentid { get; set; }

        /// <summary>
        /// 在父部门中的次序值。order值小的排序靠前。 
        /// </summary>
        public Int64 order { get; set; }
    }
}
