using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using Winthusiasm.HtmlEditor;

using System.Web.Management;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Xml;
using System.Text;
using System.Threading;
using System.IO;

public partial class TestDescriptiveResult_Print :BasePage
{
    DataTable dt = new DataTable();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    public bool list = false;
    string T_Code = "";
    public string FID = "01-";
    private string testnametm;
    public string Testnametm
    {
        get { return testnametm; }
        set { testnametm = value; }
    }
    private string testcode;
    public string Testcode
    {
        get { return testcode; }
        set { testcode = value; }
    }
    private string fid;
    public string Fid
    {
        get { return fid; }
        set { fid = value; }
    }
    private string _PatRegID;
    public string PatRegID
    {
        get { return _PatRegID; }
        set { _PatRegID = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Request.QueryString["MTCode"] != null && Request.QueryString["PatRegID"] == null && Request.QueryString["FID"] == null)
        {
            Testcode = Request.QueryString["MTCode"];           
            T_Code = Request.QueryString["MTCode"].ToString();

           

        }
        else
        {

            PatRegID = Request.QueryString["PatRegID"];
            fid = Request.QueryString["FID"];
            Testcode = Request.QueryString["MTCode"];
          

           


        }
        Fill_Labels();
        try
        {
            if (!Page.IsPostBack)
            {
                LUNAME.Text = Convert.ToString(Session["username"]);
                LblDCName.Text = Convert.ToString(Session["Bannername"]);
                LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                dt = new DataTable();
                dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                this.PopulateTreeView(dt, 0, null);

                TestDescriptiveResultPrint_b Obj_TDR_C = new TestDescriptiveResultPrint_b(PatRegID, fid,  Convert.ToInt32(Session["Branchid"]));

                if (Obj_TDR_C.TextDesc != "")
                {
                    string SavedString = Obj_TDR_C.TextDesc;
                    string mhtml = Obj_TDR_C.TextDesc.Replace("<b>", "<strong>").Replace("</b>", "</strong>").Replace("<b>", "<STRONG>").Replace("</b>", "</STRONG>").Replace("#FFFFFF", "#000000").Replace("#ffffff", "#000000");

                    Editor.Text = mhtml;
                    
                }

               



                string sRegno = PatRegID.ToString().Trim(); 
            }

        }
        catch
        {


        }
       
        if (Request.QueryString["FID"] != null)
            FID = Request.QueryString["FID"];
        string labcode1 = "";
        if (Session["UnitCode"] != null)
        {
            labcode1 = Session["UnitCode"] .ToString();
        }

    }
  

 

   
   

    public void Fill_Labels()
    {
        #region  Information Of Patient
        Patmst_Bal_C CIT = null;
        try
        {

            CIT = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));

            lblRegNo.Text = Convert.ToString(CIT.PatRegID);
            lbldate.Text = CIT.FID;

            lblName.Text = CIT.Initial.Trim() + "." + CIT.Patname;
            lblage.Text = Convert.ToString(CIT.Age) + "/" + CIT.MYD;
            lblSex.Text = CIT.Sex;

            LblMobileno.Text = CIT.Phone;
            Lblcenter.Text = CIT.CenterName;
            lbldate.Text = Convert.ToString(CIT.Patregdate);
            LblRefDoc.Text = CIT.Drname;
        }
        catch
        {
            lblRegNo.Visible = true;
            lblRegNo.Text = "Record not foud";
        }
        #endregion
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
}