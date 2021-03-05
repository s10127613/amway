using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinqKit;

public partial class Admin_Customer_Edit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PagePeer = "Customer.aspx";
        PageID = 4;

        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void SetData()
    {
        if (Request["ID"] != null)
        {
            int ID = Convert.ToInt32(Request["ID"]);
            var vCustomer = db.Customer.FirstOrDefault(n => n.Del == false && n.ID == ID && n.UserID == UserID);
            if (vCustomer != null)
            {
                //來源
                vCustomer.Source = DDLSource.SelectedValue;
                //類型
                vCustomer.Type = DDLType.SelectedValue;
                //編號
                vCustomer.No = TBNo.Text;
                //加入日期
                vCustomer.SDate = Convert.ToDateTime(TBDate.Text);
                //姓名
                vCustomer.Name = TBName.Text;
                //性別
                vCustomer.Gender = DDLGender.SelectedValue;
                //縣/市
                vCustomer.Add1 = app_Address.Add1.ToString();
                //鄉/鎮
                vCustomer.Add2 = app_Address.Add2.ToString();
                //村/里
                vCustomer.Add3 = app_Address.Add3.ToString();
                //地址
                vCustomer.Add4 = app_Address.Add4;
                //連絡電話(1)
                vCustomer.Tel = TBTel.Text;
                //連絡電話(2)
                vCustomer.Tel2 = TBTel2.Text;
                //電子郵件
                vCustomer.Email = TBEmail.Text;
                //備註
                vCustomer.Remark = TBRemark.Text;
                //更新時間
                vCustomer.UDate = DateTime.Now;

                db.SubmitChanges();

                Function.MsgBoxRedirect(this.Page, "顧客資料儲存成功。", "Customer.aspx");
            }
            else
            {
                Function.MsgBox(this.Page, "您無權限可以訪問資料");
            }
        }
        else
        {
            Customer vCustomer = new Customer();

            int num = Convert.ToInt32(db.Customer.Where(n => n.UserID == UserID && n.Del == false).Max(n => n.Num));

            if (num == 0)
            {
                num = 1;
            }
            else
            {
                num++;
            }

            //累進值
            vCustomer.Num = num;
            //來源
            vCustomer.Source = DDLSource.SelectedValue;
            //類型
            vCustomer.Type = DDLType.SelectedValue;
            //編號
            vCustomer.No = TBNo.Text;
            //加入日期
            if (!string.IsNullOrEmpty(TBDate.Text))
            {
                vCustomer.SDate = Convert.ToDateTime(TBDate.Text);
            }
            //姓名
            vCustomer.Name = TBName.Text;
            //性別
            vCustomer.Gender = DDLGender.SelectedValue;
            //縣/市
            vCustomer.Add1 = app_Address.Add1.ToString();
            //鄉/鎮
            vCustomer.Add2 = app_Address.Add2.ToString();
            //村/里
            vCustomer.Add3 = app_Address.Add3.ToString();
            //地址
            vCustomer.Add4 = app_Address.Add4;
            //連絡電話(1)
            vCustomer.Tel = TBTel.Text;
            //連絡電話(2)
            vCustomer.Tel2 = TBTel2.Text;
            //電子郵件
            vCustomer.Email = TBEmail.Text;
            //備註
            vCustomer.Remark = TBRemark.Text;
            //建立日期
            vCustomer.CDate = DateTime.Now;
            //使用者ID
            vCustomer.UserID = UserID;

            db.Customer.InsertOnSubmit(vCustomer);

            db.SubmitChanges();

            Function.MsgBoxRedirect(this.Page, "顧客資料新增成功。", "Customer.aspx");
        }
    }

    private void BindData()
    {
        if (Request["CID"] != null)
        {
            LMode.Text = "(編輯模式)";

            int ID = Convert.ToInt32(Request["CID"]);

            var pbSearch = PredicateBuilder.New<Customer>();

            if (RoleID != 1)
            {
                pbSearch = pbSearch.And(n => n.UserID == UserID);
            }

            pbSearch = pbSearch.And(n => n.ID == ID && n.Del == false);

            var vInvoice = db.Invoice.Where(n => n.Del == false && n.CustomerID == ID);
            if (vInvoice.Count() > 0)
            {
                LVInvoice.DataSource = vInvoice.Select(n => new
                {
                    n.IDate,
                    n.No,
                    n.Description,
                    n.ID,
                }).ToList();
            }
            LVInvoice.DataBind();

            var vCustomer = db.Customer.FirstOrDefault(pbSearch);
            if (vCustomer != null)
            {
                //來源
                DDLSource.SelectedValue = vCustomer.Source;
                //類型
                DDLType.SelectedValue = vCustomer.Type;
                //編號
                TBNo.Text = vCustomer.No;
                //加入日期
                TBDate.Text = string.Format("{0:yyyy-MM-dd}", vCustomer.SDate);
                //姓名
                TBName.Text = vCustomer.Name;
                //性別
                DDLGender.SelectedValue = vCustomer.Gender;
                //地址
                app_Address.BindAddress(Convert.ToInt32(vCustomer.Add1), Convert.ToInt32(vCustomer.Add2), Convert.ToInt32(vCustomer.Add3));
                app_Address.Add4 = vCustomer.Add4;
                //連絡電話(1)
                TBTel.Text = vCustomer.Tel;
                //連絡電話(2)
                TBTel2.Text = vCustomer.Tel2;
                //電子郵件
                TBEmail.Text = vCustomer.Email;
                //備註
                TBRemark.Text = vCustomer.Remark;
            }
            else
            {
                Function.MsgBoxRedirect(this.Page, "您沒有訪問的權限", "Customer.aspx");
            }
        }
    }

    protected void LBEdit_Command(object sender, CommandEventArgs e)
    {
        if (Request["CID"] != null)
        {
            int IID = Convert.ToInt32(e.CommandArgument);
            int CID = Convert.ToInt32(Request["CID"]);
            Response.Redirect($"~/Admin/Invoice.aspx?CID={CID}&IID={IID}");
        }
    }

    protected void LBDel_Command(object sender, CommandEventArgs e)
    {

    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        SetData();
    }

    protected void LVInvoice_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        Label LGrandTotal = (Label)e.Item.FindControl("LGrandTotal");

        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            if (LGrandTotal != null)
            {
                int ID = Convert.ToInt32(LGrandTotal.Text);

                var vItem = db.Invoice_Item.Where(n => n.Del == false && n.InvoiceID == ID);
                if (vItem.Count() > 0)
                {
                    int SUM = 0;
                    foreach (var Item in vItem)
                    {
                        SUM += Convert.ToInt32(Item.Quantity) * Convert.ToInt32(Item.UnitCost);
                    }
                    LGrandTotal.Text = SUM.ToString();
                }
                else
                {
                    LGrandTotal.Text = "(無)";
                }
            }
        }
    }
}