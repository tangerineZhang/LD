//******************************************************************************
//文 件 名： DeptInfo
//版权所有： 蓝思创工作室
//创 建 人： 蓝思创
//创建日期： 2016-05-08
//网    址：https://shop112893715.taobao.com/
//功能描述： 对通讯录中部门操作

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

namespace BlueThink.Dept.Class
{
    public class DeptInfo : IDeptInfo
    {
        /// <summary>
        /// 创建部门。
        /// 管理员须拥有“操作通讯录”的接口权限，以及父部门的管理权限。
        /// </summary>
        public DeptCreateJson CreateDept(string accessToken, string name, string parentId)
        { 
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}";
            var DeptInfo = new
            {
                name = name,
                parentid = parentId
            };
            var url = string.Format(urlFormat, accessToken);
            //序列化
            string postData = JsonConvert.SerializeObject(DeptInfo);
            //post
            string resultMsg = HttpHelper.HttpPost(url, postData);
            //反序列化
            DeptCreateJson result = JsonConvert.DeserializeObject<DeptCreateJson>(resultMsg);

            return result;
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        public DeptCommResult UpdateDept(string accessToken, Int64 id, string name)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0}";
            var DeptInfo = new
            {
                id=id,
                name = name
            };
            var url = string.Format(urlFormat, accessToken);
            //序列化
            string postData = JsonConvert.SerializeObject(DeptInfo);
            //post
            string resultMsg = HttpHelper.HttpPost(url, postData);
            //反序列化
            DeptCommResult result = JsonConvert.DeserializeObject<DeptCommResult>(resultMsg);

            return result;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        public DeptCommResult DeleteDept(string accessToken, Int64 id)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token={0}&id={1}";

            var url = string.Format(urlFormat, accessToken,id);

            //post
            string resultMsg = HttpHelper.HttpGet(url,"");
            //反序列化
            DeptCommResult result = JsonConvert.DeserializeObject<DeptCommResult>(resultMsg);

            return result;
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        public DeptListJson ListDept(string accessToken, Int64 id)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}&id={1}";

            var url = string.Format(urlFormat, accessToken,id);
            //post
            string resultMsg = HttpHelper.HttpGet(url, "");
            //反序列化
            //DeptListJson result = (DeptListJson)JsonConvert.DeserializeObject(resultMsg, typeof(DeptListJson));
            DeptListJson result = JsonConvert.DeserializeObject<DeptListJson>(resultMsg);

            return result;
        }


    }
}
