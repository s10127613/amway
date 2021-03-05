using System;
using System.Linq;
using System.Web.Security;
using System.Web.UI;

public partial class Admin_Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated)
        {
            if (Session["username"] != null)
            {
                FormsAuthentication.RedirectFromLoginPage(Session["username"].ToString(), false);
            }
            else
            {
                Function.Logout(this);
            }
        }
    }

    protected void LBtnLogin_Click(object sender, EventArgs e)
    {
        string account = TbAccount.Text.Trim();
        string password = TbPassword.Text.Trim();

        if (account.Length > 0)
        {
            if (password.Length > 0)
            {
                var data = Function.GetDB().SystemUser.SingleOrDefault(n => n.Account == account && n.Del == false);

                if (data != null)
                {
                    if (Function.IsPwdValid(password, data.Password))
                    {
                        if (data.Enable)
                        {
                            Session["user_id"] = data.ID;
                            Session["role_id"] = Function.GetDB().SystemUser_Role.Single(n => n.SystemUserID == data.ID).RoleID;
                            Session["username"] = data.Name;
                            Function.InsertLog("Login", "", "", "執行動作：登入系統", data.ID);
                            FormsAuthentication.RedirectFromLoginPage(data.Name, false);
                        }
                        else
                        {
                            SA2Util.ShowMsg(this, "此帳號尚未啟用，請聯繫系統管理員。", SA2Util.PopupType.Warning);
                        }
                    }
                    else
                    {
                        SA2Util.ShowMsg(this, "密碼錯誤", SA2Util.PopupType.Error);
                    }
                }
                else
                {
                    SA2Util.ShowMsg(this, "查無資料", SA2Util.PopupType.Error);
                }
            }
            else
            {
                SA2Util.ShowMsg(this, "請輸入密碼", SA2Util.PopupType.Warning);
            }
        }
        else
        {
            SA2Util.ShowMsg(this, "請輸入帳號", SA2Util.PopupType.Warning);
        }
    }
}