using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
public partial class Testformulashow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            try
            {
                Label1.Visible = false;
                GridView1.AutoGenerateColumns = false;
                GridView1.DataSource = TestFormulaLogic_Bal_c.getAllFormulaTbl1TableValues(Session["Branchid"], Convert.ToInt32(Session["DigModule"]));
                GridView1.DataBind();
            }
            catch (Exception exc)
            {
                if (exc.Message.Equals("Exception aborted."))
                {
                    return;
                }
                else
                {
                    Response.Cookies["error"].Value = exc.Message;
                    Server.Transfer("~/ErrorMessage.aspx");
                }
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = Convert.ToString(GridView1.Rows[e.RowIndex].Cells[3].Text);
            CalculateSet_Bal_C Obj_CBC_C = new CalculateSet_Bal_C();
            Obj_CBC_C.ID = Convert.ToInt32(GridView1.Rows[e.RowIndex].Cells[3].Text); ;
            Obj_CBC_C.Delete(Convert.ToInt32(Session["Branchid"]));
            GridView1.DataSource = TestFormulaLogic_Bal_c.getAllFormulaTbl1TableValues(Session["Branchid"], Convert.ToInt32(Session["DigModule"]));
            GridView1.DataBind();
            Label1.Visible = true;
            Label1.Text = "Record deleted successfully";
        }
        catch (Exception exc)
        {
            if (exc.Message.Equals("Exception aborted."))
            {
                return;
            }
            else
            {
                Response.Cookies["error"].Value = exc.Message;
                Server.Transfer("~/ErrorMessage.aspx");
            }
        }

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;
    
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = TestFormulaLogic_Bal_c.getAllFormulaTbl1TableValues(Session["Branchid"], Convert.ToInt32(Session["DigModule"]));
        GridView1.DataBind();
    }
}