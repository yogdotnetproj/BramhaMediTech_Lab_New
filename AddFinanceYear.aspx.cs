using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddFinanceYear :BasePage
{
    Expence_Bal_C ObjEBC = new Expence_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {       
        ObjEBC.ExpenceDate = Convert.ToDateTime(fromdate.Text);
        ObjEBC.ExpenceTo = Convert.ToDateTime(todate.Text);
        ObjEBC.Branchid = Convert.ToInt32(1);
        ObjEBC.UserName = Convert.ToString("Admin");
        ObjEBC.Insert_FinanceYear();
        Server.Transfer("Login.aspx");
    }
}