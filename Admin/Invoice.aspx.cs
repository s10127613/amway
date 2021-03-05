using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Default : PageBase
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

    private void BindData()
    {
        if (Request["CID"] != null)
        {
            int InvoiceID = Convert.ToInt32(Request["IID"]);

            var vInvoice = db.Invoice.FirstOrDefault(n => n.Del == false && n.ID == InvoiceID);
            if (vInvoice != null)
            {
                //發票編號
                TBNo.Text = vInvoice.No;
                //發票日期
                TBIDate.Text = string.Format("{0:yyyy-MM-dd}", vInvoice.IDate);
                //發票描述
                TBDescription.Text = vInvoice.Description;

                var vItem = db.Invoice_Item.Where(n => n.Del == false && n.InvoiceID == InvoiceID);
                if (vItem.Count() > 0)
                {
                    LVItem.DataSource = vItem.Select(n => new
                    {
                        n.ID,
                        n.Name,
                        n.Quantity,
                        n.UnitCost,
                    }).ToList();

                    Label LGrandTotal = LVItem.FindControl("LGrandTotal") as Label;
                    if (LGrandTotal != null)
                    {
                        int Sum = 0, Quantity = 0, UnitCost = 0;
                        foreach (var Item in vItem)
                        {
                            Quantity = Convert.ToInt32(Item.Quantity);
                            UnitCost = Convert.ToInt32(Item.UnitCost);
                            Sum += Quantity * UnitCost;
                        }
                        LGrandTotal.Text = Sum.ToString();
                    }
                }
                LVItem.DataBind();
            }
        }
        else
        {
            Response.Redirect("Customer.aspx");
        }
    }

    private void SetData()
    {
        if (Request["IID"] != null)
        {
            int InvoiceID = Convert.ToInt32(Request["IID"]);

            var vInvoice = db.Invoice.FirstOrDefault(n => n.Del == false && n.ID == InvoiceID);
            if (vInvoice != null)
            {               
                vInvoice.IDate = Convert.ToDateTime(TBIDate.Text);
                vInvoice.Description = TBDescription.Text;
                vInvoice.UDate = DateTime.Now;
            }
        }
    }

    private void InsertData()
    {
        string Name = (LVItem.FindControl("TBName") as TextBox).Text;
        string Quantity = (LVItem.FindControl("TBQuantity") as TextBox).Text;
        string UnitCost = (LVItem.FindControl("TBUnitCost") as TextBox).Text;

        if (Request["IID"] != null)
        {
            int IID = Convert.ToInt32(Request["IID"]);

            Invoice_Item invoice_Item = new Invoice_Item()
            {
                InvoiceID = IID,
                Name = Name,
                Quantity = Convert.ToInt32(Quantity),
                UnitCost = Convert.ToInt32(UnitCost),
                Del = false,
            };

            db.Invoice_Item.InsertOnSubmit(invoice_Item);
            db.SubmitChanges();

            BindData();
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        if (Request["CID"] != null)
        {
            int CID = Convert.ToInt32(Request["CID"]);
            int InvoiceID = Convert.ToInt32(Request["IID"]);

            var vInvoice = db.Invoice.FirstOrDefault(n => n.Del == false && n.ID == InvoiceID);
            if (vInvoice != null)
            {
                Response.Redirect($"Customer_Edit.aspx?CID={CID}");
            }
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

    }

    protected void LBDel_Command(object sender, CommandEventArgs e)
    {

    }

    protected void LBEdit_Command(object sender, CommandEventArgs e)
    {

    }


    protected void LVItem_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }

    protected void LVItem_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        LVItem.EditIndex = e.NewEditIndex;
        BindData();
    }

    protected void LVItem_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        LVItem.EditIndex = -1;
        BindData();
    }

    protected void LVItem_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        string Name = (LVItem.Items[e.ItemIndex].FindControl("TBName") as TextBox).Text;
        string Quantity = (LVItem.Items[e.ItemIndex].FindControl("TBQuantity") as TextBox).Text;
        string UnitCost = (LVItem.Items[e.ItemIndex].FindControl("TBUnitCost") as TextBox).Text;
        string ID = (LVItem.Items[e.ItemIndex].FindControl("LBtnUpdate") as LinkButton).CommandArgument;

        var vItem = db.Invoice_Item.FirstOrDefault(n => n.Del == false && n.ID.ToString() == ID);
        if (vItem != null)
        {
            vItem.Name = Name;
            vItem.Quantity = Convert.ToInt32(Quantity);
            vItem.UnitCost = Convert.ToInt32(UnitCost);
            db.SubmitChanges();
        }

        LVItem.EditIndex = -1;
        BindData();
    }

    protected void LVItem_ItemDeleting(object sender, ListViewDeleteEventArgs e)
    {
        int ID = Convert.ToInt32((LVItem.Items[e.ItemIndex].FindControl("LBtnDel") as LinkButton).CommandArgument);
        var vItem = db.Invoice_Item.FirstOrDefault(n => n.Del == false && n.ID == ID);
        if (vItem != null)
        {
            vItem.Del = true;
            db.SubmitChanges();
        }
        LVItem.EditIndex = -1;
        BindData();
    }

    protected void LBtnInsert_Click(object sender, EventArgs e)
    {
        InsertData();
    }
}