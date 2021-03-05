using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


/// <summary>
/// PageBase
/// </summary>
public class PageBase : System.Web.UI.Page
{
    /// <summary>
    /// 取得 DataDataContext
    /// </summary>
    public AmwayDataContext db = Function.GetDB();

    public int? PageID { get; set; }

    /// <summary>
    /// 使用者的 ID 值
    /// </summary>
    private int user_id;

    /// <summary>
    /// 使用者的角色的 ID 值
    /// </summary>
    private int role_id;

    /// <summary>
    /// 使用者的名稱
    /// </summary>
    private string user_name;

    /// <summary>
    /// 同等頁面
    /// </summary>
    public string PagePeer { get; set; }

    public PageBase()
    {
        this.Init += CustomInit;
        this.LoadComplete += CheckPageID;
        this.LoadComplete += CustomLoad;
        this.LoadComplete += CheckPageAccessPermission;
    }

    /// <summary>
    /// 自動設定PageID
    /// </summary>
    protected void CheckPageID(object sender, EventArgs e)
    {
        if (!PageID.HasValue)
        {
            string PageUrl = !string.IsNullOrEmpty(PagePeer) ? PagePeer : Request.AppRelativeCurrentExecutionFilePath.Replace("~/", "");
            var pageobj = db.BackendMenu.SingleOrDefault(s => s.URL.Contains(PageUrl));

            if (pageobj != null)
            {
                PageID = pageobj.ID;
            }
            else
            {
                throw new Exception("找不到PageID");
            }
        }
    }



    protected void CheckPageAccessPermission(object sender, EventArgs e)
    {
        string path = string.Empty;
        if (!string.IsNullOrEmpty(PagePeer))
        {
            path = PagePeer;
        }
        else
        {
            path = Path.GetFileName(Request.PhysicalPath);
        }


        var vBackEndMenu = db.BackendMenu.FirstOrDefault(n => n.Del == false && n.Enable == true && n.URL.Contains(path.Trim()));
        if (vBackEndMenu != null)
        {
            var vSystemUser_Role = db.SystemUser_Role.FirstOrDefault(n => !n.Del && n.SystemUserID == UserID);
            if (vSystemUser_Role != null)
            {
                //確認是否有訪問頁面權限
                var vRole_BackEndMenu = db.Role_BackendMenu.FirstOrDefault(n => !n.Del && n.BackendMenuID == vBackEndMenu.ID && n.RoleID == vSystemUser_Role.RoleID);
                if (vRole_BackEndMenu == null)
                    Response.Redirect(ResolveClientUrl("~/Admin/Index.aspx"));
            }
        }
    }

    /// <summary>
    /// 自定義 Init
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void CustomInit(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            if (Session["user_id"] == null || Session["username"] == null || Session["role_id"] == null)
            {
                if (db.SystemUser_Role.Any(c => c.Del == false && c.SystemUserID.ToString() == Page.User.Identity.Name))
                {
                    Session["user_id"] = Page.User.Identity.Name;
                    Session["username"] = db.SystemUser.First(c => c.ID.ToString() == Page.User.Identity.Name).Name;

                    if (db.SystemUser_Role.Any(c => c.Del == false && c.SystemUserID.ToString() == Page.User.Identity.Name))
                    {
                        Session["role_id"] = db.SystemUser_Role.First(c => c.Del == false && c.SystemUserID.ToString() == Page.User.Identity.Name).RoleID;
                    }
                    else
                    {
                        Logout();
                    }
                }
                else
                {
                    Logout();
                }
            }
            else
            {
                user_id = Convert.ToInt32(Session["user_id"]);
                role_id = Convert.ToInt32(Session["role_id"]);
                user_name = Session["username"].ToString();
            }
        }
        else
        {
            Logout();
        }
    }

    /// <summary>
    /// 自定義 Load
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void CustomLoad(object sender, EventArgs e)
    {
        if(PageID != 0)
        {
            if(!db.Role_BackendMenu.Any(n => n.RoleID == RoleID && n.BackendMenuID == PageID && n.Del == false))
            {
                Logout();
            }
        }
    }

    /// <summary>
    /// 使用者的 ID 值
    /// </summary>
    public int UserID
    {
        get
        {
            return user_id;
        }
    }

    /// <summary>
    /// 使用者的角色的 ID 值
    /// </summary>
    public int RoleID
    {
        get
        {
            return role_id;
        }
    }

    /// <summary>
    /// 使用者名稱
    /// </summary>
    public string UserName
    {
        get
        {
            return user_name;
        }
    }

    /// <summary>
    /// 登出系統
    /// </summary>
    private void Logout()
    {
        Function.Logout(this);
    }

    /// <summary>
    /// 驗證是否有指定的頁面權限
    /// </summary>
    /// <param name="privlege">Function.Privlege</param>
    /// <returns>布林值，true 為有此權限；false 則表反之</returns>
    public bool CheckPagePrivilege(Function.Privlege privlege)
    {
        int flagValue = (int)privlege;

        var data = db.BackendMenuPrivilege.FirstOrDefault(p => p.BackendMenuID == PageID && p.Flag == flagValue && p.Del == false);

        return data != null && db.Role_BackendPrivilege.Any(sp => sp.RoleID == RoleID && sp.BackendMenuPrvilegeID == data.ID);
    }

    /// <summary>
    /// 檢查 Session
    /// </summary>
    /// <param name="httpContext">HttpContext</param>
    /// <returns>布林值</returns>
    public static bool CheckSession(HttpContext httpContext)
    {
        bool result = true;

        if (httpContext.Session["user_id"] == null)
        {
            result = false;
        }

        if (httpContext.Session["username"] == null)
        {
            result = false;
        }

        if (httpContext.Session["role_id"] == null)
        {
            result = false;
        }

        return result;
    }
}

public static class PageBaeExtensionMethods
{
    public static PageBase GetPageBase(this Page self)
    {
        if (self?.Is<PageBase>() != true)
        {
            throw new NullReferenceException();
        }

        return (PageBase)self;
    }
}