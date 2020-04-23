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

namespace LD.DAL
{
    /// <summary>
    /// 数据访问类:LotInfo
    /// </summary>
    public partial class BillingDao
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        public BillingDao()
        { }

        public DataSet GetBillingList(string name, string userid)
        {
            DataSet ds = new DataSet();

            StringBuilder sb = new StringBuilder();

            //sb.Append("SELECT a.[id],a.[name],a.[identifyNum],a.[address],a.[phone],a.[bankAccount],a.[accountNum],a.[orderNum],a.[sysDate],a.[isUse],  ");
            //sb.Append("c.name as orgName   ");
            //sb.Append("FROM [LDXX].[dbo].[BillingInfo] a   ");
            //sb.Append("left join [LDXX].[dbo].[BillingInfoOrg] b on a.id = b.infoid  ");
            //sb.Append("left join [LDXX].[dbo].[BillingOrg] c on b.orgid = c.id  ");

            sb.Append("SELECT a.[id],a.[name],a.[identifyNum],a.[address],a.[phone],a.[bankAccount],a.[accountNum],a.[orderNum],a.[sysDate],a.[isUse],a.[ebCode],a.[pname],a.[paddress], ");
            sb.Append("c.name as orgName,d.sysDate as dsysDate  ");
            sb.Append("FROM [LDXX].[dbo].[BillingInfo] a    ");
            sb.Append("left join [LDXX].[dbo].[BillingInfoOrg] b on a.id = b.infoid   ");
            sb.Append("left join [LDXX].[dbo].[BillingOrg] c on b.orgid = c.id  ");
            sb.Append("left join [LDXX].[dbo].[BillingInfoCollect] d on d.infoid = convert(varchar(36),a.id) and d.userid = '" + userid + "'  ");

            if (name != String.Empty && name != null)
            {
                sb.Append("where a.[name] like '%" + name + "%' or c.name like '%" + name + "%'");
            }

            //sb.Append("order by  d.sysDate desc ,a.sysDate asc ");
            sb.Append("order by a.sysDate asc ");

            ds = DbHelperSQL.Query(sb.ToString());
            return ds;

        }

        public DataSet GetBillingProjectList(string id)
        {
            DataSet ds = new DataSet();

            StringBuilder sb = new StringBuilder();

            sb.Append("select a.id,a.name,p.name as pname,p.address,p.sysDate  ");
            sb.Append("from [LDXX].[dbo].[BillingInfoPro] bip   ");
            sb.Append("left join [LDXX].[dbo].[BillingInfo] a on bip.infoid = a.id  ");
            sb.Append("left join [LDXX].[dbo].[BillingProject] p on bip.proid = p.id   ");
            sb.Append("where bip.infoid =  '" + id + "'  ");
            sb.Append("order by p.sysDate asc ");


            ds = DbHelperSQL.Query(sb.ToString());
            return ds;

        }

        public bool EditCollect(string infoid, string userid, ref int rCount, ref string errorInfo)
        {
            //string sql = String.Empty;
            StringBuilder sb = new StringBuilder();

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;

            try
            {
                DataSet ds = new DataSet();
                sb.Append("  SELECT [id],[infoid],[userid],[sysDate] FROM [LDXX].[dbo].[BillingInfoCollect] where [infoid] = '" + infoid + "' and [userid] = '" + userid + "'");
                ds = DbHelperSQL.Query(sb.ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    sb = new StringBuilder();
                    sb.Append("delete from BillingInfoCollect where [infoid] = '" + infoid + "' and [userid] = '" + userid + "'");
                }
                else
                {
                    sb = new StringBuilder();
                    sb.Append("insert into BillingInfoCollect(infoid,userid) values ('" + infoid + "','" + userid + "')");
                }


                comm.CommandText = sb.ToString();
                comm.ExecuteNonQuery();
                rCount += 1;


                tran.Commit();
                conn.Close();
                return true;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = sb.ToString();
                conn.Close();
                return false;
            }
            finally
            {
                //conn.Close();
            }

        }

        public bool EditInfo(string id, string ebCode, string name, string identifyNum, string phone, string bankAccount, string accountNum, string address, ref int rCount, ref string errorInfo)
        {
            //string sql = String.Empty;
            StringBuilder sb = new StringBuilder();

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction tran;
            tran = conn.BeginTransaction();
            SqlCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = tran;

            try
            {

                sb = new StringBuilder();
               

                sb.Append("update [LDXX].[dbo].[BillingInfo] set ");
                sb.Append("ebCode = '" + ebCode + "',name = '" + name + "',identifyNum = '" + identifyNum + "',phone = '" + phone + "',");
                sb.Append("bankAccount = '" + bankAccount + "',accountNum = '" + accountNum + "',address = '" + address + "' ");
                sb.Append("where id = '" + id + "'");

                comm.CommandText = sb.ToString();
                comm.ExecuteNonQuery();
                rCount += 1;


                tran.Commit();
                conn.Close();
                return true;

            }
            catch (Exception ex)
            {
                tran.Rollback();
                errorInfo = sb.ToString();
                conn.Close();
                return false;
            }
            finally
            {
                //conn.Close();
            }

        }




    }





}




