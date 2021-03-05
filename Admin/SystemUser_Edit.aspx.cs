using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Admin_SystemUser_Edit : PageBase
{
    private string pageFilePath = string.Empty;

    protected void Page_Init(object sender, EventArgs e)
    {
        PageID = 14; //設定頁面ID
        mL_Title.Text = Function.SetTitle(Path.GetFileName(Request.PhysicalPath));
        LMode.Text = Request["ID"] != null ? "(編輯模式)" : "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AddRoleItem(); // 添加群組選項
            BindData();
        }
    }



    protected void LBtn_Back_Click(object sender, EventArgs e)
    {
        #region 返回上一頁

        Response.Redirect(ResolveClientUrl("~/Admin/SystemUser.aspx"));

        #endregion
    }

    protected void LBtn_Save_Click(object sender, EventArgs e)
    {
        #region 儲存

        if (SaveCheck() != string.Empty)
        {
            SA2Util.ShowMsg(this, SaveCheck(), SA2Util.PopupType.Warning, 1500, false);
            return;
        }

        if (Request["id"] != null)
        {
            #region 編輯

            int ID = int.Parse(Request["id"]);
            var UserData = db.SystemUser.SingleOrDefault(n => n.ID == ID && n.Del == false);

            if (UserData != null)
            {
                if (db.SystemUser.Any(n => (n.Name == TB_Name.Text && n.Name != UserData.Name) && n.Del == false))
                {
                    SA2Util.ShowMsg(this, "此帳號已被使用", SA2Util.PopupType.Error, 1500, false);
                    return;
                }

                UserData.Name = TB_Name.Text;

                if (TB_Password.Text != null && TB_Password.Text != string.Empty)
                {
                    UserData.Password = Function.GenPwdHash(TB_Password.Text.Trim());
                }

                UserData.Description = TB_Description.Text;

                UserData.PositionID = int.Parse(DDL_Position.SelectedValue);

                UserData.Email = TB_Email.Text;

                UserData.Phone = TB_Phone.Text;

                UserData.Enable = CB_Enable.Checked;

                db.SubmitChanges();
            }

            var RoleData = db.SystemUser_Role.SingleOrDefault(n => n.SystemUserID == ID);

            if (RoleData != null)
            {
                RoleData.RoleID = DDL_Role.SelectedIndex;

                db.SubmitChanges();
            }
            Function.InsertLog("Update", Function.GetPageName(Path.GetFileName(Request.PhysicalPath)), "SystemUser", $"序號：{ID}；執行動作：更新資料", UserID);
            Function.MsgBoxRedirect(this.Page, "帳號編輯成功", "SystemUser.aspx");
            #endregion
        }
        else
        {
            #region 新增

            if (TB_Password.Text == null || TB_Password.Text == string.Empty)
            {
                Function.MsgBox(this.Page, "請輸入密碼");
                return;
            }

            if (db.SystemUser.Any(n => n.Account == TB_Account.Text.Trim() && n.Del == false))
            {
                Function.MsgBox(this.Page, "此帳號已被使用");
                return;
            }

            if (db.SystemUser.Any(n => n.Name == TB_Name.Text && n.Del == false))
            {
                Function.MsgBox(this.Page, "此名稱已被使用");
                return;
            }

            SystemUser item_sUser = new SystemUser
            {
                PositionID = int.Parse(DDL_Position.SelectedValue),
                Name = TB_Name.Text,
                Account = TB_Account.Text,
                Password = Function.GenPwdHash(TB_Password.Text.Trim()),
                Description = TB_Description.Text,
                CreateTime = DateTime.Now,
                Email = TB_Email.Text,
                Phone = TB_Phone.Text,
                Enable = CB_Enable.Checked,
                Del = false
            };

            db.SystemUser.InsertOnSubmit(item_sUser);
            db.SubmitChanges();

            SystemUser_Role item_sRole = new SystemUser_Role
            {
                RoleID = DDL_Role.SelectedIndex,
                SystemUserID = item_sUser.ID
            };

            db.SystemUser_Role.InsertOnSubmit(item_sRole);

            db.SubmitChanges();

            ViewState["suid"] = item_sUser.ID;
            Function.InsertLog("Create", Function.GetPageName(Path.GetFileName(Request.PhysicalPath)), "SystemUser", $"序號：{ID}；執行動作：新增資料", UserID);
            Function.MsgBoxRedirect(this.Page, "帳號新增成功", "SystemUser.aspx");
            #endregion
        }

        #endregion
    }

    /// <summary>
    /// 資料連繫
    /// </summary>
    private void BindData()
    {
        if (Request["ID"] != null)
        {
            #region 讀取資料

            if (!CheckPagePrivilege(Function.Privlege.修改))
            {
                LBtn_Basic_Save.Visible = false;
            }

            int ID = int.Parse(Request["ID"]);

            ViewState["suid"] = ID;

            var Data = db.SystemUser.SingleOrDefault(n => n.ID == ID && n.Del == false);

            if (Data != null)
            {
                TB_Name.Text = Data.Name;

                TB_Account.Text = $"{Data.Account}";

                TB_Account.Enabled = false;

                LBL_Password.Visible = true;

                DDL_Position.SelectedValue = Data.PositionID.ToString();

                TB_Phone.Text = Data.Phone;

                TB_Email.Text = Data.Email;

                if (Data.Description != null && Data.Description != string.Empty)
                {
                    TB_Description.Text = Data.Description;
                }

                CB_Enable.Checked = Data.Enable;

                var SystemRoleData = db.SystemUser_Role.SingleOrDefault(n => n.SystemUserID == ID && n.Del == false);

                if (SystemRoleData != null)
                {
                    DDL_Role.SelectedValue = SystemRoleData.RoleID.ToString();

                    DDL_Role.Enabled = false;
                }
            }
            #endregion
        }
        else
        {
            if (!CheckPagePrivilege(Function.Privlege.新增))
            {
                LBtn_Basic_Save.Visible = false;
            }
        }
    }

    /// <summary>
    /// 添加群組選項
    /// </summary>
    protected void AddRoleItem()
    {
        #region 添加群組選項

        var RoleDatas = db.Role.Where(n => n.Enable == true && n.Del == false);

        if (RoleDatas.Count() > 0)
        {
            foreach (var item in RoleDatas)
            {
                DDL_Role.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }
        }
        #endregion

        #region 添加身分選項

        var PositionData = db.SystemPosition.Where(n => n.Del == false);

        if (PositionData.Count() > 0)
        {
            foreach (var item in PositionData)
            {
                DDL_Position.Items.Add(new ListItem(item.Name, item.ID.ToString()));
            }
        }
        #endregion
    }

    /// <summary>
    /// 存檔檢查
    /// </summary>
    /// <returns>檢查結果的訊息字串</returns>
    protected string SaveCheck()
    {
        #region 存檔檢查

        string Temp = string.Empty;

        if (TB_Name.Text == null || TB_Name.Text == string.Empty)
        {
            Temp = "請輸入使用者名稱";

            return Temp;
        }

        if (TB_Account.Text == null || TB_Account.Text == string.Empty)
        {
            Temp = "請輸入帳號";

            return Temp;
        }

        if (DDL_Role.SelectedValue == "0")
        {
            Temp = "請選擇群組";

            return Temp;
        }

        if (DDL_Position.SelectedValue == "0")
        {
            Temp = "請選擇身分";

            return Temp;
        }

        if (string.IsNullOrEmpty(TB_Email.Text))
        {
            Temp = "請輸入電子郵件";

            return Temp;
        }

        if (string.IsNullOrEmpty(TB_Phone.Text))
        {
            Temp = "請輸入連絡電話";

            return Temp;
        }

        return Temp;

        #endregion
    }
}