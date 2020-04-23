using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using HL.DAL;

namespace HLKQDao
{
    public partial class Class1
    {
        public DataSet IsRoleLogin(int resourceid)
        {
            bool row = false;


            try
            {
                DataSet ds = new DataSet();
                //ds = DbHelperSQL.Query("select * from HrmRoleMembers where roleid =2614 and resourceid = " + resourceid + "");
                //ds = DbHelperSQL.Query("select * from HrmRoleMembers where resourceid = " + resourceid + "");
                //select * from HrmRoles where rolesmark like '财务日报-%';
                //select * from HrmRoleMembers where roleid in (select id from HrmRoles where rolesmark like '财务日报-%');

                ds = DbHelperSQL.Query("select * from HrmRoleMembers where roleid in(3063,3064,3065,3066,3067) and resourceid = " + resourceid + " order by roleid ");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = true;
                }

                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //conn.Close();
            }

        }

        
        public DataSet UserInfo_ERP(string workcode)
        {
            bool row = false;


            try
            {
                DataSet ds = new DataSet();

                ds = DbHelperSQL207.Query("select SSGS as '公司名称',csguid,myu.JobNumber as '工号',myu.UserName as '用户名称',myu.UserCode as '用户' from myuser myu where myu.JobNumber = '" + workcode + "'");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    row = true;
                }

                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //conn.Close();
            }

        }

    }
}
