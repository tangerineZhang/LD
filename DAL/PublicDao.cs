using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using HL.DAL;
using System.Configuration;

using BlueThink.Comm;
using System.Configuration;
using DotNet.Utilities;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;

namespace LD.DAL
{
    /// <summary>
    /// 数据访问类:LotInfo
    /// </summary>
    public partial class PublicDao
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        public PublicDao()
        { }
        #region  BasicMethod


        #endregion  BasicMethod
        #region  ExtensionMethod

        //数据库连接字符串(web.config来配置)，多数据库可使用DbHelperSQLP来实现.
        /// <summary>
        /// 确定
        /// </summary>    

        public string AccessToken()
        {
            string AccessToken = string.Empty;
            string CorpID = ConfigurationManager.AppSettings["appid"];
            string Secret = ConfigurationManager.AppSettings["appsecret"];

            AccessToken = GetAccessToken(CorpID, Secret);

            return AccessToken;
        }

        public string AccessToken(string CorpID, string Secret)
        {
            string AccessToken = string.Empty;

            AccessToken = GetAccessToken(CorpID, Secret);

            return AccessToken;
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>
        /// <returns></returns>
        private string GetAccessToken(string corpid, string corpsecret)
        {
            string Gurl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", corpid, corpsecret);
            string html = HttpHelper.HttpGet(Gurl, "");
            string regex = "\"access_token\":\"(?<token>.*?)\"";

            string token = CRegex.GetText(html, regex, "token");
            return token;
        }

        public string GetCacheAccessToken()
        {
            string token = String.Empty;
            if (CacheHelper.GetCache("wxAccessToken") == String.Empty || CacheHelper.GetCache("wxAccessToken") == null)
            {
                token = AccessToken();
                TimeSpan ts = new TimeSpan(1, 30, 0);
                CacheHelper.SetCache("wxAccessToken", token, ts);
            }
            else
            {
                token = CacheHelper.GetCache("wxAccessToken").ToString();
            }

            return token;
        }

        public string GetCacheAccessTokenYQ()
        {
            string token = String.Empty;
            if (CacheHelper.GetCache("wxAccessTokenYQ") == String.Empty || CacheHelper.GetCache("wxAccessTokenYQ") == null)
            {
                token = AccessTokenYQ();
                TimeSpan ts = new TimeSpan(1, 30, 0);
                CacheHelper.SetCache("wxAccessTokenYQ", token, ts);
            }
            else
            {
                token = CacheHelper.GetCache("wxAccessTokenYQ").ToString();
            }

            return token;
        }

        public string AccessTokenYQ()
        {
            string AccessToken = string.Empty;
            string CorpID = ConfigurationManager.AppSettings["appid"];
            string Secret = ConfigurationManager.AppSettings["appsecretYQ"];

            AccessToken = GetAccessToken(CorpID, Secret);

            return AccessToken;
        }

        /// <summary>
        /// 获取jsapi_ticket
        /// </summary>
        /// <param name="corpid"></param>
        /// <param name="corpsecret"></param>
        /// <returns></returns>
        public string GetJsapi_ticket(string accesstoken)
        {
            string Gurl = string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}", accesstoken);
            string html = HttpHelper.HttpGet(Gurl, "");
            string regex = "\"ticket\":\"(?<token>.*?)\"";

            string token = CRegex.GetText(html, regex, "token");
            return token;
        }

        public string GetCachewxTicket(string appId)
        {
            string token = String.Empty;
            if (CacheHelper.GetCache("wxTicket") == String.Empty || CacheHelper.GetCache("wxTicket") == null)
            {
                token = GetJsapi_ticket(appId);
                //TimeSpan ts = new TimeSpan(0, 1, 30);
                TimeSpan ts = new TimeSpan(1, 30, 0);
                CacheHelper.SetCache("wxTicket", token, ts);
            }
            else
            {
                token = CacheHelper.GetCache("wxTicket").ToString();
            }


            return token;
        }


