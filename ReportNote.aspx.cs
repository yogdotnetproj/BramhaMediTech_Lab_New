using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Winthusiasm.HtmlEditor;

using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;

public partial class ReportNote :BasePage
{
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    TreeviewBind_C ObjTB = new TreeviewBind_C();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            try
            {
              
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

    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ViewState["updateflag"]) != 1)
            {
                if (TestspecialnoteLogic_Bal_C.is_MTCodeExists(Convert.ToString(ViewState["MTCODE"]), Convert.ToInt32(Session["Branchid"])))
                {

                    Testspecialnote_Bal_C Obj_TsPN = new Testspecialnote_Bal_C(Convert.ToString(ViewState["MTCODE"]), Convert.ToInt32(Session["Branchid"]));

                    Obj_TsPN.Name = Convert.ToString(ViewState["MTCODE"]);

                    //Obj_TsPN.SpecialNote = Editor.Value.Replace("<p>&nbsp;</p>", "<br />");// Editor.Value;
                    Obj_TsPN.SpecialNote = Editor.Text.Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
                    Obj_TsPN.MTCode = Convert.ToString(ViewState["MTCODE"]);
                    Obj_TsPN.SNFlag = true;
                    Obj_TsPN.updatrReportNote(Convert.ToInt32(Session["Branchid"]), Obj_TsPN.SpecialNote);
                    //Obj_TsPN.Update(Convert.ToInt32(Session["Branchid"]), Obj_TsPN.SpecialNote);
                    Label3.Visible = true;
                    Label3.Text = "Record Updated successfully.";
                }
                else
                {
                    Testspecialnote_Bal_C Obj_TsPN = new Testspecialnote_Bal_C();

                    Obj_TsPN.Name = Convert.ToString(ViewState["MTCODE"]);
                    Obj_TsPN.SNFlag = true;
                    Obj_TsPN.MTCode = Convert.ToString(ViewState["MTCODE"]);

                    Obj_TsPN.SpecialNote = Editor.Text.Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");// Editor.Value;
                    Obj_TsPN.SNFlag = true;
                    Obj_TsPN.P_username = Convert.ToString(Session["username"]);
                    Obj_TsPN.Insert(Convert.ToInt32(Session["Branchid"]));
                    Label3.Visible = true;
                    Label3.Text = "Record Saved successfully.";
                }
            }
            else
            {
                Testspecialnote_Bal_C Obj_TsPN = new Testspecialnote_Bal_C(Convert.ToString(ViewState["MTCODE"]), Convert.ToInt32(Session["Branchid"]));

                Obj_TsPN.Name = Convert.ToString(ViewState["MTCODE"]);

                Obj_TsPN.SpecialNote = Editor.Text.Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");// Editor.Value;
                Obj_TsPN.MTCode = Convert.ToString(ViewState["MTCODE"]);
                Obj_TsPN.SNFlag = true;
                Obj_TsPN.updatrReportNote(Convert.ToInt32(Session["Branchid"]), Obj_TsPN.SpecialNote);

                Label3.Visible = true;
                Label3.Text = "Record Updated successfully.";
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


        Label3.Visible = true;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ViewState["list"] = null;
        Editor.Text = "";
        ViewState["updateflag"] = 0;
        txttestcode.Text = "";
    }


    [WebMethod]
    [ScriptMethod]
    public static string[] FillDoctor(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;

        if (HttpContext.Current.Session["usertype"].ToString() == "CollectionCenter")
        {
            sda = new SqlDataAdapter("Select * from MainTest   where branchid=1   Maintestname like '%" + prefixText + "%' order by MTCode", con);

        }
        else
        {
            sda = new SqlDataAdapter("Select * from MainTest   where branchid=1  and  Maintestname  like '%" + prefixText + "%' order by MTCode", con);

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
    protected void txttestcode_TextChanged(object sender, EventArgs e)
    {
        if (txttestcode.Text != "")
        {
            try
            {
                string MTCode = "";
                string[] MTCodeT = new string[] { "", "" };
                MTCodeT = txttestcode.Text.Split('=');
                if (MTCodeT.Length > 1)
                {
                    MTCode = MTCodeT[0];
                    ViewState["MTCODE"] = MTCode;

                    try
                    {
                        Testspecialnote_Bal_C OBJ_spnote = new Testspecialnote_Bal_C(Convert.ToString(ViewState["MTCODE"]), 1, Convert.ToInt32(Session["Branchid"]));
                        Editor.Text = OBJ_spnote.SpecialNote;
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
    protected void Btn_Add_Dept_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubDeptAdd.aspx", false);
    }
    protected void Btn_Add_Test_Click(object sender, EventArgs e)
    {

        // Response.Redirect("ShowTest.aspx", false);
        Response.Redirect("~/AddTest.aspx", false);
    }
    protected void Btn_Add_NR_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReferanceRange.aspx", false);
    }
    protected void Btn_Add_PK_Click(object sender, EventArgs e)
    {
        Response.Redirect("Showpackage.aspx", false);
    }
    protected void Btn_Add_Sample_Click(object sender, EventArgs e)
    {
        Response.Redirect("SampleType.aspx", false);

    }
    protected void Btn_Add_ShortCut_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShortCut.aspx", false);
    }
    protected void Btn_Add_Formula_Click(object sender, EventArgs e)
    {
        Response.Redirect("TestFormulasetting.aspx", false);
    }
    protected void Btn_Add_RN_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportNote.aspx", false);
    }
    protected void btnedittest_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowTest.aspx", false);
    }
}