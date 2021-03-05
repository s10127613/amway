using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinqKit;

public partial class Admin_Customer : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageID = 4;
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        var pbSearch = PredicateBuilder.New<Customer>();

        if (RoleID != 1)
        {
            pbSearch = pbSearch.And(n => n.UserID == UserID);
        }

        if (DDLType.SelectedValue != "-1")
        {  
            pbSearch = pbSearch.And(n => n.Type == DDLType.SelectedValue);
        }

        if (!string.IsNullOrEmpty(TBNo.Text))
        {
            string No = TBNo.Text;
            pbSearch = pbSearch.And(n => n.No.Contains(No.Trim()));
        }

        if (DDLSource.SelectedValue != "-1")
        {
            pbSearch = pbSearch.And(n => n.Source == DDLSource.SelectedValue);
        }

        if (!string.IsNullOrEmpty(TBName.Text))
        {
            string Name = TBName.Text;
            pbSearch = pbSearch.And(n => n.Name.Contains(Name.Trim()));
        }

        pbSearch = pbSearch.And(n => n.Del == false);

        var vCustomer = db.Customer.Where(pbSearch);
        if (vCustomer.Count() > 0)
        {
            LVCustomer.DataSource = vCustomer.Select(n => new
            {
                ID = n.ID,
                No = n.No,
                Source = n.Source,
                Name = n.Name,
                SDate = n.SDate,
                Type = n.Type,
                Num = n.Num,
            }).ToList();
        }
        LVCustomer.DataBind();
    }

    protected void LVCustomer_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        LinkButton LBEdit = (LinkButton)e.Item.FindControl("LBEdit");
        LinkButton LBDel = (LinkButton)e.Item.FindControl("LBDel");

        Label LType = (Label)e.Item.FindControl("LType");
        Label LSource = (Label)e.Item.FindControl("LSource");
        Label LSDate = (Label)e.Item.FindControl("LSDate");
        Label LNo = (Label)e.Item.FindControl("LNo");

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            if (LType.Text == Convert.ToInt32(Type.直銷商).ToString())
            {
                LType.Text = Type.直銷商.ToString();
            }

            if (LType.Text == Convert.ToInt32(Type.生活會員).ToString())
            {
                LType.Text = Type.生活會員.ToString();
            }

            if (LType.Text == Convert.ToInt32(Type.尚未加入).ToString())
            {
                LType.Text = Type.尚未加入.ToString();
            }

            if (LSource.Text == Convert.ToInt32(Source.陌生開發).ToString())
            {
                LSource.Text = Source.陌生開發.ToString();
            }
            else
            {
                LSource.Text = Source.親友好友介紹.ToString();
            }

            if (!string.IsNullOrEmpty(LSDate.Text))
            {
                DateTime SDate = Convert.ToDateTime(LSDate.Text);
                LSDate.Text = SDate.ToShortDateString();
            }
            else
            {
                LSDate.Text = "(無)";
            }

            if (string.IsNullOrEmpty(LNo.Text))
            {
                LNo.Text = "(無)";
            }

            if (!CheckPagePrivilege(Function.Privlege.新增))
            {

            }

            if (!CheckPagePrivilege(Function.Privlege.修改))
            {

            }

            if (!CheckPagePrivilege(Function.Privlege.刪除))
            {

            }
        }
    }

    protected void LVCustomer_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
    {
        DataPager dataPager = this.LVCustomer.FindControl("DataPager1") as DataPager;
        int StartRowIndex = e.StartRowIndex;
        int MaximunRows = e.MaximumRows;
        dataPager.SetPageProperties(StartRowIndex, MaximunRows, false);

        BindData();
    }

    protected void LBEdit_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect($"Customer_Edit.aspx?CID={e.CommandArgument.ToString()}");
    }

    protected void LBDel_Command(object sender, CommandEventArgs e)
    {
        int ID = Convert.ToInt32(e.CommandArgument);

        var vCustomer = db.Customer.FirstOrDefault(n => n.Del == false && n.ID == ID);
        if (vCustomer != null)
        {
            vCustomer.Del = true;
            vCustomer.UDate = DateTime.Now;
            db.SubmitChanges();
        }
    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void Btn_Clear_Click(object sender, EventArgs e)
    {
        DDLSource.SelectedValue = "-1";
        DDLType.SelectedValue = "-1";
        TBName.Text = string.Empty;
        TBNo.Text = string.Empty;

        BindData();
    }
}