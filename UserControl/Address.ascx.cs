using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Address : System.Web.UI.UserControl
{
    bool set = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !set)
        {
            DefaultSelect();
        }
    }

    protected void SetAddress(int level)
    {
        var db = Function.GetDB();

        if (level >= 3)
        {
            DDL_Add1.Items.Clear();
            DDL_Add1.Items.Add(new ListItem("請選擇…", "-2"));
            var a = db.Address.Where(c => c.Del == false && c.Parent == 0).ToList();
            DDL_Add1.DataSource = a;
            DDL_Add1.DataBind();
        }

        if (level >= 2)
        {
            DDL_Add2.Items.Clear();
            DDL_Add2.Items.Add(new ListItem("請選擇…", "-2"));
            var b = db.Address.Where(c => c.Del == false && c.Parent.ToString() == DDL_Add1.SelectedValue).OrderByDescending(c => c.ID).ToList();
            DDL_Add2.DataSource = b;
            DDL_Add2.DataBind();
        }

        if (level >= 1)
        {
            DDL_Add3.Items.Clear();
            DDL_Add3.Items.Add(new ListItem("請選擇…", "-2"));
            var d = db.Address.Where(c => c.Del == false && c.Parent.ToString() == DDL_Add2.SelectedValue).OrderBy(n => n.Name).ToList();
            DDL_Add3.DataSource = d;
            DDL_Add3.DataBind();

            var dd = db.Address.Where(c => c.Del == false && c.ID.ToString() == DDL_Add2.SelectedValue).ToList();
            foreach (var data in dd)
            {
                AreaNum.Text = data.ZipNum.ToString();
            }
        }
    }

    public void BindAddress(int add1, int add2, int add3)
    {
        SetAddress(3);
        DDL_Add1.SelectedValue = add1.ToString();

        SetAddress(2);
        DDL_Add2.SelectedValue = add2.ToString();

        SetAddress(1);
        DDL_Add3.SelectedValue = add3.ToString();

        set = true;
    }

    private void DefaultSelect()
    {
        SetAddress(3);
        DDL_Add1.SelectedValue = "4822";
        SetAddress(2);
        DDL_Add2.SelectedValue = "5241";
        SetAddress(1);
    }


    public string PostCode
    {
        set
        {
            AreaNum.Text = value.ToString();
        }
        get
        {
            return AreaNum.Text;
        }
    }

    public int Add1
    {
        set
        {
            DDL_Add1.SelectedValue = value.ToString();
            SetAddress(3);
        }
        get
        {
            return Convert.ToInt32(DDL_Add1.SelectedValue);
        }
    }

    public int Add2
    {
        set
        {
            DDL_Add2.SelectedValue = value.ToString();
            SetAddress(2);
        }
        get
        {
            return Convert.ToInt32(DDL_Add2.SelectedValue);
        }
    }

    public int Add3
    {
        set
        {
            DDL_Add3.SelectedValue = value.ToString();
        }
        get
        {
            return Convert.ToInt32(DDL_Add3.SelectedValue);
        }
    }

    public string Add4
    {
        set
        {
            if (!string.IsNullOrEmpty(value))
                TB_Add4.Text = value.ToString();
        }
        get
        {
            return TB_Add4.Text;
        }
    }

    protected void DDL_Add1_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetAddress(2);
    }

    protected void DDL_Add2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetAddress(1);
    }

    public void SetEnable(bool e)
    {
        DDL_Add1.Enabled = false;
        DDL_Add2.Enabled = false;
        DDL_Add3.Enabled = false;
    }
}