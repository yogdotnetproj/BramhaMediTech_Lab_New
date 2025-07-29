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
using System.Net;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class Referingdoctor :BasePage
{
    string Centercode = "", doctornew = "", doctor = "";
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    string[] doctorcode = new string[] { "", "" };
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("Referingdoctor.aspx");
                    }
                }
                if (Session["usertype"] != null && Session["username"] != null)
                {
                    string sess = Session["usertype"].ToString();
                    createuserTable_Bal_C CTB = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));

                }
                if (Session["usertype"] != null)
                {
                    if (Session["usertype"].ToString().Trim() == "CollectionCenter")
                    {
                        gridMain.DataSource = DrMT_sign_Bal_C.GetcenterDoctor(Label1.Text, hdnSort.Value, Convert.ToInt32(Session["Branchid"]));
                        gridMain.DataBind();
                        //Txtcenter.Visible = false;
                        // btnshow.Visible = false;
                        // T.Visible = false;
                        Txtcenter.Text = Convert.ToString(Session["CenterCode"]);

                    }
                    else
                    {
                        gridMain.DataSource = DrMT_sign_Bal_C.getAllDoctor(hdnSort.Value, Session["UnitCode"], Convert.ToInt32(Session["Branchid"]));
                        gridMain.DataBind();

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
    }

  
    protected void gridMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex == -1)
                return;


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
    protected void gridMain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string dcode = Convert.ToString(gridMain.Rows[e.RowIndex].Cells[1].Text);
            Patmst_Bal_C ObjPBC=new Patmst_Bal_C ();
            DrMT_Bal_C dr = new DrMT_Bal_C();
            dr.DoctorCode = dcode;
          bool DrRegiste= ObjPBC.getallreadyRegister_ReferenceDoctor(dcode);
          if (DrRegiste == true)
          {
              dr.Delete(Convert.ToInt32(Session["Branchid"]));
          }
            if (txtDoctorName.Text.Trim() != "")
            {
                try
                {
                    doctorcode = txtDoctorName.Text.Split('=');
                    doctornew = doctorcode[1];
                    doctor = doctornew.TrimStart();
                }
                catch (Exception dfg) { }
            }
            if (Session["usertype"].ToString().Trim() == "CollectionCenter")
            {
                gridMain.DataSource = DrMT_sign_Bal_C.GetcenterDoctor(Label1.Text, hdnSort.Value, Convert.ToInt32(Session["Branchid"]));
                gridMain.DataBind();
            }
            if (Session["usertype"].ToString().Trim() == "Administrator")
            {

                gridMain.DataSource = DrMT_sign_Bal_C.getAllDoctor(doctor, Session["UnitCode"], Convert.ToInt32(Session["Branchid"]));
                gridMain.DataBind();
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

    protected void gridMain_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gridMain_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gridMain_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {


    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (Txtcenter.Text.Trim() != "")
        {
            Centercode = Txtcenter.Text.Trim();
        }
        if (txtDoctorName.Text.Trim() != "")
        {

            try
            {
                doctorcode = txtDoctorName.Text.Split('=');
                doctornew = doctorcode[1];
                doctor = doctornew.TrimStart();
            }
            catch (Exception dfg) { }
        }

        if (Session["usertype"].ToString().Trim() == "CollectionCenter")
        {
            gridMain.DataSource = DrMT_sign_Bal_C.GetcenterDoctor(doctor, hdnSort.Value, Convert.ToInt32(Session["Branchid"]));
            gridMain.DataBind();
        }
        else
        {
            gridMain.DataSource = DrMT_sign_Bal_C.getAllDoctor(doctor, Session["UnitCode"], Convert.ToInt32(Session["Branchid"]));
            gridMain.DataBind();
        }

    }
    protected void gridMain_Sorting1(object sender, GridViewSortEventArgs e)
    {
        try
        {
            hdnSort.Value = e.SortExpression;

            if (hdnSort.Value != null)
            {
                if (Session["usertype"].ToString().Trim() == "CollectionCenter")
                {
                    gridMain.DataSource = DrMT_sign_Bal_C.GetcenterDoctor1(Label1.Text, hdnSort.Value, Convert.ToInt32(Session["Branchid"]));

                }
                if (Session["usertype"].ToString().Trim() == "Administrator")
                {
                    gridMain.DataSource = DrMT_sign_Bal_C.getAllDoctor(hdnSort.Value, Session["UnitCode"], Convert.ToInt32(Session["Branchid"]));

                }
            }
            else
            {
                if (Session["usertype"].ToString().Trim() == "CollectionCenter")
                {
                    gridMain.DataSource = DrMT_sign_Bal_C.GetcenterDoctor1(Label1.Text, "", Convert.ToInt32(Session["Branchid"]));

                }
                if (Session["usertype"].ToString().Trim() == "Administrator")
                {
                    gridMain.DataSource = DrMT_sign_Bal_C.getAllDoctor("", Session["UnitCode"], Convert.ToInt32(Session["Branchid"]));

                }

            }
            gridMain.DataBind();
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
    protected void gridMain_Sorted(object sender, EventArgs e)
    {

    }
    protected void gridMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridMain.PageIndex = e.NewPageIndex;
        if (txtDoctorName.Text.Trim() != "")
        {
            try
            {
                doctorcode = txtDoctorName.Text.Split('=');
                doctornew = doctorcode[1];
                doctor = doctornew.TrimStart();
            }
            catch (Exception dfg) { }
        }
        if (Session["usertype"] != null)
        {
            if (Session["usertype"].ToString().Trim() == "CollectionCenter")
            {
                gridMain.DataSource = DrMT_sign_Bal_C.GetcenterDoctor(Label1.Text, hdnSort.Value, Convert.ToInt32(Session["Branchid"]));
                gridMain.Columns[7].HeaderText = Session["CenterName"].ToString();
                gridMain.DataBind();
                Txtcenter.Visible = false;
                btnshow.Visible = false;
                Label1.Visible = false;

            }
            else
            {

                gridMain.DataSource = DrMT_sign_Bal_C.getAllDoctor(doctor, Session["UnitCode"], Convert.ToInt32(Session["Branchid"]));
                gridMain.Columns[7].HeaderText = Session["CenterName"].ToString();
                gridMain.DataBind();
            }
        }
    }
    protected void Txtcenter_TextChanged(object sender, EventArgs e)
    {
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] GetCenter(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT DoctorCode,DoctorName FROM DrMT where DoctorName like '" + prefixText + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT DoctorCode,DoctorName FROM DrMT where DoctorName like '" + prefixText + "%' and DrTpey='CC' and branchid=" + branchid + " order by DoctorName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["DoctorName"], i);
            i++;
        }

        return tests;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillDoctor(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;

        if (HttpContext.Current.Session["usertype"].ToString() == "CollectionCenter")
        {
            sda = new SqlDataAdapter("SELECT DoctorCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR'  and DoctorName like  N'" + prefixText + "%' order by DoctorName", con);//and DoctorCode='" + HttpContext.Current.Session["CenterCode"].ToString() + "'
        }
        else
        {
            sda = new SqlDataAdapter("SELECT DoctorCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR' and DoctorName like  N'" + prefixText + "%' or DoctorCode like '" + prefixText + "%' order by DoctorName", con);
        }

        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["name"] + " = " + dr["DoctorCode"], i);
            i++;
        }

        return tests;
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        //Response.Redirect("AddReferingDoctor.aspx");
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1250/2);var Mtop = (screen.height/2)-(700/2);window.open( 'AddReferingDoctor.aspx ', null, 'height=700,width=1250,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);



    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Reference  doctor" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gridMain.GridLines = GridLines.Both;
        gridMain.HeaderStyle.Font.Bold = true;
        gridMain.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }

    protected void gridMain_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Refering Doctor";
            // HeaderCell.Font = System.Drawing.Color.Black;

            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            gridMain.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
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