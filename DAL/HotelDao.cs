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
    public partial class HotelDao
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
        public HotelDao()
        { }

        public DataSet GetHotelList(string city,string sort)
        {
            DataSet ds = new DataSet();

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT a.[id],a.[name],a.[starlevel],a.[city],a.[region],a.[address],a.[basicinfo],a.[aroundinfo],a.[summary],a.[sysDate],a.[isUse],   ");
            sb.Append("b.filePath,  ");
            sb.Append("min(c.currentPrice) as currentPrice    ");
            sb.Append("FROM [LDXX].[dbo].[HotelInfo] a    ");
            sb.Append("left join [LDXX].[dbo].[HotelImages] b on a.id = b.hotelid and b.isTop = 1  and b.type = 1   ");
            sb.Append("left join [LDXX].[dbo].[HotelPrice] c on a.id = c.hotelid   ");
            if (city != String.Empty && sort ==String.Empty)
            {
                sb.Append("where a.[city] = '" + city + "' ");
            }
            sb.Append("group by a.[id],a.[name],a.[starlevel],a.[city],a.[region],a.[address],a.[basicinfo],a.[aroundinfo],a.[summary],a.[sysDate],a.[isUse],b.filePath   ");

            if (city == String.Empty && sort == "均价从低到高")
            {
                sb.Append("order by min(c.currentPrice)");
            }
            else if (city == String.Empty && sort == "均价从高到低")
            {
                sb.Append("order by min(c.currentPrice) desc ");
            }
            else if (city == String.Empty && sort == "星级从低到高")
            {
                sb.Append("order by  a.[starlevel] ");
            }
            else if (city == String.Empty && sort == "星级从高到低")
            {
                sb.Append("order by  a.[starlevel] desc");
            }
            else
            {
                sb.Append("order by min(c.currentPrice) desc");
            }

            ds = DbHelperSQL.Query(sb.ToString());
            return ds;

        }

        public DataSet GetsingleHotel(string id)
        {
            DataSet ds = new DataSet();

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT a.[id],a.[name],a.[starlevel],a.[city],a.[region],a.[address],a.[basicinfo],a.[aroundinfo],a.[summary],a.[sysDate],a.[isUse],   ");
            sb.Append("b.filePath  ");
            sb.Append("FROM [LDXX].[dbo].[HotelInfo] a     ");
            sb.Append("left join [LDXX].[dbo].[HotelImages] b on a.id = b.hotelid and b.isTop = 1     ");
            sb.Append("where a.id = '" + id + "'  ");

            ds = DbHelperSQL.Query(sb.ToString());
            return ds;

        }

        public DataSet GetPriceList(string hotelid)
        {
            DataSet ds = new DataSet();

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT a.[id],a.[hotelid],a.[name],a.[retailSales],a.[currentPrice],a.[remarks],a.[sortid],a.[sysDate],a.[isUse],");
            sb.Append("b.filePath  ");
            sb.Append("FROM [LDXX].[dbo].[HotelPrice] a  ");
            sb.Append("left join [LDXX].[dbo].[HotelImages] b on a.id = b.PriceId ");
            sb.Append("where a.hotelid = '" + hotelid + "'  ");

            ds = DbHelperSQL.Query(sb.ToString());
            return ds;

        }

        public DataSet GetContact(string hotelid)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT [id],[hotelid],[telephone],[mobile],[fax],[contacts],[sign],[sortid],[sysDate],[isUse]   ");
            sb.Append("FROM [LDXX].[dbo].[HotelContact]   ");
            sb.Append("where [hotelid] = '" + hotelid + "'  ");

            ds = DbHelperSQL.Query(sb.ToString());
            return ds;
        }

        public DataSet GetDistance(string hotelid)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT [id],[hotelid],[destination],[distance],[unit],[sortid],[sysDate],[isUse] ");
            sb.Append("FROM [LDXX].[dbo].[HotelDistance]  ");
            sb.Append("where [hotelid] = '" + hotelid + "'  ");

            ds = DbHelperSQL.Query(sb.ToString());
            return ds;
        }



    }





}




