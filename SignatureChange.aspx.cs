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


public partial class SignatureChange : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    string Center_Code = "", patientName = "", Reg_no = "", labcode_main = "", Barcode = "";
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
                //LUNAME.Text = Convert.ToString(Session["username"]);
                //LblDCName.Text = Convert.ToString(Session["Bannername"]);
                //LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                dt = new DataTable();
                //dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                //this.PopulateTreeView(dt, 0, null);
                ViewState["regno"] = "";
                if (Session["usertype"].ToString() == "CollectionCenter")
                {
                    Center_Code = Session["CenterCode"].ToString().Trim();
                    Txt_Center.Visible = false;
                    
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
               // FillGrid();
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

   

    [WebMethod]
    [ScriptMethod]
    public static string[] Fillcollection(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixText + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixText + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
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
                patientName = txtPatientName.Text.Trim();
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


              PatNB.AlterViewvw_VW_SignatureChange(fromDate, toDate);
              GV_Phlebotomist.DataSource = PatNB.Get_SignatureChange(Center_Code, fromDate, toDate, patientName, Reg_no, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["DigModule"]), "", 0, Session["UserName"].ToString(), Session["UserType"].ToString(), Barcode, txtmobileno.Text);
            
            GV_Phlebotomist.DataBind();
            dt = new DataTable();
            dt = PatNB.GetFirstTechnicanName();

            int k = 1;
            string PID = "";
            string regno = "";
            string autovail = "";
            for (int i = 0; i < GV_Phlebotomist.Rows.Count; i++)
            {
                //bool Emg = Convert.ToBoolean( (GV_Phlebotomist.Rows[i].FindControl("isemergency") as HiddenField).Value);
                //if (Emg == true)
                //{
                //    (GV_Phlebotomist.Rows[i].FindControl("btnEmergency") as ImageButton).Visible = true;
                //}
                //else
                //{
                //    (GV_Phlebotomist.Rows[i].FindControl("btnEmergency") as ImageButton).Visible = false;
                //}
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
                        GV_Phlebotomist.Rows[i].Cells[7].Text = "";

                       // GV_Phlebotomist.Rows[i].BorderStyle = BorderStyle.Inset;
                       
                       
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
                        GV_Phlebotomist.Rows[i].Cells[7].Text = "";
                       
                       // GV_Phlebotomist.Rows[i].BorderStyle = BorderStyle.Inset;
                       
                       
                    }
                    else
                    {
                       
                       
                    }
                }
                else
                {
                    ViewState["PID"] = GV_Phlebotomist.DataKeys[i].Value.ToString().Trim();
                    
                }
                

                #region  Is Empty Generete New


                #endregion
             
             

                ViewState["SampleTemp"] = (GV_Phlebotomist.Rows[i].FindControl("hdnsampletype") as Label).Text;
                #region for color

                
                #endregion
                DropDownList ddlFirstTechnican = GV_Phlebotomist.Rows[i].FindControl("ddlFirstTechnican") as DropDownList;
                if (dt.Rows.Count > 0)
                {
                    ddlFirstTechnican.DataSource = dt;
                    ddlFirstTechnican.DataTextField = "Drsignature";
                    ddlFirstTechnican.DataValueField = "Signatureid";
                    ddlFirstTechnican.DataBind();
                    ddlFirstTechnican.Items.Insert(0, "-Select Tech-");
                    ddlFirstTechnican.SelectedIndex = 0;
                }
                DropDownList ddlSecondTechnican = GV_Phlebotomist.Rows[i].FindControl("ddlSecondTechnican") as DropDownList;
                if (dt.Rows.Count > 0)
                {
                    ddlSecondTechnican.DataSource = dt;
                    ddlSecondTechnican.DataTextField = "Drsignature";
                    ddlSecondTechnican.DataValueField = "Signatureid";
                    ddlSecondTechnican.DataBind();
                    ddlSecondTechnican.Items.Insert(0, "-Select Tech-");
                    ddlSecondTechnican.SelectedIndex = 0;
                }
                //int outlab = Convert.ToInt32((GV_Phlebotomist.Rows[i].FindControl("hdnoutsourceLab") as HiddenField).Value);
                //if (outlab > 0)
                //{
                //    ddloutsourceLab.SelectedValue = Convert.ToString( outlab);
                //    GV_Phlebotomist.Rows[i].ForeColor = Color.Red;
                //}
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
        

    }

  


    protected void GV_Phlebotomist_RowEditing1(object sender, GridViewEditEventArgs e)
    {
       
        int PID = Convert.ToInt32(GV_Phlebotomist.DataKeys[e.NewEditIndex].Values["PID"].ToString().Trim());
        int PID1 = Convert.ToInt32(GV_Phlebotomist.DataKeys[e.NewEditIndex].Values["SrNo"].ToString().Trim());
        string regNo = Convert.ToString(GV_Phlebotomist.DataKeys[e.NewEditIndex].Values["PatRegID"].ToString().Trim());
       
        string FID = (GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("hdnfid") as HiddenField).Value.ToString().Trim();
        string PackageCode = "";
        string TestCodesT = (GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("lblTestCodes") as Label).Text.ToString().Trim();
        //string sampletypeT = (GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("hdnsampletype") as Label).Text.ToString().Trim();
        //string SpecimanRemark = (GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("txtSpecimanRemarks") as TextBox).Text.ToString().Trim();
        //string ReqbyDoc = (GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("txtReqbyDoc") as TextBox).Text.ToString().Trim();
        Barcode_C ObjBCC = new Barcode_C();
        DropDownList ddlFirstTechnican = GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("ddlFirstTechnican") as DropDownList;

        DropDownList ddlSecondTechnican = GV_Phlebotomist.Rows[e.NewEditIndex].FindControl("ddlSecondTechnican") as DropDownList;
      
            //ObjBCC.UpdateIspheboAccept_TestWise(Convert.ToInt32(PID), Convert.ToInt32(Session["Branchid"]), 1, PID1, TestCodesT, Convert.ToString(Session["username"]), SpecimanRemark, ReqbyDoc);
        if (ddlFirstTechnican.SelectedIndex > 0)
        {
            ObjBCC.UpdateFirst_Signature(Convert.ToInt32(PID), TestCodesT, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ddlFirstTechnican.SelectedValue), PID1);
        }
        if (ddlSecondTechnican.SelectedIndex > 0)
        {
            ObjBCC.UpdateSecond_Signature(Convert.ToInt32(PID), TestCodesT, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ddlSecondTechnican.SelectedValue), PID1);
        }

        FillGrid();
        e.Cancel = true;
      
    }
    protected void GV_Phlebotomist_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
       

      
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
   
}