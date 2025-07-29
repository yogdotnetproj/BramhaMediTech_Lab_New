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

public partial class RoutinTest :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.SetFocus(txtRoutinTest);

        if (!IsPostBack)
        {
            try
            {               
                Label2.Visible = false;
                bindgrid();

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

    private void bindgrid()
    {
        try
        {
            RoutinTest_Grid.DataSource = RoutinTest_C.getRoutinTest(Convert.ToInt32(Session["Branchid"]));
            RoutinTest_Grid.DataBind();
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        txtRoutinTest.Text = "";
        Label2.Visible = false;
        ttttt();
    }
    public void ttttt()
    {
        string xxx = "<span style=" + "font-weight: bold" + ">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Testing Purpose<br />this is testing purpose report please check itName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ValueABC&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; 1PQR &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; 2&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>";
        string filepath = Server.MapPath("XMLFile//" + 1 + ".html");
        System.IO.File.WriteAllText(filepath, xxx);
        Response.ContentType = "text/plain";
        Response.AddHeader("Content-Disposition", "attachment:filename=" + filepath + ";");
        Response.TransmitFile(filepath);
        Response.Flush();
        FileInfo fi = new FileInfo(filepath);
        fi.Delete();
        Response.End();
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTests(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
            sda = new SqlDataAdapter("select PackageCode as MTCode,PackageName as Maintestname,'P' as Tes from PackMst where (PackageCode like '%" + prefixText + "%' or PackageName like '%" + prefixText + "%') and PackageCode in (select PackageCode from PackmstD where SDCode in (select SDCode from SubDepartment where DigModule ='" + Convert.ToInt32(HttpContext.Current.Session["DigModule"]) + "')) UNION " +
                               " select MTCode, Maintestname, 'T' as Tes from MainTest WHERE ISTestActive=1 and (MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%') and SDCode in (select SDCode from SubDepartment where DigModule='" + Convert.ToInt32(HttpContext.Current.Session["DigModule"]) + "') order by Maintestname ", con);
        else
            sda = new SqlDataAdapter("select PackageCode as MTCode, PackageName as Maintestname, 'P' as Tes from PackMst WHERE PackageCode like '%" + prefixText + "%' or PackageName like '%" + prefixText + "%' UNION " +
                                " select MTCode, Maintestname, 'T' as Tes from MainTest WHERE ISTestActive=1 and MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%' order by Maintestname ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["MTCode"] + " - " + dr["Maintestname"] + " - " + dr["Tes"], i);
            i++;
        }

        return tests;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtRoutinTest.Text != "")
            {
                string[] SplitTest;
                SplitTest = txtRoutinTest.Text.Split('-');

                if (Convert.ToInt32(ViewState["Editflag"]) == 1)
                {
                    RoutinTest_C Obj_Routin = new RoutinTest_C();
                    Obj_Routin.routinTestCode = SplitTest[0] + "-" + SplitTest[2];
                    if (SplitTest.Length > 1)
                    {
                        Obj_Routin.Update(Convert.ToInt32(ViewState["rid"]), SplitTest[1] + "-" + SplitTest[2], Convert.ToInt32(Session["Branchid"]));
                    }
                    Label2.Visible = true;
                    Label2.Text = "record Updated successfully.";
                    bindgrid();
                    ViewState["Editflag"] = null;

                }
                else
                {

                    RoutinTest_C Obj_Routin = new RoutinTest_C();
                    if (SplitTest.Length == 2)
                    {
                        Obj_Routin.routinTestName = SplitTest[1] + "-" + SplitTest[2];
                    }
                    else
                    {
                        Obj_Routin.routinTestName = SplitTest[1] + "-" + SplitTest[2];
                    }
                    Obj_Routin.routinTestCode = SplitTest[0] + "-" + SplitTest[2];
                    Obj_Routin.P_username = Convert.ToString(Session["username"]);
                    Obj_Routin.Insert(Convert.ToInt32(Session["Branchid"]));
                    Label2.Visible = true;
                    Label2.Text = "Record Saved successfully.";
                    bindgrid();

                }
                txtRoutinTest.Text = "";
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
    protected void RoutinTest_Grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ViewState["rid"] = RoutinTest_Grid.DataKeys[e.NewEditIndex].Value;
            RoutinTest_C samp = new RoutinTest_C(Convert.ToInt32(ViewState["rid"]), Convert.ToInt32(Session["Branchid"]));
            txtRoutinTest.Text = samp.routinTestName;
            ViewState["Editflag"] = 1;

            Label2.Visible = false;
            e.Cancel = true;
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
    protected void RoutinTest_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            RoutinTest_Grid.PageIndex = e.NewPageIndex;
            bindgrid();
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
    protected void RoutinTest_Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int reaid = Convert.ToInt32(RoutinTest_Grid.DataKeys[e.RowIndex].Value);
            RoutinTest_C sampl = new RoutinTest_C();
            sampl.delete(reaid, Convert.ToInt32(Session["Branchid"]));
            bindgrid();
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvr in RoutinTest_Grid.Rows)
            {
                CheckBox chk = gvr.FindControl("chk") as CheckBox;
                if (chk.Checked)
                {
                    int reaid = Convert.ToInt32(RoutinTest_Grid.DataKeys[gvr.RowIndex].Value);
                    RoutinTest_C sampl = new RoutinTest_C();
                    sampl.delete(reaid, Convert.ToInt32(Session["Branchid"]));
                }
            }
            bindgrid();
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