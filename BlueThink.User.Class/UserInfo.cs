
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueThink.Comm;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlueThink.User.Class
{
    public class UserInfo:IUserInfo
    {
        /// <summary>
        /// 创建成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserCommResult CreateUser(string accessToken, UserJsonAll user)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}";
            var data = new
            {
                userid = user.userid,
                name = user.name,
                department = user.department,
                position = user.position,
                mobile = user.mobile,
                gender = user.gender,
                email = user.email,
                weixinid = user.weixinid
            };
            var url = string.Format(urlFormat, accessToken);
            string postData = JsonConvert.SerializeObject(data);            
            //post
            string resultMsg = HttpHelper.HttpPost(url, postData);
            //反序列化
            UserCommResult result = JsonConvert.DeserializeObject<UserCommResult>(resultMsg);

            return result;
        }

        /// <summary>
        /// 更新成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserCommResult UpdateUser(string accessToken, UserJsonAll user)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token={0}";
            var data = new
            {
                userid = user.userid,
                name = user.name,
                department = user.department,
                position = user.position,
                mobile = user.mobile,
                gender = user.gender,
                email = user.email,
                weixinid = user.weixinid
            };
            var url = string.Format(urlFormat, accessToken);
            string postData = JsonConvert.SerializeObject(data);
            //post
            string resultMsg = HttpHelper.HttpPost(url, postData);
            //反序列化
            UserCommResult result = JsonConvert.DeserializeObject<UserCommResult>(resultMsg);

            return result;
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserCommResult DeleteUser(string accessToken, string userid)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid={1}";
            var url = string.Format(urlFormat, accessToken, userid);
            string resultMsg = HttpHelper.HttpGet(url, "");
            //反序列化
            UserCommResult result = JsonConvert.DeserializeObject<UserCommResult>(resultMsg);

            return result;
        }

        /// <summary>
        /// 批量删除成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserCommResult BatchDeleteUser(string accessToken, Useridlist usridlst)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/batchdelete?access_token={0}";
            var url = string.Format(urlFormat, accessToken);

            string postData = JsonConvert.SerializeObject(usridlst);
            //post
            string resultMsg = HttpHelper.HttpPost(url, postData);

            //反序列化
            UserCommResult result = JsonConvert.DeserializeObject<UserCommResult>(resultMsg);

            return result;
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserGetJson GetUser(string accessToken, string userid)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}";
            var url = string.Format(urlFormat, accessToken, userid);
            string resultMsg = HttpHelper.HttpGet(url, "");
            //反序列化
            UserGetJson result = JsonConvert.DeserializeObject<UserGetJson>(resultMsg);

            return result;
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="department_id"></param>
        /// <param name="fetch_child"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public UserListJson GetDeptUser(string accessToken, Int64 department_id, int fetch_child = 0, int status = 0)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}&status={3} ";

            var url = string.Format(urlFormat, accessToken, department_id, fetch_child, status);
            //post
            string resultMsg = HttpHelper.HttpGet(url, "");
            //反序列化
            //DeptListJson result = (DeptListJson)JsonConvert.DeserializeObject(resultMsg, typeof(DeptListJson));
            UserListJson result = JsonConvert.DeserializeObject<UserListJson>(resultMsg);

            return result;
        }

        /// <summary>
        /// 获取部门成员（详细）
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="department_id"></param>
        /// <param name="fetch_child"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public UserListJsonAll GetDeptUserAll(string accessToken, Int64 department_id, int fetch_child = 0, int status = 0)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child={2}&status={3} ";
            var url = string.Format(urlFormat, accessToken, department_id, fetch_child, status);
            //get
            string resultMsg = HttpHelper.HttpGet(url, "");
          
            //反序列化
            UserListJsonAll result = JsonConvert.DeserializeObject<UserListJsonAll>(resultMsg);

            return result;
        }

        /// <summary>
        /// 邀请成员关注
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="UsrIJson"></param>
        /// <returns></returns>
        public UserInviteResult InviteUserAttention(string accessToken, UserInviteJson UsrIJson)
        {
            string urlFormat = "https://qyapi.weixin.qq.com/cgi-bin/invite/send?access_token={0}";
            var url = string.Format(urlFormat, accessToken);

            string postData = JsonConvert.SerializeObject(UsrIJson);
            //post
            string resultMsg = HttpHelper.HttpPost(url, postData);

            //反序列化
            UserInviteResult result = JsonConvert.DeserializeObject<UserInviteResult>(resultMsg);

            return result;
        }

    }
}
