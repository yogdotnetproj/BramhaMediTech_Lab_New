using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;

public partial class EditPatientDetails : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Patmstd_Bal_C ObjPBC = new Patmstd_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {

       
        if (!IsPostBack)
        {
            try
            {
               
                LUNAME.Text = Convert.ToString(Session["username"]);
                LblDCName.Text = Convert.ToString(Session["Bannername"]);
                LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                dt = new DataTable();
                dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                this.PopulateTreeView(dt, 0, null);
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
            try
            {

                
                BindGrid();
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
    private void PopulateTreeView(DataTable dtparent, int Parentid, TreeNode treeNode)
    {
        foreach (DataRow row in dtparent.Rows)
        {
            TreeNode child = new TreeNode
            {
                Text = row["MenuName"].ToString(),
                Value = row["MenuID"].ToString()

            };
            if (Parentid == 0)
            {
                TrMenu.Nodes.Add(child);
                DataTable dtchild = new DataTable();
                dtchild = ObjTB.BindChildMenu(child.Value, Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                PopulateTreeView(dtchild, int.Parse(child.Value), child);

            }
            else
            {
                treeNode.ChildNodes.Add(child);
            }

        }
    }


    protected void TrMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        int tId = Convert.ToInt32(TrMenu.SelectedValue);
        DataTable dtform = new DataTable();
        dtform = ObjTB.BindForm(tId);
        if (dtform.Rows.Count > 0)
        {
            Response.Redirect(dtform.Rows[0]["SubMenuNavigateURL"].ToString());
        }
    }
    void BindGrid()
    {
        try
        {
            object fromDate = null;
            object Todate = null;

                RateGrid.DataSource = TreeviewBind_C.PatientTestDelete(Convert.ToInt32(Request.QueryString["PID"]));
                RateGrid.DataBind();
                
           // }

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

    protected void RateGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void RateGrid_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void RateGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            try
            {
                bool TestDeActive = Convert.ToBoolean((e.Row.FindControl("hdn_TestDeActive") as HiddenField).Value);
                CheckBox chkAuto = (e.Row.FindControl("CheckBox1") as CheckBox);
                Button btnAuto = (e.Row.FindControl("btncancel") as Button);
                if (TestDeActive == true)
                {
                    chkAuto.Enabled = false;
                    btnAuto.Enabled = false;
                    //e.Row.Cells[12].Text= System.Drawing.Color.Red;

                    //e.Row.ForeColor = System.Drawing.Color.White;
                    // e.Row.BackColor = System.Drawing.Color.Orange;
                    e.Row.Cells[03].Text = "<span class='btn btn-xs btn-warning' >Canele </span>";
                   // e.Row.Cells[01].Enabled = false;
                   // e.Row.Cells[0].Enabled = false;
                }
                else
                {
                    chkAuto.Enabled = true;
                    btnAuto.Enabled = true; ;
                    e.Row.Cells[03].Text = "<span class='btn btn-xs btn-success' >Active</span>";
                }
            }
            catch (Exception ex)
            { }
        }
    }
    protected void RateGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void chkAction_CheckedChanged(object sender, EventArgs e)
    {
        LblAmt.Text = "0";
        for (int i = 0; i < RateGrid.Rows.Count; i++)
        {
            CheckBox chkAuto = (RateGrid.Rows[i].FindControl("CheckBox1") as CheckBox);
            TextBox Txt_amt = (RateGrid.Rows[i].FindControl("txtamount") as TextBox);
            if (chkAuto.Checked == true)
            {
                LblAmt.Text = Convert.ToString(Convert.ToSingle(LblAmt.Text) + Convert.ToSingle(Txt_amt.Text));
            }

        }
    }
  
    protected void btncancel_Click(object sender, EventArgs e)
    {
        LblAmt.Text = "0";
        for (int i = 0; i < RateGrid.Rows.Count; i++)
        {
            CheckBox chkAuto = (RateGrid.Rows[i].FindControl("CheckBox1") as CheckBox);
            TextBox Txt_amt = (RateGrid.Rows[i].FindControl("txtamount") as TextBox);
            //if (chkAuto.Checked == true)
            //{
            //    LblAmt.Text = Convert.ToString(Convert.ToSingle(LblAmt.Text) + Convert.ToSingle(Txt_amt.Text));
            //}
            string RegNo = RateGrid.Rows[i].Cells[0].Text;
            string TestCode = RateGrid.Rows[i].Cells[2].Text;
            string TestName = RateGrid.Rows[i].Cells[3].Text;

            string username = Convert.ToString(Session["username"]);
            int amt = Convert.ToInt32((RateGrid.Rows[i].Cells[5].FindControl("txtamount") as TextBox).Text);
            CheckBox Chk_Action = RateGrid.Rows[i].Cells[5].FindControl("CheckBox1") as CheckBox;
            string RNo = (RateGrid.Rows[i].FindControl("hdnRegNo") as HiddenField).Value;
            string PID = (RateGrid.Rows[i].FindControl("hdnPID") as HiddenField).Value;
            string MTCode = (RateGrid.Rows[i].FindControl("hdnMTCode") as HiddenField).Value;
            if (Chk_Action.Checked == true)
            {
                ObjPBC.Username = Convert.ToString(Session["username"]);
                ObjPBC.PatRegID = RNo;
                ObjPBC.MTCode = MTCode;
                ObjPBC.PID = Convert.ToInt32(PID);
                ObjPBC.DisactiveTest(1);
                
                //ObjPBC.AlterView_Defaulte(Convert.ToInt32(PID));
                //ObjPBC.Delete_Default(Convert.ToInt32(PID), 1, MTCode);
                //ObjPBC.Insert_Update_ForPmstDefault(1);
            }
        }
        BindGrid();
    }
}