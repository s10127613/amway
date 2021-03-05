using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int id = 0;
            if (Session["user_id"] != null)
            {
                id = Convert.ToInt32(Session["user_id"]);
            }
            Function.InsertLog("Logout", "", "", "執行動作：登出系統", id);
            Function.Logout(this);
        }
    }
}