using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinqKit;
using System.IO;
using System.Net;
using System.Text;

public partial class Admin_index : PageBase
{
    protected void Page_Init(object sender, EventArgs e)
    {
        PageID = 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
}