        //获取jssdk所需签名
        public string Signature(string ticket, string noncestr, string timestamp, string url, ref string jsapi_ticket)
        {
            //string noncestr = "Wm3WZYTPz0wzccnW";
            //int timestamp = 1414587457;
            //string ticket = GetTicket();

            string string1 = "jsapi_ticket=" + ticket + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + url;
            jsapi_ticket = string1;
            string signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(string1, "SHA1");
            return signature.ToLower();
        }


        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns>返回毫秒数</returns>
        public long GetTime()
        {
            return (DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000;
        }

        //根据openid，access token获得用户信息
        public WXUser Get_UserInfo(string REFRESH_TOKEN, string OPENID, ref string getjson)
        {
            //string Str = GetJson("https://api.weixin.qq.com/sns/userinfo?access_token=" + REFRESH_TOKEN + "&openid=" + OPENID);公众号接口
            string Str = GetJson("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=" + REFRESH_TOKEN + "&code=" + OPENID);
            getjson = Str;
            //div1.InnerText += " <Get_UserInfo> " + Str + " <> ";

            WXUser OAuthUser_Model = JsonHelper.ParseFromJson<WXUser>(Str);
            return OAuthUser_Model;
        }

        //访问微信url并返回微信信息
        protected string GetJson(string url)
        {
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;
            wc.Encoding = System.Text.Encoding.UTF8;
            string returnText = wc.DownloadString(url);

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
                return returnText;
            }
            return returnText;
        }

        public class WXUser
        {
            public WXUser()
            { }
            #region 数据库字段
            private string _UserId;
            private string _DeviceId;

            #endregion

            #region 字段属性
            /// <summary>
            /// 用户的唯一标识
            /// </summary>
            public string UserId
            {
                set { _UserId = value; }
                get { return _UserId; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string DeviceId
            {
                set { _DeviceId = value; }
                get { return _DeviceId; }
            }

            #endregion
        }

        #endregion  ExtensionMethod




        /// <summary>
        /// 添加疫情
        /// </summary>
        /// <param name="company"></param>
        /// <param name="cCity"></param>
        /// <param name="fCity"></param>
        /// <param name="returnDate"></param>
        /// <param name="influence"></param>
        /// <param name="sProperties"></param>
        /// <param name="sScope"></param>
        /// <param name="isaffect"></param>
        /// <param name="projectInfo"></param>
        /// <param name="projectNode"></param>
        /// <param name="iDays"></param>
        /// <param name="describe"></param>
        /// <param name="applicant"></param>
        /// <param name="applicantTel"></param>
        /// <param name="rCount"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public bool TransactionAddYQ(string company, string cCity, string fCity, string returnDate, string influence, string sProperties, string sScope,
         string isaffect, string projectInfo, string projectNode, string iDays, string describe, string applicant, string applicantTel, ref int rCount, ref string errorInfo)
        {
            StringBuilder strSql = new StringBuilder();

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;

            try
            {

                strSql = new StringBuilder();

                strSql.Append("insert into [LDXX].[dbo].[YQInfo] (company,cCity,fCity,returnDate,influence,sProperties,");
                strSql.Append("sScope,isaffect,projectInfo,projectNode,iDays,describe,applicant,applicantTel) ");
                strSql.Append("Values('" + company + "','" + cCity + "','" + fCity + "','" + returnDate + "','" + influence + "','" + sProperties + "',");
                strSql.Append("'" + sScope + "','" + isaffect + "','" + projectInfo + "','" + projectNode + "'," + iDays + ",'" + describe + "','" + applicant + "','" + applicantTel + "')");

                comm.CommandText = strSql.ToString();
                //comm.Parameters.Clear();
                //comm.Parameters.AddRange(parametersDM);
                rCount += comm.ExecuteNonQuery();

                tran.Commit();
                conn.Close();
                return true;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = strSql.ToString();
                conn.Close();
                return false;
            }
            finally
            {
                //conn.Close();
            }
        }


