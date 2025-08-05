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

public partial class AddTestRate :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DrMT_sign_Bal_C obj = new DrMT_sign_Bal_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.SetFocus(ddlRate);
        if (!Uniquemethod_Bal_C.isSessionSet())
        {
            string ss = System.Configuration.ConfigurationManager.AppSettings["LogOutURL"].Trim();
            Response.Redirect(ss);
        }

        if (!Page.IsPostBack)
        {

            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("AddTestRate.aspx");
                    }
                }
                obj.flag_type = 'R';
                ddlRate.DataSource = DrMT_sign_Bal_C.getSelectratemaster(Convert.ToInt32(Session["Branchid"]), obj.flag_type);
                ddlRate.DataTextField = "RateName";
                ddlRate.DataValueField = "RatID";
                ddlRate.DataBind();
                ddlRate.Items.Insert(0, "Select Rate Type");
                ddlRate.SelectedIndex = -1;

                Ddlsubdept.DataSource = SubdepartmentLogic_Bal_C.getSubDepartment(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]));
                Ddlsubdept.DataTextField = "subdeptName";
                Ddlsubdept.DataValueField = "SDCode";
                Ddlsubdept.DataBind();
                Ddlsubdept.Items.Insert(0, "Select Department");
                Ddlsubdept.SelectedIndex = -1;
                btnsave.Visible = false;
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

    protected void Ddlsubdept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void RateGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void RateGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            RateGrid.PageIndex = e.NewPageIndex;
            int selrate = Convert.ToInt32(ddlRate.SelectedValue);
            string SDCode = Ddlsubdept.SelectedValue;
            RateGrid.DataSource = MainTestLog_Bal_C.getGrideValue(SDCode, Convert.ToString(selrate), Convert.ToInt32(Session["Branchid"]));
            RateGrid.DataBind();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string STCODE = "";
            if (Ddlsubdept.SelectedItem != null && ddlRate.SelectedItem != null)
            {


                foreach (GridViewRow row in RateGrid.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        STCODE = RateGrid.DataKeys[row.RowIndex].Value.ToString();

                    }
                }

                try
                {
                    SpeCh_Bal_C sft = new SpeCh_Bal_C(STCODE, ddlRate.SelectedValue, Convert.ToInt32(Session["Branchid"]));

                    sft.STCODE = STCODE;
                    //  sft.TestName = RateGrid.Rows[i].Cells[2].Text;
                    sft.DrCode = ddlRate.SelectedValue;
                    sft.DrName = ddlRate.SelectedItem.Text;
                    sft.P_username = Convert.ToString(Session["username"]);
                    int amt = Convert.ToInt32((RateGrid.Rows[0].Cells[3].FindControl("txtamount") as TextBox).Text);
                    sft.Amount = amt;
                    sft.Patregdate = Date.getOnlydate();
                    try
                    {
                        sft.InsertORUpdate(Convert.ToInt32(Session["Branchid"]));
                        Label4.Text = "Record updated successfully";

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
                catch
                {
                    SpeCh_Bal_C sft = new SpeCh_Bal_C();
                    sft.STCODE = STCODE;
                    sft.TestName = RateGrid.Rows[0].Cells[2].Text;
                    sft.DrCode = ddlRate.SelectedValue;
                    sft.DrName = ddlRate.SelectedItem.Text;
                    int amt = Convert.ToInt32((RateGrid.Rows[0].Cells[3].FindControl("txtamount") as TextBox).Text);
                    sft.Amount = amt;
                    sft.P_username = Convert.ToString(Session["username"]);
                    try
                    {
                        if (amt == 0)
                        {

                        }
                        else
                        {
                            sft.Insert(Convert.ToInt32(Session["Branchid"]));
                            Label4.Text = "Record inserted successfully";
                        }

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Ddlsubdept.SelectedValue != "Select Department")
            {
                if (txttestname.Text == "")
                {
                    int selrate = Convert.ToInt32(ddlRate.SelectedValue);
                    string SDCode = Ddlsubdept.SelectedValue;
                    RateGrid.DataSource = MainTestLog_Bal_C.getGrideValue(SDCode, Convert.ToString(selrate), Convert.ToInt32(Session["Branchid"]));
                    RateGrid.DataBind();
                    btnsave.Visible = true;
                }
                else
                {
                    int selrate = Convert.ToInt32(ddlRate.SelectedValue);
                    string SDCode = Ddlsubdept.SelectedValue;
                    RateGrid.DataSource = MainTestLog_Bal_C.getGrideValue_Testname(Convert.ToString(ViewState["MTCODE"]), Convert.ToString(selrate), Convert.ToInt32(Session["Branchid"]));
                    RateGrid.DataBind();
                    btnsave.Visible = true;
                }
            }
            // ExportGridToExcel();
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

    protected void RateGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SpeCh_Bal_C SP_BC = null;
            if (Ddlsubdept.SelectedItem.Text != null && ddlRate.SelectedItem.Text != null)
            {
                SP_BC = new SpeCh_Bal_C();
                SP_BC.STCODE = RateGrid.Rows[e.RowIndex].Cells[1].Text;
                SP_BC.DrCode = ddlRate.SelectedValue;
                SP_BC.Delete(Convert.ToInt32(Session["Branchid"]));
                int selrate = Convert.ToInt32(ddlRate.SelectedValue);
                string SDCode = Ddlsubdept.SelectedValue;
                RateGrid.DataSource = MainTestLog_Bal_C.getGrideValue(SDCode, Convert.ToString(selrate), Session["Branchid"]);
                RateGrid.DataBind();
            }
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
    [WebMethod]
    [ScriptMethod]
    public static string[] GetTestname(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;

        if (HttpContext.Current.Session["usertype"].ToString() == "CollectionCenter")
        {
            sda = new SqlDataAdapter("select Maintestname,MTCode from MainTest where Maintestname  like '%" + prefixText + "%'  union all select PackageName as Maintestname,PackageCode as MTCode from PackMst where PackageName like '%" + prefixText + "%' ", con);

        }
        else
        {
            sda = new SqlDataAdapter("select Maintestname,MTCode from MainTest where Maintestname  like '%" + prefixText + "%'  union all select PackageName as Maintestname,PackageCode as MTCode from PackMst where PackageName like '%" + prefixText + "%' ", con);

        }

        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["MTCode"] + " = " + dr["Maintestname"], i);
            i++;
        }

        return tests;
    }
    protected void txttestname_TextChanged(object sender, EventArgs e)
    {
        if (txttestname.Text != "")
        {
            try
            {
                string MTCode = "";
                string[] MTCodeT = new string[] { "", "" };
                MTCodeT = txttestname.Text.Split('=');
                if (MTCodeT.Length > 1)
                {
                    MTCode = MTCodeT[0];
                    ViewState["MTCODE"] = MTCode.Trim();

                    try
                    {
                        int selrate = 0;
                        if (ddlRate.SelectedValue == "Select Rate Type")
                        {
                            selrate = 0;
                        }
                        else
                        {
                            selrate = Convert.ToInt32(ddlRate.SelectedValue);
                        }
                        string SDCode = "0";
                        if (Ddlsubdept.SelectedValue == "Select Department")
                        {
                            SDCode = "0";
                        }
                        else
                        {
                             SDCode = Ddlsubdept.SelectedValue;
                        }
                        RateGrid.DataSource = MainTestLog_Bal_C.getGrideValue_Testname(Convert.ToString(ViewState["MTCODE"]), Convert.ToString(selrate), Convert.ToInt32(Session["Branchid"]));
                        RateGrid.DataBind();
                        btnsave.Visible = true;

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
                else
                {

                }
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
    protected void RateGrid_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Test Rate";
            // HeaderCell.Font = System.Drawing.Color.Black;

            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            RateGrid.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "RateType" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        RateGrid.GridLines = GridLines.Both;
        RateGrid.HeaderStyle.Font.Bold = true;
        RateGrid.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    public void checkexistpageright(string PageName)
    {

        string MenuSQL = "";
        DataTable MenuDt = new DataTable();
        MenuSQL = String.Format(@"SELECT        Roleright.Rightid, Roleright.Usertypeid, Roleright.FormId, Roleright.FormName, Roleright.Branchid, usr.ROLENAME, " +
              "  TBL_SubMenuMaster.SubMenuNavigateURL, TBL_MenuMaster.MenuName, TBL_MenuMaster.MenuID,   TBL_SubMenuMaster.SubMenuName, TBL_MenuMaster.Icon, " +
              "  TBL_SubMenuMaster.SubMenuID   " +
              "  FROM            Roleright INNER JOIN   usr ON Roleright.Usertypeid = usr.ROLLID AND Roleright.Branchid = usr.branchid INNER JOIN   " +
              "  TBL_SubMenuMaster ON Roleright.FormId = TBL_SubMenuMaster.SubMenuID INNER JOIN   TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID INNER JOIN  " +
              "  CTuser ON Roleright.Usertypeid = CTuser.Rollid  where (CTuser.USERNAME = '" + Convert.ToString(Session["username"]) + "') AND (CTuser.password = '" + Convert.ToString(Session["password"]) + "') and  TBL_SubMenuMaster.Isvisable=1  and TBL_SubMenuMaster.SubMenuNavigateURL='" + PageName + "'  " +
                               " order by MenuID  ");



        string connectionString1 = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString1);

        SqlCommand cmd = new SqlCommand(MenuSQL, con);

        SqlDataAdapter Adp = new SqlDataAdapter(cmd);

        Adp.Fill(MenuDt);
        if (MenuDt.Rows.Count == 0)
        {
            Response.Redirect("Login.aspx", false);
        }
        con.Close();
        con.Dispose();

    }

}