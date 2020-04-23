using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
//using System.Xml;
//using System.Xml.Serialization;
using System.Collections;
using System.Web.Script.Serialization;

namespace LD.DAL
{
    public partial class JSSerializer
    {
        //  public DataTable GetNodeList(string stype, string cityName, string aqi)
        //{
        //    DataTable dt = new DataTable();
        //    Air_nodeService nodeService = new Air_nodeService();
        //    dt = nodeService.GetNodeList(stype, cityName, aqi);//sname,Longitude,Latitude,did,isMonitor,stype,city_name,sid,Udate,AQILevel,AQI,aqiTime,sno
        //    return dt;
        //}
 
        //public List<Air_nodeInfo> GetAir_nodeListAll()
        //{
        //    List<Air_nodeInfo> nodeList = new List<Air_nodeInfo>();
        //    Air_nodeService nodeService = new Air_nodeService();
        //    nodeList = nodeService.GetAir_nodeListAll();
        //    return nodeList;
        //}
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        /// <summary>序列化方法
        /// 不需要分页
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="flag">false</param>
        /// <returns></returns>
        public string Serialize(DataTable dt, bool flag)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            return serializer.Serialize(list); ;
        }

        /// <summary>序列化方法
        /// 不需要分页
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="flag">false</param>
        /// <returns></returns>
        public string Serialize(DataSet ds, bool flag)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                ArrayList listss = new ArrayList();
                List<Dictionary<string, object>> list;
                DataTable[] dt = new DataTable[] { ds.Tables[0] };
                for (int i = 0; i < dt.Length; i++)
                {
                    list = new List<Dictionary<string, object>>();
                    foreach (DataRow dr in dt[i].Rows)
                    {
                        Dictionary<string, object> result = new Dictionary<string, object>();
                        foreach (DataColumn dc in dt[i].Columns)
                        {
                            result.Add(dc.ColumnName, dr[dc].ToString());
                        }
                        list.Add(result);
                    }
                    listss.Add(list);
                }
                return serializer.Serialize(listss);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}
