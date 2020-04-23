using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using HL.DAL;
using System.Configuration;

namespace HL.DAL
{
	/// <summary>
	/// 数据访问类:hl_cw_FundBalance
	/// </summary>
    public partial class HL_Employee
	{

        public HL_Employee()
		{}
		#region  BasicMethod



		#endregion  BasicMethod



		#region  ExtensionMethod

        public DataSet EmployeeList()
        {

            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();


            sb.Append("select t1.id as t1id,* from HrmResource t1    ");
            sb.Append("left join HrmSubCompany t2 on t1.subcompanyid1=t2.id    ");
            sb.Append("left join HrmDepartment t3 on t1.departmentid=t3.id    ");
            sb.Append("left join HrmJobTitles t4 on t1.jobtitle=t4.id    ");
            sb.Append("where t1.status in(0,1,2,3) and t1.workcode<>'' and t1.mobile<>'' and departmentname='信息管理部'  ");
            sb.Append("order by t1.departmentid	  ");


            ds = DbHelperSQL31.Query(sb.ToString());

            return ds;
        }


		#endregion  ExtensionMethod
	}


}

