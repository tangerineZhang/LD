//******************************************************************************
//文 件 名： ITagInfo
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 标签接口

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
    public interface ITagInfo
    {
        #region 标签管理
        /// <summary>
        /// 创建标签。
        /// </summary>
        TagCreateResult CreateTag(string accessToken,  TagCreateJson tagCreateJson);

        /// <summary>
        /// 更新标签名字。
        /// </summary>
        TagCommResult UpdateTag(string accessToken,  TagUpdateJson tagUpdateJson);

        /// <summary>
        /// 删除标签
        /// </summary>
        TagCommResult DeleteTag(string accessToken, Int64 tagid);

        /// <summary>
        /// 获取标签列表
        /// </summary>
        TagListResult ListTag(string accessToken);

        /// <summary>
        /// 获取标签成员
        /// </summary>
        TagUserList ReadListUser(string accessToken, Int64 tagid);

        /// <summary>
        /// 增加标签成员
        /// </summary>
        TagUserCResult AddTagUser(string accessToken, TagUserCreate tagUserCreate);

        /// <summary>
        /// 删除标签成员
        /// </summary>
        TagUserCResult DelTagUser(string accessToken, TagUserCreate tagUserDel);

        #endregion

    }
}
