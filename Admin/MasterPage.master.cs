using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BackendMenu();
            BindData();
            SetBreadcrumb();
        }
    }

    /// <summary>
    /// 設定後臺頁面麵包屑
    /// </summary>
    protected void SetBreadcrumb()
    {
        int pageID = Page.GetPageBase().PageID.Value;

        var db = Function.GetDB();

        string TempStr = string.Empty;

        var vBackendMenu = db.BackendMenu.FirstOrDefault(n => n.ID == pageID && n.Enable == true && n.Del == false);
        if (vBackendMenu != null)
        {
            LPageTitle.Text = vBackendMenu.Name;
            TempStr += "<div class='breadcrumb pd-0 mg-0'>";
            TempStr += "<a class='breadcrumb-item' href='index.aspx'><i class='icon ion-ios-home-outline'></i>&nbsp;首頁</a>";
            var vParent = db.BackendMenu.FirstOrDefault(n => n.Del == false && n.Enable == true && n.ID == vBackendMenu.ParentID);
            if (vParent != null)
            {
                var vParent02 = db.BackendMenu.FirstOrDefault(n => n.Del == false && n.Enable == true && n.ID == vParent.ParentID);
                if (vParent02 != null)
                {
                    TempStr += $"<a class='breadcrumb-item' href='#'>{vParent02.Name}</a>";
                }
                TempStr += $"<a class='breadcrumb-item' href='#'>{vParent.Name}</a>";
            }
            TempStr += $"<span class='breadcrumb-item active'>{vBackendMenu.Name}</span>";
            TempStr += "</div>";
            LBreadcrumb.Text = TempStr;
        }
    }

    /// <summary>
    /// 資料繫結
    /// </summary>
    protected void BindData()
    {
        var db = Function.GetDB();
        if (Session["user_id"] != null)
        {
            int User_ID = int.Parse(Session["user_id"].ToString());
            var UserData = db.SystemUser.SingleOrDefault(n => n.ID == User_ID && n.Enable == true && n.Del == false);

            if (UserData != null)
            {
                LUserName.Text = LName.Text = UserData.Name;
            }
        }
    }

    /// <summary>
    /// 設定後臺選單項目
    /// </summary>
    private void BackendMenu()
    {
        string TempStr = string.Empty;
        int ParentID = 0;

        var db = Function.GetDB();

        string PageUrl = Path.GetFileName(Request.PhysicalPath);
        if (PageUrl.Contains("_Edit"))
        {
            PageUrl = PageUrl.Replace("_Edit", "");
        }

        var data = db.BackendMenu.FirstOrDefault(n => n.Del == false && n.Enable == true && n.URL.Contains(PageUrl));
        if (data != null)
        {
            ParentID = Convert.ToInt32(data.ParentID);
        }

        var vLayer01 = db.BackendMenu.Where(n => n.Del == false & n.Enable == true & n.ParentID == 0);
        if (vLayer01.Count() > 0)
        {
            foreach (var Layer01 in vLayer01)
            {
                TempStr += $"<li class='mg-20-force menu-elements'>{Layer01.Name}</li>";
                var vLayer02 = db.BackendMenu.Where(n => n.Del == false & n.Enable == true & n.ParentID == Layer01.ID);
                if (vLayer02.Count() > 0)
                {
                    foreach (var Layer02 in vLayer02)
                    {
                        if (Layer02.ID == ParentID)
                            TempStr += "<li class='active open'>";
                        else
                            TempStr += "<li>";

                        var vLayer03 = db.BackendMenu.Where(n => n.Del == false & n.Enable == true & n.ParentID == Layer02.ID);
                        if (vLayer03.Count() > 0)
                        {
                            TempStr += $"<a href='#'><i data-feather='{Layer02.Icon}' class=''></i><span>{Layer02.Name}</span><i class='accordion-icon fa fa-angle-left'></i></a>";

                            if (Layer02.ID == ParentID)
                                TempStr += "<ul class='sub-menu' style='display: block;'>";
                            else
                                TempStr += "<ul class='sub-menu'>";

                            foreach (var Layer03 in vLayer03)
                            {
                                if (Layer03.URL.Contains(PageUrl))
                                    TempStr += $"<li class='active'>";
                                else
                                    TempStr += "<li>";

                                TempStr += $"       <a href='{ResolveClientUrl(Layer03.URL)}'>{Layer03.Name}</a>";
                                TempStr += "    </li>";
                            }
                            TempStr += "</ul>";
                        }
                        else
                        {
                            if (Layer02.URL.Contains(PageUrl))
                                TempStr += "<li class='active open'>";
                            else
                                TempStr += "<li>";

                            TempStr += $"<a href='{ResolveClientUrl(Layer02.URL)}'><i data-feather='{Layer02.Icon}'></i><span>{Layer02.Name}</span></a>";
                        }
                        TempStr += "</li>";
                    }
                }
                TempStr += "<li class='menu-divider mg-y-20-force'></li>";
            }
        }
        LSideMenu.Text = TempStr;
    }
}
