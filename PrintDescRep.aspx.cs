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
using System.Drawing;


using System.Net.Http;


using System.Drawing.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.Drawing.Imaging;

public partial class PrintDescRep :BasePage
{
    DataTable dt = new DataTable();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    PatSt_Bal_C psnew = new PatSt_Bal_C();
    TestDescriptiveResult_b ObjTDR = new TestDescriptiveResult_b();
    public bool list = false;
    string tlcd = "";
    public string FID = "01";
    private string testnametm;
    public string Testnametm
    {
        get { return testnametm; }
        set { testnametm = value; }
    }
    private string _STCodeT;
    public string STCodeT
    {
        get { return _STCodeT; }
        set { _STCodeT = value; }
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
        if (!Page.IsPostBack)
        {
            BindDescRep();
        }
    }
    public void BindDescRep()
    {
       dt= ObjTDR.Get_DescResult();
       if (dt.Rows.Count > 0)
       {
           string mhtml = "";
            mhtml = dt.Rows[0]["TextDesc"].ToString().Replace("<b>", "<strong>").Replace("</b>", "</strong>").Replace("<b>", "<STRONG>").Replace("</b>", "</STRONG>").Replace("#FFFFFF", "#000000").Replace("#ffffff", "#000000");

            Editor1.Text = mhtml + "" + dt.Rows[0]["SignImage"];
       }
    }
}