        /// <summary>
        /// 验证是否已经填报
        /// </summary>
        /// <param name="rGuid"></param>
        /// <param name="begDate"></param>
        /// <param name="endDate"></param>
        /// <param name="vtid"></param>
        /// <returns></returns>
        public DataSet GetHYQDMeetingInfo(string whereinfo, string orderinfo)
        {//获取该餐厅配餐结束的最大日期
            StringBuilder strSql = new StringBuilder();

            strSql.Append("SELECT m.ID,m.Name,m.Company,m.Department,m.UserName,m.UserID,m.BegTime,m.EndTime,m.Duration,m.SortID,m.Status,m.CreatorID,m.Creator,m.CreateDate ");
            strSql.Append("FROM LDHY.dbo.HYQD_MeetingInfo m ");

            if (whereinfo != String.Empty)
            {
                strSql.Append(" where " + whereinfo + " ");
            }
            if (orderinfo != String.Empty)
            {
                strSql.Append(" order by " + orderinfo);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public bool TransactionAddMeeting(string Name, string BegTime, float Duration, string UserName, string UserID, ref string guid, ref int rCount, ref string errorInfo)
        {
            StringBuilder strSql = new StringBuilder();

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;

            try
            {
                //查询出Guid
                strSql = new StringBuilder();
                strSql.Append("select newid()");
                guid = DbHelperSQL.GetSingle(strSql.ToString()).ToString();
                //Guid guid = new Guid();
                //guid = new Guid(oguid.ToString());

                strSql = new StringBuilder();
                strSql.Append("insert into LDHY.dbo.HYQD_MeetingInfo (ID,Name,Company,Department,UserName,UserID,BegTime,EndTime,Duration,SortID,Status,CreatorID,Creator,CreateDate) ");
                strSql.Append("values('" + guid.ToString() + "','" + Name + "','','','" + UserName + "','"+UserID+"','" + BegTime + "',''," + Duration + ",null,1,'','','" + DateTime.Now.ToString() + "') ");

                comm.CommandText = strSql.ToString();
                //comm.Parameters.Clear();
                //comm.Parameters.AddRange(parametersDM);
                rCount += comm.ExecuteNonQuery();

                tran.Commit();
                conn.Close();
                return true;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = strSql.ToString();
                conn.Close();
                return false;
            }
            finally
            {
                //conn.Close();
            }
        }

        public bool TransactionAddEndSignIn(string mID, string UserName, string Q1, string Q2, string Q3, string Q4, string Q5, ref int rCount, ref string errorInfo)
        {
            StringBuilder strSql = new StringBuilder();

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;

            try
            {
                //查询出Guid
                //strSql = new StringBuilder();
                //strSql.Append("select newid()");
                //guid = DbHelperSQL.GetSingle(strSql.ToString()).ToString();

                //插入签到表
                strSql = new StringBuilder();
                strSql.Append("insert into LDHY.dbo.HYQD_MeetingEndSignIn (MID,UserName,UserID,SignInTime,SortID,Status,CreatorID,Creator)  ");
                strSql.Append("values('" + mID + "','" + UserName + "','','" + DateTime.Now.ToString() + "',null,1,'','') ");
                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();

                //插入答题表
                string[] strArrQ1 = Q1.Split(';');//根据逗号分隔字符串str
                for (int i = 0; i < strArrQ1.Length; i++)
                {
                    strSql = new StringBuilder();
                    strSql.Append("insert into LDHY.dbo.HYQD_MeetingEndAnswer (MID,UserName,UserID,AnswerTime,TID,Answer,IsCorrect,SortID,Status,CreatorID,Creator)  ");
                    strSql.Append("values('" + mID + "','" + UserName + "','','" + DateTime.Now.ToString() + "','E0CD01F3-E65D-4DE5-B777-0F66C9B95BFB','" + strArrQ1[i] + "',1,null,1,'','') ");
                    comm.CommandText = strSql.ToString();
                    rCount += comm.ExecuteNonQuery();
                }

                string[] strArrQ2 = Q2.Split(';');//根据逗号分隔字符串str
                for (int i = 0; i < strArrQ2.Length; i++)
                {
                    strSql = new StringBuilder();
                    strSql.Append("insert into LDHY.dbo.HYQD_MeetingEndAnswer (MID,UserName,UserID,AnswerTime,TID,Answer,IsCorrect,SortID,Status,CreatorID,Creator)  ");
                    strSql.Append("values('" + mID + "','" + UserName + "','','" + DateTime.Now.ToString() + "','66B6A125-53A3-438E-B95A-C88730C300F9','" + strArrQ2[i] + "',1,null,1,'','') ");
                    comm.CommandText = strSql.ToString();
                    rCount += comm.ExecuteNonQuery();
                }

                string[] strArrQ3 = Q3.Split(';');//根据逗号分隔字符串str
                for (int i = 0; i < strArrQ3.Length; i++)
                {
                    strSql = new StringBuilder();
                    strSql.Append("insert into LDHY.dbo.HYQD_MeetingEndAnswer (MID,UserName,UserID,AnswerTime,TID,Answer,IsCorrect,SortID,Status,CreatorID,Creator)  ");
                    strSql.Append("values('" + mID + "','" + UserName + "','','" + DateTime.Now.ToString() + "','ED1D6BB4-CEF6-43B0-AFD1-3DED3B436525','" + strArrQ3[i] + "',1,null,1,'','') ");
                    comm.CommandText = strSql.ToString();
                    rCount += comm.ExecuteNonQuery();
                }

                string[] strArrQ4 = Q4.Split(';');//根据逗号分隔字符串str
                for (int i = 0; i < strArrQ4.Length; i++)
                {
                    strSql = new StringBuilder();
                    strSql.Append("insert into LDHY.dbo.HYQD_MeetingEndAnswer (MID,UserName,UserID,AnswerTime,TID,Answer,IsCorrect,SortID,Status,CreatorID,Creator)  ");
                    strSql.Append("values('" + mID + "','" + UserName + "','','" + DateTime.Now.ToString() + "','75B50C0F-FBD9-4CDC-8512-F05AEC3B5A16','" + strArrQ4[i] + "',1,null,1,'','') ");
                    comm.CommandText = strSql.ToString();
                    rCount += comm.ExecuteNonQuery();
                }

                strSql = new StringBuilder();
                strSql.Append("insert into LDHY.dbo.HYQD_MeetingEndAnswer (MID,UserName,UserID,AnswerTime,TID,Answer,IsCorrect,SortID,Status,CreatorID,Creator)  ");
                strSql.Append("values('" + mID + "','" + UserName + "','','" + DateTime.Now.ToString() + "','6349E2CA-75C5-42D8-95F9-B7496C4F601A','" + Q5 + "',1,null,1,'','') ");
                comm.CommandText = strSql.ToString();
                rCount += comm.ExecuteNonQuery();



                tran.Commit();
                conn.Close();
                return true;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = strSql.ToString();
                conn.Close();
                return false;
            }
            finally
            {
                //conn.Close();
            }
        }



        /// <summary>
        /// 执行sql返回DataTable
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DataTable ExecSqlDateTable(string str)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter(str, conn);
                DataTable dt = new DataTable();
                sqlData.Fill(dt);
                sqlData.Dispose();
                conn.Close();
                return dt;
            }

        }

