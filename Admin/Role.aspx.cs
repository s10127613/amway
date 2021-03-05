using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using LinqKit;

public partial class Admin_Role : PageBase
{
    private bool IsCheck = false;

    protected void Page_Init(object sender, EventArgs e)
    {

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
        var vRole = db.Role.Where(n => n.Del == false);
        if (vRole.Count() > 0)
        {
            ddl_Position.DataSource = vRole;
            ddl_Position.DataTextField = "Name";
            ddl_Position.DataValueField = "ID";
            ddl_Position.DataBind();
        }
        ddl_Position.Items.Add(new ListItem("請選擇…", "-1"));
        ddl_Position.SelectedValue = "-1";
    }

    private void BindData()
    {
        var pbSearch = PredicateBuilder.New<Role>();

        if (ddl_Position.SelectedValue != "-1")
        {
            int ID = Convert.ToInt32(ddl_Position.SelectedValue);
            pbSearch = pbSearch.And(n => n.ID == ID);
        }

        if (mTB_Description.Text != "")
        {
            pbSearch = pbSearch.And(n => n.Description.Contains(mTB_Description.Text));
        }

        pbSearch = pbSearch.And(n => n.Del == false);

        var Data = db.Role.Where(pbSearch).AsEnumerable().OrderBy(n => n.Sort).ToList();
        if (Data.Count() > 0)
        {
            LvRoleList.DataSource = Data.Select(n => new
            {
                ID = $"{string.Format("{0:0000}", n.ID)}",
                n.Name,
                n.Description,
                n.Enable,
            }).ToList();
        }
        LvRoleList.DataBind();
    }

    protected void LBtn_AddData_Click(object sender, EventArgs e)
    {
        #region 新增
        Response.Redirect(ResolveClientUrl("~/Admin/Role_Edit.aspx"));
        #endregion
    }

    protected void LBtn_Edit_Command(object sender, CommandEventArgs e)
    {
        #region 編輯
        int ID = Convert.ToInt32(e.CommandArgument);
        Response.Redirect(ResolveClientUrl($"~/Admin/Role_Edit.aspx?id={ID}"));
        #endregion
    }

    protected void LBtn_Delete_Command(object sender, CommandEventArgs e)
    {
        #region 刪除
        int ID = Convert.ToInt32(e.CommandArgument);
        if (db.SystemUser_Role.Any(n => n.RoleID == ID))
        {
            Function.MsgBox(this, "尚有帳號在這群組裡");

            return;
        }

        var BackendDatas = db.Role_BackendMenu.Where(n => n.RoleID == ID && n.Del == false);

        foreach (var item in BackendDatas)
        {
            item.Del = true;

            db.SubmitChanges();
        }

        var PrivilegeDatas = db.Role_BackendPrivilege.Where(n => n.RoleID == ID);

        foreach (var item in PrivilegeDatas)
        {
            db.Role_BackendPrivilege.DeleteOnSubmit(item);

            db.SubmitChanges();
        }

        var RoleData = db.Role.SingleOrDefault(n => n.ID == ID);

        if (RoleData != null)
        {
            RoleData.Del = true;

            db.SubmitChanges();
        }
        Function.InsertLog("Delete", Function.GetPageName(Path.GetFileName(Request.PhysicalPath)), "Role", $"序號：{ID}；執行動作：刪除資料", UserID);

        LvRoleList.DataBind();

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

    protected void BT_Search_Click(object sender, EventArgs e)
    {
        #region 快速查詢
        BindData();
        #endregion
    }

    protected void BT_Clean_Click(object sender, EventArgs e)
    {
        #region 清除搜尋選項
        ddl_Position.SelectedValue = "-1";
        mTB_Description.Text = string.Empty;
        BindData();
        #endregion
    }

    protected void LvRoleList_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton LBtn_Delete = (LinkButton)e.Item.FindControl("LBtn_Delete");

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            if (!CheckPagePrivilege(Function.Privlege.新增))
            {

            }

            if (!CheckPagePrivilege(Function.Privlege.修改))
            {

            }

            if (!CheckPagePrivilege(Function.Privlege.刪除))
            {
                LBtn_Delete.Visible = false;
            }
        }
    }

    protected void LvRoleList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager dataPager = this.LvRoleList.FindControl("DataPager1") as DataPager;
        int StartRowIndex = e.StartRowIndex;
        int MaximunRows = e.MaximumRows;
        dataPager.SetPageProperties(StartRowIndex, MaximunRows, false);
        BindData();
    }
}