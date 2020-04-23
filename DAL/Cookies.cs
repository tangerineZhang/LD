using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
///Cookies 的摘要说明
/// </summary>
public class Cookies
{
    string _username = null;
    string _password = null;
    int _year;
    string _projectID = null;
    string _guid = null;
    public Cookies(string username, string password, string projectID)
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
        _username = username;
        _password = password;
        _projectID = projectID;
        _year = DateTime.Now.Year;
        _guid = Guid.NewGuid().ToString("N");
    }
    public HttpCookie ReturnCookie(string username, string password, string projectID)
    {
        HttpCookie thisCookies = new HttpCookie(_guid);
        thisCookies.Values["UserName"] = _username;
        thisCookies.Values["PassWord"] = _password; 
        thisCookies.Values["ProjectID"] = _projectID;
        thisCookies.Values["Year"] = _year.ToString();
        return thisCookies;
    }

    public static void SetUserCookie(string lastname, string subcompanyname, string departmentname, string subcompanyid1, string workcode,string id,string roleid)
    {
        HttpCookie cookie = new HttpCookie("TXLUser");
        cookie.Values["lastname"] = lastname;
        //cookie.Values["UserName"] = HttpUtility.UrlEncode(username, Encoding.GetEncoding("UTF-8"));
        cookie.Values["subcompanyname"] = subcompanyname;
        cookie.Values["departmentname"] = departmentname;
        cookie.Values["subcompanyid1"] = subcompanyid1;
        cookie.Values["workcode"] = workcode;//长度太长不能存cookie里
        cookie.Values["id"] = id;
        cookie.Values["roleid"] = roleid;
        cookie.Expires = DateTime.Now.AddDays(1);

        HttpContext.Current.Response.Cookies.Add(cookie);

    }

    public static void SetProjectCookie(string cookiename,string year,string projectid)
    {
        HttpCookie cookie = new HttpCookie(cookiename);
        cookie.Values["Year"] = year;
        cookie.Values["ProjectID"] = projectid;

        HttpContext.Current.Response.Cookies.Add(cookie);
    }
}

public class Users
{
    //创建私有变量
    private string _subcompanyname;
    private string _lastname;
    private string _departmentname;
    private string _subcompanyid1;
    private string _workcode;
    private string _id;
    private string _roleid;
    //添加公有属性
    public string Lastname
    {
        get { return _lastname; }
        set { _lastname = value; }
    }

    public string Departmentname
    {
        get { return _departmentname; }
        set { _departmentname = value; }
    }

    public string Subcompanyname
    {
        get { return _subcompanyname; }
        set { _subcompanyname = value; }
    }

    public string Subcompanyid1
    {
        get { return _subcompanyid1; }
        set { _subcompanyid1 = value; }
    }

    public string Workcode
    {
        get { return _workcode; }
        set { _workcode = value; }
    }

    public string ID
    {
        get { return _id; }
        set { _id = value; }
    }

    public string RoleID
    {
        get { return _roleid; }
        set { _roleid = value; }
    }
    

    public Users()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        HttpCookie cookie = HttpContext.Current.Request.Cookies["TXLUser"];
        if (cookie != null)
        {
            this._lastname = cookie.Values["lastname"];
            this._subcompanyname = cookie.Values["subcompanyname"];
            this._departmentname = cookie.Values["departmentname"];
            this._subcompanyid1 = cookie.Values["subcompanyid1"];
            this._workcode = cookie.Values["workcode"];
            this._id = cookie.Values["id"];
            this._roleid = cookie.Values["roleid"];
            //this._projectid = cookie.Values["ProjectID"];//长度太长不能存cookie里
        }
        else
        {
            this._lastname = null;
            this._subcompanyname = null;
            this._departmentname = null;
            this._subcompanyid1 = null;
            this._workcode = null;
            this._id = null;
            this._roleid = null;
        }
    }
    //构造函数,创建Users的传参对象
    //public Users(string UserName, string Userpwd)
    //{
    //    this._username = UserName;
    //    this._userpwd = Userpwd;
    //}

}




public class ProjectCookie
{
    //创建私有变量
    private string _year;
    private string _projectid;
    //添加公有属性
    public string Year
    {
        get { return _year; }
        set { _year = value; }
    }

    public string ProjectID
    {
        get { return _projectid; }
        set { _projectid = value; }
    }

    public ProjectCookie(string cookiename)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
        this._year = cookie.Values["Year"];
        this._projectid = cookie.Values["ProjectID"];
    }


}

public class CookieHelper
{
    /// <summary>  
    /// 清除指定Cookie  
    /// </summary>  
    /// <param name="cookiename">cookiename</param>  
    public static void ClearCookie(string cookiename)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
        if (cookie != null)
        {
            cookie.Expires = DateTime.Now.AddYears(-3);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
    /// <summary>  
    /// 获取指定Cookie值  
    /// </summary>  
    /// <param name="cookiename">cookiename</param>  
    /// <returns></returns>  
    public static string GetCookieValue(string cookiename)
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
        string str = string.Empty;
        if (cookie != null)
        {
            str = cookie.Value;
        }
        return str;
    }
    /// <summary>  
    /// 添加一个Cookie（24小时过期）  
    /// </summary>  
    /// <param name="cookiename"></param>  
    /// <param name="cookievalue"></param>  
    public static void SetCookie(string cookiename, string cookievalue)
    {
        SetCookie(cookiename, cookievalue, DateTime.Now.AddDays(1.0));
    }
    /// <summary>  
    /// 添加一个Cookie  
    /// </summary>  
    /// <param name="cookiename">cookie名</param>  
    /// <param name="cookievalue">cookie值</param>  
    /// <param name="expires">过期时间 DateTime</param>  
    public static void SetCookie(string cookiename, string cookievalue, DateTime expires)
    {
        HttpCookie cookie = new HttpCookie(cookiename)
         {
             Value = cookievalue,
             Expires = expires
         };
        HttpContext.Current.Response.Cookies.Add(cookie);
    }

    /// <summary>  
    /// 创建Cookies  
    /// </summary>  
    /// <param name="strName">Cookie 主键</param>  
    /// <param name="strValue">Cookie 键值</param>  
    /// <param name="strDay">Cookie 天数</param>  
    /// <code>Cookie ck = new Cookie();</code>  
    /// <code>ck.setCookie("主键","键值","天数");</code>  
    public bool setCookie(string strName, string strValue, int strDay)
    {
        try
        {
            HttpCookie Cookie = new HttpCookie(strName);
            Cookie.Expires = DateTime.Now.AddDays(strDay);
            Cookie.Value = strValue;
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>  
    /// 读取Cookies  
    /// </summary>  
    /// <param name="strName">Cookie 主键</param>  
    /// <code>Cookie ck = new Cookie();</code>  
    /// <code>ck.getCookie("主键");</code>  
    public string getCookie(string strName)
    {
        HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
        if (Cookie != null)
        {
            return Cookie.Value.ToString();
        }
        else
        {
            return null;
        }
    }

    /// <summary>  
    /// 删除Cookies  
    /// </summary>  
    /// <param name="strName">Cookie 主键</param>  
    /// <code>Cookie ck = new Cookie();</code>  
    /// <code>ck.delCookie("主键");</code>  
    public bool delCookie(string strName)
    {
        try
        {
            HttpCookie Cookie = new HttpCookie(strName);
            Cookie.Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
            return true;
        }
        catch
        {
            return false;
        }
    }

}
