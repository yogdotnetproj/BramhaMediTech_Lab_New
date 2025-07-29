using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Web.Services;


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

public partial class CS :BasePage
{
    [WebMethod()]
    public static bool SaveCapturedImage(string data)
    {
        string fileName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss") + "_" + Convert.ToInt32(HttpContext.Current.Session["PID_Img"]);

        //Convert Base64 Encoded string to Byte Array.
       string PPP = Convert.ToString( Convert.FromBase64String(data.Split(',')[1]));
        byte[] imageBytes = Convert.FromBase64String(data.Split(',')[1]);
        Byte[] imgByte1 = null;
        //Save the Byte Array as Image File.
        string filePath = HttpContext.Current.Server.MapPath(string.Format("~/Captures/{0}.jpg", fileName));
        File.WriteAllBytes(filePath, imageBytes);
        imgByte1 = imageBytes;

     //  string  imgByte11 = Convert.ToString( imageBytes);
        //string result = System.Text.Encoding.UTF8.GetString(data);

        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        int PatRegID = Convert.ToInt32(HttpContext.Current.Request.QueryString["PatRegID"]);
        int PID = Convert.ToInt32(HttpContext.Current.Session["PID_Img"]);
        int Fid = Convert.ToInt32(HttpContext.Current.Request.QueryString["Fid"]);
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc = new SqlCommand("" +
             "update patmst set  Image1=@Image1,ImagePath='" + fileName + ".jpg' where  PID=@PID ", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(PatRegID);

        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Fid;

        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(branchid);
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
       
        if (imgByte1 != null)
        {
            sc.Parameters.AddWithValue("@Image1", data);
        }
        else
        {
            //sc.Parameters.AddWithValue("@Image1", DBNull.Value);
            SqlParameter imageParameter = new SqlParameter("@Image1", SqlDbType.NText);
            imageParameter.Value = DBNull.Value;
            sc.Parameters.Add(imageParameter);
        }

        conn.Open();
        sc.ExecuteNonQuery();
        conn.Dispose();
        
        return true;
    }
}