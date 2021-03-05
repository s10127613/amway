using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_Role_Edit : PageBase
{
    protected new int ID = 0;

    private bool isNew = false;

    string html = "";

    private string pageFilePath = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //CreatePrivilege();

        isNew = Request["id"] == null;

        ID = Request["id"] == null ? 0 : int.Parse(Request["id"]);

        LPermission.Text = ShowActionTable();

        if (!IsPostBack)
        {
            pageFilePath = Path.GetFileName(Request.PhysicalPath);
            if (pageFilePath.Contains("_Edit"))
            {
                pageFilePath = pageFilePath.Replace("_Edit", "");
            }
            mL_Title.Text = Function.SetTitle(pageFilePath);

            if (Request["id"] != null)
            {
                LMode.Text = "(編輯模式)";
            }

            if (!isNew)
            {
                var Data = db.Role.SingleOrDefault(n => n.ID == ID);

                if (Data != null)
                {
                    TB_Name.Text = Data.Name;

                    TB_Description.Text = Data.Description;

                    CB_Enable.Checked = Data.Enable;
                }
            }
        }
    }

    /// <summary>
    /// 產生權限設定表 
    /// </summary>
    /// <returns></returns>
    protected string ShowActionTable()
    {
        #region 產生權限設定表 

        string htm = "";

        htm += "<ul data-role='treeview'>";
        htm += ActionSubMenu(0, 0);
        htm += "</ul>";

        return htm;

        #endregion
    }

    /// <summary>
    /// 產生SITEMAP與權限選項
    /// </summary>
    /// <param name="id">MENU父ID</param>
    /// <param name="level">階層</param>
    /// <returns></returns>
    protected string ActionSubMenu(int id, int level)
    {
        var BackendMenu = db.BackendMenu.Where(n => n.Del == false && n.Enable == true && n.ParentID == 0).OrderBy(n => n.Sort);
        if (BackendMenu.Count() > 0)
        {
            foreach (var Item in BackendMenu)
            {
                html += "<li>";
                //html += $"<input value='chk1_{Item.ID.ToString()} ' type ='checkbox' id='chk1_{Item.ID.ToString()} ' name='chk1_{Item.ID.ToString()}' data-role='checkbox' data-caption='{Item.Name}'";
                //if (!isNew)
                //    html += PrintChecked(ID, Item.ID);
                //html += $"/><ul>";

                html += $"<i class='{Item.Icon}'></i>&nbsp;{Item.Name}";
                html += "<ul>";

                var BackendMenu2 = db.BackendMenu.Where(n => n.Del == false && n.Enable == true && n.ParentID == Item.ID);
                if (BackendMenu2.Count() > 0)
                {
                    foreach (var Item_2 in BackendMenu2)
                    {
                        html += "<li>";
                        //html += $"<input value='chk2_{Item_2.ID.ToString()} ' type ='checkbox' id='chk2_{Item_2.ID.ToString()} ' name='chk2_{Item_2.ID.ToString()}' data-role='checkbox' data-caption='{Item_2.Name}'  />";
                        html += $"<i class='{Item_2.Icon}'></i>&nbsp;{Item_2.Name}";

                        var BackendMenu3 = db.BackendMenu.Where(n => n.Del == false && n.Enable == true && n.ParentID == Item_2.ID);
                        if (BackendMenu3.Count() > 0)
                        {
                            html += "<ul>";
                            foreach (var Item_3 in BackendMenu3)
                            {
                                html += "<li>";
                                //html += $"<input value='chk3_{Item_3.ID.ToString()} ' style='font-size:16px' type ='checkbox' id='chk3_{Item_3.ID.ToString()} ' name='chk3_{Item_3.ID.ToString()}' data-role='checkbox' data-caption='{Item_3.Name}' />";
                                html += $"<i class='{Item_3.Icon}'></i>&nbsp;{Item_3.Name}";
                                var Data = db.BackendMenuPrivilege.Where(n => n.Del == false && n.BackendMenuID == Item_3.ID);
                                if (Data.Count() > 0)
                                {
                                    html += "<ul>";
                                    foreach (var Item_4 in Data)
                                    {
                                        html += "<li>";
                                        html += "   <div class='custom-control custom-checkbox'>";
                                        html += $"      <input id='chk4_{Item_4.ID.ToString()}' runat='server' type='checkbox' value='{PrintChecked2(ID, Item_4.ID)}' class='custom-control-input input-mini' checked=''>";
                                        html += $"      <label class='custom-control-label' for='chk4_{Item_4.ID.ToString()}'>{Item_4.Name}</label>";
                                        html += "   </div>";
                                        html += "</li>";
                                    }
                                    html += "</ul>";
                                    html += "</li>";
                                }
                            }
                            html += "</ul>";
                            html += "</li>";
                        }
                        else
                        {
                            var Data = db.BackendMenuPrivilege.Where(n => n.Del == false && n.BackendMenuID == Item_2.ID);
                            if (Data.Count() > 0)
                            {
                                html += "<ul>";
                                foreach (var Item_4 in Data)
                                {
                                    html += "<li>";
                                    html += "   <div class='custom-control custom-checkbox'>";
                                    html += $"      <input id='chk4_{Item_4.ID.ToString()}' runat='server' type='checkbox' value='{PrintChecked2(ID, Item_4.ID)}' class='custom-control-input input-mini' checked=''>";
                                    html += $"      <label class='custom-control-label' for='chk4_{Item_4.ID.ToString()}'>{Item_4.Name}</label>";
                                    html += "   </div>";
                                    html += "</li>";
                                }
                                html += "</ul>";
                                html += "</li>";
                            }
                        }
                    }
                }
                else
                {
                    var Data = db.BackendMenuPrivilege.Where(n => n.Del == false && n.BackendMenuID == Item.ID);
                    if (Data.Count() > 0)
                    {
                        html += "<ul>";
                        foreach (var Item_4 in Data)
                        {
                            html += "<li>";
                            html += "   <div class='custom-control custom-checkbox'>";
                            html += $"      <input id='chk4_{Item_4.ID.ToString()}' runat='server' type='checkbox' value='{PrintChecked2(ID, Item_4.ID)}' class='custom-control-input input-mini' checked=''>";
                            html += $"      <label class='custom-control-label' for='chk4_{Item_4.ID.ToString()}'>{Item_4.Name}</label>";
                            html += "   </div>";
                            html += "</li>";
                        }
                        html += "</ul>";
                        html += "</li>";
                    }
                }
                html += "</ul>";
                html += "</li>";
            }
        }

        #region 產生SITEMAP與權限選項
        return html;
        #endregion
    }

    /// <summary>
    /// 產生checkbox已選字串
    /// </summary>
    /// <param name="role_id"></param>
    /// <param name="sitemap_id"></param>
    /// <param name="action_id"></param>
    /// <returns></returns>
    protected string PrintChecked(int role_id, int sitemap_id)
    {
        #region 產生checkbox已選字串

        var a = from s in db.Role_BackendMenu
                where s.BackendMenuID == sitemap_id && s.RoleID == role_id && s.Del == false
                select s;

        if (a.Count() > 0)
            return "checked";
        else
            return "";

        #endregion
    }

    /// <summary>
    /// 產生checkbox已選字串
    /// </summary>
    /// <param name="role_id"></param>
    /// <param name="sitemap_id"></param>
    /// <param name="action_id"></param>
    /// <returns></returns>
    protected string PrintChecked2(int role_id, int backendmenu_id)
    {
        #region 產生checkbox已選字串

        var data = db.Role_BackendPrivilege.Where(n => n.RoleID == role_id && n.BackendMenuPrvilegeID == backendmenu_id);
        if (data.Count() > 0)
        {
            return "checked";
        }
        else
        {
            return "";
        }
        #endregion
    }

    /// <summary>
    /// 產生checkbox已選字串
    /// </summary>
    /// <param name="role_id"></param>
    /// <param name="sitemap_id"></param>
    /// <param name="action_id"></param>
    /// <returns></returns>
    protected string PrivilegeChecked(int role_id, int PrivilegeID)
    {
        #region 產生checkbox已選字串

        var a = from s in db.Role_BackendPrivilege
                where s.BackendMenuPrvilegeID == PrivilegeID && s.RoleID == role_id
                select s;

        if (a.Count() > 0)
            return "checked";
        else
            return "";

        #endregion
    }

    protected void Btn_Send_Click(object sender, EventArgs e)
    {
        #region 儲存

        DateTime UpdateTime = DateTime.Now;

        if (TB_Name.Text == null || TB_Name.Text == string.Empty)
        {
            Function.MsgBox(this, "請填入名稱。");

            return;
        }

        if (isNew)
        {
            #region 新增

            if (db.Role.Any(n => n.Name == TB_Name.Text && n.Del == false))
            {
                Function.MsgBox(this, "此名稱已使用。");

                return;
            }

            Role item = new Role()
            {
                Name = TB_Name.Text,
                Description = TB_Description.Text,
                CreateTime = UpdateTime,
                UpdateTime = UpdateTime,
                Enable = CB_Enable.Checked,
                Del = false
            };

            db.Role.InsertOnSubmit(item);
            db.SubmitChanges();

            SaveActionSel(item.ID);
            Function.InsertLog("Create", Function.GetPageName(Path.GetFileName(Request.PhysicalPath)), "Role", $"序號：{item.ID}；執行動作：新增資料", UserID);
            SA2Util.ShowMsg(this, "權限新增成功", SA2Util.PopupType.Success, 1500, ResolveClientUrl("~/Admin/Role.aspx"), false);

            #endregion
        }
        else
        {
            #region 編輯

            var Data = db.Role.SingleOrDefault(n => n.ID == ID);

            if (Data != null)
            {
                if (db.Role.Any(n => (n.Name == TB_Name.Text && n.Name != Data.Name) && n.Del == false))
                {
                    Function.MsgBox(this, "此名稱已使用。");

                    return;
                }

                Data.Name = TB_Name.Text;

                Data.Description = TB_Description.Text;

                Data.UpdateTime = UpdateTime;

                Data.UpdateUserID = UserID;

                Data.Enable = CB_Enable.Checked;

                db.SubmitChanges();

                SaveActionSel(Data.ID);
                Function.InsertLog("Update", Function.GetPageName(Path.GetFileName(Request.PhysicalPath)), "Role", $"序號：{Data.ID}；執行動作：更新資料", UserID);
                SA2Util.ShowMsg(this, "權限修改成功", SA2Util.PopupType.Success, 1500, ResolveClientUrl("~/Admin/Role.aspx"), false);
            }

            #endregion
        }

        #endregion

    }

    protected void SaveActionSel(int Role_ID)
    {
        #region 先刪除

        var RoleSiteMapData = db.Role_BackendMenu.Where(n => n.RoleID == Role_ID && n.Del == false);

        if (RoleSiteMapData.Count() > 0)
        {
            foreach (var item in RoleSiteMapData)
            {
                db.Role_BackendMenu.DeleteOnSubmit(item);
            }
            db.SubmitChanges();
        }

        var RoleSiteMapPrivilegeData = db.Role_BackendPrivilege.Where(n => n.RoleID == Role_ID);

        if (RoleSiteMapPrivilegeData.Count() > 0)
        {
            foreach (var item in RoleSiteMapPrivilegeData)
            {
                db.Role_BackendPrivilege.DeleteOnSubmit(item);
            }

            db.SubmitChanges();
        }

        #endregion

        #region 再新增

        for (int i = 0; i < Request.Form.Count; i++)
        {
            if (Request.Form.Keys[i].Substring(0, 5) == "chk4_")
            {
                string[] arr = Request[Request.Form.Keys[i]].ToString().Split('_');

                int _BackendPrivilegeID = Convert.ToInt32(arr[1]);

                var Data = db.BackendMenuPrivilege.FirstOrDefault(n => n.Del == false && n.ID == _BackendPrivilegeID);
                if (Data != null)
                {
                    Role_BackendPrivilege _BackendPrivilege = new Role_BackendPrivilege()
                    {
                        RoleID = Role_ID,
                        BackendMenuPrvilegeID = _BackendPrivilegeID,
                    };
                    db.Role_BackendPrivilege.InsertOnSubmit(_BackendPrivilege);
                    db.SubmitChanges();

                    int _BackendMenuID = Convert.ToInt32(Data.BackendMenuID);
                    if (CheckRole(Role_ID, _BackendMenuID))
                    {
                        Role_BackendMenu _BackendMenu = new Role_BackendMenu()
                        {
                            RoleID = Role_ID,
                            BackendMenuID = _BackendMenuID,
                            Del = false,
                        };
                        db.Role_BackendMenu.InsertOnSubmit(_BackendMenu);
                        db.SubmitChanges();
                    }

                    int ParentID = Convert.ToInt32(db.BackendMenu.FirstOrDefault(n => n.Del == false && n.Enable == true && n.ID == _BackendMenuID).ParentID);
                    while (ParentID != 0)
                    {
                        var data = db.BackendMenu.FirstOrDefault(n => n.Del == false && n.Enable == true && n.ID == ParentID);
                        if (data != null)
                        {
                            if (CheckRole(Role_ID, data.ID))
                            {
                                Role_BackendMenu _BackendMenu2 = new Role_BackendMenu()
                                {
                                    RoleID = Role_ID,
                                    BackendMenuID = data.ID,
                                    Del = false,
                                };
                                db.Role_BackendMenu.InsertOnSubmit(_BackendMenu2);
                                db.SubmitChanges();
                            }
                            ParentID = Convert.ToInt32(data.ParentID);
                        }
                    }
                }
            }
        }
        #endregion
    }


    protected bool CheckRole(int role_id,int backendmenu_id)
    {
        var data = db.Role_BackendMenu.Where(n => n.Del == false && n.RoleID == role_id && n.BackendMenuID == backendmenu_id);
        if (data.Count() > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void CreatePrivilege()
    {
        #region 建立選單權限細項

        string[] Names = new string[] { "新增", "修改", "刪除" };

        var BackendDatas = db.BackendMenu.Where(n => n.Enable == true && n.Del == false);

        foreach (var item in BackendDatas)
        {
            if (db.BackendMenuPrivilege.Any(n => n.BackendMenuID == item.ID)) continue;

            if (item.URL == null || item.URL == string.Empty || item.URL == "#") continue;

            for (int i = 0; i < 3; i++)
            {
                BackendMenuPrivilege item_New = new BackendMenuPrivilege();

                item_New.BackendMenuID = item.ID;

                item_New.Name = Names[i];

                item_New.Sort = i;

                if (i == 0)
                {
                    item_New.Flag = 1;
                }
                else
                {
                    item_New.Flag = 2 * i;
                }

                item_New.Del = false;

                db.BackendMenuPrivilege.InsertOnSubmit(item_New);

                db.SubmitChanges();
            }
        }

        #endregion
    }
}