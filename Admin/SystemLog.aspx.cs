using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using LinqKit;

public partial class Admin_SystemLog : PageBase
{
    protected void Page_Init(object sender, EventArgs e)
    {
        PageID = 15; //設定頁面ID
        LTitle.Text = Function.SetTitle(Path.GetFileName(Request.PhysicalPath));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDDL();
            BindData();
        }
    }

    private void SetDDL()
    {
        var vSystemUser = db.SystemUser.Where(n => n.Del == false).OrderBy(n => n.ID).ToList();
        if (vSystemUser.Count() > 0)
        {
            ddl_User.DataSource = vSystemUser;
            ddl_User.DataTextField = "Name";
            ddl_User.DataValueField = "ID";
            ddl_User.DataBind();
        }
        ddl_User.Items.Add(new ListItem("請選擇", "-1"));
        ddl_User.SelectedValue = "-1";

        var vPageName = db.BackendMenu.Where(n => n.Del == false && n.Enable == true && n.ParentID != 0 && !n.Name.Contains("系統使用記錄")).OrderBy(n => n.Sort).ToList();
        if (vPageName.Count() > 0)
        {
            ddl_PageName.DataSource = vPageName;
            ddl_PageName.DataTextField = "Name";
            ddl_PageName.DataValueField = "ID";
            ddl_PageName.DataBind();
        }
        ddl_PageName.Items.Add(new ListItem("請選擇", "-1"));
        ddl_PageName.SelectedValue = "-1";
    }

    private void BindData()
    {
        var pbSearch = PredicateBuilder.New<SystemLog>();

        if (ddl_Action.SelectedValue != "-1")
        {
            string Action = ddl_Action.SelectedValue;
            pbSearch = pbSearch.And(n => n.Action.Contains(Action));
        }

        if (mTB_Memo.Text.Trim() != "")
        {
            string Memo = mTB_Memo.Text.Trim();
            pbSearch = pbSearch.And(n => n.Memo.Contains(Memo));
        }
        else
        {
            pbSearch = pbSearch.And(n => n.Memo != null);
        }

        if (ddl_User.SelectedValue != "-1")
        {
            int SystemUserID = Convert.ToInt32(ddl_User.SelectedValue);
            pbSearch = pbSearch.And(n => n.SystemUserID == SystemUserID);
        }

        var Data = db.SystemLog.Where(pbSearch).OrderByDescending(n => n.CreateDate).Select(n => new
        {
            n.Action,
            n.PageName,
            n.Memo,
            n.SystemUserID,
            n.CreateDate,
        }).ToList();

        if (Data.Count() > 0)
        {
            LVLog.DataSource = Data;
        }
        LVLog.DataBind();
    }

    protected void LVLog_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label LAction = (Label)e.Item.FindControl("LAction");
        Label LSystemUser = (Label)e.Item.FindControl("LSystemUser");
        Label LPageName = (Label)e.Item.FindControl("LPageName");

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            if (LAction != null)
            {
                string str = LAction.Text;
                switch (str)
                {
                    case "Login":
                        LAction.Text = "登入";
                        if (LPageName != null)
                        {
                            LPageName.Text = "首頁";
                        }
                        break;
                    case "Logout":
                        LAction.Text = "登出";
                        if (LPageName != null)
                        {
                            LPageName.Text = "首頁";
                        }
                        break;
                    case "Create":
                        LAction.Text = "新增";
                        break;
                    case "Update":
                        LAction.Text = "修改";
                        break;
                    case "Delete":
                        LAction.Text = "刪除";
                        break;
                }
            }



            if (LSystemUser != null)
            {
                int ID = Convert.ToInt32(LSystemUser.Text);
                var vSystemUser = db.SystemUser.FirstOrDefault(n => n.Del == false && n.ID == ID);
                if (vSystemUser != null)
                {
                    LSystemUser.Text = vSystemUser.Name;
                }
            }
        }
    }

    protected void LVLog_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager dataPager = this.LVLog.FindControl("DataPager1") as DataPager;
        int StartRowIndex = e.StartRowIndex;
        int MaximunRows = e.MaximumRows;
        dataPager.SetPageProperties(StartRowIndex, MaximunRows, false);
        BindData();
    }

    protected void LBSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void LBClean_Click(object sender, EventArgs e)
    {
        mTB_Memo.Text = "";
        ddl_User.SelectedValue = "-1";
        ddl_Action.SelectedValue = "-1";
        BindData();
    }
}