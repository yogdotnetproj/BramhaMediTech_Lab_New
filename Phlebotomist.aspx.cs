using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Drawing;


public partial class Phlebotomist :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    string Center_Code = "", patientName = "", Reg_no = "", labcode_main = "", Barcode = "",NewBarcode="",PPID="";
    object fromDate = null, toDate = null;
    Patmst_Bal_C PBC = new Patmst_Bal_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Patmst_New_Bal_C PatNB = new Patmst_New_Bal_C();
   
    string Barcode_ID = "";
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
                        checkexistpageright("Phlebotomist.aspx");
                    }
                }
                ViewState["regno"] = "";
                if (Session["usertype"].ToString() == "CollectionCenter")
                {
                    Center_Code = Session["CenterCode"].ToString().Trim();
                   // Txt_Center.Visible = false;
                    
                }
                else
                {
                    Txt_Center.Text = "All";
                    Session["CenterCode"] = "";
                    Center_Code = Session["CenterCode"].ToString().Trim();
                   
                }
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");

               
                dt = new DataTable();
                dt = PatNB.GetoutsourceLab(Convert.ToInt32(Session["Branchid"]));
                
                if (Session["DigModule"].ToString() == "1")
                {
                    GV_Phlebotomist.Columns[12].Visible = false;
                }
                if (Session["usertype"].ToString() == "CollectionCenter" && Session["username"] != null)
                {
                   
                    // Label4.Visible = false;
                    createuserTable_Bal_C Obj_CTB = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));

                    DrMT_Bal_C DrMT = new DrMT_Bal_C(Obj_CTB.CenterCode, "CC", Convert.ToInt32(Session["Branchid"]));
                    Session["CenterCode"] = Obj_CTB.CenterCode;


                    Txt_Center.Enabled = false;
                }
              FillGrid();
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
        if (Session["usertype"].ToString() == "CollectionCenter" && Session["username"] != null)
        {

            // Label4.Visible = false;
            createuserTable_Bal_C Obj_CTB = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));

            DrMT_Bal_C DrMT = new DrMT_Bal_C(Obj_CTB.CenterCode, "CC", Convert.ToInt32(Session["Branchid"]));
            Session["CenterCode"] = Obj_CTB.CenterCode;

            Center_Code = Obj_CTB.CenterCode;
            Txt_Center.Enabled = false;
        }
    }



    [WebMethod]
    [ScriptMethod]
    public static string[] Fillcollection(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixTextNew + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixTextNew + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["DoctorName"], i);
            i++;
        }
        return tests;
    }

    public void FillGrid()
    {
        try
        {
            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
            if (labcode != null && labcode != "")
            {
                labcode_main = labcode;
            }
            if (Txt_Center.Text.Trim() != "")
            {
                Center_Code = Session["CenterCode"].ToString().Trim();
            }
            if (fromdate.Text.Trim() != "" && todate.Text.Trim() != "")
            {
                fromDate = fromdate.Text.Trim();
                toDate = todate.Text.Trim();
            }
            if (txtregno.Text.Trim() != "")
            {
                Reg_no = txtregno.Text.Trim();
            }
            if (txtPatientName.Text.Trim() != "")
            {
                patientName = txtPatientName.Text.Trim().Replace("'", "'+char(39)+'"); ;
               // string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
            }
            
                Barcode = "";
              Barcode=  RblPhenoStatus.SelectedItem.Text;
              if (Barcode == "Accept")
                 {
                Barcode = "1";
                 }
              if (Barcode == "Pending")
              {
                  Barcode = "0";
              }
            if (txtbarcodeNo.Text.Trim() != "")
            {
                NewBarcode = txtbarcodeNo.Text.Trim();
            }
            if (txtPPID.Text.Trim() != "")
            {
                PPID = txtPPID.Text.Trim();
            }
            
              if (Convert.ToString(Session["UserType"]) == "Main Doctor" || Convert.ToString(Session["UserType"]) == "Technician" || Convert.ToString(Session["UserType"]) == "Reporting")
            {
                PatNB.AlterViewvw_VW_testmgr_nNew(fromDate, toDate);
                GV_Phlebotomist.DataSource = PatNB.Get_Phlebotomist_MainDoc(labcode, fromDate, toDate, patientName, Reg_no, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["DigModule"]), "", 0, Session["UserName"].ToString(), Session["UserType"].ToString(), Center_Code, Barcode, NewBarcode,PPID);
            }
            else
            {
                PatNB.AlterViewvw_VW_testmgr_nMain(fromDate, toDate);
                GV_Phlebotomist.DataSource = PatNB.Get_Phlebotomist(Center_Code, fromDate, toDate, patientName, Reg_no, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["DigModule"]), "", 0, Session["UserName"].ToString(), Session["UserType"].ToString(), Barcode, txtmobileno.Text, NewBarcode,PPID);
            }
            GV_Phlebotomist.DataBind();
            dt = new DataTable();
            dt = PatNB.GetoutsourceLab(Convert.ToInt32(Session["Branchid"]));

            int k = 1;
            string PID = "";
            string regno = "";
            string autovail = "";
            for (int i = 0; i < GV_Phlebotomist.Rows.Count; i++)
            {
                bool Emg = Convert.ToBoolean( (GV_Phlebotomist.Rows[i].FindControl("isemergency") as HiddenField).Value);
                if (Emg == true)
                {
                    (GV_Phlebotomist.Rows[i].FindControl("btnEmergency") as ImageButton).Visible = true;
                }
                else
                {
                    (GV_Phlebotomist.Rows[i].FindControl("btnEmergency") as ImageButton).Visible = false;
                }
                regno = GV_Phlebotomist.Rows[i].Cells[0].Text.Trim();

                if (i > 0)
                {
                    if (GV_Phlebotomist.DataKeys[i].Value.ToString().Trim() == GV_Phlebotomist.DataKeys[i - 1].Value.ToString().Trim())
                    {
                        ViewState["PID"] = GV_Phlebotomist.DataKeys[i].Value.ToString().Trim();
                        GV_Phlebotomist.Rows[i].Cells[0].Text = "";
                        GV_Phlebotomist.Rows[i].Cells[1].Text = "";
                        GV_Phlebotomist.Rows[i].Cells[2].Text = "";
                        GV_Phlebotomist.Rows[i].Cells[3].Text = "";
                        GV_Phlebotomist.Rows[i].Cells[4].Text = "";
                        GV_Phlebotomist.Rows[i].Cells[5].Text = "";
                        GV_Phlebotomist.Rows[i].Cells[6].Text = "";

                       // GV_Phlebotomist.Rows[i].BorderStyle = BorderStyle.Inset;
                        string ss = (GV_Phlebotomist.Rows[i].FindControl("hdnstatus") as HiddenField).Value;
                        if (ss == "2")
                        {
                            (GV_Phlebotomist.Rows[i].FindControl("rdreject") as RadioButton).Checked = true;
                            (GV_Phlebotomist.Rows[i].FindControl("rdaccept") as RadioButton).Checked = false;
                           
                        }
                       
                    }
                    else if (ViewState["PID"].ToString() == GV_Phlebotomist.DataKeys[i].Value.ToString())
                    {
                        GV_Phlebotomist.Rows[i].Cells[0].Text = "";
                        GV_Phlebotomist.Rows[i].Cells[1].Text = "";

                        GV_Phlebotomist.Rows[i].Cells[2].Text = "";
                        GV_Phlebotomist.Rows[i].Cells[3].Text = "";
                        GV_Phlebotomist.Rows[i].Cells[4].Text = "";
                        GV_Phlebotomist.Rows[i].Cells[5].Text = "";
                        GV_Phlebotomist.Rows[i].Cells[6].Text = "";

                       
                       // GV_Phlebotomist.Rows[i].BorderStyle = BorderStyle.Inset;
                        string ss = (GV_Phlebotomist.Rows[i].FindControl("hdnstatus") as HiddenField).Value;
                        if (ss == "2")
                        {
                            (GV_Phlebotomist.Rows[i].FindControl("rdreject") as RadioButton).Checked = true;
                            (GV_Phlebotomist.Rows[i].FindControl("rdaccept") as RadioButton).Checked = false;
                           
                        }
                       
                    }
                    else
                    {
                        k = 1;
                        Label lbltest = GV_Phlebotomist.Rows[i].FindControl("lblTest") as Label;
                        (GV_Phlebotomist.Rows[i].FindControl("label20") as HiddenField).Value = Convert.ToString(PBC.getcomplimentamount(GV_Phlebotomist.Rows[i].Cells[0].Text.Trim(), "", Convert.ToInt32(GV_Phlebotomist.DataKeys[i].Value.ToString()), Session["Branchid"].ToString(), lbltest.Text));
                        string ss = (GV_Phlebotomist.Rows[i].FindControl("hdnstatus") as HiddenField).Value;
                        if (ss == "2")
                        {
                            (GV_Phlebotomist.Rows[i].FindControl("rdreject") as RadioButton).Checked = true;
                            (GV_Phlebotomist.Rows[i].FindControl("rdaccept") as RadioButton).Checked = false;
                            
                        }
                       
                    }
                }
                else
                {
                    Label lbltest = GV_Phlebotomist.Rows[i].FindControl("lblTest") as Label;
                    (GV_Phlebotomist.Rows[i].FindControl("label20") as HiddenField).Value = Convert.ToString(PBC.getcomplimentamount(GV_Phlebotomist.Rows[i].Cells[0].Text.Trim(), "", Convert.ToInt32(GV_Phlebotomist.DataKeys[i].Value.ToString()), Session["Branchid"].ToString(), lbltest.Text));
                    ViewState["PID"] = GV_Phlebotomist.DataKeys[i].Value.ToString().Trim();
                    if ((GV_Phlebotomist.Rows[i].FindControl("hdnstatus") as HiddenField).Value == "2")
                    {
                        (GV_Phlebotomist.Rows[i].FindControl("rdreject") as RadioButton).Checked = true;
                        (GV_Phlebotomist.Rows[i].FindControl("rdaccept") as RadioButton).Checked = false;
                       
                    }
                    
                }
                TextBox tb = GV_Phlebotomist.Rows[i].FindControl("txtBarcodeid") as TextBox;

                #region  Is Empty Generete New

                if (tb.Text == "")
                {
                    Label lbltest = GV_Phlebotomist.Rows[i].FindControl("lblTest") as Label;
                    string[] cudate = DateTime.Now.ToShortDateString().Split('/');
                    cudate[2] = cudate[2].Substring(2, 2);
                    if (regno == "&nbsp;" || regno == "")
                    {
                        autovail = cudate[2] + PID + k.ToString();
                        (GV_Phlebotomist.Rows[i].FindControl("txtBarcodeid") as TextBox).Text = autovail;
                    }
                    else if ( regno != "")
                    {
                        if (Convert.ToString(ViewState["SampleTemp"]) != (GV_Phlebotomist.Rows[i].FindControl("hdnsampletype") as Label).Text)
                        {
                            autovail = cudate[2] + regno + k.ToString();
                            (GV_Phlebotomist.Rows[i].FindControl("txtBarcodeid") as TextBox).Text = autovail;
                        }
                        else
                        {
                            (GV_Phlebotomist.Rows[i].FindControl("txtBarcodeid") as TextBox).Text = autovail;
                        }
                    }
                    else
                    {
                        if (Convert.ToString(ViewState["SampleTemp"]) != (GV_Phlebotomist.Rows[i].FindControl("hdnsampletype") as Label).Text)
                        {
                            autovail = cudate[2] + regno + k.ToString();
                        }
                        (GV_Phlebotomist.Rows[i].FindControl("txtBarcodeid") as TextBox).Text = autovail;
                    }
                    PatNB.UpdateBarCode_Fix(Convert.ToString(autovail), Convert.ToString(GV_Phlebotomist.DataKeys[i].Values[1].ToString().Trim()), Convert.ToInt32(0), Convert.ToString(Session["Branchid"]));
                }

                #endregion
             
                if (Convert.ToString( ViewState["SampleTemp"]) != (GV_Phlebotomist.Rows[i].FindControl("hdnsampletype") as Label).Text)
                {
                   
                    k = k + 1;
                }

                ViewState["SampleTemp"] = (GV_Phlebotomist.Rows[i].FindControl("hdnsampletype") as Label).Text;
                #region for color

                if ((GV_Phlebotomist.Rows[i].FindControl("hdnstatus") as HiddenField).Value == "0" || (GV_Phlebotomist.Rows[i].FindControl("hdnstatus") as HiddenField).Value == "0")
                {
                    try
                    {
                        (GV_Phlebotomist.Rows[i].FindControl("rdaccept") as RadioButton).Enabled = true;
                        (GV_Phlebotomist.Rows[i].FindControl("rdreject") as RadioButton).Enabled = true;
                    }
                    catch { }
                   // GV_Phlebotomist.Rows[i].ForeColor = Color.Purple;
                }
                else
                {
                    //GV_Phlebotomist.Rows[i].ForeColor = Color.Blue;
                }
                if ((GV_Phlebotomist.Rows[i].FindControl("hdnstatus") as HiddenField).Value == "1")
                {
                    try
                    {
                        (GV_Phlebotomist.Rows[i].FindControl("rdaccept") as RadioButton).Enabled = false;
                        (GV_Phlebotomist.Rows[i].FindControl("rdreject") as RadioButton).Enabled = false;
                        (GV_Phlebotomist.Rows[i].FindControl("rdaccept") as RadioButton).ForeColor = System.Drawing.Color.Green;
                        (GV_Phlebotomist.Rows[i].FindControl("rdreject") as RadioButton).ForeColor = System.Drawing.Color.Green;
                    }
                    catch { }
                   // GV_Phlebotomist.Rows[i].ForeColor = Color.Green;
                }
                if ((GV_Phlebotomist.Rows[i].FindControl("hdnstatus") as HiddenField).Value == "2")
                {
                    try
                    {
                       // (GV_Phlebotomist.Rows[i].FindControl("rdaccept") as RadioButton).Enabled = false;
                        (GV_Phlebotomist.Rows[i].FindControl("rdreject") as RadioButton).Enabled = false;
                        (GV_Phlebotomist.Rows[i].FindControl("rdaccept") as RadioButton).ForeColor = System.Drawing.Color.Red;
                        (GV_Phlebotomist.Rows[i].FindControl("rdreject") as RadioButton).ForeColor = System.Drawing.Color.Red;
                    }
                    catch { }
                    //GV_Phlebotomist.Rows[i].ForeColor = Color.Green;
                }
               
                if ((GV_Phlebotomist.Rows[i].FindControl("hdnstatus") as HiddenField).Value != "0")
                {
                    GV_Phlebotomist.Rows[i].Cells[10].Enabled = false;
                    tb.Enabled = false;
                }
                #endregion
                DropDownList ddloutsourceLab = GV_Phlebotomist.Rows[i].FindControl("ddloutsourceLab") as DropDownList;
                if (dt.Rows.Count > 0)
                {
                    ddloutsourceLab.DataSource = dt;
                    ddloutsourceLab.DataTextField = "OutsourceLabName";
                    ddloutsourceLab.DataValueField = "Id";
                    ddloutsourceLab.DataBind();
                    ddloutsourceLab.Items.Insert(0, "-Select Lab-");
                    ddloutsourceLab.SelectedIndex = 0;
                }
                int outlab = Convert.ToInt32((GV_Phlebotomist.Rows[i].FindControl("hdnoutsourceLab") as HiddenField).Value);
                if (outlab > 0)
                {
                    ddloutsourceLab.SelectedValue = Convert.ToString( outlab);
                    GV_Phlebotomist.Rows[i].ForeColor = Color.Red;
                }
               // 

            }
            


        }
        catch (Exception exx)
        {

        }
        //e.Cancel = true;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void Txt_Center_TextChanged(object sender, EventArgs e)
    {
        string C_Code = DrMT_sign_Bal_C.Get_C_Code(Txt_Center.Text.Trim(), Convert.ToInt32(Session["Branchid"]));
        Session["CenterCode"] = C_Code;
        Center_Code = Session["CenterCode"].ToString().Trim();
    }

    protected void GV_Phlebotomist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Phlebotomist.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void btnIPrint_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < GV_Phlebotomist.Rows.Count; i++)
            {
                Button DelOk = (Button)GV_Phlebotomist.Rows[i].FindControl("btnIPrint");
                if (DelOk == (Button)sender)
                {
                   string regNo="",FID="";
                    string STCODE = DelOk.CommandArgument;
                    int PID = Convert.ToInt32(GV_Phlebotomist.DataKeys[i].Value.ToString().Trim());
                    int PID1 = Convert.ToInt32(GV_Phlebotomist.DataKeys[i].Values["SrNo"].ToString().Trim());
                    regNo = Convert.ToString(GV_Phlebotomist.DataKeys[i].Values["PatRegID"].ToString().Trim());
                    string TestCodesT = (GV_Phlebotomist.Rows[i].FindControl("lblTestCodes") as Label).Text.ToString().Trim();
                    string sampletypeT = (GV_Phlebotomist.Rows[i].FindControl("hdnsampletype") as Label).Text.ToString().Trim();

                    FID = (GV_Phlebotomist.Rows[i].FindControl("hdnfid") as HiddenField).Value;
                    Barcode_C ObjBCC = new Barcode_C();
                    DropDownList ddloutlab = GV_Phlebotomist.Rows[i].FindControl("ddloutsourceLab") as DropDownList;
                    TextBox txtbarcode = GV_Phlebotomist.Rows[i].FindControl("txtBarcodeid") as TextBox;
                    string CenterCode = GV_Phlebotomist.Rows[i].Cells[6].Text;
                    Response.Redirect("showDemographic.aspx?PID=" + PID + "&Center=" + CenterCode + "&FType=Edit&PatRegID=" + regNo + "&FID=" + FID + "", false);
                    if (Barcode_C.IsbarcodeIdExist_Previous_Patient(txtbarcode.Text, Convert.ToInt32(Session["Branchid"]), PID) == true)
                    {
                        // lblRequiredField.Visible = true;
                        // lblRequiredField.Text = "Barcode already exists";
                        // flag = true;
                        // ViewState["flag"] = "1";
                        txtbarcode.BorderColor = System.Drawing.Color.Red;
                      //  e.Cancel = true;
                        return;
                    } 
                    //if ((GV_Phlebotomist.Rows[i].FindControl("rdaccept") as RadioButton).Checked == true)
                    //{

                        // ..........ObjBCC.UpdateIspheboAccept_TestWise_Firstway(Convert.ToInt32(PID), Convert.ToInt32(Session["Branchid"]), 1, PID1, TestCodesT, Convert.ToString(Session["username"]));
                        //ObjBCC.Insert_Update_SpecimanNo(Convert.ToInt32(Session["Branchid"]), TestCodesT, PID);
                        //if (ddloutlab.SelectedIndex > 0)
                        //{
                        //    ObjBCC.UpdateoutsourceLab(Convert.ToInt32(PID), TestCodesT, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ddloutlab.SelectedValue), PID1);
                        //}
                    //}
                    //else
                    //{
                    //    ObjBCC.UpdateIspheboAccept_TestWise_Firstway(Convert.ToInt32(PID), Convert.ToInt32(Session["Branchid"]), 2, PID1, TestCodesT, Convert.ToString(Session["username"]));
                    //}

                    try
                    {
                      

                        //string[] urlArr = new string[1];
                        //string[] targetArr = new string[1];
                        //string[] featuresArr = new string[1];
                        //targetArr[0] = "1";
                        //featuresArr[0] = "";
                        //urlArr[0] = "BarcodePrint.aspx?PID=" + PID + "&Branchid=" + Session["Branchid"].ToString() + "&PatRegID=" + regNo + "&FID=" + FID + "&TestCode=" + TestCodesT + "&SampleType=" + sampletypeT + "";
                        //ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
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
                    //FillGrid();
                   
                }
            }
           
        }
        catch (Exception exc)
        {
            Response.Cookies["error"].Value = exc.Message;
            Server.Transfer("~/ErrorMessage.aspx");
        }

    }

  


    protected void GV_Phlebotomist_RowEditing1(object sender, GridViewEditEventArgs e)
    {
       
        int PID = Convert.ToInt32(GV_Phlebotomist.DataKeys[e.NewEditIndex].Values["PID"].ToString().Trim());
        int PID1 = Convert.ToInt32(GV_Phlebotomist.DataKeys[e.NewEditIndex].Values["SrNo"].ToString().Trim());
        string regNo = Convert.ToString(GV_Phlebotomist.DataKeys[e.NewEditIndex].Values["PatRegID"].ToString().Trim());
       
        string FID = (GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("hdnfid") as HiddenField).Value.ToString().Trim();
        string PackageCode = "";
        string TestCodesT = (GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("lblTestCodes") as Label).Text.ToString().Trim();
        string sampletypeT = (GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("hdnsampletype") as Label).Text.ToString().Trim();
        Barcode_C ObjBCC = new Barcode_C();
        DropDownList ddloutlab = GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("ddloutsourceLab") as DropDownList;
        TextBox txtbarcode = GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("txtBarcodeid") as TextBox;
        if (Barcode_C.IsbarcodeIdExist_Previous_Patient(txtbarcode.Text, Convert.ToInt32(Session["Branchid"]), PID) == true)
        {
           // lblRequiredField.Visible = true;
           // lblRequiredField.Text = "Barcode already exists";
           // flag = true;
           // ViewState["flag"] = "1";
            txtbarcode.BorderColor = System.Drawing.Color.Red;
            e.Cancel = true;
            return;
        }
        ObjTB.P_Patregno = Convert.ToString(regNo); ;
        ObjTB.P_FormName = "Phlebotomist Accept";
        ObjTB.P_EventName = "Sample Accept";
        ObjTB.P_UserName = Convert.ToString(Session["username"]);
        ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
        ObjTB.Insert_DailyActivity();


        //if ((GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("rdaccept") as RadioButton).Checked == true)
        //{
        ObjBCC.UpdateIspheboAccept_TestWise_Firstway(Convert.ToInt32(PID), Convert.ToInt32(Session["Branchid"]), 1, PID1, TestCodesT, Convert.ToString(Session["username"]), txtbarcode.Text);
            //ObjBCC.Insert_Update_SpecimanNo(Convert.ToInt32(Session["Branchid"]), TestCodesT, PID);
            //if (ddloutlab.SelectedIndex > 0)
            //{
            //    ObjBCC.UpdateoutsourceLab(Convert.ToInt32(PID), TestCodesT, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ddloutlab.SelectedValue), PID1);
            //}
        //}
        //else
        //{
        //    ObjBCC.UpdateIspheboAccept_TestWise_Firstway(Convert.ToInt32(PID), Convert.ToInt32(Session["Branchid"]), 2, PID1, TestCodesT, Convert.ToString(Session["username"]));
        //}

        FillGrid();
        e.Cancel = true;
      
    }



    protected void GV_Phlebotomist_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
       

        int PID = Convert.ToInt32(GV_Phlebotomist.DataKeys[e.RowIndex].Values["PID"].ToString().Trim());
        int PID1 = Convert.ToInt32(GV_Phlebotomist.DataKeys[e.RowIndex].Values["SrNo"].ToString().Trim());
        string regNo = Convert.ToString(GV_Phlebotomist.DataKeys[e.RowIndex].Values["PatRegID"].ToString().Trim());

       
        string FID = (GV_Phlebotomist.Rows[e.RowIndex].FindControl("hdnfid") as HiddenField).Value.ToString().Trim();
        string TestCodesT = (GV_Phlebotomist.Rows[e.RowIndex].FindControl("lblTestCodes") as Label).Text.ToString().Trim();
        DataTable dtspe = new DataTable();
        Barcode_C ObjBCC = new Barcode_C();
        DropDownList ddloutlab = GV_Phlebotomist.Rows[e.RowIndex].FindControl("ddloutsourceLab") as DropDownList;
        TextBox txtbarcode = GV_Phlebotomist.Rows[e.RowIndex].FindControl("txtBarcodeid") as TextBox;
        //if ((GV_Phlebotomist.Rows[e.RowIndex].FindControl("rdaccept") as RadioButton).Checked == true)
        //{
        if (Barcode_C.IsbarcodeIdExist_Previous_Patient(txtbarcode.Text, Convert.ToInt32(Session["Branchid"]), PID) == true)
        {
            // lblRequiredField.Visible = true;
            // lblRequiredField.Text = "Barcode already exists";
            // flag = true;
            // ViewState["flag"] = "1";
            txtbarcode.BorderColor = System.Drawing.Color.Red;
            e.Cancel = true;
            return;
        }    

           dtspe= ObjBCC.GetTestCodeSpecimanNo_FirstWay(Convert.ToString(PID), Convert.ToInt32(Session["Branchid"]));
           if (dtspe.Rows.Count > 0)
           {
               for (int i = 0; i < dtspe.Rows.Count; i++)
               {
                  // ObjBCC.Insert_Update_SpecimanNo(Convert.ToInt32(Session["Branchid"]), Convert.ToString( dtspe.Rows[i]["MTCode"]), PID);
                   if (Convert.ToInt32(dtspe.Rows[i]["PhlebotomistCollect"]) == 0 && Convert.ToInt32(dtspe.Rows[i]["IspheboAccept"]) != 1)
                   {
                      //........... ObjBCC.UpdateIspheboAccept_PrintAll_Firstway(Convert.ToInt32(PID), Convert.ToInt32(Session["Branchid"]), 1, PID1, Convert.ToString(dtspe.Rows[i]["MTCode"]), Convert.ToString(Session["username"]));
                   }
               }
           }
           ObjTB.P_Patregno = Convert.ToString(regNo); ;
           ObjTB.P_FormName = "Phlebotomist Accept";
           ObjTB.P_EventName = "Sample Accept All";
           ObjTB.P_UserName = Convert.ToString(Session["username"]);
           ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
           ObjTB.Insert_DailyActivity();
            //if (ddloutlab.SelectedIndex > 0)
            //{
            //    ObjBCC.UpdateoutsourceLab(Convert.ToInt32(PID), TestCodesT, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ddloutlab.SelectedValue), PID1);
            //}
        //}
        //else
        //{
        //    ObjBCC.UpdateIspheboAccept_firstway(Convert.ToInt32(PID), Convert.ToInt32(Session["Branchid"]), 2, PID1, Convert.ToString(Session["username"]));
        //}

        string PackageCode = "";
        if (regNo == "&nbsp;" || regNo == "")
        {
            string fID = FinancialYearTableLogic.getCurrentFinancialYear().FinancialYearId;
           
            FID = fID ;
            regNo = Patmst_New_Bal_C.Get_Regno(FID, Convert.ToInt32(Session["Branchid"]),Convert.ToString(PID));
            
        }
       
        try
        {
            string[] urlArr = new string[1];
            string[] targetArr = new string[1];
            string[] featuresArr = new string[1];
            targetArr[0] = "1";
            featuresArr[0] = "";
            urlArr[0] = "BarcodePrint.aspx?PID=" + PID + "&Branchid=" + Session["Branchid"].ToString() + "&PatRegID=" + regNo + "&FID=" + FID + "";
            ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
            FillGrid();
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


    protected void GV_Phlebotomist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex == -1)
                return;

            
        }
        catch (Exception ex)
        { }
    }

  
    protected void GV_Phlebotomist_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

    }
   
    protected void rdaccept_CheckedChanged(object sender, EventArgs e)
    {
       
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