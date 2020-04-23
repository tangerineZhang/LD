
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueThink.User.Class
{
    public interface IUserInfo
    {
        #region 部门成员管理
        /// <summary>
        /// 创建成员
        /// </summary>
        UserCommResult CreateUser(string accessToken, UserJsonAll user);

        /// <summary>
        /// 更新成员
        /// </summary>
        UserCommResult UpdateUser(string accessToken, UserJsonAll user);

        /// <summary>
        /// 删除成员
        /// </summary>
        UserCommResult DeleteUser(string accessToken, string userid);

        /// <summary>
        /// 批量删除成员
        /// </summary>
        UserCommResult BatchDeleteUser(string accessToken, Useridlist usridlst);

        /// <summary>
        /// 根据成员id获取成员信息
        /// </summary>
        UserGetJson GetUser(string accessToken, string userid);

        /// <summary>
        /// 获取部门成员
        /// </summary>
        UserListJson GetDeptUser(string accessToken, Int64 department_id, int fetch_child = 0, int status = 0);

        /// <summary>
        /// 获取部门成员(详细）
        /// </summary>
        UserListJsonAll GetDeptUserAll(string accessToken, Int64 department_id, int fetch_child = 0, int status = 0);

        /// <summary>
        /// 邀请成员关注
        /// </summary>
        UserInviteResult InviteUserAttention(string accessToken, UserInviteJson UsrIJson);

        #endregion
    }
}
