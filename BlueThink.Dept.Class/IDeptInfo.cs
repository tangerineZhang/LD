//******************************************************************************
//文 件 名： IDeptInfo
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 部门接口

//--------------------------------------------------------------------------------
//修改人：
//修改原因：
//修改日期:

//******************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueThink.Dept.Class;

namespace BlueThink.Dept.Class
{
    public interface IDeptInfo
    {
        #region 部门管理
        /// <summary>
        /// 创建部门。
        /// 管理员须拥有“操作通讯录”的接口权限，以及父部门的管理权限。
        /// </summary>
        DeptCreateJson CreateDept(string accessToken, string name, string parentId);

        /// <summary>
        /// 更新部门。
        /// 管理员须拥有“操作通讯录”的接口权限，以及该部门的管理权限。
        /// </summary>
        DeptCommResult UpdateDept(string accessToken, Int64 id, string name);

        /// <summary>
        /// 删除部门.
        /// 管理员须拥有“操作通讯录”的接口权限，以及该部门的管理权限。
        /// </summary>
        DeptCommResult DeleteDept(string accessToken, Int64 id);
        
        /// <summary>
        /// 获取部门列表.
        /// 管理员须拥有’获取部门列表’的接口权限，以及对部门的查看权限。
        /// </summary>
        DeptListJson ListDept(string accessToken, Int64 id);

        #endregion

    }
}