       //获取会议信息及问卷数量
        public DataTable LdhySelectCount(string name)
        {
            string str = string.Format("select  m.ID,m.Name,m.UserName,m.BegTime,count(s.ID) as Num " +
                " from LDHY.dbo.HYQD_MeetingInfo m " +
                " left join [LDHY].[dbo].[HYQD_MeetingEndSignIn] s on cast(m.ID as varchar(36)) = s.MID" +
                " group by  m.ID,m.Name,m.UserName,m.BegTime" +
                " having m.ID = '{0}'",name);
            return ExecSqlDateTable(str);
        }


        //查询会议问卷题目
        public DataTable LdhySelectData(string Names)
        {
            string str = string.Format("SELECT m.ID,m.Name,m.UserName,m.BegTime," +
                " q.ID as QuestionID,q.Question,a.Answer,count(a.ID) as Countsum " +
                " FROM LDHY.dbo.HYQD_MeetingInfo m " +
                "  left join LDHY.dbo.HYQD_MeetingEndAnswer a on cast(m.ID as varchar(36)) = a.MID " +
                "  left join LDHY.dbo.HYQD_MeetingEndQuestion q on a.TID = cast(q.ID as varchar(36)) " +
                "  group by m.ID,m.Name,m.UserName,m.BegTime," +
                "  q.ID,q.Question,a.Answer" +
                " having m.ID = '{0}'", Names);
            return ExecSqlDateTable(str);
        }

        //查询会议问答题
        public DataTable LdhySelectWD(string Names)
        {
            string str = string.Format(" SELECT m.ID,m.Name,m.UserName,m.BegTime, " +
                " q.ID as QuestionID,q.Question,a.Answer" +
                " FROM LDHY.dbo.HYQD_MeetingInfo m" +
                " left join LDHY.dbo.HYQD_MeetingEndAnswer a on cast(m.ID as varchar(36)) = a.MID " +
                " left join LDHY.dbo.HYQD_MeetingEndQuestion q on a.TID = cast(q.ID as varchar(36))" +
                " where m.ID = '{0}' and q.ID = '6349E2CA-75C5-42D8-95F9-B7496C4F601A'", Names);
            return ExecSqlDateTable(str);
        }


    }


    /// <summary>
    /// 将Json格式数据转化成对象
    /// </summary>
    public class JsonHelper
    {
        /// <summary>  
        /// 生成Json格式  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
        public static string GetJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray()); return szJson;
            }
        }
        /// <summary>  
        /// 获取Json的Model  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="szJson"></param>  
        /// <returns></returns>  
        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }
    }






}

public class T//:IEquatable<Person>
{
    public string _sql = null;
    public string _sql2 = null;
    public string _inputdate = null;
    public string _bankaccountno = null;
    public string _cName = null;
    public string _cAreaname = null;

    public T() { }

    public T(string sql, string inputdate, string bankaccountno, string cName, string cAreaname, string sql2)
    {
        this._sql = sql;
        this._sql2 = sql2;
        this._inputdate = inputdate;
        this._bankaccountno = bankaccountno;
        this._cName = cName;
        this._cAreaname = cAreaname;
    }
}


