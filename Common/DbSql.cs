using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace HL.DAL
{
    public class DbSql
    {
        /// <summary>
        /// 返回Bool类型
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public bool Exists(string Sql)
        {
            //连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            SqlConnection myConn = null;
            SqlDataAdapter sda = null;
            SqlCommand cmd = null;
            sda = new SqlDataAdapter();
            try
            {
                string strSql = Sql;
                myConn = new SqlConnection(connectionString);
                sda = new SqlDataAdapter(strSql, myConn);
                DataSet ds = new DataSet();
                sda.Fill(ds, "isPlanName");
                bool isTorF = false;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    isTorF = true;
                }
                else
                {
                    isTorF = false;
                }
                return isTorF;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sda.Dispose();
                myConn.Dispose();
                myConn.Close();
            }
        }

        public bool ExecuteSql(string Sql)
        {
            //连接字符串
            string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            SqlConnection myConn = null;
            SqlDataAdapter sda = null;
            SqlCommand cmd = null;
            try
            {
                using (myConn = new SqlConnection(connectionString))
                {
                    myConn.Open();
                    cmd = myConn.CreateCommand();
                    SqlTransaction transaction = myConn.BeginTransaction(IsolationLevel.ReadCommitted);
                    cmd.Transaction = transaction;
                    try
                    {
                        //cmd.CommandText = "insert into T_Investment(PlanId,TotalInvestment,PowerlInvestment,OrganizationInfo,DistrictFinancial,UrbanFinancial)  ";
                        //cmd.CommandText += "Values ('" + PlanID + "','" + totalInvestment + "','" + PowerlInvestment + "',";
                        //cmd.CommandText += "'" + OrganizationInfo + "','" + DistrictFinancial + "','" + urbanFinancial + "')";
                        cmd.CommandText = Sql;
                        cmd.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                myConn.Close();
            }
        }

    }
}
