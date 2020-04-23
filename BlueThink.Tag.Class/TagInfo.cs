//******************************************************************************
//文 件 名： TagInfo
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 标签的操作

//--------------------------------------------------------------------------------
//修改人：
//修改原因：
//修改日期:

//******************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BlueThink.Comm;

namespace BlueThink.Tag.Class
{
    public class TagInfo:ITagInfo
    {
        /// <summary>
        /// 创建标签
        /// </summary>
        public TagCreateResult CreateTag(string accessToken,  TagCreateJson tagCreateJson)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/create?access_token={0}";

            var url = string.Format(urlFormat, accessToken);
            //序列化
            string postData = JsonConvert.SerializeObject(tagCreateJson);
            //post
            string resultMsg = HttpHelper.HttpPost(url, postData);
            //反序列化
            TagCreateResult result = JsonConvert.DeserializeObject<TagCreateResult>(resultMsg);

            return result;
        }

        /// <summary>
        /// 更新标签
        /// </summary>
        public TagCommResult UpdateTag(string accessToken, TagUpdateJson tagUpdateJson)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/update?access_token={0}";

            var url = string.Format(urlFormat, accessToken);
            //序列化
            string postData = JsonConvert.SerializeObject(tagUpdateJson);
            //post
            string resultMsg = HttpHelper.HttpPost(url, postData);
            //反序列化
            TagCommResult result = JsonConvert.DeserializeObject<TagCommResult>(resultMsg);

            return result;
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        public TagCommResult DeleteTag(string accessToken, long tagid)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/delete?access_token={0}&tagid={1}";

            var url = string.Format(urlFormat, accessToken, tagid);
            //get
            string resultMsg = HttpHelper.HttpGet(url, "");
            //反序列化
            TagCommResult result = JsonConvert.DeserializeObject<TagCommResult>(resultMsg);

            return result;
        }

        /// <summary>
        /// 标签列表
        /// </summary>
        public TagListResult ListTag(string accessToken)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/list?access_token={0}";

            var url = string.Format(urlFormat, accessToken);
            //get
            string resultMsg = HttpHelper.HttpGet(url, "");
            //反序列化
            TagListResult result = JsonConvert.DeserializeObject<TagListResult>(resultMsg);

            return result;
        }

        /// <summary>
        /// 取标签下成员或部门
        /// </summary>
        public TagUserList ReadListUser(string accessToken, long tagid)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/get?access_token={0}&tagid={1}";

            var url = string.Format(urlFormat, accessToken, tagid);
            //get
            string resultMsg = HttpHelper.HttpGet(url, "");
            //反序列化
            TagUserList result = JsonConvert.DeserializeObject<TagUserList>(resultMsg);

            return result;
        }

        /// <summary>
        /// 增加标签下成员
        /// </summary>
        public TagUserCResult AddTagUser(string accessToken, TagUserCreate tagUserCreate)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/addtagusers?access_token={0}";

            var url = string.Format(urlFormat, accessToken);
            //序列化
            string postData = JsonConvert.SerializeObject(tagUserCreate);
            //post
            string resultMsg = HttpHelper.HttpPost(url, postData);
            //反序列化
            TagUserCResult result = JsonConvert.DeserializeObject<TagUserCResult>(resultMsg);

            return result;
        }

        /// <summary>
        /// 删除标签下成员
        /// </summary>
        public TagUserCResult DelTagUser(string accessToken, TagUserCreate tagUserDel)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/tag/deltagusers?access_token={0}";

            var url = string.Format(urlFormat, accessToken);
            //序列化
            string postData = JsonConvert.SerializeObject(tagUserDel);
            //post
            string resultMsg = HttpHelper.HttpPost(url, postData);
            //反序列化
            TagUserCResult result = JsonConvert.DeserializeObject<TagUserCResult>(resultMsg);

            return result;
        }

    }
}
