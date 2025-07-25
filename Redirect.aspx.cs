  using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Redirect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Uniquemethod_Bal_C.isSessionSet())
        {
            string ss = System.Configuration.ConfigurationManager.AppSettings["LogOutURL"].Trim();
            Response.Redirect(ss);

        }
        string RepName = Request.QueryString["RepName"].ToString();
        Response.Redirect(RepName);
    }
}