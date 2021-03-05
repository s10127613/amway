using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinqKit;
using System.IO;

public partial class Admin_SystemUser : PageBase
{
    private bool IsCheck = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        PageID = 14; //設定頁面ID
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
        var db = Function.GetDB();
        var vRole = db.Role.Where(n => n.Del == false && n.Enable == true).ToList();
        if (vRole.Count() > 0)
        {
            ddl_Role.DataSource = vRole;
            ddl_Role.DataTextField = "Name";
            ddl_Role.DataValueField = "ID";
            ddl_Role.DataBind();
        }
        ddl_Role.Items.Add(new ListItem("請選擇…", "-1"));
        ddl_Role.SelectedValue = "-1";
    }

    private void BindData()
    {
        var pbSearch = PredicateBuilder.New<SystemUser>();

        if (ddl_Enable.SelectedValue != "-1")
        {
            bool Enable = Convert.ToBoolean(ddl_Enable.SelectedValue);
            pbSearch = pbSearch.And(n => n.Enable == Enable);
        }

        if (ddl_Role.SelectedValue != "-1")
        {
            int RoleID = Convert.ToInt32(ddl_Role.SelectedValue);
            pbSearch = pbSearch.And(n => n.SystemUser_Role.FirstOrDefault(a => a.RoleID == RoleID) != null);
        }

        if (mTB_Name.Text != "")
        {
            pbSearch = pbSearch.And(n => n.Name.Contains(mTB_Name.Text.Trim()));
        }

        if (mTB_Account.Text != "")
        {
            pbSearch = pbSearch.And(n => n.Account.Contains(mTB_Account.Text.Trim()));
        }

        pbSearch = pbSearch.And(n => n.Del == false);

        var Data = db.SystemUser.Where(pbSearch).OrderBy(n => n.ID).Select(n => new
        {
            ID = $"{string.Format("{0:0000}", n.ID)}",
            n.Name,
            Account = $"{n.Account.Substring(0, n.Account.Length / 2)}****",
            n.Description,
            n.Enable,
        }).ToList();

        if (Data.Count() > 0)
        {
            LvAccount.DataSource = Data;
        }
        LvAccount.DataBind();
    }

    protected void LBtn_Edit_Command(object sender, CommandEventArgs e)
    {
        #region 編輯

        int ID = Convert.ToInt32(e.CommandArgument);

        Response.Redirect(ResolveClientUrl($"~/Admin/SystemUser_Edit.aspx?id={ID}"));

        #endregion
    }

    protected void LBtn_Delete_Command(object sender, CommandEventArgs e)
    {
        #region 刪除

        int ID = Convert.ToInt32(e.CommandArgument);

        var UserData = db.SystemUser.SingleOrDefault(n => n.ID == ID && n.Del == false);

        if (UserData != null)
        {
            UserData.Del = true;

            db.SubmitChanges();
        }

        var UserRoleData = db.SystemUser_Role.SingleOrDefault(n => n.SystemUserID == ID);

        if (UserRoleData != null)
        {
            UserRoleData.Del = true;

            db.SubmitChanges();
        }
        Function.InsertLog("Delete", Function.GetPageName(Path.GetFileName(Request.PhysicalPath)), "SystemUser", $"序號：{ID}；執行動作：刪除資料", UserID);
        Response.Redirect(ResolveClientUrl($"~/Admin/SystemUser.aspx"));

        #endregion
    }

    protected void LBtn_AddData_Click(object sender, EventArgs e)
    {
        #region 建立新帳號

        Response.Redirect(ResolveClientUrl("~/Admin/SystemUser_Edit.aspx"));

        #endregion
    }

    protected void CheckPrivilege()
    {
        #region 檢查權限

        if (!CheckPagePrivilege(Function.Privlege.新增))
        {
            LBtn_AddData.Visible = false;
        }

        #endregion
    }

    protected void Lbtn_Search_Click(object sender, EventArgs e)
    {
        #region 查詢

        BindData();

        #endregion
    }

    protected void Lbtn_SearchClear_Click(object sender, EventArgs e)
    {
        #region 清除
        ddl_Enable.SelectedValue = "-1";
        ddl_Role.SelectedValue = "-1";
        mTB_Account.Text = "";
        mTB_Name.Text = "";
        BindData();

        #endregion
    }

    protected void LvAccount_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton LBtn_Edit = (LinkButton)e.Item.FindControl("LBtn_Edit");
        LinkButton LBtn_Delete = (LinkButton)e.Item.FindControl("LBtn_Delete");

        Label LEnable = (Label)e.Item.FindControl("LEnable");

        bool Enable = Convert.ToBoolean(LEnable.Text);
        if (Enable)
        {
            LEnable.Text = "已啟用";
            LEnable.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            LEnable.Text = "未啟用";
            LEnable.ForeColor = System.Drawing.Color.DarkRed;
        }

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            if (!CheckPagePrivilege(Function.Privlege.新增))
            {
                LBtn_AddData.Visible = false;
            }

            if (!CheckPagePrivilege(Function.Privlege.修改))
            {
                //LBtn_Edit.Visible = false;
            }

            if (!CheckPagePrivilege(Function.Privlege.刪除))
            {
                LBtn_Delete.Visible = false;
            }
        }
    }

    protected void LvAccount_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager dataPager = this.LvAccount.FindControl("DataPager1") as DataPager;
        int StartRowIndex = e.StartRowIndex;
        int MaximunRows = e.MaximumRows;
        dataPager.SetPageProperties(StartRowIndex, MaximunRows, false);
        BindData();
    }
}